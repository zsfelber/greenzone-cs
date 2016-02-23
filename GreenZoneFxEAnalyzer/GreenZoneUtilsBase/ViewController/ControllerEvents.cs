using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneUtil.ViewController
{
    
    public delegate void ControllerEventHandler(object sender, ControllerEventArgs args);
    
    public delegate void ControllerMouseEventHandler(object sender, ControllerMouseEventArgs args);
    
    public delegate void FormEventHandler(FormControllerEventArgs args);
    
    public delegate void ShowDialogEventHandler(ShowDialogEventArgs args);

    
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs args);
    
    public delegate void PropertyChangingEventHandler(object sender, PropertyChangingEventArgs args);
    
    public delegate void PropertyChangingEventHandlerC(object sender, PropertyChangingEventArgsC args);
    
    public delegate void ListChangedEventHandler(object sender, ListChangedEventArgs args);

    public class ControllerEventArgs
    {
        bool consumed = false;
        public bool Consumed
        {
            get
            {
                return consumed;
            }
        }

        public void Consume()
        {
            consumed = true;
        }
    }

    public class ControllerMouseEventArgs : ControllerEventArgs
    {
        internal ControllerMouseEventArgs(Point point)
        {
            this.point = point;
        }
        readonly Point point;
        public Point Point
        {
            get
            {
                return point;
            }
        }
    }
    
    public class PropertyChangedEventArgs : ControllerEventArgs
    {
        public PropertyChangedEventArgs(string propertyName, object value)
        {
            this.propertyName = propertyName;
            this.value = value;
        }

        readonly string propertyName;
        public string PropertyName
        {
            get
            {
                return propertyName;
            }
        }

        readonly object value;
        public object Value
        {
            get
            {
                return value;
            }
        }
    }

    public class PropertyChangingEventArgs : ControllerEventArgs
    {
        public PropertyChangingEventArgs(string propertyName, object value)
        {
            this.propertyName = propertyName;
            this.value = value;
        }

        readonly string propertyName;
        public string PropertyName {
            get
            {
                return propertyName;
            }
        }

        readonly object value;
        public object Value
        {
            get
            {
                return value;
            }
        }
    }

    public class PropertyChangingEventArgsC : PropertyChangingEventArgs
    {
        public PropertyChangingEventArgsC(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        bool cancel;
        public bool Cancel
        {
            get
            {
                return cancel;
            }
            set
            {
                cancel = value;
            }
        }
    }

    
    public enum ListChangedType
    {
        ItemAdded, ItemDeleted, ItemChanged
    }

    public class ListChangedEventArgs : ControllerEventArgs
    {
        public ListChangedEventArgs(string propertyName, ListChangedType operation, object item, int index)
        {
            this.propertyName = propertyName;
            this.operation = operation;
            this.item = item;
            this.index = index;
        }

        public ListChangedEventArgs(ListChangedType operation, int index, object item)
        {
            this.operation = operation;
            this.item = item;
            this.index = index;
        }

        readonly string propertyName;
        public string PropertyName {
            get
            {
                return propertyName;
            }
        }

        readonly ListChangedType operation;
        public ListChangedType Operation
        {
            get
            {
                return operation;
            }
        }

        readonly object item;
        public object Item
        {
            get
            {
                return item;
            }
        }
        public object Element
        {
            get
            {
                return item;
            }
        }

        readonly int index;
        public int Index
        {
            get
            {
                return index;
            }
        }
        public int NewIndex
        {
            get
            {
                return index;
            }
        }
    }

    public class TabControllerEventArgs : ControllerEventArgs
    {
    }

    public class FormControllerEventArgs : ControllerEventArgs
    {
        readonly System.ComponentModel.CancelEventArgs wfe;

        internal FormControllerEventArgs()
        {
        }
        internal FormControllerEventArgs(System.ComponentModel.CancelEventArgs wfe)
        {
            this.wfe = wfe;
        }

        bool cancel;
        public bool Cancel
        {
            get
            {
                return cancel;
            }
            set
            {
                cancel = value;
                if (wfe != null)
                {
                    wfe.Cancel = true;
                }
            }
        }
    }


    public class ShowDialogEventArgs : ControllerEventArgs
    {
        internal ShowDialogEventArgs(FormController dialog)
            : base()
        {
            this.dialog = dialog;
        }

        internal ShowDialogEventArgs(FormController dialog, Controller invoker)
            : base()
        {
            this.dialog = dialog;
            this.invoker = invoker;
        }

        FormController dialog;
        public FormController Dialog
        {
            get
            {
                return dialog;
            }
        }

        Controller invoker;
        public Controller Invoker
        {
            get
            {
                return invoker;
            }
        }

        DialogResult dialogResult;
        public DialogResult DialogResult
        {
            get
            {
                return dialogResult;
            }
            set
            {
                dialogResult = value;
            }
        }
    }
}
