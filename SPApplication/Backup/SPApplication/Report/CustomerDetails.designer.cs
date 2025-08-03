namespace SPApplication.Report
{
    partial class CustomerDetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbToday = new System.Windows.Forms.CheckBox();
            this.cmbBillNo = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReceiptTotal = new System.Windows.Forms.TextBox();
            this.lblTotalCountReceipt = new System.Windows.Forms.Label();
            this.dgvReceipt = new System.Windows.Forms.DataGridView();
            this.clmSrNoReceipt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDateReceipt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmReceipt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPaymentType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAmountReceipt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblBalance = new System.Windows.Forms.Label();
            this.txtSaleTotal = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.dgvSale = new System.Windows.Forms.DataGridView();
            this.clmSrNoSale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDateSale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPaymentTypeSale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAmountSale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSaleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPendingAmount = new System.Windows.Forms.TextBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.lblEmailId = new System.Windows.Forms.Label();
            this.cbEmail = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSale)).BeginInit();
            this.SuspendLayout();
            // 
            // cbToday
            // 
            this.cbToday.AutoSize = true;
            this.cbToday.Location = new System.Drawing.Point(746, 29);
            this.cbToday.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbToday.Name = "cbToday";
            this.cbToday.Size = new System.Drawing.Size(58, 19);
            this.cbToday.TabIndex = 2;
            this.cbToday.Text = "Today";
            this.cbToday.UseVisualStyleBackColor = true;
            this.cbToday.CheckedChanged += new System.EventHandler(this.cbDate_CheckedChanged);
            // 
            // cmbBillNo
            // 
            this.cmbBillNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBillNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBillNo.FormattingEnabled = true;
            this.cmbBillNo.Location = new System.Drawing.Point(465, 52);
            this.cmbBillNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbBillNo.Name = "cmbBillNo";
            this.cmbBillNo.Size = new System.Drawing.Size(269, 23);
            this.cmbBillNo.TabIndex = 3;
            this.cmbBillNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBillNo_KeyDown);
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(400, 52);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(44, 15);
            this.lblSupplier.TabIndex = 11381;
            this.lblSupplier.Text = "Bill No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1023, 455);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 15);
            this.label6.TabIndex = 11380;
            this.label6.Text = "Total Receipts";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(423, 454);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 11379;
            this.label3.Text = "Total Sale";
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(661, 79);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 30);
            this.btnExit.TabIndex = 7;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnView
            // 
            this.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnView.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(482, 79);
            this.btnView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(80, 30);
            this.btnView.TabIndex = 5;
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(746, 54);
            this.cbSelectAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(76, 19);
            this.cbSelectAll.TabIndex = 4;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(571, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 11366;
            this.label2.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(628, 27);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(106, 23);
            this.dtpToDate.TabIndex = 1;
            this.dtpToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpToDate_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(400, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 11365;
            this.label1.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(465, 27);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(106, 23);
            this.dtpFromDate.TabIndex = 0;
            this.dtpFromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpFromDate_KeyDown);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(572, 79);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 6;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Blue;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1225, 24);
            this.lblHeader.TabIndex = 11362;
            this.lblHeader.Text = "Customer Details";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(256, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 11378;
            this.label5.Text = "Sale Details";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(876, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 15);
            this.label4.TabIndex = 11377;
            this.label4.Text = "Receipt Details";
            // 
            // txtReceiptTotal
            // 
            this.txtReceiptTotal.BackColor = System.Drawing.Color.Chartreuse;
            this.txtReceiptTotal.Location = new System.Drawing.Point(1108, 451);
            this.txtReceiptTotal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtReceiptTotal.Name = "txtReceiptTotal";
            this.txtReceiptTotal.ReadOnly = true;
            this.txtReceiptTotal.Size = new System.Drawing.Size(103, 23);
            this.txtReceiptTotal.TabIndex = 11376;
            this.txtReceiptTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalCountReceipt
            // 
            this.lblTotalCountReceipt.AutoSize = true;
            this.lblTotalCountReceipt.BackColor = System.Drawing.Color.White;
            this.lblTotalCountReceipt.Location = new System.Drawing.Point(616, 454);
            this.lblTotalCountReceipt.Name = "lblTotalCountReceipt";
            this.lblTotalCountReceipt.Size = new System.Drawing.Size(69, 15);
            this.lblTotalCountReceipt.TabIndex = 11375;
            this.lblTotalCountReceipt.Text = "Total Count";
            // 
            // dgvReceipt
            // 
            this.dgvReceipt.AllowUserToAddRows = false;
            this.dgvReceipt.AllowUserToDeleteRows = false;
            this.dgvReceipt.AllowUserToResizeRows = false;
            this.dgvReceipt.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceipt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSrNoReceipt,
            this.clmDateReceipt,
            this.clmReceipt,
            this.clmPaymentType,
            this.clmAmountReceipt});
            this.dgvReceipt.Location = new System.Drawing.Point(612, 113);
            this.dgvReceipt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvReceipt.Name = "dgvReceipt";
            this.dgvReceipt.RowHeadersVisible = false;
            this.dgvReceipt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReceipt.Size = new System.Drawing.Size(600, 335);
            this.dgvReceipt.TabIndex = 9;
            // 
            // clmSrNoReceipt
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmSrNoReceipt.DefaultCellStyle = dataGridViewCellStyle1;
            this.clmSrNoReceipt.HeaderText = "Sr.No.";
            this.clmSrNoReceipt.Name = "clmSrNoReceipt";
            this.clmSrNoReceipt.ReadOnly = true;
            this.clmSrNoReceipt.Width = 50;
            // 
            // clmDateReceipt
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.clmDateReceipt.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmDateReceipt.HeaderText = "Date";
            this.clmDateReceipt.Name = "clmDateReceipt";
            this.clmDateReceipt.ReadOnly = true;
            // 
            // clmReceipt
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.clmReceipt.DefaultCellStyle = dataGridViewCellStyle3;
            this.clmReceipt.HeaderText = "Particulars";
            this.clmReceipt.Name = "clmReceipt";
            this.clmReceipt.ReadOnly = true;
            this.clmReceipt.Width = 220;
            // 
            // clmPaymentType
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmPaymentType.DefaultCellStyle = dataGridViewCellStyle4;
            this.clmPaymentType.HeaderText = "Payment Mode";
            this.clmPaymentType.Name = "clmPaymentType";
            this.clmPaymentType.ReadOnly = true;
            this.clmPaymentType.Width = 120;
            // 
            // clmAmountReceipt
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clmAmountReceipt.DefaultCellStyle = dataGridViewCellStyle5;
            this.clmAmountReceipt.HeaderText = "Amount";
            this.clmAmountReceipt.Name = "clmAmountReceipt";
            this.clmAmountReceipt.ReadOnly = true;
            this.clmAmountReceipt.Width = 80;
            // 
            // lblBalance
            // 
            this.lblBalance.BackColor = System.Drawing.Color.White;
            this.lblBalance.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.Location = new System.Drawing.Point(12, 87);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(181, 19);
            this.lblBalance.TabIndex = 11384;
            this.lblBalance.Text = "Opening Balance";
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSaleTotal
            // 
            this.txtSaleTotal.BackColor = System.Drawing.Color.OrangeRed;
            this.txtSaleTotal.Location = new System.Drawing.Point(485, 450);
            this.txtSaleTotal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSaleTotal.Name = "txtSaleTotal";
            this.txtSaleTotal.ReadOnly = true;
            this.txtSaleTotal.Size = new System.Drawing.Size(95, 23);
            this.txtSaleTotal.TabIndex = 11373;
            this.txtSaleTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BackColor = System.Drawing.Color.White;
            this.lblCount.Location = new System.Drawing.Point(9, 455);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(69, 15);
            this.lblCount.TabIndex = 11372;
            this.lblCount.Text = "Total Count";
            // 
            // dgvSale
            // 
            this.dgvSale.AllowUserToAddRows = false;
            this.dgvSale.AllowUserToDeleteRows = false;
            this.dgvSale.AllowUserToResizeRows = false;
            this.dgvSale.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvSale.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSale.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSrNoSale,
            this.clmDateSale,
            this.clmDetails,
            this.clmPaymentTypeSale,
            this.clmAmountSale,
            this.clmSaleId});
            this.dgvSale.Location = new System.Drawing.Point(8, 114);
            this.dgvSale.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvSale.Name = "dgvSale";
            this.dgvSale.RowHeadersVisible = false;
            this.dgvSale.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSale.Size = new System.Drawing.Size(600, 335);
            this.dgvSale.TabIndex = 8;
            // 
            // clmSrNoSale
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmSrNoSale.DefaultCellStyle = dataGridViewCellStyle6;
            this.clmSrNoSale.HeaderText = "Sr.No.";
            this.clmSrNoSale.Name = "clmSrNoSale";
            this.clmSrNoSale.ReadOnly = true;
            this.clmSrNoSale.Width = 50;
            // 
            // clmDateSale
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.clmDateSale.DefaultCellStyle = dataGridViewCellStyle7;
            this.clmDateSale.HeaderText = "Date";
            this.clmDateSale.Name = "clmDateSale";
            this.clmDateSale.ReadOnly = true;
            // 
            // clmDetails
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.clmDetails.DefaultCellStyle = dataGridViewCellStyle8;
            this.clmDetails.HeaderText = "Particulars";
            this.clmDetails.Name = "clmDetails";
            this.clmDetails.ReadOnly = true;
            this.clmDetails.Width = 220;
            // 
            // clmPaymentTypeSale
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmPaymentTypeSale.DefaultCellStyle = dataGridViewCellStyle9;
            this.clmPaymentTypeSale.HeaderText = "Payment Type";
            this.clmPaymentTypeSale.Name = "clmPaymentTypeSale";
            this.clmPaymentTypeSale.ReadOnly = true;
            this.clmPaymentTypeSale.Width = 120;
            // 
            // clmAmountSale
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clmAmountSale.DefaultCellStyle = dataGridViewCellStyle10;
            this.clmAmountSale.HeaderText = "Amount";
            this.clmAmountSale.Name = "clmAmountSale";
            this.clmAmountSale.ReadOnly = true;
            this.clmAmountSale.Width = 80;
            // 
            // clmSaleId
            // 
            this.clmSaleId.HeaderText = "SaleId";
            this.clmSaleId.Name = "clmSaleId";
            this.clmSaleId.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1010, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 15);
            this.label7.TabIndex = 11386;
            this.label7.Text = "Pending Amount";
            // 
            // txtPendingAmount
            // 
            this.txtPendingAmount.BackColor = System.Drawing.Color.Cyan;
            this.txtPendingAmount.Location = new System.Drawing.Point(1108, 27);
            this.txtPendingAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPendingAmount.Name = "txtPendingAmount";
            this.txtPendingAmount.ReadOnly = true;
            this.txtPendingAmount.Size = new System.Drawing.Size(103, 23);
            this.txtPendingAmount.TabIndex = 11385;
            this.txtPendingAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnReport
            // 
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Location = new System.Drawing.Point(571, 476);
            this.btnReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(80, 30);
            this.btnReport.TabIndex = 11387;
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // lblEmailId
            // 
            this.lblEmailId.AutoSize = true;
            this.lblEmailId.Location = new System.Drawing.Point(114, 485);
            this.lblEmailId.Name = "lblEmailId";
            this.lblEmailId.Size = new System.Drawing.Size(52, 15);
            this.lblEmailId.TabIndex = 11393;
            this.lblEmailId.Text = "Email Id";
            this.lblEmailId.Visible = false;
            // 
            // cbEmail
            // 
            this.cbEmail.AutoSize = true;
            this.cbEmail.Location = new System.Drawing.Point(12, 483);
            this.cbEmail.Name = "cbEmail";
            this.cbEmail.Size = new System.Drawing.Size(96, 19);
            this.cbEmail.TabIndex = 11392;
            this.cbEmail.Text = "Email Report";
            this.cbEmail.UseVisualStyleBackColor = true;
            this.cbEmail.CheckedChanged += new System.EventHandler(this.cbEmail_CheckedChanged);
            // 
            // CustomerDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1223, 511);
            this.ControlBox = false;
            this.Controls.Add(this.lblEmailId);
            this.Controls.Add(this.cbEmail);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPendingAmount);
            this.Controls.Add(this.cbToday);
            this.Controls.Add(this.cmbBillNo);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.cbSelectAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtReceiptTotal);
            this.Controls.Add(this.lblTotalCountReceipt);
            this.Controls.Add(this.dgvReceipt);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.txtSaleTotal);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.dgvSale);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerDetails";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CustomerDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbToday;
        private System.Windows.Forms.ComboBox cmbBillNo;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtReceiptTotal;
        private System.Windows.Forms.Label lblTotalCountReceipt;
        private System.Windows.Forms.DataGridView dgvReceipt;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.TextBox txtSaleTotal;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvSale;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNoReceipt;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDateReceipt;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmReceipt;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPaymentType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAmountReceipt;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNoSale;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDateSale;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPaymentTypeSale;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAmountSale;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSaleId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPendingAmount;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label lblEmailId;
        private System.Windows.Forms.CheckBox cbEmail;
    }
}