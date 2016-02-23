using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreenZoneUtil.Gui
{
    public partial class CharMapDialog : Form
    {
        public event DelegateCharSelected CharSelected;

        public delegate void DelegateCharSelected(char c);

        char selectedChar;
        string selectedFont;

        public CharMapDialog()
        {
            InitializeComponent();
        }

        public CharMapDialog(string selectedFont, char selectedChar)
        {
            InitializeComponent();

            SelectedFont = selectedFont;
            SelectedChar = selectedChar;
        }

        public char SelectedChar
        {
            get
            {
                return selectedChar;
            }
            set
            {
                selectedChar = value;
                selectedCharLabel.Text = "" + selectedChar;
                int i = selectedChar;
                dataGridView1.CurrentCell = dataGridView1.Rows[i / 10].Cells[2 * (i % 10)];
            }
        }

        public string SelectedFont
        {
            get
            {
                return selectedFont;
            }
            set
            {
                this.selectedFont = value;

                Font font = new Font(value,10f);
                selectedCharLabel.Font = new Font(value,15f);

                this.dataGridView1.Rows.Clear();
                this.dataGridView1.Columns.Clear();

                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = font;

                for (int i = 0; i < 10; i++)
                {
                    DataGridViewColumn c1 = new DataGridViewTextBoxColumn();
                    DataGridViewColumn c2 = new DataGridViewColumn(new StringDrawerCell());
                    c1.MinimumWidth = 35;
                    c1.ReadOnly = true;
                    c1.Width = 35;

                    c2.MinimumWidth = 20;
                    c2.ReadOnly = true;
                    c2.Width = 20;
                    c2.DefaultCellStyle.ApplyStyle(style);

                    this.dataGridView1.Columns.Add(c1);
                    this.dataGridView1.Columns.Add(c2);
                }

                int ch = 0;
                for (int i = 0; i < 26; i++)
                {
                    List<string> itms = new List<string>();
                    for (int j = 0; j < 10; j++)
                    {
                        itms.Add(ch + ":");
                        itms.Add("" + (char)ch);
                        ch++;
                        if (ch == 256)
                        {
                            break;
                        }
                    }

                    dataGridView1.Rows.Add(itms.ToArray());
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            int column = dataGridView1.CurrentCell.ColumnIndex / 2;
            int code = row * 10 + column;

            SelectedChar = (char)code;

            if (CharSelected != null)
            {
                CharSelected(SelectedChar);
            }
        }
    }

    class StringDrawerCell : DataGridViewTextBoxCell
    {
        Brush brush;

        public StringDrawerCell() : base()
        {                
        }

        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            Object value,
            Object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts
        )
        {
            if (brush == null) {
                brush = new SolidBrush(cellStyle.ForeColor);
            }

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, "", "", "", cellStyle, advancedBorderStyle, paintParts);

            graphics.DrawString((string)formattedValue, cellStyle.Font, brush, cellBounds.X, cellBounds.Y);
        }
    }
}
