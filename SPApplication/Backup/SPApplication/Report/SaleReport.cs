using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using Microsoft.Office.Interop.Excel;

namespace SPApplication.Reports
{
    public partial class SaleReport : Form
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

        int RowCount = 18, SRNO = 1;
        bool MH_Value = false;
        Microsoft.Office.Interop.Excel.Application myExcelApp;
        Microsoft.Office.Interop.Excel.Workbooks myExcelWorkbooks;
        Microsoft.Office.Interop.Excel.Workbook myExcelWorkbook;

        public SaleReport()
        {
            InitializeComponent();
            objBL.Set_Report_Design(this, lblHeader, btnReport, btnClear, btnExit, "Sales Report");
        }


        private void SaleReport_Load(object sender, EventArgs e)
        {
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            objRL.Fill_Customer(cmbCustomerName);
            ClearAll();
        }

        public void ClearAll()
        {
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            chkCustomer.Checked = true;
            cbToday.Checked = true;
            dtpFromDate.Focus();

        }
        int AFlag = 0; DateTime dtString;

        string DateString = "";
        double InvoiceTotalAmount = 0, TotalAmount = 0,  OtherChargesTotal = 0;

        string BillTypeOther = string.Empty;

        string OtherCharges_R = "";

        private void ClearReportValues()
        {
            InvoiceTotalAmount = 0; TotalAmount = 0; OtherChargesTotal = 0;
        }

        private void ExcelReport()
        {
            InvoiceTotalAmount = 0;

            DataSet dsSales = new DataSet();
            object misValue = System.Reflection.Missing.Value;

            //if (cbAll.Checked)
            //    BillTypeOther = "";
            //else
            //{
            //    if (cmbBillType.Text == "None")
            //        BillTypeOther = " and S.IsGST='" + cmbBillType.Text + "'";
            //    else
            //        BillTypeOther = " and S.IsGST='" + cmbBillType.Text + "'";
            //}

            if (chkCustomer.Checked)
//                objBL.Query = "select S.CustomerId,S.ID,S.InvoiceDate,C.CustomerName,S.Total,S.Discount,S.OtherCharges,S.NetAmount,S.IsGST,S.CGSTAmount,S.SGSTAmount,S.IGSTAmount,S.InvoiceTotal,S.PaymentMode,S.UserId from Sale S inner join Customer C on C.ID=S.CustomerId where S.CancelTag=0 " + BillTypeOther + "  and C.CancelTag=0 and S.InvoiceDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# order by S.InvoiceDate desc";
                objBL.Query = "select DC.ID,DC.InvoiceDate,DC.DCNo,DC.JobNo,DC.BillNo,DC.PONo,DC.CustomerId,C.CustomerName,C.Address,C.MobileNumber,C.EmailId,C.GSTNumber,C.StateCode,DC.NoteD,DC.TaxPaybleOnReverseCharge,DC.Total,DC.FreightCharges,DC.LoadingAndPackingCharges,DC.InsuranceCharges,DC.OtherCharges,DC.InvoiceTotal,DC.Naration,DC.PaymentMode,DC.BankId,DC.BankName,DC.AccountNumber,DC.TransactionDate,DC.ChequeNumber,DC.PartyBank,DC.PartyBankAccountNumber,DC.TransportationMode,DC.VehicalNumber,DC.DateOfSupply,DC.PlaceOfSupply,DC.BillStatus,DC.PrintFlag,DC.PrintCount,DC.CompanyId,DC.CompanyName from Sale DC inner join Customer C on C.ID=DC.CustomerId where DC.CancelTag=0 and C.CancelTag=0 and DC.InvoiceDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# order by DC.InvoiceDate desc"; 
            else
                objBL.Query = "select DC.ID,DC.InvoiceDate,DC.DCNo,DC.JobNo,DC.BillNo,DC.PONo,DC.CustomerId,C.CustomerName,C.Address,C.MobileNumber,C.EmailId,C.GSTNumber,C.StateCode,DC.NoteD,DC.TaxPaybleOnReverseCharge,DC.Total,DC.FreightCharges,DC.LoadingAndPackingCharges,DC.InsuranceCharges,DC.OtherCharges,DC.InvoiceTotal,DC.Naration,DC.PaymentMode,DC.BankId,DC.BankName,DC.AccountNumber,DC.TransactionDate,DC.ChequeNumber,DC.PartyBank,DC.PartyBankAccountNumber,DC.TransportationMode,DC.VehicalNumber,DC.DateOfSupply,DC.PlaceOfSupply,DC.BillStatus,DC.PrintFlag,DC.PrintCount,DC.CompanyId,DC.CompanyName from Sale DC inner join Customer C on C.ID=DC.CustomerId where DC.CancelTag=0 and C.CancelTag=0 and DC.CustomerId=" + cmbCustomerName.SelectedValue + " and DC.InvoiceDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# order by DC.InvoiceDate desc"; 
                //objBL.Query = "select S.CustomerId,S.ID,S.InvoiceDate,C.CustomerName,S.Total,S.Discount,S.OtherCharges,S.NetAmount,S.IsGST,S.CGSTAmount,S.SGSTAmount,S.IGSTAmount,S.InvoiceTotal,S.PaymentMode,S.UserId from Sale S inner join Customer C on C.ID=S.CustomerId where S.CancelTag=0 " + BillTypeOther + "  and C.CancelTag=0 and S.CustomerId=" + cmbCustomerName.SelectedValue + " and S.InvoiceDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# order by S.InvoiceDate desc";

            dsSales = objBL.ReturnDataSet();

            if (dsSales.Tables[0].Rows.Count > 0)
            {
                ClearReportValues();

                objRL.FillCompanyData();

                myExcelApp = new Microsoft.Office.Interop.Excel.Application();
                myExcelWorkbooks = myExcelApp.Workbooks;

                objRL.ClearExcelPath();
                objRL.isPDF = true;

                objRL.Form_ExcelFileName = "SalesReport.xlsx";

                if (chkCustomer.Checked)
                    objRL.Form_DestinationReportFilePath = "Sales Report\\";
                else
                    objRL.Form_DestinationReportFilePath = "Sales Report\\" + cmbCustomerName.Text + "\\";

                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName;
                myExcelWorksheet.get_Range("A2", misValue).Formula = "Sales Report- " + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat) + " To Date: " + dtpToDate.Value.ToString(RedundancyLogics.SystemDateFormat);
                myExcelWorksheet.get_Range("I2", misValue).Formula = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                RowCount = 4;

                if (dsSales.Tables[0].Rows.Count > 0)
                {
                    SRNO = 1;
                    for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
                    {
                        AFlag = 0;
                        //ClearPurchase_Transaction();

                        dtString = Convert.ToDateTime(dsSales.Tables[0].Rows[i]["InvoiceDate"].ToString());

                        DateString = dtString.ToString(RedundancyLogics.SystemDateFormat);
                        AFlag = 1;
                        Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SRNO.ToString());
                        Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, DateString);

                        
                        AFlag = 0;
                        Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dsSales.Tables[0].Rows[i]["CustomerName"].ToString());

