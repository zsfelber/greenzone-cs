using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreenZoneParser
{
    public partial class SearchForm : Form
    {
        ParseTesterForm form1;

        public SearchForm(ParseTesterForm form1)
        {
            this.form1 = form1;
            InitializeComponent();

            goToLineNupd.Minimum = 1;
            goToLineNupd.Maximum = form1.LineCount;
        }

        private void goToLineButton_Click(object sender, EventArgs e)
        {
            form1.GoToLine((int)goToLineNupd.Value);
            Close();
            Dispose();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            form1.Search(searchTb.Text, caseSensitiveChb.Checked ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }
    }
}
