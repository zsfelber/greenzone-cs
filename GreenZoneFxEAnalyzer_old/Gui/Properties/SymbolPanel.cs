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
    public partial class SymbolPanel : UserControl
    {
        public SymbolPanel()
        {
            InitializeComponent();
        }

        public SymbolPanel(SymbolContext symbolContext)
        {
            InitializeComponent();
            SymbolContext = symbolContext;
            propertyGrid1.SelectedObject = symbolContext;
        }

        public SymbolContext SymbolContext
        {
            get;
            private set;
        }
    }
}
