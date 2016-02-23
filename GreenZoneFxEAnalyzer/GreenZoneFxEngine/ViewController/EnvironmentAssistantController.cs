using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Assistant;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    public class EnvironmentAssistantController : EnvironmentAssistantControllerBase
    {
        internal EnvironmentAssistantController(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
            Add((Controller)(StartPageController = new StartPageController(rmiManager, this)));
            Add((Controller)(SelectEnvTypePageController = new SelectEnvTypePageController(rmiManager, this)));
            Add((Controller)(ImportMetatraderPage1Controller = new ImportMetatraderPage1Controller(rmiManager, this)));
            Add((Controller)(ImportMetatraderPage2Controller = new ImportMetatraderPage2Controller(rmiManager, this)));
            Add((Controller)(DukascopyPage0Controller = new DukascopyPage0Controller(rmiManager, this)));
            Add((Controller)(DukascopyPage1Controller = new DukascopyPage1Controller(rmiManager, this)));
            Add((Controller)(DukascopyPage2Controller = new DukascopyPage2Controller(rmiManager, this)));
            Add((Controller)(DukascopyPage3Controller = new DukascopyPage3Controller(rmiManager, this)));
        }

        internal EnvironmentAssistantController(GreenRmiManager rmiManager, string updatedEnvironment, string updatedEnvironmentDir, string updatedEnvironmentType, string updatedEnvironmentHistoryDir, string[] updatedEnvironmentData, ISet<string> environments)
            : this(rmiManager)
        {
            UpdatedEnvironment = updatedEnvironment;
            UpdatedEnvironmentDir = updatedEnvironmentDir;
            UpdatedEnvironmentType = updatedEnvironmentType;
            UpdatedEnvironmentData = updatedEnvironmentData;
            UpdatedEnvironmentHistoryDir = updatedEnvironmentHistoryDir;
            Environments = environments;
        }


    }
}

