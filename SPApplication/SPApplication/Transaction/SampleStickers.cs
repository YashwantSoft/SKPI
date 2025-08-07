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

namespace SPApplication.Transaction
{
    public partial class SampleStickers : Form
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

        public SampleStickers()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            GetReportSingle();
        }

        string CapName = string.Empty, QRImagePath = string.Empty;

        private void SetQRCode(Excel.Worksheet myExcelWorksheet, int RowNo, int ColumnNo)
        {
            float ImageSize = 40;
            //QRImagePath = objRL.GetPath("CapImagePath") + txtID.Text;

            string imgPath = @"D:\Softwares\Reports\SKPI Reports\TestImg\3.jpg";

            //QRImagePath = imgPath; // objRL.GetPath("CapImagePath") + txtID.Text;
            
            //Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[RowNo, ColumnNo];
            //float Left = (float)((double)oRange.Left);
            //float Top = (float)((double)oRange.Top);
            ////const float ImageSize = 40; 

            ////if (cmbPrint.Text == "100X50")
            //    ImageSize = 150;

            //// const float ImageSize = 50;

            //oRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //oRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
            //// myExcelWorksheet.get_Range("A1", "A14").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


            //// Target dimensions in cm
            //double widthCm = 75.0;
            //double heightCm = 50.0;

            //// Convert cm to points (1 cm = 28.35 points)
            //float widthPoints = (float)(widthCm * 28.35);   // ≈ 2126.25
            //float heightPoints = (float)(heightCm * 28.35); // ≈ 1417.5

            //// Optional: set zoom for better visibility
            //myExcelWorksheet.Application.ActiveWindow.Zoom = 50;

            //// Insert image
            //Excel.Shape picture = myExcelWorksheet.Shapes.AddPicture(
            //    Filename: imgPath,
            //    LinkToFile: Microsoft.Office.Core.MsoTriState.msoFalse,
            //    SaveWithDocument: Microsoft.Office.Core.MsoTriState.msoCTrue,
            //    Left: 0,     // Position from left in points
            //    Top: 0,      // Position from top in points
            //    Width: widthPoints,
            //    Height: heightPoints
            //);


            // Convert mm to points
            double widthMm = 65.0;
            double heightMm = 45.0;

            float widthPoints = (float)(widthMm * 2.83465);   // ≈ 212.6 points
            float heightPoints = (float)(heightMm * 2.83465); // ≈ 141.73 points

            // Adjust column width and row height
            // Column width in Excel ≈ points / 7
            //myExcelWorksheet.Columns["A"]  = widthPoints / 7.0; // ≈ 30.37
            //myExcelWorksheet.Rows[1]  = heightPoints;             // in points

            // Insert image at cell A1 (Top=0, Left=0)
            Excel.Shape picture = myExcelWorksheet.Shapes.AddPicture(
                Filename: imgPath,
                LinkToFile: Microsoft.Office.Core.MsoTriState.msoFalse,
                SaveWithDocument: Microsoft.Office.Core.MsoTriState.msoTrue,
                Left: 0,
                Top: 0,
                Width: widthPoints,
                Height: heightPoints
            );


            //// Optionally adjust row height and column width to fit image
            //myExcelWorksheet.Rows["1:1"] = heightPoints;
            //myExcelWorksheet.Columns["A:A"] = widthPoints / 7; // rough conversion
        }

        public void GetReportSingle()
        {
            using (new CursorWait())
            {
                BorderFlag = false;

                object misValue = System.Reflection.Missing.Value;
                myExcelApp = new Excel.Application();
                myExcelWorkbooks = myExcelApp.Workbooks;

                objRL.ClearExcelPath();
                objRL.isPDF = true;

                //if (cmbPrint.Text == "50X25")
                //    objRL.Form_ExcelFileName = "CapQR50X25.xlsx";
                //else
                
                //objRL.Form_ExcelFileName = "QR100X50.xlsx";
                    
                objRL.Form_ExcelFileName = "SampleSticker.xlsx";

                objRL.Form_ReportFileName = "ID-3" ;
                objRL.Form_DestinationReportFilePath = "\\Sample Stickers\\";
                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                //CapName = lblCapName.Text;
                //WadName = lblWadName.Text;

                string sourceString = CapName, removeString = " ";
                int index = sourceString.IndexOf(removeString);
                string cleanPath = (index < 0)
                    ? sourceString
                    : sourceString.Remove(index, removeString.Length);

                CapName = cleanPath;

                //if (cmbPrint.Text == "50X25")
                //{
                //    //50X25 Label
                //    myExcelWorksheet.get_Range("A1", misValue).Formula = CapName.ToString();
                //    myExcelWorksheet.get_Range("A3", misValue).Formula = WadName.ToString();
                //    myExcelWorksheet.get_Range("A6", misValue).Formula = "Qty-" + txtQty.Text.ToString();
                //    myExcelWorksheet.get_Range("A7", misValue).Formula = txtBatchNo.Text.ToString();
                //    SetQRCode(myExcelWorksheet, 3, 2);
                //}
                //else
                //{
                    //100X50 Label
                    //myExcelWorksheet.get_Range("A1", misValue).Formula = CapName.ToString();
                    //myExcelWorksheet.get_Range("A3", misValue).Formula = WadName.ToString();
                    //myExcelWorksheet.get_Range("A5", misValue).Formula = "Qty-" + txtQty.Text.ToString();
                    //myExcelWorksheet.get_Range("A6", misValue).Formula = txtBatchNo.Text.ToString();
                    //SetQRCode(myExcelWorksheet, 5, 4);

                    //myExcelWorksheet.get_Range("A1", misValue).Formula = CapName.ToString();
                    //myExcelWorksheet.get_Range("A4", misValue).Formula = WadName.ToString();
                    //myExcelWorksheet.get_Range("A7", misValue).Formula = "Qty-" + txtQty.Text.ToString();
                    //myExcelWorksheet.get_Range("A8", misValue).Formula = txtBatchNo.Text.ToString();

                    SetQRCode(myExcelWorksheet,2, 2);
                //}

                myExcelWorkbook.Save();

                try
                {
                    myExcelWorkbook.Close(true, misValue, misValue);
                    myExcelApp.Quit();

                    System.Diagnostics.Process.Start(objRL.RL_DestinationPath);
                }
                catch (Exception ex1)
                {
                    objRL.ShowMessage(27, 4);
                    return;
                }
            }
        }
        public class CursorWait : IDisposable
        {
            public CursorWait(bool appStarting = false, bool applicationCursor = false)
            {
                // Wait
                Cursor.Current = appStarting ? Cursors.AppStarting : Cursors.WaitCursor;
                if (applicationCursor) System.Windows.Forms.Application.UseWaitCursor = true;
            }

            public void Dispose()
            {
                // Reset
                Cursor.Current = Cursors.Default;
                System.Windows.Forms.Application.UseWaitCursor = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //New Code

        //public void InsertExactSizedImageToExcel(string excelPath, string imagePath)
        //{
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //    double dpi = 96.0;
        //    double widthCm = 75;
        //    double heightCm = 50;

        //    int widthPx = (int)((widthCm / 2.54) * dpi);   // ≈ 2835 px
        //    int heightPx = (int)((heightCm / 2.54) * dpi); // ≈ 1890 px

        //    double excelColWidth = widthPx / 7.0;          // ≈ 405
        //    double excelRowHeight = heightPx * 0.75;       // ≈ 1417

        //    using (var package = new ExcelPackage())
        //    {
        //        var worksheet = package.Workbook.Worksheets.Add("Sheet1");

        //        int targetRow = 1;
        //        int targetColumn = 1;

        //        // Set column width and row height to match the image
        //        worksheet.Column(targetColumn).Width = excelColWidth;
        //        worksheet.Row(targetRow).Height = excelRowHeight;

        //        using (var image = Image.FromFile(imagePath))
        //        {
        //            var picture = worksheet.Drawings.AddPicture("MyImage", image);

        //            // Position at cell A1 (0-based index)
        //            picture.SetPosition(targetRow - 1, 0, targetColumn - 1, 0);

        //            // Set exact size in pixels
        //            picture.SetSize(widthPx, heightPx);
        //        }

        //        // Save Excel file
        //        package.SaveAs(new FileInfo(excelPath));
        //    }
        //}
    }
}
