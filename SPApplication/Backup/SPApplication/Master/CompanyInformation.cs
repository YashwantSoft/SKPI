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
    public partial class CompanyInformation : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        int TableId = 0;

        public CompanyInformation()
        {
            InitializeComponent();
            objBL.Set_Design_All(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_FIRMINFORMATION);
            
            btnAddGrid.BackColor = objBL.GetBackgroundColor();
            btnAddGrid.ForeColor = objBL.GetForeColor();
            btnClearGrid.BackColor = objBL.GetBackgroundColor();
            btnClearGrid.ForeColor = objBL.GetForeColor();
            btnDeleteGrid.BackColor = objBL.GetBackgroundColor();
            btnDeleteGrid.ForeColor = objBL.GetForeColor();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void CompanyInformation_Load(object sender, EventArgs e)
        {
            try
            {
                objRL.Add_Tool_Tip(btnSave, btnClear, btnDelete, btnExit);
                ClearAll();
                //FillData();
                FillGrid();
                txtCompanyName.Focus();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void FillGrid()
        {
            try
            {
                objBL.Query = "select ID,CompanyName,Address,SiteAddress,ContactNo,EmailId,WebSite,VAT,CST,GST from CompanyInformation where CancelTag=0 order by ID";
                DataSet ds = new DataSet();
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //0 ID,
                    //1 CompanyName,
                    //2 Address,
                    //3 SiteAddress,
                    //4 ContactNo,
                    //5 EmailId,
                    //6 WebSite,
                    //7 VAT,
                    //8 CST,
                    //9 GST

                    lblTotalCount.Text = "Total Count-" + ds.Tables[0].Rows.Count;
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                 
                    dataGridView1.Columns[1].Width = 200;
                    dataGridView1.Columns[2].Width = 200;
                    dataGridView1.Columns[3].Width = 150;
                    dataGridView1.Columns[4].Width = 120;
                    dataGridView1.Columns[5].Width = 160;
                    dataGridView1.Columns[6].Width = 100;
                    dataGridView1.Columns[7].Width = 100;
                    dataGridView1.Columns[8].Width = 100;
                    dataGridView1.Columns[9].Width = 100;

                    //for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    //{
                    //    dataGridView1.Columns[i].Width = 200;
                    //}
                }
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void ClearAll()
        {
            objEP.Clear();

            TableId = 0;
            txtCompanyName.Text = "";
            txtAddress.Text = "";
            txtSiteAddress.Text = "";
            txtContactNo.Text = "";
            txtEmailId.Text = "";
            txtWebSite.Text = "";
            txtGSTNo.Text = "";
            txtVATTIN.Text = "";
            txtCST.Text = "";
            dgvBankDetails.Rows.Clear();
            txtCompanyName.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDB();
        }

        private bool Validation()
        {
            objEP.Clear();

            if (txtCompanyName.Text == "")
            {
                txtCompanyName.Focus();
                objEP.SetError(txtCompanyName, "Enter Company");
                return true;
            }
            else if (txtAddress.Text == "")
            {
                txtContactNo.Focus();
                objEP.SetError(txtAddress, "Enter Address");
                return true;
            }
            else if (txtContactNo.Text == "")
            {
                txtContactNo.Focus();
                objEP.SetError(txtContactNo, "Enter Contact No");
                return true;
            }
            //else if (dgvBankDetails.Rows.Count == 0)
            //{
            //    dgvBankDetails.Focus();
            //    objEP.SetError(dgvBankDetails, "Enter your bank account(s)");
            //    return true;
            //}
            //else if (!Validation_Primary_Account())
            //{
            //    cbPrimary.Focus();
            //    objEP.SetError(cbPrimary, "Set Primary Bank in this list");
            //    return true;
            //}
            else
                return false;
        }

        string PrimaryInsert = "", BankName = "", BankAddress = "", AccountNumber = "", AccountType = "", AccountHolderName = "", IFSCCode = "";

        private void ClearAll_Bank_Variables()
        {
            PrimaryInsert = ""; BankName = ""; BankAddress = ""; AccountNumber = ""; AccountType = ""; AccountHolderName = "";
        }
        private void SaveDB()
        {
            if (!Validation())
            {
                if (TableId != 0)
                    objBL.Query = "update CompanyInformation set CompanyName='" + txtCompanyName.Text + "',Address='" + txtAddress.Text + "',SiteAddress='" + txtSiteAddress.Text + "',ContactNo='" + txtContactNo.Text + "',EmailId='" + txtEmailId.Text + "',WebSite='" + txtWebSite.Text + "',VAT='" + txtVATTIN.Text + "',CST='" + txtCST.Text + "',GST='" + txtGSTNo.Text + "',UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + TableId + "";
                else
                    objBL.Query = "insert into CompanyInformation(CompanyName,Address,SiteAddress,ContactNo,EmailId,WebSite,VAT,CST,GST,UserId) values('" + txtCompanyName.Text + "','" + txtAddress.Text + "','" + txtSiteAddress.Text + "','" + txtContactNo.Text + "','" + txtEmailId.Text + "','" + txtWebSite.Text + "','" + txtVATTIN.Text + "','" + txtCST.Text + "','" + txtGSTNo.Text + "'," + BusinessLayer.UserId_Static + " )";

                objBL.Function_ExecuteNonQuery();

                if (FlagFillData == true)
                {
                    objBL.Query = "delete from BankDetails where CancelTag=0";
                    objBL.Function_ExecuteNonQuery();
                }

                if (dgvBankDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvBankDetails.Rows.Count; i++)
                    {
                        ClearAll_Bank_Variables();
                        PrimaryInsert = !string.IsNullOrEmpty(Convert.ToString(dgvBankDetails.Rows[i].Cells["clmPrimary"].Value)) ? dgvBankDetails.Rows[i].Cells["clmPrimary"].Value.ToString() : "No";
                        BankName = !string.IsNullOrEmpty(Convert.ToString(dgvBankDetails.Rows[i].Cells["clmBankName"].Value)) ? dgvBankDetails.Rows[i].Cells["clmBankName"].Value.ToString() : "-";
                        BankAddress = !string.IsNullOrEmpty(Convert.ToString(dgvBankDetails.Rows[i].Cells["clmBankAddress"].Value)) ? dgvBankDetails.Rows[i].Cells["clmBankAddress"].Value.ToString() : "-";
                        AccountNumber = !string.IsNullOrEmpty(Convert.ToString(dgvBankDetails.Rows[i].Cells["clmAccountNumber"].Value)) ? dgvBankDetails.Rows[i].Cells["clmAccountNumber"].Value.ToString() : "-";
                        AccountType = !string.IsNullOrEmpty(Convert.ToString(dgvBankDetails.Rows[i].Cells["clmAccountType"].Value)) ? dgvBankDetails.Rows[i].Cells["clmAccountType"].Value.ToString() : "-";
                        AccountHolderName = !string.IsNullOrEmpty(Convert.ToString(dgvBankDetails.Rows[i].Cells["clmAccountHolderName"].Value)) ? dgvBankDetails.Rows[i].Cells["clmAccountHolderName"].Value.ToString() : "-";
                        IFSCCode = !string.IsNullOrEmpty(Convert.ToString(dgvBankDetails.Rows[i].Cells["clmIFSCCode"].Value)) ? dgvBankDetails.Rows[i].Cells["clmIFSCCode"].Value.ToString() : "-";
                        objBL.Query = "insert into BankDetails(PrimaryAccount,BankName,BankAddress,AccountNumber,AccountType,AccountHolderName,IFSCCode,UserId) values('" + PrimaryInsert + "','" + BankName + "','" + BankAddress + "','" + AccountNumber + "','" + AccountType + "','" + AccountHolderName + "','" + IFSCCode + "'," + BusinessLayer.UserId_Static + ")";
                        objBL.Function_ExecuteNonQuery();
                    }
                }

                objRL.ShowMessage(7, 1);
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        bool FlagFillData = false;
        private void FillData()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,CompanyName,Address,SiteAddress,ContactNo,EmailId,WebSite,VAT,CST,GST from CompanyInformation where CancelTag=0";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                FlagFillData = true;
                TableId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                txtCompanyName.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                txtSiteAddress.Text = ds.Tables[0].Rows[0]["SiteAddress"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                txtEmailId.Text = ds.Tables[0].Rows[0]["EmailId"].ToString();
                txtWebSite.Text = ds.Tables[0].Rows[0]["WebSite"].ToString();
                txtVATTIN.Text = ds.Tables[0].Rows[0]["VAT"].ToString();
                txtCST.Text = ds.Tables[0].Rows[0]["CST"].ToString();
                txtGSTNo.Text = ds.Tables[0].Rows[0]["GST"].ToString();
                Fill_Bank_List();
            }
        }

        private bool Validation_Bank()
        {
            objEP.Clear();

            if (txtBankName.Text == "")
            {
                txtBankName.Focus();
                objEP.SetError(txtBankName, "Enter Bank Name");
                return true;
            }
            else if (txtAccountNumber.Text == "")
            {
                txtAccountNumber.Focus();
                objEP.SetError(txtAccountNumber, "Enter Account Number");
                return true;
            }
            else if (cmbAccountType.SelectedIndex == -1)
            {
                cmbAccountType.Focus();
                objEP.SetError(cmbAccountType, "Enter Account Type");
                return true;
            }
            else if (txtAccountHolderName.Text == "")
            {
                txtAccountHolderName.Focus();
                objEP.SetError(txtAccountHolderName, "Enter Account Holder Name");
                return true;
            }
            else
                return false;

        }

        static int dgvItemRow;
        string PrimaryValue = "No";
        bool GridFlag = false;
        int TempRowIndex = 0;

        private void btnAddGrid_Click(object sender, EventArgs e)
        {
            if (!Validation_Bank())
            {
                if (GridFlag == false)
                    dgvBankDetails.Rows.Add();

                if (cbPrimary.Checked == true)
                    PrimaryValue = "Yes";
                else
                    PrimaryValue = "No";

                dgvBankDetails.Rows[dgvItemRow].Cells["clmPrimary"].Value = PrimaryValue;
                dgvBankDetails.Rows[dgvItemRow].Cells["clmBankName"].Value = txtBankName.Text;
                dgvBankDetails.Rows[dgvItemRow].Cells["clmBankAddress"].Value = txtBankAddress.Text;
                dgvBankDetails.Rows[dgvItemRow].Cells["clmAccountNumber"].Value = txtAccountNumber.Text;
                dgvBankDetails.Rows[dgvItemRow].Cells["clmAccountType"].Value = cmbAccountType.Text;
                dgvBankDetails.Rows[dgvItemRow].Cells["clmAccountHolderName"].Value = txtAccountHolderName.Text;
                dgvBankDetails.Rows[dgvItemRow].Cells["clmIFSCCode"].Value = txtIFSCCode.Text;

                SrNo_Add();

                if (GridFlag == true)
                    dgvItemRow = TempRowIndex;
                else
                    dgvItemRow++;

                ClearBankDetails();
            }
        }

        private void SrNo_Add()
        {
            if (dgvBankDetails.Rows.Count > 0)
            {
                int SrNo = 1;
                for (int i = 0; i < dgvBankDetails.Rows.Count; i++)
                {
                    dgvBankDetails.Rows[i].Cells["clmSrNo"].Value = SrNo.ToString();
                    SrNo++;
                }
            }
        }

        private void ClearBankDetails()
        {
            GridFlag = false;
            txtBankName.Text = "";
            txtBankAddress.Text = "";
            txtAccountNumber.Text = "";
            cmbAccountType.SelectedIndex = -1;
            txtAccountHolderName.Text = "";
            txtBankName.Focus();
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            ClearBankDetails();
        }

        private void cbPrimary_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPrimary.Checked == true)
            {
                if (objRL.CheckPrimaryAccount())
                {
                    MessageBox.Show("Primary Account Already Set");
                    return;
                }
                else
                    PrimaryValue = "Yes";
            }
            else
                PrimaryValue = "No";
        }

        private void dgvBankDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TempRowIndex = dgvItemRow;
            dgvItemRow = e.RowIndex;
            GridFlag = true;
            btnDeleteGrid.Enabled = true;

            if (!string.IsNullOrEmpty(dgvBankDetails.Rows[dgvItemRow].Cells["clmPrimary"].Value.ToString()))
            {
                if (dgvBankDetails.Rows[dgvItemRow].Cells["clmPrimary"].Value.ToString() == "Yes")
                    cbPrimary.Checked = true;
                else
                    cbPrimary.Checked = false;
            }
            txtBankName.Text = dgvBankDetails.Rows[dgvItemRow].Cells["clmBankName"].Value.ToString();
            txtBankAddress.Text = dgvBankDetails.Rows[dgvItemRow].Cells["clmBankAddress"].Value.ToString();
            txtAccountNumber.Text = dgvBankDetails.Rows[dgvItemRow].Cells["clmAccountNumber"].Value.ToString();
            cmbAccountType.Text = dgvBankDetails.Rows[dgvItemRow].Cells["clmAccountType"].Value.ToString();
            txtAccountHolderName.Text = dgvBankDetails.Rows[dgvItemRow].Cells["clmAccountHolderName"].Value.ToString();
            txtIFSCCode.Text = dgvBankDetails.Rows[dgvItemRow].Cells["clmIFSCCode"].Value.ToString();
        }

        private void btnDeleteGrid_Click(object sender, EventArgs e)
        {
            if (!Validation_Bank())
            {
                DialogResult dr;
                dr = MessageBox.Show("Do you want to delete this bank details.?", "Delete", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    dgvBankDetails.Rows.RemoveAt(dgvItemRow);
                    ClearBankDetails();
                    if (dgvBankDetails.Rows.Count > 0)
                        dgvItemRow = dgvBankDetails.Rows.Count;
                    else
                        dgvItemRow = 0;
                    SrNo_Add();
                }
            }
        }

        private bool Validation_Primary_Account()
        {
            bool RValue = false;
            if (dgvBankDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dgvBankDetails.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dgvBankDetails.Rows[i].Cells["clmPrimary"].Value.ToString()))
                    {
                        if (dgvBankDetails.Rows[i].Cells["clmPrimary"].Value.ToString() == "Yes")
                        {
                            RValue = true;
                            break;
                        }
                        else
                            RValue = false;
                    }
                }
            }
            return RValue;
        }

        private void Fill_Bank_List()
        {
            if (TableId != 0)
            {
                ClearAll_Bank_Variables();
                ClearBankDetails();
                dgvBankDetails.Rows.Clear();
                DataSet ds = new DataSet();
                objBL.Query = "select ID,PrimaryAccount,BankName,BankAddress,AccountNumber,AccountType,AccountHolderName,IFSCCode from BankDetails where CancelTag=0 and CompanyId=" + TableId + "";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgvItemRow = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvBankDetails.Rows.Add();
                        dgvBankDetails.Rows[dgvItemRow].Cells["clmPrimary"].Value = Convert.ToString(ds.Tables[0].Rows[i]["PrimaryAccount"]);
                        dgvBankDetails.Rows[dgvItemRow].Cells["clmBankName"].Value = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["BankName"])) ? ds.Tables[0].Rows[i]["BankName"] : "-";
                        dgvBankDetails.Rows[dgvItemRow].Cells["clmBankAddress"].Value = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["BankAddress"])) ? ds.Tables[0].Rows[i]["BankAddress"] : "-";
                        dgvBankDetails.Rows[dgvItemRow].Cells["clmAccountNumber"].Value = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["AccountNumber"])) ? ds.Tables[0].Rows[i]["AccountNumber"] : "-";
                        dgvBankDetails.Rows[dgvItemRow].Cells["clmAccountType"].Value = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["AccountType"])) ? ds.Tables[0].Rows[i]["AccountType"] : "-";
                        dgvBankDetails.Rows[dgvItemRow].Cells["clmAccountHolderName"].Value = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["AccountHolderName"])) ? ds.Tables[0].Rows[i]["AccountHolderName"] : "-";
                        dgvBankDetails.Rows[dgvItemRow].Cells["clmIFSCCode"].Value = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["IFSCCode"])) ? ds.Tables[0].Rows[i]["IFSCCode"] : "-";
                        dgvItemRow++;
                    }
                    SrNo_Add();
                }
            }
        }

        private void txtCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAddress.Focus();
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSiteAddress.Focus();
        }

        private void txtSiteAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtContactNo.Focus();
        }

        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtEmailId.Focus();
        }

        private void txtEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtWebSite.Focus();
        }

        private void txtWebSite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtVATTIN.Focus();
        }

        private void txtVATTIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCST.Focus();
        }

        private void txtCST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtGSTNo.Focus();
        }

        private void txtGSTNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtBankName.Focus();
        }

        private void txtBankName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAddress.Focus();
        }

        private void txtBankAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAccountNumber.Focus();
        }

        private void txtAccountNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbAccountType.Focus();
        }

        private void cmbAccountType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAccountHolderName.Focus();
        }

        private void txtAccountHolderName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtIFSCCode.Focus();
        }

        private void txtIFSCCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddGrid.Focus();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClearAll();
                btnDelete.Enabled = true;
                TableId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtCompanyName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtSiteAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtContactNo.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtEmailId.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtWebSite.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtVATTIN.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtCST.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                txtGSTNo.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                Fill_Bank_List();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }
    }
}

