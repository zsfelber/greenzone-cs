using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Util;
using GreenZoneFxEngine;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.Gui.ViewController;

namespace GreenZoneFxEngine
{
    public partial class ScriptRunnerPanel : UserControl
    {
        public ScriptRunnerPanel()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, ScriptRunnerController controller)
        {
            new SimpleControlVCBinder(context, this, controller);
        }
    }
}
