using System;
using System.Collections.Generic;
using System.Linq;
using GreenZoneFxEngine.Types;
using System.Threading;
using System.Windows.Forms;
using GreenZoneUtil.Util;
using System.Reflection;

namespace GreenZoneFxEngine.Trading
{















    public enum EaTestRequest
    {
        NONE,
        START_EA_TEST,
        STOP_EA_TEST,
        PAUSE_EA_TEST,
        CHANGE_SPEED,
    }

    public enum ScriptRunningRequest
    {
        NONE,
        START_SCRIPT,
        STOP_SCRIPT,
        PAUSE_SCRIPT,
    }



    public abstract class TestManagerNode<P, SELF> : VINode<P>
        where P : BaseVINode
        where SELF : TestManagerNode<P, SELF>
    {
        internal datetime time;

        internal TestManagerNode(P parent, int level, int priority = 0)
            : base(parent, level, priority)
        {
            time = datetime.MinValue;
        }

        public abstract datetime PrepareNext(datetime currentBest);
        public abstract void ProcessNext();
    }

    public abstract class TestManagerNode<P, SELF, C> : TestManagerNode<P, SELF>
        where P : BaseVINode
        where SELF : TestManagerNode<P, SELF>
        where C : TestManagerNode<SELF, C>
    {
        internal struct Pair : IComparable<Pair>
        {
            internal C node;
            internal datetime nextTime;

            internal Pair(C node, datetime nextTime)
            {
                this.node = node;
                this.nextTime = nextTime;
            }

            public int CompareTo(Pair other)
            {
                return nextTime.CompareTo(other.nextTime);
            }
        }

        internal Pair bestChild;
        internal C firstChild;

        internal TestManagerNode(P parent, int level, int priority = 0)
            : base(parent, level, priority)
        {
        }

        public override datetime PrepareNext(datetime currentBest)
        {
            bestChild = new Pair(null, datetime.MaxValue);

            foreach (List<BaseVINode> cs in childrenByPriority.Values)
            {
                foreach (C c in cs)
                {
                    try
                    {
                        datetime t = c.PrepareNext(currentBest);
                        if (t < bestChild.nextTime)
                        {
                            bestChild = new Pair(c, t);
                            currentBest = t;
                        }
                    }
                    catch (Exception e)
                    {
                        // TODO Log
                        MessageBox.Show(e.Message);
                    }
                }
            }

            firstChild = (C)childNodes[0];

            this.time = bestChild.nextTime;
            return this.time;
        }

        public override void ProcessNext()
        {
            DoIt1();

            bestChild.node.ProcessNext();

            DoIt2();
        }

        internal abstract void DoIt1();
        internal virtual void DoIt2()
        {
        }
    }

    public class TestManagerRootNode : TestManagerNode<TestManagerRootNode, TestManagerRootNode>
    {
        internal static readonly TestManagerRootNode Instance = new TestManagerRootNode();

        TestManagerRootNode() :
            base(null, 0)
        {
        }

        public override datetime PrepareNext(datetime currentBest)
        {
            return datetime.MaxValue;
        }
        public override void ProcessNext()
        {
        }
    }

    public class TestManagerNoChildNode<P> : TestManagerNode<P, TestManagerNoChildNode<P>> where P : BaseVINode
    {
        TestManagerNoChildNode() :
            base(null, 0)
        {
        }

        public override datetime PrepareNext(datetime currentBest)
        {
            return datetime.MaxValue;
        }
        public override void ProcessNext()
        {
        }
    }




    public class EaTestManager : TestManagerNode<TestManagerRootNode, EaTestManager, SymbolTestManager>
    {
        Thread thread;
        readonly List<EaTestRequest> requestQueue = new List<EaTestRequest>();

        internal readonly IServerEnvironmentSession environmentSession;
        internal readonly ServerSeriesManagerRuntimeEx seriesManager;

