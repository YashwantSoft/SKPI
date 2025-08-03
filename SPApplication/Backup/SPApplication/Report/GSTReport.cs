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
    public partial class GSTReport : Form
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

        public GSTReport()
        {
            InitializeComponent();
            objBL.Set_Report_Design(this, lblHeader, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_GSTREPORT);
        }
       
        private void GSTReport_Load(object sender, EventArgs e)
        {
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            ClearAll();
        }
        public void ClearAll()
        {
            objEP.Clear();
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            chkDate.Checked = false;
            AmountFlag = false;
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


        string CellDisplay1 = "";
        int CellCheckCount = 0;
        double TotalSaleTax = 0, TotalFileTax = 0, FileCGST = 0, FileSGST = 0, FileIGST = 0, TotalPurchaseTax = 0, SaleCGST = 0, SaleSGST = 0, SaleIGST = 0, PurchaseCGST = 0, PurchaseSGST = 0, PurchaseIGST = 0;
        bool Flag_Check = false;

        string CRange1 = "", CRange2 = "";
        Range firstRow_Static;
        int PasteCount = 0;
        string DateString = ""; DateTime dtString;

        double SumCGSTPer = 0, SumSGSTPer = 0, SumIGSTPer = 0, SumCGSTAmount = 0, SumSGSTAmount = 0, SumIGSTAmount = 0;
        double SumTaxableAmount = 0, SumTotalTaxAmount = 0, SumNetAmount = 0, TotalGSTPer = 0, TotalGSTAmount = 0;

        int PurchaseId = 0, SaleId = 0;

        double Purchase_TaxableAmount = 0, Purchase_TotalTaxAmount = 0, Purchase_NetAmount = 0, Purchase_CGSTAmount = 0, Purchase_SGSTAmount = 0, Purchase_IGSTAmount = 0;
        double Sale_TaxableAmount = 0, Sale_TotalTaxAmount = 0, Sale_NetAmount = 0, Sale_CGSTAmount = 0, Sale_SGSTAmount = 0, Sale_IGSTAmount = 0;

        private void ClearPurchase()
        {
            SumCGSTPer = 0; SumSGSTPer = 0; SumIGSTPer = 0; SumCGSTAmount = 0; SumSGSTAmount = 0; SumIGSTAmount = 0;
            SumTaxableAmount = 0; SumTotalTaxAmount = 0; SumNetAmount = 0; TotalGSTPer = 0; TotalGSTAmount = 0;
            Purchase_TaxableAmount = 0; Purchase_TotalTaxAmount = 0; Purchase_NetAmount = 0; Purchase_CGSTAmount = 0; Purchase_SGSTAmount = 0; Purchase_IGSTAmount = 0;
        }

        private void ClearSale()
        {
            SumCGSTPer = 0; SumSGSTPer = 0; SumIGSTPer = 0; SumCGSTAmount = 0; SumSGSTAmount = 0; SumIGSTAmount = 0;
            SumTaxableAmount = 0; SumTotalTaxAmount = 0; SumNetAmount = 0; TotalGSTPer = 0; TotalGSTAmount = 0;
            Sale_TaxableAmount = 0; Sale_TotalTaxAmount = 0; Sale_NetAmount = 0; Sale_CGSTAmount = 0; Sale_SGSTAmount = 0; Sale_IGSTAmount = 0;
        }

        private void ClearPurchase_Transaction()
        {
            SumCGSTPer = 0; SumSGSTPer = 0; SumIGSTPer = 0; SumCGSTAmount = 0; SumSGSTAmount = 0; SumIGSTAmount = 0;
            SumTaxableAmount = 0; SumTotalTaxAmount = 0; SumNetAmount = 0; TotalGSTPer = 0; TotalGSTAmount = 0;
        }

        private void Get_Purchase()
        {
            DataSet dsP = new DataSet();
            //,CGSTAmount,SGSTPer,SGSTAmount,IGSTPer,IGSTAmount,TotalTaxAmount,NetAmount,UserId
            objBL.Query = "Select sum(Val(TaxableAmount)) as [SumTaxableAmount],sum(Val(CGSTPer)) as [SumCGSTPer],sum(Val(SGSTPer)) as [SumSGSTPer],sum(Val(IGSTPer)) as [SumIGSTPer],sum(Val(CGSTAmount)) as [SumCGSTAmount],sum(Val(SGSTAmount)) as [SumSGSTAmount],sum(Val(IGSTAmount)) as [SumIGSTAmount],sum(Val(TotalTaxAmount)) as [SumTotalTaxAmount],sum(Val(NetAmount)) as [SumNetAmount] from PurchaseTransaction where PurchaseID=" + PurchaseId + "";
            dsP = objBL.ReturnDataSet();
            if (dsP.Tables[0].Rows.Count > 0)
            {
                SumTaxableAmount = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumTaxableAmount"].ToString());
                SumCGSTPer = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumCGSTPer"].ToString());
                SumSGSTPer = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumSGSTPer"].ToString());
                SumIGSTPer = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumIGSTPer"].ToString());

                SumCGSTAmount = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumCGSTAmount"].ToString());
                SumSGSTAmount = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumSGSTAmount"].ToString());
                SumIGSTAmount = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumIGSTAmount"].ToString());

                SumTotalTaxAmount = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumTotalTaxAmount"].ToString());
                SumNetAmount = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumNetAmount"].ToString()); 
            }
        }

        private void Get_Sale()
        {
            DataSet dsP = new DataSet();
            //,CGSTAmount,SGSTPer,SGSTAmount,IGSTPer,IGSTAmount,TotalTaxAmount,NetAmount,UserId
            objBL.Query = "Select sum(Val(TaxableAmount)) as [SumTaxableAmount],sum(Val(CGSTPer)) as [SumCGSTPer],sum(Val(SGSTPer)) as [SumSGSTPer],sum(Val(IGSTPer)) as [SumIGSTPer],sum(Val(CGSTAmount)) as [SumCGSTAmount],sum(Val(SGSTAmount)) as [SumSGSTAmount],sum(Val(IGSTAmount)) as [SumIGSTAmount],sum(Val(TotalTaxAmount)) as [SumTotalTaxAmount],sum(Val(NetAmount)) as [SumNetAmount] from SaleTransaction where SaleID=" + SaleId + "";
            dsP = objBL.ReturnDataSet();
            if (dsP.Tables[0].Rows.Count > 0)
            {
                SumTaxableAmount = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumTaxableAmount"].ToString());
                SumCGSTPer = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumCGSTPer"].ToString());
                SumSGSTPer = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumSGSTPer"].ToString());
                SumIGSTPer = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumIGSTPer"].ToString());

                SumCGSTAmount = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumCGSTAmount"].ToString());
                SumSGSTAmount = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumSGSTAmount"].ToString());
                SumIGSTAmount = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumIGSTAmount"].ToString());

                SumTotalTaxAmount = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumTotalTaxAmount"].ToString());
                SumNetAmount = Convert.ToDouble(dsP.Tables[0].Rows[0]["SumNetAmount"].ToString());
            }
        }

        private void ExcelReport()
        {
            ClearPurchase();
            ClearPurchase_Transaction();
            ClearSale();

            string Cell = "";
            bool AmountFlag = false;

            DataSet dsSale = new DataSet();
            DataSet dsPurchase = new DataSet();

            if (chkDate.Checked)
                objBL.Query = "select P.ID,P.PurchaseDate,P.PurchaseNo,P.ChallanNo,P.BillNo,P.SupplierId,S.SupplierName,S.Address,S.MobileNumber,S.EmailId,S.GSTNumber,P.SubTotal,P.TotalGST,P.FreightCharges,P.LoadingAndPackingCharges,P.InsuranceCharges,P.OtherCharges,P.InvoiceTotal,P.PaymentMode,P.PrintFlag,P.PrintCount from Purchase P inner join Supplier S on S.ID=P.SupplierId where P.CancelTag=0 and S.CancelTag=0 and P.IsGST=1 and Val(P.TotalGST) >0";
            else
                objBL.Query = "select P.ID,P.PurchaseDate,P.PurchaseNo,P.ChallanNo,P.BillNo,P.SupplierId,S.SupplierName,S.Address,S.MobileNumber,S.EmailId,S.GSTNumber,P.SubTotal,P.TotalGST,P.FreightCharges,P.LoadingAndPackingCharges,P.InsuranceCharges,P.OtherCharges,P.InvoiceTotal,P.PaymentMode,P.PrintFlag,P.PrintCount from Purchase P inner join Supplier S on S.ID=P.SupplierId where P.CancelTag=0 and S.CancelTag=0 and P.PurchaseDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and P.IsGST=1 and Val(P.TotalGST) >0";

            dsPurchase = objBL.ReturnDataSet();

            if (chkDate.Checked)
                objBL.Query = "select S.CustomerId,S.ID as [Invoice No],S.InvoiceDate,C.CustomerName,C.GSTNumber,S.SubTotal,S.TotalGST,S.Total,S.InvoiceTotal,S.PaymentMode from Sale S inner join Customer C on C.ID=S.CustomerId where S.CancelTag=0 and Val(S.TotalGST) >0";
            else
                objBL.Query = "select S.CustomerId,S.ID as [Invoice No],S.InvoiceDate,C.CustomerName,C.GSTNumber,S.SubTotal,S.TotalGST,S.Total,S.InvoiceTotal,S.PaymentMode from Sale S inner join Customer C on C.ID=S.CustomerId where S.CancelTag=0 and S.InvoiceDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and Val(S.TotalGST) >0";

            dsSale = objBL.ReturnDataSet();

            if (dsPurchase.Tables[0].Rows.Count > 0 || dsSale.Tables[0].Rows.Count > 0)
            {
                ClearPurchase();

                objRL.FillCompanyData();

                DialogResult dr;
                dr = MessageBox.Show("Do you want to create this report.?", "Report View", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                    object misValue = System.Reflection.Missing.Value;
                    myExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    myExcelWorkbooks = myExcelApp.Workbooks;

                    objRL.ClearExcelPath();
                    objRL.isPDF = true;

                    objRL.Form_ExcelFileName = "GSTReport.xlsx";

                    objRL.Form_ReportFileName = "GSTReport-";
                    objRL.Form_DestinationReportFilePath = "GST Report\\";

                    objRL.Path_Comman();

                    myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                    Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                    myExcelWorksheet.get_Range("C2", misValue).Formula = objRL.CI_CompanyName;
                    myExcelWorksheet.get_Range("C1", misValue).Formula = objRL.CI_GSTIN;
                    myExcelWorksheet.get_Range("C3", misValue).Formula = "From Date: " + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat) + " To Date: " + dtpToDate.Value.ToString(RedundancyLogics.SystemDateFormat);

                    RowCount = 7;

                    //Purchase Reports

                    if (dsPurchase.Tables[0].Rows.Count > 0)
                    {
                        CellCheckCount = dsPurchase.Tables[0].Rows.Count;

                        myExcelWorksheet.get_Range("A5", misValue).Formula = "Purchase GST Report";

                        CellCheckCount = dsPurchase.Tables[0].Rows.Count;
                        SRNO = 1;
                        DateTime dt = new DateTime();

                        for (int i = 0; i < dsPurchase.Tables[0].Rows.Count; i++)
                        {
                            AFlag = 0;
                            ClearPurchase_Transaction();

                            PurchaseId = Convert.ToInt32(dsPurchase.Tables[0].Rows[i]["ID"].ToString());
                            Get_Purchase();
                            dtString = Convert.ToDateTime(dsPurchase.Tables[0].Rows[i]["PurchaseDate"].ToString());
                            DateString = dtString.ToString(RedundancyLogics.SystemDateFormat);
                            Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SRNO.ToString());
                            Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, dsPurchase.Tables[0].Rows[i]["SupplierName"].ToString());
                            Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, "27");
                            Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dsPurchase.Tables[0].Rows[i]["GSTNumber"].ToString());
                            Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, DateString);
                            Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dsPurchase.Tables[0].Rows[i]["BillNo"].ToString());
                            AmountFlag = true; AFlag = 1;
                            Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, SumTaxableAmount.ToString());
                            Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, "0");
                            Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, SumCGSTAmount.ToString());
                            Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, SumSGSTAmount.ToString());
                            Fill_Merge_Cell("K", "K", misValue, myExcelWorksheet, SumIGSTAmount.ToString());
                           
                            TotalGSTPer = SumCGSTPer + SumSGSTPer + SumIGSTPer;
                            TotalGSTAmount = SumCGSTAmount + SumSGSTAmount + SumIGSTAmount;

                            Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, TotalGSTPer.ToString());
                            Fill_Merge_Cell("M", "M", misValue, myExcelWorksheet, TotalGSTAmount.ToString());
                            Fill_Merge_Cell("N", "N", misValue, myExcelWorksheet, SumNetAmount.ToString());

                            Purchase_TaxableAmount += SumTaxableAmount;
                            Purchase_CGSTAmount += SumCGSTAmount;
                            Purchase_SGSTAmount += SumSGSTAmount;
                            Purchase_IGSTAmount += SumIGSTAmount;
                            Purchase_NetAmount += SumNetAmount;

                            Purchase_TotalTaxAmount += TotalGSTAmount;
                            RowCount++;
                            SRNO++;
                            Flag_Check = true;
                            AFlag=0;
                        }
                         
                        Fill_Merge_Cell("A", "F", misValue, myExcelWorksheet, "Total");
                        AFlag = 1;
                        Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, Purchase_TaxableAmount.ToString());
                        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, "0");
                        Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, Purchase_CGSTAmount.ToString());
                        Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, Purchase_SGSTAmount.ToString());
                        Fill_Merge_Cell("K", "K", misValue, myExcelWorksheet, Purchase_IGSTAmount.ToString());
                        Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, "0");
                        Fill_Merge_Cell("M", "M", misValue, myExcelWorksheet, Purchase_TotalTaxAmount.ToString());
                        Fill_Merge_Cell("N", "N", misValue, myExcelWorksheet, Purchase_NetAmount.ToString());
                        AFlag = 0;
                        RowCount++;
                    }

                    if (dsSale.Tables[0].Rows.Count > 0)
                    {
                        RowCount++;
                        ClearSale();
                        Fill_Merge_Cell("A", "N", misValue, myExcelWorksheet, "Sale GST Report");
                        RowCount++;
                        Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, "Sr.No");
                        Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, "Name of Party");
                        Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, "State of Party");
                        Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, "GST TIN ");
                        Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, "Invoice Date");
                        Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, "Invoice No.");
                        Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, "Taxable Amount");
                        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, "Discount Amount");
                        Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, "CGST");
                        Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, "SGST");
                        Fill_Merge_Cell("K", "K", misValue, myExcelWorksheet, "IGST");
                        Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, "Total GST Per");
                        Fill_Merge_Cell("M", "M", misValue, myExcelWorksheet, "Total GST");
                        Fill_Merge_Cell("N", "N", misValue, myExcelWorksheet, "Invoice Total");
                        RowCount++;
                        
                        SRNO = 1;
                        for (int i = 0; i < dsSale.Tables[0].Rows.Count; i++)
                        {
                            ClearPurchase_Transaction();
                            SaleId = Convert.ToInt32(dsSale.Tables[0].Rows[i]["Invoice No"].ToString());
                            Get_Sale();
                            dtString = Convert.ToDateTime(dsSale.Tables[0].Rows[i]["InvoiceDate"].ToString());
                            DateString = dtString.ToString(RedundancyLogics.SystemDateFormat);
                            Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SRNO.ToString());
                            Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, dsSale.Tables[0].Rows[i]["CustomerName"].ToString());
                            Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, "27");
                            Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dsSale.Tables[0].Rows[i]["GSTNumber"].ToString());
                            Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, DateString);
                            Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dsSale.Tables[0].Rows[i]["Invoice No"].ToString());
                            AmountFlag = true; AFlag = 1;
                            Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, SumTaxableAmount.ToString());
                            Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, "0");
                            Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, SumCGSTAmount.ToString());
                            Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, SumSGSTAmount.ToString());
                            Fill_Merge_Cell("K", "K", misValue, myExcelWorksheet, SumIGSTAmount.ToString());

                            TotalGSTPer = SumCGSTPer + SumSGSTPer + SumIGSTPer;
                            TotalGSTAmount = SumCGSTAmount + SumSGSTAmount + SumIGSTAmount;

                            Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, TotalGSTPer.ToString());
                            Fill_Merge_Cell("M", "M", misValue, myExcelWorksheet, TotalGSTAmount.ToString());
                            Fill_Merge_Cell("N", "N", misValue, myExcelWorksheet, SumNetAmount.ToString());

                            Sale_TaxableAmount += SumTaxableAmount;
                            Sale_CGSTAmount += SumCGSTAmount;
                            Sale_SGSTAmount += SumSGSTAmount;
                            Sale_IGSTAmount += SumIGSTAmount;
                            Sale_NetAmount += SumNetAmount;

                            Sale_TotalTaxAmount += TotalGSTAmount;
                            RowCount++;
                            SRNO++;
                            Flag_Check = true;
                            AFlag = 0;
                        }

                        Fill_Merge_Cell("A", "F", misValue, myExcelWorksheet, "Total");
                        AFlag = 1;
                        Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, Sale_TaxableAmount.ToString());
                        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, "0");
                        Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, Sale_CGSTAmount.ToString());
                        Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, Sale_SGSTAmount.ToString());
                        Fill_Merge_Cell("K", "K", misValue, myExcelWorksheet, Sale_IGSTAmount.ToString());
                        Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, "0");
                        Fill_Merge_Cell("M", "M", misValue, myExcelWorksheet, Sale_TotalTaxAmount.ToString());
                        Fill_Merge_Cell("N", "N", misValue, myExcelWorksheet, Sale_NetAmount.ToString());
                        AFlag = 0;
                        RowCount++;
                    }

                    if (Purchase_NetAmount != 0 || Sale_NetAmount != 0)
                    {
                        //Fill_Merge_Cell("A", "F", misValue, myExcelWorksheet, "GST Payable");
                        double GP_CGCSTAmount = 0, GP_SGCSTAmount = 0, GP_IGCSTAmount = 0, TotalPayble = 0;
                        
                        //GP_CGCSTAmount = Sale_CGSTAmount - Purchase_CGSTAmount;
                        //GP_SGCSTAmount = Sale_SGSTAmount - Purchase_SGSTAmount;
                        //GP_IGCSTAmount = Sale_IGSTAmount - Purchase_IGSTAmount;
                        //AFlag = 1;

                        //if (GP_IGCSTAmount < 0) GP_IGCSTAmount = 0;
                        //if (GP_SGCSTAmount < 0) GP_SGCSTAmount = 0;
                        //if (GP_IGCSTAmount < 0) GP_IGCSTAmount = 0;

                        //Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, GP_CGCSTAmount.ToString());
                        //Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, GP_SGCSTAmount.ToString());
                        //Fill_Merge_Cell("K", "K", misValue, myExcelWorksheet, GP_IGCSTAmount.ToString());

                        //Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, "");
                        //Fill_Merge_Cell("M", "M", misValue, myExcelWorksheet, "");
                        //Fill_Merge_Cell("N", "N", misValue, myExcelWorksheet, "");
                        //RowCount++;

                        //TotalPayble = GP_CGCSTAmount + GP_SGCSTAmount + GP_IGCSTAmount;
                        AFlag = 0;
                        Fill_Merge_Cell("A", "M", misValue, myExcelWorksheet, "Total Payable");
                        AFlag = 1;

                        if (TotalPayble < 0)
                            TotalPayble = 0;

                        double GSTPer = 1;
                        if (Sale_NetAmount != 0)
                        {
                            //TotalPayble = Sale_NetAmount * GSTPer / 100;
                            TotalPayble = Purchase_TotalTaxAmount - Sale_TotalTaxAmount;
                            TotalPayble = Math.Round(TotalPayble);
                            Fill_Merge_Cell("N", "N", misValue, myExcelWorksheet, TotalPayble.ToString());
                        }
                    }

                    myExcelWorkbook.Save();

                    string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
                    const int xlQualityStandard = 0;
                    myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);

                    myExcelWorkbook.Close(true, misValue, misValue);
                    myExcelApp.Quit();

                    //objRL.ShowMessage(21, 1);
                    MessageBox.Show("Report Generated Successfully");

                    DialogResult dr1;
                    dr1 = MessageBox.Show("Do you want to view this report.?", "Report View", MessageBoxButtons.YesNo);
                    if (dr1 == DialogResult.Yes)
                        System.Diagnostics.Process.Start(PDFReport);
                    //objRL.DeleteExcelFile();
                }
            }
            else
            {
                MessageBox.Show("No Record Found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        static bool AmountFlag;
        int AFlag = 0;
        
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

        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDate.Checked == true)
            {
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
            }
            else
            {
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ExcelReport();
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
    }
}
