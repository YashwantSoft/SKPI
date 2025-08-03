namespace SPApplication.Master
{
    partial class CompanyInformation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtContactNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmailId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWebSite = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtCST = new System.Windows.Forms.TextBox();
            this.txtVATTIN = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtGSTNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSiteAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtIFSCCode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbPrimary = new System.Windows.Forms.CheckBox();
            this.txtBankAddress = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dgvBankDetails = new System.Windows.Forms.DataGridView();
            this.clmSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPrimary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBankName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBankAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAccountNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAccountType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAccountHolderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmIFSCCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDeleteGrid = new System.Windows.Forms.Button();
            this.btnClearGrid = new System.Windows.Forms.Button();
            this.btnAddGrid = new System.Windows.Forms.Button();
            this.cmbAccountType = new System.Windows.Forms.ComboBox();
            this.txtAccountHolderName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBankDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(138, 56);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(453, 23);
            this.txtCompanyName.TabIndex = 0;
            this.txtCompanyName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompanyName_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 133;
            this.label2.Text = "Company Name";
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1451, 28);
            this.lblHeader.TabIndex = 135;
            this.lblHeader.Text = "Company Information";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(138, 80);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(453, 69);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 136;
            this.label1.Text = "Address";
            // 
            // txtContactNo
            // 
            this.txtContactNo.Location = new System.Drawing.Point(138, 221);
            this.txtContactNo.Name = "txtContactNo";
            this.txtContactNo.Size = new System.Drawing.Size(453, 23);
            this.txtContactNo.TabIndex = 3;
            this.txtContactNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContactNo_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 138;
            this.label3.Text = "Contact No.";
            // 
            // txtEmailId
            // 
            this.txtEmailId.Location = new System.Drawing.Point(138, 245);
            this.txtEmailId.Name = "txtEmailId";
            this.txtEmailId.Size = new System.Drawing.Size(453, 23);
            this.txtEmailId.TabIndex = 4;
            this.txtEmailId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmailId_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 140;
            this.label4.Text = "Email Id";
            // 
            // txtWebSite
            // 
            this.txtWebSite.Location = new System.Drawing.Point(138, 269);
            this.txtWebSite.Name = "txtWebSite";
            this.txtWebSite.Size = new System.Drawing.Size(453, 23);
            this.txtWebSite.TabIndex = 5;
            this.txtWebSite.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWebSite_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 272);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 142;
            this.label5.Text = "Web Site";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(242, 343);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 19;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(162, 343);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 18;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(322, 343);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 21;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(402, 343);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            // 
            // txtCST
            // 
            this.txtCST.Location = new System.Drawing.Point(752, 99);
            this.txtCST.Name = "txtCST";
            this.txtCST.Size = new System.Drawing.Size(23, 23);
            this.txtCST.TabIndex = 6;
            this.txtCST.Visible = false;
            this.txtCST.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCST_KeyDown);
            // 
            // txtVATTIN
            // 
            this.txtVATTIN.Location = new System.Drawing.Point(752, 75);
            this.txtVATTIN.Name = "txtVATTIN";
            this.txtVATTIN.Size = new System.Drawing.Size(23, 23);
            this.txtVATTIN.TabIndex = 6;
            this.txtVATTIN.Visible = false;
            this.txtVATTIN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVATTIN_KeyDown);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(673, 101);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(68, 15);
            this.label25.TabIndex = 198;
            this.label25.Text = "CST TIN No.";
            this.label25.Visible = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(673, 78);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(67, 15);
            this.label26.TabIndex = 197;
            this.label26.Text = "VAT TIN No.";
            this.label26.Visible = false;
            // 
            // txtGSTNo
            // 
            this.txtGSTNo.Location = new System.Drawing.Point(138, 293);
            this.txtGSTNo.Name = "txtGSTNo";
            this.txtGSTNo.Size = new System.Drawing.Size(453, 23);
            this.txtGSTNo.TabIndex = 6;
            this.txtGSTNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGSTNo_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 296);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 200;
            this.label6.Text = "GSTIN No.";
            // 
            // txtSiteAddress
            // 
            this.txtSiteAddress.Location = new System.Drawing.Point(138, 150);
            this.txtSiteAddress.Multiline = true;
            this.txtSiteAddress.Name = "txtSiteAddress";
            this.txtSiteAddress.Size = new System.Drawing.Size(453, 69);
            this.txtSiteAddress.TabIndex = 2;
            this.txtSiteAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSiteAddress_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 15);
            this.label7.TabIndex = 202;
            this.label7.Text = "Site Address";
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(222, 13);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(424, 23);
            this.txtBankName.TabIndex = 8;
            this.txtBankName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBankName_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(95, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 15);
            this.label8.TabIndex = 204;
            this.label8.Text = "Bank Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtIFSCCode);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cbPrimary);
            this.groupBox1.Controls.Add(this.txtBankAddress);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.dgvBankDetails);
            this.groupBox1.Controls.Add(this.btnDeleteGrid);
            this.groupBox1.Controls.Add(this.txtCST);
            this.groupBox1.Controls.Add(this.txtVATTIN);
            this.groupBox1.Controls.Add(this.btnClearGrid);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.btnAddGrid);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.cmbAccountType);
            this.groupBox1.Controls.Add(this.txtAccountHolderName);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtAccountNumber);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtBankName);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(633, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(806, 354);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account Details";
            // 
            // txtIFSCCode
            // 
            this.txtIFSCCode.Location = new System.Drawing.Point(222, 148);
            this.txtIFSCCode.MaxLength = 14;
            this.txtIFSCCode.Name = "txtIFSCCode";
            this.txtIFSCCode.Size = new System.Drawing.Size(180, 23);
            this.txtIFSCCode.TabIndex = 13;
            this.txtIFSCCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIFSCCode_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(95, 155);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 15);
            this.label13.TabIndex = 221;
            this.label13.Text = "IFSC Code";
            // 
            // cbPrimary
            // 
            this.cbPrimary.AutoSize = true;
            this.cbPrimary.Location = new System.Drawing.Point(666, 16);
            this.cbPrimary.Name = "cbPrimary";
            this.cbPrimary.Size = new System.Drawing.Size(83, 19);
            this.cbPrimary.TabIndex = 9;
            this.cbPrimary.Text = "Is Primary";
            this.cbPrimary.UseVisualStyleBackColor = true;
            this.cbPrimary.CheckedChanged += new System.EventHandler(this.cbPrimary_CheckedChanged);
            // 
            // txtBankAddress
            // 
            this.txtBankAddress.Location = new System.Drawing.Point(222, 37);
            this.txtBankAddress.Multiline = true;
            this.txtBankAddress.Name = "txtBankAddress";
            this.txtBankAddress.Size = new System.Drawing.Size(424, 38);
            this.txtBankAddress.TabIndex = 9;
            this.txtBankAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBankAddress_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(95, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 15);
            this.label12.TabIndex = 218;
            this.label12.Text = "Bank Address";
            // 
            // dgvBankDetails
            // 
            this.dgvBankDetails.AllowUserToAddRows = false;
            this.dgvBankDetails.AllowUserToDeleteRows = false;
            this.dgvBankDetails.AllowUserToResizeRows = false;
            this.dgvBankDetails.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBankDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBankDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBankDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSrNo,
            this.clmPrimary,
            this.clmBankName,
            this.clmBankAddress,
            this.clmAccountNumber,
            this.clmAccountType,
            this.clmAccountHolderName,
            this.clmIFSCCode});
            this.dgvBankDetails.Location = new System.Drawing.Point(20, 176);
            this.dgvBankDetails.Name = "dgvBankDetails";
            this.dgvBankDetails.RowHeadersVisible = false;
            this.dgvBankDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBankDetails.Size = new System.Drawing.Size(773, 173);
            this.dgvBankDetails.TabIndex = 17;
            this.dgvBankDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBankDetails_CellDoubleClick);
            // 
            // clmSrNo
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmSrNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmSrNo.HeaderText = "Sr.No.";
            this.clmSrNo.Name = "clmSrNo";
            this.clmSrNo.ReadOnly = true;
            this.clmSrNo.Width = 40;
            // 
            // clmPrimary
            // 
            this.clmPrimary.HeaderText = "Primary";
            this.clmPrimary.Name = "clmPrimary";
            this.clmPrimary.ReadOnly = true;
            // 
            // clmBankName
            // 
            this.clmBankName.HeaderText = "Bank Name";
            this.clmBankName.Name = "clmBankName";
            this.clmBankName.ReadOnly = true;
            this.clmBankName.Width = 150;
            // 
            // clmBankAddress
            // 
            this.clmBankAddress.HeaderText = "Bank Address";
            this.clmBankAddress.Name = "clmBankAddress";
            this.clmBankAddress.ReadOnly = true;
            this.clmBankAddress.Width = 150;
            // 
            // clmAccountNumber
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clmAccountNumber.DefaultCellStyle = dataGridViewCellStyle3;
            this.clmAccountNumber.HeaderText = "Account No.";
            this.clmAccountNumber.Name = "clmAccountNumber";
            this.clmAccountNumber.Width = 150;
            // 
            // clmAccountType
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clmAccountType.DefaultCellStyle = dataGridViewCellStyle4;
            this.clmAccountType.HeaderText = "Account Type";
            this.clmAccountType.Name = "clmAccountType";
            this.clmAccountType.ReadOnly = true;
            this.clmAccountType.Width = 120;
            // 
            // clmAccountHolderName
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmAccountHolderName.DefaultCellStyle = dataGridViewCellStyle5;
            this.clmAccountHolderName.HeaderText = "Account Holder Name";
            this.clmAccountHolderName.Name = "clmAccountHolderName";
            this.clmAccountHolderName.ReadOnly = true;
            this.clmAccountHolderName.Width = 150;
            // 
            // clmIFSCCode
            // 
            this.clmIFSCCode.HeaderText = "IFSC Code";
            this.clmIFSCCode.Name = "clmIFSCCode";
            this.clmIFSCCode.ReadOnly = true;
            // 
            // btnDeleteGrid
            // 
            this.btnDeleteGrid.BackColor = System.Drawing.Color.Blue;
            this.btnDeleteGrid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteGrid.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteGrid.ForeColor = System.Drawing.Color.White;
            this.btnDeleteGrid.Location = new System.Drawing.Point(584, 148);
            this.btnDeleteGrid.Name = "btnDeleteGrid";
            this.btnDeleteGrid.Size = new System.Drawing.Size(62, 22);
            this.btnDeleteGrid.TabIndex = 16;
            this.btnDeleteGrid.Text = "&Delete";
            this.btnDeleteGrid.UseVisualStyleBackColor = false;
            this.btnDeleteGrid.Click += new System.EventHandler(this.btnDeleteGrid_Click);
            // 
            // btnClearGrid
            // 
            this.btnClearGrid.BackColor = System.Drawing.Color.Blue;
            this.btnClearGrid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearGrid.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearGrid.ForeColor = System.Drawing.Color.White;
            this.btnClearGrid.Location = new System.Drawing.Point(518, 148);
            this.btnClearGrid.Name = "btnClearGrid";
            this.btnClearGrid.Size = new System.Drawing.Size(62, 22);
            this.btnClearGrid.TabIndex = 15;
            this.btnClearGrid.Text = "&Clear";
            this.btnClearGrid.UseVisualStyleBackColor = false;
            this.btnClearGrid.Click += new System.EventHandler(this.btnClearGrid_Click);
            // 
            // btnAddGrid
            // 
            this.btnAddGrid.BackColor = System.Drawing.Color.Blue;
            this.btnAddGrid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddGrid.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddGrid.ForeColor = System.Drawing.Color.White;
            this.btnAddGrid.Location = new System.Drawing.Point(451, 148);
            this.btnAddGrid.Name = "btnAddGrid";
            this.btnAddGrid.Size = new System.Drawing.Size(62, 22);
            this.btnAddGrid.TabIndex = 14;
            this.btnAddGrid.Text = "&Add";
            this.btnAddGrid.UseVisualStyleBackColor = false;
            this.btnAddGrid.Click += new System.EventHandler(this.btnAddGrid_Click);
            // 
            // cmbAccountType
            // 
            this.cmbAccountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccountType.FormattingEnabled = true;
            this.cmbAccountType.Items.AddRange(new object[] {
            "Saving",
            "Current",
            "Cash Credit"});
            this.cmbAccountType.Location = new System.Drawing.Point(222, 100);
            this.cmbAccountType.Name = "cmbAccountType";
            this.cmbAccountType.Size = new System.Drawing.Size(424, 23);
            this.cmbAccountType.TabIndex = 11;
            this.cmbAccountType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbAccountType_KeyDown);
            // 
            // txtAccountHolderName
            // 
            this.txtAccountHolderName.Location = new System.Drawing.Point(222, 124);
            this.txtAccountHolderName.Name = "txtAccountHolderName";
            this.txtAccountHolderName.Size = new System.Drawing.Size(424, 23);
            this.txtAccountHolderName.TabIndex = 12;
            this.txtAccountHolderName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccountHolderName_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(95, 131);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 15);
            this.label10.TabIndex = 210;
            this.label10.Text = "Account Holder Name";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(95, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 15);
            this.label11.TabIndex = 208;
            this.label11.Text = "Account Type";
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Location = new System.Drawing.Point(222, 76);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new System.Drawing.Size(424, 23);
            this.txtAccountNumber.TabIndex = 10;
            this.txtAccountNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccountNumber_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(95, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 15);
            this.label9.TabIndex = 206;
            this.label9.Text = "Account Number";
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
            this.dataGridView1.Location = new System.Drawing.Point(14, 387);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1425, 178);
            this.dataGridView1.TabIndex = 21;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(17, 367);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(73, 15);
            this.lblTotalCount.TabIndex = 204;
            this.lblTotalCount.Text = "Total Count-";
            // 
            // CompanyInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1449, 572);
            this.ControlBox = false;
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtSiteAddress);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtGSTNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtWebSite);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEmailId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtContactNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.txtCompanyName);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CompanyInformation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CompanyInformation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBankDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtContactNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmailId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWebSite;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtCST;
        private System.Windows.Forms.TextBox txtVATTIN;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtGSTNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSiteAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbAccountType;
        private System.Windows.Forms.TextBox txtAccountHolderName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAccountNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnDeleteGrid;
        private System.Windows.Forms.Button btnClearGrid;
        private System.Windows.Forms.Button btnAddGrid;
        private System.Windows.Forms.DataGridView dgvBankDetails;
        private System.Windows.Forms.TextBox txtBankAddress;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbPrimary;
        private System.Windows.Forms.TextBox txtIFSCCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPrimary;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBankName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBankAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAccountNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAccountType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAccountHolderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIFSCCode;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblTotalCount;
    }
}