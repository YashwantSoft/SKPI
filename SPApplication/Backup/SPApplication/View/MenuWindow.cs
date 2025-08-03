using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using SPApplication.Authentication;
using SPApplication.Transaction;
using SPApplication.Master;

namespace SPApplication
{
    public partial class MenuWindow : Form
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        BusinessLayer objBL = new BusinessLayer();

        private void MenuWindow_Load(object sender, EventArgs e)
        {
            //LoginWindow objForm = new LoginWindow();
            //objForm.ShowDialog(this);

           

            //this.label1.Text = datetime.ToString();
            DateTime datetime = DateTime.Now;
            StartTimer();
            lblUserName.Text += BusinessLayer.UserName_Static;
    //        this.label1.Text =
    //this.monthCalendar1.SelectionRange.Start.ToShortDateString();
        }

        System.Windows.Forms.Timer tmr = null;
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaster_Click(object sender, EventArgs e)
        {
            MasterMenuWindow objForm = new MasterMenuWindow();
            objForm.ShowDialog(this);

        }

        private void btnReport_Click(object sender, EventArgs e)
        {

        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword objForm = new ChangePassword();
            objForm.ShowDialog(this);
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            TransactionMenuWindow objForm = new TransactionMenuWindow();
            objForm.ShowDialog(this);
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            Purchase objForm = new Purchase();
            objForm.ShowDialog(this);
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            Sale objForm = new Sale();
            objForm.ShowDialog(this);
        }

        private void btnCompanyInformation_Click(object sender, EventArgs e)
        {
            CompanyInformation objForm = new CompanyInformation();
            objForm.ShowDialog(this);
        }
    }
}
