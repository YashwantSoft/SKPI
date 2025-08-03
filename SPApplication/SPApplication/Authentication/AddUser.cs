using BusinessLayerUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPApplication.Master
{
    public partial class AddUser : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        int TableID = 0, Result = 0, Age = 0; string AsAbove = "No", Gender = "";
        bool FlagDelete = true;

        public AddUser()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_ADDUSER);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void StaffMaster_Load(object sender, EventArgs e)
        {
            dtpJoiningDate.CustomFormat = "dd/MM/yyyy";
            dtpDOB.CustomFormat = "dd/MM/yyyy";

            objBL.Query = "select ID,Speciallists from SpectialistsMaster";
            objBL.FillComboBox(cmbSpeciality, "Speciallists", "ID");
            Fill_OnebyOne_ComboBox(cmbDesignation);
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

            txtQualification.Text = "";
            txtRegNo.Text = "";
            cmbSpeciality.SelectedIndex = -1;
            txtExperience.Text = "";
            dtpJoiningDate.Value = DateTime.Now;

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";

            txtSearchByFirstName.Text = "";
            txtMobileNumber.Text = "";
        }

        private bool Validation()
        {
            if (DesignationId == 0)
            {
                cmbDesignation.Focus();
                objEP.SetError(cmbDesignation, "Select Designation");
                return true;
            }
            else if (txtFullName.Text == "")
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
            else if (txtUserName.Text == "")
            {
                txtUserName.Focus();
                objEP.SetError(txtUserName, "Enter User Name");
                return true;
            }
            else if (txtPassword.Text == "")
            {
                txtPassword.Focus();
                objEP.SetError(txtPassword, "Enter Password");
                return true;
            }
            else if (txtConfirmPassword.Text == "")
            {
                txtConfirmPassword.Focus();
                objEP.SetError(txtConfirmPassword, "Enter Confirm Password");
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
            objBL.Query = "select ID from Staff where CancelTag=0 and FullName='" + FullName + "' and MobileNo1='" + MobileNo + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        protected void SaveDB()
        {
            if (TableID != 0)
                if (FlagDelete == true)
                    objBL.Query = "update Staff set CancelTag=1 where ID=" + TableID + "";
                else
                    objBL.Query = "update Staff set DesignationId=" + DesignationId + ",FullName='" + txtFullName.Text + "',Gender='" + Gender + "',DOB='" + dtpDOB.Value.ToString(RedundancyLogics.SystemDateFormat) + "',Age=" + Age + ",BloodGroup='" + cmbBloodGroup.Text + "',CurrentAddress='" + txtCurrentAddress.Text + "',AsAbove='" + AsAbove + "',PermenentAddress='" + txtPermnentAddress.Text + "',MobileNo1='" + txtMobileNo1.Text + "',MobileNo2='" + txtMobileNo2.Text + "',EmailId='" + txtEmailId.Text + "',Qualification='" + txtQualification.Text + "',RegNo='" + txtRegNo.Text + "',Speciality='" + cmbSpeciality.Text + "',Experience='" + txtExperience.Text + "',DateOfJoining='" + dtpJoiningDate.Value.ToString(RedundancyLogics.SystemDateFormat) + "',UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + TableID + "";
            else
            {
                if (txtConfirmPassword.Text != txtPassword.Text)
                {
                    txtConfirmPassword.Text = ""; txtPassword.Text = "";
                    MessageBox.Show("Password Value not matched");
                    txtPassword.Focus();
                    return;
                }
                else
                {
                    objBL.Query = "insert into Staff(DesignationId,FullName,Gender,DOB,Age,BloodGroup,CurrentAddress,AsAbove,PermenentAddress,MobileNo1,MobileNo2,EmailId,Qualification,RegNo,Speciality,Experience,DateOfJoining,UserId) values(" + DesignationId + ",'" + txtFullName.Text + "','" + Gender + "','" + dtpDOB.Value.ToString(RedundancyLogics.SystemDateFormat) + "'," + Age + ",'" + cmbBloodGroup.Text + "','" + txtCurrentAddress.Text + "','" + AsAbove + "','" + txtPermnentAddress.Text + "','" + txtMobileNo1.Text + "','" + txtMobileNo2.Text + "','" + txtEmailId.Text + "','" + txtQualification.Text + "','" + txtRegNo.Text + "','" + cmbSpeciality.Text + "','" + txtExperience.Text + "','" + dtpJoiningDate.Value.ToString(RedundancyLogics.SystemDateFormat) + "'," + BusinessLayer.UserId_Static + ")";
                }
            }
            Result = objBL.Function_ExecuteNonQuery();
            if (TableID == 0)
            {
                if (Result > 0)
                {
                    int UserId = objRL.ReturnMaxID("Staff");
                    string Password = BusinessLayer.Encrypt(txtPassword.Text, true);
                    objBL.Query = "insert into Login(UserName,[Password],UserId) values('" + txtUserName.Text + "','" + Password + "', " + UserId + ")";
                    objBL.Function_ExecuteNonQuery();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                if (!CheckExist())
                {
                    SaveDB();
                    FillGrid();
                    ClearAll();
                    objRL.ShowMessage(7, 1);
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

        private void FillGrid()
        {
            if (cmbDesignation.SelectedIndex > -1)
            {
                DataSet ds = new DataSet();
                objBL.Query = "select ID,DesignationId,FullName,Gender,DOB,Age,BloodGroup,CurrentAddress,AsAbove,PermenentAddress,MobileNo1,MobileNo2,EmailId,Qualification,RegNo,Speciality,Experience,DateOfJoining from Staff where CancelTag=0 and DesignationId=" + DesignationId+ "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;

                    dataGridView1.Columns[2].Width = 200;
                    dataGridView1.Columns[3].Width = 100;
                    dataGridView1.Columns[4].Width = 100;
                    dataGridView1.Columns[5].Width = 100;
                    dataGridView1.Columns[6].Width = 100;
                    dataGridView1.Columns[7].Width = 100;
                    dataGridView1.Columns[8].Width = 100;
                    dataGridView1.Columns[9].Width = 100;
                    dataGridView1.Columns[10].Width = 100;

                    dataGridView1.DefaultCellStyle.SelectionBackColor = objBL.GetRowSelectionColor();
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearAll();
            TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            DesignationId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            //cmbDesignation.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtFullName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            Gender = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            if (Gender == "Male") rbMale.Checked = true; else rbFemale.Checked = true;
            //dtpDOB.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            dtpDOB.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            Age = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
            lblAge.Text = "Age=" + Age + " years.";
            cmbBloodGroup.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtCurrentAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            AsAbove = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            if (AsAbove == "Yes")
                cbAsAbove.Checked = true;
            txtPermnentAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtMobileNo1.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            txtMobileNo2.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            txtEmailId.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
            txtQualification.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            txtRegNo.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
            cmbSpeciality.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
            txtExperience.Text = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
            //dtpJoiningDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString());
            dtpJoiningDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString());
            gbLoginInformation.Enabled = false;
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
            if (cbAsAbove.Checked == true)
            {
                txtPermnentAddress.Text = txtCurrentAddress.Text;
            }
            else
            {
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

        int DesignationId = 0;

        private void cmbDesignation_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbDesignation.SelectedIndex > -1)
            {
                if (cmbDesignation.Text != "")
                {
                    DesignationId = objRL.Fill_Staff_DesignationID(cmbDesignation.Text);
                    if (DesignationId != 0)
                    {
                        FillGrid();
                    }
                    else
                        return;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
            dataGridView1.DataSource = null;
            cmbDesignation.SelectedIndex = -1;
            cmbDesignation.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnAddDesignation_Click(object sender, EventArgs e)
        {
           
        }

        private void cmbDesignation_Leave(object sender, EventArgs e)
        {
            Save_WardDetails(cmbDesignation.Text);
        }

        private void Fill_OnebyOne_ComboBox(ComboBox cmb)
        {
            //cmb.Items.Clear();
            DataSet ds = new DataSet();
            objBL.Query = "select ID,Designation from DesignationMaster where CancelTag=0";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cmb.Items.Add(ds.Tables[0].Rows[i][1].ToString());
                }
        }

        private void Save_WardDetails(string SaveComboValue)
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

        private void cmbDesignation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtFullName.Focus();
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
                cmbBloodGroup.Focus();
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
                txtEmailId.Focus();
        }

        private void txtEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQualification.Focus();
        }

        private void txtQualification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRegNo.Focus();
        }

        private void txtRegNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbSpeciality.Focus();
        }

        private void cmbSpeciality_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtExperience.Focus();
        }

        private void txtExperience_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpJoiningDate.Focus();
        }

        private void dtpJoiningDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtUserName.Focus();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtConfirmPassword.Focus();
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

    }
}
