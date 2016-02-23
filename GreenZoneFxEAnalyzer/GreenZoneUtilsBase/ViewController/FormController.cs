using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    public interface IFormController : ILabelledController
    {
    }

    public class FormController : LabelledController, IFormController
    {
        public static event ShowDialogEventHandler ShowDialogInvoked;

        public event ControllerEventHandler FormClosing;
        public event ControllerEventHandler FormClosed;
        public event ControllerEventHandler FormLoad;
        public event PropertyChangedEventHandler AcceptButtonChanged;
        public event PropertyChangedEventHandler CancelButtonChanged;
        public event PropertyChangedEventHandler DialogResultChanged;
        public event PropertyChangedEventHandler AllowMinimizeChanged;
        public event PropertyChangedEventHandler AllowMaximizeChanged;
        public event PropertyChangedEventHandler ShowInTaskbarChanged;

        public FormController(GreenRmiManager rmiManager)
            : base(rmiManager, (Controller)null)
        {
        }

        public FormController(GreenRmiManager rmiManager, string text)
            : base(rmiManager, null, text)
        {
        }

        public FormController(GreenRmiManager rmiManager, string text, int image)
            : base(rmiManager, null, text, image)
        {
        }

        public FormController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected FormController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            AcceptButton = (ButtonController) info.GetValue("AcceptButton", typeof(ButtonController));
            CancelButton = (ButtonController) info.GetValue("CancelButton", typeof(ButtonController));
            DialogResult = (DialogResult)info.GetValue("DialogResult", typeof(DialogResult));
            AllowMinimize = (bool) info.GetValue("AllowMinimize", typeof(bool));
            AllowMaximize = (bool) info.GetValue("AllowMaximize", typeof(bool));
            ShowInTaskbar = (bool) info.GetValue("ShowInTaskbar", typeof(bool));
        }


        ButtonController acceptButton;
        const int PROPERTY_16_ACCEPTBUTTON_ID = 16;
        public ButtonController AcceptButton
        {
            get
            {
                return acceptButton;
            }
            set
            {
                if (acceptButton != value)
                {
                    acceptButton = value;
                    changed[PROPERTY_16_ACCEPTBUTTON_ID] = true;
                    somethingChanged = true;
                    if (AcceptButtonChanged != null)
                    {
                        AcceptButtonChanged(this, new PropertyChangedEventArgs("AcceptButton", value));
                    }
                }
            }
        }

        ButtonController cancelButton;
        const int PROPERTY_17_CANCELBUTTON_ID = 17;
        public ButtonController CancelButton
        {
            get
            {
                return cancelButton;
            }
            set
            {
                if (cancelButton != value)
                {
                    cancelButton = value;
                    changed[PROPERTY_17_CANCELBUTTON_ID] = true;
                    somethingChanged = true;
                    if (CancelButtonChanged != null)
                    {
                        CancelButtonChanged(this, new PropertyChangedEventArgs("CancelButton", value));
                    }
                }
            }
        }

        DialogResult dialogResult;
        const int PROPERTY_18_DIALOGRESULT_ID = 18;
        public DialogResult DialogResult
        {
            get
            {
                return dialogResult;
            }
            protected set
            {
                if (dialogResult != value)
                {
                    dialogResult = value;
                    changed[PROPERTY_18_DIALOGRESULT_ID] = true;
                    somethingChanged = true;
                    if (DialogResultChanged != null)
                    {
                        DialogResultChanged(this, new PropertyChangedEventArgs("DialogResult", value));
                    }
                }
            }
        }

        bool allowMinimize = true;
        const int PROPERTY_19_ALLOWMINIMIZE_ID = 19;
        public bool AllowMinimize
        {
            get
            {
                return allowMinimize;
            }
            set
            {
                if (allowMinimize != value)
                {
                    allowMinimize = value;
                    changed[PROPERTY_19_ALLOWMINIMIZE_ID] = true;
                    somethingChanged = true;
                    if (AllowMinimizeChanged != null)
                    {
                        AllowMinimizeChanged(this, new PropertyChangedEventArgs("AllowMinimize", value));
                    }
                }
            }
        }


        bool allowMaximize = true;
        const int PROPERTY_20_ALLOWMAXIMIZE_ID = 20;
        public bool AllowMaximize
        {
            get
            {
                return allowMaximize;
            }
            set
            {
                if (allowMaximize != value)
                {
                    allowMaximize = value;
                    changed[PROPERTY_20_ALLOWMAXIMIZE_ID] = true;
                    somethingChanged = true;
                    if (AllowMaximizeChanged != null)
                    {
                        AllowMaximizeChanged(this, new PropertyChangedEventArgs("AllowMaximize", value));
                    }
                }
            }
        }


        bool showInTaskbar = true;
        const int PROPERTY_21_SHOWINTASKBAR_ID = 21;
        public bool ShowInTaskbar
        {
            get
            {
                return showInTaskbar;
            }
            set
            {
                if (showInTaskbar != value)
                {
                    showInTaskbar = value;
                    changed[PROPERTY_21_SHOWINTASKBAR_ID] = true;
                    somethingChanged = true;
                    if (ShowInTaskbarChanged != null)
                    {
                        ShowInTaskbarChanged(this, new PropertyChangedEventArgs("ShowInTaskbar", value));
                    }
                }
            }
        }

        public DialogResult ShowDialog()
        {
            ShowDialogEventArgs a = new ShowDialogEventArgs(this);
            ShowDialogInvoked(a);
            dialogResult = a.DialogResult;
            return dialogResult;
        }

        public DialogResult ShowDialog(Controller invoker)
        {
            ShowDialogEventArgs a = new ShowDialogEventArgs(this, invoker);
            ShowDialogInvoked(a);
            dialogResult = a.DialogResult;
            return dialogResult;
        }

        protected void ShowDialog(ShowDialogEventArgs a)
        {
            ShowDialogInvoked(a);
            dialogResult = a.DialogResult;
        }

        public void Closing()
        {
            OnClosing((ControllerEventArgs)null);
        }
        public void ForceClose()
        {
            OnClose(null);
        }
        void Close()
        {
            OnClose(null);
        }
        public void Load()
        {
            OnLoad(null);
        }

        public virtual void OnClosing(System.ComponentModel.CancelEventArgs args)
        {
            FormControllerEventArgs fe = new FormControllerEventArgs((System.ComponentModel.CancelEventArgs)args);
            OnClosing(fe);
        }

        public virtual void OnClosing(ControllerEventArgs args)
        {
            if (FormClosing != null)
            {
                FormControllerEventArgs fe;
                if (args == null)
                {
                    fe = new FormControllerEventArgs();
                }
                else if (args.Consumed)
                {
                    return;
                }
                else
                {
                    fe = (FormControllerEventArgs)args;
                }

                FormClosing(this, fe);
                if (args == null && !fe.Cancel)
                {
                    OnClose(args);
                }
                args.Consume();
            }
            else
            {
                Close();
            }
        }

        public virtual void OnClose(ControllerEventArgs args)
        {
            if (FormClosed != null)
            {
                if (args == null)
                {
                    args = new FormControllerEventArgs();
                }
                else if (args.Consumed)
                {
                    return;
                }

                FormClosed(this, args);
                args.Consume();
            }
        }

        /// <seealso cref="System.Windows.Forms.Form.OnLoad">
        /// System.Windows.Forms.Form.OnLoad
        /// </seealso>
        protected virtual void OnLoad(FormControllerEventArgs args)
        {
            if (FormLoad != null)
            {
                if (args == null)
                {
                    args = new FormControllerEventArgs();
                }
                else if (args.Consumed)
                {
                    return;
                }

                FormLoad(this, args);
                args.Consume();
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_16_ACCEPTBUTTON_ID:
                    return AcceptButton;
                case PROPERTY_17_CANCELBUTTON_ID:
                    return CancelButton;
                case PROPERTY_18_DIALOGRESULT_ID:
                    return DialogResult;
                case PROPERTY_19_ALLOWMINIMIZE_ID:
                    return AllowMinimize;
                case PROPERTY_20_ALLOWMAXIMIZE_ID:
                    return AllowMaximize;
                case PROPERTY_21_SHOWINTASKBAR_ID:
                    return ShowInTaskbar;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_16_ACCEPTBUTTON_ID:
                    AcceptButton = (ButtonController)value;
                    break;
                case PROPERTY_17_CANCELBUTTON_ID:
                    CancelButton = (ButtonController)value;
                    break;
                case PROPERTY_18_DIALOGRESULT_ID:
                    DialogResult = (DialogResult)value;
                    break;
                case PROPERTY_19_ALLOWMINIMIZE_ID:
                    AllowMinimize = (bool)value;
                    break;
                case PROPERTY_20_ALLOWMAXIMIZE_ID:
                    AllowMaximize = (bool)value;
                    break;
                case PROPERTY_21_SHOWINTASKBAR_ID:
                    ShowInTaskbar = (bool)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("AcceptButton", AcceptButton);
            info.AddValue("CancelButton", CancelButton);
            info.AddValue("DialogResult", DialogResult);
            info.AddValue("AllowMinimize", AllowMinimize);
            info.AddValue("AllowMaximize", AllowMaximize);
            info.AddValue("ShowInTaskbar", ShowInTaskbar);
        }
    }
}
