using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;

using System.Collections;
using GreenZoneUtil.Util;

namespace GreenZoneUtil.Gui.ViewController
{
    public class CheckBoxListVCBinder : ListVCBinder
    {
        public CheckBoxListVCBinder(WinFormsMVContext context, CheckedListBox control, ListController controller)
            : base(context, control, controller)
        {
        }

        public override IList SelectedItems
        {
            get
            {
                return ((CheckedListBox)control).CheckedItems;
            }
            set
            {
                if (!_sel)
                {
                    _sel = true;
                    try
                    {
                        int i = 0;
                        foreach (var v in new ArrayList(control.Items))
                        {
                            bool ch = value.Contains(v);
                            ((CheckedListBox)control).SetItemChecked(i, ch);
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
    }
}
