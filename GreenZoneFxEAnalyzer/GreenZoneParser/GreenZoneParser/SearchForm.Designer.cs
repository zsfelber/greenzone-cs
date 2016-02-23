namespace GreenZoneParser
{
    partial class SearchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.searchTb = new System.Windows.Forms.TextBox();
            this.caseSensitiveChb = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.goToLineNupd = new System.Windows.Forms.NumericUpDown();
            this.SearchButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this.goToLineButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.goToLineNupd)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search";
            // 
            // searchTb
            // 
            this.searchTb.Location = new System.Drawing.Point(74, 23);
            this.searchTb.Name = "searchTb";
            this.searchTb.Size = new System.Drawing.Size(206, 20);
            this.searchTb.TabIndex = 1;
            // 
            // caseSensitiveChb
            // 
            this.caseSensitiveChb.AutoSize = true;
            this.caseSensitiveChb.Checked = true;
            this.caseSensitiveChb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.caseSensitiveChb.Location = new System.Drawing.Point(186, 49);
            this.caseSensitiveChb.Name = "caseSensitiveChb";
            this.caseSensitiveChb.Size = new System.Drawing.Size(94, 17);
            this.caseSensitiveChb.TabIndex = 2;
            this.caseSensitiveChb.Text = "Case sensitive";
            this.caseSensitiveChb.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Go to Line";
            // 
            // goToLineNupd
            // 
            this.goToLineNupd.Location = new System.Drawing.Point(74, 78);
            this.goToLineNupd.Name = "goToLineNupd";
            this.goToLineNupd.Size = new System.Drawing.Size(103, 20);
            this.goToLineNupd.TabIndex = 6;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(123, 134);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 7;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // CancelButton
            // 
            this._CancelButton.Location = new System.Drawing.Point(204, 134);
            this._CancelButton.Name = "CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(75, 23);
            this._CancelButton.TabIndex = 8;
            this._CancelButton.Text = "Cancel";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // goToLineButton
            // 
            this.goToLineButton.Location = new System.Drawing.Point(204, 75);
            this.goToLineButton.Name = "goToLineButton";
            this.goToLineButton.Size = new System.Drawing.Size(75, 23);
            this.goToLineButton.TabIndex = 9;
            this.goToLineButton.Text = "Go";
            this.goToLineButton.UseVisualStyleBackColor = true;
            this.goToLineButton.Click += new System.EventHandler(this.goToLineButton_Click);
            // 
            // SearchForm
            // 
            this.AcceptButton = this.SearchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(287, 169);
            this.Controls.Add(this.goToLineButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.goToLineNupd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.caseSensitiveChb);
            this.Controls.Add(this.searchTb);
            this.Controls.Add(this.label1);
            this.Name = "SearchForm";
            this.Text = "SearchForm";
            ((System.ComponentModel.ISupportInitialize)(this.goToLineNupd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchTb;
        private System.Windows.Forms.CheckBox caseSensitiveChb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown goToLineNupd;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button goToLineButton;
    }
}