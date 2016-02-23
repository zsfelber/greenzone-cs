using SMS.Windows.Forms;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GreenZoneFxEngine.Util;
using System.Collections.Generic;
using System.Text;
using GreenZoneFxEngine.Trading;
using System.IO;
using GreenZoneFxEngine.Types;


namespace EnvironmentAssistant
{
	public class DukascopyPage1 : AssistantPage
	{
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private OpenFileDialog openFileDialog1;
        private CheckedListBox generateChl;
        private Label label11;
		private System.ComponentModel.IContainer components = null;
        private Label label2;
        private TextBox generateDefTb;
        private GroupBox groupBox1;
        private RadioButton updateNoneRb;
        private RadioButton downloadTickGenPeriodsRb;
        private RadioButton updateTicksGenPeriodsRb;
        private RadioButton updateAllRb;

        Dictionary<string, TimePeriodConst> DTimePeriodConst = new Dictionary<string, TimePeriodConst>();
        List<TimePeriodConst> customPeriods = new List<TimePeriodConst>();
        List<TimePeriodConst> allSelectedPeriods = new List<TimePeriodConst>();
        private ToolTip toolTip1;
        private CheckBox deleteCorruptPeriodsCb;
        private DateTimePicker toDateP;
        private DateTimePicker fromDateP;
        private Label label4;
        private Label label3;
        

        UpdateMode updateMode = UpdateMode.NONE;

        public DukascopyPage1()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
        }

        new internal EnvironmentAssistantForm ParentForm
        {
            get
            {
                return (EnvironmentAssistantForm)base.ParentForm;
            }
        }

        public string[] ImportedParameters
        {
            get;
            private set;
        }

        public string EnvironmentName
        {
            get;
            private set;
        }

