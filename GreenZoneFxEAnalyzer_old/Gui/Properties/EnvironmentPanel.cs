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
    public partial class EnvironmentPanel : UserControl
    {
        public EnvironmentPanel()
        {
            InitializeComponent();
        }

        public EnvironmentPanel(EnvironmentRuntime environment)
        {
            InitializeComponent();
            Environment = environment;
            propertyGrid1.SelectedObject = environment;
        }

        public EnvironmentRuntime Environment
        {
            get;
            private set;
        }
    }
}
