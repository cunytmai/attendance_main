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
            this.userTabPage = new System.Windows.Forms.TabPage();
            this.addUserTabPage = new System.Windows.Forms.TabPage();
            this.groupTabPage = new System.Windows.Forms.TabPage();
            this.addGroupTabPage = new System.Windows.Forms.TabPage();
            this.contactTabPage = new System.Windows.Forms.TabPage();
            this.adminTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // adminTabControl1
            // 
            this.adminTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.adminTabControl1.Controls.Add(this.homeTabPage);
            this.adminTabControl1.Controls.Add(this.userTabPage);
            this.adminTabControl1.Controls.Add(this.addUserTabPage);
            this.adminTabControl1.Controls.Add(this.groupTabPage);
            this.adminTabControl1.Controls.Add(this.addGroupTabPage);
            this.adminTabControl1.Controls.Add(this.contactTabPage);
            this.adminTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adminTabControl1.ItemSize = new System.Drawing.Size(40, 190);
            this.adminTabControl1.Location = new System.Drawing.Point(0, 0);
            this.adminTabControl1.Multiline = true;
            this.adminTabControl1.Name = "adminTabControl1";
            this.adminTabControl1.SelectedIndex = 0;
            this.adminTabControl1.Size = new System.Drawing.Size(800, 450);
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
            this.homeTabPage.Name = "homeTabPage";
            this.homeTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.homeTabPage.Size = new System.Drawing.Size(602, 442);
            this.homeTabPage.TabIndex = 0;
            this.homeTabPage.Text = "Home";
            // 
            // userTabPage
            // 
            this.userTabPage.BackColor = System.Drawing.Color.White;
            this.userTabPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.userTabPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.userTabPage.Location = new System.Drawing.Point(194, 4);
            this.userTabPage.Name = "userTabPage";
            this.userTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.userTabPage.Size = new System.Drawing.Size(602, 442);
            this.userTabPage.TabIndex = 1;
            this.userTabPage.Text = "search user";
            // 
            // addUserTabPage
            // 
            this.addUserTabPage.BackColor = System.Drawing.Color.White;
            this.addUserTabPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.addUserTabPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.addUserTabPage.Location = new System.Drawing.Point(194, 4);
            this.addUserTabPage.Name = "addUserTabPage";
            this.addUserTabPage.Size = new System.Drawing.Size(602, 442);
            this.addUserTabPage.TabIndex = 2;
            this.addUserTabPage.Text = "add user";
            // 
            // groupTabPage
            // 
            this.groupTabPage.BackColor = System.Drawing.Color.White;
            this.groupTabPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupTabPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.groupTabPage.Location = new System.Drawing.Point(194, 4);
            this.groupTabPage.Name = "groupTabPage";
            this.groupTabPage.Size = new System.Drawing.Size(602, 442);
            this.groupTabPage.TabIndex = 3;
            this.groupTabPage.Text = "search group";
            // 
            // addGroupTabPage
            // 
            this.addGroupTabPage.BackColor = System.Drawing.Color.White;
            this.addGroupTabPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.addGroupTabPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.addGroupTabPage.Location = new System.Drawing.Point(194, 4);
            this.addGroupTabPage.Name = "addGroupTabPage";
            this.addGroupTabPage.Size = new System.Drawing.Size(602, 442);
            this.addGroupTabPage.TabIndex = 4;
            this.addGroupTabPage.Text = "add group";
            // 
            // contactTabPage
            // 
            this.contactTabPage.BackColor = System.Drawing.Color.White;
            this.contactTabPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.contactTabPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.contactTabPage.Location = new System.Drawing.Point(194, 4);
            this.contactTabPage.Name = "contactTabPage";
            this.contactTabPage.Size = new System.Drawing.Size(602, 442);
            this.contactTabPage.TabIndex = 5;
            this.contactTabPage.Text = "contact";
            // 
            // adminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.adminTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
        private System.Windows.Forms.TabPage userTabPage;
        private System.Windows.Forms.TabPage addUserTabPage;
        private System.Windows.Forms.TabPage groupTabPage;
        private System.Windows.Forms.TabPage addGroupTabPage;
        private System.Windows.Forms.TabPage contactTabPage;
    }
}