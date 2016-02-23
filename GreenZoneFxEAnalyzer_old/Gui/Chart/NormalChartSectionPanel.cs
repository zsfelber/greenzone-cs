using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreenZoneFxEngine.Gui.Chart
{
    public partial class NormalChartSectionPanel : ChartSectionPanel
    {

        public NormalChartSectionPanel()
        {
            InitializeComponent();
        }

        internal override ChartPane ChartPane
        {
            get
            {
                return chartPane1;
            }
        }

        public override void CalculateSeriesRangeToFit()
        {
        }
    }
}
