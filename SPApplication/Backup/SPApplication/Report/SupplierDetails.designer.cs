namespace SPApplication.Report
{
    partial class SupplierDetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txPaymentTotal = new System.Windows.Forms.TextBox();
            this.lblTotalCountPayment = new System.Windows.Forms.Label();
            this.dgvPayment = new System.Windows.Forms.DataGridView();
            this.clmSrNoExpeses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDateExpenses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPaymentType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAmountPayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPurchaseTotal = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.dgvPurchase = new System.Windows.Forms.DataGridView();
            this.clmSrNoIncome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDateIncome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPaymentTypeIncome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAmountPurchase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPurchaseId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBillNo = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.cbToday = new System.Windows.Forms.CheckBox();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.lblBalance = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPendingAmount = new System.Windows.Forms.TextBox();
            this.btnView = new System.Windows.Forms.Button();
            this.cbEmail = new System.Windows.Forms.CheckBox();
            this.lblEmailId = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(979, 466);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 11321;
            this.label6.Text = "Total Payments";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(238, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 15);
            this.label5.TabIndex = 11319;
            this.label5.Text = "Purchase Details";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(879, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 15);
            this.label4.TabIndex = 11318;
            this.label4.Text = "Payment Details";
            // 
            // txPaymentTotal
            // 
            this.txPaymentTotal.BackColor = System.Drawing.Color.Chartreuse;
            this.txPaymentTotal.Location = new System.Drawing.Point(1071, 463);
            this.txPaymentTotal.Name = "txPaymentTotal";
            this.txPaymentTotal.ReadOnly = true;
            this.txPaymentTotal.Size = new System.Drawing.Size(108, 23);
            this.txPaymentTotal.TabIndex = 11317;
            this.txPaymentTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalCountPayment
            // 
            this.lblTotalCountPayment.AutoSize = true;
            this.lblTotalCountPayment.BackColor = System.Drawing.Color.White;
            this.lblTotalCountPayment.Location = new System.Drawing.Point(619, 467);
            this.lblTotalCountPayment.Name = "lblTotalCountPayment";
            this.lblTotalCountPayment.Size = new System.Drawing.Size(69, 15);
            this.lblTotalCountPayment.TabIndex = 11316;
            this.lblTotalCountPayment.Text = "Total Count";
            // 
            // dgvPayment
            // 
            this.dgvPayment.AllowUserToAddRows = false;
            this.dgvPayment.AllowUserToDeleteRows = false;
            this.dgvPayment.AllowUserToResizeRows = false;
            this.dgvPayment.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSrNoExpeses,
            this.clmDateExpenses,
            this.clmPayment,
            this.clmPaymentType,
            this.clmAmountPayment});
            this.dgvPayment.Location = new System.Drawing.Point(614, 120);
            this.dgvPayment.Name = "dgvPayment";
            this.dgvPayment.RowHeadersVisible = false;
            this.dgvPayment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPayment.Size = new System.Drawing.Size(600, 340);
            this.dgvPayment.TabIndex = 11315;
            // 
            // clmSrNoExpeses
            // 
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmSrNoExpeses.DefaultCellStyle = dataGridViewCellStyle31;
            this.clmSrNoExpeses.HeaderText = "Sr.No.";
            this.clmSrNoExpeses.Name = "clmSrNoExpeses";
            this.clmSrNoExpeses.ReadOnly = true;
            this.clmSrNoExpeses.Width = 40;
            // 
            // clmDateExpenses
            // 
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.clmDateExpenses.DefaultCellStyle = dataGridViewCellStyle32;
            this.clmDateExpenses.HeaderText = "Date";
            this.clmDateExpenses.Name = "clmDateExpenses";
            this.clmDateExpenses.ReadOnly = true;
            // 
            // clmPayment
            // 
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.clmPayment.DefaultCellStyle = dataGridViewCellStyle33;
            this.clmPayment.HeaderText = "Particulars";
            this.clmPayment.Name = "clmPayment";
            this.clmPayment.ReadOnly = true;
            this.clmPayment.Width = 220;
            // 
            // clmPaymentType
            // 
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmPaymentType.DefaultCellStyle = dataGridViewCellStyle34;
            this.clmPaymentType.HeaderText = "Payment Mode";
            this.clmPaymentType.Name = "clmPaymentType";
            this.clmPaymentType.ReadOnly = true;
            this.clmPaymentType.Width = 120;
            // 
            // clmAmountPayment
            // 
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clmAmountPayment.DefaultCellStyle = dataGridViewCellStyle35;
            this.clmAmountPayment.HeaderText = "Amount";
            this.clmAmountPayment.Name = "clmAmountPayment";
            this.clmAmountPayment.ReadOnly = true;
            this.clmAmountPayment.Width = 80;
            // 
            // txtPurchaseTotal
            // 
            this.txtPurchaseTotal.BackColor = System.Drawing.Color.OrangeRed;
            this.txtPurchaseTotal.Location = new System.Drawing.Point(459, 463);
            this.txtPurchaseTotal.Name = "txtPurchaseTotal";
            this.txtPurchaseTotal.ReadOnly = true;
            this.txtPurchaseTotal.Size = new System.Drawing.Size(108, 23);
            this.txtPurchaseTotal.TabIndex = 11314;
            this.txtPurchaseTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BackColor = System.Drawing.Color.White;
            this.lblCount.Location = new System.Drawing.Point(12, 466);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(69, 15);
            this.lblCount.TabIndex = 11313;
            this.lblCount.Text = "Total Count";
            // 
            // dgvPurchase
            // 
            this.dgvPurchase.AllowUserToAddRows = false;
            this.dgvPurchase.AllowUserToDeleteRows = false;
            this.dgvPurchase.AllowUserToResizeRows = false;
            this.dgvPurchase.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchase.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSrNoIncome,
            this.clmDateIncome,
            this.clmDetails,
            this.clmPaymentTypeIncome,
            this.clmAmountPurchase,
            this.clmPurchaseId});
            this.dgvPurchase.Location = new System.Drawing.Point(9, 121);
            this.dgvPurchase.Name = "dgvPurchase";
            this.dgvPurchase.RowHeadersVisible = false;
            this.dgvPurchase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchase.Size = new System.Drawing.Size(600, 339);
            this.dgvPurchase.TabIndex = 11312;
            // 
            // clmSrNoIncome
            // 
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmSrNoIncome.DefaultCellStyle = dataGridViewCellStyle36;
            this.clmSrNoIncome.HeaderText = "Sr.No.";
            this.clmSrNoIncome.Name = "clmSrNoIncome";
            this.clmSrNoIncome.ReadOnly = true;
            this.clmSrNoIncome.Width = 40;
            // 
            // clmDateIncome
            // 
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.clmDateIncome.DefaultCellStyle = dataGridViewCellStyle37;
            this.clmDateIncome.HeaderText = "Date";
            this.clmDateIncome.Name = "clmDateIncome";
            this.clmDateIncome.ReadOnly = true;
            // 
            // clmDetails
            // 
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.clmDetails.DefaultCellStyle = dataGridViewCellStyle38;
            this.clmDetails.HeaderText = "Particulars";
            this.clmDetails.Name = "clmDetails";
            this.clmDetails.ReadOnly = true;
            this.clmDetails.Width = 220;
            // 
            // clmPaymentTypeIncome
            // 
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmPaymentTypeIncome.DefaultCellStyle = dataGridViewCellStyle39;
            this.clmPaymentTypeIncome.HeaderText = "Payment Mode";
            this.clmPaymentTypeIncome.Name = "clmPaymentTypeIncome";
            this.clmPaymentTypeIncome.ReadOnly = true;
            this.clmPaymentTypeIncome.Width = 120;
            // 
            // clmAmountPurchase
            // 
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clmAmountPurchase.DefaultCellStyle = dataGridViewCellStyle40;
            this.clmAmountPurchase.HeaderText = "Amount";
            this.clmAmountPurchase.Name = "clmAmountPurchase";
            this.clmAmountPurchase.ReadOnly = true;
            this.clmAmountPurchase.Width = 80;
            // 
            // clmPurchaseId
            // 
            this.clmPurchaseId.HeaderText = "PurchaseId";
            this.clmPurchaseId.Name = "clmPurchaseId";
            this.clmPurchaseId.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(664, 84);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 30);
            this.btnExit.TabIndex = 11311;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Location = new System.Drawing.Point(571, 485);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(80, 30);
            this.btnReport.TabIndex = 11309;
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(581, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 11307;
            this.label2.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(630, 31);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(120, 23);
            this.dtpToDate.TabIndex = 11305;
            this.dtpToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpToDate_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(377, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 11306;
            this.label1.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(446, 31);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(120, 23);
            this.dtpFromDate.TabIndex = 11304;
            this.dtpFromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpFromDate_KeyDown);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(571, 84);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 11310;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Blue;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(-5, -2);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1229, 30);
            this.lblHeader.TabIndex = 11303;
            this.lblHeader.Text = "Supplier Details";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 466);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 11320;
            this.label3.Text = "Total Purchase";
            // 
            // cmbBillNo
            // 
            this.cmbBillNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBillNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBillNo.FormattingEnabled = true;
            this.cmbBillNo.Location = new System.Drawing.Point(446, 56);
            this.cmbBillNo.Name = "cmbBillNo";
            this.cmbBillNo.Size = new System.Drawing.Size(304, 23);
            this.cmbBillNo.TabIndex = 11323;
            this.cmbBillNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBillNo_KeyDown);
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(377, 59);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(44, 15);
            this.lblSupplier.TabIndex = 11322;
            this.lblSupplier.Text = "Bill No";
            // 
            // cbToday
            // 
            this.cbToday.AutoSize = true;
            this.cbToday.Location = new System.Drawing.Point(768, 34);
            this.cbToday.Name = "cbToday";
            this.cbToday.Size = new System.Drawing.Size(58, 19);
            this.cbToday.TabIndex = 11324;
            this.cbToday.Text = "Today";
            this.cbToday.UseVisualStyleBackColor = true;
            this.cbToday.CheckedChanged += new System.EventHandler(this.cbDate_CheckedChanged);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(769, 55);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(76, 19);
            this.cbSelectAll.TabIndex = 11308;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // lblBalance
            // 
            this.lblBalance.BackColor = System.Drawing.Color.White;
            this.lblBalance.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.Location = new System.Drawing.Point(6, 35);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(207, 24);
            this.lblBalance.TabIndex = 11361;
            this.lblBalance.Text = "Opening Balance";
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(973, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 15);
            this.label7.TabIndex = 11388;
            this.label7.Text = "Pending Amount";
            // 
            // txtPendingAmount
            // 
            this.txtPendingAmount.BackColor = System.Drawing.Color.Cyan;
            this.txtPendingAmount.Location = new System.Drawing.Point(1071, 32);
            this.txtPendingAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPendingAmount.Name = "txtPendingAmount";
            this.txtPendingAmount.ReadOnly = true;
            this.txtPendingAmount.Size = new System.Drawing.Size(103, 23);
            this.txtPendingAmount.TabIndex = 11387;
            this.txtPendingAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnView
            // 
            this.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnView.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(474, 84);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(80, 30);
            this.btnView.TabIndex = 11389;
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // cbEmail
            // 
            this.cbEmail.AutoSize = true;
            this.cbEmail.Location = new System.Drawing.Point(14, 494);
            this.cbEmail.Name = "cbEmail";
            this.cbEmail.Size = new System.Drawing.Size(96, 19);
            this.cbEmail.TabIndex = 11390;
            this.cbEmail.Text = "Email Report";
            this.cbEmail.UseVisualStyleBackColor = true;
            this.cbEmail.CheckedChanged += new System.EventHandler(this.cbEmail_CheckedChanged);
            // 
            // lblEmailId
            // 
            this.lblEmailId.AutoSize = true;
            this.lblEmailId.Location = new System.Drawing.Point(116, 496);
            this.lblEmailId.Name = "lblEmailId";
            this.lblEmailId.Size = new System.Drawing.Size(52, 15);
            this.lblEmailId.TabIndex = 11391;
            this.lblEmailId.Text = "Email Id";
            this.lblEmailId.Visible = false;
            // 
            // SupplierDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1223, 521);
            this.ControlBox = false;
            this.Controls.Add(this.lblEmailId);
            this.Controls.Add(this.cbEmail);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPendingAmount);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.cbToday);
            this.Controls.Add(this.cmbBillNo);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txPaymentTotal);
            this.Controls.Add(this.lblTotalCountPayment);
            this.Controls.Add(this.dgvPayment);
            this.Controls.Add(this.txtPurchaseTotal);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.dgvPurchase);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.cbSelectAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SupplierDetails";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.SupplierDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txPaymentTotal;
        private System.Windows.Forms.Label lblTotalCountPayment;
        private System.Windows.Forms.DataGridView dgvPayment;
        private System.Windows.Forms.TextBox txtPurchaseTotal;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvPurchase;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbBillNo;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.CheckBox cbToday;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNoExpeses;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDateExpenses;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPayment;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPaymentType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAmountPayment;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNoIncome;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDateIncome;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPaymentTypeIncome;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAmountPurchase;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPurchaseId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPendingAmount;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.CheckBox cbEmail;
        private System.Windows.Forms.Label lblEmailId;
    }
}