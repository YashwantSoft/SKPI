using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using SPApplication.Master;
using SPApplication.Transaction;

namespace SPApplication
{
    public partial class TransactionMenuWindow : Form
    {
        DesignLayer objDL = new DesignLayer();
        RedundancyLogics objRL = new RedundancyLogics();

        public TransactionMenuWindow()
        {
            InitializeComponent();
            set_Design();
        }

        private void set_Design()
        {
            btnQualityControl.BackgroundImage = BusinessResources.QualityControl;
            btnProductionLabel.BackgroundImage = BusinessResources.ProductionLabel;
            btnCapLabel.BackgroundImage = BusinessResources.CapLabel;
            btnSalesOrder.BackgroundImage = BusinessResources.Client_Requiremts;
            btnPlanner.BackgroundImage = BusinessResources.Planner;
            btnRenewalEntry.BackgroundImage = BusinessResources.Renewal;
            btnQualityControl.BackgroundImage = BusinessResources.QualityControl;
            btnPurchaseOrder.BackgroundImage = BusinessResources.PurchaseOrder;
            btnEmployeeTemperature.BackgroundImage = BusinessResources.Temperature;
            btnTask.BackgroundImage = BusinessResources.Task;
            btnExit.BackgroundImage = BusinessResources.Exit;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
 

        private void TransactionMenuWindow_Load(object sender, EventArgs e)
        {

        }
         

        private void btnQualityControl_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION)
            {
                QualityAnalysis objForm = new QualityAnalysis();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnQRCode_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION)
            {
                ProductionLabel objForm = new ProductionLabel();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnCapLabel_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_STORE)
            {
                CapLabel objForm = new CapLabel();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }
    }
}
