using SMS.Windows.Forms;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using GreenZoneUtil.Gui.ViewController;
using GreenZoneFxEngine.ViewController.Assistant;

namespace EnvironmentAssistant
{
	public class ImportMetatraderPage2 : AssistantPage
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private OpenFileDialog openFileDialog1;
        private CheckBox checkBox1;
        private TextBox accountNameTb;
        private TextBox accountCompanyTb;
        private Label label6;
        private Label label5;
        private Label label4;
        private ComboBox historyDirectoryCb;
        private Label label7;
        private TextBox nameTb;
        private Label label3;
        private NumericUpDown accountNumberNupd;
        private Timer timer1;
		private System.ComponentModel.IContainer components = null;
        private TextBox accountCurrencyTb;
        private Label label9;
        private NumericUpDown leverageNupd;
        private Label label8;

        public ImportMetatraderPage2()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

        }

        public void Bind(WinFormsMVContext context, ImportMetatraderPage2Controller controller)
        {
            base.Bind(context, controller);
            new FileDialogVCBinder(context, openFileDialog1 , controller.OpenFileDialog1);
            new CheckBoxVCBinder(context, checkBox1 , controller.CheckBox1);
            new TextBoxVCBinder1(context, accountNameTb , controller.AccountNameTb);
            new TextBoxVCBinder1(context, accountCompanyTb , controller.AccountCompanyTb);
            new ComboBoxVCBinder(context, historyDirectoryCb , controller.HistoryDirectoryCb);
            new TextBoxVCBinder2(context, nameTb , controller.NameTb);
            new NumericUpDownVCBinder(context, accountNumberNupd , controller.AccountNumberNupd);
            new TextBoxVCBinder1(context, accountCurrencyTb , controller.AccountCurrencyTb);
            new NumericUpDownVCBinder(context, leverageNupd , controller.LeverageNupd);
            new ErrorProviderVCBinder(context, errorProvider1, controller.ErrorProvider1);
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.accountCompanyTb = new System.Windows.Forms.TextBox();
            this.accountNameTb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nameTb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.historyDirectoryCb = new System.Windows.Forms.ComboBox();
            this.accountNumberNupd = new System.Windows.Forms.NumericUpDown();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.leverageNupd = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.accountCurrencyTb = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountNumberNupd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leverageNupd)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(471, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "This page demonstrates that you can inherit directly from AssistantPage to create" +
                " whatever look && feel you like.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(471, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "It also demonstrates how data validation is handled in the default implementation" +
                " of OnKillActive.  In this example, validation will fail if the text box below i" +
                "s empty.";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkRate = 100;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataMember = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(253, 273);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(228, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Close Metatrader application when finished";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(299, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Account #:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Account Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Account Company:";
            // 
            // accountCompanyTb
            // 
            this.accountCompanyTb.Location = new System.Drawing.Point(110, 92);
            this.accountCompanyTb.Name = "accountCompanyTb";
            this.accountCompanyTb.Size = new System.Drawing.Size(166, 20);
            this.accountCompanyTb.TabIndex = 0;
            // 
            // accountNameTb
            // 
            this.accountNameTb.Location = new System.Drawing.Point(110, 118);
            this.accountNameTb.Name = "accountNameTb";
            this.accountNameTb.Size = new System.Drawing.Size(166, 20);
            this.accountNameTb.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Name:";
            // 
            // nameTb
            // 
            this.nameTb.Location = new System.Drawing.Point(110, 155);
            this.nameTb.Name = "nameTb";
            this.nameTb.Size = new System.Drawing.Size(360, 20);
            this.nameTb.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "History Directory:";
            // 
            // historyDirectoryCb
            // 
            this.historyDirectoryCb.FormattingEnabled = true;
            this.historyDirectoryCb.Location = new System.Drawing.Point(110, 181);
            this.historyDirectoryCb.Name = "historyDirectoryCb";
            this.historyDirectoryCb.Size = new System.Drawing.Size(360, 21);
            this.historyDirectoryCb.TabIndex = 4;
            // 
            // accountNumberNupd
            // 
            this.accountNumberNupd.Location = new System.Drawing.Point(365, 93);
            this.accountNumberNupd.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.accountNumberNupd.Name = "accountNumberNupd";
            this.accountNumberNupd.Size = new System.Drawing.Size(105, 20);
            this.accountNumberNupd.TabIndex = 1;
            this.accountNumberNupd.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // leverageNupd
            // 
            this.leverageNupd.Location = new System.Drawing.Point(365, 226);
            this.leverageNupd.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.leverageNupd.Name = "leverageNupd";
            this.leverageNupd.Size = new System.Drawing.Size(105, 20);
            this.leverageNupd.TabIndex = 6;
            this.leverageNupd.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(299, 229);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Leverage:";
            // 
            // accountCurrencyTb
            // 
            this.accountCurrencyTb.Location = new System.Drawing.Point(110, 226);
            this.accountCurrencyTb.Name = "accountCurrencyTb";
            this.accountCurrencyTb.Size = new System.Drawing.Size(166, 20);
            this.accountCurrencyTb.TabIndex = 5;
            this.accountCurrencyTb.Text = "USD";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 229);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Account Currency:";
            // 
            // ImportMetatraderPage2
            // 
            this.Controls.Add(this.accountCurrencyTb);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.leverageNupd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.accountNumberNupd);
            this.Controls.Add(this.historyDirectoryCb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nameTb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.accountNameTb);
            this.Controls.Add(this.accountCompanyTb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ImportMetatraderPage2";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountNumberNupd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leverageNupd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion



    }
}

