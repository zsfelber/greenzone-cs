using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    public class AddExpertController : AddExpertControllerBase
    {
        bool initialized;

        internal AddExpertController(GreenRmiManager rmiManager, MainWindowController mainWindow, ServerEnvironmentRuntime environment)
            : base(rmiManager)
        {
            MainWindow = mainWindow;
            this.environment = environment;

            EaCombo = new ComboController(rmiManager, this, false);
            SymbolCombo = new ComboController(rmiManager, this, false);
            PeriodCombo = new ComboController(rmiManager, this, false);
            OkButton = new ButtonController(rmiManager, this);
            ErrorProvider1 = new ChildControlMap<string>(rmiManager);
            
            EaCombo.Add(new ListItem<Mt4ExecutableInfo>(null, "-- None --"));
            EaCombo.SelectedIndex = 0;
            foreach (var e in Environment.Experts)
            {
                Mt4ExecutableInfo scriptinf = e.Value;
                EaCombo.Add(new ListItem<Mt4ExecutableInfo>(scriptinf, scriptinf.ShortTypeName));
            }

            foreach (symbol symbol in environment.Symbols)
            {
                SymbolCombo.Add(symbol);
            }

            try
            {
                symbol sym = Environment.GetSymbol("EURUSD");
                SymbolCombo.SelectedItem = sym;
            }
            catch (SymbolNotFoundException)
            {
                SymbolCombo.SelectedItem = Environment.Symbols.ToArray()[0];
            }

            UpdatePeriods();

            initialized = true;
        }

        public new MainWindowController MainWindow
        {
            get
            {
                return (MainWindowController)base.MainWindow;
            }
            protected set
            {
                base.MainWindow = value;
            }
        }

        readonly ServerEnvironmentRuntime environment;
        public ServerEnvironmentRuntime Environment
        {
            get
            {
                return environment;
            }
        }

        public Mt4ExecutableInfo Expert
        {
            get
            {
                ListItem<Mt4ExecutableInfo> item = (ListItem<Mt4ExecutableInfo>)EaCombo.SelectedItem;
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

        public override symbol Symbol
        {
            get
            {
                symbol symbol = (symbol)SymbolCombo.SelectedItem;
                return symbol;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override TimePeriodConst Period
        {
            get
            {
                ListItem<TimePeriodConst> item = (ListItem<TimePeriodConst>)PeriodCombo.SelectedItem;
                if (item == null)
                {
                    // TODO ??
                    return TimePeriodConst.PERIOD_CURRENT;
                }
                else
                {
                    return item.Value;
                }
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        void UpdatePeriods()
        {
            ListItem<TimePeriodConst> h1perItm = null;
            foreach (TimePeriodConst v in EnumExtensions.GetPeriods(EnumExtensions.VISIBLE_PERIODS))
            {
                if (ServerTimeSeriesRuntimeEx.IsSeriesAvailable(Environment, Symbol, v))
                {
                    ListItem<TimePeriodConst> item = new ListItem<TimePeriodConst>(v, v.GetLongTxt());
                    PeriodCombo.Add(item);
                    if (v == TimePeriodConst.PERIOD_H1)
                    {
                        h1perItm = item;
                    }
                }
            }
            PeriodCombo.SelectedItem = h1perItm;
        }

        private bool validate()
        {
            if (!initialized)
            {
                return false;
            }
            bool valid = true;

            if (EaCombo.SelectedIndex <= 0)
            {
                ErrorProvider1[EaCombo] = "EA must be selected";
                valid = false;
            }
            else
            {
                ErrorProvider1[EaCombo] = null;
            }

            if (SymbolCombo.SelectedIndex < 0)
            {
                ErrorProvider1[SymbolCombo] = "Symbol must be selected";
                valid = false;
            }
            else
            {
                ErrorProvider1[SymbolCombo] = null;
            }

            if (PeriodCombo.SelectedIndex < 0)
            {
                ErrorProvider1[PeriodCombo] = "Period must be selected";
                valid = false;
            }
            else
            {
                ErrorProvider1[PeriodCombo] = null;
            }

            OkButton.Enabled = valid;
            return valid;
        }

        private void okButton_Click(object sender, ControllerEventArgs e)
        {
            if (!validate())
            {
                return;
            }
            DialogResult = DialogResult.OK;
            Closing();
        }

        private void cancelButton_Click(object sender, ControllerEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Closing();
        }

        private void eaCombo_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            validate();
        }

        private void symbolCombo_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            validate();
        }

        private void periodCombo_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            validate();
        }
    }
}
