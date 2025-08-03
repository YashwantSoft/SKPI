namespace SPApplication.Report
{
    partial class GradeNoticeBordReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPacker = new System.Windows.Forms.DataGridView();
            this.clmEmployeeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSidel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSidel25Packer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmP5Packer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmP3Packer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmP1Packer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmP2Packer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPHEPacker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm5Packer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm3Packer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAAAPacker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmA2Packer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmA1Packer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAPacker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBPacker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmOtherWork = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbToday = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblHeader = new System.Windows.Forms.Label();
            this.cmbDesignationEmployee = new System.Windows.Forms.ComboBox();
            this.label61 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPacker
            // 
            this.dgvPacker.AllowUserToAddRows = false;
            this.dgvPacker.AllowUserToDeleteRows = false;
            this.dgvPacker.AllowUserToResizeColumns = false;
            this.dgvPacker.AllowUserToResizeRows = false;
            this.dgvPacker.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPacker.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPacker.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPacker.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmEmployeeId,
            this.clmEmployeeName,
            this.clmSidel,
            this.clmSidel25Packer,
            this.clmP5Packer,
            this.clmP3Packer,
            this.clmP1Packer,
            this.clmP2Packer,
            this.clmPHEPacker,
            this.clm5Packer,
            this.clm3Packer,
            this.clmAAAPacker,
            this.clmA2Packer,
            this.clmA1Packer,
            this.clmAPacker,
            this.clmBPacker,
            this.clmOtherWork});
            this.dgvPacker.GridColor = System.Drawing.Color.White;
            this.dgvPacker.Location = new System.Drawing.Point(7, 272);
            this.dgvPacker.Name = "dgvPacker";
            this.dgvPacker.ReadOnly = true;
            this.dgvPacker.RowHeadersVisible = false;
            this.dgvPacker.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPacker.Size = new System.Drawing.Size(1285, 378);
            this.dgvPacker.TabIndex = 11581;
            this.dgvPacker.TabStop = false;
            this.dgvPacker.Visible = false;
            // 
            // clmEmployeeId
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmEmployeeId.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmEmployeeId.HeaderText = "EmployeeId";
            this.clmEmployeeId.Name = "clmEmployeeId";
            this.clmEmployeeId.ReadOnly = true;
            this.clmEmployeeId.Visible = false;
            this.clmEmployeeId.Width = 22;
            // 
            // clmEmployeeName
            // 
            this.clmEmployeeName.HeaderText = "Employee Name";
            this.clmEmployeeName.Name = "clmEmployeeName";
            this.clmEmployeeName.ReadOnly = true;
            this.clmEmployeeName.Width = 200;
            // 
            // clmSidel
            // 
            this.clmSidel.HeaderText = "Sidel";
            this.clmSidel.Name = "clmSidel";
            this.clmSidel.ReadOnly = true;
            this.clmSidel.Width = 70;
            // 
            // clmSidel25Packer
            // 
            this.clmSidel25Packer.HeaderText = "Sidel-2.5";
            this.clmSidel25Packer.Name = "clmSidel25Packer";
            this.clmSidel25Packer.ReadOnly = true;
            this.clmSidel25Packer.Width = 70;
            // 
            // clmP5Packer
            // 
            this.clmP5Packer.HeaderText = "P5*";
            this.clmP5Packer.Name = "clmP5Packer";
            this.clmP5Packer.ReadOnly = true;
            this.clmP5Packer.Width = 70;
            // 
            // clmP3Packer
            // 
            this.clmP3Packer.HeaderText = "P3*";
            this.clmP3Packer.Name = "clmP3Packer";
            this.clmP3Packer.ReadOnly = true;
            this.clmP3Packer.Width = 70;
            // 
            // clmP1Packer
            // 
            this.clmP1Packer.HeaderText = "P1*";
            this.clmP1Packer.Name = "clmP1Packer";
            this.clmP1Packer.ReadOnly = true;
            this.clmP1Packer.Width = 70;
            // 
            // clmP2Packer
            // 
            this.clmP2Packer.HeaderText = "P-2";
            this.clmP2Packer.Name = "clmP2Packer";
            this.clmP2Packer.ReadOnly = true;
            this.clmP2Packer.Width = 70;
            // 
            // clmPHEPacker
            // 
            this.clmPHEPacker.HeaderText = "P-HE";
            this.clmPHEPacker.Name = "clmPHEPacker";
            this.clmPHEPacker.ReadOnly = true;
            this.clmPHEPacker.Width = 70;
            // 
            // clm5Packer
            // 
            this.clm5Packer.HeaderText = "5*";
            this.clm5Packer.Name = "clm5Packer";
            this.clm5Packer.ReadOnly = true;
            this.clm5Packer.Width = 70;
            // 
            // clm3Packer
            // 
            this.clm3Packer.HeaderText = "3*";
            this.clm3Packer.Name = "clm3Packer";
            this.clm3Packer.ReadOnly = true;
            this.clm3Packer.Width = 70;
            // 
            // clmAAAPacker
            // 
            this.clmAAAPacker.HeaderText = "AAA";
            this.clmAAAPacker.Name = "clmAAAPacker";
            this.clmAAAPacker.ReadOnly = true;
            this.clmAAAPacker.Width = 70;
            // 
            // clmA2Packer
            // 
            this.clmA2Packer.HeaderText = "A++";
            this.clmA2Packer.Name = "clmA2Packer";
            this.clmA2Packer.ReadOnly = true;
            this.clmA2Packer.Width = 70;
            // 
            // clmA1Packer
            // 
            this.clmA1Packer.HeaderText = "A+";
            this.clmA1Packer.Name = "clmA1Packer";
            this.clmA1Packer.ReadOnly = true;
            this.clmA1Packer.Width = 70;
            // 
            // clmAPacker
            // 
            this.clmAPacker.HeaderText = "A";
            this.clmAPacker.Name = "clmAPacker";
            this.clmAPacker.ReadOnly = true;
            this.clmAPacker.Width = 70;
            // 
            // clmBPacker
            // 
            this.clmBPacker.HeaderText = "B";
            this.clmBPacker.Name = "clmBPacker";
            this.clmBPacker.ReadOnly = true;
            this.clmBPacker.Width = 70;
            // 
            // clmOtherWork
            // 
            this.clmOtherWork.HeaderText = "Other Work";
            this.clmOtherWork.Name = "clmOtherWork";
            this.clmOtherWork.ReadOnly = true;
            this.clmOtherWork.Width = 70;
            // 
            // cbToday
            // 
            this.cbToday.AutoSize = true;
            this.cbToday.Location = new System.Drawing.Point(825, 37);
            this.cbToday.Name = "cbToday";
            this.cbToday.Size = new System.Drawing.Size(58, 19);
            this.cbToday.TabIndex = 11584;
            this.cbToday.Text = "Today";
            this.cbToday.UseVisualStyleBackColor = true;
            this.cbToday.CheckedChanged += new System.EventHandler(this.cbToday_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(638, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 11586;
            this.label2.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(687, 34);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(120, 23);
            this.dtpToDate.TabIndex = 11583;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(438, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 11585;
            this.label1.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(503, 34);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(120, 23);
            this.dtpFromDate.TabIndex = 11582;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 1);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1299, 28);
            this.lblHeader.TabIndex = 11587;
            this.lblHeader.Text = "Grade Notice Bord Report";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbDesignationEmployee
            // 
            this.cmbDesignationEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDesignationEmployee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDesignationEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDesignationEmployee.FormattingEnabled = true;
            this.cmbDesignationEmployee.Items.AddRange(new object[] {
            "Operator",
            "Packer"});
            this.cmbDesignationEmployee.Location = new System.Drawing.Point(503, 59);
            this.cmbDesignationEmployee.Name = "cmbDesignationEmployee";
            this.cmbDesignationEmployee.Size = new System.Drawing.Size(304, 23);
            this.cmbDesignationEmployee.TabIndex = 11589;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(429, 62);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(72, 15);
            this.label61.TabIndex = 11588;
            this.label61.Text = "Designation";
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(692, 88);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 11592;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnView
            // 
            this.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnView.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(532, 88);
            this.btnView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 30);
            this.btnView.TabIndex = 11590;
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(612, 88);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 11591;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Location = new System.Drawing.Point(610, 656);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 30);
            this.btnReport.TabIndex = 11593;
            this.btnReport.UseVisualStyleBackColor = true;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(12, 107);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(69, 15);
            this.lblTotalCount.TabIndex = 11594;
            this.lblTotalCount.Text = "Total Count";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(12, 125);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1280, 525);
            this.dataGridView1.TabIndex = 11595;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn1.HeaderText = "EmployeeId";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 22;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Employee Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // GradeNoticeBordReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1298, 698);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.cmbDesignationEmployee);
            this.Controls.Add(this.label61);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.cbToday);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.dgvPacker);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GradeNoticeBordReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ProgressCardNoticeBoardReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPacker;
        private System.Windows.Forms.CheckBox cbToday;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ComboBox cmbDesignationEmployee;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEmployeeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSidel;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSidel25Packer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmP5Packer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmP3Packer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmP1Packer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmP2Packer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPHEPacker;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm5Packer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm3Packer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAAAPacker;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmA2Packer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmA1Packer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAPacker;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBPacker;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOtherWork;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}