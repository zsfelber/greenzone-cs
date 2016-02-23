using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class OrderChartController : ServerOrderChartControllerBase
    {

        public OrderChartController(GreenRmiManager rmiManager, OrdersOverviewController ordersOverviewPanel)
            : base(rmiManager, ordersOverviewPanel, ordersOverviewPanel.Orders)
        {
            this.TableLayoutPanel1.Add(MasterChartSectionPanel);
            OrdersView = ordersOverviewPanel.Orders;
        }

        protected override void CreateMaster()
        {
            MasterChartSectionPanel = new OrderChartSectionPanelController(rmiManager, this, OrdersOverviewPanel.Orders);
        }

        protected override ITimeLabelPaneController CreateTimeLabelPaneController()
        {
            return new TimeLabelPaneController(rmiManager, this);
        }


        internal void AddFilterPanel(ServerOrdersHistoryView ordersHistoryView)
        {
            OrderChartSectionPanelController p = new OrderChartSectionPanelController(rmiManager, this, ordersHistoryView);
            chartSectionPanels.Add(p);

            TableLayoutPanel1.Add(p);
        }

        internal void RemoveFilterPanel(ServerOrdersHistoryView ordersHistoryView)
        {
            foreach (OrderChartSectionPanelController p in chartSectionPanels)
            {
                if (p.OrdersView == ordersHistoryView)
                {
                    chartSectionPanels.Remove(p);
                    this.TableLayoutPanel1.Remove(p);
                    break;
                }
            }
        }

        internal void RemoveFilterPanel(OrderChartSectionPanelController p)
        {
            chartSectionPanels.Remove(p);
            this.TableLayoutPanel1.Remove(p);
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
                    this.TableLayoutPanel1.RemoveFrom((Controller)chartSectionPanels[0]);
                    chartSectionPanels.Clear();
                }
                if (!string.IsNullOrEmpty(ordersView.Filter.GroupBy))
                {
                    foreach (object g in ordersView.Groups)
                    {
                        OrderFilter filter = ordersView.Filter.Clone();
                        filter.GroupField = g;
                        ServerOrdersHistoryView view = new ServerOrdersHistoryView(ordersView.Parent, filter);
                        AddFilterPanel(view);
                    }
                }
            }
        }


    }



}