                        AFlag = 0;
                        Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dsSales.Tables[0].Rows[i]["MobileNumber"].ToString());

                        Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, dsSales.Tables[0].Rows[i]["ID"].ToString());

                        AFlag = 2;
                        Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dsSales.Tables[0].Rows[i]["Total"].ToString());
                        TotalAmount += Convert.ToDouble(dsSales.Tables[0].Rows[i]["Total"].ToString());

                        double FreightCharges_R = 0, LoadingAndPackingCharges_R = 0, InsuranceCharges_R = 0, OtherCharges_R = 0;
                        FreightCharges_R = Convert.ToDouble(objRL.Check_Null_String_RetrunZero(dsSales.Tables[0].Rows[i]["FreightCharges"].ToString()));
                        LoadingAndPackingCharges_R = Convert.ToDouble(objRL.Check_Null_String_RetrunZero(dsSales.Tables[0].Rows[i]["LoadingAndPackingCharges"].ToString()));
                        InsuranceCharges_R = Convert.ToDouble(objRL.Check_Null_String_RetrunZero(dsSales.Tables[0].Rows[i]["InsuranceCharges"].ToString()));
                        OtherCharges_R = Convert.ToDouble(objRL.Check_Null_String_RetrunZero(dsSales.Tables[0].Rows[i]["OtherCharges"].ToString()));
                        OtherCharges_R = FreightCharges_R + LoadingAndPackingCharges_R + InsuranceCharges_R + OtherCharges_R;

                        AFlag = 2;
                        Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, OtherCharges_R.ToString());
                        OtherChargesTotal += Convert.ToDouble(OtherCharges_R);


                        AFlag = 1;
                        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, dsSales.Tables[0].Rows[i]["PaymentMode"].ToString());


                        AFlag = 2;
                        Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, dsSales.Tables[0].Rows[i]["InvoiceTotal"].ToString());
                        InvoiceTotalAmount += Convert.ToDouble(dsSales.Tables[0].Rows[i]["InvoiceTotal"].ToString());


                        //InvoiceTotalAmount = Convert.ToDouble(objRL.Check_Null_String_RetrunZero(dsSales.Tables[0].Rows[i]["InvoiceTotal"].ToString()));
                        //AFlag = 2;
                        //Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, InvoiceTotalAmount.ToString());
                        //InvoiceTotalAmount += Convert.ToDouble(InvoiceTotalAmount);

                         
                       
                        RowCount++;
                        SRNO++;

                        AFlag = 0;
                    }

                    Fill_Merge_Cell("A", "E", misValue, myExcelWorksheet, "Total");

                    AFlag = 2;
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, TotalAmount.ToString());

                    AFlag = 2;
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, OtherChargesTotal.ToString());

                   
                    AFlag = 2;
                    Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, InvoiceTotalAmount.ToString());

                  
                    AFlag = 0;
                    RowCount++;
                }

                myExcelWorkbook.Save();

                string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
                const int xlQualityStandard = 0;
                myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);

                myExcelWorkbook.Close(true, misValue, misValue);
                myExcelApp.Quit();

                System.Diagnostics.Process.Start(PDFReport);
                objRL.DeleteExcelFile();
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

        bool AmountFlag = false;
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
            else if (AFlag == 2)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            DrawBorder(AlingRange2);

            if (MH_Value == true)
            {
                AlingRange2.RowHeight = 40;
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                AlingRange2.WrapText = true;
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ExcelReport();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void chkCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCustomer.Checked)
            {
                cmbCustomerName.SelectedIndex = -1;
                cmbCustomerName.Enabled = false;
            }
            else
            {
                objRL.Fill_Customer(cmbCustomerName);
                cmbCustomerName.SelectedIndex = -1;
                cmbCustomerName.Enabled = true;
            }
        }

        bool FlagToday = false;

        private void cbToday_CheckedChanged(object sender, EventArgs e)
        {
            if (cbToday.Checked)
            {
                FlagToday = true;
                dtpFromDate.Value = DateTime.Now.Date;
                dtpToDate.Value = DateTime.Now.Date;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
            }
            else
            {
                FlagToday = false;
                dtpFromDate.Value = DateTime.Now.Date;
                dtpToDate.Value = DateTime.Now.Date;
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
            }
        }

        
    }
}
