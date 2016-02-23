using SMS.Windows.Forms;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace EnvironmentAssistant
{
	public class ImportMetatraderPage2 : AssistantPage
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private OpenFileDialog openFileDialog1;
        private CheckBox checkBox1;
        private TextBox accountNameTb;
        private TextBox accountCompanyTb;
        private Label label6;
        private Label label5;
        private Label label4;
        private ComboBox historyDirectoryCb;
        private Label label7;
        private TextBox nameTb;
        private Label label3;
        private NumericUpDown accountNumberNupd;
        private Timer timer1;
		private System.ComponentModel.IContainer components = null;
        private TextBox accountCurrencyTb;
        private Label label9;
        private NumericUpDown leverageNupd;
        private Label label8;

        bool initialized;

        public ImportMetatraderPage2()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

        }

        new internal EnvironmentAssistantForm ParentForm
        {
            get
            {
                return (EnvironmentAssistantForm)base.ParentForm;
            }
        }

        public Process MetatraderProcess
        {
            get;
            private set;
        }

        public bool CloseMetatrader
        {
            get
            {
                return checkBox1.Checked;
            }
        }
        
        public string[] ImportedParameters
        {
            get;
            private set;
        }

        public string[] HistoryDirectories
        {
            get;
            private set;
        }

        public string ImportedMetatarderDir
        {
            get;
            private set;
        }

        public string ImportedMetatarderVersion
        {
            get;
            private set;
        }

        public string HistoryDirectory
        {
            get;
            private set;
        }

        public string EnvironmentName
        {
            get;
            private set;
        }

        /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.accountCompanyTb = new System.Windows.Forms.TextBox();
            this.accountNameTb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nameTb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.historyDirectoryCb = new System.Windows.Forms.ComboBox();
            this.accountNumberNupd = new System.Windows.Forms.NumericUpDown();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.leverageNupd = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.accountCurrencyTb = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountNumberNupd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leverageNupd)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(471, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "This page demonstrates that you can inherit directly from AssistantPage to create" +
                " whatever look && feel you like.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(471, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "It also demonstrates how data validation is handled in the default implementation" +
                " of OnKillActive.  In this example, validation will fail if the text box below i" +
                "s empty.";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkRate = 100;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataMember = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(253, 273);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(228, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Close Metatrader application when finished";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(299, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Account #:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Account Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Account Company:";
            // 
            // accountCompanyTb
            // 
            this.accountCompanyTb.Location = new System.Drawing.Point(110, 92);
            this.accountCompanyTb.Name = "accountCompanyTb";
            this.accountCompanyTb.Size = new System.Drawing.Size(166, 20);
            this.accountCompanyTb.TabIndex = 0;
            this.accountCompanyTb.TextChanged += new System.EventHandler(this.accountCompanyTb_TextChanged);
            // 
            // accountNameTb
            // 
            this.accountNameTb.Location = new System.Drawing.Point(110, 118);
            this.accountNameTb.Name = "accountNameTb";
            this.accountNameTb.Size = new System.Drawing.Size(166, 20);
            this.accountNameTb.TabIndex = 2;
            this.accountNameTb.TextChanged += new System.EventHandler(this.accountNameTb_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Name:";
            // 
            // nameTb
            // 
            this.nameTb.Location = new System.Drawing.Point(110, 155);
            this.nameTb.Name = "nameTb";
            this.nameTb.Size = new System.Drawing.Size(360, 20);
            this.nameTb.TabIndex = 3;
            this.nameTb.TextChanged += new System.EventHandler(this.nameTb_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "History Directory:";
            // 
            // historyDirectoryCb
            // 
            this.historyDirectoryCb.FormattingEnabled = true;
            this.historyDirectoryCb.Location = new System.Drawing.Point(110, 181);
            this.historyDirectoryCb.Name = "historyDirectoryCb";
            this.historyDirectoryCb.Size = new System.Drawing.Size(360, 21);
            this.historyDirectoryCb.TabIndex = 4;
            this.historyDirectoryCb.SelectedIndexChanged += new System.EventHandler(this.historyDirectoryCb_SelectedIndexChanged);
            // 
            // accountNumberNupd
            // 
            this.accountNumberNupd.Location = new System.Drawing.Point(365, 93);
            this.accountNumberNupd.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.accountNumberNupd.Name = "accountNumberNupd";
            this.accountNumberNupd.Size = new System.Drawing.Size(105, 20);
            this.accountNumberNupd.TabIndex = 1;
            this.accountNumberNupd.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.accountNumberNupd.ValueChanged += new System.EventHandler(this.accountNumberNupd_ValueChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // leverageNupd
            // 
            this.leverageNupd.Location = new System.Drawing.Point(365, 226);
            this.leverageNupd.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.leverageNupd.Name = "leverageNupd";
            this.leverageNupd.Size = new System.Drawing.Size(105, 20);
            this.leverageNupd.TabIndex = 6;
            this.leverageNupd.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.leverageNupd.ValueChanged += new System.EventHandler(this.leverageNupd_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(299, 229);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Leverage:";
            // 
            // accountCurrencyTb
            // 
            this.accountCurrencyTb.Location = new System.Drawing.Point(110, 226);
            this.accountCurrencyTb.Name = "accountCurrencyTb";
            this.accountCurrencyTb.Size = new System.Drawing.Size(166, 20);
            this.accountCurrencyTb.TabIndex = 5;
            this.accountCurrencyTb.Text = "USD";
            this.accountCurrencyTb.TextChanged += new System.EventHandler(this.accountCurrencyTb_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 229);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Account Currency:";
            // 
            // ImportMetatraderPage2
            // 
            this.Controls.Add(this.accountCurrencyTb);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.leverageNupd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.accountNumberNupd);
            this.Controls.Add(this.historyDirectoryCb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nameTb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.accountNameTb);
            this.Controls.Add(this.accountCompanyTb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ImportMetatraderPage2";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountNumberNupd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leverageNupd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

        protected override bool OnSetActive()
        {
            if( !base.OnSetActive() )
                return false;

            Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.DisabledFinish);

            if (SelectedNextPage == null)
            {
                accountCompanyTb.Text = "";
                accountNumberNupd.Value = 1000;
                accountNameTb.Text = "";
                nameTb.Text = "";
                historyDirectoryCb.Items.Clear();
                historyDirectoryCb.SelectedItem = null;
                accountCurrencyTb.Text = "USD";
                leverageNupd.Value = 100;
            }

            if (string.IsNullOrEmpty(ParentForm.UpdatedEnvironment))
            {
                nameTb.ReadOnly = false;

                ImportMetatraderPage1 imp = (ImportMetatraderPage1)PreviousPage;
                string[] mt4inf = imp.SelectedImportDirectory;

                string[] path = mt4inf[0].Split('\\');

                if (accountCompanyTb.Text.Length == 0)
                {
                    accountCompanyTb.Text = path[path.Length - 1].
                            Replace("MetaTrader 4 ", "").Replace("Meta Trader 4 ", "").Replace(" MetaTrader 4", "").Replace(" Meta Trader 4", "").
                            Replace("MetaTrader ", "").Replace("Meta Trader ", "").Replace(" MetaTrader", "").Replace(" Meta Trader", "").
                            Replace("MetaTrader", "").Replace("Meta Trader", "").

                            Replace("Metatrader 4 ", "").Replace("Meta trader 4 ", "").Replace(" Metatrader 4", "").Replace(" Meta trader 4", "").
                            Replace("Metatrader ", "").Replace("Meta trader ", "").Replace(" Metatrader", "").Replace(" Meta trader", "").
                            Replace("Metatrader", "").Replace("Meta trader", "").

                            Replace("metatrader 4 ", "").Replace("meta trader 4 ", "").Replace(" metatrader 4", "").Replace(" meta trader 4", "").
                            Replace("metatrader ", "").Replace("meta trader ", "").Replace(" metatrader", "").Replace(" meta trader", "").
                            Replace("metatrader", "").Replace("meta trader", "")
                            ;
                }
                if (accountNameTb.Text.Length == 0)
                {
                    accountNameTb.Text = System.Environment.MachineName + " " + System.Environment.UserName;
                }
            }
            else
            {
                nameTb.ReadOnly = true;
                nameTb.Text = ParentForm.UpdatedEnvironment;

                set(accountCompanyTb, ParentForm.UpdatedEnvironmentData[1]);
                set(accountNameTb, ParentForm.UpdatedEnvironmentData[2]);
                set(accountNumberNupd, ParentForm.UpdatedEnvironmentData[3]);
                set(accountCurrencyTb, ParentForm.UpdatedEnvironmentData[4]);
                set(leverageNupd, ParentForm.UpdatedEnvironmentData[5]);
                set(historyDirectoryCb, null, ParentForm.UpdatedEnvironmentHistoryDir);
            }

            initialized = true;

            try
            {
                startMetatrader();

                return true;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message + "\n\nHave you exited from the selected Metatrader ?", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        protected override bool OnAssistantFinish()
        {
            if (string.IsNullOrEmpty(ImportedParameters[0]))
            {
                ImportedParameters[0] = HistoryDirectory;
            }

            return true;
        }

        private void startMetatrader()
        {
            ImportMetatraderPage1 imp = (ImportMetatraderPage1)PreviousPage;
            checkBox1.Visible = imp.StartMetatrader;

            string[] mt4inf = imp.SelectedImportDirectory;
            if (mt4inf != null)
            {
                if (mt4inf[1] == "Metatrader 4")
                {
                    File.Delete(mt4inf[0] + "\\experts\\files\\GZEA_AccountData.csv");

                    FileStream symFile = File.Open(mt4inf[0] + "\\experts\\files\\GZEA_Symbols.csv", FileMode.Create, FileAccess.Write);

                    SortedSet<string> symbols = new SortedSet<string>();
                    SortedSet<string> histDirs = new SortedSet<string>();

                    int[] periods = { 43200, 10080, 1440, 240, 60, 30, 15, 5, 1 };
                    int hislen = mt4inf[0].Length + "\\history".Length;
                    string[] dirs = Directory.GetDirectories(mt4inf[0] + "\\history", "*", SearchOption.TopDirectoryOnly);
                    foreach (string acctDir in dirs)
                    {
                        string histDir = acctDir.Substring(hislen + 1);
                        string[] m1hsts = Directory.GetFiles(acctDir, "*.hst", SearchOption.TopDirectoryOnly);

                        foreach (string m1hst in m1hsts)
                        {
                            int traillen = 0;
                            foreach (var p in periods)
                            {
                                if (m1hst.EndsWith(p + ".hst"))
                                {
                                    traillen = (p + ".hst").Length;
                                    break;
                                }
                            }
                            if (traillen > 0)
                            {
                                histDirs.Add(histDir);

                                string symbol = m1hst.Substring(acctDir.Length + 1, m1hst.Length - traillen - acctDir.Length - 1);
                                symbols.Add(symbol);
                            }
                        }
                    }

                    HistoryDirectories = (string[])new ArrayList(histDirs).ToArray(typeof(string));

                    StreamWriter symWriter = new StreamWriter(symFile);
                    foreach (var symbol in symbols)
                    {
                        symWriter.WriteLine(symbol);
                    }
                    symWriter.Close();

                    //string cd = Directory.GetCurrentDirectory();

                    // TODO current directory  vs  install directory
                    File.Copy("environment\\GreenZoneFxEAImporter.ex4", mt4inf[0] + "\\experts\\scripts\\-- LAUNCH IT NOW -- GreenZoneFxEAImporter.ex4", true);
                    if (imp.StartMetatrader)
                    {
                        MetatraderProcess = Process.Start(mt4inf[0] + "\\terminal.exe");

                        Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.DisabledFinish);
                        initialized = false;
                        timer1.Start();
                    }
                }
                else if (mt4inf[1] == "Metatrader 5")
                {
                }
            }
        }

        private bool checkLoadFromMetatrader()
        {
            if (PreviousPage is ImportMetatraderPage1)
            {
                ImportMetatraderPage1 imp = (ImportMetatraderPage1)PreviousPage;

                string[] mt4inf = imp.SelectedImportDirectory;
                if (mt4inf != null)
                {
                    if (mt4inf[1] == "Metatrader 4")
                    {
                        try
                        {
                            FileStream exportFile =
                                File.Open(mt4inf[0] + "\\experts\\files\\GZEA_AccountData.csv", FileMode.Open, FileAccess.ReadWrite);
                            exportFile.Close();
                            timer1.Stop();
                            return true;
                        }
                        catch (IOException)
                        {
                        }
                    }
                }
            }
            return false;
        }

        private void loadFromMetatrader()
        {
            try
            {
            if (PreviousPage is ImportMetatraderPage1)
                {
                    ImportMetatraderPage1 imp = (ImportMetatraderPage1)PreviousPage;

                    string[] mt4inf = imp.SelectedImportDirectory;
                    if (mt4inf != null)
                    {
                        if (mt4inf[1] == "Metatrader 4")
                        {
                            FileStream exportFile =
                                File.Open(mt4inf[0] + "\\experts\\files\\GZEA_AccountData.csv", FileMode.Open, FileAccess.ReadWrite);
                            exportFile.Close();

                            ImportedParameters = File.ReadAllLines(mt4inf[0] + "\\experts\\files\\GZEA_AccountData.csv");
                            ImportedMetatarderDir = mt4inf[0];
                            ImportedMetatarderVersion = mt4inf[1];
                        }
                        else if (mt4inf[1] == "Metatrader 5")
                        {
                        }

                        // Cleanup
                        File.Delete(mt4inf[0] + "\\experts\\scripts\\-- LAUNCH IT NOW -- GreenZoneFxEAImporter.ex4");
                        File.Delete(mt4inf[0] + "\\experts\\files\\GZEA_Symbols.csv");
                        File.Delete(mt4inf[0] + "\\experts\\files\\GZEA_AccountData.csv");

                        if (imp.StartMetatrader && CloseMetatrader)
                        {
                            try
                            {
                                if (!MetatraderProcess.CloseMainWindow())
                                {
                                    MetatraderProcess.Kill();
                                }
                                else if (!MetatraderProcess.WaitForExit(5000))
                                {
                                    MetatraderProcess.Kill();
                                }


                                if (MetatraderProcess.Responding)
                                {
                                    MessageBox.Show("Unable to close Metatarder, please close it manually.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + "\n\nUnable to close Metatarder, please close it manually.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        if (MetatraderProcess != null)
                        {
                            MetatraderProcess.Close();
                            MetatraderProcess = null;
                        }

                        set(accountCompanyTb, ImportedParameters[1]);
                        set(accountNameTb, ImportedParameters[2]);
                        set(accountNumberNupd, ImportedParameters[3]);
                        set(accountCurrencyTb, ImportedParameters[4]);
                        set(leverageNupd, ImportedParameters[5]);
                        set(historyDirectoryCb, HistoryDirectories, ImportedParameters[0]);

                        initialized = true;
                        validate();
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message + "\n\nPlease run the \"-- LAUNCH IT NOW -- GreenZoneFxEAImporter\" script in Metatrader* and wait until it finished.\n\n   * It should be attached to a Chart Window. See Metatrader manual.", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                timer1.Start();
            }
        }

        private void set(TextBox tb, string txt)
        {
            if (txt != null)
            {
                txt = txt.Trim();
                if (txt != "")
                {
                    tb.Text = txt.Trim();
                }
            }
        }
        private void set(NumericUpDown nupd, string txt)
        {
            if (txt != null)
            {
                int n = Convert.ToInt32(txt);
                if (n != 0)
                {
                    nupd.Value = n;
                }
            }
        }

        private void set(ComboBox cb, string[] range, string txt)
        {
            cb.Items.Clear();
            if (range != null)
            {
                cb.Items.AddRange(range);
            }
            else
            {
                cb.Items.Add(txt);
            }
            if (txt != null)
            {
                txt = txt.Trim();
                if (txt != "")
                {
                    cb.SelectedItem = txt;
                }
            }
        }

        private void validate()
        {
            if (initialized)
            {
                bool validationError = false;

                ImportedParameters[1] = accountCompanyTb.Text.Trim();
                ImportedParameters[2] = accountNameTb.Text.Trim();
                ImportedParameters[3] = "" + accountNumberNupd.Value;
                EnvironmentName = nameTb.Text;
                HistoryDirectory = (string)historyDirectoryCb.SelectedItem;
                ImportedParameters[4] = accountCurrencyTb.Text;
                ImportedParameters[5] = "" + leverageNupd.Value;


                if (accountCompanyTb.Text == "")
                {
                    errorProvider1.SetError(accountCompanyTb, "Account Company should not be empty.");
                    validationError = true;
                }
                else
                {
                    errorProvider1.SetError(accountCompanyTb, null);
                }
                
                
                if (accountNameTb.Text == "")
                {
                    errorProvider1.SetError(accountNameTb, "Account Name should not be empty.");
                    validationError = true;
                }
                else
                {
                    errorProvider1.SetError(accountNameTb, null);
                }
                
                
                if (nameTb.Text == "")
                {
                    errorProvider1.SetError(nameTb, "Name should not be empty.");
                    validationError = true;
                }
                else if (ParentForm.Environments.Contains(nameTb.Text) && nameTb.Text!=ParentForm.UpdatedEnvironment)
                {
                    errorProvider1.SetError(nameTb, "Environment already exists.");
                    validationError = true;
                }
                else
                {
                    errorProvider1.SetError(nameTb, null);
                }

                
                if (historyDirectoryCb.SelectedItem == null)
                {
                    errorProvider1.SetError(historyDirectoryCb, "History should be selected.");
                    validationError = true;
                }
                else
                {
                    errorProvider1.SetError(historyDirectoryCb, null);
                }

                
                if (accountCurrencyTb.Text == "")
                {
                    errorProvider1.SetError(accountCurrencyTb, "Account Currency should not be empty.");
                    validationError = true;
                }
                else
                {
                    errorProvider1.SetError(accountCurrencyTb, null);
                }

                
                if (leverageNupd.Value < 10)
                {
                    errorProvider1.SetError(leverageNupd, "Account Leverage should be >= 10.");
                    validationError = true;
                }
                else
                {
                    errorProvider1.SetError(leverageNupd, null);
                }



                if (validationError)
                {
                    Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.DisabledFinish);
                }
                else
                {
                    Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.Finish);
                }
            }
        }

        private void accountCompanyTb_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ParentForm.UpdatedEnvironment))
            {
                nameTb.Text = accountCompanyTb.Text + ", " + accountNameTb.Text + ", " + accountNumberNupd.Value;
            }
            validate();
        }

        private void accountNameTb_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ParentForm.UpdatedEnvironment))
            {
                nameTb.Text = accountCompanyTb.Text + ", " + accountNameTb.Text + ", " + accountNumberNupd.Value;
            }
            validate();
        }

        private void accountNumberNupd_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ParentForm.UpdatedEnvironment))
            {
                nameTb.Text = accountCompanyTb.Text + ", " + accountNameTb.Text + ", " + accountNumberNupd.Value;
            }
            validate();
        }

        private void nameTb_TextChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void historyDirectoryCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void accountCurrencyTb_TextChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void leverageNupd_ValueChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkLoadFromMetatrader())
            {
                loadFromMetatrader();
            }
        }

    }
}

