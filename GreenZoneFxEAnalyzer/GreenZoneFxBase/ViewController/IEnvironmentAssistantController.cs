using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Assistant;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    [GreenRmi]
	public interface IEnvironmentAssistantController : IAssistantFormController
    {

        string UpdatedEnvironment
        {
            get;
            set;
        }

        string UpdatedEnvironmentDir
        {
            get;
            set;
        }

        string UpdatedEnvironmentType
        {
            get;
            set;
        }

        string UpdatedEnvironmentHistoryDir
        {
            get;
            set;
        }

        ISet<string> Environments
        {
            get;
            set;
        }

        string[] UpdatedEnvironmentData
        {
            get;
            set;
        }


        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IStartPageController StartPageController
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ISelectEnvTypePageController SelectEnvTypePageController
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IImportMetatraderPage1Controller ImportMetatraderPage1Controller
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IImportMetatraderPage2Controller ImportMetatraderPage2Controller
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IDukascopyPage0Controller DukascopyPage0Controller
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IDukascopyPage1Controller DukascopyPage1Controller
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IDukascopyPage2Controller DukascopyPage2Controller
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IDukascopyPage3Controller DukascopyPage3Controller
        {
            get;
            set;
        }

    }
}

