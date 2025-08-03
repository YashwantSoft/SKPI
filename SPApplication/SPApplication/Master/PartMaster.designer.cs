namespace SPApplication
{
    partial class PartMaster
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
            this.txtHSNCode = new System.Windows.Forms.TextBox();
            this.lblHSNCode = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearchItem = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtPartName = new System.Windows.Forms.TextBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.cmbUseFor = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.btnAddUsedFor = new System.Windows.Forms.Button();
            this.btnAddUOM = new System.Windows.Forms.Button();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOpeningStock = new System.Windows.Forms.TextBox();
            this.lblOpeningStock = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddSupplierName = new System.Windows.Forms.Button();
            this.cmbSupplierName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddPlace = new System.Windows.Forms.Button();
            this.cmbPlace = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtHSNCode
            // 
            this.txtHSNCode.Location = new System.Drawing.Point(119, 152);
            this.txtHSNCode.Name = "txtHSNCode";
            this.txtHSNCode.Size = new System.Drawing.Size(357, 23);
            this.txtHSNCode.TabIndex = 4;
            this.txtHSNCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHSNCode_KeyDown);
            // 
            // lblHSNCode
            // 
            this.lblHSNCode.AutoSize = true;
            this.lblHSNCode.Location = new System.Drawing.Point(26, 156);
            this.lblHSNCode.Name = "lblHSNCode";
            this.lblHSNCode.Size = new System.Drawing.Size(90, 15);
            this.lblHSNCode.TabIndex = 79;
            this.lblHSNCode.Text = "HSN Code (GST)";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(16, 275);
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
            this.dataGridView1.Location = new System.Drawing.Point(13, 292);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1178, 400);
            this.dataGridView1.TabIndex = 73;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(947, 261);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(218, 23);
            this.txtSearch.TabIndex = 13;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearchItem
            // 
            this.lblSearchItem.AutoSize = true;
            this.lblSearchItem.Location = new System.Drawing.Point(901, 265);
            this.lblSearchItem.Name = "lblSearchItem";
            this.lblSearchItem.Size = new System.Drawing.Size(44, 15);
            this.lblSearchItem.TabIndex = 77;
            this.lblSearchItem.Text = "Search";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(605, 256);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(525, 256);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 10;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtPartName
            // 
            this.txtPartName.Location = new System.Drawing.Point(119, 56);
            this.txtPartName.Name = "txtPartName";
            this.txtPartName.Size = new System.Drawing.Size(1046, 23);
            this.txtPartName.TabIndex = 0;
            this.txtPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(26, 60);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(64, 15);
            this.lblItemName.TabIndex = 71;
            this.lblItemName.Text = "Part Name";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(445, 256);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 9;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(685, 256);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 12;
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
            this.lblHeader.Size = new System.Drawing.Size(1204, 29);
            this.lblHeader.TabIndex = 68;
            this.lblHeader.Text = "Equipment / Part Master";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbUseFor
            // 
            this.cmbUseFor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbUseFor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUseFor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUseFor.FormattingEnabled = true;
            this.cmbUseFor.Location = new System.Drawing.Point(119, 176);
            this.cmbUseFor.Name = "cmbUseFor";
            this.cmbUseFor.Size = new System.Drawing.Size(357, 23);
            this.cmbUseFor.TabIndex = 5;
            this.cmbUseFor.SelectionChangeCommitted += new System.EventHandler(this.cmbManufracture_SelectionChangeCommitted);
            this.cmbUseFor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUseFor_KeyDown);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(26, 180);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(55, 15);
            this.lblCategory.TabIndex = 90;
            this.lblCategory.Text = "Used For";
            // 
            // btnAddUsedFor
            // 
            this.btnAddUsedFor.BackColor = System.Drawing.Color.Blue;
            this.btnAddUsedFor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddUsedFor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddUsedFor.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUsedFor.ForeColor = System.Drawing.Color.White;
            this.btnAddUsedFor.Location = new System.Drawing.Point(484, 177);
            this.btnAddUsedFor.Name = "btnAddUsedFor";
            this.btnAddUsedFor.Size = new System.Drawing.Size(20, 20);
            this.btnAddUsedFor.TabIndex = 94;
            this.btnAddUsedFor.TabStop = false;
            this.btnAddUsedFor.Text = "+";
            this.btnAddUsedFor.UseVisualStyleBackColor = false;
            this.btnAddUsedFor.Click += new System.EventHandler(this.btnAddUsedFor_Click);
            // 
            // btnAddUOM
            // 
            this.btnAddUOM.BackColor = System.Drawing.Color.Blue;
            this.btnAddUOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddUOM.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddUOM.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUOM.ForeColor = System.Drawing.Color.White;
            this.btnAddUOM.Location = new System.Drawing.Point(223, 105);
            this.btnAddUOM.Name = "btnAddUOM";
            this.btnAddUOM.Size = new System.Drawing.Size(20, 20);
            this.btnAddUOM.TabIndex = 93;
            this.btnAddUOM.TabStop = false;
            this.btnAddUOM.Text = "+";
            this.btnAddUOM.UseVisualStyleBackColor = false;
            this.btnAddUOM.Click += new System.EventHandler(this.btnAddUOM_Click);
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(119, 104);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(98, 23);
            this.cmbUnit.TabIndex = 2;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 15);
            this.label3.TabIndex = 92;
            this.label3.Text = "Unit";
            // 
            // txtOpeningStock
            // 
            this.txtOpeningStock.BackColor = System.Drawing.Color.White;
            this.txtOpeningStock.Location = new System.Drawing.Point(119, 224);
            this.txtOpeningStock.Name = "txtOpeningStock";
            this.txtOpeningStock.Size = new System.Drawing.Size(98, 23);
            this.txtOpeningStock.TabIndex = 7;
            this.txtOpeningStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOpeningStock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOpeningStock_KeyDown);
            this.txtOpeningStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOpeningStock_KeyPress);
            // 
            // lblOpeningStock
            // 
            this.lblOpeningStock.AutoSize = true;
            this.lblOpeningStock.Location = new System.Drawing.Point(26, 228);
            this.lblOpeningStock.Name = "lblOpeningStock";
            this.lblOpeningStock.Size = new System.Drawing.Size(85, 15);
            this.lblOpeningStock.TabIndex = 97;
            this.lblOpeningStock.Text = "Opening Stock";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(1067, 32);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(98, 23);
            this.txtID.TabIndex = 95;
            this.txtID.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(973, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 96;
            this.label1.Text = "Part No.";
            // 
            // btnAddSupplierName
            // 
            this.btnAddSupplierName.BackColor = System.Drawing.Color.Blue;
            this.btnAddSupplierName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddSupplierName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddSupplierName.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSupplierName.ForeColor = System.Drawing.Color.White;
            this.btnAddSupplierName.Location = new System.Drawing.Point(484, 129);
            this.btnAddSupplierName.Name = "btnAddSupplierName";
            this.btnAddSupplierName.Size = new System.Drawing.Size(20, 20);
            this.btnAddSupplierName.TabIndex = 99;
            this.btnAddSupplierName.TabStop = false;
            this.btnAddSupplierName.Text = "+";
            this.btnAddSupplierName.UseVisualStyleBackColor = false;
            this.btnAddSupplierName.Click += new System.EventHandler(this.btnAddSupplierName_Click);
            // 
            // cmbSupplierName
            // 
            this.cmbSupplierName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSupplierName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSupplierName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplierName.FormattingEnabled = true;
            this.cmbSupplierName.Location = new System.Drawing.Point(119, 128);
            this.cmbSupplierName.Name = "cmbSupplierName";
            this.cmbSupplierName.Size = new System.Drawing.Size(357, 23);
            this.cmbSupplierName.TabIndex = 3;
            this.cmbSupplierName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSupplierName_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 98;
            this.label2.Text = "Supplier Name";
            // 
            // btnAddPlace
            // 
            this.btnAddPlace.BackColor = System.Drawing.Color.Blue;
            this.btnAddPlace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddPlace.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddPlace.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPlace.ForeColor = System.Drawing.Color.White;
            this.btnAddPlace.Location = new System.Drawing.Point(484, 201);
            this.btnAddPlace.Name = "btnAddPlace";
            this.btnAddPlace.Size = new System.Drawing.Size(20, 20);
            this.btnAddPlace.TabIndex = 102;
            this.btnAddPlace.TabStop = false;
            this.btnAddPlace.Text = "+";
            this.btnAddPlace.UseVisualStyleBackColor = false;
            this.btnAddPlace.Click += new System.EventHandler(this.btnAddPlace_Click);
            // 
            // cmbPlace
            // 
            this.cmbPlace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPlace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPlace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlace.FormattingEnabled = true;
            this.cmbPlace.Location = new System.Drawing.Point(119, 200);
            this.cmbPlace.Name = "cmbPlace";
            this.cmbPlace.Size = new System.Drawing.Size(357, 23);
            this.cmbPlace.TabIndex = 6;
            this.cmbPlace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPlace_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.TabIndex = 101;
            this.label4.Text = "Place Rack/Box";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Active",
            "Renewal",
            "Inactive"});
            this.cmbStatus.Location = new System.Drawing.Point(378, 224);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(98, 23);
            this.cmbStatus.TabIndex = 8;
            this.cmbStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbStatus_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(335, 228);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 104;
            this.label5.Text = "Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 15);
            this.label6.TabIndex = 106;
            this.label6.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(119, 80);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(1046, 23);
            this.txtDescription.TabIndex = 1;
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(119, 32);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(178, 23);
            this.cmbDepartment.TabIndex = 0;
            this.cmbDepartment.SelectionChangeCommitted += new System.EventHandler(this.cmbDepartment_SelectionChangeCommitted);
            this.cmbDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDepartment_KeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(27, 36);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(71, 15);
            this.label21.TabIndex = 11568;
            this.label21.Text = "Department";
            // 
            // PartMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1204, 698);
            this.ControlBox = false;
            this.Controls.Add(this.cmbDepartment);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnAddPlace);
            this.Controls.Add(this.cmbPlace);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOpeningStock);
            this.Controls.Add(this.lblOpeningStock);
            this.Controls.Add(this.btnAddSupplierName);
            this.Controls.Add(this.cmbSupplierName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddUsedFor);
            this.Controls.Add(this.btnAddUOM);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmbUseFor);
            this.Controls.Add(this.lblItemName);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblSearchItem);
            this.Controls.Add(this.txtPartName);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblHSNCode);
            this.Controls.Add(this.txtHSNCode);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PartMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHSNCode;
        private System.Windows.Forms.Label lblHSNCode;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearchItem;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtPartName;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ComboBox cmbUseFor;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Button btnAddUOM;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddUsedFor;
        private System.Windows.Forms.TextBox txtOpeningStock;
        private System.Windows.Forms.Label lblOpeningStock;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddSupplierName;
        private System.Windows.Forms.ComboBox cmbSupplierName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddPlace;
        private System.Windows.Forms.ComboBox cmbPlace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label label21;
    }
}