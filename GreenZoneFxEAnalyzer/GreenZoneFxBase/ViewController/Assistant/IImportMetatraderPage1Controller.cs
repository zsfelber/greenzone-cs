using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using System.IO;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{

    [GreenRmi]
    public interface IImportMetatraderPage1Controller : IAssistantPageController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ChildControlMap<string> ErrorProvider1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridController DataGridView1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController PathColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController VersionColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController Button1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController CheckBox1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        FolderBrowserController FolderBrowserDialog1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly, Modifiers = "virtual")]
        string[] SelectedImportDirectory
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly, Modifiers = "virtual")]
        bool StartMetatrader
        {
            get;
            set;
        }

    }
}
