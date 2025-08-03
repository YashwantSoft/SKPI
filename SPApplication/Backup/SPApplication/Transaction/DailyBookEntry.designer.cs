namespace TestApplication
{
    partial class DailyBookEntry
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
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblOB = new System.Windows.Forms.Label();
            this.txtOpeningBalance = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNarration = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbExpenses = new System.Windows.Forms.RadioButton();
            this.rbDeposite = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalExpencesAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTotalDepositeAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCurrentBalanceAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClosingBalance = new System.Windows.Forms.TextBox();
            this.lblDay = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblHeader.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHeader.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1098, 29);
            this.lblHeader.TabIndex = 57;
            this.lblHeader.Text = "Daily Book Entry";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(429, 34);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(138, 23);
            this.dtpDate.TabIndex = 0;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            this.dtpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDate_KeyDown);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(359, 37);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(37, 14);
            this.lblDate.TabIndex = 60;
            this.lblDate.Text = "Date";
            // 
            // lblOB
            // 
            this.lblOB.AutoSize = true;
            this.lblOB.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOB.Location = new System.Drawing.Point(731, 36);
            this.lblOB.Name = "lblOB";
            this.lblOB.Size = new System.Drawing.Size(113, 14);
            this.lblOB.TabIndex = 71;
            this.lblOB.Text = "Opening Balance";
            // 
            // txtOpeningBalance
            // 
            this.txtOpeningBalance.BackColor = System.Drawing.Color.MistyRose;
            this.txtOpeningBalance.Location = new System.Drawing.Point(959, 33);
            this.txtOpeningBalance.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOpeningBalance.Name = "txtOpeningBalance";
            this.txtOpeningBalance.ReadOnly = true;
            this.txtOpeningBalance.Size = new System.Drawing.Size(134, 23);
            this.txtOpeningBalance.TabIndex = 1;
            this.txtOpeningBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.White;
            this.txtAmount.Location = new System.Drawing.Point(106, 33);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(138, 23);
            this.txtAmount.TabIndex = 4;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 14);
            this.label2.TabIndex = 81;
            this.label2.Text = "Amount";
            // 
            // txtNarration
            // 
            this.txtNarration.Location = new System.Drawing.Point(106, 57);
            this.txtNarration.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(342, 23);
            this.txtNarration.TabIndex = 5;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 14);
            this.label3.TabIndex = 83;
            this.label3.Text = "Narration";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbExpenses);
            this.groupBox1.Controls.Add(this.rbDeposite);
            this.groupBox1.Controls.Add(this.txtNarration);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Location = new System.Drawing.Point(321, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 85);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // rbExpenses
            // 
            this.rbExpenses.AutoSize = true;
            this.rbExpenses.Location = new System.Drawing.Point(194, 11);
            this.rbExpenses.Name = "rbExpenses";
            this.rbExpenses.Size = new System.Drawing.Size(87, 20);
            this.rbExpenses.TabIndex = 3;
            this.rbExpenses.TabStop = true;
            this.rbExpenses.Text = "Expenses";
            this.rbExpenses.UseVisualStyleBackColor = true;
            this.rbExpenses.CheckedChanged += new System.EventHandler(this.rbExpenses_CheckedChanged);
            this.rbExpenses.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbExpences_KeyDown);
            // 
            // rbDeposite
            // 
            this.rbDeposite.AutoSize = true;
            this.rbDeposite.Location = new System.Drawing.Point(108, 11);
            this.rbDeposite.Name = "rbDeposite";
            this.rbDeposite.Size = new System.Drawing.Size(83, 20);
            this.rbDeposite.TabIndex = 2;
            this.rbDeposite.TabStop = true;
            this.rbDeposite.Text = "Deposite";
            this.rbDeposite.UseVisualStyleBackColor = true;
            this.rbDeposite.CheckedChanged += new System.EventHandler(this.rbDeposite_CheckedChanged);
            this.rbDeposite.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbDeposite_KeyDown);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 182);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(540, 433);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(553, 182);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(540, 433);
            this.dataGridView2.TabIndex = 12;
            this.dataGridView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellDoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(803, 622);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 14);
            this.label5.TabIndex = 86;
            this.label5.Text = "Total Expences Amount";
            // 
            // txtTotalExpencesAmount
            // 
            this.txtTotalExpencesAmount.BackColor = System.Drawing.Color.White;
            this.txtTotalExpencesAmount.Location = new System.Drawing.Point(959, 618);
            this.txtTotalExpencesAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotalExpencesAmount.Name = "txtTotalExpencesAmount";
            this.txtTotalExpencesAmount.ReadOnly = true;
            this.txtTotalExpencesAmount.Size = new System.Drawing.Size(134, 23);
            this.txtTotalExpencesAmount.TabIndex = 85;
            this.txtTotalExpencesAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(261, 622);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 14);
            this.label6.TabIndex = 92;
            this.label6.Text = "Total Deposite Amount";
            // 
            // txtTotalDepositeAmount
            // 
            this.txtTotalDepositeAmount.BackColor = System.Drawing.Color.White;
            this.txtTotalDepositeAmount.Location = new System.Drawing.Point(413, 618);
            this.txtTotalDepositeAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotalDepositeAmount.Name = "txtTotalDepositeAmount";
            this.txtTotalDepositeAmount.ReadOnly = true;
            this.txtTotalDepositeAmount.Size = new System.Drawing.Size(134, 23);
            this.txtTotalDepositeAmount.TabIndex = 91;
            this.txtTotalDepositeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(797, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 14);
            this.label1.TabIndex = 94;
            this.label1.Text = "Current Balance Amount";
            // 
            // txtCurrentBalanceAmount
            // 
            this.txtCurrentBalanceAmount.BackColor = System.Drawing.Color.Lime;
            this.txtCurrentBalanceAmount.Location = new System.Drawing.Point(959, 57);
            this.txtCurrentBalanceAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCurrentBalanceAmount.Name = "txtCurrentBalanceAmount";
            this.txtCurrentBalanceAmount.ReadOnly = true;
            this.txtCurrentBalanceAmount.Size = new System.Drawing.Size(134, 23);
            this.txtCurrentBalanceAmount.TabIndex = 95;
            this.txtCurrentBalanceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(797, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 14);
            this.label4.TabIndex = 96;
            this.label4.Text = "Net Balance";
            // 
            // txtClosingBalance
            // 
            this.txtClosingBalance.BackColor = System.Drawing.SystemColors.Info;
            this.txtClosingBalance.Location = new System.Drawing.Point(959, 81);
            this.txtClosingBalance.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtClosingBalance.Name = "txtClosingBalance";
            this.txtClosingBalance.ReadOnly = true;
            this.txtClosingBalance.Size = new System.Drawing.Size(134, 23);
            this.txtClosingBalance.TabIndex = 97;
            this.txtClosingBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDay
            // 
            this.lblDay.AutoSize = true;
            this.lblDay.BackColor = System.Drawing.Color.Transparent;
            this.lblDay.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDay.Location = new System.Drawing.Point(12, 34);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(36, 14);
            this.lblDay.TabIndex = 98;
            this.lblDay.Text = "Day-";
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(554, 147);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 101;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(646, 147);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 30);
            this.btnExit.TabIndex = 102;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(462, 147);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 100;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(370, 147);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 99;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // DailyBookEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1097, 643);
            this.ControlBox = false;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtClosingBalance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCurrentBalanceAmount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTotalDepositeAmount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.txtTotalExpencesAmount);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblOB);
            this.Controls.Add(this.txtOpeningBalance);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DailyBookEntry";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.DailyBookEntryNew_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblOB;
        private System.Windows.Forms.TextBox txtOpeningBalance;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNarration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalExpencesAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTotalDepositeAmount;
        private System.Windows.Forms.RadioButton rbExpenses;
        private System.Windows.Forms.RadioButton rbDeposite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCurrentBalanceAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtClosingBalance;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
    }
}