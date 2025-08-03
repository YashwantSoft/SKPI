namespace SPApplication.Report
{
    partial class ProfitLossReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeader = new System.Windows.Forms.Label();
            this.cbToday = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.txtIncomeTotal = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.dgvIncome = new System.Windows.Forms.DataGridView();
            this.clmSrNoIncome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDateIncome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmIncome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPaymentTypeIncome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAmountIncome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.txtExpensesTotal = new System.Windows.Forms.TextBox();
            this.lblTotalCountExpenses = new System.Windows.Forms.Label();
            this.dgvExpenses = new System.Windows.Forms.DataGridView();
            this.clmSrNoExpeses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDateExpenses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmExpenses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPaymentType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmExpensesAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNetProfit = new System.Windows.Forms.TextBox();
            this.btnView = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenses)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Blue;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(-1, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1184, 30);
            this.lblHeader.TabIndex = 179;
            this.lblHeader.Text = "Profit and Loss Account";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbToday
            // 
            this.cbToday.AutoSize = true;
            this.cbToday.Location = new System.Drawing.Point(764, 36);
            this.cbToday.Name = "cbToday";
            this.cbToday.Size = new System.Drawing.Size(58, 19);
            this.cbToday.TabIndex = 11209;
            this.cbToday.Text = "Today";
            this.cbToday.UseVisualStyleBackColor = true;
            this.cbToday.CheckedChanged += new System.EventHandler(this.cbToday_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(577, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 11208;
            this.label2.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(626, 33);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(120, 23);
            this.dtpToDate.TabIndex = 11206;
            this.dtpToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpToDate_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(359, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 11207;
            this.label1.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(442, 33);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(120, 23);
            this.dtpFromDate.TabIndex = 11205;
            this.dtpFromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpFromDate_KeyDown);
            // 
            // txtIncomeTotal
            // 
            this.txtIncomeTotal.BackColor = System.Drawing.Color.Lime;
            this.txtIncomeTotal.Location = new System.Drawing.Point(466, 528);
            this.txtIncomeTotal.Name = "txtIncomeTotal";
            this.txtIncomeTotal.ReadOnly = true;
            this.txtIncomeTotal.Size = new System.Drawing.Size(108, 23);
            this.txtIncomeTotal.TabIndex = 11293;
            this.txtIncomeTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BackColor = System.Drawing.Color.Lime;
            this.lblCount.Location = new System.Drawing.Point(9, 532);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(69, 15);
            this.lblCount.TabIndex = 11292;
            this.lblCount.Text = "Total Count";
            // 
            // dgvIncome
            // 
            this.dgvIncome.AllowUserToAddRows = false;
            this.dgvIncome.AllowUserToDeleteRows = false;
            this.dgvIncome.AllowUserToResizeRows = false;
            this.dgvIncome.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvIncome.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIncome.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSrNoIncome,
            this.clmDateIncome,
            this.clmIncome,
            this.clmPaymentTypeIncome,
            this.clmAmountIncome});
            this.dgvIncome.Location = new System.Drawing.Point(8, 98);
            this.dgvIncome.Name = "dgvIncome";
            this.dgvIncome.RowHeadersVisible = false;
            this.dgvIncome.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIncome.Size = new System.Drawing.Size(586, 429);
            this.dgvIncome.TabIndex = 11291;
            // 
            // clmSrNoIncome
            // 
            this.clmSrNoIncome.HeaderText = "Sr.No.";
            this.clmSrNoIncome.Name = "clmSrNoIncome";
            this.clmSrNoIncome.ReadOnly = true;
            this.clmSrNoIncome.Width = 40;
            // 
            // clmDateIncome
            // 
            this.clmDateIncome.HeaderText = "Date";
            this.clmDateIncome.Name = "clmDateIncome";
            this.clmDateIncome.ReadOnly = true;
            this.clmDateIncome.Width = 80;
            // 
            // clmIncome
            // 
            this.clmIncome.HeaderText = "Income";
            this.clmIncome.Name = "clmIncome";
            this.clmIncome.ReadOnly = true;
            this.clmIncome.Width = 220;
            // 
            // clmPaymentTypeIncome
            // 
            this.clmPaymentTypeIncome.HeaderText = "Payment Type";
            this.clmPaymentTypeIncome.Name = "clmPaymentTypeIncome";
            this.clmPaymentTypeIncome.ReadOnly = true;
            this.clmPaymentTypeIncome.Width = 120;
            // 
            // clmAmountIncome
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clmAmountIncome.DefaultCellStyle = dataGridViewCellStyle5;
            this.clmAmountIncome.HeaderText = "Amount";
            this.clmAmountIncome.Name = "clmAmountIncome";
            this.clmAmountIncome.ReadOnly = true;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(646, 60);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 30);
            this.btnExit.TabIndex = 11290;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(556, 60);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 11289;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Location = new System.Drawing.Point(550, 582);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(80, 30);
            this.btnReport.TabIndex = 11288;
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // txtExpensesTotal
            // 
            this.txtExpensesTotal.BackColor = System.Drawing.Color.Lime;
            this.txtExpensesTotal.Location = new System.Drawing.Point(1053, 528);
            this.txtExpensesTotal.Name = "txtExpensesTotal";
            this.txtExpensesTotal.ReadOnly = true;
            this.txtExpensesTotal.Size = new System.Drawing.Size(99, 23);
            this.txtExpensesTotal.TabIndex = 11296;
            this.txtExpensesTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalCountExpenses
            // 
            this.lblTotalCountExpenses.AutoSize = true;
            this.lblTotalCountExpenses.BackColor = System.Drawing.Color.Lime;
            this.lblTotalCountExpenses.Location = new System.Drawing.Point(604, 531);
            this.lblTotalCountExpenses.Name = "lblTotalCountExpenses";
            this.lblTotalCountExpenses.Size = new System.Drawing.Size(69, 15);
            this.lblTotalCountExpenses.TabIndex = 11295;
            this.lblTotalCountExpenses.Text = "Total Count";
            // 
            // dgvExpenses
            // 
            this.dgvExpenses.AllowUserToAddRows = false;
            this.dgvExpenses.AllowUserToDeleteRows = false;
            this.dgvExpenses.AllowUserToResizeRows = false;
            this.dgvExpenses.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvExpenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExpenses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSrNoExpeses,
            this.clmDateExpenses,
            this.clmExpenses,
            this.clmPaymentType,
            this.clmExpensesAmount});
            this.dgvExpenses.Location = new System.Drawing.Point(600, 97);
            this.dgvExpenses.Name = "dgvExpenses";
            this.dgvExpenses.RowHeadersVisible = false;
            this.dgvExpenses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExpenses.Size = new System.Drawing.Size(575, 429);
            this.dgvExpenses.TabIndex = 11294;
            // 
            // clmSrNoExpeses
            // 
            this.clmSrNoExpeses.HeaderText = "Sr.No.";
            this.clmSrNoExpeses.Name = "clmSrNoExpeses";
            this.clmSrNoExpeses.ReadOnly = true;
            this.clmSrNoExpeses.Width = 40;
            // 
            // clmDateExpenses
            // 
            this.clmDateExpenses.HeaderText = "Date";
            this.clmDateExpenses.Name = "clmDateExpenses";
            this.clmDateExpenses.ReadOnly = true;
            this.clmDateExpenses.Width = 80;
            // 
            // clmExpenses
            // 
            this.clmExpenses.HeaderText = "Expenses";
            this.clmExpenses.Name = "clmExpenses";
            this.clmExpenses.ReadOnly = true;
            this.clmExpenses.Width = 220;
            // 
            // clmPaymentType
            // 
            this.clmPaymentType.HeaderText = "Payment Type";
            this.clmPaymentType.Name = "clmPaymentType";
            this.clmPaymentType.ReadOnly = true;
            this.clmPaymentType.Width = 110;
            // 
            // clmExpensesAmount
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clmExpensesAmount.DefaultCellStyle = dataGridViewCellStyle6;
            this.clmExpensesAmount.HeaderText = "Amount";
            this.clmExpensesAmount.Name = "clmExpensesAmount";
            this.clmExpensesAmount.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(883, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 11297;
            this.label4.Text = "Expenses";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(242, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 15);
            this.label5.TabIndex = 11298;
            this.label5.Text = "Income";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(388, 532);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 11299;
            this.label3.Text = "Total Income";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(965, 531);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 15);
            this.label6.TabIndex = 11300;
            this.label6.Text = "Total Expenses";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(388, 557);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 15);
            this.label7.TabIndex = 11302;
            this.label7.Text = "Net Profit";
            // 
            // txtNetProfit
            // 
            this.txtNetProfit.BackColor = System.Drawing.Color.Lime;
            this.txtNetProfit.Location = new System.Drawing.Point(466, 553);
            this.txtNetProfit.Name = "txtNetProfit";
            this.txtNetProfit.ReadOnly = true;
            this.txtNetProfit.Size = new System.Drawing.Size(108, 23);
            this.txtNetProfit.TabIndex = 11301;
            this.txtNetProfit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnView
            // 
            this.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnView.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(466, 60);
            this.btnView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(80, 30);
            this.btnView.TabIndex = 11303;
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // ProfitLossReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 616);
            this.ControlBox = false;
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtNetProfit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtExpensesTotal);
            this.Controls.Add(this.lblTotalCountExpenses);
            this.Controls.Add(this.dgvExpenses);
            this.Controls.Add(this.txtIncomeTotal);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.dgvIncome);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.cbToday);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ProfitLossReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ProfitLossReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.CheckBox cbToday;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.TextBox txtIncomeTotal;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvIncome;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.TextBox txtExpensesTotal;
        private System.Windows.Forms.Label lblTotalCountExpenses;
        private System.Windows.Forms.DataGridView dgvExpenses;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNoIncome;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDateIncome;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIncome;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPaymentTypeIncome;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAmountIncome;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNoExpeses;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDateExpenses;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmExpenses;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPaymentType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmExpensesAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNetProfit;
        private System.Windows.Forms.Button btnView;
    }
}