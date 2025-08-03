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
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.lblContain = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.gbItemDetails = new System.Windows.Forms.GroupBox();
            this.txtOldPartNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPartNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnAddManufacturer = new System.Windows.Forms.Button();
            this.btnAddUOM = new System.Windows.Forms.Button();
            this.cmbManufacturer = new System.Windows.Forms.ComboBox();
            this.cmbUOM = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnManufracture = new System.Windows.Forms.Button();
            this.txtOpeningStock = new System.Windows.Forms.TextBox();
            this.lblOpeningStock = new System.Windows.Forms.Label();
            this.gbPricing = new System.Windows.Forms.GroupBox();
            this.txtMOQ = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtProfitMarginAmount = new System.Windows.Forms.TextBox();
            this.lblProfitMarginAmount = new System.Windows.Forms.Label();
            this.gbTax = new System.Windows.Forms.GroupBox();
            this.txtProfitMarginPer = new System.Windows.Forms.TextBox();
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
            this.txtIGSTPer.TabIndex = 11;
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
            this.txtSGSTPer.TabIndex = 10;
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
            this.txtCGSTPer.TabIndex = 9;
            this.txtCGSTPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCGSTPer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCGSTPer_KeyDown);
            this.txtCGSTPer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCGSTPer_KeyPress);
            // 
            // lblCGSTPer
            // 
            this.lblCGSTPer.AutoSize = true;
            this.lblCGSTPer.Location = new System.Drawing.Point(16, 20);
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
            this.txtCost.TabIndex = 6;
            this.txtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCost_KeyDown);
            this.txtCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCost_KeyPress);
            this.txtCost.Leave += new System.EventHandler(this.txtCost_Leave);
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
            this.txtBatchNumber.Location = new System.Drawing.Point(108, 234);
            this.txtBatchNumber.Name = "txtBatchNumber";
            this.txtBatchNumber.Size = new System.Drawing.Size(357, 23);
            this.txtBatchNumber.TabIndex = 3;
            this.txtBatchNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNumber_KeyDown);
            // 
            // lblBatchNumber
            // 
            this.lblBatchNumber.AutoSize = true;
            this.lblBatchNumber.Location = new System.Drawing.Point(6, 237);
            this.lblBatchNumber.Name = "lblBatchNumber";
            this.lblBatchNumber.Size = new System.Drawing.Size(84, 15);
            this.lblBatchNumber.TabIndex = 80;
            this.lblBatchNumber.Text = "Batch Number";
            // 
            // txtHSNCode
            // 
            this.txtHSNCode.Location = new System.Drawing.Point(108, 258);
            this.txtHSNCode.Name = "txtHSNCode";
            this.txtHSNCode.Size = new System.Drawing.Size(357, 23);
            this.txtHSNCode.TabIndex = 4;
            this.txtHSNCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHSNCode_KeyDown);
            // 
            // lblHSNCode
            // 
            this.lblHSNCode.AutoSize = true;
            this.lblHSNCode.Location = new System.Drawing.Point(6, 260);
            this.lblHSNCode.Name = "lblHSNCode";
            this.lblHSNCode.Size = new System.Drawing.Size(90, 15);
            this.lblHSNCode.TabIndex = 79;
            this.lblHSNCode.Text = "HSN Code (GST)";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(18, 347);
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
            this.dataGridView1.Location = new System.Drawing.Point(12, 365);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1006, 219);
            this.dataGridView1.TabIndex = 73;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // txtSearchItemName
            // 
            this.txtSearchItemName.Location = new System.Drawing.Point(766, 333);
            this.txtSearchItemName.Name = "txtSearchItemName";
            this.txtSearchItemName.Size = new System.Drawing.Size(252, 23);
            this.txtSearchItemName.TabIndex = 16;
            this.txtSearchItemName.TextChanged += new System.EventHandler(this.txtSearchItemName_TextChanged);
            // 
            // lblSearchItem
            // 
            this.lblSearchItem.AutoSize = true;
            this.lblSearchItem.Location = new System.Drawing.Point(693, 337);
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
            this.btnDelete.Location = new System.Drawing.Point(518, 329);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(438, 329);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 13;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtMRP
            // 
            this.txtMRP.Location = new System.Drawing.Point(139, 63);
            this.txtMRP.Name = "txtMRP";
            this.txtMRP.Size = new System.Drawing.Size(120, 23);
            this.txtMRP.TabIndex = 8;
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
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(108, 114);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(357, 23);
            this.txtItemCode.TabIndex = 5;
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContain_KeyDown);
            // 
            // lblContain
            // 
            this.lblContain.AutoSize = true;
            this.lblContain.Location = new System.Drawing.Point(7, 118);
            this.lblContain.Name = "lblContain";
            this.lblContain.Size = new System.Drawing.Size(61, 15);
            this.lblContain.TabIndex = 74;
            this.lblContain.Text = "Item Code";
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(108, 42);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(357, 23);
            this.txtItemName.TabIndex = 1;
            this.txtItemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemName_KeyDown);
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(7, 45);
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
            this.btnSave.Location = new System.Drawing.Point(358, 329);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 12;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(598, 329);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 15;
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
            // cmbCategory
            // 
            this.cmbCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(108, 18);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(357, 23);
            this.cmbCategory.TabIndex = 0;
            this.cmbCategory.SelectionChangeCommitted += new System.EventHandler(this.cmbManufracture_SelectionChangeCommitted);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(7, 22);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(55, 15);
            this.lblCategory.TabIndex = 90;
            this.lblCategory.Text = "Category";
            // 
            // gbItemDetails
            // 
            this.gbItemDetails.Controls.Add(this.txtOldPartNumber);
            this.gbItemDetails.Controls.Add(this.label2);
            this.gbItemDetails.Controls.Add(this.txtPartNumber);
            this.gbItemDetails.Controls.Add(this.label5);
            this.gbItemDetails.Controls.Add(this.label4);
            this.gbItemDetails.Controls.Add(this.txtDescription);
            this.gbItemDetails.Controls.Add(this.btnAddManufacturer);
            this.gbItemDetails.Controls.Add(this.btnAddUOM);
            this.gbItemDetails.Controls.Add(this.cmbManufacturer);
            this.gbItemDetails.Controls.Add(this.lblHSNCode);
            this.gbItemDetails.Controls.Add(this.cmbUOM);
            this.gbItemDetails.Controls.Add(this.label1);
            this.gbItemDetails.Controls.Add(this.label3);
            this.gbItemDetails.Controls.Add(this.txtHSNCode);
            this.gbItemDetails.Controls.Add(this.lblBatchNumber);
            this.gbItemDetails.Controls.Add(this.lblItemName);
            this.gbItemDetails.Controls.Add(this.txtBatchNumber);
            this.gbItemDetails.Controls.Add(this.txtItemName);
            this.gbItemDetails.Controls.Add(this.lblContain);
            this.gbItemDetails.Controls.Add(this.cmbCategory);
            this.gbItemDetails.Controls.Add(this.btnManufracture);
            this.gbItemDetails.Controls.Add(this.txtItemCode);
            this.gbItemDetails.Controls.Add(this.lblCategory);
            this.gbItemDetails.Location = new System.Drawing.Point(12, 30);
            this.gbItemDetails.Name = "gbItemDetails";
            this.gbItemDetails.Size = new System.Drawing.Size(500, 293);
            this.gbItemDetails.TabIndex = 0;
            this.gbItemDetails.TabStop = false;
            this.gbItemDetails.Text = "groupBox1";
            // 
            // txtOldPartNumber
            // 
            this.txtOldPartNumber.Location = new System.Drawing.Point(108, 186);
            this.txtOldPartNumber.Name = "txtOldPartNumber";
            this.txtOldPartNumber.Size = new System.Drawing.Size(357, 23);
            this.txtOldPartNumber.TabIndex = 103;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 15);
            this.label2.TabIndex = 104;
            this.label2.Text = "Old Part Number";
            // 
            // txtPartNumber
            // 
            this.txtPartNumber.Location = new System.Drawing.Point(108, 162);
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.Size = new System.Drawing.Size(357, 23);
            this.txtPartNumber.TabIndex = 101;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 15);
            this.label5.TabIndex = 102;
            this.label5.Text = "Part Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 15);
            this.label4.TabIndex = 100;
            this.label4.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(108, 66);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(357, 47);
            this.txtDescription.TabIndex = 99;
            // 
            // btnAddManufacturer
            // 
            this.btnAddManufacturer.BackColor = System.Drawing.Color.Blue;
            this.btnAddManufacturer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddManufacturer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddManufacturer.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddManufacturer.ForeColor = System.Drawing.Color.White;
            this.btnAddManufacturer.Location = new System.Drawing.Point(473, 210);
            this.btnAddManufacturer.Name = "btnAddManufacturer";
            this.btnAddManufacturer.Size = new System.Drawing.Size(20, 20);
            this.btnAddManufacturer.TabIndex = 101;
            this.btnAddManufacturer.TabStop = false;
            this.btnAddManufacturer.Text = "+";
            this.btnAddManufacturer.UseVisualStyleBackColor = false;
            this.btnAddManufacturer.Click += new System.EventHandler(this.btnAddManufacturer_Click);
            // 
            // btnAddUOM
            // 
            this.btnAddUOM.BackColor = System.Drawing.Color.Blue;
            this.btnAddUOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddUOM.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddUOM.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUOM.ForeColor = System.Drawing.Color.White;
            this.btnAddUOM.Location = new System.Drawing.Point(473, 138);
            this.btnAddUOM.Name = "btnAddUOM";
            this.btnAddUOM.Size = new System.Drawing.Size(20, 20);
            this.btnAddUOM.TabIndex = 93;
            this.btnAddUOM.TabStop = false;
            this.btnAddUOM.Text = "+";
            this.btnAddUOM.UseVisualStyleBackColor = false;
            this.btnAddUOM.Click += new System.EventHandler(this.btnAddUOM_Click);
            // 
            // cmbManufacturer
            // 
            this.cmbManufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbManufacturer.FormattingEnabled = true;
            this.cmbManufacturer.Location = new System.Drawing.Point(108, 210);
            this.cmbManufacturer.Name = "cmbManufacturer";
            this.cmbManufacturer.Size = new System.Drawing.Size(357, 23);
            this.cmbManufacturer.TabIndex = 99;
            // 
            // cmbUOM
            // 
            this.cmbUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUOM.FormattingEnabled = true;
            this.cmbUOM.Location = new System.Drawing.Point(108, 138);
            this.cmbUOM.Name = "cmbUOM";
            this.cmbUOM.Size = new System.Drawing.Size(357, 23);
            this.cmbUOM.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 100;
            this.label1.Text = "Manufacturer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 142);
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
            this.btnManufracture.Location = new System.Drawing.Point(473, 18);
            this.btnManufracture.Name = "btnManufracture";
            this.btnManufracture.Size = new System.Drawing.Size(20, 20);
            this.btnManufracture.TabIndex = 94;
            this.btnManufracture.TabStop = false;
            this.btnManufracture.Text = "+";
            this.btnManufracture.UseVisualStyleBackColor = false;
            this.btnManufracture.Click += new System.EventHandler(this.btnManufracture_Click);
            // 
            // txtOpeningStock
            // 
            this.txtOpeningStock.BackColor = System.Drawing.Color.White;
            this.txtOpeningStock.Location = new System.Drawing.Point(354, 114);
            this.txtOpeningStock.Name = "txtOpeningStock";
            this.txtOpeningStock.Size = new System.Drawing.Size(120, 23);
            this.txtOpeningStock.TabIndex = 98;
            this.txtOpeningStock.TabStop = false;
            this.txtOpeningStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblOpeningStock
            // 
            this.lblOpeningStock.AutoSize = true;
            this.lblOpeningStock.Location = new System.Drawing.Point(267, 118);
            this.lblOpeningStock.Name = "lblOpeningStock";
            this.lblOpeningStock.Size = new System.Drawing.Size(85, 15);
            this.lblOpeningStock.TabIndex = 97;
            this.lblOpeningStock.Text = "Opening Stock";
            // 
            // gbPricing
            // 
            this.gbPricing.Controls.Add(this.txtMOQ);
            this.gbPricing.Controls.Add(this.label6);
            this.gbPricing.Controls.Add(this.lblPrice);
            this.gbPricing.Controls.Add(this.txtPrice);
            this.gbPricing.Controls.Add(this.txtProfitMarginAmount);
            this.gbPricing.Controls.Add(this.lblProfitMarginAmount);
            this.gbPricing.Controls.Add(this.txtOpeningStock);
            this.gbPricing.Controls.Add(this.lblOpeningStock);
            this.gbPricing.Controls.Add(this.gbTax);
            this.gbPricing.Controls.Add(this.txtProfitMarginPer);
            this.gbPricing.Controls.Add(this.lblMRP);
            this.gbPricing.Controls.Add(this.lblProfitMarginInPer);
            this.gbPricing.Controls.Add(this.txtMRP);
            this.gbPricing.Controls.Add(this.txtCost);
            this.gbPricing.Controls.Add(this.lblCost);
            this.gbPricing.Location = new System.Drawing.Point(518, 30);
            this.gbPricing.Name = "gbPricing";
            this.gbPricing.Size = new System.Drawing.Size(500, 293);
            this.gbPricing.TabIndex = 6;
            this.gbPricing.TabStop = false;
            this.gbPricing.Text = "groupBox1";
            // 
            // txtMOQ
            // 
            this.txtMOQ.BackColor = System.Drawing.Color.White;
            this.txtMOQ.Location = new System.Drawing.Point(139, 158);
            this.txtMOQ.Name = "txtMOQ";
            this.txtMOQ.Size = new System.Drawing.Size(120, 23);
            this.txtMOQ.TabIndex = 100;
            this.txtMOQ.TabStop = false;
            this.txtMOQ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 15);
            this.label6.TabIndex = 99;
            this.label6.Text = "MOQ";
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
            this.txtPrice.TabIndex = 7;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrice_KeyDown);
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            this.txtPrice.Leave += new System.EventHandler(this.txtPrice_Leave);
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
            this.gbTax.TabIndex = 9;
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
            // Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1030, 591);
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
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblContain;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ComboBox cmbCategory;
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
        private System.Windows.Forms.TextBox txtOpeningStock;
        private System.Windows.Forms.Label lblOpeningStock;
        private System.Windows.Forms.Button btnAddManufacturer;
        private System.Windows.Forms.ComboBox cmbManufacturer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPartNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtOldPartNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMOQ;
        private System.Windows.Forms.Label label6;
    }
}