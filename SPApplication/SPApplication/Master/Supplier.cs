using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace SPApplication.Master
{
    public partial class Supplier : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        System.Globalization.RegionInfo objRegInfo = new RegionInfo("en-IN");
        int TableId = 0;
        bool DeleteFlag = false, SearchFlag = false;
        string ExecuteType = "";

        public Supplier()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_SUPPLIER);
        }

        private void Supplier_Load(object sender, EventArgs e)
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

        private void GetSupplierNo()
        {
            int CustomerNo = objRL.ReturnMaxID("Supplier");
            txtSupplierCode.Text = CustomerNo.ToString();
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
            btnDelete.Enabled = true;
            ExecuteType = "";
            txtSupplierCode.Text = "";
            txtSupplierName.Text = "";
            txtAddress.Text = "";
            txtMobileNumber.Text = "";
            txtEmailId.Text = "";
            txtGSTNumber.Text = "";
            txtStateCode.Text = "";
            GetSupplierNo();
            txtSupplierCode.Focus();
        }

        private bool Validation()
        {
            objEP.Clear();
            if (txtSupplierName.Text == "")
            {
                objEP.SetError(txtSupplierName, "Enter Supplier Name");
                txtSupplierName.Focus();
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
            string ExistValue = txtSupplierName.Text.Replace(" ", "");
            string ExistValueAddress = txtAddress.Text.Replace(" ", "");
            DataSet ds = new DataSet();
            objBL.Query = "select ID from Supplier where CancelTag=0 and Trim(SupplierName)='" + ExistValue.Replace("'", "''") + "' and  Trim(Address)='" + txtAddress.Text.Replace("'", "''") + "' and ID <> " + TableId + "";
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
                            objBL.Query = "update Supplier set CancelTag=1 where ID=" + TableId + "";
                            ExecuteType = "Delete";
                        }
                        else
                        {
                            if (!AlreadyExist())
                            {
                                objBL.Query = "update Supplier set SupplierCode='" + txtSupplierCode.Text + "',SupplierName='" + objRL.ApostropheSave(txtSupplierName.Text) + "',Address='" + txtAddress.Text.Replace("'", "''") + "',MobileNumber='" + txtMobileNumber.Text + "',EmailId='" + txtEmailId.Text + "',GSTNumber='" + txtGSTNumber.Text + "',StateCode='" + txtStateCode.Text + "',UserId= " + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + TableId + "";
                                ExecuteType = "Update";
                            }
                        }
                    }
                    else
                    {
                        objBL.Query = "insert into Supplier(SupplierCode,SupplierName,Address,MobileNumber,EmailId,GSTNumber,StateCode,UserId) values('" + txtSupplierCode.Text + "','" + txtSupplierName.Text.Replace("'", "''") + "','" + txtAddress.Text.Replace("'", "''") + "','" + txtMobileNumber.Text + "','" + txtEmailId.Text + "','" + txtGSTNumber.Text + "','" + txtStateCode.Text + "'," + BusinessLayer.UserId_Static + ")";
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
                if (SearchFlag == false)
                    objBL.Query = "select S.ID, (SELECT COUNT(*) FROM Supplier S2 WHERE S2.ID <= S.ID and S2.CancelTag=0 and S.CancelTag=0) AS SrNo,S.SupplierCode,S.SupplierName as [Supplier Name],S.Address,S.MobileNumber as [Mobile No],S.EmailId as [Email],S.GSTNumber as [GSTIN],S.StateCode as [State Code] from Supplier S where S.CancelTag=0 order by S.ID";
                else
                    objBL.Query = "select S.ID, (SELECT COUNT(*) FROM Supplier S2 WHERE S2.ID <= S.ID and S2.CancelTag=0 and S.CancelTag=0) AS SrNo,S.SupplierCode,S.SupplierName as [Supplier Name],S.Address,S.MobileNumber as [Mobile No],S.EmailId as [Email],S.GSTNumber as [GSTIN],S.StateCode as [State Code] from Supplier S where S.CancelTag=0 and S.SupplierName like '%" + txtSearch.Text + "%' order by S.ID";

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
                    dataGridView1.Columns[3].Width = 300;
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
                TableId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtSupplierCode.Text = TableId.ToString();
                txtSupplierName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtMobileNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtEmailId.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtGSTNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtStateCode.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

                if (objRL.Checks_Record_Allready_Exist(TableId, "Supplier") == false)
                {
                    //btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else
                {
                    //btnSave.Enabled = false;
                    btnDelete.Enabled = false;
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

        private void txtSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSupplierName.Focus();
        }
        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAddress.Focus();
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMobileNumber.Focus();
        }

        private void txtMobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtEmailId.Focus();
        }

        private void txtEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtGSTNumber.Focus();
        }

        private void txtGSTNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtStateCode.Focus();
        }

        private void txtStateCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }
    }
}
