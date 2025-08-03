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

namespace SPApplication.Report
{
    public partial class ProfitLossReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        ErrorProvider objEP = new ErrorProvider();

        bool FlagToday = false;

        public ProfitLossReport()
        {
            InitializeComponent();
            objBL.Set_Report_Design(this, lblHeader, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_PROFITANDREPORT);
            btnView.Text = BusinessResources.BTN_VIEW;

            btnView.BackColor = objBL.GetBackgroundColor();
            btnView.ForeColor = objBL.GetForeColor();
        }

        private void ClearAll()
        {
            objEP.Clear();
            cbToday.Checked = true;
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            dgvIncome.DataSource = null;
            lblCount.Text = "";
            dgvExpenses.Rows.Clear();
            dgvIncome.Rows.Clear();
            ExpesesTotal = 0; IncomeTotal = 0; NetProfit = 0;
            SrNo = 1;
            dtpFromDate.Focus();
        }
        private void cbToday_CheckedChanged(object sender, EventArgs e)
        {
            if (cbToday.Checked == true)
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

        private void ProfitLossReport_Load(object sender, EventArgs e)
        {
            ClearAll();
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            cbToday.Checked = true;
        }
        
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (dgvIncome.Rows.Count > 0 || dgvExpenses.Rows.Count > 0)
            {
                ExcelReport();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        int SrNo = 1;
        private void ExpensesView()
        {
            int j = 0;
            dgvExpenses.Rows.Clear();
            DataSet ds = new DataSet();
           // objBL.Query = "select ID,EntryDate,ExpensesHeadId,ExpensesHead,Naration,Amount,PaymentMode,TransactionDate,ChequeBankName,ChequeNumber,NEFTDate,NEFTBankName,NEFTAccountNumber,UserId from Expenses where CancelTag=0 and EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            
            objBL.Query = "select ID,EntryDate,ExpensesHeadId,ExpensesHead,Naration,Amount,PaymentMode,BankId,BankName,AccountNumber,TransactionDate,ChequeNumber from Expenses where CancelTag=0 and EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                SrNo = 1; ExpesesTotal = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgvExpenses.Rows.Add();
                    dgvExpenses.Rows[i].Cells["clmSrNoExpeses"].Value = SrNo.ToString();
                    dgvExpenses.Rows[i].Cells["clmDateExpenses"].Value = ds.Tables[0].Rows[i]["EntryDate"].ToString();
                    dgvExpenses.Rows[i].Cells["clmExpenses"].Value = ds.Tables[0].Rows[i]["ExpensesHead"].ToString();
                    dgvExpenses.Rows[i].Cells["clmPaymentType"].Value = ds.Tables[0].Rows[i]["PaymentMode"].ToString();
                    dgvExpenses.Rows[i].Cells["clmExpensesAmount"].Value = ds.Tables[0].Rows[i]["Amount"].ToString();
                    ExpesesTotal += Convert.ToDouble(ds.Tables[0].Rows[i]["Amount"].ToString());
                    SrNo++;
                    j = i;
                }
                if (j >= 0)
                    j++;
                //txtExpensesTotal.Text = ExpesesTotal.ToString();
            }

            DataSet dsPurchase = new DataSet();
            objBL.Query = "select P.ID,P.PurchaseDate,P.PurchaseNo,P.ChallanNo,P.BillNo,P.SupplierId,S.SupplierName,S.Address,S.MobileNumber,S.EmailId,S.GSTNumber,P.SubTotal,P.TotalGST,P.FreightCharges,P.LoadingAndPackingCharges,P.InsuranceCharges,P.OtherCharges,P.InvoiceTotal,P.PaymentMode,P.PrintFlag,P.PrintCount from Purchase P inner join Supplier S on S.ID=P.SupplierId where P.CancelTag=0 and S.CancelTag=0 and P.IsGST=1 and P.PurchaseDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and P.PaymentMode NOT IN('CREDIT','CHARITY') order by P.PurchaseDate desc";
            dsPurchase = objBL.ReturnDataSet();

            for (int i = 0; i < dsPurchase.Tables[0].Rows.Count; i++)
            {
                dgvExpenses.Rows.Add();
                dgvExpenses.Rows[j].Cells["clmSrNoExpeses"].Value = SrNo.ToString();
                dgvExpenses.Rows[j].Cells["clmDateExpenses"].Value = dsPurchase.Tables[0].Rows[i]["PurchaseDate"].ToString();
                dgvExpenses.Rows[j].Cells["clmExpenses"].Value = dsPurchase.Tables[0].Rows[i]["SupplierName"].ToString();
                dgvExpenses.Rows[j].Cells["clmPaymentType"].Value = dsPurchase.Tables[0].Rows[i]["PaymentMode"].ToString();
                dgvExpenses.Rows[j].Cells["clmExpensesAmount"].Value = dsPurchase.Tables[0].Rows[i]["InvoiceTotal"].ToString();
                ExpesesTotal += Convert.ToDouble(dsPurchase.Tables[0].Rows[i]["InvoiceTotal"].ToString());
                SrNo++;
                j++;
            }

            DataSet dsPayment = new DataSet();
            objBL.Query = "select P.ID,P.PaymentDate,P.SupplierId,S.SupplierName,P.PaidAmount,P.TotalDue,P.Naration,P.PaymentMode,P.BankId,P.BankName,P.AccountNumber,P.TransactionDate,P.ChequeNumber,P.PartyBank,P.PartyBankAccountNumber from Payment P inner join Supplier S on S.ID=P.SupplierId where P.CancelTag=0 and S.CancelTag=0 and P.PaymentDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#  order by P.PaymentDate desc";
            dsPayment = objBL.ReturnDataSet();

            for (int i = 0; i < dsPayment.Tables[0].Rows.Count; i++)
            {
                dgvExpenses.Rows.Add();
                dgvExpenses.Rows[j].Cells["clmSrNoExpeses"].Value = SrNo.ToString();
                dgvExpenses.Rows[j].Cells["clmDateExpenses"].Value = dsPayment.Tables[0].Rows[i]["PaymentDate"].ToString();
                dgvExpenses.Rows[j].Cells["clmExpenses"].Value = dsPayment.Tables[0].Rows[i]["SupplierName"].ToString();
                dgvExpenses.Rows[j].Cells["clmPaymentType"].Value = dsPayment.Tables[0].Rows[i]["PaymentMode"].ToString();
                dgvExpenses.Rows[j].Cells["clmExpensesAmount"].Value = dsPayment.Tables[0].Rows[i]["PaidAmount"].ToString();
                ExpesesTotal += Convert.ToDouble(dsPayment.Tables[0].Rows[i]["PaidAmount"].ToString());
                SrNo++;
                j++;
            }
            txtExpensesTotal.Text = ExpesesTotal.ToString();
        }

