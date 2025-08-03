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
using System.Drawing.Imaging;
using System.IO;

namespace SPApplication.Report
{
    public partial class ProductionQuantityReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;

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

        public ProductionQuantityReport()
        {
            InitializeComponent();
            objDL.Set_Report_Design(this, lblHeader, btnView, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_PRODUCTION_QUANTITY_REPORT);
        }

        private void ProductionQuantityReport_Load(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void ClearAll()
        {
            dataGridView1.DataSource = null;
            btnReport.Visible = false;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count >0)
                ExcelReport();
        }

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;

        private void FillGrid()
        {
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;
            UserClause = string.Empty;

            dataGridView1.DataSource = null;

            DataSet ds = new DataSet();

            MainQuery = "select PE.ID,PE.EntryDate as [Date],PE.EntryTime as [Time],PE.Shift,PE.MachinNo as [Machine No],PE.ProductId,P.ProductName,PE.PurchaseOrderNo as [PO Number],PE.ProductQty as [Bag Qty],PE.StickerHeader as [Sticker Header],PE.DateFlag,PE.PreformPartyId,PPM.PreformParty as [Preform Party],PE.FromRange,PE.ToRange,PE.ToRange as [Total Stickers],PE.UsedSticker as [Used Sticker],PE.ToRange-PE.UsedSticker as [Balance Stickers] from ((ProductionEntry PE inner join Product P on P.ID=PE.ProductId) inner join PreformPartyMaster PPM on PPM.ID=PE.PreformPartyId) where PE.CancelTag=0 and P.CancelTag=0 and PPM.CancelTag=0 and EntryDate=#" + dtpDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "#";
            OrderByClause = " order by Val(PE.MachinNo) asc,PE.ID asc"; //ORDER BY Val([YourTextField]) ASC;

            objBL.Query = MainQuery + OrderByClause;
            //objBL.Query = "select * from ProductionEntry where CancelTag=0 and EntryDate=#" + dtpDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "#";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 ID,
                //1 EntryDate as [Date],
                //2 EntryTime as [Time],
                //3 Shift,
                //4 MachinNo as [Machine No],
                //5 ProductId,
                //6 ProductName as [Product Name],
                //7 PurchaseOrderNo as [PO Number],
                //8 ProdutQty as [Bag Qty],
                //9 StickerHeader as [Sticker Header],
                //10 DateFlag
                //11 ,PE.PreformPartyId
                //12 PreformParty as [Preform Party
                //13 FromRange [From],
                //14 ToRange as [To]
                //15 PE.ToRange as [Total Stickers],
                //16 PE.UsedSticker as [Used Sticker],
                //17 PE.ToRange-PE.UsedSticker as [Balance Stickers] 

                //PE.ID,PE.EntryDate as [Date],PE.EntryTime as [Time],PE.Shift,PE.MachinNo as [Machine No],PE.ProductId,P.ProductName,PE.PurchaseOrderNo as [PO Number],PE.ProductQty as [Bag Qty],PE.StickerHeader as [Sticker Header],PE.DateFlag,PPM.PreformParty as [Preform Party]

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;

                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 60;
                dataGridView1.Columns[3].Width = 50;
                dataGridView1.Columns[4].Width = 80;
                dataGridView1.Columns[6].Width = 400;
                dataGridView1.Columns[8].Width = 60;
                dataGridView1.Columns[13].Width = 80;
                dataGridView1.Columns[14].Width = 80;
                dataGridView1.Columns[15].Width = 100;
                dataGridView1.Columns[16].Width = 100;
                dataGridView1.Columns[17].Width = 100;

                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                btnReport.Visible = true;
            }
        }

        private void ExcelReport()
        {
           
            //0 ID,
            //1 EntryDate as [Date],
            //2 EntryTime as [Time],
            //3 Shift,
            //4 MachinNo as [Machine No],
            //5 ProductId,
            //6 ProductName as [Product Name],
            //7 PurchaseOrderNo as [PO Number],
            //8 ProdutQty as [Bag Qty],
            //9 StickerHeader as [Sticker Header],
            //10 DateFlag
            //11 ,PE.PreformPartyId
            //12 PreformParty as [Preform Party
            //13 FromRange [From],
            //14 ToRange as [To]
            //15 UsedSticker as [Used Sticker]

           
            if (dataGridView1.Rows.Count > 0)
            {
                objRL.FillCompanyData();

                object misValue = System.Reflection.Missing.Value;
                myExcelApp = new Microsoft.Office.Interop.Excel.Application();
                myExcelWorkbooks = myExcelApp.Workbooks;

                objRL.ClearExcelPath();
                objRL.isPDF = true;
                objRL.Form_ExcelFileName = "ProductionReport.xlsx";
                objRL.Form_ReportFileName = "Production Quantity Report-" + DateTime.Now.Date.ToString("dd-MMM-yyyy");
                objRL.Form_DestinationReportFilePath = "Production Report\\";

                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;


                myExcelWorksheet.get_Range("B5", misValue).Formula = dtpDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);

                //Report Date
                myExcelWorksheet.get_Range("G5", misValue).Formula = DateTime.Now.Date.ToString(BusinessResources.DATEFORMATDDMMYYYY);

                SrNo = 1; RowCount = 8; BorderFlag = false;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    AFlag = 1;
                    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SrNo.ToString());

                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[3].Value)))
                    {
                        AFlag = 1;
                        Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, Convert.ToString(dataGridView1.Rows[i].Cells[3].Value));
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[4].Value)))
                    {
                         
                        Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, Convert.ToString(dataGridView1.Rows[i].Cells[4].Value));
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[6].Value)))
                    {
                        AFlag = 0;
                        Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, Convert.ToString(dataGridView1.Rows[i].Cells[6].Value));
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[8].Value)))
                    {
                        AFlag = 2;
                        Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, Convert.ToString(dataGridView1.Rows[i].Cells[8].Value));
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[15].Value)))
                    {
                        //AFlag = 0;
                        Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, Convert.ToString(dataGridView1.Rows[i].Cells[15].Value));
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[15].Value)))
                    {
                        //AFlag = 0;
                        Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, Convert.ToString(dataGridView1.Rows[i].Cells[16].Value));
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[15].Value)))
                    {
                        //AFlag = 0;
                        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, Convert.ToString(dataGridView1.Rows[i].Cells[17].Value));
                    }
                    RowCount++;
                    SrNo++;
                }

                
                myExcelWorkbook.Save();
                string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
                const int xlQualityStandard = 0;
                myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                myExcelWorkbook.Close(true, misValue, misValue);
                myExcelApp.Quit();
                // objRL.ShowMessage(22, 1);

                //DialogResult dr1;
                //dr1 = MessageBox.Show("Do you want to view this report.?", "Report View", MessageBoxButtons.YesNo);
                //if (dr1 == DialogResult.Yes)

                //System.Diagnostics.Process.Start(PDFReport);
                System.Diagnostics.Process.Start(PDFReport);
                //System.Diagnostics.Process.Start(objRL.RL_DestinationPath);
                //objRL.DeleteExcelFile();

                //if (!string.IsNullOrEmpty(objRL.EmailId) && cbEmail.Checked)
                //{
                //    objRL.EmailId_RL = objRL.EmailId;
                //    objRL.Subject_RL = "Working Report";
                //    //string body = "<div><p>Dear " + objRL.CustomerName_Customer + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
                //    string body = "<div> <p>Please find attachment of pdf file.</p><p>Thanks,</p><p>" + objRL.CI_CompanyName + "</p></div>";

                //    objRL.Body_RL = body;// "Dear " + RedundancyLogics.SupplierName + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
                //    objRL.FilePath_RL = PDFReport;
                //    objRL.SendEMail();
                //}
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
            else
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            if (!BorderFlag)
                DrawBorder(AlingRange2);

            if (MH_Value)
            {
                AlingRange2.RowHeight = 40;
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                AlingRange2.WrapText = true;
            }
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnView.Focus();
        }
    }
}
