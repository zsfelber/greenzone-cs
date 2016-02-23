﻿using System;
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
    public partial class EnvironmentPanel : UserControl
    {
        public EnvironmentPanel()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, EnvironmentPropsController controller)
        {
            new SimpleControlVCBinder(context, this, controller);

            new BufferedPropertyGridVCBinder(context, propertyGrid1, controller.PropertyGrid1);
        }
    }
}
