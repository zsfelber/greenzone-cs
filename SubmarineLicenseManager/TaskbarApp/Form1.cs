using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace FxSubmarineTaskbarApp
{
    public partial class Form1 : Form
    {
        bool enableExit = false;
        bool visible = false;

        public Form1()
        {
            InitializeComponent();
            var _textWriter = new TextBoxWriter(consoleOutBox);
            System.Console.SetOut(_textWriter);
#if (DEBUG)
            Console.WriteLine("Console has been redirected.");
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
#else
            Console.WriteLine("");
#endif
        }

        private void notifyIcon1_DblClick(object sender, EventArgs e)
        {
            if (visible) {
                Hide();
                visible = false;
            }
            else
            {
                Show();
                visible = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            visible = false;
            e.Cancel = !enableExit; // this cancels the close event.
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        internal void Exit()
        {
            enableExit = true;
            notifyIcon1.Visible = false;
            Application.Exit();
        }

    }

    internal class TextBoxWriter : TextWriter
    {
        TextBox _output;

        public TextBoxWriter(TextBox output)
        {
            _output = output;
        }

        public override void WriteLine(string value)
        {
            Write(value + System.Console.Out.NewLine);
        }

        public override void Write(string value)
        {
            if (_output.InvokeRequired)
            {
                _output.BeginInvoke((Action<string>)Write, value);
            }
            else
            {
                _output.AppendText(value);
            }
        }

        public override void Write(char value)
        {
            Write(value.ToString());
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
