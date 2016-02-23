using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using GreenZoneParser.Lexer;
using GreenZoneParser.Parsers.Cs;
using GreenZoneParser.Parsers;
using System.Collections;
using System.Drawing.Drawing2D;
using GreenZoneParser.Xml;

namespace GreenZoneParser
{
    public partial class ParseTesterForm : Form, IMessageFilter
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        Parser parser;
        Parser.TokenEventHandler tokenAdded;
        Parser.TokenPosEventHandler newlineAdded, tokenRead;
        Parser.NodeEventHandler nodeCreated, nodeAdded;
        Parser.ErrorEventHandler errorAdded, tmpErrorAdded;

        readonly SortedSet<int> breakPoints = new SortedSet<int>();

        Brush bpBrush;
        Brush bpInaBrush;

        ParserGuiBinder parserGuiBinder;

        public ParseTesterForm()
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
            posLabel.Text = "";
            lineLabel.Text = "";
            columnLabel.Text = "";
            bpBrush = new SolidBrush(Color.Red);
            bpInaBrush = new SolidBrush(Color.Gray);
            sourceTextBox.Paint += new PaintEventHandler(sourceTextBox_Paint);
        }

        internal int LineCount
        {
            get
            {
                return sourceTextBox.Lines.Length;
            }
        }

        bool ControlsEnabled
        {
            get
            {
                return fileTextBox.Enabled;
            }
            set
            {
                fileTextBox.Enabled = value;
                browseXmlButton.Enabled = value;
                browseCsButton.Enabled = value;
                loadButton.Enabled = value;
                sourceTextBox.ReadOnly = !value;
                showTokensChb.Enabled = value;
                debugTokensChb.Enabled = value;
                debugParseChb.Enabled = value;
                parseButton.Enabled = value;
                stepButton.Enabled = value;

                sourceTextBox.BackColor = SystemColors.Window;
            }
        }

        bool Debugging
        {
            get
            {
                return debugTokensChb.Checked || debugParseChb.Checked;
            }
        }

        internal void GoToLine(int line)
        {
            sourceTextBox.Focus();
            sourceTextBox.GoTo(line, 1, sourceTextBox.Lines[line - 1].Length);
        }

        internal bool Search(string text, StringComparison comp)
        {
            int i = sourceTextBox.Text.IndexOf(text, sourceTextBox.SelectionStart, comp);
            if (i == -1)
            {
                return false;
            }
            else
            {
                sourceTextBox.Select(i, text.Length);
                return true;
            }
        }

        void ShowSearchDialog()
        {
            SearchForm sf = new SearchForm(this);
            sf.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //fileTextBox.Text = "F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\GreenZoneFxEAnalyzer\\GreenZoneFxEngine\\Trading\\ExecutableRuntime.cs";
            fileTextBox.Text = "F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\GreenZoneFxEAnalyzer\\GreenZoneFxBaseGEx\\generated\\reflection-info-GreenZoneParser.xml";
            sourceTextBox.Text = File.ReadAllText(fileTextBox.Text);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            sourceTextBox.Text = File.ReadAllText(fileTextBox.Text);
        }

        private void parseButton_Click(object sender, EventArgs e)
        {

            if ((!debugTokensChb.Checked && !debugParseChb.Checked) || parser == null)
            {
                StartParse();
            }

            errLabel.BackColor = SystemColors.Control;
            nlLabel.BackColor = SystemColors.Control;
            tkLabel.BackColor = SystemColors.Control;
            prodLabel.BackColor = SystemColors.Control;
            addNodeLabel.BackColor = SystemColors.Control;

            if (this.breakPoints.Count > 0 && (debugTokensChb.Checked || debugParseChb.Checked))
            {
                bool justInit = false;

                if (!debugTokensChb.Checked && !parser.Tokenizer.Finished)
                {
                    parser.TokenizeAllFirst();

                    parserGuiBinder.UpdateTables(showTokensChb.Checked);

                    parserGuiBinder.GenerateLists();

                    justInit = true;
                }

                List<int> breakPoints = new List<int>(this.breakPoints);
                int ind = breakPoints.BinarySearch(parser.LastPosition);
                if (ind < 0)
                {
                    ind = ~ind;
                }
                if (ind < breakPoints.Count)
                {
                    int bp = breakPoints[ind];
                    int pos = sourceTextBox.GetFirstCharIndexFromLine(bp - 1);
                    if (!justInit || pos > 0)
                    {
                        parser.ParseToPosition(pos);
                    }
                }
                else
                {
                    parser.ParseToPosition(int.MaxValue);
                }

                infoLabel.Text = "Tokens : " + parser.Tokenizer.Result.Count;
                infoLabel.Text += "     Errors : " + parser.CompilationErrors.Count;
            }
            else
            {
                TreeNode root = null;
                if (parser is CsParser)
                {
                    MainBlockNode mainNode = (MainBlockNode)parser.Parse();

                    root = parserGuiBinder.CreateGuiNode(mainNode);
                    treeView1.Nodes.Add(root);
                }
                else
                {
                    XmlNode mainNode = (XmlNode)parser.Parse();

                    root = parserGuiBinder.CreateGuiNode(mainNode);
                    treeView1.Nodes.Add(root);
                }


                parserGuiBinder.UpdateTables(showTokensChb.Checked);

                parserGuiBinder.GenerateLists();

                infoLabel.Text = "Tokens : " + parser.Tokenizer.Result.Count;
                infoLabel.Text += "     Errors : " + parser.CompilationErrors.Count;

                ControlsEnabled = true;
            }
        }

        private void stepButton_Click(object sender, EventArgs e)
        {
            if (parser == null)
            {
                StartParse();
            }

            errLabel.BackColor = SystemColors.Control;
            nlLabel.BackColor = SystemColors.Control;
            tkLabel.BackColor = SystemColors.Control;
            prodLabel.BackColor = SystemColors.Control;
            addNodeLabel.BackColor = SystemColors.Control;

            if (!debugTokensChb.Checked)
            {
                if (!parser.Tokenizer.Finished)
                {
                    parser.TokenizeAllFirst();

                    parserGuiBinder.UpdateTables(showTokensChb.Checked);

                    parserGuiBinder.GenerateLists(true, true, false);
                }
            }

            infoLabel.Text = "Tokens : " + parser.Tokenizer.Result.Count;
            infoLabel.Text += "     Errors : " + parser.CompilationErrors.Count;

            parser.ParseNext();
        }

        void parser_ErrorAdded(string info, CompilationErrorEnty err, bool sync)
        {
            if (Debugging && sync)
            {
                parseButton.Enabled = true;
                stepButton.Enabled = true;
                parserGuiBinder.ClearPendingErrors();

                errLabel.BackColor = Color.Pink;
                int ri = parserGuiBinder.AddError(err);

                parserGuiBinder.GenerateLists(false, true, false);

                errorsTable.CurrentCell = errorsTable.Rows[ri].Cells[0];

                parserGuiBinder.GoTo(err);
            }
        }

        void parser_NewLineAdded(int position, bool sync)
        {
            if (Debugging && sync)
            {
                parseButton.Enabled = true;
                stepButton.Enabled = true;

                nlLabel.BackColor = Color.Pink;
            }
        }

        void parser_TokenRead(int position, bool sync)
        {
            if (Debugging && sync)
            {
                parseButton.Enabled = true;
                stepButton.Enabled = true;

                Parser.Pos pos = parser.FindPos(position);
                parserGuiBinder.GoTo(pos, 1);
            }
        }

        void parserr_TokenAdded(Token token, bool sync)
        {
            if (Debugging && sync)
            {
                parseButton.Enabled = true;
                stepButton.Enabled = true;

                tkLabel.BackColor = Color.Pink;
                Parser.Pos pos = parserGuiBinder.AddToken(token);

                parserGuiBinder.GenerateLists(true, false, false);

                parserGuiBinder.GoTo(pos, token.Length);
            }
        }

        void parser_NodeCreated(BaseNode child, bool sync)
        {
            if (Debugging && sync)
            {
                parseButton.Enabled = true;
                stepButton.Enabled = true;

                curProdInfLabel.Text = child.GetType().Name;

                Parser.Pos pos;
                int len;

                prodLabel.BackColor = Color.Pink;
                pos = parser.FindPos(child.StartPos);
                len = child.Length;

                parserGuiBinder.GoTo(pos, len);
            }
        }

        void parser_TmpErrorAdded(string info, CompilationErrorEnty err, bool sync)
        {
            if (Debugging && sync)
            {
                parseButton.Enabled = true;
                stepButton.Enabled = true;

                curProdInfLabel.Text = info;

                Parser.Pos pos;
                int len;

                errLabel.BackColor = Color.Pink;

                int ri = parserGuiBinder.AddError(err, true);

                parserGuiBinder.GenerateLists(false, true, false);

                int start = int.MaxValue;
                int end = int.MinValue;
                start = Math.Min(start, err.Position);
                end = Math.Max(end, err.Position + err.Length);

                pos = parser.FindPos(start);
                len = end - start;

                errorsTable.CurrentCell = errorsTable.Rows[ri].Cells[0];

                parserGuiBinder.GoTo(pos, len);
            }
        }

        void parser_NodeAdded(BaseNode node, bool sync)
        {
            if (Debugging && sync)
            {
                parseButton.Enabled = true;
                stepButton.Enabled = true;

                parserGuiBinder.ClearPendingErrors();

                parserGuiBinder.GenerateLists(false, true, false);

                curProdInfLabel.Text = node.GetType().Name;

                addNodeLabel.BackColor = Color.Pink;
                Parser.Pos pos = parser.FindPos(node.StartPos);

                parserGuiBinder.GoTo(pos, node.Length);

                if (node is BlockNode)
                {
                    foreach (TreeNode tn in new ArrayList(treeView1.Nodes))
                    {
                        BaseNode n2 = (BaseNode)tn.Tag;
                        if (n2.Parent == node)
                        {
                            treeView1.Nodes.Remove(tn);
                        }
                    }
                    parserGuiBinder.AddBlockNode(treeView1.Nodes, (BlockNode)node, null);
                }
                else if (node is StatementNode)
                {
                    parserGuiBinder.AddStatementNode(treeView1.Nodes, (StatementNode)node, null);
                }
                else
                {
                    TreeNode treeNode = parserGuiBinder.CreateGuiNode0(node, null);
                    parserGuiBinder.SetupGuiNode(treeNode, node);
                    treeView1.Nodes.Add(treeNode);
                }

                parserGuiBinder.GenerateLists(false, false, true);
            }
        }

        void StartParse()
        {
            if (parserGuiBinder != null)
            {
                parserGuiBinder.Clear();
            }

            if (parser != null)
            {
                parser.ErrorAdded -= errorAdded;
                parser.NewLineAdded -= newlineAdded;
                parser.TokenRead -= tokenRead;
                parser.TokenAdded -= tokenAdded;
                parser.NodeCreated -= nodeCreated;
                parser.NodeAdded -= nodeAdded;
            }
            string fl = fileTextBox.Text.ToLower();
            if (fl.EndsWith(".cs"))
            {
                parser = new CsParser(fileTextBox.Text, sourceTextBox.Text);
            }
            else if (fl.EndsWith(".xml"))
            {
                parser = new XmlParser(fileTextBox.Text, sourceTextBox.Text);
            }
            else
            {
                throw new NotSupportedException();
            }
            if (parserGuiBinder != null)
            {
                parserGuiBinder.Dispose();
            }
            parserGuiBinder = new ParserGuiBinder(sourceTextBox, treeView1, tokensTable, errorsTable, posLabel, lineLabel, columnLabel, parser);

            parser.ErrorAdded += errorAdded = new Parser.ErrorEventHandler(parser_ErrorAdded);
            parser.NewLineAdded += newlineAdded = new Parser.TokenPosEventHandler(parser_NewLineAdded);
            parser.TokenRead += tokenRead = new Parser.TokenPosEventHandler(parser_TokenRead);
            parser.TokenAdded += tokenAdded = new Parser.TokenEventHandler(parserr_TokenAdded);
            parser.NodeCreated += nodeCreated = new Parser.NodeEventHandler(parser_NodeCreated);
            parser.TmpErrorsAdded += tmpErrorAdded = new Parser.ErrorEventHandler(parser_TmpErrorAdded);
            parser.NodeAdded += nodeAdded = new Parser.NodeEventHandler(parser_NodeAdded);


            ControlsEnabled = false;
        }




        private void clearTablesBtn_Click(object sender, EventArgs e)
        {
            parserGuiBinder.Clear();

            parser = null;
            if (parserGuiBinder != null)
            {
                parserGuiBinder.Dispose();
                parserGuiBinder = null;
            }
            ControlsEnabled = true;
        }

        private void browseCsButton_Click(object sender, EventArgs e)
        {
            fileTextBox.Text = "F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\GreenZoneFxEAnalyzer\\GreenZoneFxEngine\\Trading\\ExecutableRuntime.cs";
            showTokensChb.Checked = true;
            OpenFileDialog d = new OpenFileDialog();
            d.FileName = fileTextBox.Text;
            if (d.ShowDialog() == DialogResult.OK)
            {
                fileTextBox.Text = d.FileName;
            }
        }

        private void browseXmlButton_Click(object sender, EventArgs e)
        {
            fileTextBox.Text = "F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\GreenZoneFxEAnalyzer\\GreenZoneFxBaseGEx\\generated\\reflection-info-GreenZoneParser.xml";
            showTokensChb.Checked = false;
            OpenFileDialog d = new OpenFileDialog();
            d.FileName = fileTextBox.Text;
            if (d.ShowDialog() == DialogResult.OK)
            {
                fileTextBox.Text = d.FileName;
            }
        }

        private void errLabel_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void tkLabel_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void debugChb_CheckedChanged(object sender, EventArgs e)
        {
            errLabel.Visible = debugTokensChb.Checked || debugParseChb.Checked;
            tkLabel.Visible = debugTokensChb.Checked;
            nlLabel.Visible = debugTokensChb.Checked || debugParseChb.Checked;
            stepButton.Visible = debugTokensChb.Checked || debugParseChb.Checked;

            breakPointBar.Invalidate();
            breakPointBar.Update();
        }

        private void debugParseChb_CheckedChanged(object sender, EventArgs e)
        {
            errLabel.Visible = debugTokensChb.Checked || debugParseChb.Checked;
            prodLabel.Visible = debugParseChb.Checked;
            addNodeLabel.Visible = debugParseChb.Checked;
            nlLabel.Visible = debugTokensChb.Checked || debugParseChb.Checked;
            stepButton.Visible = debugTokensChb.Checked || debugParseChb.Checked;

            breakPointBar.Invalidate();
            breakPointBar.Update();
        }


        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSearchDialog();
        }


        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN)
            {
                if ((ModifierKeys & Keys.Control) == Keys.Control && (ModifierKeys & Keys.Alt) != Keys.Alt)
                {
                    if ((Keys)m.WParam == Keys.F)
                    {
                        ShowSearchDialog();
                        return true;
                    }
                }
            }
            return false;
        }

        private void breakPointBar_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (var bp in breakPoints)
            {
                int y = sourceTextBox.GetLineY(bp);
                int h = sourceTextBox.LineHeight;
                int W = breakPointBar.Width;
                int w = breakPointBar.Width - 2;
                int b = SystemInformation.Border3DSize.Height;

                if (y >= sourceTextBox.Height)
                {
                    break;
                }
                else if (0 <= y)
                {
                    if (debugParseChb.Checked || debugTokensChb.Checked)
                    {
                        e.Graphics.FillEllipse(bpBrush, 0, b + y + (h - w) / 2, w, w);
                    }
                    else
                    {
                        e.Graphics.FillEllipse(bpInaBrush, 0, b + y + (h - w) / 2, w, w);
                    }
                }
            }
        }

        void sourceTextBox_Paint(object sender, PaintEventArgs e)
        {
            breakPointBar.Invalidate();
            breakPointBar.Update();
        }

        private void breakPointBar_Click(object sender, EventArgs e)
        {
            int line = sourceTextBox.GetYLine(sourceTextBox.PointToClient(Control.MousePosition).Y);
            if (breakPoints.Contains(line))
            {
                breakPoints.Remove(line);
            }
            else
            {
                breakPoints.Add(line);
            }
            breakPointBar.Invalidate();
            breakPointBar.Update();
        }
    }

    class ParserGuiBinder : TesterGuiBinder
    {
        internal ParserGuiBinder(RichTextBoxEx sourceTextBox, TreeView treeView1, DataGridView tokensTable, DataGridView errorsTable, 
                                 Label posLabel, Label lineLabel, Label columnLabel,
                                 Parser parser)
            : base( null, sourceTextBox, treeView1, tokensTable, errorsTable, 
                    posLabel, lineLabel, columnLabel,
                    parser)
        {
        }

        internal override int AddError(CompilationErrorEnty err, bool pending = false)
        {
            if (pending)
            {
                pendingParseErrors.Add(err);
            }

            int ri = errorsTable.Rows.Add(err.Message, err.Line, err.Column, err.Length, err.Position, err);
            errorRows[err/*.Position*/] = errorsTable.Rows[ri];
            return ri;
        }
    }
}