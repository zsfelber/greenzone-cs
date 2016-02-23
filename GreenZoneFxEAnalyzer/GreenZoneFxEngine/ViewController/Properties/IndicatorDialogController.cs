using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using System.Reflection;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Etc;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;
using GreenZoneParser.Reflect;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    public class IndicatorDialogController : IndicatorDialogControllerBase
    {
        readonly ServerIndicatorRuntime indicatorRuntime;
        readonly ServerIndicatorRuntime newIndicatorRuntime;
        bool hasIndicatorProperties;

        public IndicatorDialogController(GreenRmiManager rmiManager, ServerIndicatorRuntime indicatorRuntime)
            : this(rmiManager, indicatorRuntime.Parent, indicatorRuntime.IndicatorInfo, indicatorRuntime)
        {
        }

        public IndicatorDialogController(GreenRmiManager rmiManager, IServerChartRuntime chartRuntime, Mt4ExecutableInfo info)
            : this(rmiManager, chartRuntime, info, null)
        {
        }

        public IndicatorDialogController(GreenRmiManager rmiManager, IServerChartRuntime chartRuntime, Mt4ExecutableInfo info, ServerIndicatorRuntime indicatorRuntime)
            : base(rmiManager)
        {
            OkButton = new ButtonController(rmiManager, this);
            _CancelButton = new ButtonController(rmiManager, this);
            LoadButton = new ButtonController(rmiManager, this);
            SaveButton = new ButtonController(rmiManager, this);
            RemoveButton = new ButtonController(rmiManager, this);

            AcceptButton = OkButton;
            CancelButton = _CancelButton;

            this.newIndicatorRuntime = (ServerIndicatorRuntime)ServerIndicatorRuntime.Create(rmiManager, chartRuntime, info, chartRuntime.GuiSeriesManager.DefaultCache);
            this.newIndicatorRuntime.Init();

            if (indicatorRuntime != null)
            {
                this.indicatorRuntime = indicatorRuntime;
            }
            else
            {
                this.indicatorRuntime = (ServerIndicatorRuntime)ServerIndicatorRuntime.Create(rmiManager, chartRuntime, info, chartRuntime.GuiSeriesManager.DefaultCache);
                this.indicatorRuntime.Init();
            }

            Text = info.FullName;
            IndicatorPanel1 = new IndicatorPanelController(rmiManager, this);

            hasIndicatorProperties = IndicatorPanel1.IndicatorRuntimePanel.Set(this.indicatorRuntime);
            Reset2(true);
            Reset3(true);


            if (!hasIndicatorProperties)
            {
                IndicatorPanel1.RemoveAt(0);
            }

            IndicatorPanel1.Reset2Button.Pressed += new ControllerEventHandler(reset2Button_Click);
            IndicatorPanel1.Reset3Button.Pressed += new ControllerEventHandler(reset3Button_Click);

            OkButton.Pressed += new ControllerEventHandler(okButton_Click);
            _CancelButton.Pressed += new ControllerEventHandler(cancel_Click);
            LoadButton.Pressed += new ControllerEventHandler(loadButton_Click);
            SaveButton.Pressed += new ControllerEventHandler(saveButton_Click);
            RemoveButton.Pressed += new ControllerEventHandler(removeButton_Click);
        }

        public new IndicatorPanelController IndicatorPanel1
        {
            get
            {
                return (IndicatorPanelController)base.IndicatorPanel1;
            }
            protected set
            {
                base.IndicatorPanel1 = value;
            }
        }


        void Reset2(bool useOriginalScript = false)
        {
            if (indicatorRuntime.Buffers != null && indicatorRuntime.Buffers.Length > 0)
            {
                Reset(IndicatorPanel1.IndexesPrgrd, newIndicatorRuntime.Buffers, indicatorRuntime.Buffers, "Buffer", useOriginalScript);
            }
            else
            {
                IndicatorPanel1.Remove(IndicatorPanel1.TabPage2);
            }
        }

        void Reset3(bool useOriginalScript = false)
        {
            if (indicatorRuntime.Levels != null && indicatorRuntime.Levels.Length > 0)
            {
                Reset(IndicatorPanel1.LevelsPrgrd, newIndicatorRuntime.Levels, indicatorRuntime.Levels, "Level", useOriginalScript);
            }
            else
            {
                IndicatorPanel1.Remove(IndicatorPanel1.TabPage3);
            }
        }

        void Save()
        {
            if (indicatorRuntime.Buffers != null && indicatorRuntime.Buffers.Length > 0)
            {
                indicatorRuntime.Session.NumBuffers = indicatorRuntime.NumIndicatorBuffers;
                Save(IndicatorPanel1.IndexesPrgrd, indicatorRuntime.Buffers, indicatorRuntime.Session.Buffers, "Buffer");
            }
            if (indicatorRuntime.Levels != null && indicatorRuntime.Levels.Length > 0)
            {
                indicatorRuntime.Session.NumLevels = indicatorRuntime.NumIndicatorLevels;
                Save(IndicatorPanel1.LevelsPrgrd, indicatorRuntime.Levels, indicatorRuntime.Session.Levels, "Level");
            }
        }

        internal void Reset<T>(BufferedPropertyGridController grid, T[] newo, T[] objects, string info, bool useOriginalScript = false) where T : IParams
        {
            ReflObjType rtype = (ReflObjType)rmiManager.Resolver.GetType(typeof(T));
            List<ReflProperty> fields = GreenZoneUtilsBase.GetTopLevelProperties(rtype);

            List<Property> properties = new List<Property>();

            int i = 0;

            if (objects != null)  foreach (T obj in objects)
            {
                Dictionary<string,object> ps = obj.Params;
                string category = info + " " + i;
                foreach (ReflProperty pi in fields)
                {
                    if (ps.ContainsKey(pi.Name))
                    {
                        string description = GreenZoneUtilsBase.GetDescription(pi);
                        object defaultValue = pi.GetValue(newo[i]);
                        object value;
                        if (useOriginalScript)
                        {
                            value = pi.GetValue(obj);
                        }
                        else
                        {
                            value = defaultValue;
                        }
                        Property p;
                        if (objects.Length == 1)
                        {
                            p = new Property(category, pi.Name, description, value, defaultValue, pi.PropertyType.TypeId, pi.Id);
                        }
                        else
                        {
                            p = new Property(category, i + " " + pi.Name, description, value, defaultValue, pi.PropertyType.TypeId, pi.Id);
                        }
                        properties.Add(p);
                    }
                }
                i++;
            }

            grid.SetProperties(properties);
        }

        internal void Save<T>(BufferedPropertyGridController grid, T[] objects, List<Dictionary<string, object>> sessionobjs, string info) where T : IParams
        {
            List<Property> properties = grid.Properties;

            foreach (Property p in properties)
            {
                int id = Convert.ToInt32(p.Category.Substring(info.Length + 1));
                string name;
                if (objects.Length > 1)
                {
                    name = p.Name.Substring((id+" ").Length);
                }
                else
                {
                    name = p.Name;
                }
                sessionobjs[id][name] = p.Value;
            }

            int i = 0;
            foreach (var s in sessionobjs)
            {
                objects[i].Params = s;
                i++;
            }
        }

        public ServerIndicatorRuntime IndicatorRuntime
        {
            get
            {
                return indicatorRuntime;
            }
        }

        private void okButton_Click(object sender, ControllerEventArgs e)
        {
            // NOTE environment.Save
            IndicatorPanel1.IndicatorRuntimePanel.Save();
            Save();
            DialogResult = DialogResult.OK;
            Closing();
        }

        private void cancel_Click(object sender, ControllerEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        void reset2Button_Click(object sender, ControllerEventArgs e)
        {
            Reset2(false);
        }

        void reset3Button_Click(object sender, ControllerEventArgs e)
        {
            Reset3(false);
        }

        private void removeButton_Click(object sender, ControllerEventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void loadButton_Click(object sender, ControllerEventArgs e)
        {
            ServerIndicatorRuntime r = IndicatorPanel1.IndicatorRuntimePanel.EditedExecRuntime;
            EngineUtils.ShowLoadFromSetController(rmiManager, r);
            IndicatorPanel1.IndicatorRuntimePanel.EditedExecRuntime= r;
        }

        private void saveButton_Click(object sender, ControllerEventArgs e)
        {
            EngineUtils.ShowSaveToSetController(rmiManager, IndicatorPanel1.IndicatorRuntimePanel.EditedExecRuntime);
        }
    }
}
