namespace UNO
{
    partial class RoundResultCellCtrl
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
            this.components = new System.ComponentModel.Container();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.winnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // valueTextBox
            // 
            this.valueTextBox.Location = new System.Drawing.Point(4, 14);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(43, 20);
            this.valueTextBox.TabIndex = 0;
            this.valueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.valueTextBox.Visible = false;
            this.valueTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.valueTextBox_KeyUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.winnerToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // winnerToolStripMenuItem
            // 
            this.winnerToolStripMenuItem.Name = "winnerToolStripMenuItem";
            this.winnerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.winnerToolStripMenuItem.Text = "Winner";
            this.winnerToolStripMenuItem.Click += new System.EventHandler(this.winnerToolStripMenuItem_Click);
            // 
            // RoundResultCellCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::UNO.Properties.Resources.RoundResult;
            this.Controls.Add(this.valueTextBox);
            this.Name = "RoundResultCellCtrl";
            this.Size = new System.Drawing.Size(50, 51);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RoundResultCellCtrl_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RoundResultCellCtrl_MouseDoubleClick);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RoundResultCellCtrl_MouseClick);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem winnerToolStripMenuItem;
    }
}
