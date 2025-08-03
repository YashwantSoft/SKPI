namespace SPApplication.Transaction
{
    partial class ItemRequisition
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.lblSearchSC = new System.Windows.Forms.Label();
            this.txtSearchSupplier = new System.Windows.Forms.TextBox();
            this.txtPurchaseNo = new System.Windows.Forms.TextBox();
            this.lblSPNo = new System.Windows.Forms.Label();
            this.lbSupplier = new System.Windows.Forms.ListBox();
            this.txtSearchItem = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtChallanNo = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.dgvItem = new System.Windows.Forms.DataGridView();
            this.clmSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPurchaseSaleItemsId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmUOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDelete = new System.Windows.Forms.DataGridViewLinkColumn();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.btnAddGrid = new System.Windows.Forms.Button();
            this.btnClearGrid = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.lbItem = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnAddVendor = new System.Windows.Forms.Button();
            this.rtbItem = new System.Windows.Forms.RichTextBox();
            this.gbItem = new System.Windows.Forms.GroupBox();
            this.label38 = new System.Windows.Forms.Label();
            this.lblAvlQuantity = new System.Windows.Forms.Label();
            this.gbInvoiceDetails = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddEmployee = new System.Windows.Forms.Button();
            this.cmbEmployeeName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEntryDate = new System.Windows.Forms.DateTimePicker();
            this.rtbSC = new System.Windows.Forms.RichTextBox();
            this.lblTotalItemCount = new System.Windows.Forms.Label();
            this.txtNaration = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.btnInvoice = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).BeginInit();
            this.gbItem.SuspendLayout();
            this.gbInvoiceDetails.SuspendLayout();
            this.SuspendLayout();
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
            this.dataGridView1.Location = new System.Drawing.Point(22, 552);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1248, 134);
            this.dataGridView1.TabIndex = 64;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 65;
            this.label1.Text = "Invoice Date";
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(106, 35);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(116, 23);
            this.dtpInvoiceDate.TabIndex = 0;
            this.dtpInvoiceDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDate_KeyDown);
            // 
            // lblSearchSC
            // 
            this.lblSearchSC.AutoSize = true;
            this.lblSearchSC.Location = new System.Drawing.Point(7, 88);
            this.lblSearchSC.Name = "lblSearchSC";
            this.lblSearchSC.Size = new System.Drawing.Size(85, 15);
            this.lblSearchSC.TabIndex = 67;
            this.lblSearchSC.Text = "Search Vendor";
            // 
            // txtSearchSupplier
            // 
            this.txtSearchSupplier.Location = new System.Drawing.Point(106, 84);
            this.txtSearchSupplier.Name = "txtSearchSupplier";
            this.txtSearchSupplier.Size = new System.Drawing.Size(424, 23);
            this.txtSearchSupplier.TabIndex = 3;
            this.txtSearchSupplier.TextChanged += new System.EventHandler(this.txtSearchSC_TextChanged);
            this.txtSearchSupplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchSC_KeyDown);
            // 
            // txtPurchaseNo
            // 
            this.txtPurchaseNo.Location = new System.Drawing.Point(414, 12);
            this.txtPurchaseNo.Name = "txtPurchaseNo";
            this.txtPurchaseNo.ReadOnly = true;
            this.txtPurchaseNo.Size = new System.Drawing.Size(116, 23);
            this.txtPurchaseNo.TabIndex = 74;
            this.txtPurchaseNo.TabStop = false;
            // 
            // lblSPNo
            // 
            this.lblSPNo.AutoSize = true;
            this.lblSPNo.Location = new System.Drawing.Point(322, 15);
            this.lblSPNo.Name = "lblSPNo";
            this.lblSPNo.Size = new System.Drawing.Size(90, 15);
            this.lblSPNo.TabIndex = 73;
            this.lblSPNo.Text = "Requisition No.";
            // 
            // lbSupplier
            // 
            this.lbSupplier.FormattingEnabled = true;
            this.lbSupplier.ItemHeight = 15;
            this.lbSupplier.Location = new System.Drawing.Point(106, 109);
            this.lbSupplier.Name = "lbSupplier";
            this.lbSupplier.Size = new System.Drawing.Size(423, 124);
            this.lbSupplier.TabIndex = 4;
            this.lbSupplier.Click += new System.EventHandler(this.lbSC_Click);
            this.lbSupplier.SelectedIndexChanged += new System.EventHandler(this.lbSC_SelectedIndexChanged);
            this.lbSupplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbSC_KeyDown);
            // 
            // txtSearchItem
            // 
            this.txtSearchItem.Location = new System.Drawing.Point(98, 12);
            this.txtSearchItem.Name = "txtSearchItem";
            this.txtSearchItem.Size = new System.Drawing.Size(541, 23);
            this.txtSearchItem.TabIndex = 6;
            this.txtSearchItem.TextChanged += new System.EventHandler(this.txtSearchItem_TextChanged);
            this.txtSearchItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchItem_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 15);
            this.label6.TabIndex = 76;
            this.label6.Text = "Search";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(98, 200);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(90, 23);
            this.txtQuantity.TabIndex = 8;
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyDown);
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            this.txtQuantity.Leave += new System.EventHandler(this.txtQuantity_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(42, 203);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 15);
            this.label10.TabIndex = 84;
            this.label10.Text = "Quantity";
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Location = new System.Drawing.Point(106, 59);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(116, 23);
            this.txtInvoiceNo.TabIndex = 1;
            this.txtInvoiceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillNo_KeyDown);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 62);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 15);
            this.label16.TabIndex = 96;
            this.label16.Text = "Invoice No.";
            // 
            // txtChallanNo
            // 
            this.txtChallanNo.Location = new System.Drawing.Point(414, 36);
            this.txtChallanNo.Name = "txtChallanNo";
            this.txtChallanNo.Size = new System.Drawing.Size(116, 23);
            this.txtChallanNo.TabIndex = 2;
            this.txtChallanNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChallanNo_KeyDown);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(333, 39);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 15);
            this.label17.TabIndex = 98;
            this.label17.Text = "Challan No.";
            // 
            // dgvItem
            // 
            this.dgvItem.AllowUserToAddRows = false;
            this.dgvItem.AllowUserToDeleteRows = false;
            this.dgvItem.AllowUserToResizeRows = false;
            this.dgvItem.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSrNo,
            this.clmPurchaseSaleItemsId,
            this.clmItemId,
            this.clmCategory,
            this.clmItemName,
            this.clmUOM,
            this.clmQty,
            this.clmDelete});
            this.dgvItem.Location = new System.Drawing.Point(22, 289);
            this.dgvItem.Name = "dgvItem";
            this.dgvItem.RowHeadersVisible = false;
            this.dgvItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItem.Size = new System.Drawing.Size(1248, 192);
            this.dgvItem.TabIndex = 11333;
            this.dgvItem.TabStop = false;
            this.dgvItem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItem_CellClick);
            this.dgvItem.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItem_CellDoubleClick);
            // 
            // clmSrNo
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmSrNo.DefaultCellStyle = dataGridViewCellStyle12;
            this.clmSrNo.HeaderText = "Sr.No.";
            this.clmSrNo.Name = "clmSrNo";
            this.clmSrNo.ReadOnly = true;
            this.clmSrNo.Width = 80;
            // 
            // clmPurchaseSaleItemsId
            // 
            this.clmPurchaseSaleItemsId.HeaderText = "Purchase Sale Items Id";
            this.clmPurchaseSaleItemsId.Name = "clmPurchaseSaleItemsId";
            this.clmPurchaseSaleItemsId.ReadOnly = true;
            this.clmPurchaseSaleItemsId.Visible = false;
            // 
            // clmItemId
            // 
            this.clmItemId.HeaderText = "ItemId";
            this.clmItemId.Name = "clmItemId";
            this.clmItemId.ReadOnly = true;
            this.clmItemId.Visible = false;
            // 
            // clmCategory
            // 
            this.clmCategory.HeaderText = "Category";
            this.clmCategory.Name = "clmCategory";
            this.clmCategory.ReadOnly = true;
            this.clmCategory.Width = 200;
            // 
            // clmItemName
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.clmItemName.DefaultCellStyle = dataGridViewCellStyle13;
            this.clmItemName.HeaderText = "Item Name";
            this.clmItemName.Name = "clmItemName";
            this.clmItemName.ReadOnly = true;
            this.clmItemName.Width = 600;
            // 
            // clmUOM
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmUOM.DefaultCellStyle = dataGridViewCellStyle14;
            this.clmUOM.HeaderText = "UOM";
            this.clmUOM.Name = "clmUOM";
            this.clmUOM.ReadOnly = true;
            // 
            // clmQty
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clmQty.DefaultCellStyle = dataGridViewCellStyle15;
            this.clmQty.HeaderText = "Qty";
            this.clmQty.Name = "clmQty";
            this.clmQty.ReadOnly = true;
            // 
            // clmDelete
            // 
            this.clmDelete.HeaderText = "Delete";
            this.clmDelete.Name = "clmDelete";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(26, 535);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(69, 15);
            this.lblTotalCount.TabIndex = 101;
            this.lblTotalCount.Text = "Total Count";
            // 
            // btnAddGrid
            // 
            this.btnAddGrid.BackColor = System.Drawing.Color.Blue;
            this.btnAddGrid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddGrid.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddGrid.ForeColor = System.Drawing.Color.White;
            this.btnAddGrid.Location = new System.Drawing.Point(548, 200);
            this.btnAddGrid.Name = "btnAddGrid";
            this.btnAddGrid.Size = new System.Drawing.Size(55, 23);
            this.btnAddGrid.TabIndex = 14;
            this.btnAddGrid.Text = "Add";
            this.btnAddGrid.UseVisualStyleBackColor = false;
            this.btnAddGrid.Click += new System.EventHandler(this.btnAddGrid_Click);
            // 
            // btnClearGrid
            // 
            this.btnClearGrid.BackColor = System.Drawing.Color.Blue;
            this.btnClearGrid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearGrid.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearGrid.ForeColor = System.Drawing.Color.White;
            this.btnClearGrid.Location = new System.Drawing.Point(607, 200);
            this.btnClearGrid.Name = "btnClearGrid";
            this.btnClearGrid.Size = new System.Drawing.Size(55, 23);
            this.btnClearGrid.TabIndex = 100;
            this.btnClearGrid.Text = "Clear";
            this.btnClearGrid.UseVisualStyleBackColor = false;
            this.btnClearGrid.Click += new System.EventHandler(this.btnClearGrid_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(-1, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1299, 29);
            this.lblHeader.TabIndex = 106;
            this.lblHeader.Text = "Purchase";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.Blue;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.Location = new System.Drawing.Point(644, 13);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(20, 20);
            this.btnAddItem.TabIndex = 7;
            this.btnAddItem.TabStop = false;
            this.btnAddItem.Text = "+";
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // lbItem
            // 
            this.lbItem.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbItem.FormattingEnabled = true;
            this.lbItem.ItemHeight = 19;
            this.lbItem.Location = new System.Drawing.Point(98, 36);
            this.lbItem.Name = "lbItem";
            this.lbItem.Size = new System.Drawing.Size(563, 156);
            this.lbItem.TabIndex = 7;
            this.lbItem.Visible = false;
            this.lbItem.Click += new System.EventHandler(this.lbItem_Click);
            this.lbItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbItem_KeyDown);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(607, 516);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 30;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(522, 516);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 29;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(437, 516);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 28;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(692, 516);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 30);
            this.btnExit.TabIndex = 31;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAddVendor
            // 
            this.btnAddVendor.BackColor = System.Drawing.Color.Blue;
            this.btnAddVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddVendor.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddVendor.ForeColor = System.Drawing.Color.White;
            this.btnAddVendor.Location = new System.Drawing.Point(534, 85);
            this.btnAddVendor.Name = "btnAddVendor";
            this.btnAddVendor.Size = new System.Drawing.Size(20, 20);
            this.btnAddVendor.TabIndex = 4;
            this.btnAddVendor.Text = "+";
            this.btnAddVendor.UseVisualStyleBackColor = false;
            this.btnAddVendor.Click += new System.EventHandler(this.btnAddVendor_Click);
            // 
            // rtbItem
            // 
            this.rtbItem.BackColor = System.Drawing.Color.White;
            this.rtbItem.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbItem.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rtbItem.Location = new System.Drawing.Point(98, 38);
            this.rtbItem.Name = "rtbItem";
            this.rtbItem.ReadOnly = true;
            this.rtbItem.Size = new System.Drawing.Size(566, 158);
            this.rtbItem.TabIndex = 11319;
            this.rtbItem.Text = "";
            this.rtbItem.Visible = false;
            // 
            // gbItem
            // 
            this.gbItem.Controls.Add(this.label38);
            this.gbItem.Controls.Add(this.lblAvlQuantity);
            this.gbItem.Controls.Add(this.lbItem);
            this.gbItem.Controls.Add(this.label6);
            this.gbItem.Controls.Add(this.txtSearchItem);
            this.gbItem.Controls.Add(this.label10);
            this.gbItem.Controls.Add(this.btnAddGrid);
            this.gbItem.Controls.Add(this.txtQuantity);
            this.gbItem.Controls.Add(this.btnClearGrid);
            this.gbItem.Controls.Add(this.btnAddItem);
            this.gbItem.Controls.Add(this.rtbItem);
            this.gbItem.Location = new System.Drawing.Point(588, 30);
            this.gbItem.Name = "gbItem";
            this.gbItem.Size = new System.Drawing.Size(682, 237);
            this.gbItem.TabIndex = 6;
            this.gbItem.TabStop = false;
            this.gbItem.Text = "Item Details";
            this.gbItem.Visible = false;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(12, 66);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(77, 15);
            this.label38.TabIndex = 11340;
            this.label38.Text = "Avl. Quantity";
            // 
            // lblAvlQuantity
            // 
            this.lblAvlQuantity.BackColor = System.Drawing.Color.Chartreuse;
            this.lblAvlQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAvlQuantity.Location = new System.Drawing.Point(13, 83);
            this.lblAvlQuantity.Name = "lblAvlQuantity";
            this.lblAvlQuantity.Size = new System.Drawing.Size(73, 58);
            this.lblAvlQuantity.TabIndex = 11339;
            this.lblAvlQuantity.Text = "Qty";
            this.lblAvlQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbInvoiceDetails
            // 
            this.gbInvoiceDetails.Controls.Add(this.label3);
            this.gbInvoiceDetails.Controls.Add(this.btnAddEmployee);
            this.gbInvoiceDetails.Controls.Add(this.cmbEmployeeName);
            this.gbInvoiceDetails.Controls.Add(this.label2);
            this.gbInvoiceDetails.Controls.Add(this.dtpEntryDate);
            this.gbInvoiceDetails.Controls.Add(this.lbSupplier);
            this.gbInvoiceDetails.Controls.Add(this.label1);
            this.gbInvoiceDetails.Controls.Add(this.dtpInvoiceDate);
            this.gbInvoiceDetails.Controls.Add(this.lblSearchSC);
            this.gbInvoiceDetails.Controls.Add(this.txtSearchSupplier);
            this.gbInvoiceDetails.Controls.Add(this.lblSPNo);
            this.gbInvoiceDetails.Controls.Add(this.txtPurchaseNo);
            this.gbInvoiceDetails.Controls.Add(this.btnAddVendor);
            this.gbInvoiceDetails.Controls.Add(this.label16);
            this.gbInvoiceDetails.Controls.Add(this.txtInvoiceNo);
            this.gbInvoiceDetails.Controls.Add(this.label17);
            this.gbInvoiceDetails.Controls.Add(this.txtChallanNo);
            this.gbInvoiceDetails.Controls.Add(this.rtbSC);
            this.gbInvoiceDetails.Location = new System.Drawing.Point(22, 30);
            this.gbInvoiceDetails.Name = "gbInvoiceDetails";
            this.gbInvoiceDetails.Size = new System.Drawing.Size(558, 237);
            this.gbInvoiceDetails.TabIndex = 0;
            this.gbInvoiceDetails.TabStop = false;
            this.gbInvoiceDetails.Text = "Invoice Details";
            this.gbInvoiceDetails.Enter += new System.EventHandler(this.gbInvoiceDetails_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(319, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 15);
            this.label3.TabIndex = 11343;
            this.label3.Text = "Employee Name";
            // 
            // btnAddEmployee
            // 
            this.btnAddEmployee.BackColor = System.Drawing.Color.Blue;
            this.btnAddEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddEmployee.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddEmployee.ForeColor = System.Drawing.Color.White;
            this.btnAddEmployee.Location = new System.Drawing.Point(534, 59);
            this.btnAddEmployee.Name = "btnAddEmployee";
            this.btnAddEmployee.Size = new System.Drawing.Size(20, 20);
            this.btnAddEmployee.TabIndex = 11342;
            this.btnAddEmployee.Text = "+";
            this.btnAddEmployee.UseVisualStyleBackColor = false;
            this.btnAddEmployee.Click += new System.EventHandler(this.btnAddEmployee_Click);
            // 
            // cmbEmployeeName
            // 
            this.cmbEmployeeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployeeName.FormattingEnabled = true;
            this.cmbEmployeeName.Location = new System.Drawing.Point(414, 60);
            this.cmbEmployeeName.Name = "cmbEmployeeName";
            this.cmbEmployeeName.Size = new System.Drawing.Size(116, 23);
            this.cmbEmployeeName.TabIndex = 11341;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 11340;
            this.label2.Text = "Entry Date";
            // 
            // dtpEntryDate
            // 
            this.dtpEntryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEntryDate.Location = new System.Drawing.Point(106, 11);
            this.dtpEntryDate.Name = "dtpEntryDate";
            this.dtpEntryDate.Size = new System.Drawing.Size(116, 23);
            this.dtpEntryDate.TabIndex = 11339;
            // 
            // rtbSC
            // 
            this.rtbSC.BackColor = System.Drawing.Color.White;
            this.rtbSC.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbSC.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rtbSC.Location = new System.Drawing.Point(106, 110);
            this.rtbSC.Name = "rtbSC";
            this.rtbSC.ReadOnly = true;
            this.rtbSC.Size = new System.Drawing.Size(424, 120);
            this.rtbSC.TabIndex = 11338;
            this.rtbSC.Text = "";
            this.rtbSC.Visible = false;
            // 
            // lblTotalItemCount
            // 
            this.lblTotalItemCount.AutoSize = true;
            this.lblTotalItemCount.Location = new System.Drawing.Point(24, 272);
            this.lblTotalItemCount.Name = "lblTotalItemCount";
            this.lblTotalItemCount.Size = new System.Drawing.Size(69, 15);
            this.lblTotalItemCount.TabIndex = 11331;
            this.lblTotalItemCount.Text = "Total Count";
            // 
            // txtNaration
            // 
            this.txtNaration.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNaration.Location = new System.Drawing.Point(84, 484);
            this.txtNaration.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNaration.Name = "txtNaration";
            this.txtNaration.Size = new System.Drawing.Size(1186, 23);
            this.txtNaration.TabIndex = 27;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(23, 488);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(55, 15);
            this.label36.TabIndex = 80;
            this.label36.Text = "Naration";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(1104, 521);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(166, 23);
            this.txtSearch.TabIndex = 32;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(1058, 524);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(44, 15);
            this.label37.TabIndex = 11342;
            this.label37.Text = "Search";
            // 
            // btnInvoice
            // 
            this.btnInvoice.BackColor = System.Drawing.Color.Transparent;
            this.btnInvoice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInvoice.Location = new System.Drawing.Point(777, 516);
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Size = new System.Drawing.Size(80, 30);
            this.btnInvoice.TabIndex = 11343;
            this.btnInvoice.UseVisualStyleBackColor = false;
            this.btnInvoice.Visible = false;
            this.btnInvoice.Click += new System.EventHandler(this.btnInvoice_Click);
            // 
            // ItemRequisition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1298, 698);
            this.ControlBox = false;
            this.Controls.Add(this.btnInvoice);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.txtNaration);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.dgvItem);
            this.Controls.Add(this.lblTotalItemCount);
            this.Controls.Add(this.gbInvoiceDetails);
            this.Controls.Add(this.gbItem);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ItemRequisition";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Purchase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).EndInit();
            this.gbItem.ResumeLayout(false);
            this.gbItem.PerformLayout();
            this.gbInvoiceDetails.ResumeLayout(false);
            this.gbInvoiceDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.Label lblSearchSC;
        private System.Windows.Forms.TextBox txtSearchSupplier;
        private System.Windows.Forms.TextBox txtPurchaseNo;
        private System.Windows.Forms.Label lblSPNo;
        private System.Windows.Forms.ListBox lbSupplier;
        private System.Windows.Forms.TextBox txtSearchItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtChallanNo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridView dgvItem;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Button btnAddGrid;
        private System.Windows.Forms.Button btnClearGrid;
        private System.Windows.Forms.Label lblHeader;

        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.ListBox lbItem;
        private System.Windows.Forms.Button btnAddVendor;
        private System.Windows.Forms.RichTextBox rtbItem;
        private System.Windows.Forms.GroupBox gbItem;
        private System.Windows.Forms.GroupBox gbInvoiceDetails;
        private System.Windows.Forms.Label lblTotalItemCount;
        private System.Windows.Forms.TextBox txtNaration;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.RichTextBox rtbSC;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label lblAvlQuantity;
        private System.Windows.Forms.Button btnInvoice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEntryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPurchaseSaleItemsId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmQty;
        private System.Windows.Forms.DataGridViewLinkColumn clmDelete;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddEmployee;
        private System.Windows.Forms.ComboBox cmbEmployeeName;
        //private System.Windows.Forms.DataGridView dgvItem1;
    }
}