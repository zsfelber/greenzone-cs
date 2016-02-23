using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using System.IO;
using GreenZoneFxEngine.Types;
using System.Collections;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    
    public class DukascopyPage1Controller : DukascopyPage1ControllerBase
    {
        Dictionary<string, TimePeriodConst> DTimePeriodConst = new Dictionary<string, TimePeriodConst>();
        List<TimePeriodConst> customPeriods = new List<TimePeriodConst>();
        List<TimePeriodConst> allSelectedPeriods = new List<TimePeriodConst>();

        public DukascopyPage1Controller(GreenRmiManager rmiManager, EnvironmentAssistantController assistant)
            : base(rmiManager, assistant)
        {
            ErrorProvider1 = new ChildControlMap<string>(rmiManager);
            ToolTip1 = new ChildControlMap<string>(rmiManager);
            OpenFileDialog1 = new FileDialogController(rmiManager);
            GenerateChl = new ListController(rmiManager, this);
            GenerateDefTb = new LabelledController(rmiManager, this);
            UpdateNoneRb = new RadioButtonController(rmiManager, this);
            DownloadTickGenPeriodsRb = new RadioButtonController(rmiManager, this);
            UpdateTicksGenPeriodsRb = new RadioButtonController(rmiManager, this);
            UpdateAllRb = new RadioButtonController(rmiManager, this);
            DeleteCorruptPeriodsCb = new ToggleButtonController(rmiManager, this);
            ToDateP = new FieldController<DateTime>(rmiManager, this);
            FromDateP = new FieldController<DateTime>(rmiManager, this);

            UpdateNoneRb.CheckedChanged += new PropertyChangedEventHandler(updateNoneRb_CheckedChanged);
            DownloadTickGenPeriodsRb.CheckedChanged += new PropertyChangedEventHandler(downloadTickGenPeriodsRb_CheckedChanged);
            UpdateTicksGenPeriodsRb.CheckedChanged += new PropertyChangedEventHandler(updateTicksGenPeriodsRb_CheckedChanged);
            UpdateAllRb.CheckedChanged += new PropertyChangedEventHandler(updateAllRb_CheckedChanged);
            DeleteCorruptPeriodsCb.CheckedChanged += new PropertyChangedEventHandler(deleteCorruptPeriodsCb_CheckedChanged);
        }


        new internal EnvironmentAssistantController Assistant
        {
            get
            {
                return (EnvironmentAssistantController)base.Assistant;
            }
        }

        public override UpdateMode UpdateMode
        {
            get
            {
                return base.UpdateMode;
            }
            set
            {
                if (base.UpdateMode != value)
                {
                    base.UpdateMode = value;

                    switch (value)
                    {
                        case UpdateMode.NONE:
                            GenerateDefTb.Text = "";
                            Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.Finish);
                            break;
                        default:
                            StringBuilder genDef = new StringBuilder();
                            foreach (var p in EnumExtensions.GetPeriods(TimePeriodCategory.TICKS | TimePeriodCategory.MT4))
                            {
                                genDef.Append(p.GetLongTxt());
                                genDef.Append(',');
                            }
                            genDef.Length--;
                            GenerateDefTb.Text = genDef.ToString();
                            Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.Next);
                            break;
                    }

                    DukascopyPage0Controller page0 = (DukascopyPage0Controller)PreviousPage;
                    string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
                    string envDir = eaRootDir + "\\" + page0.EnvironmentName;
                    string hp = envDir + "\\history\\";

                    if (page0.SelectedSymbols.Count > 1)
                    {
                        DeleteCorruptPeriodsCb.Text = "Delete unselected old period files (for all selected periods !)";
                    }
                    else
                    {
                        DeleteCorruptPeriodsCb.Text = "Delete unselected old period files";
                    }

                    IList selitms;
                    switch (value)
                    {
                        case UpdateMode.NONE:
                            GenerateChl.Enabled = false;
                            DeleteCorruptPeriodsCb.Enabled = false;
                            DeleteCorruptPeriodsCb.Checked = false;
                            FromDateP.Enabled = false;
                            ToDateP.Enabled = false;
                            GenerateChl.Clear();
                            DTimePeriodConst.Clear();
                            break;
                        case UpdateMode.UPDATE_ALL:
                            GenerateChl.Enabled = false;
                            DeleteCorruptPeriodsCb.Enabled = false;
                            DeleteCorruptPeriodsCb.Checked = false;
                            FromDateP.Enabled = true;
                            ToDateP.Enabled = true;
                            GenerateChl.Clear();
                            DTimePeriodConst.Clear();
                            selitms = GenerateChl.SelectedItems;
                            foreach (var p in EnumExtensions.GetPeriods(TimePeriodCategory.MT5 | TimePeriodCategory.SECS))
                            {
                                bool found = false;
                                foreach (var symbol in page0.SelectedSymbols)
                                {
                                    if (ServerTimeSeriesRuntimeEx.IsSeriesAvailable(hp, symbol, p))
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if (found)
                                {
                                    GenerateChl.AddItem(p.GetLongTxt());
                                    selitms.Add(p.GetLongTxt());
                                    DTimePeriodConst[p.GetLongTxt()] = p;
                                }
                            }
                            GenerateChl.SelectedItems = selitms;

                            if (Directory.Exists(hp))
                            {
                                datetime minlast = datetime.MaxValue;
                                foreach (var symbol in page0.SelectedSymbols)
                                {
                                    foreach (var f in Directory.GetFiles(hp, symbol + "*.ticks"))
                                    {
                                        try
                                        {
                                            ServerTimeSeriesRuntimeEx r = new ServerTickTimeSeriesRuntime(f, false, datetime.MaxValue);
                                            minlast = datetime.Min(minlast, r.GetTime(0));
                                        }
                                        catch (TimeSeriesException)
                                        {
                                        }
                                    }
                                    foreach (var f in Directory.GetFiles(hp, symbol + "*.hst"))
                                    {
                                        try
                                        {
                                            ServerTimeSeriesRuntimeEx r = new ServerPeriodTimeSeriesRuntime(f, false, datetime.MaxValue);
                                            minlast = datetime.Min(minlast, r.GetTime(0));
                                        }
                                        catch (TimeSeriesException)
                                        {
                                        }
                                    }
                                }
                                if (minlast == datetime.MaxValue)
                                {
                                    FromDateP.Value = new DateTime(2007, 03, 29);
                                }
                                else
                                {
                                    FromDateP.Value = (DateTime)minlast;
                                }
                            }
                            else
                            {
                                FromDateP.Value = new DateTime(2007, 03, 29);
                            }

                            break;
                        default:
                            GenerateChl.Enabled = true;
                            DeleteCorruptPeriodsCb.Enabled = true;
                            DeleteCorruptPeriodsCb.Checked = true;
                            FromDateP.Enabled = true;
                            ToDateP.Enabled = true;
                            GenerateChl.Clear();
                            DTimePeriodConst.Clear();

                            FromDateP.Value = new DateTime(2007, 03, 29);

                            selitms = GenerateChl.SelectedItems;
                            foreach (var p in EnumExtensions.GetPeriods(TimePeriodCategory.MT5 | TimePeriodCategory.SECS))
                            {
                                bool found = false;
                                foreach (var symbol in page0.SelectedSymbols)
                                {
                                    if (ServerTimeSeriesRuntimeEx.IsSeriesAvailable(hp, symbol, p))
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                GenerateChl.AddItem(p.GetLongTxt());
                                if (found)
                                {
                                    selitms.Add(p.GetLongTxt());
                                }
                                DTimePeriodConst[p.GetLongTxt()] = p;
                            }
                            GenerateChl.SelectedItems = selitms;
                            break;
                    }
                }
            }
        }

        protected override bool OnSetActive()
        {
            if (!base.OnSetActive())
                return false;

            UpdateMode = UpdateMode.NONE;

            if (string.IsNullOrEmpty(Assistant.UpdatedEnvironment))
            {
                UpdateTicksGenPeriodsRb.Checked = true;
            }
            else
            {
                UpdateAllRb.Checked = true;
            }

            return true;
        }

        protected override string OnAssistantNext()
        {
            if (validate())
            {
                if (UpdateMode == UpdateMode.NONE)
                {
                    throw new NotSupportedException();
                }
                else
                {
                    allSelectedPeriods.Clear();
                    customPeriods.Clear();
                    foreach (var p in EnumExtensions.GetPeriods(TimePeriodCategory.MT4))
                    {
                        allSelectedPeriods.Add(p);
                    }
                    foreach (var s in GenerateChl.SelectedItems)
                    {
                        TimePeriodConst p = DTimePeriodConst[(string)s];
                        customPeriods.Add(p);
                        allSelectedPeriods.Add(p);
                    }

                    return typeof(DukascopyPage2Controller).Name;
                }
            }
            else
            {
                return null;
            }
        }

        //protected override string OnAssistantBack()
        //{
        //    return typeof(DukascopyPage0).Name;
        //}

        private bool validate()
        {
            bool validationError = false;

            if (FromDateP.Value > ToDateP.Value)
            {
                ErrorProvider1[FromDateP] = ("From should not be higher than To.");
                ErrorProvider1[ToDateP] = ("From should not be higher than To.");
                validationError = true;
            }
            else
            {
                ErrorProvider1[FromDateP] = (null);
                ErrorProvider1[ToDateP] = (null);
            }

            From = FromDateP.Value;
            To = ToDateP.Value;

            return !validationError;
        }

        private void updateNoneRb_CheckedChanged(object sender, ControllerEventArgs e)
        {
            if (UpdateNoneRb.Checked)
                UpdateMode = UpdateMode.NONE;
        }

        private void updateAllRb_CheckedChanged(object sender, ControllerEventArgs e)
        {
            if (UpdateAllRb.Checked)
                UpdateMode = UpdateMode.UPDATE_ALL;
        }

        private void updateTicksGenPeriodsRb_CheckedChanged(object sender, ControllerEventArgs e)
        {
            if (UpdateTicksGenPeriodsRb.Checked)
                UpdateMode = UpdateMode.UPDATE_TICKS_GEN_PERIODS;
        }

        private void downloadTickGenPeriodsRb_CheckedChanged(object sender, ControllerEventArgs e)
        {
            if (DownloadTickGenPeriodsRb.Checked)
                UpdateMode = UpdateMode.DOWNLOAD_TICKS_GEN_PERIODS;
        }

        private void deleteCorruptPeriodsCb_CheckedChanged(object sender, ControllerEventArgs e)
        {
            DeleteCorruptPeriods = DeleteCorruptPeriodsCb.Checked;
        }




    }
}
