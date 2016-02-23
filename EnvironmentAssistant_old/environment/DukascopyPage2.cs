using SMS.Windows.Forms;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GreenZoneFxEngine.Util;
using System.Threading;
using System.Net;
using GreenZoneFxEngine.Trading;
using System.IO;
using SevenZip;
using System.Text;
using GreenZoneFxEngine.Types;


namespace EnvironmentAssistant
{
    public class DukascopyPage2 : AssistantPage
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private OpenFileDialog openFileDialog1;
        private Label dateLabel;
        private Label symbolLabel;
        private ProgressBar symbolProgressBar;
        private ProgressBar dateProgressBar;
        private System.ComponentModel.IContainer components = null;
        private Label toLabel;
        private Label fromLabel;
        private Label estimatedLabel;
        private Label elapsedLabel;
        private TextBox symbolsTb;
        private Label label2;

        BackgroundWorker backgroundWorker;
        object locker = new object();
        IState lastState;

        public DukascopyPage2()
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

        internal datetime FirstUpdatedFileTime
        {
            get;
            set;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dateProgressBar = new System.Windows.Forms.ProgressBar();
            this.symbolProgressBar = new System.Windows.Forms.ProgressBar();
            this.symbolLabel = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.fromLabel = new System.Windows.Forms.Label();
            this.toLabel = new System.Windows.Forms.Label();
            this.estimatedLabel = new System.Windows.Forms.Label();
            this.elapsedLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.symbolsTb = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(471, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "STEP 1 >>  Downloading...";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataMember = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dateProgressBar
            // 
            this.dateProgressBar.Location = new System.Drawing.Point(34, 243);
            this.dateProgressBar.Name = "dateProgressBar";
            this.dateProgressBar.Size = new System.Drawing.Size(429, 10);
            this.dateProgressBar.TabIndex = 2;
            // 
            // symbolProgressBar
            // 
            this.symbolProgressBar.Location = new System.Drawing.Point(34, 173);
            this.symbolProgressBar.Name = "symbolProgressBar";
            this.symbolProgressBar.Size = new System.Drawing.Size(429, 11);
            this.symbolProgressBar.TabIndex = 3;
            // 
            // symbolLabel
            // 
            this.symbolLabel.AutoSize = true;
            this.symbolLabel.Location = new System.Drawing.Point(36, 157);
            this.symbolLabel.Name = "symbolLabel";
            this.symbolLabel.Size = new System.Drawing.Size(35, 13);
            this.symbolLabel.TabIndex = 5;
            this.symbolLabel.Text = "label2";
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(36, 227);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(35, 13);
            this.dateLabel.TabIndex = 6;
            this.dateLabel.Text = "label3";
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.Location = new System.Drawing.Point(35, 204);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(33, 13);
            this.fromLabel.TabIndex = 7;
            this.fromLabel.Text = "From:";
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(219, 204);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(23, 13);
            this.toLabel.TabIndex = 8;
            this.toLabel.Text = "To:";
            // 
            // estimatedLabel
            // 
            this.estimatedLabel.AutoSize = true;
            this.estimatedLabel.Location = new System.Drawing.Point(219, 272);
            this.estimatedLabel.Name = "estimatedLabel";
            this.estimatedLabel.Size = new System.Drawing.Size(56, 13);
            this.estimatedLabel.TabIndex = 10;
            this.estimatedLabel.Text = "Estimated:";
            // 
            // elapsedLabel
            // 
            this.elapsedLabel.AutoSize = true;
            this.elapsedLabel.Location = new System.Drawing.Point(36, 272);
            this.elapsedLabel.Name = "elapsedLabel";
            this.elapsedLabel.Size = new System.Drawing.Size(48, 13);
            this.elapsedLabel.TabIndex = 9;
            this.elapsedLabel.Text = "Elapsed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Symbols:";
            // 
            // symbolsTb
            // 
            this.symbolsTb.Location = new System.Drawing.Point(34, 65);
            this.symbolsTb.Multiline = true;
            this.symbolsTb.Name = "symbolsTb";
            this.symbolsTb.ReadOnly = true;
            this.symbolsTb.Size = new System.Drawing.Size(429, 85);
            this.symbolsTb.TabIndex = 12;
            // 
            // DukascopyPage2
            // 
            this.Controls.Add(this.symbolsTb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.estimatedLabel);
            this.Controls.Add(this.elapsedLabel);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.fromLabel);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.symbolLabel);
            this.Controls.Add(this.symbolProgressBar);
            this.Controls.Add(this.dateProgressBar);
            this.Controls.Add(this.label1);
            this.Name = "DukascopyPage2";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        protected override bool OnSetActive()
        {
            if (!base.OnSetActive())
                return false;

            symbolLabel.Text = "";
            dateLabel.Text = "";

            // Enable both the Next and Back buttons on this page    
            Assistant.SetAssistantButtons(AssistantButton.Back);

            DukascopyPage0 page0 = (DukascopyPage0)PreviousPage.PreviousPage;
            DukascopyPage1 page1 = (DukascopyPage1)PreviousPage;

            StringBuilder stxt = new StringBuilder();
            foreach (var s in page0.SelectedSymbols)
            {
                stxt.Append(s);
                stxt.Append(',');
            }
            if (stxt.Length > 0)
            {
                stxt.Length--;
            }
            symbolsTb.Text = stxt.ToString();

            fromLabel.Text = "From: " + page1.From.ToShortDateString();
            toLabel.Text = "To: " + page1.To.ToShortDateString();

            int hours = (int)(page1.To - page1.From).TotalHours;
            int total = page0.SelectedSymbols.Count * hours;

            symbolProgressBar.Maximum = total;
            dateProgressBar.Maximum = hours;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(bw_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            backgroundWorker.WorkerReportsProgress = true;

            lastState = null;
            backgroundWorker.RunWorkerAsync();

            return true;
        }

        protected override string OnAssistantNext()
        {
            return typeof(DukascopyPage3).Name;
        }

        protected override string OnAssistantBack()
        {
            if (backgroundWorker == null || MessageBox.Show("Do you really want to abort operation?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                stop();
                return typeof(DukascopyPage1).Name;
            }
            else
            {
                return null;
            }
        }

        protected override bool OnAssistantCancel()
        {
            if (backgroundWorker == null || MessageBox.Show("Do you really want to abort operation?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                stop();
                return true;
            }
            else
            {
                return false;
            }
        }

        void stop()
        {
            if (backgroundWorker != null)
            {
                lock (locker)
                {
                    backgroundWorker = null;
                    Monitor.Wait(locker);
                }
            }
        }


        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // update UI with status
            IState s = (IState)e.UserState;
            s.Draw();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
             //Check for cancel
            if (e.Cancelled)
            {
                //Handle the cancellation.
            }
            else
            {
                Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.Next);
            }

            if (lastState != null)
            {
                lastState.Draw();
            }
            backgroundWorker = null;
            // Update UI that data retrieval is complete
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get data
            //foreach to process data
            //Report progress
            Thread.Sleep(1000);
            download();
        }

        void download()
        {
            FirstUpdatedFileTime = datetime.MaxValue;
            DateTime started = DateTime.Now;

            DukascopyPage0 page0 = (DukascopyPage0)PreviousPage.PreviousPage;
            DukascopyPage1 page1 = (DukascopyPage1)PreviousPage;
            using (WebClient Client = new WebClient())
            {
                string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
                string dir = eaRootDir + "/" + page0.EnvironmentName + "/datafeed/";

                int total = 0;
                int NON = 0;
                int non = 0;
                int si = 1;
                foreach (var s in page0.SelectedSymbols)
                {
                    if (!showProgress(new SetLabelState(symbolLabel, "http://www.dukascopy.com/datafeed/" + s + "   ( " + si + " of " + page0.SelectedSymbols.Count + " )"))) return;

                    int hours = 0;
                    DateTime date = page1.From;
                    date = new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);

                    string prev_path = null;
                    while ((page1.To - date).TotalHours >= 1)
                    {
                        string path = s + "/" + date.Year + "/" + (date.Month - 1).ToString("00") + "/" + date.Day.ToString("00") + "/";

                        if (!path.Equals(prev_path))
                        {
                            if (!showProgress(new SetLabelState(dateLabel, "http://www.dukascopy.com/datafeed/" + path))) return;

                            if (prev_path != null)
                            {
                                if (!Directory.EnumerateFileSystemEntries(dir + prev_path, "*", SearchOption.TopDirectoryOnly).GetEnumerator().MoveNext())
                                {
                                    File.WriteAllText(dir + prev_path + "empty", "");
                                }
                            }
                            if (File.Exists(dir + path + "empty"))
                            {
                                date = date.AddDays(1);
                                hours += 24;
                                total += 24;
                                NON += 24;
                                prev_path = path;

                                if (!showProgress(new ProgressState(this, hours, total, NON, 0, started))) return;
                                continue;
                            }
                        }
                        string file0 = date.Hour.ToString("00") + "h_ticks";
                        string file = file0+".bi5";
                        bool exists = File.Exists(dir + path + file);


                        if (page1.UpdateMode==UpdateMode.DOWNLOAD_TICKS_GEN_PERIODS || !exists)
                        {
                            string url = "http://www.dukascopy.com/datafeed/" + path + file;

                            if (!showProgress(new SetLabelState(dateLabel, url))) return;

                            try
                            {
                                Directory.CreateDirectory(dir + path);
                                Client.DownloadFile(url, dir + path + file);
                                if (FirstUpdatedFileTime == datetime.MaxValue)
                                {
                                    FirstUpdatedFileTime = date;
                                }
                            }
                            catch (WebException)
                            {
                                if (File.Exists(dir + path + file))
                                {
                                    File.Delete(dir + path + file);
                                }
                            }
                        }
                        else
                        {
                            non++;
                        }

                        date = date.AddHours(1);
                        hours++;
                        total++;

                        if (!showProgress(new ProgressState(this, hours, total, NON, 0, started))) return;

                        prev_path = path;
                    }
                    si++;
                }
            }

        }

