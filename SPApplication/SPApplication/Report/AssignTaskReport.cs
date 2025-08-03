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
    public partial class AssignTaskReport : Form
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

        public AssignTaskReport()
        {
            InitializeComponent();
            objDL.Set_Report_Design(this, lblHeader, btnView, btnReport, btnClear, btnExit, BusinessResources.LBL_REPORT_USERSTASKREPORT);
            objRL.Fill_Users_CheckBox(clbDepartment);
        }
     
        private void ClearAll()
        {
            cbToday.Checked = true;
            cbStatusAll.Checked = true;
            cbDepartmentAll.Checked = true;
            cbToday.Checked = true;
            FlagTask = false;
            FlagToday = true;
            FlagDepartment = true;
            FlagStatus = true;
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            cmbStatus.SelectedIndex = -1;
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

        private void Fill_Check_Box()
        {
            if (clbDepartment.Items.Count > 0)
            {
                for (int i = 0; i < clbDepartment.Items.Count; i++)
                {
                    if (FlagDepartment)
                        clbDepartment.SetItemChecked(i, true);
                    else
                        clbDepartment.SetItemChecked(i, false);
                }
            }
        }

        private void cbDepartmentAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDepartmentAll.Checked)
            {
                FlagDepartment = true;
                clbDepartment.Enabled = false;
            }
            else
            {
                FlagDepartment = false;
                clbDepartment.Enabled = true;
            }

            Fill_Check_Box();
        }

        private void cbStatusAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbStatusAll.Checked)
            {
                FlagStatus = true;
                cmbStatus.SelectedIndex = -1;
                cmbStatus.Enabled = false;
            }
            else
            {
                FlagStatus = false;
                cmbStatus.SelectedIndex = -1;
                cmbStatus.Enabled = true;
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
        string DepartmentList = string.Empty;

        private void Get_UserId_CheckListBox()
        {
            UserIdList = string.Empty; DepartmentList = string.Empty;
            foreach (object itemChecked in clbDepartment.CheckedItems)
            {
                DataRowView castedItem = itemChecked as DataRowView;
                int? id = Convert.ToInt32(castedItem["ID"]);
                DepartmentList += Convert.ToString(castedItem["UserName"]) + ","; ;
                LoginId = (int)id;
                UserIdList += LoginId +",";
            }
            if (UserIdList != "")
            {
                UserIdList = UserIdList.Substring(0, UserIdList.Length - 1);
                DepartmentList = DepartmentList.Substring(0, DepartmentList.Length - 1);
            }
        }

        string InQuery_Department = string.Empty;
        protected void FillGrid()
        {
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;
            InQuery_Department = string.Empty;
            UserIdList = string.Empty;

            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            MainQuery = "select T.ID,T.EntryDate as [Date],T.EntryTime as [Time],L.UserName as [Department],T.Task,T.FollowUp,T.Status,T.CompleteDate  as [Complete Date],T.CompleteDays as [Complete Days],T.LoginId from AssignTask T inner join Login L on L.ID=T.LoginId where T.CancelTag=0 and L.CancelTag=0";
            OrderByClause = "  order by T.EntryDate asc";

          //  if(!FlagToday)
                WhereClause += " and T.EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
                if (!FlagDepartment)
                {
                    Get_UserId_CheckListBox();

                    if (UserIdList != "")
                    {
                        InQuery_Department = " and T.LoginId IN(" + UserIdList + ")";
                        WhereClause += InQuery_Department;
                    }
                    //" and T.LoginId=" + cmbDepartment.SelectedValue + "";
                    //WhereClause += " and T.LoginId=" + cmbDepartment.SelectedValue + "";
                }
            if(!FlagStatus)
                WhereClause += " and T.Status='" + cmbStatus.Text + "'";
             
            if (string.IsNullOrEmpty(WhereClause))
                WhereClause = string.Empty;
        
            objBL.Query = MainQuery +  WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 ID,
                //1 EntryDate as [Date],
                //2 EntryTime as [Time],
                //3 L.UserName as [Department],,
                //4 AT.Task
                //5 T.FollowUp,
                //6 T.Status,
                //7 T.CompleteDate  as [Complete Date],
                //8 T.CompleteDays as [Complete Days],
                //9 T.LoginId 

                dataGridView1.DataSource = ds.Tables[0];
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                //dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[0].Width = 30;
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Columns[4].Width = 400;
                dataGridView1.Columns[5].Width = 200;
                dataGridView1.Columns[6].Width = 100;
                dataGridView1.Columns[7].Width = 120;
                dataGridView1.Columns[8].Width = 120;
         
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        Status = row.Cells[6].Value.ToString();
                        if (Status.ToString() == "Pending")
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                        else if (Status.ToString() == "Complete")
                            row.DefaultCellStyle.BackColor = Color.Lime;
                        else
                            row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
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
            objRL.Form_ExcelFileName = "AssignTaskReport.xlsx";
            objRL.Form_ReportFileName = "AssignTaskReport-" + DateTime.Now.Date.ToString("dd-MMM-yyyy");
            objRL.Form_DestinationReportFilePath = "AssignTaskReport\\";

            objRL.Path_Comman();

            myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
            Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

            myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName;
            myExcelWorksheet.get_Range("A3", misValue).Formula = "From Date-" + dtpFromDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY) + " to " + dtpToDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);

            myExcelWorksheet.get_Range("F3", misValue).Formula = "Date-" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATDDMMYYYY);
            //myExcelWorksheet.get_Range("H4", misValue).Formula = txtShift.Text.ToString();

            myExcelWorksheet.get_Range("A4", misValue).Formula = "Report Created by: " + BusinessLayer.UserName_Static;

            string DName = string.Empty,SName = string.Empty;

            if (FlagDepartment)
                DName = "All Department";
            else
            {
                Get_UserId_CheckListBox();
                DName = DepartmentList;

            }

            if (FlagStatus)
                SName = "All Status";
            else
                SName = cmbStatus.Text;

            myExcelWorksheet.get_Range("D4", misValue).Formula = " Department Names-" + DName + ",  Status-" + SName;

            SRNO = 1; RowCount = 6;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //0 ID,
                //1 EntryDate as [Date],
                //2 EntryTime as [Time],
                //3 L.UserName as [Department],,
                //4 AT.Task
                //5 T.FollowUp,
                //6 T.Status,
                //7 T.CompleteDate  as [Complete Date],
                //8 T.CompleteDays as [Complete Days],
                //9 T.LoginId 

                //SrNo
                AFlag = 1;
                Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SRNO.ToString());

                //0 ID,
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[1].Value.ToString())))
                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value.ToString())));

                //3 L.UserName as [Department],,
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[3].Value.ToString())))
                    Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[3].Value.ToString());
                else
                    Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, "");

                ////1 EntryDate as [Date],
                //Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value.ToString())));

                ////2 EntryTime as [Time],
                //Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet,  dataGridView1.Rows[i].Cells[2].Value.ToString());

                ////3 L.UserName as [Department],,
            

                //Task
                AFlag = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[4].Value.ToString())))
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[4].Value.ToString());
                else
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, "");

                // AT.Priority
                AFlag = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[5].Value.ToString())))
                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[5].Value.ToString());
                else
                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, "");

                //6 T.Status,
                AFlag = 1;
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[6].Value.ToString())))
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[6].Value.ToString());
                else
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, "");

                //7 T.DueDate as[Due Date],
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[7].Value.ToString())))
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dataGridView1.Rows[i].Cells[7].Value.ToString())));
                else
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, "");

                //8 T.TaskDays as [Task Days],
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[8].Value.ToString())))
                    Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[8].Value.ToString());
                else
                    Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, "");

                ////9 T.CompleteDate  as [Complete Date],
                //if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[9].Value.ToString())))
                //    Fill_Merge_Cell("K", "K", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dataGridView1.Rows[i].Cells[9].Value.ToString())));
                //else
                //    Fill_Merge_Cell("K", "K", misValue, myExcelWorksheet, "");

                ////10 T.CompleteDays as [Complete Days],
                //if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[10].Value.ToString())))
                //    Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[10].Value.ToString());
                //else
                //    Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, "");

                ////11 T.Notes
                //AFlag = 0;
                //if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[11].Value.ToString())))
                //    Fill_Merge_Cell("M", "M", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[11].Value.ToString());
                //else
                //    Fill_Merge_Cell("M", "M", misValue, myExcelWorksheet, "");

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

            if (!string.IsNullOrEmpty(objRL.EmailId) && cbEmail.Checked)
            {
                objRL.EmailId_RL = objRL.EmailId;
                objRL.Subject_RL = "Working Report";
                //string body = "<div><p>Dear " + objRL.CustomerName_Customer + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
                string body = "<div> <p>Please find attachment of pdf file.</p><p>Thanks,</p><p>" + objRL.CI_CompanyName + "</p></div>";

                objRL.Body_RL = body;// "Dear " + RedundancyLogics.SupplierName + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
                objRL.FilePath_RL = PDFReport;
                objRL.SendEMail();
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

        private void AssignTaskReport_Load(object sender, EventArgs e)
        {
            ClearAll();
            objRL.SetReportMailId(cbEmail);
        }
    }
}
