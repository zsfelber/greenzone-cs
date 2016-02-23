using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.Gui.ViewController;

namespace GreenZoneFxEngine.Gui
{
    public partial class IndicatorPanel : UserControl
    {
        public IndicatorPanel()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, IndicatorPanelController controller)
        {
            new SimpleControlVCBinder(context, this, controller);

            if (controller.TabPage1.Parent != null)
            {
                new TabPageVCBinder(context, tabPage1, controller.TabPage1);
            }
            else
            {
                tabControl1.TabPages.Remove(tabPage1);
            }

            if (controller.TabPage2.Parent != null)
            {
                new TabPageVCBinder(context, tabPage2, controller.TabPage2);
            }
            else
            {
                tabControl1.TabPages.Remove(tabPage2);
            }

            if (controller.TabPage3.Parent != null)
            {
                new TabPageVCBinder(context, tabPage3, controller.TabPage3);
            }
            else
            {
                tabControl1.TabPages.Remove(tabPage3);
            }

            new TabControlVCBinder(context, tabControl1, controller);
            indicatorRuntimePanel.Bind(context, controller.IndicatorRuntimePanel);
            new BufferedPropertyGridVCBinder(context, indexesPrgrd, controller.IndexesPrgrd);
            new BufferedPropertyGridVCBinder(context, levelsPrgrd, controller.LevelsPrgrd);
            new ButtonVCBinder(context, reset1Button, controller.Reset1Button);
            new ButtonVCBinder(context, reset2Button, controller.Reset2Button);
            new ButtonVCBinder(context, reset3Button, controller.Reset3Button);
        }
    }
}