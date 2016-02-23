using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using C_Sharp.AST;

namespace Parse_Tree_C_Sharp
{
    public partial class frmMain : Form
    {
        MyParserClass MyParser = new MyParserClass();
        string txtSource;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //This procedure can be called to load the parse tables. The class can
            //read tables using a BinaryReader.
            //try
            //{
                if (MyParser.Setup(txtTableFile.Text))
                {
                    //Change button enable/disable for the user
                    btnLoad.Enabled = false;
                    btnParse.Enabled = true;
                }
                else
                {
                    MessageBox.Show("CGT failed to load");
                }
            //}
                //catch (ParserException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            btnParse.Enabled = false;
            txtSource = File.ReadAllText(sourceFileTb.Text);

            if (MyParser.Parse(new StringReader(txtSource)))
            {
                DrawReductionTree(MyParser.Root);
            }
            else
            {
                txtParseTree.Text = MyParser.FailMessage;
            }

            btnParse.Enabled = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btnLoad.Enabled = true;
            btnParse.Enabled = false;

            //txtTableFile.Text = Application.StartupPath + "\\simple 2.egt";
            //txtTableFile.Text = "E:\\downloads\\GOLD Bnf parser\\C-Sharp\\C# 2.0 r7.egt";
            txtTableFile.Text = "F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\GreenZoneFxEAnalyzer\\Parse Tree C-Sharp\\C-Sharp\\C# 2.0 r7.egt";
            sourceFileTb.Text = "F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\GreenZoneFxEAnalyzer\\GreenZoneFxBuildTasks\\GreenZoneUtilsBase\\ViewController\\Controller.cs";
        }

        private void DrawReductionTree(Reduction Root)
        {
            //This procedure starts the recursion that draws the parse tree.
            StringBuilder tree = new StringBuilder();

            tree.AppendLine("+-" + Root.Parent.Text(false));
            DrawReduction(tree, Root, 1);

            txtParseTree.Text = tree.ToString();

            CompilationUnitAST u = new CompilationUnitAST(Root);
            txtReparse.Text = u.ToString();
        }

        private void DrawReduction(StringBuilder tree, Reduction reduction, int indent)
        {
            //This is a simple recursive procedure that draws an ASCII version of the parse
            //tree

            int n;
            string indentText = "";

            for (n = 1; n <= indent; n++)
            {
                indentText += "| ";
            }

            //=== Display the children of the reduction
            for (n = 0; n < reduction.Count(); n++)
            {
                switch (reduction[n].Type())
                {
                    case SymbolType.Nonterminal:
                        Reduction branch = (Reduction)reduction[n].Data;

                        tree.AppendLine(indentText + "+-" + branch.Parent.Text(false));
                        DrawReduction(tree, branch, indent + 1);
                        break;

                    default:
                        string leaf = (string)reduction[n].Data;

                        tree.AppendLine(indentText + "+-" + leaf);
                        break;
                }
            }
        }

    }
} //Form
