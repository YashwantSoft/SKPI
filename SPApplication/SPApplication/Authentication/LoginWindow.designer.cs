namespace SPApplication
{
    partial class LoginWindow
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
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lbHelp = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pbClientLogo = new System.Windows.Forms.PictureBox();
            this.lblContactDetails = new System.Windows.Forms.Label();
            this.pbProductLogo = new System.Windows.Forms.PictureBox();
            this.lblCopyRights = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbClientLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProductLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.Blue;
            this.lblUserName.Location = new System.Drawing.Point(125, 130);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(76, 18);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "User Name";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.Blue;
            this.lblPassword.Location = new System.Drawing.Point(125, 196);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(67, 18);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Password";
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(123, 150);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(360, 26);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(123, 216);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(360, 26);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(261, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogin.CausesValidation = false;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogin.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(182, 250);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 30);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "&Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Navy;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(340, 250);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lbHelp
            // 
            this.lbHelp.AutoSize = true;
            this.lbHelp.Location = new System.Drawing.Point(502, 353);
            this.lbHelp.Name = "lbHelp";
            this.lbHelp.Size = new System.Drawing.Size(32, 15);
            this.lbHelp.TabIndex = 6;
            this.lbHelp.TabStop = true;
            this.lbHelp.Text = "Help";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.ForeColor = System.Drawing.Color.Navy;
            this.lblVersion.Location = new System.Drawing.Point(479, 374);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(85, 15);
            this.lblVersion.TabIndex = 11305;
            this.lblVersion.Text = "Version 8.0.12";
            // 
            // pbClientLogo
            // 
            this.pbClientLogo.Location = new System.Drawing.Point(205, 3);
            this.pbClientLogo.Margin = new System.Windows.Forms.Padding(4);
            this.pbClientLogo.Name = "pbClientLogo";
            this.pbClientLogo.Size = new System.Drawing.Size(189, 105);
            this.pbClientLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbClientLogo.TabIndex = 11311;
            this.pbClientLogo.TabStop = false;
            // 
            // lblContactDetails
            // 
            this.lblContactDetails.AutoSize = true;
            this.lblContactDetails.ForeColor = System.Drawing.Color.Navy;
            this.lblContactDetails.Location = new System.Drawing.Point(15, 353);
            this.lblContactDetails.Name = "lblContactDetails";
            this.lblContactDetails.Size = new System.Drawing.Size(77, 15);
            this.lblContactDetails.TabIndex = 11315;
            this.lblContactDetails.Text = "9822837797";
            // 
            // pbProductLogo
            // 
            this.pbProductLogo.Location = new System.Drawing.Point(15, 304);
            this.pbProductLogo.Margin = new System.Windows.Forms.Padding(4);
            this.pbProductLogo.Name = "pbProductLogo";
            this.pbProductLogo.Size = new System.Drawing.Size(88, 45);
            this.pbProductLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbProductLogo.TabIndex = 11314;
            this.pbProductLogo.TabStop = false;
            // 
            // lblCopyRights
            // 
            this.lblCopyRights.AutoSize = true;
            this.lblCopyRights.ForeColor = System.Drawing.Color.Navy;
            this.lblCopyRights.Location = new System.Drawing.Point(12, 374);
            this.lblCopyRights.Name = "lblCopyRights";
            this.lblCopyRights.Size = new System.Drawing.Size(195, 15);
            this.lblCopyRights.TabIndex = 11313;
            this.lblCopyRights.Text = "Copyright @ 2020. 2dk Apps, Pune";
            // 
            // LoginWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(598, 398);
            this.ControlBox = false;
            this.Controls.Add(this.lblContactDetails);
            this.Controls.Add(this.pbProductLogo);
            this.Controls.Add(this.lblCopyRights);
            this.Controls.Add(this.pbClientLogo);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lbHelp);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.LoginWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbClientLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProductLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.LinkLabel lbHelp;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox pbClientLogo;
        private System.Windows.Forms.Label lblContactDetails;
        private System.Windows.Forms.PictureBox pbProductLogo;
        private System.Windows.Forms.Label lblCopyRights;
    }
}

