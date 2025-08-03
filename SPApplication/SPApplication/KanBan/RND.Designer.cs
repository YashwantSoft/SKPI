namespace SPApplication.KanBan
{
    partial class RND
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvP = new System.Windows.Forms.DataGridView();
            this.cmbShiftCombo = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmPType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmPName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmProdctionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDispatchDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmProductionStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmTest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbProductName = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvP
            // 
            this.dgvP.AllowUserToDeleteRows = false;
            this.dgvP.AllowUserToResizeColumns = false;
            this.dgvP.AllowUserToResizeRows = false;
            this.dgvP.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvP.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cmbShiftCombo,
            this.clmPType,
            this.clmPName,
            this.clmProdctionDate,
            this.clmDispatchDate,
            this.clmProductionStatus});
            this.dgvP.GridColor = System.Drawing.Color.White;
            this.dgvP.Location = new System.Drawing.Point(12, 12);
            this.dgvP.Name = "dgvP";
            this.dgvP.RowHeadersVisible = false;
            this.dgvP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvP.Size = new System.Drawing.Size(936, 108);
            this.dgvP.TabIndex = 11552;
            this.dgvP.TabStop = false;
            this.dgvP.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvP_EditingControlShowing);
            // 
            // cmbShiftCombo
            // 
            this.cmbShiftCombo.HeaderText = "Column1";
            this.cmbShiftCombo.Name = "cmbShiftCombo";
            this.cmbShiftCombo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clmPType
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmPType.DefaultCellStyle = dataGridViewCellStyle8;
            this.clmPType.HeaderText = "Product Type";
            this.clmPType.Items.AddRange(new object[] {
            "Product",
            "Cap",
            "Wad"});
            this.clmPType.Name = "clmPType";
            this.clmPType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmPType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmPType.Width = 200;
            // 
            // clmPName
            // 
            this.clmPName.DataPropertyName = "ProductName";
            this.clmPName.HeaderText = "Product Name";
            this.clmPName.Name = "clmPName";
            this.clmPName.Width = 500;
            // 
            // clmProdctionDate
            // 
            this.clmProdctionDate.HeaderText = "Production Date";
            this.clmProdctionDate.Name = "clmProdctionDate";
            // 
            // clmDispatchDate
            // 
            this.clmDispatchDate.HeaderText = "Dispatch Date";
            this.clmDispatchDate.Name = "clmDispatchDate";
            // 
            // clmProductionStatus
            // 
            this.clmProductionStatus.HeaderText = "Production Status";
            this.clmProductionStatus.Name = "clmProductionStatus";
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(1036, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 33);
            this.btnAdd.TabIndex = 11553;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductName,
            this.Price,
            this.Category,
            this.clmTest});
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(12, 126);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(936, 108);
            this.dataGridView1.TabIndex = 11554;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "Name";
            this.ProductName.HeaderText = "Name";
            this.ProductName.Name = "ProductName";
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            // 
            // Category
            // 
            this.Category.DataPropertyName = "CategoryID";
            this.Category.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            // 
            // clmTest
            // 
            this.clmTest.HeaderText = "TestID";
            this.clmTest.Name = "clmTest";
            // 
            // cmbProductName
            // 
            this.cmbProductName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProductName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProductName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductName.FormattingEnabled = true;
            this.cmbProductName.Location = new System.Drawing.Point(230, 297);
            this.cmbProductName.Name = "cmbProductName";
            this.cmbProductName.Size = new System.Drawing.Size(326, 21);
            this.cmbProductName.TabIndex = 11555;
            this.cmbProductName.TextChanged += new System.EventHandler(this.cmbProductName_TextChanged);
            // 
            // RND
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 472);
            this.Controls.Add(this.cmbProductName);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvP);
            this.Name = "RND";
            this.Text = "RND";
            this.Load += new System.EventHandler(this.RND_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvP;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmbShiftCombo;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmPType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmProdctionDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDispatchDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmProductionStatus;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewComboBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTest;
        private System.Windows.Forms.ComboBox cmbProductName;
    }
}