using System;
using System.Collections.Generic;
using GreenZoneFxEngine.Util;
using System.Reflection;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ComponentModel;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Types;
using System.IO;
using System.Text;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.Trading
{

    public abstract class ServerScriptRuntime : ServerScriptRuntimeBase
    {

        public ServerScriptRuntime(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo executableInfo, ISeriesManagerCache icache)
            : base(rmiManager, parent, icache)
        {
            Session = new ServerScriptSession(rmiManager, parent.Environment, executableInfo);
        }

        public ServerScriptRuntime(GreenRmiManager rmiManager, IChartRuntime parent, IScriptSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
            this.Session = session;
        }

    }

    public abstract class ServerExpertRuntime : ServerExpertRuntimeBase
    {

        public ServerExpertRuntime(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo executableInfo, ISeriesManagerCache icache)
            : base(rmiManager, parent, icache)
        {
            Session = new ServerExpertSession(rmiManager, parent.Environment, executableInfo);
        }

        public ServerExpertRuntime(GreenRmiManager rmiManager, IChartRuntime parent, IExpertSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
            Session = session;
        }


    }

    public abstract class ServerIndicatorRuntime : ServerIndicatorRuntimeEx
    {

        public ServerIndicatorRuntime(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo executableInfo, ISeriesManagerCache icache, IndicatorWindowType windowType = IndicatorWindowType.CHART_WINDOW, int buffersCnt = 0, params IndicatorBuffer[] bufferData)
            : this(rmiManager, parent, executableInfo, icache, windowType, buffersCnt, Double.MinValue, Double.MaxValue, bufferData)
        {
        }

        public ServerIndicatorRuntime(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo executableInfo, ISeriesManagerCache icache, IndicatorWindowType windowType, int buffersCnt, double minimum, double maximum, params IndicatorBuffer[] bufferData)
            : base(rmiManager, parent, icache)
        {
            Session = new ServerIndicatorSession(rmiManager, parent.Environment, executableInfo);
            Session.WindowType = windowType;

            AddBuffers(buffersCnt, bufferData);

            Session.IndicatorMinimum = minimum;
            Session.IndicatorMaximum = maximum;
        }

        public ServerIndicatorRuntime(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
            this.Session = session;
            NumIndicatorBuffers = session.NumBuffers;
            NumIndicatorLevels = session.NumLevels;
            for (int i = 0; i < Buffers.Length; i++)
            {
                Buffers[i].Params = session.Buffers[i];
            }
            for (int i = 0; i < Levels.Length; i++)
            {
                Levels[i].Params = session.Levels[i];
            }
        }


    }




}
