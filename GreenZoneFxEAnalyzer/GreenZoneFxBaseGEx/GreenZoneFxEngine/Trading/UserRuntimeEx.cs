using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Types;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using GreenZoneFxEngine.Util;
using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;
using GreenZoneParser.Reflect;
using GreenZoneUtil.Util;

namespace GreenZoneFxEngine.Trading
{
    public class UserRuntimeEx : UserRuntimeBase
    {
        public UserRuntimeEx(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
            : base(rmiManager)
        {
            Environment = parent.Environment;
            Parent = parent;

            if (cache != null)
            {
                SeriesManager = (ISeriesManagerRuntime)Cache.Parent;
            }
            Cache = cache;

            TmpArrayCaches = new Dictionary<DArr, IArraySeriesManagerCache>();
        }

        public UserRuntimeEx(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
            : this(rmiManager, parent, icache)
        {
            foreach (var e in session.Parameters)
            {
                GreenZoneUtilsBase.SetProperty(rmiManager.Resolver, this, e.Key, e.Value);
            }
        }

        public UserRuntimeEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }


        protected UserRuntimeEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        public override object[] GenerateParamArray()
        {
            List<ReflProperty> fields = GreenZoneUtilsBase.GetTopLevelProperties(rmiManager.Resolver, this);
            List<object> list = new List<object>();
            foreach (var f in fields)
            {
                list.Add(f.GetValue(this));
            }
            object[] result = list.ToArray();

            return result;
        }

        public override void CopyParamsTo(IExecRuntime other)
        {
            GreenZoneUtilsBase.CopyTopLevelProperties(rmiManager.Resolver, this, other);
            other.CopyTopLevelParamsToSession();
        }

        public override void CopyTopLevelParamsToSession()
        {
            List<ReflProperty> fields = GreenZoneUtilsBase.GetTopLevelProperties(rmiManager.Resolver, this);
            Session.SetParameters(this, fields);
        }

        public override void LoadFromSet(string file)
        {
            List<ReflProperty> fields = GreenZoneUtilsBase.GetTopLevelProperties(rmiManager.Resolver, this, true);
            string[] fcontents = File.ReadAllLines(file);
            Array.Sort(fcontents);

            int i1 = 0, i2 = 0;
            while (i1 < fields.Count && i2 < fcontents.Length)
            {
                ReflProperty f1 = fields[i1];
                string n1 = f1.Name;
                string n2 = fcontents[i2];
                string[] contline = n2.Split('=');
                string value = null;
                // TODO optimization parameters
                if (contline.Length == 2 && contline[0].IndexOf(',') == -1)
                {
                    n2 = contline[0];
                    value = contline[1];
                }

                switch (Math.Sign(n1.CompareTo(n2)))
                {
                    case -1:
                        i1++;
                        break;
                    case 1:
                        i2++;
                        break;
                    case 0:
                        object objval = GzEngineFormatter.ToObject(f1.PropertyType.NativeType, value);
                        f1.SetValue(this, objval);
                        i1++;
                        i2++;
                        break;
                }
            }
        }

        public override void SaveToSet(string file)
        {
            List<ReflProperty> fields = GreenZoneUtilsBase.GetTopLevelProperties(rmiManager.Resolver, this, true);

            StringBuilder b = new StringBuilder();
            foreach (ReflProperty pi in fields)
            {
                b.Append(pi.Name);
                b.Append('=');
                string strval = GzEngineFormatter.ToString(pi.PropertyType.NativeType, pi.GetValue(this));
                b.Append(strval);
                b.Append('\n');
            }

            File.WriteAllText(file, b.ToString());
        }

        public override IExecRuntime Copy()
        {
            IExecRuntime newInstance = Create(rmiManager, (IChartRuntime)Parent, ExecutableInfo, Cache);
            CopyParamsTo(newInstance);
            return newInstance;
        }

        public static IExecRuntime Create(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo executableInfo, ISeriesManagerCache icache)
        {
            ReflObjType systemType = executableInfo.SystemType;
            Resolver r = rmiManager.Resolver;

            ReflConstructor c = systemType.GetConstructor(new ReflType[] { r.GetType(typeof(GreenRmiManager)), r.GetType(parent.GetType()), r.GetType(typeof(Mt4ExecutableInfo)), r.GetType(typeof(ISeriesManagerCache)) });
            IExecRuntime executableInstance = (IExecRuntime)c.CreateObject(new object[] { rmiManager, parent, executableInfo, icache });

            return executableInstance;
        }

        public static IExecRuntime Create(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
        {
            Mt4ExecutableInfo executableInfo = session.ExecutableInfo;
            ReflObjType systemType = executableInfo.SystemType;
            Resolver r = rmiManager.Resolver;

            ReflConstructor c = systemType.GetConstructor(new ReflType[] { r.GetType(typeof(GreenRmiManager)), r.GetType(parent.GetType()), r.GetType(session.GetType()), r.GetType(typeof(ISeriesManagerCache)) });
            IExecRuntime executableInstance = (IExecRuntime)c.CreateObject(new object[] { rmiManager, parent, session, icache });
            return executableInstance;
        }

        protected DArr GetSeriesArray(PriceConstant appliedPrice)
        {
            DArr seriesArray;
            switch (appliedPrice)
            {
                case PriceConstant.PRICE_OPEN: seriesArray = (DArr)Cache.GetArray(SeriesArrayPool.MODE_OPEN); break;
                case PriceConstant.PRICE_LOW: seriesArray = (DArr)Cache.GetArray(SeriesArrayPool.MODE_LOW); break;
                case PriceConstant.PRICE_HIGH: seriesArray = (DArr)Cache.GetArray(SeriesArrayPool.MODE_HIGH); break;
                case PriceConstant.PRICE_CLOSE: seriesArray = (DArr)Cache.GetArray(SeriesArrayPool.MODE_CLOSE); break;
                default:
                    Mt4ExecutableInfo info_Prices = Environment.GetIndicatorInfo(typeof(Prices).FullName);
                    object[] args_Prices = { appliedPrice };
                    Prices ir_Prices = (Prices)Cache.Get(info_Prices, args_Prices, true);
                    seriesArray = (DArr)ir_Prices.Buffers[0].Buffer;
                    break;
            }
            return seriesArray;
        }
    }
}
