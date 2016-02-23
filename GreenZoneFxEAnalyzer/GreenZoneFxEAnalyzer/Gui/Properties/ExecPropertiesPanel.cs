using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using System.Reflection;
using Flobbster.Windows.Forms;
using GreenZoneUtil.Util;
using GreenZoneUtil.Gui.ViewController;
using GreenZoneFxEngine.ViewController.Properties;

namespace GreenZoneFxEngine
{
    public partial class ExecPropertiesPanel<R, I, S, A> : UserControl
        where R : ExecRuntime<R, I, S, A>
        where I : Mt4ExecutableInfo<R, A>
        where S : ExecSession<R,A>
        where A : ExecAttribute
    {

        public ExecPropertiesPanel()
        {
            InitializeComponent();
        }

        public virtual void Bind(GreenWinFormsMVContext context, ExecPropertiesController<R, I, S, A> controller)
        {
            new SimpleControlVCBinder(context, this, controller);

            new BufferedPropertyGridVCBinder(context, propertyGrid1, controller.PropertyGrid1);
        }
    }
}
