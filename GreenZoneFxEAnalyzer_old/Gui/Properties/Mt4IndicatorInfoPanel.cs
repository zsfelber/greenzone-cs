using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;

namespace GreenZoneFxEngine
{
    public partial class Mt4IndicatorInfoPanel : UserControl
    {
        public Mt4IndicatorInfoPanel()
        {
            InitializeComponent();
        }

        public Mt4IndicatorInfoPanel(Mt4IndicatorInfo mt4IndicatorInfo)
        {
            InitializeComponent();
            Mt4IndicatorInfo = mt4IndicatorInfo;
            propertyGrid1.SelectedObject = mt4IndicatorInfo;
        }

        public Mt4IndicatorInfo Mt4IndicatorInfo
        {
            get;
            private set;
        }
    }
}
