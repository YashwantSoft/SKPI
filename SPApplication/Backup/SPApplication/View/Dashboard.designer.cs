namespace SPApplication
{
    partial class Dashboard
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
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblDateTimeRunning = new System.Windows.Forms.Label();
            this.pbProductLogo = new System.Windows.Forms.PictureBox();
            this.pbClientLogo = new System.Windows.Forms.PictureBox();
            this.btnTransaction = new System.Windows.Forms.Button();
            this.btnMaster = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbHelp = new System.Windows.Forms.LinkLabel();
            this.lblCopyRights = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbProductLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClientLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.ForeColor = System.Drawing.Color.Navy;
            this.lblVersion.Location = new System.Drawing.Point(1094, 650);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(85, 15);
            this.lblVersion.TabIndex = 11304;
            this.lblVersion.Text = "Version 8.0.12";
            // 
            // lblUser
            // 
            this.lblUser.BackColor = System.Drawing.Color.White;
            this.lblUser.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.Black;
            this.lblUser.Location = new System.Drawing.Point(940, 7);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(239, 24);
            this.lblUser.TabIndex = 11309;
            this.lblUser.Text = "User:";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDateTimeRunning
            // 
            this.lblDateTimeRunning.BackColor = System.Drawing.Color.White;
            this.lblDateTimeRunning.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTimeRunning.ForeColor = System.Drawing.Color.Black;
            this.lblDateTimeRunning.Location = new System.Drawing.Point(940, 38);
            this.lblDateTimeRunning.Name = "lblDateTimeRunning";
            this.lblDateTimeRunning.Size = new System.Drawing.Size(239, 24);
            this.lblDateTimeRunning.TabIndex = 11308;
            this.lblDateTimeRunning.Text = "Date Time";
            this.lblDateTimeRunning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbProductLogo
            // 
            this.pbProductLogo.Location = new System.Drawing.Point(13, 7);
            this.pbProductLogo.Margin = new System.Windows.Forms.Padding(4);
            this.pbProductLogo.Name = "pbProductLogo";
            this.pbProductLogo.Size = new System.Drawing.Size(145, 52);
            this.pbProductLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbProductLogo.TabIndex = 11312;
            this.pbProductLogo.TabStop = false;
            // 
            // pbClientLogo
            // 
            this.pbClientLogo.Location = new System.Drawing.Point(414, 38);
            this.pbClientLogo.Name = "pbClientLogo";
            this.pbClientLogo.Size = new System.Drawing.Size(376, 375);
            this.pbClientLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbClientLogo.TabIndex = 11266;
            this.pbClientLogo.TabStop = false;
            // 
            // btnTransaction
            // 
            this.btnTransaction.BackColor = System.Drawing.Color.Transparent;
            this.btnTransaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTransaction.Location = new System.Drawing.Point(346, 450);
            this.btnTransaction.Name = "btnTransaction";
            this.btnTransaction.Size = new System.Drawing.Size(100, 100);
            this.btnTransaction.TabIndex = 11264;
            this.btnTransaction.UseVisualStyleBackColor = false;
            this.btnTransaction.Click += new System.EventHandler(this.btnTransaction_Click);
            // 
            // btnMaster
            // 
            this.btnMaster.BackColor = System.Drawing.Color.Transparent;
            this.btnMaster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMaster.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMaster.Location = new System.Drawing.Point(140, 450);
            this.btnMaster.Name = "btnMaster";
            this.btnMaster.Size = new System.Drawing.Size(100, 100);
            this.btnMaster.TabIndex = 2;
            this.btnMaster.UseVisualStyleBackColor = false;
            this.btnMaster.Click += new System.EventHandler(this.btnMaster_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSettings.Location = new System.Drawing.Point(758, 450);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(100, 100);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnReports
            // 
            this.btnReports.BackColor = System.Drawing.Color.Transparent;
            this.btnReports.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReports.Location = new System.Drawing.Point(552, 450);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(100, 100);
            this.btnReports.TabIndex = 3;
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(964, 450);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 100);
            this.btnExit.TabIndex = 5;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(49, 634);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 15);
            this.label1.TabIndex = 11321;
            this.label1.Text = "Mobile: 9822837797";
            // 
            // lbHelp
            // 
            this.lbHelp.AutoSize = true;
            this.lbHelp.Location = new System.Drawing.Point(1119, 634);
            this.lbHelp.Name = "lbHelp";
            this.lbHelp.Size = new System.Drawing.Size(32, 15);
            this.lbHelp.TabIndex = 11320;
            this.lbHelp.TabStop = true;
            this.lbHelp.Text = "Help";
            this.lbHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbHelp_LinkClicked);
            // 
            // lblCopyRights
            // 
            this.lblCopyRights.AutoSize = true;
            this.lblCopyRights.ForeColor = System.Drawing.Color.Navy;
            this.lblCopyRights.Location = new System.Drawing.Point(12, 653);
            this.lblCopyRights.Name = "lblCopyRights";
            this.lblCopyRights.Size = new System.Drawing.Size(195, 15);
            this.lblCopyRights.TabIndex = 11303;
            this.lblCopyRights.Text = "Copyright @ 2020. 2dk Apps, Pune";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1204, 671);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbHelp);
            this.Controls.Add(this.pbProductLogo);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblDateTimeRunning);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblCopyRights);
            this.Controls.Add(this.pbClientLogo);
            this.Controls.Add(this.btnTransaction);
            this.Controls.Add(this.btnMaster);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnExit);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Dashboard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Dashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbProductLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClientLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnMaster;
        private System.Windows.Forms.Button btnTransaction;
        private System.Windows.Forms.PictureBox pbClientLogo;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblDateTimeRunning;
        private System.Windows.Forms.PictureBox pbProductLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lbHelp;
        private System.Windows.Forms.Label lblCopyRights;
    }
}