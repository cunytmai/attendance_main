namespace attendance_tracker
{
    partial class class_breakdown
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
            this.aetherTabControl1 = new AetherTheme.AetherTabControl();
            this.class_stats = new System.Windows.Forms.TabPage();
            this.aetherTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // aetherTabControl1
            // 
            this.aetherTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.aetherTabControl1.Controls.Add(this.class_stats);
            this.aetherTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aetherTabControl1.ItemSize = new System.Drawing.Size(40, 190);
            this.aetherTabControl1.Location = new System.Drawing.Point(0, 0);
            this.aetherTabControl1.Multiline = true;
            this.aetherTabControl1.Name = "aetherTabControl1";
            this.aetherTabControl1.SelectedIndex = 0;
            this.aetherTabControl1.Size = new System.Drawing.Size(800, 450);
            this.aetherTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.aetherTabControl1.TabIndex = 0;
            this.aetherTabControl1.UpperText = true;
            // 
            // class_stats
            // 
            this.class_stats.BackColor = System.Drawing.Color.White;
            this.class_stats.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.class_stats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.class_stats.Location = new System.Drawing.Point(194, 4);
            this.class_stats.Name = "class_stats";
            this.class_stats.Padding = new System.Windows.Forms.Padding(3);
            this.class_stats.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.class_stats.Size = new System.Drawing.Size(602, 442);
            this.class_stats.TabIndex = 0;
            this.class_stats.Text = "stats";
            // 
            // class_breakdown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.aetherTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "class_breakdown";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "class_breakdown";
            this.aetherTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AetherTheme.AetherTabControl aetherTabControl1;
        private System.Windows.Forms.TabPage class_stats;
    }
}