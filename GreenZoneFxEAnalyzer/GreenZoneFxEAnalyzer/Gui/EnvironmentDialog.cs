using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.Gui.PropertyGrid;
using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.Gui.ViewController;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.ViewController.Properties;

namespace GreenZoneFxEngine
{
    public partial class EnvironmentDialog : Form
    {

        public EnvironmentDialog()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, EnvironmentSettingsController controller)
        {
            this.context = context;
            this.controller = controller;
            new FormVCBinder(context, this, controller);

            new SplitContainerVCBinder(context, splitContainer1, controller.SplitContainer1);
            new TreeViewVCBinder(context, treeView1, controller.TreeView1);
            new ButtonVCBinder(context, cancelButton, controller._CancelButton);
            new ButtonVCBinder(context, resetButton, controller.ResetButton);
            new ButtonVCBinder(context, saveButton, controller.SaveButton);

            controller.SplitContainer1.Panel2.ControlsChanged += new PropertyChangedEventHandler(Panel2_ControlsChanged);
            controller.SplitContainer1.Panel2.ChildControlInserted += new ListChangedEventHandler(Panel2_ChildControlAdded);
        }

        GreenWinFormsMVContext context;
        public GreenWinFormsMVContext Context
        {
            get
            {
                return context;
            }
        }

        EnvironmentSettingsController controller;
        EnvironmentSettingsController Controller
        {
            get
            {
                return controller;
            }
        }

        void Panel2_ControlsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (controller.SplitContainer1.Panel2.Controls.Count == 0)
            {
                splitContainer1.Panel2.Controls.Clear();
            }
            else
            {
                throw new NotSupportedException();
            }
        }


        void Panel2_ChildControlAdded(object sender, ListChangedEventArgs e)
        {
            Controller child = (Controller)e.Element;
            if (child is ProgramOptionsController)
            {
                OptionsPanel p = new OptionsPanel();
                p.Bind(context, (ProgramOptionsController)child);
            }
            else if (child is EnvironmentPropsController)
            {
                EnvironmentPanel p = new EnvironmentPanel();
                p.Bind(context, (EnvironmentPropsController)child);
            }
            else if (child is SymbolPropertiesController)
            {
                SymbolPanel p = new SymbolPanel();
                p.Bind(context, (SymbolPropertiesController)child);
            }
            else if (child is ExpertInfoController)
            {
                Mt4ExpertInfoPanel p = new Mt4ExpertInfoPanel();
                p.Bind(context, (ExpertInfoController)child);
            }
            else if (child is ScriptInfoController)
            {
                Mt4ScriptInfoPanel p = new Mt4ScriptInfoPanel();
                p.Bind(context, (ScriptInfoController)child);
            }
            else if (child is IndicatorInfoController)
            {
                Mt4IndicatorInfoPanel p = new Mt4IndicatorInfoPanel();
                p.Bind(context, (IndicatorInfoController)child);
            }
            else
            {
                throw new NotSupportedException("child:" + child);
            }

            Control childControl = (Control)child.BoundControl;
            childControl.Location = new Point(0, 0);
            childControl.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(childControl);
            splitContainer1.Panel2.Invalidate();
            splitContainer1.Panel2.Update();
        }

    }
}
