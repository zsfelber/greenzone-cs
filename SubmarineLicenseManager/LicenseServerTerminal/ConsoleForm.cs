using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Submarine.LicenseServerTerminal
{
    public partial class ConsoleForm : Form
    {
        public ConsoleForm()
        {
            InitializeComponent();
            var _textWriter = new TextBoxWriter(textBox1);
            System.Console.SetOut(_textWriter);
#if (DEBUG)
            Console.WriteLine("Console has been redirected.");
            textBox1.ReadOnly = false;
#else
            Console.WriteLine("");
#endif
        }

        private void ConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel=true;
            Hide();
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