        public EaTestManager(ServerEnvironmentRuntime environment)
            : base(TestManagerRootNode.Instance, 0)
        {
            if (environment.EnvironmentType.IsOnline())
            {
                throw new NotSupportedException("Environment type is online");
            }
            Environment = environment;
            environmentSession = environment.Session;

            this.seriesManager = new ServerSeriesManagerRuntimeEx(environment.RmiManager, environment);

            thread = new Thread(Run);
            thread.Start();
        }

        public ServerEnvironmentRuntime Environment
        {
            get;
            private set;
        }

        public void AddRequest(EaTestRequest request)
        {
            lock (requestQueue)
            {
                if (request == EaTestRequest.NONE)
                {
                    throw new NotSupportedException();
                }
                requestQueue.Add(request);
                Monitor.Pulse(requestQueue);
            }
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    lock (requestQueue)
                    {
                        bool r1 = ProcessNextRequest();
                        bool r2;

                        if (environmentSession.EAStartStatus == StartStatus.STARTED)
                        {
                            try
                            {
                                datetime nextTime = PrepareNext(datetime.MaxValue);
                                r2 = nextTime != datetime.MaxValue;
                                if (r2)
                                {
                                    environmentSession.Time = nextTime;
                                    seriesManager.FocusedTime = nextTime;
                                    ProcessNext();
                                }
                            }
                            catch (TimeSeriesEOFException)
                            {
                                environmentSession.EAStartStatus = StartStatus.FINISHED;
                                r2 = false;
                            }
                            if (!r2)
                            {
                                seriesManager.DeinitAll();
                            }
                        }
                        else
                        {
                            r2 = false;
                        }

                        if (!r1 && !r2)
                        {
                            Monitor.Wait(requestQueue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (environmentSession.EAStartStatus.IsRunning())
                    {
                        environmentSession.EAStartStatus = StartStatus.ABORTED;
                    }
                    MessageBox.Show(ex.Message + "\n\nError in EATestManager : " + Environment.EnvironmentId, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        bool ProcessNextRequest()
        {
            EaTestRequest request = EaTestRequest.NONE;

            if (requestQueue.Count > 0)
            {
                request = requestQueue[0];
                requestQueue.RemoveAt(0);
            }

            if (request != EaTestRequest.NONE)
            {
                ProcessNextRequest(request);
                return true;
            }
            else
            {
                return false;
            }
        }

        void ProcessNextRequest(EaTestRequest request)
        {
            // TODO
            switch (request)
            {
                case EaTestRequest.START_EA_TEST:

                    switch (environmentSession.EAStartStatus)
                    {
                        case StartStatus.NOT_RUNNING:
                            environmentSession.Time = datetime.MinValue;
                            UpdateChildren();

                            environmentSession.EAStartStatus = StartStatus.STARTED;
                            // Max - Min !!
                            environmentSession.EATestingGlobalFrom = datetime.MaxValue;
                            environmentSession.EATestingGlobalTo = datetime.MinValue;
                            foreach (SymbolTestManager s in childNodes)
                            {
                                // Min - Max !!
                                environmentSession.EATestingGlobalFrom = datetime.Min(environmentSession.EATestingGlobalFrom, s.symbolSession.EATestingGlobalFrom);
                                environmentSession.EATestingGlobalTo = datetime.Max(environmentSession.EATestingGlobalTo, s.symbolSession.EATestingGlobalTo);
                            }

                            break;
                        default:
                            throw new NotSupportedException();
                    }
                    break;
                case EaTestRequest.PAUSE_EA_TEST:
                    switch (environmentSession.EAStartStatus)
                    {
                        case StartStatus.STARTED:
                            environmentSession.EAStartStatus = StartStatus.PAUSED;
                            break;
                        case StartStatus.PAUSED:
                            environmentSession.EAStartStatus = StartStatus.STARTED;
                            break;
                    }
                    break;
            }
        }

        void UpdateChildren()
        {
            childNodes.Clear();

            foreach (var s in environmentSession.SymbolSessions)
            {
                new SymbolTestManager(this, s.Key);
            }
        }

        internal override void DoIt1()
        {
        }

        internal override void DoIt2()
        {
        }
    }

    public class SymbolTestManager : TestManagerNode<EaTestManager, SymbolTestManager, SymbolTestManagerChild>
    {
        internal readonly EaTestManager eaTestManager;
        internal readonly ServerEnvironmentRuntime environment;
        internal readonly IServerEnvironmentSession environmentSession;
        internal readonly IServerSymbolContext symbolContext;
        internal readonly IServerSymbolRuntime symbolRuntime;
        internal readonly ISymbolSession symbolSession;

        internal SymbolTestManager(EaTestManager eaTestManager, symbol symbol)
            : base(eaTestManager, 1)
        {
            this.eaTestManager = eaTestManager;

            environment = eaTestManager.Environment;
            environmentSession = eaTestManager.Environment.Session;

            symbolContext = (ServerSymbolContext)environment.SymbolContexts[symbol];
            symbolRuntime = symbolContext.Runtime;
            symbolSession = symbolRuntime.Session;

            // Max - Min !!
            datetime from = datetime.MaxValue;
            datetime to = datetime.MinValue;

            foreach (ServerChartGroupRuntime cg in environment.Charts)
            {
                var c = cg.MasterChart;
                var cs = c.Session;
                if (c.Symbol == symbol && cs.AppearsInTest && c.Expert != null)
                {
                    // Min - Max !!
                    from = datetime.Min(from, cs.From);
                    to = datetime.Max(to, cs.To);

                    // !!!
                    eaTestManager.seriesManager.AddExpert(c);
                }
            }


            // !!!!
            TickSymbolTestManager.Create(this, symbolSession);


            // Max - Min !!
            datetime from2 = datetime.MaxValue;
            datetime to2 = datetime.MinValue;

            // NOTE !!! Not for ticks, just for OLHC periods
            TimePeriodConst[] periods = EnumExtensions.GetPeriods(TimePeriodCategory.MT4 | TimePeriodCategory.MT5 | TimePeriodCategory.SECS);
            Array.Sort(periods, comparePeriodBySecs);

            foreach (TimePeriodConst p in periods)
            {
                if (ServerTimeSeriesRuntimeEx.IsSeriesAvailable(environment, symbol, p))
                {
                    SymbolPeriodBarTestManager pt = new SymbolPeriodBarTestManager(this, p);
                    // Min - Max !!
                    from2 = datetime.Min(from2, pt.seriesCache.From);
                    to2 = datetime.Max(to2, pt.seriesCache.To);
                }
            }

            // Max - Min !!
            from = datetime.Max(from, from2);
            to = datetime.Min(to, to2);

            symbolSession.EATestingGlobalFrom = from;
            symbolSession.EATestingGlobalFrom = to;
        }

        internal override void DoIt1()
        {
        }

        static int comparePeriodBySecs(TimePeriodConst p1, TimePeriodConst p2)
        {
            int secs1 = p1.GetSecs();
            int secs2 = p2.GetSecs();
            return secs1 - secs2;
        }
    }

    public abstract class SymbolTestManagerChild : TestManagerNode<SymbolTestManager, SymbolTestManagerChild, TestManagerNoChildNode<SymbolTestManagerChild>>
    {
        internal bool virgin;

        internal SymbolTestManagerChild(SymbolTestManager symbolTestManager, int priority)
            : base(symbolTestManager, 2, priority)
        {
            virgin = true;
        }
    }

    public abstract class TickSymbolTestManager : SymbolTestManagerChild
    {
        internal readonly EaTestManager eaTestManager;
        internal readonly IEnvironmentRuntime environment;
        internal readonly IEnvironmentSession environmentSession;

        internal symbol symbol;
        internal readonly IServerSymbolContext symbolContext;
        internal readonly IServerSymbolRuntime symbolRuntime;
        internal readonly ISymbolSession symbolSession;
        internal ServerSeriesManagerRuntimeEx seriesManager;
        internal IServerNormalSeriesManagerCache tickSeriesCache;

        internal readonly TimePeriodConst period;
        internal readonly TestType testType;

        internal double bid, ask, open, low, high, close, spread;
        internal double volume;
        internal int subindex;
        internal bool upwards;

        // NOTE  priority : 1   !!!!   see SymbolPeriodBarTestManager priority : 0
        internal TickSymbolTestManager(SymbolTestManager symbolTestManager, symbol symbol)
            : base(symbolTestManager, 1)
        {
            this.eaTestManager = symbolTestManager.eaTestManager;
            this.symbol = symbol;

            environment = eaTestManager.Environment;
            environmentSession = eaTestManager.Environment.Session;

            symbolContext = (ServerSymbolContext)environment.SymbolContexts[symbol];
            symbolRuntime = symbolContext.Runtime;
            symbolSession = symbolRuntime.Session;
            period = symbolSession.DataPeriod;
            testType = symbolSession.TestType;

            spread = symbolContext.Spread * symbolContext.Point;

            TimePeriodConst[] periods = EnumExtensions.GetPeriods(EnumExtensions.VISIBLE_PERIODS);
            Array.Sort(periods, comparePeriodBySecs);

            this.seriesManager = eaTestManager.seriesManager;
            this.tickSeriesCache = (IServerNormalSeriesManagerCache)eaTestManager.seriesManager[symbol, symbolSession.DataPeriod];
            this.subindex = -1;

        }

        internal static TickSymbolTestManager Create(SymbolTestManager symbolTestManager, ISymbolSession symbolSession)
        {
            switch (symbolSession.TestType)
            {
                case TestType.EVERY_TICK:
                    if (symbolSession.DataPeriod.GetCategory() == TimePeriodCategory.TICKS)
                    {
                        return new EveryTickSymbolTestManager(symbolTestManager, symbolSession.Symbol);
                    }
                    else
                    {
                        return new ControlPointsSymbolTestManager(symbolTestManager, symbolSession.Symbol);
                    }
                case TestType.CONTROL_POINTS:
                    return new ControlPointsSymbolTestManager(symbolTestManager, symbolSession.Symbol);
                case TestType.OPEN_PRICES:
                    return new OpenPricesOnlySymbolTestManager(symbolTestManager, symbolSession.Symbol);
                default:
                    throw new NotSupportedException();
            }
        }

        protected void tick()
        {
            seriesManager.Tick(symbol, bid, ask, volume);
            symbolContext.Tick(bid, ask, volume);
        }

        static int comparePeriodBySecs(TimePeriodConst p1, TimePeriodConst p2)
        {
            int secs1 = p1.GetSecs();
            int secs2 = p2.GetSecs();
            return secs1 - secs2;
        }
    }

    public class EveryTickSymbolTestManager : TickSymbolTestManager
    {
        internal EveryTickSymbolTestManager(SymbolTestManager symbolTestManager, symbol symbol)
            : base(symbolTestManager, symbol)
        {
        }

        public override datetime PrepareNext(datetime currentBest)
        {
            // NOTE
            // Running after SymbolPeriodBarTestManager-s if both this.time values are the same 
            // See  priority    TestManagerNode.PrepareNext    BaseVINode.childrenByPriority

            if (currentBest > this.time)
            {
                if (virgin)
                {
                    tickSeriesCache.Load(environmentSession.EATestingGlobalFrom);
                    virgin = false;
                }
                else
                {
                    tickSeriesCache.LoadForward(1);
                }
                this.time = tickSeriesCache.FocusedTime;
            }

            return this.time;
        }

        internal override void DoIt1()
        {
            bid = this.tickSeriesCache.Bids[0];
            if (environmentSession.UpdateSpreadTick)
            {
                ask = this.tickSeriesCache.Asks[0];
            }
            else
            {
                ask = bid + spread;
            }
            volume = this.tickSeriesCache.Volume[0];

            tick();
        }

    }

    public class ControlPointsSymbolTestManager : TickSymbolTestManager
    {
        int numControlPoints;
        Random random;

        internal ControlPointsSymbolTestManager(SymbolTestManager symbolTestManager, symbol symbol)
            : base(symbolTestManager, symbol)
        {
        }

        public override datetime PrepareNext(datetime currentBest)
        {
            // NOTE
            // Running after SymbolPeriodBarTestManager-s if both this.time values are the same 
            // See  priority    TestManagerNode.PrepareNext    BaseVINode.childrenByPriority

            if (currentBest > this.time)
            {
                subindex++;

                if (subindex == numControlPoints)
                {
                    subindex = 0;
                }

                if (subindex == 0)
                {
                    if (virgin)
                    {
                        tickSeriesCache.Load(environmentSession.EATestingGlobalFrom);
                        virgin = false;
                    }
                    else
                    {
                        tickSeriesCache.LoadForward(1);
                    }
                    time = tickSeriesCache.FocusedTime;
                    open = this.tickSeriesCache.Open[0];
                    low = this.tickSeriesCache.Low[0];
                    high = this.tickSeriesCache.High[0];
                    close = this.tickSeriesCache.Close[0];
                    volume = this.tickSeriesCache.Volume[0];
                    upwards = open <= close;

                    if (testType == TestType.EVERY_TICK)
                    {
                        numControlPoints = 6 + Math.Max(1, (int)Math.Round(Math.Pow((high - low) / symbolContext.TraditionalPip, 0.6)));
                        random = new Random(environment.RandomSeed + numControlPoints * 1000);
                    }
                    else
                    {
                        numControlPoints = 4;
                    }
                }
                else
                {
                    int secs = period.GetSecs();

                    this.time = tickSeriesCache.FocusedTime;
                    this.time += subindex == numControlPoints - 1 ? secs : secs * subindex / (numControlPoints - 1);
                    if (subindex == numControlPoints - 1)
                    {
                        this.time = this.time.AddMilliseconds(-1);
                    }
                }
            }

            return this.time;
        }

        internal override void DoIt1()
        {
            if (testType == TestType.EVERY_TICK)
            {
                switch (subindex)
                {
                    case 0:
                        bid = open; break;
                    case 1:
                        bid = upwards ? low : high; break;
                    case 2:
                        bid = upwards ? high : low; break;
                    case 3:
                        bid = close; break;
                }
            }
            else
            {
                double d1 = upwards ? open - low : high - open;
                double d2 = high - close;
                double d3 = upwards ? high - close : close - low;
                double d = d1 + d2 + d3;
                int p1 = (int)Math.Round((numControlPoints - 1) * d1 / d);
                int p2 = numControlPoints - 1 - (int)Math.Round((numControlPoints - 1) * d3 / d);
                if (p1 == 0)
                {
                    p1 = 1;
                }
                if (p1 == p2)
                {
                    p2 = p1 + 1;
                }
                if (p2 == numControlPoints - 1)
                {
                    p2 = numControlPoints - 2;
                }
                if (p1 == p2)
                {
                    p1 = p2 - 1;
                }

                if (subindex == 0)
                {
                    bid = open;
                }
                else if (subindex == p1)
                {
                    bid = upwards ? low : high;
                }
                else if (subindex == p2)
                {
                    bid = upwards ? high : low;
                }
                else if (subindex == numControlPoints - 1)
                {
                    bid = close;
                }
                else if (0 < subindex && subindex < p1)
                {
                    double r1 = open;
                    double r2 = upwards ? low : high;
                    bid = rnd(r1, r2, subindex - 1, p1);
                }
                else if (p1 < subindex && subindex < p2)
                {
                    double r1 = upwards ? low : high;
                    double r2 = upwards ? high : low;
                    bid = rnd(r1, r2, subindex - p1 - 1, p2 - p1);
                }
                else if (p2 < subindex)
                {
                    double r1 = upwards ? high : low;
                    double r2 = close;
                    bid = rnd(r1, r2, subindex - p2 - 1, numControlPoints - p2);
                }
            }
            ask = bid + spread;

            tick();
        }

        double rnd(double r1, double r2, int i, int cnt)
        {
            double d = r2 - r1;
            double linear = r1 + i * d / (cnt - 1);
            double v = linear - d / 3 + random.NextDouble() * 2 * d / 3;
            v = Math.Round(v, symbolContext.Digits);
            if (r1 < r2)
            {
                v = Math.Max(v, r1);
                v = Math.Min(v, r2);
            }
            else
            {
                v = Math.Max(v, r2);
                v = Math.Min(v, r1);
            }
            return v;
        }
    }

    public class OpenPricesOnlySymbolTestManager : TickSymbolTestManager
    {
        internal OpenPricesOnlySymbolTestManager(SymbolTestManager symbolTestManager, symbol symbol)
            : base(symbolTestManager, symbol)
        {
        }

        public override datetime PrepareNext(datetime currentBest)
        {
            // NOTE
            // Running after SymbolPeriodBarTestManager-s if both this.time values are the same 
            // See  priority    TestManagerNode.PrepareNext    BaseVINode.childrenByPriority

            if (currentBest > this.time)
            {

                if (virgin)
                {
                    tickSeriesCache.Load(environmentSession.EATestingGlobalFrom);
                    virgin = false;
                }
                else
                {
                    tickSeriesCache.LoadForward(1);
                }
                this.time = tickSeriesCache.FocusedTime;
            }

            return this.time;
        }

        internal override void DoIt1()
        {
            bid = this.tickSeriesCache.Open[0];
            ask = bid + spread;
            volume = this.tickSeriesCache.Volume[0];

            tick();
        }

    }

    public class SymbolPeriodBarTestManager : SymbolTestManagerChild
    {
        internal readonly EaTestManager eaTestManager;
        internal readonly SymbolTestManager symbolTestManager;
        internal readonly ServerEnvironmentRuntime environment;
        internal readonly IServerEnvironmentSession environmentSession;
        internal readonly IServerSymbolContext symbolContext;
        internal readonly IServerSymbolRuntime symbolRuntime;
        internal readonly ISymbolSession symbolSession;

        internal ServerStreamSeriesManagerCache seriesCache;

        internal readonly TimePeriodConst period;
        internal readonly symbol symbol;

        internal double saved_open, saved_low, saved_high, saved_close;
        internal double saved_volume;

        // NOTE  priority : 0   !!!!   see TickSymbolTestManager priority : 1
        internal SymbolPeriodBarTestManager(SymbolTestManager symbolTestManager, TimePeriodConst period)
            : base(symbolTestManager, 0)
        {
            if (period.GetSecs() < 1)
            {
                throw new NotSupportedException("No Bars supported for period:" + period);
            }
            this.symbolTestManager = symbolTestManager;
            this.eaTestManager = symbolTestManager.eaTestManager;
            this.environment = symbolTestManager.environment;
            this.environmentSession = symbolTestManager.environmentSession;
            this.symbolContext = symbolTestManager.symbolContext;
            this.symbolRuntime = symbolTestManager.symbolRuntime;
            this.symbolSession = symbolTestManager.symbolSession;

            this.period = period;
            this.symbol = symbolTestManager.symbolSession.Symbol;

            this.seriesCache = (ServerStreamSeriesManagerCache)eaTestManager.seriesManager[symbol, period];
        }

        public override datetime PrepareNext(datetime currentBest)
        {
            // NOTE
            // Running before TickSymbolTestManager if both this.time values are the same 
            // See  priority    TestManagerNode.PrepareNext    BaseVINode.childrenByPriority
            if (firstChild.time >= time)
            {
                seriesCache.Load(firstChild.time);

                if (virgin)
                {
                    time = (datetime)seriesCache.LTime[-1];
                    virgin = false;
                }
                else if (time != seriesCache.FocusedTime)
                {
                    throw new NotSupportedException("Next bar time mismatch of " + symbol + " " + period.GetShortTxt() + " : " + time + " != " + seriesCache.FocusedTime);
                }
            }
            return time;
        }

        internal override void DoIt1()
        {
            symbolContext.Bar(period, saved_open, saved_low, saved_high, saved_close, saved_volume, 1);
            seriesCache.Bar(period, saved_open, saved_low, saved_high, saved_close, saved_volume, 1);

            saved_open = seriesCache.Open[0];
            saved_low = seriesCache.Low[0];
            saved_high = seriesCache.High[0];
            saved_close = seriesCache.Close[0];
            saved_volume = seriesCache.Volume[0];

            symbolContext.Bar(period, saved_open, saved_open, saved_open, saved_open, saved_volume, 0);
            seriesCache.Bar(period, saved_open, saved_open, saved_open, saved_open, saved_volume, 0);

            time = (datetime)seriesCache.LTime[-1];
        }
    }



    public class ScriptRunner
    {
        Thread thread;
        readonly List<ScriptRunningRequest> requestQueue = new List<ScriptRunningRequest>();

        public ScriptRunner(ServerEnvironmentRuntime environment, ServerScriptRuntime script)
        {
            Script = script;
            thread = new Thread(Run);
            thread.Start();
        }

        public ServerEnvironmentRuntime Environment
        {
            get;
            private set;
        }
        public ServerScriptRuntime Script
        {
            get;
            private set;
        }


        public void AddRequest(ScriptRunningRequest request)
        {
            lock (requestQueue)
            {
                requestQueue.Add(request);
            }
        }

        public void Run()
        {
            while (true)
            {

            }
        }

    }




    public class EnvironmentQueue
    {
        private readonly SymbolQueue[] symbolQs;

        internal readonly ServerEnvironmentRuntime environment;

        internal EnvironmentQueue(ServerEnvironmentRuntime environment)
        {
            this.environment = environment;

            symbolQs = new SymbolQueue[environment.SymbolContexts.Count];

            int i = 0;
            foreach (var e in environment.SymbolContexts)
            {
                ServerSymbolContext sc = (ServerSymbolContext)e.Value;
                if (sc.Runtime != null)
                {
                    SymbolQueue sq = new SymbolQueue(environment, sc);
                    symbolQs[i] = sq;
                }

                i++;
            }
        }

        internal void OnTick(symbol symbol, double Bid, double Ask, double Volume, datetime time)
        {
            //environment.Tick(symbol, Bid, Ask, Volume);
            environment.Session.Time = time;

            SymbolQueue sq = symbolQs[symbol.index];
            sq.OnTick(Bid, Ask, Volume);
        }

        internal void OnBar(symbol symbol, TimePeriodConst Period, double Open, double Low, double High, double Close, double Volume, int offset)
        {
            //environment.Bar(symbol, Period, Open, Low, High, Close, Volume, offset);

            SymbolQueue sq = symbolQs[symbol.index];
            sq.OnBar(Period, Open, Low, High, Close, Volume, offset);
        }
    }

    public class SymbolQueue
    {
        private readonly ChartQueue[] chartQs;

        internal readonly ServerSymbolContext symbolContext;
        internal readonly IServerSymbolRuntime symbolRuntime;

        internal SymbolQueue(ServerEnvironmentRuntime environment, ServerSymbolContext symbolContext)
        {
            this.symbolContext = symbolContext;
            this.symbolRuntime = symbolContext.Runtime;

            List<ChartQueue> _chartQs = new List<ChartQueue>();

            foreach (var cg in environment.Charts)
            {
                foreach (ServerChartRuntime ch in cg.Charts)
                {
                    if (ch.Symbol == symbolContext.Symbol)
                    {
                        ChartQueue cq = new ChartQueue(environment, symbolContext, ch);
                        _chartQs.Add(cq);
                    }
                }
            }
            chartQs = _chartQs.ToArray();
        }

        internal void OnTick(double Bid, double Ask, double Volume)
        {
            symbolContext.Tick(Bid, Ask, Volume);

            foreach (var pq in chartQs)
            {
                pq.OnTick(Bid, Ask, Volume);
            }
        }

        internal void OnBar(TimePeriodConst Period, double Open, double Low, double High, double Close, double Volume, int offset)
        {
            symbolContext.Bar(Period, Open, Low, High, Close, Volume, offset);

            foreach (var pq in chartQs)
            {
                if (Period == pq.Period)
                {
                    pq.OnBar(Open, Low, High, Close, Volume, offset);
                }
            }
        }
    }

    public class ChartQueue
    {

        ServerChartRuntime chart;

        // TODO ChartRuntime -> SeriesManagerRuntime.SeriesManagerCache
        //TimeSeriesRuntime series;

        internal readonly TimePeriodConst Period;

        private readonly UserQueue[] childChartQs;

        internal ChartQueue(ServerEnvironmentRuntime environment, ServerSymbolContext symbolContext, ServerChartRuntime chart)
        {
            this.chart = chart;
            // TODO ChartRuntime -> SeriesManagerRuntime.SeriesManagerCache
            // this.series = chart.SeriesRuntime;
            Period = chart.Period;

            List<UserQueue> _childChartQs = new List<UserQueue>();

            // TODO update tree if changed
            if (chart.Script != null)
            {
                UserQueue cq = new UserQueue((ServerScriptRuntime)chart.Script);
                _childChartQs.Add(cq);
            }
            if (chart.Expert != null)
            {
                UserQueue cq = new UserQueue((ServerExpertRuntime)chart.Expert);
                _childChartQs.Add(cq);
            }

            //foreach (var ir in chart.indicators.Values)
            //{
            //    UserQueue cq = new UserQueue(ir);
            //    _childChartQs.Add(cq);
            //}
            childChartQs = _childChartQs.ToArray();
        }

        internal void OnTick(double Bid, double Ask, double Volume)
        {
            // TODO ChartRuntime -> SeriesManagerRuntime.SeriesManagerCache
            //series.Tick(Bid, Ask, Volume);
            //chart.Tick(Bid, Ask, Volume);

            // ?
            // foreach (var pq in childChartQs)
            // {
            //     pq.OnTick(Bid, Ask, Volume);
            // }
        }

        internal void OnBar(double Open, double Low, double High, double Close, double Volume, int offset)
        {
            // TODO ChartRuntime -> SeriesManagerRuntime.SeriesManagerCache
            //series.Bar(Open, Low, High, Close, Volume, offset);

            // TOOD LoadSeriesArrays
            // TODO ChartRuntime -> SeriesManagerRuntime.SeriesManagerCache
            //chart.Bar(Open, Low, High, Close, Volume, offset);

            // ?
            // foreach (var pq in childChartQs)
            // {
            //     pq.OnBar(Open, Low, High, Close, Volume, offset);
            // }
        }
    }

    public class UserQueue
    {

        IUserRuntime chart;

        internal UserQueue(IUserRuntime chart)
        {
            this.chart = chart;
        }

        internal void OnTick(double Bid, double Ask, double Volume)
        {
            // ?
        }

        internal void OnBar(double Open, double Low, double High, double Close, double Volume, int offset)
        {
            // ?
        }
    }


}
