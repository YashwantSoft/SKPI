using BusinessLayerUtility;
using SPApplication.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPApplication.View
{
    public partial class MaintenanceDashboard : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        System.Windows.Forms.Timer tmr = null;

        public MaintenanceDashboard()
        {
            InitializeComponent();
            SetDesign();
        }

        private void SetDesign()
        {
            lblHeader.BackColor = objDL.GetBackgroundColor();
            lblHeader.ForeColor = objDL.GetForeColor();

            btnItem.BackgroundImage = BusinessResources.Item;
            btnItemRequisition.BackgroundImage = BusinessResources.ItemRequisition;
            btnExit.BackgroundImage = BusinessResources.Exit;
            btnItem.BackgroundImage = BusinessResources.Item;
            btnItem.BackgroundImage = BusinessResources.Item;
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            Item objForm = new Item();
            objForm.ShowDialog(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnItemRequisition_Click(object sender, EventArgs e)
        {
            ItemRequisition objForm = new ItemRequisition();
            objForm.ShowDialog(this);
        }

        private void MaintenanceDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
