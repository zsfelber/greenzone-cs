using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreenZoneFxEngine.Gui
{
    public partial class IndicatorPanel : UserControl
    {
        public IndicatorPanel()
        {
            InitializeComponent();
        }

        private void reset1Button_Click(object sender, EventArgs e)
        {
            indicatorRuntimePanel.Reset();
        }
    }
}
