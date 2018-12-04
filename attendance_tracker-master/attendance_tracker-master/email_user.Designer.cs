namespace attendance_tracker
{
    partial class email_user
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
            this.email_tab = new System.Windows.Forms.TabPage();
            this.aetherGroupBox1 = new AetherTheme.AetherGroupBox();
            this.aetherGroupBox2 = new AetherTheme.AetherGroupBox();
            this.aetherButton1 = new AetherTheme.AetherButton();
            this.aetherTextbox2 = new AetherTheme.AetherTextbox();
            this.aetherTextbox1 = new AetherTheme.AetherTextbox();
            this.aetherTabControl1.SuspendLayout();
            this.email_tab.SuspendLayout();
            this.aetherGroupBox1.SuspendLayout();
            this.aetherGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // aetherTabControl1
            // 
            this.aetherTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.aetherTabControl1.Controls.Add(this.email_tab);
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
            // email_tab
            // 
            this.email_tab.BackColor = System.Drawing.Color.White;
            this.email_tab.Controls.Add(this.aetherGroupBox1);
            this.email_tab.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.email_tab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.email_tab.Location = new System.Drawing.Point(194, 4);
            this.email_tab.Name = "email_tab";
            this.email_tab.Padding = new System.Windows.Forms.Padding(3);
            this.email_tab.Size = new System.Drawing.Size(602, 442);
            this.email_tab.TabIndex = 0;
            this.email_tab.Text = "email";
            // 
            // aetherGroupBox1
            // 
            this.aetherGroupBox1.Controls.Add(this.aetherGroupBox2);
            this.aetherGroupBox1.Controls.Add(this.aetherTextbox1);
            this.aetherGroupBox1.Footer = false;
            this.aetherGroupBox1.FooterText = null;
            this.aetherGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.aetherGroupBox1.Name = "aetherGroupBox1";
            this.aetherGroupBox1.Size = new System.Drawing.Size(603, 436);
            this.aetherGroupBox1.TabIndex = 0;
            this.aetherGroupBox1.Text = "Subject";
            // 
            // aetherGroupBox2
            // 
            this.aetherGroupBox2.Controls.Add(this.aetherButton1);
            this.aetherGroupBox2.Controls.Add(this.aetherTextbox2);
            this.aetherGroupBox2.Footer = false;
            this.aetherGroupBox2.FooterText = null;
            this.aetherGroupBox2.Location = new System.Drawing.Point(3, 90);
            this.aetherGroupBox2.Name = "aetherGroupBox2";
            this.aetherGroupBox2.Size = new System.Drawing.Size(588, 343);
            this.aetherGroupBox2.TabIndex = 1;
            this.aetherGroupBox2.Text = "Message";
            // 
            // aetherButton1
            // 
            this.aetherButton1.EnabledCalc = true;
            this.aetherButton1.Location = new System.Drawing.Point(124, 315);
            this.aetherButton1.Name = "aetherButton1";
            this.aetherButton1.Size = new System.Drawing.Size(305, 23);
            this.aetherButton1.TabIndex = 1;
            this.aetherButton1.Text = "Email";
            this.aetherButton1.Click += new AetherTheme.AetherButton.ClickEventHandler(this.aetherButton1_Click);
            // 
            // aetherTextbox2
            // 
            this.aetherTextbox2.EnabledCalc = true;
            this.aetherTextbox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.aetherTextbox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.aetherTextbox2.Location = new System.Drawing.Point(4, 35);
            this.aetherTextbox2.MaxLength = 32767;
            this.aetherTextbox2.MultiLine = true;
            this.aetherTextbox2.Name = "aetherTextbox2";
            this.aetherTextbox2.ReadOnly = false;
            this.aetherTextbox2.Size = new System.Drawing.Size(581, 272);
            this.aetherTextbox2.TabIndex = 0;
            this.aetherTextbox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.aetherTextbox2.UseSystemPasswordChar = false;
            // 
            // aetherTextbox1
            // 
            this.aetherTextbox1.EnabledCalc = true;
            this.aetherTextbox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.aetherTextbox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.aetherTextbox1.Location = new System.Drawing.Point(3, 41);
            this.aetherTextbox1.MaxLength = 32767;
            this.aetherTextbox1.MultiLine = false;
            this.aetherTextbox1.Name = "aetherTextbox1";
            this.aetherTextbox1.ReadOnly = false;
            this.aetherTextbox1.Size = new System.Drawing.Size(588, 29);
            this.aetherTextbox1.TabIndex = 0;
            this.aetherTextbox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.aetherTextbox1.UseSystemPasswordChar = false;
            // 
            // email_user
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.aetherTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "email_user";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "email_user";
            this.aetherTabControl1.ResumeLayout(false);
            this.email_tab.ResumeLayout(false);
            this.aetherGroupBox1.ResumeLayout(false);
            this.aetherGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AetherTheme.AetherTabControl aetherTabControl1;
        private System.Windows.Forms.TabPage email_tab;
        private AetherTheme.AetherGroupBox aetherGroupBox1;
        private AetherTheme.AetherTextbox aetherTextbox1;
        private AetherTheme.AetherGroupBox aetherGroupBox2;
        private AetherTheme.AetherButton aetherButton1;
        private AetherTheme.AetherTextbox aetherTextbox2;
    }
}