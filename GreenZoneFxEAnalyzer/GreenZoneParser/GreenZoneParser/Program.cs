using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;

namespace GreenZoneParser
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
            Application.Run(new ParseTesterForm());
        }
    }
}