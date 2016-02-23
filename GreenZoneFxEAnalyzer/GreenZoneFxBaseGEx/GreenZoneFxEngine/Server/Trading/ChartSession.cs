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

    public class ServerChartGroupSession : ChartGroupSessionEx
    {
        public ServerChartGroupSession(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
            ChartSessions = new List<IChartSession>();
        }

        protected ServerChartGroupSession(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }



    [Serializable]
    public class ServerChartSession : ChartSessionBase
    {

        public ServerChartSession(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
            From = datetime.MinValue;
            To = datetime.MaxValue;
            TopBarVisible = true;
            BottomBarVisible = true;
            Indicators = new Dictionary<SymbolPeriodIndicatorId, IIndicatorSession>();
        }

        protected ServerChartSession(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        public override ChartType ChartType
        {
            get
            {
                return base.ChartType;
            }
            set
            {
                base.ChartType = value;
                if (Period.GetCategory() != TimePeriodCategory.TICKS)
                {
                    DefaultChartType = value;
                }
            }
        }

        [Category("Script running")]
        public override int ScriptRunningProgress
        {
            get
            {
                int d = To - From;
                int ttd = ScriptRunningTickTime - From;
                int progress = 100 * ttd / d;
                return progress;
            }
        }
    }








}
