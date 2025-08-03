namespace SPApplication.KanBan
{
    partial class KB_PurchaseOrder
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
            this.lbDate = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbpurchasesList = new System.Windows.Forms.Label();
            this.lbPurchasesQuantity = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtPartyName = new System.Windows.Forms.TextBox();
            this.lbPurchaseList = new System.Windows.Forms.ListBox();
            this.txtPurchaseQuantity = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Location = new System.Drawing.Point(40, 40);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(32, 15);
            this.lbDate.TabIndex = 0;
            this.lbDate.Text = "Date";
            this.lbDate.Click += new System.EventHandler(this.lbDate_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Blue;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(1, 2);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(880, 32);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Purshases Order";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Party Name";
            // 
            // lbpurchasesList
            // 
            this.lbpurchasesList.AutoSize = true;
            this.lbpurchasesList.Location = new System.Drawing.Point(392, 40);
            this.lbpurchasesList.Name = "lbpurchasesList";
            this.lbpurchasesList.Size = new System.Drawing.Size(83, 15);
            this.lbpurchasesList.TabIndex = 3;
            this.lbpurchasesList.Text = "Purchase List ";
            // 
            // lbPurchasesQuantity
            // 
            this.lbPurchasesQuantity.AutoSize = true;
            this.lbPurchasesQuantity.Location = new System.Drawing.Point(185, 43);
            this.lbPurchasesQuantity.Name = "lbPurchasesQuantity";
            this.lbPurchasesQuantity.Size = new System.Drawing.Size(114, 15);
            this.lbPurchasesQuantity.TabIndex = 4;
            this.lbPurchasesQuantity.Text = "Purchases Quantity";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(92, 40);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(89, 23);
            this.dtpDate.TabIndex = 5;
            // 
            // txtPartyName
            // 
            this.txtPartyName.Location = new System.Drawing.Point(90, 67);
            this.txtPartyName.Name = "txtPartyName";
            this.txtPartyName.Size = new System.Drawing.Size(296, 23);
            this.txtPartyName.TabIndex = 6;
            // 
            // lbPurchaseList
            // 
            this.lbPurchaseList.FormattingEnabled = true;
            this.lbPurchaseList.ItemHeight = 15;
            this.lbPurchaseList.Location = new System.Drawing.Point(481, 40);
            this.lbPurchaseList.Name = "lbPurchaseList";
            this.lbPurchaseList.Size = new System.Drawing.Size(333, 49);
            this.lbPurchaseList.TabIndex = 7;
            // 
            // txtPurchaseQuantity
            // 
            this.txtPurchaseQuantity.Location = new System.Drawing.Point(301, 40);
            this.txtPurchaseQuantity.Name = "txtPurchaseQuantity";
            this.txtPurchaseQuantity.Size = new System.Drawing.Size(85, 23);
            this.txtPurchaseQuantity.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(320, 92);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(398, 92);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(476, 92);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(554, 92);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Exit ";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-3, 128);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(884, 426);
            this.dataGridView1.TabIndex = 14;
            // 
            // KB_PurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 566);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPurchaseQuantity);
            this.Controls.Add(this.lbPurchaseList);
            this.Controls.Add(this.txtPartyName);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lbPurchasesQuantity);
            this.Controls.Add(this.lbpurchasesList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lbDate);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "KB_PurchaseOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDate;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbpurchasesList;
        private System.Windows.Forms.Label lbPurchasesQuantity;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtPartyName;
        private System.Windows.Forms.ListBox lbPurchaseList;
        private System.Windows.Forms.TextBox txtPurchaseQuantity;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}