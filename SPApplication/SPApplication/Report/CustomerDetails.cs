using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using System.Globalization;
using Microsoft.Office.Interop.Excel;

namespace SPApplication.Report
{
    public partial class CustomerDetails : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        ErrorProvider objEP = new ErrorProvider();
        DesignLayer objDL = new DesignLayer();

        int SrNo = 1, dgvSaleRow = 0, dgvReceiptRow = 0;
        double SaleTotal = 0, ReceiptTotal = 0;
        System.Globalization.RegionInfo objRegInfo = new RegionInfo("en-IN");

        public CustomerDetails()
        {
            InitializeComponent();
            objDL.SetDesign3Buttons(this, lblHeader, btnView, btnClear, btnExit, BusinessResources.LBL_HEADER_CUSTOMERPAYMENTDEPOSITREPORT);
            objDL.SetButtonDesign(btnReport, BusinessResources.BTN_REPORT);
        }

        private void ClearAll()
        {
            objEP.Clear();
            dgvSale.DataSource = null;
            dgvReceipt.DataSource = null;
            lblCount.Text = "Total Count:";
            lblTotalCountReceipt.Text = "Total Count:";
            txtSaleTotal.Text = "";
            txtReceiptTotal.Text = "";
            dgvReceipt.Rows.Clear();
            dgvSale.Rows.Clear();
            SrNo = 1;
            cbToday.Checked = true;
            cbSelectAll.Checked = true;
            dtpFromDate.Focus();
        }

        private void CustomerDetails_Load(object sender, EventArgs e)
        {
            //lblHeader.Text += "-" + RedundancyLogics.CustomerName;
            //dtpFromDate.CustomFormat = "dd/MM/yyyy";
            //dtpToDate.CustomFormat = "dd/MM/yyyy";
            //objRL.Fill_SaleNo(cmbBillNo, RedundancyLogics.CustomerId);
            //objRL.GetCustomerRecords(RedundancyLogics.CustomerId);
            //ClearAll();
        }

