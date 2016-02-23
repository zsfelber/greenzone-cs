using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreenZoneUtil.Gui
{
    public class NonselButton : Button
    {
        public NonselButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
