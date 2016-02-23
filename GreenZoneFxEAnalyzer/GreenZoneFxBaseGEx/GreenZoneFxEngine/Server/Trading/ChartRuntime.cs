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

namespace GreenZoneFxEngine.Trading
{
    public class ServerChartGroupRuntime : ServerChartGroupRuntimeBase
    {

        public ServerChartGroupRuntime(GreenRmiManager rmiManager, IEnvironmentRuntime parent)
            : base(rmiManager)
        {
            Environment = parent;
            Session = new ServerChartGroupSession(rmiManager);
        }

        internal ServerChartGroupRuntime(GreenRmiManager rmiManager, IEnvironmentRuntime parent, IChartGroupSession session)
            : base(rmiManager)
        {
            Environment = parent;
            Session = session;

            Charts = new List<IChartRuntime>();
            Charts.Clear();
            bool isMaster = true;
            for (int i = 0; i < session.ChartSessions.Count; i++)
            {
                var chart = new ServerChartRuntime(rmiManager, this, isMaster, (ServerChartSession)session.ChartSessions[i]);
                Charts.Add(chart);
                isMaster = false;
            }
        }

        public override void AddChart(IServerChartRuntime chart)
        {
            Charts.Add(chart);
            Session.AddChart(chart.Session);
        }

        public override void RemoveChart(IServerChartRuntime chart)
        {
            if (Charts.Count <= 1)
            {
                throw new NotSupportedException();
            }
            Charts.Remove(chart);
            Session.RemoveChart(chart.Session);
        }
    }

    public class ServerChartRuntime : ServerChartRuntimeBase
    {
        //bool online;

        public ServerChartRuntime(GreenRmiManager rmiManager, ServerChartGroupRuntime group, bool isMaster)
            : base(rmiManager)
        {
            Group = group;
            Session = new ServerChartSession(rmiManager);
            CursorRuntime = new ServerChartCursorRuntime(rmiManager, this);
            //this.online = this.root.EnvironmentType.IsOnline();
            IsMaster = isMaster;
        }

        internal ServerChartRuntime(GreenRmiManager rmiManager, ServerChartGroupRuntime group, bool isMaster, ServerChartSession session)
            : base(rmiManager)
        {
            Group = group;
            IsMaster = isMaster;

            Session = session;
            CursorRuntime = new ServerChartCursorRuntime(rmiManager, this);

            datetime dt = session.ScrolledBarTime;

            Symbol = session.Symbol;
            // NOTE LoadSeriesArrays here
            Period = session.Period;

            session.ScrolledBarTime = dt;

            // !  guiSeriesManager
            LoadSeriesArrays();

            if (session.Script == null)
            {
                Script = null;
            }
            else
            {
                Script = (IServerScriptRuntime)ServerScriptRuntime.Create(rmiManager, this, session.Script, GuiSeriesManager.DefaultCache);
            }

            if (session.Expert == null)
            {
                Expert = null;
            }
            else
            {
                Expert = (IServerExpertRuntime)ServerExpertRuntime.Create(rmiManager, this, session.Expert, GuiSeriesManager.DefaultCache);
            }

            try
            {
                GuiSeriesManager.FocusedTime = session.ScrolledBarTime;

                foreach (var ie in session.Indicators)
                {
                    ISeriesManagerCache cache = GuiSeriesManager[ie.Key.Symbol, ie.Key.Period];
                    IIndicatorRuntime ind = (IIndicatorRuntime)ServerIndicatorRuntime.Create(rmiManager, this, ie.Value, cache);
                    cache.AddIndicator(ind);
                    ind.Visible = true;
                }
            }
            catch (TimeSeriesEOFException)
            {
            }
        }

        public override IServerSymbolRuntime SymbolRuntime
        {
            get
            {
                lock (Environment.Locker)
                {
                    return (ServerSymbolRuntime)base.SymbolRuntime;
                }
            }
            set
            {
                lock (Environment.Locker)
                {
                    base.SymbolRuntime = value;
                }
            }
        }

        public override IServerScriptRuntime Script
        {
            get
            {
                return base.Script;
            }
            set
            {
                Script = value;
                if (Script == null)
                    Session.Script = null;
                else
                    Session.Script = (ServerScriptSession)Script.Session;
            }
        }

        public override IServerExpertRuntime Expert
        {
            get
            {
                return base.Expert;
            }
            set
            {
                Expert = value;
                if (Expert == null)
                    Session.Expert = null;
                else
                    Session.Expert = (ServerExpertSession)Expert.Session;
            }
        }

