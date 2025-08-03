namespace SPApplication.Master
{
    partial class Customer
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
            this.txtEmailId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGSTNumber = new System.Windows.Forms.TextBox();
            this.txtMobileNumber = new System.Windows.Forms.TextBox();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStateCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblAadharCard = new System.Windows.Forms.Label();
            this.txtAadharCard = new System.Windows.Forms.TextBox();
            this.lblPANCard = new System.Windows.Forms.Label();
            this.txtPANCard = new System.Windows.Forms.TextBox();
            this.lblDrivingLisence = new System.Windows.Forms.Label();
            this.txtDrivingLicence = new System.Windows.Forms.TextBox();
            this.cmbCity = new System.Windows.Forms.ComboBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.btnAddCity = new System.Windows.Forms.Button();
            this.txtCCMailIDList = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEmailId
            // 
            this.txtEmailId.Location = new System.Drawing.Point(134, 177);
            this.txtEmailId.Multiline = true;
            this.txtEmailId.Name = "txtEmailId";
            this.txtEmailId.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEmailId.Size = new System.Drawing.Size(380, 81);
            this.txtEmailId.TabIndex = 3;
            this.txtEmailId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmailId_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 68;
            this.label3.Text = "To Email Id";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(15, 284);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(73, 15);
            this.lblTotalCount.TabIndex = 67;
            this.lblTotalCount.Text = "Total Count-";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(13, 301);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1044, 330);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(846, 276);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(211, 23);
            this.txtSearch.TabIndex = 13;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(745, 279);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 15);
            this.label5.TabIndex = 66;
            this.label5.Text = "Search Customer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 15);
            this.label4.TabIndex = 65;
            this.label4.Text = "Mobile Number";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(134, 83);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(380, 45);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 64;
            this.label2.Text = "Address";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(134, 59);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(380, 23);
            this.txtCustomerName.TabIndex = 0;
            this.txtCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 63;
            this.label1.Text = "Customer Name";
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(1, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1069, 28);
            this.lblHeader.TabIndex = 62;
            this.lblHeader.Text = "T and T Infra Limited";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(537, 265);
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
            this.btnClear.Location = new System.Drawing.Point(458, 265);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 10;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(379, 265);
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
            this.btnExit.Location = new System.Drawing.Point(616, 265);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 12;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(562, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 15);
            this.label6.TabIndex = 70;
            this.label6.Text = "GSTIN Number";
            // 
            // txtGSTNumber
            // 
            this.txtGSTNumber.Location = new System.Drawing.Point(651, 105);
            this.txtGSTNumber.MaxLength = 15;
            this.txtGSTNumber.Name = "txtGSTNumber";
            this.txtGSTNumber.Size = new System.Drawing.Size(380, 23);
            this.txtGSTNumber.TabIndex = 7;
            this.txtGSTNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGSTNumber_KeyDown);
            this.txtGSTNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGSTNumber_KeyPress);
            // 
            // txtMobileNumber
            // 
            this.txtMobileNumber.Location = new System.Drawing.Point(134, 153);
            this.txtMobileNumber.MaxLength = 2000;
            this.txtMobileNumber.Name = "txtMobileNumber";
            this.txtMobileNumber.Size = new System.Drawing.Size(380, 23);
            this.txtMobileNumber.TabIndex = 2;
            this.txtMobileNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContactNumber_KeyDown);
            this.txtMobileNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobileNumber_KeyPress);
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.BackColor = System.Drawing.Color.White;
            this.txtCustomerCode.Location = new System.Drawing.Point(134, 35);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.ReadOnly = true;
            this.txtCustomerCode.Size = new System.Drawing.Size(380, 23);
            this.txtCustomerCode.TabIndex = 0;
            this.txtCustomerCode.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(40, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 15);
            this.label8.TabIndex = 78;
            this.label8.Text = "Customer No.";
            // 
            // txtStateCode
            // 
            this.txtStateCode.Location = new System.Drawing.Point(651, 129);
            this.txtStateCode.MaxLength = 3;
            this.txtStateCode.Name = "txtStateCode";
            this.txtStateCode.Size = new System.Drawing.Size(380, 23);
            this.txtStateCode.TabIndex = 8;
            this.txtStateCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStateCode_KeyDown);
            this.txtStateCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStateCode_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(562, 132);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 15);
            this.label10.TabIndex = 80;
            this.label10.Text = "State Code";
            // 
            // lblAadharCard
            // 
            this.lblAadharCard.AutoSize = true;
            this.lblAadharCard.Location = new System.Drawing.Point(562, 36);
            this.lblAadharCard.Name = "lblAadharCard";
            this.lblAadharCard.Size = new System.Drawing.Size(76, 15);
            this.lblAadharCard.TabIndex = 81;
            this.lblAadharCard.Text = "Aadhar Card";
            // 
            // txtAadharCard
            // 
            this.txtAadharCard.Location = new System.Drawing.Point(651, 33);
            this.txtAadharCard.MaxLength = 12;
            this.txtAadharCard.Name = "txtAadharCard";
            this.txtAadharCard.Size = new System.Drawing.Size(380, 23);
            this.txtAadharCard.TabIndex = 4;
            this.txtAadharCard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAadharCard_KeyDown);
            this.txtAadharCard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAadharCard_KeyPress);
            // 
            // lblPANCard
            // 
            this.lblPANCard.AutoSize = true;
            this.lblPANCard.Location = new System.Drawing.Point(562, 60);
            this.lblPANCard.Name = "lblPANCard";
            this.lblPANCard.Size = new System.Drawing.Size(57, 15);
            this.lblPANCard.TabIndex = 83;
            this.lblPANCard.Text = "PAN Card";
            // 
            // txtPANCard
            // 
            this.txtPANCard.Location = new System.Drawing.Point(651, 57);
            this.txtPANCard.MaxLength = 10;
            this.txtPANCard.Name = "txtPANCard";
            this.txtPANCard.Size = new System.Drawing.Size(380, 23);
            this.txtPANCard.TabIndex = 5;
            this.txtPANCard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPANCard_KeyDown);
            this.txtPANCard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPANCard_KeyPress);
            // 
            // lblDrivingLisence
            // 
            this.lblDrivingLisence.AutoSize = true;
            this.lblDrivingLisence.Location = new System.Drawing.Point(562, 84);
            this.lblDrivingLisence.Name = "lblDrivingLisence";
            this.lblDrivingLisence.Size = new System.Drawing.Size(87, 15);
            this.lblDrivingLisence.TabIndex = 85;
            this.lblDrivingLisence.Text = "DrivingLicence";
            // 
            // txtDrivingLicence
            // 
            this.txtDrivingLicence.Location = new System.Drawing.Point(651, 81);
            this.txtDrivingLicence.MaxLength = 16;
            this.txtDrivingLicence.Name = "txtDrivingLicence";
            this.txtDrivingLicence.Size = new System.Drawing.Size(380, 23);
            this.txtDrivingLicence.TabIndex = 6;
            this.txtDrivingLicence.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDrivingLisence_KeyDown);
            // 
            // cmbCity
            // 
            this.cmbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCity.FormattingEnabled = true;
            this.cmbCity.Location = new System.Drawing.Point(134, 129);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(380, 23);
            this.cmbCity.TabIndex = 2;
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(40, 132);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(28, 15);
            this.lblCity.TabIndex = 87;
            this.lblCity.Text = "City";
            // 
            // btnAddCity
            // 
            this.btnAddCity.BackColor = System.Drawing.Color.DarkViolet;
            this.btnAddCity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCity.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCity.ForeColor = System.Drawing.Color.White;
            this.btnAddCity.Location = new System.Drawing.Point(525, 129);
            this.btnAddCity.Name = "btnAddCity";
            this.btnAddCity.Size = new System.Drawing.Size(20, 20);
            this.btnAddCity.TabIndex = 209;
            this.btnAddCity.Text = "+";
            this.btnAddCity.UseVisualStyleBackColor = false;
            this.btnAddCity.Click += new System.EventHandler(this.btnAddCity_Click);
            // 
            // txtCCMailIDList
            // 
            this.txtCCMailIDList.Location = new System.Drawing.Point(651, 153);
            this.txtCCMailIDList.Multiline = true;
            this.txtCCMailIDList.Name = "txtCCMailIDList";
            this.txtCCMailIDList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCCMailIDList.Size = new System.Drawing.Size(380, 105);
            this.txtCCMailIDList.TabIndex = 210;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(562, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 15);
            this.label7.TabIndex = 211;
            this.label7.Text = "CC Mail ID List";
            // 
            // Customer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1071, 637);
            this.ControlBox = false;
            this.Controls.Add(this.txtCCMailIDList);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnAddCity);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.cmbCity);
            this.Controls.Add(this.txtDrivingLicence);
            this.Controls.Add(this.lblDrivingLisence);
            this.Controls.Add(this.txtPANCard);
            this.Controls.Add(this.lblPANCard);
            this.Controls.Add(this.txtAadharCard);
            this.Controls.Add(this.lblAadharCard);
            this.Controls.Add(this.txtStateCode);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtCustomerCode);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtMobileNumber);
            this.Controls.Add(this.txtGSTNumber);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtEmailId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Customer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Customer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmailId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtGSTNumber;
        private System.Windows.Forms.TextBox txtMobileNumber;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtStateCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblAadharCard;
        private System.Windows.Forms.TextBox txtAadharCard;
        private System.Windows.Forms.Label lblPANCard;
        private System.Windows.Forms.TextBox txtPANCard;
        private System.Windows.Forms.Label lblDrivingLisence;
        private System.Windows.Forms.TextBox txtDrivingLicence;
        private System.Windows.Forms.ComboBox cmbCity;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Button btnAddCity;
        private System.Windows.Forms.TextBox txtCCMailIDList;
        private System.Windows.Forms.Label label7;
    }
}