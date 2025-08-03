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
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Globalization;

namespace SPApplication.Transaction
{
    public partial class Sale : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();

        static int dgvItemRow;
        int TableId = 0, TempRowIndex = 0, CustomerID = 0, ItemID = 0, RowCount = 18, SRNO = 1, ItemId_Insert = 0, SaleTransactionId = 0;
        bool GridFlag = false, DeleteFlag = false, MH_Value = false;

        string ItemString = string.Empty;
        string HSNCode = "", ItemName = "", Description = "", UOM = "", Category = "";

        double Price = 0, Qty = 0, MRP = 0, TaxableAmount = 0, DiscountPer = 0, DiscountAmount = 0, Amount = 0, CFTQty = 0;
        double CGST = 0, SGST = 0, IGST = 0, ProfitMarginPer = 0, ProfitMarginAmount = 0, Cost = 0, NetAmount = 0, TotalGST = 0;
        double TotalTaxAmount = 0, TotalTaxableAmount = 0, QtyInsert = 0, BasicRate = 0;
        double SQTY = 0, AQTY = 0, ASQty = 0, ActualQty = 0;
        double TotalTax = 0, CGSTTotal = 0, IGSTTotal = 0, SGSTTotal = 0, Discount = 0, DiscountAmt;
        double CGST_Total = 0, SGST_Total = 0, IGST_Total = 0;
        double Total = 0, OtherCharges = 0, InvoiceTotal = 0;

        string BasicRate_Insert = "", Quantity_Insert = "", Discount_Insert = "", DiscountAmount_Insert = "", TaxableAmount_Insert = "";
        string CGSTPercentage_Insert = "", CGSTAmount_Insert = "", SGSTPercentage_Insert = "", SGSTAmount_Insert = "", IGSTPercentage_Insert = "", IGSTAmount_Insert = "", CGSTPercentageRC_Insert = "";
        string CGSTAmountRC_Insert = "", SGSTPercentageRC_Insert = "", SGSTAmountRC_Insert = "", IGSTPercentageRC_Insert = "", IGSTAmountRC_Insert = "", Price_Insert = "";
        string CustomerName = "", Address = "", MobileNumber = "", GSTNumber = "";

        bool ItemFlag = false;
        bool QTYFlag = false;
        bool AmountFlag = false;

        int PrintFlag = 0;

        Excel.Application myExcelApp;
        Excel.Workbooks myExcelWorkbooks;
        Excel.Workbook myExcelWorkbook;

        public Sale()
        {
            InitializeComponent();
            objBL.Set_Design_All(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_DASHBOARD_SALE);
            SetCurrency();
            objRL.Fill_Payment_Type(cmbPaymentMode);
        }

        private void SalesNew_Load(object sender, EventArgs e)
        {
            dtpDate.CustomFormat = "dd/MM/yyyy";
            txtInvoiceNo.Text = Convert.ToString(Convert.ToInt32(objRL.ReturnMaxID("Sale")));
            FillGrid();
            dtpDate.Focus();
        }

