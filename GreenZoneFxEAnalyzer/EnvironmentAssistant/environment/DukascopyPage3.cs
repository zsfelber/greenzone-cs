using SMS.Windows.Forms;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GreenZoneFxEngine.Util;
using System.Threading;
using System.Net;
using GreenZoneFxEngine.Trading;
using System.IO;
using SevenZip;
using System.Text;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Types;
using System.Globalization;
using System.Collections.Generic;
using GreenZoneUtil.Gui.ViewController;
using GreenZoneFxEngine.ViewController.Assistant;


namespace EnvironmentAssistant
{
    public class DukascopyPage3 : AssistantPage
    {
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private OpenFileDialog openFileDialog1;
        private Label dateLabel;
        private Label symbolLabel;
        private ProgressBar symbolProgressBar;
        private ProgressBar dateProgressBar;
        private System.ComponentModel.IContainer components = null;
        private Label toLabel;
        private Label fromLabel;
        private Label estimatedLabel;
        private Label elapsedLabel;
        private TextBox symbolsTb;
        private Label label1;
        private Label label2;

        public DukascopyPage3()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call

        }

        public void Bind(WinFormsMVContext context, DukascopyPage3Controller controller)
        {
            base.Bind(context, controller);
            new FileDialogVCBinder(context, openFileDialog1, controller.OpenFileDialog1);
            new LabelVCBinder1(context, dateLabel, controller.DateLabel);
            new LabelVCBinder1(context, symbolLabel, controller.SymbolLabel);
            new ProgressBarVCBinder(context, symbolProgressBar, controller.SymbolProgressBar);
            new ProgressBarVCBinder(context, dateProgressBar, controller.DateProgressBar);
            new LabelVCBinder1(context, toLabel, controller.ToLabel);
            new LabelVCBinder1(context, fromLabel, controller.FromLabel);
            new LabelVCBinder1(context, estimatedLabel, controller.EstimatedLabel);
            new LabelVCBinder1(context, elapsedLabel, controller.ElapsedLabel);
            new TextBoxVCBinder1(context, symbolsTb, controller.SymbolsTb);
            new ErrorProviderVCBinder(context, errorProvider1, controller.ErrorProvider1);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
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
            this.dateProgressBar = new System.Windows.Forms.ProgressBar();
            this.symbolProgressBar = new System.Windows.Forms.ProgressBar();
            this.symbolLabel = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.fromLabel = new System.Windows.Forms.Label();
            this.toLabel = new System.Windows.Forms.Label();
            this.estimatedLabel = new System.Windows.Forms.Label();
            this.elapsedLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.symbolsTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            // dateProgressBar
            // 
            this.dateProgressBar.Location = new System.Drawing.Point(34, 243);
            this.dateProgressBar.Name = "dateProgressBar";
            this.dateProgressBar.Size = new System.Drawing.Size(429, 10);
            this.dateProgressBar.TabIndex = 2;
            // 
            // symbolProgressBar
            // 
            this.symbolProgressBar.Location = new System.Drawing.Point(34, 173);
            this.symbolProgressBar.Name = "symbolProgressBar";
            this.symbolProgressBar.Size = new System.Drawing.Size(429, 11);
            this.symbolProgressBar.TabIndex = 3;
            // 
            // symbolLabel
            // 
            this.symbolLabel.AutoSize = true;
            this.symbolLabel.Location = new System.Drawing.Point(36, 157);
            this.symbolLabel.Name = "symbolLabel";
            this.symbolLabel.Size = new System.Drawing.Size(35, 13);
            this.symbolLabel.TabIndex = 5;
            this.symbolLabel.Text = "label2";
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(36, 227);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(35, 13);
            this.dateLabel.TabIndex = 6;
            this.dateLabel.Text = "label3";
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.Location = new System.Drawing.Point(35, 204);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(33, 13);
            this.fromLabel.TabIndex = 7;
            this.fromLabel.Text = "From:";
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(219, 204);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(23, 13);
            this.toLabel.TabIndex = 8;
            this.toLabel.Text = "To:";
            // 
            // estimatedLabel
            // 
            this.estimatedLabel.AutoSize = true;
            this.estimatedLabel.Location = new System.Drawing.Point(219, 272);
            this.estimatedLabel.Name = "estimatedLabel";
            this.estimatedLabel.Size = new System.Drawing.Size(56, 13);
            this.estimatedLabel.TabIndex = 10;
            this.estimatedLabel.Text = "Estimated:";
            // 
            // elapsedLabel
            // 
            this.elapsedLabel.AutoSize = true;
            this.elapsedLabel.Location = new System.Drawing.Point(36, 272);
            this.elapsedLabel.Name = "elapsedLabel";
            this.elapsedLabel.Size = new System.Drawing.Size(48, 13);
            this.elapsedLabel.TabIndex = 9;
            this.elapsedLabel.Text = "Elapsed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Symbols:";
            // 
            // symbolsTb
            // 
            this.symbolsTb.Location = new System.Drawing.Point(34, 65);
            this.symbolsTb.Multiline = true;
            this.symbolsTb.Name = "symbolsTb";
            this.symbolsTb.ReadOnly = true;
            this.symbolsTb.Size = new System.Drawing.Size(429, 85);
            this.symbolsTb.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(471, 26);
            this.label1.TabIndex = 13;
            this.label1.Text = "STEP 2 >>  Generating...";
            // 
            // DukascopyPage3
            // 
            this.Controls.Add(this.label1);
            this.Controls.Add(this.symbolsTb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.estimatedLabel);
            this.Controls.Add(this.elapsedLabel);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.fromLabel);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.symbolLabel);
            this.Controls.Add(this.symbolProgressBar);
            this.Controls.Add(this.dateProgressBar);
            this.Name = "DukascopyPage3";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

    }
}

