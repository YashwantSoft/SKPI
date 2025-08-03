using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using SPApplication.Master;
using System.Globalization;

namespace SPApplication.Transaction
{
    public partial class Purchase : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        System.Globalization.RegionInfo objRegInfo = new RegionInfo("en-IN");
        int TableId = 0;
        bool DeleteFlag = false;

        int SupplierID = 0, ItemID = 0;

        double Quantity = 0, NetAmount = 0, DiscountPercentage = 0;
        static int dgvItemRow;

        public Purchase()
        {
            InitializeComponent();
            objBL.Set_Design_All(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_PURCHASE);
            objRL.Fill_Payment_Type(cmbPaymentMode);
        }

        private void Purchase_Load(object sender, EventArgs e)
        {
            dtpDate.CustomFormat = "dd/MM/yyyy";
            cbGST.Checked = true;
            
            ClearAll();
            SearchFlag = false;
            FillGrid();
            btnSave.Enabled = true;
            btnDelete.Enabled = false;

            lblCurrency.Text = objRegInfo.CurrencySymbol;
            txtPurchaseNo.Text = Convert.ToString(Convert.ToInt32(objRL.ReturnMaxID("Purchase")));
            objRL.GetBank(cmbBankName);
            txtSearchSupplier.Focus();
        }

