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
    public partial class NormalChart : Chart
    {

        ChartGroupPanel chartGroupPanel;
        ChartPanel chartPanel;
        ChartGroupRuntime chartGroupRuntime;
        ChartRuntime chartRuntime;

        public NormalChart()
        {
            InitializeComponent();
            this.tableLayoutPanel1.Add(this.masterChartSectionPanel);
        }

        // !!!

        internal void Init(ChartPanel chartPanel, ChartRuntime chartRuntime)
        {
            this.chartPanel = chartPanel;
            this.chartGroupPanel = chartPanel.ChartGroupPanel;
            this.form = this.chartGroupPanel.Form1;
            this.chartRuntime = chartRuntime;
            this.chartGroupRuntime = chartRuntime.Group;
            base.Init(chartPanel.ChartGroupPanel, masterChartSectionPanel, chartRuntime);
            masterChartSectionPanel.Init(this);
            UpdateIndicatorPanels();
        }

        [Category("Time series")]
        public ChartRuntime ChartRuntime
        {
            get
            {
                return chartRuntime;
            }
        }


        [Category("Time series")]
        public ChartGroupRuntime ChartGroupRuntime
        {
            get
            {
                return chartGroupRuntime;
            }
        }

        [Category("Time series")]
        public ChartGroupPanel ChartGroupPanel
        {
            get
            {
                return chartGroupPanel;
            }
        }

        [Category("Time series")]
        public ChartPanel ChartPanel
        {
            get
            {
                return chartPanel;
            }
        }

        internal void AddIndicatorPanel(IndicatorRuntime indicatorRuntime)
        {
            if (indicatorRuntime.Session.WindowType == IndicatorWindowType.SEPARATE_WINDOW)
            {
                IndicatorChartSectionPanel p = new IndicatorChartSectionPanel();
                p.Init(this, indicatorRuntime);
                chartSectionPanels.Add(p);

                tableLayoutPanel1.Add(p);
            }
        }

        internal void RemoveIndicatorPanel(IndicatorRuntime indicatorRuntime)
        {
            if (indicatorRuntime.Session.WindowType == IndicatorWindowType.SEPARATE_WINDOW)
            {
                foreach (IndicatorChartSectionPanel p in chartSectionPanels)
                {
                    if (p.Indicator == indicatorRuntime)
                    {
                        chartSectionPanels.Remove(p);
                        this.tableLayoutPanel1.Remove(p);
                        break;
                    }
                }
            }
        }

        internal void RemoveIndicatorPanel(IndicatorChartSectionPanel p)
        {
            chartSectionPanels.Remove(p);
            this.tableLayoutPanel1.Remove(p);
        }

        internal void UpdateIndicatorPanels()
        {
            if (chartRuntime != null)
            {
                //foreach (IndicatorChartSectionPanel p in chartSectionPanels)
                //{
                //    RemoveIndicatorPanel(p);
                //}
                if (chartSectionPanels.Count > 0)
                {
                    this.tableLayoutPanel1.RemoveFrom(chartSectionPanels[0]);
                    chartSectionPanels.Clear();
                }
                foreach (var ind in chartRuntime.GuiSeriesManager.DefaultCache)
                {
                    if (ind.Visible)
                    {
                        AddIndicatorPanel(ind);
                    }
                }
            }
        }


        internal override void SetAllFocusTime()
        {
            UpdateChartAndCursor();

            if (chartRuntime.Session.IsCursorBarConnected)
            {
                datetime focusedTime = chartRuntime.ScrolledBarTime;
                chartGroupPanel.SetAllScrolledTime(chartPanel, focusedTime);
            }
        }

        public override void CalculateSeriesRangeToFit(bool includeMainChart)
        {
            masterChartSectionPanel.CalculateSeriesRangeToFit();
            foreach (var p in chartSectionPanels)
            {
                p.CalculateSeriesRangeToFit();
            }

            if (includeMainChart)
            {
                SeriesManagerRuntime seriesManager = chartRuntime.GuiSeriesManager;
                SeriesManagerCache seriesCache = seriesManager.DefaultCache;
                SeriesRange seriesRange = chartRuntime.SeriesRange;

                double min = double.MaxValue, max = double.MinValue;
                FindPriceMinMax(ref min, ref max);

                seriesRange.PriceFrom = min;
                seriesRange.PriceTo = max;

                chartRuntime.SeriesRange = seriesRange;
            }
        }

        public override void ScrollYToPrice()
        {
            SeriesManagerRuntime seriesManager = chartRuntime.GuiSeriesManager;
            SeriesManagerCache seriesCache = seriesManager.DefaultCache;
            SeriesRange seriesRange = chartRuntime.SeriesRange;

            double range = seriesRange.PriceRange;

            double min = double.MaxValue, max = double.MinValue;
            FindPriceMinMax(ref min, ref max);

            seriesRange.PriceFrom = (min + max) / 2 - range / 2;
            seriesRange.PriceTo = (min + max) / 2 + range / 2;

            chartRuntime.SeriesRange = seriesRange;
        }

        public void FindPriceMinMax(ref double min, ref double max)
        {
            min = double.MaxValue;
            max = double.MinValue;

            SeriesManagerRuntime seriesManager = chartRuntime.GuiSeriesManager;
            SeriesManagerCache seriesCache = seriesManager.DefaultCache;

            SeriesRange seriesRange = chartRuntime.SeriesRange;

            for (int i = seriesRange.OffsetFrom; i <= seriesRange.OffsetTo; i++)
            {
                if (seriesCache.sLTime.StartIndex <= i && i < seriesCache.sLTime.Length)
                {
                    if (seriesCache.Period.GetCategory() == TimePeriodCategory.TICKS)
                    {
                        double bid = seriesCache.sBids[i];
                        double ask = seriesCache.sAsks[i];

                        min = Math.Min(min, bid);
                        min = Math.Min(min, ask);

                        max = Math.Max(max, bid);
                        max = Math.Max(max, ask);
                    }
                    else
                    {
                        double open = seriesCache.sOpen[i];
                        double low = seriesCache.sLow[i];
                        double high = seriesCache.sHigh[i];
                        double close = seriesCache.sClose[i];

                        min = Math.Min(min, open);
                        min = Math.Min(min, low);
                        min = Math.Min(min, high);
                        min = Math.Min(min, close);

                        max = Math.Max(max, open);
                        max = Math.Max(max, low);
                        max = Math.Max(max, high);
                        max = Math.Max(max, close);
                    }
                }
            }
        }

        internal override void ParentDrawAllCursor()
        {
            chartGroupPanel.DrawAllCursor(chartPanel);
        }

        internal override void ParentUpdateAllCursor()
        {
            chartGroupPanel.UpdateAllCursor(chartPanel);
        }

    }
}
