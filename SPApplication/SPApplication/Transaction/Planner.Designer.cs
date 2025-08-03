namespace SPApplication.Transaction
{
    partial class Planner
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.clmSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEntryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDepartment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFollowUp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmComplete = new System.Windows.Forms.DataGridViewLinkColumn();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ToolTip_Message = new System.Windows.Forms.ToolTip(this.components);
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.cbDepartmentAll = new System.Windows.Forms.CheckBox();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.gbCalendar = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblJarInformation = new System.Windows.Forms.Label();
            this.lblBottleInformation = new System.Windows.Forms.Label();
            this.btn31 = new System.Windows.Forms.Button();
            this.btn30 = new System.Windows.Forms.Button();
            this.btn29 = new System.Windows.Forms.Button();
            this.btn27 = new System.Windows.Forms.Button();
            this.btn26 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn14 = new System.Windows.Forms.Button();
            this.btn28 = new System.Windows.Forms.Button();
            this.btn13 = new System.Windows.Forms.Button();
            this.btn20 = new System.Windows.Forms.Button();
            this.btn21 = new System.Windows.Forms.Button();
            this.btn25 = new System.Windows.Forms.Button();
            this.btn24 = new System.Windows.Forms.Button();
            this.btn23 = new System.Windows.Forms.Button();
            this.btn22 = new System.Windows.Forms.Button();
            this.btn19 = new System.Windows.Forms.Button();
            this.btn18 = new System.Windows.Forms.Button();
            this.btn17 = new System.Windows.Forms.Button();
            this.btn16 = new System.Windows.Forms.Button();
            this.btn15 = new System.Windows.Forms.Button();
            this.btn12 = new System.Windows.Forms.Button();
            this.btn11 = new System.Windows.Forms.Button();
            this.btn10 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btnAddYourTask = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBlank = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTodayDate = new System.Windows.Forms.Label();
            this.gbDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbCalendar.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDetails
            // 
            this.gbDetails.Controls.Add(this.dataGridView1);
            this.gbDetails.Location = new System.Drawing.Point(9, 357);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(1277, 335);
            this.gbDetails.TabIndex = 115;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Details";
            this.gbDetails.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSrNo,
            this.clmId,
            this.clmEntryDate,
            this.clmDepartment,
            this.clmTask,
            this.clmFollowUp,
            this.clmStatus,
            this.clmComplete});
            this.dataGridView1.Location = new System.Drawing.Point(8, 18);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1259, 310);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
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
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clmDepartment.DefaultCellStyle = dataGridViewCellStyle5;
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
            this.clmFollowUp.Width = 250;
            // 
            // clmStatus
            // 
            this.clmStatus.HeaderText = "Status";
            this.clmStatus.Name = "clmStatus";
            this.clmStatus.ReadOnly = true;
            // 
            // clmComplete
            // 
            this.clmComplete.HeaderText = "Complete";
            this.clmComplete.Name = "clmComplete";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(619, 31);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 112;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(700, 31);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 111;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(-1, -2);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1297, 28);
            this.lblHeader.TabIndex = 110;
            this.lblHeader.Text = "PLANNER";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 105;
            this.label2.Text = "Month";
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
            this.cmbMonth.Location = new System.Drawing.Point(49, 30);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(121, 23);
            this.cmbMonth.TabIndex = 103;
            this.cmbMonth.SelectionChangeCommitted += new System.EventHandler(this.cmbMonth_SelectionChangeCommitted);
            // 
            // cbDepartmentAll
            // 
            this.cbDepartmentAll.AutoSize = true;
            this.cbDepartmentAll.Location = new System.Drawing.Point(395, 32);
            this.cbDepartmentAll.Name = "cbDepartmentAll";
            this.cbDepartmentAll.Size = new System.Drawing.Size(41, 19);
            this.cbDepartmentAll.TabIndex = 11596;
            this.cbDepartmentAll.Text = "All";
            this.cbDepartmentAll.UseVisualStyleBackColor = true;
            this.cbDepartmentAll.Visible = false;
            this.cbDepartmentAll.CheckedChanged += new System.EventHandler(this.cbDepartmentAll_CheckedChanged);
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(278, 29);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(110, 23);
            this.cmbDepartment.TabIndex = 11594;
            this.cmbDepartment.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(205, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 15);
            this.label10.TabIndex = 11595;
            this.label10.Text = "Department";
            this.label10.Visible = false;
            // 
            // gbCalendar
            // 
            this.gbCalendar.Controls.Add(this.label16);
            this.gbCalendar.Controls.Add(this.label15);
            this.gbCalendar.Controls.Add(this.lblJarInformation);
            this.gbCalendar.Controls.Add(this.lblBottleInformation);
            this.gbCalendar.Controls.Add(this.btn31);
            this.gbCalendar.Controls.Add(this.btn30);
            this.gbCalendar.Controls.Add(this.btn29);
            this.gbCalendar.Controls.Add(this.btn27);
            this.gbCalendar.Controls.Add(this.btn26);
            this.gbCalendar.Controls.Add(this.btn7);
            this.gbCalendar.Controls.Add(this.btn6);
            this.gbCalendar.Controls.Add(this.btn14);
            this.gbCalendar.Controls.Add(this.btn28);
            this.gbCalendar.Controls.Add(this.btn13);
            this.gbCalendar.Controls.Add(this.btn20);
            this.gbCalendar.Controls.Add(this.btn21);
            this.gbCalendar.Controls.Add(this.btn25);
            this.gbCalendar.Controls.Add(this.btn24);
            this.gbCalendar.Controls.Add(this.btn23);
            this.gbCalendar.Controls.Add(this.btn22);
            this.gbCalendar.Controls.Add(this.btn19);
            this.gbCalendar.Controls.Add(this.btn18);
            this.gbCalendar.Controls.Add(this.btn17);
            this.gbCalendar.Controls.Add(this.btn16);
            this.gbCalendar.Controls.Add(this.btn15);
            this.gbCalendar.Controls.Add(this.btn12);
            this.gbCalendar.Controls.Add(this.btn11);
            this.gbCalendar.Controls.Add(this.btn10);
            this.gbCalendar.Controls.Add(this.btn9);
            this.gbCalendar.Controls.Add(this.btn8);
            this.gbCalendar.Controls.Add(this.btn5);
            this.gbCalendar.Controls.Add(this.btn4);
            this.gbCalendar.Controls.Add(this.btn3);
            this.gbCalendar.Controls.Add(this.btn2);
            this.gbCalendar.Controls.Add(this.btn1);
            this.gbCalendar.Location = new System.Drawing.Point(7, 62);
            this.gbCalendar.Name = "gbCalendar";
            this.gbCalendar.Size = new System.Drawing.Size(1279, 295);
            this.gbCalendar.TabIndex = 11597;
            this.gbCalendar.TabStop = false;
            this.gbCalendar.Text = "Calendar";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1213, 273);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 15);
            this.label16.TabIndex = 11605;
            this.label16.Text = "Pending";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1126, 274);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 15);
            this.label15.TabIndex = 11604;
            this.label15.Text = "Complete";
            // 
            // lblJarInformation
            // 
            this.lblJarInformation.BackColor = System.Drawing.Color.Yellow;
            this.lblJarInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblJarInformation.Location = new System.Drawing.Point(1191, 270);
            this.lblJarInformation.Name = "lblJarInformation";
            this.lblJarInformation.Size = new System.Drawing.Size(20, 20);
            this.lblJarInformation.TabIndex = 11603;
            this.lblJarInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBottleInformation
            // 
            this.lblBottleInformation.BackColor = System.Drawing.Color.LawnGreen;
            this.lblBottleInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBottleInformation.Location = new System.Drawing.Point(1103, 271);
            this.lblBottleInformation.Name = "lblBottleInformation";
            this.lblBottleInformation.Size = new System.Drawing.Size(20, 20);
            this.lblBottleInformation.TabIndex = 11602;
            this.lblBottleInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn31
            // 
            this.btn31.BackColor = System.Drawing.Color.Snow;
            this.btn31.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn31.Location = new System.Drawing.Point(188, 208);
            this.btn31.Name = "btn31";
            this.btn31.Size = new System.Drawing.Size(80, 80);
            this.btn31.TabIndex = 132;
            this.btn31.Text = "31";
            this.btn31.UseVisualStyleBackColor = false;
            this.btn31.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn30
            // 
            this.btn30.BackColor = System.Drawing.Color.Snow;
            this.btn30.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn30.Location = new System.Drawing.Point(98, 208);
            this.btn30.Name = "btn30";
            this.btn30.Size = new System.Drawing.Size(80, 80);
            this.btn30.TabIndex = 131;
            this.btn30.Text = "30";
            this.btn30.UseVisualStyleBackColor = false;
            this.btn30.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn29
            // 
            this.btn29.BackColor = System.Drawing.Color.Snow;
            this.btn29.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn29.Location = new System.Drawing.Point(8, 208);
            this.btn29.Name = "btn29";
            this.btn29.Size = new System.Drawing.Size(80, 80);
            this.btn29.TabIndex = 130;
            this.btn29.Text = "29";
            this.btn29.UseVisualStyleBackColor = false;
            this.btn29.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn27
            // 
            this.btn27.BackColor = System.Drawing.Color.Snow;
            this.btn27.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn27.Location = new System.Drawing.Point(1098, 107);
            this.btn27.Name = "btn27";
            this.btn27.Size = new System.Drawing.Size(80, 80);
            this.btn27.TabIndex = 128;
            this.btn27.Text = "27";
            this.btn27.UseVisualStyleBackColor = false;
            this.btn27.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn26
            // 
            this.btn26.BackColor = System.Drawing.Color.Snow;
            this.btn26.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn26.Location = new System.Drawing.Point(1007, 107);
            this.btn26.Name = "btn26";
            this.btn26.Size = new System.Drawing.Size(80, 80);
            this.btn26.TabIndex = 127;
            this.btn26.Text = "26";
            this.btn26.UseVisualStyleBackColor = false;
            this.btn26.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn7
            // 
            this.btn7.BackColor = System.Drawing.Color.Snow;
            this.btn7.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn7.Location = new System.Drawing.Point(552, 18);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(80, 80);
            this.btn7.TabIndex = 126;
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = false;
            this.btn7.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn6
            // 
            this.btn6.BackColor = System.Drawing.Color.Snow;
            this.btn6.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn6.Location = new System.Drawing.Point(461, 19);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(80, 80);
            this.btn6.TabIndex = 125;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = false;
            this.btn6.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn14
            // 
            this.btn14.BackColor = System.Drawing.Color.Snow;
            this.btn14.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn14.Location = new System.Drawing.Point(1189, 19);
            this.btn14.Name = "btn14";
            this.btn14.Size = new System.Drawing.Size(80, 80);
            this.btn14.TabIndex = 124;
            this.btn14.Text = "14";
            this.btn14.UseVisualStyleBackColor = false;
            this.btn14.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn28
            // 
            this.btn28.BackColor = System.Drawing.Color.Snow;
            this.btn28.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn28.Location = new System.Drawing.Point(1189, 107);
            this.btn28.Name = "btn28";
            this.btn28.Size = new System.Drawing.Size(80, 80);
            this.btn28.TabIndex = 129;
            this.btn28.Text = "28";
            this.btn28.UseVisualStyleBackColor = false;
            this.btn28.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn13
            // 
            this.btn13.BackColor = System.Drawing.Color.Snow;
            this.btn13.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn13.Location = new System.Drawing.Point(1098, 19);
            this.btn13.Name = "btn13";
            this.btn13.Size = new System.Drawing.Size(80, 80);
            this.btn13.TabIndex = 123;
            this.btn13.Text = "13";
            this.btn13.UseVisualStyleBackColor = false;
            this.btn13.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn20
            // 
            this.btn20.BackColor = System.Drawing.Color.Snow;
            this.btn20.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn20.Location = new System.Drawing.Point(461, 110);
            this.btn20.Name = "btn20";
            this.btn20.Size = new System.Drawing.Size(80, 80);
            this.btn20.TabIndex = 122;
            this.btn20.Text = "20";
            this.btn20.UseVisualStyleBackColor = false;
            this.btn20.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn21
            // 
            this.btn21.BackColor = System.Drawing.Color.Snow;
            this.btn21.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn21.Location = new System.Drawing.Point(552, 110);
            this.btn21.Name = "btn21";
            this.btn21.Size = new System.Drawing.Size(80, 80);
            this.btn21.TabIndex = 121;
            this.btn21.Text = "21";
            this.btn21.UseVisualStyleBackColor = false;
            this.btn21.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn25
            // 
            this.btn25.BackColor = System.Drawing.Color.Snow;
            this.btn25.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn25.Location = new System.Drawing.Point(916, 107);
            this.btn25.Name = "btn25";
            this.btn25.Size = new System.Drawing.Size(80, 80);
            this.btn25.TabIndex = 120;
            this.btn25.Text = "25";
            this.btn25.UseVisualStyleBackColor = false;
            this.btn25.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn24
            // 
            this.btn24.BackColor = System.Drawing.Color.Snow;
            this.btn24.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn24.Location = new System.Drawing.Point(825, 109);
            this.btn24.Name = "btn24";
            this.btn24.Size = new System.Drawing.Size(80, 80);
            this.btn24.TabIndex = 119;
            this.btn24.Text = "24";
            this.btn24.UseVisualStyleBackColor = false;
            this.btn24.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn23
            // 
            this.btn23.BackColor = System.Drawing.Color.Snow;
            this.btn23.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn23.Location = new System.Drawing.Point(734, 110);
            this.btn23.Name = "btn23";
            this.btn23.Size = new System.Drawing.Size(80, 80);
            this.btn23.TabIndex = 118;
            this.btn23.Text = "23";
            this.btn23.UseVisualStyleBackColor = false;
            this.btn23.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn22
            // 
            this.btn22.BackColor = System.Drawing.Color.Snow;
            this.btn22.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn22.Location = new System.Drawing.Point(643, 110);
            this.btn22.Name = "btn22";
            this.btn22.Size = new System.Drawing.Size(80, 80);
            this.btn22.TabIndex = 117;
            this.btn22.Text = "22";
            this.btn22.UseVisualStyleBackColor = false;
            this.btn22.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn19
            // 
            this.btn19.BackColor = System.Drawing.Color.Snow;
            this.btn19.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn19.Location = new System.Drawing.Point(370, 110);
            this.btn19.Name = "btn19";
            this.btn19.Size = new System.Drawing.Size(80, 80);
            this.btn19.TabIndex = 116;
            this.btn19.Text = "19";
            this.btn19.UseVisualStyleBackColor = false;
            this.btn19.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn18
            // 
            this.btn18.BackColor = System.Drawing.Color.Snow;
            this.btn18.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn18.Location = new System.Drawing.Point(279, 110);
            this.btn18.Name = "btn18";
            this.btn18.Size = new System.Drawing.Size(80, 80);
            this.btn18.TabIndex = 115;
            this.btn18.Text = "18";
            this.btn18.UseVisualStyleBackColor = false;
            this.btn18.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn17
            // 
            this.btn17.BackColor = System.Drawing.Color.Snow;
            this.btn17.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn17.Location = new System.Drawing.Point(188, 111);
            this.btn17.Name = "btn17";
            this.btn17.Size = new System.Drawing.Size(80, 80);
            this.btn17.TabIndex = 114;
            this.btn17.Text = "17";
            this.btn17.UseVisualStyleBackColor = false;
            this.btn17.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn16
            // 
            this.btn16.BackColor = System.Drawing.Color.Snow;
            this.btn16.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn16.Location = new System.Drawing.Point(97, 112);
            this.btn16.Name = "btn16";
            this.btn16.Size = new System.Drawing.Size(80, 80);
            this.btn16.TabIndex = 113;
            this.btn16.Text = "16";
            this.btn16.UseVisualStyleBackColor = false;
            this.btn16.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn15
            // 
            this.btn15.BackColor = System.Drawing.Color.Snow;
            this.btn15.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn15.Location = new System.Drawing.Point(6, 112);
            this.btn15.Name = "btn15";
            this.btn15.Size = new System.Drawing.Size(80, 80);
            this.btn15.TabIndex = 112;
            this.btn15.Text = "15";
            this.btn15.UseVisualStyleBackColor = false;
            this.btn15.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn12
            // 
            this.btn12.BackColor = System.Drawing.Color.Snow;
            this.btn12.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn12.Location = new System.Drawing.Point(1007, 20);
            this.btn12.Name = "btn12";
            this.btn12.Size = new System.Drawing.Size(80, 80);
            this.btn12.TabIndex = 111;
            this.btn12.Text = "12";
            this.btn12.UseVisualStyleBackColor = false;
            this.btn12.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn11
            // 
            this.btn11.BackColor = System.Drawing.Color.Snow;
            this.btn11.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn11.Location = new System.Drawing.Point(916, 20);
            this.btn11.Name = "btn11";
            this.btn11.Size = new System.Drawing.Size(80, 80);
            this.btn11.TabIndex = 110;
            this.btn11.Text = "11";
            this.btn11.UseVisualStyleBackColor = false;
            this.btn11.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn10
            // 
            this.btn10.BackColor = System.Drawing.Color.Snow;
            this.btn10.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn10.Location = new System.Drawing.Point(825, 20);
            this.btn10.Name = "btn10";
            this.btn10.Size = new System.Drawing.Size(80, 80);
            this.btn10.TabIndex = 109;
            this.btn10.Text = "10";
            this.btn10.UseVisualStyleBackColor = false;
            this.btn10.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn9
            // 
            this.btn9.BackColor = System.Drawing.Color.Snow;
            this.btn9.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn9.Location = new System.Drawing.Point(734, 19);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(80, 80);
            this.btn9.TabIndex = 108;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = false;
            this.btn9.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn8
            // 
            this.btn8.BackColor = System.Drawing.Color.Snow;
            this.btn8.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn8.Location = new System.Drawing.Point(643, 19);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(80, 80);
            this.btn8.TabIndex = 107;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = false;
            this.btn8.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn5
            // 
            this.btn5.BackColor = System.Drawing.Color.Snow;
            this.btn5.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn5.Location = new System.Drawing.Point(370, 19);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(80, 80);
            this.btn5.TabIndex = 106;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = false;
            this.btn5.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn4
            // 
            this.btn4.BackColor = System.Drawing.Color.Snow;
            this.btn4.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4.Location = new System.Drawing.Point(279, 19);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(80, 80);
            this.btn4.TabIndex = 105;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = false;
            this.btn4.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn3
            // 
            this.btn3.BackColor = System.Drawing.Color.Snow;
            this.btn3.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn3.Location = new System.Drawing.Point(188, 18);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(80, 80);
            this.btn3.TabIndex = 104;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = false;
            this.btn3.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn2
            // 
            this.btn2.BackColor = System.Drawing.Color.Snow;
            this.btn2.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2.Location = new System.Drawing.Point(97, 18);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(80, 80);
            this.btn2.TabIndex = 103;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = false;
            this.btn2.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btn1
            // 
            this.btn1.BackColor = System.Drawing.Color.Snow;
            this.btn1.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1.Location = new System.Drawing.Point(6, 18);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(80, 80);
            this.btn1.TabIndex = 102;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = false;
            this.btn1.Click += new System.EventHandler(this.ButtonClickMain);
            // 
            // btnAddYourTask
            // 
            this.btnAddYourTask.BackColor = System.Drawing.Color.Transparent;
            this.btnAddYourTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddYourTask.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddYourTask.Location = new System.Drawing.Point(519, 31);
            this.btnAddYourTask.Name = "btnAddYourTask";
            this.btnAddYourTask.Size = new System.Drawing.Size(94, 30);
            this.btnAddYourTask.TabIndex = 11598;
            this.btnAddYourTask.Text = "Add Your Task";
            this.btnAddYourTask.UseVisualStyleBackColor = false;
            this.btnAddYourTask.Click += new System.EventHandler(this.btnAddYourTask_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1076, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 11609;
            this.label1.Text = "Blank";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1142, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 11608;
            this.label3.Text = "Select";
            // 
            // lblBlank
            // 
            this.lblBlank.BackColor = System.Drawing.Color.LightSkyBlue;
            this.lblBlank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBlank.Location = new System.Drawing.Point(1054, 33);
            this.lblBlank.Name = "lblBlank";
            this.lblBlank.Size = new System.Drawing.Size(20, 20);
            this.lblBlank.TabIndex = 11607;
            this.lblBlank.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Pink;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(1120, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 20);
            this.label5.TabIndex = 11606;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1209, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 11611;
            this.label4.Text = "Today Date";
            // 
            // lblTodayDate
            // 
            this.lblTodayDate.BackColor = System.Drawing.Color.AliceBlue;
            this.lblTodayDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTodayDate.Location = new System.Drawing.Point(1187, 33);
            this.lblTodayDate.Name = "lblTodayDate";
            this.lblTodayDate.Size = new System.Drawing.Size(20, 20);
            this.lblTodayDate.TabIndex = 11606;
            this.lblTodayDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Planner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1295, 697);
            this.ControlBox = false;
            this.Controls.Add(this.lblTodayDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddYourTask);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gbCalendar);
            this.Controls.Add(this.lblBlank);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbDepartmentAll);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.cmbDepartment);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbMonth);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Planner";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Planner_Load);
            this.gbDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbCalendar.ResumeLayout(false);
            this.gbCalendar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip ToolTip_Message;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.CheckBox cbDepartmentAll;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox gbCalendar;
        private System.Windows.Forms.Button btn31;
        private System.Windows.Forms.Button btn30;
        private System.Windows.Forms.Button btn29;
        private System.Windows.Forms.Button btn27;
        private System.Windows.Forms.Button btn26;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn14;
        private System.Windows.Forms.Button btn28;
        private System.Windows.Forms.Button btn13;
        private System.Windows.Forms.Button btn20;
        private System.Windows.Forms.Button btn21;
        private System.Windows.Forms.Button btn25;
        private System.Windows.Forms.Button btn24;
        private System.Windows.Forms.Button btn23;
        private System.Windows.Forms.Button btn22;
        private System.Windows.Forms.Button btn19;
        private System.Windows.Forms.Button btn18;
        private System.Windows.Forms.Button btn17;
        private System.Windows.Forms.Button btn16;
        private System.Windows.Forms.Button btn15;
        private System.Windows.Forms.Button btn12;
        private System.Windows.Forms.Button btn11;
        private System.Windows.Forms.Button btn10;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblJarInformation;
        private System.Windows.Forms.Label lblBottleInformation;
        private System.Windows.Forms.Button btnAddYourTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEntryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFollowUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStatus;
        private System.Windows.Forms.DataGridViewLinkColumn clmComplete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBlank;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTodayDate;
    }
}