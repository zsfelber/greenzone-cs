using GreenZoneFxEngine.Trading;
using GreenZoneUtil.Gui.PropertyGrid;
using System.Windows.Forms;
namespace GreenZoneFxEngine
{
    partial class ExecPropertiesPanel<R, I, S, A>
        where R : ExecRuntime<R, I, S, A>
        where I : Mt4ExecutableInfo<R, A>
        where S : ExecSession<R,A>
        where A : ExecAttribute
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.propertyGrid1 = new PropertyGrid();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(408, 384);
            this.propertyGrid1.TabIndex = 0;
            // 
            // ScriptPropertiesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertyGrid1);
            this.Name = "ExecPropertiesPanel";
            this.Size = new System.Drawing.Size(408, 384);
            this.ResumeLayout(false);

        }

        #endregion

        private PropertyGrid propertyGrid1;
    }
}
