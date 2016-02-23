using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{

    public interface IClientController : IController
    {
        void Update();

        bool SupportsPaint
        {
            get;
            set;
        }

        GraphicsController Graphics
        {
            get;
        }
    }


    public class ClientController : Controller, IClientController
    {

        public event ControllerEventHandler Updated;

        public ClientController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public ClientController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected ClientController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            SupportsPaint = (bool) info.GetValue("SupportsPaint", typeof(bool));
        }

        bool supportsPaint = false;
        const int PROPERTY_14_SUPPORTSPAINT_ID = 14;
        public bool SupportsPaint
        {
            get
            {
                return supportsPaint;
            }
            set
            {
                supportsPaint = value;
            }
        }

        GraphicsController graphics;
        ControllerPaintEventArgs paintEvent;
        public GraphicsController Graphics
        {
            get
            {
                return graphics;
            }
        }

        public void Paint(PaintEventArgs args)
        {
            OnPaint(args);
        }

        public void Paint(OfflineGraphicsController g)
        {
            ControllerPaintEventArgs args = new ControllerPaintEventArgs(g);
            OnPaint(args);
        }

        internal void OnPaint(PaintEventArgs e)
        {
            if (supportsPaint)
            {
                if (graphics == null || ((WinFormsGraphicsController)graphics).Graphics != e.Graphics)
                {
                    graphics = new WinFormsGraphicsController(e.Graphics);
                    paintEvent = new ControllerPaintEventArgs(graphics);
                }
                OnPaint((ControllerEventArgs)null);
            }
        }

        internal void OnPaint(ControllerEventArgs args)
        {
            if (supportsPaint)
            {
                if (args is ControllerPaintEventArgs)
                {
                    ControllerPaintEventArgs e = (ControllerPaintEventArgs)args;
                    graphics = e.Graphics;
                    paintEvent = e;
                }
                else if (paintEvent == null)
                {
                    throw new NotSupportedException();
                }

                OnPaint(paintEvent);
            }
        }

        protected virtual void OnPaint(ControllerPaintEventArgs e)
        {
        }

        public void Update()
        {
            OnUpdate(null);
        }

        internal void OnUpdate(ControllerEventArgs args)
        {
            if (Updated != null)
            {
                if (args == null)
                {
                    args = new ControllerEventArgs();
                }
                else if (args.Consumed)
                {
                    return;
                }
                Updated(this, args);
                args.Consume();
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_14_SUPPORTSPAINT_ID:
                    return SupportsPaint;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_14_SUPPORTSPAINT_ID:
                    SupportsPaint = (bool)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("SupportsPaint", SupportsPaint);
        }

    }
}