        double ExpesesTotal = 0, IncomeTotal = 0, NetProfit = 0;
        private void IncomeView()
        {
            int j = 0;
            dgvIncome.Rows.Clear();
            DataSet dsSale = new DataSet();
            objBL.Query = "select DC.ID,DC.InvoiceDate,DC.DCNo,DC.JobNo,DC.BillNo,DC.PONo,DC.CustomerId,C.CustomerName,C.Address,C.MobileNumber,C.EmailId,C.GSTNumber,C.StateCode,DC.NoteD,DC.TaxPaybleOnReverseCharge,DC.Total,DC.FreightCharges,DC.LoadingAndPackingCharges,DC.InsuranceCharges,DC.OtherCharges,DC.InvoiceTotal,DC.Naration,DC.PaymentMode,DC.BankId,DC.BankName,DC.AccountNumber,DC.TransactionDate,DC.ChequeNumber,DC.PartyBank,DC.PartyBankAccountNumber,DC.TransportationMode,DC.VehicalNumber,DC.DateOfSupply,DC.PlaceOfSupply,DC.BillStatus,DC.PrintFlag,DC.PrintCount,DC.CompanyId,DC.CompanyName from Sale DC inner join Customer C on C.ID=DC.CustomerId where DC.CancelTag=0 and C.CancelTag=0 and DC.InvoiceDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and DC.PaymentMode NOT IN('CREDIT','CHARITY') order by DC.InvoiceDate desc"; 
            dsSale = objBL.ReturnDataSet();

            if (dsSale.Tables[0].Rows.Count > 0)
            {
                SrNo = 1; IncomeTotal = 0;
                for (int i = 0; i < dsSale.Tables[0].Rows.Count; i++)
                {
                    dgvIncome.Rows.Add();
                    dgvIncome.Rows[i].Cells["clmSrNoIncome"].Value = SrNo.ToString();
                    dgvIncome.Rows[i].Cells["clmDateIncome"].Value = dsSale.Tables[0].Rows[i]["InvoiceDate"].ToString();
                    dgvIncome.Rows[i].Cells["clmIncome"].Value = dsSale.Tables[0].Rows[i]["CustomerName"].ToString();
                    dgvIncome.Rows[i].Cells["clmPaymentTypeIncome"].Value = dsSale.Tables[0].Rows[i]["PaymentMode"].ToString();
                    dgvIncome.Rows[i].Cells["clmAmountIncome"].Value = dsSale.Tables[0].Rows[i]["InvoiceTotal"].ToString();
                    IncomeTotal += Convert.ToDouble(dsSale.Tables[0].Rows[i]["InvoiceTotal"].ToString());
                    SrNo++;
                    j = i;
                }
                if (j >= 0)
                    j++;
            }

            DataSet dsReceipt = new DataSet();
            objBL.Query = "select R.ID,R.PaymentDate,R.CustomerId,C.CustomerName,R.PaidAmount,R.TotalDue,R.Naration,R.PaymentMode,R.BankId,R.BankName,R.AccountNumber,R.TransactionDate,R.ChequeNumber,R.PartyBank,R.PartyBankAccountNumber from Receipt R inner join Customer C on C.ID=R.CustomerId where R.CancelTag=0 and C.CancelTag=0 and R.PaymentDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# order by R.PaymentDate desc";
            dsReceipt = objBL.ReturnDataSet();

            if (dsReceipt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsReceipt.Tables[0].Rows.Count; i++)
                {
                    dgvIncome.Rows.Add();
                    dgvIncome.Rows[j].Cells["clmSrNoIncome"].Value = SrNo.ToString();
                    dgvIncome.Rows[j].Cells["clmDateIncome"].Value = dsReceipt.Tables[0].Rows[i]["PaymentDate"].ToString();
                    dgvIncome.Rows[j].Cells["clmIncome"].Value = dsReceipt.Tables[0].Rows[i]["CustomerName"].ToString();
                    dgvIncome.Rows[j].Cells["clmPaymentTypeIncome"].Value = dsReceipt.Tables[0].Rows[i]["PaymentMode"].ToString();
                    dgvIncome.Rows[j].Cells["clmAmountIncome"].Value = dsReceipt.Tables[0].Rows[i]["PaidAmount"].ToString();
                    IncomeTotal += Convert.ToDouble(dsReceipt.Tables[0].Rows[i]["PaidAmount"].ToString());
                    SrNo++;
                    j++;
                }
            }

