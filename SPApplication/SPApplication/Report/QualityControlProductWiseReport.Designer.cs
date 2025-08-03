namespace SPApplication.Report
{
    partial class QualityControlProductWiseReport
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
            this.cbEmail = new System.Windows.Forms.CheckBox();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.btnReport = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cbToday = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblHeader = new System.Windows.Forms.Label();
            this.gbSearchProduct = new System.Windows.Forms.GroupBox();
            this.lbItem = new System.Windows.Forms.ListBox();
            this.txtSearchProductName = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblProductType = new System.Windows.Forms.Label();
            this.lblBottleInformation = new System.Windows.Forms.Label();
            this.lblJarInformation = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbSearchProduct.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbEmail
            // 
            this.cbEmail.AutoSize = true;
            this.cbEmail.Location = new System.Drawing.Point(10, 680);
            this.cbEmail.Name = "cbEmail";
            this.cbEmail.Size = new System.Drawing.Size(67, 19);
            this.cbEmail.TabIndex = 11483;
            this.cbEmail.Text = "Mail ID";
            this.cbEmail.UseVisualStyleBackColor = true;
            this.cbEmail.Visible = false;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(10, 265);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(69, 15);
            this.lblTotalCount.TabIndex = 11489;
            this.lblTotalCount.Text = "Total Count";
            // 
            // btnReport
            // 
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Location = new System.Drawing.Point(572, 673);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 30);
            this.btnReport.TabIndex = 6;
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 282);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1199, 385);
            this.dataGridView1.TabIndex = 11488;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(653, 247);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 11482;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnView
            // 
            this.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnView.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(493, 247);
            this.btnView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 30);
            this.btnView.TabIndex = 5;
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(573, 247);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 11481;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cbToday
            // 
            this.cbToday.AutoSize = true;
            this.cbToday.Location = new System.Drawing.Point(422, 35);
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
            this.label2.Location = new System.Drawing.Point(246, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 11487;
            this.label2.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(295, 33);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(120, 23);
            this.dtpToDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 11486;
            this.label1.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(111, 33);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(120, 23);
            this.dtpFromDate.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Blue;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(-1, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1221, 30);
            this.lblHeader.TabIndex = 11485;
            this.lblHeader.Text = "Machine Wise Quality Control Report";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbSearchProduct
            // 
            this.gbSearchProduct.Controls.Add(this.lbItem);
            this.gbSearchProduct.Controls.Add(this.txtSearchProductName);
            this.gbSearchProduct.Location = new System.Drawing.Point(31, 57);
            this.gbSearchProduct.Name = "gbSearchProduct";
            this.gbSearchProduct.Size = new System.Drawing.Size(465, 185);
            this.gbSearchProduct.TabIndex = 3;
            this.gbSearchProduct.TabStop = false;
            this.gbSearchProduct.Text = "Search Product";
            // 
            // lbItem
            // 
            this.lbItem.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbItem.FormattingEnabled = true;
            this.lbItem.ItemHeight = 18;
            this.lbItem.Location = new System.Drawing.Point(8, 43);
            this.lbItem.Name = "lbItem";
            this.lbItem.Size = new System.Drawing.Size(451, 130);
            this.lbItem.TabIndex = 4;
            this.lbItem.Visible = false;
            this.lbItem.Click += new System.EventHandler(this.lbItem_Click);
            this.lbItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbItem_KeyDown);
            // 
            // txtSearchProductName
            // 
            this.txtSearchProductName.BackColor = System.Drawing.Color.White;
            this.txtSearchProductName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchProductName.Location = new System.Drawing.Point(7, 16);
            this.txtSearchProductName.Name = "txtSearchProductName";
            this.txtSearchProductName.Size = new System.Drawing.Size(452, 26);
            this.txtSearchProductName.TabIndex = 3;
            this.txtSearchProductName.TextChanged += new System.EventHandler(this.txtSearchProductName_TextChanged);
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(18, 61);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(84, 15);
            this.label79.TabIndex = 11448;
            this.label79.Text = "Product Name";
            // 
            // lblProductName
            // 
            this.lblProductName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProductName.Location = new System.Drawing.Point(14, 78);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(578, 81);
            this.lblProductName.TabIndex = 11353;
            this.lblProductName.Text = "Bottle/Jar";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProductType
            // 
            this.lblProductType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProductType.Location = new System.Drawing.Point(15, 17);
            this.lblProductType.Name = "lblProductType";
            this.lblProductType.Size = new System.Drawing.Size(88, 28);
            this.lblProductType.TabIndex = 271;
            this.lblProductType.Text = "Bottle/Jar";
            this.lblProductType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBottleInformation
            // 
            this.lblBottleInformation.BackColor = System.Drawing.Color.White;
            this.lblBottleInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBottleInformation.Location = new System.Drawing.Point(231, 21);
            this.lblBottleInformation.Name = "lblBottleInformation";
            this.lblBottleInformation.Size = new System.Drawing.Size(20, 20);
            this.lblBottleInformation.TabIndex = 11438;
            this.lblBottleInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblJarInformation
            // 
            this.lblJarInformation.BackColor = System.Drawing.Color.Yellow;
            this.lblJarInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblJarInformation.Location = new System.Drawing.Point(302, 21);
            this.lblJarInformation.Name = "lblJarInformation";
            this.lblJarInformation.Size = new System.Drawing.Size(20, 20);
            this.lblJarInformation.TabIndex = 11439;
            this.lblJarInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(253, 24);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(39, 15);
            this.label69.TabIndex = 11440;
            this.label69.Text = "Bottle";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(324, 23);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(23, 15);
            this.label68.TabIndex = 11441;
            this.label68.Text = "Jar";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblProductName);
            this.groupBox1.Controls.Add(this.label68);
            this.groupBox1.Controls.Add(this.label79);
            this.groupBox1.Controls.Add(this.label69);
            this.groupBox1.Controls.Add(this.lblJarInformation);
            this.groupBox1.Controls.Add(this.lblProductType);
            this.groupBox1.Controls.Add(this.lblBottleInformation);
            this.groupBox1.Location = new System.Drawing.Point(522, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(612, 205);
            this.groupBox1.TabIndex = 11491;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Product Information";
            // 
            // QualityControlProductWiseReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1218, 708);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbSearchProduct);
            this.Controls.Add(this.cbEmail);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.cbToday);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "QualityControlProductWiseReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.QualityControlProductWiseReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbSearchProduct.ResumeLayout(false);
            this.gbSearchProduct.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbEmail;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox cbToday;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox gbSearchProduct;
        private System.Windows.Forms.ListBox lbItem;
        private System.Windows.Forms.TextBox txtSearchProductName;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblProductType;
        private System.Windows.Forms.Label lblBottleInformation;
        private System.Windows.Forms.Label lblJarInformation;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}