        public override symbol Symbol
        {
            get
            {
                return base.Symbol;
            }
            set
            {
                // TODO in child classes : Script Indicator Expert
                lock (Environment.Locker)
                {
                    base.Symbol = value;
                    IServerSymbolContext symbolContext = (IServerSymbolContext)Environment.SymbolContexts[value];
                    SymbolRuntime = symbolContext.Runtime;

                    Session.Symbol = value;

                    LoadSeriesArrays();
                }
            }
        }

        public override TimePeriodConst Period
        {
            get
            {
                return base.Period;
            }
            set
            {
                // TODO in child classes : Script Indicator Expert
                lock (Environment.Locker)
                {
                    base.Period = value;
                    Session.Period = value;

                    LoadSeriesArrays();
                }
            }
        }

        public override void Update(symbol _symbol, TimePeriodConst _period)
        {
            // TODO in child classes : Script Indicator Expert
            lock (Environment.Locker)
            {
                Symbol = _symbol;
                IServerSymbolContext symbolContext = (IServerSymbolContext)Environment.SymbolContexts[_symbol];
                SymbolRuntime = symbolContext.Runtime;

                Session.Symbol = _symbol;

                Period = _period;
                Session.Period = _period;

                LoadSeriesArrays();
            }
        }

        // TODO in child classes : Script Indicator Expert
        internal void LoadSeriesArrays()
        {
            // TODO lock ??
            lock (Environment.Locker)
            {
                if (Symbol != null && Period != TimePeriodConst.PERIOD_CURRENT)
                {
                    if (GuiSeriesManager == null)
                    {
                        Session.ScrolledBarTime = Group.ScrolledBarTime;
                        //FIXME always invoked from form currently
                        //environment.Session.Save();
                        GuiSeriesManager = new ServerSeriesManagerRuntimeEx(rmiManager, this);
                        SeriesRange = Session.SeriesRange;

                        CursorRuntime.SeriesManager = new ServerSeriesManagerRuntimeEx(rmiManager, this, true);
                    }
                    else if (Symbol != GuiSeriesManager.DefaultCache.Symbol ||
                             Period != GuiSeriesManager.DefaultCache.Period)
                    {
                        try
                        {
                            GuiSeriesManager.UpdateDefault();
                        }
                        catch (TimeSeriesException ex)
                        {
                            Symbol = GuiSeriesManager.DefaultCache.Symbol;
                            Period = GuiSeriesManager.DefaultCache.Period;
                            throw ex;
                        }
                        // TODO Client invoked?
                        CursorRuntime.UpdateSeriesManager();
                    }
                }
            }
        }

        

        public override void AddIndicator(IServerIndicatorRuntime indicatorRuntime)
        {
            IndicatorId id0 = GuiSeriesManager.DefaultCache.AddIndicator(indicatorRuntime);
            SymbolPeriodIndicatorId id = new SymbolPeriodIndicatorId(Symbol, Period, id0);
            Session.Indicators[id] = indicatorRuntime.Session;
        }

        public override void ReplaceIndicator(IndicatorId id0, IServerIndicatorRuntime indicatorRuntime)
        {
            IndicatorId id1 = GuiSeriesManager.DefaultCache.ReplaceIndicator(id0, indicatorRuntime);
            SymbolPeriodIndicatorId spid0 = new SymbolPeriodIndicatorId(Symbol, Period, id0);
            SymbolPeriodIndicatorId spid1 = new SymbolPeriodIndicatorId(Symbol, Period, id1);
            Session.Indicators.Remove(spid0);
            Session.Indicators[spid1] = indicatorRuntime.Session;
        }

        public override IIndicatorRuntime RemoveIndicator(IndicatorId id0)
        {
            SymbolPeriodIndicatorId id = new SymbolPeriodIndicatorId(Symbol, Period, id0);
            Session.Indicators.Remove(id);
            IIndicatorRuntime ind = GuiSeriesManager.DefaultCache.RemoveIndicator(id0);
            return ind;
        }



    }



    public class ServerChartCursorRuntime : ServerChartCursorRuntimeBase
    {

        internal ServerChartCursorRuntime(GreenRmiManager rmiManager, ServerChartRuntime parent)
            : base(rmiManager)
        {
            Parent = parent;
        }

    }





}
