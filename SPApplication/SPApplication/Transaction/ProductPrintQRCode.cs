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
using Zen.Barcode;
using System.IO;
using System.Drawing.Imaging;

namespace SPApplication.Transaction
{
    public partial class ProductPrintQRCode : Form
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
        DateTime BookingDate, BookingTime;

        Excel.Application myExcelApp;
        Excel.Workbooks myExcelWorkbooks;
        Excel.Workbook myExcelWorkbook;

        public ProductPrintQRCode()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_QUALITY_CONTROL_REGISTER_ENTRY);
        }

        private void ProductPrintQRCode_Load(object sender, EventArgs e)
        {
            //Product 
            //400ML premium Spices Jars Set of 5
            //MRP Offer Price- MRP-49 
        }

        private void txtSearchProductName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        string QRImagePath = string.Empty;
        string BarcodeText = "MRP-49";

        private void GenerateBarCode()
        {
            string BarCode = "12345678";//   string.Empty;
            Bitmap bitmap = new Bitmap(BarCode.Length * 40, 150);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                System.Drawing.Font oFont = new System.Drawing.Font("Calibri", 20);
                PointF point = new PointF(2f, 2f);
                SolidBrush black = new SolidBrush(Color.Black);
                SolidBrush White = new SolidBrush(Color.White);
                graphics.FillRectangle(White, 0, 0, bitmap.Width, bitmap.Height);
                graphics.DrawString("*" + BarCode + "*", oFont, black, point);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                pbCode.Image = bitmap;
                pbCode.Height = bitmap.Height;
                pbCode.Width = bitmap.Width;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Graphics g = ev.Graphics;

            //string BarcodeText = "400ML premium Spices Jars Set of 5";
            

            //BarcodeDraw bdraw = BarcodeDrawFactory.GetSymbology(BarcodeSymbology.Code128);
            //Image barcodeImage = bdraw.Draw(BarcodeText, 40);
            //g.DrawImage(barcodeImage, 0, 0);


            //Zen.Barcode.BarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeEan13WithChecksum;
            //pbQRCode.Image = qrcode.Draw(BarcodeText.ToString(), 10);

            GetReportSingle();

            //BarcodeDraw bdraw = BarcodeDrawFactory.GetSymbology(BarcodeSymbology.Code128);
            //Image barcodeImage = bdraw.Draw(BarcodeText, 20);
            //pbQRCode.Image = barcodeImage;



            //QRImagePath = objRL.GetPath("ImagePath");
            //var filePath = QRImagePath;
            //Directory.CreateDirectory(filePath);
            //string FileName = "NewProduct";
            //pbQRCode.Image.Save(Path.Combine(filePath, FileName), System.Drawing.Imaging.ImageFormat.Png);
            //rtbStickerHeader.Text = BarcodeText.ToString();

        }

        private void RunBarCode()
        {
           //// BarcodeDraw bdraw = BarcodeDrawFactory.GetSymbology(BarcodeSymbology.Code128);
           // Zen.Barcode.Code128BarcodeDraw bdraw = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;

           // Image barcodeImage = bdraw.Draw(BarcodeText, 20);
           // pbQRCode.Image = barcodeImage;


            string BarCode = "12345678";//   string.Empty;
            //Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            //var image = barcode.Draw(BarCode, 50);

            //using (var graphics = Graphics.FromImage(image))
            ////System.Drawing.Font oFont = new System.Drawing.Font("Calibri", 20);
            //using (var font = new System.Drawing.Font("Consolas", 12)) // Any font you want
            //using (var brush = new SolidBrush(Color.White))
            //using (var format = new StringFormat() { LineAlignment = StringAlignment.Far }) // To align text above the specified point
            //{
            //    // Print a string at the left bottom corner of image
            //    graphics.DrawString(BarCode, font, brush, 0, image.Height, format);
            //}

            //pbQRCode.Image = image;


            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            var barcodeImage = barcode.Draw(BarCode, 50);

            var resultImage = new Bitmap(barcodeImage.Width, barcodeImage.Height + 20); // 20 is bottom padding, adjust to your text

            using (var graphics = Graphics.FromImage(resultImage))
            using (var font = new System.Drawing.Font("Consolas", 10)) // Any font you want
            using (var brush = new SolidBrush(Color.Black))
            using (var format = new StringFormat()
            {
                Alignment = StringAlignment.Center, // Also, horizontally centered text, as in your example of the expected output
                LineAlignment = StringAlignment.Far
            })
            {
                graphics.Clear(Color.White);
                graphics.DrawImage(barcodeImage, 0, 0);
                graphics.DrawString(BarCode, font, brush, resultImage.Width / 2, resultImage.Height, format);
            }

            pbQRCode.Image = resultImage;

            //Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            //pbQRCode.Image = qrcode.Draw(QRCodeData.ToString(), 10);

            QRImagePath = objRL.GetPath("ImagePath");
            var filePath = QRImagePath;
            Directory.CreateDirectory(filePath);
            string FileName = "Test"; //.ToString();
            pbQRCode.Image.Save(Path.Combine(filePath, FileName), System.Drawing.Imaging.ImageFormat.Png);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            RunBarCode();

            //GenerateBarCode();


            //string barcode = txtSearchProductName.Text;

            //Bitmap bitm = new Bitmap(barcode.Length * 45, 160);
            //using (Graphics graphic = Graphics.FromImage(bitm))
            //{


            //    Font newfont = new System.Drawing.Font("IDAutomationHC39M", 20);
            //    PointF point = new PointF(2f, 2f);
            //    SolidBrush black = new SolidBrush(Color.Black);
            //    SolidBrush white = new SolidBrush(Color.White);
            //    graphic.FillRectangle(white, 0, 0, bitm.Width, bitm.Height);
            //    graphic.DrawString("*" + barcode + "*", newfont, black, point);
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void SetQRCode(Excel.Worksheet myExcelWorksheet, int RowNo, int ColumnNo)
        {
            QRImagePath = objRL.GetPath("ImagePath") + "Test";
            Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[RowNo, ColumnNo];
            float Left = (float)((double)oRange.Left);
            float Top = (float)((double)oRange.Top);
            const float ImageSize = 45;
            oRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            oRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);

            // myExcelWorksheet.get_Range("A1", "A14").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
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
                objRL.Form_ExcelFileName = "BarCode50X25.xlsx";
                objRL.Form_ReportFileName = "ID-New";
                objRL.Form_DestinationReportFilePath = "\\Stickers\\";
                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                //SetQRCode(myExcelWorksheet, 2, 2);
                SetQRCode(myExcelWorksheet, 1, 1);
                myExcelWorksheet.get_Range("A6", misValue).Formula = BarcodeText.ToString();
 
                myExcelWorkbook.Save();
                 
                try
                {


                    //const int xlQualityStandard = 0;
                    //myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
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
    }
}
