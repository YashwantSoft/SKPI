namespace SPApplication.Transaction
{
    partial class AssignTask_Old
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
            this.rtbTask = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.btnReport = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSearchID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.cbToday = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.cbAllSearch = new System.Windows.Forms.CheckBox();
            this.cmbSearchList = new System.Windows.Forms.ComboBox();
            this.btnAll = new System.Windows.Forms.Button();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblJarInformation = new System.Windows.Forms.Label();
            this.lblBottleInformation = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpCompleteDate = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTaskDays = new System.Windows.Forms.TextBox();
            this.cmbStatusSearch = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtCompleteDays = new System.Windows.Forms.TextBox();
            this.txtSearchTask = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btnAddTask = new System.Windows.Forms.Button();
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
            this.lblHeader.Size = new System.Drawing.Size(1200, 30);
            this.lblHeader.TabIndex = 11486;
            this.lblHeader.Text = "Asign Task";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtbTask
            // 
            this.rtbTask.Location = new System.Drawing.Point(110, 81);
            this.rtbTask.Name = "rtbTask";
            this.rtbTask.Size = new System.Drawing.Size(477, 116);
            this.rtbTask.TabIndex = 1;
            this.rtbTask.Text = "";
            this.rtbTask.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbTask_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 15);
            this.label8.TabIndex = 11500;
            this.label8.Text = "Task ";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(27, 61);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(71, 15);
            this.label21.TabIndex = 11502;
            this.label21.Text = "Department";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(110, 57);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(477, 23);
            this.cmbDepartment.TabIndex = 0;
            this.cmbDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDepartment_KeyDown);
            // 
            // dtpTime
            // 
            this.dtpTime.Enabled = false;
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(294, 33);
            this.dtpTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(125, 23);
            this.dtpTime.TabIndex = 11505;
            this.dtpTime.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(259, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 11507;
            this.label6.Text = "Time";
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(110, 33);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(125, 23);
            this.dtpDate.TabIndex = 11504;
            this.dtpDate.TabStop = false;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(27, 37);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(32, 15);
            this.lblDate.TabIndex = 11506;
            this.lblDate.Text = "Date";
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Location = new System.Drawing.Point(563, 659);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 30);
            this.btnReport.TabIndex = 11516;
            this.btnReport.TabStop = false;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Visible = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(702, 291);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 15);
            this.label13.TabIndex = 11520;
            this.label13.Text = "ID";
            // 
            // txtSearchID
            // 
            this.txtSearchID.Location = new System.Drawing.Point(723, 287);
            this.txtSearchID.Name = "txtSearchID";
            this.txtSearchID.Size = new System.Drawing.Size(57, 23);
            this.txtSearchID.TabIndex = 14;
            this.txtSearchID.TextChanged += new System.EventHandler(this.txtSearchID_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(235, 291);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 15);
            this.label10.TabIndex = 11519;
            this.label10.Text = "Search";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(11, 290);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(73, 15);
            this.lblTotalCount.TabIndex = 11518;
            this.lblTotalCount.Text = "Total Count-";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(523, 250);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 8;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(443, 250);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(603, 250);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 9;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(6, 313);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1188, 342);
            this.dataGridView1.TabIndex = 11517;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Pending",
            "Complete",
            "Cancel"});
            this.cmbStatus.Location = new System.Drawing.Point(110, 222);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(109, 23);
            this.cmbStatus.TabIndex = 3;
            this.cmbStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbStatus_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 11522;
            this.label1.Text = "Status";
            // 
            // rtbNotes
            // 
            this.rtbNotes.Location = new System.Drawing.Point(658, 33);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(516, 212);
            this.rtbNotes.TabIndex = 6;
            this.rtbNotes.Text = "";
            this.rtbNotes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbNotes_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(619, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 11525;
            this.label2.Text = "Notes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(509, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 15);
            this.label3.TabIndex = 11527;
            this.label3.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(530, 33);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(57, 23);
            this.txtID.TabIndex = 11526;
            this.txtID.TabStop = false;
            // 
            // cbToday
            // 
            this.cbToday.AutoSize = true;
            this.cbToday.Location = new System.Drawing.Point(1080, 257);
            this.cbToday.Name = "cbToday";
            this.cbToday.Size = new System.Drawing.Size(58, 19);
            this.cbToday.TabIndex = 12;
            this.cbToday.Text = "Today";
            this.cbToday.UseVisualStyleBackColor = true;
            this.cbToday.Visible = false;
            this.cbToday.CheckedChanged += new System.EventHandler(this.cbToday_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(971, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 15);
            this.label4.TabIndex = 11532;
            this.label4.Text = "To";
            this.label4.Visible = false;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(992, 255);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(82, 23);
            this.dtpToDate.TabIndex = 11;
            this.dtpToDate.Visible = false;
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(850, 259);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 15);
            this.label5.TabIndex = 11531;
            this.label5.Text = "From";
            this.label5.Visible = false;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(887, 255);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(82, 23);
            this.dtpFromDate.TabIndex = 13;
            this.dtpFromDate.Visible = false;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // cbAllSearch
            // 
            this.cbAllSearch.AutoSize = true;
            this.cbAllSearch.Location = new System.Drawing.Point(651, 289);
            this.cbAllSearch.Name = "cbAllSearch";
            this.cbAllSearch.Size = new System.Drawing.Size(41, 19);
            this.cbAllSearch.TabIndex = 13;
            this.cbAllSearch.Text = "All";
            this.cbAllSearch.UseVisualStyleBackColor = true;
            this.cbAllSearch.CheckedChanged += new System.EventHandler(this.cbAllSearch_CheckedChanged);
            // 
            // cmbSearchList
            // 
            this.cmbSearchList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchList.FormattingEnabled = true;
            this.cmbSearchList.Location = new System.Drawing.Point(281, 287);
            this.cmbSearchList.Name = "cmbSearchList";
            this.cmbSearchList.Size = new System.Drawing.Size(201, 23);
            this.cmbSearchList.TabIndex = 11;
            this.cmbSearchList.SelectionChangeCommitted += new System.EventHandler(this.cmbSearchList_SelectionChangeCommitted);
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.Transparent;
            this.btnAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAll.Location = new System.Drawing.Point(1147, 286);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(45, 23);
            this.btnAll.TabIndex = 14;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = false;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // cmbPriority
            // 
            this.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High",
            "Urgent"});
            this.cmbPriority.Location = new System.Drawing.Point(110, 198);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(109, 23);
            this.cmbPriority.TabIndex = 2;
            this.cmbPriority.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPriority_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 15);
            this.label7.TabIndex = 11538;
            this.label7.Text = "Priority";
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDueDate.Location = new System.Drawing.Point(314, 198);
            this.dtpDueDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(112, 23);
            this.dtpDueDate.TabIndex = 4;
            this.dtpDueDate.ValueChanged += new System.EventHandler(this.dtpDueDate_ValueChanged);
            this.dtpDueDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDueDate_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(226, 201);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 15);
            this.label9.TabIndex = 11540;
            this.label9.Text = "Due Date";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(123, 666);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 15);
            this.label16.TabIndex = 11544;
            this.label16.Text = "Pending";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(36, 667);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 15);
            this.label15.TabIndex = 11543;
            this.label15.Text = "Complete";
            // 
            // lblJarInformation
            // 
            this.lblJarInformation.BackColor = System.Drawing.Color.Yellow;
            this.lblJarInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblJarInformation.Location = new System.Drawing.Point(101, 663);
            this.lblJarInformation.Name = "lblJarInformation";
            this.lblJarInformation.Size = new System.Drawing.Size(20, 20);
            this.lblJarInformation.TabIndex = 11542;
            this.lblJarInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBottleInformation
            // 
            this.lblBottleInformation.BackColor = System.Drawing.Color.LawnGreen;
            this.lblBottleInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBottleInformation.Location = new System.Drawing.Point(14, 664);
            this.lblBottleInformation.Name = "lblBottleInformation";
            this.lblBottleInformation.Size = new System.Drawing.Size(20, 20);
            this.lblBottleInformation.TabIndex = 11541;
            this.lblBottleInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(204, 666);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 15);
            this.label11.TabIndex = 11546;
            this.label11.Text = "Cancel";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Red;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Location = new System.Drawing.Point(182, 663);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 20);
            this.label12.TabIndex = 11545;
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpCompleteDate
            // 
            this.dtpCompleteDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCompleteDate.Location = new System.Drawing.Point(314, 222);
            this.dtpCompleteDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpCompleteDate.Name = "dtpCompleteDate";
            this.dtpCompleteDate.Size = new System.Drawing.Size(112, 23);
            this.dtpCompleteDate.TabIndex = 5;
            this.dtpCompleteDate.ValueChanged += new System.EventHandler(this.dtpCompleteDate_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(226, 225);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 15);
            this.label14.TabIndex = 11548;
            this.label14.Text = "Complete Date";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(428, 201);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(104, 15);
            this.label17.TabIndex = 11550;
            this.label17.Text = "Task Days (Aprox)";
            // 
            // txtTaskDays
            // 
            this.txtTaskDays.Location = new System.Drawing.Point(534, 198);
            this.txtTaskDays.Name = "txtTaskDays";
            this.txtTaskDays.ReadOnly = true;
            this.txtTaskDays.Size = new System.Drawing.Size(53, 23);
            this.txtTaskDays.TabIndex = 11549;
            this.txtTaskDays.TabStop = false;
            this.txtTaskDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbStatusSearch
            // 
            this.cmbStatusSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusSearch.FormattingEnabled = true;
            this.cmbStatusSearch.Items.AddRange(new object[] {
            "Pending",
            "Complete",
            "Cancel"});
            this.cmbStatusSearch.Location = new System.Drawing.Point(530, 287);
            this.cmbStatusSearch.Name = "cmbStatusSearch";
            this.cmbStatusSearch.Size = new System.Drawing.Size(115, 23);
            this.cmbStatusSearch.TabIndex = 12;
            this.cmbStatusSearch.SelectionChangeCommitted += new System.EventHandler(this.cmbStatusSearch_SelectionChangeCommitted);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(487, 290);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 15);
            this.label18.TabIndex = 11552;
            this.label18.Text = "Status";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(683, 250);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(589, 202);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 15);
            this.label19.TabIndex = 11553;
            this.label19.Text = "(In Days)";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(589, 226);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 15);
            this.label20.TabIndex = 11556;
            this.label20.Text = "(In Days)";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(429, 225);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 15);
            this.label22.TabIndex = 11555;
            this.label22.Text = "Complete Days";
            // 
            // txtCompleteDays
            // 
            this.txtCompleteDays.Location = new System.Drawing.Point(534, 222);
            this.txtCompleteDays.Name = "txtCompleteDays";
            this.txtCompleteDays.ReadOnly = true;
            this.txtCompleteDays.Size = new System.Drawing.Size(53, 23);
            this.txtCompleteDays.TabIndex = 11554;
            this.txtCompleteDays.TabStop = false;
            this.txtCompleteDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSearchTask
            // 
            this.txtSearchTask.BackColor = System.Drawing.Color.White;
            this.txtSearchTask.Location = new System.Drawing.Point(887, 287);
            this.txtSearchTask.Name = "txtSearchTask";
            this.txtSearchTask.Size = new System.Drawing.Size(251, 23);
            this.txtSearchTask.TabIndex = 11557;
            this.txtSearchTask.TabStop = false;
            this.txtSearchTask.TextChanged += new System.EventHandler(this.txtSearchTask_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(814, 290);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(71, 15);
            this.label23.TabIndex = 11558;
            this.label23.Text = "Search Task";
            // 
            // btnAddTask
            // 
            this.btnAddTask.BackColor = System.Drawing.Color.DarkViolet;
            this.btnAddTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTask.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTask.ForeColor = System.Drawing.Color.White;
            this.btnAddTask.Location = new System.Drawing.Point(593, 83);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(20, 20);
            this.btnAddTask.TabIndex = 11559;
            this.btnAddTask.Text = "+";
            this.btnAddTask.UseVisualStyleBackColor = false;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // AssignTask_Old
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1201, 694);
            this.ControlBox = false;
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txtSearchTask);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtCompleteDays);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.cmbStatusSearch);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtTaskDays);
            this.Controls.Add(this.dtpCompleteDate);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblJarInformation);
            this.Controls.Add(this.lblBottleInformation);
            this.Controls.Add(this.dtpDueDate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbPriority);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.cmbSearchList);
            this.Controls.Add(this.cbAllSearch);
            this.Controls.Add(this.cbToday);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtSearchID);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dataGridView1);
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
            this.Name = "AssignTask_Old";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AssignTask_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.RichTextBox rtbTask;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSearchID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.CheckBox cbToday;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.CheckBox cbAllSearch;
        private System.Windows.Forms.ComboBox cmbSearchList;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.ComboBox cmbPriority;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblJarInformation;
        private System.Windows.Forms.Label lblBottleInformation;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpCompleteDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtTaskDays;
        private System.Windows.Forms.ComboBox cmbStatusSearch;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtCompleteDays;
        private System.Windows.Forms.TextBox txtSearchTask;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnAddTask;
    }
}