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
using System.Diagnostics;
using SPApplication.Master;
using SPApplication.Authentication;
using SPApplication.View;
using SPApplication.Planning;

namespace SPApplication
{
    public partial class Dashboard : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();

        System.Windows.Forms.Timer tmr = null;

        public Dashboard()
        {
            InitializeComponent();
            Set_Design();
        }

        private void Set_Design()
        {
            btnQualityControl.BackgroundImage = BusinessResources.QualityControl;
            btnProductionLabel.BackgroundImage = BusinessResources.ProductionLabel;
            btnCapLabel.BackgroundImage = BusinessResources.CapLabel;
            btnSalesOrder.BackgroundImage = BusinessResources.Client_Requiremts;
            btnPlanner.BackgroundImage = BusinessResources.Planner;
            btnRenewalEntry.BackgroundImage = BusinessResources.Renewal;
            btnMaster.BackgroundImage = BusinessResources.Masters;
            btnTransaction.BackgroundImage = BusinessResources.Transaction;
            btnReports.BackgroundImage = BusinessResources.Reports;
            btnSettings.BackgroundImage = BusinessResources.Settings;
            btnQualityControl.BackgroundImage = BusinessResources.QualityControl;
            btnPurchaseOrder.BackgroundImage = BusinessResources.PurchaseOrder;
            btnEmployeeTemperature.BackgroundImage = BusinessResources.Temperature;
            btnTask.BackgroundImage = BusinessResources.Task;
            btnOEE.BackgroundImage = BusinessResources.OEE;
            btnExit.BackgroundImage = BusinessResources.Exit;
            pbProductLogo.Image = BusinessResources.ProductLogo;
            pbClientLogo.Image = BusinessResources.ClientLogo;
            btnKanBan.BackgroundImage = BusinessResources.DispatchSchedule;
            btnMaintenance.BackgroundImage = BusinessResources.Maintenance;
            btnShiftEntry.BackgroundImage = BusinessResources.ShiftSchedule;
            this.Icon = BusinessResources.ICOLogo;

            btnOtherProduct.BackgroundImage = BusinessResources.OtherProducts;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_QC || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_AJAY || BusinessLayer.UserName_Static == BusinessResources.USER_VIJAY || BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS1)
            {
                ReportList objForm = new ReportList();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
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
            lblUser.Text = "Welcome " + BusinessLayer.UserName_Static;
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
            //lblDateTimeRunning.Text = DateTime.Now.ToString("dd/MMM/yyyy hh:MM ss tt");
            lblDateTimeRunning.Text = DateTime.Now.ToString("dddd , dd/MMM/yyyy, hh:mm:ss tt");
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

        private void lbHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://2dkapps.com/");
            Process.Start(sInfo);
        }

        private void btnQualityControl_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION || BusinessLayer.UserName_Static == BusinessResources.USER_QC ||  BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS)
            {
                QualityControl objForm = new QualityControl();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnProductionLabel_Click(object sender, EventArgs e)
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

        private void btnItem_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION)
            {
                Product objForm = new Product();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnSalesOrder_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS1)
            {
                SalesOrder objForm = new SalesOrder();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnPurchaseOrder_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_STORE || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS1)
            {
                PurchaseOrder objForm = new PurchaseOrder();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Production Label
            if (keyData == Keys.F1)
            {
                if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION)
                {
                    ProductionLabel objForm = new ProductionLabel();
                    objForm.ShowDialog(this);
                    return true;
                }
                else
                {
                    objRL.ShowMessage(30, 4);
                    return false;
                }
            }
            //Cap Label
            if (keyData == Keys.F2)
            {
                if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_STORE)
                {
                    CapLabel objForm = new CapLabel();
                    objForm.ShowDialog(this);
                    return true;
                }
                else
                {
                    objRL.ShowMessage(30, 4);
                    return false;
                }
            }
            //Sales Order
            if (keyData == Keys.F3)
            {
                if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS1)
                {
                    SalesOrder objForm = new SalesOrder();
                    objForm.ShowDialog(this);
                    return true;
                }
                else
                {
                    objRL.ShowMessage(30, 4);
                    return false;
                }
            }
            //Purchase Order
            if (keyData == Keys.F4)
            {
                if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_STORE || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS1)
                {
                    PurchaseOrder objForm = new PurchaseOrder();
                    objForm.ShowDialog(this);
                    return true;
                }
                else
                {
                    objRL.ShowMessage(30, 4);
                    return false;
                }

            }
            //Quality Control
            if (keyData == Keys.F5)
            {
                if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION)
                {
                    QualityAnalysis objForm = new QualityAnalysis();
                    objForm.ShowDialog(this);
                    return true;
                }
                else
                {
                    objRL.ShowMessage(30, 4);
                    return false;
                }
            }
            //Import Tally Data
            if (keyData == Keys.F6)
            {
                if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION || BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS || BusinessLayer.UserName_Static == BusinessResources.USER_STORE || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS1)
                {
                    ImportTallyData objForm = new ImportTallyData();
                    objForm.ShowDialog(this);
                    return true;
                }
                else
                {
                    objRL.ShowMessage(30, 4);
                    return false;
                }
            }
            if (keyData == Keys.F7)
            {
                    EmployeeTemperature objForm = new EmployeeTemperature();
                    objForm.ShowDialog(this);
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnEmployeeTemperature_Click(object sender, EventArgs e)
        {
            //if (BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION || BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_STORE || BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS)
            //    {
            //        CapLabel objForm = new CapLabel();
            //        objForm.ShowDialog(this);
            //        return true;
            //    }
            //    else
            //    {
            //        objRL.ShowMessage(30, 4);
            //        return false;
            //    }
            EmployeeTemperature objForm = new EmployeeTemperature();
            objForm.ShowDialog(this);
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static != BusinessResources.USER_QC)
            {
                AssignTask objForm = new AssignTask();
                //objForm.Show();
                objForm.ShowDialog(this);

                //this.WindowState = FormWindowState.Minimized;
                //this.DebugForm.Show();
                //DebugForm.WindowState = FormWindowState.Normal;
                //DebugForm.BringToFront();
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnPlanner_Click(object sender, EventArgs e)
        {
            Planner objForm = new Planner();
            objForm.ShowDialog(this);
        }

        private void btnRenewalEntry_Click(object sender, EventArgs e)
        {
            PartRenewalEntry objForm = new PartRenewalEntry();
            objForm.ShowDialog(this);
        }

        private void btnOEE_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_QC || BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION || BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS)
            {
                OEECalculation objForm = new OEECalculation();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnKanBan_Click(object sender, EventArgs e)
        {
            DispatchSchedule objForm = new DispatchSchedule();
            objForm.ShowDialog(this);
        }

        private void btnOtherProduct_Click(object sender, EventArgs e)
        {
            ProductPrintQRCode objForm = new ProductPrintQRCode();
            objForm.ShowDialog(this);
        }

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static != BusinessResources.USER_QC)
            {
                MaintenanceDashboard objForm = new MaintenanceDashboard();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnShiftEntry_Click(object sender, EventArgs e)
        {
            ShiftScheduleNew objForm = new ShiftScheduleNew();
            objForm.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SalesOrderPlanning objForm = new SalesOrderPlanning();
            objForm.ShowDialog(this);
        }
    }
}
