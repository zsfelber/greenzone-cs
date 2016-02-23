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
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Types;
using System.Globalization;
using System.Collections.Generic;


namespace EnvironmentAssistant
{
    public class DukascopyPage3 : AssistantPage
    {
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

        Dictionary<string, List<TimePeriodConst>> availableSymbols = new Dictionary<string, List<TimePeriodConst>>();

        int[] Bars;
        int[] LastBarTime;
        datetime[] LastBarDtime;
        float[] LastOpen;
        float[] LastLow;
        float[] LastHigh;
        float[] LastClose;
        float[] LastVolume;
        string[] Hsts;
        FileStream[] HstStreams;
        private Label label1;
        BinaryWriter[] HstBins;

        long lastSavedTickTime;
        long lastSavedBarOrTickTimeMin;
        float[] LastSavedOpen;
        float[] LastSavedLow;
        float[] LastSavedHigh;
        float[] LastSavedClose;
        float[] LastSavedVolume;

        public DukascopyPage3()
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
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
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
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(471, 26);
            this.label1.TabIndex = 13;
            this.label1.Text = "STEP 2 >>  Generating...";
            // 
            // DukascopyPage3
            // 
            this.Controls.Add(this.label1);
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
            this.Name = "DukascopyPage3";
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
            Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.DisabledFinish);

            DukascopyPage0 page0 = (DukascopyPage0)PreviousPage.PreviousPage.PreviousPage;
            DukascopyPage1 page1 = (DukascopyPage1)PreviousPage.PreviousPage;
            DukascopyPage2 page2 = (DukascopyPage2)PreviousPage;

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

            if (page1.UpdateMode == UpdateMode.UPDATE_ALL && page2.FirstUpdatedFileTime!=datetime.MaxValue)
            {
                fromLabel.Text = "From: " + page1.From.ToShortDateString()+"  ( "+page2.FirstUpdatedFileTime.ToShortDateString()+" )";
            }
            else
            {
                fromLabel.Text = "From: " + page1.From.ToShortDateString();
            }
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

        protected override bool OnAssistantFinish()
        {
            return true;
        }

        protected override string OnAssistantBack()
        {
            stop();
            return typeof(DukascopyPage1).Name;
        }

