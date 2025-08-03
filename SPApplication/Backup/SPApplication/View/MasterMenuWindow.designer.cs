namespace SPApplication
{
    partial class MasterMenuWindow
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
            this.btnManufracturer = new System.Windows.Forms.Button();
            this.btnSupplier = new System.Windows.Forms.Button();
            this.btnFirmInfo = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnItemMaster = new System.Windows.Forms.Button();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.btnExpenses = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnManufracturer
            // 
            this.btnManufracturer.BackColor = System.Drawing.Color.Transparent;
            this.btnManufracturer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnManufracturer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnManufracturer.Location = new System.Drawing.Point(122, 13);
            this.btnManufracturer.Name = "btnManufracturer";
            this.btnManufracturer.Size = new System.Drawing.Size(100, 100);
            this.btnManufracturer.TabIndex = 11270;
            this.btnManufracturer.UseVisualStyleBackColor = false;
            this.btnManufracturer.Click += new System.EventHandler(this.btnManufracturer_Click);
            // 
            // btnSupplier
            // 
            this.btnSupplier.BackColor = System.Drawing.Color.Transparent;
            this.btnSupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSupplier.Location = new System.Drawing.Point(340, 12);
            this.btnSupplier.Name = "btnSupplier";
            this.btnSupplier.Size = new System.Drawing.Size(100, 100);
            this.btnSupplier.TabIndex = 12;
            this.btnSupplier.UseVisualStyleBackColor = false;
            this.btnSupplier.Click += new System.EventHandler(this.btnSupplier_Click);
            // 
            // btnFirmInfo
            // 
            this.btnFirmInfo.BackColor = System.Drawing.Color.Transparent;
            this.btnFirmInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFirmInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFirmInfo.Location = new System.Drawing.Point(13, 13);
            this.btnFirmInfo.Name = "btnFirmInfo";
            this.btnFirmInfo.Size = new System.Drawing.Size(100, 100);
            this.btnFirmInfo.TabIndex = 11;
            this.btnFirmInfo.UseVisualStyleBackColor = false;
            this.btnFirmInfo.Click += new System.EventHandler(this.btnFirmInfo_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(667, 10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 100);
            this.btnExit.TabIndex = 10;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnItemMaster
            // 
            this.btnItemMaster.BackColor = System.Drawing.Color.Transparent;
            this.btnItemMaster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnItemMaster.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnItemMaster.Location = new System.Drawing.Point(231, 13);
            this.btnItemMaster.Name = "btnItemMaster";
            this.btnItemMaster.Size = new System.Drawing.Size(100, 100);
            this.btnItemMaster.TabIndex = 9;
            this.btnItemMaster.UseVisualStyleBackColor = false;
            this.btnItemMaster.Click += new System.EventHandler(this.btnItemMaster_Click);
            // 
            // btnCustomer
            // 
            this.btnCustomer.BackColor = System.Drawing.Color.Transparent;
            this.btnCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCustomer.Location = new System.Drawing.Point(449, 11);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(100, 100);
            this.btnCustomer.TabIndex = 7;
            this.btnCustomer.UseVisualStyleBackColor = false;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // btnExpenses
            // 
            this.btnExpenses.BackColor = System.Drawing.Color.Transparent;
            this.btnExpenses.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExpenses.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExpenses.Location = new System.Drawing.Point(558, 10);
            this.btnExpenses.Name = "btnExpenses";
            this.btnExpenses.Size = new System.Drawing.Size(100, 100);
            this.btnExpenses.TabIndex = 4;
            this.btnExpenses.UseVisualStyleBackColor = false;
            this.btnExpenses.Click += new System.EventHandler(this.btnExpenses_Click);
            // 
            // MasterMenuWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(781, 122);
            this.ControlBox = false;
            this.Controls.Add(this.btnManufracturer);
            this.Controls.Add(this.btnSupplier);
            this.Controls.Add(this.btnFirmInfo);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnItemMaster);
            this.Controls.Add(this.btnCustomer);
            this.Controls.Add(this.btnExpenses);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MasterMenuWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MasterMenuWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExpenses;
        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Button btnItemMaster;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnFirmInfo;
        private System.Windows.Forms.Button btnSupplier;
        private System.Windows.Forms.Button btnManufracturer;

    }
}