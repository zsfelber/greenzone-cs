using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using System.Collections;
using System.Collections.ObjectModel;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class ComboController : ButtonController
    {
        public event PropertyChangedEventHandler SelectedItemChanged;
        public event PropertyChangedEventHandler SelectedIndexChanged;
        public event PropertyChangedEventHandler ItemsChanged;
        public event ListChangedEventHandler ItemAdded;
        public event ListChangedEventHandler ItemRemoved;

        public ComboController(GreenRmiManager rmiManager, Controller parent, bool splitButton)
            : base(rmiManager, parent)
        {
            this.splitButton = splitButton;
            Clear();
        }

        public ComboController(GreenRmiManager rmiManager, Controller parent, string text)
            : base(rmiManager, parent, text)
        {
            Clear();
        }

        public ComboController(GreenRmiManager rmiManager, Controller parent, string text, int image)
            : base(rmiManager, parent, text, image)
        {
            Clear();
        }

        public ComboController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            Clear();
            splitButton = (bool)buffer.ChangedProps[PROPERTY_16_SPLITBUTTON_ID];
        }

        protected ComboController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            splitButton = (bool)info.GetValue("SplitButton", typeof(bool));
            SelectedIndex = (int) info.GetValue("SelectedIndex", typeof(int));
            Items = (List<object>) info.GetValue("Items", typeof(List<object>));
        }

        const int PROPERTY_16_SPLITBUTTON_ID = 16;
        readonly bool splitButton;
        public bool SplitButton
        {
            get
            {
                return splitButton;
            }
        }

        object selectedItem;
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    selectedIndex = items.IndexOf(selectedItem);
                    changed[PROPERTY_17_SELECTEDINDEX_ID] = true;
                    somethingChanged = true;
                    if (SelectedItemChanged != null)
                    {
                        SelectedItemChanged(this, new PropertyChangedEventArgs("SelectedItem", value));
                    }
                    if (SelectedIndexChanged != null)
                    {
                        SelectedIndexChanged(this, new PropertyChangedEventArgs("SelectedIndex", value));
                    }

                    if (!splitButton)
                    {
                        Press();
                    }
                }
            }
        }

        int selectedIndex;
        const int PROPERTY_17_SELECTEDINDEX_ID = 17;
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    if (selectedIndex == -1)
                    {
                        selectedItem = null;
                    }
                    else
                    {
                        selectedItem = items[selectedIndex];
                    }
                    changed[PROPERTY_17_SELECTEDINDEX_ID] = true;
                    somethingChanged = true;
                    if (SelectedIndexChanged != null)
                    {
                        SelectedIndexChanged(this, new PropertyChangedEventArgs("SelectedIndex", value));
                    }
                    if (SelectedItemChanged != null)
                    {
                        SelectedItemChanged(this, new PropertyChangedEventArgs("SelectedItem", value));
                    }

                    if (!splitButton)
                    {
                        Press();
                    }
                }
            }
        }

        IList items;
        IList itemsUm;
        const int PROPERTY_18_ITEMS_ID = 18;
        public IList Items
        {
            get
            {
                return itemsUm;
            }
            set
            {
                if (items != value)
                {
                    items = value;
                    itemsUm = new ReadOnlyCollection(items);
                    base.Controls = new BridgeCollection<Controller>(items);
                    changed[PROPERTY_18_ITEMS_ID] = true;
                    somethingChanged = true;
                    if (ItemsChanged != null)
                    {
                        ItemsChanged(this, new PropertyChangedEventArgs("Items", value));
                    }
                }
            }
        }

        public override IList<Controller> Controls
        {
            get
            {
                return base.Controls;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public void Add(object item)
        {
            if (item is Controller)
            {
                Controller control = (Controller)item;
                base.Add(control);
            }
            else
            {
                items.Add(item);
                if (ItemAdded != null)
                {
                    ItemAdded(null, new ListChangedEventArgs(ListChangedType.ItemAdded, items.Count-1, item));
                }
            }
        }

        public void Remove(object item)
        {
            if (item is Controller)
            {
                Controller control = (Controller)item;
                base.Remove(control);
            }
            else
            {
                items.Remove(item);
                if (ItemRemoved != null)
                {
                    ItemRemoved(null, new ListChangedEventArgs(ListChangedType.ItemDeleted, -1, item));
                }
            }
        }

        public override void Clear()
        {
            Items = new List<object>();
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_16_SPLITBUTTON_ID:
                    return SplitButton;
                case PROPERTY_17_SELECTEDINDEX_ID:
                    return SelectedIndex;
                case PROPERTY_18_ITEMS_ID:
                    return Items;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_16_SPLITBUTTON_ID:
                    break;
                case PROPERTY_17_SELECTEDINDEX_ID:
                    SelectedIndex = (int)value;
                    break;
                case PROPERTY_18_ITEMS_ID:
                    Items = (List<object>)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("SplitButton", SplitButton);
            info.AddValue("SelectedIndex", SelectedIndex);
            info.AddValue("Items", Items);
        }
    }
}
