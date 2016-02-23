using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.Gui.ViewController;

namespace GreenZoneFxEngine
{
    public partial class Mt4IndicatorInfoPanel : UserControl
    {
        public Mt4IndicatorInfoPanel()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, IndicatorInfoController controller)
        {
            new SimpleControlVCBinder(context, this, controller);

            new BufferedPropertyGridVCBinder(context, propertyGrid1, controller.PropertyGrid1);
        }
    }
}
