using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using System.Threading;
using GreenZoneFxEngine.Types;
using System.Net;
using GreenZoneFxEngine.Trading;
using System.IO;

using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    
    [GreenRmi]
    public interface IDukascopyPage2Controller : IAssistantPageController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ChildControlMap<string> ErrorProvider1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        FileDialogController OpenFileDialog1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController DateLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController SymbolLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ProgressTrackController SymbolProgressBar
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ProgressTrackController DateProgressBar
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController ToLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController FromLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController EstimatedLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController ElapsedLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController SymbolsTb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController Label2
        {
            get;
            set;
        }

        datetime FirstUpdatedFileTime
        {
            get;
            set;
        }

    }
}