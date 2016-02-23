using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GreenZoneFxBuildTasks
{
    static class Program
    {
        [DllImport("Kernel32.dll")]
        static extern Boolean AllocConsole();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if (DEBUG)
            if (!AllocConsole())
                MessageBox.Show("Failed");
            Console.WriteLine("Debug console initialized");
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
