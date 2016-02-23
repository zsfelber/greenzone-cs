namespace GreenZoneFxBuildTasks
{
    partial class Form1
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
            this.testParserButton = new System.Windows.Forms.Button();
            this.testCsCompilerButton = new System.Windows.Forms.Button();
            this.generateFxBase = new System.Windows.Forms.Button();
            this.genReflButton = new System.Windows.Forms.Button();
            this.debugButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // testParserButton
            // 
            this.testParserButton.Location = new System.Drawing.Point(324, 12);
            this.testParserButton.Name = "testParserButton";
            this.testParserButton.Size = new System.Drawing.Size(105, 23);
            this.testParserButton.TabIndex = 0;
            this.testParserButton.Text = "Test parser";
            this.testParserButton.UseVisualStyleBackColor = true;
            this.testParserButton.Click += new System.EventHandler(this.testCsParserButton_Click);
            // 
            // testCsCompilerButton
            // 
            this.testCsCompilerButton.Location = new System.Drawing.Point(324, 41);
            this.testCsCompilerButton.Name = "testCsCompilerButton";
            this.testCsCompilerButton.Size = new System.Drawing.Size(105, 23);
            this.testCsCompilerButton.TabIndex = 1;
            this.testCsCompilerButton.Text = "Test c# compiler";
            this.testCsCompilerButton.UseVisualStyleBackColor = true;
            this.testCsCompilerButton.Click += new System.EventHandler(this.testCsCompilerButton_Click);
            // 
            // generateFxBase
            // 
            this.generateFxBase.Location = new System.Drawing.Point(324, 84);
            this.generateFxBase.Name = "generateFxBase";
            this.generateFxBase.Size = new System.Drawing.Size(105, 23);
            this.generateFxBase.TabIndex = 2;
            this.generateFxBase.Text = "Generate Fx Base";
            this.generateFxBase.UseVisualStyleBackColor = true;
            this.generateFxBase.Click += new System.EventHandler(this.generateFxBase_Click);
            // 
            // genReflButton
            // 
            this.genReflButton.Location = new System.Drawing.Point(324, 113);
            this.genReflButton.Name = "genReflButton";
            this.genReflButton.Size = new System.Drawing.Size(105, 23);
            this.genReflButton.TabIndex = 3;
            this.genReflButton.Text = "Generate Refl.";
            this.genReflButton.UseVisualStyleBackColor = true;
            this.genReflButton.Click += new System.EventHandler(this.genReflButton_Click);
            // 
            // debugButton
            // 
            this.debugButton.Location = new System.Drawing.Point(324, 167);
            this.debugButton.Name = "debugButton";
            this.debugButton.Size = new System.Drawing.Size(105, 23);
            this.debugButton.TabIndex = 4;
            this.debugButton.Text = "Debug ...";
            this.debugButton.UseVisualStyleBackColor = true;
            this.debugButton.Click += new System.EventHandler(this.debugButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 309);
            this.Controls.Add(this.debugButton);
            this.Controls.Add(this.genReflButton);
            this.Controls.Add(this.generateFxBase);
            this.Controls.Add(this.testCsCompilerButton);
            this.Controls.Add(this.testParserButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button testParserButton;
        private System.Windows.Forms.Button testCsCompilerButton;
        private System.Windows.Forms.Button generateFxBase;
        private System.Windows.Forms.Button genReflButton;
        private System.Windows.Forms.Button debugButton;
    }
}

