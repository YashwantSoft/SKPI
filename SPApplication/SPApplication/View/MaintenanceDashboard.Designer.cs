namespace SPApplication.View
{
    partial class MaintenanceDashboard
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
            this.btnItem = new System.Windows.Forms.Button();
            this.btnPurchaseOrder = new System.Windows.Forms.Button();
            this.btnOutward = new System.Windows.Forms.Button();
            this.btnInward = new System.Windows.Forms.Button();
            this.btnItemRequisition = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.DarkViolet;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(-1, 1);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1199, 29);
            this.lblHeader.TabIndex = 70;
            this.lblHeader.Text = "Maintenance Dashboard";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnItem
            // 
            this.btnItem.BackColor = System.Drawing.Color.Transparent;
            this.btnItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnItem.Location = new System.Drawing.Point(174, 299);
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(100, 100);
            this.btnItem.TabIndex = 11356;
            this.btnItem.UseVisualStyleBackColor = false;
            this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
            // 
            // btnPurchaseOrder
            // 
            this.btnPurchaseOrder.BackColor = System.Drawing.Color.Transparent;
            this.btnPurchaseOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPurchaseOrder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPurchaseOrder.Location = new System.Drawing.Point(474, 299);
            this.btnPurchaseOrder.Name = "btnPurchaseOrder";
            this.btnPurchaseOrder.Size = new System.Drawing.Size(100, 100);
            this.btnPurchaseOrder.TabIndex = 11355;
            this.btnPurchaseOrder.UseVisualStyleBackColor = false;
            // 
            // btnOutward
            // 
            this.btnOutward.BackColor = System.Drawing.Color.Transparent;
            this.btnOutward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOutward.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOutward.Location = new System.Drawing.Point(774, 299);
            this.btnOutward.Name = "btnOutward";
            this.btnOutward.Size = new System.Drawing.Size(100, 100);
            this.btnOutward.TabIndex = 11354;
            this.btnOutward.UseVisualStyleBackColor = false;
            // 
            // btnInward
            // 
            this.btnInward.BackColor = System.Drawing.Color.Transparent;
            this.btnInward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnInward.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInward.Location = new System.Drawing.Point(624, 299);
            this.btnInward.Name = "btnInward";
            this.btnInward.Size = new System.Drawing.Size(100, 100);
            this.btnInward.TabIndex = 11353;
            this.btnInward.UseVisualStyleBackColor = false;
            // 
            // btnItemRequisition
            // 
            this.btnItemRequisition.BackColor = System.Drawing.Color.Transparent;
            this.btnItemRequisition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnItemRequisition.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnItemRequisition.Location = new System.Drawing.Point(324, 299);
            this.btnItemRequisition.Name = "btnItemRequisition";
            this.btnItemRequisition.Size = new System.Drawing.Size(100, 100);
            this.btnItemRequisition.TabIndex = 11352;
            this.btnItemRequisition.UseVisualStyleBackColor = false;
            this.btnItemRequisition.Click += new System.EventHandler(this.btnItemRequisition_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(924, 299);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 100);
            this.btnExit.TabIndex = 11351;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MaintenanceDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1198, 698);
            this.ControlBox = false;
            this.Controls.Add(this.btnItem);
            this.Controls.Add(this.btnPurchaseOrder);
            this.Controls.Add(this.btnOutward);
            this.Controls.Add(this.btnInward);
            this.Controls.Add(this.btnItemRequisition);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MaintenanceDashboard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MaintenanceDashboard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnItem;
        private System.Windows.Forms.Button btnPurchaseOrder;
        private System.Windows.Forms.Button btnOutward;
        private System.Windows.Forms.Button btnInward;
        private System.Windows.Forms.Button btnItemRequisition;
        private System.Windows.Forms.Button btnExit;
    }
}