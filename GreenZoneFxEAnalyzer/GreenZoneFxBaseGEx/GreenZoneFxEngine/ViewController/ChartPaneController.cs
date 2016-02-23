using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;

using System.Windows.Forms.VisualStyles;
using System.Drawing;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public abstract class ChartPaneControllerEx : ChartPaneControllerBase
    {
        public ChartPaneControllerEx(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public ChartPaneControllerEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected ChartPaneControllerEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [System.ComponentModel.Category("ChartController pane")]
        public override int ChartMinimumX
        {
            get
            {
                return ChartLeftGap + ChartAutoGap;
            }
        }

        [System.ComponentModel.Category("ChartController pane")]
        public override int ChartMaximumX
        {
            get
            {
                if (Parent == null)
                {
                    return 100;
                }
                else
                {
                    return Width - ChartRightGap - ChartAutoGap - 1;
                }
            }
        }

        [System.ComponentModel.Category("ChartController pane")]
        public override int ChartEffectiveWidth
        {
            get
            {
                return ChartMaximumX - ChartMinimumX + 1;
            }
        }


        [System.ComponentModel.Category("Slider bar")]
        public override int SliderPosition
        {
            get
            {
                return ThumbRectangle.X + SliderDefaultAutoGap;
            }
            set
            {
                if (value < ChartMinimumX || value > ChartMaximumX)
                {
                    throw new ArgumentOutOfRangeException("value: " + value + "  of  " + ChartMinimumX + ".." + ChartMaximumX);
                }
                int x = value - SliderDefaultAutoGap;
                if (x != ThumbRectangle.X)
                {
                    Rectangle r = ThumbRectangle;
                    r.X = x;
                    ThumbRectangle = r;
                    CalcValue();
                    LayOut();
                    Update();
                }
            }
        }

        [System.ComponentModel.Category("Slider bar")]
        public override int CpBarPosition
        {
            get
            {
                double x = cursorToPosition(CpBarValue);
                return (int)x;
            }
        }

        [System.ComponentModel.Category("ChartController pane")]
        public override int ChartAutoGap
        {
            get
            {
                if (ChartCalcAutoGap)
                {
                    return ThumbRectangle.Width / 2;
                }
                else
                {
                    return 0;
                }
            }
        }

        [System.ComponentModel.Category("Slider bar")]
        public override int SliderDefaultAutoGap
        {
            get
            {
                return ThumbRectangle.Width / 2;
            }
        }

        public override Size ThumbRectangleSize
        {
            get
            {
                return ThumbRectangle.Size;
            }
            set
            {
                Rectangle r = ThumbRectangle;
                r.Size = value;
                r.Height = (int)(r.Height * 0.8);
                r.Width = (int)(r.Width * 0.8);
                ThumbRectangle = r;
                ThumbInitiliazed = true;
            }
        }

        public double cursorToPosition(int cursorValue)
        {
            double x = (double)ChartMinimumX + ((double)ChartEffectiveWidth) * ((double)cursorValue - SliderMinimum) / ((double)SliderMaximum - SliderMinimum) - (double)SliderDefaultAutoGap;
            return x;
        }

        public double cursorToValue(int cursorPosition)
        {
            double x = (double)SliderMinimum + ((double)SliderMaximum - SliderMinimum) * ((double)cursorPosition - ChartMinimumX) / ((double)ChartEffectiveWidth);
            return x;
        }


        protected void CalcRect()
        {
            double x = cursorToPosition(SliderValue);
            Rectangle r = ThumbRectangle;
            r.X = (int)x;
            ThumbRectangle = r;
        }

        void CalcValue()
        {
            double x = cursorToValue(SliderPosition);
            SliderValue = (int)x;
        }

        protected abstract void LayOut();

    }
}
