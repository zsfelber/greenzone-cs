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

    public abstract class ServerExecRuntimeEx : ServerExecRuntimeBase
    {
        public ServerExecRuntimeEx(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
            : base(rmiManager, parent, cache)
        {
        }
        
        public ServerExecRuntimeEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
        
        protected ServerExecRuntimeEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ServerExecRuntimeEx(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

    }

    public abstract class ServerIndicatorRuntimeEx : ServerIndicatorRuntimeBase
    {
        public delegate void DInstanceChanged(IIndicatorRuntime newInstance);
        public event DInstanceChanged InstanceChanged;

        public ServerIndicatorRuntimeEx(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
            : base(rmiManager, parent, cache)
        {
        }

        public ServerIndicatorRuntimeEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected ServerIndicatorRuntimeEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public ServerIndicatorRuntimeEx(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int IndicatorCounted
        {
            get
            {
                if (IndicatorLastOffset == 0)
                {
                    return Cache.Bars - 1;
                }
                else
                {
                    return Cache.Bars - IndicatorLastOffset;
                }
            }
        }

        public override IndicatorId Id
        {
            get
            {
                string argKey = GreenZoneSysUtilsBase.GenerateArgKey(GenerateParamArray());
                IndicatorId id = new IndicatorId(IndicatorInfo, argKey);
                return id;
            }
        }

        public override int NumIndicatorBuffers
        {
            get
            {
                return Buffers == null ? 0 : Buffers.Length;
            }
            set
            {
                AddBuffers(value, Buffers);
                Session.NumBuffers = value;
            }
        }

        public override int NumIndicatorLevels
        {
            get
            {
                return Levels == null ? 0 : Levels.Length;
            }
            set
            {
                AddLevels(value, Levels);
                Session.NumLevels = value;
            }
        }

        protected void AddBuffers(int buffersCnt, params IndicatorBuffer[] bufferData)
        {
            Buffers = new IndicatorBuffer[buffersCnt];
            int i = 0;

            if (bufferData != null) foreach (var b in bufferData)
                {
                    if (i >= buffersCnt)
                    {
                        break;
                    }
                    Buffers[i] = b;
                    i++;
                }
            for (; i < buffersCnt; i++)
            {
                Buffers[i] = new IndicatorBuffer(i);
            }
        }

        void AddLevels(int levelsCnt, params IndicatorLevel[] levelData)
        {
            Levels = new IndicatorLevel[levelsCnt];
            int i = 0;
            if (levelData != null) foreach (var b in levelData)
                {
                    if (i >= levelsCnt)
                    {
                        break;
                    }
                    Levels[i] = b;
                    i++;
                }
            for (; i < levelsCnt; i++)
            {
                Levels[i] = new IndicatorLevel(i);
            }
        }

        public override void CopyParamsTo(IIndicatorRuntime ind2)
        {
            base.CopyParamsTo((IExecRuntime)ind2);

            ind2.NumIndicatorBuffers = NumIndicatorBuffers;
            ind2.NumIndicatorLevels = NumIndicatorLevels;

            for (int i = 0; i < NumIndicatorBuffers; i++)
            {
                ind2.Buffers[i].Params = Buffers[i].Params;
                ind2.Session.Buffers[i] = Buffers[i].Params;
            }
            for (int i = 0; i < NumIndicatorLevels; i++)
            {
                ind2.Levels[i].Params = Levels[i].Params;
                ind2.Session.Levels[i] = Levels[i].Params;
            }
        }

        public override void IndicatorBuffers(int count)
        {
            NumIndicatorBuffers = count;
        }
        public override void IndicatorShortName(string shortName)
        {
            Session.ShortName = shortName;
        }
        public override void IndicatorDigits(int digits)
        {
            Session.IndicatorDigits = digits;
        }

        public override void SetIndicatorBuffer(int index, IndicatorBuffer buffer)
        {
            Buffers[index] = buffer;
        }

        public override bool SetIndexBuffer(int index, ref DArr array)
        {
            if (array == null)
            {
                array = new DArr();
            }
            Buffers[index].Buffer = array;
            return true;
        }

        public override void SetIndexDrawBegin(int index, int begin)
        {
            Buffers[index].DrawBegin = begin;
        }

        public override void SetIndexShift(int index, int shift)
        {
            Buffers[index].Shift = shift;
        }
        public override void SetIndexStyle(int index, DrawingStyle style)
        {
            Buffers[index].StyleType = style;
        }
        public override void SetIndexStyleWidth(int index, DrawingWidth width)
        {
            Buffers[index].StyleWidth = width;
        }
        public override void SetIndexLabel(int index, string label)
        {
            Buffers[index].Label = label;
        }
        public override void SetIndexArrow(int index, IWingdingsChar arrow)
        {
            Buffers[index].Arrow = (WingdingsChar)arrow;
        }
        public override void SetIndexColor(int index, Color color)
        {
            Buffers[index].StyleColor = color;
        }

        public override void SetIndexEmptyValue(int index, double value)
        {
            Buffers[index].EmptyValue = value;
        }

        public override void SetIndexStyle(int index, DrawingStyle type, DrawingStylesWidth1 style = DrawingStylesWidth1.STYLE_SOLID, DrawingWidth width = DrawingWidth.WIDTH_1, Color clr = default(Color))
        {
            IndicatorBuffer b = Buffers[index];
            b.SetIndexStyle(type, style, width, clr);
        }


        public override void SetLevelStyle(DrawingStylesWidth1 draw_style, DrawingWidth line_width, Color clr = default(Color), int levelInd = -1, double value = 0)
        {
            if (clr == default(Color))
            {
                clr = CLR_NONE;
            }
            if (levelInd == -1)
            {
                GetLevel(0);
                foreach (IndicatorLevel l in Levels)
                {
                    l.SetLevelStyle(draw_style, line_width, clr, value);
                }
            }
            else
            {
                IndicatorLevel l = GetLevel(levelInd);
                l.SetLevelStyle(draw_style, line_width, clr, value);
            }
        }

        public override void SetLevelColor(Color clr, int levelInd = -1)
        {
            if (levelInd == -1)
            {
                GetLevel(0);
                foreach (IndicatorLevel l in Levels)
                {
                    l.StyleColor = clr;
                }
            }
            else
            {
                IndicatorLevel l = GetLevel(levelInd);
                l.StyleColor = clr;
            }
        }

        public override void SetLevelValue(int level, double value)
        {
            IndicatorLevel l = GetLevel(level);
            l.Value = value;
        }

        public override void RaiseInstanceChanged(IIndicatorRuntime newInstance)
        {
            if (InstanceChanged != null)
            {
                InstanceChanged(newInstance);
            }
        }
    }




}