        private void cbDate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbToday.Checked)
            {
                dtpFromDate.Value = DateTime.Now.Date;
                dtpToDate.Value = DateTime.Now.Date;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
            }
            else
            {
                dtpFromDate.Value = DateTime.Now.Date;
                dtpToDate.Value = DateTime.Now.Date;
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
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

        protected bool Validation()
        {
            objEP.Clear();
            if (cbSelectAll.Checked == false)
            {
                if (cmbBillNo.SelectedIndex == -1)
                {
                    objEP.SetError(cmbBillNo, "Select Bill No");
                    cmbBillNo.Focus();
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        private void ReceiptView()
        {
            SrNo = 1;
            dgvReceiptRow = 0;
            ReceiptTotal = 0;
            dgvReceipt.Rows.Clear();
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();

            if (cbToday.Checked)
                objBL.Query = "select S.CustomerId,Format([S.PaymentDate],'dd/MM/yyyy') as [Payment Date],C.CustomerName as [Customer Name],S.PaidAmount as [Paid Amount],S.TotalDue as [Total Due],S.PaymentMode,S.Naration,S.BankId,S.BankName,S.AccountNumber,S.TransactionDate,S.ChequeNumber from Receipt S inner join Customer C on C.ID=S.CustomerId where S.CancelTag=0 and C.CancelTag=0 and S.CustomerId=" + objRL.CustomerId + " ";
            else
                objBL.Query = "select S.CustomerId,Format([S.PaymentDate],'dd/MM/yyyy') as [Payment Date],C.CustomerName as [Customer Name],S.PaidAmount as [Paid Amount],S.TotalDue as [Total Due],S.PaymentMode,S.Naration,S.BankId,S.BankName,S.AccountNumber,S.TransactionDate,S.ChequeNumber from Receipt S inner join Customer C on C.ID=S.CustomerId where S.CancelTag=0 and C.CancelTag=0 and S.CustomerId=" + objRL.CustomerId + "  and S.PaymentDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";

            ds1 = objBL.ReturnDataSet();

            if (ds1.Tables[0].Rows.Count > 0)
            {
                lblTotalCountReceipt.Text = "Total Count : " + ds1.Tables[0].Rows.Count;
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    dgvReceipt.Rows.Add();
                    dgvReceipt.Rows[dgvReceiptRow].Cells["clmSrNoReceipt"].Value = SrNo.ToString();
                    dgvReceipt.Rows[dgvReceiptRow].Cells["clmDateReceipt"].Value = ds1.Tables[0].Rows[i]["Payment Date"].ToString();
                    dgvReceipt.Rows[dgvReceiptRow].Cells["clmReceipt"].Value = ds1.Tables[0].Rows[i]["Naration"].ToString();

                    if (ds1.Tables[0].Rows[i]["PaymentMode"].ToString() == "CHEQUE")
                        dgvReceipt.Rows[dgvReceiptRow].Cells["clmPaymentType"].Value = "Cheque Number: " + ds1.Tables[0].Rows[i]["ChequeNumber"].ToString() + "  " + ds1.Tables[0].Rows[i]["Naration"].ToString();
                    else if (ds1.Tables[0].Rows[i]["PaymentMode"].ToString() == "NEFT")
                        dgvReceipt.Rows[dgvReceiptRow].Cells["clmPaymentType"].Value = "NEFT Account Number: " + ds1.Tables[0].Rows[i]["AccountNumber"].ToString() + "  " + ds1.Tables[0].Rows[i]["Naration"].ToString();
                    else if (ds1.Tables[0].Rows[i]["PaymentMode"].ToString() == "RTGS")
                        dgvReceipt.Rows[dgvReceiptRow].Cells["clmPaymentType"].Value = "RTGS Account Number: " + ds1.Tables[0].Rows[i]["AccountNumber"].ToString() + "  " + ds1.Tables[0].Rows[i]["Naration"].ToString();
                    else
                        dgvReceipt.Rows[dgvReceiptRow].Cells["clmPaymentType"].Value = "CASH : " + ds1.Tables[0].Rows[i]["Naration"].ToString();


                    dgvReceipt.Rows[dgvReceiptRow].Cells["clmPaymentType"].Value = ds1.Tables[0].Rows[i]["PaymentMode"].ToString();
                    dgvReceipt.Rows[dgvReceiptRow].Cells["clmAmountReceipt"].Value = ds1.Tables[0].Rows[i]["Paid Amount"].ToString();
                    if (ds1.Tables[0].Rows[i]["Paid Amount"].ToString() != "")
                        ReceiptTotal += Convert.ToDouble(ds1.Tables[0].Rows[i]["Paid Amount"].ToString());
                    dgvReceiptRow++;
                    SrNo++;
                }
            }
            txtReceiptTotal.Text = ReceiptTotal.ToString();
        }

        private void SaleView()
        {
            SrNo = 1;
            dgvSaleRow = 0;
            SaleTotal = 0;
            dgvSale.Rows.Clear();
            DataSet ds = new DataSet();

            if (cbToday.Checked)
            {
                if (cbSelectAll.Checked)
                    objBL.Query = "select S.CustomerId,S.ID as [Invoice No],Format([S.InvoiceDate],'dd/MM/yyyy') as [Invoice Date],C.CustomerName as [Customer Name],S.InvoiceTotal as [Invoice Total],S.PaymentMode as [Payment Type] from Sale S inner join Customer C on C.ID=S.CustomerId where S.CancelTag=0 and C.CancelTag=0 and S.CustomerId=" + objRL.CustomerId + " and S.InvoiceDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# order by S.InvoiceDate desc"; 
                else
                    objBL.Query = "select S.CustomerId,S.ID as [Invoice No],Format([S.InvoiceDate],'dd/MM/yyyy') as [Invoice Date],C.CustomerName as [Customer Name],S.InvoiceTotal as [Invoice Total],S.PaymentMode as [Payment Type] from Sale S inner join Customer C on C.ID=S.CustomerId where S.CancelTag=0 and C.CancelTag=0 and S.ID=" + cmbBillNo.SelectedValue + " and S.CustomerId=" + objRL.CustomerId + "  and S.InvoiceDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# order by S.InvoiceDate desc"; 
            }
            else
            {
                if (cbSelectAll.Checked)
                    objBL.Query = "select S.CustomerId,S.ID as [Invoice No],Format([S.InvoiceDate],'dd/MM/yyyy') as [Invoice Date],C.CustomerName as [Customer Name],S.InvoiceTotal as [Invoice Total],S.PaymentMode as [Payment Type] from Sale S inner join Customer C on C.ID=S.CustomerId where S.CancelTag=0 and C.CancelTag=0 and S.CustomerId=" + objRL.CustomerId + " and  S.InvoiceDate  BETWEEN " + "#" + dtpFromDate.Value.ToString("dd/MM/yyyy") + "# and #" + dtpToDate.Value.ToString("dd/MM/yyyy") + "#";
                else
                    objBL.Query = "select S.CustomerId,S.ID as [Invoice No],Format([S.InvoiceDate],'dd/MM/yyyy') as [Invoice Date],C.CustomerName as [Customer Name],S.InvoiceTotal as [Invoice Total],S.PaymentMode as [Payment Type] from Sale S inner join Customer C on C.ID=S.CustomerId where S.CancelTag=0 and C.CancelTag=0 and S.ID=" + cmbBillNo.SelectedValue + " and S.CustomerId=" + objRL.CustomerId + " and  S.InvoiceDate  BETWEEN " + "#" + dtpFromDate.Value.ToString("dd/MM/yyyy") + "# and #" + dtpToDate.Value.ToString("dd/MM/yyyy") + "#";
            }

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblCount.Text = "Total Count : " + ds.Tables[0].Rows.Count;
                //lblBalance.Text = "Opening Balance : " + ds.Tables[0].Rows[0]["OpeningBalance"].ToString() + " " + objRegInfo.CurrencySymbol;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgvSale.Rows.Add();
                    dgvSale.Rows[dgvSaleRow].Cells["clmSrNoSale"].Value = SrNo.ToString();
                    dgvSale.Rows[dgvSaleRow].Cells["clmDateSale"].Value = ds.Tables[0].Rows[i]["Invoice Date"].ToString();
                    dgvSale.Rows[dgvSaleRow].Cells["clmDetails"].Value = "Invoice No:" + ds.Tables[0].Rows[i]["Invoice No"].ToString();
                    dgvSale.Rows[dgvSaleRow].Cells["clmPaymentTypeSale"].Value = ds.Tables[0].Rows[i]["Payment Type"].ToString();
                    dgvSale.Rows[dgvSaleRow].Cells["clmAmountSale"].Value = ds.Tables[0].Rows[i]["Invoice Total"].ToString();
                    if (ds.Tables[0].Rows[i]["Invoice Total"].ToString() != "")
                        SaleTotal += Convert.ToDouble(ds.Tables[0].Rows[i]["Invoice Total"].ToString());
                    dgvSaleRow++;
                    SrNo++;
                }
            }
            txtSaleTotal.Text = SaleTotal.ToString();



        }

        private void dtpFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpToDate.Focus();
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbBillNo.Focus();
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            cmbBillNo.SelectedIndex = -1;
            if (cbSelectAll.Checked == true)
                cmbBillNo.Enabled = false;
            else
                cmbBillNo.Enabled = true;
        }

        private void cmbBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnView.Focus();
        }
        double PendingAmount = 0;
        private void btnView_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                ReceiptView();
                SaleView();
                
                PendingAmount = SaleTotal - ReceiptTotal;
                txtPendingAmount.Text = PendingAmount.ToString();
            }
            else
            {
                objRL.ShowMessage(21, 4);
                return;
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (dgvReceipt.Rows.Count > 0 || dgvSale.Rows.Count > 0)
            {
                ExcelReport();
            }
        }

        int RowCount = 18, SRNO = 1,AFlag = 0;
        bool MH_Value = false;
        Microsoft.Office.Interop.Excel.Application myExcelApp;
        Microsoft.Office.Interop.Excel.Workbooks myExcelWorkbooks;
        Microsoft.Office.Interop.Excel.Workbook myExcelWorkbook;
        DateTime dtString;
        string DateString = "";

        private void ExcelReport()
        {
            object misValue = System.Reflection.Missing.Value;

            if (dgvSale.Rows.Count > 0)
            {
                objRL.FillCompanyData();

                myExcelApp = new Microsoft.Office.Interop.Excel.Application();
                myExcelWorkbooks = myExcelApp.Workbooks;

                objRL.ClearExcelPath();
                objRL.isPDF = true;

                objRL.Form_ExcelFileName = "CutomerPendingPayment.xlsx";
                objRL.Form_DestinationReportFilePath = "Customer Pending Payment\\" + objRL.CustomerName + "\\";

                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName;
                myExcelWorksheet.get_Range("A2", misValue).Formula = "Sale and Paid Amount Report- " + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat) + " To Date: " + dtpToDate.Value.ToString(RedundancyLogics.SystemDateFormat);
                myExcelWorksheet.get_Range("A3", misValue).Formula = "Customer Name-" + objRL.CustomerName.ToString();
                myExcelWorksheet.get_Range("J2", misValue).Formula = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                RowCount = 6;


                SRNO = 1;
                for (int i = 0; i < dgvSale.Rows.Count; i++)
                {
                    AFlag = 0;
                    //ClearPurchase_Transaction();

                    AFlag = 1;
                    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SRNO.ToString());

                    dtString = Convert.ToDateTime(dgvSale.Rows[i].Cells["clmDateSale"].Value);
                    DateString = dtString.ToString(RedundancyLogics.SystemDateFormat);
                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, DateString);


                    AFlag = 0;
                    Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dgvSale.Rows[i].Cells["clmDetails"].Value.ToString());

                    AFlag = 2;
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dgvSale.Rows[i].Cells["clmAmountSale"].Value.ToString());
                    RowCount++;
                    SRNO++;
                    AFlag = 0;
                }

                Fill_Merge_Cell("A", "C", misValue, myExcelWorksheet, "Total Sale");
                AFlag = 2;
                Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, SaleTotal.ToString());

                SRNO = 1; RowCount = 6;
                for (int i = 0; i < dgvReceipt.Rows.Count; i++)
                {
                    AFlag = 0;
                    //ClearPurchase_Transaction();


                    AFlag = 1;
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, SRNO.ToString());

                    dtString = Convert.ToDateTime(dgvReceipt.Rows[i].Cells["clmDateReceipt"].Value.ToString());
                    DateString = dtString.ToString(RedundancyLogics.SystemDateFormat);
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, DateString);

                    AFlag = 0;
                    Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, dgvReceipt.Rows[i].Cells["clmReceipt"].Value.ToString());

                    AFlag = 1;
                    Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, dgvReceipt.Rows[i].Cells["clmPaymentType"].Value.ToString());

                    AFlag = 2;
                    Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, dgvReceipt.Rows[i].Cells["clmAmountReceipt"].Value.ToString());
                    RowCount++;
                    SRNO++;
                }
                AFlag = 0;
                Fill_Merge_Cell("F", "I", misValue, myExcelWorksheet, "Total Payment Received");
                AFlag = 2;
                Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, ReceiptTotal.ToString());
                RowCount++;
                AFlag = 0;
                Fill_Merge_Cell("F", "I", misValue, myExcelWorksheet, "Total Pending Amount");
                AFlag = 2;
                Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, PendingAmount.ToString());

                myExcelWorkbook.Save();

                string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
                const int xlQualityStandard = 0;
                myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);

                myExcelWorkbook.Close(true, misValue, misValue);
                myExcelApp.Quit();

                System.Diagnostics.Process.Start(PDFReport);
                
                if (!string.IsNullOrEmpty(objRL.EmailId_Customer) && cbEmail.Checked)
                {
                    objRL.EmailId_RL = objRL.EmailId_Customer;
                    objRL.Subject_RL = "Sales and Payment Details Report";
                    //string body = "<div><p>Dear " + objRL.CustomerName_Customer + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
                    string body = "<div><p>Dear " + objRL.CustomerName_Customer + ",<p/><p>Please find attachment of pdf file.</p><p>Thanks,</p><p>" + objRL.CI_CompanyName + "</p></div>";

                    objRL.Body_RL = body;// "Dear " + RedundancyLogics.SupplierName + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
                    objRL.FilePath_RL = PDFReport;
                    objRL.SendEMail();
                }
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

        private void cbEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEmail.Checked)
            {
                lblEmailId.Visible = true;
                lblEmailId.Text = objRL.EmailId_Customer.ToString();
            }
            else
            {
                lblEmailId.Visible = false;
                lblEmailId.Text = "";
            }
        }
    }
}
