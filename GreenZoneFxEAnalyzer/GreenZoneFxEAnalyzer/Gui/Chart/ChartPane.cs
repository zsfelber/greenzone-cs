using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.ComponentModel;
using GreenZoneFxEngine.Types;
using System.Drawing.Drawing2D;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Properties;
using GreenZoneFxEngine.Util;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.Gui.ViewController;
using System.Collections.ObjectModel;
using GreenZoneUtil.Gui;

namespace GreenZoneFxEngine.Gui.Chart
{
    abstract class ChartPane : Panel
    {
        protected enum DraggingState
        {
            INACTIVE,
            DRAGGED
        }

        ContextMenuStripWTitle popupMenu;

        TrackBarThumbState thumbState = TrackBarThumbState.Normal;
        GreenWinFormsMVContext context;
        ChartPaneController controller;
        ChartController chartController;
        ChartSectionPanelController chartSectionPanelController;

        public ChartPane()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer, true);

            popupMenu = new ContextMenuStripWTitle();
            ContextMenuStrip = popupMenu;

            MouseDown += new MouseEventHandler(CursorBar_MouseDown);
            MouseUp += new MouseEventHandler(CursorBar_MouseUp);
            MouseMove += new MouseEventHandler(CursorBar_MouseMove);
            MouseLeave += new EventHandler(CursorBar_MouseLeave);
            popupMenu.Opened += new EventHandler(popupMenu_Opened);

            DragState = DraggingState.INACTIVE;
            ForeColor = Color.Black;
        }

        public virtual void Bind(GreenWinFormsMVContext context, ChartPaneController controller)
        {
            this.context = context;
            this.controller = controller;

            controller.DragTimerUsed = true;

            chartSectionPanelController = (ChartSectionPanelController)controller.Parent;
            chartController = (ChartController)chartSectionPanelController.Parent.Parent;

            new SimpleControlVCBinder(context, this, controller);
            new ContextMenuStripVCBinder(context, popupMenu, controller.PopupMenu);

            if (TrackBarRenderer.IsSupported)
            {
                using (var g = CreateGraphics())
                {
                    controller.ThumbRectangleSize = TrackBarRenderer.GetBottomPointingThumbSize(g, TrackBarThumbState.Normal);
                }
            }
            else
            {
                controller.ThumbRectangleSize = new Size(21, 11);
            }
        }

        protected DraggingState DragState
        {
            get;
            set;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (controller != null && controller.ThumbInitiliazed && controller.ThumbRectBarVisible)
            {
                Graphics g = e.Graphics;

                Brush barBrush = controller.BarBrush;
                Rectangle barRectangle = controller.BarRectangle;
                g.FillRectangle(barBrush, barRectangle);

                //g.DrawLine(linePen, thumbRectangle.X + AutoGap, 0, thumbRectangle.X + AutoGap, Height);

                if (controller.SliderThumbVisible)
                {
                    Rectangle thumbRectangle = controller.ThumbRectangle;
                    if (TrackBarRenderer.IsSupported)
                    {
                        TrackBarRenderer.DrawBottomPointingThumb(g, thumbRectangle, thumbState);
                    }
                    else
                    {
                        g.DrawImage(Resources.downthumb, thumbRectangle);
                    }
                }
            }
        }

        void CursorBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (DragState == DraggingState.INACTIVE && e.Button == MouseButtons.Left)
            {
                if (controller.SliderContains(e.Location))
                {
                    thumbState = TrackBarThumbState.Pressed;
                }
                else
                {
                    thumbState = TrackBarThumbState.Normal;
                }
                DragState = DraggingState.DRAGGED;
                Invalidate();
                Update();
            }
        }

        void CursorBar_MouseUp(object sender, MouseEventArgs e)
        {
            switch (DragState)
            {
                case DraggingState.DRAGGED:
                    DragState = DraggingState.INACTIVE;
                    thumbState = TrackBarThumbState.Normal;
                    Invalidate();
                    Update();
                    Drag(e.Location);
                    break;
                default:
                    break;
            }
        }

        void CursorBar_MouseMove(object sender, MouseEventArgs e)
        {
            CursorBar_MouseMove(e.Location);
        }

        void CursorBar_MouseMove(Point p)
        {
            switch (DragState)
            {
                case DraggingState.DRAGGED:
                    Drag(p);
                    break;
                case DraggingState.INACTIVE:
                    TrackBarThumbState oldThumbState = thumbState;
                    UpdateMovingCursors(p);

                    SeriesBar oldSeriesBar = controller.SelectedSeriesBar;
                    IndicatorBar oldIndicatorBar = controller.SelectedIndicatorBar;
                    controller.SelectSeriesBar(p);

                    if (thumbState != oldThumbState || controller.SelectedSeriesBar != oldSeriesBar || controller.SelectedIndicatorBar != oldIndicatorBar)
                    {
                        Invalidate();
                        Update();
                    }

                    break;
                default:
                    break;
            }
        }

        protected virtual void UpdateMovingCursors(Point p)
        {
            if (controller.SliderContains(p))
            {
                thumbState = TrackBarThumbState.Hot;
                Cursor = Cursors.Hand;
            }
            else
            {
                thumbState = TrackBarThumbState.Normal;
                Cursor = Cursors.Default;
            }
        }

        void CursorBar_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;

            if (!popupMenu.Visible)
            {
                switch (DragState)
                {
                    case DraggingState.DRAGGED:
                        break;
                    case DraggingState.INACTIVE:
                        DragState = DraggingState.INACTIVE;
                        thumbState = TrackBarThumbState.Normal;
                        Invalidate();
                        Update();

                        if (controller.SelectedSeriesBar != null)
                        {
                            controller.ClearSeriesBarSelection();
                            Invalidate();
                            Update();
                        }
                        break;
                    default:
                        break;
                }
            }
        }


        void Drag(Point p)
        {
            controller.Drag(p);
        }

        void popupMenu_Opened(object sender, EventArgs e)
        {
            select();
            if (string.IsNullOrEmpty(popupMenu.Text))
            {
                popupMenu.TitleLabel = null;
            }
            else
            {
                popupMenu.TitleLabel = new ContextMenuHeader();
            }
            popupMenu.PerformLayout();
        }

        void select()
        {
            Point p0 = Control.MousePosition;
            Point p = PointToClient(p0);
            CursorBar_MouseMove(p);
        }
    }
}
