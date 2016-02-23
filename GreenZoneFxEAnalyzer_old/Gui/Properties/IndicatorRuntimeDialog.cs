using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using System.Reflection;
using GreenZoneFxEngine.Util;
using Flobbster.Windows.Forms;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Etc;

namespace GreenZoneFxEngine
{
    public partial class IndicatorRuntimeDialog : Form
    {
        readonly IndicatorRuntime indicatorRuntime;
        readonly IndicatorRuntime newIndicatorRuntime;
        bool hasIndicatorProperties;

        public IndicatorRuntimeDialog(IndicatorRuntime indicatorRuntime)
            : this(indicatorRuntime.Parent, indicatorRuntime.IndicatorInfo, indicatorRuntime)
        {
        }

        public IndicatorRuntimeDialog(ChartRuntime chartRuntime, Mt4IndicatorInfo info)
            : this(chartRuntime, info, null)
        {
        }

        public IndicatorRuntimeDialog(ChartRuntime chartRuntime, Mt4IndicatorInfo info, IndicatorRuntime indicatorRuntime)
        {
            this.newIndicatorRuntime = IndicatorRuntime.Create(chartRuntime, info, chartRuntime.GuiSeriesManager.DefaultCache);
            this.newIndicatorRuntime.Init();

            if (indicatorRuntime != null)
            {
                this.indicatorRuntime = indicatorRuntime;
            }
            else
            {
                this.indicatorRuntime = IndicatorRuntime.Create(chartRuntime, info, chartRuntime.GuiSeriesManager.DefaultCache);
                this.indicatorRuntime.Init();
            }

            InitializeComponent();
            Text = info.FullName;

            hasIndicatorProperties = indicatorPanel1.indicatorRuntimePanel.Set(this.indicatorRuntime);
            Reset2(true);
            Reset3(true);


            if (!hasIndicatorProperties)
            {
                indicatorPanel1.tabControl1.TabPages.RemoveAt(0);
            }

            indicatorPanel1.reset2Button.Click += new EventHandler(reset2Button_Click);
            indicatorPanel1.reset3Button.Click += new EventHandler(reset3Button_Click);
        }

        void Reset2(bool useOriginalScript = false)
        {
            if (indicatorRuntime.Buffers != null && indicatorRuntime.Buffers.Length > 0)
            {
                Reset(indicatorPanel1.indexesPrgrd, newIndicatorRuntime.Buffers, indicatorRuntime.Buffers, "Buffer", useOriginalScript);
            }
            else
            {
                indicatorPanel1.tabControl1.TabPages.Remove(indicatorPanel1.tabPage2);
            }
        }

        void Reset3(bool useOriginalScript = false)
        {
            if (indicatorRuntime.Levels != null && indicatorRuntime.Levels.Length > 0)
            {
                Reset(indicatorPanel1.levelsPrgrd, newIndicatorRuntime.Levels, indicatorRuntime.Levels, "Level", useOriginalScript);
            }
            else
            {
                indicatorPanel1.tabControl1.TabPages.Remove(indicatorPanel1.tabPage3);
            }
        }

        void Save()
        {
            if (indicatorRuntime.Buffers != null && indicatorRuntime.Buffers.Length > 0)
            {
                indicatorRuntime.Session.NumBuffers = indicatorRuntime.NumIndicatorBuffers;
                Save(indicatorPanel1.indexesPrgrd, indicatorRuntime.Buffers, indicatorRuntime.Session.Buffers, "Buffer");
            }
            if (indicatorRuntime.Levels != null && indicatorRuntime.Levels.Length > 0)
            {
                indicatorRuntime.Session.NumLevels = indicatorRuntime.NumIndicatorLevels;
                Save(indicatorPanel1.levelsPrgrd, indicatorRuntime.Levels, indicatorRuntime.Session.Levels, "Level");
            }
        }

        internal void Reset<T>(PropertyGrid grid, T[] newo, T[] objects, string info, bool useOriginalScript = false) where T : IParams
        {
            PropertyInfo[] fields = GreenZoneUtils.GetTopLevelProperties(typeof(T));

            PropertyTable propertyTable = new PropertyTable();

            int i = 0;

            if (objects != null)  foreach (T obj in objects)
            {
                Dictionary<string,object> ps = obj.Params;
                string category = info + " " + i;
                foreach (PropertyInfo pi in fields)
                {
                    if (ps.ContainsKey(pi.Name))
                    {
                        string description = GreenZoneUtils.GetDescription(pi);
                        object defaultValue = pi.GetValue(newo[i], null);
                        object value;
                        if (useOriginalScript)
                        {
                            value = pi.GetValue(obj, null);
                        }
                        else
                        {
                            value = defaultValue;
                        }
                        if (objects.Length == 1)
                        {
                            propertyTable.Properties.Add(new PropertySpec(pi.Name, pi.PropertyType, category, description, defaultValue));
                            propertyTable[pi.Name] = value;
                        }
                        else
                        {
                            propertyTable.Properties.Add(new PropertySpec(i + " " + pi.Name, pi.PropertyType, category, description, defaultValue));
                            propertyTable[i + " " + pi.Name] = value;
                        }
                    }
                }
                i++;
            }

            grid.SelectedObject = propertyTable;
        }

        internal void Save<T>(PropertyGrid grid, T[] objects, List<Dictionary<string, object>> sessionobjs, string info) where T : IParams
        {
            PropertyTable propertyTable = (PropertyTable)grid.SelectedObject;

            foreach (PropertySpec p in propertyTable.Properties)
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
                sessionobjs[id][name] = propertyTable[p.Name];
            }

            int i = 0;
            foreach (var s in sessionobjs)
            {
                objects[i].Params = s;
                i++;
            }
        }

        public IndicatorRuntime IndicatorRuntime
        {
            get
            {
                return indicatorRuntime;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            // NOTE environment.Save
            indicatorPanel1.indicatorRuntimePanel.Save();
            Save();
            DialogResult = DialogResult.OK;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        void reset2Button_Click(object sender, EventArgs e)
        {
            Reset2(false);
        }

        void reset3Button_Click(object sender, EventArgs e)
        {
            Reset3(false);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            IndicatorRuntime r = indicatorPanel1.indicatorRuntimePanel.EditedExecRuntime;
            EngineUtils.ShowLoadFromSetDialog(r);
            indicatorPanel1.indicatorRuntimePanel.EditedExecRuntime= r;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            EngineUtils.ShowSaveToSetDialog(indicatorPanel1.indicatorRuntimePanel.EditedExecRuntime);
        }

    }
}
