namespace UNO
{
    partial class GameTittlesDlg
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
            this.SuspendLayout();
            // 
            // GameTittlesDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::UNO.Properties.Resources.BAckGridSample;
            this.ClientSize = new System.Drawing.Size(494, 204);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameTittlesDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Этап UNO";
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GameTittlesDlg_MouseDoubleClick);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameTittlesDlg_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameTittlesDlg_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion


    }
}