namespace SPApplication.Transaction
{
    partial class Payment
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblPaymentMode = new System.Windows.Forms.Label();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.cmbPaymentMode = new System.Windows.Forms.ComboBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblTotalPending = new System.Windows.Forms.Label();
            this.txtPaidAmount = new System.Windows.Forms.TextBox();
            this.txtTotalDue = new System.Windows.Forms.TextBox();
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this.txtDueAmount = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.lbDetails = new System.Windows.Forms.ListBox();
            this.rtbDetails = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNaration = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblCDueAmount = new System.Windows.Forms.Label();
            this.lblCPaidAmount = new System.Windows.Forms.Label();
            this.lblCTotalDue = new System.Windows.Forms.Label();
            this.gbAmountDetails = new System.Windows.Forms.GroupBox();
            this.lblCTotalPaidAmount = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
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
            this.gbDetails.SuspendLayout();
            this.gbAmountDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbChequeDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(1, -2);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblHeader.Size = new System.Drawing.Size(1017, 37);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Payment";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(100, 18);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Supplier Name";
            // 
            // lblPaymentMode
            // 
            this.lblPaymentMode.AutoSize = true;
            this.lblPaymentMode.Location = new System.Drawing.Point(11, 103);
            this.lblPaymentMode.Name = "lblPaymentMode";
            this.lblPaymentMode.Size = new System.Drawing.Size(102, 18);
            this.lblPaymentMode.TabIndex = 3;
            this.lblPaymentMode.Text = "Payment Mode";
            // 
            // txtDetails
            // 
            this.txtDetails.Location = new System.Drawing.Point(114, 17);
            this.txtDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.Size = new System.Drawing.Size(407, 26);
            this.txtDetails.TabIndex = 1;
            this.txtDetails.TextChanged += new System.EventHandler(this.txtDetails_TextChanged);
            // 
            // cmbPaymentMode
            // 
            this.cmbPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMode.FormattingEnabled = true;
            this.cmbPaymentMode.Location = new System.Drawing.Point(115, 101);
            this.cmbPaymentMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbPaymentMode.Name = "cmbPaymentMode";
            this.cmbPaymentMode.Size = new System.Drawing.Size(153, 26);
            this.cmbPaymentMode.TabIndex = 5;
            this.cmbPaymentMode.SelectionChangeCommitted += new System.EventHandler(this.cmbPaymentMode_SelectionChangeCommitted);
            this.cmbPaymentMode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPaymentMode_KeyDown);
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(11, 49);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(88, 18);
            this.lblAmount.TabIndex = 9;
            this.lblAmount.Text = "Paid Amount";
            // 
            // lblTotalPending
            // 
            this.lblTotalPending.AutoSize = true;
            this.lblTotalPending.Location = new System.Drawing.Point(11, 76);
            this.lblTotalPending.Name = "lblTotalPending";
            this.lblTotalPending.Size = new System.Drawing.Size(66, 18);
            this.lblTotalPending.TabIndex = 10;
            this.lblTotalPending.Text = "Total Due";
            // 
            // txtPaidAmount
            // 
            this.txtPaidAmount.Location = new System.Drawing.Point(115, 45);
            this.txtPaidAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPaidAmount.Name = "txtPaidAmount";
            this.txtPaidAmount.Size = new System.Drawing.Size(153, 26);
            this.txtPaidAmount.TabIndex = 4;
            this.txtPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPaidAmount.TextChanged += new System.EventHandler(this.txtPaidAmount_TextChanged);
            this.txtPaidAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPaidAmount_KeyDown);
            this.txtPaidAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPaidAmount_KeyPress);
            // 
            // txtTotalDue
            // 
            this.txtTotalDue.BackColor = System.Drawing.Color.White;
            this.txtTotalDue.Location = new System.Drawing.Point(115, 73);
            this.txtTotalDue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotalDue.Name = "txtTotalDue";
            this.txtTotalDue.ReadOnly = true;
            this.txtTotalDue.Size = new System.Drawing.Size(153, 26);
            this.txtTotalDue.TabIndex = 12;
            this.txtTotalDue.TabStop = false;
            this.txtTotalDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblBalanceAmount
            // 
            this.lblBalanceAmount.AutoSize = true;
            this.lblBalanceAmount.Location = new System.Drawing.Point(11, 22);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(86, 18);
            this.lblBalanceAmount.TabIndex = 13;
            this.lblBalanceAmount.Text = "Due Amount";
            // 
            // txtDueAmount
            // 
            this.txtDueAmount.BackColor = System.Drawing.Color.Chartreuse;
            this.txtDueAmount.Location = new System.Drawing.Point(115, 17);
            this.txtDueAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDueAmount.Name = "txtDueAmount";
            this.txtDueAmount.ReadOnly = true;
            this.txtDueAmount.Size = new System.Drawing.Size(153, 26);
            this.txtDueAmount.TabIndex = 14;
            this.txtDueAmount.TabStop = false;
            this.txtDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(24, 41);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(37, 18);
            this.lblDate.TabIndex = 21;
            this.lblDate.Text = "Date";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(513, 397);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(91, 36);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(413, 397);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(91, 36);
            this.btnClear.TabIndex = 11;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(313, 397);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 36);
            this.btnSave.TabIndex = 10;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(613, 397);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(91, 36);
            this.btnExit.TabIndex = 13;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(128, 39);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(150, 26);
            this.dtpDate.TabIndex = 0;
            // 
            // gbDetails
            // 
            this.gbDetails.Controls.Add(this.lbDetails);
            this.gbDetails.Controls.Add(this.rtbDetails);
            this.gbDetails.Controls.Add(this.lblName);
            this.gbDetails.Controls.Add(this.txtDetails);
            this.gbDetails.Location = new System.Drawing.Point(14, 66);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(553, 132);
            this.gbDetails.TabIndex = 1;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Supplier Details";
            // 
            // lbDetails
            // 
            this.lbDetails.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDetails.FormattingEnabled = true;
            this.lbDetails.ItemHeight = 19;
            this.lbDetails.Location = new System.Drawing.Point(114, 44);
            this.lbDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbDetails.Name = "lbDetails";
            this.lbDetails.Size = new System.Drawing.Size(407, 80);
            this.lbDetails.TabIndex = 2;
            this.lbDetails.Visible = false;
            this.lbDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbDetails_KeyDown);
            this.lbDetails.Click += new System.EventHandler(this.lbDetails_Click);
            // 
            // rtbDetails
            // 
            this.rtbDetails.Location = new System.Drawing.Point(14, 46);
            this.rtbDetails.Name = "rtbDetails";
            this.rtbDetails.ReadOnly = true;
            this.rtbDetails.Size = new System.Drawing.Size(507, 80);
            this.rtbDetails.TabIndex = 5555;
            this.rtbDetails.TabStop = false;
            this.rtbDetails.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 18);
            this.label2.TabIndex = 70;
            this.label2.Text = "Naration";
            // 
            // txtNaration
            // 
            this.txtNaration.Location = new System.Drawing.Point(115, 129);
            this.txtNaration.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNaration.Multiline = true;
            this.txtNaration.Name = "txtNaration";
            this.txtNaration.Size = new System.Drawing.Size(407, 55);
            this.txtNaration.TabIndex = 6;
            this.txtNaration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNaration_KeyDown);
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(859, 618);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(123, 26);
            this.txtTotal.TabIndex = 73;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(736, 622);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 18);
            this.label3.TabIndex = 72;
            this.label3.Text = "Total Paid Amount";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(17, 621);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(78, 18);
            this.lblTotalCount.TabIndex = 74;
            this.lblTotalCount.Text = "Total Count";
            // 
            // lblCDueAmount
            // 
            this.lblCDueAmount.AutoSize = true;
            this.lblCDueAmount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCDueAmount.Location = new System.Drawing.Point(270, 21);
            this.lblCDueAmount.Name = "lblCDueAmount";
            this.lblCDueAmount.Size = new System.Drawing.Size(17, 19);
            this.lblCDueAmount.TabIndex = 11357;
            this.lblCDueAmount.Text = "C";
            // 
            // lblCPaidAmount
            // 
            this.lblCPaidAmount.AutoSize = true;
            this.lblCPaidAmount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPaidAmount.Location = new System.Drawing.Point(270, 47);
            this.lblCPaidAmount.Name = "lblCPaidAmount";
            this.lblCPaidAmount.Size = new System.Drawing.Size(17, 19);
            this.lblCPaidAmount.TabIndex = 11358;
            this.lblCPaidAmount.Text = "C";
            // 
            // lblCTotalDue
            // 
            this.lblCTotalDue.AutoSize = true;
            this.lblCTotalDue.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCTotalDue.Location = new System.Drawing.Point(270, 76);
            this.lblCTotalDue.Name = "lblCTotalDue";
            this.lblCTotalDue.Size = new System.Drawing.Size(17, 19);
            this.lblCTotalDue.TabIndex = 11359;
            this.lblCTotalDue.Text = "C";
            // 
            // gbAmountDetails
            // 
            this.gbAmountDetails.Controls.Add(this.txtNaration);
            this.gbAmountDetails.Controls.Add(this.lblCTotalDue);
            this.gbAmountDetails.Controls.Add(this.txtDueAmount);
            this.gbAmountDetails.Controls.Add(this.lblCPaidAmount);
            this.gbAmountDetails.Controls.Add(this.lblPaymentMode);
            this.gbAmountDetails.Controls.Add(this.lblCDueAmount);
            this.gbAmountDetails.Controls.Add(this.cmbPaymentMode);
            this.gbAmountDetails.Controls.Add(this.lblAmount);
            this.gbAmountDetails.Controls.Add(this.lblTotalPending);
            this.gbAmountDetails.Controls.Add(this.txtPaidAmount);
            this.gbAmountDetails.Controls.Add(this.label2);
            this.gbAmountDetails.Controls.Add(this.txtTotalDue);
            this.gbAmountDetails.Controls.Add(this.lblBalanceAmount);
            this.gbAmountDetails.Location = new System.Drawing.Point(13, 198);
            this.gbAmountDetails.Name = "gbAmountDetails";
            this.gbAmountDetails.Size = new System.Drawing.Size(554, 192);
            this.gbAmountDetails.TabIndex = 3;
            this.gbAmountDetails.TabStop = false;
            this.gbAmountDetails.Text = "Amount Details";
            this.gbAmountDetails.Visible = false;
            // 
            // lblCTotalPaidAmount
            // 
            this.lblCTotalPaidAmount.AutoSize = true;
            this.lblCTotalPaidAmount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCTotalPaidAmount.Location = new System.Drawing.Point(984, 621);
            this.lblCTotalPaidAmount.Name = "lblCTotalPaidAmount";
            this.lblCTotalPaidAmount.Size = new System.Drawing.Size(17, 19);
            this.lblCTotalPaidAmount.TabIndex = 11360;
            this.lblCTotalPaidAmount.Text = "C";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Info;
            this.dataGridView1.Location = new System.Drawing.Point(13, 439);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(993, 175);
            this.dataGridView1.TabIndex = 11361;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
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
            this.gbChequeDetails.Location = new System.Drawing.Point(573, 198);
            this.gbChequeDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbChequeDetails.Name = "gbChequeDetails";
            this.gbChequeDetails.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbChequeDetails.Size = new System.Drawing.Size(433, 191);
            this.gbChequeDetails.TabIndex = 11362;
            this.gbChequeDetails.TabStop = false;
            this.gbChequeDetails.Text = "Cheque Details";
            this.gbChequeDetails.Visible = false;
            // 
            // txtAccountNoParty
            // 
            this.txtAccountNoParty.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAccountNoParty.Location = new System.Drawing.Point(98, 158);
            this.txtAccountNoParty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAccountNoParty.Name = "txtAccountNoParty";
            this.txtAccountNoParty.Size = new System.Drawing.Size(294, 26);
            this.txtAccountNoParty.TabIndex = 76;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(8, 161);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(81, 18);
            this.label30.TabIndex = 77;
            this.label30.Text = "Account no.";
            // 
            // txtBankParty
            // 
            this.txtBankParty.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBankParty.Location = new System.Drawing.Point(98, 130);
            this.txtBankParty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBankParty.Name = "txtBankParty";
            this.txtBankParty.Size = new System.Drawing.Size(294, 26);
            this.txtBankParty.TabIndex = 74;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(8, 133);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(82, 18);
            this.label27.TabIndex = 75;
            this.label27.Text = "Party\'s Bank";
            // 
            // btnAddBankInfo
            // 
            this.btnAddBankInfo.BackColor = System.Drawing.Color.Blue;
            this.btnAddBankInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBankInfo.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddBankInfo.ForeColor = System.Drawing.Color.White;
            this.btnAddBankInfo.Location = new System.Drawing.Point(398, 21);
            this.btnAddBankInfo.Name = "btnAddBankInfo";
            this.btnAddBankInfo.Size = new System.Drawing.Size(20, 20);
            this.btnAddBankInfo.TabIndex = 73;
            this.btnAddBankInfo.Text = "+";
            this.btnAddBankInfo.UseVisualStyleBackColor = false;
            // 
            // dtpTransactionDate
            // 
            this.dtpTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTransactionDate.Location = new System.Drawing.Point(98, 74);
            this.dtpTransactionDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(294, 26);
            this.dtpTransactionDate.TabIndex = 8;
            // 
            // cmbBankName
            // 
            this.cmbBankName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBankName.FormattingEnabled = true;
            this.cmbBankName.Location = new System.Drawing.Point(98, 18);
            this.cmbBankName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbBankName.Name = "cmbBankName";
            this.cmbBankName.Size = new System.Drawing.Size(294, 26);
            this.cmbBankName.TabIndex = 7;
            this.cmbBankName.SelectionChangeCommitted += new System.EventHandler(this.cmbBankName_SelectionChangeCommitted);
            // 
            // lblChequeDate
            // 
            this.lblChequeDate.AutoSize = true;
            this.lblChequeDate.Location = new System.Drawing.Point(8, 78);
            this.lblChequeDate.Name = "lblChequeDate";
            this.lblChequeDate.Size = new System.Drawing.Size(53, 18);
            this.lblChequeDate.TabIndex = 30;
            this.lblChequeDate.Text = "Tr. Date";
            // 
            // txtChequeNo
            // 
            this.txtChequeNo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtChequeNo.Location = new System.Drawing.Point(98, 102);
            this.txtChequeNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Size = new System.Drawing.Size(294, 26);
            this.txtChequeNo.TabIndex = 9;
            // 
            // lblChequeNo
            // 
            this.lblChequeNo.AutoSize = true;
            this.lblChequeNo.Location = new System.Drawing.Point(8, 105);
            this.lblChequeNo.Name = "lblChequeNo";
            this.lblChequeNo.Size = new System.Drawing.Size(81, 18);
            this.lblChequeNo.TabIndex = 28;
            this.lblChequeNo.Text = "Cheque No.";
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.BackColor = System.Drawing.Color.White;
            this.txtAccountNo.Location = new System.Drawing.Point(98, 46);
            this.txtAccountNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.ReadOnly = true;
            this.txtAccountNo.Size = new System.Drawing.Size(294, 26);
            this.txtAccountNo.TabIndex = 27;
            this.txtAccountNo.TabStop = false;
            // 
            // lblAccountNo
            // 
            this.lblAccountNo.AutoSize = true;
            this.lblAccountNo.Location = new System.Drawing.Point(8, 51);
            this.lblAccountNo.Name = "lblAccountNo";
            this.lblAccountNo.Size = new System.Drawing.Size(83, 18);
            this.lblAccountNo.TabIndex = 26;
            this.lblAccountNo.Text = "Account No.";
            // 
            // lblBankName
            // 
            this.lblBankName.AutoSize = true;
            this.lblBankName.Location = new System.Drawing.Point(8, 24);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(78, 18);
            this.lblBankName.TabIndex = 24;
            this.lblBankName.Text = "Bank Name";
            // 
            // Payment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1017, 650);
            this.ControlBox = false;
            this.Controls.Add(this.gbChequeDetails);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblCTotalPaidAmount);
            this.Controls.Add(this.gbAmountDetails);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Payment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Payment_Load);
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.gbAmountDetails.ResumeLayout(false);
            this.gbAmountDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbChequeDetails.ResumeLayout(false);
            this.gbChequeDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPaymentMode;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.ComboBox cmbPaymentMode;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblTotalPending;
        private System.Windows.Forms.TextBox txtPaidAmount;
        private System.Windows.Forms.TextBox txtTotalDue;
        private System.Windows.Forms.Label lblBalanceAmount;
        private System.Windows.Forms.TextBox txtDueAmount;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNaration;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.RichTextBox rtbDetails;
        private System.Windows.Forms.Label lblCDueAmount;
        private System.Windows.Forms.Label lblCPaidAmount;
        private System.Windows.Forms.Label lblCTotalDue;
        private System.Windows.Forms.ListBox lbDetails;
        private System.Windows.Forms.GroupBox gbAmountDetails;
        private System.Windows.Forms.Label lblCTotalPaidAmount;
        private System.Windows.Forms.DataGridView dataGridView1;
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
    }
}