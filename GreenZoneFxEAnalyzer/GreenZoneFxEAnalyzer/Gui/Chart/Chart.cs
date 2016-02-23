using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.Gui.ViewController;
using GreenZoneUtil.ViewController;

namespace GreenZoneFxEngine.Gui.Chart
{
    public partial class Chart : UserControl
    {
        public Chart()
        {
            InitializeComponent();
        }

        protected GreenWinFormsMVContext context;
        public GreenWinFormsMVContext Context
        {
            get
            {
                return context;
            }
        }

        protected ChartController controller;
        public ChartController Controller
        {
            get
            {
                return controller;
            }
        }

        public virtual void Bind(GreenWinFormsMVContext context, ChartController controller)
        {
            this.context = context;
            this.controller = controller;
            new SimpleControlVCBinder(context, this, controller);
        }


    }
}
