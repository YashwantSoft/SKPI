namespace SPApplication.Report
{
    partial class MonthlyProductionReport
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
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.cmbMachineNo = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.btnView = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.clmMachineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPerformance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAvailibility = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmQuality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmOEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRatings = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReport = new System.Windows.Forms.Button();
            this.cbToday = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(786, 58);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(76, 19);
            this.cbSelectAll.TabIndex = 11587;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // cmbMachineNo
            // 
            this.cmbMachineNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineNo.FormattingEnabled = true;
            this.cmbMachineNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14"});
            this.cmbMachineNo.Location = new System.Drawing.Point(464, 56);
            this.cmbMachineNo.Name = "cmbMachineNo";
            this.cmbMachineNo.Size = new System.Drawing.Size(304, 23);
            this.cmbMachineNo.TabIndex = 11585;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(389, 59);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(73, 15);
            this.label21.TabIndex = 11586;
            this.label21.Text = "Machine No";
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(-1, 1);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1253, 28);
            this.lblHeader.TabIndex = 11584;
            this.lblHeader.Text = "Progress Card Report";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 11593;
            this.label2.Text = "Month";
            this.label2.Visible = false;
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.cmbMonth.Location = new System.Drawing.Point(86, 51);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(120, 23);
            this.cmbMonth.TabIndex = 11592;
            this.cmbMonth.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 15);
            this.label3.TabIndex = 11595;
            this.label3.Text = "Year";
            this.label3.Visible = false;
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Items.AddRange(new object[] {
            "2021",
            "2022",
            "2023",
            "2024"});
            this.cmbYear.Location = new System.Drawing.Point(242, 51);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(120, 23);
            this.cmbYear.TabIndex = 11594;
            this.cmbYear.Visible = false;
            // 
            // btnView
            // 
            this.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnView.Location = new System.Drawing.Point(508, 85);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 30);
            this.btnView.TabIndex = 11598;
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(588, 85);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 11597;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(668, 85);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 11596;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmMachineNo,
            this.clmPerformance,
            this.clmAvailibility,
            this.clmQuality,
            this.clmOEE,
            this.clmRatings});
            this.dgvReport.Location = new System.Drawing.Point(12, 122);
            this.dgvReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(1227, 546);
            this.dgvReport.TabIndex = 11599;
            // 
            // clmMachineNo
            // 
            this.clmMachineNo.HeaderText = "Machine No";
            this.clmMachineNo.Name = "clmMachineNo";
            this.clmMachineNo.ReadOnly = true;
            this.clmMachineNo.Width = 200;
            // 
            // clmPerformance
            // 
            this.clmPerformance.HeaderText = "Performance %";
            this.clmPerformance.Name = "clmPerformance";
            this.clmPerformance.ReadOnly = true;
            this.clmPerformance.Width = 200;
            // 
            // clmAvailibility
            // 
            this.clmAvailibility.HeaderText = "Availibility %";
            this.clmAvailibility.Name = "clmAvailibility";
            this.clmAvailibility.ReadOnly = true;
            this.clmAvailibility.Width = 200;
            // 
            // clmQuality
            // 
            this.clmQuality.HeaderText = "Quality %";
            this.clmQuality.Name = "clmQuality";
            this.clmQuality.ReadOnly = true;
            this.clmQuality.Width = 200;
            // 
            // clmOEE
            // 
            this.clmOEE.HeaderText = "OEE %";
            this.clmOEE.Name = "clmOEE";
            this.clmOEE.ReadOnly = true;
            this.clmOEE.Width = 200;
            // 
            // clmRatings
            // 
            this.clmRatings.HeaderText = "Ratings";
            this.clmRatings.Name = "clmRatings";
            this.clmRatings.ReadOnly = true;
            this.clmRatings.Width = 200;
            // 
            // btnReport
            // 
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Location = new System.Drawing.Point(588, 675);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(80, 30);
            this.btnReport.TabIndex = 11601;
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Visible = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // cbToday
            // 
            this.cbToday.AutoSize = true;
            this.cbToday.Location = new System.Drawing.Point(786, 35);
            this.cbToday.Name = "cbToday";
            this.cbToday.Size = new System.Drawing.Size(58, 19);
            this.cbToday.TabIndex = 11604;
            this.cbToday.Text = "Today";
            this.cbToday.UseVisualStyleBackColor = true;
            this.cbToday.CheckedChanged += new System.EventHandler(this.cbToday_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(599, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 11606;
            this.label1.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(648, 32);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(120, 23);
            this.dtpToDate.TabIndex = 11603;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(389, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 15);
            this.label4.TabIndex = 11605;
            this.label4.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(464, 32);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(120, 23);
            this.dtpFromDate.TabIndex = 11602;
            // 
            // MonthlyProductionReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1251, 708);
            this.ControlBox = false;
            this.Controls.Add(this.cbToday);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbMonth);
            this.Controls.Add(this.cbSelectAll);
            this.Controls.Add(this.cmbMachineNo);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MonthlyProductionReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MonthlyProductionReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.ComboBox cmbMachineNo;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.CheckBox cbToday;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMachineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPerformance;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAvailibility;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmQuality;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOEE;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRatings;
    }
}