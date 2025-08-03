namespace SPApplication
{
    partial class BankAccounts
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblBankName = new System.Windows.Forms.Label();
            this.cmbBankName = new System.Windows.Forms.ComboBox();
            this.lblCountDebit = new System.Windows.Forms.Label();
            this.lblCountCredit = new System.Windows.Forms.Label();
            this.txtTotalDebit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalCredit = new System.Windows.Forms.TextBox();
            this.dgvCredit = new System.Windows.Forms.DataGridView();
            this.clmSrNoCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDateCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmParticularsCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPaymentModeCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAmountCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClear = new System.Windows.Forms.Button();
            this.cbToday = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dgvDebit = new System.Windows.Forms.DataGridView();
            this.clmSrNoDebit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDateDebit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDebitParticulars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPaymentModeDebit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAmountDebit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.rtbBankDetails = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDebit)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1329, 29);
            this.lblHeader.TabIndex = 63;
            this.lblHeader.Text = "Bank Account";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBankName
            // 
            this.lblBankName.AutoSize = true;
            this.lblBankName.Location = new System.Drawing.Point(439, 61);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(68, 15);
            this.lblBankName.TabIndex = 0;
            this.lblBankName.Text = "Bank Name";
            // 
            // cmbBankName
            // 
            this.cmbBankName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBankName.Location = new System.Drawing.Point(509, 58);
            this.cmbBankName.Name = "cmbBankName";
            this.cmbBankName.Size = new System.Drawing.Size(359, 23);
            this.cmbBankName.TabIndex = 3;
            this.cmbBankName.SelectionChangeCommitted += new System.EventHandler(this.cmbBankName_SelectionChangeCommitted);
            // 
            // lblCountDebit
            // 
            this.lblCountDebit.AutoSize = true;
            this.lblCountDebit.BackColor = System.Drawing.Color.Yellow;
            this.lblCountDebit.Location = new System.Drawing.Point(15, 628);
            this.lblCountDebit.Name = "lblCountDebit";
            this.lblCountDebit.Size = new System.Drawing.Size(69, 15);
            this.lblCountDebit.TabIndex = 11313;
            this.lblCountDebit.Text = "Total Count";
            // 
            // lblCountCredit
            // 
            this.lblCountCredit.AutoSize = true;
            this.lblCountCredit.BackColor = System.Drawing.Color.Lime;
            this.lblCountCredit.Location = new System.Drawing.Point(669, 628);
            this.lblCountCredit.Name = "lblCountCredit";
            this.lblCountCredit.Size = new System.Drawing.Size(69, 15);
            this.lblCountCredit.TabIndex = 11316;
            this.lblCountCredit.Text = "Total Count";
            // 
            // txtTotalDebit
            // 
            this.txtTotalDebit.BackColor = System.Drawing.Color.Yellow;
            this.txtTotalDebit.Location = new System.Drawing.Point(531, 626);
            this.txtTotalDebit.Name = "txtTotalDebit";
            this.txtTotalDebit.ReadOnly = true;
            this.txtTotalDebit.Size = new System.Drawing.Size(108, 23);
            this.txtTotalDebit.TabIndex = 11314;
            this.txtTotalDebit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1117, 628);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 15);
            this.label6.TabIndex = 11321;
            this.label6.Text = "Total Expenses";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(463, 630);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 11320;
            this.label3.Text = "Total Debit";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 15);
            this.label5.TabIndex = 11319;
            this.label5.Text = "Debit";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1274, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 11318;
            this.label4.Text = "Credit";
            // 
            // txtTotalCredit
            // 
            this.txtTotalCredit.BackColor = System.Drawing.Color.Lime;
            this.txtTotalCredit.Location = new System.Drawing.Point(1206, 625);
            this.txtTotalCredit.Name = "txtTotalCredit";
            this.txtTotalCredit.ReadOnly = true;
            this.txtTotalCredit.Size = new System.Drawing.Size(99, 23);
            this.txtTotalCredit.TabIndex = 11317;
            this.txtTotalCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dgvCredit
            // 
            this.dgvCredit.AllowUserToAddRows = false;
            this.dgvCredit.AllowUserToDeleteRows = false;
            this.dgvCredit.AllowUserToResizeRows = false;
            this.dgvCredit.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCredit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCredit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCredit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSrNoCredit,
            this.clmDateCredit,
            this.clmParticularsCredit,
            this.clmPaymentModeCredit,
            this.clmAmountCredit});
            this.dgvCredit.Location = new System.Drawing.Point(667, 213);
            this.dgvCredit.Name = "dgvCredit";
            this.dgvCredit.RowHeadersVisible = false;
            this.dgvCredit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCredit.Size = new System.Drawing.Size(650, 408);
            this.dgvCredit.TabIndex = 9;
            // 
            // clmSrNoCredit
            // 
            this.clmSrNoCredit.HeaderText = "Sr.No.";
            this.clmSrNoCredit.Name = "clmSrNoCredit";
            this.clmSrNoCredit.ReadOnly = true;
            this.clmSrNoCredit.Width = 40;
            // 
            // clmDateCredit
            // 
            this.clmDateCredit.HeaderText = "Date";
            this.clmDateCredit.Name = "clmDateCredit";
            this.clmDateCredit.ReadOnly = true;
            this.clmDateCredit.Width = 80;
            // 
            // clmParticularsCredit
            // 
            this.clmParticularsCredit.HeaderText = "Particulars";
            this.clmParticularsCredit.Name = "clmParticularsCredit";
            this.clmParticularsCredit.ReadOnly = true;
            this.clmParticularsCredit.Width = 300;
            // 
            // clmPaymentModeCredit
            // 
            this.clmPaymentModeCredit.HeaderText = "Payment Mode";
            this.clmPaymentModeCredit.Name = "clmPaymentModeCredit";
            this.clmPaymentModeCredit.ReadOnly = true;
            this.clmPaymentModeCredit.Width = 115;
            // 
            // clmAmountCredit
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clmAmountCredit.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmAmountCredit.HeaderText = "Amount";
            this.clmAmountCredit.Name = "clmAmountCredit";
            this.clmAmountCredit.ReadOnly = true;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(624, 177);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 6;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cbToday
            // 
            this.cbToday.AutoSize = true;
            this.cbToday.Location = new System.Drawing.Point(888, 35);
            this.cbToday.Name = "cbToday";
            this.cbToday.Size = new System.Drawing.Size(58, 19);
            this.cbToday.TabIndex = 2;
            this.cbToday.Text = "Today";
            this.cbToday.UseVisualStyleBackColor = true;
            this.cbToday.CheckedChanged += new System.EventHandler(this.cbToday_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(698, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 11307;
            this.label2.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(747, 33);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(120, 23);
            this.dtpToDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(439, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 11306;
            this.label1.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(509, 33);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(120, 23);
            this.dtpFromDate.TabIndex = 0;
            // 
            // dgvDebit
            // 
            this.dgvDebit.AllowUserToAddRows = false;
            this.dgvDebit.AllowUserToDeleteRows = false;
            this.dgvDebit.AllowUserToResizeRows = false;
            this.dgvDebit.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDebit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDebit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDebit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSrNoDebit,
            this.clmDateDebit,
            this.clmDebitParticulars,
            this.clmPaymentModeDebit,
            this.clmAmountDebit});
            this.dgvDebit.Location = new System.Drawing.Point(11, 213);
            this.dgvDebit.Name = "dgvDebit";
            this.dgvDebit.RowHeadersVisible = false;
            this.dgvDebit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDebit.Size = new System.Drawing.Size(650, 409);
            this.dgvDebit.TabIndex = 8;
            // 
            // clmSrNoDebit
            // 
            this.clmSrNoDebit.HeaderText = "Sr.No.";
            this.clmSrNoDebit.Name = "clmSrNoDebit";
            this.clmSrNoDebit.ReadOnly = true;
            this.clmSrNoDebit.Width = 40;
            // 
            // clmDateDebit
            // 
            this.clmDateDebit.HeaderText = "Date";
            this.clmDateDebit.Name = "clmDateDebit";
            this.clmDateDebit.ReadOnly = true;
            this.clmDateDebit.Width = 80;
            // 
            // clmDebitParticulars
            // 
            this.clmDebitParticulars.HeaderText = "Particulars";
            this.clmDebitParticulars.Name = "clmDebitParticulars";
            this.clmDebitParticulars.ReadOnly = true;
            this.clmDebitParticulars.Width = 300;
            // 
            // clmPaymentModeDebit
            // 
            this.clmPaymentModeDebit.HeaderText = "Payment Mode";
            this.clmPaymentModeDebit.Name = "clmPaymentModeDebit";
            this.clmPaymentModeDebit.ReadOnly = true;
            this.clmPaymentModeDebit.Width = 115;
            // 
            // clmAmountDebit
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clmAmountDebit.DefaultCellStyle = dataGridViewCellStyle4;
            this.clmAmountDebit.HeaderText = "Amount";
            this.clmAmountDebit.Name = "clmAmountDebit";
            this.clmAmountDebit.ReadOnly = true;
            // 
            // btnReport
            // 
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Location = new System.Drawing.Point(532, 177);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(80, 30);
            this.btnReport.TabIndex = 5;
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(716, 177);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 30);
            this.btnExit.TabIndex = 7;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // rtbBankDetails
            // 
            this.rtbBankDetails.BackColor = System.Drawing.Color.White;
            this.rtbBankDetails.Location = new System.Drawing.Point(509, 83);
            this.rtbBankDetails.Name = "rtbBankDetails";
            this.rtbBankDetails.ReadOnly = true;
            this.rtbBankDetails.Size = new System.Drawing.Size(359, 88);
            this.rtbBankDetails.TabIndex = 4;
            this.rtbBankDetails.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(439, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 15);
            this.label8.TabIndex = 11325;
            this.label8.Text = "Details";
            // 
            // BankAccounts
            // 
            this.AccessibleName = "gbBankInformation";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1329, 654);
            this.ControlBox = false;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.rtbBankDetails);
            this.Controls.Add(this.lblCountDebit);
            this.Controls.Add(this.lblCountCredit);
            this.Controls.Add(this.txtTotalDebit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTotalCredit);
            this.Controls.Add(this.dgvCredit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.cbToday);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.dgvDebit);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cmbBankName);
            this.Controls.Add(this.lblBankName);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BankAccounts";
            this.Load += new System.EventHandler(this.BankDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDebit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.ComboBox cmbBankName;
        private System.Windows.Forms.Label lblCountDebit;
        private System.Windows.Forms.Label lblCountCredit;
        private System.Windows.Forms.TextBox txtTotalDebit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotalCredit;
        private System.Windows.Forms.DataGridView dgvCredit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox cbToday;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DataGridView dgvDebit;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RichTextBox rtbBankDetails;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNoCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDateCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmParticularsCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPaymentModeCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAmountCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNoDebit;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDateDebit;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDebitParticulars;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPaymentModeDebit;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAmountDebit;
    }
}