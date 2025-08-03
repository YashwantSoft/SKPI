namespace SPApplication.Planning
{
    partial class SalesOrderPlanning
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
            this.label4 = new System.Windows.Forms.Label();
            this.pbQRCode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(192, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 11426;
            this.label4.Text = "QR Code ";
            // 
            // pbQRCode
            // 
            this.pbQRCode.Location = new System.Drawing.Point(188, 85);
            this.pbQRCode.Name = "pbQRCode";
            this.pbQRCode.Size = new System.Drawing.Size(233, 211);
            this.pbQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbQRCode.TabIndex = 11425;
            this.pbQRCode.TabStop = false;
            // 
            // SalesOrderPlanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 363);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pbQRCode);
            this.Name = "SalesOrderPlanning";
            this.Text = "SalesOrderPlanning";
            this.Load += new System.EventHandler(this.SalesOrderPlanning_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbQRCode;
    }
}