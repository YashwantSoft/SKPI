namespace SPApplication.Transaction
{
    partial class KB_CommercialOrder
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
            this.lbDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lbCommercialQty = new System.Windows.Forms.Label();
            this.txtCommercialqty = new System.Windows.Forms.TextBox();
            this.lbCommercial = new System.Windows.Forms.Label();
            this.lbcommercialList = new System.Windows.Forms.ListBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(2, -1);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(597, 31);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Commercial Order ";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Location = new System.Drawing.Point(95, 39);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(32, 15);
            this.lbDate.TabIndex = 1;
            this.lbDate.Text = "Date";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(192, 39);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(89, 23);
            this.dtpDate.TabIndex = 6;
            // 
            // lbCommercialQty
            // 
            this.lbCommercialQty.AutoSize = true;
            this.lbCommercialQty.Location = new System.Drawing.Point(282, 42);
            this.lbCommercialQty.Name = "lbCommercialQty";
            this.lbCommercialQty.Size = new System.Drawing.Size(123, 15);
            this.lbCommercialQty.TabIndex = 7;
            this.lbCommercialQty.Text = "Commercial Quantity";
            // 
            // txtCommercialqty
            // 
            this.txtCommercialqty.Location = new System.Drawing.Point(407, 38);
            this.txtCommercialqty.Name = "txtCommercialqty";
            this.txtCommercialqty.Size = new System.Drawing.Size(100, 23);
            this.txtCommercialqty.TabIndex = 8;
            // 
            // lbCommercial
            // 
            this.lbCommercial.AutoSize = true;
            this.lbCommercial.Location = new System.Drawing.Point(92, 71);
            this.lbCommercial.Name = "lbCommercial";
            this.lbCommercial.Size = new System.Drawing.Size(95, 15);
            this.lbCommercial.TabIndex = 9;
            this.lbCommercial.Text = "Commercial List";
            // 
            // lbcommercialList
            // 
            this.lbcommercialList.FormattingEnabled = true;
            this.lbcommercialList.ItemHeight = 15;
            this.lbcommercialList.Location = new System.Drawing.Point(191, 65);
            this.lbcommercialList.Name = "lbcommercialList";
            this.lbcommercialList.Size = new System.Drawing.Size(316, 94);
            this.lbcommercialList.TabIndex = 10;
            // 
            // btnExit
            // 
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(386, 167);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "Exit ";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(303, 167);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(221, 167);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(138, 167);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 204);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(580, 388);
            this.dataGridView1.TabIndex = 18;
            // 
            // KB_CommercialOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(598, 598);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lbcommercialList);
            this.Controls.Add(this.lbCommercial);
            this.Controls.Add(this.txtCommercialqty);
            this.Controls.Add(this.lbCommercialQty);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lbDate);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "KB_CommercialOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.KB_CommercialOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lbDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lbCommercialQty;
        private System.Windows.Forms.TextBox txtCommercialqty;
        private System.Windows.Forms.Label lbCommercial;
        private System.Windows.Forms.ListBox lbcommercialList;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}