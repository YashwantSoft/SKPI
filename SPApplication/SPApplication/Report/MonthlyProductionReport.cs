using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using Microsoft.Office.Interop.Excel;

namespace SPApplication.Report
{
    public partial class MonthlyProductionReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        ErrorProvider objEP = new ErrorProvider();
        DesignLayer objDL = new DesignLayer();

        int RowCount = 18, AFlag = 0, RentCount = 0, SrNo = 1;
        double TotalAmount = 0;

        bool MH_Value = false;
        Microsoft.Office.Interop.Excel.Application myExcelApp;
        Microsoft.Office.Interop.Excel.Workbooks myExcelWorkbooks;
        Microsoft.Office.Interop.Excel.Workbook myExcelWorkbook;
        object misValue = System.Reflection.Missing.Value;

        string PDFReport = string.Empty;
        string ConcatQuery = string.Empty;
        string FacilityName = string.Empty;
        string Duration = string.Empty;

        public MonthlyProductionReport()
        {
            InitializeComponent();
            objDL.Set_Report_Design(this, lblHeader, btnView, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_MONTHLYPRODUCTIONREPORT);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (dgvReport.Rows.Count > 0)
            {
                ExcelReportMail();
            }
            else
            {
                objRL.ShowMessage(25, 4);
                return;
            }
        }

