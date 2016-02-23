using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GreenZoneParser.Reflect;
using GreenZoneParser.Xml;
using GreenZoneParser.Parsers;
using System.Collections;

namespace GreenZoneParser
{
    public partial class CompilerTesterForm : Form
    {
        readonly Dictionary<XmlParser, CompilerGuiBinder> xmlParserBinders = new Dictionary<XmlParser, CompilerGuiBinder>();
        Resolver resolver;

        const string path0 = "F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\GreenZoneFxEAnalyzer\\GreenZoneFxBaseGEx\\generated";

        public CompilerTesterForm()
        {
            InitializeComponent();

            var fs = Directory.EnumerateFiles(path0, "*.xml");
            //xmlTable.Rows.Add(path0, null);
            foreach (var f in fs)
            {
                string[] s = f.Split('\\');
                string path = string.Join("\\", s, 0, s.Length-1);
                string file = s[s.Length - 1];
                if (file.StartsWith("reflection-info-GreenZone"))
                {
                    xmlTable.Rows.Add(path, file);
                }
            }
        }

        string dt()
        {
            DateTime d = DateTime.Now;
            return d.ToShortTimeString() + ":" + d.Second + "." + d.Millisecond;
        }

        private void compileButton_Click(object sender, EventArgs eventArgs)
        {
            foreach (var b in xmlParserBinders.Values)
            {
                b.Dispose();
            }

            xmlParserBinders.Clear();

            Console.WriteLine(dt() + " Reading all Xml texts");
            int j = 0;
            int k = 0;
            foreach (DataGridViewRow r in xmlTable.Rows)
            {
                j++;
                if ((int)(100.0 * j / xmlTable.Rows.Count) > (int)(100.0 * k / xmlTable.Rows.Count))
                {
                    Console.WriteLine((int)(100.0 * j / xmlTable.Rows.Count) + " %");
                    k = j;
                }
                
                string path = (string)r.Cells[0].Value;
                string file = (string)r.Cells[1].Value;
                if (file != null)
                {
                    XmlParser p = new XmlParser(file, File.ReadAllText(path + '\\' + file).Replace("\r\n","\n"));
                    CompilerGuiBinder b = new CompilerGuiBinder(fileTextBox, sourceTextBox, treeView1, errorsTable, posLabel, lineLabel, colLabel, p);
                    xmlParserBinders[p] = b;
                }
            }
            Console.WriteLine(dt() + " Xml texts ok.");

            Console.WriteLine(dt() + " Parsing all Xml-s");
            j = 0;
            k = 0;
            foreach (var be in xmlParserBinders)
            {
                j++;
                if ((int)(100.0 * j / xmlParserBinders.Count) > (int)(100.0 * k / xmlParserBinders.Count))
                {
                    Console.WriteLine((int)(100.0 * j / xmlParserBinders.Count) + " %");
                    k = j;
                }
                XmlParser p = be.Key;
                p.Parse();
                GC.Collect();
            }
            Console.WriteLine(dt() + " Xml-s parsed.");

            Console.WriteLine(dt() + " Resolver : Parsing...");
            resolver = new Resolver(xmlParserBinders.Keys);
            Console.WriteLine(dt() + " Resolver : Parsed");

            if (!preParsePhaseChb.Checked)
            {
                Console.WriteLine(dt() + " Resolving...");
                resolver.ResolveAll();
                Console.WriteLine(dt() + " Resolved");
            }
            else
            {
                Console.WriteLine(dt() + " Resolving disabled.");
            }

            
            Console.WriteLine(dt() + " Generating gui tree nodes");
            j = 0;
            k = 0;

            if (preParsePhaseChb.Checked)
            {
                foreach (var be in xmlParserBinders)
                {
                    j++;
                    if ((int)(100.0 * j / xmlParserBinders.Count) > (int)(100.0 * k / xmlParserBinders.Count))
                    {
                        Console.WriteLine((int)(100.0 * j / xmlParserBinders.Count) + " %");
                        k = j;
                    }

                    CompilerGuiBinder b = be.Value;
                    TreeNode broot = b.CreateGuiNode(resolver.rootParseInfo.Children[be.Key.FileName], be.Key == null ? "<System>" : be.Key.FileName);
                    treeView1.Nodes.Add(broot);

                    b.UpdateTables(false);
                    b.GenerateLists();
                }
            }
            else
            {
                SortedDictionary<string, ReflType> otherTypes = new SortedDictionary<string, ReflType>();
                foreach (var e in resolver.Types)
                {
                    otherTypes[e.Key] = e.Value;
                }
                foreach (var be in xmlParserBinders)
                {
                    CompilerGuiBinder b = be.Value;
                    TreeNode broot = new TreeNode(be.Key.FileName);
                    treeView1.Nodes.Add(broot);

                    foreach (var e in resolver.Types)
                    {
                        if (e.Value.Parser == be.Key)
                        {
                            otherTypes.Remove(e.Key);
                            j++;
                            if ((int)(10.0 * j / resolver.Types.Count) > (int)(10.0 * k / resolver.Types.Count))
                            {
                                Console.WriteLine(10 * (int)(10.0 * j / resolver.Types.Count) + " %");
                                k = j;
                            }

                            b.AddReflNode(broot.Nodes, e.Value, e.Key);
                            b.UpdateTables(false);
                            b.GenerateLists();
                        }
                    }
                }

                CompilerGuiBinder b0 = new CompilerGuiBinder(fileTextBox, sourceTextBox, treeView1, errorsTable, posLabel, lineLabel, colLabel, null);
                TreeNode broot0 = new TreeNode("<System>");
                treeView1.Nodes.Add(broot0);
                foreach (var e in otherTypes)
                {
                    j++;
                    if ((int)(10.0 * j / resolver.Types.Count) > (int)(10.0 * k / resolver.Types.Count))
                    {
                        Console.WriteLine(10 * (int)(10.0 * j / resolver.Types.Count) + " %");
                        k = j;
                    }

                    b0.AddReflNode(broot0.Nodes, e.Value, e.Key);
                    b0.UpdateTables(false);
                    b0.GenerateLists();
                }
            }
            Console.WriteLine(dt() + " Finished");
        }

