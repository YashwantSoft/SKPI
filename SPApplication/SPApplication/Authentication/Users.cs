using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace SPApplication.Authentication
{
    public partial class Users : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        int TableId = 0;
        bool DeleteFlag = false;
        string ExecuteType = "";
        bool SearchFlag = false;

        public Users()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_ADDUSER);
        }

        private void Users_Load(object sender, EventArgs e)
        {
            try
            {
                objRL.Add_Tool_Tip(btnSave, btnClear, btnDelete, btnExit);
                FillGrid();
                ClearAll();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDB();
            }
            catch (Exception ex1) { MessageBox.Show(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteFlag = true;
                SaveDB();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ClearAll()
        {
            TableId = 0;
            objEP.Clear();
            DeleteFlag = false;
            SearchFlag = false;
            
            ExecuteType = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtSearch.Text = "";

            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            txtPassword.Enabled = true;
            txtConfirmPassword.Enabled = true;
            txtUserName.Focus();
        }

        private bool Validation()
        {
            objEP.Clear();
            if (txtUserName.Text == "")
            {
                objEP.SetError(txtUserName, "Enter User Name");
                txtUserName.Focus();
                return true;
            }
            else if (txtPassword.Text == "")
            {
                objEP.SetError(txtPassword, "Enter Password");
                txtPassword.Focus();
                return true;
            }
            else if (txtConfirmPassword.Text == "")
            {
                objEP.SetError(txtConfirmPassword, "Enter Confirm Password");
                txtConfirmPassword.Focus();
                return true;
            }
            else if (txtConfirmPassword.Text != txtPassword.Text)
            {
                objEP.SetError(txtPassword, "Not Match");
                objEP.SetError(txtConfirmPassword, "Not Match");
                txtConfirmPassword.Focus();
                return true;
            }
            else
                return false;
        }

        private bool AlreadyExist()
        {
            string ExistValue = txtUserName.Text.Replace(" ", "");
            DataSet ds = new DataSet();
            objBL.Query = "select ID from Login where CancelTag=0 and Trim(UserName)='" + ExistValue + "' and ID <> " + TableId + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void SaveDB()
        {
            try
            {
                if (!Validation())
                {
                    if (DeleteFlag == false && TableId != 0 || TableId == 0)
                    {
                        if (AlreadyExist())
                        {
                            objRL.ShowMessage(12, 5);
                            return;
                        }
                    }

                    if (TableId != 0)
                    {
                        if (DeleteFlag)
                        {
                            objBL.Query = "update Login set CancelTag=1 where ID=" + TableId + "";
                            ExecuteType = "Delete";
                        }
                    }
                    else
                    {
                        string Password = BusinessLayer.Encrypt(txtPassword.Text, true);
                        objBL.Query = "insert into Login(UserName,[Password]) values('" + txtUserName.Text + "','" + Password + "')";
                        ExecuteType = "Save";
                    }

                    int Result = objBL.Function_ExecuteNonQuery();
                    if (Result > 0)
                    {
                        if (ExecuteType == "Save")
                            objRL.ShowMessage(7, 1);
                        else if (ExecuteType == "Update")
                            objRL.ShowMessage(8, 1);
                        else
                            objRL.ShowMessage(9, 1);

                        FillGrid();
                        ClearAll();
                    }
                }
                else
                {
                    objRL.ShowMessage(17, 4);
                    return;
                }
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void FillGrid()
        {
            try
            {
                if (!SearchFlag)
                    objBL.Query = "select S.ID, (SELECT COUNT(*) FROM Login S2 WHERE S2.ID <= S.ID and S2.CancelTag=0 and S.CancelTag=0) AS SrNo,S.UserName from Login S where S.CancelTag=0 order by S.ID";
                else
                    objBL.Query = "select S.ID, (SELECT COUNT(*) FROM Login S2 WHERE S2.ID <= S.ID and S2.CancelTag=0 and S.CancelTag=0) AS SrNo,S.UserName from Login S where S.CancelTag=0 and S.UserName like '%" + txtSearch.Text + "%' order by S.ID";

                DataSet ds = new DataSet();
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblTotalCount.Text = "Total Count-" + ds.Tables[0].Rows.Count;
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Width = 60;
                    dataGridView1.Columns[2].Width = 760;
                }
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchFlag = true;
            else
                SearchFlag = false;

            FillGrid();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClearAll();
                TableId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtUserName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                btnSave.Enabled = false;
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }
    }
}
