using SPApplication.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using SPApplication.Transaction;

namespace SPApplication
{
    public partial class MasterMenuWindow : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();

        public MasterMenuWindow()
        {
            InitializeComponent();
            Set_Design();
        }

        private void Set_Design()
        {
            btnFirmInfo.BackgroundImage = BusinessResources.FirmInfo;
            btnMouldMaster.BackgroundImage = BusinessResources.MouldMaster;
            btnManufracturer.BackgroundImage = BusinessResources.UOM;
            btnItemMaster.BackgroundImage = BusinessResources.Product;
            btnEmployee.BackgroundImage = BusinessResources.Employee;
            btnPreform.BackgroundImage = BusinessResources.Preform;
            btnCustomer.BackgroundImage = BusinessResources.Customer;
            btnTaskMaster.BackgroundImage = BusinessResources.TaskMaster;
            btnPreformPartyMaster.BackgroundImage = BusinessResources.PreformSupplier;
            btnUsedForMaster.BackgroundImage = BusinessResources.UsedFor;
            btnPlaceMaster.BackgroundImage = BusinessResources.PlaceMaster;
            btnCapMaster.BackgroundImage = BusinessResources.CapMaster;
            btnPartMaster.BackgroundImage = BusinessResources.PartMaster;
            btnMachineMaster.BackgroundImage = BusinessResources.MachineMaster;
            btnShiftSchedule.BackgroundImage = BusinessResources.ShiftSchedule;
            btnExit.BackgroundImage = BusinessResources.Exit;
        }
        private void btnFirmInfo_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN)
            {
                CompanyInformation objForm = new CompanyInformation();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnItemMaster_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION)
             //   if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN)
            {
                Product objForm = new Product();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void MasterMenuWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void btnManufracturer_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN)
            {
                UnitOfMessurement objForm = new UnitOfMessurement();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN)
            {
                Employee objForm = new Employee();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnMouldMaster_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION)
            //    if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN)
            {
                MouldMaster objForm = new MouldMaster();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnQualityControl_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION)
            {
                Transaction.QualityAnalysis objForm = new Transaction.QualityAnalysis();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnPreform_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION)
            //    if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN)
            {
                Preform objForm = new Preform();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN)
            {
                Customer objForm = new Customer();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnTaskMaster_Click(object sender, EventArgs e)
        {
            TaskMaster objForm = new TaskMaster();
            objForm.ShowDialog(this);
        }

        private void btnPreformPartyMaster_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN)
            {
                PreformPartyMaster objForm = new PreformPartyMaster();
                objForm.ShowDialog(this);
            }
        }

        private void btnPartMaster_Click(object sender, EventArgs e)
        {
            PartMaster objForm = new PartMaster();
            objForm.ShowDialog(this);
        }

        private void btnUsedForMaster_Click(object sender, EventArgs e)
        {
            UsedForMaster objForm = new UsedForMaster();
            objForm.ShowDialog(this);
        }

        private void btnPlaceMaster_Click(object sender, EventArgs e)
        {
            PlaceMaster objForm = new PlaceMaster();
            objForm.ShowDialog(this);
        }

        private void btnCapMaster_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_STORE)
            {
                CapMaster objForm = new CapMaster();
                objForm.ShowDialog(this);
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnMachineMaster_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN)
            {
                MachineMaster objForm = new MachineMaster();
                objForm.ShowDialog(this);
            }
        }

        private void btnShiftSchedule_Click(object sender, EventArgs e)
        {
            ShiftSchedule objForm = new ShiftSchedule();
            objForm.ShowDialog(this);
        }

        private void btnWadMaster_Click(object sender, EventArgs e)
        {
            WadMaster objForm = new WadMaster();
            objForm.ShowDialog(this);
        }
    }
}
