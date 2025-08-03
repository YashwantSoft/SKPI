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

namespace SPApplication.Reports
{
    public partial class DueDateReport : Form
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

        public DueDateReport()
        {
            InitializeComponent();
            objDL.Set_Report_Design(this, lblHeader, btnView, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_DUEDATEREPORT);
            pbWhatsApp.Image = BusinessResources.WhatsAppImage;
            objRL.SetReportMailId(cbEmail);
            objRL.SetWhatsAppsNoReport(cbWhatsAppsNo);
            objRL.Fill_Users(cmbDepartment);
            Set_Department();
        }

        private void Set_Department()
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_AJAY)
            {
                //cmbDepartmentSearch.Text = BusinessLayer.UserName_Static.ToString();
                cmbDepartment.Enabled = true;
            }
            else
            {
                cmbDepartment.Enabled = false;
                cmbDepartment.Text = BusinessLayer.UserName_Static.ToString();
            }
        }

        private void SaleReport_Load(object sender, EventArgs e)
        {
            ClearAll();
            dtpFromDate.CustomFormat = RedundancyLogics.SystemDateFormat;
            dtpToDate.CustomFormat = RedundancyLogics.SystemDateFormat;
            //objRL.Fill_Facilities(cmbFacilities);
            //objRL.Fill_EmailList(clbEmailId);
        }

        public void ClearAll()
        {
            dataGridView1.DataSource = null;
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            cbPartName.Checked = true;
            cbDepartment.Checked = true;
            cbToday.Checked = true;
            cbToday.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cbToday_CheckedChanged(object sender, EventArgs e)
        {
            if (cbToday.Checked)
            {
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
                dtpFromDate.Value = DateTime.Now.Date;
                dtpToDate.Value = DateTime.Now.Date;
            }
            else
            {
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
                dtpFromDate.Value = DateTime.Now.Date;
                dtpToDate.Value = DateTime.Now.Date;
            }
        }

        private void cbPartName_CheckedChanged(object sender, EventArgs e)
        {
            cmbPartName.SelectedIndex = -1;
            if (cbPartName.Checked)
            {
                cmbPartName.Enabled = false;
                cmbPartName.DataSource = null;
            }
            else
            {
              //  objRL.Fill_Facilities(cmbFacilities);
                cmbPartName.Enabled = true;
                cmbPartName.SelectedIndex = -1;
            }
        }

        private bool Validation()
        {
            bool ReturnValue=false;

            //if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
            //{
            //    dtpFromDate.Focus();
            //    ReturnValue = true;
            //}
            //else
            //    ReturnValue = false;

            if (!ReturnValue)
            {
                if (!cbPartName.Checked)
                {
                    if (cmbPartName.SelectedIndex == -1)
                        ReturnValue = true;
                    else
                        ReturnValue = false;
                }
                else
                    ReturnValue = false;
            }

            if (!ReturnValue)
            {
                if (!cbDepartment.Checked)
                {
                    if (cmbDepartment.SelectedIndex == -1)
                        ReturnValue = true;
                    else
                        ReturnValue = false;
                }
                else
                    ReturnValue = false;
            }

            return ReturnValue;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            GetReport();
        }

        private void cmbFacilities_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //cmbDuration.SelectedIndex = -1;
            //if (cmbFacilities.SelectedIndex > -1)
            //    objRL.Fill_Duration_Report_Due_Date(cmbDuration);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ExcelReport();
        }

        private void ExcelReport()
        {
            objRL.FillCompanyData();
            myExcelApp = new Microsoft.Office.Interop.Excel.Application();
            myExcelWorkbooks = myExcelApp.Workbooks;

            objRL.FillCompanyData();
            objRL.ClearExcelPath();
            objRL.isPDF = true;

            if (!cbPartName.Checked)
            {
                objRL.Form_ExcelFileName = "DueDateReport.xlsx";
                objRL.Form_DestinationReportFilePath = "Part Due Date Report";
                objRL.Form_ReportFileName = "Part Due Date Report-ID-"+cmbPartName.SelectedValue+"-" + DateTime.Now.Date.ToString("dd-MMM-yyyy");
            }
            else
            {
                objRL.Form_ExcelFileName = "DueDateReportAll.xlsx";
                objRL.Form_DestinationReportFilePath = "Part Due Date Report";
                objRL.Form_ReportFileName = "Part Due Date Report All-" + DateTime.Now.Date.ToString("dd-MMM-yyyy");
            }

            objRL.Path_Comman();

            myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
            Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

            if (!cbPartName.Checked)
            {
                myExcelWorksheet.get_Range("A3", misValue).Formula = "From Date " + objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dtpFromDate.Value)) + " To Date " + objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dtpToDate.Value));

                if (!cbPartName.Checked)
                    myExcelWorksheet.get_Range("C4", misValue).Formula = cmbPartName.Text;

                if (!cbDepartment.Checked)
                    myExcelWorksheet.get_Range("C6", misValue).Formula = cmbDepartment.Text;

                myExcelWorksheet.get_Range("G4", misValue).Formula = objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(DateTime.Now.Date));
                //myExcelWorksheet.get_Range("F7", misValue).Formula = BusinessLayer.UserName_Static;

                //0 PM.ID,
                //1 PRE.EntryDate,
                //2 PRE.EntryTime,
                //3 PRE.DepartmentId,
                //4 L.UserName as [Department],
                //5 PRE.PartId,
                //6 PM.PartName as [Part Name],
                //7 PRE.StartDate,
                //8 PRE.IsCompressor,
                //9 PRE.CopressorType,
                //10 PRE.RenewarPeriod +'-'+ PRE.RenewarPeriodFor as [Period]
                //11 PRE.ExpiryDate,
                //12 PRE.StartReadingNo,
                //13 EndReadingNo,
                //14 PRE.RenewalBy,
                //15 PRE.ContactNo
                //16 PRE.Naration

                RowCount =6; SrNo = 1;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    AFlag = 2;
                    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SrNo.ToString());
                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value)));
                    AFlag = 0;
                    Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[4].Value.ToString());
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[6].Value.ToString());
                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[14].Value.ToString());
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[15].Value.ToString());
                    AFlag = 2;
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dataGridView1.Rows[i].Cells[7].Value)));
                    Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dataGridView1.Rows[i].Cells[11].Value)));
                    RowCount++;
                    SrNo++;
                }

                //AFlag = 0;
                //Fill_Merge_Cell("A", "F", misValue, myExcelWorksheet, "Total");
                //AFlag = 1;
                //Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, txtTotalAmount.Text.ToString());
            }
            else
            {
                myExcelWorksheet.get_Range("C3", misValue).Formula = "From Date " + objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dtpFromDate.Value)) + " To Date " + objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dtpToDate.Value));

                myExcelWorksheet.get_Range("G2", misValue).Formula = objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(DateTime.Now.Date));
                myExcelWorksheet.get_Range("G3", misValue).Formula = BusinessLayer.UserName_Static;

                RowCount = 5; SrNo = 1;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    AFlag = 2;
                    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SrNo.ToString());
                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value)));
                    AFlag = 0;
                    Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[4].Value.ToString());
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[6].Value.ToString());
                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[14].Value.ToString());
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[15].Value.ToString());
                    AFlag = 2;
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dataGridView1.Rows[i].Cells[7].Value)));
                    Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dataGridView1.Rows[i].Cells[11].Value)));
                    RowCount++;
                    SrNo++;
                }

                //AFlag = 0;
                //Fill_Merge_Cell("A", "H", misValue, myExcelWorksheet, "Total");
                //AFlag = 1;
                //Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, txtTotalAmount.Text.ToString());
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

                //Email Sending code
                if (!string.IsNullOrEmpty(cbEmail.Text) && cbEmail.Checked)
                {
                    //string FileName = Path.GetFileName(PDFReport);
                    //objRL.EmailBody = "I have send you Membership Due Date Report. on " + dtpFromDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY) + "- To- " + dtpToDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY) +".-" + FileName;
                    //objRL.SendEmail_Details(cbEmail.Text.ToString(), 34, PDFReport);
                }

                //Whats apps file sending code
                //if (cbWhatsAppsNo.Checked)
                //    objRL.WhatsAppsReportOpen(cbWhatsAppsNo.Text.ToString(), objRL.RL_DestinationPath);
                   
            }
            catch (Exception ex1)
            {
                objRL.ShowMessage(27, 4);
                return;
            }
        }

        private void GetReport()
        {
            try
            {
                if (!Validation())
                {
                    dataGridView1.DataSource = null;
                     
                    ConcatQuery = string.Empty;

                    DataSet ds = new DataSet();
                    //ConcatQuery = " where PRE.CancelTag=0 and L.CancelTag=0 and PM.CancelTag=0 and PRE.ExpiryDate between #" + dtpFromDate.Value.AddDays(-10).ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# ";
                    ConcatQuery = " where PRE.CancelTag=0 and L.CancelTag=0 and PM.CancelTag=0 and PRE.ExpiryDate >= #" + dtpFromDate.Value.AddDays(-10).ToString(BusinessResources.DATEFORMATMMDDYYYY) + "#"; //  and #" + dtpToDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# ";

                   // objBL.Query = "select PRE.ID,PRE.EntryDate,PRE.EntryTime,PRE.DepartmentId,L.UserName as [Department],PRE.PartId,PM.PartName as [Part Name],PRE.StartDate as [Start Date],PRE.IsCompressor,PRE.CopressorType as [Compressor Type],PRE.RenewarPeriod as [Renewal Period],PRE.RenewarPeriodFor as [Duration],PRE.ExpiryDate as [Expiry Date],PRE.StartReadingNo as [Start Reading],EndReadingNo as [End Reading],PRE.RenewalBy as [Renewal by],PRE.ContactNo as [Contact No],PRE.Naration from ((PartRenewalEntry PRE inner join PartMaster PM on PM.ID=PRE.PartId) inner join Login L on L.ID=PM.DepartmentId) where L.CancelTag=0 and PM.CancelTag=0 and PRE.CancelTag=0 and PRE.DepartmentId=" + DepartmentId + "";

                    if (!cbPartName.Checked)
                        ConcatQuery += " and PRE.PartId='" + cmbPartName.SelectedValue + "'";

                    if (!cbDepartment.Checked)
                        ConcatQuery += " and PRE.DepartmentId='" + cmbDepartment.SelectedValue + "'";

                    //objBL.Query = "select FC.ID,FC.ID AS SrNo,FC.FeesDate as [Fees Date],FC.MemberId,M.MemberName as [Member Name],M.MobileNumber as [Mobile Number],M.Gender,FC.FacilityId,FC.FacilityName as [Facility Name],FC.Duration,FC.JoiningDate as [Joining Date],FC.DueDate as [Due Date],FC.Fees as [Amount] from ((FeesCollection FC inner join Member M on M.ID=FC.MemberId) inner join Trainer T on T.ID=FC.TrainerId) " + ConcatQuery + " order by FC.DueDate";
                    objBL.Query = "select PRE.ID,PRE.EntryDate,PRE.EntryTime,PRE.DepartmentId,L.UserName as [Department],PRE.PartId,PM.PartName as [Part Name],PRE.StartDate as [Start Date],PRE.IsCompressor,PRE.CopressorType as [Compressor Type],PRE.RenewarPeriod +'-'+ PRE.RenewarPeriodFor as [Period],PRE.ExpiryDate as [Expiry Date],PRE.StartReadingNo as [Start Reading],EndReadingNo as [End Reading],PRE.RenewalBy as [Renewal by],PRE.ContactNo as [Contact No],PRE.Naration from ((PartRenewalEntry PRE inner join PartMaster PM on PM.ID=PRE.PartId) inner join Login L on L.ID=PM.DepartmentId) " + ConcatQuery + " order by PRE.ExpiryDate asc";
                    ds = objBL.ReturnDataSet();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //0 PM.ID,
                        //1 PRE.EntryDate,
                        //2 PRE.EntryTime,
                        //3 PRE.DepartmentId,
                        //4 L.UserName as [Department],
                        //5 PRE.PartId,
                        //6 PM.PartName as [Part Name],
                        //7 PRE.StartDate,
                        //8 PRE.IsCompressor,
                        //9 PRE.CopressorType,
                        //10 PRE.RenewarPeriod +'-'+ PRE.RenewarPeriodFor as [Period]
                        //11 PRE.ExpiryDate,
                        //12 PRE.StartReadingNo,
                        //13 EndReadingNo,
                        //14 PRE.RenewalBy,
                        //15 PRE.ContactNo
                        //16 PRE.Naration

                        dataGridView1.DataSource = ds.Tables[0];
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[2].Visible = false;
                        dataGridView1.Columns[3].Visible = false;
                        dataGridView1.Columns[5].Visible = false;
                        dataGridView1.Columns[8].Visible = false;
                        dataGridView1.Columns[9].Visible = false;
                        dataGridView1.Columns[16].Visible = false;

                        dataGridView1.Columns[1].Width = 80;
                        dataGridView1.Columns[4].Width = 100;
                        dataGridView1.Columns[6].Width = 150;
                        dataGridView1.Columns[7].Width = 130;
                        dataGridView1.Columns[9].Width = 120;
                        dataGridView1.Columns[10].Width = 80;
                        dataGridView1.Columns[11].Width = 130;
                        dataGridView1.Columns[12].Width = 120;
                        dataGridView1.Columns[13].Width = 120;
                        dataGridView1.Columns[14].Width = 120;
                        dataGridView1.Columns[15].Width = 120;
                        dataGridView1.Columns[16].Width = 120;
                        lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                        btnReport.Visible = true;
                    }
                }
                else
                {
                    objRL.ShowMessage(25, 4);
                    return;
                }
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
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
                else if (AFlag == 2)
                    AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
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

            private void cbDepartment_CheckedChanged(object sender, EventArgs e)
            {
                cmbDepartment.SelectedIndex = -1;
                if (cbDepartment.Checked)
                {
                    cmbDepartment.Enabled = false;
                    cmbDepartment.DataSource = null;
                }
                else
                {
                    //  objRL.Fill_Facilities(cmbFacilities);
                    cmbDepartment.Enabled = true;
                    cmbDepartment.SelectedIndex = -1;
                }
            }

            private void cmbDepartment_SelectionChangeCommitted(object sender, EventArgs e)
            {
                Get_DepartmentId();
            }
            int DepartmentId = 0;

            private void Get_DepartmentId()
            {
                if (cmbDepartment.SelectedIndex > -1)
                {
                    DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue);
                    objRL.DepartmentId = DepartmentId;
                }
            }
    }
}