        bool BorderFlag = false;
        private void ExcelReportMail()
        {
            objRL.FillCompanyData();

            object misValue = System.Reflection.Missing.Value;
            myExcelApp = new Microsoft.Office.Interop.Excel.Application();
            myExcelWorkbooks = myExcelApp.Workbooks;

            objRL.ClearExcelPath();
            objRL.isPDF = true;
            objRL.Form_ExcelFileName = "MonthlyProductionReport.xlsx";
            objRL.Form_ReportFileName = "MonthlyProductionReport-" + DateTime.Now.Date.ToString("dd-MMM-yyyy");
            objRL.Form_DestinationReportFilePath = "Monthly Production Report\\";

            objRL.Path_Comman();

            myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
            Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

            myExcelWorksheet.get_Range("A5", misValue).Formula = "Report Date- " + dtpFromDate.Value.ToShortDateString() +" to "+ dtpToDate.Value.ToShortDateString();

            if(cbSelectAll.Checked)
                myExcelWorksheet.get_Range("A6", misValue).Formula = "Report Name- All Machine Report";
            else
                myExcelWorksheet.get_Range("A6", misValue).Formula = "Report Name-  " + cmbMachineNo.Enabled;
          
            myExcelWorksheet.get_Range("F6", misValue).Formula = DateTime.Now.Date.ToString(BusinessResources.DATEFORMATDDMMYYYY);
           
            SrNo = 1; RowCount = 8; BorderFlag = false;
            for (int i = 0; i < dgvReport.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dgvReport.Rows[i].Cells["clmMachineNo"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, Convert.ToString(dgvReport.Rows[i].Cells["clmMachineNo"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvReport.Rows[i].Cells["clmPerformance"].Value)))
                {
                    AFlag = 2;
                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, Convert.ToString(dgvReport.Rows[i].Cells["clmPerformance"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvReport.Rows[i].Cells["clmPerformance"].Value)))
                {
                    AFlag = 2;
                    Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, Convert.ToString(dgvReport.Rows[i].Cells["clmAvailibility"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvReport.Rows[i].Cells["clmAvailibility"].Value)))
                {
                    AFlag = 2;
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, Convert.ToString(dgvReport.Rows[i].Cells["clmQuality"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvReport.Rows[i].Cells["clmQuality"].Value)))
                {
                    AFlag = 2;
                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, Convert.ToString(dgvReport.Rows[i].Cells["clmOEE"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvReport.Rows[i].Cells["clmOEE"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, Convert.ToString(dgvReport.Rows[i].Cells["clmRatings"].Value));
                }
                RowCount++;
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

        private void btnView_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string Status = string.Empty;
        int LoginId = 0;
        string UserIdList = string.Empty;
        string DepartmentList = string.Empty, MachineNo=string.Empty,Ratings=string.Empty;

        double PerformanceAverage = 0, AvailibilityAverage = 0, QualityAverage = 0, OEEAverage = 0;

        private void Get_Average_PerMachine()
        {
            string SetColumn=string.Empty;
            WhereClause = "";
            //if(ColumnName =="Performance")
            //{

            //}

            DataSet ds = new DataSet();
            PerformanceAverage = 0; AvailibilityAverage = 0; QualityAverage = 0; OEEAverage = 0;
            WhereClause += " and EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            //objBL.Query = "select ID,EntryDate,EntryTime,ProductionEntryId,OEEEntryId,MachineNo,ProductId,SupervisorId,OperatorId,PackerId,PackerId1,PackerId2,PreformLoadingId,MouldChangerId,MouldChangerId1,ShiftLengthA,ShortMealBreakB,NoPlanning,NoElectricity,TotalProductionTime,Breakdown,Changeover,ManpowerShortage,StartupLoss,MaintainanceMachineShutDownTime,MaterialNotAvailable,TotalDowntime,OperatingTime,Availabilty,IdealRunRate,Cavity,PlanningQty,TotalShot,TotalProductionInNos,Packing,TotalPacket,TargetProduction,Performance,RejectInNos,GoodInNos,Quality,OEE,SwitchFlag,SwitchPackerMachineId,SwitchOperatiorMachineId,Reason,Status,SwitchNote from OEEEntryMachine where CancelTag=0 and OEEEntryId=" + OEEEntryId + " and MachineNo=" + MachineNo + " order by ID desc";

            objBL.Query = "select AVG(Val(IIf(Performance,Performance,0))) as [PerformanceAverage],AVG(Val(IIf(Availabilty,Availabilty,0))) as [AvailabiltyAverage],AVG(Val(IIf(Quality,Quality,0))) as [QualityAverage],AVG(Val(IIf(OEE,OEE,0))) as [OEEAverage] from OEEEntryMachine where CancelTag=0 " + WhereClause + " and MachineNo=" + MachineNo + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0][0].ToString())))
                    PerformanceAverage = Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString());
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0][1].ToString())))
                    AvailibilityAverage = Convert.ToDouble(ds.Tables[0].Rows[0][1].ToString());
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0][2].ToString())))
                    QualityAverage = Convert.ToDouble(ds.Tables[0].Rows[0][2].ToString());
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0][3].ToString())))
                    OEEAverage = Convert.ToDouble(ds.Tables[0].Rows[0][3].ToString());
            }
        }

        static int DataGridIndex;

        private void Add_Values_In_Grid()
        {
            if (dgvReport.Rows.Count > 0)
                DataGridIndex = dgvReport.Rows.Count;
            else
                DataGridIndex = 0;

            dgvReport.Rows.Add();
            dgvReport.Rows[DataGridIndex].Cells[0].Value = MachineNo.ToString();
            dgvReport.Rows[DataGridIndex].Cells[1].Value = PerformanceAverage.ToString();
            dgvReport.Rows[DataGridIndex].Cells[2].Value = AvailibilityAverage.ToString();
            dgvReport.Rows[DataGridIndex].Cells[3].Value = QualityAverage.ToString();
            dgvReport.Rows[DataGridIndex].Cells[4].Value = OEEAverage.ToString();

            //=(IF(E7>=90%,"Excellent","Satisfactory"))
            if (OEEAverage > 90)
                Ratings = "Excellent";
            else if (OEEAverage < 90 && OEEAverage >1)
                Ratings = "Satisfactory";
            else
                Ratings = "NA";

            dgvReport.Rows[DataGridIndex].Cells[5].Value = Ratings.ToString();
            DataGridIndex++;
        }

        protected void FillGrid()
        {
            btnReport.Visible = false;
            dgvReport.Rows.Clear();
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;
            UserIdList = string.Empty;
            dgvReport.DataSource = null;

            if (cbSelectAll.Checked)
            {
                for (int i = 1; i < 15; i++)
                {
                    MachineNo = string.Empty; Ratings = string.Empty;
                    PerformanceAverage = 0; AvailibilityAverage = 0; QualityAverage = 0; OEEAverage = 0;

                    MachineNo = i.ToString();
                    Get_Average_PerMachine();
                    Add_Values_In_Grid();
                }
            }
            else
            {
                MachineNo = cmbMachineNo.Text;
                Get_Average_PerMachine();
                Add_Values_In_Grid();
            }

            if (dgvReport.Rows.Count > 0)
                btnReport.Visible = true;
            else
                btnReport.Visible = false;
             
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            objEP.Clear();
            cbSelectAll.Checked = true;
            cbToday.Checked = true;
            cmbMonth.SelectedIndex = -1;
            cmbYear.SelectedIndex = -1;
            cmbMachineNo.SelectedIndex = -1;
            cmbMonth.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        bool MachineNoAll = false;
        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSelectAll.Checked)
            {
                MachineNoAll = true;
                cmbMachineNo.SelectedIndex = -1;
                cmbMachineNo.Enabled = false;
            }
            else
            {
                MachineNoAll = false;
                cmbMachineNo.SelectedIndex = -1;
                cmbMachineNo.Enabled = true;
            }
        }

        private void MonthlyProductionReport_Load(object sender, EventArgs e)
        {
            ClearAll();
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
