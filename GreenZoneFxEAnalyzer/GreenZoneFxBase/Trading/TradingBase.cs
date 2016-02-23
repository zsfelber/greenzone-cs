using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using GreenZoneUtil.Util;
using System.Drawing;
using GreenZoneFxEngine.Util;

namespace GreenZoneFxEngine.Trading
{
    [Serializable]
    public struct SeriesRange
    {
        public SeriesRange(int offsetFrom, int offsetTo, double priceFrom, double priceTo)
            : this()
        {
            OffsetFrom = offsetFrom;
            OffsetTo = offsetTo;
            PriceFrom = priceFrom;
            PriceTo = priceTo;
        }

        public void Zoom(double d, int min, int max)
        {
            int of = (int)Math.Round(OffsetFrom * d);
            int ot = (int)Math.Round(OffsetTo * d);
            int n = ot - of + 1;
            if (min <= n && n <= max)
            {
                OffsetFrom = of;
                OffsetTo = ot;
            }
        }

        public void ChangeOffsetFrom(int newOffsetFrom)
        {
            int delta = newOffsetFrom - OffsetFrom;
            OffsetFrom += delta;
            OffsetTo += delta;
        }

        public void ChangeOffsetTo(int newOffsetTo)
        {
            int delta = newOffsetTo - OffsetTo;
            OffsetFrom += delta;
            OffsetTo += delta;
        }

        public int OffsetFrom
        {
            get;
            set;
        }
        public int OffsetTo
        {
            get;
            set;
        }
        public int NumBars
        {
            get
            {
                return OffsetTo - OffsetFrom + 1;
            }
        }
        public int CursorPosition
        {
            get
            {
                double d = 1000.0 / NumBars;
                int cp = (int)Math.Round(1000.0 + d * OffsetFrom);
                return cp;
            }
            set
            {
                int n = NumBars;
                double d = 1000.0 / NumBars;
                OffsetFrom = -(int)Math.Round((1000.0 - value) / d);
                OffsetTo = OffsetFrom + n - 1;
            }
        }

        public double PriceFrom
        {
            get;
            set;
        }
        public double PriceTo
        {
            get;
            set;
        }
        public double PriceRange
        {
            get
            {
                return PriceTo - PriceFrom;
            }
        }

        public override bool Equals(object o)
        {
            return this == (SeriesRange)o;
        }

        public override int GetHashCode()
        {
            return OffsetFrom + OffsetTo;
        }

        public static bool operator ==(SeriesRange a, SeriesRange b)
        {
            return a.OffsetFrom == b.OffsetFrom && a.OffsetTo == b.OffsetTo &&
                    a.PriceFrom == b.PriceFrom && a.PriceTo == b.PriceTo;
        }

        public static bool operator !=(SeriesRange a, SeriesRange b)
        {
            return a.OffsetFrom != b.OffsetFrom || a.OffsetTo != b.OffsetTo ||
                    a.PriceFrom != b.PriceFrom || a.PriceTo != b.PriceTo;
        }
    }




    public abstract class IndicatorEntry : TradingConstBase, IParams
    {
        public abstract Dictionary<string, object> Params
        {
            get;
            set;
        }

        public abstract int Index { get; }
        public abstract DrawingStyle StyleType { get; set; }
        public abstract DrawingStylesWidth1 StyleCode { get; set; }
        public abstract DrawingWidth StyleWidth { get; set; }
        public abstract Color StyleColor { get; set; }
    }

    public class IndicatorBuffer : IndicatorEntry
    {
        internal int index;
        internal DArr buffer;
        internal DArr sBuffer;
        internal WingdingsChar arrow;
        internal int drawBegin = 0;
        internal double emptyValue = EMPTY_VALUE;
        internal string label;
        internal int shift = 0;
        internal DrawingStyle styleType = DrawingStyle.DRAW_NONE;
        internal DrawingStylesWidth1 styleCode;
        internal DrawingWidth styleWidth = DrawingWidth.WIDTH_1;
        internal Color styleColor;

        public IndicatorBuffer(int index)
        {
            this.index = index;
        }

