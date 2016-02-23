using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Util;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.Gui.ViewController;

namespace GreenZoneFxEngine
{
    public partial class AddExpertDialog : Form
    {
        public AddExpertDialog()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, AddExpertController controller)
        {
            new FormVCBinder(context, this, controller);

            new ComboBoxVCBinder(context, eaCombo, controller.EaCombo);
            new ComboBoxVCBinder(context, symbolCombo, controller.SymbolCombo);
            new ComboBoxVCBinder(context, periodCombo, controller.PeriodCombo);
            new ButtonVCBinder(context, okButton, controller.OkButton);
            new ButtonVCBinder(context, cancelButton, controller.CancelButton);
            new ErrorProviderVCBinder(context, errorProvider1, controller.ErrorProvider1);
        }
    }
}
