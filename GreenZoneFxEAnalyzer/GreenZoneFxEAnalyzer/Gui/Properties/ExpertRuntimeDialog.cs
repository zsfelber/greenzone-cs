﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Etc;
using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.Gui.ViewController;

namespace GreenZoneFxEngine
{
    public partial class ExpertRuntimeDialog : Form
    {
        public ExpertRuntimeDialog()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, ExpertDialogController controller)
        {
            new FormVCBinder(context, this, controller);

            expertRuntimePanel.Bind(context, controller.ExpertRuntimePanel);
            new ButtonVCBinder(context, button1, controller.OkButton);
            new ButtonVCBinder(context, button2, controller._CancelButton);
            new ButtonVCBinder(context, button3, controller.ResetButton);
            new ButtonVCBinder(context, loadButton, controller.LoadButton);
            new ButtonVCBinder(context, saveButton, controller.SaveButton);
        }
    }
}