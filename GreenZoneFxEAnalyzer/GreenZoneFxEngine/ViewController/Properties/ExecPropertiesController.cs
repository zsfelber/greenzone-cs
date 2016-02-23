using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using System.Reflection;
using GreenZoneUtil.Util;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;
using GreenZoneParser.Reflect;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    public class ExecPropertiesController<R, S, A> : ExecPropertiesControllerBase
        where R : IServerExecRuntime
        where S : IExecSession
        where A : ExecAttribute
    {
        public ExecPropertiesController(GreenRmiManager rmiManager, Controller parent, R execRuntime)
            : base(rmiManager, parent)
        {
            PropertyGrid1 = new BufferedPropertyGridController(rmiManager, this);
            if (execRuntime != null)
            {
                Set(execRuntime);
            }
        }

        internal bool Set(R execRuntime)
        {
            ExecRuntime = execRuntime;

            bool r = Reset(true);
            return r;
        }

        internal bool Reset(bool useOriginalScript = false)
        {
            bool result = Reset(ExecRuntime, useOriginalScript);
            return result;
        }

        internal bool Reset(R execRuntime, bool useOriginalScript = false)
        {
            R NewScriptInstance = (R)UserRuntimeEx.Create(rmiManager, (ServerChartRuntime)ExecRuntime.Parent, ExecRuntime.ExecutableInfo, null);

            PropertyGrid1.BindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            PropertyGrid1.PropertySort = PropertySort.NoSort;
            PropertyGrid1.SelectedObject = execRuntime;
            bool r = PropertyGrid1.Properties.Count > 0;
            return r;
        }

        internal void Save()
        {
            Save(ExecRuntime);

            ExecRuntime.CopyTopLevelParamsToSession();
            ExecRuntime.Parent.Environment.Session.Save();
        }

        private void Save(R execRuntime)
        {
            PropertyGrid1.Save(execRuntime);
        }

        public R ExecRuntime
        {
            get;
            private set;
        }

        public R EditedExecRuntime
        {
            get
            {
                R value = (R)UserRuntimeEx.Create(rmiManager, ExecRuntime.Parent, ExecRuntime.ExecutableInfo, null);
                Save(value);
                return value;
            }
            internal set
            {
                Reset(value, true);
            }
        }
    }
}
