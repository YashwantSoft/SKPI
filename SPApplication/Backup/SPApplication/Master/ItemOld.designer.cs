namespace SPApplication
{
    partial class ItemOld
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbUOM = new System.Windows.Forms.ComboBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearchItemName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.btnAddUOM = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtHSNCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProfitMargin = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMRP = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIGSTPer = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSGSTPer = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCGSTPer = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtProfitMarginAmount = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(1, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(896, 29);
            this.lblHeader.TabIndex = 12;
            this.lblHeader.Text = "Item";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "Item Name";
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(338, 32);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(422, 23);
            this.txtItemName.TabIndex = 0;
            this.txtItemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemName_KeyDown);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(338, 104);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(422, 49);
            this.txtDescription.TabIndex = 1;
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Item Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 15);
            this.label3.TabIndex = 19;
            this.label3.Text = "Unit of Messurement";
            // 
            // cmbUOM
            // 
            this.cmbUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUOM.FormattingEnabled = true;
            this.cmbUOM.Location = new System.Drawing.Point(338, 154);
            this.cmbUOM.Name = "cmbUOM";
            this.cmbUOM.Size = new System.Drawing.Size(121, 23);
            this.cmbUOM.TabIndex = 2;
            this.cmbUOM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUOM_KeyDown);
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(338, 202);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(121, 23);
            this.txtPrice.TabIndex = 3;
            this.txtPrice.TextChanged += new System.EventHandler(this.txtPrice_TextChanged);
            this.txtPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrice_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(215, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 15);
            this.label4.TabIndex = 21;
            this.label4.Text = "Price";
            // 
            // txtSearchItemName
            // 
            this.txtSearchItemName.Location = new System.Drawing.Point(124, 312);
            this.txtSearchItemName.Name = "txtSearchItemName";
            this.txtSearchItemName.Size = new System.Drawing.Size(627, 23);
            this.txtSearchItemName.TabIndex = 8;
            this.txtSearchItemName.TextChanged += new System.EventHandler(this.txtSearchItemName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 315);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 15);
            this.label5.TabIndex = 26;
            this.label5.Text = "Search Item Name";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Info;
            this.dataGridView1.Location = new System.Drawing.Point(12, 339);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(874, 254);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(757, 320);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(73, 15);
            this.lblTotalCount.TabIndex = 30;
            this.lblTotalCount.Text = "Total Count-";
            // 
            // btnAddUOM
            // 
            this.btnAddUOM.BackColor = System.Drawing.Color.Blue;
            this.btnAddUOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddUOM.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddUOM.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUOM.ForeColor = System.Drawing.Color.White;
            this.btnAddUOM.Location = new System.Drawing.Point(475, 155);
            this.btnAddUOM.Name = "btnAddUOM";
            this.btnAddUOM.Size = new System.Drawing.Size(20, 20);
            this.btnAddUOM.TabIndex = 28;
            this.btnAddUOM.Text = "+";
            this.btnAddUOM.UseVisualStyleBackColor = false;
            this.btnAddUOM.Click += new System.EventHandler(this.btnAddUOM_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(455, 277);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(364, 277);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 5;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(273, 277);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(546, 277);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 30);
            this.btnExit.TabIndex = 7;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtHSNCode
            // 
            this.txtHSNCode.Location = new System.Drawing.Point(338, 80);
            this.txtHSNCode.Name = "txtHSNCode";
            this.txtHSNCode.Size = new System.Drawing.Size(422, 23);
            this.txtHSNCode.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(215, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 32;
            this.label6.Text = "HSN Code (GST)";
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(338, 56);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(422, 23);
            this.txtItemCode.TabIndex = 33;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(215, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 15);
            this.label7.TabIndex = 34;
            this.label7.Text = "Item Code";
            // 
            // txtCost
            // 
            this.txtCost.Location = new System.Drawing.Point(338, 178);
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(121, 23);
            this.txtCost.TabIndex = 35;
            this.txtCost.TextChanged += new System.EventHandler(this.txtCost_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(215, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 15);
            this.label8.TabIndex = 36;
            this.label8.Text = "Cost";
            // 
            // txtProfitMargin
            // 
            this.txtProfitMargin.BackColor = System.Drawing.Color.White;
            this.txtProfitMargin.Location = new System.Drawing.Point(338, 250);
            this.txtProfitMargin.Name = "txtProfitMargin";
            this.txtProfitMargin.ReadOnly = true;
            this.txtProfitMargin.Size = new System.Drawing.Size(121, 23);
            this.txtProfitMargin.TabIndex = 37;
            this.txtProfitMargin.TabStop = false;
            this.txtProfitMargin.TextChanged += new System.EventHandler(this.txtProfitMargin_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(215, 254);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 15);
            this.label9.TabIndex = 38;
            this.label9.Text = "Profit Margin In %";
            // 
            // txtMRP
            // 
            this.txtMRP.Location = new System.Drawing.Point(338, 226);
            this.txtMRP.Name = "txtMRP";
            this.txtMRP.Size = new System.Drawing.Size(121, 23);
            this.txtMRP.TabIndex = 39;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(215, 230);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 15);
            this.label10.TabIndex = 40;
            this.label10.Text = "MRP";
            // 
            // txtIGSTPer
            // 
            this.txtIGSTPer.Location = new System.Drawing.Point(639, 246);
            this.txtIGSTPer.Name = "txtIGSTPer";
            this.txtIGSTPer.Size = new System.Drawing.Size(121, 23);
            this.txtIGSTPer.TabIndex = 45;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(512, 249);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 15);
            this.label11.TabIndex = 46;
            this.label11.Text = "IGST In %";
            // 
            // txtSGSTPer
            // 
            this.txtSGSTPer.Location = new System.Drawing.Point(639, 222);
            this.txtSGSTPer.Name = "txtSGSTPer";
            this.txtSGSTPer.Size = new System.Drawing.Size(121, 23);
            this.txtSGSTPer.TabIndex = 43;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(512, 225);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 15);
            this.label12.TabIndex = 44;
            this.label12.Text = "SGST In %";
            // 
            // txtCGSTPer
            // 
            this.txtCGSTPer.Location = new System.Drawing.Point(639, 198);
            this.txtCGSTPer.Name = "txtCGSTPer";
            this.txtCGSTPer.Size = new System.Drawing.Size(121, 23);
            this.txtCGSTPer.TabIndex = 41;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(512, 201);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 15);
            this.label13.TabIndex = 42;
            this.label13.Text = "CGST In %";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(512, 177);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(125, 15);
            this.label14.TabIndex = 47;
            this.label14.Text = "Profit Margin Amount";
            // 
            // txtProfitMarginAmount
            // 
            this.txtProfitMarginAmount.BackColor = System.Drawing.Color.White;
            this.txtProfitMarginAmount.Location = new System.Drawing.Point(639, 174);
            this.txtProfitMarginAmount.Name = "txtProfitMarginAmount";
            this.txtProfitMarginAmount.ReadOnly = true;
            this.txtProfitMarginAmount.Size = new System.Drawing.Size(121, 23);
            this.txtProfitMarginAmount.TabIndex = 48;
            this.txtProfitMarginAmount.TabStop = false;
            // 
            // ItemOld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(898, 598);
            this.ControlBox = false;
            this.Controls.Add(this.txtProfitMarginAmount);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtIGSTPer);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSGSTPer);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtCGSTPer);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtMRP);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtProfitMargin);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCost);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtHSNCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnAddUOM);
            this.Controls.Add(this.txtSearchItemName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbUOM);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ItemOld";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ItemMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbUOM;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtSearchItemName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddUOM;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.TextBox txtHSNCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCost;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProfitMargin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMRP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIGSTPer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSGSTPer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCGSTPer;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtProfitMarginAmount;
    }
}