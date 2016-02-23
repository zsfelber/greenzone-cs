using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace Launcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Assembly assembly = Assembly.Load("GreenZoneFxEAnalyzer");
            //Type entryPoint = assembly.GetType("GreenZoneFxEAnalyzer.Program");

            Assembly assembly = Assembly.Load("GreenZoneFxRobots");
            Type entryPoint = assembly.GetType("GreenZoneFxRobots.Program");
            MethodInfo m = entryPoint.GetMethod("Main");

            m.Invoke(null, new object[]{});
        }
    }
}
