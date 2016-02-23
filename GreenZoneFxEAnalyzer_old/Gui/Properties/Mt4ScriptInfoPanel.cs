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
    public partial class Mt4ScriptInfoPanel : UserControl
    {
        public Mt4ScriptInfoPanel()
        {
            InitializeComponent();
        }

        public Mt4ScriptInfoPanel(Mt4ScriptInfo mt4ScriptInfo)
        {
            InitializeComponent();
            Mt4ScriptInfo = mt4ScriptInfo;
            propertyGrid1.SelectedObject = mt4ScriptInfo;
        }

        public Mt4ScriptInfo Mt4ScriptInfo
        {
            get;
            private set;
        }
    }
}
