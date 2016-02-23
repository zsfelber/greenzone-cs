using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using System.Windows.Forms;
using GreenZoneFxEngine.Types;

namespace GreenZoneFxEngine.Gui.Chart
{
    class IndicatorChartPane : ChartChartPane
    {
        readonly List<IndicatorLevelLine> levelLines = new List<IndicatorLevelLine>();

        Chart chart;
        IndicatorChartSectionPanel parent;

        internal override void Init(Chart chart, ChartSectionPanel parent)
        {
            base.Init(chart, parent);
            this.chart = chart;
            this.parent = (IndicatorChartSectionPanel)parent;
        }

        protected override IChartOwner Owner
        {
            get
            {
                return chart == null ? null : chart.Owner;
            }
        }

        internal override void InvalidateDrawer()
        {
        }

        protected override void LayOut()
        {
            levelLines.Clear();
            if (parent != null && chart != null && Owner != null)
            {
                SeriesRange seriesRange = parent.SectionRange;

                indicatorDrawers.Clear();
                foreach (IndicatorBuffer b in parent.Indicator.Buffers)
                {
                    if (b.Buffer != null)
                    {
                        indicatorDrawers[b] = IndicatorDrawer.Create(b);
                    }
                }
                if (parent.Indicator.Levels != null)
                {
                    foreach (IndicatorLevel l in parent.Indicator.Levels)
                    {
                        int h = Height;
                        double minp = seriesRange.PriceFrom;
                        double pw = seriesRange.PriceRange;
                        double y = h - h * (l.Value - minp) / pw;
                        IndicatorLevelLine line = new IndicatorLevelLine(l, (int)y, l.Value);
                        levelLines.Add(line);
                    }
                    levelLines.Sort();
                }
            }


            base.LayOut();
        }

        protected override void DrawChart(PaintEventArgs e)
        {
        }

        protected override void DrawLevels(PaintEventArgs e)
        {
            foreach (var l in levelLines)
            {
                try
                {
                    l.drawer.DrawStarted(e.Graphics);
                }
                catch (OverflowException)
                {
                }
            }
            int last = Font.Height + 2;
            foreach (var l in levelLines)
            {
                try
                {
                    l.drawer.Draw(e.Graphics, 0, 0, l.y, l.y, 0);
                    l.drawer.Draw(e.Graphics, Width, Width, l.y, l.y, 0);
                    string lstr = l.value.ToString(parent.PriceFormat);
                    if (l.y < last + LevelFont.Height)
                    {
                        e.Graphics.DrawString(lstr, LevelFont, l.drawer.NormalBrush, 3, l.y + 1);
                        last = l.y + 1 + LevelFont.Height;
                    }
                    else
                    {
                        e.Graphics.DrawString(lstr, LevelFont, l.drawer.NormalBrush, 3, l.y - Font.Height);
                        last = l.y - Font.Height + LevelFont.Height;
                    }
                }
                catch (OverflowException)
                {
                }
            }
            foreach (var l in levelLines)
            {
                try
                {
                    l.drawer.DrawFinished(e.Graphics);
                }
                catch (OverflowException)
                {
                }
            }

            IndicatorRuntime ind = parent.Indicator;
            Mt4IndicatorInfo info = ind.IndicatorInfo;
            string lab = info.Name + " " + ind.Session.ShortName;
            e.Graphics.DrawString(lab, Font, fgBrush, 3, 2);
        }
    }
}
