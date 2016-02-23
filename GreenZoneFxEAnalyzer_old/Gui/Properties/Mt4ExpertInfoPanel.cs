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
    public partial class Mt4ExpertInfoPanel : UserControl
    {
        public Mt4ExpertInfoPanel()
        {
            InitializeComponent();
        }

        public Mt4ExpertInfoPanel(Mt4ExpertInfo mt4ExpertInfo)
        {
            InitializeComponent();
            Mt4ExpertInfo = mt4ExpertInfo;
            propertyGrid1.SelectedObject = mt4ExpertInfo;
        }

        public Mt4ExpertInfo Mt4ExpertInfo
        {
            get;
            private set;
        }
    }
}
