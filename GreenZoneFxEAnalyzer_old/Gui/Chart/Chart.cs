using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.Util;

namespace GreenZoneFxEngine.Gui.Chart
{
    public partial class Chart : UserControl
    {
        protected Form1 form;
        EnvironmentRuntime environment;

        ChartSectionPanel masterChartSectionPanel;
        internal readonly List<ChartSectionPanel> chartSectionPanels;

        internal readonly List<TimeLabelX> timeLabelXsUpper = new List<TimeLabelX>();
        internal readonly List<TimeLabelX> timeLabelXsLower = new List<TimeLabelX>();

        IForm1TabPanel tabPanel;
        internal IChartOwner owner;

        public Chart()
        {
            chartSectionPanels = new List<ChartSectionPanel>();
            InitializeComponent();
        }

        // !!!

        internal void Init(IForm1TabPanel tabPanel, ChartSectionPanel masterChartSectionPanel, IChartOwner owner)
        {
            this.owner = owner;
            this.masterChartSectionPanel = masterChartSectionPanel;
            this.masterChartSectionPanel.parent = this;
            this.tabPanel = tabPanel;
            this.environment = tabPanel.Environment;

            this.timeLabelPane1.Init(this);

            UpdateChartAndCursor();
        }

        internal Form1 Form1
        {
            get
            {
                return form;
            }
        }

        internal IChartOwner Owner
        {
            get
            {
                return owner;
            }
        }

        internal EnvironmentRuntime Environment
        {
            get
            {
                return environment;
            }
        }

        internal ChartSectionPanel MasterChartSectionPanel
        {
            get
            {
                return masterChartSectionPanel;
            }
        }

        internal void UpdateChildrenCursor()
        {
            masterChartSectionPanel.UpdateCursor();
            foreach (var p in chartSectionPanels)
            {
                p.UpdateCursor();
            }
        }

        internal void DrawCursor()
        {
            masterChartSectionPanel.DrawCursor();
            foreach (var p in chartSectionPanels)
            {
                p.DrawCursor();
            }
        }

        internal void UpdateCpCursor()
        {
            masterChartSectionPanel.UpdateCursor();
            foreach (var p in chartSectionPanels)
            {
                p.UpdateCpCursor();
            }
        }

        internal void UpdateChartAndCursor()
        {
            if (!UpdateCursor())
            {
                UpdateSeries();
            }
        }

        internal bool UpdateCursor()
        {
            bool result = false;
            if (owner.IsCursorBarConnected)
            {
                SeriesRange r = owner.SeriesRange;
                int offsetFrom = r.OffsetFrom;
                r.CursorPosition = owner.ParentCursorPosition;
                if (r.OffsetFrom != offsetFrom)
                {
                    owner.SeriesRange = r;
                    r = owner.SeriesRange;

                    result = true;
                }
            }
            UpdateChildrenCursor();

            if (result)
            {
                UpdateSeries();
            }

            return result;
        }

        //public void UpdateSeries(bool updateChildren = true)
        public void UpdateSeries(bool enableAutoFit = true)
        {
            if (owner != null)
            {

                if (enableAutoFit && owner.AutoSeriesRange)
                {
                    CalculateSeriesRangeToFit(true);
                }
                else
                {
                    CalculateSeriesRangeToFit(false);
                }

                //UpdateChartOnScreen(updateChildren);
                UpdateChartOnScreen();
            }
        }

        //void UpdateChartOnScreen(bool updateChildren = true)
        internal virtual void UpdateChartOnScreen()
        {
            masterChartSectionPanel.CalcSeriesBars();

            CalculateTimeLabelXs();
            timeLabelPane1.Invalidate();
            timeLabelPane1.Update();

            masterChartSectionPanel.UpdateChartOnScreen();

            //if (updateChildren)
            //{
            foreach (var p in chartSectionPanels)
            {
                p.UpdateChartOnScreen();
            }
            //}
        }

        internal virtual void SetAllFocusTime()
        {
            UpdateChartAndCursor();
        }

        public virtual void CalculateSeriesRangeToFit(bool includeMainChart)
        {
        }

        public virtual void ScrollYToPrice()
        {
        }


