using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Windows.Forms;
using GreenZoneUtil.Util;

using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public abstract class ClientChartControllerEx : ClientChartControllerBase
    {
        public ClientChartControllerEx(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
            TimeLabelXsUpper = new List<TimeLabelX>();
            TimeLabelXsLower = new List<TimeLabelX>();
        }

        public ClientChartControllerEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            TimeLabelXsUpper = new List<TimeLabelX>();
            TimeLabelXsLower = new List<TimeLabelX>();

            SizeChanged += new PropertyChangedEventHandler(ChartController_SizeChanged);

            ZoomOutVButton.Pressed += new ControllerEventHandler(zoomOutVButton_Click);
            ZoomOutHButton.Pressed += new ControllerEventHandler(zoomOutHButton_Click);
            ZoomInVButton.Pressed += new ControllerEventHandler(zoomInVButton_Click);
            ZoomInHButton.Pressed += new ControllerEventHandler(zoomInHButton_Click);
            ZoomToFitButton.Pressed += new ControllerEventHandler(zoomToFitButton_Click);
            ZoomToScrollPriceButton.Pressed += new ControllerEventHandler(zoomToScrollPriceButton_Click);

            UpdateChartAndCursor();
        }

        protected ClientChartControllerEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            TimeLabelXsUpper = new List<TimeLabelX>();
            TimeLabelXsLower = new List<TimeLabelX>();
        }

        protected abstract void SaveSessionAsync();

        public override void UpdateCursor()
        {
            MasterChartSectionPanel.UpdateCursor();
            foreach (ClientChartSectionPanelControllerEx p in ChartSectionPanels)
            {
                p.UpdateCursor();
            }
        }

        public override void UpdateChartAndCursor()
        {
            UpdateCursor();
            UpdateSeries();
        }


        public override void UpdateSeries(bool enableAutoFit = true)
        {
            if (enableAutoFit && Owner.AutoSeriesRange)
            {
                CalculateSeriesRangeToFit(true);
            }
            else
            {
                CalculateSeriesRangeToFit(false);
            }

            UpdateChartOnScreen();
        }

        public override void UpdateChartOnScreen()
        {
            MasterChartSectionPanel.ChartPane.Layout();

            CalculateTimeLabelXs();

            MasterChartSectionPanel.UpdateChartOnScreen(false);

            foreach (ClientChartSectionPanelControllerEx p in ChartSectionPanels)
            {
                p.UpdateChartOnScreen();
            }

            TimeLabelPane1.Update();
        }

        public override void CalculateSeriesRangeToFit(bool includeMainChart)
        {
        }

        public override void ScrollYToPrice()
        {
        }


        public override void CalculateTimeLabelXs()
        {
            SeriesRange seriesRange = Owner.SeriesRange;

            List<TimeLabelX> timeLabelXs1 = new List<TimeLabelX>();
            int maximp = 0;
            int i = seriesRange.OffsetTo;
            foreach (SeriesBar bar in MasterChartSectionPanel.ChartPane.SeriesBars)
            {
                if (bar != null && i < Owner.sLTime.Length - 1)
                {
                    datetime pd = (datetime)Owner.sLTime[i + 1];

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
                TimeLabelXsUpper.Clear();
                TimeLabelXsLower.Clear();
                TimeLabelXsUpper.AddRange(timeLabelXs1);
                TimeLabelXsLower.AddRange(timeLabelXs2);
            }
            else
            {
                for (int imp = 0; imp < 7; imp++)
                {
                    clear_lxs(timeLabelXs1, imp);
                }
                TimeLabelXsUpper.Clear();
                TimeLabelXsLower.Clear();
                TimeLabelXsUpper.AddRange(timeLabelXs1);
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

                        txtWidth = TextRenderer.MeasureText(p.formattedTime, MasterChartSectionPanel.Font).Width;
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

        public override void UpdateAllChartAndCursor()
        {
            ParentUpdateAllChartAndCursor();
            UpdateChartAndCursor();
        }

        public override void ParentUpdateAllChartAndCursor()
        {
        }

        public override void PrintStatus(SeriesBar bar, IndicatorBar ibar, string f)
        {
        }

        void ChartController_SizeChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateSeries();
        }




        void zoomH(double factor, int min, int max)
        {
            datetime scrtm = Owner.ScrolledBarTime;
            SeriesRange r = Owner.SeriesRange;
            r.Zoom(factor, min, max);
            Owner.SeriesRange = r;

            if (Owner.IsCursorBarConnected)
            {
                Owner.ScrolledBarTime = scrtm;
                UpdateCursor();
            }

            SaveSessionAsync();
            UpdateSeries();
        }

        private void zoomToFitButton_Click(object sender, ControllerEventArgs e)
        {
            CalculateSeriesRangeToFit(true);
            UpdateChartOnScreen();
        }

        private void zoomToScrollPriceButton_Click(object sender, ControllerEventArgs e)
        {
            ScrollYToPrice();
            UpdateChartOnScreen();
        }

        private void zoomOutHButton_Click(object sender, ControllerEventArgs e)
        {
            zoomH(1.1, 50, 400);
        }

        private void zoomInHButton_Click(object sender, ControllerEventArgs e)
        {
            zoomH(0.9, 50, 400);
        }

        private void zoomOutVButton_Click(object sender, ControllerEventArgs e)
        {
            SeriesRange r = Owner.SeriesRange;
            double d = (r.PriceTo - r.PriceFrom) / 20;
            r.PriceFrom -= d;
            r.PriceTo += d;
            Owner.SeriesRange = r;
            SaveSessionAsync();
            UpdateSeries(false);
        }

        private void zoomInVButton_Click(object sender, ControllerEventArgs e)
        {
            SeriesRange r = Owner.SeriesRange;
            double d = (r.PriceTo - r.PriceFrom) / 20;
            r.PriceFrom += d;
            r.PriceTo -= d;
            Owner.SeriesRange = r;
            SaveSessionAsync();
            UpdateSeries(false);
        }

    }
}