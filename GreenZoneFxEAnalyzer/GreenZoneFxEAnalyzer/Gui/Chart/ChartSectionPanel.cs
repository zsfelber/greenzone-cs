using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.Gui.ViewController;

namespace GreenZoneFxEngine.Gui.Chart
{
    public partial class ChartSectionPanel : UserControl
    {
        public ChartSectionPanel()
        {
            InitializeComponent();
        }

        public virtual void Bind(GreenWinFormsMVContext context, ChartSectionPanelController controller)
        {
            new SimpleControlVCBinder(context, this, controller);

            this.priceLabelPane.Bind(context, controller.PriceLabelPane1);
        }

    
    }
}
