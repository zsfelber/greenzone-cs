using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class IndicatorChartPaneController : ClientIndicatorChartPaneControllerBase
    {
        readonly List<IndicatorLevelLine> levelLines = new List<IndicatorLevelLine>();

        internal IndicatorChartPaneController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        public override ChartDrawerController Drawer
        {
            get
            {
                return null;
            }
        }

        public override Font LevelFont
        {
            get
            {
                return base.LevelFont;
            }
            set
            {
                base.LevelFont = value;
                Update();
            }
        }

        protected override void LayOut()
        {
            levelLines.Clear();
            if (Parent != null && Chart != null && Owner != null)
            {
                SeriesRange seriesRange = Parent.SectionRange;

                indicatorDrawers.Clear();
                foreach (IndicatorBuffer b in Parent.Indicator.Buffers)
                {
                    if (b.Buffer != null)
                    {
                        indicatorDrawers[b] = IndicatorDrawerController.Create((IndicatorRuntime)Parent.Indicator, b);
                    }
                }
                if (Parent.Indicator.Levels != null)
                {
                    foreach (IndicatorLevel l in Parent.Indicator.Levels)
                    {
                        int h = Height - 2;
                        double minp = seriesRange.PriceFrom;
                        double pw = seriesRange.PriceRange;
                        double y = h - h * (l.Value - minp) / pw;
                        IndicatorLevelLine line = new IndicatorLevelLine((IndicatorRuntime)Parent.Indicator, l, (int)y, l.Value);
                        levelLines.Add(line);
                    }
                    levelLines.Sort();
                }
            }


            base.LayOut();
        }

        protected override void paintBar(GraphicsController g, SeriesBar bar, SeriesBar next, bool isActive)
        {
        }

        protected override void DrawLevels(ControllerPaintEventArgs e)
        {
            GraphicsController g = e.Graphics;
            foreach (var l in levelLines)
            {
                try
                {
                    l.drawer.DrawStarted(e);
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
                    l.drawer.Draw(g, 0, 0, l.y, l.y, 0, false);
                    l.drawer.Draw(g, Width, Width, l.y, l.y, 0, false);
                    string lstr = l.value.ToString(Parent.PriceFormat);
                    if (l.y < last + LevelFont.Height)
                    {
                        g.DrawString(lstr, LevelFont, l.drawer.NormalBrush, 3, l.y + 1);
                        last = l.y + 1 + LevelFont.Height;
                    }
                    else
                    {
                        g.DrawString(lstr, LevelFont, l.drawer.NormalBrush, 3, l.y - Font.Height);
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
                    l.drawer.DrawFinished(e);
                }
                catch (OverflowException)
                {
                }
            }

            IndicatorRuntime ind = (IndicatorRuntime)Parent.Indicator;
            Mt4ExecutableInfo info = ind.IndicatorInfo;
            string lab = info.Name + " " + ind.Session.ShortName;
            g.DrawString(lab, Font, FgBrush, 3, 2);
        }
    }

}
