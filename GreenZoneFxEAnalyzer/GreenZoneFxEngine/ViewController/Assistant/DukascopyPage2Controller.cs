using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using System.Threading;
using GreenZoneFxEngine.Types;
using System.Net;
using GreenZoneFxEngine.Trading;
using System.IO;

using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    
    public class DukascopyPage2Controller : DukascopyPage2ControllerBase
    {
        System.ComponentModel.BackgroundWorker backgroundWorker;
        object locker = new object();
        IState lastState;

        public DukascopyPage2Controller(GreenRmiManager rmiManager, EnvironmentAssistantController assistant)
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
            Label2 = new LabelledController(rmiManager, this);
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
            Assistant.SetAssistantButtons(AssistantButton.Back);

            DukascopyPage0Controller page0 = (DukascopyPage0Controller)PreviousPage.PreviousPage;
            DukascopyPage1Controller page1 = (DukascopyPage1Controller)PreviousPage;

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

            FromLabel.Text = "From: " + page1.From.ToShortDateString();
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

        protected override string OnAssistantNext()
        {
            return typeof(DukascopyPage3Controller).Name;
        }

        protected override string OnAssistantBack()
        {
            if (backgroundWorker == null || MessageBoxController.Show(rmiManager, "Do you really want to abort operation?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                stop();
                return typeof(DukascopyPage1Controller).Name;
            }
            else
            {
                return null;
            }
        }

        protected override bool OnAssistantCancel()
        {
            if (backgroundWorker == null || MessageBoxController.Show(rmiManager, "Do you really want to abort operation?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.Next);
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
            download();
        }

        void download()
        {
            FirstUpdatedFileTime = datetime.MaxValue;
            DateTime started = DateTime.Now;

            DukascopyPage0Controller page0 = (DukascopyPage0Controller)PreviousPage.PreviousPage;
            DukascopyPage1Controller page1 = (DukascopyPage1Controller)PreviousPage;
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
                    if (!showProgress(new SetLabelState(SymbolLabel, "http://www.dukascopy.com/datafeed/" + s + "   ( " + si + " of " + page0.SelectedSymbols.Count + " )"))) return;

                    int hours = 0;
                    DateTime date = page1.From;
                    date = new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);

                    string prev_path = null;
                    while ((page1.To - date).TotalHours >= 1)
                    {
                        string path = s + "/" + date.Year + "/" + (date.Month - 1).ToString("00") + "/" + date.Day.ToString("00") + "/";

                        if (!path.Equals(prev_path))
                        {
                            if (!showProgress(new SetLabelState(DateLabel, "http://www.dukascopy.com/datafeed/" + path))) return;

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
                        string file = file0 + ".bi5";
                        bool exists = File.Exists(dir + path + file);


                        if (page1.UpdateMode == UpdateMode.DOWNLOAD_TICKS_GEN_PERIODS || !exists)
                        {
                            string url = "http://www.dukascopy.com/datafeed/" + path + file;

                            if (!showProgress(new SetLabelState(DateLabel, url))) return;

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

        struct ProgressState : IState
        {
            internal readonly DukascopyPage2Controller page;
            internal readonly int hours;
            internal readonly int total;
            internal readonly int NON;
            internal readonly int non;
            internal readonly DateTime started;

            internal ProgressState(DukascopyPage2Controller page, int hours, int total, int NON, int non, DateTime started)
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