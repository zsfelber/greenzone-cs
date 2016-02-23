using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;

namespace GreenZoneFxEngine.Gui.Chart
{
    public partial class IndicatorChartSectionPanel : ChartSectionPanel
    {
        new NormalChart parent;
        ChartRuntime chartRuntime;
        IndicatorRuntime indicatorRuntime;
        SeriesRange sectionRange;

        IndicatorRuntime.DInstanceChanged ic;

        public IndicatorChartSectionPanel()
        {
            InitializeComponent();
            this.closeButton.Click += new System.EventHandler(this.button1_Click);
            this.propertiesButton.Click += new System.EventHandler(this.propertiesButton_Click);
        }

        internal void Init(Chart parent, IndicatorRuntime indicatorRuntime)
        {
            this.parent = (NormalChart)parent;
            this.chartRuntime = this.parent.ChartRuntime;
            this.indicatorRuntime = indicatorRuntime;
            Init(parent);

            indicatorRuntime.InstanceChanged += ic = new IndicatorRuntime.DInstanceChanged(indicatorRuntime_InstanceChanged);
        }

        void indicatorRuntime_InstanceChanged(IndicatorRuntime newInstance)
        {
            indicatorRuntime.InstanceChanged -= ic;
            newInstance.InstanceChanged += ic;
            indicatorRuntime = newInstance;
        }


        internal override ChartPane ChartPane
        {
            get
            {
                return chartPane1;
            }
        }

        internal IndicatorRuntime Indicator
        {
            get
            {
                return indicatorRuntime;
            }
        }

        internal override SeriesRange SectionRange
        {
            get
            {
                return sectionRange;
            }
        }

        internal override IndicatorWindowType WindowType
        {
            get
            {
                return IndicatorWindowType.SEPARATE_WINDOW;
            }
        }

        internal override string PriceFormat
        {
            get
            {
                SymbolContext symbolContext = chartRuntime.SymbolRuntime.Context;

                int d = indicatorRuntime.Session.IndicatorDigits;
                if (d == 0)
                {
                    d = symbolContext.Digits - Math.Max(0, indicatorRuntime.Session.DisplayScale);
                }

                d = Math.Min(d, symbolContext.Digits);
                if (d > 0)
                {
                    string f2 = "00000000000000000".Substring(0, d);
                    string f = "0." + f2;
                    return f;
                }
                else
                {
                    return "0";
                }
            }
        }

        internal override int Scale
        {
            get
            {
                return indicatorRuntime.Session.DisplayScale;
            }
        }

        public override void CalculateSeriesRangeToFit()
        {
            double min = double.MaxValue, max = double.MinValue;

            sectionRange = chartRuntime.SeriesRange;

            if (indicatorRuntime.Levels != null)
            {
                foreach (var b in indicatorRuntime.Levels)
                {
                    min = Math.Min(min, b.Value);
                    max = Math.Max(max, b.Value);
                }
            }
            for (int i = sectionRange.OffsetFrom; i <= sectionRange.OffsetTo; i++)
            {
                if (chartRuntime.sLTime.StartIndex <= i && i < chartRuntime.sLTime.Length)
                {
                    foreach (var b in indicatorRuntime.Buffers)
                    {
                        if (b.Buffer != null && b.StyleType != DrawingStyle.DRAW_NONE)
                        {
                            double v = b.SBuffer[i];
                            min = Math.Min(min, v);
                            max = Math.Max(max, v);
                        }
                    }
                }
            }
            sectionRange.PriceFrom = min;
            sectionRange.PriceTo = max;
            if (indicatorRuntime.Levels != null)
            {
                foreach (var b in indicatorRuntime.Levels)
                {
                    max = Math.Max(max, b.Value + chartPane1.ChartWindowTopGapValue);
                }
            }
            if (indicatorRuntime.Session.IndicatorMinimum != Double.MinValue)
            {
                min = indicatorRuntime.Session.IndicatorMinimum;
            }
            if (indicatorRuntime.Session.IndicatorMaximum != Double.MaxValue)
            {
                max = indicatorRuntime.Session.IndicatorMaximum;
            }
            sectionRange.PriceFrom = min;
            sectionRange.PriceTo = max;
        }

        internal override void DrawCursor()
        {
            sectionRange.ChangeOffsetFrom(chartRuntime.SeriesRange.OffsetFrom);
            base.DrawCursor();
        }

        internal override void UpdateCursor()
        {
            sectionRange.ChangeOffsetFrom(chartRuntime.SeriesRange.OffsetFrom);
            base.UpdateCursor();
        }

        internal override void UpdateCpCursor()
        {
            sectionRange.ChangeOffsetFrom(chartRuntime.SeriesRange.OffsetFrom);
            base.UpdateCpCursor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.ChartRuntime.RemoveIndicator(indicatorRuntime.Id);
            parent.RemoveIndicatorPanel(indicatorRuntime);
            parent.UpdateSeries();
            parent.ChartPanel.UpdateIndicators();
            parent.Form1.SaveSession();
        }

        private void propertiesButton_Click(object sender, EventArgs e)
        {
            parent.ChartPanel.ShowIndicatorProperties(indicatorRuntime);
        }
    }
}