            txtIncomeTotal.Text = IncomeTotal.ToString();
        }

        private void dtpFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpToDate.Focus();
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnReport.Focus();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ExpesesTotal = 0; IncomeTotal = 0; NetProfit = 0;
            IncomeView();
            ExpensesView();
            NetProfit = IncomeTotal - ExpesesTotal;
            txtNetProfit.Text = NetProfit.ToString();
        }

        int RowCount = 18, SRNO = 1, AFlag = 0;
        bool MH_Value = false;
        Microsoft.Office.Interop.Excel.Application myExcelApp;
        Microsoft.Office.Interop.Excel.Workbooks myExcelWorkbooks;
        Microsoft.Office.Interop.Excel.Workbook myExcelWorkbook;
        DateTime dtString;
        string DateString = "";
        double PendingAmount = 0;

        private void ExcelReport()
        {

            object misValue = System.Reflection.Missing.Value;
            
            if (dgvIncome.Rows.Count > 0)
            {

                objRL.FillCompanyData();

                myExcelApp = new Microsoft.Office.Interop.Excel.Application();
                myExcelWorkbooks = myExcelApp.Workbooks;

                objRL.ClearExcelPath();
                objRL.isPDF = true;

                objRL.Form_ExcelFileName = "ProfitExpenses.xlsx";
                objRL.Form_DestinationReportFilePath = "Profit And Expenses\\" + DateTime.Now.Date.ToString("dd-MMM-yyyy") + "\\";

                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName;
                myExcelWorksheet.get_Range("A2", misValue).Formula = "Report Date- " + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat) + " To Date: " + dtpToDate.Value.ToString(RedundancyLogics.SystemDateFormat);
                myExcelWorksheet.get_Range("A3", misValue).Formula = "Profit and Expenses Report";
                myExcelWorksheet.get_Range("J2", misValue).Formula = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                RowCount = 6;


                SRNO = 1;
                for (int i = 0; i < dgvIncome.Rows.Count; i++)
                {
                    AFlag = 0;
                    //ClearPurchase_Transaction();


                    AFlag = 1;
                    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SRNO.ToString());

                    dtString = Convert.ToDateTime(dgvIncome.Rows[i].Cells["clmDateIncome"].Value);
                    DateString = dtString.ToString(RedundancyLogics.SystemDateFormat);
                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, DateString);


                    AFlag = 0;
                    Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dgvIncome.Rows[i].Cells["clmIncome"].Value.ToString());

                    AFlag = 2;
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dgvIncome.Rows[i].Cells["clmPaymentTypeIncome"].Value.ToString());

                    AFlag = 2;
                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, dgvIncome.Rows[i].Cells["clmAmountIncome"].Value.ToString());


                    RowCount++;
                    SRNO++;
                    AFlag = 0;
                }

                Fill_Merge_Cell("A", "D", misValue, myExcelWorksheet, "Total Income");
                AFlag = 2;
                Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, txtIncomeTotal.Text.ToString());

                SRNO = 1; RowCount = 6;
                for (int i = 0; i < dgvExpenses.Rows.Count; i++)
                {
                    AFlag = 0;
                    //ClearPurchase_Transaction();


                    AFlag = 1;
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, SRNO.ToString());

                    dtString = Convert.ToDateTime(dgvExpenses.Rows[i].Cells["clmDateExpenses"].Value.ToString());
                    DateString = dtString.ToString(RedundancyLogics.SystemDateFormat);
                    Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, DateString);

                    AFlag = 0;
                    Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, dgvExpenses.Rows[i].Cells["clmExpenses"].Value.ToString());

                    AFlag = 1;
                    Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, dgvExpenses.Rows[i].Cells["clmPaymentType"].Value.ToString());

                    AFlag = 2;
                    Fill_Merge_Cell("K", "K", misValue, myExcelWorksheet, dgvExpenses.Rows[i].Cells["clmExpensesAmount"].Value.ToString());
                    RowCount++;
                    SRNO++;
                }
                AFlag = 0;
                Fill_Merge_Cell("G", "J", misValue, myExcelWorksheet, "Total Expenses");
                AFlag = 2;
                Fill_Merge_Cell("K", "K", misValue, myExcelWorksheet, txtExpensesTotal.Text.ToString());
                RowCount++;
                AFlag = 0;
                Fill_Merge_Cell("G", "J", misValue, myExcelWorksheet, "Net Profit");
                AFlag = 2;
                Fill_Merge_Cell("K", "K", misValue, myExcelWorksheet, txtNetProfit.Text.ToString());

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
    }
}
