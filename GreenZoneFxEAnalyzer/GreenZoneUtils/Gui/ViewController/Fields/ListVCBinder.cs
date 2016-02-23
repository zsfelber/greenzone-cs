using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;

using System.Collections;

namespace GreenZoneUtil.Gui.ViewController
{
    public class ListVCBinder : ControlVCBinder1<ListBox, ListController>
    {
        public ListVCBinder(WinFormsMVContext context, ListBox control, ListController controller)
            : base(context, control, controller)
        {

            foreach (var item in controller.Items)
            {
                AddItem(item);
            }
            SelectedItems = controller.SelectedItems;

            controller.RowInserted += new ListChangedEventHandler(controller_RowAdded);
            controller.RowRemovedAt += new ListChangedEventHandler(controller_RowRemovedAt);
            controller.RowsChanged += new PropertyChangedEventHandler(controller_RowsChanged);
            controller.SelectedRowsChanged += new PropertyChangedEventHandler(controller_SelectedRowsChanged);

            control.SelectedIndexChanged += new EventHandler(control_SelectedIndexChanged);
        }

        protected bool _sel = false;

        public virtual IList SelectedItems
        {
            get
            {
                return control.SelectedItems;
            }
            set
            {
                if (!_sel)
                {
                    _sel = true;
                    try
                    {
                        control.ClearSelected();
                        int i = 0;
                        foreach (var v in control.Items)
                        {
                            if (value.Contains(v))
                            {
                                control.SetSelected(i, true);
                            }
                            i++;
                        }
                    }
                    finally
                    {
                        _sel = false;
                    }
                }
            }
        }

        protected virtual void AddItem(object item)
        {
            control.Items.Add(item);
        }

        void controller_RowAdded(object sender, ListChangedEventArgs e)
        {
            GridRowController row = (GridRowController)e.Element;
            AddItem(row.DataBoundObject);
        }

        void controller_RowRemovedAt(object sender, ListChangedEventArgs e)
        {
            control.Items.RemoveAt(e.NewIndex);
        }

        void controller_RowsChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Items.Clear();
            foreach (var item in controller.Items)
            {
                AddItem(item);
            }
        }

        void controller_SelectedRowsChanged(object sender, PropertyChangedEventArgs e)
        {
            SelectedItems = controller.SelectedItems;
        }


        /////////////////////////////////////////////////////////////////////



        void control_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                if (!_sel)
                {
                    _sel = true;
                    try
                    {
                        controller.SelectedItems = SelectedItems;
                    }
                    finally
                    {
                        _sel = false;
                    }
                }
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

    }
}
