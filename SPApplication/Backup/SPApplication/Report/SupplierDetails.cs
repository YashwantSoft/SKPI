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
    public partial class SupplierDetails : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        ErrorProvider objEP = new ErrorProvider();
        int SrNo = 1, dgvPurchaseRow = 0, dgvPaymentRow = 0;
        double PurchaseTotal = 0, PaymentTotal = 0;
        public SupplierDetails()
        {
            InitializeComponent();
            objBL.Set_Report_Design(this, lblHeader, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_SUPPLIERDETAILS);
            btnView.Text = BusinessResources.BTN_VIEW;

            btnView.BackColor = objBL.GetBackgroundColor();
            btnView.ForeColor = objBL.GetForeColor();
        }

        private void ClearAll()
        {
            objEP.Clear();
            dgvPurchase.DataSource = null;
            dgvPayment.DataSource = null;
            lblCount.Text = "Total Count:";
            lblTotalCountPayment.Text = "Total Count:";
            txtPurchaseTotal.Text = "";
            txPaymentTotal.Text = "";
            dgvPayment.Rows.Clear();
            dgvPurchase.Rows.Clear();
            SrNo = 1;
            cbToday.Checked = true;
            cbSelectAll.Checked = true;
            dtpFromDate.Focus();
        }
        private void SupplierDetails_Load(object sender, EventArgs e)
        {
            lblHeader.Text += "-" + RedundancyLogics.SupplierName;
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            objRL.Fill_PurchaseNo(cmbBillNo,RedundancyLogics.SupplierId);
            objRL.GetSupplierRecords(RedundancyLogics.SupplierId);
            ClearAll();
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
            if (cbSelectAll.Checked==false)
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
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (dgvPayment.Rows.Count > 0 || dgvPurchase.Rows.Count > 0)
            {
                ExcelReport();
            }
        }


        private void PaymentView()
        {
            SrNo = 1;
            dgvPaymentRow = 0;
            PaymentTotal = 0;
            dgvPayment.Rows.Clear();
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();

            if (cbToday.Checked == true)
                objBL.Query = "select S.ID,Format([S.PaymentDate],'dd/MM/yyyy') as [Payment Date],S.SupplierId,S.PaidAmount,S.TotalDue,S.PaymentMode,S.Naration,S.BankName,S.AccountNumber,S.TransactionDate,S.ChequeNumber,C.SupplierName from Payment S inner join Supplier C on C.ID=S.SupplierId where S.CancelTag=0 and C.CancelTag=0 and S.SupplierId=" + RedundancyLogics.SupplierId + "";
            else
                objBL.Query = "select S.ID,Format([S.PaymentDate],'dd/MM/yyyy') as [Payment Date],S.SupplierId,S.PaidAmount,S.TotalDue,S.PaymentMode,S.Naration,S.BankName,S.AccountNumber,S.TransactionDate,S.ChequeNumber,C.SupplierName from Payment S inner join Supplier C on C.ID=S.SupplierId where S.CancelTag=0 and C.CancelTag=0 and S.SupplierId=" + RedundancyLogics.SupplierId + " and S.PaymentDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";

            ds1 = objBL.ReturnDataSet();

            if (ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    dgvPayment.Rows.Add();
                    dgvPayment.Rows[dgvPaymentRow].Cells["clmSrNoExpeses"].Value = SrNo.ToString();
                    dgvPayment.Rows[dgvPaymentRow].Cells["clmDateExpenses"].Value = ds1.Tables[0].Rows[i]["Payment Date"].ToString();
                    if (ds1.Tables[0].Rows[i]["PaymentMode"].ToString() == "CHEQUE")
                        dgvPayment.Rows[dgvPaymentRow].Cells["clmPayment"].Value = "Cheque Number: " + ds1.Tables[0].Rows[i]["ChequeNumber"].ToString() + "  " + ds1.Tables[0].Rows[i]["Naration"].ToString();
                    else if (ds1.Tables[0].Rows[i]["PaymentMode"].ToString() == "NEFT")
                        dgvPayment.Rows[dgvPaymentRow].Cells["clmPayment"].Value = "NEFT Account Number: " + ds1.Tables[0].Rows[i]["AccountNumber"].ToString() + "  " + ds1.Tables[0].Rows[i]["Naration"].ToString();
                    else if (ds1.Tables[0].Rows[i]["PaymentMode"].ToString() == "RTGS")
                        dgvPayment.Rows[dgvPaymentRow].Cells["clmPayment"].Value = "RTGS Account Number: " + ds1.Tables[0].Rows[i]["AccountNumber"].ToString() + "  " + ds1.Tables[0].Rows[i]["Naration"].ToString();
                    else
                        dgvPayment.Rows[dgvPaymentRow].Cells["clmPayment"].Value =  ds1.Tables[0].Rows[i]["Naration"].ToString();
                        
                    dgvPayment.Rows[dgvPaymentRow].Cells["clmPaymentType"].Value = ds1.Tables[0].Rows[i]["PaymentMode"].ToString();
                    dgvPayment.Rows[dgvPaymentRow].Cells["clmAmountPayment"].Value = ds1.Tables[0].Rows[i]["PaidAmount"].ToString();
                    if(ds1.Tables[0].Rows[i]["PaidAmount"].ToString() !="")
                    PaymentTotal += Convert.ToDouble(ds1.Tables[0].Rows[i]["PaidAmount"].ToString());
                    dgvPaymentRow++;
                    SrNo++;
                }
            }
            txPaymentTotal.Text = PaymentTotal.ToString();
        }
        System.Globalization.RegionInfo objRegInfo = new RegionInfo("en-IN");
        private void PurchaseView()
        {
            SrNo = 1;
            dgvPurchaseRow = 0;
            PurchaseTotal = 0;
            dgvPurchase.Rows.Clear();
            DataSet ds = new DataSet();

            if (cbToday.Checked == true)
            {
                if (cbSelectAll.Checked == true)
                    objBL.Query = "select P.ID,P.SupplierId,P.PurchaseDate,P.PurchaseNo,P.ChallanNo,P.BillNo,P.SubTotal,P.TotalGST,P.FreightCharges,P.LoadingAndPackingCharges,P.InsuranceCharges,P.OtherCharges,P.InvoiceTotal,P.PaymentMode,S.SupplierName,S.MobileNumber,S.GSTNumber,S.CreatedDate from (Purchase P inner join Supplier S on S.ID=P.SupplierId) where P.CancelTag=0 and S.CancelTag=0 and P.SupplierId=" + RedundancyLogics.SupplierId + "  and P.PurchaseDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# order by P.PurchaseDate desc"; 
                else
                    objBL.Query = "select P.ID,P.SupplierId,P.PurchaseDate,P.PurchaseNo,P.ChallanNo,P.BillNo,P.SubTotal,P.TotalGST,P.FreightCharges,P.LoadingAndPackingCharges,P.InsuranceCharges,P.OtherCharges,P.InvoiceTotal,P.PaymentMode,S.SupplierName,S.MobileNumber,S.GSTNumber,S.CreatedDate from (Purchase P inner join Supplier S on S.ID=P.SupplierId) where P.CancelTag=0 and S.CancelTag=0 and P.ID=" + cmbBillNo.SelectedValue + " and P.SupplierId=" + RedundancyLogics.SupplierId + " and P.PurchaseDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# order by P.PurchaseDate desc"; 
            }
            else
            {
                if (cbSelectAll.Checked)
                    objBL.Query = "select P.ID,P.SupplierId,P.PurchaseDate,P.PurchaseNo,P.ChallanNo,P.BillNo,P.SubTotal,P.TotalGST,P.FreightCharges,P.LoadingAndPackingCharges,P.InsuranceCharges,P.OtherCharges,P.InvoiceTotal,P.PaymentMode,S.SupplierName,S.MobileNumber,S.GSTNumber,S.CreatedDate from (Purchase P inner join Supplier S on S.ID=P.SupplierId) where P.CancelTag=0 and S.CancelTag=0 and P.SupplierId=" + RedundancyLogics.SupplierId + " and  P.PurchaseDate  BETWEEN " + "#" + dtpFromDate.Value.ToString("dd/MM/yyyy") + "# and #" + dtpToDate.Value.ToString("dd/MM/yyyy") + "#";
                else
                    objBL.Query = "select P.ID,P.SupplierId,P.PurchaseDate,P.PurchaseNo,P.ChallanNo,P.BillNo,P.SubTotal,P.TotalGST,P.FreightCharges,P.LoadingAndPackingCharges,P.InsuranceCharges,P.OtherCharges,P.InvoiceTotal,P.PaymentMode,S.SupplierName,S.MobileNumber,S.GSTNumber,S.CreatedDate from (Purchase P inner join Supplier S on S.ID=P.SupplierId) where P.CancelTag=0 and S.CancelTag=0 and P.SupplierId=" + RedundancyLogics.SupplierId + " and P.ID=" + cmbBillNo.SelectedValue + " and  P.PurchaseDate  BETWEEN " + "#" + dtpFromDate.Value.ToString("dd/MM/yyyy") + "# and #" + dtpToDate.Value.ToString("dd/MM/yyyy") + "#";
            }

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //lblBalance.Text = "Opening Balance : " + ds.Tables[0].Rows[0]["OpeningBalance"].ToString() + " " + objRegInfo.CurrencySymbol;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgvPurchase.Rows.Add();
                    dgvPurchase.Rows[dgvPurchaseRow].Cells["clmSrNoIncome"].Value = SrNo.ToString();
                    dgvPurchase.Rows[dgvPurchaseRow].Cells["clmDateIncome"].Value = ds.Tables[0].Rows[i]["PurchaseDate"].ToString();
                    dgvPurchase.Rows[dgvPurchaseRow].Cells["clmDetails"].Value = "Purchase No:" + ds.Tables[0].Rows[i]["PurchaseNo"].ToString();
                    dgvPurchase.Rows[dgvPurchaseRow].Cells["clmPaymentTypeIncome"].Value = ds.Tables[0].Rows[i]["PaymentMode"].ToString();
                    dgvPurchase.Rows[dgvPurchaseRow].Cells["clmAmountPurchase"].Value = ds.Tables[0].Rows[i]["InvoiceTotal"].ToString();
                    if(ds.Tables[0].Rows[i]["InvoiceTotal"].ToString() !="")
                    PurchaseTotal += Convert.ToDouble(ds.Tables[0].Rows[i]["InvoiceTotal"].ToString());
                    dgvPurchaseRow++;
                    SrNo++;
                }
            }
            txtPurchaseTotal.Text = PurchaseTotal.ToString();
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
                btnReport.Focus();
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

            if (dgvPurchase.Rows.Count > 0)
            {
                objRL.FillCompanyData();

                myExcelApp = new Microsoft.Office.Interop.Excel.Application();
                myExcelWorkbooks = myExcelApp.Workbooks;

                objRL.ClearExcelPath();
                objRL.isPDF = true;

                objRL.Form_ExcelFileName = "SupplierPendingPayment.xlsx";
                objRL.Form_DestinationReportFilePath = "Supplier Pending Payment\\" + RedundancyLogics.SupplierName + "\\";

                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName;
                myExcelWorksheet.get_Range("A2", misValue).Formula = "Purchase and Paid Amount Report- " + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat) + " To Date: " + dtpToDate.Value.ToString(RedundancyLogics.SystemDateFormat);
                myExcelWorksheet.get_Range("A3", misValue).Formula = "Suppplier Name-" + RedundancyLogics.SupplierName.ToString();
                myExcelWorksheet.get_Range("J2", misValue).Formula = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                RowCount = 6;


                SRNO = 1;
                for (int i = 0; i < dgvPurchase.Rows.Count; i++)
                {
                    AFlag = 0;
                    //ClearPurchase_Transaction();


                    AFlag = 1;
                    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SRNO.ToString());

                    dtString = Convert.ToDateTime(dgvPurchase.Rows[i].Cells["clmDateIncome"].Value);
                    DateString = dtString.ToString(RedundancyLogics.SystemDateFormat);
                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, DateString);


                    AFlag = 0;
                    Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dgvPurchase.Rows[i].Cells["clmDetails"].Value.ToString());

                    AFlag = 2;
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dgvPurchase.Rows[i].Cells["clmAmountPurchase"].Value.ToString());
                    RowCount++;
                    SRNO++;
                    AFlag = 0;
                }

                Fill_Merge_Cell("A", "C", misValue, myExcelWorksheet, "Total Purchase");
                AFlag = 2;
                Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, PurchaseTotal.ToString());

                SRNO = 1; RowCount = 6;
                for (int i = 0; i < dgvPayment.Rows.Count; i++)
                {
                    AFlag = 0;
                    //ClearPurchase_Transaction();


                    AFlag = 1;
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, SRNO.ToString());

                    dtString = Convert.ToDateTime(dgvPayment.Rows[i].Cells["clmDateExpenses"].Value.ToString());
                    DateString = dtString.ToString(RedundancyLogics.SystemDateFormat);
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, DateString);

                    AFlag = 0;
                    Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, dgvPayment.Rows[i].Cells["clmPayment"].Value.ToString());

                    AFlag = 1;
                    Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, dgvPayment.Rows[i].Cells["clmPaymentType"].Value.ToString());

                    AFlag = 2;
                    Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, dgvPayment.Rows[i].Cells["clmAmountPayment"].Value.ToString());
                    RowCount++;
                    SRNO++;
                }

                AFlag = 0;
                Fill_Merge_Cell("F", "I", misValue, myExcelWorksheet, "Total Payment Paid");
                AFlag = 2;
                Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, PaymentTotal.ToString());
                RowCount++;
                AFlag = 0;
                Fill_Merge_Cell("F", "I", misValue, myExcelWorksheet, "Total Pending Amount");
                AFlag = 2;
                Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, txtPendingAmount.Text.ToString());

                myExcelWorkbook.Save();

                string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
                const int xlQualityStandard = 0;
                myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);

                myExcelWorkbook.Close(true, misValue, misValue);
                myExcelApp.Quit();

                System.Diagnostics.Process.Start(PDFReport);

                    if(!string.IsNullOrEmpty(objRL.EmailId_RL_S) && cbEmail.Checked)
                    {
                        objRL.EmailId_RL = objRL.EmailId_RL_S; 
                        objRL.Subject_RL = "Purchase and Payment Details Report";
                        //string body = "<div><p>Dear " + objRL.SupplierName_RL_S + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
                        string body = "<div><p>Dear " + objRL.SupplierName_RL_S + ",<p/><p>Please find attachment of pdf file.</p><p>Thanks,</p><p>" + objRL.CI_CompanyName + "</p></div>";

                        objRL.Body_RL = body;// "Dear " + objRL.SupplierName_RL_S + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
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

        private void btnView_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                PaymentView();
                PurchaseView();
                double PendingAmount = 0;
                PendingAmount = PurchaseTotal - PaymentTotal;
                txtPendingAmount.Text = PendingAmount.ToString();
            }
            else
            {
                objRL.ShowMessage(21, 4);
                return;
            }
        }

        private void cbEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEmail.Checked)
            {
                lblEmailId.Visible = true;
                lblEmailId.Text = objRL.EmailId_RL_S.ToString();
            }
            else
            {
                lblEmailId.Visible = false;
                lblEmailId.Text = "";
            }
        }
    }
}
