using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Util;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Timers;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Orders
{
    
    [GreenRmi]
    public interface IOrdersToolbarController : IController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController SymbolCb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController OperationCb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController ExpertCb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController MagicCb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController GroupByCb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController BuyCheckBox
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController SellCheckBox
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController LimitCheckBox
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController StopCheckBox
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController ShowFiltersCheckBox
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController CommentTb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        FieldController<DateTime> FromDtp
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        FieldController<DateTime> ToDtp
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

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController CloseChartButton2
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        Controller Panel1
        {
            get;
            set;
        }

    }
}
