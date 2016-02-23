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
    public partial class ChartSectionPanel : UserControl
    {
        internal Chart parent;

        internal readonly List<PriceLabelY> priceLabelYs = new List<PriceLabelY>();

        public ChartSectionPanel()
        {
            InitializeComponent();
        }

        internal virtual ChartPane ChartPane {
            get
            {
                return null;
            }
        }

        internal virtual void Init(Chart parent)
        {
            this.parent = parent;
            ChartPane.Init(parent, this);
            this.priceLabelPane.Init(parent, this);

            ChartPane.SliderValueDragged += new PropertyChangedEventHandler(ChartPane_SliderValueDragged);

            UpdateCursor();

            SizeChanged += new EventHandler(ChartSectionPanel_SizeChanged);
        }

        void ChartSectionPanel_SizeChanged(object sender, EventArgs e)
        {
            UpdateChartOnScreen();
        }


        internal virtual SeriesRange SectionRange
        {
            get
            {
                return parent.owner.SeriesRange;
            }
        }

        internal virtual IndicatorWindowType WindowType
        {
            get
            {
                return IndicatorWindowType.CHART_WINDOW;
            }
        }

        internal virtual string PriceFormat
        {
            get
            {
                string f = parent.owner.SymbolFormat;
                return f;
            }
        }


        internal virtual void DrawCursor()
        {
            SeriesRange r = SectionRange;

            if (parent.owner.IsCursorBarConnected)
            {
                ChartPane.SliderValue = parent.owner.ParentCursorPosition;
            }
            else
            {
                ChartPane.SliderValue = r.CursorPosition;
            }
            UpdateCpCursor();
        }

        internal virtual void UpdateCursor()
        {
            SeriesRange r = SectionRange;

            int offsetFrom = r.OffsetFrom;

            if (parent.owner.IsCursorBarConnected)
            {
                ChartPane.SliderBarColor = Color.FromArgb(100, Color.OrangeRed);

                r.CursorPosition = parent.owner.SeriesRange.CursorPosition;

                ChartPane.SliderValue = parent.owner.ParentCursorPosition;
            }
            else
            {
                ChartPane.SliderBarColor = Color.FromArgb(100, Color.Blue);
                ChartPane.SliderValue = r.CursorPosition;
            }

            UpdateCpCursor();
        }

        internal virtual void UpdateCpCursor()
        {
            SeriesRange r = SectionRange;
            ChartPane.CpBarVisible = parent.owner.IsCursorBarConnected && ChartPane.SeriesBars.Count > 0 &&
                                      Math.Abs(parent.owner.ParentCursorPosition - r.CursorPosition) > ChartPane.SeriesBarWidth;
            ChartPane.CpBarValue = r.CursorPosition;
        }

        internal void CalcSeriesBars()
        {
            ChartPane.PerformLayout();
        }

        internal void UpdateChartOnScreen()
        {
            ChartPane.PerformLayout();
            CalculatePriceLabelYs();
            ChartPane.Invalidate();
            ChartPane.Update();
            priceLabelPane.Invalidate();
            priceLabelPane.Update();
        }

        public virtual void CalculateSeriesRangeToFit()
        {
        }

        internal new virtual int Scale
        {
            get
            {
                return 0;
            }
        }

        internal virtual void CalculatePriceLabelYs()
        {
            priceLabelYs.Clear();

            int scale = Scale;

            SeriesRange seriesRange = SectionRange;
            double pr = seriesRange.PriceRange;

            const int levelPix = 30;
            // levelPix ~= 10^x
            double levelD = (double)levelPix / ChartPane.Height;

            double prD = levelD * pr;
            double lPrice10 = Math.Pow(10.0, Math.Round(Math.Log10(prD)));
            double lPrice25 = 2.5 * Math.Pow(10.0, Math.Round(Math.Log10(prD / 2.5)));
            double lPrice50 = 5.0 * Math.Pow(10.0, Math.Round(Math.Log10(prD / 5.0)));

            double lPrice = lPrice10;

            if (Math.Abs(lPrice - prD) > Math.Abs(lPrice25 - prD))
            {
                lPrice = lPrice25;
            }
            if (Math.Abs(lPrice - prD) > Math.Abs(lPrice50 - prD))
            {
                lPrice = lPrice50;
            }

            long iPrice = (int)Math.Round(lPrice / parent.owner.Point);

            int prFrom = (int)Math.Ceiling(seriesRange.PriceFrom / lPrice);
            int prTo = (int)Math.Floor(seriesRange.PriceTo / lPrice);
            for (int i = prFrom; i <= prTo; i++)
            {
                double prLev = i * lPrice;
                int screenY = ChartPane.Height - (int)(ChartPane.Height * (prLev - seriesRange.PriceFrom) / pr);

                long iip = i * iPrice * 1000;
                long jb;
                if (parent.owner.Digits % 2 == 0)
                {
                    jb = 10000;
                }
                else
                {
                    jb = 100000;
                }
                jb = jb * (int)Math.Pow(10, 3 + scale);
                for (Int64 j = jb, imp = 5; imp >= 0; j /= 10, imp--)
                {
                    if (iip % j == 0)
                    {
                        PriceLabelY l = new PriceLabelY(prLev, screenY, (int)imp);
                        priceLabelYs.Add(l);
                        break;
                    }
                }
            }
        }
        
        void ChartPane_SliderValueDragged(object sender, PropertyChangedEventArgs e)
        {
            parent.SliderValueDragged(this);
        }
    }
}
