using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using System.IO;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    
    public class ImportMetatraderPage1Controller : ImportMetatraderPage1ControllerBase
    {

        public ImportMetatraderPage1Controller(GreenRmiManager rmiManager, EnvironmentAssistantController assistant)
            : base(rmiManager, assistant)
		{
            ErrorProvider1 = new ChildControlMap<string>(rmiManager);
            DataGridView1 = new GridController(rmiManager, this);
            PathColumn = new GridColumnController(rmiManager, DataGridView1, typeof(string));
            PathColumn.DataPropertyName = "UpdatedEnvironmentDir";
            VersionColumn = new GridColumnController(rmiManager, DataGridView1, typeof(string));
            PathColumn.DataPropertyName = "UpdatedEnvironmentType";
            Button1 = new ButtonController(rmiManager, this);
            CheckBox1 = new ToggleButtonController(rmiManager, this);
            FolderBrowserDialog1 = new FolderBrowserController(rmiManager);

            Button1.Pressed += new ControllerEventHandler(button1_Click);
        }

        new internal EnvironmentAssistantController Assistant
        {
            get
            {
                return (EnvironmentAssistantController)base.Assistant;
            }
        }

        public override string[] SelectedImportDirectory
        {
            get
            {
                if (DataGridView1.SelectedRows.Count == 1)
                {
                    return new string[] { (string)DataGridView1.SelectedCells[0].Value, (string)DataGridView1.SelectedCells[1].Value };
                }
                else
                {
                    return null;
                }
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override bool StartMetatrader
        {
            get
            {
                return CheckBox1.Checked;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        private EnvironmentAssistantController EAF
        {
            get
            {
                return (EnvironmentAssistantController)Assistant;
            }
        }

        bool searched = false;

        protected override bool OnSetActive()
        {
            if (!base.OnSetActive())
                return false;

            // Enable both the Next and Back buttons on this page    
            Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.Next);

            if (!searched)
            {
                searched = true;

                DataGridView1.ClearRows();

                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Fixed && drive.IsReady)
                    {
                        try
                        {
                            string[] dirs = Directory.GetDirectories(drive.RootDirectory + "Program Files", "*", SearchOption.TopDirectoryOnly);
                            foreach (string mt4dir in dirs)
                            {
                                ParseDirectory(mt4dir);
                            }
                        }
                        catch (DirectoryNotFoundException)
                        {
                        }
                    }
                }
            }

            if (EAF.UpdatedEnvironment != null)
            {
                bool found = false;
                foreach (GridRowController row in DataGridView1.Rows)
                {
                    if (row.Cells[0].Value.Equals(EAF.UpdatedEnvironmentDir))
                    {
                        found = true;
                        DataGridView1.CurrentCell = row.Cells[0];
                        break;
                    }
                }
                if (!found)
                {
                    DataGridView1.AddItem(EAF);
                    DataGridView1.CurrentCell = DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[0];
                }
            }


            return true;
        }

        protected override string OnAssistantNext()
        {
            return typeof(ImportMetatraderPage2Controller).Name;
        }

        private bool ParseDirectory(string mt4dir)
        {
            //DataGridView1.Rows.Add(mt4dir, "?");
            bool mt4dirs_terminalexe = File.Exists(mt4dir + "\\terminal.exe");
            bool mt4dirs_experts = Directory.Exists(mt4dir + "\\experts");
            bool mt4dirs_indicators = Directory.Exists(mt4dir + "\\experts\\indicators");
            bool mt4dirs_scripts = Directory.Exists(mt4dir + "\\experts\\scripts");
            if (mt4dirs_terminalexe && mt4dirs_experts && mt4dirs_indicators && mt4dirs_scripts)
            {
                GridRowController row = new GridRowController(rmiManager, DataGridView1);
                row.AddItem(mt4dir);
                row.AddItem("Metatrader 4");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button1_Click(object sender, ControllerEventArgs e)
        {
            FolderBrowserDialog1.ShowDialog(this.Assistant);
            if (ParseDirectory(FolderBrowserDialog1.SelectedPath))
            {
                DataGridView1.CurrentCell = DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[0];
            }
        }

    }
}