        public IndicatorBuffer(int index, IndicatorBuffer b)
            : this(index,
                    b.buffer,
                    b.arrow,
                    b.drawBegin,
                    b.emptyValue,
                    b.label,
                    b.shift,
                    b.styleType,
                    b.styleCode,
                    b.styleWidth,
                    b.styleColor)
        {
        }

        public IndicatorBuffer(int index,
                                DArr buffer,
                                WingdingsChar arrow,
                                int drawBegin,
                                double emptyValue,
                                string label,
                                int shift,
                                DrawingStyle styleType,
                                DrawingStylesWidth1 styleCode,
                                DrawingWidth styleWidth,
                                Color styleColor
                              )
        {
            this.index = index;
            this.buffer = buffer;
            this.arrow = arrow;
            this.drawBegin = drawBegin;
            this.emptyValue = emptyValue;
            this.label = label;
            this.shift = shift;
            this.styleType = styleType;
            this.styleCode = styleCode;
            this.styleWidth = styleWidth;
            this.styleColor = styleColor;
        }
        public IndicatorBuffer(int index,
                                DArr buffer,
                                DrawingStyle styleType,
                                DrawingWidth styleWidth,
                                Color styleColor
                              )
        {
            this.index = index;
            this.buffer = buffer;
            this.styleType = styleType;
            this.styleWidth = styleWidth;
            this.styleColor = styleColor;
        }

        public IndicatorBuffer(int index, Color styleColor)
        {
            this.index = index;
            this.styleColor = styleColor;
        }

        public override Dictionary<string, object> Params
        {
            get
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["Arrow"] = this.arrow;
                parameters["DrawBegin"] = this.drawBegin;
                parameters["EmptyValue"] = this.emptyValue;
                parameters["Label"] = this.label;
                parameters["Shift"] = this.shift;
                parameters["StyleType"] = this.styleType;
                parameters["StyleCode"] = this.styleCode;
                parameters["StyleWidth"] = this.styleWidth;
                parameters["StyleColor"] = this.styleColor;
                return parameters;
            }
            set
            {
                Dictionary<string, object> parameters = value;
                this.arrow = GreenZoneSysUtilsBase.to<WingdingsChar>(parameters["Arrow"]);
                this.drawBegin = GreenZoneSysUtilsBase.toi(parameters["DrawBegin"]);
                this.emptyValue = GreenZoneSysUtilsBase.tod(parameters["EmptyValue"]);
                this.label = (string)parameters["Label"];
                this.shift = GreenZoneSysUtilsBase.toi(parameters["Shift"]);
                this.styleType = (DrawingStyle)GreenZoneSysUtilsBase.toi(parameters["StyleType"]);
                this.styleCode = (DrawingStylesWidth1)GreenZoneSysUtilsBase.toi(parameters["StyleCode"]);
                this.styleWidth = (DrawingWidth)GreenZoneSysUtilsBase.toi(parameters["StyleWidth"], 1);
                this.styleColor = GreenZoneSysUtilsBase.to<Color>(parameters["StyleColor"]);
            }
        }

        public override int Index { get { return index; } }

        public DArr Buffer { get { return buffer; } set { this.buffer = value; } }
        public DArr SBuffer { get { return sBuffer; } set { this.sBuffer = value; } }
        [Description("Sets an arrow symbol for indicators line of the DRAW_ARROW type.  Symbol code from Wingdings font.")]
        public WingdingsChar Arrow { get { return arrow; } set { this.arrow = value; } }
        [Description("Sets the bar number (from the data beginning) from which the drawing of the given indicator line must start. The indicators are drawn from left to right. The indicator array values that are to the left of the given bar will not be shown in the chart or in the DataWindow. 0 will be set as default, and all data will be drawn.")]
        public int DrawBegin { get { return drawBegin; } set { this.drawBegin = value; } }
        [Description("Sets drawing line empty value. Empty values are not drawn or shown in the DataWindow. By default, empty value is EMPTY_VALUE")]
        public double EmptyValue { get { return emptyValue; } set { this.emptyValue = value; } }
        [Description("Sets drawing line description for showing in the DataWindow and in the tooltip. ")]
        public string Label { get { return label; } set { this.label = value; } }
        [Description("Sets offset for the drawing line. For positive values, the line drawing will be shifted to the right, otherwise it will be shifted to the left. I.e., the value calculated on the current bar will be drawn shifted relatively to the current bar. ")]
        public int Shift { get { return shift; } set { this.shift = value; } }
        [Description("Shape style. Can be one of Drawing shape styles listed.")]
        public override DrawingStyle StyleType { get { return styleType; } set { this.styleType=value; } }
        [Description("Drawing style. It is used for lines. It can be one of the Drawing shape styles listed. ")]
        public override DrawingStylesWidth1 StyleCode { get { return styleCode; } set { this.styleCode = value; } }
        [Description("Line width. Valid values are: 1,2,3,4,5.")]
        public override DrawingWidth StyleWidth { get { return styleWidth; } set { this.styleWidth = value; } }
        [Description("Line color.")]
        public override Color StyleColor { get { return styleColor; } set { this.styleColor = value; } }

