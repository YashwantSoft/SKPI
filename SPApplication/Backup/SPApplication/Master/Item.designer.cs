namespace SPApplication
{
    partial class Item
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
            this.txtIGSTPer = new System.Windows.Forms.TextBox();
            this.lblIGSTPer = new System.Windows.Forms.Label();
            this.txtSGSTPer = new System.Windows.Forms.TextBox();
            this.lblSGSTPer = new System.Windows.Forms.Label();
            this.txtCGSTPer = new System.Windows.Forms.TextBox();
            this.lblCGSTPer = new System.Windows.Forms.Label();
            this.lblProfitMarginInPer = new System.Windows.Forms.Label();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.lblCost = new System.Windows.Forms.Label();
            this.txtBatchNumber = new System.Windows.Forms.TextBox();
            this.lblBatchNumber = new System.Windows.Forms.Label();
            this.txtHSNCode = new System.Windows.Forms.TextBox();
            this.lblHSNCode = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtSearchItemName = new System.Windows.Forms.TextBox();
            this.lblSearchItem = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtMRP = new System.Windows.Forms.TextBox();
            this.lblMRP = new System.Windows.Forms.Label();
            this.txtContain = new System.Windows.Forms.TextBox();
            this.lblContain = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.cmbManufracture = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.gbItemDetails = new System.Windows.Forms.GroupBox();
            this.gbPricing = new System.Windows.Forms.GroupBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtProfitMarginAmount = new System.Windows.Forms.TextBox();
            this.lblProfitMarginAmount = new System.Windows.Forms.Label();
            this.gbTax = new System.Windows.Forms.GroupBox();
            this.txtProfitMarginPer = new System.Windows.Forms.TextBox();
            this.btnAddUOM = new System.Windows.Forms.Button();
            this.cmbUOM = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnManufracture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbItemDetails.SuspendLayout();
            this.gbPricing.SuspendLayout();
            this.gbTax.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIGSTPer
            // 
            this.txtIGSTPer.Location = new System.Drawing.Point(78, 65);
            this.txtIGSTPer.Name = "txtIGSTPer";
            this.txtIGSTPer.Size = new System.Drawing.Size(120, 23);
            this.txtIGSTPer.TabIndex = 13;
            this.txtIGSTPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtIGSTPer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIGSTPer_KeyDown);
            this.txtIGSTPer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIGSTPer_KeyPress);
            // 
            // lblIGSTPer
            // 
            this.lblIGSTPer.AutoSize = true;
            this.lblIGSTPer.Location = new System.Drawing.Point(15, 68);
            this.lblIGSTPer.Name = "lblIGSTPer";
            this.lblIGSTPer.Size = new System.Drawing.Size(57, 15);
            this.lblIGSTPer.TabIndex = 86;
            this.lblIGSTPer.Text = "IGST In %";
            // 
            // txtSGSTPer
            // 
            this.txtSGSTPer.Location = new System.Drawing.Point(78, 41);
            this.txtSGSTPer.Name = "txtSGSTPer";
            this.txtSGSTPer.Size = new System.Drawing.Size(120, 23);
            this.txtSGSTPer.TabIndex = 12;
            this.txtSGSTPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSGSTPer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSGSTPer_KeyDown);
            this.txtSGSTPer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSGSTPer_KeyPress);
            // 
            // lblSGSTPer
            // 
            this.lblSGSTPer.AutoSize = true;
            this.lblSGSTPer.Location = new System.Drawing.Point(15, 44);
            this.lblSGSTPer.Name = "lblSGSTPer";
            this.lblSGSTPer.Size = new System.Drawing.Size(59, 15);
            this.lblSGSTPer.TabIndex = 85;
            this.lblSGSTPer.Text = "SGST In %";
            // 
            // txtCGSTPer
            // 
            this.txtCGSTPer.Location = new System.Drawing.Point(78, 17);
            this.txtCGSTPer.Name = "txtCGSTPer";
            this.txtCGSTPer.Size = new System.Drawing.Size(120, 23);
            this.txtCGSTPer.TabIndex = 11;
            this.txtCGSTPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCGSTPer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCGSTPer_KeyDown);
            this.txtCGSTPer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCGSTPer_KeyPress);
            // 
            // lblCGSTPer
            // 
            this.lblCGSTPer.AutoSize = true;
            this.lblCGSTPer.Location = new System.Drawing.Point(15, 20);
            this.lblCGSTPer.Name = "lblCGSTPer";
            this.lblCGSTPer.Size = new System.Drawing.Size(60, 15);
            this.lblCGSTPer.TabIndex = 84;
            this.lblCGSTPer.Text = "CGST In %";
            // 
            // lblProfitMarginInPer
            // 
            this.lblProfitMarginInPer.AutoSize = true;
            this.lblProfitMarginInPer.Location = new System.Drawing.Point(12, 91);
            this.lblProfitMarginInPer.Name = "lblProfitMarginInPer";
            this.lblProfitMarginInPer.Size = new System.Drawing.Size(106, 15);
            this.lblProfitMarginInPer.TabIndex = 82;
            this.lblProfitMarginInPer.Text = "Profit Margin In %";
            // 
            // txtCost
            // 
            this.txtCost.Location = new System.Drawing.Point(139, 15);
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(120, 23);
            this.txtCost.TabIndex = 8;
            this.txtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCost_KeyDown);
            this.txtCost.Leave += new System.EventHandler(this.txtCost_Leave);
            this.txtCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCost_KeyPress);
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(12, 22);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(31, 15);
            this.lblCost.TabIndex = 81;
            this.lblCost.Text = "Cost";
            // 
            // txtBatchNumber
            // 
            this.txtBatchNumber.Location = new System.Drawing.Point(109, 87);
            this.txtBatchNumber.Name = "txtBatchNumber";
            this.txtBatchNumber.Size = new System.Drawing.Size(357, 23);
            this.txtBatchNumber.TabIndex = 4;
            this.txtBatchNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNumber_KeyDown);
            // 
            // lblBatchNumber
            // 
            this.lblBatchNumber.AutoSize = true;
            this.lblBatchNumber.Location = new System.Drawing.Point(8, 91);
            this.lblBatchNumber.Name = "lblBatchNumber";
            this.lblBatchNumber.Size = new System.Drawing.Size(84, 15);
            this.lblBatchNumber.TabIndex = 80;
            this.lblBatchNumber.Text = "Batch Number";
            // 
            // txtHSNCode
            // 
            this.txtHSNCode.Location = new System.Drawing.Point(109, 111);
            this.txtHSNCode.Name = "txtHSNCode";
            this.txtHSNCode.Size = new System.Drawing.Size(357, 23);
            this.txtHSNCode.TabIndex = 5;
            this.txtHSNCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHSNCode_KeyDown);
            // 
            // lblHSNCode
            // 
            this.lblHSNCode.AutoSize = true;
            this.lblHSNCode.Location = new System.Drawing.Point(8, 115);
            this.lblHSNCode.Name = "lblHSNCode";
            this.lblHSNCode.Size = new System.Drawing.Size(90, 15);
            this.lblHSNCode.TabIndex = 79;
            this.lblHSNCode.Text = "HSN Code (GST)";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(869, 237);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(73, 15);
            this.lblTotalCount.TabIndex = 78;
            this.lblTotalCount.Text = "Total Count-";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Info;
            this.dataGridView1.Location = new System.Drawing.Point(15, 262);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1003, 316);
            this.dataGridView1.TabIndex = 73;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // txtSearchItemName
            // 
            this.txtSearchItemName.Location = new System.Drawing.Point(121, 234);
            this.txtSearchItemName.Name = "txtSearchItemName";
            this.txtSearchItemName.Size = new System.Drawing.Size(712, 23);
            this.txtSearchItemName.TabIndex = 72;
            this.txtSearchItemName.TextChanged += new System.EventHandler(this.txtSearchItemName_TextChanged);
            // 
            // lblSearchItem
            // 
            this.lblSearchItem.AutoSize = true;
            this.lblSearchItem.Location = new System.Drawing.Point(20, 237);
            this.lblSearchItem.Name = "lblSearchItem";
            this.lblSearchItem.Size = new System.Drawing.Size(71, 15);
            this.lblSearchItem.TabIndex = 77;
            this.lblSearchItem.Text = "Search Item";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(521, 199);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 69;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(430, 199);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 67;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtMRP
            // 
            this.txtMRP.Location = new System.Drawing.Point(139, 63);
            this.txtMRP.Name = "txtMRP";
            this.txtMRP.Size = new System.Drawing.Size(120, 23);
            this.txtMRP.TabIndex = 10;
            this.txtMRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMRP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMRP_KeyDown);
            this.txtMRP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMRP_KeyPress);
            // 
            // lblMRP
            // 
            this.lblMRP.AutoSize = true;
            this.lblMRP.Location = new System.Drawing.Point(12, 68);
            this.lblMRP.Name = "lblMRP";
            this.lblMRP.Size = new System.Drawing.Size(32, 15);
            this.lblMRP.TabIndex = 76;
            this.lblMRP.Text = "MRP";
            // 
            // txtContain
            // 
            this.txtContain.Location = new System.Drawing.Point(109, 135);
            this.txtContain.Name = "txtContain";
            this.txtContain.Size = new System.Drawing.Size(357, 23);
            this.txtContain.TabIndex = 6;
            this.txtContain.Visible = false;
            this.txtContain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContain_KeyDown);
            // 
            // lblContain
            // 
            this.lblContain.AutoSize = true;
            this.lblContain.Location = new System.Drawing.Point(8, 139);
            this.lblContain.Name = "lblContain";
            this.lblContain.Size = new System.Drawing.Size(50, 15);
            this.lblContain.TabIndex = 74;
            this.lblContain.Text = "Contain";
            this.lblContain.Visible = false;
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(109, 39);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(357, 23);
            this.txtItemName.TabIndex = 3;
            this.txtItemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemName_KeyDown);
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(8, 43);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(65, 15);
            this.lblItemName.TabIndex = 71;
            this.lblItemName.Text = "Item Name";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(339, 199);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 66;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(612, 199);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 30);
            this.btnExit.TabIndex = 70;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1030, 29);
            this.lblHeader.TabIndex = 68;
            this.lblHeader.Text = "Item";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbManufracture
            // 
            this.cmbManufracture.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbManufracture.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbManufracture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbManufracture.FormattingEnabled = true;
            this.cmbManufracture.Location = new System.Drawing.Point(109, 15);
            this.cmbManufracture.Name = "cmbManufracture";
            this.cmbManufracture.Size = new System.Drawing.Size(357, 23);
            this.cmbManufracture.TabIndex = 1;
            this.cmbManufracture.SelectionChangeCommitted += new System.EventHandler(this.cmbManufracture_SelectionChangeCommitted);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(8, 19);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(99, 15);
            this.lblCategory.TabIndex = 90;
            this.lblCategory.Text = "Manufracture By";
            // 
            // gbItemDetails
            // 
            this.gbItemDetails.Controls.Add(this.btnManufracture);
            this.gbItemDetails.Controls.Add(this.btnAddUOM);
            this.gbItemDetails.Controls.Add(this.cmbUOM);
            this.gbItemDetails.Controls.Add(this.label3);
            this.gbItemDetails.Controls.Add(this.cmbManufracture);
            this.gbItemDetails.Controls.Add(this.lblItemName);
            this.gbItemDetails.Controls.Add(this.lblCategory);
            this.gbItemDetails.Controls.Add(this.txtItemName);
            this.gbItemDetails.Controls.Add(this.lblContain);
            this.gbItemDetails.Controls.Add(this.txtContain);
            this.gbItemDetails.Controls.Add(this.txtBatchNumber);
            this.gbItemDetails.Controls.Add(this.lblBatchNumber);
            this.gbItemDetails.Controls.Add(this.lblHSNCode);
            this.gbItemDetails.Controls.Add(this.txtHSNCode);
            this.gbItemDetails.Location = new System.Drawing.Point(12, 30);
            this.gbItemDetails.Name = "gbItemDetails";
            this.gbItemDetails.Size = new System.Drawing.Size(500, 165);
            this.gbItemDetails.TabIndex = 0;
            this.gbItemDetails.TabStop = false;
            this.gbItemDetails.Text = "groupBox1";
            // 
            // gbPricing
            // 
            this.gbPricing.Controls.Add(this.lblPrice);
            this.gbPricing.Controls.Add(this.txtPrice);
            this.gbPricing.Controls.Add(this.txtProfitMarginAmount);
            this.gbPricing.Controls.Add(this.lblProfitMarginAmount);
            this.gbPricing.Controls.Add(this.gbTax);
            this.gbPricing.Controls.Add(this.txtProfitMarginPer);
            this.gbPricing.Controls.Add(this.lblMRP);
            this.gbPricing.Controls.Add(this.lblProfitMarginInPer);
            this.gbPricing.Controls.Add(this.txtMRP);
            this.gbPricing.Controls.Add(this.txtCost);
            this.gbPricing.Controls.Add(this.lblCost);
            this.gbPricing.Location = new System.Drawing.Point(518, 30);
            this.gbPricing.Name = "gbPricing";
            this.gbPricing.Size = new System.Drawing.Size(500, 165);
            this.gbPricing.TabIndex = 8;
            this.gbPricing.TabStop = false;
            this.gbPricing.Text = "groupBox1";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(12, 45);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(35, 15);
            this.lblPrice.TabIndex = 96;
            this.lblPrice.Text = "Price";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(139, 39);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(120, 23);
            this.txtPrice.TabIndex = 9;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrice_KeyDown);
            this.txtPrice.Leave += new System.EventHandler(this.txtPrice_Leave);
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // txtProfitMarginAmount
            // 
            this.txtProfitMarginAmount.BackColor = System.Drawing.Color.White;
            this.txtProfitMarginAmount.Location = new System.Drawing.Point(139, 111);
            this.txtProfitMarginAmount.Name = "txtProfitMarginAmount";
            this.txtProfitMarginAmount.ReadOnly = true;
            this.txtProfitMarginAmount.Size = new System.Drawing.Size(120, 23);
            this.txtProfitMarginAmount.TabIndex = 65;
            this.txtProfitMarginAmount.TabStop = false;
            this.txtProfitMarginAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblProfitMarginAmount
            // 
            this.lblProfitMarginAmount.AutoSize = true;
            this.lblProfitMarginAmount.Location = new System.Drawing.Point(12, 114);
            this.lblProfitMarginAmount.Name = "lblProfitMarginAmount";
            this.lblProfitMarginAmount.Size = new System.Drawing.Size(125, 15);
            this.lblProfitMarginAmount.TabIndex = 87;
            this.lblProfitMarginAmount.Text = "Profit Margin Amount";
            // 
            // gbTax
            // 
            this.gbTax.Controls.Add(this.txtIGSTPer);
            this.gbTax.Controls.Add(this.lblCGSTPer);
            this.gbTax.Controls.Add(this.txtCGSTPer);
            this.gbTax.Controls.Add(this.lblSGSTPer);
            this.gbTax.Controls.Add(this.txtSGSTPer);
            this.gbTax.Controls.Add(this.lblIGSTPer);
            this.gbTax.Location = new System.Drawing.Point(276, 10);
            this.gbTax.Name = "gbTax";
            this.gbTax.Size = new System.Drawing.Size(218, 96);
            this.gbTax.TabIndex = 11;
            this.gbTax.TabStop = false;
            this.gbTax.Text = "groupBox2";
            // 
            // txtProfitMarginPer
            // 
            this.txtProfitMarginPer.BackColor = System.Drawing.Color.White;
            this.txtProfitMarginPer.Location = new System.Drawing.Point(139, 87);
            this.txtProfitMarginPer.Name = "txtProfitMarginPer";
            this.txtProfitMarginPer.ReadOnly = true;
            this.txtProfitMarginPer.Size = new System.Drawing.Size(120, 23);
            this.txtProfitMarginPer.TabIndex = 60;
            this.txtProfitMarginPer.TabStop = false;
            this.txtProfitMarginPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnAddUOM
            // 
            this.btnAddUOM.BackColor = System.Drawing.Color.Blue;
            this.btnAddUOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddUOM.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddUOM.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUOM.ForeColor = System.Drawing.Color.White;
            this.btnAddUOM.Location = new System.Drawing.Point(474, 63);
            this.btnAddUOM.Name = "btnAddUOM";
            this.btnAddUOM.Size = new System.Drawing.Size(20, 20);
            this.btnAddUOM.TabIndex = 93;
            this.btnAddUOM.Text = "+";
            this.btnAddUOM.UseVisualStyleBackColor = false;
            this.btnAddUOM.Click += new System.EventHandler(this.btnAddUOM_Click);
            // 
            // cmbUOM
            // 
            this.cmbUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUOM.FormattingEnabled = true;
            this.cmbUOM.Location = new System.Drawing.Point(109, 63);
            this.cmbUOM.Name = "cmbUOM";
            this.cmbUOM.Size = new System.Drawing.Size(357, 23);
            this.cmbUOM.TabIndex = 91;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 15);
            this.label3.TabIndex = 92;
            this.label3.Text = "UOM";
            // 
            // btnManufracture
            // 
            this.btnManufracture.BackColor = System.Drawing.Color.Blue;
            this.btnManufracture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnManufracture.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnManufracture.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManufracture.ForeColor = System.Drawing.Color.White;
            this.btnManufracture.Location = new System.Drawing.Point(474, 16);
            this.btnManufracture.Name = "btnManufracture";
            this.btnManufracture.Size = new System.Drawing.Size(20, 20);
            this.btnManufracture.TabIndex = 94;
            this.btnManufracture.Text = "+";
            this.btnManufracture.UseVisualStyleBackColor = false;
            this.btnManufracture.Click += new System.EventHandler(this.btnManufracture_Click);
            // 
            // Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1030, 585);
            this.ControlBox = false;
            this.Controls.Add(this.gbPricing);
            this.Controls.Add(this.gbItemDetails);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtSearchItemName);
            this.Controls.Add(this.lblSearchItem);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Item";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbItemDetails.ResumeLayout(false);
            this.gbItemDetails.PerformLayout();
            this.gbPricing.ResumeLayout(false);
            this.gbPricing.PerformLayout();
            this.gbTax.ResumeLayout(false);
            this.gbTax.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIGSTPer;
        private System.Windows.Forms.Label lblIGSTPer;
        private System.Windows.Forms.TextBox txtSGSTPer;
        private System.Windows.Forms.Label lblSGSTPer;
        private System.Windows.Forms.TextBox txtCGSTPer;
        private System.Windows.Forms.Label lblCGSTPer;
        private System.Windows.Forms.Label lblProfitMarginInPer;
        private System.Windows.Forms.TextBox txtCost;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.TextBox txtBatchNumber;
        private System.Windows.Forms.Label lblBatchNumber;
        private System.Windows.Forms.TextBox txtHSNCode;
        private System.Windows.Forms.Label lblHSNCode;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtSearchItemName;
        private System.Windows.Forms.Label lblSearchItem;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtMRP;
        private System.Windows.Forms.Label lblMRP;
        private System.Windows.Forms.TextBox txtContain;
        private System.Windows.Forms.Label lblContain;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ComboBox cmbManufracture;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.GroupBox gbItemDetails;
        private System.Windows.Forms.GroupBox gbPricing;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.GroupBox gbTax;
        private System.Windows.Forms.TextBox txtProfitMarginAmount;
        private System.Windows.Forms.Label lblProfitMarginAmount;
        private System.Windows.Forms.TextBox txtProfitMarginPer;
        private System.Windows.Forms.Button btnAddUOM;
        private System.Windows.Forms.ComboBox cmbUOM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnManufracture;
    }
}