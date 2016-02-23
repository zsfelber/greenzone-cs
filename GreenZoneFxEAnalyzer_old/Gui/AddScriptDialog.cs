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

namespace GreenZoneFxEngine
{
    public partial class AddScriptDialog : Form
    {
        private Form1 parent;

        public AddScriptDialog(Form1 parent, EnvironmentRuntime environment)
        {
            this.parent = parent;
            Environment = environment;

            InitializeComponent();

            scriptCombo.Items.Add(new ListItem<Mt4ScriptInfo>(null, "-- None --"));
            scriptCombo.SelectedIndex = 0;
            foreach (string s in Environment.Scripts)
            {
                Mt4ScriptInfo script = Environment.GetScriptInfo(s);
                scriptCombo.Items.Add(new ListItem<Mt4ScriptInfo>(script, script.ShortTypeName));
            }

            foreach (symbol symbol in environment.Symbols)
            {
                symbolCombo.Items.Add(symbol);
            }

            try
            {
                symbol sym = Environment.GetSymbol("EURUSD");
                symbolCombo.SelectedItem = sym;
            }
            catch (SymbolNotFoundException)
            {
                symbolCombo.SelectedItem = Environment.Symbols.ToArray()[0];
            }

            UpdatePeriods();

            Initialized = true;
        }

        private bool Initialized
        {
            get;
            set;
        }

        public EnvironmentRuntime Environment
        {
            get;
            internal set;
        }

        public Mt4ScriptInfo Script
        {
            get
            {
                ListItem<Mt4ScriptInfo> item = (ListItem<Mt4ScriptInfo>)scriptCombo.SelectedItem;
                if (item == null)
                {
                    return null;
                }
                else
                {
                    return item.Value;
                }
            }
        }

        public symbol Symbol
        {
            get
            {
                symbol symbol = (symbol)symbolCombo.SelectedItem;
                return symbol;
            }
        }

        public TimePeriodConst Period
        {
            get
            {
                ListItem<TimePeriodConst> item = (ListItem<TimePeriodConst>)periodCombo.SelectedItem;
                if (item == null)
                {
                    // TODO ???
                    return TimePeriodConst.PERIOD_CURRENT;
                }
                else
                {
                    return item.Value;
                }
            }
        }

        void UpdatePeriods()
        {
            ListItem<TimePeriodConst> h1perItm = null;
            foreach (TimePeriodConst v in EnumExtensions.GetPeriods(EnumExtensions.VISIBLE_PERIODS))
            {
                if (TimeSeriesRuntime.IsSeriesAvailable(Environment, Symbol, v))
                {
                    ListItem<TimePeriodConst> item = new ListItem<TimePeriodConst>(v, v.GetLongTxt());
                    periodCombo.Items.Add(item);
                    if (v == TimePeriodConst.PERIOD_H1)
                    {
                        h1perItm = item;
                    }
                }
            }
            periodCombo.SelectedItem = h1perItm;
        }

        private bool validate()
        {
            if (!Initialized)
            {
                return false;
            }
            bool valid = true;

            if (scriptCombo.SelectedIndex <= 0)
            {
                errorProvider1.SetError(scriptCombo, "Script must be selected");
                valid = false;
            }
            else
            {
                errorProvider1.SetError(scriptCombo, null);
            }

            if (symbolCombo.SelectedIndex < 0)
            {
                errorProvider1.SetError(symbolCombo, "Symbol must be selected");
                valid = false;
            }
            else
            {
                errorProvider1.SetError(symbolCombo, null);
            }

            if (periodCombo.SelectedIndex < 0)
            {
                errorProvider1.SetError(periodCombo, "Period must be selected");
                valid = false;
            }
            else
            {
                errorProvider1.SetError(periodCombo, null);
            }

            okButton.Enabled = valid;
            return valid;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!validate())
            {
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
            Dispose();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
            Dispose();
        }

        private void eaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void symbolCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void periodCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate();
        }

    }
}
