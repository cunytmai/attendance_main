namespace attendance_tracker
{
    partial class adminForm
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
            this.adminTabControl1 = new AetherTheme.AetherTabControl();
            this.homeTabPage = new System.Windows.Forms.TabPage();
            this.updateTabPage = new System.Windows.Forms.TabPage();
            this.deleteTabPage = new System.Windows.Forms.TabPage();
            this.adminTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // adminTabControl1
            // 
            this.adminTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.adminTabControl1.Controls.Add(this.homeTabPage);
            this.adminTabControl1.Controls.Add(this.updateTabPage);
            this.adminTabControl1.Controls.Add(this.deleteTabPage);
            this.adminTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adminTabControl1.ItemSize = new System.Drawing.Size(40, 190);
            this.adminTabControl1.Location = new System.Drawing.Point(0, 0);
            this.adminTabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.adminTabControl1.Multiline = true;
            this.adminTabControl1.Name = "adminTabControl1";
            this.adminTabControl1.SelectedIndex = 0;
            this.adminTabControl1.Size = new System.Drawing.Size(1067, 554);
            this.adminTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.adminTabControl1.TabIndex = 0;
            this.adminTabControl1.UpperText = true;
            // 
            // homeTabPage
            // 
            this.homeTabPage.BackColor = System.Drawing.Color.White;
            this.homeTabPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.homeTabPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.homeTabPage.Location = new System.Drawing.Point(194, 4);
            this.homeTabPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.homeTabPage.Name = "homeTabPage";
            this.homeTabPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.homeTabPage.Size = new System.Drawing.Size(869, 546);
            this.homeTabPage.TabIndex = 0;
            this.homeTabPage.Text = "Home";
            // 
            // updateTabPage
            // 
            this.updateTabPage.BackColor = System.Drawing.Color.White;
            this.updateTabPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.updateTabPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.updateTabPage.Location = new System.Drawing.Point(194, 4);
            this.updateTabPage.Name = "updateTabPage";
            this.updateTabPage.Size = new System.Drawing.Size(869, 546);
            this.updateTabPage.TabIndex = 1;
            this.updateTabPage.Text = "Update Users";
            // 
            // deleteTabPage
            // 
            this.deleteTabPage.BackColor = System.Drawing.Color.White;
            this.deleteTabPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.deleteTabPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.deleteTabPage.Location = new System.Drawing.Point(194, 4);
            this.deleteTabPage.Name = "deleteTabPage";
            this.deleteTabPage.Size = new System.Drawing.Size(869, 546);
            this.deleteTabPage.TabIndex = 2;
            this.deleteTabPage.Text = "Delete Users";
            // 
            // adminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.adminTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "adminForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "adminForm";
            this.adminTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AetherTheme.AetherTabControl adminTabControl1;
        private System.Windows.Forms.TabPage homeTabPage;
        private System.Windows.Forms.TabPage updateTabPage;
        private System.Windows.Forms.TabPage deleteTabPage;
    }
}