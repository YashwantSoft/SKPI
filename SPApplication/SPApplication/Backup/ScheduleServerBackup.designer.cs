
namespace SPApplication.Backup
{
    partial class ScheduleServerBackup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeader = new System.Windows.Forms.Label();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.clmDeleteProduct = new System.Windows.Forms.DataGridViewLinkColumn();
            this.clmSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBackupFolderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtAddBackupFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnAddGrid = new System.Windows.Forms.Button();
            this.btnClearGrid = new System.Windows.Forms.Button();
            this.txtDestinationPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowseDestination = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.dtpBackupTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.gbListOfBackup = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.btnAddBackup = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.gbListOfBackup.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1211, 28);
            this.lblHeader.TabIndex = 64;
            this.lblHeader.Text = "Add Backup Folder";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpTime
            // 
            this.dtpTime.Enabled = false;
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(1051, 32);
            this.dtpTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(125, 23);
            this.dtpTime.TabIndex = 11439;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1016, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 11441;
            this.label6.Text = "Time";
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(867, 31);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(125, 23);
            this.dtpDate.TabIndex = 11438;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(833, 35);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(32, 15);
            this.lblDate.TabIndex = 11440;
            this.lblDate.Text = "Date";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Snow;
            this.label12.Location = new System.Drawing.Point(577, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 15);
            this.label12.TabIndex = 11529;
            this.label12.Text = "Backup Folder Data";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmDeleteProduct,
            this.clmSrNo,
            this.clmPath,
            this.clmBackupFolderName});
            this.dgv.GridColor = System.Drawing.Color.White;
            this.dgv.Location = new System.Drawing.Point(10, 79);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(1166, 430);
            this.dgv.TabIndex = 6;
            this.dgv.TabStop = false;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // clmDeleteProduct
            // 
            this.clmDeleteProduct.HeaderText = "Delete";
            this.clmDeleteProduct.Name = "clmDeleteProduct";
            this.clmDeleteProduct.ReadOnly = true;
            this.clmDeleteProduct.Width = 50;
            // 
            // clmSrNo
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmSrNo.DefaultCellStyle = dataGridViewCellStyle4;
            this.clmSrNo.HeaderText = "No.";
            this.clmSrNo.Name = "clmSrNo";
            this.clmSrNo.ReadOnly = true;
            this.clmSrNo.Width = 50;
            // 
            // clmPath
            // 
            this.clmPath.HeaderText = "Backup Path";
            this.clmPath.Name = "clmPath";
            this.clmPath.ReadOnly = true;
            this.clmPath.Width = 900;
            // 
            // clmBackupFolderName
            // 
            this.clmBackupFolderName.HeaderText = "Backup Folder Name";
            this.clmBackupFolderName.Name = "clmBackupFolderName";
            this.clmBackupFolderName.ReadOnly = true;
            this.clmBackupFolderName.Width = 150;
            // 
            // txtAddBackupFolder
            // 
            this.txtAddBackupFolder.Location = new System.Drawing.Point(215, 22);
            this.txtAddBackupFolder.Name = "txtAddBackupFolder";
            this.txtAddBackupFolder.Size = new System.Drawing.Size(938, 23);
            this.txtAddBackupFolder.TabIndex = 2;
            this.txtAddBackupFolder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddBackupFolder_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 15);
            this.label3.TabIndex = 11531;
            this.label3.Text = "Add Backup Folder";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrowse.Location = new System.Drawing.Point(134, 22);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnAddGrid
            // 
            this.btnAddGrid.BackColor = System.Drawing.Color.Transparent;
            this.btnAddGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddGrid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddGrid.Location = new System.Drawing.Point(999, 50);
            this.btnAddGrid.Name = "btnAddGrid";
            this.btnAddGrid.Size = new System.Drawing.Size(75, 23);
            this.btnAddGrid.TabIndex = 4;
            this.btnAddGrid.Text = "&Add";
            this.btnAddGrid.UseVisualStyleBackColor = false;
            this.btnAddGrid.Click += new System.EventHandler(this.btnAddGrid_Click);
            // 
            // btnClearGrid
            // 
            this.btnClearGrid.BackColor = System.Drawing.Color.Transparent;
            this.btnClearGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearGrid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearGrid.Location = new System.Drawing.Point(1078, 50);
            this.btnClearGrid.Name = "btnClearGrid";
            this.btnClearGrid.Size = new System.Drawing.Size(75, 23);
            this.btnClearGrid.TabIndex = 5;
            this.btnClearGrid.Text = "&Clear";
            this.btnClearGrid.UseVisualStyleBackColor = false;
            this.btnClearGrid.Click += new System.EventHandler(this.btnClearGrid_Click);
            // 
            // txtDestinationPath
            // 
            this.txtDestinationPath.Location = new System.Drawing.Point(196, 512);
            this.txtDestinationPath.Name = "txtDestinationPath";
            this.txtDestinationPath.Size = new System.Drawing.Size(935, 23);
            this.txtDestinationPath.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 516);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 11536;
            this.label1.Text = "Destination Path";
            // 
            // btnBrowseDestination
            // 
            this.btnBrowseDestination.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseDestination.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseDestination.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrowseDestination.Location = new System.Drawing.Point(115, 512);
            this.btnBrowseDestination.Name = "btnBrowseDestination";
            this.btnBrowseDestination.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseDestination.TabIndex = 6;
            this.btnBrowseDestination.Text = "Browse";
            this.btnBrowseDestination.UseVisualStyleBackColor = false;
            this.btnBrowseDestination.Click += new System.EventHandler(this.btnBrowseDestination_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(569, 608);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(489, 608);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(649, 608);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 10;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(1109, 608);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 11541;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Visible = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BackColor = System.Drawing.Color.Snow;
            this.lblCount.Location = new System.Drawing.Point(13, 62);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(39, 15);
            this.lblCount.TabIndex = 11542;
            this.lblCount.Text = "Count";
            // 
            // dtpBackupTime
            // 
            this.dtpBackupTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpBackupTime.Location = new System.Drawing.Point(84, 31);
            this.dtpBackupTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpBackupTime.Name = "dtpBackupTime";
            this.dtpBackupTime.Size = new System.Drawing.Size(125, 23);
            this.dtpBackupTime.TabIndex = 11543;
            this.dtpBackupTime.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 11544;
            this.label2.Text = "Backup Time";
            this.label2.Visible = false;
            // 
            // gbListOfBackup
            // 
            this.gbListOfBackup.Controls.Add(this.label4);
            this.gbListOfBackup.Controls.Add(this.txtFolderName);
            this.gbListOfBackup.Controls.Add(this.txtAddBackupFolder);
            this.gbListOfBackup.Controls.Add(this.dgv);
            this.gbListOfBackup.Controls.Add(this.label12);
            this.gbListOfBackup.Controls.Add(this.lblCount);
            this.gbListOfBackup.Controls.Add(this.label3);
            this.gbListOfBackup.Controls.Add(this.btnBrowse);
            this.gbListOfBackup.Controls.Add(this.btnAddGrid);
            this.gbListOfBackup.Controls.Add(this.btnClearGrid);
            this.gbListOfBackup.Controls.Add(this.label1);
            this.gbListOfBackup.Controls.Add(this.btnBrowseDestination);
            this.gbListOfBackup.Controls.Add(this.txtDestinationPath);
            this.gbListOfBackup.Location = new System.Drawing.Point(8, 61);
            this.gbListOfBackup.Name = "gbListOfBackup";
            this.gbListOfBackup.Size = new System.Drawing.Size(1194, 541);
            this.gbListOfBackup.TabIndex = 0;
            this.gbListOfBackup.TabStop = false;
            this.gbListOfBackup.Text = "List of Backup Folder";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(94, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 15);
            this.label4.TabIndex = 11544;
            this.label4.Text = "Backup Folder Name";
            // 
            // txtFolderName
            // 
            this.txtFolderName.Location = new System.Drawing.Point(215, 46);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(293, 23);
            this.txtFolderName.TabIndex = 3;
            this.txtFolderName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolderName_KeyDown);
            // 
            // btnAddBackup
            // 
            this.btnAddBackup.BackColor = System.Drawing.Color.Transparent;
            this.btnAddBackup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddBackup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddBackup.Location = new System.Drawing.Point(242, 31);
            this.btnAddBackup.Name = "btnAddBackup";
            this.btnAddBackup.Size = new System.Drawing.Size(103, 23);
            this.btnAddBackup.TabIndex = 11543;
            this.btnAddBackup.Text = "Add Backup";
            this.btnAddBackup.UseVisualStyleBackColor = false;
            this.btnAddBackup.Visible = false;
            this.btnAddBackup.Click += new System.EventHandler(this.btnAddBackup_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(1028, 608);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 11546;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ScheduleServerBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1212, 644);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAddBackup);
            this.Controls.Add(this.gbListOfBackup);
            this.Controls.Add(this.dtpBackupTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ScheduleServerBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AddBackupFolder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.gbListOfBackup.ResumeLayout(false);
            this.gbListOfBackup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtAddBackupFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnAddGrid;
        private System.Windows.Forms.Button btnClearGrid;
        private System.Windows.Forms.TextBox txtDestinationPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowseDestination;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DateTimePicker dtpBackupTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbListOfBackup;
        private System.Windows.Forms.Button btnAddBackup;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtFolderName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewLinkColumn clmDeleteProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBackupFolderName;
    }
}