using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Submarine.LicenseServer
{
    public partial class Form1 : Form
    {
        bool enableExit = false;
        bool visible = false;

        public Form1()
        {
            InitializeComponent();

            var _textWriter = new TextBoxWriter(consoleTextBox);
            System.Console.SetOut(_textWriter);
#if (DEBUG)
            Console.WriteLine("Console has been redirected.");
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
#else
            Console.WriteLine("");
#endif
        }

        internal LicenseServerApplicationContext AppContext {get; set;}

        internal void UpdateIsRunning()
        {
            if (AppContext.Running)
            {
                serverInfoLab.Text = "Server is running.";
                startStopBtn.Text = "Stop server";
                portTextBox.Enabled = false;
            }
            else
            {
                serverInfoLab.Text = "Server is stopped.";
                startStopBtn.Text = "Start server";
                portTextBox.Enabled = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (visible)
            {
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
            AppContext.Exit();
        }

        private void startStopBtn_Click(object sender, EventArgs e)
        {
            if (AppContext.Running)
            {
                AppContext.Stop();
            }
            else
            {
                AppContext.Start();
            }
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