        protected override bool OnAssistantCancel()
        {
            stop();
            return true;
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
                Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.Finish);
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
            convert();
        }

        void convert()
        {
            DateTime started = DateTime.Now;

            DukascopyPage0 page0 = (DukascopyPage0)PreviousPage.PreviousPage.PreviousPage;
            DukascopyPage1 page1 = (DukascopyPage1)PreviousPage.PreviousPage;

            string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
            string envDir = eaRootDir + "\\" + page0.EnvironmentName;
            string hp = envDir + "\\history\\";
            Directory.CreateDirectory(hp);

            string dir = envDir + "\\datafeed\\";

            int total = 0;
            int NON = 0;
            int non = 0;
            int si = 1;

            ////////////////////////////////////////////
            CheckPeriods();
            ////////////////////////////////////////////

            foreach (var symbol in page0.SelectedSymbols)
            {
                showProgress(new SetLabelState(symbolLabel, symbol + "   ( " + si + " of " + page0.SelectedSymbols.Count + " )"));

                Bars = new int[50];
                LastBarTime = new int[50];
                LastBarDtime = new datetime[50];
                LastOpen = new float[50];
                LastLow = new float[50];
                LastHigh = new float[50];
                LastClose = new float[50];
                LastVolume = new float[50];
                Hsts = new string[50];
                HstStreams = new FileStream[50];
                HstBins = new BinaryWriter[50];
                if (page1.UpdateMode == UpdateMode.UPDATE_ALL)
                {
                    LastSavedOpen = new float[50];
                    LastSavedLow = new float[50];
                    LastSavedHigh = new float[50];
                    LastSavedClose = new float[50];
                    LastSavedVolume = new float[50];
                    lastSavedBarOrTickTimeMin = long.MaxValue;
                }

                FileStream fs = null;
                BinaryWriter bw = null;
                try
                {
                    ////////////////////////////////////////////
                    OpenFiles(symbol, out fs, out bw);
                    ////////////////////////////////////////////

                    DateTime date = page1.From;
                    int hours = 0;
                    string prev_path = null;
                    while ((page1.To - date).TotalHours >= 1)
                    {
                        if (page1.UpdateMode == UpdateMode.UPDATE_ALL)
                        {
                            datetime nextDate = date.AddHours(1);

                            bool skip = (long)nextDate <= lastSavedBarOrTickTimeMin;

                            if (skip)
                            {
                                date = date.AddHours(1);
                                hours ++;
                                total ++;
                                NON ++;
                                showProgress(new ProgressState(this, hours, total, NON, non, started));
                                continue;
                            }
                        }

                        string path = symbol + "\\" + date.Year + "\\" + (date.Month - 1).ToString("00") + "\\" + date.Day.ToString("00") + "\\";

                        if (!path.Equals(prev_path))
                        {
                            showProgress(new SetLabelState(dateLabel, path));

                            if (File.Exists(dir + path + "empty"))
                            {
                                date = date.AddDays(1);
                                hours += 24;
                                total += 24;
                                NON += 24;
                                prev_path = path;

                                showProgress(new ProgressState(this, hours, total, NON, non, started));
                                continue;
                            }
                        }
                        string file0 = date.Hour.ToString("00") + "h_ticks";
                        string file = file0 + ".bi5";

                        if (File.Exists(dir + path + file))
                        {
                            showProgress(new SetLabelState(dateLabel, path + file));

                            FileStream bi5 = File.OpenRead(dir + path + file);
                            try
                            {
                                MemoryStream ms = new MemoryStream();
                                LZMA.Decompress(bi5, ms);

                                ms.Seek(0, SeekOrigin.Begin);
                                BinaryReaderBE r = new BinaryReaderBE(ms);

                                for (int i = 0; i < ms.Length; i += 20)
                                {
                                    int dt1 = r.ReadInt32();
                                    int _ask = r.ReadInt32();
                                    int _bid = r.ReadInt32();
                                    float askVol = r.ReadSingle();
                                    float bidVol = r.ReadSingle();
                                    float volume = Math.Min(bidVol, askVol);

                                    float ask = (float)Math.Round(_ask * 0.00001f, 5);
                                    float bid = (float)Math.Round(_bid * 0.00001f, 5);

                                    DateTime d0 = date.AddMilliseconds(dt1);
                                    datetime d1 = new datetime(d0);
                                    long d1long = (long)d1;
                                    int d1int = (int)d1;

                                    bool skip;
                                    if (page1.UpdateMode == UpdateMode.UPDATE_ALL)
                                    {
                                        skip = d1long <= lastSavedTickTime;
                                    }
                                    else
                                    {
                                        skip = false;
                                    }

                                    if (!skip)
                                    {
                                        bw.Write(d1long);
                                        bw.Write(volume);
                                        bw.Write(bid);
                                        bw.Write(ask);
                                    }

                                    ////////////////////////////////////////////
                                    ProcessHSTs(symbol, bid, volume, d0, d1, d1int);
                                    ////////////////////////////////////////////
                                }
                            }
                            catch (LZMAException)
                            {
                            }
                            finally
                            {
                                bi5.Close();
                            }
                        }
                        else
                        {
                            non++;
                        }

                        date = date.AddHours(1);
                        hours++;
                        total++;
                        prev_path = path;

                        showProgress(new ProgressState(this, hours, total, NON, non, started));
                    }

                    bw.Close();
                    fs.Close();
                    foreach (TimePeriodConst period in page1.AllSelectedPeriods)
                    {
                        int p = period.Id;
                        if (HstBins[p] != null)
                        {
                            HstBins[p].Close();
                        }
                        if (HstStreams[p] != null)
                        {
                            HstStreams[p].Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (bw != null)
                    {
                        bw.Close();
                    }
                    if (fs != null)
                    {
                        fs.Close();
                    }
                    foreach (TimePeriodConst period in page1.AllSelectedPeriods)
                    {
                        int p = period.Id;
                        if (HstBins[p] != null)
                        {
                            HstBins[p].Close();
                        }
                        if (HstStreams[p] != null)
                        {
                            HstStreams[p].Close();
                        }
                    }

                    if (ex.Message != "Cancel")
                    {
                        if (File.Exists(hp + symbol + ".ticks"))
                        {
                            File.Delete(hp + symbol + ".ticks");
                        }
                        foreach (TimePeriodConst period in page1.AllSelectedPeriods)
                        {
                            int p = period.Id;
                            if (Hsts[p] != null && File.Exists(Hsts[p]))
                            {
                                File.Delete(Hsts[p]);
                            }
                        }
                    }

                    if (ex.Message != "Cancel")
                    {
                        MessageBox.Show(ex.Message + "\n\nUnable to load symbol : " + symbol, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (page1.UpdateMode == UpdateMode.UPDATE_ALL)
                    {
                        MessageBox.Show("Generating timeframe data is not finished, you can continue updating later.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                si++;
            }
        }

        void CheckPeriods()
        {
            DukascopyPage0 page0 = (DukascopyPage0)PreviousPage.PreviousPage.PreviousPage;
            DukascopyPage1 page1 = (DukascopyPage1)PreviousPage.PreviousPage;

            string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
            string envDir = eaRootDir + "\\" + page0.EnvironmentName;
            string hp = envDir + "\\history\\";

            if (page1.DeleteCorruptPeriods)
            {
                foreach (var p in EnumExtensions.GetPeriods(TimeSeriesCategory.MT5 | TimeSeriesCategory.SECS))
                {
                    foreach (var symbol in page0.SelectedSymbols)
                    {
                        if (TimeSeriesRuntime.IsSeriesAvailable(hp, symbol, p))
                        {
                            if (!page1.AllSelectedPeriods.Contains(p))
                            {
                                string corruptHst = hp + symbol + (int)p + ".hst";
                                File.Delete(corruptHst);
                            }
                        }
                    }
                }
            }

            availableSymbols.Clear();

            if (page1.UpdateMode == UpdateMode.UPDATE_ALL)
            {
                foreach (var symbol in page0.SelectedSymbols)
                {
                    availableSymbols[symbol] = new List<TimePeriodConst>(page1.AllSelectedPeriods);

                    foreach (TimePeriodConst period in new List<TimePeriodConst>(page1.CustomPeriods))
                    {
                        if (!TimeSeriesRuntime.IsSeriesAvailable(hp, symbol, period))
                        {
                            availableSymbols[symbol].Remove(period);
                        }
                    }
                }
            }
            else
            {
                foreach (var symbol in page0.SelectedSymbols)
                {
                    availableSymbols[symbol] = page1.AllSelectedPeriods;
                }
            }
        }

        void OpenFiles(string symbol, out FileStream fs, out BinaryWriter bw)
        {
            fs = null;
            bw = null;
            DukascopyPage0 page0 = (DukascopyPage0)PreviousPage.PreviousPage.PreviousPage;
            DukascopyPage1 page1 = (DukascopyPage1)PreviousPage.PreviousPage;
            DukascopyPage2 page2 = (DukascopyPage2)PreviousPage;

            string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
            string envDir = eaRootDir + "\\" + page0.EnvironmentName;
            string hp = envDir + "\\history\\";

            float point;
            int digits;
            // TODO Digits
            if (symbol.StartsWith("USDJPY"))
            {
                point = 0.001f;
                digits = 3;
            }
            else
            {
                point = 0.00001f;
                digits = 5;
            }

            foreach (TimePeriodConst period in page1.AllSelectedPeriods)
            {

                int p = period.Id;
                Hsts[p] = hp + symbol + (int)period + ".hst";
                FileInfo barFileInfo = new FileInfo(Hsts[p]);
                long len;

                if (page1.UpdateMode == UpdateMode.UPDATE_ALL)
                {
                    if (barFileInfo.Exists)
                    {
                        len = barFileInfo.Length;
                        if (barFileInfo.Length > 0)
                        {
                            const int headerLen = 4 + 64 + 12 + 4 + 4 + 15 * 4;
                            const int recordLen = 4 + 8 + 8 + 8 + 8 + 8;

                            if ((barFileInfo.Length - headerLen) % recordLen == 0)
                            {
                                PeriodTimeSeriesRuntime r = (PeriodTimeSeriesRuntime)TimeSeriesRuntime.Create(hp, symbol, period, false, page2.FirstUpdatedFileTime);

                                // NOTE reverse indexing...
                                int i = 0;
                                for (; r.GetTime(i) > page2.FirstUpdatedFileTime; i++) ;

                                long tol = (long)r.GetTime(i);
                                int toi = (int)r.GetTime(i);
                                lastSavedBarOrTickTimeMin = Math.Min(lastSavedBarOrTickTimeMin, tol);
                                LastBarDtime[p] = toi;
                                LastBarTime[p] = toi;
                                LastSavedOpen[p] = (float)r.GetOpen(i);
                                LastSavedLow[p] = (float)r.GetLow(i);
                                LastSavedHigh[p] = (float)r.GetHigh(i);
                                LastSavedClose[p] = (float)r.GetClose(i);
                                LastSavedVolume[p] = (float)r.GetVolume(i);
                                HstStreams[p] = File.Open(Hsts[p], FileMode.Open, FileAccess.Write);
                                HstStreams[p].Seek(r.HeaderLen + (r.TotalFileOffset - i + 1) * r.RecordLen, SeekOrigin.Begin);
                            }
                            else
                            {
                                MessageBox.Show("Current HST length is invalid (" + Hsts[p] + ") :  deleting and generating it from scratch..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                File.Delete(Hsts[p]);
                                lastSavedBarOrTickTimeMin = 0;
                                HstStreams[p] = File.Open(Hsts[p], FileMode.Append, FileAccess.Write);
                            }
                        }
                        else
                        {
                            lastSavedBarOrTickTimeMin = 0;
                            HstStreams[p] = File.Open(Hsts[p], FileMode.Append, FileAccess.Write);
                        }
                    }
                    else if (period.Category==TimeSeriesCategory.MT4)
                    {
                        len = 0;
                        lastSavedBarOrTickTimeMin = 0;
                        HstStreams[p] = File.Open(Hsts[p], FileMode.Append, FileAccess.Write);
                    }
                    else
                    {
                        len = 0;
                        Hsts[p] = null;
                    }
                }
                else
                {
                    len = 0;
                    HstStreams[p] = File.Open(Hsts[p], FileMode.Create, FileAccess.Write);
                }

                if (Hsts[p] != null)
                {
                    HstBins[p] = new BinaryWriter(HstStreams[p], Encoding.ASCII);
                    if (len == 0)
                    {
                        const int i_version = 400;
                        string c_copyright = "(C)opyright 2003, MetaQuotes Software Corp.";
                        HstBins[p].Write(i_version);
                        HstBins[p].Write(c_copyright.PadRight(64, '\0').ToCharArray());
                        HstBins[p].Write(symbol.PadRight(12, '\0').ToCharArray());
                        HstBins[p].Write(p);
                        HstBins[p].Write(digits);
                        HstBins[p].Write("".PadRight(15 * 4, '\0').ToCharArray());// unused
                    }
                }
            }

            FileInfo ticksFileInfo = new FileInfo(hp + symbol + ".ticks");
            if (page1.UpdateMode == UpdateMode.UPDATE_ALL && ticksFileInfo.Exists)
            {
                if (ticksFileInfo.Length > 0)
                {
                    if (ticksFileInfo.Length % 20 == 0)
                    {
                        TimeSeriesRuntime r = TimeSeriesRuntime.Create(hp, symbol, TimePeriodConst.PERIOD_TICK, false, page2.FirstUpdatedFileTime);

                        // NOTE reverse indexing...
                        int i = 0;
                        for (; r.GetTime(i) > page2.FirstUpdatedFileTime; i++) ;

                        long tol = (long)r.GetTime(i);
                        int toi = (int)r.GetTime(i);
                        lastSavedTickTime = tol;
                        fs = File.Open(hp + symbol + ".ticks", FileMode.Open, FileAccess.Write);
                        fs.Seek(r.HeaderLen + (r.TotalFileOffset - i + 1) * r.RecordLen, SeekOrigin.Begin);
                    }
                    else
                    {
                        MessageBox.Show("Current tick file length is invalid :  deleting and generating it from scratch..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        File.Delete(hp + symbol + ".ticks");
                        lastSavedTickTime = 0;
                        fs = File.Open(hp + symbol + ".ticks", FileMode.Append, FileAccess.Write);
                    }
                }
                else
                {
                    lastSavedTickTime = 0;
                    fs = File.Open(hp + symbol + ".ticks", FileMode.Append, FileAccess.Write);
                }
            }
            else
            {
                fs = File.Open(hp + symbol + ".ticks", FileMode.Create, FileAccess.Write);
            }
            bw = new BinaryWriter(fs);
            lastSavedBarOrTickTimeMin = Math.Min(lastSavedBarOrTickTimeMin, lastSavedTickTime);
        }

        void ProcessHSTs(string symbol, float bid, float volume, DateTime d0, datetime d1, int d1int)
        {
            DukascopyPage0 page0 = (DukascopyPage0)PreviousPage.PreviousPage.PreviousPage;
            DukascopyPage1 page1 = (DukascopyPage1)PreviousPage.PreviousPage;

            string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
            string envDir = eaRootDir + "\\" + page0.EnvironmentName;
            string hp = envDir + "\\history\\";

            foreach (TimePeriodConst period in availableSymbols[symbol])
            {
                int p = period.Id;

                if (page1.UpdateMode == UpdateMode.UPDATE_ALL)
                {
                    bool skip = d1int < LastBarTime[p];
                    if (skip)
                    {
                        continue;
                    }
                }

                //---- new bar?
                bool newBar = false;
                DateTime cur_open_0 = new DateTime();
                datetime cur_open_1 = 0;

                if (LastBarTime[p] == 0)
                {
                    newBar = true;
                }

                int psec = 0;

                switch (period)
                {
                    case TimePeriodConst.PERIOD_MN1:
                        // monthly timeframe
                        if (d1.Month != LastBarDtime[p].Month)
                        {
                            newBar = true;
                        }
                        if (newBar)
                        {
                            cur_open_0 = (DateTime)d1;
                            cur_open_0 -= new TimeSpan(cur_open_0.Day - 1, cur_open_0.Hour, cur_open_0.Minute, cur_open_0.Second);
                            cur_open_1 = cur_open_0;
                        }
                        break;
                    case TimePeriodConst.PERIOD_W1:
                        int w0 = LastBarDtime[p].GetWeekOfYear(CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);
                        int w1 = d1.GetWeekOfYear(CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);
                        if (w0 != w1)
                        {
                            newBar = true;
                        }

                        if (newBar)
                        {
                            cur_open_0 = (DateTime)d1;
                            // Sunday
                            cur_open_0 -= new TimeSpan((int)cur_open_0.DayOfWeek, cur_open_0.Hour, cur_open_0.Minute, cur_open_0.Second);
                            cur_open_1 = cur_open_0;
                        }
                        break;
                    default:
                        psec = period.GetSecs();
                        break;
                }
                if (psec > 0)
                {
                    int opdt = d1int / psec * psec;
                    if (LastBarTime[p] != opdt)
                    {
                        newBar = true;
                    }
                    if (newBar)
                    {
                        cur_open_1 = opdt;
                        cur_open_0 = (DateTime)cur_open_1;
                    }
                }
                if (newBar)
                {
                    if (Bars[p] > 0)
                    {
                        WriteHSTBar(period);
                    }
                    else if (page1.UpdateMode == UpdateMode.UPDATE_ALL && LastBarTime[p] != 0)
                    {
                        StringBuilder b = new StringBuilder();
                        if (LastSavedOpen[p] != LastOpen[p])
                        {
                            b.Append("Open  " + LastSavedOpen[p] + " != " + LastOpen[p] + "\n");
                        }
                        if (LastSavedLow[p] != LastLow[p])
                        {
                            b.Append("Low  " + LastSavedLow[p] + " != " + LastLow[p] + "\n");
                        }
                        if (LastSavedHigh[p] != LastHigh[p])
                        {
                            b.Append("High  " + LastSavedHigh[p] + " != " + LastHigh[p] + "\n");
                        }
                        if (LastSavedClose[p] != LastClose[p])
                        {
                            b.Append("Close  " + LastSavedClose[p] + " != " + LastClose[p] + "\n");
                        }
                        if (LastSavedVolume[p] != LastVolume[p])
                        {
                            b.Append("Volume  " + LastSavedVolume[p] + " != " + LastVolume[p] + "\n");
                        }
                        if (b.Length > 0)
                        {
                            MessageBox.Show("Period file " + symbol + " " + period.GetShortTxt() + " is invalid :\n\n" + b, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    LastBarTime[p] = (int)cur_open_1;
                    LastBarDtime[p] = cur_open_0;
                    LastOpen[p] = bid;
                    LastLow[p] = bid;
                    LastHigh[p] = bid;
                    LastClose[p] = bid;
                    //if (volume > 0)
                    //{
                    LastVolume[p] = volume;
                    //}
                    //else
                    //{
                    //    LastVolume[p] = 1;
                    //}
                    Bars[p]++;
                }
                else
                {
                    if (LastHigh[p] == 0)
                    {
                        if (page1.UpdateMode == UpdateMode.UPDATE_ALL)
                        {
                            LastOpen[p] = bid;
                            LastLow[p] = bid;
                            LastHigh[p] = bid;
                            LastVolume[p] = 0;
                        }
                        else
                        {
                            MessageBox.Show("IO Error during operation, press OK to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    //---- check for minimum and maximum
                    if (LastLow[p] > bid) LastLow[p] = bid;
                    if (LastHigh[p] < bid) LastHigh[p] = bid;
                    LastClose[p] = bid;
                    LastVolume[p] += volume;
                }
            }
        }
        

        void WriteHSTBar(TimePeriodConst period)
        {
            int p = period.Id;
            BinaryWriter bw = HstBins[p];
            bw.Write(LastBarTime[p]);
            bw.Write((double)LastOpen[p]);
            bw.Write((double)LastLow[p]);
            bw.Write((double)LastHigh[p]);
            bw.Write((double)LastClose[p]);
            bw.Write((double)LastVolume[p]);
        }

        void showProgress(IState state)
        {
            lock (locker)
            {
                if (backgroundWorker == null)
                {
                    lastState = state;
                    Monitor.Pulse(locker);
                    throw new Exception("Cancel");
                }
                else
                {
                    backgroundWorker.ReportProgress(0, state);
                }
            }
        }

        interface IState
        {
            void Draw();
        }

        struct ProgressState : IState {
            internal readonly DukascopyPage3 page;
            internal readonly int hours;
            internal readonly int total;
            internal readonly int NON;
            internal readonly int non;
            internal readonly DateTime started;

            internal ProgressState(DukascopyPage3 page, int hours, int total, int NON, int non, DateTime started) {
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

