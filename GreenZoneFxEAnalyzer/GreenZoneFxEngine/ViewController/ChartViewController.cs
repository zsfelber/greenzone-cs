using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;

using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneFxEngine.Types;
using System.Windows.Forms;
using GreenZoneFxEngine.Etc;
using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.Util;
using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    public class ChartViewController : ChartViewControllerBase
    {
        PropertyChangedEventHandler dFrom, dTo;

        bool initialized;
        public ChartViewController(GreenRmiManager rmiManager, ChartGroupController parent, ServerEnvironmentRuntime environment, ServerChartRuntime chartRuntime)
            : base(rmiManager, (Controller)null)
        {
            FromTimePicker = new FieldController<DateTime>(rmiManager, this);
            ToTimePicker = new FieldController<DateTime>(rmiManager, this);
            CloseChartButton1 = new ButtonController(rmiManager, this);
            CloseChartButton2 = new ButtonController(rmiManager, this);
            ConnectCursorButton = new ToggleButtonController(rmiManager, this);
            AutoSeriesRangeButton = new ToggleButtonController(rmiManager, this);
            ToggleTopBarButton1 = new ButtonController(rmiManager, this);
            ToggleTopBarButton2 = new ButtonController(rmiManager, this);
            AddChartButton = new ComboController(rmiManager, this, true);
            SymbolDdButton = new ComboController(rmiManager, this, false);
            PeriodDdButton = new ComboController(rmiManager, this, false);
            ChartTypeDdButton = new ComboController(rmiManager, this, false);
            IndicatorsDdButton = new ComboController(rmiManager, this, false);
            TopToolStrip = new Controller(rmiManager, this);
            SymPerMiniLabel = new ButtonController(rmiManager, this);

            dFrom = delegate(object sender2, PropertyChangedEventArgs e2)
                    {
                        ChartRuntime.Session.From = FromTimePicker.Value;
                        MainWindow.UpdateListInLauncherPanel();
                        MainWindow.SaveSession();
                    };
            dTo = delegate(object sender2, PropertyChangedEventArgs e2)
                    {
                        ChartRuntime.Session.To = ToTimePicker.Value;
                        MainWindow.UpdateListInLauncherPanel();
                        MainWindow.SaveSession();
                    };

            FromTimePicker.ValueChanged += dFrom;
            ToTimePicker.ValueChanged += dTo;

            Parent = parent;
            MainWindow = parent.MainWindow;
            this.environment = environment;
            this.chartRuntime = chartRuntime;

            UpdateSymbols();

            UpdateIndicators();

            FromTimePicker.Value = (DateTime)ChartRuntime.Session.From;
            ToTimePicker.Value = (DateTime)ChartRuntime.Session.To;
            SetTopBarVisible(ChartRuntime.Session.TopBarVisible);

            if (ChartRuntime.IsMaster)
            {
                CloseChartButton1.Image = AppResources.Close_16xLG;
                CloseChartButton2.Image = AppResources.Close_16xLG;
                AddChartButton.Enabled = true;
                AddChartButton.Image = AppResources.action_add_16xMD;
            }
            else
            {
                CloseChartButton1.Image = AppResources.action_Cancel_16xSM;
                CloseChartButton2.Image = AppResources.action_Cancel_16xSM;
                AddChartButton.Enabled = false;
                AddChartButton.Image = AppResources.None;
            }

            UpdateAutoSeries();

            UpdateConnectButton();

            Chart1 = new NormalChartController(rmiManager, this, chartRuntime);
            CursorChart = new CursorChartController(rmiManager, this, chartRuntime.CursorRuntime);

            if (ChartRuntime.Session.IsCursorBarConnected)
            {
                Chart1.SliderBarColor = Color.FromArgb(100, Color.OrangeRed);
                CursorChart.SliderBarColor = Color.FromArgb(100, Color.OrangeRed);
            }
            else
            {
                Chart1.SliderBarColor = Color.FromArgb(100, Color.Blue);
                CursorChart.SliderBarColor = Color.FromArgb(100, Color.Blue);
            }

            MainWindow.SaveSession();
            MainWindow.UpdateListInLauncherPanel();

            ToggleTopBarButton1.Pressed += new ControllerEventHandler(toggleTopBarButton1_Click);
            ToggleTopBarButton2.Pressed += new ControllerEventHandler(toggleTopBarButton2_Click);
            SymPerMiniLabel.Pressed += new ControllerEventHandler(symPerMiniLabel_Click);
            CloseChartButton1.Pressed += new ControllerEventHandler(closeChartButton1_Click);
            CloseChartButton2.Pressed += new ControllerEventHandler(closeChartButton2_Click);
            AddChartButton.Pressed += new ControllerEventHandler(addChartButton_ButtonClick);
            ConnectCursorButton.Pressed += new ControllerEventHandler(connectCursorButton_Click);
            AutoSeriesRangeButton.Pressed += new ControllerEventHandler(autoSeriesRangeButton_Click);

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

        public new ChartGroupController Parent
        {
            get
            {
                return (ChartGroupController)base.Parent;
            }
            protected set
            {
                base.Parent = value;
            }
        }

        readonly ServerChartRuntime chartRuntime;
        public ServerChartRuntime ChartRuntime
        {
            get
            {
                return chartRuntime;
            }
        }

        public new NormalChartController Chart1
        {
            get
            {
                return (NormalChartController)base.Chart1;
            }
            protected set
            {
                base.Chart1 = value;
            }
        }

        public new ChartGroupController ChartGroupController
        {
            get
            {
                return (ChartGroupController)base.ChartGroupController;
            }
            protected set
            {
                base.ChartGroupController = value;
            }
        }

        public new CursorChartController CursorChart
        {
            get
            {
                return (CursorChartController)base.CursorChart;
            }
            protected set
            {
                base.CursorChart = value;
            }
        }

        internal void UpdateAll()
        {
            UpdateSymbols();
        }

        void UpdateSymbols()
        {
            SymbolDdButton.Clear();
            AddChartButton.Clear();
            foreach (symbol symbol in Environment.Symbols)
            {
                ToggleButtonController mi = new ToggleButtonController(rmiManager, null, symbol.ToString());
                ComboController mi_ac = new ComboController(rmiManager, null, symbol.ToString());

                Dictionary<string, TimePeriodConst> DTimePeriodConst = new Dictionary<string, TimePeriodConst>();

                foreach (TimePeriodConst v in EnumExtensions.GetPeriods(EnumExtensions.VISIBLE_PERIODS))
                {
                    if (ServerTimeSeriesRuntimeEx.IsSeriesAvailable(Environment, symbol, v))
                    {
                        DTimePeriodConst[v.GetLongTxt()] = v;

                        //ButtonController mi_ac_2 = new ButtonController(v.GetLongTxt(), null, null, symbol.strSymbol);
                        ButtonController mi_ac_2 = new ButtonController(rmiManager, mi_ac, v.GetLongTxt());

                        mi_ac_2.Pressed += new ControllerEventHandler(mi_ac_2_Click);
                    }
                }
                if (DTimePeriodConst.Count > 0)
                {
                    SymbolDdButton.Add(mi);
                    mi.Pressed += new ControllerEventHandler(symbolDd_mi_Click);

                    if (symbol == ChartRuntime.Symbol)
                    {
                        mi.Press();
                    }

                    AddChartButton.Add(mi_ac);
                    mi_ac.Tag = DTimePeriodConst;
                    mi_ac.Pressed += new ControllerEventHandler(mi_ac_Click);
                }
            }
        }

        void symbolDd_mi_Click(object sender, ControllerEventArgs e)
        {
            foreach (ToggleButtonController m0 in SymbolDdButton.Items)
            {
                m0.Checked = false;
            }
            ToggleButtonController misnd = (ToggleButtonController)sender;
            misnd.Checked = true;
            SymbolDdButton.Text = misnd.Text;

            symbol newSymbol = Environment.GetSymbol(misnd.Text);
            TimePeriodConst newPeriod = ChartRuntime.Period;
            if (!ServerTimeSeriesRuntimeEx.IsSeriesAvailable(Environment, newSymbol, newPeriod))
            {
                foreach (TimePeriodConst v in EnumExtensions.GetPeriods(EnumExtensions.VISIBLE_PERIODS))
                {
                    if (ServerTimeSeriesRuntimeEx.IsSeriesAvailable(Environment, newSymbol, v))
                    {
                        newPeriod = v;
                        break;
                    }
                }
            }

            try
            {
                ChartRuntime.SymbolRuntime.Session.DataPeriod = TimePeriodConst.PERIOD_CURRENT;
                ChartRuntime.Update(newSymbol, newPeriod);
                PeriodDdButton.Text = newPeriod.GetShortTxt();

                if (ChartRuntime.IsMaster)
                {
                    Parent.UpdateTabText();
                }
                UpdateTabText();

                if (initialized)
                {
                    MainWindow.SaveSession();
                    MainWindow.UpdateListInLauncherPanel();
                }

                UpdatePeriods();
                UpdateChartTypes();
                UpdateIndicators();
                if (initialized)
                {
                    Chart1.UpdateIndicatorPanels();
                    Chart1.UpdateSeries();
                }
            }
            catch (TimeSeriesException ex)
            {
                MessageBoxController.Show(rmiManager, ex.Message + "\n\nUnable to load chart for symbol:" + newSymbol + " period:" + newPeriod, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void mi_ac_2_Click(object sender, ControllerEventArgs e)
        {
            ButtonController misnd = (ButtonController)sender;
            symbol sym = Environment.GetSymbol(misnd.OwnerItem.Text);
            Dictionary<string, TimePeriodConst> DTimePeriodConst = (Dictionary<string, TimePeriodConst>)misnd.OwnerItem.Tag;

            TimePeriodConst v2 = DTimePeriodConst[misnd.Text];
            ChartGroupController.AddChart(null, sym, v2);
        }
        void mi_ac_Click(object sender, ControllerEventArgs e)
        {
            ButtonController misnd = (ButtonController)sender;
            symbol sym = Environment.GetSymbol(misnd.Text);
            ChartGroupController.AddChart(null, sym);
        }

        internal void UpdatePeriods()
        {
            PeriodDdButton.Clear();
            Dictionary<string, TimePeriodConst> DTimePeriodConst = new Dictionary<string, TimePeriodConst>();
            PeriodDdButton.Tag = DTimePeriodConst;

            bool addsep;
            addsep = false;
            foreach (TimePeriodConst v in EnumExtensions.GetPeriods(TimePeriodCategory.TICKS))
            {
                add_period(DTimePeriodConst, v);
                addsep = true;
            }

            if (addsep)
            {
                PeriodDdButton.Add(new SeparatorButtonController(rmiManager, (Controller)null));
            }

            addsep = false;
            foreach (TimePeriodConst v in EnumExtensions.GetPeriods(TimePeriodCategory.SECS))
            {
                add_period(DTimePeriodConst, v);
                addsep = true;
            }

            if (addsep)
            {
                PeriodDdButton.Add(new SeparatorButtonController(rmiManager, (Controller)null));
            }

            foreach (TimePeriodConst v in EnumExtensions.GetPeriods(TimePeriodCategory.MT4 | TimePeriodCategory.MT5))
            {
                add_period(DTimePeriodConst, v);
            }
        }
        private void add_period(Dictionary<string, TimePeriodConst> DTimePeriodConst, TimePeriodConst period)
        {
            if (ServerTimeSeriesRuntimeEx.IsSeriesAvailable(Environment, ChartRuntime.Symbol, period))
            {

                DTimePeriodConst[period.GetLongTxt()] = period;
                ToggleButtonController mi = new ToggleButtonController(rmiManager, PeriodDdButton, period.GetLongTxt());
                mi.Pressed += new ControllerEventHandler(periodDd_mi_Click);
                if (period == ChartRuntime.Period)
                {
                    mi.Press();
                }
            }
        }

        void periodDd_mi_Click(object sender, ControllerEventArgs e)
        {
            foreach (ButtonController m0 in PeriodDdButton.Items)
            {
                if (m0 is ToggleButtonController)
                {
                    ToggleButtonController m = (ToggleButtonController)m0;
                    m.Checked = false;
                }
            }
            ToggleButtonController misnd = (ToggleButtonController)sender;
            misnd.Checked = true;
            Dictionary<string, TimePeriodConst> DTimePeriodConst = (Dictionary<string, TimePeriodConst>)
                            PeriodDdButton.Tag;

            TimePeriodConst v2 = DTimePeriodConst[misnd.Text];
            PeriodDdButton.Image = misnd.Image;
            PeriodDdButton.Text = v2.GetShortTxt();
            ChartRuntime.SymbolRuntime.Session.DataPeriod = TimePeriodConst.PERIOD_CURRENT;
            ChartRuntime.Period = v2;
            if (ChartRuntime.IsMaster)
            {
                Parent.UpdateTabText();
            }
            UpdateTabText();

            UpdateChartTypes();
            if (initialized)
            {
                Chart1.UpdateIndicatorPanels();
                Chart1.UpdateSeries();
            }
            UpdateIndicators();

            if (initialized)
            {
                MainWindow.SaveSession();
                MainWindow.UpdateListInLauncherPanel();
            }
        }

        private void UpdateChartTypes()
        {
            ChartTypeDdButton.Clear();
            Dictionary<string, ChartType> DChartType = new Dictionary<string, ChartType>();
            ChartTypeDdButton.Tag = DChartType;

            ChartType oldct = ChartRuntime.Session.ChartType;
            if (ChartRuntime.Period.GetCategory() != TimePeriodCategory.TICKS)
            {
                ChartRuntime.Session.ChartType = ChartRuntime.Session.DefaultChartType;
            }

            ButtonController mi0 = null;

            foreach (ChartType v in Enum.GetValues(typeof(ChartType)))
            {
                if (EngineUtils.IsDrawerAvailable(v, ChartRuntime.Period))
                {
                    DChartType[v.GetLongTxt()] = v;
                    ToggleButtonController mi = new ToggleButtonController(rmiManager, ChartTypeDdButton, v.GetLongTxt());
                    mi.Pressed += new ControllerEventHandler(chartTypeDd_mi_Click);
                    if (v == ChartRuntime.Session.ChartType)
                    {
                        mi.Press();
                    }
                    if (mi0 == null)
                    {
                        mi0 = mi;
                    }
                }
            }

            if (!DChartType.ContainsValue(oldct))
            {
                mi0.Press();
            }
        }

        void chartTypeDd_mi_Click(object sender, ControllerEventArgs e)
        {
            foreach (ToggleButtonController m0 in ChartTypeDdButton.Items)
            {
                m0.Checked = false;
            }
            ToggleButtonController misnd = (ToggleButtonController)sender;
            misnd.Checked = true;
            Dictionary<string, ChartType> DChartType = (Dictionary<string, ChartType>)
                             ChartTypeDdButton.Tag;

            ChartType v2 = DChartType[misnd.Text];
            ChartTypeDdButton.Image = misnd.Image;
            ChartTypeDdButton.Text = v2.GetShortTxt();
            ChartRuntime.Session.ChartType = v2;

            if (initialized)
            {
                Chart1.UpdateSeries();
                MainWindow.SaveSession();
            }
        }

        internal void UpdateIndicators()
        {
            IndicatorsDdButton.Clear();

            List<ButtonController> l = new List<ButtonController>();
            foreach (var ind in ChartRuntime.GuiSeriesManager.DefaultCache)
            {
                if (ind.Visible)
                {
                    Mt4ExecutableInfo info = ind.IndicatorInfo;
                    if (info.SystemType != null)
                    {
                        ButtonController mi = new ButtonController(rmiManager, null, info.Name + " " + ind.Session.ShortName);
                        mi.Name = info.SystemTypeId + "," + GreenZoneUtils.GenerateArgKey(ind.GenerateParamArray());
                        l.Add(mi);

                        mi.Pressed += new ControllerEventHandler(indicatorsDd_1_mi_Click);

                    }
                }
            }
            l.Sort(compareButtonControllers);
            foreach (var i in l)
            {
                IndicatorsDdButton.Add(i);
            }


            IndicatorsDdButton.Add(new SeparatorButtonController(rmiManager, (Controller)null));
            l.Clear();

            SortedDictionary<string, List<ButtonController>> cat = new SortedDictionary<string, List<ButtonController>>();
            SortedDictionary<string, ButtonController> all = new SortedDictionary<string, ButtonController>();
            foreach (string s in Environment.Indicators)
            {
                Mt4ExecutableInfo info = Environment.GetIndicatorInfo(s);

                if (info.SystemType != null)
                {
                    ButtonController mi = new ButtonController(rmiManager, null, info.FullName);
                    mi.Name = info.SystemTypeId;

                    List<string> cs = new List<string>();
                    if (info.Categories == null)
                    {
                        cs.Add("zzz");
                    }
                    else
                    {
                        cs.AddRange(info.Categories);
                    }
                    foreach (var c in cs)
                    {
                        List<ButtonController> cl;
                        if (!cat.TryGetValue(c, out cl))
                        {
                            cl = new List<ButtonController>();
                            cat[c] = cl;
                        }
                        cl.Add(mi);
                    }
                    mi.Pressed += new ControllerEventHandler(indicatorsDd_2_mi_Click);

                    if (!all.ContainsKey(info.FullName))
                    {
                        ButtonController miall = new ButtonController(rmiManager, null, info.FullName);
                        miall.Name = info.SystemTypeId;
                        all[info.FullName] = miall;
                        miall.Pressed += new ControllerEventHandler(indicatorsDd_2_mi_Click);
                    }
                }
            }

            foreach (var e in cat)
            {
                e.Value.Sort(compareButtonControllers);

                ComboController mci;
                if ("zzz" == e.Key)
                {
                    mci = new ComboController(rmiManager, IndicatorsDdButton, "Others");
                }
                else
                {
                    mci = new ComboController(rmiManager, IndicatorsDdButton, e.Key);
                }

                foreach (var mi in e.Value)
                {
                    mci.Add(mi);
                }
            }

            ComboController mciall = new ComboController(rmiManager, IndicatorsDdButton, "All");
            foreach (var mi in all.Values)
            {
                mciall.Add(mi);
            }
        }

        internal void ShowIndicatorProperties(ServerIndicatorRuntime ind)
        {
            IndicatorId id0 = ind.Id;

            IndicatorDialogController d = new IndicatorDialogController(rmiManager, ind);
            d.ShowDialog(Parent);

            if (d.DialogResult == DialogResult.OK)
            {
                ind = ind.Copy();
                ChartRuntime.ReplaceIndicator(id0, ind);
                ind.Visible = true;

                UpdateIndicators();
                Chart1.UpdateSeries();
            }
            else if (d.DialogResult == DialogResult.Abort)
            {
                ChartRuntime.RemoveIndicator(id0);
                if (initialized)
                {
                    Chart1.RemoveIndicatorPanel(ind);
                }

                UpdateIndicators();
                Chart1.UpdateSeries();
            }

            MainWindow.SaveSession();
        }

        internal void UpdateTabText()
        {
            SymPerMiniLabel.Text = ChartRuntime.Symbol + " " + ChartRuntime.Period.GetShortTxt();
        }

        void indicatorsDd_1_mi_Click(object sender, ControllerEventArgs e)
        {
            if (initialized)
            {
                ButtonController misnd = (ButtonController)sender;
                IndicatorId id = new IndicatorId(Environment, misnd.Name);

                ServerIndicatorRuntime ind = ChartRuntime.GuiSeriesManager.DefaultCache[id];
                ShowIndicatorProperties(ind);
            }
        }

        void indicatorsDd_2_mi_Click(object sender, ControllerEventArgs e)
        {
            if (initialized)
            {
                ButtonController misnd = (ButtonController)sender;
                Mt4ExecutableInfo info = Environment.GetIndicatorInfo(misnd.Name);

                IndicatorDialogController d = new IndicatorDialogController(rmiManager, ChartRuntime, info);
                d.RemoveButton.Visible = false;
                d.ShowDialog(Parent);

                if (d.DialogResult == DialogResult.OK)
                {
                    ServerIndicatorRuntime ind = d.IndicatorRuntime;
                    ChartRuntime.AddIndicator(ind);

                    if (initialized)
                    {
                        Chart1.AddIndicatorPanel(ind);
                    }
                    ind.Visible = true;

                    UpdateIndicators();
                    Chart1.UpdateSeries();

                    MainWindow.SaveSession();
                }
            }
        }


        private void SetTopBarVisible(bool visible, bool save = false)
        {
            ChartRuntime.Session.TopBarVisible = visible;
            TopToolStrip.Visible = visible;
            SymPerMiniLabel.Visible = !visible;

            if (save)
            {
                MainWindow.SaveSession();
            }
        }

        private void UpdateConnectButton()
        {

            if (ChartRuntime.Session.IsCursorBarConnected)
            {
                ConnectCursorButton.Checked = true;
                ConnectCursorButton.Image = AppResources.CursorBar_16xLG;
            }
            else
            {
                ConnectCursorButton.Checked = false;
                ConnectCursorButton.Image = AppResources.CursorBarB_16xLG;
            }
        }

        internal void UpdateAutoSeries()
        {
            AutoSeriesRangeButton.Checked = ChartRuntime.Session.AutoSeriesRange;
        }


        int compareButtonControllers(ButtonController a, ButtonController b)
        {
            return a.Text.CompareTo(b.Text);
        }

        ///////////////////////////////////////////////////////////////////


        private void toggleTopBarButton1_Click(object sender, ControllerEventArgs e)
        {
            SetTopBarVisible(!ChartRuntime.Session.TopBarVisible, true);
        }

        private void toggleTopBarButton2_Click(object sender, ControllerEventArgs e)
        {
            SetTopBarVisible(!ChartRuntime.Session.TopBarVisible, true);
        }

        private void symPerMiniLabel_Click(object sender, ControllerEventArgs e)
        {
            SetTopBarVisible(!ChartRuntime.Session.TopBarVisible, true);
        }

        private void closeChartButton1_Click(object sender, ControllerEventArgs e)
        {
            if (ChartRuntime.IsMaster)
            {
                MainWindow.RemoveChartGroup(ChartGroupController);
            }
            else
            {
                Parent.RemoveChart(this);
            }
        }

        private void closeChartButton2_Click(object sender, ControllerEventArgs e)
        {
            if (ChartRuntime.IsMaster)
            {
                MainWindow.RemoveChartGroup(ChartGroupController);
            }
            else
            {
                Parent.RemoveChart(this);
            }
        }

        private void addChartButton_ButtonClick(object sender, ControllerEventArgs e)
        {
            ChartGroupController.AddChart();
        }

        private void connectCursorButton_Click(object sender, ControllerEventArgs e)
        {
            if (ChartRuntime.Session.IsCursorBarConnected)
            {
                ChartRuntime.Session.IsCursorBarConnected = false;
                Chart1.SliderBarColor = Color.FromArgb(100, Color.Blue);
                CursorChart.SliderBarColor = Color.FromArgb(100, Color.Blue);
            }
            else
            {
                ChartRuntime.Session.IsCursorBarConnected = true;
                Chart1.SliderBarColor = Color.FromArgb(100, Color.OrangeRed);
                CursorChart.SliderBarColor = Color.FromArgb(100, Color.OrangeRed);
            }
            MainWindow.SaveSession();
            UpdateConnectButton();
        }

        private void autoSeriesRangeButton_Click(object sender, ControllerEventArgs e)
        {
            ChartRuntime.Session.AutoSeriesRange = AutoSeriesRangeButton.Checked;
            Chart1.UpdateSeries();
            MainWindow.SaveSession();
        }
 
    }
}