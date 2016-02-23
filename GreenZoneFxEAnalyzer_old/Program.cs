using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace GreenZoneFxEngine
{
    static class Program
    {
        static Thread thread;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            File.AppendAllText("fxanalyzer.out", "GreenZoneFxEAnalyzer.Main()  starting" + "\n");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            File.AppendAllText("fxanalyzer.out", "GreenZoneFxEAnalyzer.Main()  started" + "\n");
        }

        public static void StartInThread()
        {
            thread = new Thread(new ThreadStart(Main));
            thread.Start();
        }

        public static void Stop()
        {
            File.AppendAllText("fxanalyzer.out", "GreenZoneFxEAnalyzer.Main()  stopping" + "\n");
            foreach (Form f in Application.OpenForms)
            {
                if (f is Form1)
                {
                    f.Close();
                    break;
                }
            }
            File.AppendAllText("fxanalyzer.out", "GreenZoneFxEAnalyzer.Main()  stopped" + "\n");
        }
    }
}
