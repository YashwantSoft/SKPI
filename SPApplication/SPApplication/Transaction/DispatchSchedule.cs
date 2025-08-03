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
using BusinessLayerUtility.TableClass;
using System.Data.OleDb;

namespace SPApplication.Transaction
{
    public partial class DispatchSchedule : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();
        ErrorProvider objEP = new ErrorProvider();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false, IDFlag = false;

        public DispatchSchedule()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_DISPATCHSCHEDULE);
            objRL.Fill_Client_ListBox(lbClient, txtSearchClient.Text, "All");
            objRL.Fill_Product_Status(clbProductStatus);
            objRL.FillEmailMainStatus(cmbStatus);
            //objDL.SetButtonDesign_SmallSize(btnAddGrid, BusinessResources.BTN_ADD);
            //objDL.SetButtonDesign_SmallSize(btnClearGrid, BusinessResources.BTN_CLEAR);
            //objDL.SetButtonDesign_SmallSize(btnDeleteGrid, BusinessResources.BTN_DELETE);
            objDL.SetButtonDesign(btnReport, BusinessResources.BTN_REPORT);

            btnAddGrid.Text = BusinessResources.BTN_ADD;
            btnAddGrid.BackColor = objDL.GetBackgroundColor();
            btnAddGrid.ForeColor = objDL.GetForeColor();

            btnClearGrid.Text = BusinessResources.BTN_CLEAR;
            btnClearGrid.BackColor = objDL.GetBackgroundColor();
            btnClearGrid.ForeColor = objDL.GetForeColor();

            btnDeleteGrid.Text = BusinessResources.BTN_DELETE;
            btnDeleteGrid.BackColor = objDL.GetBackgroundColor();
            btnDeleteGrid.ForeColor = objDL.GetForeColor();
        }

        private void CalculateTotal()
        {

        }

        private List<ProductClass> GetProductAll()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,ProductType,ProductName from Product where CancelTag=0";
            ds = objBL.ReturnDataSet();

            var productList = ds.Tables[0].AsEnumerable()
            .Select(dataRow => new ProductClass
            {
                ProductType = dataRow.Field<string>("ProductType"),
                ProductName = dataRow.Field<string>("ProductName")
            }).ToList();


            return productList;
            //return new List<ProductClass>
            //{
            //    new ProductClass(ProductType, 
            //}
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            //dgvP.DataSource=  GetProductAll();
            ClearAll();
            FillGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();

            //if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS)
            //{
                
            //}
            //else
            //{
            //    objRL.ShowMessage(30, 4);
            //}
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS)
            {
                FlagDelete = true;
                SaveDB();
            }
            else
            {
                objRL.ShowMessage(30, 4);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtPartyName_TextChanged(object sender, EventArgs e)
        {

        }
 
        private void txtSearchClient_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchClient.Text != "")
                objRL.Fill_Client_ListBox(lbClient, txtSearchClient.Text, "Text");
            else
                objRL.Fill_Client_ListBox(lbClient, txtSearchClient.Text, "All");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearItem();
        }

        private void ClearItem()
        {
            txtSearch.Text = "";
            lbList.DataSource = null;
            lblDetails.Text = "";
            txtQty.Text = "";
          
        }

        private void btnAddGrid_Click(object sender, EventArgs e)
        {
            //if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS)
            // {
            if (!ValidationItem())
            {
                SrNo = 1; DataGridIndex = 0;

                if (dgvProduct.Rows.Count > 0)
                    DataGridIndex = dgvProduct.Rows.Count;
                else
                    DataGridIndex = 0;

                if (TableGridId == 0)
                    dgvProduct.Rows.Add();
                else
                    DataGridIndex= SetCurrentRowIndex;

                dgvProduct.Rows[DataGridIndex].Cells["clmProductId"].Value = ProductId.ToString();
                dgvProduct.Rows[DataGridIndex].Cells["clmProductType"].Value = cmbType.Text.ToString();
                dgvProduct.Rows[DataGridIndex].Cells["clmProductName"].Value = lblDetails.Text.ToString();
                
                if(!string.IsNullOrEmpty(Convert.ToString(txtItemCode.Text)))
                    dgvProduct.Rows[DataGridIndex].Cells["clmProductCode"].Value = txtItemCode.Text.ToString();
                
                dgvProduct.Rows[DataGridIndex].Cells["clmQty"].Value = txtQty.Text.ToString();

                if (cbProductionDate.Checked)
                {
                    dgvProduct.Rows[DataGridIndex].Cells["clmProductionDateFlag"].Value = 1;
                    dgvProduct.Rows[DataGridIndex].Cells["clmProductionDate"].Value = dtpProductionDate.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    dgvProduct.Rows[DataGridIndex].Cells["clmProductionDateFlag"].Value = 0;
                    dgvProduct.Rows[DataGridIndex].Cells["clmProductionDate"].Value = "";
                }

                if (cbDispatchDate.Checked)
                {
                    dgvProduct.Rows[DataGridIndex].Cells["clmDispatchDateFlag"].Value = 1;
                    dgvProduct.Rows[DataGridIndex].Cells["clmDispatchDate"].Value = dtpDispatchDate.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    dgvProduct.Rows[DataGridIndex].Cells["clmDispatchDateFlag"].Value = 0;
                    dgvProduct.Rows[DataGridIndex].Cells["clmDispatchDate"].Value = "";
                }

                Get_Status_Names();
                dgvProduct.Rows[DataGridIndex].Cells["clmProductStatusId"].Value = SelectedStatusId.ToString();// cmbProductionStatus.Text.ToString();
                dgvProduct.Rows[DataGridIndex].Cells["clmProductionStatus"].Value = SelectedStatusValue.ToString();// cmbProductionStatus.Text.ToString();
                DataGridIndex++;

                if (dgvProduct.Rows.Count > 0)
                {
                    DataGridIndex = dgvProduct.Rows.Count;
                    lblTotalCountGrid.Text = "Total Count: " + DataGridIndex.ToString();
                }

                objRL.ShowMessage(40, 1);
                FillSRNO(dgvProduct);
                ClearAllGridItem();
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
            //  }
            // else
            //{
            //objRL.ShowMessage(30, 4);
            //  }
        }

        private void txtBags_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void lblDetails_Click(object sender, EventArgs e)
        {
            CalculateTotal();

        }
        
        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        string SelectedStatusId =string.Empty;
        string SelectedStatusValue = string.Empty;
        string SelectedValue = string.Empty;
        int clbID = 0;
        
        private void Get_Status_Names()
        {
            SelectedStatusId = string.Empty;
            SelectedStatusValue = string.Empty;
            SelectedValue = string.Empty;

            foreach (object itemChecked in clbProductStatus.CheckedItems)
            {
                DataRowView castedItem = itemChecked as DataRowView;
                SelectedValue = castedItem[1].ToString();
                int? id = Convert.ToInt32(castedItem[0]);
                clbID = (int)id;
                SelectedStatusId += clbID.ToString() + ",";
                SelectedStatusValue += SelectedValue.ToString() + ", ";
            }

            SelectedStatusId = SelectedStatusId.Remove(SelectedStatusId.Length - 1);
            SelectedStatusValue = SelectedStatusValue.Remove(SelectedStatusValue.Length - 2);


            //List<string> listStrLineElements = CheckBoxListSelectedValue.Split(',').ToList();

            //for (int i = 0; i < clb.Items.Count; i++)
            //{
            //    DataRowView view = clb.Items[i] as DataRowView;
            //    //value = (int)[0];
            //    int? id = Convert.ToInt32(view[0]);
            //    if (listStrLineElements.Contains(id.ToString()))
            //        clb.SetItemChecked(i, true);
            //}


            //string s = "a,b, b, c";
            //string[] values = s.Split(',');
            //for (int i = 0; i < values.Length; i++)
            //{
            //    values[i] = values[i].Trim();
            //}

            //StringBuilder items = new StringBuilder();
            //foreach (object checkedItem in clbProductStatus.CheckedItems)
            //{
            //    DataRowView dr = (DataRowView)checkedItem;
            //    items.Append(dr["tanimId"]).Append(",");
            //}

            //MessageBox.Show(items.ToString().TrimEnd(','));

           // string concat = " IN (" + INConcat + ")";
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
            else if (txtPlanningId.Text == "")
            {
                txtPlanningId.Focus();
                objEP.SetError(txtPlanningId, "Enter Planning Id");
                return true;
            }
            else if (cmbStatus.SelectedIndex == -1)
            {
                cmbStatus.Focus();
                objEP.SetError(cmbStatus, "Selct Status");
                return true;
            }
            else if (cmbAmendment.SelectedIndex == -1)
            {
                cmbAmendment.Focus();
                objEP.SetError(cmbAmendment, "Selct Amendment");
                return true;
            }
            else
                return false;
        }

        string MailStatus = string.Empty;
        int IsMail = 0;

        private void SaveDB()
        {
            if (!Validation())
            {
                if (cbClientEmail.Checked)
                    IsMail = 1;
                else
                    IsMail = 0;

                if (TableID != 0)
                    if (FlagDelete)
                        objBL.Query = "update SalesPlanning set CancelTag=1 where ID=" + TableID + "";
                    else
                        objBL.Query = "update SalesPlanning set EntryDate='" + dtpDate.Value.ToShortDateString() + "',EntryTime='" + dtpDate.Value.ToShortTimeString() + "',CustomerId=" + CustomerId + ",OrderSource='" + cmbOrderSource.Text + "',OrderSourceDetails='" + txtOrderSourceDetails.Text + "',Status='" + cmbStatus.Text + "',Amendment='" + cmbAmendment.Text + "',IsMail=" + IsMail + ",MailStatus='" + MailStatus + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                else
                    objBL.Query = "insert into SalesPlanning(EntryDate,EntryTime,CustomerId,OrderSource,OrderSourceDetails,Status,Amendment,IsMail,MailStatus,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpDate.Value.ToShortTimeString() + "'," + CustomerId + ",'" + cmbOrderSource.Text + "','" + txtOrderSourceDetails.Text + "','" + cmbStatus.Text + "','" + cmbAmendment.Text + "'," + IsMail + ",'" + MailStatus + "'," + BusinessLayer.UserId_Static + ")";

                int Result = objBL.Function_ExecuteNonQuery();

                if (Result > 0)
                {

                    if (TableID != 0)
                    {
                        objBL.Query = "delete from SalesPlanningProducts where SalesPlanningId=" + TableID + "";
                        objBL.Function_ExecuteNonQuery();
                    }
                    else
                        TableID = objRL.ReturnMaxID_Fix("SalesPlanning","ID");

                    SaveDB_Products();
                  
                    if (FlagDelete)
                        objRL.ShowMessage(9, 1);
                    else
                    {
                        objRL.ShowMessage(7, 1);
                        btnReport.Visible = true;
                        //DialogResult dr;
                        //dr = objRL.ReturnDialogResult_Report();
                        //if (dr == DialogResult.Yes)
                        //    SetReport();
                    }
                   // ClearAll();
                    FillGrid();
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        //bool SearchTag = false;
        string WhereClause = string.Empty;

        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (SearchTag)
                WhereClause = " and C.CustomerName like '%" + txtSearchName.Text + "%'";
            else if (IDFlag)
                WhereClause = " and SP.ID=" + txtSearchID.Text + "";
            else
                WhereClause = string.Empty;

            objBL.Query = "select SP.ID,SP.EntryDate as [Date],SP.EntryTime  as [Time],SP.CustomerId,C.CustomerName as [Customer Name],SP.OrderSource as [Order Source],SP.OrderSourceDetails as [Order Source Details],SP.Status,SP.Amendment,SP.IsMail,SP.MailStatus as [EMail Status] from SalesPlanning SP inner join Customer C on C.ID=SP.CustomerId where C.CancelTag=0 and SP.CancelTag=0 " + WhereClause + " order by SP.EntryDate desc";

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 CR.ID,
                //1 CR.EntryDate  as [Date],
                //2 CR.EntryTime  as [Time],
                //3 CR.CustomerId,
                //4 C.CustomerName as [Customer Name],
                //5 SP.OrderSource as [Order Source],
                //6 SP.OrderSourceDetails as [Order Source Details]
                //7 SP.Status,
                //8 SP.Amendment,
                //9 IsMail
                //9 SP.MailStatus

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[9].Visible = false;

                //dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[4].Width = 250;
                dataGridView1.Columns[5].Width = 120;
                dataGridView1.Columns[6].Width = 120;
                dataGridView1.Columns[7].Width = 120;

                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;

                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{
                //    foreach (DataGridViewRow row in dataGridView1.Rows)
                //        if (row.Cells[7].Value.ToString() == "Pending")
                //            row.DefaultCellStyle.BackColor = Color.Yellow;
                //        else
                //            row.DefaultCellStyle.BackColor = Color.Lime;
                //}
            }
        }

        string PType_I = string.Empty, Quantity_I = string.Empty, ProductionStatus_I = string.Empty,ItemCode_I=string.Empty;
        int ProductId_I = 0, ProductionDateFlag_I = 0, DispatchDateFlag_I = 0;

        DateTime ProductionDate_I, DispatchDate_I;

        private void SaveDB_Products()
        {
            if (dgvProduct.Rows.Count > 0)
            {
                for (int i = 0; i < dgvProduct.Rows.Count; i++)
                {
                    ProductId = 0; PType_I = string.Empty; Quantity_I = string.Empty; ProductionStatus_I = string.Empty;
                    ProductionDateFlag_I = 0; DispatchDateFlag_I = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmProductId"].Value)))
                        ProductId_I = Convert.ToInt32(dgvProduct.Rows[i].Cells["clmProductId"].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmProductCode"].Value)))
                        ItemCode_I = Convert.ToString(dgvProduct.Rows[i].Cells["clmProductCode"].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmProductType"].Value)))
                        PType_I = Convert.ToString(dgvProduct.Rows[i].Cells["clmProductType"].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmQty"].Value)))
                        Quantity_I = Convert.ToString(dgvProduct.Rows[i].Cells["clmQty"].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmProductionDateFlag"].Value)))
                        ProductionDateFlag_I = Convert.ToInt32(dgvProduct.Rows[i].Cells["clmProductionDateFlag"].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmProductionDate"].Value)))
                        ProductionDate_I = Convert.ToDateTime(dgvProduct.Rows[i].Cells["clmProductionDate"].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmDispatchDateFlag"].Value)))
                        DispatchDateFlag_I = Convert.ToInt32(dgvProduct.Rows[i].Cells["clmDispatchDateFlag"].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmDispatchDate"].Value)))
                        DispatchDate_I = Convert.ToDateTime(dgvProduct.Rows[i].Cells["clmDispatchDate"].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmProductStatusId"].Value)))
                        ProductionStatus_I = Convert.ToString(dgvProduct.Rows[i].Cells["clmProductStatusId"].Value);

                    objBL.Query = "insert into SalesPlanningProducts(SalesPlanningId,PType,ProductId,ItemCode,Quantity,ProductionDateFlag,ProductionDate,DispatchDateFlag,DispatchDate,ProductionStatus,UserId) values(" + TableID + ", '" + PType_I + "'," + ProductId_I + ",'" + ItemCode_I + "','" + Quantity_I + "'," + ProductionDateFlag_I + ",'" + ProductionDate_I + "'," + DispatchDateFlag_I + ",'" + DispatchDate_I + "','" + ProductionStatus_I + "','" + BusinessLayer.UserId_Static + "')";
                    objBL.Function_ExecuteNonQuery();
                }
            }
        }

        private void lblClient_Click(object sender, EventArgs e)
        {
            SetClientDetails();
        }

        int CustomerId = 0; string CustomerDetails = string.Empty;
        string Standard = string.Empty;

        private void SetClientDetails()
        {
            rtbClientDetails.Text = "";

            if (TableID == 0)
                CustomerId = Convert.ToInt32(lbClient.SelectedValue);

            if (CustomerId != 0)
            {
                lbClient.Visible = false;
                rtbClientDetails.Text = "";
                CustomerDetails = string.Empty;
                objRL.Get_Customer_Records_By_Id(CustomerId);

                CustomerDetails = "Client Name-" + objRL.CustomerName.ToString() + System.Environment.NewLine +
                                                 "Address-" + objRL.Address.ToString() + System.Environment.NewLine +
                                                     "Mobile-" + objRL.MobileNumber.ToString() + System.Environment.NewLine +
                                                     "Email Id-" + objRL.EmailId.ToString();

                rtbClientDetails.Text = CustomerDetails.ToString();

                cbClientEmail.Text = objRL.EmailId.ToString();

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.CCList)))
                    txtCCList.Text = objRL.CCList.ToString();

                cmbType.Focus();
            }
        }

        int ProductId = 0; string Details = string.Empty;
        string ListType = string.Empty;

        private void lbList_Click(object sender, EventArgs e)
        {
            Fill_Details_Information();
        }

        private void Fill_Details_Information()
        {
            if (cmbType.SelectedIndex > -1)
            {
                // if (TableID == 0)
                ProductId = Convert.ToInt32(lbList.SelectedValue);

                if (ProductId != 0)
                {
                    lblDetails.Text = "";
                    Details = string.Empty;

                    if (ListType == "Product")
                    {
                        objRL.Get_Product_Records_By_Id(ProductId);
                        Details = objRL.ProductName;
                    }
                    else if (ListType == "Cap")
                    {
                        objRL.Get_Cap_Records_By_Id(ProductId);
                        Details = objRL.CapName;
                    }
                    else
                    {
                        objRL.Get_Wad_Records_By_Id(ProductId);
                        Details = objRL.WadName;
                    }

                    if (!string.IsNullOrEmpty(Details))
                    {
                        lblDetails.Text = Details.ToString();
                        //txtBags.Focus();
                    }
                }
            }
        }

        private void cmbType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Set_List();
        }

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
                else if (ListType == "Cap")
                    objRL.Fill_Cap_ListBox(lbList, txtSearch.Text, "All");
                else
                    objRL.Fill_Wad_ListBox(lbList, txtSearch.Text, "All");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SetListSearchText();
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
 

        string ProductType = string.Empty;

        private void dgvP_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //var newColIndex= dgvP.Columns[0]

            
        }

        private AutoCompleteStringCollection GetProductNames()
        {
            string[] postSource = GetDataFromTable();
            AutoCompleteStringCollection objASC = new AutoCompleteStringCollection();
            objASC.AddRange(postSource);
            return objASC;
        }

        string QueryNew = string.Empty;
        string SetName = string.Empty;

        private string[] GetDataFromTable()
        {
            objBL.Connect();
            System.Data.DataTable dtPosts = new System.Data.DataTable();
            using (OleDbConnection conn = new OleDbConnection(objBL.conString))
            {
                if (ProductType == "Product")
                {
                    SetName = "ProductName";
                    QueryNew = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard from Product where CancelTag=0 order by ProductName asc";
                }
                else if (ProductType == "Cap")
                {
                    SetName = "CapName";
                    QueryNew = "select ID,CapName from CapMaster where CancelTag=0 order by ID";
                }
                else if (ProductType == "Wad")
                {
                    SetName = "WadName";
                    QueryNew = "select ID,WadName from WadMaster where CancelTag=0 order by WadName asc";
                }
                else
                    QueryNew = "";

                conn.Open();
                using (OleDbDataAdapter adapt = new OleDbDataAdapter(QueryNew, conn))
                {
                    adapt.SelectCommand.CommandTimeout = 120;
                    adapt.Fill(dtPosts);
                }
            }

            //ProductType 
            //use LINQ method syntax to pull the Title field from a DT into a string array...
            string[] postSource = dtPosts
                                .AsEnumerable()
                                .Select<System.Data.DataRow, String>(x => x.Field<String>(SetName))
                                .ToArray();

            return postSource;
            //var source = new AutoCompleteStringCollection();
            //source.AddRange(postSource);
            //textBox1.AutoCompleteCustomSource = source;
            //textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void dgvP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvP.Rows.Count > 0)
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            //        if (dgvP.Rows[e.RowIndex].Cells[0].Value == "Product")
            //        {

            //        }
            //    }
            //}
        }

        int DataGridIndex = 0;

        private void btnAddGrid_Click_1(object sender, EventArgs e)
        {
            
        }

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

        private void ClearAllGridItem()
        {
            TableGridId = 0;
            btnDeleteGrid.Visible = false;
            SetCurrentRowIndex = 0;
            DataGridIndex = 0;
            cmbType.SelectedIndex = -1;
            txtSearch.Text = "";
            lblDetails.Text = "";
            txtQty.Text = "";
            txtItemCode.Text = "";
            txtItemCode.Text = "";
            cbDispatchDate.Checked = false;
            cbProductionDate.Checked = false;
            clbProductStatus.DataSource = null;
            dtpDispatchDate.Value = DateTime.Now.Date;
            dtpProductionDate.Value = DateTime.Now.Date;
            objRL.Fill_Product_Status(clbProductStatus);
            TableGridId = 0;

            clbProductStatus.DataSource = null;
            objRL.Fill_Product_Status(clbProductStatus);

            cmbType.Focus();
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
            else if (ProductId == 0)
            {
                lbList.Focus();
                objEP.SetError(lbList, "Select List");
                return true;
            }

            else if (txtQty.Text == "")
            {
                txtQty.Focus();
                objEP.SetError(txtQty, "Enter Total");
                return true;
            }
            else
                return false;
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            ClearAllGridItem();
        }

        private void lbClient_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                SetClientDetails();
        }

        private void cbProductionDate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbProductionDate.Checked)
                dtpProductionDate.Enabled = true;
            else
                dtpProductionDate.Enabled = false;
        }

        private void cbDispatchDate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDispatchDate.Checked)
                dtpDispatchDate.Enabled = true;
            else
                dtpDispatchDate.Enabled = false;
        }

        int TableGridId = 0,SetCurrentRowIndex=0;
        private void dgvProduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                RowCount_Grid = dgvProduct.Rows.Count;
                CurrentRowIndex = dgvProduct.CurrentRow.Index;

                SetCurrentRowIndex = CurrentRowIndex;
                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    ClearAllGridItem();
                    btnDeleteGrid.Visible = true; 

                    TableGridId = Convert.ToInt32(dgvProduct.Rows[e.RowIndex].Cells[0].Value);
                    txtPlanningId.Text = TableID.ToString();

                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[e.RowIndex].Cells["clmProductType"].Value)))
                        cmbType.Text = dgvProduct.Rows[e.RowIndex].Cells["clmProductType"].Value.ToString();

                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[e.RowIndex].Cells["clmProductName"].Value)))
                        lblDetails.Text = dgvProduct.Rows[e.RowIndex].Cells["clmProductName"].Value.ToString();

                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[e.RowIndex].Cells["clmQty"].Value)))
                        txtQty.Text = dgvProduct.Rows[e.RowIndex].Cells["clmQty"].Value.ToString();

                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[e.RowIndex].Cells["clmProductCode"].Value)))
                     txtItemCode.Text = dgvProduct.Rows[e.RowIndex].Cells["clmProductCode"].Value.ToString();

                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[e.RowIndex].Cells["clmProductionDateFlag"].Value)))
                    {
                        if(dgvProduct.Rows[e.RowIndex].Cells["clmProductionDateFlag"].Value.ToString() =="1")
                            dtpProductionDate.Value = Convert.ToDateTime(dgvProduct.Rows[e.RowIndex].Cells["clmProductionDate"].Value.ToString());
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[e.RowIndex].Cells["clmDispatchDateFlag"].Value)))
                    {
                        if (dgvProduct.Rows[e.RowIndex].Cells["clmDispatchDateFlag"].Value.ToString() == "1")
                            dtpDispatchDate.Value = Convert.ToDateTime(dgvProduct.Rows[e.RowIndex].Cells["clmDispatchDate"].Value.ToString());
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[e.RowIndex].Cells["clmProductStatusId"].Value)))
                    {

                        ProductionStatus_I = Convert.ToString(dgvProduct.Rows[e.RowIndex].Cells["clmProductStatusId"].Value.ToString());

                        List<string> listStrLineElements = ProductionStatus_I.Split(',').ToList();

                        for (int f = 0; f < clbProductStatus.Items.Count; f++)
                        {
                            DataRowView view = clbProductStatus.Items[f] as DataRowView;
                            //value = (int)[0];
                            int? id = Convert.ToInt32(view[0]);
                            if (listStrLineElements.Contains(id.ToString()))
                                clbProductStatus.SetItemChecked(f, true);
                        }
                    }
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

        private void ClearAll()
        {
            ClearAllGridItem();
            txtPlanningId.Text = "";
            txtSearchClient.Text = "";
            cmbStatus.SelectedIndex = -1;
            cmbAmendment.SelectedIndex = -1;
            cbClientEmail.Checked = false;
            dtpDate.Value = DateTime.Now.Date;
            dtpTime.Value = DateTime.Now;
            cbClientEmail.Text = "";
            txtCCList.Text = "";
            cbClientEmail.Checked = false;
            dgvProduct.Rows.Clear();
            GetID();
            txtSearchClient.Focus();
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("SalesPlanning"));
            txtPlanningId.Text = IDNo.ToString();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                 
                RowCount_Grid = dataGridView1.Rows.Count;
                CurrentRowIndex = dataGridView1.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    ClearAll();
                    btnDelete.Enabled = true;

                    //0 CR.ID,
                    //1 CR.EntryDate  as [Date],
                    //2 CR.EntryTime  as [Time],
                    //3 CR.CustomerId,
                    //4 C.CustomerName as [Customer Name],
                    //5 SP.OrderSource as [Order Source],
                    //6 SP.OrderSourceDetails as [Order Source Details]
                    //7 SP.Status,
                    //8 SP.Amendment,
                    //9 IsMail
                    //10 SP.MailStatus

                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    txtPlanningId.Text = TableID.ToString();

                    dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    CustomerId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                    SetClientDetails();

                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[5].Value)))
                        cmbOrderSource.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[6].Value)))
                        txtOrderSourceDetails.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[7].Value)))
                        cmbStatus.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[8].Value)))
                        cmbAmendment.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[9].Value)))
                    {
                        IsMail =Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString());

                        if (IsMail == 1)
                            cbClientEmail.Checked = true;
                        else
                            cbClientEmail.Checked = false;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[10].Value)))
                        MailStatus = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                   
                    Fill_DataGridView_Products(); 
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

        string ProductName = "";

        private void Get_Product_Details()
        {
            DataSet ds=new DataSet();

            ProductName = "";

            if (ProductType == "Product")
                QueryNew = "select ID,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard from Product where CancelTag=0 and ID=" + ProductId + " order by ProductName asc";
            else if (ProductType == "Cap")
                QueryNew = "select ID,CapName from CapMaster where CancelTag=0 and ID=" + ProductId + " order by ID";
            else if (ProductType == "Wad")
                QueryNew = "select ID,WadName from WadMaster where CancelTag=0 and ID=" + ProductId + " order by WadName asc";
            else
                QueryNew = "";
            objBL.Query=QueryNew;

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0][1].ToString())))
                    ProductName = Convert.ToString(ds.Tables[0].Rows[0][1].ToString());
            }
        }

        private void Fill_DataGridView_Products()
        {
           // dgvProduct.DataSource = null;
            dgvProduct.Rows.Clear();

            DataGridIndex = 0;

            DataSet ds = new DataSet();
            objBL.Query = "select SPP.SalesPlanningId,SPP.PType,SPP.ProductId,SPP.ItemCode,SPP.Quantity,SPP.ProductionDateFlag,SPP.ProductionDate,SPP.DispatchDateFlag,SPP.DispatchDate,SPP.ProductionStatus from SalesPlanningProducts SPP inner join SalesPlanning SP on SPP.SalesPlanningId=SP.ID where SPP.CancelTag=0 and SPP.SalesPlanningId=" + TableID + "";
            ds = objBL.ReturnDataSet();
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ProductId"].ToString())))
                        ProductId =Convert.ToInt32(ds.Tables[0].Rows[i]["ProductId"].ToString());
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["PType"].ToString())))
                        ProductType = Convert.ToString(ds.Tables[0].Rows[i]["PType"].ToString());

                    Get_Product_Details();

                    dgvProduct.Rows.Add();
                     
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ProductId"].ToString())))
                        dgvProduct.Rows[DataGridIndex].Cells["clmProductId"].Value = ProductId.ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["PType"].ToString())))
                        dgvProduct.Rows[DataGridIndex].Cells["clmProductType"].Value = ProductType.ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"].ToString())))
                        dgvProduct.Rows[DataGridIndex].Cells["clmProductCode"].Value = ds.Tables[0].Rows[i]["ItemCode"].ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(ProductName.ToString())))
                        dgvProduct.Rows[DataGridIndex].Cells["clmProductName"].Value = ProductName.ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Quantity"].ToString())))
                        dgvProduct.Rows[DataGridIndex].Cells["clmQty"].Value = ds.Tables[0].Rows[i]["Quantity"].ToString();

                    DateTime dt;
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ProductionDateFlag"].ToString())))
                    {
                        ProductionDateFlag_I = Convert.ToInt32(ds.Tables[0].Rows[i]["ProductionDateFlag"].ToString());
                        dgvProduct.Rows[DataGridIndex].Cells["clmProductionDateFlag"].Value = ProductionDateFlag_I.ToString();

                        if (ProductionDateFlag_I == 1)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ProductionDate"].ToString())))
                            {
                                dt = Convert.ToDateTime(ds.Tables[0].Rows[i]["ProductionDate"].ToString());
                                dgvProduct.Rows[DataGridIndex].Cells["clmProductionDate"].Value = dt.ToString(BusinessResources.DATEFORMATDDMMYYYY);
                            }
                        }
                    }
                    else
                    {
                        dgvProduct.Rows[DataGridIndex].Cells["clmProductionDateFlag"].Value = 0;
                        dgvProduct.Rows[DataGridIndex].Cells["clmProductionDate"].Value = "";
                    }
                    
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["DispatchDateFlag"].ToString())))
                    {
                        DispatchDateFlag_I = Convert.ToInt32(ds.Tables[0].Rows[i]["DispatchDateFlag"].ToString());
                        dgvProduct.Rows[DataGridIndex].Cells["clmDispatchDateFlag"].Value = DispatchDateFlag_I.ToString();
                        if (DispatchDateFlag_I == 1)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["DispatchDate"].ToString())))
                            {
                                dt = Convert.ToDateTime(ds.Tables[0].Rows[i]["DispatchDate"].ToString());
                                dgvProduct.Rows[DataGridIndex].Cells["clmDispatchDate"].Value = dt.ToString(BusinessResources.DATEFORMATDDMMYYYY);
                            }
                        }
                    }
                    else
                    {
                        dgvProduct.Rows[DataGridIndex].Cells["clmDispatchDateFlag"].Value = 0;
                        dgvProduct.Rows[DataGridIndex].Cells["DispatchDate"].Value = "";
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ProductionStatus"].ToString())))
                    {
                        ProductionStatus_I = Convert.ToString(ds.Tables[0].Rows[i]["ProductionStatus"].ToString());

                        List<string> listStrLineElements = ProductionStatus_I.Split(',').ToList();

                        for (int f = 0; f < clbProductStatus.Items.Count; f++)
                        {
                            DataRowView view = clbProductStatus.Items[f] as DataRowView;
                            //value = (int)[0];
                            int? id = Convert.ToInt32(view[0]);
                            if (listStrLineElements.Contains(id.ToString()))
                                clbProductStatus.SetItemChecked(f, true);
                        }

                       // dgvProduct.Rows[DataGridIndex].Cells["clmProductionStatus"].Value = ds.Tables[0].Rows[i]["ProductionStatus"].ToString();
                    }
                    Get_Status_Names();

                    dgvProduct.Rows[DataGridIndex].Cells["clmProductStatusId"].Value = SelectedStatusId.ToString();
                    dgvProduct.Rows[DataGridIndex].Cells["clmProductionStatus"].Value = SelectedStatusValue.ToString();

                    DataGridIndex++;
                }
                FillSRNO(dgvProduct);
                btnReport.Visible = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

      
        private void btnDeleteGrid_Click(object sender, EventArgs e)
        {
            if(SetCurrentRowIndex > -1)
                DeleteValueDataGridView();

        }

        private void DeleteValueDataGridView()
        {
            if (!ValidationItem())
            {
                DialogResult dr;
                dr = objRL.ReturnDialogResult_Delete();
                if (dr == DialogResult.Yes)
                {
                    dgvProduct.Rows.RemoveAt(SetCurrentRowIndex);
                    FillSRNO(dgvProduct);
                    Fill_DataGridView_Products();
                    SetCurrentRowIndex = 0;
                    ClearAllGridItem();
                }
            }
        }

        private void txtSearchID_TextChanged(object sender, EventArgs e)
        {
            SearchTag = false;
            if (txtSearchID.Text != "")
                IDFlag = true;
            else
                IDFlag = false;

            FillGrid();
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            //DateFlag = false;
            IDFlag = false;
            if (txtSearchName.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

        private void cbClientEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (cbClientEmail.Checked)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(objRL.EmailId)))
                {
                    cbClientEmail.Text = objRL.EmailId.ToString();
                    txtCCList.Visible = true;
                    if (!string.IsNullOrEmpty(Convert.ToString(objRL.CCList)))
                        txtCCList.Text = objRL.CCList.ToString();
                }
                else
                {
                    cbClientEmail.Text = "";
                    txtCCList.Visible = false;
                    txtCCList.Text = "";
                }
            }
            else
            {
                cbClientEmail.Text = "";
                txtCCList.Visible = false;
                txtCCList.Text = "";
            }
        }

        private bool ValidationReport()
        {
            bool ReturnValue = false;

            if (cmbStatus.SelectedIndex == -1)
            {
                cmbStatus.Focus();
                objEP.SetError(cmbStatus, "Select Status");
                ReturnValue = true;
            }
            //else if (cmbStatus.Text != "Complete")
            //{
            //    cmbStatus.Focus();
            //    objEP.SetError(cmbStatus, "Status is not completed");
            //    ReturnValue = true;
            //}
            else if (dgvProduct.Rows.Count ==0)
            {
                dgvProduct.Focus();
                objEP.SetError(dgvProduct, "Please add Product");
                ReturnValue = true;
            }
            else if (dgvProduct.Rows.Count == 0)
            {
                dgvProduct.Focus();
                objEP.SetError(dgvProduct, "Please add Product");
                ReturnValue = true;
            }
            else 
            {
                ReturnValue = false;
            }

            return ReturnValue;
        }

        private void GetReport()
        {
            if (!ValidationReport())
            {
                ExcelReportMail();
            }
            else
            {
                objRL.ShowMessage(25, 4);
                return;
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            GetReport();
        }

        bool BorderFlag = false; bool CellFlag = false;
        string PDFReport = string.Empty;
        int AFlag = 0; int SrNo = 1;
        bool AlignFlag = false; bool boldflag = false;
        int RowCount = 18;
        bool MH_Value = false;
        string ReportName = string.Empty;

        Excel.Application myExcelApp;
        Excel.Workbooks myExcelWorkbooks;
        Excel.Workbook myExcelWorkbook;

        private void ExcelReportMail()
        {
            objRL.FillCompanyData();

            object misValue = System.Reflection.Missing.Value;
            myExcelApp = new Microsoft.Office.Interop.Excel.Application();
            myExcelWorkbooks = myExcelApp.Workbooks;

            objRL.ClearExcelPath();
            objRL.isPDF = true;
            objRL.Form_ExcelFileName = "DispatchSchedule.xlsx";
            objRL.Form_ReportFileName = "Dispatch Schedule-" + CustomerId +"-" + DateTime.Now.Date.ToString("dd-MMM-yyyy");
            objRL.Form_DestinationReportFilePath = "DispatchSchedule\\" + CustomerId + "\\";

            objRL.Path_Comman();

            myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
            Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

            //myExcelWorksheet.get_Range("J6", misValue).Formula = dtpDated.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);
            //myExcelWorksheet.get_Range("J9", misValue).Formula = BusinessLayer.UserName_Static.ToString();
            //myExcelWorksheet.get_Range("C7", misValue).Formula = txtQuantity.Text;
            //myExcelWorksheet.get_Range("C8", misValue).Formula = txtInvoiceNo.Text;
            //myExcelWorksheet.get_Range("C10", misValue).Formula = txtNoOfPackages.Text;
            //myExcelWorksheet.get_Range("C11", misValue).Formula = txtVehicalNo.Text;
            //myExcelWorksheet.get_Range("C12", misValue).Formula = txtOrderReferanceNo.Text;
            //myExcelWorksheet.get_Range("J7", misValue).Formula = dtpOrderDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);

            string ConcatCompanyDetails = string.Empty;

            ConcatCompanyDetails = "Company Name: " + objRL.CI_CompanyName.ToString(); // +System.Environment.NewLine +
                                   //"Address: " + objRL.CI_Address.ToString() + System.Environment.NewLine +
                                   //"Email Id: " + objRL.CI_EmailId.ToString() + System.Environment.NewLine +
                                   //"Mobile No: " + objRL.CI_ContactNo.ToString() + System.Environment.NewLine +
                                   //"GSTIN: " + objRL.CI_CST.ToString();

            myExcelWorksheet.get_Range("A1", misValue).Formula = ConcatCompanyDetails.ToString();
            myExcelWorksheet.get_Range("A3", misValue).Formula = "Party Name: " + objRL.CustomerName;

            myExcelWorksheet.get_Range("G1", misValue).Formula =  DateTime.Now.Date.ToString(BusinessResources.DATEFORMATDDMMYYYY);
            myExcelWorksheet.get_Range("G2", misValue).Formula = DateTime.Now.ToShortTimeString();
            myExcelWorksheet.get_Range("G3", misValue).Formula = txtPlanningId.Text;

            myExcelWorksheet.get_Range("A5", misValue).Formula = "Order Soure & Details: " + cmbOrderSource.Text + "-" +txtOrderSourceDetails.Text;
            myExcelWorksheet.get_Range("A6", misValue).Formula = "Status: " + cmbStatus.Text;
            myExcelWorksheet.get_Range("A7", misValue).Formula = "Amendment : " + cmbAmendment.Text;
            

            RowCount = 10;

            SrNo = 1; BorderFlag = false;

            for (int i = 0; i < dgvProduct.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmSrNo"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, Convert.ToString(dgvProduct.Rows[i].Cells["clmSrNo"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmProductCode"].Value)))
                {
                    AFlag = 0;
                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, Convert.ToString(dgvProduct.Rows[i].Cells["clmProductCode"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmProductName"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, Convert.ToString(dgvProduct.Rows[i].Cells["clmProductName"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmQty"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, Convert.ToString(dgvProduct.Rows[i].Cells["clmQty"].Value));
                }

                DateTime dt;

                if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmProductionDateFlag"].Value)))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmProductionDate"].Value)))
                    {
                        AFlag = 1;
                        dt = Convert.ToDateTime(dgvProduct.Rows[i].Cells["clmProductionDate"].Value);
                        Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, dt.ToString(BusinessResources.DATEFORMATDDMMYYYY)); // Convert.ToString(dgvProduct.Rows[i].Cells["clmProductionDate"].Value));
                    }
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmDispatchDateFlag"].Value)))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmDispatchDate"].Value)))
                    {
                        AFlag = 1;
                        dt = Convert.ToDateTime(dgvProduct.Rows[i].Cells["clmDispatchDate"].Value);
                        Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dt.ToString(BusinessResources.DATEFORMATDDMMYYYY)); // Convert.ToString(dgvProduct.Rows[i].Cells["clmDispatchDate"].Value));
                    }
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmProductionStatus"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, Convert.ToString(dgvProduct.Rows[i].Cells["clmProductionStatus"].Value));
                }

                RowCount++;
            }

            RowCount++; BorderFlag = true;
            //AFlag = 0;
            //Fill_Merge_Cell("A", "G", misValue, myExcelWorksheet, "Remark - All 17 Points are within Standards");
            //RowCount++;
            //Fill_Merge_Cell("A", "G", misValue, myExcelWorksheet, "Inspected by- " + PlantIncharge + ", Tested by - " + VolumeCheckerName + "");

            myExcelWorkbook.Save();
            string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
            const int xlQualityStandard = 0;
            myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
            myExcelWorkbook.Close(true, misValue, misValue);
            myExcelApp.Quit();
            // objRL.ShowMessage(22, 1);

            //DialogResult dr1;
            //dr1 = MessageBox.Show("Do you want to view this report.?", "Report View", MessageBoxButtons.YesNo);
            //if (dr1 == DialogResult.Yes)

            System.Diagnostics.Process.Start(PDFReport);
            //System.Diagnostics.Process.Start(PDFReport);
           // System.Diagnostics.Process.Start(objRL.RL_DestinationPath);
            //objRL.DeleteExcelFile();

            if (!string.IsNullOrEmpty(objRL.EmailId) && cbClientEmail.Checked)
            {
                objRL.EmailId_RL = objRL.EmailId;
                objRL.Subject_RL = "Dispatch Schedule";
                //string body = "<div><p>Dear " + objRL.CustomerName_Customer + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
                string body = "<div> <p>Please find attachment of pdf file.</p><p>Thanks,</p><p>" + objRL.CI_CompanyName + "</p></div>";

                objRL.Body_RL = body;// "Dear " + RedundancyLogics.SupplierName + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
                objRL.FilePath_RL = PDFReport;
                objRL.SendEMail();

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.MailStatus)))
                {
                    objBL.Query = "update SalesPlanning set MailStatus='" + objRL.MailStatus + "' where CancelTag=0 and ID=" + TableID + "";
                    objBL.ReturnDataSet();
                    FillGrid();
                }
            }
        }

        protected void DrawBorder(Range Functionrange)
        {
            Borders borders = Functionrange.Borders;
            //borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;
            borders.Weight = 1D;
        }

        protected void Fill_Merge_Cell(string Cell1, string Cell2, object val1, Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet, string SetValue)
        {
            Cell1 = Cell1 + RowCount;
            Cell2 = Cell2 + RowCount;
            Microsoft.Office.Interop.Excel.Range AlingRange1 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange1.Merge(val1);
            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            Microsoft.Office.Interop.Excel.Range AlingRange2 = myExcelWorksheet.get_Range(Cell1, Cell2);

            if (AFlag == 0)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            else if (AFlag == 1)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            else
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            if (!BorderFlag)
                DrawBorder(AlingRange2);

            if (MH_Value)
            {
                AlingRange2.RowHeight = 40;
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                AlingRange2.WrapText = true;
            }
        }

        private void CallToReport()
        {
            if (cmbStatus.SelectedIndex > -1)
            {
                if (cmbStatus.Text == "Completed")
                    btnReport.Visible = true;
            }
        }
        private void cmbStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //CallToReport();
        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            Customer objForm = new Customer();
            objForm.ShowDialog(this);
            objRL.Fill_Client_ListBox(lbClient, txtSearchClient.Text, "All");
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtQty);
        }

        private void txtItemCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.TxtNumericValue(sender, e, txtQty);
        }
    }
}
