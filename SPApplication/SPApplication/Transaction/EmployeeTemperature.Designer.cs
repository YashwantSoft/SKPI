namespace SPApplication.Transaction
{
    partial class EmployeeTemperature
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
            this.label12 = new System.Windows.Forms.Label();
            this.txtShift = new System.Windows.Forms.TextBox();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbEmployeeList = new System.Windows.Forms.ListBox();
            this.txtSearchCap = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInTemperature = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOutTemperature = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSearchID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.cbToday = new System.Windows.Forms.CheckBox();
            this.label27 = new System.Windows.Forms.Label();
            this.dtpSearchDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.rtbEmployeeInformation = new System.Windows.Forms.RichTextBox();
            this.txtInRemarks = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpInTime = new System.Windows.Forms.DateTimePicker();
            this.dtpOutTime = new System.Windows.Forms.DateTimePicker();
            this.btnReport = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtOutRemarks = new System.Windows.Forms.TextBox();
            this.gbOutInformation = new System.Windows.Forms.GroupBox();
            this.gbIntInformation = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbOutInformation.SuspendLayout();
            this.gbIntInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(-2, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1189, 37);
            this.lblHeader.TabIndex = 11379;
            this.lblHeader.Text = "Employee Temperature";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(272, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 19);
            this.label12.TabIndex = 11439;
            this.label12.Text = "Shift";
            // 
            // txtShift
            // 
            this.txtShift.Location = new System.Drawing.Point(312, 41);
            this.txtShift.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtShift.Name = "txtShift";
            this.txtShift.ReadOnly = true;
            this.txtShift.Size = new System.Drawing.Size(125, 27);
            this.txtShift.TabIndex = 11438;
            this.txtShift.TabStop = false;
            // 
            // dtpTime
            // 
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(1042, 41);
            this.dtpTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(125, 27);
            this.dtpTime.TabIndex = 11434;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(999, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 19);
            this.label6.TabIndex = 11437;
            this.label6.Text = "Time";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(858, 40);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(125, 27);
            this.dtpDate.TabIndex = 11433;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(815, 44);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(40, 19);
            this.lblDate.TabIndex = 11435;
            this.lblDate.Text = "Date";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(127, 41);
            this.txtID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(125, 27);
            this.txtID.TabIndex = 11432;
            this.txtID.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 19);
            this.label2.TabIndex = 11436;
            this.label2.Text = "Sr No";
            // 
            // lbEmployeeList
            // 
            this.lbEmployeeList.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmployeeList.FormattingEnabled = true;
            this.lbEmployeeList.ItemHeight = 19;
            this.lbEmployeeList.Location = new System.Drawing.Point(128, 113);
            this.lbEmployeeList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbEmployeeList.Name = "lbEmployeeList";
            this.lbEmployeeList.Size = new System.Drawing.Size(425, 194);
            this.lbEmployeeList.TabIndex = 1;
            this.lbEmployeeList.Visible = false;
            this.lbEmployeeList.Click += new System.EventHandler(this.lbEmployeeList_Click);
            this.lbEmployeeList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbEmployeeList_KeyDown);
            // 
            // txtSearchCap
            // 
            this.txtSearchCap.Location = new System.Drawing.Point(128, 82);
            this.txtSearchCap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearchCap.Name = "txtSearchCap";
            this.txtSearchCap.Size = new System.Drawing.Size(425, 27);
            this.txtSearchCap.TabIndex = 0;
            this.txtSearchCap.TextChanged += new System.EventHandler(this.txtSearchCap_TextChanged);
            this.txtSearchCap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchCap_KeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 116);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(99, 19);
            this.label21.TabIndex = 11459;
            this.label21.Text = "Employee List";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 19);
            this.label1.TabIndex = 11458;
            this.label1.Text = "Search Employee";
            // 
            // txtInTemperature
            // 
            this.txtInTemperature.Location = new System.Drawing.Point(124, 50);
            this.txtInTemperature.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtInTemperature.MaxLength = 4;
            this.txtInTemperature.Name = "txtInTemperature";
            this.txtInTemperature.Size = new System.Drawing.Size(140, 27);
            this.txtInTemperature.TabIndex = 2;
            this.txtInTemperature.TextChanged += new System.EventHandler(this.txtInTemperature_TextChanged);
            this.txtInTemperature.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInTemperature_KeyDown);
            this.txtInTemperature.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInTemperature_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 19);
            this.label3.TabIndex = 11461;
            this.label3.Text = "In Temperature";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 19);
            this.label4.TabIndex = 11463;
            this.label4.Text = "Out Time";
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
            this.dataGridView1.Location = new System.Drawing.Point(12, 386);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1162, 263);
            this.dataGridView1.TabIndex = 11464;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(516, 314);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 4;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(595, 314);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(437, 314);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(674, 314);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 6;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 19);
            this.label5.TabIndex = 11470;
            this.label5.Text = "In Time";
            // 
            // txtOutTemperature
            // 
            this.txtOutTemperature.Location = new System.Drawing.Point(127, 50);
            this.txtOutTemperature.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOutTemperature.MaxLength = 4;
            this.txtOutTemperature.Name = "txtOutTemperature";
            this.txtOutTemperature.Size = new System.Drawing.Size(140, 27);
            this.txtOutTemperature.TabIndex = 11471;
            this.txtOutTemperature.TextChanged += new System.EventHandler(this.txtOutTemperature_TextChanged);
            this.txtOutTemperature.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOutTemperature_KeyDown);
            this.txtOutTemperature.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOutTemperature_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 19);
            this.label7.TabIndex = 11472;
            this.label7.Text = "Out Temperature";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(569, 357);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 19);
            this.label13.TabIndex = 11477;
            this.label13.Text = "ID";
            // 
            // txtSearchID
            // 
            this.txtSearchID.Location = new System.Drawing.Point(594, 353);
            this.txtSearchID.Name = "txtSearchID";
            this.txtSearchID.Size = new System.Drawing.Size(57, 27);
            this.txtSearchID.TabIndex = 8;
            this.txtSearchID.TextChanged += new System.EventHandler(this.txtSearchID_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(310, 356);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 19);
            this.label10.TabIndex = 11476;
            this.label10.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(364, 353);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 27);
            this.txtSearch.TabIndex = 7;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(11, 359);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(88, 19);
            this.lblTotalCount.TabIndex = 11475;
            this.lblTotalCount.Text = "Total Count-";
            // 
            // cbToday
            // 
            this.cbToday.AutoSize = true;
            this.cbToday.Location = new System.Drawing.Point(813, 355);
            this.cbToday.Name = "cbToday";
            this.cbToday.Size = new System.Drawing.Size(66, 23);
            this.cbToday.TabIndex = 10;
            this.cbToday.Text = "Today";
            this.cbToday.UseVisualStyleBackColor = true;
            this.cbToday.CheckedChanged += new System.EventHandler(this.cbToday_CheckedChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(670, 356);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(40, 19);
            this.label27.TabIndex = 11480;
            this.label27.Text = "Date";
            // 
            // dtpSearchDate
            // 
            this.dtpSearchDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchDate.Location = new System.Drawing.Point(712, 353);
            this.dtpSearchDate.Name = "dtpSearchDate";
            this.dtpSearchDate.Size = new System.Drawing.Size(94, 27);
            this.dtpSearchDate.TabIndex = 9;
            this.dtpSearchDate.ValueChanged += new System.EventHandler(this.dtpSearchDate_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(574, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 19);
            this.label8.TabIndex = 11498;
            this.label8.Text = "Employee Information";
            // 
            // rtbEmployeeInformation
            // 
            this.rtbEmployeeInformation.Location = new System.Drawing.Point(575, 85);
            this.rtbEmployeeInformation.Name = "rtbEmployeeInformation";
            this.rtbEmployeeInformation.Size = new System.Drawing.Size(592, 106);
            this.rtbEmployeeInformation.TabIndex = 11499;
            this.rtbEmployeeInformation.TabStop = false;
            this.rtbEmployeeInformation.Text = "";
            // 
            // txtInRemarks
            // 
            this.txtInRemarks.BackColor = System.Drawing.Color.White;
            this.txtInRemarks.Location = new System.Drawing.Point(124, 78);
            this.txtInRemarks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtInRemarks.Name = "txtInRemarks";
            this.txtInRemarks.ReadOnly = true;
            this.txtInRemarks.Size = new System.Drawing.Size(140, 27);
            this.txtInRemarks.TabIndex = 11500;
            this.txtInRemarks.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 19);
            this.label9.TabIndex = 11501;
            this.label9.Text = "In Remarks";
            // 
            // dtpInTime
            // 
            this.dtpInTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpInTime.Location = new System.Drawing.Point(124, 22);
            this.dtpInTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpInTime.Name = "dtpInTime";
            this.dtpInTime.Size = new System.Drawing.Size(140, 27);
            this.dtpInTime.TabIndex = 11502;
            // 
            // dtpOutTime
            // 
            this.dtpOutTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpOutTime.Location = new System.Drawing.Point(127, 22);
            this.dtpOutTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpOutTime.Name = "dtpOutTime";
            this.dtpOutTime.Size = new System.Drawing.Size(140, 27);
            this.dtpOutTime.TabIndex = 11503;
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Location = new System.Drawing.Point(1092, 350);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 30);
            this.btnReport.TabIndex = 11;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 19);
            this.label11.TabIndex = 11506;
            this.label11.Text = "Out Remarks";
            // 
            // txtOutRemarks
            // 
            this.txtOutRemarks.BackColor = System.Drawing.Color.White;
            this.txtOutRemarks.Location = new System.Drawing.Point(127, 78);
            this.txtOutRemarks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOutRemarks.Name = "txtOutRemarks";
            this.txtOutRemarks.ReadOnly = true;
            this.txtOutRemarks.Size = new System.Drawing.Size(140, 27);
            this.txtOutRemarks.TabIndex = 11505;
            // 
            // gbOutInformation
            // 
            this.gbOutInformation.Controls.Add(this.txtOutRemarks);
            this.gbOutInformation.Controls.Add(this.label11);
            this.gbOutInformation.Controls.Add(this.label4);
            this.gbOutInformation.Controls.Add(this.label7);
            this.gbOutInformation.Controls.Add(this.txtOutTemperature);
            this.gbOutInformation.Controls.Add(this.dtpOutTime);
            this.gbOutInformation.Location = new System.Drawing.Point(867, 191);
            this.gbOutInformation.Name = "gbOutInformation";
            this.gbOutInformation.Size = new System.Drawing.Size(300, 115);
            this.gbOutInformation.TabIndex = 11507;
            this.gbOutInformation.TabStop = false;
            this.gbOutInformation.Text = "Out Information";
            this.gbOutInformation.Visible = false;
            // 
            // gbIntInformation
            // 
            this.gbIntInformation.Controls.Add(this.txtInRemarks);
            this.gbIntInformation.Controls.Add(this.label3);
            this.gbIntInformation.Controls.Add(this.txtInTemperature);
            this.gbIntInformation.Controls.Add(this.dtpInTime);
            this.gbIntInformation.Controls.Add(this.label5);
            this.gbIntInformation.Controls.Add(this.label9);
            this.gbIntInformation.Location = new System.Drawing.Point(561, 191);
            this.gbIntInformation.Name = "gbIntInformation";
            this.gbIntInformation.Size = new System.Drawing.Size(300, 115);
            this.gbIntInformation.TabIndex = 2;
            this.gbIntInformation.TabStop = false;
            this.gbIntInformation.Text = "In Information";
            this.gbIntInformation.Visible = false;
            // 
            // EmployeeTemperature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1186, 654);
            this.ControlBox = false;
            this.Controls.Add(this.gbIntInformation);
            this.Controls.Add(this.gbOutInformation);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.rtbEmployeeInformation);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbToday);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.dtpSearchDate);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtSearchID);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbEmployeeList);
            this.Controls.Add(this.txtSearchCap);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtShift);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EmployeeTemperature";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.EmployeeTemperature_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbOutInformation.ResumeLayout(false);
            this.gbOutInformation.PerformLayout();
            this.gbIntInformation.ResumeLayout(false);
            this.gbIntInformation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtShift;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbEmployeeList;
        private System.Windows.Forms.TextBox txtSearchCap;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInTemperature;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOutTemperature;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSearchID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.CheckBox cbToday;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.DateTimePicker dtpSearchDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox rtbEmployeeInformation;
        private System.Windows.Forms.TextBox txtInRemarks;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpInTime;
        private System.Windows.Forms.DateTimePicker dtpOutTime;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtOutRemarks;
        private System.Windows.Forms.GroupBox gbOutInformation;
        private System.Windows.Forms.GroupBox gbIntInformation;
    }
}