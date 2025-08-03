namespace SPApplication
{
    partial class TransactionMenuWindow
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
            this.btnReceipt = new System.Windows.Forms.Button();
            this.btnDeposit = new System.Windows.Forms.Button();
            this.btnExpenses = new System.Windows.Forms.Button();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPayment = new System.Windows.Forms.Button();
            this.btnSale = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReceipt
            // 
            this.btnReceipt.BackColor = System.Drawing.Color.Transparent;
            this.btnReceipt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReceipt.Location = new System.Drawing.Point(587, 11);
            this.btnReceipt.Name = "btnReceipt";
            this.btnReceipt.Size = new System.Drawing.Size(100, 100);
            this.btnReceipt.TabIndex = 11282;
            this.btnReceipt.UseVisualStyleBackColor = false;
            this.btnReceipt.Click += new System.EventHandler(this.btnReceipt_Click);
            // 
            // btnDeposit
            // 
            this.btnDeposit.BackColor = System.Drawing.Color.Transparent;
            this.btnDeposit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDeposit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeposit.Location = new System.Drawing.Point(245, 11);
            this.btnDeposit.Name = "btnDeposit";
            this.btnDeposit.Size = new System.Drawing.Size(100, 100);
            this.btnDeposit.TabIndex = 11284;
            this.btnDeposit.UseVisualStyleBackColor = false;
            this.btnDeposit.Click += new System.EventHandler(this.btnDeposit_Click);
            // 
            // btnExpenses
            // 
            this.btnExpenses.BackColor = System.Drawing.Color.Transparent;
            this.btnExpenses.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExpenses.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExpenses.Location = new System.Drawing.Point(359, 11);
            this.btnExpenses.Name = "btnExpenses";
            this.btnExpenses.Size = new System.Drawing.Size(100, 100);
            this.btnExpenses.TabIndex = 11275;
            this.btnExpenses.UseVisualStyleBackColor = false;
            this.btnExpenses.Click += new System.EventHandler(this.btnExpenses_Click);
            // 
            // btnPurchase
            // 
            this.btnPurchase.BackColor = System.Drawing.Color.Transparent;
            this.btnPurchase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPurchase.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPurchase.Location = new System.Drawing.Point(17, 12);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(100, 100);
            this.btnPurchase.TabIndex = 11274;
            this.btnPurchase.UseVisualStyleBackColor = false;
            this.btnPurchase.Click += new System.EventHandler(this.btnPurchase_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(701, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 100);
            this.btnExit.TabIndex = 11273;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPayment
            // 
            this.btnPayment.BackColor = System.Drawing.Color.Transparent;
            this.btnPayment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPayment.Location = new System.Drawing.Point(473, 11);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(100, 100);
            this.btnPayment.TabIndex = 11271;
            this.btnPayment.UseVisualStyleBackColor = false;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // btnSale
            // 
            this.btnSale.BackColor = System.Drawing.Color.Transparent;
            this.btnSale.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSale.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSale.Location = new System.Drawing.Point(131, 12);
            this.btnSale.Name = "btnSale";
            this.btnSale.Size = new System.Drawing.Size(100, 100);
            this.btnSale.TabIndex = 11270;
            this.btnSale.UseVisualStyleBackColor = false;
            this.btnSale.Click += new System.EventHandler(this.btnSale_Click);
            // 
            // TransactionMenuWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(820, 124);
            this.ControlBox = false;
            this.Controls.Add(this.btnDeposit);
            this.Controls.Add(this.btnExpenses);
            this.Controls.Add(this.btnPurchase);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.btnSale);
            this.Controls.Add(this.btnReceipt);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TransactionMenuWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TransactionMenuWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExpenses;
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Button btnSale;
        private System.Windows.Forms.Button btnReceipt;
        private System.Windows.Forms.Button btnDeposit;

    }
}