using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Types;
using System.ComponentModel;
using GreenZoneFxEngine.Util;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
    public class ChartGroupRuntime : ClientChartGroupRuntimeEx
    {
        public ChartGroupRuntime(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
        protected ChartGroupRuntime(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
    }

    public class ChartRuntime : ClientChartRuntimeEx
    {

        public ChartRuntime(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
        protected ChartRuntime(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

    }


    public class ChartCursorRuntime : ClientChartCursorRuntimeEx
    {

        public ChartCursorRuntime(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
        protected ChartCursorRuntime(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
    }


    public class EnvironmentRuntime : EnvironmentRuntimeEx 
    {
        public EnvironmentRuntime(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
        protected EnvironmentRuntime(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

    }

    public class SeriesManagerRuntime : SeriesManagerRuntimeEx
    {
        public SeriesManagerRuntime(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
        protected SeriesManagerRuntime(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override void UpdateCursorDefault()
        {
            rmiManager.InvokeMethodFar(this, 1, null);
        }

        public override INormalSeriesManagerCache CreateSeriesManagerCache(SymbolPeriodId sp)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeSeriesRuntime : TimeSeriesRuntimeEx
    {
        public TimeSeriesRuntime(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
        protected TimeSeriesRuntime(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        protected override void LoadFile(datetime focusedTime)
        {
            rmiManager.InvokeMethodFar(this, 1, new object[] { focusedTime });
        }

        protected override void LoadFile()
        {
            rmiManager.InvokeMethodFar(this, 2, null);
        }

        protected override void FillFromFile(datetime focusedTime)
        {
            rmiManager.InvokeMethodFar(this, 3, new object[] { focusedTime });
        }

        protected override void FillFromFile(long tot_ind)
        {
            rmiManager.InvokeMethodFar(this, 4, new object[] { tot_ind });
        }

    }

    public class NormalSeriesManagerCache : NormalSeriesManagerCacheEx
    {
        public NormalSeriesManagerCache(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
        protected NormalSeriesManagerCache(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        protected override void updateIndicatorsServer(IIndicatorRuntime indicator)
        {
            rmiManager.InvokeMethodFar(this, 1, new object[] { indicator });
        }

        protected override void updateBuffersServer()
        {
            rmiManager.InvokeMethodFar(this, 2, null);
        }
    }

    public class OrdersHistoryView : OrdersHistoryViewEx
    {
        public OrdersHistoryView(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
        protected OrdersHistoryView(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override void LoadForward(int offset)
        {
            SeriesRange seriesRange = SeriesRange;
            int to = seriesRange.OffsetTo - offset;
            seriesRange.ChangeOffsetTo(to);
            if (seriesRange.OffsetFrom < 0)
            {
                throw new TimeSeriesEOFException();
            }
            SeriesRange = seriesRange;
        }

        public override void LoadAtTotal(long total_ind)
        {
            SeriesRange seriesRange = SeriesRange;
            seriesRange.ChangeOffsetTo((int)total_ind);
            if (seriesRange.OffsetFrom < 0 || seriesRange.OffsetTo >= FilteredOrders.Count)
            {
                throw new TimeSeriesEOFException();
            }
            SeriesRange = seriesRange;
        }
    }

    public class IndicatorRuntime : ClientIndicatorRuntimeBase
    {
        public IndicatorRuntime(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
        protected IndicatorRuntime(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override void RaiseInstanceChanged(IIndicatorRuntime newInstance)
        {
            NewClientInstance = newInstance;
        }
    }




}
