using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using System.Reflection;
using GreenZoneFxEngine.Util;
using Flobbster.Windows.Forms;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Etc;
using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.Gui.ViewController;

namespace GreenZoneFxEngine
{
    public partial class IndicatorRuntimeDialog : Form
    {
        public IndicatorRuntimeDialog()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, IndicatorDialogController controller)
        {
            new FormVCBinder(context, this, controller);

            indicatorPanel1.Bind(context, controller.IndicatorPanel1);
            new ButtonVCBinder(context, okButton, controller.OkButton);
            new ButtonVCBinder(context, cancelButton, controller._CancelButton);
            new ButtonVCBinder(context, removeButton, controller.RemoveButton);
            new ButtonVCBinder(context, loadButton, controller.LoadButton);
            new ButtonVCBinder(context, saveButton, controller.SaveButton);
        }
    }
}
