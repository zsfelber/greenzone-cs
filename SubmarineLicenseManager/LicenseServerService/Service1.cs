using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Submarine.LicenseServer;


namespace Submarine.LicenseServerService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LicenseServerLauncher.Start();
        }

        protected override void OnStop()
        {
            LicenseServerLauncher.Exit();
        }
    }
}
