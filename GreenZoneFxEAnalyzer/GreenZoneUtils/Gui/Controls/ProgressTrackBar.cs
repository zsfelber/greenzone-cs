using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using GreenZoneUtil.Properties;
using System.Drawing.Drawing2D;

namespace GreenZoneUtil.Gui
{
	// TODO similar, an even better one : mono icon+title with effect+errors/warnings icon,+progress bar
    [DefaultProperty("MaxValue")]
    [DefaultEvent("TickPositionChanged")]
    public class ProgressTrackBar : UserControl
    {
        [Category("Trackbar events")]
        public event PropertyChangedEventHandler TickPositionChanged;
        [Category("Trackbar events")]
        public event PropertyChangedEventHandler ProgressValueChanged;
        [Category("Trackbar events")]
        public event PropertyChangedEventHandler MaxValueChanged, MaxProgressChanged, SegmentSizeChanged, ProgressBarHeightChanged, DrawTicksChanged, DrawSegmentsChanged;

        private int maxValue = 25;
        private int currentTickPosition = 0;
        private int maxProgress = 100;
        private int currentProgressValue = 0;
        private int segmentSize = 10;
        private int progressBarHeight = 0;
        private bool drawTicks = true;
        private bool drawSegments = false;

        private Rectangle trackRectangle = new Rectangle();
        private Rectangle progressTrackRectangle = new Rectangle();
        private Rectangle ticksRectangle = new Rectangle();
        private Rectangle bigTicksRectangle = new Rectangle();
        private Rectangle thumbRectangle = new Rectangle();
        private int progressBarHeightForUse = 0;
        private int maxValueBigTicks = 0;
        private bool thumbClicked = false;
        private TrackBarThumbState thumbState = TrackBarThumbState.Normal;
        private Pen pen;
        private Brush brush;

        public ProgressTrackBar()
        {
            this.Location = new Point(0, 0);
            this.BackColor = SystemColors.Control;
            this.DoubleBuffered = true;
            TabStop = true;

            SizeChanged += new EventHandler(ProgressTrackBar_SizeChanged);
            ForeColor = Color.Black;
            BrushColor = Color.Gray;
        }

        void ProgressTrackBar_SizeChanged(object sender, EventArgs e)
        {
            // Calculate the initial sizes of the bar, 
            // thumb and ticks.
            SetupTrackBar();
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                pen = new Pen(ForeColor);
            }
        }

        Color brushColor;
        public Color BrushColor
        {
            get
            {
                return brushColor;
            }
            set
            {
                brushColor = value;
                brush = new SolidBrush(brushColor);
                //brush = new HatchBrush(HatchStyle.Weave, brushColor, BackColor);
            }
        }

        [Category("Trackbar properties")]
        public int MaxValue
        {
            get
            {
                return maxValue;
            }
            set
            {
                if (value < 1)
                {
                    throw new NotSupportedException("MaxValue < 1");
                }
                maxValue = value;
                currentTickPosition = Math.Min(currentTickPosition, maxValue);
                segmentSize = Math.Min(segmentSize, maxValue);
                SegmentSize = segmentSize;
                if (MaxValueChanged != null)
                    MaxValueChanged(this, new PropertyChangedEventArgs("MaxValue"));
            }
        }

        [Category("Trackbar properties")]
        public int TickPosition
        {
            get
            {
                return currentTickPosition;
            }
            set
            {
                if (value < 0 || value > maxValue)
                {
                    throw new NotSupportedException("TickPosition < 0 || TickPosition > MaxValue");
                }
                currentTickPosition = value;
                SetupTrackBar();
                if (TickPositionChanged != null)
                    TickPositionChanged(this, new PropertyChangedEventArgs("TickPosition"));
            }
        }

        [Category("Trackbar properties")]
        public int MaxProgress
        {
            get
            {
                return maxProgress;
            }
            set
            {
                if (value < 1)
                {
                    throw new NotSupportedException("MaxProgress < 1");
                }
                maxProgress = value;
                currentProgressValue = Math.Min(currentProgressValue, maxProgress);
                SetupTrackBar();
                if (MaxProgressChanged != null)
                    MaxProgressChanged(this, new PropertyChangedEventArgs("MaxProgress"));
            }
        }

