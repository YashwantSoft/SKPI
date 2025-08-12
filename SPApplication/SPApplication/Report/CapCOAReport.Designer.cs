namespace SPApplication.Report
{
    partial class CapCOAReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeader = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.gbCOAParameters = new System.Windows.Forms.GroupBox();
            this.txtQCCheckerName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnReport = new System.Windows.Forms.Button();
            this.lblSearchItem = new System.Windows.Forms.Label();
            this.dgvValues = new System.Windows.Forms.DataGridView();
            this.clmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmParameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStandards = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTolerance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmQCValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearchItemName = new System.Windows.Forms.TextBox();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCapName = new System.Windows.Forms.Label();
            this.gbCapDetails = new System.Windows.Forms.GroupBox();
            this.lbCap = new System.Windows.Forms.ListBox();
            this.txtSearchCap = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSupplierMaterialRef = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbCOAParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).BeginInit();
            this.gbCapDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(-1, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1185, 29);
            this.lblHeader.TabIndex = 70;
            this.lblHeader.Text = "CAP Certificate Of Analysis ";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(569, 46);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 15);
            this.label16.TabIndex = 11524;
            this.label16.Text = "COA No";
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.BackColor = System.Drawing.Color.White;
            this.txtBatchNo.Location = new System.Drawing.Point(108, 19);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.ReadOnly = true;
            this.txtBatchNo.Size = new System.Drawing.Size(88, 23);
            this.txtBatchNo.TabIndex = 11;
            this.txtBatchNo.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(47, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 15);
            this.label17.TabIndex = 11497;
            this.label17.Text = "Batch No.";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(567, 501);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 11507;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(623, 42);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(100, 23);
            this.txtID.TabIndex = 11523;
            this.txtID.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(93, 506);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 11508;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(12, 505);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 11506;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(648, 501);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 11509;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // gbCOAParameters
            // 
            this.gbCOAParameters.Controls.Add(this.txtSupplierMaterialRef);
            this.gbCOAParameters.Controls.Add(this.label5);
            this.gbCOAParameters.Controls.Add(this.txtSubject);
            this.gbCOAParameters.Controls.Add(this.label4);
            this.gbCOAParameters.Controls.Add(this.txtQCCheckerName);
            this.gbCOAParameters.Controls.Add(this.label3);
            this.gbCOAParameters.Controls.Add(this.txtSupplierName);
            this.gbCOAParameters.Controls.Add(this.label2);
            this.gbCOAParameters.Controls.Add(this.txtInvoiceNumber);
            this.gbCOAParameters.Controls.Add(this.label37);
            this.gbCOAParameters.Controls.Add(this.txtBatchNo);
            this.gbCOAParameters.Controls.Add(this.label17);
            this.gbCOAParameters.Location = new System.Drawing.Point(12, 245);
            this.gbCOAParameters.Name = "gbCOAParameters";
            this.gbCOAParameters.Size = new System.Drawing.Size(545, 248);
            this.gbCOAParameters.TabIndex = 11505;
            this.gbCOAParameters.TabStop = false;
            this.gbCOAParameters.Text = "COA Parameters";
            // 
            // txtQCCheckerName
            // 
            this.txtQCCheckerName.Location = new System.Drawing.Point(108, 91);
            this.txtQCCheckerName.Name = "txtQCCheckerName";
            this.txtQCCheckerName.Size = new System.Drawing.Size(412, 23);
            this.txtQCCheckerName.TabIndex = 11558;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 15);
            this.label3.TabIndex = 11559;
            this.label3.Text = "QC Checker Name";
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(108, 67);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(412, 23);
            this.txtSupplierName.TabIndex = 11556;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 11557;
            this.label2.Text = "Supplier Name";
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Location = new System.Drawing.Point(108, 43);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(412, 23);
            this.txtInvoiceNumber.TabIndex = 11554;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(13, 46);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(93, 15);
            this.label37.TabIndex = 11555;
            this.label37.Text = "Invoice Number";
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
            this.dataGridView1.Location = new System.Drawing.Point(10, 542);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1158, 143);
            this.dataGridView1.TabIndex = 11522;
            this.dataGridView1.TabStop = false;
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Location = new System.Drawing.Point(486, 501);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 30);
            this.btnReport.TabIndex = 11526;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Visible = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // lblSearchItem
            // 
            this.lblSearchItem.AutoSize = true;
            this.lblSearchItem.Location = new System.Drawing.Point(809, 509);
            this.lblSearchItem.Name = "lblSearchItem";
            this.lblSearchItem.Size = new System.Drawing.Size(71, 15);
            this.lblSearchItem.TabIndex = 11530;
            this.lblSearchItem.Text = "Search Item";
            // 
            // dgvValues
            // 
            this.dgvValues.AllowUserToAddRows = false;
            this.dgvValues.AllowUserToDeleteRows = false;
            this.dgvValues.AllowUserToResizeColumns = false;
            this.dgvValues.AllowUserToResizeRows = false;
            this.dgvValues.BackgroundColor = System.Drawing.Color.White;
            this.dgvValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmId,
            this.clmSrNo,
            this.clmParameters,
            this.clmStandards,
            this.clmTolerance,
            this.clmQCValue});
            this.dgvValues.Location = new System.Drawing.Point(569, 71);
            this.dgvValues.Name = "dgvValues";
            this.dgvValues.RowHeadersVisible = false;
            this.dgvValues.Size = new System.Drawing.Size(599, 422);
            this.dgvValues.TabIndex = 11525;
            // 
            // clmId
            // 
            this.clmId.HeaderText = "Id";
            this.clmId.Name = "clmId";
            this.clmId.ReadOnly = true;
            this.clmId.Visible = false;
            // 
            // clmSrNo
            // 
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.LavenderBlush;
            this.clmSrNo.DefaultCellStyle = dataGridViewCellStyle9;
            this.clmSrNo.HeaderText = "Sr. No";
            this.clmSrNo.Name = "clmSrNo";
            this.clmSrNo.ReadOnly = true;
            this.clmSrNo.Width = 62;
            // 
            // clmParameters
            // 
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.LavenderBlush;
            this.clmParameters.DefaultCellStyle = dataGridViewCellStyle10;
            this.clmParameters.HeaderText = "Parameters";
            this.clmParameters.Name = "clmParameters";
            this.clmParameters.ReadOnly = true;
            this.clmParameters.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmParameters.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmParameters.Width = 200;
            // 
            // clmStandards
            // 
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.LavenderBlush;
            this.clmStandards.DefaultCellStyle = dataGridViewCellStyle11;
            this.clmStandards.HeaderText = "Standards";
            this.clmStandards.Name = "clmStandards";
            this.clmStandards.ReadOnly = true;
            // 
            // clmTolerance
            // 
            this.clmTolerance.HeaderText = "Tolerance";
            this.clmTolerance.Name = "clmTolerance";
            this.clmTolerance.ReadOnly = true;
            // 
            // clmQCValue
            // 
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.LavenderBlush;
            this.clmQCValue.DefaultCellStyle = dataGridViewCellStyle12;
            this.clmQCValue.HeaderText = "QC Value";
            this.clmQCValue.Name = "clmQCValue";
            this.clmQCValue.ReadOnly = true;
            this.clmQCValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmQCValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtSearchItemName
            // 
            this.txtSearchItemName.Location = new System.Drawing.Point(882, 505);
            this.txtSearchItemName.Name = "txtSearchItemName";
            this.txtSearchItemName.Size = new System.Drawing.Size(286, 23);
            this.txtSearchItemName.TabIndex = 11529;
            // 
            // dtpTime
            // 
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(1058, 42);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(110, 23);
            this.dtpTime.TabIndex = 11519;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1023, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 11521;
            this.label6.Text = "Time";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(897, 42);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(122, 23);
            this.dtpDate.TabIndex = 11518;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(863, 45);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(32, 15);
            this.lblDate.TabIndex = 11520;
            this.lblDate.Text = "Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 11533;
            this.label1.Text = "Cap Name";
            // 
            // lblCapName
            // 
            this.lblCapName.BackColor = System.Drawing.Color.White;
            this.lblCapName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCapName.Location = new System.Drawing.Point(104, 199);
            this.lblCapName.Name = "lblCapName";
            this.lblCapName.Size = new System.Drawing.Size(428, 28);
            this.lblCapName.TabIndex = 11532;
            this.lblCapName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbCapDetails
            // 
            this.gbCapDetails.Controls.Add(this.lbCap);
            this.gbCapDetails.Controls.Add(this.txtSearchCap);
            this.gbCapDetails.Controls.Add(this.label21);
            this.gbCapDetails.Controls.Add(this.label18);
            this.gbCapDetails.Location = new System.Drawing.Point(10, 30);
            this.gbCapDetails.Name = "gbCapDetails";
            this.gbCapDetails.Size = new System.Drawing.Size(528, 149);
            this.gbCapDetails.TabIndex = 11531;
            this.gbCapDetails.TabStop = false;
            this.gbCapDetails.Text = "Cap Details";
            // 
            // lbCap
            // 
            this.lbCap.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCap.FormattingEnabled = true;
            this.lbCap.ItemHeight = 19;
            this.lbCap.Location = new System.Drawing.Point(94, 41);
            this.lbCap.Name = "lbCap";
            this.lbCap.Size = new System.Drawing.Size(428, 99);
            this.lbCap.TabIndex = 11498;
            this.lbCap.Visible = false;
            this.lbCap.Click += new System.EventHandler(this.lbCap_Click);
            this.lbCap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbCap_KeyDown);
            // 
            // txtSearchCap
            // 
            this.txtSearchCap.Location = new System.Drawing.Point(94, 16);
            this.txtSearchCap.Name = "txtSearchCap";
            this.txtSearchCap.Size = new System.Drawing.Size(428, 23);
            this.txtSearchCap.TabIndex = 11497;
            this.txtSearchCap.TextChanged += new System.EventHandler(this.txtSearchCap_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 43);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(50, 15);
            this.label21.TabIndex = 11500;
            this.label21.Text = "Cap List";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 19);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 15);
            this.label18.TabIndex = 11499;
            this.label18.Text = "Cap Name";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(108, 115);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(412, 23);
            this.txtSubject.TabIndex = 11560;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 11561;
            this.label4.Text = "Subject";
            // 
            // txtSupplierMaterialRef
            // 
            this.txtSupplierMaterialRef.Location = new System.Drawing.Point(108, 139);
            this.txtSupplierMaterialRef.Multiline = true;
            this.txtSupplierMaterialRef.Name = "txtSupplierMaterialRef";
            this.txtSupplierMaterialRef.Size = new System.Drawing.Size(412, 84);
            this.txtSupplierMaterialRef.TabIndex = 11562;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 45);
            this.label5.TabIndex = 11563;
            this.label5.Text = "Supplier \r\nMaterial \r\nRef";
            // 
            // CapCOAReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1183, 698);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCapName);
            this.Controls.Add(this.gbCapDetails);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.gbCOAParameters);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.lblSearchItem);
            this.Controls.Add(this.dgvValues);
            this.Controls.Add(this.txtSearchItemName);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CapCOAReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CapCOAReport_Load);
            this.gbCOAParameters.ResumeLayout(false);
            this.gbCOAParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).EndInit();
            this.gbCapDetails.ResumeLayout(false);
            this.gbCapDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox gbCOAParameters;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label lblSearchItem;
        private System.Windows.Forms.DataGridView dgvValues;
        private System.Windows.Forms.TextBox txtSearchItemName;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCapName;
        private System.Windows.Forms.GroupBox gbCapDetails;
        private System.Windows.Forms.ListBox lbCap;
        private System.Windows.Forms.TextBox txtSearchCap;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmParameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStandards;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTolerance;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmQCValue;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txtQCCheckerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSupplierMaterialRef;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label label4;
    }
}