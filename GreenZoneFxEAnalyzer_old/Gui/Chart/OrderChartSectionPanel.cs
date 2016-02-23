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
    public partial class OrderChartSectionPanel : ChartSectionPanel
    {
        new OrderChart parent;
        internal OrdersHistoryView ordersView;
        SeriesRange sectionRange;

        public OrderChartSectionPanel()
        {
            InitializeComponent();
        }

        internal void Init(Chart parent, OrdersHistoryView ordersView)
        {
            this.parent = (OrderChart)parent;
            this.ordersView = ordersView;
            Init(parent);
        }

        internal OrdersHistoryView OrdersView
        {
            get
            {
                return ordersView;
            }
        }

        internal override ChartPane ChartPane
        {
            get
            {
                return chartPane1;
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
                return ordersView.SymbolFormat;
            }
        }

        internal override int Scale
        {
            get
            {
                return 0;
            }
        }

        public override void CalculateSeriesRangeToFit()
        {
            double min = double.MaxValue, max = double.MinValue;

            SeriesRange seriesRange = ordersView.SeriesRange;

            for (int i = seriesRange.OffsetFrom; i <= seriesRange.OffsetTo; i++)
            {
                if (ordersView.sLTime.StartIndex <= i && i < ordersView.sLTime.Length)
                {
                    double balance = ordersView.BalanceHistAsDArr[i];

                    min = Math.Min(min, balance);
                    max = Math.Max(max, balance);
                }
            }
            seriesRange.PriceFrom = min;
            seriesRange.PriceTo = max;

            ordersView.SeriesRange = seriesRange;
        }

        internal override void DrawCursor()
        {
            sectionRange.ChangeOffsetFrom(ordersView.SeriesRange.OffsetFrom);
            base.DrawCursor();
        }

        internal override void UpdateCursor()
        {
            sectionRange.ChangeOffsetFrom(ordersView.SeriesRange.OffsetFrom);
            base.UpdateCursor();
        }

        internal override void UpdateCpCursor()
        {
            sectionRange.ChangeOffsetFrom(ordersView.SeriesRange.OffsetFrom);
            base.UpdateCpCursor();
        }

        private void propertiesButton_Click(object sender, EventArgs e)
        {
            parent.OrdersOverviewPanel.ShowProperties();
        }
    }
}
