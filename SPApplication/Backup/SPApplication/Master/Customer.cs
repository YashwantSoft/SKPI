using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace SPApplication.Master
{
    public partial class Customer : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        int TableId = 0;
        bool DeleteFlag = false;
        string ExecuteType = "";
        bool SearchFlag = false;

        public Customer()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_CUSTOMER);
        }

        private void Customer_Load(object sender, EventArgs e)
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
                Save_Update_Delete();
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
                Save_Update_Delete();
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
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            ExecuteType = "";
            txtCustomerCode.Text = "";
            txtCustomerName.Text = "";
            txtAddress.Text = "";
            txtMobileNumber.Text = "";
            txtEmailId.Text = "";
            txtGSTNumber.Text = "";
            txtStateCode.Text = "";
            txtCustomerCode.Focus();
        }

        private bool Validation()
        {
            objEP.Clear();
            if (txtCustomerName.Text == "")
            {
                objEP.SetError(txtCustomerName, "Enter Customer Name");
                txtCustomerName.Focus();
                return true;
            }
            else if (txtAddress.Text == "")
            {
                objEP.SetError(txtAddress, "Enter Address");
                txtAddress.Focus();
                return true;
            }
            else if (txtMobileNumber.Text == "")
            {
                objEP.SetError(txtMobileNumber, "Enter Mobile Number");
                txtMobileNumber.Focus();
                return true;
            }
            else
                return false;
        }

        private bool AlreadyExist()
        {
            string ExistValue = txtCustomerName.Text.Replace(" ", "");
            string ExistValueAddress = txtAddress.Text.Replace(" ", "");
            DataSet ds = new DataSet();
            objBL.Query = "select ID from Customer where CancelTag=0 and Trim(CustomerName)='" + ExistValue + "' and  Trim(Address)='" + txtAddress.Text + "' and ID <> " + TableId + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void Save_Update_Delete()
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
                        if (DeleteFlag == true)
                        {
                            objBL.Query = "update Customer set CancelTag=1 where ID=" + TableId + "";
                            ExecuteType = "Delete";
                        }
                        else
                        {
                            if (!AlreadyExist())
                            {
                                objBL.Query = "update Customer set CustomerCode='" + txtCustomerCode.Text + "',CustomerName='" + txtCustomerName.Text + "',Address='" + txtAddress.Text + "',MobileNumber='" + txtMobileNumber.Text + "',EmailId='" + txtEmailId.Text + "',GSTNumber='" + txtGSTNumber.Text + "',StateCode='" + txtStateCode.Text + "',UserId= " + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + TableId + "";
                                ExecuteType = "Update";
                            }
                        }
                    }
                    else
                    {
                        objBL.Query = "insert into Customer(CustomerCode,CustomerName,Address,MobileNumber,EmailId,GSTNumber,StateCode,UserId) values('" + txtCustomerCode.Text + "','" + txtCustomerName.Text + "','" + txtAddress.Text + "','" + txtMobileNumber.Text + "','" + txtEmailId.Text + "','" + txtGSTNumber.Text + "','" + txtStateCode.Text + "'," + BusinessLayer.UserId_Static + ")";
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
                    objBL.Query = "select S.ID, (SELECT COUNT(*) FROM Customer S2 WHERE S2.ID <= S.ID and S2.CancelTag=0 and S.CancelTag=0) AS SrNo,S.CustomerCode,S.CustomerName as [Customer Name],S.Address,S.MobileNumber as [Mobile Number],S.EmailId,S.GSTNumber,S.StateCode from Customer S where S.CancelTag=0 order by S.ID";
                else
                    objBL.Query = "select S.ID, (SELECT COUNT(*) FROM Customer S2 WHERE S2.ID <= S.ID and S2.CancelTag=0 and S.CancelTag=0) AS SrNo,S.CustomerCode,S.CustomerName as [Customer Name],S.Address,S.MobileNumber as [Mobile Number],S.EmailId,S.GSTNumber,S.StateCode from Customer S where S.CancelTag=0 and S.CustomerName like '%" + txtSearch.Text + "%' order by S.ID";

                DataSet ds = new DataSet();
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblTotalCount.Text = "Total Count-" + ds.Tables[0].Rows.Count;
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[7].Visible = false;
                    //dataGridView1.Columns[0].Width = 60;
                    //dataGridView1.Columns[1].Width = 60;
                    dataGridView1.Columns[1].Width = 50;
                    dataGridView1.Columns[3].Width = 220;
                    dataGridView1.Columns[4].Width = 180;
                    dataGridView1.Columns[5].Width = 120;
                    dataGridView1.Columns[6].Width = 160;
                    //dataGridView1.Columns[7].Width = 100;

                    //for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    //{
                    //    dataGridView1.Columns[i].Width = 200;
                    //}
                }
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClearAll();
                btnDelete.Enabled = true;
                TableId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtCustomerCode.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtCustomerName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtMobileNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtEmailId.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtGSTNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtStateCode.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                 
                //if (objRL.Checks_Record_Allready_Exist(TableId, "Customer") == false)
                //{
                //    btnSave.Enabled = true;
                //    btnDelete.Enabled = true;
                //}
                //else
                //{
                //    btnSave.Enabled = false;
                //    btnDelete.Enabled = false;
                //}
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAddress.Focus();
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMobileNumber.Focus();
        }

        private void txtContactNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtEmailId.Focus();
        }

        private void txtEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtGSTNumber.Focus();
        }

       
        private void txtContactNumber_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtEmailId.Select();
        }

        private void txtCustomerCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.TxtNumericValue(sender, e, txtCustomerCode);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchFlag = true;
            else
                SearchFlag = false;

            FillGrid();
        }
    }
}
