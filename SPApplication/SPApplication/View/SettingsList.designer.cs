namespace SPApplication
{
    partial class SettingsList
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.lbReportList = new System.Windows.Forms.ListBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Blue;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(2, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(501, 30);
            this.lblHeader.TabIndex = 11185;
            this.lblHeader.Text = "Settings List";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbReportList
            // 
            this.lbReportList.BackColor = System.Drawing.Color.White;
            this.lbReportList.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReportList.ForeColor = System.Drawing.Color.Blue;
            this.lbReportList.FormattingEnabled = true;
            this.lbReportList.ItemHeight = 19;
            this.lbReportList.Items.AddRange(new object[] {
            "Users",
            "Change Password",
            "Email Credentials",
            "Backup",
            "Import Tally Data",
            "Schedule Server Backup",
            "QR Code Creation",
            "Logout"});
            this.lbReportList.Location = new System.Drawing.Point(12, 35);
            this.lbReportList.Name = "lbReportList";
            this.lbReportList.Size = new System.Drawing.Size(478, 384);
            this.lbReportList.TabIndex = 0;
            this.lbReportList.Click += new System.EventHandler(this.lbReportList_Click);
            this.lbReportList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbReportList_DrawItem);
            this.lbReportList.SelectedIndexChanged += new System.EventHandler(this.lbReportList_SelectedIndexChanged);
            this.lbReportList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbReportList_KeyDown);
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(214, 424);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 1;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // SettingsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(502, 458);
            this.ControlBox = false;
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lbReportList);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MenuSettings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ListBox lbReportList;
    }
}