        private void Fill_Supplier_ListBox()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,SupplierCode,SupplierName,Address,MobileNumber,EmailId,GSTNumber,StateCode from Supplier where CancelTag=0 and SupplierName like '%" + txtSearchSupplier.Text + "%' or SupplierCode like '%" + txtSearchSupplier.Text + "%' order by SupplierName desc";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbSupplier.Visible = true;
                lbSupplier.DataSource = ds.Tables[0];
                lbSupplier.DisplayMember = "SupplierName";
                lbSupplier.ValueMember = "ID";
            }
        }

        private void txtSearchSupplier_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchSupplier.Text != "")
                Fill_Supplier_ListBox();
            else
            {
                lbSupplier.Visible = false;
                lblSupplierDetails.Visible = false;
                SearchFlag = false;
                FillGrid();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void lbSupplier_Click(object sender, EventArgs e)
        {
            if (lbSupplier.Items.Count > 0)
            {
                SupplierID = Convert.ToInt32(lbSupplier.SelectedValue.ToString());
                Fill_Supplier_Details();
            }
        }

        private void lbSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SupplierID = Convert.ToInt32(lbSupplier.SelectedValue.ToString());
                Fill_Supplier_Details();
            }
        }

        private void Fill_Supplier_Details()
        {
            if (SupplierID != 0)
            {
                DataSet ds = new DataSet();
                objBL.Query = "select ID,SupplierCode,SupplierName,Address,MobileNumber,EmailId,GSTNumber,StateCode from Supplier where CancelTag=0 and ID=" + lbSupplier.SelectedValue + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblSupplierDetails.Visible = true;
                    lbSupplier.Visible = false;
                    SupplierName = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                    lblSupplierDetails.Text = "Supplier Name : " + SupplierName + "\n" + "Address:-" + ds.Tables[0].Rows[0]["Address"].ToString() + "\n" + "GST Number.:-" + ds.Tables[0].Rows[0]["GSTNumber"].ToString() + "";
                    gbItem.Visible = true;
                    dgvItem1.Visible = true;
                    ViewPendingAmount();
                    SearchFlag = true;
                    FillGrid();
                    txtSearchItemName.Select();
                }
            }
        }

        string SupplierName = "";

        private void ClearAll()
        {
            gbChequeDetails.Visible = false;

            txtAccountNoParty.Text = "";
            txtAccountNo.Text = "";
            cmbBankName.SelectedIndex = -1;
            txtBankParty.Text = "";
            txtChallanNo.Text = "";
            dtpTransactionDate.Value = DateTime.Now.Date;


            lblTotalCount.Text = "";
            lblTotalItemCount.Text = "";
            SearchFlag = false;
            dgvItem1.Rows.Clear();
            gbItem.Visible = false;
            //dgvItem1.Visible = false;
            objEP.Clear();
            cbGST.Checked = true;
            txtSearchSupplier.Text = "";
            txtSubTotal.Text = "";
            txtTotalGST.Text = "";
            txtFreight.Text = "";
            txtLoading.Text = "";
            txtSearchItemName.Text = "";
            txtInsurance.Text = "";
            txtOtherCharges.Text = "";
            txtInvoiceTotal.Text = "";
            dtpDate.Value = DateTime.Now;
            txtChallanNo.Text = "";
            txtBillNo.Text = "";
            txtPurchaseNo.Text = "";
            cmbPaymentMode.SelectedIndex = -1;
            lblSupplierDetails.Text = "Supplier Details";
            txtPurchaseNo.Text = Convert.ToString(Convert.ToInt32(objRL.ReturnMaxID("Purchase")));
            dgvItemRow = 0;
            TableId = 0;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            ClearAll_Item();

            if (dataGridView1.Rows.Count > 0)
                lblTotalCount.Text = "Total Count: " + dataGridView1.Rows.Count;

            txtChallanNo.Focus();
        }

        private void txtSearchSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearchSupplier.Text != "" && lbSupplier.Items.Count > 0)
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                    lbSupplier.Focus();
            }
        }

        private void Fill_Item_ListBox()
        {
            lbItem.DataSource = null;
            DataSet ds = new DataSet();
            //objBL.Query = "select I.ID,I.ItemName,I.Description,I.UOMID,U.UnitOfMessurement,I.ItemPrice from Item I inner join UOM U on U.ID=I.UOMID where I.CancelTag=0 and U.CancelTag=0 and I.ItemName like '%" + txtSearchItemName.Text + "%' order by ItemName desc";
            //objBL.Query = "select ID,Category,CompanyName,ItemName,BatchNumber,HSNCode,Contain,UOM,Price,Cost,MRP,ProfitMarginPer,ProfitMarginAmount,CGST,SGST,IGST from Item where CancelTag=0 and ItemName like '%" + txtSearchItemName.Text + "%' or BatchNumber like '%" + txtSearchItemName.Text + "%'";
            objBL.Query = "select I.ID,I.ManufracturerId,M.ManufracturerName as [Manufracturer Name],I.ItemName as [Item Name],I.BatchNumber,I.HSNCode,I.UOM,I.Price,I.Cost,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from Item I inner join Manufracturer M on I.ManufracturerId=M.ID where I.CancelTag=0 and I.ItemName like '%" + txtSearchItemName.Text + "%'";
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
            if (txtSearchItemName.Text != "")
                Fill_Item_ListBox();
            else
                lbItem.Visible = false;
        }

        private void ClearAll_Item()
        {
            btnDeleteGridMain.Visible = true;
            PurchaseTransactionId = 0;
            TempRowIndex = 0;
            dgvItemRow = 0;
            rtbItemDetails.Text = "";

            lblCGST.Text = "@";
            lblSGST.Text = "@";
            lblIGST.Text = "@";
            txtQuantity.Text = "";
            txtCost.Text = "";
            txtAmount.Text = "";

            txtDiscountPercentage.Text = "";
            txtDiscountAmount.Text = "";
            
            txtGSTAmount.Text = "";
            txtSGSTAmount.Text = "";
            txtIGSTAmount.Text = "";
           
            txtTaxableAmount.Text = "";
            txtTaxTotal.Text = "";
            txtNetAmount.Text = "";

            rtbItemDetails.Visible = false;

            if (dgvItem1.Rows.Count > 0)
                dgvItemRow = dgvItem1.Rows.Count;
        }

        string UOM = "";
        double CGST = 0, SGST = 0, IGST = 0;
        private void Fill_Items_Details()
        {
            if (lbItem.Items.Count > 0)
            {
                ClearAll_Item();


                DataSet ds = new DataSet();
                //objBL.Query = "select I.ID,I.CompanyName,I.ItemName,I.Description,I.UOMID,U.UnitOfMessurement,I.HSNCode,I.ItemPrice,I.Cost,I.CGST,I.SGST,I.IGST from Item I inner join UOM U on U.ID=I.UOMID where I.CancelTag=0 and U.CancelTag=0 and I.ItemName='" + lbItem.Text + "' and I.ID=" + lbItem.SelectedValue + "";
                //objBL.Query = "select ID,Category,CompanyName,ItemName,BatchNumber,HSNCode,Contain,UOM,Price,Cost,MRP,ProfitMarginPer,ProfitMarginAmount,CGST,SGST,IGST from Item where CancelTag=0 and ID=" + lbItem.SelectedValue + "";
                objBL.Query = "select I.ID,I.ManufracturerId,M.ManufracturerName,I.ItemName,I.BatchNumber,I.HSNCode,I.UOM,I.Price,I.Cost,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from Item I inner join Manufracturer M on I.ManufracturerId=M.ID where I.CancelTag=0 and I.ID=" + lbItem.SelectedValue + "";
                //objBL.Query = "select I.ID,I.ItemName,I.ItemCode,I.HSNCode,I.Description,I.UOMID,U.UnitOfMessurement from Item I inner join UOM U on U.ID=I.UOMID where I.CancelTag=0 and U.CancelTag=0 and I.ID=" + lbItem.SelectedValue + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbItem.Visible = false;
                    rtbItemDetails.Visible = true;
                    // objRL.Category = ds.Tables[0].Rows[0]["Category"].ToString();
                    objRL.CompanyName = ds.Tables[0].Rows[0]["ManufracturerName"].ToString();
                    objRL.ItemName = ds.Tables[0].Rows[0]["ItemName"].ToString();
                    objRL.BatchNumber = ds.Tables[0].Rows[0]["BatchNumber"].ToString();
                    objRL.HSNCode = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                    //objRL.Contain = ds.Tables[0].Rows[0]["Contain"].ToString();
                    objRL.UOM = ds.Tables[0].Rows[0]["UOM"].ToString();
                    objRL.Set_Label();
                    rtbItemDetails.Text = objRL.OutputLabel;

                    //UOM = ds.Tables[0].Rows[0]["UnitOfMessurement"].ToString();

                    if (ds.Tables[0].Rows[0]["CGST"].ToString() != "")
                    {
                        CGST = Convert.ToDouble(Convert.ToDouble(ds.Tables[0].Rows[0]["CGST"].ToString()));
                        lblCGST.Text = "@" + ds.Tables[0].Rows[0]["CGST"].ToString() + " %";
                    }

                    if (ds.Tables[0].Rows[0]["SGST"].ToString() != "")
                    {
                        SGST = Convert.ToDouble(ds.Tables[0].Rows[0]["SGST"].ToString());
                        lblSGST.Text = "@" + ds.Tables[0].Rows[0]["SGST"].ToString() + " %";
                    }


                    if (ds.Tables[0].Rows[0]["IGST"].ToString() != "")
                    {
                        IGST = Convert.ToDouble(ds.Tables[0].Rows[0]["IGST"].ToString());
                        lblIGST.Text = "@" + ds.Tables[0].Rows[0]["IGST"].ToString() + " %";
                    }

                    ItemID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    txtCost.Text = ds.Tables[0].Rows[0]["Cost"].ToString();
                    txtQuantity.Focus();
                }
            }
        }

        private void lbItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Fill_Items_Details();
        }

        private void lbItem_Click(object sender, EventArgs e)
        {
            Fill_Items_Details();
        }

        private void Fill_ItemDetails_by_ID()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select I.ID,I.ItemName,I.Description,I.UOMID,U.UnitOfMessurement,I.ItemPrice from Item I inner join UOM U on U.ID=I.UOMID where I.CancelTag=0 and U.CancelTag=0 and I.ID=" + ItemID + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lbItem.Visible = false;
                // txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                //txtUOM.Text = ds.Tables[0].Rows[0]["UnitOfMessurement"].ToString();
                ItemID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                txtQuantity.Focus();
            }
        }

        private void txtSearchItemId_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Item();
            txtSearchItemName.Text = "";
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

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtCost);
        }

        private void txtVAT_KeyPress(object sender, KeyPressEventArgs e)
        {
            // objRL.FloatValue(sender, e, txtVAT);
        }

        private void txtVATAll_KeyPress(object sender, KeyPressEventArgs e)
        {
            //objRL.FloatValue(sender, e, txtVATPerBill);
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtTotalGST);
        }

        private void txtTransportationCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            //objRL.FloatValue(sender, e, txtTransportationCharges);
        }

        private void txtOtherCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtFreight);
        }

        private void FillGrid()
        {
             
                dataGridView1.DataSource = null;
                DataSet ds = new DataSet();

                if(SearchFlag)
                    objBL.Query = "select P.ID,P.PurchaseDate,P.PurchaseNo,P.ChallanNo,P.BillNo,P.SupplierId,C.SupplierName,C.Address,C.MobileNumber,C.EmailId,C.GSTNumber,P.SubTotal,P.TotalGST,P.FreightCharges,P.LoadingAndPackingCharges,P.InsuranceCharges,P.OtherCharges,P.InvoiceTotal,P.PaymentMode,P.BankId,P.BankName,P.AccountNumber,P.TransactionDate,P.ChequeNumber,P.PartyBank,P.PartyBankAccountNumber,P.IsGST from Purchase P inner join Supplier C on C.ID=P.SupplierId where P.CancelTag=0 and C.CancelTag=0 and P.SupplierId=" + SupplierID + "";
                else
                    objBL.Query = "select P.ID,P.PurchaseDate,P.PurchaseNo,P.ChallanNo,P.BillNo,P.SupplierId,C.SupplierName,C.Address,C.MobileNumber,C.EmailId,C.GSTNumber,P.SubTotal,P.TotalGST,P.FreightCharges,P.LoadingAndPackingCharges,P.InsuranceCharges,P.OtherCharges,P.InvoiceTotal,P.PaymentMode,P.BankId,P.BankName,P.AccountNumber,P.TransactionDate,P.ChequeNumber,P.PartyBank,P.PartyBankAccountNumber,P.IsGST from Purchase P inner join Supplier C on C.ID=P.SupplierId where P.CancelTag=0 and C.CancelTag=0";

                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[19].Visible = false;
                    //dataGridView1.Columns[20].Visible = false;
                    dataGridView1.Columns[1].Width = 80;
                    dataGridView1.Columns[2].Width = 60;
                    dataGridView1.Columns[3].Width = 60;
                    dataGridView1.Columns[4].Width = 60;
                    dataGridView1.Columns[6].Width = 120;
                    dataGridView1.Columns[7].Width = 120;
                    dataGridView1.Columns[8].Width = 80;
                    dataGridView1.Columns[9].Width = 100;
                    dataGridView1.Columns[10].Width = 80;
                    dataGridView1.Columns[11].Width = 60;
                    dataGridView1.Columns[12].Width = 60;
                    dataGridView1.Columns[13].Width = 60;
                    dataGridView1.Columns[14].Width = 60;
                    dataGridView1.Columns[15].Width = 60;
                    dataGridView1.Columns[16].Width = 60;
                    dataGridView1.Columns[17].Width = 60;
                    dataGridView1.Columns[18].Width = 60;
                    this.dataGridView1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dataGridView1.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dataGridView1.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dataGridView1.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dataGridView1.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dataGridView1.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dataGridView1.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
        }

        DateTime dt = new DateTime();
        string PurchaseNo = "", ChallanNo = "", BillNo = "", NetTotal = "", FreightCharges = "", LoadingAndPackingCharges = "", InsuranceCharges = "", OtherCharge = "";
        double InvoiceTotal = 0, TotalGST = 0;

        string ChequeNumber = "", PartyBankAccountNumber = "", PartyBank = "", AccountNumber = "", BankName="";
        int BankId = 0;
        DateTime dtTransactionDate;

        private void Fill_BankDetails_PaymentMode()
        {
            if (cmbPaymentMode.Text != "CASH" && cmbPaymentMode.Text != "CREDIT")
            {
                if(cmbPaymentMode.Text == "CHEQUE")
                    ChequeNumber = txtChequeNo.Text;

                BankId = Convert.ToInt32(cmbBankName.SelectedValue);
                AccountNumber = txtAccountNo.Text;
                BankName = cmbBankName.Text;

                PartyBankAccountNumber = txtAccountNoParty.Text;
                PartyBank =txtBankParty.Text;
                dtTransactionDate = dtpTransactionDate.Value;
            }
        }

        private bool ValidationBank()
        {
            bool ReturnFlag = false;
            objEP.Clear();
            if (cmbPaymentMode.Text != "CASH" && cmbPaymentMode.Text != "CREDIT")
            {
                if (cmbBankName.SelectedIndex == -1)
                {
                    cmbBankName.Focus();
                    objEP.SetError(cmbBankName, "Select Bank Name");
                    ReturnFlag = true;
                }
                else
                    ReturnFlag = false;

                if (!ReturnFlag)
                {
                    if (cmbBankName.Text == "CHEQUE")
                    {
                        if (txtChequeNo.Text == "")
                        {
                            txtChequeNo.Focus();
                            objEP.SetError(txtChequeNo, "Enter Cheque Number");
                            ReturnFlag = true;
                        }
                        else
                            ReturnFlag = false;
                    }
                    else
                        ReturnFlag = false;
                }
                if (!ReturnFlag)
                {
                    if (txtBankParty.Text == "")
                    {
                        txtBankParty.Focus();
                        objEP.SetError(txtBankParty, "Enter Bank Party");
                        ReturnFlag = true;
                    }
                    else
                        ReturnFlag = false;
                }
                if (!ReturnFlag)
                {
                    if (txtAccountNoParty.Text == "")
                    {
                        txtAccountNoParty.Focus();
                        objEP.SetError(txtAccountNoParty, "Enter Account No Party");
                        ReturnFlag = true;
                    }
                    else
                        ReturnFlag = false;
                }
            }
            else
                ReturnFlag = false;

            return ReturnFlag;
        }
        
        protected void SaveDB()
        {
            if (!Validation())
            {
                Fill_BankDetails_PaymentMode();
                if (TableId != 0)
                {
                    if (!DeleteFlag)
                        objBL.Query = "update Purchase set PurchaseDate='" + dtpDate.Value.ToString("MM/dd/yyyy") + "',PurchaseNo='" + txtPurchaseNo.Text + "',ChallanNo='" + txtChallanNo.Text + "',BillNo='" + txtBillNo.Text + "',SupplierId=" + SupplierID + ",SubTotal='" + txtSubTotal.Text + "',TotalGST='" + txtTotalGST.Text + "',FreightCharges='" + txtFreight.Text + "',LoadingAndPackingCharges='" + txtLoading.Text + "',InsuranceCharges='" + txtInsurance.Text + "',OtherCharges='" + txtOtherCharges.Text + "',InvoiceTotal='" + txtInvoiceTotal.Text + "',PaymentMode='" + cmbPaymentMode.Text + "',BankId=" + BankId + ",BankName='" + BankName + "',AccountNumber='" + AccountNumber + "',TransactionDate='" + dtTransactionDate.ToShortDateString() + "',ChequeNumber='" + ChequeNumber + "',PartyBank='" + PartyBank + "',PartyBankAccountNumber='" + PartyBankAccountNumber + "',IsGST=" + ISGST + ",UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableId + " and CancelTag=0 ";
                    else
                        objBL.Query = "update Purchase set CancelTag=1 where ID=" + TableId + " and CancelTag=0 ";
                }
                else
                {
                    dt = dtpDate.Value;
                    PurchaseNo = txtPurchaseNo.Text; ChallanNo = txtChallanNo.Text; BillNo = txtBillNo.Text; NetTotal = txtSubTotal.Text; TotalGST = Convert.ToDouble(txtTotalGST.Text); FreightCharges = txtFreight.Text; LoadingAndPackingCharges = txtLoading.Text; InsuranceCharges = txtInsurance.Text; OtherCharge = txtOtherCharges.Text; InvoiceTotal = Convert.ToDouble(txtInvoiceTotal.Text);
                    objBL.Query = "insert into Purchase(PurchaseDate,PurchaseNo,ChallanNo,BillNo,SupplierId,SubTotal,TotalGST,FreightCharges,LoadingAndPackingCharges,InsuranceCharges,OtherCharges,InvoiceTotal,PaymentMode,BankId,BankName,AccountNumber,TransactionDate,ChequeNumber,PartyBank,PartyBankAccountNumber,IsGST,PrintFlag,PrintCount,UserId) values('" + dtpDate.Value.ToString("MM/dd/yyyy") + "','" + txtPurchaseNo.Text + "','" + txtChallanNo.Text + "','" + txtBillNo.Text + "'," + SupplierID + ",'" + txtSubTotal.Text + "','" + txtTotalGST.Text + "','" + txtFreight.Text + "','" + txtLoading.Text + "','" + txtInsurance.Text + "','" + txtOtherCharges.Text + "','" + txtInvoiceTotal.Text + "','" + cmbPaymentMode.Text + "'," + BankId + ",'" + BankName + "','" + AccountNumber + "','" + dtTransactionDate.ToShortDateString() + "','" + ChequeNumber + "','" + PartyBank + "','" + PartyBankAccountNumber + "'," + ISGST + ",0,0," + BusinessLayer.UserId_Static + ") ";
                }
                objBL.Function_ExecuteNonQuery();

                if (TableId == 0)
                    TableId = objRL.Return_Transaction_ID("Purchase");

                if (TableId != 0)
                {
                    SaveItemList();

                    if (cmbPaymentMode.SelectedIndex > -1)
                    {
                        if (cmbPaymentMode.Text == "CREDIT")
                        {
                            if (txtInvoiceTotal.Text != "")
                            {
                                ViewPendingAmount();
                                PendingAmount_Insert = Convert.ToDouble(txtInvoiceTotal.Text);
                                PendingAmount_Insert = PendingAmount + PendingAmount_Insert;

                                if (objRL.PendingFlag == true)
                                    objBL.Query = "update SupplierPendingAmount set PendingAmount='" + PendingAmount_Insert + "' where CancelTag=0 and SupplierId=" + SupplierID + "";
                                else
                                    objBL.Query = "insert into SupplierPendingAmount(SupplierId,PendingAmount) values(" + SupplierID + ",'" + PendingAmount_Insert + "')";

                                objBL.Function_ExecuteNonQuery();
                            }
                        }
                    }
                    objRL.ShowMessage(7, 1);
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

        double PendingAmount_Insert = 0; double PendingAmount = 0;
        private void ViewPendingAmount()
        {
            PendingAmount = objRL.Get_Pending_Details("SupplierPendingAmount", SupplierID);
            txtPendingAmount.Text = PendingAmount.ToString();
        }


        int ItemId_Insert = 0;
        double QtyInsert = 0;
        string Cost_Insert = "", Quantity_Insert = "", Amount_Insert = "", DiscountPer_Insert = "", DiscountAmount_Insert = "", TotalTax_Insert = "", TaxableAmount_Insert = "", NetAmount_Insert = "";
        string CGSTPercentage_Insert = "", CGSTAmount_Insert = "", SGSTPercentage_Insert = "", SGSTAmount_Insert = "";
        string IGSTPercentage_Insert = "", IGSTAmount_Insert = "", ItemPurchaseQuantityId_Insert = "";

        DateTime InsertExpiry;

        protected void SaveItemList()
        {
            for (int i = 0; i < dgvItem1.Rows.Count; i++)
            {
                ClearItemList();
                int PurchaseTransactionId = 0;

                PurchaseTransactionId = Convert.ToInt32(dgvItem1.Rows[i].Cells["clmPurchaseTransactionId"].Value.ToString());
                ItemId_Insert = Convert.ToInt32(dgvItem1.Rows[i].Cells["clmItemId"].Value.ToString());

                Cost_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmCost"].Value));
                Quantity_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmQty"].Value));

                InsertExpiry = Convert.ToDateTime(dgvItem1.Rows[i].Cells["clmExpiryDate"].Value.ToString());

                Amount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmAmount"].Value));
                DiscountPer_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmDiscountPercentage"].Value));
                DiscountAmount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmDiscountAmount"].Value));

                TaxableAmount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmTaxableAmount"].Value));

                CGSTPercentage_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmCGSTPer"].Value));
                SGSTPercentage_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmSGSTPer"].Value));
                IGSTPercentage_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmIGSTPer"].Value));

                CGSTAmount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmCGSTAmount"].Value));
                SGSTAmount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmSGSTAmount"].Value));
                IGSTAmount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmIGSTAmount"].Value));

                TotalTax_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmTotalGST"].Value));
                NetAmount_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmNetAmount"].Value));

                ItemPurchaseQuantityId_Insert = objRL.Check_Null_String(Convert.ToString(dgvItem1.Rows[i].Cells["clmItemPurchaseQuantityId"].Value));
                QtyInsert = Convert.ToDouble(Quantity_Insert);

                if (!DeleteFlag)
                {
                    if (PurchaseTransactionId == 0)
                        objBL.Query = "insert into PurchaseTransaction(PurchaseID,ItemId,ExpiryDate,Quantity,Cost,Amount,DiscountPercentage,DiscountAmount,TaxableAmount,CGSTPer,CGSTAmount,SGSTPer,SGSTAmount,IGSTPer,IGSTAmount,TotalTaxAmount,NetAmount,UserId) values(" + TableId + "," + ItemId_Insert + ",'" + InsertExpiry + "','" + Quantity_Insert + "','" + Cost_Insert + "','" + Amount_Insert + "','" + DiscountPer_Insert + "','" + DiscountAmount_Insert + "','" + TaxableAmount_Insert + "','" + CGSTPercentage_Insert + "','" + CGSTAmount_Insert + "','" + SGSTPercentage_Insert + "','" + SGSTAmount_Insert + "','" + IGSTPercentage_Insert + "','" + IGSTAmount_Insert + "','" + TotalTax_Insert + "','" + NetAmount_Insert + "'," + BusinessLayer.UserId_Static + ")";
                    else
                        objBL.Query = "Update PurchaseTransaction set ItemId=" + ItemId_Insert + ",ExpiryDate='" + InsertExpiry + "',Quantity='" + Quantity_Insert + "',Cost='" + Cost_Insert + "',Amount='" + Amount_Insert + "',DiscountPercentage='" + DiscountPer_Insert + "',DiscountAmount='" + DiscountAmount_Insert + "',TaxableAmount='" + TaxableAmount_Insert + "',CGSTPer='" + CGSTPercentage_Insert + "',CGSTAmount='" + CGSTAmount_Insert + "',SGSTPer='" + SGSTPercentage_Insert + "',SGSTAmount='" + SGSTAmount_Insert + "',IGSTPer='" + IGSTPercentage_Insert + "',IGSTAmount='" + IGSTAmount_Insert + "',TotalTaxAmount='" + TotalTax_Insert + "',NetAmount='" + NetAmount_Insert + "',UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + PurchaseTransactionId + "";

                    objBL.Function_ExecuteNonQuery();

                    if (PurchaseTransactionId == 0)
                        PurchaseTransactionId = objRL.Return_Transaction_ID("PurchaseTransaction");
                }
                else
                {
                    objBL.Query = "Update PurchaseTransaction set CancelTag=1,UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + PurchaseTransactionId + "";
                    objBL.Function_ExecuteNonQuery();
                }

                DataSet dsItemPurchaseQuantity = new DataSet();
                //objBL.Query = "select ID,ItemId,PurchaseId,PurchaseTransactionId,ExpiryDate,PurchaseQuantity,UserId from ItemPurchaseQuantity where CancelTag=0 and ItemId=" + ItemId_Insert + "";
                objBL.Query = "select ItemId,Quantity from ItemQuantity where CancelTag=0 and ItemId=" + ItemId_Insert + "";
                dsItemPurchaseQuantity = objBL.ReturnDataSet();

                double PurchaseQuantity = 0, PreviousQty = 0, CurrentQty = 0;

                if (dsItemPurchaseQuantity.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dsItemPurchaseQuantity.Tables[0].Rows[0][0].ToString()))
                    {
                        PurchaseQuantity = 0; CurrentQty = 0; PreviousQty = 0;
                        CurrentQty = Convert.ToDouble(Quantity_Insert);
                        PurchaseQuantity = Convert.ToDouble(dsItemPurchaseQuantity.Tables[0].Rows[0]["Quantity"].ToString());

                        if (!string.IsNullOrEmpty(Convert.ToString(dgvItem1.Rows[i].Cells["clmPreviousQuantity"].Value)) && !string.IsNullOrEmpty(Convert.ToString(dgvItem1.Rows[i].Cells["clmQty"].Value)))
                        {
                            if (dgvItem1.Rows[i].Cells["clmPreviousQuantity"].Value.ToString() != dgvItem1.Rows[i].Cells["clmQty"].Value.ToString())
                            {
                                PreviousQty = Convert.ToDouble(dgvItem1.Rows[i].Cells["clmPreviousQuantity"].Value.ToString());
                                PurchaseQuantity = (PurchaseQuantity + CurrentQty) - PreviousQty;
                            }
                            else
                                PurchaseQuantity = PurchaseQuantity + CurrentQty;
                        }
                        objBL.Query = "Update ItemQuantity set Quantity='" + PurchaseQuantity + "',UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0  and ItemId=" + ItemId_Insert + "";
                        objBL.Function_ExecuteNonQuery();
                    }
                }
                else
                {
                    if (ItemPurchaseQuantityId_Insert == "0" || ItemPurchaseQuantityId_Insert == "")
                    {
                        objBL.Query = "insert into ItemQuantity(ItemId,Quantity,UserId) values(" + ItemId_Insert + ",'" + Quantity_Insert + "'," + BusinessLayer.UserId_Static + ")";
                        objBL.Function_ExecuteNonQuery();
                    }
                }
            }
        }

        bool ItemFlag = false;
        double PQTY = 0, AQTY = 0;

        protected bool Check_Exixt_ItemIn_Stock()
        {
            objBL.Query = "Select ID,ItemId,PQty,SQty,AvailableQty,UserId from ItemStock where ItemId=" + ItemId_Insert + " and ExpiryDate=#" + InsertExpiry + "#";
            DataSet ds = new DataSet();
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                PQTY = Convert.ToDouble(ds.Tables[0].Rows[0][2].ToString());
                AQTY = Convert.ToDouble(ds.Tables[0].Rows[0][4].ToString());
                ItemFlag = true;
            }
            else
                ItemFlag = false;

            return ItemFlag;
        }

        private void ClearItemList()
        {
            Cost_Insert = "";
            Quantity_Insert = "";
            CGSTPercentage_Insert = "";
            CGSTAmount_Insert = "";
            SGSTPercentage_Insert = "";
            SGSTAmount_Insert = "";
            IGSTPercentage_Insert = "";
            IGSTAmount_Insert = "";
            TotalTax_Insert = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDB();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
        }

        bool CheckDeleteFlag = false;
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                DialogResult dr;
                dr = MessageBox.Show("Do you want to delete this record.?", "Delete", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    if (dgvItem1.Rows.Count > 0)
                    {
                        //string ConcatString=" where CancelTag=0 and ItemId in(";

                        string AddItem = string.Empty;

                        //Select distinct ID from ItemPurchaseQuantity where ItemId in (275,276) and SaleFlag=1
                        for (int i = 0; i < dgvItem1.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dgvItem1.Rows[i].Cells["clmSaleFlag"].Value)))
                            {
                                SaveFlag = Convert.ToInt32(dgvItem1.Rows[i].Cells["clmSaleFlag"].Value.ToString());
                                if (SaveFlag == 1)
                                {
                                    CheckDeleteFlag = true;
                                    MessageBox.Show("You Can't Delete this items");
                                    break;
                                    return;
                                }
                                else
                                    CheckDeleteFlag = false;
                            }
                            else
                                CheckDeleteFlag = false;
                        }

                        if (!CheckDeleteFlag)
                        {
                            DeleteFlag = true;
                            SaveDB();
                        }
                    }
                }
                else
                    return;
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
            if (txtSearchItemName.Text != "" && lbItem.Items.Count > 0)
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                    lbItem.Focus();
            }
        }

        double Amount = 0, DiscountAmount = 0;

        private void CalculateTaxableAmount_Item()
        {
            Cost = 0; Quantity = 0; Amount = 0; DiscountPercentage = 0; DiscountAmount = 0; TaxableAmount = 0;

            double.TryParse(txtCost.Text, out Cost);
            double.TryParse(txtQuantity.Text, out Quantity);
            double.TryParse(txtDiscountAmount.Text, out DiscountAmount);

            Amount = Cost * Quantity;
            txtAmount.Text = Amount.ToString();

            if (txtDiscountAmount.Text != "")
            {
                DiscountPercentage = DiscountAmount / Amount * 100;
                DiscountPercentage = Math.Round(DiscountPercentage, 2);

                if (DiscountPercentage != 0)
                    txtDiscountPercentage.Text = DiscountPercentage.ToString();
                else
                    txtDiscountPercentage.Text = "";
            }
            else
                txtDiscountPercentage.Text = "";

            TaxableAmount = Amount - DiscountAmount;
            txtTaxableAmount.Text = TaxableAmount.ToString();

            GST_Calculation();
            Calculate_NetAmount_GridValues();
            Calculate_TotalAmount_All();
        }

        double SubTotal = 0, Freight = 0, Loading = 0, Insurance = 0, OtherCharges = 0, AllTotal = 0;
        double Cost = 0, TaxableAmount = 0;
        int PurchaseTransactionId = 0;
        bool GridFlag = false;
        int TempRowIndex = 0;


        private void Calculate_TotalAmount_All()
        {
            SubTotal = 0; Freight = 0; Loading = 0; Insurance = 0; OtherCharges = 0; AllTotal = 0;
            double.TryParse(txtSubTotal.Text, out SubTotal);
            double.TryParse(txtTotalGST.Text, out TotalGST);
            double.TryParse(txtFreight.Text, out Freight);
            double.TryParse(txtLoading.Text, out Loading);
            double.TryParse(txtInsurance.Text, out Insurance);
            double.TryParse(txtOtherCharges.Text, out OtherCharges);

            NetAmount = SubTotal + TotalGST + Freight + Loading + Insurance + OtherCharges;
            NetAmount = Math.Round(NetAmount);
            txtInvoiceTotal.Text = NetAmount.ToString();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateTaxableAmount_Item();
        }

        private void btnClearItem_Click(object sender, EventArgs e)
        {
            ClearAll_Item();
        }

        private bool Validation_Item()
        {
            objEP.Clear();

            if (SupplierID == 0)
            {
                objEP.SetError(txtSearchSupplier, "Select Supplier");
                txtSearchSupplier.Focus();
                return true;
            }
            else if (ItemID == 0)
            {
                objEP.SetError(txtSearchItemName, "Select Item");
                txtSearchItemName.Focus();
                return true;
            }
            else if (txtQuantity.Text == "")
            {
                objEP.SetError(txtQuantity, "Enter QTY");
                txtSearchItemName.Focus();
                return true;
            }
            else if (txtCost.Text == "")
            {
                objEP.SetError(txtCost, "Enter Cost");
                txtCost.Focus();
                return true;
            }
            else
                return false;
        }

        private bool Validation()
        {
            objEP.Clear();
            if (txtChallanNo.Text == "")
            {
                objEP.SetError(txtChallanNo, "Enter Challan No.");
                txtChallanNo.Focus();
                return true;
            }
            else if (txtBillNo.Text == "")
            {
                objEP.SetError(txtBillNo, "Enter Bill No.");
                txtBillNo.Focus();
                return true;
            }
            else if (SupplierID == 0)
            {
                objEP.SetError(txtSearchSupplier, "Select Supplier");
                txtSearchSupplier.Focus();
                return true;
            }
            else if (dgvItem1.Rows.Count == 0)
            {
                objEP.SetError(dgvItem1, "Enter Item in Grid");
                txtSearchItemName.Focus();
                return true;
            }
            else if (cmbPaymentMode.SelectedIndex == -1)
            {
                objEP.SetError(cmbPaymentMode, "Select Payment Mode.");
                cmbPaymentMode.Focus();
                return true;
            }
          
            else
                return false;
        }

        static int CurrentRowIndex = 0;

        bool SearchFlag = false;
        private void btnAddToGrid_Click(object sender, EventArgs e)
        {
            if (!Validation_Item())
            {
                if (GridFlag)
                    dgvItemRow = CurrentRowIndex;
                else
                    dgvItem1.Rows.Add();

                //if(PurchaseTransactionId ==0)
                //    dgvItem1.Rows.Add();
                //else
                //   dgvItemRow= CurrentRowIndex;

                dgvItem1.Rows[dgvItemRow].Cells["clmPurchaseTransactionId"].Value = PurchaseTransactionId.ToString();
                dgvItem1.Rows[dgvItemRow].Cells["clmItemId"].Value = ItemID.ToString();
                dgvItem1.Rows[dgvItemRow].Cells["clmItemName"].Value = objRL.ItemName;
                dgvItem1.Rows[dgvItemRow].Cells["clmCompany"].Value = objRL.CompanyName;
                dgvItem1.Rows[dgvItemRow].Cells["clmBatchNumber"].Value = objRL.BatchNumber;
                dgvItem1.Rows[dgvItemRow].Cells["clmExpiryDate"].Value = dtpExpiryDate.Value.ToString(objRL.SetDateFormat);
                dgvItem1.Rows[dgvItemRow].Cells["clmHSNCode"].Value = objRL.HSNCode;
                dgvItem1.Rows[dgvItemRow].Cells["clmUOM"].Value = objRL.UOM;
                dgvItem1.Rows[dgvItemRow].Cells["clmQty"].Value = txtQuantity.Text;

                if (PurchaseTransactionId == 0)
                    dgvItem1.Rows[dgvItemRow].Cells["clmPreviousQuantity"].Value = txtQuantity.Text;

                dgvItem1.Rows[dgvItemRow].Cells["clmCost"].Value = txtCost.Text;
                dgvItem1.Rows[dgvItemRow].Cells["clmAmount"].Value = txtAmount.Text;
                dgvItem1.Rows[dgvItemRow].Cells["clmDiscountPercentage"].Value = txtDiscountPercentage.Text;
                dgvItem1.Rows[dgvItemRow].Cells["clmDiscountAmount"].Value = txtDiscountAmount.Text;
                dgvItem1.Rows[dgvItemRow].Cells["clmTaxableAmount"].Value = txtTaxableAmount.Text;


                dgvItem1.Rows[dgvItemRow].Cells["clmCGSTPer"].Value = CGST.ToString();
                dgvItem1.Rows[dgvItemRow].Cells["clmSGSTPer"].Value = SGST.ToString();
                dgvItem1.Rows[dgvItemRow].Cells["clmIGSTPer"].Value = IGST.ToString();

                dgvItem1.Rows[dgvItemRow].Cells["clmCGSTAmount"].Value = RedundancyLogics.CGSTAmount.ToString();
                dgvItem1.Rows[dgvItemRow].Cells["clmSGSTAmount"].Value = RedundancyLogics.SGSTAmount.ToString();
                dgvItem1.Rows[dgvItemRow].Cells["clmIGSTAmount"].Value = RedundancyLogics.IGSTAmount.ToString();

                dgvItem1.Rows[dgvItemRow].Cells["clmTotalGST"].Value = txtTaxTotal.Text;

                dgvItem1.Rows[dgvItemRow].Cells["clmNetAmount"].Value = objRL.RoundUp_Function(txtNetAmount.Text).ToString();

                SrNo_Add();

                if (PurchaseTransactionId != 0)
                    dgvItemRow = TempRowIndex;
                else
                    dgvItemRow++;

                Calculate_NetAmount_GridValues();
                Calculate_TotalAmount_All();
                ClearAll_Item();
                GridFlag = false;
                lblTotalItemCount.Text = "Total Item Count: " + dgvItem1.Rows.Count;
                txtSearchItemName.Focus();
            }
        }

        private void SrNo_Add()
        {
            if (dgvItem1.Rows.Count > 0)
            {
                int SrNo = 1;
                for (int i = 0; i < dgvItem1.Rows.Count; i++)
                {
                    dgvItem1.Rows[i].Cells["clmSrNo"].Value = SrNo.ToString();
                    SrNo++;
                }
            }
            lblTotalItemCount.Text = "Total Item Count: " + dgvItem1.Rows.Count.ToString();
        }

        int SaveFlag = 0;

        private void dgvItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearAll_Item();
            TempRowIndex = dgvItemRow;
            dgvItemRow = e.RowIndex;
            CurrentRowIndex = e.RowIndex;
            GridFlag = true;
            btnDeleteGridMain.Visible = true;
            rtbItemDetails.Visible = true;

            PurchaseTransactionId = Convert.ToInt32(dgvItem1.Rows[dgvItemRow].Cells["clmPurchaseTransactionId"].Value.ToString());
            ItemID = Convert.ToInt32(dgvItem1.Rows[dgvItemRow].Cells["clmItemId"].Value.ToString());
            objRL.ItemID_RL = ItemID; objRL.Get_Item_Value(rtbItemDetails);
            UOM = objRL.UOM;

            dtpExpiryDate.Value = Convert.ToDateTime(dgvItem1.Rows[dgvItemRow].Cells["clmExpiryDate"].Value.ToString());

            txtQuantity.Text = dgvItem1.Rows[dgvItemRow].Cells["clmQty"].Value.ToString();
            txtCost.Text = dgvItem1.Rows[dgvItemRow].Cells["clmCost"].Value.ToString();
            txtAmount.Text = dgvItem1.Rows[dgvItemRow].Cells["clmAmount"].Value.ToString();
            
            txtDiscountPercentage.Text = dgvItem1.Rows[dgvItemRow].Cells["clmDiscountPercentage"].Value.ToString();
            txtDiscountAmount.Text = dgvItem1.Rows[dgvItemRow].Cells["clmDiscountAmount"].Value.ToString();

            txtTaxableAmount.Text = dgvItem1.Rows[dgvItemRow].Cells["clmTaxableAmount"].Value.ToString();

            CGST = Convert.ToDouble(dgvItem1.Rows[dgvItemRow].Cells["clmCGSTPer"].Value.ToString());
            SGST = Convert.ToDouble(dgvItem1.Rows[dgvItemRow].Cells["clmSGSTPer"].Value.ToString());
            IGST = Convert.ToDouble(dgvItem1.Rows[dgvItemRow].Cells["clmIGSTPer"].Value.ToString());

            lblCGST.Text = "@ " + dgvItem1.Rows[dgvItemRow].Cells["clmCGSTPer"].Value.ToString() + " %";
            lblSGST.Text = "@ " + dgvItem1.Rows[dgvItemRow].Cells["clmSGSTPer"].Value.ToString() + " %";
            lblIGST.Text = "@ " + dgvItem1.Rows[dgvItemRow].Cells["clmIGSTPer"].Value.ToString() + " %";

            txtGSTAmount.Text = dgvItem1.Rows[dgvItemRow].Cells["clmCGSTAmount"].Value.ToString();
            txtSGSTAmount.Text = dgvItem1.Rows[dgvItemRow].Cells["clmSGSTAmount"].Value.ToString();
            txtIGSTAmount.Text = dgvItem1.Rows[dgvItemRow].Cells["clmIGSTAmount"].Value.ToString();

            txtTaxTotal.Text = dgvItem1.Rows[dgvItemRow].Cells["clmTotalGST"].Value.ToString();
            txtNetAmount.Text = dgvItem1.Rows[dgvItemRow].Cells["clmNetAmount"].Value.ToString();

            SaveFlag = Convert.ToInt32(objRL.Check_NullString(Convert.ToString(dgvItem1.Rows[dgvItemRow].Cells["clmSaleFlag"].Value)));
            ItemPurchaseQuantityId_Insert = Convert.ToString(objRL.Check_NullString(Convert.ToString(dgvItem1.Rows[dgvItemRow].Cells["clmItemPurchaseQuantityId"].Value)));
        }

        double TotalTaxAmount = 0, TotalTaxableAmount = 0;

        private void Calculate_NetAmount_GridValues()
        {
            if (dgvItem1.Rows.Count > 0)
            {
                TotalTaxAmount = 0; TotalTaxableAmount = 0; InvoiceTotal = 0;

                for (int i = 0; i < dgvItem1.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dgvItem1.Rows[i].Cells["clmTotalGST"].Value)))
                        TotalTaxAmount += Convert.ToDouble(dgvItem1.Rows[i].Cells["clmTotalGST"].Value.ToString());
                    if (!string.IsNullOrEmpty(Convert.ToString(dgvItem1.Rows[i].Cells["clmTaxableAmount"].Value)))
                        TotalTaxableAmount += objRL.Check_NullString(dgvItem1.Rows[i].Cells["clmTaxableAmount"].Value.ToString());
                }

                txtSubTotal.Text = TotalTaxableAmount.ToString();
                txtTotalGST.Text = TotalTaxAmount.ToString();
                cmbPaymentMode.Text = "Cash";
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            Item objForm = new Item();
            objForm.ShowDialog(this);
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            Supplier objForm = new Supplier();
            objForm.ShowDialog(this);
        }

        private void btnDeleteGrid_Click(object sender, EventArgs e)
        {
            if (dgvItem1.Rows.Count > 0)
            {
                DialogResult dr;
                dr = MessageBox.Show("Do you want to delete this Item.?", "Delete", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    for (int i = 0; i < dgvItem1.Rows.Count; i++)
                    {
                        dgvItem1.Rows.RemoveAt(i);
                        ClearAll_Item();
                        if (dgvItem1.Rows.Count > 0)
                            dgvItemRow = dgvItem1.Rows.Count;
                        else
                            dgvItemRow = 0;
                    }
                    SrNo_Add();
                }
            }
        }

        public void GST_Calculation()
        {
            if (!string.IsNullOrEmpty(txtTaxableAmount.Text))
            {
                txtNetAmount.Text = "";
                txtTaxTotal.Text = "";
                txtGSTAmount.Text = "";
                txtSGSTAmount.Text = "";
                txtIGSTAmount.Text = "";
                txtTaxTotal.Text = "";

                objRL.Calculate_GST(Convert.ToDouble(txtTaxableAmount.Text), CGST, SGST, IGST);
                txtGSTAmount.Text = RedundancyLogics.CGSTAmount.ToString();
                txtSGSTAmount.Text = RedundancyLogics.SGSTAmount.ToString();
                txtIGSTAmount.Text = RedundancyLogics.IGSTAmount.ToString();
                txtTaxTotal.Text = Convert.ToString(RedundancyLogics.CGSTAmount + RedundancyLogics.SGSTAmount + RedundancyLogics.IGSTAmount);
                txtNetAmount.Text = Convert.ToString(objRL.RoundUp_Function(txtTaxableAmount.Text) + objRL.RoundUp_Function(txtTaxTotal.Text)).ToString();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClearAll();
                dgvItem1.Visible = true;
                gbItem.Visible = true;
                btnDelete.Enabled = true;


                //0 P.ID,
                //1 P.PurchaseDate,
                //2 P.PurchaseNo,
                //3 P.ChallanNo,
                //4 P.BillNo,
                //5 P.SupplierId,
                //6 C.SupplierName,
                //7 C.Address,
                //8 C.MobileNumber,
                //9 C.EmailId,
                //10 C.GSTNumber,
                //11 P.SubTotal,
                //12 P.TotalGST,
                //13 P.FreightCharges,
                //14 P.LoadingAndPackingCharges,
                //15 P.InsuranceCharges,
                //16 P.OtherCharges,
                //17 P.InvoiceTotal,
                //18 P.PaymentMode,
                //19 P.BankId,
                //20 P.BankName,
                //21 P.AccountNumber,
                //22 P.TransactionDate,
                //23 P.ChequeNumber,
                //24 P.PartyBank,
                //25 P.PartyBankAccountNumber,
                //26 P.IsGST

                TableId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                dtpDate.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtPurchaseNo.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtChallanNo.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtBillNo.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                SupplierID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                SupplierName = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                lblSupplierDetails.Visible = true;
                lblSupplierDetails.Text = "Supplier Name: " + SupplierName + "\n" + "Address: " + dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString() + "\n" + "GST Number: " + dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString() + "";
                txtSubTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                txtTotalGST.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                txtFreight.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                txtLoading.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                txtInsurance.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                txtOtherCharges.Text = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
                txtInvoiceTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
                cmbPaymentMode.Text = dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString();
                ViewPendingAmount();

                if (cmbPaymentMode.Text != "CASH" && cmbPaymentMode.Text != "CREDIT")
                {
                    gbChequeDetails.Visible = true;
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[19].Value)))
                    {
                        BankId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[19].Value)));
                        cmbBankName.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[20].Value));
                        txtAccountNo.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[21].Value));
                        dtpTransactionDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[22].Value.ToString());
                    }
                    if (cmbPaymentMode.Text == "CHEQUE")
                        txtChequeNo.Text = dataGridView1.Rows[e.RowIndex].Cells[23].Value.ToString();
                    else
                        txtChequeNo.Text = "";

                    txtBankParty.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[24].Value));
                    txtAccountNoParty.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[25].Value));
                }
                else
                    gbChequeDetails.Visible = false;

                ISGST = Convert.ToInt32(objRL.Check_NullString(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[26].Value)));
                if (ISGST == 1)
                    cbGST.Checked = true;
                else
                    cbGST.Checked = false;

                if (TableId != 0)
                {
                    dgvItem1.Rows.Clear();
                    dgvItemRow = 0;
                    DataSet ds = new DataSet();
                    //objBL.Query = "select P.ID,P.ItemId,P.Quantity,P.ExpiryDate,P.UOM,P.Cost,P.CGSTAmount,P.SGSTAmount,P.IGSTAmount,P.CGSTTax,P.SGSTTax,P.IGSTTax,P.TotalTaxAmount,C.CompanyName,C.ItemName,C.HSNCode from ((PurchaseTransaction P inner join Item C on C.ID=P.ItemId) inner join Purchase DC on P.PurchaseID=DC.ID) where P.CancelTag=0 and C.CancelTag=0 and DC.CancelTag=0 and DC.ID=" + TableId + "";

                    objBL.Query = "select PT.ID,PT.ItemId,PT.Quantity,PT.ExpiryDate,PT.Cost,PT.Amount,PT.DiscountPercentage,PT.DiscountAmount,PT.TaxableAmount,PT.CGSTPer,PT.CGSTAmount,PT.SGSTPer,PT.SGSTAmount,PT.IGSTPer,PT.IGSTAmount,PT.TotalTaxAmount,PT.NetAmount,I.Category,I.CompanyName,I.ItemName,I.BatchNumber,I.HSNCode,I.Contain,I.UOM from ((PurchaseTransaction PT inner join Item I on I.ID=PT.ItemId) inner join Purchase DC on PT.PurchaseID=DC.ID) where PT.CancelTag=0 and I.CancelTag=0 and DC.CancelTag=0 and DC.ID=" + TableId + "";

                    ds = objBL.ReturnDataSet();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dgvItem1.Rows.Add();
                            dgvItem1.Rows[dgvItemRow].Cells["clmPurchaseTransactionId"].Value = ds.Tables[0].Rows[i]["ID"].ToString();
                            PurchaseTransactionId = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                            dgvItem1.Rows[dgvItemRow].Cells["clmExpiryDate"].Value = ds.Tables[0].Rows[i]["ExpiryDate"].ToString();
                            dgvItem1.Rows[dgvItemRow].Cells["clmItemId"].Value = ds.Tables[0].Rows[i]["ItemId"].ToString();
                            ItemID = Convert.ToInt32(ds.Tables[0].Rows[i]["ItemId"].ToString());

                            DateTime EDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ExpiryDate"].ToString());

                            DataSet dsQuantity = new DataSet();
                            objBL.Query = "select ID,SaleFlag from ItemPurchaseQuantity where ItemId=" + ItemID + " and ExpiryDate=#" + EDate.ToString("MM/dd/yyyy") + "#";
                            dsQuantity = objBL.ReturnDataSet();
                            if (dsQuantity.Tables[0].Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(dsQuantity.Tables[0].Rows[0]["ID"].ToString()))
                                {
                                    ItemPurchaseQuantityId_Insert = dsQuantity.Tables[0].Rows[0]["ID"].ToString();
                                    dgvItem1.Rows[dgvItemRow].Cells["clmPurchaseTransactionId"].Value = ItemPurchaseQuantityId_Insert;
                                    dgvItem1.Rows[dgvItemRow].Cells["clmSaleFlag"].Value = dsQuantity.Tables[0].Rows[0]["SaleFlag"].ToString();
                                }
                                else
                                {
                                    ItemPurchaseQuantityId_Insert = "0";
                                    dgvItem1.Rows[dgvItemRow].Cells["clmSaleFlag"].Value = "0";
                                }
                            }

                            dgvItem1.Rows[dgvItemRow].Cells["clmItemPurchaseQuantityId"].Value = ItemPurchaseQuantityId_Insert;

                            dgvItem1.Rows[dgvItemRow].Cells["clmItemName"].Value = ds.Tables[0].Rows[i]["ItemName"].ToString();
                            dgvItem1.Rows[dgvItemRow].Cells["clmCompany"].Value = ds.Tables[0].Rows[i]["CompanyName"].ToString();
                            dgvItem1.Rows[dgvItemRow].Cells["clmBatchNumber"].Value = ds.Tables[0].Rows[i]["BatchNumber"].ToString();

                            dgvItem1.Rows[dgvItemRow].Cells["clmHSNCode"].Value = ds.Tables[0].Rows[i]["HSNCode"].ToString();
                            dgvItem1.Rows[dgvItemRow].Cells["clmUOM"].Value = ds.Tables[0].Rows[i]["UOM"].ToString();

                            dgvItem1.Rows[dgvItemRow].Cells["clmQty"].Value = ds.Tables[0].Rows[i]["Quantity"].ToString();
                            dgvItem1.Rows[dgvItemRow].Cells["clmPreviousQuantity"].Value = ds.Tables[0].Rows[i]["Quantity"].ToString();

                            dgvItem1.Rows[dgvItemRow].Cells["clmCost"].Value = ds.Tables[0].Rows[i]["Cost"].ToString();
                            dgvItem1.Rows[dgvItemRow].Cells["clmAmount"].Value = ds.Tables[0].Rows[i]["Amount"].ToString();

                            dgvItem1.Rows[dgvItemRow].Cells["clmDiscountPercentage"].Value = ds.Tables[0].Rows[i]["DiscountPercentage"].ToString();
                            dgvItem1.Rows[dgvItemRow].Cells["clmDiscountAmount"].Value = ds.Tables[0].Rows[i]["DiscountAmount"].ToString();

                            dgvItem1.Rows[dgvItemRow].Cells["clmTaxableAmount"].Value = ds.Tables[0].Rows[i]["TaxableAmount"].ToString();

                            dgvItem1.Rows[dgvItemRow].Cells["clmCGSTPer"].Value = ds.Tables[0].Rows[i]["CGSTPer"].ToString();
                            dgvItem1.Rows[dgvItemRow].Cells["clmSGSTPer"].Value = ds.Tables[0].Rows[i]["SGSTPer"].ToString();
                            dgvItem1.Rows[dgvItemRow].Cells["clmIGSTPer"].Value = ds.Tables[0].Rows[i]["IGSTPer"].ToString();

                            dgvItem1.Rows[dgvItemRow].Cells["clmCGSTAmount"].Value = ds.Tables[0].Rows[i]["CGSTAmount"].ToString();
                            dgvItem1.Rows[dgvItemRow].Cells["clmSGSTAmount"].Value = ds.Tables[0].Rows[i]["SGSTAmount"].ToString();
                            dgvItem1.Rows[dgvItemRow].Cells["clmIGSTAmount"].Value = ds.Tables[0].Rows[i]["IGSTAmount"].ToString();

                            dgvItem1.Rows[dgvItemRow].Cells["clmTotalGST"].Value = ds.Tables[0].Rows[i]["TotalTaxAmount"].ToString();
                            dgvItem1.Rows[dgvItemRow].Cells["clmNetAmount"].Value = ds.Tables[0].Rows[i]["NetAmount"].ToString();

                            dgvItemRow++;
                        }
                        SrNo_Add();
                    }
                }
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void txtChallanNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtBillNo.Select();
        }

        private void txtBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSearchSupplier.Select();
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCost.Select();
        }

        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDiscountPercentage.Select();
        }

        private void cmbPaymentMode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtFreight.Select();
        }

        private void txtFreight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtLoading.Select();
        }

        private void txtLoading_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtInsurance.Select();
        }

         

        private void dtpExpiryDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQuantity.Focus();
        }

        private void txtCost_TextChanged(object sender, EventArgs e)
        {
            CalculateTaxableAmount_Item();
        }

        private void txtFreight_TextChanged(object sender, EventArgs e)
        {
            Calculate_TotalAmount_All();
        }

        private void txtFinalBillAmount_TextChanged(object sender, EventArgs e)
        {
            Calculate_TotalAmount_All();
        }

        private void txtInsurance_TextChanged(object sender, EventArgs e)
        {
            Calculate_TotalAmount_All();
        }

        private void txtOtherCharges_TextChanged(object sender, EventArgs e)
        {
            Calculate_TotalAmount_All();
        }

        private void btnDeleteGridMain_Click(object sender, EventArgs e)
        {
            if (!Validation_Item())
            {
                DialogResult dr;
                dr = MessageBox.Show("Do you want to delete this Item.?", "Delete", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    if (SaveFlag == 1)
                    {
                        MessageBox.Show("You cann't delete this item.");
                        return;
                    }
                    else
                    {
                        if (TableId != 0)
                        {
                            objBL.Query = "update PurchaseTransaction set CancelTag=1 where CancelTag=0 and ID=" + PurchaseTransactionId + "";
                            objBL.Function_ExecuteNonQuery();

                            objBL.Query = "update ItemPurchaseQuantity set PurchaseQuantity=Val(PurchaseQuantity)-Val('" + txtQuantity.Text + "') where CancelTag=0 and ID=" + ItemPurchaseQuantityId_Insert + "";
                            objBL.Function_ExecuteNonQuery();
                        }
                        else
                        {
                            dgvItem1.Rows.RemoveAt(dgvItemRow);

                            if (dgvItem1.Rows.Count > 0)
                                dgvItemRow = dgvItem1.Rows.Count;
                            else
                                dgvItemRow = 0;

                            SrNo_Add();
                            ClearAll_Item();
                        }
                    }
                }
                else
                    return;
            }
        }

        private void lblSupplierDetails_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        int ISGST = 0;
        private void cbGST_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGST.Checked)
                ISGST = 1;
            else
                ISGST = 0;
        }

        private void txtDiscountPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddToGrid.Focus();
        }

        private void txtDiscountAmount_Leave(object sender, EventArgs e)
        {
            CalculateTaxableAmount_Item();
        }

        private void Fill_BankDetails()
        {
            if (cmbBankName.SelectedIndex > -1)
            {
                objRL.GetBankDetails(Convert.ToInt32(cmbBankName.SelectedValue.ToString()));
                txtAccountNo.Text = RedundancyLogics.AccountNo;
            }
        }

        private void cmbBankName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_BankDetails();
        }
        
        private void cmbPaymentMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            objRL.Set_PaymentMode_Details(cmbPaymentMode, gbChequeDetails, lblChequeNo, txtChequeNo);
        }

      
    }
}
