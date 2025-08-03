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
    public partial class PurchaseReport : Form
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

        public PurchaseReport()
        {
            InitializeComponent();
            objBL.Set_Report_Design(this, lblHeader, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_PURCHASEREPORT);
        }
       
        private void PurchaseReport_Load(object sender, EventArgs e)
        {
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            ClearAll();
            objRL.Fill_Supplier(cmbSupplierName);
            cmbSupplierName.SelectedIndex = -1;
            chkSupplier.Checked = true;
            ClearAll();
        }

        public void ClearAll()
        {
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            chkSupplier.Checked = true;
            cbToday.Checked = true;
            cmbSupplierName.SelectedIndex = -1;
            dtpFromDate.Focus();
        }
       

        protected bool Validation()
        {
            objEP.Clear();
            //if (chkSupplier.Checked == false)
            //{
            //    if (cmbSupplierName.SelectedIndex == -1)
            //    {
            //        objEP.SetError(cmbSupplierName, "Select Supplier Name");
            //        cmbSupplierName.Focus();
            //        return true;
            //    }
            //    else
            //        return false;
            //}
            //else
                return false;
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                ExcelReport();
                //ExcelReport();
            }
            else
            {
                objRL.ShowMessage(21, 4);
                return;
            }
        }

        int PurchaseId = 0,AFlag = 0;
        double SumCGSTPer = 0, SumSGSTPer = 0, SumIGSTPer = 0, SumCGSTAmount = 0, SumSGSTAmount = 0, SumIGSTAmount = 0;
        double SumTaxableAmount = 0, SumTotalTaxAmount = 0, SumNetAmount = 0, TotalGSTPer = 0, TotalGSTAmount = 0;

        int   SaleId = 0;

        double Purchase_TaxableAmount = 0, Purchase_TotalTaxAmount = 0, Purchase_NetAmount = 0, Purchase_CGSTAmount = 0, Purchase_SGSTAmount = 0, Purchase_IGSTAmount = 0;
        double Sale_TaxableAmount = 0, Sale_TotalTaxAmount = 0, Sale_NetAmount = 0, Sale_CGSTAmount = 0, Sale_SGSTAmount = 0, Sale_IGSTAmount = 0;
        DateTime dtString;

        private void ClearPurchase()
        {
            SumCGSTPer = 0; SumSGSTPer = 0; SumIGSTPer = 0; SumCGSTAmount = 0; SumSGSTAmount = 0; SumIGSTAmount = 0;
            SumTaxableAmount = 0; SumTotalTaxAmount = 0; SumNetAmount = 0; TotalGSTPer = 0; TotalGSTAmount = 0;
            Purchase_TaxableAmount = 0; Purchase_TotalTaxAmount = 0; Purchase_NetAmount = 0; Purchase_CGSTAmount = 0; Purchase_SGSTAmount = 0; Purchase_IGSTAmount = 0;
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
        private void ClearPurchase_Transaction()
        {
            SumCGSTPer = 0; SumSGSTPer = 0; SumIGSTPer = 0; SumCGSTAmount = 0; SumSGSTAmount = 0; SumIGSTAmount = 0;
            SumTaxableAmount = 0; SumTotalTaxAmount = 0; SumNetAmount = 0; TotalGSTPer = 0; TotalGSTAmount = 0;
        }
       
        bool Flag_Check = false;
        string DateString = "";
       
        private void ExcelReport()
        {
            string Cell = "";
            bool AmountFlag = false;
            DataSet dsPurchase = new DataSet();
            object misValue = System.Reflection.Missing.Value;


            if (chkSupplier.Checked)
                objBL.Query = "select P.ID,P.PurchaseDate,P.PurchaseNo,P.ChallanNo,P.BillNo,P.SupplierId,S.SupplierName,S.Address,S.MobileNumber,S.EmailId,S.GSTNumber,P.SubTotal,P.TotalGST,P.FreightCharges,P.LoadingAndPackingCharges,P.InsuranceCharges,P.OtherCharges,P.InvoiceTotal,P.PaymentMode,P.PrintFlag,P.PrintCount from Purchase P inner join Supplier S on S.ID=P.SupplierId where P.CancelTag=0 and S.CancelTag=0 and P.IsGST=1 and P.PurchaseDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# order by P.PurchaseDate desc";
            else
                objBL.Query = "select P.ID,P.PurchaseDate,P.PurchaseNo,P.ChallanNo,P.BillNo,P.SupplierId,S.SupplierName,S.Address,S.MobileNumber,S.EmailId,S.GSTNumber,P.SubTotal,P.TotalGST,P.FreightCharges,P.LoadingAndPackingCharges,P.InsuranceCharges,P.OtherCharges,P.InvoiceTotal,P.PaymentMode,P.PrintFlag,P.PrintCount from Purchase P inner join Supplier S on S.ID=P.SupplierId where P.CancelTag=0 and S.CancelTag=0 and P.SupplierId=" + cmbSupplierName.SelectedValue + " and P.PurchaseDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and P.IsGST=1 order by P.PurchaseDate desc";

            dsPurchase = objBL.ReturnDataSet();

            if (dsPurchase.Tables[0].Rows.Count > 0)
            {
                objRL.FillCompanyData();

                DialogResult dr;
                dr = MessageBox.Show("Do you want to create this report.?", "Report View", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {

                    myExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    myExcelWorkbooks = myExcelApp.Workbooks;

                    objRL.ClearExcelPath();
                    objRL.isPDF = true;

                    objRL.Form_ExcelFileName = "PurchaseReport.xlsx";

                    if(chkSupplier.Checked)
                        objRL.Form_DestinationReportFilePath = "Purchase Report\\";
                    else
                        objRL.Form_DestinationReportFilePath = "Purchase Report\\" + cmbSupplierName.Text + "\\";

                    objRL.Path_Comman();

                    myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                    Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                    myExcelWorksheet.get_Range("C1", misValue).Formula = objRL.CI_GSTIN;
                    myExcelWorksheet.get_Range("C2", misValue).Formula = objRL.CI_CompanyName;
                    myExcelWorksheet.get_Range("C3", misValue).Formula = "From Date: " + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat) + " To Date: " + dtpToDate.Value.ToString(RedundancyLogics.SystemDateFormat);

                    RowCount = 6;

                    if (dsPurchase.Tables[0].Rows.Count > 0)
                    {
                        myExcelWorksheet.get_Range("A5", misValue).Formula = "Purchase Report";
                        SRNO = 1;
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
                            AFlag = 0;
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
                }

                myExcelWorkbook.Save();

                string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
                const int xlQualityStandard = 0;
                myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);

                myExcelWorkbook.Close(true, misValue, misValue);
                myExcelApp.Quit();

                //objRL.ShowMessage(22, 1);
                //DialogResult dr1;
                //dr1 = MessageBox.Show("Do you want to view this report.?", "Report View", MessageBoxButtons.YesNo);
                //if (dr1 == DialogResult.Yes)
                    System.Diagnostics.Process.Start(PDFReport);
                //objRL.DeleteExcelFile();
            }
            else
            {
                objRL.ShowMessage(25, 4);
                return;
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void chkSupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSupplier.Checked)
            {
                cmbSupplierName.SelectedIndex = -1;
                cmbSupplierName.Enabled = false;
            }
            else
            {
                cmbSupplierName.SelectedIndex = -1;
                cmbSupplierName.Enabled = true;
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
