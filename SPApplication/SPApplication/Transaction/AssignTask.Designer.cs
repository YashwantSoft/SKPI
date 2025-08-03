namespace SPApplication.Transaction
{
    partial class AssignTask
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbFollowUp = new System.Windows.Forms.RichTextBox();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.rtbTask = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblJarInformation = new System.Windows.Forms.Label();
            this.lblBottleInformation = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtSearchTask = new System.Windows.Forms.TextBox();
            this.btnAll = new System.Windows.Forms.Button();
            this.cmbDepartmentSearch = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbStatusSearch = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cbDepartmentAll = new System.Windows.Forms.CheckBox();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSearchID = new System.Windows.Forms.TextBox();
            this.cbStatusAll = new System.Windows.Forms.CheckBox();
            this.clmCheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEntryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDepartment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFollowUp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCompleteDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCompleteDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Blue;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1242, 30);
            this.lblHeader.TabIndex = 11486;
            this.lblHeader.Text = "Assign Task ";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddTask
            // 
            this.btnAddTask.BackColor = System.Drawing.Color.DarkViolet;
            this.btnAddTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTask.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTask.ForeColor = System.Drawing.Color.White;
            this.btnAddTask.Location = new System.Drawing.Point(582, 86);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(20, 20);
            this.btnAddTask.TabIndex = 11572;
            this.btnAddTask.Text = "+";
            this.btnAddTask.UseVisualStyleBackColor = false;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(498, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 15);
            this.label3.TabIndex = 11571;
            this.label3.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(519, 36);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(57, 23);
            this.txtID.TabIndex = 11570;
            this.txtID.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(618, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 11569;
            this.label2.Text = "Follow Up";
            // 
            // rtbFollowUp
            // 
            this.rtbFollowUp.Location = new System.Drawing.Point(681, 36);
            this.rtbFollowUp.Name = "rtbFollowUp";
            this.rtbFollowUp.Size = new System.Drawing.Size(547, 212);
            this.rtbFollowUp.TabIndex = 3;
            this.rtbFollowUp.Text = "";
            // 
            // dtpTime
            // 
            this.dtpTime.Enabled = false;
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(283, 36);
            this.dtpTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(125, 23);
            this.dtpTime.TabIndex = 11566;
            this.dtpTime.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 11568;
            this.label6.Text = "Time";
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(99, 36);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(125, 23);
            this.dtpDate.TabIndex = 11565;
            this.dtpDate.TabStop = false;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(16, 40);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(32, 15);
            this.lblDate.TabIndex = 11567;
            this.lblDate.Text = "Date";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(99, 60);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(477, 23);
            this.cmbDepartment.TabIndex = 0;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(16, 64);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(71, 15);
            this.label21.TabIndex = 11564;
            this.label21.Text = "Department";
            // 
            // rtbTask
            // 
            this.rtbTask.Location = new System.Drawing.Point(99, 84);
            this.rtbTask.Name = "rtbTask";
            this.rtbTask.Size = new System.Drawing.Size(477, 164);
            this.rtbTask.TabIndex = 1;
            this.rtbTask.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 15);
            this.label8.TabIndex = 11563;
            this.label8.Text = "Task ";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(584, 254);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 4;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Location = new System.Drawing.Point(584, 659);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 30);
            this.btnReport.TabIndex = 11576;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Visible = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(504, 254);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(664, 254);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 5;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCheckBox,
            this.clmSrNo,
            this.clmId,
            this.clmEntryDate,
            this.clmDepartment,
            this.clmTask,
            this.clmFollowUp,
            this.clmStatus,
            this.clmCompleteDate,
            this.clmCompleteDays});
            this.dataGridView1.Location = new System.Drawing.Point(2, 289);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1238, 366);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(23, 268);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(76, 19);
            this.cbSelectAll.TabIndex = 10;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cmbSelectAll_CheckedChanged);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Red;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Location = new System.Drawing.Point(177, 659);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 20);
            this.label12.TabIndex = 11583;
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(118, 662);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 15);
            this.label16.TabIndex = 11582;
            this.label16.Text = "Pending";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(31, 663);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 15);
            this.label15.TabIndex = 11581;
            this.label15.Text = "Complete";
            // 
            // lblJarInformation
            // 
            this.lblJarInformation.BackColor = System.Drawing.Color.Yellow;
            this.lblJarInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblJarInformation.Location = new System.Drawing.Point(96, 659);
            this.lblJarInformation.Name = "lblJarInformation";
            this.lblJarInformation.Size = new System.Drawing.Size(20, 20);
            this.lblJarInformation.TabIndex = 11580;
            this.lblJarInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBottleInformation
            // 
            this.lblBottleInformation.BackColor = System.Drawing.Color.LawnGreen;
            this.lblBottleInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBottleInformation.Location = new System.Drawing.Point(9, 660);
            this.lblBottleInformation.Name = "lblBottleInformation";
            this.lblBottleInformation.Size = new System.Drawing.Size(20, 20);
            this.lblBottleInformation.TabIndex = 11579;
            this.lblBottleInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(199, 662);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 15);
            this.label11.TabIndex = 11584;
            this.label11.Text = "Cancel";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Pending",
            "Complete",
            "Cancel"});
            this.cmbStatus.Location = new System.Drawing.Point(378, 257);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(109, 23);
            this.cmbStatus.TabIndex = 11585;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(335, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 11586;
            this.label1.Text = "Status";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(942, 663);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(71, 15);
            this.label23.TabIndex = 11589;
            this.label23.Text = "Search Task";
            // 
            // txtSearchTask
            // 
            this.txtSearchTask.BackColor = System.Drawing.Color.White;
            this.txtSearchTask.Location = new System.Drawing.Point(1015, 660);
            this.txtSearchTask.Name = "txtSearchTask";
            this.txtSearchTask.Size = new System.Drawing.Size(166, 23);
            this.txtSearchTask.TabIndex = 11588;
            this.txtSearchTask.TabStop = false;
            this.txtSearchTask.TextChanged += new System.EventHandler(this.txtSearchTask_TextChanged);
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.Transparent;
            this.btnAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAll.Location = new System.Drawing.Point(1187, 660);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(45, 23);
            this.btnAll.TabIndex = 11587;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = false;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // cmbDepartmentSearch
            // 
            this.cmbDepartmentSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartmentSearch.FormattingEnabled = true;
            this.cmbDepartmentSearch.Location = new System.Drawing.Point(1077, 257);
            this.cmbDepartmentSearch.Name = "cmbDepartmentSearch";
            this.cmbDepartmentSearch.Size = new System.Drawing.Size(110, 23);
            this.cmbDepartmentSearch.TabIndex = 8;
            this.cmbDepartmentSearch.SelectionChangeCommitted += new System.EventHandler(this.cmbDepartmentSearch_SelectionChangeCommitted);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1004, 261);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 15);
            this.label10.TabIndex = 11591;
            this.label10.Text = "Department";
            // 
            // cmbStatusSearch
            // 
            this.cmbStatusSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusSearch.FormattingEnabled = true;
            this.cmbStatusSearch.Items.AddRange(new object[] {
            "Pending",
            "Complete",
            "Cancel"});
            this.cmbStatusSearch.Location = new System.Drawing.Point(840, 257);
            this.cmbStatusSearch.Name = "cmbStatusSearch";
            this.cmbStatusSearch.Size = new System.Drawing.Size(110, 23);
            this.cmbStatusSearch.TabIndex = 6;
            this.cmbStatusSearch.SelectionChangeCommitted += new System.EventHandler(this.cmbStatusSearch_SelectionChangeCommitted);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(757, 261);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(81, 15);
            this.label18.TabIndex = 11594;
            this.label18.Text = "Search Status";
            // 
            // cbDepartmentAll
            // 
            this.cbDepartmentAll.AutoSize = true;
            this.cbDepartmentAll.Location = new System.Drawing.Point(1193, 259);
            this.cbDepartmentAll.Name = "cbDepartmentAll";
            this.cbDepartmentAll.Size = new System.Drawing.Size(41, 19);
            this.cbDepartmentAll.TabIndex = 11593;
            this.cbDepartmentAll.Text = "All";
            this.cbDepartmentAll.UseVisualStyleBackColor = true;
            this.cbDepartmentAll.CheckedChanged += new System.EventHandler(this.cbDepartmentAll_CheckedChanged);
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(105, 268);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(73, 15);
            this.lblTotalCount.TabIndex = 11595;
            this.lblTotalCount.Text = "Total Count-";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(858, 664);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 15);
            this.label13.TabIndex = 11597;
            this.label13.Text = "ID";
            // 
            // txtSearchID
            // 
            this.txtSearchID.Location = new System.Drawing.Point(879, 660);
            this.txtSearchID.Name = "txtSearchID";
            this.txtSearchID.Size = new System.Drawing.Size(57, 23);
            this.txtSearchID.TabIndex = 11596;
            this.txtSearchID.TextChanged += new System.EventHandler(this.txtSearchID_TextChanged);
            // 
            // cbStatusAll
            // 
            this.cbStatusAll.AutoSize = true;
            this.cbStatusAll.Location = new System.Drawing.Point(953, 260);
            this.cbStatusAll.Name = "cbStatusAll";
            this.cbStatusAll.Size = new System.Drawing.Size(41, 19);
            this.cbStatusAll.TabIndex = 11598;
            this.cbStatusAll.TabStop = false;
            this.cbStatusAll.Text = "All";
            this.cbStatusAll.UseVisualStyleBackColor = true;
            this.cbStatusAll.CheckedChanged += new System.EventHandler(this.cbStatusAll_CheckedChanged);
            // 
            // clmCheckBox
            // 
            this.clmCheckBox.HeaderText = "Select";
            this.clmCheckBox.Name = "clmCheckBox";
            this.clmCheckBox.Width = 50;
            // 
            // clmSrNo
            // 
            this.clmSrNo.HeaderText = "Sr.No";
            this.clmSrNo.Name = "clmSrNo";
            this.clmSrNo.Width = 40;
            // 
            // clmId
            // 
            this.clmId.HeaderText = "Id";
            this.clmId.Name = "clmId";
            this.clmId.Visible = false;
            // 
            // clmEntryDate
            // 
            this.clmEntryDate.HeaderText = "Date";
            this.clmEntryDate.Name = "clmEntryDate";
            this.clmEntryDate.ReadOnly = true;
            // 
            // clmDepartment
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clmDepartment.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmDepartment.HeaderText = "Department";
            this.clmDepartment.Name = "clmDepartment";
            this.clmDepartment.Width = 120;
            // 
            // clmTask
            // 
            this.clmTask.HeaderText = "Task";
            this.clmTask.Name = "clmTask";
            this.clmTask.ReadOnly = true;
            this.clmTask.Width = 500;
            // 
            // clmFollowUp
            // 
            this.clmFollowUp.HeaderText = "Follow Up";
            this.clmFollowUp.Name = "clmFollowUp";
            this.clmFollowUp.Width = 300;
            // 
            // clmStatus
            // 
            this.clmStatus.HeaderText = "Status";
            this.clmStatus.Name = "clmStatus";
            this.clmStatus.ReadOnly = true;
            this.clmStatus.Width = 80;
            // 
            // clmCompleteDate
            // 
            this.clmCompleteDate.HeaderText = "Completed Date";
            this.clmCompleteDate.Name = "clmCompleteDate";
            this.clmCompleteDate.ReadOnly = true;
            this.clmCompleteDate.Visible = false;
            this.clmCompleteDate.Width = 120;
            // 
            // clmCompleteDays
            // 
            this.clmCompleteDays.HeaderText = "Days";
            this.clmCompleteDays.Name = "clmCompleteDays";
            this.clmCompleteDays.ReadOnly = true;
            this.clmCompleteDays.Visible = false;
            this.clmCompleteDays.Width = 50;
            // 
            // AssignTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1243, 693);
            this.Controls.Add(this.cbStatusAll);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtSearchID);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.cmbStatusSearch);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.cbDepartmentAll);
            this.Controls.Add(this.cmbDepartmentSearch);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txtSearchTask);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblJarInformation);
            this.Controls.Add(this.lblBottleInformation);
            this.Controls.Add(this.cbSelectAll);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtbFollowUp);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.cmbDepartment);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.rtbTask);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AssignTask";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MinimumSizeChanged += new System.EventHandler(this.AssignTask_MinimumSizeChanged);
            this.Load += new System.EventHandler(this.AT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtbFollowUp;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.RichTextBox rtbTask;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblJarInformation;
        private System.Windows.Forms.Label lblBottleInformation;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtSearchTask;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.ComboBox cmbDepartmentSearch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbStatusSearch;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox cbDepartmentAll;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSearchID;
        private System.Windows.Forms.CheckBox cbStatusAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEntryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFollowUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCompleteDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCompleteDays;
    }
}