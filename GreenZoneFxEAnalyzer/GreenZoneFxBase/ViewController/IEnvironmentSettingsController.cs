using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using System.Windows.Forms;
using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    [GreenRmi]
    public interface IEnvironmentSettingsController : IDialogController
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        TreeController TreeView1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        SplitController SplitContainer1
        {
            get;
            set;
        }

        TreeNodeController Current
        {
            get;
            set;
        }

        BufferedPropertyGridController CurrentPanel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController SaveButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController _CancelButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ResetButton
        {
            get;
            set;
        }

    }
}
