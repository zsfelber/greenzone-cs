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
using GreenZoneUtil.Util;
using System.Reflection;
using System.Drawing;
using GreenZoneParser.Reflect;

namespace GreenZoneFxEngine.Trading
{

    public class ClientChartGroupRuntimeEx : ClientChartGroupRuntimeBase
    {

        public ClientChartGroupRuntimeEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected ClientChartGroupRuntimeEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override int CursorPosition
        {
            get
            {
                return Session.CursorPosition;
            }
            set
            {
                Session.CursorPosition = value;
                foreach (IClientChartRuntime ch in Charts)
                {
                    if (ch.Session.IsCursorBarConnected)
                    {
                        ch.CursorPosition = value;
                    }
                }
            }
        }

    }


    public class ClientChartRuntimeEx : ClientChartRuntimeBase
    {

        public ClientChartRuntimeEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected ClientChartRuntimeEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override SeriesRange SeriesRange
        {
            get
            {
                return Session.SeriesRange;
            }
            set
            {
                GuiSeriesManager.DefaultCache.fixSeriesRange(ref value);
                Session.SeriesRange = value;
            }
        }

        public override datetime ScrolledBarTime
        {
            get
            {
                return Session.ScrolledBarTime;
            }
            set
            {
                GuiSeriesManager.FocusedTime = value;
                CursorRuntime.SeriesManager.FocusedTime = value;
                CursorRuntime.UpdateSeriesRange();

                Session.ScrolledBarTime = value;
                // NOTE fixSeriesRange is invoked
                SeriesRange = SeriesRange;
            }
        }

        public override datetime ParentScrolledBarTime
        {
            get
            {
                return Group.ScrolledBarTime;
            }
            set
            {
                Group.ScrolledBarTime = value;
                if (IsCursorBarConnected)
                {
                    Group.CursorPosition = Group.CursorPosition;
                }
                else
                {
                    CursorPosition = CursorPosition;
                }
            }
        }

        public override int ParentCursorPosition
        {
            get
            {
                return Group.Session.CursorPosition;
            }
            set
            {
                int offs0 = SeriesRange.OffsetFrom;
                //group.CursorPosition = value;
                //group.ScrolledBarTime = (datetime)sLTime[offs0 - SeriesRange.OffsetFrom];
                SeriesRange r = SeriesRange;
                r.CursorPosition = value;
                Group.ScrolledBarTime = (datetime)sLTime[offs0 - r.OffsetFrom];
                Group.CursorPosition = value;
            }
        }

        public override int CursorPosition
        {
            get
            {
                return SeriesRange.CursorPosition;
            }
            set
            {
                SeriesRange r = SeriesRange;
                r.CursorPosition = value;
                SeriesRange = r;
            }
        }

        public override void LoadForward(int offset)
        {
            GuiSeriesManager.DefaultCache.LoadForward(offset);
            ScrolledBarTime = GuiSeriesManager.FocusedTime;
        }

        public override void LoadAtTotal(long total_ind)
        {
            GuiSeriesManager.DefaultCache.LoadAtTotal(total_ind);
            ScrolledBarTime = GuiSeriesManager.FocusedTime;
        }
    }


    public class ClientChartCursorRuntimeEx : ClientChartCursorRuntimeBase
    {

        public ClientChartCursorRuntimeEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            IChartCursorRuntime_SeriesManager_Changed += new GreenZoneUtil.ViewController.PropertyChangedEventHandler(ChartCursorRuntimeEx_SeriesManagerChanged);
        }

        protected ClientChartCursorRuntimeEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        void ChartCursorRuntimeEx_SeriesManagerChanged(object sender, GreenZoneUtil.ViewController.PropertyChangedEventArgs args)
        {
            UpdateSeriesRange();
        }

        public override void LoadForward(int offset)
        {
            SeriesCache.LoadForward(offset);
            ScrolledBarTime = SeriesManager.FocusedTime;
        }

        public override void LoadAtTotal(long total_ind)
        {
            SeriesCache.LoadAtTotal(total_ind);
            ScrolledBarTime = SeriesManager.FocusedTime;
        }



    }


}
