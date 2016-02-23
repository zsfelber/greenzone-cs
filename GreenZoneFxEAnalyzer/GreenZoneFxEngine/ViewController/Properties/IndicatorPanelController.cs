using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    public class IndicatorPanelController : IndicatorPanelControllerBase
    {
        public IndicatorPanelController(GreenRmiManager rmiManager, IndicatorDialogController parent)
            : base(rmiManager, parent)
        {
            this.Name = "IndicatorPanelController";

            TabPage1 = new TabPageController(rmiManager, this, "Indicator", null);
            TabPage2 = new TabPageController(rmiManager, this, "Buffer styles", null);
            TabPage3 = new TabPageController(rmiManager, this, "Level styles", null);
            IndicatorRuntimePanel = new IndicatorPropertiesController(rmiManager, (TabPageController)TabPage1);
            IndexesPrgrd = new BufferedPropertyGridController(rmiManager, TabPage2);
            LevelsPrgrd = new BufferedPropertyGridController(rmiManager, TabPage3);
            Reset1Button = new ButtonController(rmiManager, (Controller)IndicatorRuntimePanel);
            Reset2Button = new ButtonController(rmiManager, IndexesPrgrd);
            Reset3Button = new ButtonController(rmiManager, LevelsPrgrd);

            Reset1Button.Pressed += new ControllerEventHandler(reset1Button_Click);
        }

        public new IndicatorPropertiesController IndicatorRuntimePanel
        {
            get
            {
                return (IndicatorPropertiesController)base.IndicatorRuntimePanel;
            }
            protected set
            {
                base.IndicatorRuntimePanel = value;
            }
        }


        private void reset1Button_Click(object sender, ControllerEventArgs e)
        {
            IndicatorRuntimePanel.Reset();
        }
    }
}
