using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GreenZoneUtil.Util;
using System.Collections;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{

    public interface ITabController : IController
    {
        IList<TabPageController> Pages
        {
            get;
        }

        int TabCount
        {
            get;
        }

        int SelectedIndex
        {
            get;
            set;
        }

        TabPageController SelectedTab
        {
            get;
            set;
        }

        void AddTabClick();

    }

    public class TabController : Controller, ITabController
    {
        public event PropertyChangedEventHandler SelectedIndexChanged;
        public event PropertyChangedEventHandler SelectedTabChanged;
        public event ControllerEventHandler AddTabClicked;

        public TabController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public TabController(GreenRmiManager rmiManager, Controller parent, params TabPageController[] pages)
            : base(rmiManager, parent)
        {
            foreach (TabPageController page in pages)
            {
                Add(page);
            }
        }

        public TabController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected TabController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            SelectedIndex = (int) info.GetValue("SelectedIndex", typeof(int));
        }

        IList<TabPageController> pages;
        public override IList<Controller> Controls
        {
            get
            {
                return base.Controls;
            }
            set
            {
                base.Controls = value;
                pages = new ReadOnlyBridgeCollection<TabPageController>((IList)value);
            }
        }

        public IList<TabPageController> Pages
        {
            get
            {
                return pages;
            }
        }

        public int TabCount
        {
            get
            {
                return ChildCount;
            }
        }

        int selectedIndex;
        const int PROPERTY_14_SELECTEDINDEX_ID = 14;
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
                    changed[PROPERTY_14_SELECTEDINDEX_ID] = true;
                    somethingChanged = true;
                    if (selectedIndex == -1)
                    {
                        selectedTab = null;
                    }
                    else
                    {
                        selectedTab = Pages[selectedIndex];
                    }
                    if (SelectedIndexChanged != null)
                    {
                        SelectedIndexChanged(this, new PropertyChangedEventArgs("SelectedIndex", value));
                    }
                    if (SelectedTabChanged != null)
                    {
                        SelectedTabChanged(this, new PropertyChangedEventArgs("SelectedTab", value));
                    }
                }
            }
        }

        TabPageController selectedTab;
        public TabPageController SelectedTab
        {
            get
            {
                return selectedTab;
            }
            set
            {
                if (selectedTab != value)
                {
                    selectedTab = value;
                    selectedIndex = Pages.IndexOf(selectedTab);
                    changed[PROPERTY_14_SELECTEDINDEX_ID] = true;
                    somethingChanged = true;
                    if (SelectedTabChanged != null)
                    {
                        SelectedTabChanged(this, new PropertyChangedEventArgs("SelectedTab", value));
                    }
                    if (SelectedIndexChanged != null)
                    {
                        SelectedIndexChanged(this, new PropertyChangedEventArgs("SelectedIndex", value));
                    }
                }
            }
        }

        public override void Add(Controller control)
        {
            base.Add((TabPageController)control);
        }

        public override void Insert(int index, Controller control)
        {
            base.Insert(index, (TabPageController)control);
        }

        public override void Remove(Controller control)
        {
            base.Remove((TabPageController)control);
        }

        public void AddTabClick()
        {
            OnAddTabClick(null);
        }

        internal void OnAddTabClick(ControllerEventArgs args)
        {
            if (AddTabClicked != null)
            {
                if (args == null)
                {
                    args = new TabControllerEventArgs();
                }
                else if (args is TabControllerEventArgs)
                {
                    return;
                }

                AddTabClicked(this, args);
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_14_SELECTEDINDEX_ID:
                    return SelectedIndex;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_14_SELECTEDINDEX_ID:
                    SelectedIndex = (int)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("SelectedIndex", SelectedIndex);
        }
    }
}
