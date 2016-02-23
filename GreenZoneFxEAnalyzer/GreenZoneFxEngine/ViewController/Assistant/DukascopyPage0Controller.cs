using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    
    public class DukascopyPage0Controller : DukascopyPage0ControllerBase
    {

        public DukascopyPage0Controller(GreenRmiManager rmiManager, EnvironmentAssistantController assistant)
            : base(rmiManager, assistant)
        {
            ErrorProvider1 = new ChildControlMap<string>(rmiManager);
            OpenFileDialog1 = new FileDialogController(rmiManager);
            FromDateP = new FieldController<DateTime>(rmiManager, this);
            ToDateP = new FieldController<DateTime>(rmiManager, this);
            AccountCurrencyTb = new LabelledController(rmiManager, this);
            LeverageNupd = new FieldController<int>(rmiManager, this);
            AccountNumberNupd = new FieldController<int>(rmiManager, this);
            NameTb = new FieldController<string>(rmiManager, this);
            AccountNameTb = new LabelledController(rmiManager, this);
            AccountCompanyTb = new LabelledController(rmiManager, this);
            SymbolsChl = new ListController(rmiManager, this);
            FromDateP.ValueChanged += new PropertyChangedEventHandler(fromDateP_ValueChanged);
            ToDateP.ValueChanged += new PropertyChangedEventHandler(toDateP_ValueChanged);
            AccountCurrencyTb.TextChanged += new PropertyChangedEventHandler(accountCurrencyTb_TextChanged);
            LeverageNupd.ValueChanged+=new PropertyChangedEventHandler(leverageNupd_ValueChanged);
            AccountNumberNupd.ValueChanged+=new PropertyChangedEventHandler(accountNumberNupd_ValueChanged);
            NameTb.ValueChanged+=new PropertyChangedEventHandler(nameTb_TextChanged);
            AccountNameTb.TextChanged+=new PropertyChangedEventHandler(accountNameTb_TextChanged);
            AccountCompanyTb.TextChanged+=new PropertyChangedEventHandler(accountCompanyTb_TextChanged);
            SymbolsChl.SelectedRowsChanged+=new PropertyChangedEventHandler(symbolsChl_SelectedIndexChanged);

            SelectedSymbols = new List<string>();
        }

        new internal EnvironmentAssistantController Assistant
        {
            get
            {
                return (EnvironmentAssistantController)base.Assistant;
            }
        }

        protected override bool OnSetActive()
        {
            if (!base.OnSetActive())
                return false;

            if (SelectedNextPage == null)
            {
                if (string.IsNullOrEmpty(Assistant.UpdatedEnvironment))
                {
                    AccountCompanyTb.Text = "Dukascopy";
                    AccountNumberNupd.Value = 1000;
                    AccountNameTb.Text = "";
                    NameTb.Value = "tick data";
                    AccountCurrencyTb.Text = "USD";
                    LeverageNupd.Value = 100;
                }
                else
                {
                    NameTb.ReadOnly = true;
                    NameTb.Value = Assistant.UpdatedEnvironment;

                    AccountCompanyTb.Text = Assistant.UpdatedEnvironmentData[1];
                    AccountNameTb.Text = Assistant.UpdatedEnvironmentData[2];
                    AccountNumberNupd.Value = Convert.ToInt32(Assistant.UpdatedEnvironmentData[3]);
                    AccountCurrencyTb.Text = Assistant.UpdatedEnvironmentData[4];
                    LeverageNupd.Value = Convert.ToInt32(Assistant.UpdatedEnvironmentData[5]);
                }
            }

            if (AccountNameTb.Text.Length == 0)
            {
                AccountNameTb.Text = System.Environment.MachineName + " " + System.Environment.UserName;
            }

            if (SelectedNextPage == null)
            {
                SymbolsChl.Clear();
                SymbolsChl.AddItem("AUDUSD");
                SymbolsChl.AddItem("EURCAD");
                SymbolsChl.AddItem("EURCHF");
                SymbolsChl.AddItem("EURGBP");
                SymbolsChl.AddItem("EURJPY");
                SymbolsChl.AddItem("EURUSD");
                SymbolsChl.AddItem("GBPUSD");
                SymbolsChl.AddItem("USDCAD");
                SymbolsChl.AddItem("USDCHF");
                SymbolsChl.AddItem("USDJPY");
                SymbolsChl.AddItem("XAGUSD");
                SymbolsChl.SelectedItem = "EURUSD";
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
                return typeof(DukascopyPage1Controller).Name;
            }
            else
            {
                return null;
            }
        }

        private bool validate()
        {
            bool validationError = false;



            if (AccountCompanyTb.Text == "")
            {
                ErrorProvider1[AccountCompanyTb] = ("Account Company should not be empty.");
                validationError = true;
            }
            else
            {
                ErrorProvider1[AccountCompanyTb] = (null);
            }


            if (AccountNameTb.Text == "")
            {
                ErrorProvider1[AccountNameTb] = ("Account Name should not be empty.");
                validationError = true;
            }
            else
            {
                ErrorProvider1[AccountNameTb] = (null);
            }


            if (NameTb.Value == "")
            {
                ErrorProvider1[NameTb] = ("Name should not be empty.");
                validationError = true;
            }
            else if (Assistant.Environments.Contains(NameTb.Value) && NameTb.Value != Assistant.UpdatedEnvironment)
            {
                ErrorProvider1[NameTb] = ("Environment already exists.");
                validationError = true;
            }
            else
            {
                ErrorProvider1[NameTb] = (null);
            }


            if (AccountCurrencyTb.Text == "")
            {
                ErrorProvider1[AccountCurrencyTb] = ("Account Currency should not be empty.");
                validationError = true;
            }
            else
            {
                ErrorProvider1[AccountCurrencyTb] = (null);
            }


            if (LeverageNupd.Value < 10)
            {
                ErrorProvider1[LeverageNupd] = ("Account Leverage should be >= 10.");
                validationError = true;
            }
            else
            {
                ErrorProvider1[LeverageNupd] = (null);
            }


            SelectedSymbols.Clear();
            foreach (var s in SymbolsChl.SelectedItems)
            {
                SelectedSymbols.Add((string)s);
            }
            if (SelectedSymbols.Count == 0)
            {
                ErrorProvider1[SymbolsChl] = ("Please select symbols.");
                validationError = true;
            }
            else
            {
                ErrorProvider1[SymbolsChl] = (null);
            }


            ImportedParameters = new string[9];
            ImportedParameters[1] = AccountCompanyTb.Text == null ? null : AccountCompanyTb.Text.Trim();
            ImportedParameters[2] = AccountNameTb.Text == null ? null : AccountNameTb.Text.Trim();
            ImportedParameters[3] = "" + AccountNumberNupd.Value;
            EnvironmentName = NameTb.Value;
            ImportedParameters[4] = AccountCurrencyTb.Text;
            ImportedParameters[5] = "" + LeverageNupd.Value;
            // TODO stopout level, mode
            ImportedParameters[6] = "100";
            ImportedParameters[7] = "0";

            return !validationError;
        }

        private void fromDateP_ValueChanged(object sender, ControllerEventArgs e)
        {
            validate();
        }

        private void toDateP_ValueChanged(object sender, ControllerEventArgs e)
        {
            validate();
        }

        private void accountCompanyTb_TextChanged(object sender, ControllerEventArgs e)
        {
            if (string.IsNullOrEmpty(Assistant.UpdatedEnvironment))
            {
                NameTb.Value = AccountCompanyTb.Text + ", " + AccountNameTb.Text + ", " + AccountNumberNupd.Value;
            }
            validate();
        }

        private void accountNumberNupd_ValueChanged(object sender, ControllerEventArgs e)
        {
            if (string.IsNullOrEmpty(Assistant.UpdatedEnvironment))
            {
                NameTb.Value = AccountCompanyTb.Text + ", " + AccountNameTb.Text + ", " + AccountNumberNupd.Value;
            }
            validate();
        }

        private void accountNameTb_TextChanged(object sender, ControllerEventArgs e)
        {

            if (string.IsNullOrEmpty(Assistant.UpdatedEnvironment))
            {
                NameTb.Value = AccountCompanyTb.Text + ", " + AccountNameTb.Text + ", " + AccountNumberNupd.Value;
            }
            validate();
        }

        private void nameTb_TextChanged(object sender, ControllerEventArgs e)
        {
            validate();
        }

        private void accountCurrencyTb_TextChanged(object sender, ControllerEventArgs e)
        {
            validate();
        }

        private void leverageNupd_ValueChanged(object sender, ControllerEventArgs e)
        {
            validate();
        }

        private void symbolsChl_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            validate();
        }

    }
}