        internal UpdateMode UpdateMode
        {
            get
            {
                return updateMode;
            }
            private set
            {
                if (updateMode != value)
                {
                    updateMode = value;

                    switch (updateMode)
                    {
                        case UpdateMode.NONE:
                            generateDefTb.Text = "";
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
                            generateDefTb.Text = genDef.ToString();
                            Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.Next);
                            break;
                    }

                    DukascopyPage0 page0 = (DukascopyPage0)PreviousPage;
                    string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
                    string envDir = eaRootDir + "\\" + page0.EnvironmentName;
                    string hp = envDir + "\\history\\";

                    if (page0.SelectedSymbols.Count > 1)
                    {
                        deleteCorruptPeriodsCb.Text = "Delete unselected old period files (for all selected periods !)";
                    }
                    else
                    {
                        deleteCorruptPeriodsCb.Text = "Delete unselected old period files";
                    }

                    switch (updateMode)
                    {
                        case UpdateMode.NONE:
                            generateChl.Enabled = false;
                            deleteCorruptPeriodsCb.Enabled = false;
                            deleteCorruptPeriodsCb.Checked = false;
                            fromDateP.Enabled = false;
                            toDateP.Enabled = false;
                            generateChl.Items.Clear();
                            DTimePeriodConst.Clear();
                            break;
                        case UpdateMode.UPDATE_ALL:
                            generateChl.Enabled = false;
                            deleteCorruptPeriodsCb.Enabled = false;
                            deleteCorruptPeriodsCb.Checked = false;
                            fromDateP.Enabled = true;
                            toDateP.Enabled = true;
                            generateChl.Items.Clear();
                            DTimePeriodConst.Clear();
                            foreach (var p in EnumExtensions.GetPeriods(TimePeriodCategory.MT5 | TimePeriodCategory.SECS))
                            {
                                bool found = false;
                                foreach (var symbol in page0.SelectedSymbols)
                                {
                                    if (TimeSeriesRuntime.IsSeriesAvailable(hp, symbol, p))
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if (found)
                                {
                                    generateChl.Items.Add(p.GetLongTxt(), found);
                                    DTimePeriodConst[p.GetLongTxt()] = p;
                                }
                            }


                            if (Directory.Exists(hp))
                            {
                                datetime minlast = datetime.MaxValue;
                                foreach (var symbol in page0.SelectedSymbols)
                                {
                                    foreach (var f in Directory.GetFiles(hp, symbol + "*.ticks"))
                                    {
                                        try
                                        {
                                            TimeSeriesRuntime r = new TickTimeSeriesRuntime(f, false, datetime.MaxValue);
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
                                            TimeSeriesRuntime r = new PeriodTimeSeriesRuntime(f, false, datetime.MaxValue);
                                            minlast = datetime.Min(minlast, r.GetTime(0));
                                        }
                                        catch (TimeSeriesException)
                                        {
                                        }
                                    }
                                }
                                if (minlast == datetime.MaxValue)
                                {
                                    fromDateP.Value = new DateTime(2007, 03, 29);
                                }
                                else
                                {
                                    fromDateP.Value = (DateTime)minlast;
                                }
                            }
                            else
                            {
                                fromDateP.Value = new DateTime(2007, 03, 29);
                            }

                            break;
                        default:
                            generateChl.Enabled = true;
                            deleteCorruptPeriodsCb.Enabled = true;
                            deleteCorruptPeriodsCb.Checked = true;
                            fromDateP.Enabled = true;
                            toDateP.Enabled = true;
                            generateChl.Items.Clear();
                            DTimePeriodConst.Clear();

                            fromDateP.Value = new DateTime(2007, 03, 29);

                            foreach (var p in EnumExtensions.GetPeriods(TimePeriodCategory.MT5 | TimePeriodCategory.SECS))
                            {
                                bool found = false;
                                foreach (var symbol in page0.SelectedSymbols)
                                {
                                    if (TimeSeriesRuntime.IsSeriesAvailable(hp, symbol, p))
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                generateChl.Items.Add(p.GetLongTxt(), found);
                                DTimePeriodConst[p.GetLongTxt()] = p;
                            }
                            break;
                    }
                }
            }
        }

        public List<TimePeriodConst> CustomPeriods
        {
            get
            {
                return customPeriods;
            }
        }

        public List<TimePeriodConst> AllSelectedPeriods
        {
            get
            {
                return allSelectedPeriods;
            }
        }

        public DateTime From
        {
            get;
            private set;
        }

        public DateTime To
        {
            get;
            private set;
        }

        public bool DeleteCorruptPeriods
        {
            get;
            set;
        }

        /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.generateChl = new System.Windows.Forms.CheckedListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.generateDefTb = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.updateNoneRb = new System.Windows.Forms.RadioButton();
            this.downloadTickGenPeriodsRb = new System.Windows.Forms.RadioButton();
            this.updateTicksGenPeriodsRb = new System.Windows.Forms.RadioButton();
            this.updateAllRb = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.deleteCorruptPeriodsCb = new System.Windows.Forms.CheckBox();
            this.toDateP = new System.Windows.Forms.DateTimePicker();
            this.fromDateP = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataMember = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // generateChl
            // 
            this.generateChl.CheckOnClick = true;
            this.generateChl.ColumnWidth = 85;
            this.generateChl.FormattingEnabled = true;
            this.generateChl.HorizontalScrollbar = true;
            this.generateChl.Location = new System.Drawing.Point(24, 232);
            this.generateChl.MultiColumn = true;
            this.generateChl.Name = "generateChl";
            this.generateChl.Size = new System.Drawing.Size(448, 94);
            this.generateChl.TabIndex = 41;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 215);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 45;
            this.label11.Text = "Generate :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Generating by default :";
            // 
            // generateDefTb
            // 
            this.generateDefTb.Location = new System.Drawing.Point(24, 164);
            this.generateDefTb.Multiline = true;
            this.generateDefTb.Name = "generateDefTb";
            this.generateDefTb.ReadOnly = true;
            this.generateDefTb.Size = new System.Drawing.Size(448, 43);
            this.generateDefTb.TabIndex = 50;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.updateNoneRb);
            this.groupBox1.Controls.Add(this.downloadTickGenPeriodsRb);
            this.groupBox1.Controls.Add(this.updateTicksGenPeriodsRb);
            this.groupBox1.Controls.Add(this.updateAllRb);
            this.groupBox1.Location = new System.Drawing.Point(24, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 121);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Update Mode";
            // 
            // updateNoneRb
            // 
            this.updateNoneRb.AutoSize = true;
            this.updateNoneRb.Checked = true;
            this.updateNoneRb.Location = new System.Drawing.Point(24, 21);
            this.updateNoneRb.Name = "updateNoneRb";
            this.updateNoneRb.Size = new System.Drawing.Size(333, 17);
            this.updateNoneRb.TabIndex = 3;
            this.updateNoneRb.TabStop = true;
            this.updateNoneRb.Text = "No download, no generation, just update environment parameters";
            this.updateNoneRb.UseVisualStyleBackColor = true;
            this.updateNoneRb.CheckedChanged += new System.EventHandler(this.updateNoneRb_CheckedChanged);
            // 
            // downloadTickGenPeriodsRb
            // 
            this.downloadTickGenPeriodsRb.AutoSize = true;
            this.downloadTickGenPeriodsRb.Location = new System.Drawing.Point(24, 90);
            this.downloadTickGenPeriodsRb.Name = "downloadTickGenPeriodsRb";
            this.downloadTickGenPeriodsRb.Size = new System.Drawing.Size(338, 17);
            this.downloadTickGenPeriodsRb.TabIndex = 2;
            this.downloadTickGenPeriodsRb.Text = "Downloads all ticks again, generates all periods from scratch again";
            this.downloadTickGenPeriodsRb.UseVisualStyleBackColor = true;
            this.downloadTickGenPeriodsRb.CheckedChanged += new System.EventHandler(this.downloadTickGenPeriodsRb_CheckedChanged);
            // 
            // updateTicksGenPeriodsRb
            // 
            this.updateTicksGenPeriodsRb.AutoSize = true;
            this.updateTicksGenPeriodsRb.Location = new System.Drawing.Point(24, 67);
            this.updateTicksGenPeriodsRb.Name = "updateTicksGenPeriodsRb";
            this.updateTicksGenPeriodsRb.Size = new System.Drawing.Size(319, 17);
            this.updateTicksGenPeriodsRb.TabIndex = 1;
            this.updateTicksGenPeriodsRb.Text = "Downloads new ticks, generates all periods from scratch again";
            this.updateTicksGenPeriodsRb.UseVisualStyleBackColor = true;
            this.updateTicksGenPeriodsRb.CheckedChanged += new System.EventHandler(this.updateTicksGenPeriodsRb_CheckedChanged);
            // 
            // updateAllRb
            // 
            this.updateAllRb.AutoSize = true;
            this.updateAllRb.Location = new System.Drawing.Point(24, 44);
            this.updateAllRb.Name = "updateAllRb";
            this.updateAllRb.Size = new System.Drawing.Size(258, 17);
            this.updateAllRb.TabIndex = 0;
            this.updateAllRb.Text = "Downloads new ticks, updates generated periods";
            this.updateAllRb.UseVisualStyleBackColor = true;
            this.updateAllRb.CheckedChanged += new System.EventHandler(this.updateAllRb_CheckedChanged);
            // 
            // deleteCorruptPeriodsCb
            // 
            this.deleteCorruptPeriodsCb.AutoSize = true;
            this.deleteCorruptPeriodsCb.Location = new System.Drawing.Point(24, 332);
            this.deleteCorruptPeriodsCb.Name = "deleteCorruptPeriodsCb";
            this.deleteCorruptPeriodsCb.Size = new System.Drawing.Size(259, 17);
            this.deleteCorruptPeriodsCb.TabIndex = 52;
            this.deleteCorruptPeriodsCb.Text = "Delete unselected old period files (for all periods !)";
            this.deleteCorruptPeriodsCb.UseVisualStyleBackColor = true;
            this.deleteCorruptPeriodsCb.CheckedChanged += new System.EventHandler(this.deleteCorruptPeriodsCb_CheckedChanged);
            // 
            // toDateP
            // 
            this.toDateP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.toDateP.Location = new System.Drawing.Point(367, 134);
            this.toDateP.Name = "toDateP";
            this.toDateP.Size = new System.Drawing.Size(105, 20);
            this.toDateP.TabIndex = 56;
            // 
            // fromDateP
            // 
            this.fromDateP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fromDateP.Location = new System.Drawing.Point(206, 134);
            this.fromDateP.Name = "fromDateP";
            this.fromDateP.Size = new System.Drawing.Size(107, 20);
            this.fromDateP.TabIndex = 55;
            this.fromDateP.Value = new System.DateTime(2007, 3, 29, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(333, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "&To:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(167, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "From:";
            // 
            // DukascopyPage1
            // 
            this.Controls.Add(this.toDateP);
            this.Controls.Add(this.fromDateP);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.deleteCorruptPeriodsCb);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.generateDefTb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.generateChl);
            this.Name = "DukascopyPage1";
            this.Size = new System.Drawing.Size(497, 366);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

        protected override bool OnSetActive()
        {
            if( !base.OnSetActive() )
                return false;

            UpdateMode = UpdateMode.NONE;

            if (string.IsNullOrEmpty(ParentForm.UpdatedEnvironment))
            {
                updateTicksGenPeriodsRb.Checked = true;
            }
            else
            {
                updateAllRb.Checked = true;
            }

            return true;
        }

        protected override string OnAssistantNext()
        {
            if (validate())
            {
                if (UpdateMode == UpdateMode.NONE)
                {
                    return typeof(FinishPage).Name;
                }
                else
                {
                    allSelectedPeriods.Clear();
                    customPeriods.Clear();
                    foreach (var p in EnumExtensions.GetPeriods(TimePeriodCategory.MT4))
                    {
                        allSelectedPeriods.Add(p);
                    }
                    foreach (var s in generateChl.CheckedItems)
                    {
                        TimePeriodConst p = DTimePeriodConst[(string)s];
                        customPeriods.Add(p);
                        allSelectedPeriods.Add(p);
                    }

                    return typeof(DukascopyPage2).Name;
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

            if (fromDateP.Value > toDateP.Value)
            {
                errorProvider1.SetError(fromDateP, "From should not be higher than To.");
                errorProvider1.SetError(toDateP, "From should not be higher than To.");
                validationError = true;
            }
            else
            {
                errorProvider1.SetError(fromDateP, null);
                errorProvider1.SetError(toDateP, null);
            }

            From = fromDateP.Value;
            To = toDateP.Value;

            return !validationError;
        }

        private void updateNoneRb_CheckedChanged(object sender, EventArgs e)
        {
            if (updateNoneRb.Checked)
                UpdateMode = UpdateMode.NONE;
        }

        private void updateAllRb_CheckedChanged(object sender, EventArgs e)
        {
            if (updateAllRb.Checked)
                UpdateMode = UpdateMode.UPDATE_ALL;
        }

        private void updateTicksGenPeriodsRb_CheckedChanged(object sender, EventArgs e)
        {
            if (updateTicksGenPeriodsRb.Checked)
                UpdateMode = UpdateMode.UPDATE_TICKS_GEN_PERIODS;
        }

        private void downloadTickGenPeriodsRb_CheckedChanged(object sender, EventArgs e)
        {
            if (downloadTickGenPeriodsRb.Checked)
                UpdateMode = UpdateMode.DOWNLOAD_TICKS_GEN_PERIODS;
        }

        private void deleteCorruptPeriodsCb_CheckedChanged(object sender, EventArgs e)
        {
            DeleteCorruptPeriods = deleteCorruptPeriodsCb.Checked;
        }




    }

    internal enum UpdateMode
    {
        NONE,
        UPDATE_ALL,
        UPDATE_TICKS_GEN_PERIODS,
        DOWNLOAD_TICKS_GEN_PERIODS
    }
}

