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

namespace SPApplication.Report
{
    public partial class QualityControlMachineWiseReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        ErrorProvider objEP = new ErrorProvider();
        DesignLayer objDL = new DesignLayer();

        bool FlagToday = false;
        string QueryParameter = string.Empty;

        bool BorderFlag = false; bool CellFlag = false;
        string PDFReport = string.Empty;
        int AFlag = 0; int SrNo = 1;
        bool AlignFlag = false; bool boldflag = false;
        int RowCount = 18;
        bool MH_Value = false;
        string ReportName = string.Empty;
        bool ColourFlag = false;
        string ConcatNames = string.Empty;

        Excel.Application myExcelApp;
        Excel.Workbooks myExcelWorkbooks;
        Excel.Workbook myExcelWorkbook;

        DateTime BookingDate;

        public QualityControlMachineWiseReport()
        {
            InitializeComponent();
            objDL.Set_Report_Design(this, lblHeader, btnView, btnReport,btnClear, btnExit, BusinessResources.LBL_REPORT_QUALITYCONTROLMACHINEWISEREPORT);
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
        private void btnReport_Click(object sender, EventArgs e)
        {
            GetReport();
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            ViewReport();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cbEmail_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void AmountCollectionReport_Load(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            QueryParameter = string.Empty;
            dataGridView1.DataSource = null;
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            cbToday.Checked = true;
            cbToday.Focus();
        }

        private bool Validation()
        {
            if (cmbMachineNo.SelectedIndex == -1)
            {
                cmbMachineNo.Focus();
                objEP.SetError(cmbMachineNo, "Select Machine No");
                return true;
            }
            else
                return false;
        }

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;
        bool DateFlag = false;
        bool SearchTag = false;
        bool IDFlag = false;

        private void SetRedCellColour(DataGridView dgv, int i, int CheckCell, int SetCell)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[CheckCell].Value)))
            {
                if (dataGridView1.Rows[i].Cells[CheckCell].Value.ToString() == "1")
                    dataGridView1.Rows[i].Cells[SetCell].Style.BackColor = Color.Red;
            }
        }

        private void ViewReport()
        {
            if (!Validation())
            {
                dataGridView1.DataSource = null;
                MainQuery = string.Empty;
                WhereClause = string.Empty;
                OrderByClause = string.Empty;
                UserClause = string.Empty;

                dataGridView1.DataSource = null;
                DataSet ds = new DataSet();

                if (BusinessLayer.UserName_Static != BusinessResources.USER_ADMIN)
                    UserClause = " and QCE.UserId = " + BusinessLayer.UserId_Static;
                else
                    UserClause = string.Empty;

                //MainQuery = "select ID,EntryDate as [Date],EntryTime as [Time],Shift,MachinNo as [Machine No],PlantInchargeId,VolumeCheckerId,ProductId,ProductName  as [Product Name],NeckSizeI,NeckSizeRemarkI,WeightI,WeightRemarkI,NeckIDI,NeckIDRemarkI,NeckODI,NeckODRemarkI,NeckCollarGapI,NeckCollarGapRemarkI,NeckHeightI,NeckHeightRemarkI,ProductHeightI,ProductHeightRemarkI,ProductVolumeI,ProductVolumeRemarkI,CapSealingI,CapSealingRemarkI,WadSealingI,WadSealingRemarkI,NeckSizeII,NeckSizeRemarkII,WeightII,WeightRemarkII,NeckIDII,NeckIDRemarkII,NeckODII,NeckODRemarkII,NeckCollarGapII,NeckCollarGapRemarkII,NeckHeightII,NeckHeightRemarkII,ProductHeightII,ProductHeightRemarkII,ProductVolumeII,ProductVolumeRemarkII,CapSealingII,CapSealingRemarkII,WadSealingII,WadSealingRemarkII,CheckerNote,Remark,ErrorCount,ShiftEntryId,PlantInchargeId as[Plant Incharge],VolumeCheckerId as [Volume Checker],Cavity from QualityRegisterEntry where CancelTag=0 ";

                //MainQuery = "select QCE.ID,QCE.EntryDate as [Date],QCE.EntryTime as [Time],QCE.ShiftEntryId,SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],QCE.MachinNo as [Machine No],QCE.ProductId,P.ProductType as [Product Type],P.ProductName as [Product Name],P.MouldId,MM.Cavity,QCE.PreformPartyId,PPM.PreformParty as [Preform Party],QCE.NeckSize1 as [Neck Size 1],QCE.NeckSizeRemark1,QCE.NeckID1 as [Neck ID 1],QCE.NeckIDRemark1,QCE.NeckOD1 as [Neck OD 1],QCE.NeckODRemark1,QCE.NeckCollarGap1 as [Neck Collar Gap 1],QCE.NeckCollarGapRemark1,QCE.NeckHeight1 as [Neck Height 1],QCE.NeckHeightRemark1,QCE.ProductHeight1 as [Product Height 1],QCE.ProductHeightRemark1,QCE.Weight1 as [Weight 1],QCE.WeightRemark1,QCE.ProductVolume1 as [Product Volume 1],QCE.ProductVolumeRemark1,QCE.CapSealing1 as [Cap Sealing 1],QCE.CapSealingRemark1,QCE.WadSealing1 as [Wad Sealing 1],QCE.WadSealingRemark1,QCE.NeckSize2 as [Neck Size 2],QCE.NeckSizeRemark2,QCE.NeckID2 as [Neck ID 2],QCE.NeckIDRemark2,QCE.NeckOD2 as [Neck OD 2],QCE.NeckODRemark2,QCE.NeckCollarGap2 as [Neck Collar Gap 2],QCE.NeckCollarGapRemark2,QCE.NeckHeight2 as [Neck Height 2],QCE.NeckHeightRemark2,QCE.ProductHeight2 as [Product Height 2],QCE.ProductHeightRemark2,QCE.Weight2 as [Weight 2],QCE.WeightRemark2,QCE.ProductVolume2 as [Product Volume 2],QCE.ProductVolumeRemark2,QCE.CapSealing2 as [Cap Sealing 2],QCE.CapSealingRemark2,QCE.WadSealing2 as [Wad Sealing 2],QCE.WadSealingRemark2,QCE.CheckerNote as [Checker Note],QCE.Remark,QCE.ErrorCount,ProductionEntryId from ((((((QCEntry QCE inner join Product P on P.ID=QCE.ProductId) inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join PreformPartyMaster PPM on PPM.ID=QCE.PreformPartyId) inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) inner join MouldMaster MM on MM.ID=P.MouldId) where QCE.CancelTag=0 and P.CancelTag=0 and SE.CancelTag=0  and PPM.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and MM.CancelTag=0";
                MainQuery = "select QCE.ID,QCE.EntryDate as [Date],QCE.EntryTime as [Time],QCE.ShiftEntryId,SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],QCE.MachinNo as [Machine No],QCE.ProductId,P.ProductType as [Product Type],P.ProductName as [Product Name],P.MouldId,MM.Cavity,QCE.PreformPartyId,PPM.PreformParty as [Preform Party],QCE.NeckSize1 as [Neck Size 1],QCE.NeckSizeRemark1,QCE.NeckID1 as [Neck ID 1],QCE.NeckIDRemark1,QCE.NeckOD1 as [Neck OD 1],QCE.NeckODRemark1,QCE.NeckCollarGap1 as [Neck Collar Gap 1],QCE.NeckCollarGapRemark1,QCE.NeckHeight1 as [Neck Height 1],QCE.NeckHeightRemark1,QCE.ProductHeight1 as [Product Height 1],QCE.ProductHeightRemark1,QCE.Weight1 as [Weight 1],QCE.WeightRemark1,QCE.ProductVolume1 as [Product Volume 1],QCE.ProductVolumeRemark1,QCE.CapSealing1 as [Cap Sealing 1],QCE.CapSealingRemark1,QCE.WadSealing1 as [Wad Sealing 1],QCE.WadSealingRemark1,QCE.NeckSize2 as [Neck Size 2],QCE.NeckSizeRemark2,QCE.NeckID2 as [Neck ID 2],QCE.NeckIDRemark2,QCE.NeckOD2 as [Neck OD 2],QCE.NeckODRemark2,QCE.NeckCollarGap2 as [Neck Collar Gap 2],QCE.NeckCollarGapRemark2,QCE.NeckHeight2 as [Neck Height 2],QCE.NeckHeightRemark2,QCE.ProductHeight2 as [Product Height 2],QCE.ProductHeightRemark2,QCE.Weight2 as [Weight 2],QCE.WeightRemark2,QCE.ProductVolume2 as [Product Volume 2],QCE.ProductVolumeRemark2,QCE.CapSealing2 as [Cap Sealing 2],QCE.CapSealingRemark2,QCE.WadSealing2 as [Wad Sealing 2],QCE.WadSealingRemark2,QCE.CheckerNote as [Checker Note],QCE.Remark,QCE.ErrorCount,ProductionEntryId,E.FirstName as [Plant],E1.FirstName as [Volume] from ((((((QCEntry QCE inner join Product P on P.ID=QCE.ProductId) inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join PreformPartyMaster PPM on PPM.ID=QCE.PreformPartyId) inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) inner join MouldMaster MM on MM.ID=P.MouldId) where QCE.CancelTag=0 and P.CancelTag=0 and SE.CancelTag=0  and PPM.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and MM.CancelTag=0";

                //MainQuery = "select QRE.ID,QRE.EntryDate as [Date],QRE.EntryTime as [Time],QRE.Shift,QRE.MachinNo as [Machine No],QRE.PlantInchargeId,QRE.VolumeCheckerId,QRE.ProductId,QRE.ProductName  as [Product Name],QRE.NeckSizeI,QRE.NeckSizeRemarkI,QRE.WeightI,QRE.WeightRemarkI,QRE.NeckIDI,QRE.NeckIDRemarkI,QRE.NeckODI,QRE.NeckODRemarkI,QRE.NeckCollarGapI,QRE.NeckCollarGapRemarkI,QRE.NeckHeightI,QRE.NeckHeightRemarkI,QRE.ProductHeightI,QRE.ProductHeightRemarkI,QRE.ProductVolumeI,QRE.ProductVolumeRemarkI,QRE.CapSealingI,QRE.CapSealingRemarkI,QRE.WadSealingI,QRE.WadSealingRemarkI,QRE.NeckSizeII,QRE.NeckSizeRemarkII,QRE.WeightII,QRE.WeightRemarkII,QRE.NeckIDII,QRE.NeckIDRemarkII,QRE.NeckODII,QRE.NeckODRemarkII,QRE.NeckCollarGapII,QRE.NeckCollarGapRemarkII,QRE.NeckHeightII,QRE.NeckHeightRemarkII,QRE.ProductHeightII,QRE.ProductHeightRemarkII,QRE.ProductVolumeII,QRE.ProductVolumeRemarkII,QRE.CapSealingII,QRE.CapSealingRemarkII,QRE.WadSealingII,QRE.WadSealingRemarkII,QRE.CheckerNote,QRE.Remark,QRE.ErrorCount from QualityRegisterEntry QRE where CancelTag=0 ";
                OrderByClause = " order by QCE.EntryDate desc";

                if (DateFlag)
                    WhereClause = " and QCE.EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
                //else
                //    WhereClause = string.Empty;

                if (cmbMachineNo.SelectedIndex > -1)
                    WhereClause += "and QCE.MachinNo='" + cmbMachineNo.Text + "'";

                objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //0 QCE.ID,
                    //1 QCE.EntryDate as [Date],
                    //2 QCE.EntryTime as [Time],
                    //3 QCE.ShiftEntryId,
                    //4 SE.Shift,
                    //5 SE.PlantInchargeId,
                    //6 E.FullName as [Plant Incharge],
                    //7 SE.VolumeCheckerId,
                    //8 E1.FullName as [Volume Checker],
                    //9 QCE.MachinNo as [Machine No],
                    //10 QCE.ProductId,
                    //11 P.ProductType as [Product Type],
                    //12 P.ProductName as [Product Name],
                    //13 P.MouldId,
                    //14 MM.Cavity,
                    //15 QCE.PreformPartyId,
                    //16 PPM.PreformParty as [Preform Party],
                    //17 QCE.NeckSize1 as [Neck Size 1],
                    //18 QCE.NeckSizeRemark1,
                    //19 QCE.NeckID1 as [Neck ID 1],
                    //20 QCE.NeckIDRemark1,
                    //21 QCE.NeckOD1  as [Neck OD 1],
                    //22 QCE.NeckODRemark1,
                    //23 QCE.NeckCollarGap1 as [Neck Collar Gap 1],
                    //24 QCE.NeckCollarGapRemark1,
                    //25 QCE.NeckHeightI as [Neck Height I],
                    //26 QCE.NeckHeightRemark1,
                    //27 QCE.ProductHeightI as [Product Height I],
                    //28 QCE.ProductHeightRemark1,
                    //29 QCE.Weight1 as [Weight 1],
                    //30 QCE.WeightRemark1,
                    //31 QCE.ProductVolume1 as [Product Volume 1],
                    //32 QCE.ProductVolumeRemark1,
                    //33 QCE.CapSealing1 as [Cap Sealing 1],
                    //34 QCE.CapSealingRemark1,
                    //35 QCE.WadSealing1 as [Wad Sealing 1],
                    //36 QCE.WadSealingRemark1,
                    //37 QCE.NeckSize2 as [Neck Size 2],
                    //38 QCE.NeckSizeRemarkII,
                    //39 QCE.NeckID2 as [Neck ID 2],
                    //40 QCE.NeckIDRemark2,
                    //41 QCE.NeckOD2 as [Neck OD 2],
                    //42 QCE.NeckODRemark2,
                    //43 QCE.NeckCollarGap2 as [Neck Collar Gap 2],
                    //44 QCE.NeckCollarGapRemark2,
                    //45 QCE.NeckHeight2 as [Neck Height 2],
                    //46 QCE.NeckHeightRemark2,
                    //47 QCE.ProductHeight2 as [Product Height 2],
                    //48 QCE.ProductHeightRemark2,
                    //49 QCE.Weight2 as [Weight 2],
                    //50 QCE.WeightRemark2,
                    //51 QCE.ProductVolume2 as [Product Volume 2],
                    //52 QCE.ProductVolumeRemark2,
                    //53 QCE.CapSealing2 as [Cap Sealing 2],
                    //54 QCE.CapSealingRemark2,
                    //55 QCE.WadSealing2 as [Wad Sealing 2],
                    //56 QCE.WadSealingRemark2,
                    //57 QCE.CheckerNote as [Checker Note],
                    //58 QCE.Remark as [],
                    //59 QCE.ErrorCount
                    //60 QCE.ProductionEntryId

                    //17 QCE.NeckSize1 as [Neck Size 1],
                    //18 QCE.NeckSizeRemark1,
                    //19 QCE.NeckID1 as [Neck ID 1],
                    //20 QCE.NeckIDRemark1,
                    //21 QCE.NeckOD1  as [Neck OD 1],
                    //22 QCE.NeckODRemark1,
                    //23 QCE.NeckCollarGap1 as [Neck Collar Gap 1],
                    //24 QCE.NeckCollarGapRemark1,
                    //25 QCE.NeckHeightI as [Neck Height I],
                    //26 QCE.NeckHeightRemark1,
                    //27 QCE.ProductHeightI as [Product Height I],
                    //28 QCE.ProductHeightRemark1,
                    //29 QCE.Weight1 as [Weight 1],
                    //30 QCE.WeightRemark1,
                    //31 QCE.ProductVolume1 as [Product Volume 1],
                    //32 QCE.ProductVolumeRemark1,
                    //33 QCE.CapSealing1 as [Cap Sealing 1],
                    //34 QCE.CapSealingRemark1,
                    //35 QCE.WadSealing1 as [Wad Sealing 1],
                    //36 QCE.WadSealingRemark1,
                    //37 QCE.NeckSize2 as [Neck Size 2],
                    //38 QCE.NeckSizeRemarkII,
                    //39 QCE.NeckID2 as [Neck ID 2],
                    //40 QCE.NeckIDRemark2,
                    //41 QCE.NeckOD2 as [Neck OD 2],
                    //42 QCE.NeckODRemark2,
                    //43 QCE.NeckCollarGap2 as [Neck Collar Gap 2],
                    //44 QCE.NeckCollarGapRemark2,
                    //45 QCE.NeckHeight2 as [Neck Height 2],
                    //46 QCE.NeckHeightRemark2,
                    //47 QCE.ProductHeight2 as [Product Height 2],
                    //48 QCE.ProductHeightRemark2,
                    //49 QCE.Weight2 as [Weight 2],
                    //50 QCE.WeightRemark2,
                    //51 QCE.ProductVolume2 as [Product Volume 2],
                    //52 QCE.ProductVolumeRemark2,
                    //53 QCE.CapSealing2 as [Cap Sealing 2],
                    //54 QCE.CapSealingRemark2,
                    //55 QCE.WadSealing2 as [Wad Sealing 2],
                    //56 QCE.WadSealingRemark2,

                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[7].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    dataGridView1.Columns[11].Visible = false;
                    dataGridView1.Columns[13].Visible = false;
                    dataGridView1.Columns[14].Visible = false;
                    dataGridView1.Columns[15].Visible = false;
                    dataGridView1.Columns[58].Visible = false;
                    dataGridView1.Columns[59].Visible = false;
                    dataGridView1.Columns[60].Visible = false;
                     
                    lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;

                    //Visible False
                    for (int k = 18; k <= 56; k = k + 2)
                    {
                        dataGridView1.Columns[k].Visible = false;
                    }
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].Width = 130;
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 18; j <= 56; j = j + 2)
                        {
                            SetRedCellColour(dataGridView1, i, j, j - 1);
                        }
                    }
                    dataGridView1.Columns[1].Width = 80;
                    dataGridView1.Columns[2].Width = 70;
                    dataGridView1.Columns[4].Width = 50;
                    dataGridView1.Columns[9].Width = 100;
                    dataGridView1.Columns[12].Width = 200;
                    dataGridView1.Columns[16].Width = 110;
                    btnReport.Visible = true;
                }
                else
                {
                    objRL.ShowMessage(25, 4);
                    return;
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        protected void DrawBorder(Excel.Range Functionrange)
        {
            Excel.Borders borders = Functionrange.Borders;
            //borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders.Weight = 1D;
        }
        protected void Fill_Merge_Cell(string Cell1, string Cell2, object val1, Excel.Worksheet myExcelWorksheet, string SetValue)
        {
            //CellFlag = false;
            if (!CellFlag)
            {
                Cell1 = Cell1 + RowCount;
                Cell2 = Cell2 + RowCount;
            }

            Microsoft.Office.Interop.Excel.Range AlingRange1 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange1.Merge(val1);

            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            if (boldflag)
                AlingRange1.EntireRow.Font.Bold = true;


            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            Microsoft.Office.Interop.Excel.Range AlingRange2 = myExcelWorksheet.get_Range(Cell1, Cell2);

            if (AFlag == 0)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            if (AFlag == 1)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            if (AFlag == 2)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            if (BorderFlag)
                DrawBorder(AlingRange2);

            if (MH_Value)
            {
                AlingRange2.RowHeight = 60;
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                AlingRange2.WrapText = true;
            }

            if (AlignFlag)
                AlingRange1.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;

            if (ColourFlag)
                myExcelWorksheet.get_Range(Cell1, Cell2).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
        }

    

        public void GetReport()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                using (new CursorWait())
                {
                    BorderFlag = false;

                    object misValue = System.Reflection.Missing.Value;
                    myExcelApp = new Excel.Application();
                    myExcelWorkbooks = myExcelApp.Workbooks;

                    objRL.ClearExcelPath();
                    objRL.isPDF = true;

                    //ReportName = "MachineNo-" + cmbMachineNo.Text;

                    objRL.Form_ExcelFileName = "QCReport.xlsx";
                    objRL.Form_ReportFileName = "QCReport-" + ReportName;
                    objRL.Form_DestinationReportFilePath = "\\Quality Control Report\\" + ReportName + "\\";
                    objRL.Path_Comman();

                    myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                    Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                    objRL.FillCompanyData();

                    //1 Sr.No	
                    //2 Date	
                    //3 Shift	
                    //4 Plant Incharge and Volume Checker	
                    //5 Product Name	
                    //6 Neck Size
                    //7 Neck ID	
                    //8 Neck OD	
                    //9 Neck Collar Gap	
                    //10 Neck Height	
                    //11 Product Height	
                    //12 Weight	
                    //13 Product Volume	
                    //14 Cap Sealing	
                    //15 Wad Sealing

                    //0 QCE.ID,
                    //1 QCE.EntryDate as [Date],
                    //2 QCE.EntryTime as [Time],
                    //3 QCE.ShiftEntryId,
                    //4 SE.Shift,
                    //5 SE.PlantInchargeId,
                    //6 E.FullName as [Plant Incharge],
                    //7 SE.VolumeCheckerId,
                    //8 E1.FullName as [Volume Checker],
                    //9 QCE.MachinNo as [Machine No],
                    //10 QCE.ProductId,
                    //11 P.ProductType as [Product Type],
                    //12 P.ProductName as [Product Name],
                    //13 P.MouldId,
                    //14 MM.Cavity,
                    //15 QCE.PreformPartyId,
                    //16 PPM.PreformParty as [Preform Party],
                    //17 QCE.NeckSize1 as [Neck Size 1],
                    //18 QCE.NeckSizeRemark1,
                    //19 QCE.NeckID1 as [Neck ID 1],
                    //20 QCE.NeckIDRemark1,
                    //21 QCE.NeckOD1  as [Neck OD 1],
                    //22 QCE.NeckODRemark1,
                    //23 QCE.NeckCollarGap1 as [Neck Collar Gap 1],
                    //24 QCE.NeckCollarGapRemark1,
                    //25 QCE.NeckHeightI as [Neck Height I],
                    //26 QCE.NeckHeightRemark1,
                    //27 QCE.ProductHeightI as [Product Height I],
                    //28 QCE.ProductHeightRemark1,
                    //29 QCE.Weight1 as [Weight 1],
                    //30 QCE.WeightRemark1,
                    //31 QCE.ProductVolume1 as [Product Volume 1],
                    //32 QCE.ProductVolumeRemark1,
                    //33 QCE.CapSealing1 as [Cap Sealing 1],
                    //34 QCE.CapSealingRemark1,
                    //35 QCE.WadSealing1 as [Wad Sealing 1],
                    //36 QCE.WadSealingRemark1,
                    //37 QCE.NeckSize2 as [Neck Size 2],
                    //38 QCE.NeckSizeRemarkII,
                    //39 QCE.NeckID2 as [Neck ID 2],
                    //40 QCE.NeckIDRemark2,
                    //41 QCE.NeckOD2 as [Neck OD 2],
                    //42 QCE.NeckODRemark2,
                    //43 QCE.NeckCollarGap2 as [Neck Collar Gap 2],
                    //44 QCE.NeckCollarGapRemark2,
                    //45 QCE.NeckHeight2 as [Neck Height 2],
                    //46 QCE.NeckHeightRemark2,
                    //47 QCE.ProductHeight2 as [Product Height 2],
                    //48 QCE.ProductHeightRemark2,
                    //49 QCE.Weight2 as [Weight 2],
                    //50 QCE.WeightRemark2,
                    //51 QCE.ProductVolume2 as [Product Volume 2],
                    //52 QCE.ProductVolumeRemark2,
                    //53 QCE.CapSealing2 as [Cap Sealing 2],
                    //54 QCE.CapSealingRemark2,
                    //55 QCE.WadSealing2 as [Wad Sealing 2],
                    //56 QCE.WadSealingRemark2,
                    //57 QCE.CheckerNote as [Checker Note],
                    //58 QCE.Remark as [],
                    //59 QCE.ErrorCount
                    //60 QCE.ProductionEntryId

                    //17 QCE.NeckSize1 as [Neck Size 1],
                    //18 QCE.NeckSizeRemark1,
                    //19 QCE.NeckID1 as [Neck ID 1],
                    //20 QCE.NeckIDRemark1,
                    //21 QCE.NeckOD1  as [Neck OD 1],
                    //22 QCE.NeckODRemark1,
                    //23 QCE.NeckCollarGap1 as [Neck Collar Gap 1],
                    //24 QCE.NeckCollarGapRemark1,
                    //25 QCE.NeckHeightI as [Neck Height I],
                    //26 QCE.NeckHeightRemark1,
                    //27 QCE.ProductHeightI as [Product Height I],
                    //28 QCE.ProductHeightRemark1,
                    //29 QCE.Weight1 as [Weight 1],
                    //30 QCE.WeightRemark1,
                    //31 QCE.ProductVolume1 as [Product Volume 1],
                    //32 QCE.ProductVolumeRemark1,
                    //33 QCE.CapSealing1 as [Cap Sealing 1],
                    //34 QCE.CapSealingRemark1,
                    //35 QCE.WadSealing1 as [Wad Sealing 1],
                    //36 QCE.WadSealingRemark1,

                    //37 QCE.NeckSize2 as [Neck Size 2],
                    //38 QCE.NeckSizeRemark2,
                    //39 QCE.NeckID2 as [Neck ID 2],
                    //40 QCE.NeckIDRemark2,
                    //41 QCE.NeckOD2 as [Neck OD 2],
                    //42 QCE.NeckODRemark2,
                    //43 QCE.NeckCollarGap2 as [Neck Collar Gap 2],
                    //44 QCE.NeckCollarGapRemark2,
                    //45 QCE.NeckHeight2 as [Neck Height 2],
                    //46 QCE.NeckHeightRemark2,
                    //47 QCE.ProductHeight2 as [Product Height 2],
                    //48 QCE.ProductHeightRemark2,
                    //49 QCE.Weight2 as [Weight 2],
                    //50 QCE.WeightRemark2,
                    //51 QCE.ProductVolume2 as [Product Volume 2],
                    //52 QCE.ProductVolumeRemark2,
                    //53 QCE.CapSealing2 as [Cap Sealing 2],
                    //54 QCE.CapSealingRemark2,
                    //55 QCE.WadSealing2 as [Wad Sealing 2],
                    //56 QCE.WadSealingRemark2,

                    myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName.ToString();
                    myExcelWorksheet.get_Range("A2", misValue).Formula = objRL.Form_ReportFileName.ToString();
                    myExcelWorksheet.get_Range("A3", misValue).Formula = "Report Date- " + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat) + "-" + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat);
                    // // + " To Date: " + dtpToDate.Value.ToString(RedundancyLogics.SystemDateFormat);
                    myExcelWorksheet.get_Range("A4", misValue).Formula = "Report Created By:" + BusinessLayer.UserName_Static.ToString();

                    myExcelWorksheet.get_Range("O3", misValue).Formula = "Date-" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATDDMMYYYY);
                    myExcelWorksheet.get_Range("O4", misValue).Formula = "Machine No-" + cmbMachineNo.Text.ToString();

                    RowCount = 6; SrNo = 1;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        ColourFlag = false;
                        BorderFlag = true;
                        //Sr.No
                        AFlag = 1;
                        Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SrNo.ToString());

                        //Date
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[1].Value)))
                        {
                            AFlag = 0;
                            BookingDate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value);
                            Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, BookingDate.ToString("dd/MM/yyyy"));
                        }

                        //Shift
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[4].Value)))
                        {
                            AFlag = 0;
                            Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[4].Value.ToString());
                        }

                        //Machine No
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[9].Value)))
                        {
                            AFlag = 1;
                            Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[9].Value.ToString());
                        }

                        ConcatNames = string.Empty;
                        //Plant Incharge and Volume Checker
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[61].Value)) && !string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[62].Value)))
                        {
                            ConcatNames = Convert.ToString(dataGridView1.Rows[i].Cells[61].Value) + "/" + Convert.ToString(dataGridView1.Rows[i].Cells[62].Value);
                            AFlag = 0;
                            Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, ConcatNames.ToString());
                        }

                        //Preform Party
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[16].Value)))
                        {
                            AFlag = 0;
                            Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[16].Value.ToString());
                        }

                        //Product Name
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[12].Value)))
                        {
                            AFlag = 0;
                            Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[12].Value.ToString());
                        }

                        //Neck Size
                        objectValue = misValue; myExcelWorksheet1 = myExcelWorksheet;
                        ArrayCount = 0;
                        // private void ExcelValuesColour(int i, int CellIndex, int DisplayIndex)

                        int CheckIndex = 18;
                        for (int j = 17; j <= 36; j = j + 2)
                        {
                            //ExcelValuesColour(i, 10, 9);
                            ExcelValuesColour(i, CheckIndex, j);
                            CheckIndex = CheckIndex + 2;
                        }

                        //If Cavity Required 2 then print again
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[14].Value)))
                        {
                            if (Convert.ToString(dataGridView1.Rows[i].Cells[14].Value.ToString()) == "2")
                            {
                                SrNo++;
                                RowCount++;

                                ColourFlag = false;
                                BorderFlag = true;
                                //Sr.No
                                AFlag = 1;
                                Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SrNo.ToString());

                                //Date
                                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[1].Value)))
                                {
                                    AFlag = 0;
                                    BookingDate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value);
                                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, BookingDate.ToString("dd/MM/yyyy"));
                                }

                                //Shift
                                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[4].Value)))
                                {
                                    AFlag = 0;
                                    Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[4].Value.ToString());
                                }

                                //Machine No
                                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[9].Value)))
                                {
                                    AFlag = 1;
                                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[9].Value.ToString());
                                }

                                ConcatNames = string.Empty;
                                //Plant Incharge and Volume Checker
                                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[61].Value)) && !string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[62].Value)))
                                {
                                    ConcatNames = Convert.ToString(dataGridView1.Rows[i].Cells[61].Value) + "/" + Convert.ToString(dataGridView1.Rows[i].Cells[62].Value);
                                    AFlag = 0;
                                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, ConcatNames.ToString());
                                }

                                //Preform Party
                                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[16].Value)))
                                {
                                    AFlag = 0;
                                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[16].Value.ToString());
                                }

                                //Product Name
                                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[12].Value)))
                                {
                                    AFlag = 0;
                                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[12].Value.ToString());
                                }
                                //Neck Size
                                objectValue = misValue; myExcelWorksheet1 = myExcelWorksheet;
                                ArrayCount = 0;
                                // private void ExcelValuesColour(int i, int CellIndex, int DisplayIndex)

                                CheckIndex = 38;
                                for (int j = 37; j <= 56; j = j + 2)
                                {
                                    //ExcelValuesColour(i, 10, 9);
                                    ExcelValuesColour(i, CheckIndex, j);
                                    CheckIndex = CheckIndex + 2;
                                }
                            }
                        }
                        SrNo++;
                        RowCount++;
                    }

                    myExcelWorkbook.Save();
                    PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");

                    try
                    {
                        const int xlQualityStandard = 0;
                        myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                        myExcelWorkbook.Close(true, misValue, misValue);
                        myExcelApp.Quit();

                        //objRL.ShowMessage(22, 1);

                        //DialogResult dr;
                        //dr = MessageBox.Show("Do you want to view this report", "Report View", MessageBoxButtons.YesNo);
                        //if (dr == DialogResult.Yes)
                        //    System.Diagnostics.Process.Start(objRL.RL_DestinationPath);

                        System.Diagnostics.Process.Start(PDFReport);
                        //objRL.DeleteExcelFile();

                        //if (!string.IsNullOrEmpty(objRL.EmailId) && cbEmail.Checked)
                        //{
                        //    objRL.EmailId_RL = objRL.EmailId;
                        //    objRL.Subject_RL = "Amount Collection Report";
                        //    //string body = "<div><p>Dear " + objRL.CustomerName_Customer + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
                        //    string body = "<div><p>Dear Sir,<p/><p>Please find attachment of pdf file.</p><p>Amount Collection Report on " + dtpFromDate.Value.ToString("dd/MM/yyyy") + " to " + dtpToDate.Value.ToString("dd/MM/yyyy") + " </p><p>Thanks,</p></div>";

                        //    objRL.Body_RL = body;// "Dear " + RedundancyLogics.SupplierName + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
                        //    objRL.FilePath_RL = PDFReport;
                        //    objRL.SendEMail();
                        //}

                        //if (cbEmail.Checked)
                        //{
                        //    objRL.EmailId_RL = "";
                        //    objRL.Subject_RL = "";
                        //    objRL.Body_RL = "";
                        //    objRL.FilePath_RL = PDFReport;
                        //    //objRL.SendEMail();
                        //}
                    }
                    catch (Exception ex1)
                    {
                        objRL.ShowMessage(27, 4);
                        return;
                    }
                }
            }
            else
            {

            }
        }
        string[] CellArray = { "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q" };
        static int ArrayCount;

        object objectValue; Excel.Worksheet myExcelWorksheet1;
        private void ExcelValuesColour(int i, int CheckIndex, int DisplayIndex)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[CheckIndex].Value)) && !string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[DisplayIndex].Value)))
            {
                AFlag = 1;
                if (Convert.ToString(dataGridView1.Rows[i].Cells[CheckIndex].Value.ToString()) == "1")
                    ColourFlag = true;
                else
                    ColourFlag = false;

                Fill_Merge_Cell(CellArray[ArrayCount], CellArray[ArrayCount], objectValue, myExcelWorksheet1, dataGridView1.Rows[i].Cells[DisplayIndex].Value.ToString());
                ArrayCount++;
                ColourFlag = false;
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

        private void cmbMachineNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_Machine_Information();
        }

        private void Fill_Machine_Information()
        {
            if (cmbMachineNo.SelectedIndex > -1)
            {
                int MNo = Convert.ToInt32(cmbMachineNo.Text);

                if (MNo == 1 || MNo == 2 || MNo == 3 || MNo == 4 || MNo == 5 || MNo == 6 || MNo == 7 || MNo == 8 || MNo == 9)
                    lblMachineDetails.Text = "Manual";
                else
                    lblMachineDetails.Text = "Automatic";
            }
        }
    }
}
