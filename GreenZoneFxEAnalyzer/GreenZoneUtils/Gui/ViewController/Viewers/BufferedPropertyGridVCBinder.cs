using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;

namespace GreenZoneUtil.Gui.ViewController
{
    // TODO PropertyTab
    // TODO SelectedObjects

    public class BufferedPropertyGridVCBinder : ControlVCBinder1<System.Windows.Forms.PropertyGrid, BufferedPropertyGridController>
    {

        public BufferedPropertyGridVCBinder(WinFormsMVContext context, System.Windows.Forms.PropertyGrid control, BufferedPropertyGridController controller)
            : base(context, control, controller)
        {
            control.SelectedObject = controller.SelectedObject;

            controller.SelectedObjectChanged += new PropertyChangedEventHandler(controller_SelectedObjectChanged);
            controller.PropertySortChanged += new PropertyChangedEventHandler(controller_PropertySortChanged);

            control.PropertySortChanged += new EventHandler(control_PropertySortChanged);
        }

        bool _sel = false;
        void controller_SelectedObjectChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_sel)
            {
                try
                {
                    _sel = true;
                    control.SelectedObject = controller.SelectedObject;
                }
                finally
                {
                    _sel = false;
                }
            }
        }

        bool _sort = false;
        void controller_PropertySortChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_sort)
            {
                try
                {
                    _sort = true;
                    control.PropertySort = controller.PropertySort;
                }
                finally
                {
                    _sort = false;
                }
            }
        }


        ////////////////////////////////////////////////////////////


        void control_PropertySortChanged(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                if (!_sort)
                {
                    try
                    {
                        _sort = true;
                        controller.PropertySort = control.PropertySort;
                    }
                    finally
                    {
                        _sort = false;
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
