using BusinessLayerUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPApplication
{
    public partial class Employee : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        int TableID = 0, Result = 0, Age = 0; string AsAbove = "No", Gender = "";
        bool FlagDelete = true;

        public Employee()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_EMPLOYEE);
            Fill_Destination_Designation();
        }

        private void Fill_Destination_Designation()
        {
            objBL.Query = "select ID,Department from DepartmentMaster where CancelTag=0";
            objBL.FillComboBox(cmbDepartment, "Department", "ID");

            objBL.Query = "select ID,Designation from DesignationMaster where CancelTag=0";
            objBL.FillComboBox(cmbDesignation, "Designation", "ID");
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            dtpJoiningDate.CustomFormat = "dd/MM/yyyy";
            dtpDOB.CustomFormat = "dd/MM/yyyy";
            FillGrid();
            cmbDesignation.Focus();
        }

        private void ClearAll()
        {
            objEP.Clear();
            
            TableID = 0;
            txtFullName.Text = "";

            rbFemale.Checked = false;
            rbMale.Checked = false;

            //dtpDOB.Value = DateTime.Now;
            lblAge.Text = "";
            cmbBloodGroup.SelectedIndex = -1;

            txtCurrentAddress.Text = "";
            cbAsAbove.Checked = false;
            AsAbove = "No";
            
            txtPermnentAddress.Text = "";
            txtMobileNo1.Text = "";
            txtMobileNo2.Text = "";
            txtEmailId.Text = "";
            dtpJoiningDate.Value = DateTime.Now;
            txtQualification.Text = "";
            cmbDepartment.SelectedIndex = -1;
            cmbDesignation.SelectedIndex = -1;
            txtQualification.Text = "";
            txtExperience.Text = "";
            txtSearch.Text = "";
            txtFullName.Text = "";
            txtSalaryPerDay.Text = "";
            txtSalaryPerMonth.Text = "";
        }

        private bool Validation()
        {
           if (txtFullName.Text == "")
            {
                txtFullName.Focus();
                objEP.SetError(txtFullName, "Enter First Name");
                return true;
            }
            else if (cmbBloodGroup.SelectedIndex == -1)
            {
                cmbBloodGroup.Focus();
                objEP.SetError(cmbBloodGroup, "Select Blood Group");
                return true;
            }
            else if (txtCurrentAddress.Text =="")
            {
                txtCurrentAddress.Focus();
                objEP.SetError(txtCurrentAddress, "Enter Current Address");
                return true;
            }
            else if (txtPermnentAddress.Text == "")
            {
                txtPermnentAddress.Focus();
                objEP.SetError(txtPermnentAddress, "Enter Permnent Address");
                return true;
            }
            else if (txtMobileNo1.Text == "")
            {
                txtMobileNo1.Focus();
                objEP.SetError(txtMobileNo1, "Enter Mobile No 1");
                return true;
            }
            else if (txtQualification.Text == "")
            {
                txtQualification.Focus();
                objEP.SetError(txtQualification, "Enter Qualification");
                return true;
            }
            else if (cmbDepartment.Text == "")
            {
                cmbDepartment.Focus();
                objEP.SetError(cmbDepartment, "Select Department");
                return true;
            }
            else if (cmbDesignation.Text == "")
            {
                cmbDesignation.Focus();
                objEP.SetError(cmbDesignation, "Select Designation");
                return true;
            }
            else
                return false;
        }

        string FullName = "", MobileNo = "";
        protected bool CheckExist()
        {
            FullName = txtFullName.Text;
            MobileNo = txtMobileNo1.Text;
            DataSet ds = new DataSet();
            objBL.Query = "select ID from Employee where CancelTag=0 and FullName='" + FullName + "' and MobileNo1='" + MobileNo + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        protected void SaveDB()
        {
            if (cbAsAbove.Checked)
                AsAbove = "Yes";
            else
                AsAbove = "No";

            if (TableID != 0)
                if (FlagDelete)
                    objBL.Query = "update Employee set CancelTag=1 where ID=" + TableID + "";
                else
                    objBL.Query = "update Employee set FullName='" + txtFullName.Text + "',Gender='" + Gender + "',DOB='" + dtpDOB.Value.ToString(RedundancyLogics.SystemDateFormat) + "',Age=" + Age + ",BloodGroup='" + cmbBloodGroup.Text + "',CurrentAddress='" + txtCurrentAddress.Text + "',AsAbove='" + AsAbove + "',PermenentAddress='" + txtPermnentAddress.Text + "',MobileNo1='" + txtMobileNo1.Text + "',MobileNo2='" + txtMobileNo2.Text + "',EmailId='" + txtEmailId.Text + "',DateOfJoining='" + dtpJoiningDate.Value.ToString(RedundancyLogics.SystemDateFormat) + "',Department='" + cmbDepartment.Text + "',Department='" + cmbDesignation.Text + "',Qualification='" + txtQualification.Text + "',Experience='" + txtExperience.Text + "',SalaryPerMonth='" + txtSalaryPerMonth.Text + "',SalaryPerDay='" + txtSalaryPerDay.Text + "',UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + TableID + "";
            else
                objBL.Query = "insert into Employee(FullName,Gender,DOB,Age,BloodGroup,CurrentAddress,AsAbove,PermenentAddress,MobileNo1,MobileNo2,EmailId,DateOfJoining,Department,Designation,Qualification,Experience,SalaryPerMonth,SalaryPerDay,UserId) values('" + txtFullName.Text + "','" + Gender + "','" + dtpDOB.Value.ToString(RedundancyLogics.SystemDateFormat) + "'," + Age + ",'" + cmbBloodGroup.Text + "','" + txtCurrentAddress.Text + "','" + AsAbove + "','" + txtPermnentAddress.Text + "','" + txtMobileNo1.Text + "','" + txtMobileNo2.Text + "','" + txtEmailId.Text + "','" + dtpJoiningDate.Value.ToString(RedundancyLogics.SystemDateFormat) + "','" + cmbDepartment.Text + "','" + cmbDesignation.Text + "','" + txtQualification.Text + "','" + txtExperience.Text + "','" + txtSalaryPerMonth.Text + "','" + txtSalaryPerDay.Text + "'," + BusinessLayer.UserId_Static + ")";

            Result = objBL.Function_ExecuteNonQuery();

            if (Result > 0)
                objRL.ShowMessage(7, 1);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            if (!Validation())
            {
                if (!CheckExist())
                {
                    SaveDB();
                    FillGrid();
                    ClearAll();
                }
                else
                {
                    objRL.ShowMessage(12, 4);
                    return;
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }
        string WhereClause=string.Empty;
        private void FillGrid()
        {
            WhereClause=string.Empty;
            dataGridView1.DataSource = null;
            lblTotalCount.Text = "";
            DataSet ds = new DataSet();

            if (SearchTag)
                WhereClause = " and FullName like '%" + txtSearch.Text + "%'";

            objBL.Query = "select ID,FullName as [Name],Gender,DOB,Age,BloodGroup as [Blood Group],CurrentAddress,AsAbove,PermenentAddress,MobileNo1,MobileNo2,EmailId,DateOfJoining,Department,Designation,Qualification,Experience,SalaryPerMonth,SalaryPerDay,UserId from Employee where CancelTag=0 " + WhereClause + "";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 ID,
                //1 FullName as [Name],
                //2 Gender,
                //3 DOB,
                //4 Age,
                //5 BloodGroup as [Blood Group],
                //6 CurrentAddress,
                //7 AsAbove,
                //8 PermenentAddress,
                //9 MobileNo1,
                //10 MobileNo2,
                //11 EmailId,
                //12 DateOfJoining,
                //13 Department,
                //14 Designation,
                //15 Qualification,
                //16 Experience,
                //17 UserId

                dataGridView1.DataSource = ds.Tables[0];
                lblTotalCount.Text = "Total Count-: " + ds.Tables[0].Rows.Count;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[17].Visible = false;
                dataGridView1.Columns[7].Visible = false;

                dataGridView1.Columns[1].Width = 200;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 120;
                dataGridView1.Columns[6].Width = 150;
                dataGridView1.Columns[8].Width = 150;
                dataGridView1.Columns[9].Width = 120;
                dataGridView1.Columns[10].Width = 120;
                dataGridView1.Columns[11].Width = 100;
                dataGridView1.Columns[12].Width = 120;
                dataGridView1.Columns[13].Width = 120;
                dataGridView1.Columns[14].Width = 120;
                dataGridView1.Columns[15].Width = 120;
                dataGridView1.Columns[16].Width = 120;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearAll();
            TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtFullName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            Gender = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            if (Gender == "Male") rbMale.Checked = true; else rbFemale.Checked = true;
            //dtpDOB.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
            //Age = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            lblAge.Text = "Age=" + Age + " years.";
            cmbBloodGroup.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtCurrentAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            AsAbove = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            if (AsAbove == "Yes")
                cbAsAbove.Checked = true;
            else
                cbAsAbove.Checked = false;

            txtPermnentAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtMobileNo1.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtMobileNo2.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            txtEmailId.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            Fill_Destination_Designation();
          //  dtpJoiningDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString());
            cmbDepartment.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            cmbDesignation.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
            txtQualification.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
            txtExperience.Text = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
            txtSalaryPerMonth.Text = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
            txtSalaryPerDay.Text = dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString();
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked == true)
            {
                Gender = "Male";
                rbFemale.Checked = false;
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked == true)
            {
                Gender = "Female";
                rbMale.Checked = false;
            }
        }

        private void cbAsAbove_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAsAbove.Checked)
            {
                AsAbove = "Yes";
                txtPermnentAddress.Text = txtCurrentAddress.Text;
            }
            else
            {
                AsAbove = "No";
                txtCurrentAddress.Text = "";
            }
        }

        int CurrentYear = 0, DOBYear = 0;

        private void ClearAgeAndMessage()
        {
            //dtpDOB.Value = DateTime.Now.Date;
            lblAge.Text = "";
            MessageBox.Show("Enter Valid Date of Birth");
        }

        private void CalculateAge()
        {
            CurrentYear = DateTime.Now.Date.Year; DOBYear = dtpDOB.Value.Year;
            Age = CurrentYear - DOBYear;

            if (Age < 18)
            {
                ClearAgeAndMessage();
                return;
            }
            else
            {
                if (Age > 100)
                {
                    ClearAgeAndMessage();
                    return;
                }
                else
                {
                    lblAge.Text = "Age=" + Age + " years.";
                }
            }
        }
        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            lblAge.Text = "";
            if (dtpDOB.Value > DateTime.Now.Date)
            {
                ClearAgeAndMessage();
                return;
            }
            else
            {
                CalculateAge();
            }
        }
        private void cmbDepartment_Leave(object sender, EventArgs e)
        {
            Save_Department(cmbDepartment.Text);
        }

        private void txtFullName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpDOB.Focus();
        }

        private void dtpDOB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbMale.Focus();
        }

        private void rbMale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbFemale.Focus();
        }

        private void rbFemale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbBloodGroup.Focus();
        }

        private void cmbBloodGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCurrentAddress.Focus();
        }

        private void txtCurrentAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cbAsAbove.Focus();
        }

        private void cbAsAbove_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPermnentAddress.Focus();
        }

        private void txtPermnentAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMobileNo1.Focus();
        }

        private void txtMobileNo1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMobileNo2.Focus();
        }

        private void txtMobileNo2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpDOB.Focus();
        }

        private void txtEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpJoiningDate.Focus();
        }

        private void dtpJoiningDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbDepartment.Focus();
        }

        private void cmbDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbDesignation.Focus();
        }

        private void cmbDesignation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQualification.Focus();
        }

        private void txtQualification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtExperience.Focus();
        }

        private void txtExperience_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
            dataGridView1.DataSource = null;
            cmbDesignation.SelectedIndex = -1;
            txtSearch.Text = "";
            WhereClause = "";
            SearchTag = false;
            FillGrid();
            cmbDesignation.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
        }

        private void cmbDesignation_Leave(object sender, EventArgs e)
        {
            Save_Designation(cmbDesignation.Text);
        }
        private void Save_Designation(string SaveComboValue)
        {
            if (SaveComboValue != "")
            {
                objBL.Query = "select ID from DesignationMaster where CancelTag=0 and Designation='" + SaveComboValue + "'";
                DataSet ds = new DataSet();
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count == 0)
                {
                    objBL.Query = "insert into DesignationMaster(Designation,UserId) values('" + SaveComboValue + "'," + BusinessLayer.UserId_Static + ")";
                    objBL.Function_ExecuteNonQuery();
                }
            }
        }
        private void Save_Department(string SaveComboValue)
        {
            if (SaveComboValue != "")
            {
                objBL.Query = "select ID from DepartmentMaster where CancelTag=0 and Department='" + SaveComboValue + "'";
                DataSet ds = new DataSet();
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count == 0)
                {
                    objBL.Query = "insert into DepartmentMaster(Department,UserId) values('" + SaveComboValue + "'," + BusinessLayer.UserId_Static + ")";
                    objBL.Function_ExecuteNonQuery();
                }
            }
        }

        bool SearchTag = false;

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

    }
}
