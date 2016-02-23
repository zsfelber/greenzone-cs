using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class SplitController : Controller
    {
        public event PropertyChangedEventHandler Panel1Changed;
        public event PropertyChangedEventHandler Panel2Changed;
        public event PropertyChangedEventHandler Panel1CollapsedChanged;
        public event PropertyChangedEventHandler Panel2CollapsedChanged;
        public event PropertyChangedEventHandler SplitterDistanceChanged;

        public SplitController(GreenRmiManager rmiManager, Controller parent)
            : this(rmiManager, parent, new Controller(rmiManager, (Controller)null), new Controller(rmiManager, (Controller)null))
        {
        }

        public SplitController(GreenRmiManager rmiManager, Controller parent, Controller panel1, Controller panel2)
            : base(rmiManager, parent)
        {
            if (panel1 == null)
            {
                throw new ArgumentNullException("panel1");
            }
            if (panel2 == null)
            {
                throw new ArgumentNullException("panel2");
            }
            base.Add(panel1);
            base.Add(panel2);
        }

        public SplitController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected SplitController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Panel1Collapsed = (bool) info.GetValue("Panel1Collapsed", typeof(bool));
            Panel2Collapsed = (bool) info.GetValue("Panel2Collapsed", typeof(bool));
            SplitterDistance = (int) info.GetValue("SplitterDistance", typeof(int));
        }

        public Controller Panel1
        {
            get
            {
                return Controls[0];
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                if (Panel1 != value)
                {
                    base.Remove(Panel1);
                    base.Insert(0, value);
                    if (Panel1Changed != null)
                    {
                        Panel1Changed(this, new PropertyChangedEventArgs("Panel1", value));
                    }
                }
            }
        }

        public Controller Panel2
        {
            get
            {
                return Controls[1];
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                if (Panel2 != value)
                {
                    base.Remove(Panel2);
                    base.Add(value);
                    if (Panel2Changed != null)
                    {
                        Panel2Changed(this, new PropertyChangedEventArgs("Panel2", value));
                    }
                }
            }
        }

        bool panel1Collapsed;
        const int PROPERTY_14_PANEL1COLLAPSED_ID = 14;
        public bool Panel1Collapsed
        {
            get
            {
                return panel1Collapsed;
            }
            set
            {
                if (panel1Collapsed != value)
                {
                    panel1Collapsed = value;
                    changed[PROPERTY_14_PANEL1COLLAPSED_ID] = true;
                    somethingChanged = true;
                    if (Panel1CollapsedChanged != null)
                    {
                        Panel1CollapsedChanged(this, new PropertyChangedEventArgs("Panel1Collapsed", value));
                    }
                }
            }
        }

        bool panel2Collapsed;
        const int PROPERTY_15_PANEL2COLLAPSED_ID = 15;
        public bool Panel2Collapsed
        {
            get
            {
                return panel2Collapsed;
            }
            set
            {
                if (panel2Collapsed != value)
                {
                    panel2Collapsed = value;
                    changed[PROPERTY_15_PANEL2COLLAPSED_ID] = true;
                    somethingChanged = true;
                    if (Panel2CollapsedChanged != null)
                    {
                        Panel2CollapsedChanged(this, new PropertyChangedEventArgs("Panel2Collapsed", value));
                    }
                }
            }
        }

        int splitterDistance;
        const int PROPERTY_16_SPLITTERDISTANCE_ID = 16;
        public int SplitterDistance
        {
            get
            {
                return splitterDistance;
            }
            set
            {
                if (splitterDistance != value)
                {
                    splitterDistance = value;
                    changed[PROPERTY_16_SPLITTERDISTANCE_ID] = true;
                    somethingChanged = true;
                    if (SplitterDistanceChanged != null)
                    {
                        SplitterDistanceChanged(this, new PropertyChangedEventArgs("SplitterDistance", value));
                    }
                }
            }
        }

        public override void Add(Controller control)
        {
            throw new NotSupportedException();
        }

        public override void Insert(int index, Controller control)
        {
            throw new NotSupportedException();
        }

        public override void Remove(Controller control)
        {
            throw new NotSupportedException();
        }

        public override void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_14_PANEL1COLLAPSED_ID:
                    return Panel1Collapsed;
                case PROPERTY_15_PANEL2COLLAPSED_ID:
                    return Panel2Collapsed;
                case PROPERTY_16_SPLITTERDISTANCE_ID:
                    return SplitterDistance;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_14_PANEL1COLLAPSED_ID:
                    Panel1Collapsed = (bool)value;
                    break;
                case PROPERTY_15_PANEL2COLLAPSED_ID:
                    Panel2Collapsed = (bool)value;
                    break;
                case PROPERTY_16_SPLITTERDISTANCE_ID:
                    SplitterDistance = (int)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Panel1Collapsed", Panel1Collapsed);
            info.AddValue("Panel2Collapsed", Panel2Collapsed);
            info.AddValue("SplitterDistance", SplitterDistance);
        }
    }
}
