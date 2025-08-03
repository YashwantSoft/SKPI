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

namespace SPApplication.Transaction
{

    public partial class SalesTransaction : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();

        int TableId = 0;
        bool DeleteFlag = false;
        int CustomerID = 0, ItemID = 0;

        static int dgvItemRow;

        bool GridFlag = false;
        int TempRowIndex = 0;

        double Rate = 0, Qty = 0, DiscountPercentage = 0, DiscountAmount = 0, Total = 0, CGSTPer = 0, SGSTPer = 0, IGSTPer = 0;
        double CGSTAmount = 0, SGSTAmount = 0, IGSTAmount = 0, TaxableValue = 0;
        double CGSTPerRC = 0, SGSTPerRC = 0, IGSTPerRC = 0, CGSTAmountRC = 0, SGSTAmountRC = 0, IGSTAmountRC = 0, NetAmount = 0;

        public SalesTransaction()
        {
            InitializeComponent();
            objBL.Set_Design_All(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_DASHBOARD_SALE);

            btnAddCompany.BackColor = objBL.GetBackgroundColor();
            btnAddCompany.ForeColor = objBL.GetForeColor();

            btnAddCustomer.BackColor = objBL.GetBackgroundColor();
            btnAddCustomer.ForeColor = objBL.GetForeColor();

            btnAddItem.BackColor = objBL.GetBackgroundColor();
            btnAddItem.ForeColor = objBL.GetForeColor();

            btnAddToGrid.BackColor = objBL.GetBackgroundColor();
            btnAddToGrid.ForeColor = objBL.GetForeColor();

            btnClearItem.BackColor = objBL.GetBackgroundColor();
            btnClearItem.ForeColor = objBL.GetForeColor();

            btnDeleteGrid.BackColor = objBL.GetBackgroundColor();
            btnDeleteGrid.ForeColor = objBL.GetForeColor();

            btnDeliveryChallan.BackColor = objBL.GetBackgroundColor();
            btnDeliveryChallan.ForeColor = objBL.GetForeColor();

            objRL.Fill_Payment_Type(cmbPaymentMode);
            objRL.GetBank(cmbBankName);
        }

