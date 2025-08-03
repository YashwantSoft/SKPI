namespace SPApplication.Transaction
{
    partial class CapLabel
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
            this.label13 = new System.Windows.Forms.Label();
            this.txtSearchID = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cbToday = new System.Windows.Forms.CheckBox();
            this.label26 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label27 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lbCap = new System.Windows.Forms.ListBox();
            this.txtSearchCap = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtShift = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPurchaseOrderNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbWad = new System.Windows.Forms.ListBox();
            this.lblCapName = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbWadFitter = new System.Windows.Forms.ComboBox();
            this.pbQRCode = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.rtbStickerHeader = new System.Windows.Forms.RichTextBox();
            this.txtSearchWad = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.lblWadName = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbWad = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPrint = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).BeginInit();
            this.gbWad.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1323, 29);
            this.lblHeader.TabIndex = 11378;
            this.lblHeader.Text = "CAP Label";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(370, 419);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 15);
            this.label13.TabIndex = 11449;
            this.label13.Text = "ID";
            // 
            // txtSearchID
            // 
            this.txtSearchID.Location = new System.Drawing.Point(391, 416);
            this.txtSearchID.Name = "txtSearchID";
            this.txtSearchID.Size = new System.Drawing.Size(57, 23);
            this.txtSearchID.TabIndex = 11;
            this.txtSearchID.TextChanged += new System.EventHandler(this.txtSearchID_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Location = new System.Drawing.Point(1239, 408);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 30);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(122, 420);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 15);
            this.label10.TabIndex = 11446;
            this.label10.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(168, 417);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 23);
            this.txtSearch.TabIndex = 10;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // cbToday
            // 
            this.cbToday.AutoSize = true;
            this.cbToday.Location = new System.Drawing.Point(1177, 416);
            this.cbToday.Name = "cbToday";
            this.cbToday.Size = new System.Drawing.Size(58, 19);
            this.cbToday.TabIndex = 14;
            this.cbToday.Text = "Today";
            this.cbToday.UseVisualStyleBackColor = true;
            this.cbToday.CheckedChanged += new System.EventHandler(this.cbToday_CheckedChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(1028, 418);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(19, 15);
            this.label26.TabIndex = 11444;
            this.label26.Text = "To";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(1051, 414);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(120, 23);
            this.dtpToDate.TabIndex = 13;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(894, 418);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(32, 15);
            this.label27.TabIndex = 11443;
            this.label27.Text = "Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(932, 414);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(94, 23);
            this.dtpFromDate.TabIndex = 12;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(6, 420);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(73, 15);
            this.lblTotalCount.TabIndex = 11439;
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
            this.dataGridView1.Location = new System.Drawing.Point(9, 444);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1304, 261);
            this.dataGridView1.TabIndex = 11438;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(584, 410);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 7;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(663, 410);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(505, 410);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(742, 410);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 9;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lbCap
            // 
            this.lbCap.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCap.FormattingEnabled = true;
            this.lbCap.ItemHeight = 19;
            this.lbCap.Location = new System.Drawing.Point(88, 57);
            this.lbCap.Name = "lbCap";
            this.lbCap.Size = new System.Drawing.Size(391, 308);
            this.lbCap.TabIndex = 1;
            this.lbCap.Visible = false;
            this.lbCap.Click += new System.EventHandler(this.lbCap_Click);
            this.lbCap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbCap_KeyDown);
            // 
            // txtSearchCap
            // 
            this.txtSearchCap.Location = new System.Drawing.Point(88, 32);
            this.txtSearchCap.Name = "txtSearchCap";
            this.txtSearchCap.Size = new System.Drawing.Size(222, 23);
            this.txtSearchCap.TabIndex = 0;
            this.txtSearchCap.TextChanged += new System.EventHandler(this.txtSearchCap_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(2, 59);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(50, 15);
            this.label21.TabIndex = 11455;
            this.label21.Text = "Cap List";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 11453;
            this.label1.Text = "Cap Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 11457;
            this.label2.Text = "Wads";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1163, 60);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 15);
            this.label12.TabIndex = 11471;
            this.label12.Text = "Shift";
            // 
            // txtShift
            // 
            this.txtShift.BackColor = System.Drawing.Color.White;
            this.txtShift.Location = new System.Drawing.Point(1201, 56);
            this.txtShift.Name = "txtShift";
            this.txtShift.ReadOnly = true;
            this.txtShift.Size = new System.Drawing.Size(110, 23);
            this.txtShift.TabIndex = 11470;
            this.txtShift.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(771, 337);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 15);
            this.label9.TabIndex = 11468;
            this.label9.Text = "P.O. No";
            // 
            // txtPurchaseOrderNo
            // 
            this.txtPurchaseOrderNo.Location = new System.Drawing.Point(839, 334);
            this.txtPurchaseOrderNo.Name = "txtPurchaseOrderNo";
            this.txtPurchaseOrderNo.Size = new System.Drawing.Size(122, 23);
            this.txtPurchaseOrderNo.TabIndex = 4;
            this.txtPurchaseOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPurchaseOrderNo_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(502, 337);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 15);
            this.label8.TabIndex = 11467;
            this.label8.Text = "Qty";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(570, 334);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(122, 23);
            this.txtQty.TabIndex = 3;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // dtpTime
            // 
            this.dtpTime.Enabled = false;
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(1035, 56);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(110, 23);
            this.dtpTime.TabIndex = 11463;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(976, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 11466;
            this.label6.Text = "Time";
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(1035, 32);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(110, 23);
            this.dtpDate.TabIndex = 11462;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(976, 36);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(32, 15);
            this.lblDate.TabIndex = 11464;
            this.lblDate.Text = "Date";
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.White;
            this.txtID.Location = new System.Drawing.Point(1201, 32);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(110, 23);
            this.txtID.TabIndex = 11458;
            this.txtID.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1163, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 15);
            this.label4.TabIndex = 11465;
            this.label4.Text = "Sr No";
            // 
            // lbWad
            // 
            this.lbWad.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWad.FormattingEnabled = true;
            this.lbWad.ItemHeight = 19;
            this.lbWad.Location = new System.Drawing.Point(6, 37);
            this.lbWad.Name = "lbWad";
            this.lbWad.Size = new System.Drawing.Size(474, 232);
            this.lbWad.TabIndex = 2;
            this.lbWad.Click += new System.EventHandler(this.lbWad_Click);
            this.lbWad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbWad_KeyDown);
            // 
            // lblCapName
            // 
            this.lblCapName.BackColor = System.Drawing.Color.White;
            this.lblCapName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCapName.Location = new System.Drawing.Point(88, 368);
            this.lblCapName.Name = "lblCapName";
            this.lblCapName.Size = new System.Drawing.Size(391, 28);
            this.lblCapName.TabIndex = 11474;
            this.lblCapName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(502, 361);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 15);
            this.label14.TabIndex = 11484;
            this.label14.Text = "Wad-Fitter";
            // 
            // cmbWadFitter
            // 
            this.cmbWadFitter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbWadFitter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbWadFitter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWadFitter.FormattingEnabled = true;
            this.cmbWadFitter.Location = new System.Drawing.Point(570, 358);
            this.cmbWadFitter.Name = "cmbWadFitter";
            this.cmbWadFitter.Size = new System.Drawing.Size(391, 23);
            this.cmbWadFitter.TabIndex = 5;
            this.cmbWadFitter.SelectionChangeCommitted += new System.EventHandler(this.cmbWadFitter_SelectionChangeCommitted);
            this.cmbWadFitter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbWadFitter_KeyDown);
            // 
            // pbQRCode
            // 
            this.pbQRCode.Location = new System.Drawing.Point(1035, 85);
            this.pbQRCode.Name = "pbQRCode";
            this.pbQRCode.Size = new System.Drawing.Size(200, 200);
            this.pbQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbQRCode.TabIndex = 11487;
            this.pbQRCode.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(977, 89);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 15);
            this.label11.TabIndex = 11488;
            this.label11.Text = "QR Code ";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(975, 293);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(46, 30);
            this.label18.TabIndex = 11490;
            this.label18.Text = "Sticker \r\nHeader";
            // 
            // rtbStickerHeader
            // 
            this.rtbStickerHeader.BackColor = System.Drawing.Color.White;
            this.rtbStickerHeader.Location = new System.Drawing.Point(1034, 290);
            this.rtbStickerHeader.Name = "rtbStickerHeader";
            this.rtbStickerHeader.ReadOnly = true;
            this.rtbStickerHeader.Size = new System.Drawing.Size(279, 115);
            this.rtbStickerHeader.TabIndex = 11489;
            this.rtbStickerHeader.TabStop = false;
            this.rtbStickerHeader.Text = "";
            // 
            // txtSearchWad
            // 
            this.txtSearchWad.Location = new System.Drawing.Point(71, 13);
            this.txtSearchWad.Name = "txtSearchWad";
            this.txtSearchWad.Size = new System.Drawing.Size(409, 23);
            this.txtSearchWad.TabIndex = 2;
            this.txtSearchWad.TextChanged += new System.EventHandler(this.txtSearchWad_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(16, 276);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 15);
            this.label19.TabIndex = 11492;
            this.label19.Text = "Wad Name";
            // 
            // lblWadName
            // 
            this.lblWadName.BackColor = System.Drawing.Color.White;
            this.lblWadName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWadName.Location = new System.Drawing.Point(85, 273);
            this.lblWadName.Name = "lblWadName";
            this.lblWadName.Size = new System.Drawing.Size(395, 23);
            this.lblWadName.TabIndex = 11493;
            this.lblWadName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(502, 386);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(59, 15);
            this.label20.TabIndex = 11494;
            this.label20.Text = "Batch No.";
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.BackColor = System.Drawing.Color.White;
            this.txtBatchNo.Location = new System.Drawing.Point(570, 382);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.ReadOnly = true;
            this.txtBatchNo.Size = new System.Drawing.Size(391, 23);
            this.txtBatchNo.TabIndex = 11495;
            this.txtBatchNo.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 373);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 11496;
            this.label3.Text = "Cap Name";
            // 
            // gbWad
            // 
            this.gbWad.Controls.Add(this.lbWad);
            this.gbWad.Controls.Add(this.label2);
            this.gbWad.Controls.Add(this.txtSearchWad);
            this.gbWad.Controls.Add(this.lblWadName);
            this.gbWad.Controls.Add(this.label19);
            this.gbWad.Location = new System.Drawing.Point(485, 30);
            this.gbWad.Name = "gbWad";
            this.gbWad.Size = new System.Drawing.Size(486, 303);
            this.gbWad.TabIndex = 11497;
            this.gbWad.TabStop = false;
            this.gbWad.Text = "Wad";
            this.gbWad.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(320, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 15);
            this.label5.TabIndex = 11499;
            this.label5.Text = "Print";
            // 
            // cmbPrint
            // 
            this.cmbPrint.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPrint.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrint.FormattingEnabled = true;
            this.cmbPrint.Items.AddRange(new object[] {
            "50X25",
            "100X50"});
            this.cmbPrint.Location = new System.Drawing.Point(356, 32);
            this.cmbPrint.Name = "cmbPrint";
            this.cmbPrint.Size = new System.Drawing.Size(123, 23);
            this.cmbPrint.TabIndex = 11498;
            // 
            // CapLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1323, 709);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbPrint);
            this.Controls.Add(this.gbWad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBatchNo);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.rtbStickerHeader);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pbQRCode);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbWadFitter);
            this.Controls.Add(this.lblCapName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtShift);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPurchaseOrderNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbCap);
            this.Controls.Add(this.txtSearchCap);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtSearchID);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cbToday);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CapLabel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CapLabel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).EndInit();
            this.gbWad.ResumeLayout(false);
            this.gbWad.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSearchID;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox cbToday;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ListBox lbCap;
        private System.Windows.Forms.TextBox txtSearchCap;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtShift;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPurchaseOrderNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbWad;
        private System.Windows.Forms.Label lblCapName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbWadFitter;
        private System.Windows.Forms.PictureBox pbQRCode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.RichTextBox rtbStickerHeader;
        private System.Windows.Forms.TextBox txtSearchWad;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblWadName;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbWad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPrint;
    }
}