using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Util;
using System.Drawing;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.Trading
{

    [Indicator()]
    class Prices : ServerIndicatorRuntime
    {
        //---- input parameters
        PriceConstant _PriceType = 0;

        public PriceConstant PriceType { get { return _PriceType; } set { _PriceType = value; } }

        //---- buffers
        DArr Buffer;

        public Prices(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.CHART_WINDOW, 1)
        {
        }

        public Prices(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            //---- indicator line
            SetIndexBuffer(0, ref Buffer);
            SetIndexStyle(0, DrawingStyle.DRAW_LINE, DrawingStylesWidth1.STYLE_SOLID, DrawingWidth.WIDTH_1, Color.DarkMagenta);

            Session.ShortName = "" + PriceType;
            return 0;
        }
        public override int Deinit()
        {
            return 0;
        }
        public override int OnTick()
        {
            int limit = Bars - IndicatorCounted;
            //----
            for (int i = 0; i < limit; i++)
            {
                switch (PriceType)
                {
                    case PriceConstant.PRICE_OPEN: Buffer[i] = Open[i]; break;
                    case PriceConstant.PRICE_LOW: Buffer[i] = Low[i]; break;
                    case PriceConstant.PRICE_HIGH: Buffer[i] = High[i]; break;
                    case PriceConstant.PRICE_CLOSE: Buffer[i] = Close[i]; break;
                    case PriceConstant.PRICE_MEDIAN: Buffer[i] = (High[i] + Low[i]) / 2; break;
                    case PriceConstant.PRICE_TYPICAL: Buffer[i] = (High[i] + Low[i] + Close[i]) / 3; break;
                    case PriceConstant.PRICE_WEIGHTED: Buffer[i] = (High[i] + Low[i] + 2 * Close[i]) / 4; break;
                }
            }
            return 0;
        }
    }
}