        private void SetCurrency()
        {
            lblCRateGrid.Text = objRL.objRegInfo.CurrencySymbol;
            lblCAmountGrid.Text = objRL.objRegInfo.CurrencySymbol;
            lblCMakingChargesGrid.Text = objRL.objRegInfo.CurrencySymbol;
            lblCRateGrid.Text = objRL.objRegInfo.CurrencySymbol;
            lblCAmountGrid.Text = objRL.objRegInfo.CurrencySymbol;
            lblCNetAmount.Text = objRL.objRegInfo.CurrencySymbol;
            lblCTotal.Text = objRL.objRegInfo.CurrencySymbol;
            lblCDiscount.Text = objRL.objRegInfo.CurrencySymbol;
            lblCOtherCharges.Text = objRL.objRegInfo.CurrencySymbol;
            lblCNetAmount.Text = objRL.objRegInfo.CurrencySymbol;
        }

        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            ClearAll();
            if (txtSearchCustomer.Text != "")
                Fill_Customer_ListBox();
            else
                lbCustomer.Visible = false;
        }

        private void ClearAll()
        {
            TableId = 0;
            objEP.Clear();
            dgvItemRow = 0;
            dgvItem.Rows.Clear();
            Clear_Item();
            dtpDate.Value = DateTime.Now.Date;

            CustomerID = 0;

            txtTotal.Text = "";
            txtDiscount.Text = "";
            txtOtherCharges.Text = "";
            txtNetAmount.Text = "";
            txtCGSTAmount.Text = "";

            txtInvoiceTotal.Text = "";
            cmbPaymentMode.SelectedIndex = -1;
             
            PrintFlag = 0;
            lblCustomerDetails.Text = "";
            dgvItem.Visible = false;
            gbItem.Visible = false;
            txtSearchCustomer.Focus();
        }

        private void Clear_Item()
        {
            ItemID = 0;
            txtItemNameGrid.Text = "";
            txtWeightGrid.Text = "";
            txtRateGrid.Text = "";
            txtAmountGrid.Text = "";
            txtMakingChargesGrid.Text = "";
            txtNetAmountGrid.Text = "";
        }

        private void Fill_Customer_ListBox()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,CustomerCode,CustomerName,Address,MobileNumber,EmailId,GSTNumber from Customer where CancelTag=0 and CustomerName like '%" + txtSearchCustomer.Text + "%' or CustomerCode like '%" + txtSearchCustomer.Text + "%' order by CustomerName desc";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lbCustomer.Visible = true;
                lbCustomer.DataSource = ds.Tables[0];
                lbCustomer.DisplayMember = "CustomerName";
                lbCustomer.ValueMember = "ID";
            }
        }

        private void Clear_Prices()
        {
            Price = 0; Qty = 0; MRP = 0; TaxableAmount = 0; DiscountPer = 0; DiscountAmount = 0; Amount = 0; CFTQty = 0;
        }


        double TaxablePrice = 0;

        double RateGrid = 0, WeightGrid = 0, MakingChargesGrid = 0;
        double AmountGrid = 0, NetAmountGrid = 0;

        private void Calculate_Amount()
        {
            Qty = 0; Price = 0; DiscountPer = 0; DiscountAmount = 0;

            double.TryParse(txtWeightGrid.Text, out WeightGrid);
            double.TryParse(txtRateGrid.Text, out RateGrid);
            double.TryParse(txtMakingChargesGrid.Text, out MakingChargesGrid);

            AmountGrid = WeightGrid * RateGrid/10;
            AmountGrid = objRL.RoundUp_Function(AmountGrid.ToString());
            txtAmountGrid.Text = AmountGrid.ToString();
            NetAmountGrid = AmountGrid + MakingChargesGrid;
            NetAmountGrid = objRL.RoundUp_Function(NetAmountGrid.ToString());
            txtNetAmountGrid.Text = NetAmountGrid.ToString();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            Calculate_Amount();
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            Calculate_Amount();
        }

        private void txtDiscountPer_TextChanged(object sender, EventArgs e)
        {
            Calculate_Amount();
        }

        private bool Validation_Item()
        {
            objEP.Clear();
            if (txtItemNameGrid.Text == "")
            {
                objEP.SetError(txtItemNameGrid, "Enter Item Name");
                txtItemNameGrid.Focus();
                return true;
            }
            else if (txtWeightGrid.Text == "")
            {
                objEP.SetError(txtWeightGrid, "Enter Weight");
                txtWeightGrid.Focus();
                return true;
            }
            else if (txtRateGrid.Text == "")
            {
                objEP.SetError(txtRateGrid, "Enter Rate");
                txtRateGrid.Focus();
                return true;
            }
            else if (txtAmountGrid.Text == "")
            {
                objEP.SetError(txtAmountGrid, "Enter Amount");
                txtAmountGrid.Focus();
                return true;
            }
            else if (txtNetAmountGrid.Text == "")
            {
                objEP.SetError(txtNetAmountGrid, "Enter Net Amount");
                txtNetAmountGrid.Focus();
                return true;
            }
            else
                return false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Validation_Item())
            {
                if (!GridFlag)
                    dgvItem.Rows.Add();

                dgvItem.Rows[dgvItemRow].Cells["clmSaleTransactionId"].Value = SaleTransactionId.ToString();
                dgvItem.Rows[dgvItemRow].Cells["clmItemName"].Value = txtItemNameGrid.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmWeight"].Value = txtWeightGrid.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmRate"].Value = txtRateGrid.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmAmount"].Value = objRL.RoundUp_Function(txtAmountGrid.Text).ToString();
                dgvItem.Rows[dgvItemRow].Cells["clmMakingCharges"].Value = objRL.RoundUp_Function(txtMakingChargesGrid.Text).ToString();
                dgvItem.Rows[dgvItemRow].Cells["clmNetAmount"].Value = objRL.RoundUp_Function(txtNetAmountGrid.Text).ToString();

                SrNo_Add();

                if (GridFlag)
                    dgvItemRow = TempRowIndex;
                else
                    dgvItemRow++;

                Calculate_NetAmount_GridValues();
                Calculate_InvoiceTotal();
                ClearAll_Item();
                txtItemNameGrid.Focus();
            }
        }

        private void ClearAll_Item()
        {
            btnDeleteGridMain.Visible = true;
            objEP.Clear();
            txtItemNameGrid.Text = "";
            txtWeightGrid.Text = "";
            txtRateGrid.Text = "";
            txtAmountGrid.Text = "";
            txtMakingChargesGrid.Text = "";
            txtNetAmountGrid.Text = "";

            SaleTransactionId = 0;
            TempRowIndex = 0;
            GridFlag = false;
            
            if (dgvItem.Rows.Count > 0)
                dgvItemRow = dgvItem.Rows.Count;
        }

        private void Calculate_NetAmount_GridValues()
        {
            Total = 0;
            if (dgvItem.Rows.Count > 0)
            {
                for (int i = 0; i < dgvItem.Rows.Count; i++)
                {
                    //if (!string.IsNullOrEmpty(Convert.ToString(dgvItem.Rows[i].Cells["clmTotalGST"].Value)))
                    //    TotalTaxAmount += Convert.ToDouble(dgvItem.Rows[i].Cells["clmTotalGST"].Value.ToString());

                    if (!string.IsNullOrEmpty(Convert.ToString(dgvItem.Rows[i].Cells["clmNetAmount"].Value)))
                        Total += objRL.Check_NullString(dgvItem.Rows[i].Cells["clmNetAmount"].Value.ToString());
                }
                txtTotal.Text = Total.ToString();
                //txtTotalGST.Text = TotalTaxAmount.ToString();
                //cmbPaymentMode.Text = "Cash";
            }
        }

        double CGSTAmount = 0, SGSTAmount = 0, IGSTAmount = 0, CGSTPer = 0, SGSTPer = 0, IGSTPer = 0;

        private void Calculate_InvoiceTotal()
        {
            Total = 0; OtherCharges = 0; InvoiceTotal = 0;
            CGSTAmount = 0; SGSTAmount = 0; IGSTAmount = 0;  

            double.TryParse(txtTotal.Text, out Total);
            double.TryParse(txtDiscount.Text, out Discount);
            double.TryParse(txtOtherCharges.Text, out OtherCharges);
            NetAmount = Total + OtherCharges - Discount;
            NetAmount = objRL.RoundUp_Function(NetAmount.ToString());
            txtNetAmount.Text = NetAmount.ToString();

            lblCGST.Text = CGSTPer.ToString() + " % ";
            lblSGST.Text = SGSTPer.ToString() + " % ";
            lblIGST.Text = IGSTPer.ToString() + " % ";

            CGSTAmount = NetAmount * CGSTPer / 100;
            CGSTAmount = objRL.RoundUp_Function(CGSTAmount.ToString());
            txtCGSTAmount.Text = CGSTAmount.ToString();

            SGSTAmount = NetAmount * SGSTPer / 100;
            SGSTAmount = objRL.RoundUp_Function(SGSTAmount.ToString());
            txtSGSTAmount.Text = SGSTAmount.ToString();

            IGSTAmount = NetAmount * IGSTPer / 100;
            IGSTAmount = objRL.RoundUp_Function(IGSTAmount.ToString());
            txtIGSTAmount.Text = IGSTAmount.ToString();


            InvoiceTotal = NetAmount + CGSTAmount + SGSTAmount + IGSTAmount;
            InvoiceTotal = objRL.RoundUp_Function(InvoiceTotal.ToString());
            txtInvoiceTotal.Text = InvoiceTotal.ToString();
        }

        private void Calculate_Total()
        {
            Total = 0;
            if (dgvItem.Rows.Count > 0)
            {
                for (int i = 0; i < dgvItem.Rows.Count; i++)
                {
                    Total += Convert.ToDouble(dgvItem.Rows[i].Cells["clmNetAmount"].Value);
                }
                Total = objRL.RoundUp_Function(Total.ToString());
                txtTotal.Text = Total.ToString();
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
        }

        private void lbCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Fill_Customer_Details();
        }

        private void lbCustomer_Leave(object sender, EventArgs e)
        {
            if (lbCustomer.Items.Count > 0)
                Fill_Customer_Details();
        }

        private void lbCustomer_Click(object sender, EventArgs e)
        {
            if (lbCustomer.Items.Count > 0)
            {
                CustomerID = Convert.ToInt32(lbCustomer.SelectedValue);
                Fill_Customer_Details();
            }
        }

        private void Fill_Customer_Details()
        {
            if (CustomerID !=0)
            {
                DataSet ds = new DataSet();
                objBL.Query = "select ID,CustomerCode,CustomerName,Address,MobileNumber,EmailId,GSTNumber from Customer where CancelTag=0 and  ID=" + CustomerID + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbCustomer.Visible = false;

                    CustomerID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());

                    CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    Address = ds.Tables[0].Rows[0]["Address"].ToString();

                    MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                    GSTNumber = ds.Tables[0].Rows[0]["GSTNumber"].ToString();

                    lblCustomerDetails.Text = "Customer Name: " + CustomerName + "\n" +
                                              "Delivery Address: " + Address + "\n" +
                                              "Mobile Number: " + MobileNumber + "\n" +
                                              "GSTIN Number: " + GSTNumber;

                    ViewPendingAmount();
                    dgvItem.Visible = true;
                    gbItem.Visible = true;
                    txtItemNameGrid.Focus();
                }
            }
        }

        private void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            //objBL.Query = "select S.CustomerId,S.ID as [Invoice No],S.InvoiceDate,C.CustomerName,S.SubTotal,S.TotalGST,S.Total,S.InvoiceTotal,S.PaymentMode,S.IsGST,S.BillType from Sale S inner join Customer C on C.ID=S.CustomerId where S.CancelTag=0 and C.CancelTag=0 and S.InvoiceDate=#" + dtpInvoiceDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            //objBL.Query = "select S.CustomerId,S.ID as [Invoice No],S.InvoiceDate,C.CustomerName,S.SubTotal,S.TotalGST,S.Total,S.InvoiceTotal,S.PaymentMode,S.IsGST,S.BillType from Sale S inner join Customer C on C.ID=S.CustomerId where S.CancelTag=0 and C.CancelTag=0 and S.InvoiceDate=#" + dtpInvoiceDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            objBL.Query = "select S.CustomerId,S.ID as [Invoice No],S.InvoiceDate as [Date],C.CustomerName,S.Total,S.Discount,S.OtherCharges,S.NetAmount,S.IsGST,S.CGSTAmount,S.SGSTAmount,S.IGSTAmount,S.InvoiceTotal,S.PaymentMode,S.UserId from Sale S inner join Customer C on C.ID=S.CustomerId where S.CancelTag=0 and C.CancelTag=0 and S.InvoiceDate=#" + dtpInvoiceDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 S.CustomerId,
                //1 S.ID as [Invoice No],
                //2 S.InvoiceDate,
                //3 C.CustomerName,
                //4 S.Total,
                //5 S.Discount,
                //6 S.OtherCharges,
                //7 S.NetAmount,
                //8 S.IsGST,
                //9 S.CGSTAmount,
                //10 S.SGSTAmount,
                //11 S.IGSTAmount,
                //12 S.InvoiceTotal,
                //13 S.PaymentMode,
                //14 S.UserId

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[14].Visible = false;
                dataGridView1.Columns[1].Width = 120;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 200;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[6].Width = 100;
                dataGridView1.Columns[7].Width = 100;
                dataGridView1.Columns[8].Width = 100;
                dataGridView1.Columns[9].Width = 100;
                dataGridView1.Columns[10].Width = 100;
                dataGridView1.Columns[11].Width = 100;
                dataGridView1.Columns[12].Width = 100;
                dataGridView1.Columns[13].Width = 100;

                this.dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            Customer objForm = new Customer();
            objForm.ShowDialog(this);
        }

        double GSTAmount = 0, GSTInclusivePrice = 0, GSTRatePercentage = 0, OriginalCost = 0;

        private String CreateClientToken(String sItem1, String sItem2)
        {
            sItem1 = "pmtst";
            sItem2 = "we are testing rpms 10";

            int L2 = sItem2.Length;

            String sClientToken = "";
            try
            {
                int[] iAsciiDigit = new int[100];

                sItem1 = sItem1.Trim();
                sItem2 = sItem2.Trim();

                //int iCharsetStart = 48;
                //int iCharsetEnd = 122;

                int iCharsetStart = 32;
                int iCharsetEnd = 126;

                //sItem.Length = 5
                //sItem.Length = 21

                //25= 26 - 1;

                int iSpacer = 96 / ((sItem1.Length + sItem2.Length) - 1);
                Random Rnd = new Random();


                //String sItems = "pmtstwe are testing rmps10";
                String sItems = sItem1 + sItem2;
                int L = sItems.Length;

                int iItemDigit = 0;
                for (int iAscii = 0; iAscii <= 96; iAscii++)
                {
                    if (iAscii % iSpacer == 0)
                    {
                        string A1 = sItems.Substring(iItemDigit);
                        L = A1.Length;

                        if (iItemDigit < L)
                        {
                            char cDigit = sItems.Substring(iItemDigit).ToCharArray()[0];


                            iAsciiDigit[iAscii] = Convert.ToInt32(((int)cDigit).ToString());
                            iItemDigit++;
                        }
                    }
                    else
                    {
                        iAsciiDigit[iAscii] = iCharsetStart + Rnd.Next(iCharsetEnd - iCharsetStart);
                    }
                }

                iAsciiDigit[97] = iCharsetStart + sItem1.Length;
                iAsciiDigit[98] = iCharsetStart + sItem2.Length;

                iAsciiDigit[99] = iCharsetStart + Rnd.Next(iCharsetEnd - iCharsetStart);

                StringBuilder sbClientTokenBldr = new StringBuilder();
                for (int iAscii = 0; iAscii <= 98; iAscii++)
                {
                    iAsciiDigit[iAscii] += iAsciiDigit[99] - iCharsetStart;

                    if(iAsciiDigit[iAscii] >iCharsetStart)
                        iAsciiDigit[iAscii] =   iCharsetStart + (iAsciiDigit[iAscii] -iCharsetEnd) -1;

                    sbClientTokenBldr.Append(new string((Char)iAsciiDigit[iAscii], 1));
                }

                sbClientTokenBldr.Append(new string((Char)iAsciiDigit[99], 1));
                sbClientTokenBldr.Replace("<", "!");
                sClientToken = sbClientTokenBldr.ToString();

            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.ToString());
            }
            return sClientToken;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Sonali Function Code
           //CreateClientToken("A", "B");
             

            //GST Amount = GST Inclusive Price - (GST Inclusive Price * (100/(100 + GST Rate Percentage)))
            //Original Cost = GST Inclusive Price - GST Amount = (GST Inclusive Price * (100/(100 + GST Rate Percentage)))

            //GSTInclusivePrice = 4000; GSTRatePercentage = 28;
            //GSTAmount = GSTInclusivePrice - (GSTInclusivePrice * (100 / (100 + GSTRatePercentage)));

            //GSTInclusivePrice = 4000; GSTRatePercentage = 28;
            //OriginalCost = GSTInclusivePrice - GSTAmount= (GSTInclusivePrice * (100 / (100 + GSTRatePercentage)));

            //OriginalCost = GSTInclusivePrice-  GSTAmount
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDB();
        }

        private bool Validation()
        {
            bool ReturnBill = false;
            objEP.Clear();

            if (!ReturnBill)
            {
                if (CustomerID == 0)
                {
                    objEP.SetError(txtSearchCustomer, "Enter Customer");
                    ReturnBill = true;
                }
                else
                    ReturnBill = false;
            }
            if (!ReturnBill)
            {
                if (dgvItem.Rows.Count == 0)
                {
                    objEP.SetError(dgvItem, "Enter Item");
                    ReturnBill = true;
                }
                else
                    ReturnBill = false;
            }
            if (!ReturnBill)
            {
                if (cmbPaymentMode.SelectedIndex == -1)
                {
                    objEP.SetError(cmbPaymentMode, "Select Payment Type");
                    ReturnBill = true;
                }
                else
                    ReturnBill = false;
            }
            return ReturnBill;
        }

        protected void SaveDB()
        {
            if (!Validation())
            {
                if (cbGST.Checked)
                    ISGST = 1;
                else
                    ISGST = 0;

                if (TableId != 0)
                {
                    if (!DeleteFlag)
                        objBL.Query = "update Sale set  InvoiceDate='" + dtpDate.Value.ToShortDateString() + "',CustomerId=" + CustomerID + ",Total='" + txtTotal.Text + "',Discount='" + txtDiscount.Text + "',OtherCharges='" + txtOtherCharges.Text + "',NetAmount='" + txtNetAmount.Text + "',IsGST=" + ISGST + ",CGSTAmount='" + txtCGSTAmount.Text + "',SGSTAmount='" + txtSGSTAmount.Text + "',IGSTAmount='" + txtIGSTAmount.Text + "',InvoiceTotal='" + txtInvoiceTotal.Text + "',PaymentMode='" + cmbPaymentMode.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableId + " and CancelTag=0 ";
                    else
                        objBL.Query = "update Sale set CancelTag=1 where ID=" + TableId + " and CancelTag=0 ";
                }
                else
                    objBL.Query = "insert into Sale(InvoiceDate,CustomerId,Total,Discount,OtherCharges,NetAmount,IsGST,CGSTAmount,SGSTAmount,IGSTAmount,InvoiceTotal,PaymentMode,UserId) values('" + dtpDate.Value.ToShortDateString() + "'," + CustomerID + ",'" + txtTotal.Text + "','" + txtDiscount.Text + "','" + txtOtherCharges.Text + "','" + txtNetAmount.Text + "'," + ISGST + ",'" + txtCGSTAmount.Text + "','" + txtSGSTAmount.Text + "','" + txtIGSTAmount.Text + "','" + txtInvoiceTotal.Text + "','" + cmbPaymentMode.Text + "'," + BusinessLayer.UserId_Static + ") ";

                objBL.Function_ExecuteNonQuery();

                if (TableId == 0)
                    TableId = objRL.Return_Transaction_ID("Sale");
                else
                {
                    if (DeleteFlag)
                    {
                        objBL.Query = "delete from SaleTransaction where SaleId=" + TableId + "";
                        objBL.Function_ExecuteNonQuery();
                    }
                }

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

                                if (!DeleteFlag)
                                    PendingAmount_Insert = PendingAmount + PendingAmount_Insert;
                                else
                                    PendingAmount_Insert = PendingAmount - PendingAmount_Insert;

                                if (objRL.PendingFlag)
                                    objBL.Query = "update CustomerPendingAmount set PendingAmount='" + PendingAmount_Insert + "' where CancelTag=0 and CustomerId=" + CustomerID + "";
                                else
                                    objBL.Query = "insert into CustomerPendingAmount(CustomerId,PendingAmount) values(" + CustomerID + ",'" + PendingAmount_Insert + "')";

                                objBL.Function_ExecuteNonQuery();
                            }
                        }
                    }

                    if (!DeleteFlag)
                        objRL.ShowMessage(7, 1);
                    else
                        objRL.ShowMessage(9, 1);

                    FillGrid();
                    ExcelReport();
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
            PendingAmount = objRL.Get_Pending_Details("CustomerPendingAmount", CustomerID);
            txtPendingAmount.Text = PendingAmount.ToString();
        }

        private void ClearItemList()
        {
            SaleTransactionId = 0; ItemName_Insert = ""; Weight_Insert = ""; Rate_Insert = ""; Amount_Insert = ""; MakingCharges_Insert = ""; NetAmount_Insert = "";
        }

        string ItemName_Insert = "", Weight_Insert = "", Rate_Insert = "", Amount_Insert = "", MakingCharges_Insert = "", NetAmount_Insert = "";

        protected void SaveItemList()
        {
            for (int i = 0; i < dgvItem.Rows.Count; i++)
            {
                ClearItemList();
                SaleTransactionId = Convert.ToInt32(dgvItem.Rows[i].Cells["clmSaleTransactionId"].Value.ToString());
                ItemName_Insert = dgvItem.Rows[i].Cells["clmItemName"].Value.ToString();
                Weight_Insert = dgvItem.Rows[i].Cells["clmWeight"].Value.ToString();
                Rate_Insert = dgvItem.Rows[i].Cells["clmRate"].Value.ToString();
                Amount_Insert = dgvItem.Rows[i].Cells["clmAmount"].Value.ToString();
                MakingCharges_Insert = dgvItem.Rows[i].Cells["clmMakingCharges"].Value.ToString();
                NetAmount_Insert = dgvItem.Rows[i].Cells["clmNetAmount"].Value.ToString();

                if (!DeleteFlag)
                {
                    if (SaleTransactionId == 0)
                        objBL.Query = "insert into SaleTransaction(SaleID,ItemName,Weight,Rate,Amount,MakingCharges,NetAmount,UserId) values(" + TableId + ",'" + ItemName_Insert + "','" + Weight_Insert + "','" + Rate_Insert + "','" + Amount_Insert + "','" + MakingCharges_Insert + "', '" + NetAmount_Insert + "'," + BusinessLayer.UserId_Static + ")";
                    else
                        objBL.Query = "Update SaleTransaction set ItemName='" + ItemName_Insert + "',Weight='" + Weight_Insert + "',Rate='" + Rate_Insert + "',Amount='" + Amount_Insert + "',MakingCharges='" + MakingCharges_Insert + "',NetAmount='" + NetAmount_Insert + "',UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + SaleTransactionId + "";

                    objBL.Function_ExecuteNonQuery();
                }
                else
                {
                    objBL.Query = "Update SaleTransaction set CancelTag=1,UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + SaleTransactionId + "";
                    objBL.Function_ExecuteNonQuery();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRateGrid.Focus();
        }

        private void txtDiscountPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAdd.Focus();
        }

        private void cmbPaymentType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //0 S.CustomerId,
            //1 S.ID as [Invoice No],
            //2 S.InvoiceDate,
            //3 C.CustomerName,
            //4 S.Total,
            //5 S.Discount,
            //6 S.OtherCharges,
            //7 S.NetAmount,
            //8 S.IsGST,
            //9 S.CGSTAmount,
            //10 S.SGSTAmount,
            //11 S.IGSTAmount,
            //12 S.InvoiceTotal,
            //13 S.PaymentMode,
            //14 S.UserId

            ClearAll();
            btnDelete.Enabled = true;
            dgvItem.Visible = true;

            CustomerID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            TableId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            Fill_Customer_Details();

            txtTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtDiscount.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtOtherCharges.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtNetAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

            if (dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString() == "1")
            {
                cbGST.Checked = true;
                txtCGSTAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                txtSGSTAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                txtIGSTAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            }
            else
            {
                VisibleFalse();
                cbGST.Checked = false;
            }

            txtInvoiceTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
            cmbPaymentMode.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            Fill_Item_List();
        }

        protected void Fill_Item_List()
        {
            if (TableId != 0)
            {
                ClearItemList();
                dgvItem.Rows.Clear();
                DataSet ds = new DataSet();
                objBL.Query = "select ID,SaleID,ItemName,Weight,Rate,Amount,MakingCharges,NetAmount,UserId from SaleTransaction where  CancelTag=0 and  SaleId=" + TableId + "";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gbItem.Visible = true;
                    dgvItem.Visible = true;
                    dgvItemRow = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvItem.Rows.Add();
                        dgvItem.Rows[dgvItemRow].Cells["clmSaleTransactionId"].Value = ds.Tables[0].Rows[i]["ID"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmItemName"].Value = ds.Tables[0].Rows[i]["ItemName"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmWeight"].Value = ds.Tables[0].Rows[i]["Weight"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmRate"].Value = ds.Tables[0].Rows[i]["Rate"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmAmount"].Value = ds.Tables[0].Rows[i]["Amount"].ToString();

                        dgvItem.Rows[dgvItemRow].Cells["clmMakingCharges"].Value = ds.Tables[0].Rows[i]["MakingCharges"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmNetAmount"].Value = ds.Tables[0].Rows[i]["NetAmount"].ToString();

                        dgvItemRow++;
                    }

                    SrNo_Add();
                    Calculate_NetAmount_GridValues();
                    Calculate_InvoiceTotal();

                    ClearAll_Item();
                    txtItemNameGrid.Focus();
                }
            }
        }

        private void ExcelReport()
        {
            DataSet dsDeliveryChallanItem = new DataSet();
            //objBL.Query = "select BI.ID,BI.BillId,BI.ItemId,I.ItemName,I.ItemCode,I.HSNCode,BI.Rate,BI.Quantity,BI.Total,BI.Discount,BI.DiscountAmount,BI.TaxableValue,BI.CGSTPercentage,BI.CGSTAmount,BI.SGSTPercentage,BI.SGSTAmount,BI.IGSTPercentage,BI.IGSTAmount,BI.CGSTPercentageRC,BI.CGSTAmountRC,BI.SGSTPercentageRC,BI.SGSTAmountRC,BI.IGSTPercentageRC,BI.IGSTAmountRC,BI.NetAmount from BillItem BI inner join Item I on I.ID=BI.ItemId where BI.CancelTag=0 and I.CancelTag=0 and BI.BillId=" + TableId + "";
            //objBL.Query = "select ID,SaleID,ItemId,I.ItemName,I.UOM,I.HSNCode,TaxablePrice,Price,Quantity,Amount,DiscountPercentage,DiscountAmount,TaxableAmount,CGSTPer,CGSTAmount,SGSTPer,SGSTAmount,IGSTPer,IGSTAmount,TotalTaxAmount,NetAmount from SaleTransaction ST inner join Item I on I.ID=ItemId where CancelTag=0 and SaleId=" + TableId + "";

            objBL.Query = "select ID,SaleID,ItemName,Weight,Rate,Amount,MakingCharges,NetAmount from SaleTransaction where CancelTag=0 and SaleID=" + TableId + "";
            dsDeliveryChallanItem = objBL.ReturnDataSet();

            if (dsDeliveryChallanItem.Tables[0].Rows.Count > 0)
            {
                objRL.FillCompanyData();
                objRL.GetCustomerRecords(CustomerID);
                //objRL.GetBill(TableId);

                DialogResult dr;
                dr = MessageBox.Show("Do you want to create this report.?", "Report View", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    object misValue = System.Reflection.Missing.Value;
                    myExcelApp = new Excel.Application();
                    myExcelWorkbooks = myExcelApp.Workbooks;

                    objRL.ClearExcelPath();
                    objRL.isPDF = true;

                    objRL.Form_ExcelFileName = "Bill.xlsx";

                    objRL.Form_DestinationReportFilePath = "Bill\\" + txtInvoiceNo.Text + "\\";


                    objRL.Path_Comman();

                    myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                    Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                    myExcelWorksheet.get_Range("C6", misValue).Formula = txtInvoiceNo.Text;
                    myExcelWorksheet.get_Range("I6", misValue).Formula = dtpDate.Value.ToString(objRL.SetDateFormat_ForReport);

                    myExcelWorksheet.get_Range("C7", misValue).Formula = objRL.CustomerName_Customer;
                    myExcelWorksheet.get_Range("C8", misValue).Formula = objRL.DeliveryAddress_Customer;
                    myExcelWorksheet.get_Range("C9", misValue).Formula = objRL.ContactNumber_Customer;
                    myExcelWorksheet.get_Range("C10", misValue).Formula = objRL.GST_Customer;

                    if (cbGST.Checked)
                    {
                        myExcelWorksheet.get_Range("A5", misValue).Formula = "GSTIN/ UIN";
                        myExcelWorksheet.get_Range("C5", misValue).Formula = objRL.CI_GSTIN.ToString();

                        myExcelWorksheet.get_Range("E5", misValue).Formula = "STATE-MAHARASHTRA";
                        myExcelWorksheet.get_Range("H5", misValue).Formula = "CODE-27";
                    }

                    RowCount = 12;

                    string CellDisplay1 = "";
                    int CellCheckCount = 0;
                    CellCheckCount = dsDeliveryChallanItem.Tables[0].Rows.Count;

                    CGST_Total = 0; SGST_Total = 0; IGST_Total = 0;
                    SRNO = 1;
                    string ConcatItem = string.Empty;
                    for (int i = 0; i < dsDeliveryChallanItem.Tables[0].Rows.Count; i++)
                    {
                        
                        ConcatItem = dsDeliveryChallanItem.Tables[0].Rows[i]["ItemName"].ToString();
                       
                        FlagBill = false;
                        AlignSet = 1;
                        Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SRNO.ToString());
                        AlignSet = 0;
                        Fill_Merge_Cell("B", "D", misValue, myExcelWorksheet, Convert.ToString(ConcatItem));
                        FlagBill = true;
                        AlignSet = 2;
                        Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["Weight"].ToString()));
                        Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["Rate"].ToString()));
                        Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["Amount"].ToString()));
                        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["MakingCharges"].ToString()));
                        Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["NetAmount"].ToString()));
                        RowCount++;
                        SRNO++;
                    }

                    FlagBill = false;
                    int StoreCount = 0;
                    StoreCount = RowCount;

                    if (cbGST.Checked)
                    {
                        if (CGSTPer > 0)
                        {
                            FlagBill = false; AlignSet = 0;
                            Fill_Merge_Cell("A", "H", misValue, myExcelWorksheet, "CGST " + CGSTPer.ToString() + "%");
                            FlagBill = true; AlignSet = 2;
                            Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, Convert.ToString(txtCGSTAmount.Text));
                            RowCount++;
                        }

                        if (SGSTPer > 0)
                        {
                            FlagBill = false; AlignSet = 0;
                            Fill_Merge_Cell("A", "H", misValue, myExcelWorksheet, "SGST " + CGSTPer.ToString() + "%");
                            FlagBill = true; AlignSet = 2;
                            Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, Convert.ToString(txtSGSTAmount.Text));
                            RowCount++;
                        }

                        if (IGSTPer > 0)
                        {
                            FlagBill = false; AlignSet = 0;
                            Fill_Merge_Cell("A", "H", misValue, myExcelWorksheet, "IGST " + CGSTPer.ToString() + "%");
                            FlagBill = true; AlignSet = 2;
                            Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, Convert.ToString(txtCGSTAmount.Text));
                            RowCount++;
                        }
                    }

                    FlagBill = false; AlignSet = 0;
                    Fill_Merge_Cell("A", "H", misValue, myExcelWorksheet, "Total Amount (in numbers)");
                    FlagBill = true; AlignSet = 2;
                    Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, Convert.ToString(txtInvoiceTotal.Text));
                    RowCount++;


                    if (txtInvoiceTotal.Text != "")
                    {
                        AlignSet = 0;
                        Fill_Merge_Cell("A", "I", misValue, myExcelWorksheet, "Total Amount (in words): " + objRL.words(Convert.ToInt32(txtInvoiceTotal.Text)));
                    }

                    RowCount++;
                    AlignSet = 0; MH_Value = true;
                    Fill_Merge_Cell("A", "E", misValue, myExcelWorksheet, "Declaration \n We declare that this invoice shows the actual price of goods described and that all particulars are true and correct.");
                    //Fill_Merge_Cell("A", "E", misValue, myExcelWorksheet, );
                    MH_Value = false; AlignSet = 1;
                    Fill_Merge_Cell("F", "I", misValue, myExcelWorksheet, "For Y B JEWELLERS \n \n \n Authorised Signatory");
                    //Fill_Merge_Cell("F", "K", misValue, myExcelWorksheet,);

                    myExcelWorkbook.Save();

                    string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
                    const int xlQualityStandard = 0;
                    myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);

                    myExcelWorkbook.Close(true, misValue, misValue);
                    myExcelApp.Quit();

                    //objRL.ShowMessage(22, 1);
                    MessageBox.Show("Report Generated Successfully");

                    DialogResult dr1;
                    dr1 = MessageBox.Show("Do you want to view this report.?", "Report View", MessageBoxButtons.YesNo);
                    if (dr1 == DialogResult.Yes)
                        System.Diagnostics.Process.Start(PDFReport);
                    objRL.DeleteExcelFile();
                }
            }
        }

        private void ExcelReport_Other()
        {
            DataSet dsDeliveryChallanItem = new DataSet();
            //objBL.Query = "select BI.ID,BI.BillId,BI.ItemId,I.ItemName,I.ItemCode,I.HSNCode,BI.Rate,BI.Quantity,BI.Total,BI.Discount,BI.DiscountAmount,BI.TaxableValue,BI.CGSTPercentage,BI.CGSTAmount,BI.SGSTPercentage,BI.SGSTAmount,BI.IGSTPercentage,BI.IGSTAmount,BI.CGSTPercentageRC,BI.CGSTAmountRC,BI.SGSTPercentageRC,BI.SGSTAmountRC,BI.IGSTPercentageRC,BI.IGSTAmountRC,BI.NetAmount from BillItem BI inner join Item I on I.ID=BI.ItemId where BI.CancelTag=0 and I.CancelTag=0 and BI.BillId=" + TableId + "";

            objBL.Query = "select ID,SaleID,ItemId,I.ItemName,I.UOM,I.HSNCode,TaxablePrice,Price,Quantity,Amount,DiscountPercentage,DiscountAmount,TaxableAmount,CGSTPer,CGSTAmount,SGSTPer,SGSTAmount,IGSTPer,IGSTAmount,TotalTaxAmount,NetAmount from SaleTransaction ST inner join Item I on I.ID=ItemId where CancelTag=0 and SaleId=" + TableId + "";

            dsDeliveryChallanItem = objBL.ReturnDataSet();

            if (dsDeliveryChallanItem.Tables[0].Rows.Count > 0)
            {
                objRL.FillCompanyData();
                objRL.GetCustomerRecords(CustomerID);
                //objRL.GetBill(TableId);

                DialogResult dr;
                dr = MessageBox.Show("Do you want to create this report.?", "Report View", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    object misValue = System.Reflection.Missing.Value;
                    myExcelApp = new Excel.Application();
                    myExcelWorkbooks = myExcelApp.Workbooks;

                    objRL.ClearExcelPath();
                    objRL.isPDF = true;

                    objRL.Form_ExcelFileName = "Bill_Other.xlsx";

                    objRL.Form_DestinationReportFilePath = "Bill_Alingment\\" + txtInvoiceNo.Text + "\\";


                    objRL.Path_Comman();

                    myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                    Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                    //myExcelWorksheet.get_Range("C4", misValue).Formula = objRL.CI_GSTIN;
                    //myExcelWorksheet.get_Range("C3", misValue).Formula = objRL.CI_CompanyName;
                    //myExcelWorksheet.get_Range("C4", misValue).Formula = objRL.CI_Address;
                    //myExcelWorksheet.get_Range("C6", misValue).Formula = objRL.CI_ContactNo + "    Email Id: " + objRL.CI_EmailId;
                    myExcelWorksheet.get_Range("C5", misValue).Formula = txtInvoiceNo.Text;
                    myExcelWorksheet.get_Range("J5", misValue).Formula = dtpDate.Value.ToString(objRL.SetDateFormat_ForReport);

                    myExcelWorksheet.get_Range("C6", misValue).Formula = objRL.CustomerName_Customer;
                    myExcelWorksheet.get_Range("C7", misValue).Formula = objRL.DeliveryAddress_Customer;
                    myExcelWorksheet.get_Range("C8", misValue).Formula = objRL.ContactNumber_Customer;
                    //myExcelWorksheet.get_Range("C9", misValue).Formula = objRL.GST_Customer;

                    RowCount = 10;

                    string CellDisplay1 = "";
                    int CellCheckCount = 0;
                    CellCheckCount = dsDeliveryChallanItem.Tables[0].Rows.Count;

                    CGST_Total = 0; SGST_Total = 0; IGST_Total = 0;
                    SRNO = 1;
                    string ConcatItem = string.Empty;
                    for (int i = 0; i < dsDeliveryChallanItem.Tables[0].Rows.Count; i++)
                    {

                        ConcatItem = dsDeliveryChallanItem.Tables[0].Rows[i]["ItemName"].ToString();

                        FlagBill = false;
                        AlignSet = 1;
                        Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SRNO.ToString());
                        AlignSet = 0;
                        Fill_Merge_Cell("B", "G", misValue, myExcelWorksheet, Convert.ToString(ConcatItem));
                        FlagBill = true;
                        AlignSet = 2;
                        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["Quantity"].ToString()));
                        Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["TaxablePrice"].ToString()));
                        Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["NetAmount"].ToString()));
                        RowCount++;
                        SRNO++;
                    }

                    FlagBill = false;
                    int StoreCount = 0;
                    StoreCount = RowCount;


                    FlagBill = false; AlignSet = 0;
                    Fill_Merge_Cell("A", "I", misValue, myExcelWorksheet, "Total Amount (in numbers)");
                    FlagBill = true; AlignSet = 2;
                    Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, Convert.ToString(txtTotal.Text));
                    RowCount++;

                    if (txtTotal.Text != "")
                    {
                        AlignSet = 0;
                        Fill_Merge_Cell("A", "J", misValue, myExcelWorksheet, "Total Amount (in words): " + objRL.words(Convert.ToInt32(txtTotal.Text)));
                    }

                    RowCount++;
                    AlignSet = 0; MH_Value = true;
                    Fill_Merge_Cell("A", "E", misValue, myExcelWorksheet, "Declaration \n We declare that this invoice shows the actual price of goods described and that all particulars are true and correct.");
                    //Fill_Merge_Cell("A", "E", misValue, myExcelWorksheet, );
                    MH_Value = false; AlignSet = 1;
                    Fill_Merge_Cell("F", "J", misValue, myExcelWorksheet, "For Sukhada Enterprises \n \n \n Authorised Signatory");
                    //Fill_Merge_Cell("F", "K", misValue, myExcelWorksheet,);

                    myExcelWorkbook.Save();

                    string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
                    const int xlQualityStandard = 0;
                    myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);

                    myExcelWorkbook.Close(true, misValue, misValue);
                    myExcelApp.Quit();

                    //objRL.ShowMessage(22, 1);
                    MessageBox.Show("Report Generated Successfully");

                    DialogResult dr1;
                    dr1 = MessageBox.Show("Do you want to view this report.?", "Report View", MessageBoxButtons.YesNo);
                    if (dr1 == DialogResult.Yes)
                        System.Diagnostics.Process.Start(PDFReport);
                    objRL.DeleteExcelFile();
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

        bool FlagBill = false; int AlignSet = 0; int ISGST = 0;

        protected void Fill_Merge_Cell(string Cell1, string Cell2, object val1, Excel.Worksheet myExcelWorksheet, string SetValue)
        {
            Cell1 = Cell1 + RowCount;
            Cell2 = Cell2 + RowCount;
            Excel.Range AlingRange1 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange1.Merge(val1);
            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            Excel.Range AlingRange2 = myExcelWorksheet.get_Range(Cell1, Cell2);

            if (AlignSet == 0)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            else if (AlignSet == 1)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            else
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            DrawBorder(AlingRange2);

            if (MH_Value)
                AlingRange2.RowHeight = 60;
        }

        private void btnCancelGrid_Click(object sender, EventArgs e)
        {
            Clear_Item();
        }

        private void btnDeleteGrid_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteGridMain_Click(object sender, EventArgs e)
        {
            if (dgvItem.Rows.Count > 0)
            {
                DialogResult dr;
                dr = MessageBox.Show("Do you want to delete this Item.?", "Delete", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    for (int i = 0; i < dgvItem.Rows.Count; i++)
                    {
                        dgvItem.Rows.RemoveAt(i);
                        //ClearAll_Item();
                        if (dgvItem.Rows.Count > 0)
                            dgvItemRow = dgvItem.Rows.Count;
                        else
                            dgvItemRow = 0;
                    }
                    SrNo_Add();
                }
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            Item objForm = new Item();
            objForm.ShowDialog(this);
        }

        private void txtSearchCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearchCustomer.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                    lbCustomer.Select();
            }
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            
        }

        protected bool Check_Exixt_StockQty()
        {
            objBL.Query = "Select ID,ItemId,PQty,SQty,AvailableQty,UserId from ItemStock where ItemId=" + ItemID + "";
            DataSet ds = new DataSet();
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ASQty = Convert.ToDouble(ds.Tables[0].Rows[0][4].ToString());
                QTYFlag = true;
            }
            else
                QTYFlag = false;

            return QTYFlag;
        }

        private void cbIsFinance_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbFinanceName_Leave(object sender, EventArgs e)
        {

        }


        private void txtSearchCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.TxtValue(sender, e, txtSearchCustomer);
        }

        private void cbIsFinance_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cbIsFinance_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            //FillGrid();
        }

        private void dtpInvoiceDate_ValueChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            Calculate_InvoiceTotal();
        }

        

        private void txtOtherCharges_TextChanged(object sender, EventArgs e)
        {
            Calculate_InvoiceTotal();
        }

        private void txtOtherCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtOtherCharges);
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtWeightGrid);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtRateGrid);
        }

        private void VisibleTrue()
        {
            lblCGSTAmount.Visible = true;
            lblCGST.Visible = true;
            txtCGSTAmount.Visible = true;
            lblCGST.Text = "";
            txtCGSTAmount.Text = "";

            lblSGSTAmount.Visible = true;
            lblSGST.Visible = true;
            txtSGSTAmount.Visible = true;
            lblSGST.Text = "";
            txtSGSTAmount.Text = "";

            lblIGSTAmount.Visible = true;
            lblIGST.Visible = true;
            txtIGSTAmount.Visible = true;
            lblIGST.Text = "";
            txtIGSTAmount.Text = "";
        }

        private void VisibleFalse()
        {
            lblCGSTAmount.Visible = false;
            lblCGST.Visible = false;
            txtCGSTAmount.Visible = false;
            lblCGST.Text = "";
            txtCGSTAmount.Text = "";

            lblSGSTAmount.Visible = false;
            lblSGST.Visible = false;
            txtSGSTAmount.Visible = false;
            lblSGST.Text = "";
            txtSGSTAmount.Text = "";

            lblIGSTAmount.Visible = false;
            lblIGST.Visible = false;
            txtIGSTAmount.Visible = false;
            lblIGST.Text = "";
            txtIGSTAmount.Text = "";
        }

        private void cbGST_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGST.Checked)
            {
                VisibleTrue();
                objRL.FillCompanyData();
                lblCGST.Text = objRL.CGSTPer.ToString();
                lblSGST.Text = objRL.SGSTPer.ToString();
                lblIGST.Text = objRL.IGSTPer.ToString();
                CGSTPer = objRL.CGSTPer;
                SGSTPer = objRL.SGSTPer;
                IGSTPer = objRL.IGSTPer;
                Calculate_InvoiceTotal();
            }
            else
            {
                VisibleFalse();
                CGSTPer = 0;
                SGSTPer = 0;
                IGSTPer = 0;
                Calculate_InvoiceTotal();
            } 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                DialogResult dr;
                dr = MessageBox.Show("Do you want to delete this record.?", "Delete", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    DeleteFlag = true;
                    SaveDB();
                }
                else
                    return;
            }
        }

        private void dgvItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearAll_Item();
            TempRowIndex = dgvItemRow;
            dgvItemRow = e.RowIndex;
            GridFlag = true;
            btnDeleteGridMain.Visible = true;
            SaleTransactionId = Convert.ToInt32(dgvItem.Rows[dgvItemRow].Cells["clmSaleTransactionId"].Value.ToString());
            txtItemNameGrid.Text = dgvItem.Rows[dgvItemRow].Cells["clmItemName"].Value.ToString();
            txtWeightGrid.Text = dgvItem.Rows[dgvItemRow].Cells["clmWeight"].Value.ToString();
            txtRateGrid.Text = dgvItem.Rows[dgvItemRow].Cells["clmRate"].Value.ToString();
            txtAmountGrid.Text = dgvItem.Rows[dgvItemRow].Cells["clmAmount"].Value.ToString();
            txtMakingChargesGrid.Text = dgvItem.Rows[dgvItemRow].Cells["clmMakingCharges"].Value.ToString();
            txtNetAmount.Text = dgvItem.Rows[dgvItemRow].Cells["clmNetAmount"].Value.ToString();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Calculate_InvoiceTotal();
        }

        private void txtMakingChargesGrid_TextChanged(object sender, EventArgs e)
        {
            Calculate_Amount();
        }

        private void txtItemNameGrid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