        private void SalesTransaction_Load(object sender, EventArgs e)
        {
            dtpDate.CustomFormat = "dd/MM/yyyy";
            txtDCID.Text = Convert.ToString(Convert.ToInt32(objRL.ReturnMaxID("Sale")));
            Fill_Company();
            dtpDate.Focus();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            Customer objForm = new Customer();
            objForm.ShowDialog(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected void ClearMain()
        {
            dgvItemRow = 0;
            dataGridView1.DataSource = null;
            txtSearchCustomer.Text = "";

            txtDCID.Text = "";
            txtPONo.Text = "";
            txtDCNo.Text = "";
            txtJobNo.Text = "";
            txtBillNo.Text = "";

            dtpDate.Value = DateTime.Now.Date;
            ClearAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearMain();
            btnDeliveryChallan.Visible = false;
            btnDelete.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDeliveryChallan = false;
            SaveDB();
        }

        private bool Validation()
        {
            bool ReturnBill = false;
            objEP.Clear();

            if (ReturnBill == false)
            {
                if (cmbCompanyName.SelectedIndex == -1)
                {
                    cmbCompanyName.Focus();
                    objEP.SetError(cmbCompanyName, "Select Company Name");
                    ReturnBill = true;
                }
                else
                    ReturnBill = false;
            }

            if (ReturnBill == false)
            {
                if (txtDCNo.Text == "")
                {
                    objEP.SetError(txtDCNo, "Enter Challan No.");
                    ReturnBill = true;
                }
                else
                    ReturnBill = false;
            }

            if (ReturnBill == false)
            {
                if (txtCustomerName.Text == "")
                {
                    objEP.SetError(txtCustomerName, "Select Customer");
                    ReturnBill = true;
                }
                else
                    ReturnBill = false;
            }
            if (ReturnBill == false)
            {
                ReturnBill = false;
            }

            return ReturnBill;
        }

        string ChequeNumber = "", PartyBankAccountNumber = "", PartyBank = "", AccountNumber = "", BankName = "";
        int BankId = 0;
        DateTime dtTransactionDate;

        protected void SaveDB()
        {
            if (!Validation())
            {
                if (TableId != 0)
                {
                    if (!DeleteFlag)
                        objBL.Query = "update Sale set CompanyId=" + cmbCompanyName.SelectedValue + ",CompanyName='" + cmbCompanyName.Text + "',InvoiceDate='" + dtpDate.Value.ToShortDateString() + "',DCNo='" + txtDCNo.Text + "',JobNo='" + txtJobNo.Text + "',BillNo='" + txtBillNo.Text + "',PONo='" + txtPONo.Text + "',CustomerId=" + CustomerID + ", NoteD='" + txtNote.Text + "',TaxPaybleOnReverseCharge='" + cmbTaxIsPayableOnReverseCharge.Text + "',Total='" + txtTotalBill.Text + "',FreightCharges='" + txtFreightCharges.Text + "',LoadingAndPackingCharges='" + txtLoadingAndPackingCharges.Text + "',InsuranceCharges='" + txtInsuranceCharges.Text + "',OtherCharges='" + txtOtherCharges.Text + "',InvoiceTotal='" + txtInvoiceTotal.Text + "',Naration='" + txtNaration.Text + "',PaymentMode='" + cmbPaymentMode.Text + "',BankId=" + BankId + ",BankName='" + BankName + "',AccountNumber='" + AccountNumber + "',TransactionDate='" + dtTransactionDate.ToShortDateString() + "',ChequeNumber='" + ChequeNumber + "',PartyBank='" + PartyBank + "',PartyBankAccountNumber='" + PartyBankAccountNumber + "',TransportationMode='" + txtTransportationMode.Text + "',VehicalNumber='" + txtVehicalNumber.Text + "',DateOfSupply='" + dtpDateOfSupply.Value.ToShortDateString() + "',PlaceOfSupply='" + txtPlaceOfSupply.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableId + " and CancelTag=0 ";
                    else
                        objBL.Query = "update Sale set CancelTag=1 where ID=" + TableId + " and CancelTag=0 ";
                }
                else
                    objBL.Query = "insert into Sale(CompanyId,CompanyName,InvoiceDate,DCNo,JobNo,BillNo,PONo,CustomerId,NoteD,TaxPaybleOnReverseCharge,Total,FreightCharges,LoadingAndPackingCharges,InsuranceCharges,OtherCharges,InvoiceTotal,Naration,PaymentMode,BankId,BankName,AccountNumber,TransactionDate,ChequeNumber,PartyBank,PartyBankAccountNumber,TransportationMode,VehicalNumber,DateOfSupply,PlaceOfSupply,BillStatus,PrintFlag,PrintCount,UserId) values(" + cmbCompanyName.SelectedValue + ",'" + cmbCompanyName.Text + "','" + dtpDate.Value.ToShortDateString() + "','" + txtDCNo.Text + "','" + txtJobNo.Text + "','" + txtBillNo.Text + "','" + txtPONo.Text + "'," + CustomerID + ",'" + txtNote.Text + "','" + cmbTaxIsPayableOnReverseCharge.Text + "','" + txtTotalBill.Text + "','" + txtFreightCharges.Text + "','" + txtLoadingAndPackingCharges.Text + "','" + txtInsuranceCharges.Text + "','" + txtOtherCharges.Text + "','" + txtInvoiceTotal.Text + "','" + txtNaration.Text + "','" + cmbPaymentMode.Text + "'," + BankId + ",'" + BankName + "','" + AccountNumber + "','" + dtTransactionDate.ToShortDateString() + "','" + ChequeNumber + "','" + PartyBank + "','" + PartyBankAccountNumber + "','" + txtTransportationMode.Text + "','" + txtVehicalNumber.Text + "','" + dtpDateOfSupply.Value.ToShortDateString() + "','" + txtPlaceOfSupply.Text + "','0',0,0," + BusinessLayer.UserId_Static + ") ";

                objBL.Function_ExecuteNonQuery();

                if (TableId == 0)
                {
                    objBL.Query = "select max(ID) from Sale where CancelTag=0";
                    DataSet ds = new DataSet();
                    ds = objBL.ReturnDataSet();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string AB = ds.Tables[0].Rows[0][0].ToString();
                        if (AB != "")
                            TableId = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                        else
                            TableId = 1;
                    }
                }
                else
                {
                    objBL.Query = "delete from SaleTransaction where SaleId=" + TableId + "";
                    objBL.Function_ExecuteNonQuery();
                }

                if (TableId != 0)
                {
                    SaveItemList();
                    objRL.ShowMessage(7, 1);
                    FillGrid();
                    CreditBill();
                    ExcelReport();
                    ClearAll();
                }
            }
            else
            {
                objRL.ShowMessage(11, 4);
                return;
            }
        }

        double PendingAmount_Insert = 0; double PendingAmount = 0;
        private void ViewPendingAmount()
        {
            PendingAmount = objRL.Get_Pending_Details("CustomerPendingAmount", CustomerID);
            //txtPendingAmount.Text = PendingAmount.ToString();
        }

        private void CreditBill()
        {
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
                            objBL.Query = "update CustomerPendingAmount set PendingAmount = '" + PendingAmount_Insert + "' where CancelTag=0 and CustomerId=" + CustomerID + "";
                        else
                            objBL.Query = "insert into CustomerPendingAmount(CustomerId,PendingAmount) values(" + CustomerID + ",'" + PendingAmount_Insert + "')";

                        objBL.Function_ExecuteNonQuery();
                    }
                }
            }
        }

        int RowCount = 18;
        bool MH_Value = false;
        Excel.Application myExcelApp;
        Excel.Workbooks myExcelWorkbooks;
        Excel.Workbook myExcelWorkbook;

        //private void ExcelReport()
        //{

        //    DialogResult dr;
        //    dr = MessageBox.Show("Do you want to download this report", "Report View", MessageBoxButtons.YesNo);
        //    if (dr == DialogResult.Yes)
        //    {
        //        objBL.Query = "select ID,DeliveryChallanId,ItemId,ItemName,ItemDescription,ItemCode,HSNCode,UOM,Quantity from DeliveryChallanItem where CancelTag=0 and DeliveryChallanId=" + TableId + "";
        //        DataSet ds = new DataSet();
        //        ds = objBL.ReturnDataSet();
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            objRL.FillCompanyData();

        //            CellCheckCount = ds.Tables[0].Rows.Count;

        //            object misValue = System.Reflection.Missing.Value;
        //            myExcelApp = new Excel.Application();
        //            myExcelWorkbooks = myExcelApp.Workbooks;

        //            ExcelFormatPath = objRL.GetPath("ExcelFormat");

        //            ExcelFormatPath += "SalesReport.xlsx";

        //            FileInfo fi = new FileInfo(ExcelFormatPath);
        //            //fi.IsReadOnly = false;

        //            CurrentDate_String = objRL.Return_Date_String_DDMMYYYY(DateTime.Now.Date);

        //            ReportPath = objRL.GetPath("ReportPath");
        //            ReportPath += "Delivery Challan" + "\\" + txtCustomerName.Text + "\\" + CurrentDate_String + "\\";

        //            DirectoryInfo DI = new DirectoryInfo(ReportPath);
        //            DI.Create();

        //            DateTime dt = DateTime.Now.Date;
        //            string dtString = dt.ToString("dd-MM-yyyy");
        //            ReportName = txtDCID.Text + "-" + CurrentDate_String;
        //            ReportPath = ReportPath + ReportName + ".xlsx";

        //            FileInfo allExist = new FileInfo(ReportPath);

        //            if (allExist.Exists == true)
        //                allExist.Delete();

        //            fi.CopyTo(ReportPath);

        //            myExcelWorkbook = myExcelWorkbooks.Open(ReportPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
        //            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

        //            myExcelWorksheet.get_Range("A2", misValue).Formula = objRL.CI_CompanyName;
        //            myExcelWorksheet.get_Range("A3", misValue).Formula = objRL.CI_Address;
        //            myExcelWorksheet.get_Range("A4", misValue).Formula = objRL.CI_ContactNo;
        //            myExcelWorksheet.get_Range("A5", misValue).Formula = objRL.CI_EmailId;

        //            myExcelWorksheet.get_Range("A6", misValue).Formula = "Customer Name: " + txtCustomerName.Text;
        //            myExcelWorksheet.get_Range("A7", misValue).Formula = "Address: " + txtDeliveryAddress.Text;

        //            myExcelWorksheet.get_Range("A9", misValue).Formula = "VAT: " + txtNote.Text;
        //            myExcelWorksheet.get_Range("A10", misValue).Formula = "CST: " + txtCST.Text;

        //            myExcelWorksheet.get_Range("A9", misValue).Formula = "VAT: " + txtVATTINNo.Text;
        //            myExcelWorksheet.get_Range("A10", misValue).Formula = "CST: " + txtCST.Text;
        //            myExcelWorksheet.get_Range("C7", misValue).Formula = "Bill No: " + txtDCNo.Text;

        //            myExcelWorksheet.get_Range("C6", misValue).Formula = "Date: " + dtString;

        //            myExcelWorksheet.get_Range("C8", misValue).Formula = "P.O.No: " + txtPONo.Text;

        //            if (CellCheckCount > 0)
        //            {
        //                int RowCount = 12;
        //                string CellDisplay1 = "";

        //                for (int i = 0; i < CellCheckCount; i++)
        //                {
        //                    CellDisplay1 = "A" + RowCount;
        //                    Excel.Range firstRow = myExcelWorksheet.get_Range(CellDisplay1, misValue);
        //                    firstRow.EntireRow.Copy(misValue);

        //                    int PasteCount = 0;
        //                    PasteCount = RowCount + 1;
        //                    string CellDisplayP = "A" + PasteCount;
        //                    Excel.Range firstRow1 = myExcelWorksheet.get_Range(CellDisplayP, misValue);
        //                    firstRow1.EntireRow.PasteSpecial(XlPasteType.xlPasteAll, XlPasteSpecialOperation.xlPasteSpecialOperationNone, misValue, misValue);
        //                    RowCount++;
        //                }

        //                int cellCount = 12;
        //                int SRNO = 1;

        //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //                {
        //                    //Sr.No
        //                    CellDisplay1 = "A" + cellCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = SRNO.ToString();

        //                    //Item Name
        //                    CellDisplay1 = "B" + cellCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(ds.Tables[0].Rows[i]["ItemName"].ToString());

        //                    //Item Descripition
        //                    CellDisplay1 = "C" + cellCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(ds.Tables[0].Rows[i]["ItemDescription"].ToString());

        //                    //Item Code
        //                    CellDisplay1 = "D" + cellCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"].ToString());

        //                    //HSN Code 
        //                    CellDisplay1 = "E" + cellCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(ds.Tables[0].Rows[i]["HSNCode"].ToString());

        //                    //Unit
        //                    CellDisplay1 = "D" + cellCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(ds.Tables[0].Rows[i]["UOM"].ToString());

        //                    //Qty
        //                    CellDisplay1 = "E" + cellCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(ds.Tables[0].Rows[i]["Quantity"].ToString());

        //                    cellCount++;
        //                    SRNO++;
        //                }

        //                RowCount++; RowCount++; RowCount++; RowCount++;

        //                string Cell1 = "", Cell2 = "";

        //                Cell1 = "A" + RowCount;
        //                Cell2 = "B" + RowCount;
        //                Excel.Range AlingRange711111 = myExcelWorksheet.get_Range(Cell1, Cell2);
        //                AlingRange711111.Merge(misValue);

        //                myExcelWorksheet.get_Range(Cell1, Cell2).Formula = "Customer Sign";
        //                Excel.Range AlingRange811111 = myExcelWorksheet.get_Range(Cell1, Cell2);
        //                AlingRange811111.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

        //                Cell1 = "C" + RowCount;
        //                Cell2 = "F" + RowCount;
        //                Excel.Range AlingRange7111111 = myExcelWorksheet.get_Range(Cell1, Cell2);
        //                AlingRange7111111.Merge(misValue);

        //                myExcelWorksheet.get_Range(Cell1, Cell2).Formula = "for Radiance Kitchen";
        //                Excel.Range AlingRange8111111 = myExcelWorksheet.get_Range(Cell1, Cell2);
        //                AlingRange8111111.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        //            }

        //            myExcelWorkbook.Save();

        //            string filename = ReportPath.Replace(".xlsx", ".pdf");
        //            string NewPath = filename;
        //            ReportDestination = NewPath;
        //            const int xlQualityStandard = 0;
        //            myExcelWorkbook.ExportAsFixedFormat(
        //            XlFixedFormatType.xlTypePDF,
        //            filename, xlQualityStandard, true, false,
        //            Type.Missing, Type.Missing, false, Type.Missing);

        //            myExcelWorkbook.Close(true, misValue, misValue);
        //            myExcelApp.Quit();


        //            MessageBox.Show("Report Generated Successfully");
        //            DialogResult dr1;
        //            dr1 = MessageBox.Show("Do you want to view this report", "Report View", MessageBoxButtons.YesNo);
        //            if (dr1 == DialogResult.Yes)
        //                System.Diagnostics.Process.Start(ReportPath);

        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //}

        int SRNO = 1;
        double CGST_Total = 0, SGST_Total = 0, IGST_Total = 0, CGST_Total_RC = 0, SGST_Total_RC = 0, IGST_Total_RC = 0;

        //private void ExcelReport()
        //{
        //    DataSet dsDeliveryChallanItem = new DataSet();
        //    objBL.Query = "select BI.ID,BI.BillId,BI.ItemId,I.ItemName,I.ItemCode,I.HSNCode,BI.Rate,BI.Quantity,BI.Total,BI.Discount,BI.DiscountAmount,BI.TaxableValue,BI.CGSTPercentage,BI.CGSTAmount,BI.SGSTPercentage,BI.SGSTAmount,BI.IGSTPercentage,BI.IGSTAmount,BI.CGSTPercentageRC,BI.CGSTAmountRC,BI.SGSTPercentageRC,BI.SGSTAmountRC,BI.IGSTPercentageRC,BI.IGSTAmountRC,BI.NetAmount from BillItem BI inner join Item I on I.ID=BI.ItemId where BI.CancelTag=0 and I.CancelTag=0 and BI.BillId=" + TableId + "";
        //    dsDeliveryChallanItem = objBL.ReturnDataSet();

        //    if (dsDeliveryChallanItem.Tables[0].Rows.Count > 0)
        //    {
        //        objRL.FillCompanyData();
        //        objRL.GetCustomerRecords(CustomerID);
        //        objRL.GetBill(TableId);

        //        DialogResult dr;
        //        dr = MessageBox.Show("Do you want to create this report.?", "Report View", MessageBoxButtons.YesNo);

        //        if (dr == DialogResult.Yes)
        //        {
        //            object misValue = System.Reflection.Missing.Value;
        //            myExcelApp = new Excel.Application();
        //            myExcelWorkbooks = myExcelApp.Workbooks;

        //            objRL.ClearExcelPath();
        //            objRL.isPDF = true;

        //            if(FlagDeliveryChallan == false)
        //                objRL.Form_ExcelFileName = "Bill.xlsx";
        //            else
        //                objRL.Form_ExcelFileName = "DeliveryChallanNew.xlsx";

        //            if (FlagDeliveryChallan == false)
        //                objRL.Form_ReportFileName = "Bill";
        //            else
        //                objRL.Form_ReportFileName = "DeliveryChallan";

        //            if (FlagDeliveryChallan == false)
        //                objRL.Form_DestinationReportFilePath = "Bill\\" + txtCustomerName.Text + "\\";
        //            else
        //                objRL.Form_DestinationReportFilePath = "Delivery Challan\\" + txtCustomerName.Text + "\\";

        //            objRL.Path_Comman();

        //            myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
        //            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

        //            if (FlagDeliveryChallan == false)
        //                 myExcelWorksheet.get_Range("A4", misValue).Formula = "TAX INVOICE";
        //            else
        //                myExcelWorksheet.get_Range("A4", misValue).Formula = "Delivery Challan";

        //            myExcelWorksheet.get_Range("A2", misValue).Formula = objRL.CI_CompanyName;
        //            myExcelWorksheet.get_Range("A3", misValue).Formula = objRL.CI_Address;
        //            myExcelWorksheet.get_Range("A4", misValue).Formula = objRL.CI_ContactNo + "    Email Id: " + objRL.CI_EmailId;

        //            myExcelWorksheet.get_Range("E5", misValue).Formula = objRL.CI_GSTIN;
        //            myExcelWorksheet.get_Range("E6", misValue).Formula = objRL.TaxPaybleOnReverseCharge_Bill;
        //            myExcelWorksheet.get_Range("E7", misValue).Formula = objRL.ID_Bill;
        //            myExcelWorksheet.get_Range("E8", misValue).Formula = objRL.Return_Date_String_DDMMYYYY(objRL.InvoiceDate_Bill);

        //            myExcelWorksheet.get_Range("M5", misValue).Formula = objRL.TransportationMode_Bill;
        //            myExcelWorksheet.get_Range("M6", misValue).Formula = objRL.VehicalNumber_Bill;
        //            myExcelWorksheet.get_Range("M7", misValue).Formula = objRL.Return_Date_String_DDMMYYYY(objRL.DateOfSupply_Bill);
        //            myExcelWorksheet.get_Range("M8", misValue).Formula = objRL.PlaceOfSupply_Bill;

        //            myExcelWorksheet.get_Range("E9", misValue).Formula = objRL.PONo_Bill;
        //            myExcelWorksheet.get_Range("E10", misValue).Formula = objRL.BillNo_Bill;
        //            myExcelWorksheet.get_Range("M9", misValue).Formula = objRL.JobNo_Bill;
        //            myExcelWorksheet.get_Range("M10", misValue).Formula = objRL.DCNo_Bill;

        //            myExcelWorksheet.get_Range("C12", misValue).Formula = objRL.CustomerName_Customer;
        //            myExcelWorksheet.get_Range("C13", misValue).Formula = objRL.BillingAddress_Customer;
        //            myExcelWorksheet.get_Range("C16", misValue).Formula = objRL.GST_Customer;

        //            myExcelWorksheet.get_Range("L12", misValue).Formula = objRL.CustomerName_Customer;
        //            myExcelWorksheet.get_Range("L13", misValue).Formula = objRL.DeliveryAddress_Customer;
        //            myExcelWorksheet.get_Range("L16", misValue).Formula = objRL.GST_Customer;

        //            if (FlagDeliveryChallan == false)
        //                RowCount = 20;
        //            else
        //                RowCount = 19;

        //            string CellDisplay1 = "";
        //            int CellCheckCount = 0;
        //            CellCheckCount = dsDeliveryChallanItem.Tables[0].Rows.Count;

        //            if (CellCheckCount > 1)
        //            {
        //                for (int i = 0; i < CellCheckCount-1; i++)
        //                {
        //                    CellDisplay1 = "A" + RowCount;
        //                    Excel.Range firstRow = myExcelWorksheet.get_Range(CellDisplay1, misValue);
        //                    firstRow.EntireRow.Copy(misValue);

        //                    int PasteCount = 0;
        //                    PasteCount = RowCount + 1;
        //                    string CellDisplayP = "A" + PasteCount;
        //                    Excel.Range firstRow1 = myExcelWorksheet.get_Range(CellDisplayP, misValue);
        //                    firstRow1.EntireRow.PasteSpecial(XlPasteType.xlPasteAll, XlPasteSpecialOperation.xlPasteSpecialOperationNone, misValue, misValue);
        //                    RowCount++;
        //                }
        //            }
        //            if (FlagDeliveryChallan == false)
        //                RowCount = 20;
        //            else
        //                RowCount = 19;

        //            CGST_Total = 0; SGST_Total = 0; IGST_Total = 0;
        //            SRNO = 1;

        //            if (FlagDeliveryChallan == false)
        //            {
        //                for (int i = 0; i < dsDeliveryChallanItem.Tables[0].Rows.Count; i++)
        //                {
        //                    //Sr.No
        //                    CellDisplay1 = "A" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = SRNO.ToString();

        //                    //Item Name
        //                    CellDisplay1 = "B" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["ItemName"].ToString());

        //                    //HSN Code
        //                    CellDisplay1 = "D" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["HSNCode"].ToString());

        //                    //HSN Code
        //                    CellDisplay1 = "E" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["Quantity"].ToString());

        //                    //HSN Code 
        //                    CellDisplay1 = "F" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = "Qty.";

        //                    //Unit
        //                    CellDisplay1 = "G" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["Rate"].ToString());

        //                    //Qty
        //                    CellDisplay1 = "H" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["Total"].ToString());

        //                    //HSN Code
        //                    CellDisplay1 = "I" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["DiscountAmount"].ToString());

        //                    //HSN Code 
        //                    CellDisplay1 = "J" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["TaxableValue"].ToString());

        //                    //Unit
        //                    CellDisplay1 = "K" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["CGSTPercentage"].ToString());

        //                    //Qty
        //                    CellDisplay1 = "L" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["CGSTAmount"].ToString());

        //                    //Unitn
        //                    CellDisplay1 = "M" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["SGSTPercentage"].ToString());

        //                    //Qty
        //                    CellDisplay1 = "N" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["SGSTAmount"].ToString());

        //                    //Unit
        //                    CellDisplay1 = "O" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["IGSTPercentage"].ToString());

        //                    //Qty
        //                    CellDisplay1 = "P" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["IGSTAmount"].ToString());


        //                    CGST_Total += objRL.Check_NullString(dsDeliveryChallanItem.Tables[0].Rows[i]["CGSTAmount"].ToString());
        //                    SGST_Total += objRL.Check_NullString(dsDeliveryChallanItem.Tables[0].Rows[i]["SGSTAmount"].ToString());
        //                    IGST_Total += objRL.Check_NullString(dsDeliveryChallanItem.Tables[0].Rows[i]["IGSTAmount"].ToString());

        //                    CGST_Total_RC += objRL.Check_NullString(dsDeliveryChallanItem.Tables[0].Rows[i]["CGSTAmountRC"].ToString());
        //                    SGST_Total_RC += objRL.Check_NullString(dsDeliveryChallanItem.Tables[0].Rows[i]["SGSTAmountRC"].ToString());
        //                    IGST_Total_RC += objRL.Check_NullString(dsDeliveryChallanItem.Tables[0].Rows[i]["IGSTAmountRC"].ToString());

        //                    RowCount++;
        //                    SRNO++;

        //                }

        //                //if (CellCheckCount > 1)
        //                //    RowCount++;

        //                Fill_Merge_Cell("A", "J", misValue, myExcelWorksheet, "Tax Total");
        //                AmountFlag = true;
        //                Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, CGST_Total.ToString());
        //                Fill_Merge_Cell("N", "N", misValue, myExcelWorksheet, SGST_Total.ToString());
        //                Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, IGST_Total.ToString());
        //                AmountFlag = false;

        //                RowCount++;

        //                Fill_Merge_Cell("A", "J", misValue, myExcelWorksheet, "Tax Is Payable on Reverse Charge");
        //                AmountFlag = true;
        //                Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, CGST_Total_RC.ToString());
        //                Fill_Merge_Cell("N", "N", misValue, myExcelWorksheet, SGST_Total_RC.ToString());
        //                Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, IGST_Total_RC.ToString());
        //                AmountFlag = false;

        //                RowCount++;

        //                //Total
        //                AmountFlag = false;
        //                Fill_Merge_Cell("A", "O", misValue, myExcelWorksheet, "Total");
        //                AmountFlag = true;
        //                Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, objRL.Total_Bill.ToString());
        //                RowCount++;

        //                //Freight Charges
        //                AmountFlag = false;
        //                Fill_Merge_Cell("A", "O", misValue, myExcelWorksheet, "Freight Charges");
        //                AmountFlag = true;
        //                Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, objRL.FreightCharges_Bill.ToString());
        //                RowCount++;

        //                //Loading and Packing Charges
        //                AmountFlag = false;
        //                Fill_Merge_Cell("A", "O", misValue, myExcelWorksheet, "Loading and Packing Charges");
        //                AmountFlag = true;
        //                Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, objRL.LoadingAndPackingCharges_Bill.ToString());
        //                RowCount++;

        //                //Insurance Charges
        //                AmountFlag = false;
        //                Fill_Merge_Cell("A", "O", misValue, myExcelWorksheet, "Insurance Charges");
        //                AmountFlag = true;
        //                Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, objRL.InsuranceCharges_Bill.ToString());
        //                RowCount++;

        //                //Other Charges
        //                AmountFlag = false;
        //                Fill_Merge_Cell("A", "O", misValue, myExcelWorksheet, "Other Charges");
        //                AmountFlag = true;
        //                Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, objRL.OtherCharges_Bill.ToString());
        //                RowCount++;

        //                //Invoice Total
        //                AmountFlag = false;
        //                Fill_Merge_Cell("A", "O", misValue, myExcelWorksheet, "Invoice Total");
        //                AmountFlag = true;
        //                Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, objRL.InvoiceTotal_Bill.ToString());
        //                RowCount++;

        //                AmountFlag = false;
        //                double ValueConvert = Convert.ToDouble(objRL.InvoiceTotal_Bill);
        //                ValueConvert = Math.Round(ValueConvert);
        //                Fill_Merge_Cell("A", "P", misValue, myExcelWorksheet, "Amount in Words-: " + objRL.words(Convert.ToInt32(ValueConvert)));
        //                RowCount++;

        //                MH_Value = true;
        //                Fill_Merge_Cell("A", "P", misValue, myExcelWorksheet, objRL.VATTINData);
        //                MH_Value = false;

        //            }
        //            else
        //            {
        //                for (int i = 0; i < dsDeliveryChallanItem.Tables[0].Rows.Count; i++)
        //                {
        //                    //Sr.No
        //                    CellDisplay1 = "A" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = SRNO.ToString();

        //                    //Item Name
        //                    CellDisplay1 = "B" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["ItemName"].ToString());

        //                    //HSN Code
        //                    CellDisplay1 = "M" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["HSNCode"].ToString());


        //                    //Qty
        //                    CellDisplay1 = "O" + RowCount;
        //                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["Quantity"].ToString());

        //                    RowCount++;
        //                    SRNO++;
        //                }
        //            }

        //            RowCount++; RowCount++; RowCount++; RowCount++; 

        //            Fill_Merge_Cell("A", "H", misValue, myExcelWorksheet, "Customer Sign");
        //            Fill_Merge_Cell("J", "P", misValue, myExcelWorksheet, "for Shree Rajput Enterprises");

        //            myExcelWorkbook.Save();

        //            string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
        //            const int xlQualityStandard = 0;
        //            myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);

        //            myExcelWorkbook.Close(true, misValue, misValue);
        //            myExcelApp.Quit();

        //            //objRL.ShowMessage(22, 1);
        //            MessageBox.Show("Report Generated Successfully");

        //            DialogResult dr1;
        //            dr1 = MessageBox.Show("Do you want to view this report.?", "Report View", MessageBoxButtons.YesNo);
        //            if (dr1 == DialogResult.Yes)
        //                System.Diagnostics.Process.Start(PDFReport);
        //            objRL.DeleteExcelFile();
        //        }
        //    }
        //    else
        //    {

        //    }
        //}

        private void ExcelReport()
        {
            DataSet dsDeliveryChallanItem = new DataSet();
            objBL.Query = "select BI.ID,BI.SaleId,BI.ItemId,I.ItemName,I.BatchNumber,I.HSNCode,I.UOM,I.Price,I.Cost,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST,BI.Rate,BI.Quantity,BI.Total,BI.Discount,BI.DiscountAmount,BI.TaxableValue,BI.CGSTPercentage,BI.CGSTAmount,BI.SGSTPercentage,BI.SGSTAmount,BI.IGSTPercentage,BI.IGSTAmount,BI.CGSTPercentageRC,BI.CGSTAmountRC,BI.SGSTPercentageRC,BI.SGSTAmountRC,BI.IGSTPercentageRC,BI.IGSTAmountRC,BI.NetAmount from SaleTransaction BI inner join Item I on I.ID=BI.ItemId where BI.CancelTag=0 and I.CancelTag=0 and BI.SaleId=" + TableId + "";
            dsDeliveryChallanItem = objBL.ReturnDataSet();

            if (dsDeliveryChallanItem.Tables[0].Rows.Count > 0)
            {
                objRL.CI_CompanyId = Convert.ToInt32(cmbCompanyName.SelectedValue.ToString());
                objRL.FillCompanyData();
                objRL.GetCustomerRecords(CustomerID);
                objRL.GetBill(TableId);

                DialogResult dr;
                dr = objRL.ReturnDialogResult_Report();// MessageBox.Show("Do you want to create this report.?", "Report View", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    object misValue = System.Reflection.Missing.Value;
                    myExcelApp = new Excel.Application();
                    myExcelWorkbooks = myExcelApp.Workbooks;

                    objRL.ClearExcelPath();
                    objRL.isPDF = true;

                    if (!FlagDeliveryChallan)
                        objRL.Form_ExcelFileName = "Bill.xlsx";
                    else
                        objRL.Form_ExcelFileName = "DC.xlsx";

                    if (!FlagDeliveryChallan)
                        objRL.Form_ReportFileName = "Bill";
                    else
                        objRL.Form_ReportFileName = "DeliveryChallan";

                    if (!FlagDeliveryChallan)
                        objRL.Form_DestinationReportFilePath = "Bill\\" + txtCustomerName.Text + "\\";
                    else
                        objRL.Form_DestinationReportFilePath = "Delivery Challan\\" + txtCustomerName.Text + "\\";

                    objRL.Path_Comman();

                    myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                    Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                    //if (!FlagDeliveryChallan)
                    //    myExcelWorksheet.get_Range("A4", misValue).Formula = "TAX INVOICE";
                    //else
                    //    myExcelWorksheet.get_Range("A4", misValue).Formula = "Delivery Challan";

                    myExcelWorksheet.get_Range("A2", misValue).Formula = objRL.CI_CompanyName;
                    myExcelWorksheet.get_Range("A3", misValue).Formula = objRL.CI_Address;
                    myExcelWorksheet.get_Range("A4", misValue).Formula = "Mobile No.- " + objRL.CI_ContactNo + "    Email Id: " + objRL.CI_EmailId;

                    myExcelWorksheet.get_Range("E5", misValue).Formula = objRL.CI_GSTIN;
                    myExcelWorksheet.get_Range("E6", misValue).Formula = objRL.TaxPaybleOnReverseCharge_Bill;
                    myExcelWorksheet.get_Range("E7", misValue).Formula = objRL.ID_Bill;
                    myExcelWorksheet.get_Range("E8", misValue).Formula = objRL.Return_Date_String_DDMMYYYY(objRL.InvoiceDate_Bill);

                    myExcelWorksheet.get_Range("M5", misValue).Formula = objRL.TransportationMode_Bill;
                    myExcelWorksheet.get_Range("M6", misValue).Formula = objRL.VehicalNumber_Bill;
                    myExcelWorksheet.get_Range("M7", misValue).Formula = objRL.Return_Date_String_DDMMYYYY(objRL.DateOfSupply_Bill);
                    myExcelWorksheet.get_Range("M8", misValue).Formula = objRL.PlaceOfSupply_Bill;

                    myExcelWorksheet.get_Range("E9", misValue).Formula = objRL.PONo_Bill;
                    myExcelWorksheet.get_Range("E10", misValue).Formula = objRL.BillNo_Bill;
                    myExcelWorksheet.get_Range("M9", misValue).Formula = objRL.JobNo_Bill;
                    myExcelWorksheet.get_Range("M10", misValue).Formula = objRL.DCNo_Bill;

                    myExcelWorksheet.get_Range("C12", misValue).Formula = objRL.CustomerName_Customer;
                    myExcelWorksheet.get_Range("C13", misValue).Formula = objRL.BillingAddress_Customer;
                    myExcelWorksheet.get_Range("C16", misValue).Formula = objRL.GST_Customer;

                    myExcelWorksheet.get_Range("L12", misValue).Formula = objRL.CustomerName_Customer;
                    myExcelWorksheet.get_Range("L13", misValue).Formula = objRL.DeliveryAddress_Customer;
                    myExcelWorksheet.get_Range("L16", misValue).Formula = objRL.GST_Customer;

                    if (!FlagDeliveryChallan)
                        RowCount = 20;
                    else
                        RowCount = 19;

                    string CellDisplay1 = "";
                    int CellCheckCount = 0;
                    CellCheckCount = dsDeliveryChallanItem.Tables[0].Rows.Count;

                    //if (CellCheckCount > 1)
                    //{
                    //    for (int i = 0; i < CellCheckCount - 1; i++)
                    //    {
                    //        CellDisplay1 = "A" + RowCount;
                    //        Excel.Range firstRow = myExcelWorksheet.get_Range(CellDisplay1, misValue);
                    //        firstRow.EntireRow.Copy(misValue);

                    //        int PasteCount = 0;
                    //        PasteCount = RowCount + 1;
                    //        string CellDisplayP = "A" + PasteCount;
                    //        Excel.Range firstRow1 = myExcelWorksheet.get_Range(CellDisplayP, misValue);
                    //        firstRow1.EntireRow.PasteSpecial(XlPasteType.xlPasteAll, XlPasteSpecialOperation.xlPasteSpecialOperationNone, misValue, misValue);
                    //        RowCount++;
                    //    }
                    //}
                    if (!FlagDeliveryChallan)
                        RowCount = 20;
                    else
                        RowCount = 19;

                    CGST_Total = 0; SGST_Total = 0; IGST_Total = 0;
                    SRNO = 1;

                    if (FlagDeliveryChallan == false)
                    {
                        for (int i = 0; i < dsDeliveryChallanItem.Tables[0].Rows.Count; i++)
                        {
                            //Sr.No
                            CellDisplay1 = "A" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = SRNO.ToString();

                            //Item Name
                            CellDisplay1 = "B" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).WrapText = true;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["ItemName"].ToString());
                            

                            //HSN Code
                            CellDisplay1 = "D" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["HSNCode"].ToString());

                            //HSN Code
                            CellDisplay1 = "E" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["Quantity"].ToString());

                            //HSN Code 
                            CellDisplay1 = "F" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = "Qty.";

                            //Unit
                            CellDisplay1 = "G" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["Rate"].ToString());

                            //Qty
                            CellDisplay1 = "H" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["Total"].ToString());

                            //HSN Code
                            CellDisplay1 = "I" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["DiscountAmount"].ToString());

                            //HSN Code 
                            CellDisplay1 = "J" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["TaxableValue"].ToString());

                            //Unit
                            CellDisplay1 = "K" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["CGSTPercentage"].ToString());

                            //Qty
                            CellDisplay1 = "L" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["CGSTAmount"].ToString());

                            //Unitn
                            CellDisplay1 = "M" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["SGSTPercentage"].ToString());

                            //Qty
                            CellDisplay1 = "N" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["SGSTAmount"].ToString());

                            //Unit
                            CellDisplay1 = "O" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["IGSTPercentage"].ToString());

                            //Qty
                            CellDisplay1 = "P" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["IGSTAmount"].ToString());


                            CGST_Total += objRL.Check_NullString(dsDeliveryChallanItem.Tables[0].Rows[i]["CGSTAmount"].ToString());
                            SGST_Total += objRL.Check_NullString(dsDeliveryChallanItem.Tables[0].Rows[i]["SGSTAmount"].ToString());
                            IGST_Total += objRL.Check_NullString(dsDeliveryChallanItem.Tables[0].Rows[i]["IGSTAmount"].ToString());

                            CGST_Total_RC += objRL.Check_NullString(dsDeliveryChallanItem.Tables[0].Rows[i]["CGSTAmountRC"].ToString());
                            SGST_Total_RC += objRL.Check_NullString(dsDeliveryChallanItem.Tables[0].Rows[i]["SGSTAmountRC"].ToString());
                            IGST_Total_RC += objRL.Check_NullString(dsDeliveryChallanItem.Tables[0].Rows[i]["IGSTAmountRC"].ToString());

                            RowCount++;
                            SRNO++;
                        }

                        //if (CellCheckCount > 1)
                        //    RowCount++;

                        myExcelWorksheet.get_Range("L34", misValue).Formula = CGST_Total.ToString();
                        myExcelWorksheet.get_Range("N34", misValue).Formula = SGST_Total.ToString();
                        myExcelWorksheet.get_Range("P34", misValue).Formula = SGST_Total.ToString();
                        myExcelWorksheet.get_Range("L35", misValue).Formula = CGST_Total_RC.ToString();
                        myExcelWorksheet.get_Range("N35", misValue).Formula = SGST_Total_RC.ToString();
                        myExcelWorksheet.get_Range("P35", misValue).Formula = IGST_Total_RC.ToString();


                        myExcelWorksheet.get_Range("P36", misValue).Formula = objRL.Total_Bill.ToString();
                        myExcelWorksheet.get_Range("P37", misValue).Formula = objRL.FreightCharges_Bill.ToString();
                        myExcelWorksheet.get_Range("P38", misValue).Formula = objRL.LoadingAndPackingCharges_Bill.ToString();
                        myExcelWorksheet.get_Range("P39", misValue).Formula = objRL.InsuranceCharges_Bill.ToString();
                        myExcelWorksheet.get_Range("P40", misValue).Formula = objRL.OtherCharges_Bill.ToString();
                        myExcelWorksheet.get_Range("P41", misValue).Formula = objRL.InvoiceTotal_Bill.ToString();

                        double ValueConvert = Convert.ToDouble(objRL.InvoiceTotal_Bill);
                        myExcelWorksheet.get_Range("A42", misValue).Formula = "Amount in Words-: " + objRL.words(Convert.ToInt32(ValueConvert));

                        //Bank Details
                        objRL.Account();
                        myExcelWorksheet.get_Range("C44", misValue).Formula = objRL.BankName_Report.ToString();
                        myExcelWorksheet.get_Range("C45", misValue).Formula = objRL.BankAddress_Report.ToString();
                        myExcelWorksheet.get_Range("C46", misValue).Formula = objRL.AccountNumber_Report.ToString();
                        myExcelWorksheet.get_Range("C47", misValue).Formula = objRL.AccountType_Report.ToString();
                        myExcelWorksheet.get_Range("C48", misValue).Formula = objRL.AccountHolderName_Report.ToString();
                        myExcelWorksheet.get_Range("C49", misValue).Formula = objRL.IFSCCode_Report.ToString();

                        if (!string.IsNullOrEmpty(Convert.ToString(objRL.PaymentMode_Bill)))
                        {
                            myExcelWorksheet.get_Range("K43", misValue).Formula = objRL.PaymentMode_Bill.ToString();
                            if (objRL.PaymentMode_Bill != "CASH" && objRL.PaymentMode_Bill != "CREDIT")
                            {
                                myExcelWorksheet.get_Range("K44", misValue).Formula = objRL.BankName_Bill.ToString();
                                myExcelWorksheet.get_Range("K45", misValue).Formula = objRL.AccountNumber_Bill.ToString();
                                myExcelWorksheet.get_Range("K46", misValue).Formula = objRL.Return_Date_String_DDMMYYYY(objRL.TransactionDate_Bill);
                                myExcelWorksheet.get_Range("K47", misValue).Formula = objRL.ChequeNumber_Bill.ToString();
                                myExcelWorksheet.get_Range("K48", misValue).Formula = objRL.PartyBank_Bill.ToString();
                                myExcelWorksheet.get_Range("K49", misValue).Formula = objRL.PartyBankAccountNumber_Bill.ToString();
                            }
                        }

                        //myExcelWorksheet.get_Range("C12", misValue).Formula = objRL.CustomerName_Customer;

                        //Fill_Merge_Cell("A", "J", misValue, myExcelWorksheet, "Tax Total");
                        //AmountFlag = true;
                        //Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, CGST_Total.ToString());
                        //Fill_Merge_Cell("N", "N", misValue, myExcelWorksheet, SGST_Total.ToString());
                        //Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, IGST_Total.ToString());
                        //AmountFlag = false;

                        //RowCount++;

                        //Fill_Merge_Cell("A", "J", misValue, myExcelWorksheet, "Tax Is Payable on Reverse Charge");
                        //AmountFlag = true;
                        //Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, CGST_Total_RC.ToString());
                        //Fill_Merge_Cell("N", "N", misValue, myExcelWorksheet, SGST_Total_RC.ToString());
                        //Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, IGST_Total_RC.ToString());
                        //AmountFlag = false;

                        //RowCount++;

                        ////Total
                        //AmountFlag = false;
                        //Fill_Merge_Cell("A", "O", misValue, myExcelWorksheet, "Total");
                        //AmountFlag = true;
                        //Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, objRL.Total_Bill.ToString());
                        //RowCount++;

                        ////Freight Charges
                        //AmountFlag = false;
                        //Fill_Merge_Cell("A", "O", misValue, myExcelWorksheet, "Freight Charges");
                        //AmountFlag = true;
                        //Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, objRL.FreightCharges_Bill.ToString());
                        //RowCount++;

                        ////Loading and Packing Charges
                        //AmountFlag = false;
                        //Fill_Merge_Cell("A", "O", misValue, myExcelWorksheet, "Loading and Packing Charges");
                        //AmountFlag = true;
                        //Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, objRL.LoadingAndPackingCharges_Bill.ToString());
                        //RowCount++;

                        ////Insurance Charges
                        //AmountFlag = false;
                        //Fill_Merge_Cell("A", "O", misValue, myExcelWorksheet, "Insurance Charges");
                        //AmountFlag = true;
                        //Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, objRL.InsuranceCharges_Bill.ToString());
                        //RowCount++;

                        ////Other Charges
                        //AmountFlag = false;
                        //Fill_Merge_Cell("A", "O", misValue, myExcelWorksheet, "Other Charges");
                        //AmountFlag = true;
                        //Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, objRL.OtherCharges_Bill.ToString());
                        //RowCount++;

                        ////Invoice Total
                        //AmountFlag = false;
                        //Fill_Merge_Cell("A", "O", misValue, myExcelWorksheet, "Invoice Total");
                        //AmountFlag = true;
                        //Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, objRL.InvoiceTotal_Bill.ToString());
                        //RowCount++;

                        //AmountFlag = false;
                        //double ValueConvert = Convert.ToDouble(objRL.InvoiceTotal_Bill);
                        //ValueConvert = Math.Round(ValueConvert);
                        //Fill_Merge_Cell("A", "P", misValue, myExcelWorksheet, "Amount in Words-: " + objRL.words(Convert.ToInt32(ValueConvert)));
                        //RowCount++;

                        //MH_Value = true;
                        //Fill_Merge_Cell("A", "P", misValue, myExcelWorksheet, objRL.VATTINData);
                        //MH_Value = false;
                    }
                    else
                    {
                        RowCount = 19;
                        for (int i = 0; i < dsDeliveryChallanItem.Tables[0].Rows.Count; i++)
                        {
                            //Sr.No
                            CellDisplay1 = "A" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = SRNO.ToString();

                            //Item Name
                            CellDisplay1 = "B" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["ItemName"].ToString());

                            //HSN Code
                            CellDisplay1 = "J" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["HSNCode"].ToString());

                            //Qty
                            CellDisplay1 = "O" + RowCount;
                            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dsDeliveryChallanItem.Tables[0].Rows[i]["Quantity"].ToString());

                            RowCount++;
                            SRNO++;
                        }

                        //RowCount++; RowCount++; RowCount++; RowCount++;

                        //Fill_Merge_Cell("A", "H", misValue, myExcelWorksheet, "Customer Sign");
                        //Fill_Merge_Cell("J", "P", misValue, myExcelWorksheet, "for Shree Rajput Enterprises");
                    }

                    myExcelWorkbook.Save();

                    string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
                    const int xlQualityStandard = 0;
                    myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);

                    myExcelWorkbook.Close(true, misValue, misValue);
                    myExcelApp.Quit();

                    objRL.ShowMessage(22, 1);
                    //MessageBox.Show("Report Generated Successfully");

                    //DialogResult dr1;
                    //dr1 = MessageBox.Show("Do you want to view this report.?", "Report View", MessageBoxButtons.YesNo);
                    //if (dr1 == DialogResult.Yes)
                        System.Diagnostics.Process.Start(PDFReport);
                    objRL.DeleteExcelFile();
                }
            }
            else
            {

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

        bool AmountFlag = false;
        protected void Fill_Merge_Cell(string Cell1, string Cell2, object val1, Excel.Worksheet myExcelWorksheet, string SetValue)
        {
            Cell1 = Cell1 + RowCount;
            Cell2 = Cell2 + RowCount;
            Excel.Range AlingRange1 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange1.Merge(val1);
            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            Excel.Range AlingRange2 = myExcelWorksheet.get_Range(Cell1, Cell2);

            if (AmountFlag == false)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            else
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            DrawBorder(AlingRange2);

            if (MH_Value == true)
            {
                AlingRange2.RowHeight = 40;
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                AlingRange2.WrapText = true;
            }
        }

        bool UpdateFlag = false;


        int ItemId_Insert = 0;
        string Rate_Insert = "", Quantity_Insert = "", Total_Insert = "", Discount_Insert = "", DiscountAmount_Insert = "", TaxableValue_Insert = "";
        string CGSTPercentage_Insert = "", CGSTAmount_Insert = "", SGSTPercentage_Insert = "", SGSTAmount_Insert = "", IGSTPercentage_Insert = "", IGSTAmount_Insert = "", CGSTPercentageRC_Insert = "";
        string CGSTAmountRC_Insert = "", SGSTPercentageRC_Insert = "", SGSTAmountRC_Insert = "", IGSTPercentageRC_Insert = "", IGSTAmountRC_Insert = "", NetAmount_Insert = "";

        private void ClearItemList()
        {
            Rate_Insert = ""; Quantity_Insert = ""; Total_Insert = ""; Discount_Insert = ""; DiscountAmount_Insert = ""; TaxableValue_Insert = "";
            CGSTPercentage_Insert = ""; CGSTAmount_Insert = ""; SGSTPercentage_Insert = ""; SGSTAmount_Insert = ""; IGSTPercentage_Insert = ""; IGSTAmount_Insert = "";
            CGSTPercentageRC_Insert = ""; CGSTAmountRC_Insert = ""; SGSTPercentageRC_Insert = ""; SGSTAmountRC_Insert = "";
            IGSTPercentageRC_Insert = ""; IGSTAmountRC_Insert = ""; NetAmount_Insert = "";
        }

        protected void SaveItemList()
        {
            for (int i = 0; i < dgvItem.Rows.Count; i++)
            {
                ClearItemList();

                ItemId_Insert = Convert.ToInt32(dgvItem.Rows[i].Cells["clmItemId"].Value.ToString());
                Rate_Insert = dgvItem.Rows[i].Cells["clmRate"].Value.ToString();
                Quantity_Insert = dgvItem.Rows[i].Cells["clmQty"].Value.ToString();
                Total_Insert = dgvItem.Rows[i].Cells["clmTotal"].Value.ToString();
                Discount_Insert = dgvItem.Rows[i].Cells["clmDiscount"].Value.ToString();
                DiscountAmount_Insert = dgvItem.Rows[i].Cells["clmDiscountAmount"].Value.ToString();
                TaxableValue_Insert = dgvItem.Rows[i].Cells["clmTaxableValue"].Value.ToString();

                CGSTPercentage_Insert = dgvItem.Rows[i].Cells["clmCGSTPer"].Value.ToString();
                //if(!string.IsNullOrEmpty(objRL.Check_NullString(Convert.ToString(dgvItem.Rows[i].Cells["clmCGSTAmount"].Value.ToString()))))
                CGSTAmount_Insert = Convert.ToString(Convert.ToDouble(objRL.Check_NullString(Convert.ToString(dgvItem.Rows[i].Cells["clmCGSTAmount"].Value))));

                SGSTPercentage_Insert = dgvItem.Rows[i].Cells["clmSGSTPer"].Value.ToString();
                SGSTAmount_Insert = dgvItem.Rows[i].Cells["clmSGSTAmount"].Value.ToString();

                IGSTPercentage_Insert = dgvItem.Rows[i].Cells["clmIGSTPer"].Value.ToString();
                IGSTAmount_Insert = dgvItem.Rows[i].Cells["clmIGSTAmount"].Value.ToString();

                CGSTPercentageRC_Insert = dgvItem.Rows[i].Cells["clmCGSTPerRC"].Value.ToString();
                CGSTAmountRC_Insert = dgvItem.Rows[i].Cells["clmCGSTAmountRC"].Value.ToString();

                SGSTPercentageRC_Insert = dgvItem.Rows[i].Cells["clmSGSTPerRC"].Value.ToString();
                SGSTAmountRC_Insert = dgvItem.Rows[i].Cells["clmSGSTAmountRC"].Value.ToString();

                IGSTPercentageRC_Insert = dgvItem.Rows[i].Cells["clmIGSTPerRC"].Value.ToString();
                IGSTAmountRC_Insert = dgvItem.Rows[i].Cells["clmIGSTAmountRC"].Value.ToString();

                NetAmount_Insert = dgvItem.Rows[i].Cells["clmNetAmount"].Value.ToString();

                if (DeleteFlag == false)
                {
                    objBL.Query = "insert into SaleTransaction(SaleId,ItemId,Rate,Quantity,Total,Discount,DiscountAmount,TaxableValue,CGSTPercentage,CGSTAmount,SGSTPercentage,SGSTAmount,IGSTPercentage,IGSTAmount,CGSTPercentageRC,CGSTAmountRC,SGSTPercentageRC,SGSTAmountRC,IGSTPercentageRC,IGSTAmountRC,NetAmount,UserId) values(" + TableId + "," + ItemId_Insert + ",'" + Rate_Insert + "','" + Quantity_Insert + "','" + Total_Insert + "','" + Discount_Insert + "','" + DiscountAmount_Insert + "','" + TaxableValue_Insert + "','" + CGSTPercentage_Insert + "','" + CGSTAmount_Insert + "','" + SGSTPercentage_Insert + "','" + SGSTAmount_Insert + "','" + IGSTPercentage_Insert + "','" + IGSTAmount_Insert + "','" + CGSTPercentageRC_Insert + "','" + CGSTAmountRC_Insert + "','" + SGSTPercentageRC_Insert + "','" + SGSTAmountRC_Insert + "','" + IGSTPercentageRC_Insert + "','" + IGSTAmountRC_Insert + "','" + NetAmount_Insert + "' ," + BusinessLayer.UserId_Static + ")";
                    objBL.Function_ExecuteNonQuery();
                }
            }
        }

        private void FillGrid()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select DC.ID,DC.InvoiceDate,DC.DCNo,DC.JobNo,DC.BillNo,DC.PONo,DC.CustomerId,C.CustomerName,C.Address,C.MobileNumber,C.EmailId,C.GSTNumber,C.StateCode,DC.NoteD,DC.TaxPaybleOnReverseCharge,DC.Total,DC.FreightCharges,DC.LoadingAndPackingCharges,DC.InsuranceCharges,DC.OtherCharges,DC.InvoiceTotal,DC.Naration,DC.PaymentMode,DC.BankId,DC.BankName,DC.AccountNumber,DC.TransactionDate,DC.ChequeNumber,DC.PartyBank,DC.PartyBankAccountNumber,DC.TransportationMode,DC.VehicalNumber,DC.DateOfSupply,DC.PlaceOfSupply,DC.BillStatus,DC.PrintFlag,DC.PrintCount,DC.CompanyId,DC.CompanyName from Sale DC inner join Customer C on C.ID=DC.CustomerId where DC.CancelTag=0 and C.CancelTag=0 and DC.CustomerId=" + CustomerID + " order by DC.InvoiceDate desc";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[7].Width = 120;
                dataGridView1.Columns[8].Width = 120;
                dataGridView1.Columns[9].Width = 120;
                dataGridView1.Columns[10].Width = 120;
                dataGridView1.Columns[11].Width = 120;
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
                    objBL.Query = "update Sale set CancelTag=1 where CancelTag=0 and ID=" + TableId + "";
                    objBL.Function_ExecuteNonQuery();

                    objBL.Query = "update SaleTransaction set CancelTag=1 where CancelTag=0 and POSID=" + TableId + "";
                    objBL.Function_ExecuteNonQuery();
                }
                else
                    return;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            Item objForm = new Item();
            objForm.ShowDialog(this);
        }

        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            ClearAll();
            dataGridView1.DataSource = null;
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

            lbCustomer.DataSource = null;
            dgvItem.Rows.Clear();
            dgvItem.Rows.Clear();

            Clear_Customer();
            txtSearchItem.Text = "";
            Clear_Item();
        }

        private void Clear_Item()
        {
            TempRowIndex = 0;
            GridFlag = false;
            btnDeleteGrid.Enabled = false;
            txtItemName.Text = "";
            txtItemDescription.Text = "";
            txtItemCode.Text = "";
            txtHSNCode.Text = "";
            txtUOM.Text = "";
            txtQuantity.Text = "";
            txtRate.Text = "";

            txtTotal.Text = "";
            txtDiscountPer.Text = "";

            txtDiscountAmount.Text = "";
            txtTaxableValue.Text = "";

            txtCGSTRate.Text = "";
            txtCGSTAmount.Text = "";

            txtSGSTRate.Text = "";
            txtSGSTAmount.Text = "";

            txtIGSTRate.Text = "";
            txtIGSTAmount.Text = "";

            txtCGSTRateRC.Text = "";
            txtCGSTRateRC.Text = "";

            txtSGSTRateRC.Text = "";
            txtSGSTAmountRC.Text = "";

            txtIGSTRateRC.Text = "";
            txtIGSTAmountRC.Text = "";

            txtNetAmount.Text = "";
        }

        private void Clear_Customer()
        {
            txtCustomerName.Text = "";
            txtDeliveryAddress.Text = "";
            txtEmailId.Text = "";
            txtPendingAmount.Text = "";
            txtContactNo.Text = "";
            txtPendingAmount.Text = "";
            txtGSTNo.Text = "";
        }

        private void Fill_Customer_ListBox()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,CustomerCode,CustomerName,Address,MobileNumber,EmailId,GSTNumber,StateCode from Customer where CancelTag=0 and CustomerName like '%" + txtSearchCustomer.Text + "%' order by CustomerName desc";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbCustomer.Visible = true;
                lbCustomer.DataSource = ds.Tables[0];
                lbCustomer.DisplayMember = "CustomerName";
                lbCustomer.ValueMember = "ID";
            }
        }

        private void Fill_Customer_Details()
        {
            if (CustomerID != 0)
            {
                DataSet ds = new DataSet();
                objBL.Query = "select ID,CustomerCode,CustomerName,Address,MobileNumber,EmailId,GSTNumber,StateCode from Customer where CancelTag=0 and ID=" + CustomerID + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbCustomer.Visible = false;
                    CustomerID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    txtCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    txtDeliveryAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    txtEmailId.Text = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    txtContactNo.Text = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                    //txtPendingAmount.Text = ds.Tables[0].Rows[0]["VAT"].ToString();
                    txtGSTNo.Text = ds.Tables[0].Rows[0]["GSTNumber"].ToString();
                    FillGrid();
                    txtSearchItem.Focus();
                }
            }
        }

        
        private void lbCustomer_Click(object sender, EventArgs e)
        {
            if (lbCustomer.Items.Count > 0)
            {
                CustomerID = Convert.ToInt32(lbCustomer.SelectedValue.ToString());
                Fill_Customer_Details();
            }
        }

        private void lbCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Fill_Customer_Details();
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

        private void Fill_Items_Details()
        {
            if (lbItem.Items.Count > 0)
            {
                DataSet ds = new DataSet();
                //objBL.Query = "select I.ID,I.ItemName,I.ItemCode,I.HSNCode,I.Description,I.UOMID,U.UnitOfMessurement from Item I inner join UOM U on U.ID=I.UOMID where I.CancelTag=0 and U.CancelTag=0 and I.ItemName='" + lbItem.Text + "' and I.ID=" + lbItem.SelectedValue + "";
                objBL.Query = "select I.ID,I.ManufracturerId,M.ManufracturerName,I.ItemName,I.BatchNumber,I.HSNCode,I.UOM,I.Price,I.Cost,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from Item I inner join Manufracturer M on I.ManufracturerId=M.ID where I.CancelTag=0 and I.ID=" + lbItem.SelectedValue + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbItem.Visible = false;
                    txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                    txtUOM.Text = ds.Tables[0].Rows[0]["UOM"].ToString();
                    ItemID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    txtItemDescription.Text = ds.Tables[0].Rows[0]["ManufracturerName"].ToString();
                    txtItemCode.Text = ds.Tables[0].Rows[0]["BatchNumber"].ToString();
                    txtHSNCode.Text = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                    txtRate.Focus();
                }
            }
        }

        private void Fill_ItemDetails_by_ID()
        {
            DataSet ds = new DataSet();
            //objBL.Query = "select I.ID,I.ItemName,I.ItemCode,I.HSNCode,I.Description,I.UOMID,U.UnitOfMessurement from Item I inner join UOM U on U.ID=I.UOMID where I.CancelTag=0 and U.CancelTag=0 and I.ID=" + ItemID + "";
            objBL.Query = "select I.ID,I.ManufracturerId,M.ManufracturerName,I.ItemName,I.BatchNumber,I.HSNCode,I.UOM,I.Price,I.Cost,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from Item I inner join Manufracturer M on I.ManufracturerId=M.ID where I.CancelTag=0 and I.ID=" + ItemID + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbItem.Visible = false;
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                txtUOM.Text = ds.Tables[0].Rows[0]["UOM"].ToString();
                ItemID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                txtItemDescription.Text = ds.Tables[0].Rows[0]["ManufracturerName"].ToString();
                txtItemCode.Text = ds.Tables[0].Rows[0]["BatchNumber"].ToString();
                txtHSNCode.Text = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                txtQuantity.Focus();
            }
        }

        private void txtSearchItemId_TextChanged(object sender, EventArgs e)
        {
            Clear_Item();
            txtSearchItem.Text = "";
            if (txtSearchItemId.Text != "")
            {
                ItemID = Convert.ToInt32(txtSearchItemId.Text);
                Fill_ItemDetails_by_ID();
            }
            else
            {
                lbItem.Visible = false;
            }
        }

        private void txtSearchItemId_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtSearchItemId);
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtQuantity);
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtItemCode);
        }


        private void txtSearchItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearchItem.Text != "" && lbItem.Items.Count > 0)
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                    lbItem.Focus();
            }
        }

        private void btnClearItem_Click(object sender, EventArgs e)
        {
            Clear_Item();
        }

        private bool Validation_Item()
        {
            objEP.Clear();
            if (txtItemName.Text == "")
            {
                objEP.SetError(txtItemName, "Select Item");
                txtSearchItem.Focus();
                return true;
            }
            else if (txtUOM.Text == "")
            {
                objEP.SetError(txtUOM, "Select Item");
                txtSearchItem.Focus();
                return true;
            }
            else if (ItemID == 0)
            {
                objEP.SetError(txtItemName, "Select Item");
                txtSearchItem.Focus();
                return true;
            }
            else
                return false;
        }

        private void btnAddToGrid_Click(object sender, EventArgs e)
        {
            if (!Validation_Item())
            {
                if (GridFlag == false)
                    dgvItem.Rows.Add();

                dgvItem.Rows[dgvItemRow].Cells["clmItemId"].Value = ItemID.ToString();
                dgvItem.Rows[dgvItemRow].Cells["clmItemName"].Value = txtItemName.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmItemCode"].Value = txtItemCode.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmHSNCode"].Value = txtHSNCode.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmUOM"].Value = txtUOM.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmQty"].Value = txtQuantity.Text;

                dgvItem.Rows[dgvItemRow].Cells["clmRate"].Value = txtRate.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmTotal"].Value = txtTotal.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmDiscount"].Value = txtDiscountPer.Text;

                dgvItem.Rows[dgvItemRow].Cells["clmDiscountAmount"].Value = txtDiscountAmount.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmTaxableValue"].Value = txtTaxableValue.Text;

                dgvItem.Rows[dgvItemRow].Cells["clmCGSTPer"].Value = txtCGSTRate.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmCGSTAmount"].Value = txtCGSTAmount.Text;

                dgvItem.Rows[dgvItemRow].Cells["clmSGSTPer"].Value = txtSGSTRate.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmSGSTAmount"].Value = txtSGSTAmount.Text;

                dgvItem.Rows[dgvItemRow].Cells["clmIGSTPer"].Value = txtIGSTRate.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmIGSTAmount"].Value = txtIGSTAmount.Text;

                dgvItem.Rows[dgvItemRow].Cells["clmCGSTPerRC"].Value = txtCGSTRateRC.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmCGSTAmountRC"].Value = txtCGSTAmountRC.Text;

                dgvItem.Rows[dgvItemRow].Cells["clmSGSTPerRC"].Value = txtSGSTRateRC.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmSGSTAmountRC"].Value = txtSGSTAmountRC.Text;

                dgvItem.Rows[dgvItemRow].Cells["clmIGSTPerRC"].Value = txtIGSTRateRC.Text;
                dgvItem.Rows[dgvItemRow].Cells["clmIGSTAmountRC"].Value = txtIGSTAmountRC.Text;

                dgvItem.Rows[dgvItemRow].Cells["clmNetAmount"].Value = txtNetAmount.Text;

                SrNo_Add();

                if (GridFlag == true)
                    dgvItemRow = TempRowIndex;
                else
                    dgvItemRow++;

                Clear_Item();
                Calculate_NetAmount();
                Calculate_InvoiceTotal();
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

        private void dgvItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TempRowIndex = dgvItemRow;
            dgvItemRow = e.RowIndex;
            GridFlag = true;
            btnDeleteGrid.Enabled = true;
            ItemID = Convert.ToInt32(dgvItem.Rows[dgvItemRow].Cells["clmItemId"].Value.ToString());
            txtItemName.Text = dgvItem.Rows[dgvItemRow].Cells["clmItemName"].Value.ToString();
            txtItemCode.Text = dgvItem.Rows[dgvItemRow].Cells["clmItemCode"].Value.ToString();
            txtHSNCode.Text = dgvItem.Rows[dgvItemRow].Cells["clmHSNCode"].Value.ToString();
            txtUOM.Text = dgvItem.Rows[dgvItemRow].Cells["clmUOM"].Value.ToString();
            txtQuantity.Text = dgvItem.Rows[dgvItemRow].Cells["clmQty"].Value.ToString();


            txtRate.Text = dgvItem.Rows[dgvItemRow].Cells["clmRate"].Value.ToString();

            txtTotal.Text = dgvItem.Rows[dgvItemRow].Cells["clmTotal"].Value.ToString();
            txtDiscountPer.Text = dgvItem.Rows[dgvItemRow].Cells["clmDiscount"].Value.ToString();

            txtDiscountAmount.Text = dgvItem.Rows[dgvItemRow].Cells["clmDiscountAmount"].Value.ToString();
            txtTaxableValue.Text = dgvItem.Rows[dgvItemRow].Cells["clmTaxableValue"].Value.ToString();

            txtCGSTRate.Text = dgvItem.Rows[dgvItemRow].Cells["clmCGSTPer"].Value.ToString();
            if (!string.IsNullOrEmpty(Convert.ToString(dgvItem.Rows[dgvItemRow].Cells["clmCGSTAmount"].Value)))
                txtCGSTAmount.Text = dgvItem.Rows[dgvItemRow].Cells["clmCGSTAmount"].Value.ToString();

            txtSGSTRate.Text = dgvItem.Rows[dgvItemRow].Cells["clmSGSTPer"].Value.ToString();
            txtSGSTAmount.Text = dgvItem.Rows[dgvItemRow].Cells["clmSGSTAmount"].Value.ToString();

            txtIGSTRate.Text = dgvItem.Rows[dgvItemRow].Cells["clmIGSTPer"].Value.ToString();
            txtIGSTAmount.Text = dgvItem.Rows[dgvItemRow].Cells["clmIGSTAmount"].Value.ToString();

            txtCGSTRateRC.Text = dgvItem.Rows[dgvItemRow].Cells["clmCGSTPerRC"].Value.ToString();
            txtCGSTRateRC.Text = dgvItem.Rows[dgvItemRow].Cells["clmCGSTAmountRC"].Value.ToString();

            txtSGSTRateRC.Text = dgvItem.Rows[dgvItemRow].Cells["clmSGSTPerRC"].Value.ToString();
            txtSGSTAmountRC.Text = dgvItem.Rows[dgvItemRow].Cells["clmSGSTAmountRC"].Value.ToString();

            txtIGSTRateRC.Text = dgvItem.Rows[dgvItemRow].Cells["clmIGSTPerRC"].Value.ToString();
            txtIGSTAmountRC.Text = dgvItem.Rows[dgvItemRow].Cells["clmIGSTAmountRC"].Value.ToString();

            txtNetAmount.Text = dgvItem.Rows[dgvItemRow].Cells["clmNetAmount"].Value.ToString();
        }

        private void txtSearchItem_TextChanged(object sender, EventArgs e)
        {
            Clear_Item();

            txtSearchItemId.Text = "";
            if (txtSearchItem.Text != "")
                Fill_Item_ListBox();
            else
                lbItem.Visible = false;
        }

        private void Fill_Item_ListBox()
        {
            DataSet ds = new DataSet();
            //objBL.Query = "select I.ID,I.ItemName,I.ItemCode,I.HSNCode,I.Description,I.UOMID,U.UnitOfMessurement from Item I inner join UOM U on U.ID=I.UOMID where I.CancelTag=0 and U.CancelTag=0 and I.ItemName like '%" + txtSearchItem.Text + "%' order by ItemName desc";

            objBL.Query = "select I.ID,I.ManufracturerId,M.ManufracturerName,I.ItemName,I.BatchNumber,I.HSNCode,I.UOM,I.Price,I.Cost,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from Item I inner join Manufracturer M on I.ManufracturerId=M.ID where I.CancelTag=0 and I.ItemName like '%" + txtSearchItem.Text + "%'";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbItem.Visible = true;
                lbItem.DataSource = ds.Tables[0];
                lbItem.DisplayMember = "ItemName";
                lbItem.ValueMember = "ID";
            }
            else
                lbItem.DataSource = null;
        }

        private void CalculateFinalAmount()
        {
            //double.TryParse(txtNetAmount.Text, out NetAmount);
            //double.TryParse(txtVATPerBill.Text, out VATPerBill);
            //double.TryParse(txtDiscount.Text, out Discount);
            //double.TryParse(txtTransportationCharges.Text, out TransportationCharges);
            //double.TryParse(txtOtherCharges.Text, out OtherCharges);
            //double CalVat = NetAmount * VATPerBill / 100;
            //VATAmount = CalVat;
            //FinalBillAmount = NetAmount + CalVat - Discount + TransportationCharges + OtherCharges;
            //txtFinalBillAmount.Text = FinalBillAmount.ToString();
        }

        private void txtNetAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateFinalAmount();
        }

        private void txtVATPerBill_TextChanged(object sender, EventArgs e)
        {
            CalculateFinalAmount();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateFinalAmount();
        }

        private void txtTransportationCharges_TextChanged(object sender, EventArgs e)
        {
            CalculateFinalAmount();
        }

        private void txtOtherCharges_TextChanged(object sender, EventArgs e)
        {
            Calculate_InvoiceTotal();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //0 DC.ID,
                //1 DC.TDate as [Date],
                //2 DC.PONo,
                //3 DC.DCNo,
                //4 DC.JobNo,
                //5 DC.BillNo,
                //6 DC.CustomerId,
                //7 C.CustomerName,
                //8 C.Address,
                //9 C.MobileNumber,
                //10 C.EmailId,
                //11 C.GSTNumber,
                //12 C.StateCode,
                //13 DC.NoteD
                //14 DC.TaxPaybleOnReverseCharge,
                //15 DC.Total,
                //16 DC.FreightCharges,
                //17 DC.LoadingAndPackingCharges,
                //18 DC.InsuranceCharges,
                //19 DC.OtherCharges,
                //20 DC.InvoiceTotal,
                //21 DC.Naration,
                //22 DC.PaymentMode,
                //23 DC.BankId,
                //24 DC.BankName,
                //25 DC.AccountNumber,
                //26 DC.TransactionDate,
                //27 DC.ChequeNumber,
                //28 DC.PartyBank,
                //29 DC.PartyBankAccountNumber,
                //30 DC.TransportationMode,
                //31 DC.VehicalNumber,
                //32 DC.DateOfSupply,
                //33 DC.PlaceOfSupply,
                //34 DC.BillStatus,
                //35 DC.PrintFlag,
                //36 DC.PrintCount 

                ClearAll();
                btnDelete.Enabled = true;
                btnDeliveryChallan.Visible = true;

                TableId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtDCID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                txtDCNo.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtJobNo.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtBillNo.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtPONo.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                
                CustomerID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                Fill_Customer_Details();

                txtNote.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                cmbTaxIsPayableOnReverseCharge.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                txtTotalBill.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                txtFreightCharges.Text = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
                txtLoadingAndPackingCharges.Text = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
                txtInsuranceCharges.Text = dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString();
                txtOtherCharges.Text = dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString();
                txtInvoiceTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[20].Value.ToString();

                txtNaration.Text = dataGridView1.Rows[e.RowIndex].Cells[21].Value.ToString();
                cmbPaymentMode.Text = dataGridView1.Rows[e.RowIndex].Cells[22].Value.ToString();
                cmbBankName.Text = dataGridView1.Rows[e.RowIndex].Cells[24].Value.ToString();
                txtAccountNo.Text = dataGridView1.Rows[e.RowIndex].Cells[25].Value.ToString();

                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[26].Value)))
                    dtpTransactionDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[26].Value.ToString());
                else
                    dtpTransactionDate.Value = DateTime.Now.Date;

                txtChequeNo.Text = dataGridView1.Rows[e.RowIndex].Cells[27].Value.ToString();
                txtBankParty.Text = dataGridView1.Rows[e.RowIndex].Cells[28].Value.ToString();
                txtAccountNoParty.Text = dataGridView1.Rows[e.RowIndex].Cells[29].Value.ToString();

                txtTransportationMode.Text = dataGridView1.Rows[e.RowIndex].Cells[30].Value.ToString();
                txtVehicalNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[31].Value.ToString();
                dtpDateOfSupply.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[32].Value.ToString());
                txtPlaceOfSupply.Text = dataGridView1.Rows[e.RowIndex].Cells[33].Value.ToString();
                cmbCompanyName.Text = dataGridView1.Rows[e.RowIndex].Cells[34].Value.ToString();
                Fill_Item_List();
                Calculate_NetAmount();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        public string Return_UOM(int ID_UOM)
        {
            string UOMString = string.Empty;

            DataSet ds = new DataSet();
            objBL.Query = "select ID,UnitOfMessurement from UOM where ID=" + ID_UOM + " and CancelTag=0";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                UOMString = Convert.ToString(ds.Tables[0].Rows[0][1].ToString());
            }

            return UOMString;
        }

        protected void Fill_Item_List()
        {
            if (TableId != 0)
            {
                ClearItemList();
                dgvItem.Rows.Clear();
                DataSet ds = new DataSet();
                //objBL.Query = "select POST.ID,POST.POSId,PTM.MaterialId,MM.MaterialName,PTM.Unit,PTM.Qty,PTM.Rate,PTM.Amount from SalesTransactionMaterial PTM inner join MaterialMaster MM on MM.ID=PTM.MaterialId where PTM.STID=" + TableID + "";
                objBL.Query = "select BI.ID,BI.SaleId,BI.ItemId,I.ItemName,I.BatchNumber,I.HSNCode,I.UOM,I.Cost,I.Price,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST,BI.Rate,BI.Quantity,BI.Total,BI.Discount,BI.DiscountAmount,BI.TaxableValue,BI.CGSTPercentage,BI.CGSTAmount,BI.SGSTPercentage,BI.SGSTAmount,BI.IGSTPercentage,BI.IGSTAmount,BI.CGSTPercentageRC,BI.CGSTAmountRC,BI.SGSTPercentageRC,BI.SGSTAmountRC,BI.IGSTPercentageRC,BI.IGSTAmountRC,BI.NetAmount from SaleTransaction BI inner join Item I on I.ID=BI.ItemId where BI.CancelTag=0 and BI.SaleId=" + TableId + "";
                //objBL.Query = "select I.ID,I.ManufracturerId,M.ManufracturerName as [Manufracturer Name],I.ItemName as [Item Name],I.BatchNumber,I.HSNCode,I.UOM,I.Cost,I.Price,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from Item I inner join Manufracturer M on I.ManufracturerId=M.ID where I.CancelTag=0";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgvItemRow = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvItem.Rows.Add();
                        dgvItem.Rows[dgvItemRow].Cells["clmItemId"].Value = ds.Tables[0].Rows[i]["ItemId"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmItemName"].Value = ds.Tables[0].Rows[i]["ItemName"].ToString();
                        //dgvItem.Rows[dgvItemRow].Cells["clmItemDescription"].Value = ds.Tables[0].Rows[i]["ItemDescription"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmItemCode"].Value = ds.Tables[0].Rows[i]["BatchNumber"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmHSNCode"].Value = ds.Tables[0].Rows[i]["HSNCode"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmUOM"].Value =  ds.Tables[0].Rows[i]["UOM"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmQty"].Value = ds.Tables[0].Rows[i]["Quantity"].ToString();

                        dgvItem.Rows[dgvItemRow].Cells["clmRate"].Value = ds.Tables[0].Rows[i]["Rate"].ToString();

                        dgvItem.Rows[dgvItemRow].Cells["clmTotal"].Value = ds.Tables[0].Rows[i]["Total"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmDiscount"].Value = ds.Tables[0].Rows[i]["Discount"].ToString();

                        dgvItem.Rows[dgvItemRow].Cells["clmDiscountAmount"].Value = ds.Tables[0].Rows[i]["DiscountAmount"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmTaxableValue"].Value = ds.Tables[0].Rows[i]["TaxableValue"].ToString();

                        dgvItem.Rows[dgvItemRow].Cells["clmCGSTPer"].Value = ds.Tables[0].Rows[i]["CGSTPercentage"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmCGSTAmount"].Value = ds.Tables[0].Rows[i]["CGSTAmount"].ToString();

                        dgvItem.Rows[dgvItemRow].Cells["clmSGSTPer"].Value = ds.Tables[0].Rows[i]["SGSTPercentage"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmSGSTAmount"].Value = ds.Tables[0].Rows[i]["SGSTAmount"].ToString();

                        dgvItem.Rows[dgvItemRow].Cells["clmIGSTPer"].Value = ds.Tables[0].Rows[i]["IGSTPercentage"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmIGSTAmount"].Value = ds.Tables[0].Rows[i]["IGSTAmount"].ToString();

                        dgvItem.Rows[dgvItemRow].Cells["clmCGSTPerRC"].Value = ds.Tables[0].Rows[i]["CGSTPercentageRC"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmCGSTAmountRC"].Value = ds.Tables[0].Rows[i]["CGSTAmountRC"].ToString();

                        dgvItem.Rows[dgvItemRow].Cells["clmSGSTPerRC"].Value = ds.Tables[0].Rows[i]["SGSTPercentageRC"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmSGSTAmountRC"].Value = ds.Tables[0].Rows[i]["SGSTAmountRC"].ToString();

                        dgvItem.Rows[dgvItemRow].Cells["clmIGSTPerRC"].Value = ds.Tables[0].Rows[i]["IGSTPercentageRC"].ToString();
                        dgvItem.Rows[dgvItemRow].Cells["clmIGSTAmountRC"].Value = ds.Tables[0].Rows[i]["IGSTAmountRC"].ToString();

                        dgvItem.Rows[dgvItemRow].Cells["clmNetAmount"].Value = ds.Tables[0].Rows[i]["NetAmount"].ToString();

                        dgvItemRow++;
                    }
                    SrNo_Add();
                }
            }
        }

        private void btnDeleteGrid_Click(object sender, EventArgs e)
        {
            if (txtItemName.Text != "")
            {
                DialogResult dr;
                dr = MessageBox.Show("Do you want to delete this Material.?", "Delete", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    dgvItem.Rows.RemoveAt(dgvItemRow);
                    Clear_Item();
                    if (dgvItem.Rows.Count > 0)
                        dgvItemRow = dgvItem.Rows.Count;
                    else
                        dgvItemRow = 0;
                    SrNo_Add();
                }
            }
        }

        bool FlagDeliveryChallan = false;
        private void btnDeliveryChallan_Click(object sender, EventArgs e)
        {
            DialogResult dr1;
            dr1 = MessageBox.Show("Do you want to generate Delivery Challan.?", "Report View", MessageBoxButtons.YesNo);
            if (dr1 == DialogResult.Yes)
            {
                FlagDeliveryChallan = true;
                ExcelReport();
            }
        }

        

        private void Calculate_NetAmount()
        {
            double.TryParse(txtRate.Text, out Rate);
            double.TryParse(txtQuantity.Text, out Qty);
            double.TryParse(txtDiscountPer.Text, out DiscountPercentage);

            double.TryParse(txtCGSTRate.Text, out CGSTPer);
            double.TryParse(txtSGSTRate.Text, out SGSTPer);
            double.TryParse(txtIGSTRate.Text, out IGSTPer);

            double.TryParse(txtCGSTRateRC.Text, out CGSTPerRC);
            double.TryParse(txtSGSTRateRC.Text, out SGSTPerRC);
            double.TryParse(txtIGSTRateRC.Text, out IGSTPerRC);

            Total = Rate * Qty;
            txtTotal.Text = Total.ToString();

            DiscountAmount = Total * DiscountPercentage / 100;
            txtDiscountAmount.Text = DiscountAmount.ToString();

            TaxableValue = Total - DiscountAmount;
            txtTaxableValue.Text = TaxableValue.ToString();

            CGSTAmount = TaxableValue * CGSTPer / 100;
            txtCGSTAmount.Text = CGSTAmount.ToString();

            SGSTAmount = TaxableValue * SGSTPer / 100;
            txtSGSTAmount.Text = SGSTAmount.ToString();

            IGSTAmount = TaxableValue * IGSTPer / 100;
            txtIGSTAmount.Text = IGSTAmount.ToString();

            CGSTAmountRC = TaxableValue * CGSTPerRC / 100;
            txtCGSTAmountRC.Text = CGSTAmountRC.ToString();

            SGSTAmountRC = TaxableValue * SGSTPerRC / 100;
            txtSGSTAmountRC.Text = SGSTAmountRC.ToString();

            IGSTAmountRC = TaxableValue * IGSTPerRC / 100;
            txtIGSTAmountRC.Text = IGSTAmountRC.ToString();

            NetAmount = TaxableValue + CGSTAmount + SGSTAmount + IGSTAmount;
            txtNetAmount.Text = NetAmount.ToString();

            double TotalBill = 0;
            for (int i = 0; i < dgvItem.Rows.Count; i++)
            {
                TotalBill += Convert.ToDouble(dgvItem.Rows[i].Cells["clmNetAmount"].Value.ToString());
            }
            txtTotalBill.Text = TotalBill.ToString();
            Calculate_InvoiceTotal();
        }

        private void txtCGSTRate_TextChanged(object sender, EventArgs e)
        {
            Calculate_NetAmount();
        }

        private void txtSGSTRate_TextChanged(object sender, EventArgs e)
        {
            Calculate_NetAmount();
        }

        private void txtIGSTRate_TextChanged(object sender, EventArgs e)
        {
            Calculate_NetAmount();
        }

        private void txtDiscountPer_TextChanged(object sender, EventArgs e)
        {
            Calculate_NetAmount();
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            Calculate_NetAmount();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            Calculate_NetAmount();
        }

        private void txtCGSTRateRC_TextChanged(object sender, EventArgs e)
        {
            Calculate_NetAmount();
        }

        private void txtSGSTRateRC_TextChanged(object sender, EventArgs e)
        {
            Calculate_NetAmount();
        }

        private void txtIGSTRateRC_TextChanged(object sender, EventArgs e)
        {
            Calculate_NetAmount();
        }

        private void txtCGSTRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtCGSTRate);
        }

        private void txtSGSTRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtSGSTRate);
        }

        private void txtIGSTRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtIGSTRate);
        }

        private void txtDiscountPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtDiscountPer);
        }

        private void txtCGSTRateRC_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtCGSTRateRC);
        }

        private void txtSGSTRateRC_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtSGSTRateRC);
        }

        private void txtIGSTRateRC_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtIGSTRateRC);
        }

        private void txtFreightCharges_TextChanged(object sender, EventArgs e)
        {
            Calculate_InvoiceTotal();
        }

        private void txtLoadingAndPackingCharges_TextChanged(object sender, EventArgs e)
        {
            Calculate_InvoiceTotal();
        }

        private void txtInsuranceCharges_TextChanged(object sender, EventArgs e)
        {
            Calculate_InvoiceTotal();
        }

        double TotalBill = 0, FreightCharges = 0, LoadingAndPackingCharges = 0, InsuranceCharges = 0, OtherCharges = 0, InvoiceTotal = 0;

        private void Calculate_InvoiceTotal()
        {
            double.TryParse(txtTotalBill.Text, out TotalBill);
            double.TryParse(txtFreightCharges.Text, out FreightCharges);
            double.TryParse(txtLoadingAndPackingCharges.Text, out LoadingAndPackingCharges);
            double.TryParse(txtInsuranceCharges.Text, out InsuranceCharges);
            double.TryParse(txtOtherCharges.Text, out OtherCharges);

            InvoiceTotal = TotalBill + FreightCharges + LoadingAndPackingCharges + InsuranceCharges + OtherCharges;
            txtInvoiceTotal.Text = InvoiceTotal.ToString();
        }

        private void cmbPaymentMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            objRL.Set_PaymentMode_Details(cmbPaymentMode, gbChequeDetails, lblChequeNo, txtChequeNo);

            //if (cmbPaymentMode.SelectedIndex > -1)
            //{
            //    gbNEFTDetails.Visible = false;
            //    gbChequeDetails.Visible = false;

            //    if (cmbPaymentMode.Text == "NEFT" || cmbPaymentMode.Text == "RTGS")
            //    {
            //        gbNEFTDetails.Visible = true;
            //        gbChequeDetails.Visible = false;
            //    }
            //    if (cmbPaymentMode.Text == "CHEQUE")
            //    {
            //        gbNEFTDetails.Visible = false;
            //        gbChequeDetails.Visible = true;
            //    }
            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            CompanyInformation objForm = new CompanyInformation();
            objForm.ShowDialog(this);
            Fill_Company();

        }

        private void Fill_Company()
        {
            objBL.Query = "select ID,CompanyName from CompanyInformation where CancelTag=0";
            objBL.FillComboBox(cmbCompanyName, "CompanyName", "ID");
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                txtDCID.Focus();
        }

        private void txtDCID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDCNo.Focus();
        }

        private void txtDCNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtJobNo.Focus();
        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtBillNo.Focus();
        }

        private void txtBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPONo.Focus();
        }

        private void txtPONo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbCompanyName.Focus();
        }

        private void cmbCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSearchCustomer.Focus();
        }

        private void txtSearchCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearchCustomer.Text != "" && lbCustomer.Items.Count > 0)
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                    lbCustomer.Focus();
            }
        }

        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQuantity.Focus();
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDiscountPer.Focus();
        }

        private void txtDiscountPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCGSTRate.Focus();
        }

        private void txtCGSTRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSGSTRate.Focus();
        }

        private void txtSGSTRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtIGSTRate.Focus();
        }

        private void txtIGSTRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCGSTRateRC.Focus();
        }

        private void txtCGSTRateRC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSGSTRateRC.Focus();
        }

        private void txtSGSTRateRC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtIGSTRateRC.Focus();
        }

        private void txtIGSTRateRC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddToGrid.Focus();
        }

        private void btnAddToGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSearchItem.Focus();
        }

        private void btnClearItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnDeleteGrid.Focus();
        }

        private void btnDeleteGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQuantity.Focus();
        }

        private void cmbTaxIsPayableOnReverseCharge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQuantity.Focus();
        }

        private void txtTotalBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQuantity.Focus();
        }

        private void txtFreightCharges_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtLoadingAndPackingCharges.Focus();

        }

        private void txtLoadingAndPackingCharges_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtInsuranceCharges.Focus();
        }

        private void txtInsuranceCharges_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtOtherCharges.Focus();
        }

        private void txtOtherCharges_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtInvoiceTotal.Focus();
        }

        private void txtInvoiceTotal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbPaymentMode.Focus();
        }

        private void cmbPaymentMode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbPaymentMode.Text == "CASH" || cmbPaymentMode.Text == "CREDIT")
                    txtNote.Focus();
                else
                    cmbBankName.Focus();
            }
        }

        private void cmbBankName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAccountNo.Focus();
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpTransactionDate.Focus();
        }

        private void dtpTransactionDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtChequeNo.Focus();
        }

        private void txtChequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtBankParty.Focus();
        }

        private void txtBankParty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAccountNoParty.Focus();
        }

        private void txtAccountNoParty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNote.Focus();
        }

        private void txtNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTransportationMode.Focus();
        }

        private void txtTransportationMode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtVehicalNumber.Focus();
        }

        private void txtVehicalNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpDateOfSupply.Focus();
        }

        private void dtpDateOfSupply_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPlaceOfSupply.Focus();
        }

        private void txtPlaceOfSupply_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNaration.Focus();
        }

        private void txtNaration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        private void txtFreightCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtFreightCharges);
        }

        private void txtLoadingAndPackingCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtLoadingAndPackingCharges);
        }

        private void txtInsuranceCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtInsuranceCharges);
        }

        private void txtOtherCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtOtherCharges);
        }

        private void txtInvoiceTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtInvoiceTotal);
        }

        private void cmbBankName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_BankDetails();
        }

        private void Fill_BankDetails()
        {
            if (cmbBankName.SelectedIndex > -1)
            {
                objRL.GetBankDetails(Convert.ToInt32(cmbBankName.SelectedValue.ToString()));
                txtAccountNo.Text = RedundancyLogics.AccountNo;
            }
        }
    }
}