        [Category("Trackbar properties")]
        public int ProgressValue
        {
            get
            {
                return currentProgressValue;
            }
            set
            {
                if (value < 0 || value > maxProgress)
                {
                    throw new NotSupportedException("ProgressValue < 0 || ProgressValue > MaxProgress");
                }
                currentProgressValue = value;
                SetupTrackBar();
                if (ProgressValueChanged != null)
                    ProgressValueChanged(this, new PropertyChangedEventArgs("ProgressValue"));
            }
        }

        [Category("Trackbar properties")]
        public int SegmentSize
        {
            get
            {
                return segmentSize;
            }
            set
            {
                if (value <= 0 || value > maxValue || (maxValue % value) != 0)
                {
                    throw new NotSupportedException("SegmentSize <= 0 || SegmentSize > MaxValue || (MaxValue % SegmentSize) != 0");
                }
                segmentSize = value;
                maxValueBigTicks = maxValue / segmentSize;
                SetupTrackBar();
                if (SegmentSizeChanged != null)
                    SegmentSizeChanged(this, new PropertyChangedEventArgs("SegmentSize"));
            }
        }

        [Category("Trackbar properties")]
        public int ProgressBarHeight
        {
            get
            {
                return progressBarHeight;
            }
            set
            {
                if (value < 0)
                {
                    throw new NotSupportedException("ProgressBarHeight < 0");
                }
                progressBarHeight = value;
                SetupTrackBar();
                if (ProgressBarHeightChanged != null)
                    ProgressBarHeightChanged(this, new PropertyChangedEventArgs("ProgressBarHeight"));
            }
        }

        [Category("Trackbar properties")]
        public bool DrawTicks
        {
            get
            {
                return drawTicks;
            }
            set
            {
                drawTicks = value;
                SetupTrackBar();
                if (DrawTicksChanged != null)
                    DrawTicksChanged(this, new PropertyChangedEventArgs("DrawTicks"));
            }
        }

        [Category("Trackbar properties")]
        public bool DrawSegments
        {
            get
            {
                return drawSegments;
            }
            set
            {
                drawSegments = value;
                SetupTrackBar();
                if (DrawSegmentsChanged != null)
                    DrawSegmentsChanged(this, new PropertyChangedEventArgs("DrawSegments"));
            }
        }

        // Calculate the sizes of the bar, thumb, and ticks rectangle.
        private void SetupTrackBar()
        {

            // Calculate the size of the thumb.
            if (TrackBarRenderer.IsSupported)
            {
                using (var g = CreateGraphics())
                {
                    thumbRectangle.Size = TrackBarRenderer.GetTopPointingThumbSize(g, TrackBarThumbState.Normal);
                    g.Dispose();
                }
            }
            else
            {
                thumbRectangle.Width = 11;
                thumbRectangle.Height = 21;
            }

            if (progressBarHeight == 0)
            {
                progressBarHeightForUse = Math.Min(thumbRectangle.Height - 10, ClientRectangle.Height - 10);
            }
            else
            {
                progressBarHeightForUse = progressBarHeight;
            }

            int h = 8 + progressBarHeightForUse + 2;
            if (h < thumbRectangle.Height)
            {
                double d = (double)h / (double)thumbRectangle.Height;
                thumbRectangle.Height = h;
                thumbRectangle.Width = (int)(thumbRectangle.Width * d);
            }

            if (drawTicks || drawSegments)
            {
                // Calculate the size of the track bar.
                trackRectangle.X = ClientRectangle.X + 2;
                trackRectangle.Y = ClientRectangle.Bottom - progressBarHeightForUse - 2;
                trackRectangle.Width = ClientRectangle.Width - 4;
                trackRectangle.Height = progressBarHeightForUse;

                // Calculate the size of the rectangle in which to 
                // draw the ticks.
                ticksRectangle.X = trackRectangle.X + 4;
                ticksRectangle.Y = trackRectangle.Y - 8;
                ticksRectangle.Width = trackRectangle.Width - 8;
                ticksRectangle.Height = 4;

                bigTicksRectangle = ticksRectangle;
                bigTicksRectangle.Y -= 4;
                bigTicksRectangle.Height += 6;

                thumbRectangle.X = CurrentTickXCoordinate();
                thumbRectangle.Y = trackRectangle.Y - 8;
            }
            else
            {
                // Calculate the size of the track bar.
                trackRectangle.X = ClientRectangle.X + 6;
                trackRectangle.Y = ClientRectangle.Bottom - progressBarHeightForUse - 10;
                trackRectangle.Width = ClientRectangle.Width - 12;
                trackRectangle.Height = progressBarHeightForUse;

                // only used for calculations here
                ticksRectangle.X = trackRectangle.X - 4;
                ticksRectangle.Y = trackRectangle.Y;
                ticksRectangle.Width = trackRectangle.Width;
                ticksRectangle.Height = 4;

                thumbRectangle.X = CurrentTickXCoordinate();
                thumbRectangle.Y = trackRectangle.Y;
            }

            progressTrackRectangle = trackRectangle;
            progressTrackRectangle.Height -= 4;
            progressTrackRectangle.Y += 2;
            progressTrackRectangle.Width -= 4;
            progressTrackRectangle.X += 2;
            progressTrackRectangle.Width
                = (int)((double)progressTrackRectangle.Width * (double)currentProgressValue / (double)maxProgress);

            Refresh();

        }

