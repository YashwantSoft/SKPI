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
    public partial class LogReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDepartment = false, FlagStatus = false, FlagToday = false, FlagTask = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0, EmployeeId = 0;

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

        string EmployeeName = string.Empty;
        string EmployeeDesignation = string.Empty;

        public LogReport()
        {
            InitializeComponent();
            objDL.Set_Report_Design(this, lblHeader, btnView, btnReport, btnClear, btnExit, BusinessResources.LBL_REPORT_LOGREPORT);
            objRL.Fill_Users(cmbDepartment);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ClearAll()
        {
            cbToday.Checked = true;
            cbDepartmentAll.Checked = true;
            cbToday.Checked = true;
            FlagTask = false;
            FlagToday = true;
            FlagDepartment = true;
            FlagStatus = true;
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            cmbDepartment.SelectedIndex = -1;
            cbToday.Focus();
        }

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

        private void cbDepartmentAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDepartmentAll.Checked)
            {
                FlagDepartment = true;
                cmbDepartment.SelectedIndex = -1;
                cmbDepartment.Enabled = false;
            }
            else
            {
                FlagDepartment = false;
                cmbDepartment.SelectedIndex = -1;
                cmbDepartment.Enabled = true;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string Status = string.Empty;

        protected void FillGrid()
        {
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;

            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            //MainQuery = "select ID,LoginId,LoginDate,LoginTime,LogoutDate,LogoutTime from LogRecord where CancelTag=0 and LoginId=" + BusinessLayer.UserId_Static + " and LoginDate= #" + DateTime.Now.Date.ToShortDateString() + "# ";

            MainQuery = "select T.ID,L.UserName as [Department],T.LoginDate as [LogIn Date],T.LoginTime as [LogIn Time],T.LogoutDate as [Logout Date],T.LogoutTime as [Logout Time] from LogRecord T inner join Login L on L.ID=T.LoginId where T.CancelTag=0 and L.CancelTag=0";
            OrderByClause = "  order by T.LoginDate asc";

            WhereClause += " and T.LoginDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";

            if (!FlagDepartment)
                WhereClause += " and T.LoginId=" + cmbDepartment.SelectedValue + "";

            if (string.IsNullOrEmpty(WhereClause))
                WhereClause = string.Empty;

            objBL.Query = MainQuery + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 ID,
                //1  L.UserName as [Department],
                //2 T.LoginDate as [LogIn Date],
                //3 T.LoginTime as [LogIn Time],
                //4 T.LogoutDate as [Logout Date],
                //5 T.LogoutTime as [Logout Time] 

                dataGridView1.DataSource = ds.Tables[0];
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                //dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 400;
                dataGridView1.Columns[2].Width = 180;
                dataGridView1.Columns[3].Width = 180;
                dataGridView1.Columns[4].Width = 180;
                dataGridView1.Columns[5].Width = 180;
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                ExcelReportMail();
            }
            else
            {
                objRL.ShowMessage(25, 4);
                return;
            }
        }

        int SRNO = 1;
        private void ExcelReportMail()
        {
            objRL.FillCompanyData();

            object misValue = System.Reflection.Missing.Value;
            myExcelApp = new Microsoft.Office.Interop.Excel.Application();
            myExcelWorkbooks = myExcelApp.Workbooks;

            objRL.ClearExcelPath();
            objRL.isPDF = true;
            objRL.Form_ExcelFileName = "LogRecords.xlsx";
            objRL.Form_ReportFileName = "LogRecords-"+DateTime.Now.Date.ToString("dd-MMM-yyyy")+"";
            objRL.Form_DestinationReportFilePath = "Log Records\\";

            objRL.Path_Comman();

            myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
            Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

            myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName;
            myExcelWorksheet.get_Range("A3", misValue).Formula = "From Date-" + dtpFromDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY) + " to " + dtpToDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);

            myExcelWorksheet.get_Range("F3", misValue).Formula = "Date-" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATDDMMYYYY);
            //myExcelWorksheet.get_Range("H4", misValue).Formula = txtShift.Text.ToString();

            myExcelWorksheet.get_Range("A4", misValue).Formula = "Report Created by: " + BusinessLayer.UserName_Static;

            string DName = string.Empty, SName = string.Empty;

            if (FlagDepartment)
                DName = "All Department";
            else
                DName = cmbDepartment.Text;

            myExcelWorksheet.get_Range("D4", misValue).Formula = "Report Selection- Department-" + DName;

            SRNO = 1; RowCount = 6;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //0 ID,
                //1  L.UserName as [Department],
                //2 T.LoginDate as [LogIn Date],
                //3 T.LoginTime as [LogIn Time],
                //4 T.LogoutDate as [Logout Date],
                //5 T.LogoutTime as [Logout Time] 

                //SrNo
                AFlag = 1;
                Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SRNO.ToString());

                //1  L.UserName as [Department],
                AFlag = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[1].Value.ToString())))
                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[1].Value.ToString());

                AFlag = 1;
                Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, "P");

                //2 T.LoginDate as [LogIn Date],
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[2].Value.ToString())))
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dataGridView1.Rows[i].Cells[2].Value.ToString())));
                else
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, "");

                //3 T.LoginTime as [LogIn Time],
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[3].Value.ToString())))
                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, objRL.GetTimeFormat_HHMMSS(Convert.ToDateTime(dataGridView1.Rows[i].Cells[3].Value.ToString())));
                else
                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, "");

                //4 T.LogoutDate as [Logout Date],
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[4].Value.ToString())))
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dataGridView1.Rows[i].Cells[4].Value.ToString())));
                else
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, "");

                //5 T.LogoutTime as [Logout Time] 
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[5].Value.ToString())))
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, objRL.GetTimeFormat_HHMMSS(Convert.ToDateTime(dataGridView1.Rows[i].Cells[5].Value.ToString())));
                else
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, "");
                 
                RowCount++;
                SRNO++;
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
            //    objRL.Subject_RL = "Daily Entry Book Report";
            //    //string body = "<div><p>Dear " + objRL.CustomerName_Customer + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
            //    string body = "<div> <p>Please find attachment of pdf file.</p><p>Thanks,</p><p>" + objRL.CI_CompanyName + "</p></div>";

            //    objRL.Body_RL = body;// "Dear " + RedundancyLogics.SupplierName + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
            //    objRL.FilePath_RL = objRL.RL_DestinationPath;
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

        private void LogReport_Load(object sender, EventArgs e)
        {
            ClearAll();
        }
    }
}