        internal void CalculateTimeLabelXs()
        {

            SeriesRange seriesRange = owner.SeriesRange;

            List<TimeLabelX> timeLabelXs1 = new List<TimeLabelX>();
            int maximp = 0;
            int i = seriesRange.OffsetTo;
            foreach (SeriesBar bar in masterChartSectionPanel.ChartPane.SeriesBars)
            {
                if (bar != null && i < owner.sLTime.Length-1)
                {
                    datetime pd = (datetime)owner.sLTime[i + 1];

                    datetime d = bar.time;
                    string fd;
                    int imp;

                    if (pd.Month != d.Month)
                    {
                        imp = 6;
                        fd = d.ToString("MMM dd yyyy");
                    }
                    else if (pd.WeekOfYear != d.WeekOfYear)
                    {
                        imp = 5;
                        fd = d.ToString("MM.dd''ddd");
                    }
                    else if (pd.DayOfYear != d.DayOfYear)
                    {
                        imp = 4;
                        fd = d.ToString("dd MMM");
                    }
                    else if (d.Minute == 0)
                    {
                        if (d.Hour % 6 == 0)
                        {
                            imp = 3;
                            fd = d.ToString("hh:mm");
                        }
                        else
                        {
                            imp = 2;
                            fd = d.ToString("hh:mm");
                        }
                    }
                    else if (d.Minute % 15 == 0)
                    {
                        imp = 1;
                        fd = d.ToString("hh:mm");
                    }
                    else
                    {
                        imp = 0;
                        fd = d.ToString("hh:mm");
                    }

                    TimeLabelX l = new TimeLabelX(d, fd, bar.x1, imp);
                    timeLabelXs1.Add(l);
                    maximp = Math.Max(maximp, imp);

                }
                i--;
            }

            List<TimeLabelX> timeLabelXs1_n = new List<TimeLabelX>();
            List<TimeLabelX> timeLabelXs2 = new List<TimeLabelX>();
            foreach (TimeLabelX x in timeLabelXs1)
            {
                if (x.importance == maximp)
                {
                    timeLabelXs2.Add(x);
                }
                else
                {
                    timeLabelXs1_n.Add(x);
                }
            }
            if (timeLabelXs2.Count < timeLabelXs1_n.Count)
            {
                timeLabelXs1 = timeLabelXs1_n;

                for (int imp = 0; imp < 7; imp++)
                {
                    clear_lxs(timeLabelXs1, imp);
                    clear_lxs(timeLabelXs2, imp);
                }
                timeLabelXsUpper.Clear();
                timeLabelXsLower.Clear();
                timeLabelXsUpper.AddRange(timeLabelXs1);
                timeLabelXsLower.AddRange(timeLabelXs2);
            }
            else
            {
                for (int imp = 0; imp < 7; imp++)
                {
                    clear_lxs(timeLabelXs1, imp);
                }
                timeLabelXsUpper.Clear();
                timeLabelXsLower.Clear();
                timeLabelXsUpper.AddRange(timeLabelXs1);
            }

        }

        bool grid_ok(int dr, int scrw, int seconds)
        {
            double numGridLines = (double)dr / seconds;
            bool result = scrw * numGridLines / dr > 10;

            return result;
        }

        void clear_lxs(List<TimeLabelX> timeLabelXs1, int importance)
        {
            bool thereWas;
            do
            {
                bool leftSide = true;
                while (true)
                {
                    int txtWidth = 0;
                    thereWas = false;
                    for (int i = 1; i < timeLabelXs1.Count; i++)
                    {
                        TimeLabelX p = timeLabelXs1[i - 1];
                        TimeLabelX l = timeLabelXs1[i];

                        txtWidth = TextRenderer.MeasureText(p.formattedTime, masterChartSectionPanel.Font).Width;
                        if ((leftSide ? p : l).importance == importance && l.screenX - p.screenX < txtWidth)
                        {
                            timeLabelXs1.RemoveAt(leftSide ? i - 1 : i);
                            thereWas = true;
                        }
                    }
                    if (leftSide)
                    {
                        leftSide = false;
                    }
                    else
                    {
                        break;
                    }
                }
            } while (thereWas);
        }


        void zoomH(double factor, int min, int max)
        {
            datetime scrtm = owner.ScrolledBarTime;
            SeriesRange r = owner.SeriesRange;
            r.Zoom(factor, min, max);
            owner.SeriesRange = r;

            if (owner.IsCursorBarConnected)
            {
                owner.ScrolledBarTime = scrtm;
                UpdateCursor();
            }

            form.SaveSession();
            UpdateSeries();
        }

        internal void SliderValueDragged(ChartSectionPanel invoker)
        {
            if (owner.IsCursorBarConnected)
            {
                SeriesRange r = owner.SeriesRange;

                owner.ParentCursorPosition = invoker.ChartPane.SliderValue;

                UpdateChildrenCursor();
                ParentDrawAllCursor();
                SimpleDelegate d = continueScroll;
                BeginInvoke(d);
            }
        }

        void continueScroll()
        {
            SeriesRange r = owner.SeriesRange;

            int offset = r.OffsetFrom;
            r.CursorPosition = masterChartSectionPanel.ChartPane.SliderValue;
            int ind = offset - r.OffsetFrom;
            if (owner.sLTime.StartIndex <= ind && ind < owner.sLTime.Length)
            {
                owner.ParentScrolledBarTime = (datetime)owner.sLTime[ind];
                owner.SeriesRange = r;
                UpdateSeries();
                ParentUpdateAllCursor();
            }
        }

        internal virtual void ParentDrawAllCursor()
        {
        }

        internal virtual void ParentUpdateAllCursor()
        {
        }

        private void zoomToFitButton_Click(object sender, EventArgs e)
        {
            CalculateSeriesRangeToFit(true);
            UpdateChartOnScreen();
        }

        private void zoomToScrollPriceButton_Click(object sender, EventArgs e)
        {
            ScrollYToPrice();
            UpdateChartOnScreen();
        }

        private void zoomOutHButton_Click(object sender, EventArgs e)
        {
            zoomH(1.1, 50, 400);
        }

        private void zoomInHButton_Click(object sender, EventArgs e)
        {
            zoomH(0.9, 50, 400);
        }

        private void zoomOutVButton_Click(object sender, EventArgs e)
        {
            SeriesRange r = owner.SeriesRange;
            double d = (r.PriceTo - r.PriceFrom) / 20;
            r.PriceFrom -= d;
            r.PriceTo += d;
            owner.SeriesRange = r;
            form.SaveSession();
            UpdateSeries(false);
        }

        private void zoomInVButton_Click(object sender, EventArgs e)
        {
            SeriesRange r = owner.SeriesRange;
            double d = (r.PriceTo - r.PriceFrom) / 20;
            r.PriceFrom += d;
            r.PriceTo -= d;
            owner.SeriesRange = r;
            form.SaveSession();
            UpdateSeries(false);
        }

        private void Chart_SizeChanged(object sender, EventArgs e)
        {
            UpdateSeries();
        }

    }
}
