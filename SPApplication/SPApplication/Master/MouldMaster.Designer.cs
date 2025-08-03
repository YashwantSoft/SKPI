
namespace SPApplication.Master
{
    partial class MouldMaster
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
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtMouldNo = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.txtSrNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNeck = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTillColarFreshBlow = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOfcFreshBlow = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtOfcFinal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTillColarFinal = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDrawingNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLebalOD = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLabelSpace = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtTallyName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtNickName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtExtraBrushes = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbCurrentStatus = new System.Windows.Forms.ComboBox();
            this.cmbExtraAccessories = new System.Windows.Forms.ComboBox();
            this.cmbMaterial = new System.Windows.Forms.ComboBox();
            this.cmbRepairing = new System.Windows.Forms.ComboBox();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.btnAddCollectionType = new System.Windows.Forms.Button();
            this.cmbAutoSemi = new System.Windows.Forms.ComboBox();
            this.cmbCavity = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(941, 28);
            this.lblHeader.TabIndex = 64;
            this.lblHeader.Text = "Customer";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(13, 373);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(73, 15);
            this.lblTotalCount.TabIndex = 83;
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
            this.dataGridView1.Location = new System.Drawing.Point(12, 398);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(918, 270);
            this.dataGridView1.TabIndex = 80;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(345, 370);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 23);
            this.txtSearch.TabIndex = 25;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(296, 373);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 15);
            this.label5.TabIndex = 82;
            this.label5.Text = "Search ";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(472, 336);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 23;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(393, 336);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 22;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(314, 336);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 21;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(551, 336);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 24;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtMouldNo
            // 
            this.txtMouldNo.Location = new System.Drawing.Point(110, 34);
            this.txtMouldNo.Name = "txtMouldNo";
            this.txtMouldNo.Size = new System.Drawing.Size(78, 23);
            this.txtMouldNo.TabIndex = 74;
            this.txtMouldNo.TabStop = false;
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(35, 38);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(61, 15);
            this.lblCity.TabIndex = 81;
            this.lblCity.Text = "Mould No";
            // 
            // txtSrNo
            // 
            this.txtSrNo.Location = new System.Drawing.Point(110, 58);
            this.txtSrNo.Name = "txtSrNo";
            this.txtSrNo.Size = new System.Drawing.Size(300, 23);
            this.txtSrNo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 85;
            this.label1.Text = "Sr No";
            // 
            // txtNeck
            // 
            this.txtNeck.Location = new System.Drawing.Point(110, 82);
            this.txtNeck.Name = "txtNeck";
            this.txtNeck.Size = new System.Drawing.Size(300, 23);
            this.txtNeck.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 87;
            this.label2.Text = "Neck";
            // 
            // txtTillColarFreshBlow
            // 
            this.txtTillColarFreshBlow.Location = new System.Drawing.Point(83, 18);
            this.txtTillColarFreshBlow.Name = "txtTillColarFreshBlow";
            this.txtTillColarFreshBlow.Size = new System.Drawing.Size(300, 23);
            this.txtTillColarFreshBlow.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 89;
            this.label3.Text = "Till Colar";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOfcFreshBlow);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTillColarFreshBlow);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(27, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(406, 74);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fresh Blow";
            // 
            // txtOfcFreshBlow
            // 
            this.txtOfcFreshBlow.Location = new System.Drawing.Point(83, 42);
            this.txtOfcFreshBlow.Name = "txtOfcFreshBlow";
            this.txtOfcFreshBlow.Size = new System.Drawing.Size(300, 23);
            this.txtOfcFreshBlow.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 15);
            this.label4.TabIndex = 91;
            this.label4.Text = "Ofc";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtOfcFinal);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtTillColarFinal);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(27, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(406, 74);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Final";
            // 
            // txtOfcFinal
            // 
            this.txtOfcFinal.Location = new System.Drawing.Point(83, 42);
            this.txtOfcFinal.Name = "txtOfcFinal";
            this.txtOfcFinal.Size = new System.Drawing.Size(300, 23);
            this.txtOfcFinal.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 15);
            this.label6.TabIndex = 91;
            this.label6.Text = "Ofc";
            // 
            // txtTillColarFinal
            // 
            this.txtTillColarFinal.Location = new System.Drawing.Point(83, 18);
            this.txtTillColarFinal.Name = "txtTillColarFinal";
            this.txtTillColarFinal.Size = new System.Drawing.Size(300, 23);
            this.txtTillColarFinal.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 15);
            this.label7.TabIndex = 89;
            this.label7.Text = "Till Colar";
            // 
            // txtDrawingNo
            // 
            this.txtDrawingNo.Location = new System.Drawing.Point(110, 260);
            this.txtDrawingNo.Name = "txtDrawingNo";
            this.txtDrawingNo.Size = new System.Drawing.Size(300, 23);
            this.txtDrawingNo.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 263);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 15);
            this.label8.TabIndex = 94;
            this.label8.Text = "Drawing No";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(35, 287);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 15);
            this.label9.TabIndex = 96;
            this.label9.Text = "Auto/Semi";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(480, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 15);
            this.label10.TabIndex = 98;
            this.label10.Text = "Cavity";
            // 
            // txtLebalOD
            // 
            this.txtLebalOD.Location = new System.Drawing.Point(585, 103);
            this.txtLebalOD.Name = "txtLebalOD";
            this.txtLebalOD.Size = new System.Drawing.Size(300, 23);
            this.txtLebalOD.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(480, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 15);
            this.label11.TabIndex = 104;
            this.label11.Text = "Lebal OD";
            // 
            // txtLabelSpace
            // 
            this.txtLabelSpace.Location = new System.Drawing.Point(585, 79);
            this.txtLabelSpace.Name = "txtLabelSpace";
            this.txtLabelSpace.Size = new System.Drawing.Size(300, 23);
            this.txtLabelSpace.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(480, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 15);
            this.label12.TabIndex = 102;
            this.label12.Text = "Lebal Space";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(585, 55);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(300, 23);
            this.txtHeight.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(480, 58);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 15);
            this.label13.TabIndex = 100;
            this.label13.Text = "Hight";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(480, 202);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 15);
            this.label14.TabIndex = 112;
            this.label14.Text = "Repairing";
            // 
            // txtTallyName
            // 
            this.txtTallyName.Location = new System.Drawing.Point(585, 175);
            this.txtTallyName.Name = "txtTallyName";
            this.txtTallyName.Size = new System.Drawing.Size(300, 23);
            this.txtTallyName.TabIndex = 14;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(480, 178);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 15);
            this.label15.TabIndex = 110;
            this.label15.Text = "Tally Name";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(480, 154);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 15);
            this.label16.TabIndex = 108;
            this.label16.Text = "Party/Customer";
            // 
            // txtNickName
            // 
            this.txtNickName.Location = new System.Drawing.Point(585, 127);
            this.txtNickName.Name = "txtNickName";
            this.txtNickName.Size = new System.Drawing.Size(300, 23);
            this.txtNickName.TabIndex = 12;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(480, 130);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 15);
            this.label17.TabIndex = 106;
            this.label17.Text = "Nick Name";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(480, 274);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(85, 15);
            this.label18.TabIndex = 118;
            this.label18.Text = "Current Status";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(480, 250);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(103, 15);
            this.label19.TabIndex = 116;
            this.label19.Text = "Extra Accessories";
            // 
            // txtExtraBrushes
            // 
            this.txtExtraBrushes.Location = new System.Drawing.Point(585, 223);
            this.txtExtraBrushes.Name = "txtExtraBrushes";
            this.txtExtraBrushes.Size = new System.Drawing.Size(300, 23);
            this.txtExtraBrushes.TabIndex = 16;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(480, 226);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 15);
            this.label20.TabIndex = 114;
            this.label20.Text = "Extra Bushes";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(35, 311);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(55, 15);
            this.label21.TabIndex = 120;
            this.label21.Text = "Material";
            // 
            // cmbCurrentStatus
            // 
            this.cmbCurrentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrentStatus.FormattingEnabled = true;
            this.cmbCurrentStatus.Items.AddRange(new object[] {
            "Running",
            "Closed",
            "Returned"});
            this.cmbCurrentStatus.Location = new System.Drawing.Point(585, 271);
            this.cmbCurrentStatus.Name = "cmbCurrentStatus";
            this.cmbCurrentStatus.Size = new System.Drawing.Size(300, 23);
            this.cmbCurrentStatus.TabIndex = 18;
            // 
            // cmbExtraAccessories
            // 
            this.cmbExtraAccessories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExtraAccessories.FormattingEnabled = true;
            this.cmbExtraAccessories.Items.AddRange(new object[] {
            "Attachments",
            "Bottoms",
            "Insert",
            "No",
            "Yes"});
            this.cmbExtraAccessories.Location = new System.Drawing.Point(585, 247);
            this.cmbExtraAccessories.Name = "cmbExtraAccessories";
            this.cmbExtraAccessories.Size = new System.Drawing.Size(300, 23);
            this.cmbExtraAccessories.TabIndex = 17;
            // 
            // cmbMaterial
            // 
            this.cmbMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaterial.FormattingEnabled = true;
            this.cmbMaterial.Items.AddRange(new object[] {
            "Aluminium",
            "R2",
            "Alumac"});
            this.cmbMaterial.Location = new System.Drawing.Point(110, 308);
            this.cmbMaterial.Name = "cmbMaterial";
            this.cmbMaterial.Size = new System.Drawing.Size(300, 23);
            this.cmbMaterial.TabIndex = 19;
            // 
            // cmbRepairing
            // 
            this.cmbRepairing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRepairing.FormattingEnabled = true;
            this.cmbRepairing.Items.AddRange(new object[] {
            "Yes",
            "No",
            "Saff"});
            this.cmbRepairing.Location = new System.Drawing.Point(585, 199);
            this.cmbRepairing.Name = "cmbRepairing";
            this.cmbRepairing.Size = new System.Drawing.Size(300, 23);
            this.cmbRepairing.TabIndex = 15;
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Items.AddRange(new object[] {
            "Malas",
            "Mapro",
            "Venkeys",
            "Manama",
            "Malvis",
            "",
            "1",
            "2",
            "3"});
            this.cmbCustomer.Location = new System.Drawing.Point(585, 151);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(300, 23);
            this.cmbCustomer.TabIndex = 13;
            // 
            // btnAddCollectionType
            // 
            this.btnAddCollectionType.BackColor = System.Drawing.Color.Blue;
            this.btnAddCollectionType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddCollectionType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddCollectionType.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCollectionType.ForeColor = System.Drawing.Color.White;
            this.btnAddCollectionType.Location = new System.Drawing.Point(897, 152);
            this.btnAddCollectionType.Name = "btnAddCollectionType";
            this.btnAddCollectionType.Size = new System.Drawing.Size(20, 20);
            this.btnAddCollectionType.TabIndex = 11397;
            this.btnAddCollectionType.Text = "+";
            this.btnAddCollectionType.UseVisualStyleBackColor = false;
            this.btnAddCollectionType.Click += new System.EventHandler(this.btnAddCollectionType_Click);
            // 
            // cmbAutoSemi
            // 
            this.cmbAutoSemi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAutoSemi.FormattingEnabled = true;
            this.cmbAutoSemi.Items.AddRange(new object[] {
            "Auto",
            "Semi",
            "Auto New",
            "Auto Old",
            "Pending"});
            this.cmbAutoSemi.Location = new System.Drawing.Point(110, 284);
            this.cmbAutoSemi.Name = "cmbAutoSemi";
            this.cmbAutoSemi.Size = new System.Drawing.Size(300, 23);
            this.cmbAutoSemi.TabIndex = 7;
            // 
            // cmbCavity
            // 
            this.cmbCavity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCavity.FormattingEnabled = true;
            this.cmbCavity.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cmbCavity.Location = new System.Drawing.Point(585, 31);
            this.cmbCavity.Name = "cmbCavity";
            this.cmbCavity.Size = new System.Drawing.Size(300, 23);
            this.cmbCavity.TabIndex = 8;
            // 
            // MouldMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(941, 686);
            this.ControlBox = false;
            this.Controls.Add(this.cmbCavity);
            this.Controls.Add(this.cmbAutoSemi);
            this.Controls.Add(this.btnAddCollectionType);
            this.Controls.Add(this.cmbCustomer);
            this.Controls.Add(this.cmbRepairing);
            this.Controls.Add(this.cmbMaterial);
            this.Controls.Add(this.cmbExtraAccessories);
            this.Controls.Add(this.cmbCurrentStatus);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtExtraBrushes);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtTallyName);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtNickName);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtLebalOD);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtLabelSpace);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDrawingNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtNeck);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSrNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtMouldNo);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MouldMaster";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MouldMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtMouldNo;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.TextBox txtSrNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNeck;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTillColarFreshBlow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtOfcFreshBlow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtOfcFinal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTillColarFinal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDrawingNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLebalOD;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLabelSpace;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTallyName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtNickName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtExtraBrushes;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbCurrentStatus;
        private System.Windows.Forms.ComboBox cmbExtraAccessories;
        private System.Windows.Forms.ComboBox cmbMaterial;
        private System.Windows.Forms.ComboBox cmbRepairing;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Button btnAddCollectionType;
        private System.Windows.Forms.ComboBox cmbAutoSemi;
        private System.Windows.Forms.ComboBox cmbCavity;
    }
}