using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;


namespace GreenZoneFxEngine.ViewController.Chart
{
    public abstract class ClientChartSectionPanelControllerEx : ClientChartSectionPanelControllerBase
    {

        public ClientChartSectionPanelControllerEx(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public ClientChartSectionPanelControllerEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            SizeChanged += new PropertyChangedEventHandler(ChartSectionPanel_SizeChanged);
            PriceLabelYs = new List<PriceLabelY>();
        }

        protected ClientChartSectionPanelControllerEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        public override string PriceFormat
        {
            get
            {
                string f = Owner.SymbolFormat;
                return f;
            }
        }

        public override int Scale
        {
            get
            {
                return 0;
            }
        }


        public override void UpdateCursor()
        {
            SeriesRange r = SectionRange;

            if (Owner.IsCursorBarConnected)
            {
                ChartPane.SliderValue = Owner.ParentCursorPosition;
            }
            else
            {
                ChartPane.SliderValue = r.CursorPosition;
            }

            ChartPane.CpBarValue = r.CursorPosition;
            ChartPane.CpBarVisible = Owner.IsCursorBarConnected && ChartPane.SeriesBars.Count > 0 &&
                                     Math.Abs(Owner.ParentCursorPosition - r.CursorPosition) > ChartPane.SeriesBarWidth;
        }

        public override void UpdateChartOnScreen(bool layout = true)
        {
            if (layout)
            {
                ChartPane.Layout();
            }
            CalculatePriceLabelYs();
            ChartPane.Update();
            PriceLabelPane1.Update();
        }

        public override void CalculateSeriesRangeToFit()
        {
        }

        public override void CalculatePriceLabelYs()
        {
            CalculatePriceLabelYs(30);
        }

        public override void CalculatePriceLabelYs(int levelPix)
        {
            PriceLabelYs.Clear();

            int scale = Scale;

            SeriesRange seriesRange = SectionRange;
            double pr = seriesRange.PriceRange;

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

            long iPrice = (int)Math.Round(lPrice / Owner.Point);

            int prFrom = (int)Math.Ceiling(seriesRange.PriceFrom / lPrice);
            int prTo = (int)Math.Floor(seriesRange.PriceTo / lPrice);
            for (int i = prFrom; i <= prTo; i++)
            {
                double prLev = i * lPrice;
                int screenY = ChartPane.Height - (int)(ChartPane.Height * (prLev - seriesRange.PriceFrom) / pr);

                long iip = i * iPrice * 1000;
                long jb;
                if (Owner.Digits % 2 == 0)
                {
                    jb = 10000;
                }
                else
                {
                    jb = 100000;
                }
                jb = jb * (int)Math.Pow(10, 3 + scale);
                int imp = 5;
                for (Int64 j = jb; imp >= 0; j /= 10, imp--)
                {
                    if (iip % j == 0)
                    {
                        PriceLabelY l = new PriceLabelY(prLev, screenY, imp);
                        PriceLabelYs.Add(l);
                        break;
                    }
                }
            }
        }

        void ChartSectionPanel_SizeChanged(object sender, ControllerEventArgs e)
        {
            UpdateChartOnScreen();
        }

    }
}