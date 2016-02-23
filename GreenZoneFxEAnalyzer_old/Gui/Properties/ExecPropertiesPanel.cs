using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using System.Reflection;
using Flobbster.Windows.Forms;
using GreenZoneUtil.Util;

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
            R NewScriptInstance = ExecRuntime<R, I, S, A>.Create(ExecRuntime.Parent, ExecRuntime.ExecutableInfo, null);
            PropertyInfo[] fields = GreenZoneUtils.GetTopLevelProperties(execRuntime);

            PropertyTable propertyTable = new PropertyTable();
            foreach (PropertyInfo pi in fields)
            {
                string category = GreenZoneUtils.GetCategory(pi);
                string description = GreenZoneUtils.GetDescription(pi);
                object defaultValue = pi.GetValue(NewScriptInstance, null);
                object value;
                if (useOriginalScript)
                {
                    value = pi.GetValue(execRuntime, null);
                }
                else
                {
                    value = defaultValue;
                }
                propertyTable.Properties.Add(new PropertySpec(pi.Name, pi.PropertyType, category, description, defaultValue));
                propertyTable[pi.Name] = value;
            }

            propertyGrid1.PropertySort = PropertySort.NoSort;
            propertyGrid1.SelectedObject = propertyTable;
            bool r = fields.Length > 0;
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
            PropertyTable propertyTable = (PropertyTable)propertyGrid1.SelectedObject;

            foreach (PropertySpec p in propertyTable.Properties)
            {
                GreenZoneUtils.SetProperty(execRuntime, p.Name, propertyTable[p.Name]);
            }
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
                R value = ExecRuntime<R, I, S, A>.Create(ExecRuntime.Parent, ExecRuntime.ExecutableInfo, null);
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
