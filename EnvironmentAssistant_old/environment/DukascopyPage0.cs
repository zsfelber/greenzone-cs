using SMS.Windows.Forms;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GreenZoneFxEngine.Util;
using System.Collections.Generic;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.IO;


namespace EnvironmentAssistant
{
	public class DukascopyPage0 : AssistantPage
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private OpenFileDialog openFileDialog1;
        private TextBox accountCurrencyTb;
        private Label label9;
        private NumericUpDown leverageNupd;
        private Label label8;
        private NumericUpDown accountNumberNupd;
        private TextBox nameTb;
        private Label label2;
        private TextBox accountNameTb;
        private TextBox accountCompanyTb;
        private Label label6;
        private Label label5;
        private Label label10;
        private CheckedListBox symbolsChl;
        private Label label11;
		private System.ComponentModel.IContainer components = null;

        readonly List<string> selectedSymbols = new List<string>();

        public DukascopyPage0()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
        }

        new internal EnvironmentAssistantForm ParentForm
        {
            get
            {
                return (EnvironmentAssistantForm)base.ParentForm;
            }
        }

        public string[] ImportedParameters
        {
            get;
            private set;
        }

        public string EnvironmentName
        {
            get;
            private set;
        }

        public List<string> SelectedSymbols
        {
            get
            {
                return selectedSymbols;
            }
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.accountCurrencyTb = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.leverageNupd = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.accountNumberNupd = new System.Windows.Forms.NumericUpDown();
            this.nameTb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.accountNameTb = new System.Windows.Forms.TextBox();
            this.accountCompanyTb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.symbolsChl = new System.Windows.Forms.CheckedListBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leverageNupd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountNumberNupd)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(477, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "This page demonstrates that you can inherit directly from AssistantPage to create" +
                " whatever look && feel you like.";
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
            // accountCurrencyTb
            // 
            this.accountCurrencyTb.Location = new System.Drawing.Point(119, 278);
            this.accountCurrencyTb.Name = "accountCurrencyTb";
            this.accountCurrencyTb.Size = new System.Drawing.Size(166, 20);
            this.accountCurrencyTb.TabIndex = 28;
            this.accountCurrencyTb.Text = "USD";
            this.accountCurrencyTb.TextChanged += new System.EventHandler(this.accountCurrencyTb_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 281);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Account Currency:";
            // 
            // leverageNupd
            // 
            this.leverageNupd.Location = new System.Drawing.Point(374, 278);
            this.leverageNupd.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.leverageNupd.Name = "leverageNupd";
            this.leverageNupd.Size = new System.Drawing.Size(113, 20);
            this.leverageNupd.TabIndex = 29;
            this.leverageNupd.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.leverageNupd.ValueChanged += new System.EventHandler(this.leverageNupd_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(308, 281);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Leverage:";
            // 
            // accountNumberNupd
            // 
            this.accountNumberNupd.Location = new System.Drawing.Point(374, 201);
            this.accountNumberNupd.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.accountNumberNupd.Name = "accountNumberNupd";
            this.accountNumberNupd.Size = new System.Drawing.Size(113, 20);
            this.accountNumberNupd.TabIndex = 24;
            this.accountNumberNupd.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.accountNumberNupd.ValueChanged += new System.EventHandler(this.accountNumberNupd_ValueChanged);
            // 
            // nameTb
            // 
            this.nameTb.Location = new System.Drawing.Point(119, 252);
            this.nameTb.Name = "nameTb";
            this.nameTb.Size = new System.Drawing.Size(368, 20);
            this.nameTb.TabIndex = 26;
            this.nameTb.Text = "tick data";
            this.nameTb.TextChanged += new System.EventHandler(this.nameTb_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 255);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Name:";
            // 
            // accountNameTb
            // 
            this.accountNameTb.Location = new System.Drawing.Point(119, 226);
            this.accountNameTb.Name = "accountNameTb";
            this.accountNameTb.Size = new System.Drawing.Size(166, 20);
            this.accountNameTb.TabIndex = 25;
            this.accountNameTb.TextChanged += new System.EventHandler(this.accountNameTb_TextChanged);
            // 
            // accountCompanyTb
            // 
            this.accountCompanyTb.Location = new System.Drawing.Point(119, 200);
            this.accountCompanyTb.Name = "accountCompanyTb";
            this.accountCompanyTb.Size = new System.Drawing.Size(166, 20);
            this.accountCompanyTb.TabIndex = 23;
            this.accountCompanyTb.Text = "Dukascopy";
            this.accountCompanyTb.TextChanged += new System.EventHandler(this.accountCompanyTb_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(303, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Account #:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Account Name:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 203);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Account Company:";
            // 
            // symbolsChl
            // 
            this.symbolsChl.CheckOnClick = true;
            this.symbolsChl.ColumnWidth = 85;
            this.symbolsChl.FormattingEnabled = true;
            this.symbolsChl.HorizontalScrollbar = true;
            this.symbolsChl.Location = new System.Drawing.Point(119, 52);
            this.symbolsChl.MultiColumn = true;
            this.symbolsChl.Name = "symbolsChl";
            this.symbolsChl.Size = new System.Drawing.Size(368, 139);
            this.symbolsChl.TabIndex = 41;
            this.symbolsChl.SelectedIndexChanged += new System.EventHandler(this.symbolsChl_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(69, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 45;
            this.label11.Text = "Symbol:";
            // 
            // DukascopyPage0
            // 
            this.Controls.Add(this.label11);
            this.Controls.Add(this.symbolsChl);
            this.Controls.Add(this.accountCurrencyTb);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.leverageNupd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.accountNumberNupd);
            this.Controls.Add(this.nameTb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.accountNameTb);
            this.Controls.Add(this.accountCompanyTb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Name = "DukascopyPage0";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leverageNupd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountNumberNupd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

        protected override bool OnSetActive()
        {
            if( !base.OnSetActive() )
                return false;

            if (SelectedNextPage == null)
            {
                if (string.IsNullOrEmpty(ParentForm.UpdatedEnvironment))
                {
                    accountCompanyTb.Text = "Dukascopy";
                    accountNumberNupd.Value = 1000;
                    accountNameTb.Text = "";
                    nameTb.Text = "tick data";
                    accountCurrencyTb.Text = "USD";
                    leverageNupd.Value = 100;
                }
                else
                {
                    nameTb.ReadOnly = true;
                    nameTb.Text = ParentForm.UpdatedEnvironment;

                    accountCompanyTb.Text = ParentForm.UpdatedEnvironmentData[1];
                    accountNameTb.Text = ParentForm.UpdatedEnvironmentData[2];
                    accountNumberNupd.Text = ParentForm.UpdatedEnvironmentData[3];
                    accountCurrencyTb.Text = ParentForm.UpdatedEnvironmentData[4];
                    leverageNupd.Text = ParentForm.UpdatedEnvironmentData[5];
                }
            }

            if (accountNameTb.Text.Length == 0)
            {
                accountNameTb.Text = System.Environment.MachineName + " " + System.Environment.UserName;
            }

            if (SelectedNextPage == null)
            {
                symbolsChl.Items.Clear();
                symbolsChl.Items.Add("AUDUSD", false);
                symbolsChl.Items.Add("EURCAD", false);
                symbolsChl.Items.Add("EURCHF", false);
                symbolsChl.Items.Add("EURGBP", false);
                symbolsChl.Items.Add("EURJPY", false);
                symbolsChl.Items.Add("EURUSD", true);
                symbolsChl.Items.Add("GBPUSD", false);
                symbolsChl.Items.Add("USDCAD", false);
                symbolsChl.Items.Add("USDCHF", false);
                symbolsChl.Items.Add("USDJPY", false);
                symbolsChl.Items.Add("XAGUSD", false);
            }

            // Enable both the Next and Back buttons on this page    
            Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.Next);

            validate();
            return true;
        }

        protected override string OnAssistantNext()
        {
            if (validate())
            {
                return typeof(DukascopyPage1).Name;
            }
            else
            {
                return null;
            }
        }

        private bool validate()
        {
            bool validationError = false;



            if (accountCompanyTb.Text == "")
            {
                errorProvider1.SetError(accountCompanyTb, "Account Company should not be empty.");
                validationError = true;
            }
            else
            {
                errorProvider1.SetError(accountCompanyTb, null);
            }


            if (accountNameTb.Text == "")
            {
                errorProvider1.SetError(accountNameTb, "Account Name should not be empty.");
                validationError = true;
            }
            else
            {
                errorProvider1.SetError(accountNameTb, null);
            }


            if (nameTb.Text == "")
            {
                errorProvider1.SetError(nameTb, "Name should not be empty.");
                validationError = true;
            }
            else if (ParentForm.Environments.Contains(nameTb.Text) && nameTb.Text != ParentForm.UpdatedEnvironment)
            {
                errorProvider1.SetError(nameTb, "Environment already exists.");
                validationError = true;
            }
            else
            {
                errorProvider1.SetError(nameTb, null);
            }


            if (accountCurrencyTb.Text == "")
            {
                errorProvider1.SetError(accountCurrencyTb, "Account Currency should not be empty.");
                validationError = true;
            }
            else
            {
                errorProvider1.SetError(accountCurrencyTb, null);
            }


            if (leverageNupd.Value < 10)
            {
                errorProvider1.SetError(leverageNupd, "Account Leverage should be >= 10.");
                validationError = true;
            }
            else
            {
                errorProvider1.SetError(leverageNupd, null);
            }


            selectedSymbols.Clear();
            foreach (var s in symbolsChl.CheckedItems)
            {
                selectedSymbols.Add((string)s);
            }
            if (selectedSymbols.Count == 0)
            {
                errorProvider1.SetError(symbolsChl, "Please select symbols.");
                validationError = true;
            }
            else
            {
                errorProvider1.SetError(symbolsChl, null);
            }


            ImportedParameters = new string[9];
            ImportedParameters[1] = accountCompanyTb.Text.Trim();
            ImportedParameters[2] = accountNameTb.Text.Trim();
            ImportedParameters[3] = "" + accountNumberNupd.Value;
            EnvironmentName = nameTb.Text;
            ImportedParameters[4] = accountCurrencyTb.Text;
            ImportedParameters[5] = "" + leverageNupd.Value;
            // TODO stopout level, mode
            ImportedParameters[6] = "100";
            ImportedParameters[7] = "0";

            return !validationError;
        }

        private void fromDateP_ValueChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void toDateP_ValueChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void accountCompanyTb_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ParentForm.UpdatedEnvironment))
            {
                nameTb.Text = accountCompanyTb.Text + ", " + accountNameTb.Text + ", " + accountNumberNupd.Value;
            }
            validate();
        }

        private void accountNumberNupd_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ParentForm.UpdatedEnvironment))
            {
                nameTb.Text = accountCompanyTb.Text + ", " + accountNameTb.Text + ", " + accountNumberNupd.Value;
            }
            validate();
        }

        private void accountNameTb_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(ParentForm.UpdatedEnvironment))
            {
                nameTb.Text = accountCompanyTb.Text + ", " + accountNameTb.Text + ", " + accountNumberNupd.Value;
            }
            validate();
        }

        private void nameTb_TextChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void accountCurrencyTb_TextChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void leverageNupd_ValueChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void symbolsChl_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate();
        }


  

    }
}

