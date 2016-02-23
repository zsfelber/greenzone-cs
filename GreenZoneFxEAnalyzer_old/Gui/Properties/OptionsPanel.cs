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
    public partial class OptionsPanel : UserControl
    {
        public OptionsPanel()
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = EAnalyzerOptions.Singleton;
        }

        public EnvironmentRuntime Environment
        {
            get;
            private set;
        }
    }
}
