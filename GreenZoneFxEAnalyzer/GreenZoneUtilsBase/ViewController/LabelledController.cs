using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{

    public interface ILabelledController : IController
    {
    }

    public class LabelledController : Controller, ILabelledController
    {
        public event PropertyChangedEventHandler TextChanged;
        public event PropertyChangedEventHandler ImageChanged;

        public LabelledController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public LabelledController(GreenRmiManager rmiManager, Controller parent, string text)
            : base(rmiManager, parent)
        {
            this.text = text;
        }

        public LabelledController(GreenRmiManager rmiManager, Controller parent, string text, int image)
            : base(rmiManager, parent)
        {
            this.text = text;
            this.image = image;
        }

        public LabelledController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            this.text = (string)buffer.ChangedProps[PROPERTY_14_TEXT_ID];
            this.image = (int)buffer.ChangedProps[PROPERTY_15_IMAGE_ID]; ;
        }

        protected LabelledController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Text = (string) info.GetValue("Text", typeof(string));
            Image = (int) info.GetValue("Image", typeof(int));
        }

        string text;
        const int PROPERTY_14_TEXT_ID = 14;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text != value)
                {
                    text = value;
                    changed[PROPERTY_14_TEXT_ID] = true;
                    somethingChanged = true;
                    if (TextChanged != null)
                    {
                        TextChanged(this, new PropertyChangedEventArgs("Text", value));
                    }
                }
            }
        }

        int image;
        const int PROPERTY_15_IMAGE_ID = 15;
        public int Image
        {
            get
            {
                return image;
            }
            set
            {
                if (image != value)
                {
                    image = value;
                    changed[PROPERTY_15_IMAGE_ID] = true;
                    somethingChanged = true;
                    if (ImageChanged != null)
                    {
                        ImageChanged(this, new PropertyChangedEventArgs("Image", value));
                    }
                }
            }
        }

        
        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_14_TEXT_ID:
                    return Text;
                case PROPERTY_15_IMAGE_ID:
                    return Image;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_14_TEXT_ID:
                    Text = (string)value;
                    break;
                case PROPERTY_15_IMAGE_ID:
                    Image = (int)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Text", Text);
            info.AddValue("Image", Image);
        }
    }
}
