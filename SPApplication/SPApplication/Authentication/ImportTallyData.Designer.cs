namespace SPApplication.Authentication
{
    partial class ImportTallyData
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnBrowseExcelFile = new System.Windows.Forms.Button();
            this.txtExcelFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.clmSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStatusNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblTotalNewArrivalProductCount = new System.Windows.Forms.Label();
            this.lblTotalExistCount = new System.Windows.Forms.Label();
            this.lblExistingItem = new System.Windows.Forms.Label();
            this.lblNewItem = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.cmbImportType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Blue;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1013, 30);
            this.lblHeader.TabIndex = 11186;
            this.lblHeader.Text = "Import Data From Tally Software";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(916, 568);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 11187;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnBrowseExcelFile
            // 
            this.btnBrowseExcelFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBrowseExcelFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrowseExcelFile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseExcelFile.Location = new System.Drawing.Point(778, 35);
            this.btnBrowseExcelFile.Name = "btnBrowseExcelFile";
            this.btnBrowseExcelFile.Size = new System.Drawing.Size(132, 30);
            this.btnBrowseExcelFile.TabIndex = 11188;
            this.btnBrowseExcelFile.Text = "Browse Excel File";
            this.btnBrowseExcelFile.UseVisualStyleBackColor = true;
            this.btnBrowseExcelFile.Click += new System.EventHandler(this.btnBrowseExcelFile_Click);
            // 
            // txtExcelFilePath
            // 
            this.txtExcelFilePath.BackColor = System.Drawing.Color.White;
            this.txtExcelFilePath.Location = new System.Drawing.Point(112, 68);
            this.txtExcelFilePath.Name = "txtExcelFilePath";
            this.txtExcelFilePath.ReadOnly = true;
            this.txtExcelFilePath.Size = new System.Drawing.Size(879, 23);
            this.txtExcelFilePath.TabIndex = 11360;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 15);
            this.label1.TabIndex = 11361;
            this.label1.Text = "Excel File Path";
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.AllowUserToDeleteRows = false;
            this.dgvProduct.AllowUserToResizeColumns = false;
            this.dgvProduct.AllowUserToResizeRows = false;
            this.dgvProduct.BackgroundColor = System.Drawing.Color.White;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSrNo,
            this.clmItemId,
            this.clmType,
            this.clmProductName,
            this.clmStatus,
            this.clmStatusNo});
            this.dgvProduct.GridColor = System.Drawing.Color.White;
            this.dgvProduct.Location = new System.Drawing.Point(6, 123);
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.ReadOnly = true;
            this.dgvProduct.RowHeadersVisible = false;
            this.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduct.Size = new System.Drawing.Size(999, 417);
            this.dgvProduct.TabIndex = 11408;
            this.dgvProduct.TabStop = false;
            // 
            // clmSrNo
            // 
            this.clmSrNo.HeaderText = "Sr.No";
            this.clmSrNo.Name = "clmSrNo";
            this.clmSrNo.ReadOnly = true;
            this.clmSrNo.Width = 60;
            // 
            // clmItemId
            // 
            this.clmItemId.HeaderText = "Product ID";
            this.clmItemId.Name = "clmItemId";
            this.clmItemId.ReadOnly = true;
            // 
            // clmType
            // 
            this.clmType.HeaderText = "Type";
            this.clmType.Name = "clmType";
            this.clmType.ReadOnly = true;
            // 
            // clmProductName
            // 
            this.clmProductName.HeaderText = "Product Name";
            this.clmProductName.Name = "clmProductName";
            this.clmProductName.ReadOnly = true;
            this.clmProductName.Width = 600;
            // 
            // clmStatus
            // 
            this.clmStatus.HeaderText = "Status";
            this.clmStatus.Name = "clmStatus";
            this.clmStatus.ReadOnly = true;
            // 
            // clmStatusNo
            // 
            this.clmStatusNo.HeaderText = "StatusNo";
            this.clmStatusNo.Name = "clmStatusNo";
            this.clmStatusNo.ReadOnly = true;
            this.clmStatusNo.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(835, 568);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 11409;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(916, 35);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 11410;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblTotalNewArrivalProductCount
            // 
            this.lblTotalNewArrivalProductCount.AutoSize = true;
            this.lblTotalNewArrivalProductCount.BackColor = System.Drawing.Color.Lime;
            this.lblTotalNewArrivalProductCount.Location = new System.Drawing.Point(9, 101);
            this.lblTotalNewArrivalProductCount.Name = "lblTotalNewArrivalProductCount";
            this.lblTotalNewArrivalProductCount.Size = new System.Drawing.Size(156, 15);
            this.lblTotalNewArrivalProductCount.TabIndex = 11411;
            this.lblTotalNewArrivalProductCount.Text = "New Arrival Product Count-";
            // 
            // lblTotalExistCount
            // 
            this.lblTotalExistCount.AutoSize = true;
            this.lblTotalExistCount.BackColor = System.Drawing.Color.Pink;
            this.lblTotalExistCount.Location = new System.Drawing.Point(261, 101);
            this.lblTotalExistCount.Name = "lblTotalExistCount";
            this.lblTotalExistCount.Size = new System.Drawing.Size(69, 15);
            this.lblTotalExistCount.TabIndex = 11412;
            this.lblTotalExistCount.Text = "Total Count";
            // 
            // lblExistingItem
            // 
            this.lblExistingItem.BackColor = System.Drawing.Color.Pink;
            this.lblExistingItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblExistingItem.Location = new System.Drawing.Point(625, 98);
            this.lblExistingItem.Name = "lblExistingItem";
            this.lblExistingItem.Size = new System.Drawing.Size(20, 20);
            this.lblExistingItem.TabIndex = 11437;
            this.lblExistingItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblExistingItem.Click += new System.EventHandler(this.lblJarInformation_Click);
            // 
            // lblNewItem
            // 
            this.lblNewItem.BackColor = System.Drawing.Color.Lime;
            this.lblNewItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNewItem.Location = new System.Drawing.Point(472, 98);
            this.lblNewItem.Name = "lblNewItem";
            this.lblNewItem.Size = new System.Drawing.Size(20, 20);
            this.lblNewItem.TabIndex = 11436;
            this.lblNewItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(494, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 15);
            this.label4.TabIndex = 11439;
            this.label4.Text = "New Arrival Product";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(647, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 11441;
            this.label6.Text = "Exist";
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(756, 568);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 11442;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "openFileDialog1";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.BackColor = System.Drawing.Color.White;
            this.lblTotalCount.Location = new System.Drawing.Point(888, 101);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(69, 15);
            this.lblTotalCount.TabIndex = 11444;
            this.lblTotalCount.Text = "Total Count";
            // 
            // cmbImportType
            // 
            this.cmbImportType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbImportType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbImportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImportType.FormattingEnabled = true;
            this.cmbImportType.Items.AddRange(new object[] {
            "Preform",
            "Product",
            "Cap",
            "Wad",
            "Customer",
            "Supplier",
            "Other Material"});
            this.cmbImportType.Location = new System.Drawing.Point(112, 43);
            this.cmbImportType.Name = "cmbImportType";
            this.cmbImportType.Size = new System.Drawing.Size(218, 23);
            this.cmbImportType.TabIndex = 11445;
            this.cmbImportType.SelectionChangeCommitted += new System.EventHandler(this.cmbImportType_SelectionChangeCommitted);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 15);
            this.label11.TabIndex = 11446;
            this.label11.Text = "Import Data Type";
            // 
            // ImportTallyData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1011, 601);
            this.ControlBox = false;
            this.Controls.Add(this.cmbImportType);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblExistingItem);
            this.Controls.Add(this.lblNewItem);
            this.Controls.Add(this.lblTotalExistCount);
            this.Controls.Add(this.lblTotalNewArrivalProductCount);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvProduct);
            this.Controls.Add(this.txtExcelFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowseExcelFile);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ImportTallyData";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TallyData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnBrowseExcelFile;
        private System.Windows.Forms.TextBox txtExcelFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblTotalNewArrivalProductCount;
        private System.Windows.Forms.Label lblTotalExistCount;
        private System.Windows.Forms.Label lblExistingItem;
        private System.Windows.Forms.Label lblNewItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStatusNo;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.ComboBox cmbImportType;
        private System.Windows.Forms.Label label11;
    }
}