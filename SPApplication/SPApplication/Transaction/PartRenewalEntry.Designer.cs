namespace SPApplication.Transaction
{
    partial class PartRenewalEntry
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.lbPart = new System.Windows.Forms.ListBox();
            this.txtSearchPartName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbPartInformation = new System.Windows.Forms.RichTextBox();
            this.gbRenewalDetails = new System.Windows.Forms.GroupBox();
            this.pnRenewal = new System.Windows.Forms.Panel();
            this.cmbRenewalPeriodFor = new System.Windows.Forms.ComboBox();
            this.txtRenewalPeriod = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbIsCompressor = new System.Windows.Forms.CheckBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.txtContactNo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRenewalBy = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNaration = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEndReadingNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtStartReading = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.gbRenewalDetails.SuspendLayout();
            this.pnRenewal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1262, 29);
            this.lblHeader.TabIndex = 70;
            this.lblHeader.Text = "Part / Equipment Renewal Entry";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 15);
            this.label3.TabIndex = 11579;
            this.label3.Text = "Part Renewal ID";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(112, 32);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(105, 23);
            this.txtID.TabIndex = 11578;
            this.txtID.TabStop = false;
            // 
            // dtpTime
            // 
            this.dtpTime.Enabled = false;
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(1131, 32);
            this.dtpTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(119, 23);
            this.dtpTime.TabIndex = 11575;
            this.dtpTime.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1096, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 11577;
            this.label6.Text = "Time";
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(969, 32);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(125, 23);
            this.dtpDate.TabIndex = 11574;
            this.dtpDate.TabStop = false;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(935, 36);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(32, 15);
            this.lblDate.TabIndex = 11576;
            this.lblDate.Text = "Date";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(112, 56);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(421, 23);
            this.cmbDepartment.TabIndex = 0;
            this.cmbDepartment.SelectionChangeCommitted += new System.EventHandler(this.cmbDepartment_SelectionChangeCommitted);
            this.cmbDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDepartment_KeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(7, 59);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(71, 15);
            this.label21.TabIndex = 11573;
            this.label21.Text = "Department";
            // 
            // lbPart
            // 
            this.lbPart.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPart.FormattingEnabled = true;
            this.lbPart.ItemHeight = 19;
            this.lbPart.Location = new System.Drawing.Point(112, 104);
            this.lbPart.Name = "lbPart";
            this.lbPart.Size = new System.Drawing.Size(421, 289);
            this.lbPart.TabIndex = 2;
            this.lbPart.Visible = false;
            this.lbPart.Click += new System.EventHandler(this.lbPart_Click);
            this.lbPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbPart_KeyDown);
            // 
            // txtSearchPartName
            // 
            this.txtSearchPartName.Location = new System.Drawing.Point(112, 80);
            this.txtSearchPartName.Name = "txtSearchPartName";
            this.txtSearchPartName.Size = new System.Drawing.Size(421, 23);
            this.txtSearchPartName.TabIndex = 1;
            this.txtSearchPartName.Click += new System.EventHandler(this.txtSearchPartName_Click);
            this.txtSearchPartName.TextChanged += new System.EventHandler(this.txtSearchPartName_TextChanged);
            this.txtSearchPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchPartName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 45);
            this.label1.TabIndex = 11583;
            this.label1.Text = "Part /\r\nEquipment\r\nList";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 15);
            this.label2.TabIndex = 11582;
            this.label2.Text = "Search Part Name";
            // 
            // rtbPartInformation
            // 
            this.rtbPartInformation.Location = new System.Drawing.Point(112, 105);
            this.rtbPartInformation.Name = "rtbPartInformation";
            this.rtbPartInformation.Size = new System.Drawing.Size(421, 256);
            this.rtbPartInformation.TabIndex = 11585;
            this.rtbPartInformation.Text = "";
            // 
            // gbRenewalDetails
            // 
            this.gbRenewalDetails.Controls.Add(this.pnRenewal);
            this.gbRenewalDetails.Controls.Add(this.cbIsCompressor);
            this.gbRenewalDetails.Controls.Add(this.cmbType);
            this.gbRenewalDetails.Controls.Add(this.txtContactNo);
            this.gbRenewalDetails.Controls.Add(this.label12);
            this.gbRenewalDetails.Controls.Add(this.txtRenewalBy);
            this.gbRenewalDetails.Controls.Add(this.label11);
            this.gbRenewalDetails.Controls.Add(this.txtNaration);
            this.gbRenewalDetails.Controls.Add(this.label10);
            this.gbRenewalDetails.Controls.Add(this.txtEndReadingNo);
            this.gbRenewalDetails.Controls.Add(this.label9);
            this.gbRenewalDetails.Controls.Add(this.txtStartReading);
            this.gbRenewalDetails.Controls.Add(this.label8);
            this.gbRenewalDetails.Controls.Add(this.dtpExpiryDate);
            this.gbRenewalDetails.Controls.Add(this.dtpStartDate);
            this.gbRenewalDetails.Controls.Add(this.label4);
            this.gbRenewalDetails.Controls.Add(this.label7);
            this.gbRenewalDetails.Location = new System.Drawing.Point(539, 56);
            this.gbRenewalDetails.Name = "gbRenewalDetails";
            this.gbRenewalDetails.Size = new System.Drawing.Size(711, 337);
            this.gbRenewalDetails.TabIndex = 3;
            this.gbRenewalDetails.TabStop = false;
            this.gbRenewalDetails.Text = "Renewal Details";
            // 
            // pnRenewal
            // 
            this.pnRenewal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnRenewal.Controls.Add(this.cmbRenewalPeriodFor);
            this.pnRenewal.Controls.Add(this.txtRenewalPeriod);
            this.pnRenewal.Controls.Add(this.label5);
            this.pnRenewal.Location = new System.Drawing.Point(0, 69);
            this.pnRenewal.Name = "pnRenewal";
            this.pnRenewal.Size = new System.Drawing.Size(542, 39);
            this.pnRenewal.TabIndex = 6;
            // 
            // cmbRenewalPeriodFor
            // 
            this.cmbRenewalPeriodFor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRenewalPeriodFor.FormattingEnabled = true;
            this.cmbRenewalPeriodFor.Items.AddRange(new object[] {
            "Days",
            "Months",
            "Years"});
            this.cmbRenewalPeriodFor.Location = new System.Drawing.Point(185, 8);
            this.cmbRenewalPeriodFor.Name = "cmbRenewalPeriodFor";
            this.cmbRenewalPeriodFor.Size = new System.Drawing.Size(63, 23);
            this.cmbRenewalPeriodFor.TabIndex = 7;
            this.cmbRenewalPeriodFor.SelectionChangeCommitted += new System.EventHandler(this.cmbRenewalPeriodFor_SelectionChangeCommitted);
            this.cmbRenewalPeriodFor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbRenewalPeriodFor_KeyDown);
            // 
            // txtRenewalPeriod
            // 
            this.txtRenewalPeriod.Location = new System.Drawing.Point(123, 8);
            this.txtRenewalPeriod.Name = "txtRenewalPeriod";
            this.txtRenewalPeriod.Size = new System.Drawing.Size(56, 23);
            this.txtRenewalPeriod.TabIndex = 6;
            this.txtRenewalPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRenewalPeriod_KeyDown);
            this.txtRenewalPeriod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRenewalPeriod_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 15);
            this.label5.TabIndex = 11590;
            this.label5.Text = "Renewal Period for";
            // 
            // cbIsCompressor
            // 
            this.cbIsCompressor.AutoSize = true;
            this.cbIsCompressor.Location = new System.Drawing.Point(10, 46);
            this.cbIsCompressor.Name = "cbIsCompressor";
            this.cbIsCompressor.Size = new System.Drawing.Size(105, 19);
            this.cbIsCompressor.TabIndex = 4;
            this.cbIsCompressor.Text = "Is Compressor";
            this.cbIsCompressor.UseVisualStyleBackColor = true;
            this.cbIsCompressor.CheckedChanged += new System.EventHandler(this.cbIsCompressor_CheckedChanged);
            this.cbIsCompressor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbIsCompressor_KeyDown);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "LPC  2000Hrs-4000Hrs",
            "HPC 4000Hrs-8000Hrs"});
            this.cmbType.Location = new System.Drawing.Point(124, 44);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(418, 23);
            this.cmbType.TabIndex = 5;
            this.cmbType.Visible = false;
            this.cmbType.SelectionChangeCommitted += new System.EventHandler(this.cmbType_SelectionChangeCommitted);
            this.cmbType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbType_KeyDown);
            // 
            // txtContactNo
            // 
            this.txtContactNo.Location = new System.Drawing.Point(124, 206);
            this.txtContactNo.Name = "txtContactNo";
            this.txtContactNo.Size = new System.Drawing.Size(555, 23);
            this.txtContactNo.TabIndex = 11;
            this.txtContactNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContactNo_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 209);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 15);
            this.label12.TabIndex = 11599;
            this.label12.Text = "Contact No";
            // 
            // txtRenewalBy
            // 
            this.txtRenewalBy.Location = new System.Drawing.Point(124, 182);
            this.txtRenewalBy.Name = "txtRenewalBy";
            this.txtRenewalBy.Size = new System.Drawing.Size(555, 23);
            this.txtRenewalBy.TabIndex = 10;
            this.txtRenewalBy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRenewalBy_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 185);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 15);
            this.label11.TabIndex = 11597;
            this.label11.Text = "Renewal by";
            // 
            // txtNaration
            // 
            this.txtNaration.Location = new System.Drawing.Point(124, 230);
            this.txtNaration.Multiline = true;
            this.txtNaration.Name = "txtNaration";
            this.txtNaration.Size = new System.Drawing.Size(555, 103);
            this.txtNaration.TabIndex = 12;
            this.txtNaration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNaration_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 233);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 15);
            this.label10.TabIndex = 11595;
            this.label10.Text = "Naration";
            // 
            // txtEndReadingNo
            // 
            this.txtEndReadingNo.Location = new System.Drawing.Point(124, 158);
            this.txtEndReadingNo.Name = "txtEndReadingNo";
            this.txtEndReadingNo.Size = new System.Drawing.Size(212, 23);
            this.txtEndReadingNo.TabIndex = 9;
            this.txtEndReadingNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEndReadingNo_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 161);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 15);
            this.label9.TabIndex = 11593;
            this.label9.Text = "End Reading No";
            // 
            // txtStartReading
            // 
            this.txtStartReading.Location = new System.Drawing.Point(124, 134);
            this.txtStartReading.Name = "txtStartReading";
            this.txtStartReading.Size = new System.Drawing.Size(212, 23);
            this.txtStartReading.TabIndex = 8;
            this.txtStartReading.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStartReading_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 15);
            this.label8.TabIndex = 11588;
            this.label8.Text = "Start Reading No";
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.Enabled = false;
            this.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExpiryDate.Location = new System.Drawing.Point(124, 110);
            this.dtpExpiryDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(212, 23);
            this.dtpExpiryDate.TabIndex = 11591;
            this.dtpExpiryDate.TabStop = false;
            this.dtpExpiryDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpiryDate_KeyDown);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(124, 20);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(212, 23);
            this.dtpStartDate.TabIndex = 3;
            this.dtpStartDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpStartDate_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 15);
            this.label4.TabIndex = 11578;
            this.label4.Text = "Start/Renewal Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 15);
            this.label7.TabIndex = 11588;
            this.label7.Text = "Expiry/Last Date";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(554, 399);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 14;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(633, 399);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(475, 399);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(712, 399);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 16;
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
            this.dataGridView1.Location = new System.Drawing.Point(10, 435);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1240, 173);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(12, 417);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(73, 15);
            this.lblTotalCount.TabIndex = 11594;
            this.lblTotalCount.Text = "Total Count-";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(1032, 404);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(218, 23);
            this.txtSearch.TabIndex = 17;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(986, 407);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(44, 15);
            this.lblSearch.TabIndex = 11593;
            this.lblSearch.Text = "Search";
            // 
            // PartRenewalEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1262, 613);
            this.ControlBox = false;
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.gbRenewalDetails);
            this.Controls.Add(this.lbPart);
            this.Controls.Add(this.rtbPartInformation);
            this.Controls.Add(this.txtSearchPartName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.cmbDepartment);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PartRenewalEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PartRenewalEntry_Load);
            this.gbRenewalDetails.ResumeLayout(false);
            this.gbRenewalDetails.PerformLayout();
            this.pnRenewal.ResumeLayout(false);
            this.pnRenewal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ListBox lbPart;
        private System.Windows.Forms.TextBox txtSearchPartName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtbPartInformation;
        private System.Windows.Forms.GroupBox gbRenewalDetails;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.TextBox txtRenewalPeriod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbRenewalPeriodFor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNaration;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtEndReadingNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtStartReading;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtContactNo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRenewalBy;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox cbIsCompressor;
        private System.Windows.Forms.Panel pnRenewal;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
    }
}