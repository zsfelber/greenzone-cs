using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneUtil.ViewController
{
    
    public delegate void ControllerPaintEventHandler(object sender, ControllerPaintEventArgs args);

    
    public class ControllerPaintEventArgs : ControllerEventArgs
    {
        internal ControllerPaintEventArgs(GraphicsController graphics)
        {
            this.graphics = graphics;
        }

        readonly GraphicsController graphics;
        public GraphicsController Graphics
        {
            get
            {
                return graphics;
            }
        }
    }

}
