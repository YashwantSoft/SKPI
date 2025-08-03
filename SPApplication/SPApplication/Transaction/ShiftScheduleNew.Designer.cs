namespace SPApplication.Transaction
{
    partial class ShiftScheduleNew
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
            this.gb3 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dtpEndTime3 = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpBeginTime3 = new System.Windows.Forms.DateTimePicker();
            this.gb2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpEndTime2 = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpBeginTime2 = new System.Windows.Forms.DateTimePicker();
            this.gb1 = new System.Windows.Forms.GroupBox();
            this.dtpEndTime1 = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpBeginTime1 = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.clbNoOfShift = new System.Windows.Forms.CheckedListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbShiftHours = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpShiftDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cmbShifts = new System.Windows.Forms.ComboBox();
            this.dgvShifts = new System.Windows.Forms.DataGridView();
            this.clmShiftScheduleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEntryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEntryTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFromDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmToDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmShiftDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmShiftHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNoOfShifts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBeginTime1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEndTime1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBeginTime2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEndTime2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBeginTime3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEndTime3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNaration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpSearchDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtShiftID = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtNarration = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gb3.SuspendLayout();
            this.gb2.SuspendLayout();
            this.gb1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(-2, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1199, 29);
            this.lblHeader.TabIndex = 72;
            this.lblHeader.Text = "Shift Entry Schedule";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gb3
            // 
            this.gb3.Controls.Add(this.label14);
            this.gb3.Controls.Add(this.dtpEndTime3);
            this.gb3.Controls.Add(this.label15);
            this.gb3.Controls.Add(this.dtpBeginTime3);
            this.gb3.Enabled = false;
            this.gb3.Location = new System.Drawing.Point(388, 134);
            this.gb3.Name = "gb3";
            this.gb3.Size = new System.Drawing.Size(561, 45);
            this.gb3.TabIndex = 11498;
            this.gb3.TabStop = false;
            this.gb3.Text = "3rd Shift Timings";
            this.gb3.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 18);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 15);
            this.label14.TabIndex = 11500;
            this.label14.Text = "Begin Time";
            // 
            // dtpEndTime3
            // 
            this.dtpEndTime3.CustomFormat = "HH:mm";
            this.dtpEndTime3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime3.Location = new System.Drawing.Point(342, 15);
            this.dtpEndTime3.Name = "dtpEndTime3";
            this.dtpEndTime3.Size = new System.Drawing.Size(200, 23);
            this.dtpEndTime3.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(284, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 15);
            this.label15.TabIndex = 11499;
            this.label15.Text = "End Time";
            // 
            // dtpBeginTime3
            // 
            this.dtpBeginTime3.CustomFormat = "HH:mm";
            this.dtpBeginTime3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBeginTime3.Location = new System.Drawing.Point(71, 15);
            this.dtpBeginTime3.Name = "dtpBeginTime3";
            this.dtpBeginTime3.Size = new System.Drawing.Size(200, 23);
            this.dtpBeginTime3.TabIndex = 7;
            // 
            // gb2
            // 
            this.gb2.Controls.Add(this.label5);
            this.gb2.Controls.Add(this.dtpEndTime2);
            this.gb2.Controls.Add(this.label13);
            this.gb2.Controls.Add(this.dtpBeginTime2);
            this.gb2.Enabled = false;
            this.gb2.Location = new System.Drawing.Point(388, 89);
            this.gb2.Name = "gb2";
            this.gb2.Size = new System.Drawing.Size(561, 45);
            this.gb2.TabIndex = 11497;
            this.gb2.TabStop = false;
            this.gb2.Text = "2nd Shift Timings";
            this.gb2.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 15);
            this.label5.TabIndex = 11500;
            this.label5.Text = "Begin Time";
            // 
            // dtpEndTime2
            // 
            this.dtpEndTime2.CustomFormat = "HH:mm";
            this.dtpEndTime2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime2.Location = new System.Drawing.Point(342, 15);
            this.dtpEndTime2.Name = "dtpEndTime2";
            this.dtpEndTime2.Size = new System.Drawing.Size(200, 23);
            this.dtpEndTime2.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(284, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 15);
            this.label13.TabIndex = 11499;
            this.label13.Text = "End Time";
            // 
            // dtpBeginTime2
            // 
            this.dtpBeginTime2.CustomFormat = "HH:mm";
            this.dtpBeginTime2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBeginTime2.Location = new System.Drawing.Point(71, 15);
            this.dtpBeginTime2.Name = "dtpBeginTime2";
            this.dtpBeginTime2.Size = new System.Drawing.Size(200, 23);
            this.dtpBeginTime2.TabIndex = 5;
            // 
            // gb1
            // 
            this.gb1.Controls.Add(this.dtpEndTime1);
            this.gb1.Controls.Add(this.label12);
            this.gb1.Controls.Add(this.dtpBeginTime1);
            this.gb1.Controls.Add(this.label11);
            this.gb1.Enabled = false;
            this.gb1.Location = new System.Drawing.Point(388, 44);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(561, 45);
            this.gb1.TabIndex = 11496;
            this.gb1.TabStop = false;
            this.gb1.Text = "1st Shift Timings";
            this.gb1.Visible = false;
            // 
            // dtpEndTime1
            // 
            this.dtpEndTime1.CustomFormat = "HH:mm";
            this.dtpEndTime1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime1.Location = new System.Drawing.Point(342, 16);
            this.dtpEndTime1.Name = "dtpEndTime1";
            this.dtpEndTime1.Size = new System.Drawing.Size(200, 23);
            this.dtpEndTime1.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(284, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 15);
            this.label12.TabIndex = 11499;
            this.label12.Text = "End Time";
            // 
            // dtpBeginTime1
            // 
            this.dtpBeginTime1.CustomFormat = "HH:mm";
            this.dtpBeginTime1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBeginTime1.Location = new System.Drawing.Point(71, 16);
            this.dtpBeginTime1.Name = "dtpBeginTime1";
            this.dtpBeginTime1.Size = new System.Drawing.Size(200, 23);
            this.dtpBeginTime1.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 15);
            this.label11.TabIndex = 11497;
            this.label11.Text = "Begin Time";
            // 
            // clbNoOfShift
            // 
            this.clbNoOfShift.FormattingEnabled = true;
            this.clbNoOfShift.Items.AddRange(new object[] {
            "1st Shift",
            "2nd Shift",
            "3rd Shift"});
            this.clbNoOfShift.Location = new System.Drawing.Point(1125, 77);
            this.clbNoOfShift.Name = "clbNoOfShift";
            this.clbNoOfShift.Size = new System.Drawing.Size(61, 22);
            this.clbNoOfShift.TabIndex = 11495;
            this.clbNoOfShift.Visible = false;
            this.clbNoOfShift.Leave += new System.EventHandler(this.clbNoOfShift_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(47, 94);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 15);
            this.label10.TabIndex = 11500;
            this.label10.Text = "Shifts";
            // 
            // cmbShiftHours
            // 
            this.cmbShiftHours.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbShiftHours.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbShiftHours.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShiftHours.FormattingEnabled = true;
            this.cmbShiftHours.Items.AddRange(new object[] {
            "360",
            "480",
            "720"});
            this.cmbShiftHours.Location = new System.Drawing.Point(87, 66);
            this.cmbShiftHours.Name = "cmbShiftHours";
            this.cmbShiftHours.Size = new System.Drawing.Size(122, 23);
            this.cmbShiftHours.TabIndex = 1;
            this.cmbShiftHours.SelectionChangeCommitted += new System.EventHandler(this.cmbShiftHours_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 11499;
            this.label1.Text = "Shift Hours";
            // 
            // dtpTime
            // 
            this.dtpTime.Enabled = false;
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(248, 34);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(110, 23);
            this.dtpTime.TabIndex = 11504;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(213, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 11506;
            this.label6.Text = "Time";
            // 
            // dtpShiftDate
            // 
            this.dtpShiftDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpShiftDate.Location = new System.Drawing.Point(87, 34);
            this.dtpShiftDate.Name = "dtpShiftDate";
            this.dtpShiftDate.Size = new System.Drawing.Size(122, 23);
            this.dtpShiftDate.TabIndex = 0;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(25, 37);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(60, 15);
            this.lblDate.TabIndex = 11505;
            this.lblDate.Text = "Shift Date";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(602, 216);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(522, 216);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 5;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(442, 216);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(682, 216);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 7;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cmbShifts
            // 
            this.cmbShifts.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbShifts.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbShifts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShifts.FormattingEnabled = true;
            this.cmbShifts.Items.AddRange(new object[] {
            "1,2,3",
            "1,2",
            "1"});
            this.cmbShifts.Location = new System.Drawing.Point(87, 90);
            this.cmbShifts.Name = "cmbShifts";
            this.cmbShifts.Size = new System.Drawing.Size(122, 23);
            this.cmbShifts.TabIndex = 2;
            this.cmbShifts.SelectionChangeCommitted += new System.EventHandler(this.cmbShifts_SelectionChangeCommitted);
            // 
            // dgvShifts
            // 
            this.dgvShifts.AllowUserToAddRows = false;
            this.dgvShifts.AllowUserToDeleteRows = false;
            this.dgvShifts.AllowUserToResizeColumns = false;
            this.dgvShifts.AllowUserToResizeRows = false;
            this.dgvShifts.BackgroundColor = System.Drawing.Color.White;
            this.dgvShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShifts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmShiftScheduleId,
            this.clmEntryDate,
            this.clmEntryTime,
            this.clmFromDate,
            this.clmToDate,
            this.clmShiftDate,
            this.clmShiftHours,
            this.clmNoOfShifts,
            this.clmBeginTime1,
            this.clmEndTime1,
            this.clmBeginTime2,
            this.clmEndTime2,
            this.clmBeginTime3,
            this.clmEndTime3,
            this.clmNaration});
            this.dgvShifts.GridColor = System.Drawing.SystemColors.Info;
            this.dgvShifts.Location = new System.Drawing.Point(1125, 104);
            this.dgvShifts.Name = "dgvShifts";
            this.dgvShifts.ReadOnly = true;
            this.dgvShifts.RowHeadersVisible = false;
            this.dgvShifts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShifts.Size = new System.Drawing.Size(61, 19);
            this.dgvShifts.TabIndex = 11512;
            this.dgvShifts.TabStop = false;
            this.dgvShifts.Visible = false;
            this.dgvShifts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // clmShiftScheduleId
            // 
            this.clmShiftScheduleId.HeaderText = "ShiftScheduleId";
            this.clmShiftScheduleId.Name = "clmShiftScheduleId";
            this.clmShiftScheduleId.ReadOnly = true;
            this.clmShiftScheduleId.Visible = false;
            // 
            // clmEntryDate
            // 
            this.clmEntryDate.HeaderText = "Date";
            this.clmEntryDate.Name = "clmEntryDate";
            this.clmEntryDate.ReadOnly = true;
            // 
            // clmEntryTime
            // 
            this.clmEntryTime.HeaderText = "Time";
            this.clmEntryTime.Name = "clmEntryTime";
            this.clmEntryTime.ReadOnly = true;
            // 
            // clmFromDate
            // 
            this.clmFromDate.HeaderText = "FromDate";
            this.clmFromDate.Name = "clmFromDate";
            this.clmFromDate.ReadOnly = true;
            this.clmFromDate.Visible = false;
            // 
            // clmToDate
            // 
            this.clmToDate.HeaderText = "ToDate";
            this.clmToDate.Name = "clmToDate";
            this.clmToDate.ReadOnly = true;
            this.clmToDate.Visible = false;
            // 
            // clmShiftDate
            // 
            this.clmShiftDate.HeaderText = "Shift Date";
            this.clmShiftDate.Name = "clmShiftDate";
            this.clmShiftDate.ReadOnly = true;
            // 
            // clmShiftHours
            // 
            this.clmShiftHours.HeaderText = "Shift Hours";
            this.clmShiftHours.Name = "clmShiftHours";
            this.clmShiftHours.ReadOnly = true;
            // 
            // clmNoOfShifts
            // 
            this.clmNoOfShifts.HeaderText = "No of Shifts";
            this.clmNoOfShifts.Name = "clmNoOfShifts";
            this.clmNoOfShifts.ReadOnly = true;
            // 
            // clmBeginTime1
            // 
            this.clmBeginTime1.HeaderText = "Begin Time 1";
            this.clmBeginTime1.Name = "clmBeginTime1";
            this.clmBeginTime1.ReadOnly = true;
            // 
            // clmEndTime1
            // 
            this.clmEndTime1.HeaderText = "End Time 1";
            this.clmEndTime1.Name = "clmEndTime1";
            this.clmEndTime1.ReadOnly = true;
            // 
            // clmBeginTime2
            // 
            this.clmBeginTime2.HeaderText = "Begin Time 2";
            this.clmBeginTime2.Name = "clmBeginTime2";
            this.clmBeginTime2.ReadOnly = true;
            // 
            // clmEndTime2
            // 
            this.clmEndTime2.HeaderText = "End Time 2";
            this.clmEndTime2.Name = "clmEndTime2";
            this.clmEndTime2.ReadOnly = true;
            // 
            // clmBeginTime3
            // 
            this.clmBeginTime3.HeaderText = "Begin Time 3";
            this.clmBeginTime3.Name = "clmBeginTime3";
            this.clmBeginTime3.ReadOnly = true;
            // 
            // clmEndTime3
            // 
            this.clmEndTime3.HeaderText = "End Time 3";
            this.clmEndTime3.Name = "clmEndTime3";
            this.clmEndTime3.ReadOnly = true;
            // 
            // clmNaration
            // 
            this.clmNaration.HeaderText = "Naration";
            this.clmNaration.Name = "clmNaration";
            this.clmNaration.ReadOnly = true;
            // 
            // dtpSearchDate
            // 
            this.dtpSearchDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchDate.Location = new System.Drawing.Point(1064, 224);
            this.dtpSearchDate.Name = "dtpSearchDate";
            this.dtpSearchDate.Size = new System.Drawing.Size(122, 23);
            this.dtpSearchDate.TabIndex = 11513;
            this.dtpSearchDate.ValueChanged += new System.EventHandler(this.dtpSearchDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(990, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 11514;
            this.label3.Text = "Search Date";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(14, 235);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(69, 15);
            this.lblTotalCount.TabIndex = 11515;
            this.lblTotalCount.Text = "Total Count";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1044, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 11517;
            this.label2.Text = "Shift ID";
            // 
            // txtShiftID
            // 
            this.txtShiftID.Location = new System.Drawing.Point(1093, 34);
            this.txtShiftID.Name = "txtShiftID";
            this.txtShiftID.ReadOnly = true;
            this.txtShiftID.Size = new System.Drawing.Size(93, 23);
            this.txtShiftID.TabIndex = 11516;
            this.txtShiftID.TabStop = false;
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
            this.dataGridView1.Location = new System.Drawing.Point(12, 252);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1174, 437);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // txtNarration
            // 
            this.txtNarration.Location = new System.Drawing.Point(388, 185);
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(542, 23);
            this.txtNarration.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(326, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 11520;
            this.label4.Text = "Narration";
            // 
            // ShiftScheduleNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1198, 698);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNarration);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtShiftID);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.dtpSearchDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvShifts);
            this.Controls.Add(this.cmbShifts);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpShiftDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.gb3);
            this.Controls.Add(this.gb2);
            this.Controls.Add(this.gb1);
            this.Controls.Add(this.clbNoOfShift);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmbShiftHours);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ShiftScheduleNew";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ShiftScheduleNew_Load);
            this.gb3.ResumeLayout(false);
            this.gb3.PerformLayout();
            this.gb2.ResumeLayout(false);
            this.gb2.PerformLayout();
            this.gb1.ResumeLayout(false);
            this.gb1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox gb3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtpEndTime3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker dtpBeginTime3;
        private System.Windows.Forms.GroupBox gb2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpEndTime2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpBeginTime2;
        private System.Windows.Forms.GroupBox gb1;
        private System.Windows.Forms.DateTimePicker dtpEndTime1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpBeginTime1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckedListBox clbNoOfShift;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbShiftHours;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpShiftDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cmbShifts;
        private System.Windows.Forms.DataGridView dgvShifts;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmShiftScheduleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEntryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEntryTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFromDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmToDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmShiftDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmShiftHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNoOfShifts;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBeginTime1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEndTime1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBeginTime2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEndTime2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBeginTime3;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEndTime3;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNaration;
        private System.Windows.Forms.DateTimePicker dtpSearchDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtShiftID;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtNarration;
        private System.Windows.Forms.Label label4;
    }
}