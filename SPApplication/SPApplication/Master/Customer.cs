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
            objDL.SetPlusButtonDesign(btnAddCity);
            FillCity();
            GetCustomerNo();
        }

        private void FillCity()
        {
            objBL.Query = "Select ID,City from CityMaster where CancelTag=0";
            objBL.FillComboBox(cmbCity, "City", "ID");
        }

       

        private void GetCustomerNo()
        {
            int CustomerNo= objRL.ReturnMaxID("Customer");
            txtCustomerCode.Text = CustomerNo.ToString();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            GetCustomerNo();
            FillGrid();
            txtCustomerName.Focus();
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
            cmbCity.SelectedIndex = -1;
            txtMobileNumber.Text = "";
            txtEmailId.Text = "";
            txtAadharCard.Text = "";
            txtPANCard.Text = "";
            txtDrivingLicence.Text = "";
            txtGSTNumber.Text = "";
            txtStateCode.Text = "";
            txtCCMailIDList.Text = "";
            GetCustomerNo();
            txtCustomerName.Focus();
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
            else if (cmbCity.SelectedIndex  == -1)
            {
                objEP.SetError(cmbCity, "Select City Name");
                cmbCity.Focus();
                return true;
            }
            else if (txtEmailId.Text == "")
            {
                objEP.SetError(txtEmailId, "Enter Email Id");
                txtEmailId.Focus();
                return true;
            }
            else if (txtCCMailIDList.Text == "")
            {
                objEP.SetError(txtCCMailIDList, "Enter CC Mail ID List");
                txtCCMailIDList.Focus();
                return true;
            }
            //else if (txtMobileNumber.Text == "")
            //{
            //    objEP.SetError(txtMobileNumber, "Enter Mobile Number");
            //    txtMobileNumber.Focus();
            //    return true;
            //}
            //else if (txtAadharCard.Text == "")
            //{
            //    objEP.SetError(txtAadharCard, "Enter Aadhar Card No.");
            //    txtMobileNumber.Focus();
            //    return true;
            //}
            
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
                    //if (!string.IsNullOrEmpty(txtEmailId.Text))
                    //{
                    //    if (objRL.ValidateEmailId(txtEmailId.Text) == 0)
                    //    {
                    //        objRL.ShowMessage(18, 4);
                    //        return;
                    //    }
                    //}

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
                                objBL.Query = "update Customer set CustomerName='" + txtCustomerName.Text + "',Address='" + txtAddress.Text + "',CityId=" + cmbCity.SelectedValue + ",MobileNumber='" + txtMobileNumber.Text + "',EmailId='" + txtEmailId.Text + "',AadharCard='" + txtAadharCard.Text + "',PANCard='" + txtPANCard.Text + "',DrivingLicence='" + txtDrivingLicence.Text + "',GSTNumber='" + txtGSTNumber.Text + "',StateCode='" + txtStateCode.Text + "',CCList='" + txtCCMailIDList.Text + "',UserId= " + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + TableId + "";
                                ExecuteType = "Update";
                            }
                        }
                    }
                    else
                    {
                        objBL.Query = "insert into Customer(CustomerName,Address,CityId,MobileNumber,EmailId,AadharCard,PANCard,DrivingLicence,GSTNumber,StateCode,CCList,UserId) values('" + txtCustomerName.Text + "','" + txtAddress.Text + "'," + cmbCity.SelectedValue + ",'" + txtMobileNumber.Text + "','" + txtEmailId.Text + "','" + txtAadharCard.Text + "','" + txtPANCard.Text + "','" + txtDrivingLicence.Text + "','" + txtGSTNumber.Text + "','" + txtStateCode.Text + "','" + txtCCMailIDList.Text + "'," + BusinessLayer.UserId_Static + ")";
                        ExecuteType = "Save";
                    }

                    int Result = objBL.Function_ExecuteNonQuery();
                    if (Result > 0)
                    {
                        int CustomerId = 0;
                        CustomerId = objRL.ReturnMaxID_Fix("Customer","ID");
                        if (ExecuteType == "Save")
                            objRL.ShowMessage(7, 1);
                        else if (ExecuteType == "Update")
                            objRL.ShowMessage(8, 1);
                        else
                            objRL.ShowMessage(9, 1);

                        FillGrid();
                        ClearAll();


                        if (RedundancyLogics.GetRecordFlag)
                        {
                            RedundancyLogics.CustomerId_Search = CustomerId;
                            
                            this.Dispose();
                        }
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
                    objBL.Query = "select S.ID, (SELECT COUNT(*) FROM Customer S2 WHERE S2.ID <= S.ID and S2.CancelTag=0 and S.CancelTag=0) AS SrNo,S.CustomerName as [Customer Name],S.Address,S.CityId,C.City,S.MobileNumber as [Mobile Number],S.EmailId,S.AadharCard,S.PANCard,S.DrivingLicence,S.GSTNumber,S.StateCode,S.CCList from Customer S inner join CityMaster C on C.ID=S.CityId where C.CancelTag=0 and S.CancelTag=0 order by S.ID";
                else
                    objBL.Query = "select S.ID, (SELECT COUNT(*) FROM Customer S2 WHERE S2.ID <= S.ID and S2.CancelTag=0 and S.CancelTag=0) AS SrNo,S.CustomerName as [Customer Name],S.Address,S.CityId,C.City,S.MobileNumber as [Mobile Number],S.EmailId,S.AadharCard,S.PANCard,S.DrivingLicence,S.GSTNumber,S.StateCode,S.CCList from Customer S inner join CityMaster C on C.ID=S.CityId where C.CancelTag=0 and S.CancelTag=0 and S.CustomerName like '%" + txtSearch.Text + "%' order by S.ID";

                //objBL.Query = "select S.ID, (SELECT COUNT(*) FROM Customer S2 WHERE S2.ID <= S.ID and S2.CancelTag=0 and S.CancelTag=0) AS SrNo,S.CustomerCode,S.CustomerName as [Customer Name],S.Address,S.MobileNumber as [Mobile Number],S.EmailId,S.AadharCard,S.PANCard,S.DrivingLisence,S.GSTNumber,S.StateCode from Customer S where S.CancelTag=0 order by S.ID";
                //else
                //    objBL.Query = "select S.ID, (SELECT COUNT(*) FROM Customer S2 WHERE S2.ID <= S.ID and S2.CancelTag=0 and S.CancelTag=0) AS SrNo,S.CustomerCode,S.CustomerName as [Customer Name],S.Address,S.MobileNumber as [Mobile Number],S.EmailId,S.AadharCard,S.PANCard,S.DrivingLisence,S.GSTNumber,S.StateCode from Customer S where S.CancelTag=0 and S.CustomerName like '%" + txtSearch.Text + "%' order by S.ID";

                DataSet ds = new DataSet();
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //0 S.ID, 
                    //1 (SELECT COUNT(*) FROM Customer S2 WHERE S2.ID <= S.ID and S2.CancelTag = 0 and S.CancelTag = 0) AS SrNo, 
                    //2 S.CustomerName as [Customer Name],
                    //3 S.Address,
                    //4 S.CityId,
                    //5 C.City,
                    //6 S.MobileNumber as [Mobile Number],
                    //7 S.EmailId,
                    //8 S.AadharCard,
                    //9 S.PANCard,
                    //10 S.DrivingLisence,
                    //11 S.GSTNumber,
                    //12 S.StateCode
                    //13 S.CCList

                    lblTotalCount.Text = "Total Count-" + ds.Tables[0].Rows.Count;
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[4].Visible = false;
                    dataGridView1.Columns[11].Visible = false;
                    dataGridView1.Columns[12].Visible = false;
                    dataGridView1.Columns[13].Visible = false;

                    dataGridView1.Columns[1].Width = 50;
                    dataGridView1.Columns[2].Width = 200;
                    dataGridView1.Columns[3].Width = 120;
                    dataGridView1.Columns[5].Width = 120;
                    dataGridView1.Columns[6].Width = 120;
                    dataGridView1.Columns[7].Width = 120;
                    dataGridView1.Columns[8].Width = 120;
                    dataGridView1.Columns[9].Width = 120;
                    dataGridView1.Columns[10].Width = 120;
                }
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //0 S.ID, 
                //1 (SELECT COUNT(*) FROM Customer S2 WHERE S2.ID <= S.ID and S2.CancelTag = 0 and S.CancelTag = 0) AS SrNo, 
                //2 S.CustomerName as [Customer Name],
                //3 S.Address,
                //4 S.CityId,
                //5 C.City,
                //6 S.MobileNumber as [Mobile Number],
                //7 S.EmailId,
                //8 S.AadharCard,
                //9 S.PANCard,
                //10 S.DrivingLisence,
                //11 S.GSTNumber,
                //12 S.StateCode
                //13 S.CCList

                ClearAll();
                btnDelete.Enabled = true;
                TableId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtCustomerCode.Text = TableId.ToString();
                txtCustomerName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                cmbCity.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtMobileNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtEmailId.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtAadharCard.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                txtPANCard.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                txtDrivingLicence.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                txtGSTNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                txtStateCode.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                txtCCMailIDList.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();

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

                if (RedundancyLogics.GetRecordFlag)
                {
                    RedundancyLogics.CustomerId_Search = TableId;
                    this.Dispose();
                }
                    
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
                cmbCity.Focus();
        }

        private void txtContactNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtEmailId.Focus();
        }

        private void txtEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAadharCard.Focus();
        }

        private void txtAadharCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPANCard.Focus();
        }

        private void txtPANCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDrivingLicence.Focus();
        }

        private void txtDrivingLisence_KeyDown(object sender, KeyEventArgs e)
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
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchFlag = true;
            else
                SearchFlag = false;

            FillGrid();
        }
        private void txtAadharCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtAadharCard);
        }

        private void txtPANCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.TxtNumericValue(sender, e, txtPANCard);
        }

        private void txtGSTNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.TxtNumericValue(sender, e, txtGSTNumber);
        }

        private void txtStateCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtStateCode);
        }

        private void txtMobileNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //objRL.NumericValue(sender, e, txtMobileNumber);
           
        }

        private void btnAddCity_Click(object sender, EventArgs e)
        {
            City objForm = new City();
            objForm.ShowDialog(this);
            FillCity();
        }

      
    }
}
