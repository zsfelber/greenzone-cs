using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class MessageBoxController : DialogController
    {
        static MessageBoxController singleton;
        static void init(GreenRmiManager rmiManager)
        {
            singleton = new MessageBoxController(rmiManager);
        }

        private MessageBoxController(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        public static DialogResult Show(    GreenRmiManager rmiManager,
                                            string text,
                                            string caption,
                                            MessageBoxButtons buttons
                                        )
        {
            if (singleton == null)
            {
                init(rmiManager);
            }
            MessageBoxEventArgs args = new MessageBoxEventArgs(singleton);
            args.Text = text;
            args.Caption = caption;
            args.Buttons = buttons;
            singleton.ShowDialog(args);
            return singleton.DialogResult;
        }

        public static DialogResult Show(    GreenRmiManager rmiManager,
                                            string text,
                                            string caption,
                                            MessageBoxButtons buttons,
                                            MessageBoxIcon icon
                                        )
        {
            if (singleton == null)
            {
                init(rmiManager);
            }
            MessageBoxEventArgs args = new MessageBoxEventArgs(singleton);
            args.Text = text;
            args.Caption = caption;
            args.Buttons = buttons;
            args.Icon = icon;
            singleton.ShowDialog(args);
            return singleton.DialogResult;
        }

        public static DialogResult Show(    GreenRmiManager rmiManager,
                                            Controller invoker,
                                            string text,
                                            string caption,
                                            MessageBoxButtons buttons,
                                            MessageBoxIcon icon
                                        )
        {
            if (singleton == null)
            {
                init(rmiManager);
            }
            MessageBoxEventArgs args = new MessageBoxEventArgs(singleton, invoker);
            args.Text = text;
            args.Caption = caption;
            args.Buttons = buttons;
            args.Icon = icon;
            singleton.ShowDialog(args);
            return singleton.DialogResult;
        }
    }

    public class MessageBoxEventArgs : ShowDialogEventArgs
    {
        internal MessageBoxEventArgs(MessageBoxController dialog)
            : base(dialog)
        {
        }

        internal MessageBoxEventArgs(MessageBoxController dialog, Controller invoker)
            : base(dialog, invoker)
        {
        }

        string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        string caption;
        public string Caption
        {
            get
            {
                return caption;
            }
            set
            {
                caption = value;
            }
        }

        MessageBoxButtons buttons;
        public MessageBoxButtons Buttons
        {
            get
            {
                return buttons;
            }
            set
            {
                buttons = value;
            }
        }

        MessageBoxIcon icon;
        public MessageBoxIcon Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
            }
        }
    }
}
