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

namespace SPApplication
{
    public partial class MasterMenuWindow : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        public MasterMenuWindow()
        {
            InitializeComponent();
            Set_Design();
        }

        private void Set_Design()
        {
            btnFirmInfo.BackgroundImage = BusinessResources.FirmInfo;
            btnManufracturer.BackgroundImage = BusinessResources.UOM;
            btnItemMaster.BackgroundImage = BusinessResources.Item;
            btnSupplier.BackgroundImage = BusinessResources.Supplier;
            btnCustomer.BackgroundImage = BusinessResources.Customer;
            btnExpenses.BackgroundImage = BusinessResources.Expenses;
            btnExit.BackgroundImage = BusinessResources.Exit;
        }

        private void btnFirmInfo_Click(object sender, EventArgs e)
        {
            CompanyInformation objForm = new CompanyInformation();
            objForm.ShowDialog(this);
        }

       

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            Customer objForm = new Customer();
            objForm.ShowDialog(this);
        }

        private void btnItemMaster_Click(object sender, EventArgs e)
        {
            Item objForm = new Item();
            objForm.ShowDialog(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            Supplier objForm = new Supplier();
            objForm.ShowDialog(this);
        }

        private void MasterMenuWindow_Load(object sender, EventArgs e)
        {

        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            ExpensesHead objForm = new ExpensesHead();
            objForm.ShowDialog(this);
        }

        private void btnManufracturer_Click(object sender, EventArgs e)
        {
            UnitOfMessurement objForm = new UnitOfMessurement();
            objForm.ShowDialog(this);
        }
    }
}
