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
    public partial class CapLabel : Form
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

        public CapLabel()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_CAPLABEL);
            btnSearch.Text = BusinessResources.BTN_SEARCH;
            btnSearch.BackColor = objDL.GetBackgroundColor();
            btnSearch.ForeColor = objDL.GetForeColorError();

            objRL.Fill_WadFitterNames(cmbWadFitter);
            objRL.Fill_Cap_ListBox(lbCap, txtSearchCap.Text, "All");
            objRL.Fill_Wad_ListBox(lbWad, txtSearchWad.Text, "All");
            GetID();
            ShiftCode();
        }

        bool ShiftFlag = false;
        string Shift = string.Empty;

        private void ShiftCode()
        {
            //TimeSpan StartTimeShift1 = new TimeSpan(07, 0, 0); //10 o'clock
            //TimeSpan EndTimeShift1 = new TimeSpan(15, 0, 0); //12 o'clock

            //TimeSpan StartTimeShift2 = new TimeSpan(15, 0, 0); //10 o'clock
            //TimeSpan EndTimeShift2 = new TimeSpan(23, 0, 0); //12 o'clock

            //TimeSpan StartTimeShift3 = new TimeSpan(23, 0, 0); //10 o'clock
            //TimeSpan EndTimeShift3 = new TimeSpan(7, 0, 0); //12 o'clock

            //TimeSpan StartTimeShift3Other = new TimeSpan(24, 0, 0); //10 o'clock

            //DateTime TodayTime;
            //TimeSpan now = DateTime.Now.TimeOfDay;
            ////TimeSpan now = TodayTime.;

            //ShiftFlag = false;
            //if ((now > StartTimeShift1) && (now < EndTimeShift1))
            //    Shift = "I";
            //else if ((now > StartTimeShift2) && (now < EndTimeShift2))
            //    Shift = "II";
            //else
            //{
            //    Shift = "III";
            //    int Checkhours = now.Hours;
            //    if (Checkhours == 23)
            //        ShiftFlag = false;
            //    else
            //        ShiftFlag = true;
            //}
            //txtShift.Text = Shift.ToString();

            Shift = objRL.ShiftCode();
            txtShift.Text = Shift.ToString();
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("CapLabelEntry"));
            txtID.Text = IDNo.ToString();
        }

        private void CapLabel_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            lbCap.Focus();
        }

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;
        bool DateFlag = false;
        bool SearchTag = false;
        bool IDFlag = false;

        protected void FillGrid()
        {
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;
            UserClause = string.Empty;

            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (BusinessLayer.UserName_Static != BusinessResources.USER_ADMIN)
                UserClause = " and UserId = " + BusinessLayer.UserId_Static;
            else
                UserClause = string.Empty;

            MainQuery = "select ID,EntryDate as [Date],EntryTime as [Time],Shift,CapId,CapName as [Cap Name],WadId,WadName as [Wad Name],Qty,PONumber,WadFitter  as [Wad Fitter],BatchNumber  as [Batch No],StickerHeader as [Sticker Header] from CapLabelEntry where CancelTag=0 ";
            OrderByClause = " order by EntryDate desc";

            if (DateFlag)
                WhereClause = " and EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            else if (SearchTag)
                WhereClause = " and CapName like '%" + txtSearch.Text + "%'";
            else if (IDFlag)
                WhereClause = " and ID=" + txtSearchID.Text + "";
            else
                WhereClause = string.Empty;

            objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 ID,
                //1 EntryDate as [Date],
                //2 EntryTime as [Time],
                //3 Shift,
                //4 CapId,
                //5 CapName as [Cap Name],
                //6 WadId,
                //7 WadName as [Wad Name],
                //8 Qty,
                //9 PONumber,
                //10 WadFitter  as [Wad Fitter],
                //11 BatchNumber  as [Batch No],
                //12 StickerHeader as [Sticker Header]
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[5].Width = 350;
                dataGridView1.Columns[7].Width = 350;
                dataGridView1.Columns[8].Width = 60;
                dataGridView1.Columns[10].Width = 200;
                dataGridView1.Columns[11].Width = 100;
                dataGridView1.Columns[12].Width = 150;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSearchCap_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Cap();
            if (txtSearchCap.Text != "")
            {
                objRL.Fill_Cap_ListBox(lbCap, txtSearchCap.Text, "Text");
                //lbItem.Focus();
            }
            else
            {
                lbCap.Visible = true;
                objRL.Fill_Cap_ListBox(lbCap, txtSearchCap.Text, "All");
            }
        }

        int CapId = 0;
        private void ClearAll_Cap()
        {
            CapId = 0;
            lblCapName.Text = "";
        }

        private void txtSearchWad_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Wad();
            if (txtSearchWad.Text != "")
            {
                objRL.Fill_Wad_ListBox(lbWad, txtSearchWad.Text, "Text");
                //lbItem.Focus();
            }
            else
            {
                lbCap.Visible = true;
                objRL.Fill_Wad_ListBox(lbWad, txtSearchWad.Text, "All");
            }
        }
        int WadId = 0;
        private void ClearAll_Wad()
        {
            WadId = 0;
            lblWadName.Text = "";
        }

        string CapDetails = string.Empty;
        string Wad = string.Empty;

        private void ClearAllCap()
        {
            WadName = string.Empty;
            gbWad.Visible = false;
            CapId = 0;
            WadId = 0;
            Wad = "";
            txtSearchWad.Text = "";
            txtQty.Text = "";
            cmbWadFitter.SelectedIndex = -1;
            txtPurchaseOrderNo.Text = "";
            txtBatchNo.Text = "";
            lblWadName.Text = "";
            pbQRCode.Image = null;
            rtbStickerHeader.Text = "";
        }

        private void Fill_Cap_Information()
        {
            ClearAllCap();

            if (TableID == 0)
                CapId = Convert.ToInt32(lbCap.SelectedValue);

            if (CapId != 0)
            {
                lblCapName.Text = "";
                CapDetails = string.Empty;
                Wad = string.Empty;
                objRL.Get_Cap_Records_By_Id(CapId);

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.CapName)))
                    CapDetails = objRL.CapName;
                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Wad)))
                    Wad = objRL.Wad;

                CapId = Convert.ToInt32(objRL.CapId);
                lblCapName.Text = objRL.CapName.ToString();
                lblCapName.BackColor = Color.Cyan;

                if (Wad == "Y")
                {
                    gbWad.Visible = true;
                    lbWad.Focus();
                }
                else
                {
                    gbWad.Visible = false;
                    txtQty.Focus();
                }
            }
        }

        string WadName = string.Empty;
        private void Fill_Wad_Information()
        {
            if (TableID == 0)
                WadId = Convert.ToInt32(lbWad.SelectedValue);

            if (WadId != 0)
            {
                lblWadName.Text = "";
                objRL.Get_Wad_Records_By_Id(WadId);

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.WadName)))
                    WadName = objRL.WadName;

                lblWadName.Text = objRL.WadName.ToString();
                lblWadName.BackColor = Color.Yellow;
                txtQty.Focus();
            }
        }

        private void lbCap_Click(object sender, EventArgs e)
        {
            Fill_Cap_Information();
        }

        private void lbCap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Fill_Cap_Information();
        }

        private void lbWad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Fill_Wad_Information();
            }
        }

        private void lbWad_Click(object sender, EventArgs e)
        {
            Fill_Wad_Information();
        }

        private void cmbWadFitter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetWADFitter();
        }

        string WadFitterName = string.Empty;
        string WadFitterValue = string.Empty;
        string BatchNumber = string.Empty;

        private void GetWADFitter()
        {
            if (cmbWadFitter.SelectedIndex > -1)
            {
                WadFitterName = string.Empty;
                WadFitterValue = string.Empty;
                WadFitterName = cmbWadFitter.Text;
                string str = WadFitterName;
                //str.Split(' ').ToList().ForEach(i => Console.Write(i[0] + " "));
                str.Split(' ').ToList().ForEach(i => WadFitterValue += i[0].ToString());
                Set_Batch_Number();
            }
        }

        string QRCodeData = string.Empty;
        string DataDisplay = string.Empty;
        string YearS = string.Empty;
        string curYear = string.Empty;
        string QRImagePath = string.Empty;
        int Year = 0, Day = 0;

        private void Set_Batch_Number()
        {
            if (lblCapName.Text != "" &&  txtQty.Text != "")
            {
                BatchNumber = string.Empty;
                QRCodeData = string.Empty;
                DataDisplay = string.Empty;
                YearS = string.Empty;
                Year = 0;
                // ShiftCode();

                if (ShiftFlag)
                    Day = DateTime.Now.DayOfYear - 1;
                else
                    Day = DateTime.Now.DayOfYear;

                Year = Convert.ToInt32(DateTime.Now.Date.Year);
                YearS = Year.ToString();
                curYear = Year.ToString().Substring(2, 2).ToString();
                BatchNumber = Shift +"-" + curYear + Day + WadFitterValue + txtID.Text;
                txtBatchNo.Text = BatchNumber.ToString();

                QRCodeData = lblCapName.Text + ",  " + BatchNumber + ", Qty-" + txtQty.Text;
                //QRCodeData = lblCapName.Text + ", " + lblWadName.Text + ", " + BatchNumber + ", Qty-" + txtQty.Text;
                Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                pbQRCode.Image = qrcode.Draw(QRCodeData.ToString(), 10);
                QRImagePath = objRL.GetPath("CapImagePath");
                var filePath = QRImagePath;
                Directory.CreateDirectory(filePath);
                string FileName = txtID.Text.ToString();
                pbQRCode.Image.Save(Path.Combine(filePath, FileName), System.Drawing.Imaging.ImageFormat.Png);

                if (Wad == "Y")
                    DataDisplay = "Cap Name-" + lblCapName.Text + "\nWad Name- " + lblWadName.Text + "\nBatch Number- " + BatchNumber + "\nQty-" + txtQty.Text;
                else
                    DataDisplay = "Cap Name-" + lblCapName.Text + "\nBatch Number- " + BatchNumber + "\nQty-" + txtQty.Text;

                rtbStickerHeader.Text = DataDisplay.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDB();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            objEP.Clear();
            TableID = 0;
            FlagDelete = false;
            BatchNumber = string.Empty;
            QRCodeData = string.Empty;
            DataDisplay = string.Empty;
            YearS = string.Empty;
            Year = 0;

            txtSearchCap.Text = "";
            lblCapName.Text = "";
            txtSearchWad.Text = "";
            lblWadName.Text = "";
            txtQty.Text = "";
            txtPurchaseOrderNo.Text = "";
            cmbWadFitter.SelectedIndex = -1;
            txtBatchNo.Text = "";
            txtID.Text = "";
            txtShift.Text = "";
            pbQRCode.Image = null;
            rtbStickerHeader.Text = "";
            GetID();
            ShiftCode();
        }


        protected void SaveDB()
        {
            if (cmbPrint.SelectedIndex > -1)
            {
                if (TableID != 0)
                    if (FlagDelete)
                        objBL.Query = "update CapLabelEntry set CancelTag=1 where ID=" + TableID + "";
                    else
                        objBL.Query = "update CapLabelEntry set EntryDate='" + dtpDate.Value.ToShortDateString() + "',EntryTime='" + dtpTime.Value.ToShortTimeString() + "',Shift='" + txtShift.Text + "',CapId=" + CapId + ",CapName='" + lblCapName.Text.Replace("'", "''") + "',WadId=" + WadId + ",WadName='" + lblWadName.Text.Replace("'", "''") + "',Qty='" + txtQty.Text + "',PONumber='" + txtPurchaseOrderNo.Text + "',WadFitter='" + cmbWadFitter.Text + "',BatchNumber='" + txtBatchNo.Text + "', StickerHeader='" + rtbStickerHeader.Text.Replace("'", "''") + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                else
                    objBL.Query = "insert into CapLabelEntry(EntryDate,EntryTime,Shift,CapId,CapName,WadId,WadName,Qty,PONumber,WadFitter,BatchNumber,StickerHeader,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "','" + txtShift.Text + "'," + CapId + ",'" + lblCapName.Text.Replace("'", "''") + "'," + WadId + ",'" + lblWadName.Text.Replace("'", "''") + "','" + txtQty.Text + "','" + txtPurchaseOrderNo.Text + "','" + cmbWadFitter.Text + "','" + txtBatchNo.Text + "', '" + rtbStickerHeader.Text.Replace("'", "''") + "'," + BusinessLayer.UserId_Static + ")";

                objBL.Function_ExecuteNonQuery();

                if (!FlagDelete)
                    GetReportSingle();
            }
            else
            {
                objRL.ShowMessage(17, 4);
                objEP.Clear();
                objEP.SetError(cmbPrint, "Select Sticker Size");

            }
        }

        string CapName = string.Empty;

        private void SetQRCode(Excel.Worksheet myExcelWorksheet, int RowNo, int ColumnNo)
        {
            float ImageSize = 40;
            QRImagePath = objRL.GetPath("CapImagePath") + txtID.Text;
            Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[RowNo, ColumnNo];
            float Left = (float)((double)oRange.Left);
            float Top = (float)((double)oRange.Top);
            //const float ImageSize = 40; 

            if (cmbPrint.Text == "100X50")
                ImageSize = 50;

           // const float ImageSize = 50;

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

                if(cmbPrint.Text == "50X25")
                    objRL.Form_ExcelFileName = "CapQR50X25.xlsx";
                else
                    objRL.Form_ExcelFileName = "QR100X50.xlsx";
                
                objRL.Form_ReportFileName = "ID-" + txtID.Text;
                objRL.Form_DestinationReportFilePath = "\\Cap Stickers\\";
                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                CapName = lblCapName.Text;
                WadName = lblWadName.Text;

                string sourceString = CapName, removeString = " ";
                int index = sourceString.IndexOf(removeString);
                string cleanPath = (index < 0)
                    ? sourceString
                    : sourceString.Remove(index, removeString.Length);

                CapName = cleanPath;

                if (cmbPrint.Text == "50X25")
                {
                    //50X25 Label
                    myExcelWorksheet.get_Range("A1", misValue).Formula = CapName.ToString();
                    myExcelWorksheet.get_Range("A3", misValue).Formula = WadName.ToString();
                    myExcelWorksheet.get_Range("A6", misValue).Formula = "Qty-" + txtQty.Text.ToString();
                    myExcelWorksheet.get_Range("A7", misValue).Formula = txtBatchNo.Text.ToString();
                    SetQRCode(myExcelWorksheet, 3, 2);
                }
                else
                {
                    //100X50 Label
                    //myExcelWorksheet.get_Range("A1", misValue).Formula = CapName.ToString();
                    //myExcelWorksheet.get_Range("A3", misValue).Formula = WadName.ToString();
                    //myExcelWorksheet.get_Range("A5", misValue).Formula = "Qty-" + txtQty.Text.ToString();
                    //myExcelWorksheet.get_Range("A6", misValue).Formula = txtBatchNo.Text.ToString();
                    //SetQRCode(myExcelWorksheet, 5, 4);

                    myExcelWorksheet.get_Range("A1", misValue).Formula = CapName.ToString();
                    myExcelWorksheet.get_Range("A4", misValue).Formula = WadName.ToString();
                    myExcelWorksheet.get_Range("A7", misValue).Formula = "Qty-" + txtQty.Text.ToString();
                    myExcelWorksheet.get_Range("A8", misValue).Formula = txtBatchNo.Text.ToString();
                    SetQRCode(myExcelWorksheet, 7, 5);
                }

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FlagDelete = true;
            SaveDB();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RowCount_Grid = dataGridView1.Rows.Count;
                CurrentRowIndex = dataGridView1.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    ClearAll();
                    btnDelete.Enabled = true;
                    //0 ID,
                    //1 EntryDate as [Date],
                    //2 EntryTime as [Time],
                    //3 Shift,
                    //4 CapId,
                    //5 CapName as [Cap Name],
                    //6 WadId,
                    //7 WadName as [Wad Name],
                    //8 Qty,
                    //9 PONumber,
                    //10 WadFitter  as [Wad Fitter],
                    //11 BatchNumber  as [Batch No],
                    //12 StickerHeader as [Sticker Header]
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    txtID.Text = TableID.ToString();
                    dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    txtShift.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    CapId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                    lblCapName.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    WadId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                    lblWadName.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtQty.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    txtPurchaseOrderNo.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    cmbWadFitter.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    GetWADFitter();
                    txtBatchNo.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    rtbStickerHeader.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                    lblWadName.BackColor = Color.Yellow;
                    lblCapName.BackColor = Color.Cyan;
                }
            }
            catch (Exception ex1)
            {
                objRL.ErrorMessge(ex1.ToString());
                return;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void txtSearchID_TextChanged(object sender, EventArgs e)
        {
            DateFlag = false;
            SearchTag = false;
            if (txtSearchID.Text != "")
                IDFlag = true;
            else
                IDFlag = false;

            FillGrid();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DateFlag = false;
            IDFlag = false;
            if (txtSearch.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateFlag = true;
            IDFlag = false;
            SearchTag = false;
            FillGrid();
        }

        bool FlagToday = false;

        private void cbToday_CheckedChanged(object sender, EventArgs e)
        {
            if (cbToday.Checked == true)
            {
                DateFlag = true;
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

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtQty);
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPurchaseOrderNo.Focus();
        }

        private void txtPurchaseOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbWadFitter.Focus();
        }

        private void cmbWadFitter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }
    }
}