        public void SetIndexStyle(DrawingStyle type, DrawingStylesWidth1 style = DrawingStylesWidth1.STYLE_SOLID, DrawingWidth width = DrawingWidth.WIDTH_1, Color clr = default(Color))
        {
            this.styleType = type;
            this.styleCode = style;
            this.styleWidth = width;
            this.styleColor = clr;
        }
    }


    public class IndicatorLevel : IndicatorEntry
    {
        internal int index;
        internal double value;
        internal DrawingStylesWidth1 styleCode = DrawingStylesWidth1.STYLE_SOLID;
        internal DrawingWidth styleWidth = DrawingWidth.WIDTH_1;
        internal Color styleColor = Color.Gray;

        public IndicatorLevel(int index)
        {
            this.index = index;
        }

        public IndicatorLevel(int index, IndicatorLevel b)
            : this(index,
                    b.value,
                    b.styleCode,
                    b.styleWidth,
                    b.styleColor)
        {
        }

        public IndicatorLevel(
                    int index,
                    double value,
                    DrawingStylesWidth1 styleCode,
                    DrawingWidth styleWidth,
                    Color styleColor
                              )
        {
            this.index = index;
            this.value = value;
            this.styleCode = styleCode;
            this.styleWidth = styleWidth;
            this.styleColor = styleColor;
        }

        public override Dictionary<string, object> Params
        {
            get
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["Value"] = this.value;
                parameters["StyleCode"] = this.styleCode;
                parameters["StyleWidth"] = this.styleWidth;
                parameters["StyleColor"] = this.styleColor;
                return parameters;
            }
            set
            {
                Dictionary<string, object> parameters = value;
                this.value = (double)GreenZoneSysUtilsBase.tod(parameters["Value"]);
                this.styleCode = (DrawingStylesWidth1)GreenZoneSysUtilsBase.toi(parameters["StyleCode"]);
                this.styleWidth = (DrawingWidth)GreenZoneSysUtilsBase.toi(parameters["StyleWidth"], 1);
                this.styleColor = GreenZoneSysUtilsBase.to<Color>(parameters["StyleColor"]);
            }
        }

        public override int Index { get { return index; } }

        public override DrawingStyle StyleType { get { return DrawingStyle.DRAW_LINE; } set { throw new NotSupportedException(); } }

        [Description("A value for a given horizontal level of the indicator to be output in a separate window.")]
        public double Value { get { return value; } set { this.value = value; } }
        [Description("Drawing style. Can be one of the Drawing shape styles listed. EMPTY value means that the style will not be changed.")]
        public override DrawingStylesWidth1 StyleCode { get { return styleCode; } set { this.styleCode = value; } }
        [Description("Line width. Valid values are 1,2,3,4,5. EMPTY value indicates that the width will not be changed.")]
        public override DrawingWidth StyleWidth { get { return styleWidth; } set { this.styleWidth = value; } }
        [Description("Line color.")]
        public override Color StyleColor { get { return styleColor; } set { this.styleColor = value; } }

        public void SetLevelStyle(DrawingStylesWidth1 draw_style, DrawingWidth line_width, Color clr = default(Color), double value = 0)
        {
            this.styleCode = draw_style;
            this.styleWidth = line_width;
            this.styleColor = clr;
            this.value = value;
        }
    }
}