        private void removeXmlButton_Click(object sender, EventArgs e)
        {
            var r = xmlTable.CurrentRow;
            string path = (string)r.Cells[0].Value;
            string file = (string)r.Cells[1].Value;
            if (file == null)
            {
                foreach (DataGridViewRow dr in new ArrayList(xmlTable.Rows))
                {
                    string dpath = (string)dr.Cells[0].Value;
                    if (dpath.Equals(path))
                    {
                        xmlTable.Rows.Remove(dr);
                    }
                }
            }
            else
            {
                xmlTable.Rows.Remove(r);
            }
        }

        private void addXmlButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.InitialDirectory = path0;
            d.Multiselect = true;
            if (d.ShowDialog() == DialogResult.OK)
            {
                foreach (var f in d.FileNames)
                {
                    if (f.ToLower().EndsWith(".xml"))
                    {
                        string[] s = f.Split('\\');
                        string path = string.Join("\\", s, 0, s.Length - 1);
                        xmlTable.Rows.Add(path, s[s.Length - 1]);
                    }
                    else if (Directory.Exists(f))
                    {
                        xmlTable.Rows.Add(f, "");
                    }
                }
            }
        }


    }


    class CompilerGuiBinder : TesterGuiBinder
    {
        internal CompilerGuiBinder(TextBox fileTextBox, RichTextBoxEx sourceTextBox, 
                                    TreeView treeView1, DataGridView errorsTable,
                                    Label posLabel, Label lineLabel, Label columnLabel,
                                    Parser parser)
            : base( fileTextBox, sourceTextBox, treeView1, null, errorsTable,
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

            int ri = errorsTable.Rows.Add(err.Message, parser.FileName, err.Line, err.Column, err);
            errorRows[err/*.Position*/] = errorsTable.Rows[ri];
            return ri;
        }
    }
}
