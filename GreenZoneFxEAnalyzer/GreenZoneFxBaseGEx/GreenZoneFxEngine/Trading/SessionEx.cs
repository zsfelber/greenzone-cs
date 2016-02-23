using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
    public class ChartGroupSessionEx : ChartGroupSessionBase
    {
        public ChartGroupSessionEx(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
            ChartSessions = new List<IChartSession>();
        }

        protected ChartGroupSessionEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void AddChart(IChartSession chartSession)
        {
            ChartSessions.Add(chartSession);
        }

        public override void RemoveChart(IChartSession chart)
        {
            if (ChartSessions.Count <= 1)
            {
                throw new NotSupportedException();
            }
            ChartSessions.Remove(chart);
        }
    }
}