        private int CurrentTickXCoordinate()
        {
            return 1 + currentTickPosition * (ticksRectangle.Width - 2) / maxValue;
        }

        private int CurrentTickXCoordinate(int pos)
        {
            return 1 + pos * (ticksRectangle.Width - 2) / maxValue;
        }

        // Draw the track bar.
        protected override void OnPaint(PaintEventArgs e)
        {
            if (TrackBarRenderer.IsSupported && ProgressBarRenderer.IsSupported)
            {
                TrackBarRenderer.DrawHorizontalTrack(e.Graphics, trackRectangle);
                ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, progressTrackRectangle);
                if (drawTicks)
                {
                    TrackBarRenderer.DrawHorizontalTicks(e.Graphics, ticksRectangle, maxValue + 1, EdgeStyle.Etched);
                }
                if (drawSegments)
                {
                    TrackBarRenderer.DrawHorizontalTicks(e.Graphics, bigTicksRectangle, maxValueBigTicks + 1, EdgeStyle.Etched);
                }
                TrackBarRenderer.DrawTopPointingThumb(e.Graphics, thumbRectangle, thumbState);
            }
            else
            {
                e.Graphics.DrawRectangle(pen, trackRectangle);
                e.Graphics.FillRectangle(brush, progressTrackRectangle);
                e.Graphics.DrawImage(Resources.uphumb, thumbRectangle);
            }
        }

        // Determine whether the user has clicked the track bar thumb.
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!TrackBarRenderer.IsSupported)
            {
                return;
            }
            if (!ProgressBarRenderer.IsSupported)
            {
                return;
            }

            if (this.thumbRectangle.Contains(e.Location))
            {
                thumbClicked = true;
                thumbState = TrackBarThumbState.Pressed;
            }

            this.Invalidate();
        }

        // Redraw the track bar thumb if the user has moved it.
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (!TrackBarRenderer.IsSupported)
            {
                return;
            }
            if (!ProgressBarRenderer.IsSupported)
            {
                return;
            }

            if (thumbClicked)
            {
                thumbState = TrackBarThumbState.Normal;
                thumbClicked = false;
            }
            else
            {
                double x = e.Location.X + thumbRectangle.Width / 2.0;

                if (x >= thumbRectangle.X)
                {
                    int pos = ((int)Math.Ceiling((float)currentTickPosition / segmentSize + 0.00001)) * segmentSize;
                    int X = CurrentTickXCoordinate(pos);
                    if (X <= x)
                    {
                        currentTickPosition = pos;
                        thumbRectangle.X = X;
                    }
                }
                else
                {
                    int pos = ((int)Math.Floor((float)currentTickPosition / segmentSize - 0.00001)) * segmentSize;
                    int X = CurrentTickXCoordinate(pos);
                    if (X >= x)
                    {
                        currentTickPosition = pos;
                        thumbRectangle.X = X;
                    }
                }
            }

            this.Invalidate();
        }

        // Track cursor movements.
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!TrackBarRenderer.IsSupported)
            {
                return;
            }
            if (!ProgressBarRenderer.IsSupported)
            {
                return;
            }

            // The user is moving the thumb.
            if (thumbClicked)
            {
                double mod;
                if (drawTicks || drawSegments)
                {
                    mod = thumbRectangle.Width / 2.0;
                }
                else
                {
                    mod = 0;
                }
                double x = e.Location.X - ticksRectangle.X + mod;
                double currentRatio = x / (double)ticksRectangle.Width;
                currentRatio = Math.Max(0, currentRatio);
                currentRatio = Math.Min(1, currentRatio);

                currentTickPosition = (int)(maxValue * currentRatio);

                thumbRectangle.X = CurrentTickXCoordinate();
            }

            // The cursor is passing over the track.
            else
            {
                thumbState = thumbRectangle.Contains(e.Location) ?
                    TrackBarThumbState.Hot : TrackBarThumbState.Normal;
            }

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs a)
        {
            if (!thumbClicked)
            {
                thumbState = TrackBarThumbState.Normal;
                Invalidate();
            }
        }


        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (!thumbClicked)
            {
                currentTickPosition -= Math.Sign(e.Delta);
                currentTickPosition = Math.Max(currentTickPosition, 0);
                currentTickPosition = Math.Min(currentTickPosition, maxValue);
                thumbRectangle.X = CurrentTickXCoordinate();
                Invalidate();
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            int bt = 0;
            if (ModifierKeys == Keys.Control || e.KeyChar == (char)Keys.PageUp || e.KeyChar == (char)Keys.PageDown)
            {
                bt = segmentSize;
            }
            else if (ModifierKeys == Keys.None)
            {
                bt = 1;
            }

            if (bt != 0)
            {
                switch ((Keys)e.KeyChar)
                {
                    case Keys.Up:
                    case Keys.PageUp:
                    case Keys.Left: currentTickPosition -= bt; break;
                    case Keys.Down:
                    case Keys.PageDown:
                    case Keys.Right: currentTickPosition += bt; break;
                    case Keys.Home: currentTickPosition = 0; break;
                    case Keys.End: currentTickPosition = maxValue; break;
                }
                currentTickPosition = Math.Max(currentTickPosition, 0);
                currentTickPosition = Math.Min(currentTickPosition, maxValue);
                thumbRectangle.X = CurrentTickXCoordinate();
                Invalidate();
            }
        }


        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                case Keys.Control | Keys.Up:
                    OnKeyPress(new KeyPressEventArgs((char)Keys.Up)); return true;
                case Keys.Down:
                case Keys.Control | Keys.Down:
                    OnKeyPress(new KeyPressEventArgs((char)Keys.Down)); return true;
                case Keys.Right:
                case Keys.Control | Keys.Right:
                    OnKeyPress(new KeyPressEventArgs((char)Keys.Right)); return true;
                case Keys.Left:
                case Keys.Control | Keys.Left:
                    OnKeyPress(new KeyPressEventArgs((char)Keys.Left)); return true;
                case Keys.Home:
                    OnKeyPress(new KeyPressEventArgs((char)Keys.Home)); return true;
                case Keys.End:
                    OnKeyPress(new KeyPressEventArgs((char)Keys.End)); return true;
                case Keys.PageUp:
                    OnKeyPress(new KeyPressEventArgs((char)Keys.PageUp)); return true;
                case Keys.PageDown:
                    OnKeyPress(new KeyPressEventArgs((char)Keys.PageDown)); return true;
            }

            return base.IsInputKey(keyData);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ProgressTrackBar
            // 
            this.Name = "ProgressTrackBar";
            this.ResumeLayout(false);

        }
    }
}
