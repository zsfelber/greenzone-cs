using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;

using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.IO;
using System.Threading;
using GreenZoneUtil.Util;
using SevenZip;
using System.Globalization;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    
    public class DukascopyPage3Controller : DukascopyPage3ControllerBase
    {
        System.ComponentModel.BackgroundWorker backgroundWorker;
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
        BinaryWriter[] HstBins;

        long lastSavedTickTime;
        long lastSavedBarOrTickTimeMin;
        float[] LastSavedOpen;
        float[] LastSavedLow;
        float[] LastSavedHigh;
        float[] LastSavedClose;
        float[] LastSavedVolume;

        public DukascopyPage3Controller(GreenRmiManager rmiManager, EnvironmentAssistantController assistant)
            : base(rmiManager, assistant)
        {
            ErrorProvider1 = new ChildControlMap<string>(rmiManager);
            OpenFileDialog1 = new FileDialogController(rmiManager);
            DateLabel = new LabelledController(rmiManager, this);
            SymbolLabel = new LabelledController(rmiManager, this);
            SymbolProgressBar = new ProgressTrackController(rmiManager, this);
            DateProgressBar = new ProgressTrackController(rmiManager, this);
            ToLabel = new LabelledController(rmiManager, this);
            FromLabel = new LabelledController(rmiManager, this);
            EstimatedLabel = new LabelledController(rmiManager, this);
            ElapsedLabel = new LabelledController(rmiManager, this);
            SymbolsTb = new LabelledController(rmiManager, this);
        }

        new internal EnvironmentAssistantController Assistant
        {
            get
            {
                return (EnvironmentAssistantController)base.Assistant;
            }
        }

        protected override bool OnSetActive()
        {
            if (!base.OnSetActive())
                return false;

            SymbolLabel.Text = "";
            DateLabel.Text = "";

            // Enable both the Next and Back buttons on this page    
            Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.DisabledFinish);

            DukascopyPage0Controller page0 = (DukascopyPage0Controller)PreviousPage.PreviousPage.PreviousPage;
            DukascopyPage1Controller page1 = (DukascopyPage1Controller)PreviousPage.PreviousPage;
            DukascopyPage2Controller page2 = (DukascopyPage2Controller)PreviousPage;

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
            SymbolsTb.Text = stxt.ToString();

            if (page1.UpdateMode == UpdateMode.UPDATE_ALL && page2.FirstUpdatedFileTime != datetime.MaxValue)
            {
                FromLabel.Text = "From: " + page1.From.ToShortDateString() + "  ( " + page2.FirstUpdatedFileTime.ToShortDateString() + " )";
            }
            else
            {
                FromLabel.Text = "From: " + page1.From.ToShortDateString();
            }
            ToLabel.Text = "To: " + page1.To.ToShortDateString();

            int hours = (int)(page1.To - page1.From).TotalHours;
            int total = page0.SelectedSymbols.Count * hours;

            SymbolProgressBar.Maximum = total;
            DateProgressBar.Maximum = hours;

            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(bw_DoWork);
            backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bw_ProgressChanged);
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
            return typeof(DukascopyPage1Controller).Name;
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


        void bw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            // update UI with status
            IState s = (IState)e.UserState;
            s.Draw();
        }

        void bw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
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

        void bw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
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

            DukascopyPage0Controller page0 = (DukascopyPage0Controller)PreviousPage.PreviousPage.PreviousPage;
            DukascopyPage1Controller page1 = (DukascopyPage1Controller)PreviousPage.PreviousPage;

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
                showProgress(new SetLabelState(SymbolLabel, symbol + "   ( " + si + " of " + page0.SelectedSymbols.Count + " )"));

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
                                hours++;
                                total++;
                                NON++;
                                showProgress(new ProgressState(this, hours, total, NON, non, started));
                                continue;
                            }
                        }

                        string path = symbol + "\\" + date.Year + "\\" + (date.Month - 1).ToString("00") + "\\" + date.Day.ToString("00") + "\\";

                        if (!path.Equals(prev_path))
                        {
                            showProgress(new SetLabelState(DateLabel, path));

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
                            showProgress(new SetLabelState(DateLabel, path + file));

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
                        int p = period.GetOrdinal();
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
                        int p = period.GetOrdinal();
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
                            int p = period.GetOrdinal();
                            if (Hsts[p] != null && File.Exists(Hsts[p]))
                            {
                                File.Delete(Hsts[p]);
                            }
                        }
                    }

                    if (ex.Message != "Cancel")
                    {
                        MessageBoxController.Show(rmiManager, ex.Message + "\n\nUnable to load symbol : " + symbol, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (page1.UpdateMode == UpdateMode.UPDATE_ALL)
                    {
                        MessageBoxController.Show(rmiManager, "Generating timeframe data is not finished, you can continue updating later.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                si++;
            }
        }

        void CheckPeriods()
        {
            DukascopyPage0Controller page0 = (DukascopyPage0Controller)PreviousPage.PreviousPage.PreviousPage;
            DukascopyPage1Controller page1 = (DukascopyPage1Controller)PreviousPage.PreviousPage;

            string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
            string envDir = eaRootDir + "\\" + page0.EnvironmentName;
            string hp = envDir + "\\history\\";

            if (page1.DeleteCorruptPeriods)
            {
                foreach (var p in EnumExtensions.GetPeriods(TimePeriodCategory.MT5 | TimePeriodCategory.SECS))
                {
                    foreach (var symbol in page0.SelectedSymbols)
                    {
                        if (ServerTimeSeriesRuntimeEx.IsSeriesAvailable(hp, symbol, p))
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
                        if (!ServerTimeSeriesRuntimeEx.IsSeriesAvailable(hp, symbol, period))
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
            DukascopyPage0Controller page0 = (DukascopyPage0Controller)PreviousPage.PreviousPage.PreviousPage;
            DukascopyPage1Controller page1 = (DukascopyPage1Controller)PreviousPage.PreviousPage;
            DukascopyPage2Controller page2 = (DukascopyPage2Controller)PreviousPage;

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

                int p = period.GetOrdinal();
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
                                ServerPeriodTimeSeriesRuntime r = (ServerPeriodTimeSeriesRuntime)ServerTimeSeriesRuntimeEx.Create(hp, symbol, period, false, page2.FirstUpdatedFileTime);

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
                                MessageBoxController.Show(rmiManager, "Current HST length is invalid (" + Hsts[p] + ") :  deleting and generating it from scratch..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    else if (period.GetCategory() == TimePeriodCategory.MT4)
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
                        ServerTimeSeriesRuntimeEx r = ServerTimeSeriesRuntimeEx.Create(hp, symbol, TimePeriodConst.PERIOD_TICK, false, page2.FirstUpdatedFileTime);

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
                        MessageBoxController.Show(rmiManager, "Current tick file length is invalid :  deleting and generating it from scratch..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            DukascopyPage0Controller page0 = (DukascopyPage0Controller)PreviousPage.PreviousPage.PreviousPage;
            DukascopyPage1Controller page1 = (DukascopyPage1Controller)PreviousPage.PreviousPage;

            string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
            string envDir = eaRootDir + "\\" + page0.EnvironmentName;
            string hp = envDir + "\\history\\";

            foreach (TimePeriodConst period in availableSymbols[symbol])
            {
                int p = period.GetOrdinal();

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
                            MessageBoxController.Show(rmiManager, "Period file " + symbol + " " + period.GetShortTxt() + " is invalid :\n\n" + b, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBoxController.Show(rmiManager, "IO Error during operation, press OK to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            int p = period.GetOrdinal();
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

        struct ProgressState : IState
        {
            internal readonly DukascopyPage3Controller page;
            internal readonly int hours;
            internal readonly int total;
            internal readonly int NON;
            internal readonly int non;
            internal readonly DateTime started;

            internal ProgressState(DukascopyPage3Controller page, int hours, int total, int NON, int non, DateTime started)
            {
                this.page = page;
                this.hours = hours;
                this.total = total;
                this.NON = NON;
                this.non = non;
                this.started = started;
            }

            public void Draw()
            {
                SetProgress(page.DateProgressBar, hours);
                SetProgress(page.SymbolProgressBar, total);

                DateTime now = DateTime.Now;
                TimeSpan elapsed = now - started;
                int eld = (int)elapsed.TotalDays;
                SetLabelText(page.ElapsedLabel, "Elapsed: " + (eld > 0 ? eld == 1 ? "1 day " : eld + " days " : "") + elapsed.ToString(@"hh\:mm\:ss"));

                long e0 = (long)(elapsed.Ticks * (estTimeNum(page.SymbolProgressBar.Maximum, NON, non) / estTimeNum(total, NON, non) - 1));
                TimeSpan estimated = new TimeSpan(e0);
                int estd = (int)estimated.TotalDays;
                SetLabelText(page.EstimatedLabel, "Estimated left: " + (estd > 0 ? estd == 1 ? "1 day " : estd + " days " : "") + estimated.ToString(@"hh\:mm\:ss"));
            }
            double estTimeNum(int total, int NON, int non)
            {
                //double result = 50000d * (total - non - NON) + 10d * non + NON;
                double result = total;
                return result;
            }
            void SetLabelText(LabelledController label, string s)
            {
                label.Text = s;
            }
            void SetProgress(ProgressTrackController bar, int v)
            {
                bar.Value = v;
            }
        }
        struct SetLabelState : IState
        {
            internal readonly LabelledController label;
            internal readonly string text;

            internal SetLabelState(LabelledController label, string text)
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
