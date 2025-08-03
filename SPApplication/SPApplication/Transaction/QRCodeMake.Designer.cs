namespace SPApplication.Transaction
{
    partial class QRCodeMake
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
            this.pbQRCode = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbStickerHeader = new System.Windows.Forms.RichTextBox();
            this.btnAddPreformParty = new System.Windows.Forms.Button();
            this.cmbRackNumber = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbQRCode
            // 
            this.pbQRCode.Location = new System.Drawing.Point(236, 118);
            this.pbQRCode.Name = "pbQRCode";
            this.pbQRCode.Size = new System.Drawing.Size(233, 211);
            this.pbQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbQRCode.TabIndex = 1;
            this.pbQRCode.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(-1, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1026, 29);
            this.lblHeader.TabIndex = 11378;
            this.lblHeader.Text = "QR Code Creation";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(580, 370);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 30);
            this.btnPrint.TabIndex = 11380;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Visible = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(421, 370);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 11381;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(342, 370);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 11379;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(501, 370);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 11382;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.dataGridView1.Location = new System.Drawing.Point(12, 409);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1000, 132);
            this.dataGridView1.TabIndex = 11408;
            this.dataGridView1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(472, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 15);
            this.label7.TabIndex = 11428;
            this.label7.Text = "Sticker Header";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(238, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 15);
            this.label4.TabIndex = 11427;
            this.label4.Text = "QR Code ";
            // 
            // rtbStickerHeader
            // 
            this.rtbStickerHeader.BackColor = System.Drawing.Color.White;
            this.rtbStickerHeader.Location = new System.Drawing.Point(475, 118);
            this.rtbStickerHeader.Name = "rtbStickerHeader";
            this.rtbStickerHeader.ReadOnly = true;
            this.rtbStickerHeader.Size = new System.Drawing.Size(317, 211);
            this.rtbStickerHeader.TabIndex = 11426;
            this.rtbStickerHeader.TabStop = false;
            this.rtbStickerHeader.Text = "";
            // 
            // btnAddPreformParty
            // 
            this.btnAddPreformParty.BackColor = System.Drawing.Color.DarkViolet;
            this.btnAddPreformParty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPreformParty.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPreformParty.ForeColor = System.Drawing.Color.White;
            this.btnAddPreformParty.Location = new System.Drawing.Point(582, 35);
            this.btnAddPreformParty.Name = "btnAddPreformParty";
            this.btnAddPreformParty.Size = new System.Drawing.Size(20, 20);
            this.btnAddPreformParty.TabIndex = 11460;
            this.btnAddPreformParty.Text = "+";
            this.btnAddPreformParty.UseVisualStyleBackColor = false;
            this.btnAddPreformParty.Visible = false;
            // 
            // cmbRackNumber
            // 
            this.cmbRackNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbRackNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbRackNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRackNumber.FormattingEnabled = true;
            this.cmbRackNumber.Items.AddRange(new object[] {
            "AA6",
            "AA4",
            "AD3",
            "AB3",
            "AB5",
            "AD3",
            "AC4",
            "AD1",
            "AD3",
            "AD6",
            "AE2",
            "AE5",
            "AF1",
            "AF4",
            "AF6",
            "AG3",
            "AG5",
            "AH2",
            "AH4",
            "AI1",
            "AI3",
            "AI6",
            "AJ2",
            "AJ5",
            "AK1",
            "AK4",
            "AK6",
            "AL3",
            "AL5",
            "AM2",
            "AM4",
            "AM6",
            "AN3",
            "AN5",
            "AO2",
            "AO4",
            "AP1",
            "AP3",
            "AP6",
            "AQ2",
            "AQ5",
            "AR1",
            "AR4",
            "AR6",
            "AS2",
            "AS5",
            "AT1",
            "AT4",
            "AT6",
            "AU3",
            "AU5",
            "AV2",
            "AV4",
            "AW1",
            "AW3",
            "AW5",
            "AX2",
            "AX4",
            "AY1",
            "AY3",
            "AY6",
            "AZ2",
            "AZ5",
            "AAA1",
            "AAA4",
            "AAA6",
            "AAB3",
            "AAB5",
            "AAC2",
            "AAC4",
            "AAD1",
            "AAD3",
            "AAD6",
            "AAE2",
            "AAE4",
            "AAF1",
            "AAF3",
            "AAF6",
            "AAG2",
            "AAG5",
            "AAH1",
            "AAH4",
            "AAH6",
            "AAI3",
            "AAI5",
            "AAJ2",
            "AAJ4",
            "AAK1",
            "AAK3",
            "AAK6",
            "AAL2",
            "AAL5",
            "BA1",
            "BA2",
            "BA3",
            "BA4",
            "BA5",
            "BA6",
            "BB1",
            "BB2",
            "BB3",
            "BB4",
            "BB5",
            "BB6",
            "BC1",
            "BC2",
            "BC3",
            "BC4",
            "BC5",
            "BC6",
            "BD1",
            "BD2",
            "BD3",
            "BD4",
            "BD5",
            "BD6",
            "BE1",
            "BE2",
            "BE3",
            "BE4",
            "BE5",
            "BE6",
            "BF1",
            "BF2",
            "BF3",
            "BF4",
            "BF5",
            "BF6",
            "BG1",
            "BG2",
            "BG3",
            "BG4",
            "BG5",
            "BG6",
            "BH1",
            "BH2",
            "BH3",
            "BH4",
            "BH5",
            "BH6",
            "BI1",
            "BI2",
            "BI3",
            "BI4",
            "BI5",
            "BI6",
            "BJ1",
            "BJ2",
            "BJ3",
            "BJ4",
            "BJ5",
            "BJ6",
            "BK1",
            "BK2",
            "BK3",
            "BK4",
            "BK5",
            "BK6",
            "BL1",
            "BL2",
            "BL3",
            "BL4",
            "BL5",
            "BL6",
            "BM1",
            "BM2",
            "BM3",
            "BM4",
            "BM5",
            "BM6",
            "BN1",
            "BN2",
            "BN3",
            "BN4",
            "BN5",
            "BN6",
            "BO1",
            "BO2",
            "BO3",
            "BO4",
            "BO5",
            "BO6",
            "BP1",
            "BP2",
            "BP3",
            "AD3",
            "AD3",
            "AD3",
            "AA6",
            "AA6",
            "AA6"});
            this.cmbRackNumber.Location = new System.Drawing.Point(381, 34);
            this.cmbRackNumber.Name = "cmbRackNumber";
            this.cmbRackNumber.Size = new System.Drawing.Size(195, 23);
            this.cmbRackNumber.TabIndex = 11458;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(300, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 15);
            this.label5.TabIndex = 11459;
            this.label5.Text = "Rack Number";
            // 
            // cmbType
            // 
            this.cmbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Inward",
            "Outward"});
            this.cmbType.Location = new System.Drawing.Point(381, 58);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(195, 23);
            this.cmbType.TabIndex = 11461;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(300, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 11462;
            this.label1.Text = "Type";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(686, 370);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 11463;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            // 
            // QRCodeMake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 553);
            this.ControlBox = false;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddPreformParty);
            this.Controls.Add(this.cmbRackNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rtbStickerHeader);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.pbQRCode);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "QRCodeMake";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.QRCodeMake_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbQRCode;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtbStickerHeader;
        private System.Windows.Forms.Button btnAddPreformParty;
        private System.Windows.Forms.ComboBox cmbRackNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
    }
}