using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPApplication.Transaction;
using BusinessLayerUtility;
using SPApplication.Reports;
using TestApplication;
using System.Diagnostics;

namespace SPApplication
{
    public partial class Dashboard : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        System.Windows.Forms.Timer tmr = null;

        public Dashboard()
        {
            InitializeComponent();
            Set_Design();
        }

        private void Set_Design()
        {
            btnMaster.BackgroundImage = BusinessResources.Masters;
            btnTransaction.BackgroundImage = BusinessResources.Transaction;
            btnReports.BackgroundImage = BusinessResources.Reports;
            btnSettings.BackgroundImage = BusinessResources.Settings;
            btnExit.BackgroundImage = BusinessResources.Exit;
            pbProductLogo.Image = BusinessResources.ProductLogo;
            pbClientLogo.Image = BusinessResources.ClientLogo;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            Master.Customer objForm = new Master.Customer();
            objForm.ShowDialog(this);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportList objForm = new ReportList();
            objForm.ShowDialog(this);
        }

        private void btnMaster_Click(object sender, EventArgs e)
        {
            MasterMenuWindow objForm = new MasterMenuWindow();
            objForm.ShowDialog(this);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            StartTimer();
            //pbMainImage.Image = BusinessResources.LogoKetan;            
            lblUser.Text = "Welcome " + BusinessLayer.UserName_Static ;
        }
        
        private void StartTimer()
        {
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 1000;
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Enabled = true;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            lblDateTimeRunning.Text = DateTime.Now.ToString("dd/MMM/yyyy hh:MM ss tt");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sale objForm = new Sale();
            objForm.ShowDialog(this);
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            Sale objForm = new Sale();
            objForm.ShowDialog(this);
        }

        //private void btnCRNote_Click(object sender, EventArgs e)
        //{
        //    CRNote objForm = new CRNote();
        //    objForm.ShowDialog(this);
        //}

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            Purchase objForm = new Purchase();
            objForm.ShowDialog(this);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsList objForm = new SettingsList();
            objForm.ShowDialog(this);
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            TransactionMenuWindow objForm = new TransactionMenuWindow();
            objForm.ShowDialog(this);
        }

        private void btnDailyBook_Click(object sender, EventArgs e)
        {
            DailyBookEntry objForm = new DailyBookEntry();
            objForm.ShowDialog(this);
        }

        private void btnSupplierPayment_Click(object sender, EventArgs e)
        {
            BillDetails objForm = new BillDetails();
            objForm.ShowDialog(this);
        }

        private void lbHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://2dkapps.com/");
            Process.Start(sInfo);
        }
    }
}
