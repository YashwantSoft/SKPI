namespace TestApplication
{
    partial class BillDetails
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
            this.dgvBill = new System.Windows.Forms.DataGridView();
            this.cmbSupplierName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblBillNum = new System.Windows.Forms.Label();
            this.txtBillNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.tcBillDetails = new System.Windows.Forms.TabControl();
            this.tpAddBill = new System.Windows.Forms.TabPage();
            this.btnDeleteBill = new System.Windows.Forms.Button();
            this.btnSaveBill = new System.Windows.Forms.Button();
            this.btnNewBill = new System.Windows.Forms.Button();
            this.txtCurrentBillAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tpPaymentDetails = new System.Windows.Forms.TabPage();
            this.btnDeletePayment = new System.Windows.Forms.Button();
            this.btnSavePayment = new System.Windows.Forms.Button();
            this.btnNewPayment = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.gbChequeDetails = new System.Windows.Forms.GroupBox();
            this.txtChequeNumber = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpChequeDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNetPendingAmount = new System.Windows.Forms.TextBox();
            this.rbCheque = new System.Windows.Forms.RadioButton();
            this.rbCash = new System.Windows.Forms.RadioButton();
            this.txtReceivedBy = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvPayment = new System.Windows.Forms.DataGridView();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCurrentPaidAmount = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtTotalPurchaseAmount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalPaidAmount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbUnFix = new System.Windows.Forms.RadioButton();
            this.rbFix = new System.Windows.Forms.RadioButton();
            this.btnNewSupplier = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBill)).BeginInit();
            this.tcBillDetails.SuspendLayout();
            this.tpAddBill.SuspendLayout();
            this.tpPaymentDetails.SuspendLayout();
            this.gbChequeDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayment)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblHeader.BackColor = System.Drawing.Color.Blue;
            this.lblHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(2, 0);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(588, 38);
            this.lblHeader.TabIndex = 33;
            this.lblHeader.Text = "Supplier Bill and Payment";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvBill
            // 
            this.dgvBill.AllowUserToAddRows = false;
            this.dgvBill.BackgroundColor = System.Drawing.Color.White;
            this.dgvBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBill.Location = new System.Drawing.Point(6, 119);
            this.dgvBill.Name = "dgvBill";
            this.dgvBill.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBill.Size = new System.Drawing.Size(549, 283);
            this.dgvBill.TabIndex = 52;
            this.dgvBill.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBill_CellDoubleClick);
            // 
            // cmbSupplierName
            // 
            this.cmbSupplierName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplierName.FormattingEnabled = true;
            this.cmbSupplierName.Location = new System.Drawing.Point(161, 67);
            this.cmbSupplierName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbSupplierName.Name = "cmbSupplierName";
            this.cmbSupplierName.Size = new System.Drawing.Size(333, 23);
            this.cmbSupplierName.TabIndex = 3;
            this.cmbSupplierName.SelectionChangeCommitted += new System.EventHandler(this.cmbSupplierName_SelectionChangeCommitted);
            this.cmbSupplierName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSupplierName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 14);
            this.label1.TabIndex = 56;
            this.label1.Text = "Select Supplier Name";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(340, 42);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(153, 23);
            this.dtpDate.TabIndex = 2;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(306, 46);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(37, 14);
            this.lblDate.TabIndex = 55;
            this.lblDate.Text = "Date";
            // 
            // lblBillNum
            // 
            this.lblBillNum.AutoSize = true;
            this.lblBillNum.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillNum.Location = new System.Drawing.Point(74, 10);
            this.lblBillNum.Name = "lblBillNum";
            this.lblBillNum.Size = new System.Drawing.Size(77, 14);
            this.lblBillNum.TabIndex = 60;
            this.lblBillNum.Text = "Bill Number";
            // 
            // txtBillNumber
            // 
            this.txtBillNumber.BackColor = System.Drawing.Color.White;
            this.txtBillNumber.Location = new System.Drawing.Point(242, 6);
            this.txtBillNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBillNumber.Name = "txtBillNumber";
            this.txtBillNumber.Size = new System.Drawing.Size(153, 23);
            this.txtBillNumber.TabIndex = 1;
            this.txtBillNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillNumber_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 14);
            this.label2.TabIndex = 64;
            this.label2.Text = "Total Amount";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.Color.White;
            this.txtTotalAmount.Location = new System.Drawing.Point(242, 54);
            this.txtTotalAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(153, 23);
            this.txtTotalAmount.TabIndex = 4;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tcBillDetails
            // 
            this.tcBillDetails.Controls.Add(this.tpAddBill);
            this.tcBillDetails.Controls.Add(this.tpPaymentDetails);
            this.tcBillDetails.Location = new System.Drawing.Point(11, 189);
            this.tcBillDetails.Name = "tcBillDetails";
            this.tcBillDetails.SelectedIndex = 0;
            this.tcBillDetails.Size = new System.Drawing.Size(567, 436);
            this.tcBillDetails.TabIndex = 1;
            this.tcBillDetails.SelectedIndexChanged += new System.EventHandler(this.tcBillDetails_SelectedIndexChanged);
            // 
            // tpAddBill
            // 
            this.tpAddBill.BackColor = System.Drawing.Color.White;
            this.tpAddBill.Controls.Add(this.btnDeleteBill);
            this.tpAddBill.Controls.Add(this.btnSaveBill);
            this.tpAddBill.Controls.Add(this.btnNewBill);
            this.tpAddBill.Controls.Add(this.txtCurrentBillAmount);
            this.tpAddBill.Controls.Add(this.label5);
            this.tpAddBill.Controls.Add(this.txtTotalAmount);
            this.tpAddBill.Controls.Add(this.txtBillNumber);
            this.tpAddBill.Controls.Add(this.lblBillNum);
            this.tpAddBill.Controls.Add(this.label2);
            this.tpAddBill.Controls.Add(this.dgvBill);
            this.tpAddBill.Location = new System.Drawing.Point(4, 24);
            this.tpAddBill.Name = "tpAddBill";
            this.tpAddBill.Padding = new System.Windows.Forms.Padding(3);
            this.tpAddBill.Size = new System.Drawing.Size(559, 408);
            this.tpAddBill.TabIndex = 0;
            this.tpAddBill.Text = "Bill";
            // 
            // btnDeleteBill
            // 
            this.btnDeleteBill.BackColor = System.Drawing.Color.Blue;
            this.btnDeleteBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteBill.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteBill.ForeColor = System.Drawing.Color.White;
            this.btnDeleteBill.Location = new System.Drawing.Point(315, 83);
            this.btnDeleteBill.Name = "btnDeleteBill";
            this.btnDeleteBill.Size = new System.Drawing.Size(80, 30);
            this.btnDeleteBill.TabIndex = 95;
            this.btnDeleteBill.Text = "&Delete";
            this.btnDeleteBill.UseVisualStyleBackColor = false;
            this.btnDeleteBill.Click += new System.EventHandler(this.btnDeleteBill_Click);
            // 
            // btnSaveBill
            // 
            this.btnSaveBill.BackColor = System.Drawing.Color.Blue;
            this.btnSaveBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveBill.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveBill.ForeColor = System.Drawing.Color.White;
            this.btnSaveBill.Location = new System.Drawing.Point(231, 83);
            this.btnSaveBill.Name = "btnSaveBill";
            this.btnSaveBill.Size = new System.Drawing.Size(80, 30);
            this.btnSaveBill.TabIndex = 94;
            this.btnSaveBill.Text = "&Save";
            this.btnSaveBill.UseVisualStyleBackColor = false;
            this.btnSaveBill.Click += new System.EventHandler(this.btnSaveBill_Click);
            // 
            // btnNewBill
            // 
            this.btnNewBill.BackColor = System.Drawing.Color.Blue;
            this.btnNewBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewBill.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewBill.ForeColor = System.Drawing.Color.White;
            this.btnNewBill.Location = new System.Drawing.Point(147, 83);
            this.btnNewBill.Name = "btnNewBill";
            this.btnNewBill.Size = new System.Drawing.Size(80, 30);
            this.btnNewBill.TabIndex = 93;
            this.btnNewBill.Text = "&New Bill";
            this.btnNewBill.UseVisualStyleBackColor = false;
            this.btnNewBill.Click += new System.EventHandler(this.btnNewBill_Click);
            // 
            // txtCurrentBillAmount
            // 
            this.txtCurrentBillAmount.BackColor = System.Drawing.Color.White;
            this.txtCurrentBillAmount.Location = new System.Drawing.Point(242, 30);
            this.txtCurrentBillAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCurrentBillAmount.Name = "txtCurrentBillAmount";
            this.txtCurrentBillAmount.Size = new System.Drawing.Size(153, 23);
            this.txtCurrentBillAmount.TabIndex = 2;
            this.txtCurrentBillAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCurrentBillAmount.TextChanged += new System.EventHandler(this.txtCurrentBillAmount_TextChanged);
            this.txtCurrentBillAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrentBillAmount_KeyDown);
            this.txtCurrentBillAmount.Leave += new System.EventHandler(this.txtCurrentBillAmount_Leave);
            this.txtCurrentBillAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCurrentBillAmount_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(74, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(166, 14);
            this.label5.TabIndex = 66;
            this.label5.Text = "Enter Current Bill Amount";
            // 
            // tpPaymentDetails
            // 
            this.tpPaymentDetails.BackColor = System.Drawing.Color.White;
            this.tpPaymentDetails.Controls.Add(this.btnDeletePayment);
            this.tpPaymentDetails.Controls.Add(this.btnSavePayment);
            this.tpPaymentDetails.Controls.Add(this.btnNewPayment);
            this.tpPaymentDetails.Controls.Add(this.label15);
            this.tpPaymentDetails.Controls.Add(this.gbChequeDetails);
            this.tpPaymentDetails.Controls.Add(this.label11);
            this.tpPaymentDetails.Controls.Add(this.txtNetPendingAmount);
            this.tpPaymentDetails.Controls.Add(this.rbCheque);
            this.tpPaymentDetails.Controls.Add(this.rbCash);
            this.tpPaymentDetails.Controls.Add(this.txtReceivedBy);
            this.tpPaymentDetails.Controls.Add(this.label4);
            this.tpPaymentDetails.Controls.Add(this.dgvPayment);
            this.tpPaymentDetails.Controls.Add(this.txtRemarks);
            this.tpPaymentDetails.Controls.Add(this.label7);
            this.tpPaymentDetails.Controls.Add(this.label8);
            this.tpPaymentDetails.Controls.Add(this.txtCurrentPaidAmount);
            this.tpPaymentDetails.Location = new System.Drawing.Point(4, 24);
            this.tpPaymentDetails.Name = "tpPaymentDetails";
            this.tpPaymentDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tpPaymentDetails.Size = new System.Drawing.Size(559, 408);
            this.tpPaymentDetails.TabIndex = 1;
            this.tpPaymentDetails.Text = "Payment";
            // 
            // btnDeletePayment
            // 
            this.btnDeletePayment.BackColor = System.Drawing.Color.Blue;
            this.btnDeletePayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePayment.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeletePayment.ForeColor = System.Drawing.Color.White;
            this.btnDeletePayment.Location = new System.Drawing.Point(332, 212);
            this.btnDeletePayment.Name = "btnDeletePayment";
            this.btnDeletePayment.Size = new System.Drawing.Size(80, 30);
            this.btnDeletePayment.TabIndex = 98;
            this.btnDeletePayment.Text = "&Delete";
            this.btnDeletePayment.UseVisualStyleBackColor = false;
            this.btnDeletePayment.Click += new System.EventHandler(this.btnDeletePayment_Click);
            // 
            // btnSavePayment
            // 
            this.btnSavePayment.BackColor = System.Drawing.Color.Blue;
            this.btnSavePayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePayment.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSavePayment.ForeColor = System.Drawing.Color.White;
            this.btnSavePayment.Location = new System.Drawing.Point(248, 212);
            this.btnSavePayment.Name = "btnSavePayment";
            this.btnSavePayment.Size = new System.Drawing.Size(80, 30);
            this.btnSavePayment.TabIndex = 97;
            this.btnSavePayment.Text = "&Save";
            this.btnSavePayment.UseVisualStyleBackColor = false;
            this.btnSavePayment.Click += new System.EventHandler(this.btnSavePayment_Click);
            // 
            // btnNewPayment
            // 
            this.btnNewPayment.BackColor = System.Drawing.Color.Blue;
            this.btnNewPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewPayment.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewPayment.ForeColor = System.Drawing.Color.White;
            this.btnNewPayment.Location = new System.Drawing.Point(164, 212);
            this.btnNewPayment.Name = "btnNewPayment";
            this.btnNewPayment.Size = new System.Drawing.Size(80, 30);
            this.btnNewPayment.TabIndex = 96;
            this.btnNewPayment.Text = "&New";
            this.btnNewPayment.UseVisualStyleBackColor = false;
            this.btnNewPayment.Click += new System.EventHandler(this.btnNewPayment_Click_1);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(82, 8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(136, 14);
            this.label15.TabIndex = 93;
            this.label15.Text = "Net Pending Amount";
            // 
            // gbChequeDetails
            // 
            this.gbChequeDetails.Controls.Add(this.txtChequeNumber);
            this.gbChequeDetails.Controls.Add(this.label14);
            this.gbChequeDetails.Controls.Add(this.label13);
            this.gbChequeDetails.Controls.Add(this.dtpChequeDate);
            this.gbChequeDetails.Controls.Add(this.label12);
            this.gbChequeDetails.Controls.Add(this.txtBankName);
            this.gbChequeDetails.Enabled = false;
            this.gbChequeDetails.Location = new System.Drawing.Point(85, 119);
            this.gbChequeDetails.Name = "gbChequeDetails";
            this.gbChequeDetails.Size = new System.Drawing.Size(458, 89);
            this.gbChequeDetails.TabIndex = 5;
            this.gbChequeDetails.TabStop = false;
            this.gbChequeDetails.Text = "Cheque Details";
            // 
            // txtChequeNumber
            // 
            this.txtChequeNumber.Location = new System.Drawing.Point(135, 62);
            this.txtChequeNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtChequeNumber.Name = "txtChequeNumber";
            this.txtChequeNumber.Size = new System.Drawing.Size(151, 23);
            this.txtChequeNumber.TabIndex = 8;
            this.txtChequeNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChequeNumber_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(3, 67);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(109, 14);
            this.label14.TabIndex = 91;
            this.label14.Text = "Cheque Number";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(3, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 14);
            this.label13.TabIndex = 86;
            this.label13.Text = "Cheque Date";
            // 
            // dtpChequeDate
            // 
            this.dtpChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChequeDate.Location = new System.Drawing.Point(135, 38);
            this.dtpChequeDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpChequeDate.Name = "dtpChequeDate";
            this.dtpChequeDate.Size = new System.Drawing.Size(151, 23);
            this.dtpChequeDate.TabIndex = 7;
            this.dtpChequeDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpChequeDate_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 14);
            this.label12.TabIndex = 88;
            this.label12.Text = "Bank Name";
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(135, 14);
            this.txtBankName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(243, 23);
            this.txtBankName.TabIndex = 6;
            this.txtBankName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBankName_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(82, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 14);
            this.label11.TabIndex = 85;
            this.label11.Text = "Payment Mode";
            // 
            // txtNetPendingAmount
            // 
            this.txtNetPendingAmount.Enabled = false;
            this.txtNetPendingAmount.Location = new System.Drawing.Point(220, 5);
            this.txtNetPendingAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNetPendingAmount.Name = "txtNetPendingAmount";
            this.txtNetPendingAmount.Size = new System.Drawing.Size(153, 23);
            this.txtNetPendingAmount.TabIndex = 0;
            this.txtNetPendingAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // rbCheque
            // 
            this.rbCheque.AutoSize = true;
            this.rbCheque.Location = new System.Drawing.Point(280, 98);
            this.rbCheque.Name = "rbCheque";
            this.rbCheque.Size = new System.Drawing.Size(65, 19);
            this.rbCheque.TabIndex = 4;
            this.rbCheque.TabStop = true;
            this.rbCheque.Text = "Cheque";
            this.rbCheque.UseVisualStyleBackColor = true;
            this.rbCheque.CheckedChanged += new System.EventHandler(this.rbCheque_CheckedChanged);
            this.rbCheque.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbCheque_KeyDown);
            // 
            // rbCash
            // 
            this.rbCash.AutoSize = true;
            this.rbCash.Location = new System.Drawing.Point(224, 98);
            this.rbCash.Name = "rbCash";
            this.rbCash.Size = new System.Drawing.Size(52, 19);
            this.rbCash.TabIndex = 3;
            this.rbCash.TabStop = true;
            this.rbCash.Text = "Cash";
            this.rbCash.UseVisualStyleBackColor = true;
            this.rbCash.CheckedChanged += new System.EventHandler(this.rbCash_CheckedChanged);
            this.rbCash.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbCash_KeyDown);
            // 
            // txtReceivedBy
            // 
            this.txtReceivedBy.Location = new System.Drawing.Point(220, 51);
            this.txtReceivedBy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtReceivedBy.Name = "txtReceivedBy";
            this.txtReceivedBy.Size = new System.Drawing.Size(245, 23);
            this.txtReceivedBy.TabIndex = 1;
            this.txtReceivedBy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReceivedBy_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(82, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 14);
            this.label4.TabIndex = 81;
            this.label4.Text = "Received By";
            // 
            // dgvPayment
            // 
            this.dgvPayment.AllowUserToAddRows = false;
            this.dgvPayment.BackgroundColor = System.Drawing.Color.White;
            this.dgvPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayment.Location = new System.Drawing.Point(6, 246);
            this.dgvPayment.Name = "dgvPayment";
            this.dgvPayment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPayment.Size = new System.Drawing.Size(549, 156);
            this.dgvPayment.TabIndex = 73;
            this.dgvPayment.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPayment_CellDoubleClick);
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(220, 73);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(245, 23);
            this.txtRemarks.TabIndex = 2;
            this.txtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemarks_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(82, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 14);
            this.label7.TabIndex = 69;
            this.label7.Text = "Paid Amount";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(82, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 14);
            this.label8.TabIndex = 71;
            this.label8.Text = "Naration";
            // 
            // txtCurrentPaidAmount
            // 
            this.txtCurrentPaidAmount.Location = new System.Drawing.Point(220, 27);
            this.txtCurrentPaidAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCurrentPaidAmount.Name = "txtCurrentPaidAmount";
            this.txtCurrentPaidAmount.Size = new System.Drawing.Size(153, 23);
            this.txtCurrentPaidAmount.TabIndex = 0;
            this.txtCurrentPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCurrentPaidAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrentPaidAmount_KeyDown);
            this.txtCurrentPaidAmount.Leave += new System.EventHandler(this.txtCurrentPaidAmount_Leave);
            this.txtCurrentPaidAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCurrentPaidAmount_KeyPress);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Blue;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(424, 153);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 28);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Blue;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(350, 153);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtTotalPurchaseAmount
            // 
            this.txtTotalPurchaseAmount.BackColor = System.Drawing.Color.White;
            this.txtTotalPurchaseAmount.Location = new System.Drawing.Point(161, 133);
            this.txtTotalPurchaseAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotalPurchaseAmount.Name = "txtTotalPurchaseAmount";
            this.txtTotalPurchaseAmount.ReadOnly = true;
            this.txtTotalPurchaseAmount.Size = new System.Drawing.Size(155, 23);
            this.txtTotalPurchaseAmount.TabIndex = 68;
            this.txtTotalPurchaseAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalPurchaseAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPendingAmount_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(151, 14);
            this.label9.TabIndex = 69;
            this.label9.Text = "Total Purchase Amount";
            // 
            // txtTotalPaidAmount
            // 
            this.txtTotalPaidAmount.BackColor = System.Drawing.Color.White;
            this.txtTotalPaidAmount.Location = new System.Drawing.Point(161, 157);
            this.txtTotalPaidAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotalPaidAmount.Name = "txtTotalPaidAmount";
            this.txtTotalPaidAmount.ReadOnly = true;
            this.txtTotalPaidAmount.Size = new System.Drawing.Size(155, 23);
            this.txtTotalPaidAmount.TabIndex = 80;
            this.txtTotalPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(8, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 14);
            this.label10.TabIndex = 81;
            this.label10.Text = "Total Paid Amount";
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.White;
            this.txtAddress.Location = new System.Drawing.Point(161, 91);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(333, 41);
            this.txtAddress.TabIndex = 82;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 14);
            this.label3.TabIndex = 83;
            this.label3.Text = "Address";
            // 
            // rbUnFix
            // 
            this.rbUnFix.AutoSize = true;
            this.rbUnFix.Location = new System.Drawing.Point(206, 42);
            this.rbUnFix.Name = "rbUnFix";
            this.rbUnFix.Size = new System.Drawing.Size(56, 19);
            this.rbUnFix.TabIndex = 1;
            this.rbUnFix.Text = "UnFix";
            this.rbUnFix.UseVisualStyleBackColor = true;
            this.rbUnFix.Visible = false;
            this.rbUnFix.CheckedChanged += new System.EventHandler(this.rbUnFix_CheckedChanged);
            this.rbUnFix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbUnFix_KeyDown);
            // 
            // rbFix
            // 
            this.rbFix.AutoSize = true;
            this.rbFix.Checked = true;
            this.rbFix.Location = new System.Drawing.Point(164, 42);
            this.rbFix.Name = "rbFix";
            this.rbFix.Size = new System.Drawing.Size(41, 19);
            this.rbFix.TabIndex = 0;
            this.rbFix.TabStop = true;
            this.rbFix.Text = "Fix";
            this.rbFix.UseVisualStyleBackColor = true;
            this.rbFix.Visible = false;
            this.rbFix.CheckedChanged += new System.EventHandler(this.rbFix_CheckedChanged);
            this.rbFix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbFix_KeyDown);
            // 
            // btnNewSupplier
            // 
            this.btnNewSupplier.BackColor = System.Drawing.Color.Blue;
            this.btnNewSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewSupplier.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewSupplier.ForeColor = System.Drawing.Color.White;
            this.btnNewSupplier.Location = new System.Drawing.Point(549, 65);
            this.btnNewSupplier.Name = "btnNewSupplier";
            this.btnNewSupplier.Size = new System.Drawing.Size(24, 23);
            this.btnNewSupplier.TabIndex = 4;
            this.btnNewSupplier.Text = "+";
            this.btnNewSupplier.UseVisualStyleBackColor = false;
            // 
            // BillDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(588, 631);
            this.ControlBox = false;
            this.Controls.Add(this.btnNewSupplier);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.rbUnFix);
            this.Controls.Add(this.rbFix);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTotalPaidAmount);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtTotalPurchaseAmount);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tcBillDetails);
            this.Controls.Add(this.cmbSupplierName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BillDetails";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.BillDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBill)).EndInit();
            this.tcBillDetails.ResumeLayout(false);
            this.tpAddBill.ResumeLayout(false);
            this.tpAddBill.PerformLayout();
            this.tpPaymentDetails.ResumeLayout(false);
            this.tpPaymentDetails.PerformLayout();
            this.gbChequeDetails.ResumeLayout(false);
            this.gbChequeDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.DataGridView dgvBill;
        private System.Windows.Forms.ComboBox cmbSupplierName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblBillNum;
        private System.Windows.Forms.TextBox txtBillNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.TabControl tcBillDetails;
        private System.Windows.Forms.TabPage tpAddBill;
        private System.Windows.Forms.TabPage tpPaymentDetails;
        private System.Windows.Forms.TextBox txtCurrentBillAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCurrentPaidAmount;
        private System.Windows.Forms.DataGridView dgvPayment;
        private System.Windows.Forms.TextBox txtTotalPurchaseAmount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTotalPaidAmount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReceivedBy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbCheque;
        private System.Windows.Forms.RadioButton rbCash;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtChequeNumber;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtpChequeDate;
        private System.Windows.Forms.GroupBox gbChequeDetails;
        private System.Windows.Forms.RadioButton rbUnFix;
        private System.Windows.Forms.RadioButton rbFix;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtNetPendingAmount;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveBill;
        private System.Windows.Forms.Button btnNewBill;
        private System.Windows.Forms.Button btnDeleteBill;
        private System.Windows.Forms.Button btnDeletePayment;
        private System.Windows.Forms.Button btnSavePayment;
        private System.Windows.Forms.Button btnNewPayment;
        private System.Windows.Forms.Button btnNewSupplier;
    }
}