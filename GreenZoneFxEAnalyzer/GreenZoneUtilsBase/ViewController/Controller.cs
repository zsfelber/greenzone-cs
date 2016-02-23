using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.Util;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    public interface IController : IRmiBase
    {
        bool Enabled
        {
            get;
            set;
        }

        bool Visible
        {
            get;
            set;
        }

        Controller Parent
        {
            get;
        }

        string Name
        {
            get;
            set;
        }

        Point Location
        {
            get;
            set;
        }

        Size Size
        {
            get;
            set;
        }

        int Width
        {
            get;
        }

        int Height
        {
            get;
        }

        Font Font
        {
            get;
            set;
        }

        Color BackColor
        {
            get;
            set;
        }

        Brush BgBrush
        {
            get;
        }

        Color ForeColor
        {
            get;
            set;
        }

        Brush FgBrush
        {
            get;
        }

        object Tag
        {
            get;
            set;
        }

        object BoundControl
        {
            get;
            set;
        }

        ComboController PopupMenu
        {
            get;
            set;
        }

        IList<Controller> Controls
        {
            get;
            set;
        }

        int ChildCount
        {
            get;
        }

        void Add(Controller control);

        void Insert(int index, Controller control);

        void Remove(Controller control);

        void RemoveAt(int index);

        void Clear();

        void Layout();

        void Click(Point point);

        void DoubleClick(Point point);

        void RaiseMouseDown(Point point);

        void Drag(Point point);
    }

    public class Controller : RmiBase, IController
    {
        public event PropertyChangedEventHandler EnabledChanged;
        public event PropertyChangedEventHandler VisibleChanged;
        public event PropertyChangedEventHandler ParentChanged;
        public event PropertyChangedEventHandler NameChanged;
        public event PropertyChangedEventHandler LocationChanged;
        public event PropertyChangedEventHandler SizeChanged;
        public event PropertyChangedEventHandler FontChanged;
        public event PropertyChangedEventHandler BackColorChanged;
        public event PropertyChangedEventHandler ForeColorChanged;
        public event PropertyChangedEventHandler TagChanged;
        public event PropertyChangedEventHandler BoundControlChanged;
        public event PropertyChangedEventHandler PopupMenuChanged;
        public event PropertyChangedEventHandler ControlsChanged;

        public event ControllerEventHandler AddedToParent;
        public event ControllerEventHandler RemovedFromParent;
        public event ListChangedEventHandler ChildControlInserted;
        public event ListChangedEventHandler ChildControlRemovedAt;

        public event ControllerEventHandler LayoutChanged;
        public event ControllerEventHandler Clicked;
        public event ControllerEventHandler DoubleClicked;
        public event ControllerEventHandler MouseDown;
        public event ControllerEventHandler Dragged;


        public Controller(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager)
        {
            Clear();
            Name = GetType().Name;
            if (parent != null)
            {
                parent.Add(this);
                dependencies.Add(parent);
            }
        }

        public Controller(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            Controller parent = (Controller)buffer.ChangedProps[PROPERTY_3_PARENT_ID];

            Clear();
            Name = GetType().Name;
            if (parent != null)
            {
                parent.Add(this);
                dependencies.Add(parent);
            }
        }

        protected Controller(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Enabled = (bool)info.GetValue("Enabled", typeof(bool));
            Visible = (bool)info.GetValue("Visible", typeof(bool));
            Parent = (Controller)info.GetValue("Parent", typeof(Controller));
            Name = (string)info.GetValue("Name", typeof(string));
            Location = (Point)info.GetValue("Location", typeof(Point));
            Size = (Size) info.GetValue("Size", typeof(Size));
            Font = (Font) info.GetValue("Font", typeof(Font));
            BackColor = (Color) info.GetValue("BackColor", typeof(Color));
            ForeColor = (Color) info.GetValue("ForeColor", typeof(Color));
            Tag = (object) info.GetValue("Tag", typeof(object));
            PopupMenu = (ComboController) info.GetValue("PopupMenu", typeof(ComboController));
            Controls = (IList<Controller>) info.GetValue("Controls", typeof(IList<Controller>));
            if (parent != null)
            {
                dependencies.Add(parent);
            }
        }

        bool enabled = true;
        const int PROPERTY_1_ENABLED_ID = 1;
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    changed[PROPERTY_1_ENABLED_ID] = true;
                    somethingChanged = true;
                    if (EnabledChanged != null)
                    {
                        EnabledChanged(this, new PropertyChangedEventArgs("Enabled", value));
                    }
                }
            }
        }

        bool visible = true;
        const int PROPERTY_2_VISIBLE_ID = 2;
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    changed[PROPERTY_2_VISIBLE_ID] = true;
                    somethingChanged = true;
                    if (VisibleChanged != null)
                    {
                        VisibleChanged(this, new PropertyChangedEventArgs("Visible", value));
                    }
                }
            }
        }

        Controller parent;
        const int PROPERTY_3_PARENT_ID = 3;
        public virtual Controller Parent
        {
            get
            {
                return parent;
            }
            protected set
            {
                if (parent != value)
                {
                    parent = value;
                    changed[PROPERTY_3_PARENT_ID] = true;
                    somethingChanged = true;
                    if (ParentChanged != null)
                    {
                        ParentChanged(this, new PropertyChangedEventArgs("Parent", value));
                    }
                }
            }
        }

        string name;
        const int PROPERTY_4_NAME_ID = 4;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    changed[PROPERTY_4_NAME_ID] = true;
                    somethingChanged = true;
                    if (NameChanged != null)
                    {
                        NameChanged(this, new PropertyChangedEventArgs("Name", value));
                    }
                }
            }
        }

        Point location;
        const int PROPERTY_5_LOCATION_ID = 5;
        public Point Location
        {
            get
            {
                return location;
            }
            set
            {
                if (location != value)
                {
                    location = value;
                    changed[PROPERTY_5_LOCATION_ID] = true;
                    somethingChanged = true;
                    if (LocationChanged != null)
                    {
                        LocationChanged(this, new PropertyChangedEventArgs("Location", value));
                    }
                }
            }
        }

        Size size;
        const int PROPERTY_6_SIZE_ID = 6;
        public Size Size
        {
            get
            {
                return size;
            }
            set
            {
                if (size != value)
                {
                    size = value;
                    changed[PROPERTY_6_SIZE_ID] = true;
                    somethingChanged = true;
                    if (SizeChanged != null)
                    {
                        SizeChanged(this, new PropertyChangedEventArgs("Size", value));
                    }
                }
            }
        }

        public int Width
        {
            get
            {
                return size.Width;
            }
        }

        public int Height
        {
            get
            {
                return size.Height;
            }
        }

        Font font;
        const int PROPERTY_7_FONT_ID = 7;
        public virtual Font Font
        {
            get
            {
                return font;
            }
            set
            {
                if (font != value)
                {
                    font = value;
                    changed[PROPERTY_7_FONT_ID] = true;
                    somethingChanged = true;
                    if (FontChanged != null)
                    {
                        FontChanged(this, new PropertyChangedEventArgs("Font", value));
                    }
                }
            }
        }

        Color backColor;
        const int PROPERTY_8_BACKCOLOR_ID = 8;
        public virtual Color BackColor
        {
            get
            {
                return backColor;
            }
            set
            {
                if (backColor != value)
                {
                    backColor = value;
                    bgBrush = new SolidBrush(value);
                    changed[PROPERTY_8_BACKCOLOR_ID] = true;
                    somethingChanged = true;
                    if (BackColorChanged != null)
                    {
                        BackColorChanged(this, new PropertyChangedEventArgs("BackColor", value));
                    }
                }
            }
        }

        Brush bgBrush;
        public Brush BgBrush
        {
            get
            {
                return bgBrush;
            }
        }

        Color foreColor;
        const int PROPERTY_9_FORECOLOR_ID = 9;
        public virtual Color ForeColor
        {
            get
            {
                return foreColor;
            }
            set
            {
                if (foreColor != value)
                {
                    foreColor = value;
                    fgBrush = new SolidBrush(value);
                    changed[PROPERTY_9_FORECOLOR_ID] = true;
                    somethingChanged = true;
                    if (ForeColorChanged != null)
                    {
                        ForeColorChanged(this, new PropertyChangedEventArgs("ForeColor", value));
                    }
                }
            }
        }

        Brush fgBrush;
        public Brush FgBrush
        {
            get
            {
                return fgBrush;
            }
        }

        object tag;
        const int PROPERTY_10_TAG_ID = 10;
        public object Tag
        {
            get
            {
                return tag;
            }
            set
            {
                if (tag != value)
                {
                    tag = value;
                    changed[PROPERTY_10_TAG_ID] = true;
                    somethingChanged = true;
                    if (TagChanged != null)
                    {
                        TagChanged(this, new PropertyChangedEventArgs("Tag", value));
                    }
                }
            }
        }

        object boundControl;
        const int PROPERTY_11_BOUNDCONTROL_ID = 11;
        public object BoundControl
        {
            get
            {
                return boundControl;
            }
            set
            {
                if (boundControl != value)
                {
                    boundControl = value;
                    changed[PROPERTY_11_BOUNDCONTROL_ID] = true;
                    somethingChanged = true;
                    if (BoundControlChanged != null)
                    {
                        BoundControlChanged(this, new PropertyChangedEventArgs("BoundControl", value));
                    }
                }
            }
        }

        ComboController popupMenu;
        const int PROPERTY_12_POPUPMENU_ID = 12;
        public ComboController PopupMenu
        {
            get
            {
                return popupMenu;
            }
            set
            {
                if (popupMenu != value)
                {
                    popupMenu = value;
                    changed[PROPERTY_12_POPUPMENU_ID] = true;
                    somethingChanged = true;
                    if (PopupMenuChanged != null)
                    {
                        PopupMenuChanged(this, new PropertyChangedEventArgs("PopupMenu", value));
                    }
                }
            }
        }

        IList<Controller> controls;
        IList<Controller> controlsUm;
        const int PROPERTY_13_CONTROLS_ID = 13;
        public virtual IList<Controller> Controls
        {
            get
            {
                return controlsUm;
            }
            set
            {
                if (controls != value)
                {
                    controls = value;
                    controlsUm = new ReadOnlyCollection<Controller>(value);
                    changed[PROPERTY_13_CONTROLS_ID] = true;
                    somethingChanged = true;
                    if (ControlsChanged != null)
                    {
                        ControlsChanged(this, new PropertyChangedEventArgs("Controls", value));
                    }
                }
            }
        }

        public int ChildCount
        {
            get
            {
                return controls.Count;
            }
        }

        protected virtual bool Validate()
        {
            return true;
        }

        public virtual void Add(IController control)
        {
            Add((Controller)control);
        }

        public virtual void Add(Controller control)
        {
            if (control.parent != null)
            {
                control.parent.Remove(control);
            }
            controls.Add(control);
            control.Parent = this;
            control.OnControlAddToParent();
            if (ChildControlInserted != null)
            {
                ChildControlInserted(null, new ListChangedEventArgs(ListChangedType.ItemAdded, controls.Count - 1, control));
            }
        }

        public virtual void Insert(int index, IController control)
        {
            Insert(index, (Controller)control);
        }

        public virtual void Insert(int index, Controller control)
        {
            if (control.parent != null)
            {
                control.parent.Remove(control);
            }
            controls.Insert(index, control);
            control.Parent = this;
            control.OnControlAddToParent();
            if (ChildControlInserted != null)
            {
                ChildControlInserted(null, new ListChangedEventArgs(ListChangedType.ItemAdded, index, control));
            }
        }

        public virtual void Remove(IController control)
        {
            Remove((Controller)control);
        }

        public virtual void Remove(Controller control)
        {
            if (control.parent != this)
            {
                throw new NotSupportedException("control.parent != this   control.parent:" + control.parent + "  this:" + this);
            }
            int remind = controls.IndexOf(control);
            controls.Remove(control);
            control.Parent = null;
            control.OnControlRemoveFromParent();
            if (ChildControlRemovedAt != null)
            {
                ChildControlRemovedAt(null, new ListChangedEventArgs(ListChangedType.ItemDeleted, remind, control));
            }
        }

        public virtual void RemoveAt(int index)
        {
            Controller remitm = controls[index];
            controls.RemoveAt(index);
            remitm.Parent = null;
            remitm.OnControlRemoveFromParent();
            if (ChildControlRemovedAt != null)
            {
                ChildControlRemovedAt(null, new ListChangedEventArgs(ListChangedType.ItemDeleted, index, remitm));
            }
        }

        protected internal virtual int RemoveFrom(Controller child)
        {
            int ind0 = Controls.IndexOf(child);
            if (controls is List<Controller>)
            {
                ((List<Controller>)controls).RemoveRange(ind0, controls.Count - ind0);
            }
            else
            {
                throw new NotSupportedException("controls : " + controls);
            }
            return ind0;
        }

        public virtual void Clear()
        {
            Controls = new List<Controller>();
        }


        public void Layout()
        {
            OnLayoutChanged(null);
        }

        public virtual void Click(Point point)
        {
            OnClick(new ControllerMouseEventArgs(point));
        }

        public virtual void DoubleClick(Point point)
        {
            OnDoubleClick(new ControllerMouseEventArgs(point));
        }

        public virtual void RaiseMouseDown(Point point)
        {
            OnMouseDown(new ControllerMouseEventArgs(point));
        }

        public virtual void Drag(Point point)
        {
            OnDrag(new ControllerMouseEventArgs(point));
        }

        internal virtual void OnControlAddToParent()
        {
            if (AddedToParent != null)
            {
                AddedToParent(this, new ControllerEventArgs());
            }
        }

        protected virtual void OnControlRemoveFromParent()
        {
            if (RemovedFromParent != null)
            {
                RemovedFromParent(this, new ControllerEventArgs());
            }
        }

        internal void OnLayoutChanged(ControllerEventArgs args)
        {
            if (LayoutChanged != null)
            {
                if (args == null)
                {
                    args = new ControllerEventArgs();
                }
                else if (args.Consumed)
                {
                    return;
                }
                LayoutChanged(this, args);
                args.Consume();
            }
        }

        internal virtual void OnClick(ControllerEventArgs args)
        {
            if (Clicked != null)
            {
                if (args == null)
                {
                    args = new ControllerEventArgs();
                }

                Clicked(this, args);
            }
        }

        internal virtual void OnDoubleClick(ControllerEventArgs args)
        {
            if (DoubleClicked != null)
            {
                if (args == null)
                {
                    args = new ControllerEventArgs();
                }

                DoubleClicked(this, args);
            }
        }

        internal virtual void OnMouseDown(ControllerEventArgs args)
        {
            if (MouseDown != null)
            {
                if (args == null)
                {
                    args = new ControllerEventArgs();
                }

                MouseDown(this, args);
            }
        }

        internal virtual void OnDrag(ControllerEventArgs args)
        {
            if (Dragged != null)
            {
                if (args == null)
                {
                    args = new ControllerEventArgs();
                }

                Dragged(this, args);
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_1_ENABLED_ID:
                    return Enabled;
                case PROPERTY_2_VISIBLE_ID:
                    return Visible;
                case PROPERTY_3_PARENT_ID:
                    return Parent;
                case PROPERTY_4_NAME_ID:
                    return Name;
                case PROPERTY_5_LOCATION_ID:
                    return Location;
                case PROPERTY_6_SIZE_ID:
                    return Size;
                case PROPERTY_7_FONT_ID:
                    return Font;
                case PROPERTY_8_BACKCOLOR_ID:
                    return BackColor;
                case PROPERTY_9_FORECOLOR_ID:
                    return ForeColor;
                case PROPERTY_10_TAG_ID:
                    return Tag;
                case PROPERTY_11_BOUNDCONTROL_ID:
                    return BoundControl;
                case PROPERTY_12_POPUPMENU_ID:
                    return PopupMenu;
                case PROPERTY_13_CONTROLS_ID:
                    return Controls;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_1_ENABLED_ID:
                    Enabled = (bool)value;
                    break;
                case PROPERTY_2_VISIBLE_ID:
                    Visible = (bool)value;
                    break;
                case PROPERTY_3_PARENT_ID:
                    Parent = (Controller)value;
                    break;
                case PROPERTY_4_NAME_ID:
                    Name = (string)value;
                    break;
                case PROPERTY_5_LOCATION_ID:
                    Location = (Point)value;
                    break;
                case PROPERTY_6_SIZE_ID:
                    Size = (Size)value;
                    break;
                case PROPERTY_7_FONT_ID:
                    Font = (Font)value;
                    break;
                case PROPERTY_8_BACKCOLOR_ID:
                    BackColor = (Color)value;
                    break;
                case PROPERTY_9_FORECOLOR_ID:
                    ForeColor = (Color)value;
                    break;
                case PROPERTY_10_TAG_ID:
                    Tag = value;
                    break;
                case PROPERTY_11_BOUNDCONTROL_ID:
                    BoundControl = value;
                    break;
                case PROPERTY_12_POPUPMENU_ID:
                    PopupMenu = (ComboController)value;
                    break;
                case PROPERTY_13_CONTROLS_ID:
                    Controls = new BridgeCollection<Controller>((ArrayList)value);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Enabled", Enabled);
            info.AddValue("Visible", Visible);
            info.AddValue("Parent", Parent);
            info.AddValue("Name", Name);
            info.AddValue("Location", Location);
            info.AddValue("Size", Size);
            info.AddValue("Font", Font);
            info.AddValue("BackColor", BackColor);
            info.AddValue("ForeColor", ForeColor);
            info.AddValue("Tag", Tag);
            info.AddValue("PopupMenu", PopupMenu);
            info.AddValue("Controls", Controls);
        }
    }

}
