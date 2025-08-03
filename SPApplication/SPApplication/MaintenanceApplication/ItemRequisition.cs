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
using System.Globalization;

namespace SPApplication.Transaction
{
    public partial class ItemRequisition : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();
        System.Globalization.RegionInfo objRegInfo = new RegionInfo("en-IN");

        static int dgvItemRow;
        static int CurrentRowIndex = 0;

        int TableId = 0, SCId = 0, ItemId = 0, SaveFlag = 0, PurchaseTransactionId = 0, TempRowIndex = 0, ItemId_Insert = 0, BankId = 0, PurchaseNo = 0;

        bool GridFlag = false, CheckDeleteFlag = false, ItemFlag = false, FlagDelete = false;
         
        DateTime dt = new DateTime();
        DateTime dtTransactionDate = DateTime.Now.Date;

        int PSFlag = 0;

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

        public ItemRequisition()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_ItemRequisition);
            objDL.SetButtonDesign(btnInvoice, BusinessResources.BTN_INVOICE);
             
            objDL.SetPlusButtonDesign(btnAddVendor);
            objDL.SetPlusButtonDesign(btnAddItem);
            objDL.SetPlusButtonDesign(btnAddEmployee);
            objDL.SetButtonDesign_SmallSize(btnAddGrid, BusinessResources.BTN_ADD);
            objDL.SetButtonDesign_SmallSize(btnClearGrid, BusinessResources.BTN_CLEAR);

            objRL.Fill_Supplier_ListBox(lbSupplier, txtSearchSupplier.Text, "All");
            objRL.Fill_Item_ListBox(lbItem, txtSearchItem.Text, "All");
            //Fill_Vendor_Customer_List();
          //  objBL.FillComboBox_TableWise(cmbEmployeeName, "Employee", "ID", "EmployeeName");
        }

        //private void Fill_Vendor_Customer_List()
        //{
        //    objBL.TableNameSP = "PurchaseSale";
        //    PSFlag = objBL.PSFlag;
        //    txtSearchSupplier.Text = "";
        //    lbSupplier.DataSource = null;

        //    if (PSFlag == 0)
        //    {
        //        lblHeader.Text = BusinessResources.LBL_HEADER_PURCHASE;
        //        //In case Manufracturer
        //        //objBL.ItemType = "Purchase";
        //        //for Shops
        //        objBL.ItemType = "Both";
        //        lblSearchSC.Text = "Search Vendor";
        //        objRL.Fill_Vendor_ListBox(lbSupplier, txtSearchSupplier.Text, "All");
        //        btnInvoice.Visible = false;
        //    }
        //    else
        //    {
        //        lblHeader.Text = BusinessResources.LBL_HEADER_SALE;
        //        //In case Manufracturer
        //        //objBL.ItemType = "Sale";
        //        //for Shops
        //        objBL.ItemType = "Both";
        //        lblSearchSC.Text = "Search Customer";
        //        objRL.Fill_Customer_ListBox(lbSupplier, txtSearchSupplier.Text, "All");
        //        btnInvoice.Visible = false;
        //    }
        //}

        private void Purchase_Load(object sender, EventArgs e)
        {
            dtpInvoiceDate.CustomFormat = BusinessResources.DATEFORMATDDMMYYYY;
            
            ClearAll();
            FillGrid();
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
 
            txtSearchSupplier.Focus();
        }

        private void txtSearchSC_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchSupplier.Text != "")
                objRL.Fill_Supplier_ListBox(lbSupplier, txtSearchSupplier.Text, "Text");

            else
                objRL.Fill_Supplier_ListBox(lbSupplier, txtSearchSupplier.Text, "All");
              
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void lbSC_Click(object sender, EventArgs e)
        {
            if (lbSupplier.Items.Count > 0)
                SetSupplierDetails();
        }

        private void lbSC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SetSupplierDetails();
        }

        private void GetVendorCustomer()
        {
            if (lbSupplier.SelectedIndex > -1)
            {
                rtbSC.Text = "";
                SupplierId = Convert.ToInt32(lbSupplier.SelectedValue.ToString());
                Fill_Vendor_Customer_Information();
            }
        }

        private void ClearAll()
        {
            lblAvlQuantity.Text = "";
            GridFlag = false;
            txtChallanNo.Text = "";
           
            lblTotalCount.Text = "";
            lblTotalItemCount.Text = "";
            dgvItem.Rows.Clear();
            gbItem.Visible = false;
             
            objEP.Clear();  
            txtSearchSupplier.Text = "";
           
            txtSearchItem.Text = "";
          
            dtpInvoiceDate.Value = DateTime.Now;
            txtChallanNo.Text = "";
            txtInvoiceNo.Text = "";
            txtPurchaseNo.Text = "";
             
            dgvItemRow = 0;
            TableId = 0;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            ClearAll_Item();

            if (dataGridView1.Rows.Count > 0)
                lblTotalCount.Text = "Total Count: " + dataGridView1.Rows.Count;

            //txtPurchaseNo.Text = Convert.ToString(Convert.ToInt32(objRL.GetPurchaseSaleNo()));
            rtbSC.Text = "";
            //Fill_Vendor_Customer_List();
            objRL.Fill_Supplier_ListBox(lbSupplier, txtSearchSupplier.Text, "All");
            dtpInvoiceDate.Focus();
        }

        private void txtSearchSC_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearchSupplier.Text != "" && lbSupplier.Items.Count > 0)
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                {
                    lbSupplier.SelectedIndex = 0;
                    lbSupplier.Focus();
                }
            }
        }

        private void Fill_Item_ListBox()
        {
            lbItem.DataSource = null;
            DataSet ds = new DataSet();
            //objBL.Query = "select I.ID,I.ItemName,I.Description,I.UOMID,U.UnitOfMessurement,I.ItemPrice from Item I inner join UOM U on U.ID=I.UOMID where I.CancelTag=0 and U.CancelTag=0 and I.ItemName like '%" + txtSearchItemName.Text + "%' order by ItemName desc";
            //objBL.Query = "select ID,Category,CompanyName,ItemName,BatchNumber,HSNCode,Contain,UOM,Price,Cost,MRP,ProfitMarginPer,ProfitMarginAmount,CGST,SGST,IGST from Item where CancelTag=0 and ItemName like '%" + txtSearchItemName.Text + "%' or BatchNumber like '%" + txtSearchItemName.Text + "%'";
            objBL.Query = "select I.ID,I.ManufracturerId,M.ManufracturerName as [Manufracturer Name],I.ItemName as [Item Name],I.BatchNumber,I.HSNCode,I.UOM,I.Price,I.Cost,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from Item I inner join Manufracturer M on I.ManufracturerId=M.ID where I.CancelTag=0 and I.ItemName like '%" + txtSearchItem.Text + "%'";
            //objBL.Query = "select I.ID,I.ItemName,I.ItemCode,I.HSNCode,I.Description,I.UOMID,U.UnitOfMessurement from Item I inner join UOM U on U.ID=I.UOMID where I.CancelTag=0 and U.CancelTag=0 and I.ItemName like '%" + txtSearchItemName.Text + "%' order by ItemName desc";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbItem.Visible = true;
                lbItem.DataSource = ds.Tables[0];
                lbItem.DisplayMember = "Item Name";
                lbItem.ValueMember = "ID";
            }
            else
                lbItem.DataSource = null;
        }

        private void txtSearchItem_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Item();
            // txtSearchItemId.Text = "";
            if (txtSearchItem.Text != "")
                objRL.Fill_Item_ListBox(lbItem, txtSearchItem.Text, "Text");
            //else
            //    lbItem.Visible = false;
        }

        private void ClearAll_Item()
        {
            lblAvlQuantity.Text = "";
            GridFlag = false;
            PurchaseTransactionId = 0;
            TempRowIndex = 0;
            dgvItemRow = 0;
            rtbItem.Text = "";

            rtbItem.Visible = false;

            if (dgvItem.Rows.Count > 0)
                dgvItemRow = dgvItem.Rows.Count;
        }

        private void SetItemValues()
        {
            objRL.ItemId = ItemId;
            //objRL.GetAvailableQuantity();
            //lblAvlQuantity.Text = objRL.AvailableQuantity.ToString();
            txtQuantity.Focus();
        }

        private void lbItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                FillItem();
        }

        private void lbItem_Click(object sender, EventArgs e)
        {
            FillItem();
        }

        private void Fill_ItemDetails_by_ID()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select I.ID,I.ItemName,I.Description,I.UOMID,U.UnitOfMessurement,I.ItemPrice from Item I inner join UOM U on U.ID=I.UOMID where I.CancelTag=0 and U.CancelTag=0 and I.ID=" + ItemId + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lbItem.Visible = false;
                // txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                //txtUOM.Text = ds.Tables[0].Rows[0]["UnitOfMessurement"].ToString();
                ItemId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                txtQuantity.Focus();
            }
        }

        private void FillItem()
        {
            if (lbItem.Items.Count > 0)
            {
                if (!GridFlag)
                    ItemId = Convert.ToInt32(lbItem.SelectedValue);
                else
                    ItemId = objRL.ItemId;

                objRL.FillItemDetailsRichTextBox(rtbItem, ItemId);

                if (!string.IsNullOrEmpty(objRL.ItemDetails_RTB))
                {
                    SetItemValues();
                    rtbItem.Visible = true;
                    lbItem.Visible = false;
                }
                else
                    lbItem.Visible = true;
            }
        }

        private void Fill_Vendor_Customer_Information()
        {
            //if (SCId > 0)
            //{
            //    if (PSFlag == 0)
            //        objRL.FillVendorDetailsRichTextBox(rtbSC, SCId);
            //    else
            //        objRL.FillCustomerDetailsRichTextBox(rtbSC, SCId);

            //    lbSupplier.Visible = false;
            //    rtbSC.Visible = true;
            //    gbItem.Visible = true;
            //    objRL.Fill_Item_ListBox(lbItem, txtSearchItem.Text, "All");
                
            //    lbItem.Focus();
            //}
            //else
            //{
            //    rtbSC.Text = "";
            //    rtbSC.Visible = true;
            //}
        }

        int SupplierId = 0; string SupplierDetails = string.Empty;
        string Standard = string.Empty;

        private void SetSupplierDetails()
        {
            rtbSC.Text = "";

            if (TableId == 0)
                SupplierId = Convert.ToInt32(lbSupplier.SelectedValue);

            if (SupplierId != 0)
            {
                rtbSC.Text = "";
                SupplierDetails = string.Empty;
                objRL.Get_Supplier_Records_By_Id(SupplierId);

                SupplierDetails = "Supplier Name-" + objRL.SupplierName.ToString() + System.Environment.NewLine +
                                                 "Address-" + objRL.Address.ToString() + System.Environment.NewLine +
                                                     "Mobile-" + objRL.MobileNumber.ToString();

                rtbSC.Text = SupplierDetails.ToString();
                 
            }
        }

        private void txtSearchItemId_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Item();
            txtSearchItem.Text = "";
            //if (txtSearchItemId.Text != "")
            //{
            //    ItemID = Convert.ToInt32(txtSearchItemId.Text);
            //    Fill_ItemDetails_by_ID();
            //}
            //else
            //{
            //    lbItem.Visible = false;
            //}

        }

        private void txtSearchItemId_KeyPress(object sender, KeyPressEventArgs e)
        {
            // objRL.NumericValue(sender, e, txtSearchItemId);
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtQuantity);
        }

        

        string PSColumn = string.Empty, PSInnerJoinClause = string.Empty, PSInvoice = string.Empty, PSSC = string.Empty, PSSCHead = string.Empty;
        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;
        bool IDFlag = false;
        bool SearchByName = false;

        private void FillGrid()
        {
            //dataGridView1.DataSource = null;
            //PSColumn = string.Empty;
            //PSInnerJoinClause = string.Empty;
            //PSInvoice = string.Empty;
            //PSSC = string.Empty;
            //PSSCHead = string.Empty;
            //MainQuery = string.Empty;
            //WhereClause = string.Empty;
            //OrderByClause = string.Empty;
            //UserClause = string.Empty;

            //DataSet ds = new DataSet();

            //if (PSFlag == 0)
            //{
            //    PSSCHead = "S.";
            //    PSColumn = "S.VendorName as [Vendor Name],S.MobileNumber as [Mobile No]";
            //    PSInnerJoinClause = " Vendor S on S.ID=P.SCId ";
            //    PSInvoice = "P.PurchaseNo as [Invoice No]";
            //    PSSC = "S.VendorName";
            //}
            //else
            //{
            //    PSSCHead = "C.";
            //    PSColumn = "C.CustomerName as [Customer Name],C.MobileNumber as [Mobile No]";
            //    PSInnerJoinClause = " Customer C on C.ID=P.SCId ";
            //    PSInvoice = "P.SaleNo as [Invoice No]";
            //    PSSC = "C.CustomerName";
            //}

           
            //if (string.IsNullOrEmpty(WhereClause))
            //    WhereClause = string.Empty;

            //MainQuery = "select P.ID,P.PSFlag,P.EntryDate as [Date],P.InvoiceDate as [Invoice Date],P.InvoiceNo,P.ChallanNo,P.SCId," + PSColumn + ",P.ItemCount,P.Naration,P.EmployeeName from PurchaseSale P inner join " + PSInnerJoinClause + " where P.CancelTag=0 and PSFlag=" + PSFlag + " and " + PSSCHead + "CancelTag=0 ";

            ////Old
            ////MainQuery = "select P.ID,P.PSFlag," + PSInvoice + ",P.EntryDate as [Date],P.ChallanNo,P.BillNo as [Bill No],P.SCId," + PSColumn + ",P.ItemCount,P.IsGST,P.TotalGST as [Total GST],P.TotalTaxable as [Total Taxable],P.Freight,P.Loading,P.Insurance,P.OtherCharges as [Other Charges],P.Total,P.DiscountAmount as [Disc Amount],P.InvoiceTotal as [Invoice Total],P.PaymentMode as [Payment Mode],P.BankId,P.BankName as [Firm Name],P.AccountNumber,P.TransactionDate,P.ChequeNumber,P.PartyMobileNo as [Party Mobile],P.Naration,P.PrintFlag,P.PrintCount from PurchaseSale P inner join " + PSInnerJoinClause + " where P.CancelTag=0 and PSFlag=" + PSFlag + " and " + PSSCHead + "CancelTag=0 ";

            //OrderByClause = " order by P.EntryDate desc";

            //objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
            //ds = objBL.ReturnDataSet();

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    //0  P.ID,
            //    //1 P.PSFlag,
            //    //2 P.EntryDate as [Date],
            //    //3 P.InvoiceDate,
            //    //4 P.InvoiceNo,
            //    //5 P.ChallanNo,
            //    //6 P.SCId
            //    //7  PSColumn = "C.CustomerName as [Customer Name],
            //    //8 C.MobileNumber as [Mobile No]";
            //    //9 P.ItemCount,
            //    //10 P.Naration

            //    lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            //    dataGridView1.DataSource = ds.Tables[0];
            //    dataGridView1.Columns[0].Visible = false;
            //    dataGridView1.Columns[1].Visible = false;
            //    dataGridView1.Columns[5].Visible = false;
            //    dataGridView1.Columns[6].Visible = false;


            //    dataGridView1.Columns[2].Width = 100;
            //    dataGridView1.Columns[3].Width = 100;
            //    dataGridView1.Columns[4].Width = 100;
            //    dataGridView1.Columns[5].Width = 100;
            //    dataGridView1.Columns[7].Width = 200;
            //    dataGridView1.Columns[8].Width = 120;
            //    dataGridView1.Columns[9].Width = 100;
            //    dataGridView1.Columns[10].Width = 100;
            //}
        }

        protected void SaveDB()
        {
            try
            {
                if (!Validation())
                {
                    if (TableId != 0)
                    {
                        if (!FlagDelete)
                            objBL.Query = "update PurchaseSale set PSFlag=" + PSFlag + ",EntryDate='" + dtpEntryDate.Value.ToShortDateString() + "',InvoiceDate='" + dtpInvoiceDate.Value.ToShortDateString() + "',InvoiceNo='" + txtInvoiceNo.Text + "',ChallanNo='" + txtChallanNo.Text + "',SCId=" + SCId + ",ItemCount=" + dgvItem.Rows.Count + ",Naration='" + txtNaration.Text + "',EmployeeName='" + cmbEmployeeName.Text + "',ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableId + " and CancelTag=0 ";
                        else
                            objBL.Query = "update PurchaseSale set CancelTag=1 where ID=" + TableId + " and CancelTag=0 ";
                    }
                    else
                    {
                        objBL.Query = "insert into PurchaseSale(PSFlag,EntryDate,InvoiceDate,InvoiceNo,ChallanNo,SCId,ItemCount,Naration,EmployeeName,UserId) values(" + PSFlag + ",'" + dtpEntryDate.Value.ToShortDateString() + "','" + dtpInvoiceDate.Value.ToShortDateString() + "','" + txtInvoiceNo.Text + "','" + txtChallanNo.Text + "'," + SCId + "," + dgvItem.Rows.Count + ",'" + txtNaration.Text + "','" + cmbEmployeeName.Text + "'," + BusinessLayer.UserId_Static + ") ";
                    }

                    if (objBL.Function_ExecuteNonQuery() > 0)
                    {
                        if (TableId == 0)
                            TableId = objRL.Return_Transaction_ID("PurchaseSale");
                        else
                        {
                            if (!FlagDelete)
                            {
                                objBL.Query = "delete from PurchaseSaleItems where PSId=" + TableId + " and CancelTag=0 ";
                                Result= objBL.Function_ExecuteNonQuery();
                            }
                            else
                            {
                                objBL.Query = "update PurchaseSaleItems set CancelTag=1 where PSId=" + TableId + " and CancelTag=0 ";
                                Result = objBL.Function_ExecuteNonQuery();
                            }
                        }

                        if (TableId != 0)
                        {
                            if (!FlagDelete)
                            {
                                SaveItemList();
                                objRL.ShowMessage(7, 1);
                                //if (PSFlag == 1)
                                //    btnInvoice.Visible = true;
                            }
                            else
                                objRL.ShowMessage(9, 1);

                            FillGrid();
                            ClearAll();
                        }
                    }
                }
                else
                {
                    objRL.ShowMessage(17, 4);
                    return;
                }
            }
            catch (Exception ex1) { MessageBox.Show(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        int PSId = 0,PurchaseSaleItemsId=0;
        double Quantity_Insert = 0;

        private void ClearItemList()
        {
            PurchaseSaleItemsId = 0;
            ItemId_Insert = 0;
            Quantity_Insert = 0;
        }

        protected void SaveItemList()
        {
            Result = 0;
            for (int i = 0; i < dgvItem.Rows.Count; i++)
            {
                ClearItemList();
                //PSId = 0;
                PurchaseSaleItemsId = 0;

                PurchaseSaleItemsId = Convert.ToInt32(dgvItem.Rows[i].Cells["clmPurchaseSaleItemsId"].Value.ToString());
                ItemId_Insert = Convert.ToInt32(dgvItem.Rows[i].Cells["clmItemId"].Value.ToString());
                Quantity_Insert = objRL.Check_NullString(objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmQty"].Value)));

                objRL.ItemId = ItemId_Insert;

                //Cost_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmCost"].Value));
                //InsertExpiry = Convert.ToDateTime(dgvItem1.Rows[i].Cells["clmExpiryDate"].Value.ToString());

                //Amount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmAmount"].Value));
                //DiscountPer_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmDiscountPercentage"].Value));
                //DiscountAmount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmDiscountAmount"].Value));

                //TaxableAmount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmTaxableAmount"].Value));

                //CGSTPercentage_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmCGSTPer"].Value));
                //SGSTPercentage_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmSGSTPer"].Value));
                //IGSTPercentage_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmIGSTPer"].Value));

                //CGSTAmount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmCGSTAmount"].Value));
                //SGSTAmount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmSGSTAmount"].Value));
                //IGSTAmount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmIGSTAmount"].Value));

                //TotalTax_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmTotalGST"].Value));
                //NetAmount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmNetAmount"].Value));

                //ItemPurchaseQuantityId_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem.Rows[i].Cells["clmItemPurchaseQuantityId"].Value));
                //QtyInsert = Convert.ToDouble(Quantity_Insert);

                if (!FlagDelete)
                {
                    objBL.Query = "insert into PurchaseSaleItems(PSFlag,PSId,EntryDate,ItemId,Quantity,UserId) values(" + PSFlag + "," + TableId + ",'" + dtpEntryDate.Value.ToShortDateString() + "'," + ItemId_Insert + ",'" + Quantity_Insert + "'," + BusinessLayer.UserId_Static + ")";
                    Result = objBL.Function_ExecuteNonQuery();
                }
                else
                {
                    objBL.Query = "Update PurchaseSaleItems set CancelTag=1,UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + PurchaseSaleItemsId + "";
                    Result = objBL.Function_ExecuteNonQuery();
                }

                //DataSet dsItemPurchaseQuantity = new DataSet();
                ////objBL.Query = "select ID,ItemId,PurchaseId,PurchaseTransactionId,ExpiryDate,PurchaseQuantity,UserId from ItemPurchaseQuantity where CancelTag=0 and ItemId=" + ItemId_Insert + "";
                //objBL.Query = "select ItemId,Quantity from ItemQuantity where CancelTag=0 and ItemId=" + ItemId_Insert + "";
                //dsItemPurchaseQuantity = objBL.ReturnDataSet();

                //double PurchaseQuantity = 0, PreviousQty = 0, CurrentQty = 0;

                //if (dsItemPurchaseQuantity.Tables[0].Rows.Count > 0)
                //{
                //    if (!string.IsNullOrEmpty(dsItemPurchaseQuantity.Tables[0].Rows[0][0].ToString()))
                //    {
                //        PurchaseQuantity = 0; CurrentQty = 0; PreviousQty = 0;
                //        CurrentQty = Convert.ToDouble(Quantity_Insert);
                //        PurchaseQuantity = Convert.ToDouble(dsItemPurchaseQuantity.Tables[0].Rows[0]["Quantity"].ToString());

                //        if (!string.IsNullOrEmpty(Convert.ToString(dgvItem.Rows[i].Cells["clmPreviousQuantity"].Value)) && !string.IsNullOrEmpty(Convert.ToString(dgvItem.Rows[i].Cells["clmQty"].Value)))
                //        {
                //            if (dgvItem.Rows[i].Cells["clmPreviousQuantity"].Value.ToString() != dgvItem.Rows[i].Cells["clmQty"].Value.ToString())
                //            {
                //                PreviousQty = Convert.ToDouble(dgvItem.Rows[i].Cells["clmPreviousQuantity"].Value.ToString());
                //                PurchaseQuantity = (PurchaseQuantity + CurrentQty) - PreviousQty;
                //            }
                //            else
                //                PurchaseQuantity = PurchaseQuantity + CurrentQty;
                //        }
                //        objBL.Query = "Update ItemQuantity set Quantity='" + PurchaseQuantity + "',UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0  and ItemId=" + ItemId_Insert + "";
                //        objBL.Function_ExecuteNonQuery();
                //    }
                //}
                //else
                //{
                //    if (ItemPurchaseQuantityId_Insert == "0" || ItemPurchaseQuantityId_Insert == "")
                //    {
                //        objBL.Query = "insert into ItemQuantity(ItemId,Quantity,UserId) values(" + ItemId_Insert + ",'" + Quantity_Insert + "'," + BusinessLayer.UserId_Static + ")";
                //        objBL.Function_ExecuteNonQuery();
                //    }
                //}

                //if(Result >0)
                //    objRL.GetAvailableQuantity();
            }
        }

        protected bool Check_Exixt_ItemIn_Stock()
        {
            objBL.Query = "Select ID,ItemId,PQty,SQty,AvailableQty,UserId from ItemStock where ItemId=" + ItemId_Insert + " ";
            DataSet ds = new DataSet();
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //PQTY = Convert.ToDouble(ds.Tables[0].Rows[0][2].ToString());
                //AQTY = Convert.ToDouble(ds.Tables[0].Rows[0][4].ToString());
                ItemFlag = true;
            }
            else
                ItemFlag = false;

            return ItemFlag;
        }

         

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = objRL.Delete_Record_Show_Message();
            if (dr == DialogResult.Yes)
            {
                FlagDelete = true;
                SaveDB();

                //if (dgvItem.Rows.Count > 0)
                //{
                //    //string ConcatString=" where CancelTag=0 and ItemId in(";

                //    string AddItem = string.Empty;

                //    //Select distinct ID from ItemPurchaseQuantity where ItemId in (275,276) and SaleFlag=1
                //    for (int i = 0; i < dgvItem.Rows.Count; i++)
                //    {
                //        if (!string.IsNullOrEmpty(Convert.ToString(dgvItem.Rows[i].Cells["clmSaleFlag"].Value)))
                //        {
                //            SaveFlag = Convert.ToInt32(dgvItem.Rows[i].Cells["clmSaleFlag"].Value.ToString());
                //            if (SaveFlag == 1)
                //            {
                //                CheckDeleteFlag = true;
                //                MessageBox.Show("You Can't Delete this items");
                //                break;
                //                return;
                //            }
                //            else
                //                CheckDeleteFlag = false;
                //        }
                //        else
                //            CheckDeleteFlag = false;
                //    }

                //    if (!CheckDeleteFlag)
                //    {
                //        FlagDelete = true;
                //        SaveDB();
                //    }
                //}
            }
                
        }

        //ConcatString += AddItem + ")";

        //                objBL.Query = "Select disinct ID from ItemPurchaseQuantity " + ConcatString + "";

        //                DataSet ds = new DataSet();
        //                ds = objBL.ReturnDataSet();
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
        //                    {

        //                        return;
        //                    }
        //                }

        private void txtSearchItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearchItem.Text != "" && lbItem.Items.Count > 0)
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                {
                    lbItem.SelectedIndex = 0;
                    lbItem.Focus();
                }
            }
        }

      

       

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            ClearAll_Item();
        }

        private bool Validation_Item()
        {
            objEP.Clear();

            if (SCId == 0)
            {
                objEP.SetError(txtSearchSupplier, "Select Vendor");
                txtSearchSupplier.Focus();
                return true;
            }
            else if (ItemId == 0)
            {
                objEP.SetError(txtSearchItem, "Select Item");
                txtSearchItem.Focus();
                return true;
            }
            else if (txtQuantity.Text == "")
            {
                objEP.SetError(txtQuantity, "Enter QTY");
                txtSearchItem.Focus();
                return true;
            }
            
            else
                return false;
        }

        private bool Validation()
        {
            objEP.Clear();
           
            if (SCId == 0)
            {
                objEP.SetError(txtSearchSupplier, "Select Vendor");
                txtSearchSupplier.Focus();
                return true;
            }
            else if (dgvItem.Rows.Count == 0 && TableId ==0)
            {
                objEP.SetError(dgvItem, "Enter Item in Grid");
                txtSearchItem.Focus();
                return true;
            }
            else if (PSFlag == 1)
            {
                if (cmbEmployeeName.SelectedIndex == -1)
                {
                    objEP.SetError(dgvItem, "Enter Item in Grid");
                    txtSearchItem.Focus();
                    return true;
                }
                else
                    return false;
            }
            else if (PSFlag == 0)
            {
                 if (txtInvoiceNo.Text == "")
            {
                objEP.SetError(txtInvoiceNo, "Enter Bill No.");
                txtInvoiceNo.Focus();
                return true;
            }
                else
                    return false;
            }
            else
                return false;
        }

        private void btnAddGrid_Click(object sender, EventArgs e)
        {
            if (!Validation_Item())
            {
                if (GridFlag)
                    dgvItemRow = CurrentRowIndex;
                else
                    dgvItem.Rows.Add();

                //if(PurchaseTransactionId ==0)
                //    dgvItem1.Rows.Add();
                //else
                //   dgvItemRow= CurrentRowIndex;
                dgvItem.Rows[dgvItemRow].Cells["clmDelete"].Value = "Delete";
                dgvItem.Rows[dgvItemRow].Cells["clmPurchaseSaleItemsId"].Value = PurchaseSaleItemsId.ToString();
                dgvItem.Rows[dgvItemRow].Cells["clmItemId"].Value = ItemId.ToString();
                dgvItem.Rows[dgvItemRow].Cells["clmItemName"].Value = objRL.ItemName;
               // dgvItem.Rows[dgvItemRow].Cells["clmCategory"].Value = objRL.CategoryName;
                dgvItem.Rows[dgvItemRow].Cells["clmUOM"].Value = objRL.UOM;
                dgvItem.Rows[dgvItemRow].Cells["clmQty"].Value = txtQuantity.Text;

                //if (PurchaseTransactionId == 0)
                //    dgvItem.Rows[dgvItemRow].Cells["clmPreviousQuantity"].Value = txtQuantity.Text;

                SrNo_Add();

                if (PurchaseTransactionId != 0)
                    dgvItemRow = TempRowIndex;
                else
                    dgvItemRow++;

               
                ClearAll_Item();
                GridFlag = false;
                lblTotalItemCount.Text = "Total Item Count: " + dgvItem.Rows.Count;
                lbItem.Visible = true;
                objRL.Fill_Item_ListBox(lbItem, txtSearchItem.Text, "All");
                txtSearchItem.Focus();
            }
        }

        private void SrNo_Add()
        {
            if (dgvItem.Rows.Count > 0)
            {
                int SrNo = 1;
                for (int i = 0; i < dgvItem.Rows.Count; i++)
                {
                    dgvItem.Rows[i].Cells["clmSrNo"].Value = SrNo.ToString();
                    SrNo++;
                }
            }
            lblTotalItemCount.Text = "Total Item Count: " + dgvItem.Rows.Count.ToString();
        }

        string UOM = string.Empty;

        private void dgvItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearAll_Item();
            TempRowIndex = dgvItemRow;
            dgvItemRow = e.RowIndex;
            CurrentRowIndex = e.RowIndex;
            GridFlag = true;
            rtbItem.Visible = true;

            PurchaseTransactionId = Convert.ToInt32(dgvItem.Rows[dgvItemRow].Cells["clmPurchaseSaleItemsId"].Value.ToString());
            ItemId = Convert.ToInt32(dgvItem.Rows[dgvItemRow].Cells["clmItemId"].Value.ToString());
            objRL.ItemId = ItemId;
            
            //objRL.FillCustomerDetailsRichTextBox(rtbItem, ItemId);
            objRL.FillItemDetailsRichTextBox(rtbItem, ItemId);
            UOM = objRL.UOM;
            FillItem();
            // dtpExpiryDate.Value = Convert.ToDateTime(dgvItem1.Rows[dgvItemRow].Cells["clmExpiryDate"].Value.ToString());

            txtQuantity.Text = dgvItem.Rows[dgvItemRow].Cells["clmQty"].Value.ToString();
           
            //CGST = Convert.ToDouble(dgvItem.Rows[dgvItemRow].Cells["clmCGSTPer"].Value.ToString());
            //SGST = Convert.ToDouble(dgvItem.Rows[dgvItemRow].Cells["clmSGSTPer"].Value.ToString());
            //IGST = Convert.ToDouble(dgvItem.Rows[dgvItemRow].Cells["clmIGSTPer"].Value.ToString());

            //lblCGST.Text = "@ " + dgvItem1.Rows[dgvItemRow].Cells["clmCGSTPer"].Value.ToString() + " %";
            //lblSGST.Text = "@ " + dgvItem1.Rows[dgvItemRow].Cells["clmSGSTPer"].Value.ToString() + " %";
            //lblIGST.Text = "@ " + dgvItem1.Rows[dgvItemRow].Cells["clmIGSTPer"].Value.ToString() + " %";

            
            //SaveFlag = Convert.ToInt32(objRL.Check_NullString(Convert.ToString(dgvItem.Rows[dgvItemRow].Cells["clmSaleFlag"].Value)));
            //ItemPurchaseQuantityId_Insert = Convert.ToString(objRL.Check_NullString(Convert.ToString(dgvItem.Rows[dgvItemRow].Cells["clmItemPurchaseQuantityId"].Value)));

            lbItem.Visible = false;
        }

        

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            Item objForm = new Item();
            objForm.ShowDialog(this);
            objRL.Fill_Item_ListBox(lbItem, txtSearchItem.Text, "All");
        }

        private void btnAddVendor_Click(object sender, EventArgs e)
        {
             
                Supplier objForm = new Supplier();
                objForm.ShowDialog(this);
                objRL.Fill_Supplier_ListBox(lbSupplier, txtSearchSupplier.Text, "All");
           
        }

        //private void btnDeleteGrid_Click(object sender, EventArgs e)
        //{
        //    if (dgvItem1.Rows.Count > 0)
        //    {
        //        DialogResult dr;
        //        dr = MessageBox.Show("Do you want to delete this Item.?", "Delete", MessageBoxButtons.YesNo);
        //        if (dr == DialogResult.Yes)
        //        {
        //            for (int i = 0; i < dgvItem1.Rows.Count; i++)
        //            {
        //                dgvItem1.Rows.RemoveAt(i);
        //                ClearAll_Item();
        //                if (dgvItem1.Rows.Count > 0)
        //                    dgvItemRow = dgvItem1.Rows.Count;
        //                else
        //                    dgvItemRow = 0;
        //            }
        //            SrNo_Add();
        //        }
        //    }
        //}

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClearAll();
                dgvItem.Visible = true;
                gbItem.Visible = true;
                btnDelete.Enabled = true;
                lbItem.Visible = false;

                //0  P.ID,
                //1 P.PSFlag,
                //2 P.EntryDate as [Date],
                //3 P.InvoiceDate,
                //4 P.InvoiceNo,
                //5 P.ChallanNo,
                //6 P.SCId
                //7  PSColumn = "C.CustomerName as [Customer Name],
                //8 C.MobileNumber as [Mobile No]";
                //9 P.ItemCount,
                //10 P.Naration

                TableId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtPurchaseNo.Text = TableId.ToString();
                PSFlag = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                dtpEntryDate.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                dtpInvoiceDate.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtInvoiceNo.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtChallanNo.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                SCId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                Fill_Vendor_Customer_Information();
                lblTotalItemCount.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                txtNaration.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                cmbEmployeeName.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();

                if (TableId != 0)
                {
                    dgvItem.Rows.Clear();
                    dgvItemRow = 0;
                    DataSet ds = new DataSet();
                    //objBL.Query = "select P.ID,P.ItemId,P.Quantity,P.ExpiryDate,P.UOM,P.Cost,P.CGSTAmount,P.SGSTAmount,P.IGSTAmount,P.CGSTTax,P.SGSTTax,P.IGSTTax,P.TotalTaxAmount,C.CompanyName,C.ItemName,C.HSNCode from ((PurchaseTransaction P inner join Item C on C.ID=P.ItemId) inner join Purchase DC on P.PurchaseID=DC.ID) where P.CancelTag=0 and C.CancelTag=0 and DC.CancelTag=0 and DC.ID=" + TableId + "";
                    //objBL.Query = "select PT.ID,PT.ItemId,PT.Quantity,PT.ExpiryDate,PT.Cost,PT.Amount,PT.DiscountPercentage,PT.DiscountAmount,PT.TaxableAmount,PT.CGSTPer,PT.CGSTAmount,PT.SGSTPer,PT.SGSTAmount,PT.IGSTPer,PT.IGSTAmount,PT.TotalTaxAmount,PT.NetAmount,I.Category,I.CompanyName,I.ItemName,I.BatchNumber,I.HSNCode,I.Contain,I.UOM from ((PurchaseTransaction PT inner join Item I on I.ID=PT.ItemId) inner join Purchase DC on PT.PurchaseID=DC.ID) where PT.CancelTag=0 and I.CancelTag=0 and DC.CancelTag=0 and DC.ID=" + TableId + "";
                    //objBL.Query = "select PT.ID,PT.ItemId,PT.Quantity,PT.ExpiryDate,PT.Cost,PT.Amount,PT.DiscountPercentage,PT.DiscountAmount,PT.TaxableAmount,PT.CGSTPer,PT.CGSTAmount,PT.SGSTPer,PT.SGSTAmount,PT.IGSTPer,PT.IGSTAmount,PT.TotalTaxAmount,PT.NetAmount,I.ManufracturerId,M.ManufracturerName,I.ItemName,I.BatchNumber,I.HSNCode,I.UOM,I.Cost as [MainCost],I.Price,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from (((PSItems PT inner join Item I on I.ID=PT.ItemId) inner join Manufracturer M on I.ManufracturerId=M.ID) inner join PurchaseSale DC on PT.PSId=DC.ID) where PT.CancelTag=0 and I.CancelTag=0 and DC.CancelTag=0 and M.CancelTag=0 and PT.PSId=" + TableId + "";

                    objBL.Query = "select PSI.ID,PSI.PSFlag,PSI.PSId,PSI.EntryDate,CM.CategoryName,PSI.ItemId,I.ItemName,I.UOM,PSI.Quantity from (((PurchaseSaleItems PSI inner join Item I on I.ID=PSI.ItemId) inner join CategoryMaster CM on I.CategoryId=CM.ID) inner join PurchaseSale PS on PSI.PSId=PS.ID) where PSI.CancelTag=0 and I.CancelTag=0 and PS.CancelTag=0 and CM.CancelTag=0 and PSI.PSId=" + TableId + "";
                    ds = objBL.ReturnDataSet();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //0 PSI.ID,
                        //1 PSI.PSFlage,
                        //2 PSI.PSId,
                        //3 PSI.EntryDate,
                        //4 PSI.ItemId,
                        //5 PSI.Quantity

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dgvItem.Rows.Add();

                            PurchaseSaleItemsId = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                            dgvItem.Rows[dgvItemRow].Cells["clmPurchaseSaleItemsId"].Value = PurchaseSaleItemsId.ToString();

                            ItemId = Convert.ToInt32(ds.Tables[0].Rows[i]["ItemId"].ToString());
                            dgvItem.Rows[dgvItemRow].Cells["clmItemId"].Value = ItemId.ToString();

                            dgvItem.Rows[dgvItemRow].Cells["clmCategory"].Value = ds.Tables[0].Rows[i]["CategoryName"].ToString();
                            dgvItem.Rows[dgvItemRow].Cells["clmItemName"].Value = ds.Tables[0].Rows[i]["ItemName"].ToString();
                            dgvItem.Rows[dgvItemRow].Cells["clmUOM"].Value = ds.Tables[0].Rows[i]["UOM"].ToString();
                            dgvItem.Rows[dgvItemRow].Cells["clmQty"].Value = ds.Tables[0].Rows[i]["Quantity"].ToString();
                            dgvItem.Rows[dgvItemRow].Cells["clmDelete"].Value = "Delete";
                             
                            dgvItemRow++;
                        }
                        SrNo_Add();
                    }
                }
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
             
        }
 
        private void lblVendorDetails_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        int Result = 0;

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvItem.CurrentCell.ColumnIndex == 7)
                {
                    DialogResult dr;
                    dr = objRL.Delete_Record_Show_Message();
                    if (dr == DialogResult.Yes)
                    {
                        ItemId = Convert.ToInt32(Convert.ToString(dgvItem.Rows[e.RowIndex].Cells["clmItemId"].Value));

                        if (TableId != 0)
                        {
                            objBL.Query = "update PurchaseSaleItems set CancelTag=1 where CancelTag=0 and PSId=" + TableId + " and ItemId=" + ItemId + "";
                            Result= objBL.Function_ExecuteNonQuery();

                           // objRL.GetAvailableQuantity();
                        }

                        dgvItem.Rows.RemoveAt(e.RowIndex);
                        SrNo_Add();

                        ClearAll_Item();
                    }
                }
            }
            catch (Exception ex1) { }
            finally { GC.Collect(); }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                IDFlag = false;
                SearchByName = true;
            }
            else
            {
                IDFlag = false;
                SearchByName = false;
            }
            FillGrid();
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtInvoiceNo.Select();
        }

        private void txtBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtChallanNo.Select();
        }

        private void txtChallanNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                lbSupplier.Select();
        }
       
        private void txtIGSTPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddGrid.Select();
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            GetReport();
        }

        string Quantity_R = string.Empty, Cost_R = string.Empty, Amount_R = string.Empty, DiscountAmount_R = string.Empty, TaxableAmount_R = string.Empty, CGSTPer_R = string.Empty, CGSTAmount_R = string.Empty, SGSTPer_R = string.Empty, SGSTAmount_R = string.Empty, IGSTPer_R = string.Empty, IGSTAmount_R = string.Empty, TotalTaxAmount_R = string.Empty, NetAmount_R = string.Empty, ItemName_R = string.Empty, HSNCode_R = string.Empty, UOM_R = string.Empty;

        private void ClearItem_Report()
        {
            Quantity_R = string.Empty; Cost_R = string.Empty; Amount_R = string.Empty; DiscountAmount_R = string.Empty; TaxableAmount_R = string.Empty; CGSTPer_R = string.Empty; CGSTAmount_R = string.Empty; SGSTPer_R = string.Empty; SGSTAmount_R = string.Empty; IGSTPer_R = string.Empty; IGSTAmount_R = string.Empty; TotalTaxAmount_R = string.Empty; NetAmount_R = string.Empty; ItemName_R = string.Empty; HSNCode_R = string.Empty; UOM_R = string.Empty;
        }

        double TotalWithoutTax = 0, TotalDiscountItem = 0, CGSTTotal = 0, SGSTTotal = 0, IGSTTotal = 0;
        public void GetReport()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select PT.ID,PT.ItemId,PT.Quantity,PT.ExpiryDate,PT.Cost,PT.Amount,PT.DiscountPercentage,PT.DiscountAmount,PT.TaxableAmount,PT.CGSTPer,PT.CGSTAmount,PT.SGSTPer,PT.SGSTAmount,PT.IGSTPer,PT.IGSTAmount,PT.TotalTaxAmount,PT.NetAmount,I.ManufracturerId,M.ManufracturerName,I.ItemName,I.BatchNumber,I.HSNCode,I.UOM,I.Cost as [MainCost],I.Price,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from (((PSItems PT inner join Item I on I.ID=PT.ItemId) inner join Manufracturer M on I.ManufracturerId=M.ID) inner join PurchaseSale DC on PT.PSId=DC.ID) where PT.CancelTag=0 and I.CancelTag=0 and DC.CancelTag=0 and M.CancelTag=0 and PT.PSId=" + TableId + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                using (new CursorWait())
                {
                    TotalWithoutTax = 0; TotalDiscountItem = 0; CGSTTotal = 0; SGSTTotal = 0; IGSTTotal = 0;
                    BorderFlag = false;

                    object misValue = System.Reflection.Missing.Value;
                    myExcelApp = new Excel.Application();
                    myExcelWorkbooks = myExcelApp.Workbooks;

                    ReportName = txtPurchaseNo.Text;
                    objRL.ClearExcelPath();
                    objRL.isPDF = true;
                    objRL.Form_ExcelFileName = "SaleInvoice.xlsx";
                    objRL.Form_ReportFileName = "Sale Invoice-" + ReportName;
                    objRL.Form_DestinationReportFilePath = "\\Sale Invoice\\";
                    objRL.Path_Comman();

                    myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                    Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                    objRL.FillCompanyData();

                    myExcelWorksheet.get_Range("K2", misValue).Formula = txtPurchaseNo.Text.ToString();
                    myExcelWorksheet.get_Range("O2", misValue).Formula = dtpInvoiceDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);
                    myExcelWorksheet.get_Range("K3", misValue).Formula = txtPurchaseNo.Text.ToString() + "-" + dtpInvoiceDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);
                    
                    string FirmInformation = string.Empty;
                    FirmInformation = "Firm Name:" + objRL.CI_CompanyName.ToString() + "\n" +
                                                       "Address:" + objRL.CI_Address.ToString() + "\n" +
                                                       "GSTIN:" + objRL.CI_GSTIN.ToString() + "\n" +
                                                       "State Name: Maharashtra,Code: 27:" + "\n" +
                                                       "Email:" + objRL.CI_EmailId.ToString();

                    myExcelWorksheet.get_Range("A2", misValue).Formula = FirmInformation.ToString();

                    string CustomerInformationBillTo = string.Empty;
                    string CustomerInformationShipTo = string.Empty;
                    CustomerInformationBillTo = "Bill Address: " + objRL.CustomerName.ToString() + "\n" +
                                                      "Address: " + objRL.Address.ToString() + "\n" +
                                                      "GSTIN: " + objRL.GSTNumber.ToString() + "\n" +
                                                      "State Name: Maharashtra,Code: 27:" + "\n" +
                                                      "Email: " + objRL.EmailId.ToString();

                    CustomerInformationShipTo = "Ship Address: " + objRL.CustomerName.ToString() + "\n" +
                                                     "Address: " + objRL.Address.ToString() + "\n" +
                                                     "GSTIN: " + objRL.GSTNumber.ToString() + "\n" +
                                                     "State Name: Maharashtra,Code: 27:" + "\n" +
                                                     "Email: " + objRL.EmailId.ToString();


                    myExcelWorksheet.get_Range("A6", misValue).Formula = CustomerInformationBillTo.ToString();
                    myExcelWorksheet.get_Range("A10", misValue).Formula = CustomerInformationShipTo.ToString();

                    //Terms & Condition
                    //myExcelWorksheet.get_Range("H10", misValue).Formula = ;

                    RowCount = 17; SrNo = 1;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //Quantity_R = string.Empty; Cost_R = string.Empty; Amount_R = string.Empty; DiscountAmount_R = string.Empty; TaxableAmount_R = string.Empty; CGSTPer_R = string.Empty; CGSTAmount_R = string.Empty; SGSTPer_R = string.Empty; SGSTAmount_R = string.Empty; IGSTPer_R = string.Empty; IGSTAmount_R = string.Empty; TotalTaxAmount_R = string.Empty; NetAmount_R = string.Empty; ItemName_R = string.Empty; HSNCode_R = string.Empty; UOM_R = string.Empty;

                        ClearItem_Report();

                        // Sr. No	
                        AFlag = 1;
                        Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SrNo.ToString());

                        //Description of Goods
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ItemName"].ToString())))
                        {
                            AFlag = 0;
                            ItemName_R = Convert.ToString(ds.Tables[0].Rows[i]["ItemName"].ToString());
                            Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, ItemName_R.ToString());
                        }

                        //HSN SAC Code	
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["HSNCode"].ToString())))
                        {
                            AFlag = 1;
                            HSNCode_R = Convert.ToString(ds.Tables[0].Rows[i]["HSNCode"].ToString());
                            Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, HSNCode_R.ToString());
                        }
                        //Unit	
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["UOM"].ToString())))
                        {
                            AFlag = 1;
                            UOM_R = Convert.ToString(ds.Tables[0].Rows[i]["UOM"].ToString());
                            Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, UOM_R.ToString());
                        }
                        //Qty
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Quantity"].ToString())))
                        {
                            AFlag = 2;
                            Quantity_R = Convert.ToString(ds.Tables[0].Rows[i]["Quantity"].ToString());
                            Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, Quantity_R.ToString());
                        }
                        //Rate
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Cost"].ToString())))
                        {
                            AFlag = 2;
                            Cost_R = Convert.ToString(ds.Tables[0].Rows[i]["Cost"].ToString());
                            Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, Cost_R.ToString());
                        }
                        //Amount	
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Amount"].ToString())))
                        {
                            AFlag = 2;
                            Amount_R = Convert.ToString(ds.Tables[0].Rows[i]["Amount"].ToString());
                            Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, Amount_R.ToString());
                            TotalWithoutTax += Convert.ToDouble(Amount_R);
                        }
                        //Disc. Amt.	
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["DiscountAmount"].ToString())))
                        {
                            AFlag = 2;
                            DiscountAmount_R = Convert.ToString(ds.Tables[0].Rows[i]["DiscountAmount"].ToString());
                            Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, DiscountAmount_R.ToString());
                            TotalDiscountItem += Convert.ToDouble(DiscountAmount_R);
                        }
                        //Taxable Amt.	
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["TaxableAmount"].ToString())))
                        {
                            AFlag = 2;
                            TaxableAmount_R = Convert.ToString(ds.Tables[0].Rows[i]["TaxableAmount"].ToString());
                            Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, TaxableAmount_R.ToString());
                        }
                        //CGST Rate
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["CGSTPer"].ToString())))
                        {
                            AFlag = 2;
                            CGSTPer_R = Convert.ToString(ds.Tables[0].Rows[i]["CGSTPer"].ToString());
                            Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, CGSTPer_R.ToString());
                        }
                        //CGST Amt
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["CGSTAmount"].ToString())))
                        {
                            AFlag = 2;
                            CGSTAmount_R = Convert.ToString(ds.Tables[0].Rows[i]["CGSTAmount"].ToString());
                            Fill_Merge_Cell("K", "K", misValue, myExcelWorksheet, CGSTAmount_R.ToString());
                            CGSTTotal = Convert.ToDouble(CGSTAmount_R);

                        }
                        //SGST Rate	
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["SGSTPer"].ToString())))
                        {
                            AFlag = 2;
                            SGSTPer_R = Convert.ToString(ds.Tables[0].Rows[i]["SGSTPer"].ToString());
                            Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, SGSTPer_R.ToString());
                        }
                        //SGST Amt
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["SGSTAmount"].ToString())))
                        {
                            AFlag = 2;
                            SGSTAmount_R = Convert.ToString(ds.Tables[0].Rows[i]["SGSTAmount"].ToString());
                            Fill_Merge_Cell("M", "M", misValue, myExcelWorksheet, SGSTAmount_R.ToString());
                            SGSTTotal = Convert.ToDouble(SGSTAmount_R);

                        }
                        //IGST Rate	
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["IGSTPer"].ToString())))
                        {
                            AFlag = 2;
                            IGSTPer_R = Convert.ToString(ds.Tables[0].Rows[i]["IGSTPer"].ToString());
                            Fill_Merge_Cell("N", "N", misValue, myExcelWorksheet, IGSTPer_R.ToString());
                        }
                        //IGST Amt
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["IGSTAmount"].ToString())))
                        {
                            AFlag = 2;
                            IGSTAmount_R = Convert.ToString(ds.Tables[0].Rows[i]["IGSTAmount"].ToString());
                            Fill_Merge_Cell("O", "O", misValue, myExcelWorksheet, IGSTAmount_R.ToString());
                            IGSTTotal = Convert.ToDouble(IGSTAmount_R);
                        }
                        //Net Amount
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["NetAmount"].ToString())))
                        {
                            AFlag = 2;
                            NetAmount_R = Convert.ToString(ds.Tables[0].Rows[i]["NetAmount"].ToString());
                            Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, NetAmount_R.ToString());
                        }
                        SrNo++;
                        RowCount++;
                    }

                    //Total Amount
                    myExcelWorksheet.get_Range("G37", misValue).Formula = TotalWithoutTax.ToString();
                    //Discount Total
                    myExcelWorksheet.get_Range("H37", misValue).Formula = TotalDiscountItem.ToString();
                    //Total Taxable
                    
                    //CGST Total
                    myExcelWorksheet.get_Range("K37", misValue).Formula = CGSTTotal.ToString();
                    //SGST Total
                    myExcelWorksheet.get_Range("M37", misValue).Formula = SGSTTotal.ToString();
                    //IGST Total
                    myExcelWorksheet.get_Range("O37", misValue).Formula = IGSTTotal.ToString();
                    
                    

                    //Firm Bank Details

                    //Firm Bank Name
                    //if(!string.IsNullOrEmpty(
                    //myExcelWorksheet.get_Range("C39", misValue).Formula = objRL.Check_Null_String(Convert.ToString(objRL.CI_BankName));
                    //myExcelWorksheet.get_Range("C40", misValue).Formula = objRL.Check_Null_String(Convert.ToString(objRL.CI_AccountNumber));
                    //myExcelWorksheet.get_Range("C41", misValue).Formula = objRL.Check_Null_String(Convert.ToString(objRL.CI_BankAddress));
                    //myExcelWorksheet.get_Range("C42", misValue).Formula = objRL.Check_Null_String(Convert.ToString(objRL.CI_AccountType));
                    //myExcelWorksheet.get_Range("C43", misValue).Formula = objRL.Check_Null_String(Convert.ToString(objRL.CI_AccountHolderName));
                    //myExcelWorksheet.get_Range("C44", misValue).Formula = objRL.Check_Null_String(Convert.ToString(objRL.CI_IFSCCode));

                    
                    //Amount in words: 
                    int InvoiceAmountWords = 0;
                    double txtInvoiceTotalMain_D = 0;
                     
                    InvoiceAmountWords = Convert.ToInt32(Math.Round(txtInvoiceTotalMain_D));
                    myExcelWorksheet.get_Range("A50", misValue).Formula = "Amount in words: " + objRL.words(InvoiceAmountWords);

                    //

                    myExcelWorksheet.get_Range("A55", misValue).Formula = "Customer Name & Seal " + objRL.CustomerName.ToString();
                    myExcelWorksheet.get_Range("H55", misValue).Formula = "for " + objRL.CI_CompanyName.ToString();

                    myExcelWorkbook.Save();
                    PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");

                    try
                    {
                        const int xlQualityStandard = 0;
                        myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                        myExcelWorkbook.Close(true, misValue, misValue);
                        myExcelApp.Quit();

                        //objRL.ShowMessage(22, 1);

                        //DialogResult dr;
                        //dr = MessageBox.Show("Do you want to view this report", "Report View", MessageBoxButtons.YesNo);
                        //if (dr == DialogResult.Yes)
                        //    System.Diagnostics.Process.Start(objRL.RL_DestinationPath);

                        System.Diagnostics.Process.Start(PDFReport);
                        //objRL.DeleteExcelFile();

                        //if (!string.IsNullOrEmpty(objRL.EmailId) && cbEmail.Checked)
                        //{
                        //    objRL.EmailId_RL = cbEmail.Text;
                        //    objRL.Subject_RL = "Amount Collection Report";
                        //    //string body = "<div><p>Dear " + objRL.CustomerName_Customer + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
                        //    string body = "<div><p>Dear Sir,<p/><p>Please find attachment of pdf file.</p><p>Sale Invoice on " + dtpDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY) + "- Invoice No- " + txtNo.Text + " </p><p>Thanks,</p></div>";

                        //    objRL.Body_RL = body;// "Dear " + RedundancyLogics.VendorName + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
                        //    objRL.FilePath_RL = PDFReport;
                        //    objRL.SendEMail();
                        //}

                        
                    }
                    catch (Exception ex1)
                    {
                        objRL.ShowMessage(27, 4);
                        return;
                    }
                }
            }
            else
            {

            }
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
            {
                AlingRange1.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            }
        }

        private void lbSC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gbInvoiceDetails_Enter(object sender, EventArgs e)
        {

        }

        double Quantity = 0;
        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            //if (txtQuantity.Text != "")
            //{
            //    Quantity = Convert.ToDouble(txtQuantity.Text);

            //    if (PSFlag == 1)
            //    {
            //        if (Quantity > objRL.AvailableQuantity)
            //        {
            //            Quantity = 0; txtQuantity.Text = ""; txtQuantity.Focus();
            //            objRL.ShowMessage(33, 4);
            //            return;
            //        }
            //    }
            //}
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Employee objForm = new Employee();
            objForm.ShowDialog(this);
            objBL.FillComboBox_TableWise(cmbEmployeeName, "Employee", "ID", "EmployeeName");
        }


        private void cbWorkshop_CheckedChanged(object sender, EventArgs e)
        {
             
        }

        int IsWorkshop = 0;
    }
}
