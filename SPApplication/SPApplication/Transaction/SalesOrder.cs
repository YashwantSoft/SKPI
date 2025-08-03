using BusinessLayerUtility;
using SPApplication.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Drawing.Imaging;
using System.IO;

namespace SPApplication.Transaction
{
    public partial class SalesOrder : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0, DataGridIndex = 0;
        double Bags = 0, PcsPerBags = 0, Total = 0;

        Excel.Application myExcelApp;
        Excel.Workbooks myExcelWorkbooks;
        Excel.Workbook myExcelWorkbook;

        public SalesOrder()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_CLIENTREQUIREMENTS);
            btnRefresh.BackColor = objDL.GetBackgroundColor();
            btnRefresh.ForeColor = objDL.GetForeColor();
            btnRefresh.Text = BusinessResources.BTN_REFRESH;
            btnSaveInTally.BackColor = objDL.GetBackgroundColor();
            btnSaveInTally.ForeColor = objDL.GetForeColor();
            btnSaveInTally.Text = BusinessResources.BTN_SAVEINTALLY;
            btnAddGrid.BackColor = objDL.GetBackgroundColor();
            btnAddGrid.ForeColor = objDL.GetForeColorError();
            btnClearGrid.BackColor = objDL.GetBackgroundColor();
            btnClearGrid.ForeColor = objDL.GetForeColorError();
            objRL.Fill_Client_ListBox(lbClient, txtSearchClient.Text, "All");
            objDL.SetButtonDesign(btnView, BusinessResources.BTN_VIEW);
        }

        private void CalculateTotal()
        {
            Bags = 0; PcsPerBags = 0; Total = 0;
            double.TryParse(txtBags.Text, out Bags);
            double.TryParse(txtPcsPerBags.Text, out PcsPerBags);
            Total = Bags * PcsPerBags;
            txtTotal.Text = Total.ToString();
        }

        private void txtBags_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void txtPcsPerBags_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void ClientRequirements_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            //lblEmail.Visible = true;
            //cbEmail.Visible = true;
            //btnSave.Visible = true;
            objRL.SetReportMailId(cbEmail);
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            ClearItem();
            cmbType.SelectedIndex = -1;
            cmbType.Focus();
        }

        private void ClearItem()
        {
            DataGridIndex = 0;
            txtSearch.Text = "";
            lbList.DataSource = null;
            lblDetails.Text = "";
            txtBags.Text = "";
            txtPcsPerBags.Text = "";
            txtTotal.Text = "";
        }

        private void ClearAll()
        {
            objEP.Clear();
            TableID = 0;
            FlagDelete = false;
            Id = 0;
            CustomerId = 0;
            lbClient.DataSource = null;
            txtClientDetails.Text = "";
            cmbType.Text = "";
            txtSearch.Text = "";
            lbList.DataSource = null;
            lblDetails.Text = "";
            txtBags.Text = "";
            txtPcsPerBags.Text = "";
            txtTotal.Text = "";
            dgvProduct.Rows.Clear();
            objRL.Fill_Client_ListBox(lbClient, txtSearchClient.Text, "All");
            dgvCap.Rows.Clear();
            dgvWad.Rows.Clear();
            txtID.Text = "";
            txtFreight.Text = "";
            cmbTermsOfDelivery.SelectedIndex = -1;
            GetID();
            SOStatus = "Pending";
            cmbPaymentMode.SelectedIndex = -1;
            btnSave.Visible = true;
            dtpDate.Value = DateTime.Now.Date;
            dtpTime.Value = DateTime.Now;
            lbClient.Focus();
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("ClientRequirements"));
            txtID.Text = IDNo.ToString();
        }

        int SrNo = 1;

        private void FillSRNO(DataGridView dgv)
        {
            SrNo = 1;
            if (dgv.Rows.Count > 0)
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].Cells[0].Value = SrNo.ToString();
                    SrNo++;
                }
            }
        }

        string ColumnParameter=string.Empty,JoinParameter=string.Empty,FetchColumn=string.Empty,WhereCondition=string.Empty;


        private void Fill_DataGridView_Products(DataGridView dgv,string ProductTypeSearch)
        {
            dgv.DataSource=null;
            ColumnParameter=string.Empty; JoinParameter=string.Empty;FetchColumn=string.Empty;

               if (ProductTypeSearch == "Product")
               {
                   ColumnParameter="P.ProductName";
                   FetchColumn="ProductName";
                   JoinParameter=" Product P on P.ID";
                   WhereCondition="P.CancelTag=0";
               }
                else if (ProductTypeSearch == "Cap")
               {
                   ColumnParameter="CM.CapName";
                    FetchColumn="CapName";
                    JoinParameter=" CapMaster CM on CM.ID";
                    WhereCondition="CM.CancelTag=0";
               }
                else
               {
                    ColumnParameter="W.WadName";
                    FetchColumn="WadName";
                    JoinParameter=" WadMaster W on W.ID";
                    WhereCondition="W.CancelTag=0";
                }
            
            DataSet ds = new DataSet();

            objBL.Query = "select CRP.ID,CRP.ClientRequirmentsId,CRP.PType,CRP.ProductId, " + ColumnParameter + ",CRP.Bags,CRP.PcsPerBag,CRP.Total from ClientRequirementsProducts CRP inner join " + JoinParameter + " =CRP.ProductId  where CRP.CancelTag=0  and CRP.ClientRequirmentsId=" + TableID + " and CRP.PType='"+ProductTypeSearch+"' and " + WhereCondition + "";
            ds = objBL.ReturnDataSet();
            DataGridIndex = 0;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgv.Rows.Add();
                    dgv.Rows[DataGridIndex].Cells[1].Value =  ProductTypeSearch.ToString();

                    if(!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ProductId"].ToString())))
                        dgv.Rows[DataGridIndex].Cells[2].Value = ds.Tables[0].Rows[i]["ProductId"].ToString();
                    if(!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i][FetchColumn].ToString())))
                        dgv.Rows[DataGridIndex].Cells[3].Value = ds.Tables[0].Rows[i][FetchColumn].ToString();
                    if(!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Bags"].ToString())))
                        dgv.Rows[DataGridIndex].Cells[4].Value = ds.Tables[0].Rows[i]["Bags"].ToString();
                    if(!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["PcsPerBag"].ToString())))
                        dgv.Rows[DataGridIndex].Cells[5].Value = ds.Tables[0].Rows[i]["PcsPerBag"].ToString();
                       if(!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Total"].ToString())))
                        dgv.Rows[DataGridIndex].Cells[6].Value = ds.Tables[0].Rows[i]["Total"].ToString();
                    dgv.Rows[DataGridIndex].Cells[7].Value = "Delete";
                  
                    DataGridIndex++;
                }
                FillSRNO(dgv);
            }
        }

        private void AddValueDataGridView(DataGridView dgv)
        {
            if (dgv.Rows.Count > 0)
                DataGridIndex = dgv.Rows.Count;
            else
                DataGridIndex = 0;

            dgv.Rows.Add();
            dgv.Rows[DataGridIndex].Cells[1].Value = cmbType.Text.ToString();
            dgv.Rows[DataGridIndex].Cells[2].Value = Id.ToString();
            dgv.Rows[DataGridIndex].Cells[3].Value = lblDetails.Text.ToString();
            dgv.Rows[DataGridIndex].Cells[4].Value = txtBags.Text.ToString();
            dgv.Rows[DataGridIndex].Cells[5].Value = txtPcsPerBags.Text.ToString();
            dgv.Rows[DataGridIndex].Cells[6].Value = txtTotal.Text.ToString();
            dgv.Rows[DataGridIndex].Cells[7].Value = "Delete";
            DataGridIndex++;
            FillSRNO(dgv);
            ClearAllGridItem();
        }

        private void ClearAllGridItem()
        {
            DataGridIndex = 0;
            txtSearch.Text = "";
            lblDetails.Text = "";
            txtBags.Text = "";
            txtPcsPerBags.Text = "";
            txtTotal.Text = "";
        }

        private void btnAddGrid_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS)
            {
                if (!ValidationItem())
                {
                    SrNo = 1; DataGridIndex = 0;
                    if (ListType == "Product")
                        AddValueDataGridView(dgvProduct);
                    else if (ListType == "Cap")
                        AddValueDataGridView(dgvCap);
                    else
                        AddValueDataGridView(dgvWad);
                }
                else
                {
                    objRL.ShowMessage(17, 4);
                    return;
                }
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private bool ValidationItem()
        {
            objEP.Clear();

            if (CustomerId == 0)
            {
                lbClient.Focus();
                objEP.SetError(lbClient, "Select Client Name");
                return true;
            }
            else if (cmbType.SelectedIndex == -1)
            {
                cmbType.Focus();
                objEP.SetError(cmbType, "Select Type");
                return true;
            }
            else if (Id == 0)
            {
                lbList.Focus();
                objEP.SetError(lbList, "Select List");
                return true;
            }
            else if (txtBags.Text == "")
            {
                txtBags.Focus();
                objEP.SetError(txtBags, "Enter Bags");
                return true;
            }
            else if (txtPcsPerBags.Text == "")
            {
                txtPcsPerBags.Focus();
                objEP.SetError(txtPcsPerBags, "Enter Pcs Per Bags");
                return true;
            }
            else if (txtTotal.Text == "")
            {
                txtTotal.Focus();
                objEP.SetError(txtTotal, "Enter Total");
                return true;
            }
            else
                return false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmbType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Set_List();
        }

        string ListType = string.Empty;
        private void Set_List()
        {
            if (cmbType.SelectedIndex > -1)
            {
                ClearItem();
                lbList.DataSource = null;
                txtSearch.Text = "";
                ListType = string.Empty;
                ListType = cmbType.Text;

                if (ListType == "Product")
                    objRL.Fill_Item_ListBox(lbList, txtSearch.Text, "All");
                else if(ListType=="Cap")
                    objRL.Fill_Cap_ListBox(lbList, txtSearch.Text, "All");
                else
                    objRL.Fill_Wad_ListBox(lbList, txtSearch.Text, "All");
            }
        }

        private void txtSearchClient_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchClient.Text != "")
                objRL.Fill_Client_ListBox(lbClient, txtSearchClient.Text, "Text");
            else
                objRL.Fill_Client_ListBox(lbClient, txtSearchClient.Text, "All");
        }

        private void lbClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetClientDetails();
                //txtQty.Focus();
            }
        }

        int CustomerId = 0; string CustomerDetails = string.Empty;
        string Standard = string.Empty;

        private void SetClientDetails()
        {
            txtClientDetails.Text = "";

            if (TableID == 0)
                CustomerId = Convert.ToInt32(lbClient.SelectedValue);

            if (CustomerId != 0)
            {
                txtClientDetails.Text = "";
                CustomerDetails = string.Empty;
                objRL.Get_Customer_Records_By_Id(CustomerId);

                CustomerDetails = "Client Name-" + objRL.CustomerName.ToString() + System.Environment.NewLine +
                                                 "Address-" + objRL.Address.ToString() + System.Environment.NewLine +
                                                     "Mobile-" + objRL.MobileNumber.ToString();

                txtClientDetails.Text = CustomerDetails.ToString();
                cmbType.Focus();
            }
        }
 
        private void SetListSearchText()
        {
            if (cmbType.SelectedIndex > -1)
            {
                if (txtSearch.Text != "")
                {
                    ListType = string.Empty;
                    ListType = cmbType.Text;

                    if (ListType == "Product")
                        objRL.Fill_Item_ListBox(lbList, txtSearch.Text, "Text");
                    else if (ListType == "Cap")
                        objRL.Fill_Cap_ListBox(lbList, txtSearch.Text, "Text");
                    else
                        objRL.Fill_Wad_ListBox(lbList, txtSearch.Text, "Text");
                }
            }
        }

        private void lbList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Fill_Details_Information();
                //txtQty.Focus();
            }
        }
        
        private void lbList_Click(object sender, EventArgs e)
        {
            Fill_Details_Information();
        }

        int Id = 0; string Details = string.Empty;
        
        private void Fill_Details_Information()
        {
            if (cmbType.SelectedIndex > -1)
            {
               // if (TableID == 0)
                    Id = Convert.ToInt32(lbList.SelectedValue);

                if (Id != 0)
                {
                    lblDetails.Text = "";
                    Details = string.Empty;

                    if (ListType == "Product")
                    {
                        objRL.Get_Product_Records_By_Id(Id);
                        Details = objRL.ProductName;
                    }
                    else if (ListType == "Cap")
                    {
                        objRL.Get_Cap_Records_By_Id(Id);
                        Details = objRL.CapName;
                    }
                    else
                    {
                        objRL.Get_Wad_Records_By_Id(Id);
                        Details = objRL.WadName;
                    }

                    if (!string.IsNullOrEmpty(Details))
                    {
                        lblDetails.Text = Details.ToString();
                        txtBags.Focus();
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DeleteValueDataGridView(dgvProduct, e.RowIndex);
        }

        private void DeleteValueDataGridView(DataGridView dgv,int IndexGrid)
        {
            if (dgv.CurrentCell.ColumnIndex == 7)
            {
                DialogResult dr;
                dr = objRL.ReturnDialogResult_Delete();
                if (dr == DialogResult.Yes)
                {
                    dgv.Rows.RemoveAt(IndexGrid);
                    FillSRNO(dgv);
                }
            }
        }

        private void dgvCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DeleteValueDataGridView(dgvCap, e.RowIndex);
        }

        private void dgvWad_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DeleteValueDataGridView(dgvWad, e.RowIndex);
        }

        private bool Validation()
        {
            objEP.Clear();

            if (CustomerId == 0)
            {
                lbClient.Focus();
                objEP.SetError(lbClient, "Select Client Name");
                return true;
            }
            //else if (txtID.Text == "")
            //{
            //    txtID.Focus();
            //    objEP.SetError(txtID, "Enter Bags");
            //    return true;
            //}
            //else if (dgvProduct.Rows.Count == 0 && dgvCap.Rows.Count == 0 && dgvWad.Rows.Count == 0)
            //{
            //    dgvProduct.Focus();
            //    dgvCap.Focus();
            //    dgvWad.Focus();
            //    objEP.SetError(dgvProduct, "Enter Product");
            //    objEP.SetError(dgvCap, "Enter Cap");
            //    objEP.SetError(dgvWad, "Enter Wad");
            //    return true;
            //}
        
            else
                return false;
        }

        string PType = string.Empty, BagsInsert = string.Empty, PcsPerBagInsert = string.Empty, TotalInsert = string.Empty, MailStatus = string.Empty,SOStatus=string.Empty;
        int ProductId = 0;
            
        protected void SaveDB()
        {
            if (!Validation())
            {
                MailStatus = "Not Send";
                SOStatus = "Pending";

                if (TableID != 0)
                    if (FlagDelete)
                        objBL.Query = "update ClientRequirements set CancelTag=1 where ID=" + TableID + "";
                    else
                        objBL.Query = "update ClientRequirements set EntryDate='" + dtpDate.Value.ToShortDateString() + "',EntryTime='" + dtpTime.Value.ToShortTimeString() + "',CustomerId=" + CustomerId + ",Freight='" + txtFreight.Text + "',TermsOfDelivery='" + cmbTermsOfDelivery.Text + "',MailStatus='" + MailStatus + "',SOStatus='" + SOStatus + "',PaymentMode=,'" + cmbPaymentMode.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                else
                    objBL.Query = "insert into ClientRequirements(EntryDate,EntryTime,CustomerId,Freight,TermsOfDelivery,MailStatus,SOStatus,PaymentMode,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "'," + CustomerId + ",'" + txtFreight.Text + "','" + cmbTermsOfDelivery.Text + "','" + MailStatus + "','" + SOStatus + "','" + cmbPaymentMode.Text + "'," + BusinessLayer.UserId_Static + ")";

               int Result= objBL.Function_ExecuteNonQuery();

               if (Result > 0)
               {
                   if (TableID != 0)
                   {
                       objBL.Query = "delete from ClientRequirementsProducts where ClientRequirmentsId=" + TableID + "";
                       objBL.Function_ExecuteNonQuery();
                   }
                   else
                       TableID = objRL.ReturnMaxID_Fix("ClientRequirements","ID");

                   SaveDB_Products(dgvProduct);
                   SaveDB_Products(dgvCap);
                   SaveDB_Products(dgvWad);

                   if (FlagDelete)
                       objRL.ShowMessage(9, 1);
                   else
                   {
                       objRL.ShowMessage(7, 1);

                       //DialogResult dr;
                       //dr = objRL.ReturnDialogResult_Report();
                       //if (dr == DialogResult.Yes)
                       //    SetReport();
                   }
                   ClearAll();
                   FillGrid();
               }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void SaveDB_Products(DataGridView dgv)
        {
            if (dgv.Rows.Count > 0)
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    Id = 0; PType = string.Empty; BagsInsert = string.Empty; PcsPerBagInsert = string.Empty; TotalInsert = string.Empty;

                    //dgv.Rows[DataGridIndex].Cells[1].Value = cmbType.Text.ToString();
                    //dgv.Rows[DataGridIndex].Cells[2].Value = Id.ToString();
                    //dgv.Rows[DataGridIndex].Cells[3].Value = lblDetails.Text.ToString();
                    //dgv.Rows[DataGridIndex].Cells[4].Value = txtBags.Text.ToString();
                    //dgv.Rows[DataGridIndex].Cells[5].Value = txtPcsPerBags.Text.ToString();
                    //dgv.Rows[DataGridIndex].Cells[6].Value = txtTotal.Text.ToString();

                    if (!string.IsNullOrEmpty(Convert.ToString(dgv.Rows[i].Cells[1].Value)))
                        PType = Convert.ToString(dgv.Rows[i].Cells[1].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(dgv.Rows[i].Cells[2].Value)))
                        Id = Convert.ToInt32(dgv.Rows[i].Cells[2].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(dgv.Rows[i].Cells[4].Value)))
                        BagsInsert = Convert.ToString(dgv.Rows[i].Cells[4].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(dgv.Rows[i].Cells[5].Value)))
                        PcsPerBagInsert = Convert.ToString(dgv.Rows[i].Cells[5].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(dgv.Rows[i].Cells[6].Value)))
                        TotalInsert = Convert.ToString(dgv.Rows[i].Cells[6].Value);

                    objBL.Query = "insert into ClientRequirementsProducts(ClientRequirmentsId,PType,ProductId,Bags,PcsPerBag,Total) values(" + TableID + ", '" + PType + "'," + Id + ",'" + BagsInsert + "','" + PcsPerBagInsert + "','" + TotalInsert + "')";
                    objBL.Function_ExecuteNonQuery();
                }
            }
        }

        bool SearchTag = false;
        string WhereClause = string.Empty;
        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (SearchTag)
                WhereClause = " and C.CustomerName like '%" + txtSearchName.Text + "%'";
            else if (IDFlag)
                WhereClause = " and CR.ID=" + txtSearchID.Text + "";
            else
                WhereClause = string.Empty;

            //Format(CR.EntryTime, 'hh:mm AM/PM') as [Time]
            //objBL.Query = "select CR.ID,CR.EntryDate  as [Date],CR.EntryTime  as [Time],CR.CustomerId,C.CustomerName as [Customer Name],CR.Freight,CR.TermsOfDelivery as [Terms Of Delivery],CR.MailStatus  as [Mail Status],CR.SOStatus as [SO Status],CR.PaymentMode as [Payment Mode] from ClientRequirements CR inner join Customer C on C.ID=CR.CustomerId where C.CancelTag=0 and CR.CancelTag=0 " + WhereClause + " order by CR.EntryDate desc";
            objBL.Query = "select CR.ID,CR.EntryDate  as [Date],Format(CR.EntryTime, 'hh:mm AM/PM') as [Time],CR.CustomerId,C.CustomerName as [Customer Name],CR.Freight,CR.TermsOfDelivery as [Terms Of Delivery],CR.MailStatus  as [Mail Status],CR.SOStatus as [SO Status],CR.PaymentMode as [Payment Mode] from ClientRequirements CR inner join Customer C on C.ID=CR.CustomerId where C.CancelTag=0 and CR.CancelTag=0 " + WhereClause + " order by CR.ID desc";

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 CR.ID,
                //1 CR.EntryDate  as [Date],
                //2 CR.EntryTime  as [Time],
                //3 CR.CustomerId,
                //4 C.CustomerName as [Customer Name],
                //5 CR.Freight,
                //6 CR.TermsOfDelivery
                //7 CR.MailStatus  as [Mail Status]
                //8 SO
                //9 CR.PaymentMode as [Payment Mode] 
                dataGridView1.DataSource = ds.Tables[0];
                // dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[3].Visible = false;

                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[4].Width = 350;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[6].Width = 150;
                dataGridView1.Columns[8].Width = 120;
                dataGridView1.Columns[9].Width = 120;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                        if (row.Cells[8].Value.ToString() == "Pending")
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                        else
                            row.DefaultCellStyle.BackColor = Color.Lime;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS)
            {
                FlagDelete = false;
                SaveDB();
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS)
            {
                DialogResult dr;
                dr = objRL.ReturnDialogResult_Delete();
                if (dr == DialogResult.Yes)
                {
                    FlagDelete = true;
                    SaveDB();
                }
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }

        }

        bool BorderFlag = false;

        private void SetReport()
        {
            using (new CursorWait())
            {
                BorderFlag = true;

                object misValue = System.Reflection.Missing.Value;
                myExcelApp = new Excel.Application();
                myExcelWorkbooks = myExcelApp.Workbooks;

                objRL.ClearExcelPath();
                objRL.isPDF = true;
                objRL.Form_ExcelFileName = "SalesOrder.xlsx";
                objRL.Form_ReportFileName = "ID-" + txtID.Text;
                objRL.Form_DestinationReportFilePath = "\\Sales Order Requirements\\";
                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                myExcelWorksheet.get_Range("A2", misValue).Formula = " Sales Order Products, Caps and Wad Requirements";
                myExcelWorksheet.get_Range("A3", misValue).Formula = " Customer Name";
                
                myExcelWorksheet.get_Range("C3", misValue).Formula = objRL.CustomerName;
                myExcelWorksheet.get_Range("J4", misValue).Formula = txtID.Text ;
                //myExcelWorksheet.get_Range("C5", misValue).Formula = objRL.EmailId;
                //myExcelWorksheet.get_Range("C6", misValue).Formula = objRL.MobileNumber;

                myExcelWorksheet.get_Range("J3", misValue).Formula = dtpDate.Value.ToString("dd/MM/yyyy");
                //myExcelWorksheet.get_Range("J4", misValue).Formula = txtID.Text;
                
                RowCount = 5;

                if (dgvProduct.Rows.Count > 0)
                {
                    AFlag = 1;
                    Fill_Merge_Cell("A", "J", misValue, myExcelWorksheet, "Products/Item");
                    RowCount++;
                    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, "Sr.No.");
                    Fill_Merge_Cell("B", "G", misValue, myExcelWorksheet, "Products");
                    Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, "Bags");
                    Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, "Pcs Per Bags");
                    Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, "Total");
                    RowCount++;

                    for (int i = 0; i < dgvProduct.Rows.Count; i++)
                    {
                        AFlag = 1;
                        Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, dgvProduct.Rows[i].Cells[0].Value.ToString());
                        AFlag = 0;
                        Fill_Merge_Cell("B", "G", misValue, myExcelWorksheet, dgvProduct.Rows[i].Cells[3].Value.ToString());
                        AFlag = 2;
                        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, dgvProduct.Rows[i].Cells[4].Value.ToString());
                        Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, dgvProduct.Rows[i].Cells[5].Value.ToString());
                        Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, dgvProduct.Rows[i].Cells[6].Value.ToString());
                        RowCount++;
                    }
                }

                if (dgvCap.Rows.Count > 0)
                {
                    RowCount++;
                    AFlag = 1;
                    Fill_Merge_Cell("A", "J", misValue, myExcelWorksheet, "Cap");
                    RowCount++;
                    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, "Sr.No.");
                    Fill_Merge_Cell("B", "G", misValue, myExcelWorksheet, "Cap");
                    Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, "Bags");
                    Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, "Pcs Per Bags");
                    Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, "Total");
                    RowCount++;

                    for (int i = 0; i < dgvCap.Rows.Count; i++)
                    {
                        AFlag = 1;
                        Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, dgvCap.Rows[i].Cells[0].Value.ToString());
                        AFlag = 0;
                        Fill_Merge_Cell("B", "G", misValue, myExcelWorksheet, dgvCap.Rows[i].Cells[3].Value.ToString());
                        AFlag = 2;
                        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, dgvCap.Rows[i].Cells[4].Value.ToString());
                        Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, dgvCap.Rows[i].Cells[5].Value.ToString());
                        Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, dgvCap.Rows[i].Cells[6].Value.ToString());
                        RowCount++;
                    }
                }

                if (dgvWad.Rows.Count > 0)
                {
                    AFlag = 1;
                    RowCount++;
                    Fill_Merge_Cell("A", "J", misValue, myExcelWorksheet, "Wad");
                    RowCount++;
                    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, "Sr.No.");
                    Fill_Merge_Cell("B", "G", misValue, myExcelWorksheet, "Wad");
                    Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, "Bags");
                    Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, "Pcs Per Bags");
                    Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, "Total");
                    RowCount++;
                    for (int i = 0; i < dgvWad.Rows.Count; i++)
                    {
                        AFlag = 1;
                        Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, dgvWad.Rows[i].Cells[0].Value.ToString());
                        AFlag = 0;
                        Fill_Merge_Cell("B", "G", misValue, myExcelWorksheet, dgvWad.Rows[i].Cells[3].Value.ToString());
                        AFlag = 2;
                        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, dgvWad.Rows[i].Cells[4].Value.ToString());
                        Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, dgvWad.Rows[i].Cells[5].Value.ToString());
                        Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, dgvWad.Rows[i].Cells[6].Value.ToString());
                        RowCount++;
                    }
                }
                RowCount++; 
                AFlag = 0;
                Fill_Merge_Cell("A", "I", misValue, myExcelWorksheet, "Freight");
                AFlag = 2;
                Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, txtFreight.Text);
                RowCount++;

                AFlag = 0;
                Fill_Merge_Cell("A", "J", misValue, myExcelWorksheet, "Terms of Delivery- " + cmbTermsOfDelivery.Text);
                RowCount++;
                Fill_Merge_Cell("A", "J", misValue, myExcelWorksheet, "Payment Mode- " + cmbPaymentMode.Text);

                myExcelWorkbook.Save();

                //var printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters;

                //int printerIndex = 0;

                //foreach (String s in printers)
                //{
                //    if (s.Equals(@"\\192.168.1.3\TSC TE210"))
                //    {
                //        break;
                //    }
                //    printerIndex++;
                //}

                //try
                //{


                //    //myExcelWorkbook.PrintOutEx(1, 1, 1, false, printerIndex, true, false, objRL.RL_DestinationPath, false);

                //   // myExcelWorkbook.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //    //myExcelWorkbook.PrintOut()

                //}
                //catch (Exception ex1)
                //{
                //    MessageBox.Show(ex1.ToString());
                //}


                 string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");

                try
                {
                    const int xlQualityStandard = 0;
                    myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                    myExcelWorkbook.Close(true, misValue, misValue);
                    myExcelApp.Quit();
                   // System.Diagnostics.Process.Start(PDFReport);

                    //objRL.ShowMessage(22, 1);

                    //DialogResult dr;
                    //dr = MessageBox.Show("Do you want to view this report", "Report View", MessageBoxButtons.YesNo);
                    //if (dr == DialogResult.Yes)
                    //    System.Diagnostics.Process.Start(objRL.RL_DestinationPath);

                    System.Diagnostics.Process.Start(objRL.RL_DestinationPath);
                    //objRL.DeleteExcelFile();

                    if (cbEmail.Checked)
                    {
                       
                        objRL.EmailId_RL = objRL.EmailId;
                        objRL.EmailId_RL = cbEmail.Text;
                        objRL.Subject_RL = "Ref No- "+txtID.Text+ "  SKPI Apps-  " + objRL.CustomerName + " Products Requirements Reports for Sales Order ";
                        //string body = "<div><p>Dear " + objRL.CustomerName_Customer + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
                        string body = "<div><p>Dear Sir,<p/><p>Please find attachment of "+objRL.CustomerName +" requirements file.</p><p>Report on " + dtpDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY) +"</p><p>Thanks,</p></div>";

                        objRL.Body_RL = body;// "Dear " + RedundancyLogics.SupplierName + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
                        objRL.FilePath_RL = objRL.RL_DestinationPath;

                        try
                        {
                            objRL.SendEMail();
                            MailStatus = "Mail Sent";
                            objBL.Query = "update ClientRequirements set MailStatus='" + MailStatus + "' where ID=" + TableID + "";
                            objBL.Function_ExecuteNonQuery();
                        }
                        catch (Exception ex1)
                        {
                            MailStatus = "Not Send";
                            objBL.Query = "update ClientRequirements set MailStatus='" + MailStatus + "' where ID=" + TableID + "";
                            objBL.Function_ExecuteNonQuery();
                        }
                    }

                    if (cbEmail.Checked)
                    {
                        objRL.EmailId_RL = "";
                        objRL.Subject_RL = "";
                        objRL.Body_RL = "";
                        objRL.FilePath_RL = PDFReport;
                        //objRL.SendEMail();
                    }
                }
                catch (Exception ex1)
                {
                    objRL.ShowMessage(27, 4);
                    return;
                }
            }
        }

        protected void DrawBorder(Excel.Range Functionrange)
        {
            Excel.Borders borders = Functionrange.Borders;
            //borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders.Weight = 1D;
        }
        int RowCount = 0,AFlag =0;
        bool CellFlag = false, boldflag = false, MH_Value = false, AlignFlag=false;

        protected void Fill_Merge_Cell(string Cell1, string Cell2, object val1, Excel.Worksheet myExcelWorksheet, string SetValue)
        {
            //CellFlag = false;
            if (!CellFlag)
            {
                Cell1 = Cell1 + RowCount;
                Cell2 = Cell2 + RowCount;
            }

            Microsoft.Office.Interop.Excel.Range AlingRange1 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange1.Merge(val1);

            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            if (boldflag)
                AlingRange1.EntireRow.Font.Bold = true;


            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            Microsoft.Office.Interop.Excel.Range AlingRange2 = myExcelWorksheet.get_Range(Cell1, Cell2);

            if (AFlag == 0)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            if (AFlag == 1)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            if (AFlag == 2)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            if (BorderFlag)
                DrawBorder(AlingRange2);

            if (MH_Value)
            {
                AlingRange2.RowHeight = 60;
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                AlingRange2.WrapText = true;
            }

            if (AlignFlag)
                AlingRange1.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
        }

        public class CursorWait : IDisposable
        {
            public CursorWait(bool appStarting = false, bool applicationCursor = false)
            {
                // Wait
                Cursor.Current = appStarting ? Cursors.AppStarting : Cursors.WaitCursor;
                if (applicationCursor) System.Windows.Forms.Application.UseWaitCursor = true;
            }

            public void Dispose()
            {
                // Reset
                Cursor.Current = Cursors.Default;
                System.Windows.Forms.Application.UseWaitCursor = false;
            }
        }

        bool IDFlag = false; bool DateFlag = false;
        private void txtSearchID_TextChanged(object sender, EventArgs e)
        {
            DateFlag = false;
            SearchTag = false;
            if (txtSearchID.Text != "")
                IDFlag = true;
            else
                IDFlag = false;

            FillGrid();
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            DateFlag = false;
            IDFlag = false;
            if (txtSearchName.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //0 CR.ID,
                //1 CR.EntryDate  as [Date],
                //2 CR.EntryTime  as [Time],
                //3 CR.CustomerId,
                //4 C.CustomerName as [Customer Name],
                //5 CR.Freight,
                //6 CR.TermsOfDelivery
                //7 CR.MailStatus  as [Mail Status]
                RowCount_Grid = dataGridView1.Rows.Count;
                CurrentRowIndex = dataGridView1.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    txtID.Text = TableID.ToString();

                    dtpDate.Value =Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    CustomerId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                    SetClientDetails();

                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[5].Value)))
                        txtFreight.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[6].Value)))
                        cmbTermsOfDelivery.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[7].Value)))
                        MailStatus = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[8].Value)))
                        SOStatus = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[9].Value)))
                        cmbPaymentMode.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();

                    Fill_DataGridView_Products(dgvProduct,"Product");
                    Fill_DataGridView_Products(dgvCap,"Cap");
                    Fill_DataGridView_Products(dgvWad,"Wad");

                    if (SOStatus == "Done")
                    {
                        btnSave.Visible = false;
                        btnDelete.Enabled = false;
                    }
                    else
                        btnSave.Visible = true;
                }
            }
            catch (Exception ex1)
            {
                objRL.ErrorMessge(ex1.ToString());
                return;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void txtSearchClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbClient.Items.Count > 0)
            {
                if (e.KeyCode == Keys.Enter)
                    lbClient.Focus();
            }
        }

        private void cmbType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                lbList.Focus();
        }

        private void txtBags_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPcsPerBags.Focus();
        }

        private void txtPcsPerBags_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddGrid.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SetListSearchText();
        }

        private void txtBags_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtBags);
        }

        private void txtPcsPerBags_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtPcsPerBags);
        }

        private void txtFreight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbTermsOfDelivery.Focus();
        }

        private void cmbTermsOfDelivery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        private void txtFreight_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtFreight);
        }

        private void btnSaveInTally_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS1)
            {
                if (!Validation() && TableID != 0)
                {
                    DialogResult dr;
                    dr = objRL.ReturnDialogResult_SalesOrder();
                    if (dr == DialogResult.Yes)
                    {
                        SOStatus = "Done";
                        objBL.Query = "update ClientRequirements set SOStatus='" + SOStatus + "' where ID=" + TableID + "";
                        int Result = objBL.Function_ExecuteNonQuery();
                        if (Result > 0)
                        {
                            objRL.ShowMessage(1, 1);
                            ClearAll();
                            FillGrid();
                        }
                    }
                }
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
        }

        private void lbClient_Click(object sender, EventArgs e)
        {
            SetClientDetails();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            //DialogResult dr;
            //dr = objRL.ReturnDialogResult_Report();
            //if (dr == DialogResult.Yes)
                SetReport();
        }
    }
}
