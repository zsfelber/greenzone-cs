using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.Util;

using GreenZoneFxEngine.Types;
using System.Drawing;
using System.Drawing.Drawing2D;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class CursorChartController : ClientCursorChartControllerBase
    {

        public CursorChartController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected override void SaveSessionAsync()
        {
            ((MainWindowController)MainWindow).SaveSessionAsync();
        }

        public override void UpdateAllChartAndCursor()
        {
            if (ChartPanel != null)
            {
                ChartPanel.Chart1.UpdateAllChartAndCursor();
            }
        }

        public override void ParentUpdateAllChartAndCursor()
        {
            if (Owner.IsCursorBarConnected)
            {
                ChartGroupPanel.UpdateAllChartAndCursor(ChartPanel);
            }
        }

        public override void CalculateSeriesRangeToFit(bool includeMainChart)
        {
            MasterChartSectionPanel.CalculateSeriesRangeToFit();
        }

        public override void ScrollYToPrice()
        {
            MasterChartSectionPanel.ScrollYToPrice();
        }

        public override void CalculateTimeLabelXs()
        {
            SeriesRange seriesRange = MasterChartSectionPanel.SectionRange;

            List<TimeLabelX> timeLabelXs = new List<TimeLabelX>();
            int i = seriesRange.OffsetTo;
            foreach (SeriesBar bar in MasterChartSectionPanel.ChartPane.SeriesBars)
            {
                if (bar != null && i < Owner.sLTime.Length - 1)
                {
                    datetime pd = (datetime)Owner.sLTime[i + 1];

                    datetime d = bar.time;
                    string fd = "";
                    int imp = 0;

                    if (pd.Year != d.Year)
                    {
                        imp = 4;
                        fd = d.ToString("yyyy");
                    }
                    else if (pd.Month != d.Month)
                    {
                        imp = 2;
                        fd = d.ToString("MM");
                    }

                    if (imp > 0)
                    {
                        TimeLabelX l = new TimeLabelX(d, fd, bar.x1, imp);
                        timeLabelXs.Add(l);
                    }

                }
                i--;
            }

            TimeLabelXsUpper.Clear();
            TimeLabelXsLower.Clear();
            TimeLabelXsUpper.AddRange(timeLabelXs);
        }


        public override void PrintStatus(SeriesBar _bar, IndicatorBar ibar, string f)
        {
            IMainWindowController mainWindowController = ChartGroupPanel.MainWindow;

            CursorSeriesBar bar = (CursorSeriesBar)_bar;

            mainWindowController.OLabel.Text = "";
            mainWindowController.LLabel.Text = "";
            mainWindowController.HLabel.Text = "";
            mainWindowController.CLabel.Text = "";

            mainWindowController.TimeLabel.Text = bar.time.ToString(GreenZoneUtilsBase.GetShortDateTimePattern());
            mainWindowController.OpenLabel.Text = bar.close.ToString(f);
            mainWindowController.LowLabel.Text = "";
            mainWindowController.HighLabel.Text = "";
            mainWindowController.CloseLabel.Text = "";

            mainWindowController.VLabel.Text = "";
            mainWindowController.ValueLabel.Text = "";
        }

    }

}
