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
    public partial class OrderChart : Chart
    {
        OrdersOverviewPanel ordersOverviewPanel;
        OrdersHistoryView ordersView;

        public OrderChart()
        {
            InitializeComponent();
            this.tableLayoutPanel1.Add(this.masterChartSectionPanel);
        }

        // !!!

        internal void Init(OrdersOverviewPanel ordersOverviewPanel)
        {
            this.ordersOverviewPanel = ordersOverviewPanel;
            this.form = ordersOverviewPanel.Form1;
            ordersView = ordersOverviewPanel.Orders;
            masterChartSectionPanel.ordersView = ordersView;
            base.Init(ordersOverviewPanel, masterChartSectionPanel, ordersView);
            masterChartSectionPanel.Init(this, ordersView);
        }

        internal OrdersOverviewPanel OrdersOverviewPanel
        {
            get
            {
                return ordersOverviewPanel;
            }
        }

        internal OrdersHistoryView OrdersView
        {
            get
            {
                return ordersView;
            }
        }

        internal void AddFilterPanel(OrdersHistoryView ordersHistoryView)
        {
            OrderChartSectionPanel p = new OrderChartSectionPanel();
            p.Init(this, ordersHistoryView);
            chartSectionPanels.Add(p);

            tableLayoutPanel1.Add(p);
        }

        internal void RemoveFilterPanel(OrdersHistoryView ordersHistoryView)
        {
            foreach (OrderChartSectionPanel p in chartSectionPanels)
            {
                if (p.OrdersView == ordersHistoryView)
                {
                    chartSectionPanels.Remove(p);
                    this.tableLayoutPanel1.Remove(p);
                    break;
                }
            }
        }

        internal void RemoveFilterPanel(OrderChartSectionPanel p)
        {
            chartSectionPanels.Remove(p);
            this.tableLayoutPanel1.Remove(p);
        }

        internal void UpdateFilterPanels()
        {
            if (Owner != null)
            {
                //foreach (OrderChartSectionPanel p in chartSectionPanels)
                //{
                //    RemoveFilterPanel(p);
                //}
                if (chartSectionPanels.Count > 0)
                {
                    this.tableLayoutPanel1.RemoveFrom(chartSectionPanels[0]);
                    chartSectionPanels.Clear();
                }
                if (!string.IsNullOrEmpty(ordersView.Filter.GroupBy))
                {
                    foreach (object g in ordersView.Groups)
                    {
                        OrderFilter filter = ordersView.Filter.Clone();
                        filter.GroupField = g;
                        OrdersHistoryView view = new OrdersHistoryView(ordersView.Parent, filter);
                        AddFilterPanel(view);
                    }
                }
            }
        }

        public override void CalculateSeriesRangeToFit(bool includeMainChart)
        {
            masterChartSectionPanel.CalculateSeriesRangeToFit();
            foreach (var p in chartSectionPanels)
            {
                p.CalculateSeriesRangeToFit();
            }
        }

        public override void ScrollYToPrice()
        {

            double min = double.MaxValue, max = double.MinValue;

            SeriesRange seriesRange = ordersView.SeriesRange;

            double range = seriesRange.PriceRange;

            for (int i = seriesRange.OffsetFrom; i <= seriesRange.OffsetTo; i++)
            {
                if (ordersView.sLTime.StartIndex <= i && i < ordersView.sLTime.Length)
                {
                    double balance = ordersView.BalanceHistAsDArr[i];

                    min = Math.Min(min, balance);
                    max = Math.Max(max, balance);
                }
            }
            seriesRange.PriceFrom = (min + max) / 2 - range / 2;
            seriesRange.PriceTo = (min + max) / 2 + range / 2;

            ordersView.SeriesRange = seriesRange;
        }

    }
}
