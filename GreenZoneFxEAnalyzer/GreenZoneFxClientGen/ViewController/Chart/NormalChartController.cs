using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;

using GreenZoneFxEngine.Types;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class NormalChartController : ClientNormalChartControllerBase
    {
        
        public NormalChartController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected override void SaveSessionAsync()
        {
            ((MainWindowController)MainWindow).SaveSessionAsync();
        }
        
        public override void UpdateCursor()
        {
            if (ChartPanel != null)
            {
                ChartPanel.CursorChart.UpdateCursor();
            }
            base.UpdateCursor();
        }

        public override void UpdateChartOnScreen()
        {
            if (ChartPanel != null)
            {
                ChartPanel.CursorChart.UpdateChartOnScreen();
            }
            base.UpdateChartOnScreen();
        }

        public override void ParentUpdateAllChartAndCursor()
        {
            if (Owner.IsCursorBarConnected)
            {
                ChartGroupPanel.UpdateAllChartAndCursor(ChartPanel);
            }
        }

    }


}
