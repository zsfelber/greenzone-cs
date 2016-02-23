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
    public partial class Mt4ExpertInfoPanel : UserControl
    {
        public Mt4ExpertInfoPanel()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, ExpertInfoController controller)
        {
            new SimpleControlVCBinder(context, this, controller);

            new BufferedPropertyGridVCBinder(context, propertyGrid1, controller.PropertyGrid1);
        }
    }
}