        bool showProgress(IState state)
        {
            lock (locker)
            {
                if (backgroundWorker == null)
                {
                    lastState = state;
                    Monitor.Pulse(locker);
                    //throw new Exception("Cancel");
                    return false;
                }
                else
                {
                    backgroundWorker.ReportProgress(0, state);
                    return true;
                }
            }
        }

        interface IState
        {
            void Draw();
        }

        struct ProgressState : IState {
            internal readonly DukascopyPage2 page;
            internal readonly int hours;
            internal readonly int total;
            internal readonly int NON;
            internal readonly int non;
            internal readonly DateTime started;

            internal ProgressState(DukascopyPage2 page, int hours, int total, int NON, int non, DateTime started) {
                this.page = page;
                this.hours = hours;
                this.total = total;
                this.NON = NON;
                this.non = non;
                this.started = started;
            }

            public void Draw()
            {
                SetProgress(page.dateProgressBar, hours);
                SetProgress(page.symbolProgressBar, total);

                DateTime now = DateTime.Now;
                TimeSpan elapsed = now - started;
                int eld = (int)elapsed.TotalDays;
                SetLabelText(page.elapsedLabel, "Elapsed: " + (eld > 0 ? eld == 1 ? "1 day " : eld + " days " : "") + elapsed.ToString(@"hh\:mm\:ss"));

                long e0 = (long)(elapsed.Ticks * (estTimeNum(page.symbolProgressBar.Maximum, NON, non) / estTimeNum(total, NON, non) - 1));
                TimeSpan estimated = new TimeSpan(e0);
                int estd = (int)estimated.TotalDays;
                SetLabelText(page.estimatedLabel, "Estimated left: " + (estd > 0 ? estd == 1 ? "1 day " : estd + " days " : "") + estimated.ToString(@"hh\:mm\:ss"));
            }
            double estTimeNum(int total, int NON, int non)
            {
                //double result = 50000d * (total - non - NON) + 10d * non + NON;
                double result = total;
                return result;
            }
            void SetLabelText(Label label, string s)
            {
                label.Text = s;
            }
            void SetProgress(ProgressBar bar, int v)
            {
                bar.Value = v;
            }
        }
        struct SetLabelState : IState 
        {
            internal readonly Label label;
            internal readonly string text;

            internal SetLabelState(Label label, string text)
            {
                this.label = label;
                this.text = text;
            }

            public void Draw()
            {
                label.Text = text;
            }
        }
    }
}

