using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Util;
using GreenZoneFxEngine.ViewController.Orders;
using GreenZoneUtil.Gui.ViewController;

namespace GreenZoneFxEngine.Gui
{
    public partial class OrdersToolbar : UserControl
    {
        public OrdersToolbar()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, OrdersToolbarController controller)
        {
            //new SimpleControlVCBinder(context, tsPanel1 ,controller.tsPanel1;
            //new SimpleControlVCBinder(context, tsPanel2 ,controller.tsPanel2;
            //new LabelVCBinder1(context, label7 ,controller.label7;
            //new LabelVCBinder1(context, label5 ,controller.label5;
            //new SimpleControlVCBinder(context, panel2 ,controller.panel2;
            //new LabelVCBinder1(context, label3 ,controller.label3;
            //new LabelVCBinder1(context, label2 ,controller.label2;
            //new LabelVCBinder1(context, label1 ,controller.label1;
            //new LabelVCBinder1(context, label6 ,controller.label6;
            //new LabelVCBinder1(context, label9 ,controller.label9;
            //new LabelVCBinder1(context, label8 ,controller.label8;
            //new LabelVCBinder1(context, label4 ,controller.label4;

            new SimpleControlVCBinder(context, this, controller);

            new ToolStripButtonVCBinder(context, closeChartButton2, controller.CloseChartButton2);
            new SimpleControlVCBinder(context, panel1, controller.Panel1);
            new ComboBoxVCBinder(context, expertCb, controller.ExpertCb);
            new ComboBoxVCBinder(context, operationCb, controller.OperationCb);
            new CheckBoxVCBinder(context, buyCheckBox, controller.BuyCheckBox);
            new CheckBoxVCBinder(context, sellCheckBox, controller.SellCheckBox);
            new CheckBoxVCBinder(context, showFiltersCheckBox, controller.ShowFiltersCheckBox);
            new CheckBoxVCBinder(context, limitCheckBox, controller.LimitCheckBox);
            new CheckBoxVCBinder(context, stopCheckBox, controller.StopCheckBox);
            new ButtonVCBinder(context, resetButton, controller.ResetButton);
            new DateTimePickerVCBinder(context, fromDtp, controller.FromDtp);
            new DateTimePickerVCBinder(context, toDtp, controller.ToDtp);
            new ComboBoxVCBinder(context, magicCb, controller.MagicCb);
            new TextBoxVCBinder1(context, commentTb, controller.CommentTb);
            new ComboBoxVCBinder(context, groupByCb, controller.GroupByCb);
            new ComboBoxVCBinder(context, symbolCb, controller.SymbolCb);
        }
    }
}
