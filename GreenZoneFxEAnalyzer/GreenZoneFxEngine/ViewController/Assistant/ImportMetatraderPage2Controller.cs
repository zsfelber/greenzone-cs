using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;

using System.Timers;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    
    public class ImportMetatraderPage2Controller : ImportMetatraderPage2ControllerBase
    {
        private System.Timers.Timer timer1;

        bool initialized;

        public ImportMetatraderPage2Controller(GreenRmiManager rmiManager, EnvironmentAssistantController assistant)
            : base(rmiManager, assistant)
        {
            ErrorProvider1 = new ChildControlMap<string>(rmiManager);
            OpenFileDialog1 = new FileDialogController(rmiManager);
            CheckBox1 = new ToggleButtonController(rmiManager, this);
            AccountNameTb = new LabelledController(rmiManager, this);
            AccountCompanyTb = new LabelledController(rmiManager, this);
            HistoryDirectoryCb = new ComboController(rmiManager, this, false);
            NameTb = new FieldController<string>(rmiManager, this);
            AccountNumberNupd = new FieldController<int>(rmiManager, this);
            AccountCurrencyTb = new LabelledController(rmiManager, this);
            LeverageNupd = new FieldController<int>(rmiManager, this);
            timer1 = new System.Timers.Timer(1000);
            timer1.AutoReset = true;

            AccountCompanyTb.TextChanged += new PropertyChangedEventHandler(accountCompanyTb_TextChanged);
            AccountNameTb.TextChanged += new PropertyChangedEventHandler(accountNameTb_TextChanged);
            AccountNumberNupd.ValueChanged += new PropertyChangedEventHandler(accountNumberNupd_ValueChanged);
            NameTb.ValueChanged += new PropertyChangedEventHandler(nameTb_TextChanged);
            HistoryDirectoryCb.SelectedIndexChanged += new PropertyChangedEventHandler(historyDirectoryCb_SelectedIndexChanged);
            AccountCurrencyTb.TextChanged += new PropertyChangedEventHandler(accountCurrencyTb_TextChanged);
            LeverageNupd.ValueChanged += new PropertyChangedEventHandler(leverageNupd_ValueChanged);
            timer1.Elapsed += new ElapsedEventHandler(timer1_Tick);
        }

        new internal EnvironmentAssistantController Assistant
        {
            get
            {
                return (EnvironmentAssistantController)base.Assistant;
            }
        }

        public Process MetatraderProcess
        {
            get;
            private set;
        }

        public override bool CloseMetatrader
        {
            get
            {
                return CheckBox1.Checked;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        protected override bool OnSetActive()
        {
            if (!base.OnSetActive())
                return false;

            Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.DisabledFinish);

            if (SelectedNextPage == null)
            {
                AccountCompanyTb.Text = "";
                AccountNumberNupd.Value = 1000;
                AccountNameTb.Text = "";
                NameTb.Value = "";
                HistoryDirectoryCb.Clear();
                HistoryDirectoryCb.SelectedItem = null;
                AccountCurrencyTb.Text = "USD";
                LeverageNupd.Value = 100;
            }

            if (string.IsNullOrEmpty(Assistant.UpdatedEnvironment))
            {
                NameTb.ReadOnly = false;

                ImportMetatraderPage1Controller imp = (ImportMetatraderPage1Controller)PreviousPage;
                string[] mt4inf = imp.SelectedImportDirectory;

                string[] path = mt4inf[0].Split('\\');

                if (AccountCompanyTb.Text.Length == 0)
                {
                    AccountCompanyTb.Text = path[path.Length - 1].
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
                if (AccountNameTb.Text.Length == 0)
                {
                    AccountNameTb.Text = System.Environment.MachineName + " " + System.Environment.UserName;
                }
            }
            else
            {
                NameTb.ReadOnly = true;
                NameTb.Value = Assistant.UpdatedEnvironment;

                set(AccountCompanyTb, Assistant.UpdatedEnvironmentData[1]);
                set(AccountNameTb, Assistant.UpdatedEnvironmentData[2]);
                set(AccountNumberNupd, Assistant.UpdatedEnvironmentData[3]);
                set(AccountCurrencyTb, Assistant.UpdatedEnvironmentData[4]);
                set(LeverageNupd, Assistant.UpdatedEnvironmentData[5]);
                set(HistoryDirectoryCb, null, Assistant.UpdatedEnvironmentHistoryDir);
            }

            initialized = true;

            try
            {
                startMetatrader();

                return true;
            }
            catch (IOException ex)
            {
                MessageBoxController.Show(rmiManager, ex.Message + "\n\nHave you exited from the selected Metatrader ?", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ImportMetatraderPage1Controller imp = (ImportMetatraderPage1Controller)PreviousPage;
            CheckBox1.Visible = imp.StartMetatrader;

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
            if (PreviousPage is ImportMetatraderPage1Controller)
            {
                ImportMetatraderPage1Controller imp = (ImportMetatraderPage1Controller)PreviousPage;

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
                if (PreviousPage is ImportMetatraderPage1Controller)
                {
                    ImportMetatraderPage1Controller imp = (ImportMetatraderPage1Controller)PreviousPage;

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
                                    MessageBoxController.Show(rmiManager, "Unable to close Metatarder, please close it manually.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBoxController.Show(rmiManager, ex.Message + "\n\nUnable to close Metatarder, please close it manually.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        if (MetatraderProcess != null)
                        {
                            MetatraderProcess.Close();
                            MetatraderProcess = null;
                        }

                        set(AccountCompanyTb, ImportedParameters[1]);
                        set(AccountNameTb, ImportedParameters[2]);
                        set(AccountNumberNupd, ImportedParameters[3]);
                        set(AccountCurrencyTb, ImportedParameters[4]);
                        set(LeverageNupd, ImportedParameters[5]);
                        set(HistoryDirectoryCb, HistoryDirectories, ImportedParameters[0]);

                        initialized = true;
                        validate();
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBoxController.Show(rmiManager, ex.Message + "\n\nPlease run the \"-- LAUNCH IT NOW -- GreenZoneFxEAImporter\" script in Metatrader* and wait until it finished.\n\n   * It should be attached to a Chart Window. See Metatrader manual.", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                timer1.Start();
            }
        }

        private void set(FieldController<string> tb, string txt)
        {
            if (txt != null)
            {
                txt = txt.Trim();
                if (txt != "")
                {
                    tb.Value = txt.Trim();
                }
            }
        }
        private void set(LabelledController tb, string txt)
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
        private void set(FieldController<int> nupd, string txt)
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

        private void set(ComboController cb, string[] range, string txt)
        {
            cb.Clear();
            if (range != null)
            {
                foreach (var r in range)
                {
                    cb.Add(r);
                }
            }
            else
            {
                cb.Add(txt);
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

                ImportedParameters[1] = AccountCompanyTb.Text == null ? null : AccountCompanyTb.Text.Trim();
                ImportedParameters[2] = AccountNameTb.Text == null ? null : AccountNameTb.Text.Trim();
                ImportedParameters[3] = "" + AccountNumberNupd.Value;
                EnvironmentName = NameTb.Value;
                HistoryDirectory = (string)HistoryDirectoryCb.SelectedItem;
                ImportedParameters[4] = AccountCurrencyTb.Text;
                ImportedParameters[5] = "" + LeverageNupd.Value;


                if (AccountCompanyTb.Text == "")
                {
                    ErrorProvider1[AccountCompanyTb] = ("Account Company should not be empty.");
                    validationError = true;
                }
                else
                {
                    ErrorProvider1[AccountCompanyTb] = (null);
                }


                if (AccountNameTb.Text == "")
                {
                    ErrorProvider1[AccountNameTb] = ("Account Name should not be empty.");
                    validationError = true;
                }
                else
                {
                    ErrorProvider1[AccountNameTb] = (null);
                }


                if (NameTb.Value == "")
                {
                    ErrorProvider1[NameTb] = ("Name should not be empty.");
                    validationError = true;
                }
                else if (Assistant.Environments.Contains(NameTb.Value) && NameTb.Value != Assistant.UpdatedEnvironment)
                {
                    ErrorProvider1[NameTb] = ("Environment already exists.");
                    validationError = true;
                }
                else
                {
                    ErrorProvider1[NameTb] = (null);
                }


                if (HistoryDirectoryCb.SelectedItem == null)
                {
                    ErrorProvider1[HistoryDirectoryCb] = ("History should be selected.");
                    validationError = true;
                }
                else
                {
                    ErrorProvider1[HistoryDirectoryCb] = (null);
                }


                if (AccountCurrencyTb.Text == "")
                {
                    ErrorProvider1[AccountCurrencyTb] = ("Account Currency should not be empty.");
                    validationError = true;
                }
                else
                {
                    ErrorProvider1[AccountCurrencyTb] = (null);
                }


                if (LeverageNupd.Value < 10)
                {
                    ErrorProvider1[LeverageNupd] = ("Account Leverage should be >= 10.");
                    validationError = true;
                }
                else
                {
                    ErrorProvider1[LeverageNupd] = (null);
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

        private void accountCompanyTb_TextChanged(object sender, ControllerEventArgs e)
        {
            if (string.IsNullOrEmpty(Assistant.UpdatedEnvironment))
            {
                NameTb.Value = AccountCompanyTb.Text + ", " + AccountNameTb.Text + ", " + AccountNumberNupd.Value;
            }
            validate();
        }

        private void accountNameTb_TextChanged(object sender, ControllerEventArgs e)
        {
            if (string.IsNullOrEmpty(Assistant.UpdatedEnvironment))
            {
                NameTb.Value = AccountCompanyTb.Text + ", " + AccountNameTb.Text + ", " + AccountNumberNupd.Value;
            }
            validate();
        }

        private void accountNumberNupd_ValueChanged(object sender, ControllerEventArgs e)
        {
            if (string.IsNullOrEmpty(Assistant.UpdatedEnvironment))
            {
                NameTb.Value = AccountCompanyTb.Text + ", " + AccountNameTb.Text + ", " + AccountNumberNupd.Value;
            }
            validate();
        }

        private void nameTb_TextChanged(object sender, ControllerEventArgs e)
        {
            validate();
        }

        private void historyDirectoryCb_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            validate();
        }

        private void accountCurrencyTb_TextChanged(object sender, ControllerEventArgs e)
        {
            validate();
        }

        private void leverageNupd_ValueChanged(object sender, ControllerEventArgs e)
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
