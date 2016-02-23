using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    public interface IDialogController : IFormController
    {
    }

    public class DialogController : FormController, IDialogController
    {
        public DialogController(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
            Visible = false;
        }

        public DialogController(GreenRmiManager rmiManager, string text)
            : base(rmiManager, text)
        {
            Visible = false;
        }

        public DialogController(GreenRmiManager rmiManager, string text, int image)
            : base(rmiManager, text, image)
        {
            Visible = false;
        }

        public DialogController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            Visible = false;
        }

        protected DialogController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }

}
