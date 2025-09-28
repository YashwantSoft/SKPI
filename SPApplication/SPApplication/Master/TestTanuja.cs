using BusinessLayerUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPApplication.Master
{
    public partial class TestTanuja : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        int TableId = 0;
        bool DeleteFlag = false;
        string ExecuteType = "";
        bool SearchFlag = false;

        public TestTanuja()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_CUSTOMER);
        }

       
        private void TestTanuja_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        double A = 0;
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            Concat = string.Empty;
            string Passsport = string.Empty,Relative=string.Empty,Gender=string.Empty;

            if (cbIsPassport.Checked)
                Passsport = "Yes"; 
            else
                Passsport = "No";


            for (int i = 0; i < clbRelative.CheckedItems.Count; i++)
            {
                A = 90.78;

                i = Convert.ToInt32(Math.Round(A));

                Relative += clbRelative.Items[i].ToString() + ",";
            }

            if (rbMale.Checked)
                Gender = "Male";
            if(rbFemale.Checked)
                Gender = "Female";


            Concat = "Name:" + txtName.Text + "\n" +
                     "Country:" + cmbCountry.Text + "\n" +
                     "Is passport:" + Passsport + "\n" +
                     "Relative:" + Relative + "\n" +
                      "Gender:" + Gender;

            rtbDisplayInformation.Text = Concat.ToString();


            MessageBox.Show(cmbCountry.Text);
        }

        string Concat = string.Empty;
        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbCountry.SelectedIndex = -1;

          
        }
    }
}
