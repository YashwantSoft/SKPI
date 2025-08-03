namespace SPApplication.Transaction
{
    partial class Expenses
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
            this.cmbExpensesHead = new System.Windows.Forms.ComboBox();
            this.lblHead = new System.Windows.Forms.Label();
            this.txtNaration = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnAddExpensesHead = new System.Windows.Forms.Button();
            this.cmbPaymentMode = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.gbChequeDetails = new System.Windows.Forms.GroupBox();
            this.txtAccountNoParty = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtBankParty = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.btnAddBankInfo = new System.Windows.Forms.Button();
            this.dtpTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.cmbBankName = new System.Windows.Forms.ComboBox();
            this.lblChequeDate = new System.Windows.Forms.Label();
            this.txtChequeNo = new System.Windows.Forms.TextBox();
            this.lblChequeNo = new System.Windows.Forms.Label();
            this.txtAccountNo = new System.Windows.Forms.TextBox();
            this.lblAccountNo = new System.Windows.Forms.Label();
            this.lblBankName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lbl = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.cbToday = new System.Windows.Forms.CheckBox();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbChequeDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Blue;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(-1, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1029, 30);
            this.lblHeader.TabIndex = 178;
            this.lblHeader.Text = "Expenses";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbExpensesHead
            // 
            this.cmbExpensesHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExpensesHead.FormattingEnabled = true;
            this.cmbExpensesHead.Location = new System.Drawing.Point(119, 61);
            this.cmbExpensesHead.Name = "cmbExpensesHead";
            this.cmbExpensesHead.Size = new System.Drawing.Size(417, 23);
            this.cmbExpensesHead.TabIndex = 1;
            this.cmbExpensesHead.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbExpensesHead_KeyDown);
            // 
            // lblHead
            // 
            this.lblHead.AutoSize = true;
            this.lblHead.Location = new System.Drawing.Point(29, 65);
            this.lblHead.Name = "lblHead";
            this.lblHead.Size = new System.Drawing.Size(88, 15);
            this.lblHead.TabIndex = 180;
            this.lblHead.Text = "Expenses Head";
            // 
            // txtNaration
            // 
            this.txtNaration.Location = new System.Drawing.Point(119, 86);
            this.txtNaration.Multiline = true;
            this.txtNaration.Name = "txtNaration";
            this.txtNaration.Size = new System.Drawing.Size(417, 82);
            this.txtNaration.TabIndex = 2;
            this.txtNaration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpensesDescription_KeyDown);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(29, 90);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(55, 15);
            this.lblDescription.TabIndex = 182;
            this.lblDescription.Text = "Naration";
            // 
            // btnAddExpensesHead
            // 
            this.btnAddExpensesHead.BackColor = System.Drawing.Color.Blue;
            this.btnAddExpensesHead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddExpensesHead.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddExpensesHead.ForeColor = System.Drawing.Color.White;
            this.btnAddExpensesHead.Location = new System.Drawing.Point(544, 64);
            this.btnAddExpensesHead.Name = "btnAddExpensesHead";
            this.btnAddExpensesHead.Size = new System.Drawing.Size(20, 20);
            this.btnAddExpensesHead.TabIndex = 1;
            this.btnAddExpensesHead.Text = "+";
            this.btnAddExpensesHead.UseVisualStyleBackColor = false;
            this.btnAddExpensesHead.Click += new System.EventHandler(this.btnAddExpensesHead_Click);
            // 
            // cmbPaymentMode
            // 
            this.cmbPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMode.FormattingEnabled = true;
            this.cmbPaymentMode.Location = new System.Drawing.Point(396, 170);
            this.cmbPaymentMode.Name = "cmbPaymentMode";
            this.cmbPaymentMode.Size = new System.Drawing.Size(140, 23);
            this.cmbPaymentMode.TabIndex = 4;
            this.cmbPaymentMode.SelectionChangeCommitted += new System.EventHandler(this.cmbPaymentMode_SelectionChangeCommitted);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(118, 170);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(140, 23);
            this.txtAmount.TabIndex = 3;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(28, 174);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(49, 15);
            this.label17.TabIndex = 11289;
            this.label17.Text = "Amount";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(313, 173);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 15);
            this.label9.TabIndex = 11288;
            this.label9.Text = "Payment Type";
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(520, 208);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(612, 208);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 30);
            this.btnExit.TabIndex = 12;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(428, 208);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 10;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(336, 208);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 9;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(14, 272);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1000, 287);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 11283;
            this.label1.Text = "Date";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(119, 36);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(140, 23);
            this.dtpDate.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.ForeColor = System.Drawing.Color.SeaGreen;
            this.txtSearch.Location = new System.Drawing.Point(118, 246);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(418, 23);
            this.txtSearch.TabIndex = 13;
            this.txtSearch.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(29, 251);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(44, 15);
            this.lblSearch.TabIndex = 11303;
            this.lblSearch.Text = "Search";
            // 
            // gbChequeDetails
            // 
            this.gbChequeDetails.Controls.Add(this.txtAccountNoParty);
            this.gbChequeDetails.Controls.Add(this.label30);
            this.gbChequeDetails.Controls.Add(this.txtBankParty);
            this.gbChequeDetails.Controls.Add(this.label27);
            this.gbChequeDetails.Controls.Add(this.btnAddBankInfo);
            this.gbChequeDetails.Controls.Add(this.dtpTransactionDate);
            this.gbChequeDetails.Controls.Add(this.cmbBankName);
            this.gbChequeDetails.Controls.Add(this.lblChequeDate);
            this.gbChequeDetails.Controls.Add(this.txtChequeNo);
            this.gbChequeDetails.Controls.Add(this.lblChequeNo);
            this.gbChequeDetails.Controls.Add(this.txtAccountNo);
            this.gbChequeDetails.Controls.Add(this.lblAccountNo);
            this.gbChequeDetails.Controls.Add(this.lblBankName);
            this.gbChequeDetails.Location = new System.Drawing.Point(570, 31);
            this.gbChequeDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbChequeDetails.Name = "gbChequeDetails";
            this.gbChequeDetails.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbChequeDetails.Size = new System.Drawing.Size(447, 166);
            this.gbChequeDetails.TabIndex = 11363;
            this.gbChequeDetails.TabStop = false;
            this.gbChequeDetails.Text = "Cheque Details";
            this.gbChequeDetails.Visible = false;
            // 
            // txtAccountNoParty
            // 
            this.txtAccountNoParty.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAccountNoParty.Location = new System.Drawing.Point(81, 139);
            this.txtAccountNoParty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAccountNoParty.Name = "txtAccountNoParty";
            this.txtAccountNoParty.Size = new System.Drawing.Size(334, 23);
            this.txtAccountNoParty.TabIndex = 76;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(4, 142);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(71, 15);
            this.label30.TabIndex = 77;
            this.label30.Text = "Account no.";
            // 
            // txtBankParty
            // 
            this.txtBankParty.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBankParty.Location = new System.Drawing.Point(81, 114);
            this.txtBankParty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBankParty.Name = "txtBankParty";
            this.txtBankParty.Size = new System.Drawing.Size(334, 23);
            this.txtBankParty.TabIndex = 74;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(4, 117);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(75, 15);
            this.label27.TabIndex = 75;
            this.label27.Text = "Party\'s Bank";
            // 
            // btnAddBankInfo
            // 
            this.btnAddBankInfo.BackColor = System.Drawing.Color.Blue;
            this.btnAddBankInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBankInfo.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddBankInfo.ForeColor = System.Drawing.Color.White;
            this.btnAddBankInfo.Location = new System.Drawing.Point(421, 15);
            this.btnAddBankInfo.Name = "btnAddBankInfo";
            this.btnAddBankInfo.Size = new System.Drawing.Size(20, 20);
            this.btnAddBankInfo.TabIndex = 73;
            this.btnAddBankInfo.Text = "+";
            this.btnAddBankInfo.UseVisualStyleBackColor = false;
            // 
            // dtpTransactionDate
            // 
            this.dtpTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTransactionDate.Location = new System.Drawing.Point(81, 64);
            this.dtpTransactionDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(334, 23);
            this.dtpTransactionDate.TabIndex = 8;
            // 
            // cmbBankName
            // 
            this.cmbBankName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBankName.FormattingEnabled = true;
            this.cmbBankName.Location = new System.Drawing.Point(81, 14);
            this.cmbBankName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbBankName.Name = "cmbBankName";
            this.cmbBankName.Size = new System.Drawing.Size(334, 23);
            this.cmbBankName.TabIndex = 7;
            this.cmbBankName.SelectionChangeCommitted += new System.EventHandler(this.cmbBankName_SelectionChangeCommitted_1);
            // 
            // lblChequeDate
            // 
            this.lblChequeDate.AutoSize = true;
            this.lblChequeDate.Location = new System.Drawing.Point(4, 68);
            this.lblChequeDate.Name = "lblChequeDate";
            this.lblChequeDate.Size = new System.Drawing.Size(47, 15);
            this.lblChequeDate.TabIndex = 30;
            this.lblChequeDate.Text = "Tr. Date";
            // 
            // txtChequeNo
            // 
            this.txtChequeNo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtChequeNo.Location = new System.Drawing.Point(81, 89);
            this.txtChequeNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Size = new System.Drawing.Size(334, 23);
            this.txtChequeNo.TabIndex = 9;
            // 
            // lblChequeNo
            // 
            this.lblChequeNo.AutoSize = true;
            this.lblChequeNo.Location = new System.Drawing.Point(4, 92);
            this.lblChequeNo.Name = "lblChequeNo";
            this.lblChequeNo.Size = new System.Drawing.Size(68, 15);
            this.lblChequeNo.TabIndex = 28;
            this.lblChequeNo.Text = "Cheque No.";
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.BackColor = System.Drawing.Color.White;
            this.txtAccountNo.Location = new System.Drawing.Point(81, 39);
            this.txtAccountNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.ReadOnly = true;
            this.txtAccountNo.Size = new System.Drawing.Size(334, 23);
            this.txtAccountNo.TabIndex = 27;
            this.txtAccountNo.TabStop = false;
            // 
            // lblAccountNo
            // 
            this.lblAccountNo.AutoSize = true;
            this.lblAccountNo.Location = new System.Drawing.Point(4, 44);
            this.lblAccountNo.Name = "lblAccountNo";
            this.lblAccountNo.Size = new System.Drawing.Size(72, 15);
            this.lblAccountNo.TabIndex = 26;
            this.lblAccountNo.Text = "Account No.";
            // 
            // lblBankName
            // 
            this.lblBankName.AutoSize = true;
            this.lblBankName.Location = new System.Drawing.Point(4, 20);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(68, 15);
            this.lblBankName.TabIndex = 24;
            this.lblBankName.Text = "Bank Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(742, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 11365;
            this.label2.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(807, 222);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(140, 23);
            this.dtpFromDate.TabIndex = 11364;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(742, 249);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(47, 15);
            this.lbl.TabIndex = 11367;
            this.lbl.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(807, 247);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(140, 23);
            this.dtpToDate.TabIndex = 11366;
            // 
            // cbToday
            // 
            this.cbToday.AutoSize = true;
            this.cbToday.Location = new System.Drawing.Point(953, 250);
            this.cbToday.Name = "cbToday";
            this.cbToday.Size = new System.Drawing.Size(58, 19);
            this.cbToday.TabIndex = 11368;
            this.cbToday.Text = "Today";
            this.cbToday.UseVisualStyleBackColor = true;
            this.cbToday.CheckedChanged += new System.EventHandler(this.cbToday_CheckedChanged);
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(15, 562);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(69, 15);
            this.lblTotalCount.TabIndex = 11369;
            this.lblTotalCount.Text = "Total Count";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(845, 562);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(140, 23);
            this.txtTotalAmount.TabIndex = 11370;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(764, 566);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 15);
            this.label4.TabIndex = 11371;
            this.label4.Text = "Total Amount";
            // 
            // Expenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1028, 596);
            this.ControlBox = false;
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.cbToday);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.gbChequeDetails);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbPaymentMode);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnAddExpensesHead);
            this.Controls.Add(this.txtNaration);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.cmbExpensesHead);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Expenses";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Expenses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbChequeDetails.ResumeLayout(false);
            this.gbChequeDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ComboBox cmbExpensesHead;
        private System.Windows.Forms.Label lblHead;
        private System.Windows.Forms.TextBox txtNaration;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnAddExpensesHead;
        private System.Windows.Forms.ComboBox cmbPaymentMode;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.GroupBox gbChequeDetails;
        private System.Windows.Forms.TextBox txtAccountNoParty;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtBankParty;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button btnAddBankInfo;
        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.ComboBox cmbBankName;
        private System.Windows.Forms.Label lblChequeDate;
        private System.Windows.Forms.TextBox txtChequeNo;
        private System.Windows.Forms.Label lblChequeNo;
        private System.Windows.Forms.TextBox txtAccountNo;
        private System.Windows.Forms.Label lblAccountNo;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.CheckBox cbToday;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label4;
    }
}