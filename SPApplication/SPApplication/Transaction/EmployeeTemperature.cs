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
    public partial class EmployeeTemperature : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
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

        public EmployeeTemperature()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_EMPLOYEETEMPERATURE);
            objRL.Fill_Employee_ListBox(lbEmployeeList, txtSearchCap.Text, "All");
            objDL.SetButtonDesign(btnReport, BusinessResources.BTN_REPORT);
            GetID();
            ShiftCode();
        }

        bool ShiftFlag = false;
        string Shift = string.Empty;

        private void ShiftCode()
        {
            TimeSpan StartTimeShift1 = new TimeSpan(07, 0, 0); //10 o'clock
            TimeSpan EndTimeShift1 = new TimeSpan(15, 0, 0); //12 o'clock

            TimeSpan StartTimeShift2 = new TimeSpan(15, 0, 0); //10 o'clock
            TimeSpan EndTimeShift2 = new TimeSpan(23, 0, 0); //12 o'clock

            TimeSpan StartTimeShift3 = new TimeSpan(23, 0, 0); //10 o'clock
            TimeSpan EndTimeShift3 = new TimeSpan(7, 0, 0); //12 o'clock

            TimeSpan StartTimeShift3Other = new TimeSpan(24, 0, 0); //10 o'clock

            DateTime TodayTime;
            TimeSpan now = DateTime.Now.TimeOfDay;
            //TimeSpan now = TodayTime.;

            ShiftFlag = false;
            if ((now > StartTimeShift1) && (now < EndTimeShift1))
                Shift = "I";
            else if ((now > StartTimeShift2) && (now < EndTimeShift2))
                Shift = "II";
            else
            {
                Shift = "III";
                int Checkhours = now.Hours;
                if (Checkhours == 23)
                    ShiftFlag = false;
                else
                    ShiftFlag = true;
            }
            txtShift.Text = Shift.ToString();
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("EmployeeTemperature"));
            txtID.Text = IDNo.ToString();
        }

        private void EmployeeTemperature_Load(object sender, EventArgs e)
        {
            ClearAll();
            cbToday.Checked = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        // 36.7 

        double InTemperature = 0, OutTemperature = 0;
        private void CheckInTemperature(System.Windows.Forms.TextBox txt, System.Windows.Forms.TextBox txtRe)
        {
            InTemperature = 0;
            if (txt.Text == "")
            {
                txtRe.Text = "";
                txtRe.BackColor = Color.White;
                return;
            }
            double.TryParse(txt.Text, out InTemperature);
            if (InTemperature > 37.8)
            {
                btnSave.Visible = false;
                txtRe.Text = "Fever";
                txtRe.BackColor = Color.Red;
            }
            else
            {
                btnSave.Visible = true;
                txtRe.Text = "Normal";
                txtRe.BackColor = Color.Lime;
            }
        }

        private void txtInTemperature_TextChanged(object sender, EventArgs e)
        {
            CheckInTemperature(txtInTemperature, txtInRemarks);
        }

        private void txtSearchCap_TextChanged(object sender, EventArgs e)
        {
            rtbEmployeeInformation.Text = "";
            objRL.Fill_Employee_ListBox(lbEmployeeList, txtSearchCap.Text, "Text");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
            dtpSearchDate.Value = DateTime.Now.Date;
        }

        private void ClearAll()
        {
            objEP.Clear();
            TableID = 0;
            FlagDelete = false;
            EmployeeName = string.Empty;
            EmployeeDesignation = string.Empty;
            EmployeeId = 0;
            txtSearchCap.Text = "";
            txtID.Text = "";
            txtShift.Text = "";
            rtbEmployeeInformation.Text = "";
            txtInTemperature.Text = "";
            dtpInTime.Value = DateTime.Now;
            txtInTemperature.Text = "";
            dtpOutTime.Value = DateTime.Now;
            txtOutTemperature.Text = "";
            txtInRemarks.Text = "";
            GetID();
            ShiftCode();
            gbIntInformation.Visible = false;
            gbOutInformation.Visible = false;
            //dtpInTime.Value =Convert.ToDateTime(DateTime.Now.TimeOfDay);
            //dtpOutTime.Value = Convert.ToDateTime(DateTime.Now.TimeOfDay);
        }

        private void txtOutTemperature_TextChanged(object sender, EventArgs e)
        {
            CheckInTemperature(txtOutTemperature,txtOutRemarks);
        }

        private void lbEmployeeList_Click(object sender, EventArgs e)
        {
            Fill_Employee_Information();
        }

        private void lbEmployeeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Fill_Employee_Information();
            }
        }

        private void Fill_Employee_Information()
        {
            ClearAllEmployee();

            if (TableID == 0)
                EmployeeId = Convert.ToInt32(lbEmployeeList.SelectedValue);

            if (EmployeeId != 0)
            {
                objRL.Get_Employee_Records_By_Id(EmployeeId);
                if (!string.IsNullOrEmpty(Convert.ToString(objRL.FullName)))
                    EmployeeName = objRL.FullName;
                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Designation)))
                    EmployeeDesignation = objRL.Designation;

                String EmployeeInformation = string.Empty;
                EmployeeInformation = "Employee ID-" + EmployeeId.ToString() + "\n" +
                                                            "Name-" + EmployeeName.ToString() + "\n" +
                                                            "Designation-" + EmployeeDesignation.ToString();

                rtbEmployeeInformation.Text = EmployeeInformation.ToString();
                gbIntInformation.Visible = false;
                gbOutInformation.Visible = false;

                if (TableID == 0)
                {
                    txtInTemperature.Focus();
                    gbIntInformation.Visible = true;
                }
                else
                {
                    txtOutTemperature.Focus();
                    gbOutInformation.Visible = true;
                }
            }
        }

        private void ClearAllEmployee()
        {
            gbIntInformation.Visible = true;
            gbOutInformation.Visible = true;
            EmployeeName = string.Empty;
            EmployeeDesignation = string.Empty;
            //EmployeeId = 0;
            rtbEmployeeInformation.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = objRL.Delete_Record_Show_Message();
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                FlagDelete = true;
                SaveDB();
            }
            else
                return;
        }

        private bool Validation()
        {
            objEP.Clear();
            if (EmployeeId == 0)
            {
                objEP.SetError(lbEmployeeList, "Select Employee Name");
                lbEmployeeList.Focus();
                return true;
            }
            else if (txtShift.Text == "")
            {
                objEP.SetError(txtShift, "Enter Shift");
                txtShift.Focus();
                return true;
            }
            else
                return false;
        }
        protected void SaveDB()
        {
            if (!Validation())
            {
                if (TableID != 0)
                    if (FlagDelete)
                        objBL.Query = "update EmployeeTemperature set CancelTag=1 where ID=" + TableID + "";
                    else
                        objBL.Query = "update EmployeeTemperature set EntryDate='" + dtpDate.Value.ToShortDateString() + "',EntryTime='" + dtpTime.Value.ToShortTimeString() + "',Shift='" + txtShift.Text + "',EmployeeId=" + EmployeeId + ",OutTime='" + dtpOutTime.Value.ToShortTimeString() + "',OutTemperature='" + txtOutTemperature.Text + "',OutRemarks='" + txtOutRemarks.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                else
                    objBL.Query = "insert into EmployeeTemperature(EntryDate,EntryTime,Shift,EmployeeId,InTime,InTemperature,InRemarks,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "','" + txtShift.Text + "'," + EmployeeId + ",'" + dtpInTime.Value.ToShortTimeString() + "','" + txtInTemperature.Text + "','" + txtInRemarks.Text + "'," + BusinessLayer.UserId_Static + ")";

                if (objBL.Function_ExecuteNonQuery() > 0)
                {
                    if (FlagDelete)
                        objRL.ShowMessage(9, 1);
                    else
                        objRL.ShowMessage(7, 1);

                    ClearAll();
                    FillGrid();
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
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

            //if (BusinessLayer.UserName_Static != BusinessResources.USER_ADMIN)
            //    UserClause = " and ET.UserId = " + BusinessLayer.UserId_Static;
            //else
            //    UserClause = string.Empty;

            MainQuery = "select ET.ID,EntryDate as [Date],ET.EntryTime as [Time],ET.Shift,ET.EmployeeId,E.FullName as [Employee Name],E.Designation,ET.InTime as [IN Time],ET.InTemperature as [IN Temp],ET.InRemarks as [In Remarks],ET.OutTime as [Out Time],ET.OutTemperature as [Out Temp],ET.OutRemarks as [Out Remarks]  from EmployeeTemperature ET inner join Employee E on E.ID=ET.EmployeeId where ET.CancelTag=0 and E.CancelTag=0";
            OrderByClause = "  order by E.FullName asc";

            if (DateFlag)
                WhereClause = " and ET.EntryDate=#" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            else if (SearchTag)
                WhereClause = " and E.FullName like '%" + txtSearch.Text + "%'";
            else if (IDFlag)
                WhereClause = " and ET.ID=" + txtSearchID.Text + "";
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
                //4 ET.EmployeeId,
                //5 E.FullName as [Employee Name],
                //6 E.Designation,
                //7 ET.InTime as [IN Time],
                //8 ET.InTemperature as [IN Temperature],
                //9 ,ET.InRemarks as [In Remarks]
                //10 ET.OutTime as [Out Time],
                //11 ET.OutTemperature as [Out Temperature],
                //12 ,ET.OutRemarks as [Out Remarks]

                dataGridView1.DataSource = ds.Tables[0];
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 50;
                dataGridView1.Columns[5].Width = 300;
                dataGridView1.Columns[6].Width = 120;
                dataGridView1.Columns[7].Width = 80;
                dataGridView1.Columns[8].Width = 80;
                dataGridView1.Columns[9].Width = 80;
                dataGridView1.Columns[10].Width = 80;
                dataGridView1.Columns[11].Width = 80;
                dataGridView1.Columns[12].Width = 80;
            }
        }

        private void cbToday_CheckedChanged(object sender, EventArgs e)
        {
            DateFlag = true;
            IDFlag = false;
            SearchTag = false;
            if (cbToday.Checked)
            {
                dtpSearchDate.Enabled = false;
                dtpSearchDate.Value = DateTime.Now.Date;
            }
            else
                dtpSearchDate.Enabled = true;

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

        private void dtpSearchDate_ValueChanged(object sender, EventArgs e)
        {
            DateFlag = true;
            IDFlag = false;
            SearchTag = false;
            if (cbToday.Checked)
                dtpSearchDate.Enabled = false;
            else
                dtpSearchDate.Enabled = true;

            FillGrid();
        }

        private void txtInTemperature_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtInTemperature);
        }

        private void txtOutTemperature_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtOutTemperature);
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
            objRL.Form_ExcelFileName = "EmployeeTemperature.xlsx";
            objRL.Form_ReportFileName = "EmployeeTemperature-";
            objRL.Form_DestinationReportFilePath = "Employee Temperature\\";

            objRL.Path_Comman();

            myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
            Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

            myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName;
            myExcelWorksheet.get_Range("A3", misValue).Formula = "From Date-" + dtpSearchDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY) + " to " + dtpSearchDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);

            myExcelWorksheet.get_Range("H3", misValue).Formula = dtpSearchDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);
            myExcelWorksheet.get_Range("H4", misValue).Formula = txtShift.Text.ToString();

            myExcelWorksheet.get_Range("A4", misValue).Formula = "Report Generate by: "+BusinessLayer.UserName_Static;
            

            SRNO = 1; RowCount = 6;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //0 ID,
                //1 EntryDate as [Date],
                //2 EntryTime as [Time],
                //3 Shift,
                //4 ET.EmployeeId,
                //5 E.FullName as [Employee Name],
                //6 E.Designation,
                //7 ET.InTime as [IN Time],
                //8 ET.InTemperature as [IN Temperature],
                //9 ,ET.InRemarks as [In Remarks]
                //10 ET.OutTime as [Out Time],
                //11 ET.OutTemperature as [Out Temperature],
                //12 ,ET.OutRemarks as [Out Remarks]

                AFlag = 1;
                Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SRNO.ToString());
             //   Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value.ToString())));
                AFlag = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[5].Value.ToString())))
                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[5].Value.ToString());
                else
                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, "");
                AFlag = 1;
                //In Time
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[7].Value.ToString())))
                    Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[7].Value.ToString());
                else
                    Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, "");

                //In Temperature
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[8].Value.ToString())))
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[8].Value.ToString());
                else
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, "");

                //In Remarks
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[9].Value.ToString())))
                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[9].Value.ToString());
                else
                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, "");

                //Out Time
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[10].Value.ToString())))
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[10].Value.ToString());
                else
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, "");

                //Out Temperature
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[11].Value.ToString())))
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[11].Value.ToString());
                else
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, "");

                //Out Remarks
                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[12].Value.ToString())))
                    Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[12].Value.ToString());
                else
                    Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, "");

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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //0 ID,
            //1 EntryDate as [Date],
            //2 EntryTime as [Time],
            //3 Shift,
            //4 ET.EmployeeId,
            //5 E.FullName as [Employee Name],
            //6 E.Designation,
            //7 ET.InTime as [IN Time],
            //8 ET.InTemperature as [IN Temperature],
            //9 ,ET.InRemarks as [In Remarks]
            //10 ET.OutTime as [Out Time],
            //11 ET.OutTemperature as [Out Temperature],
            //12 ,ET.OutRemarks as [Out Remarks]

            try
            {
                RowCount_Grid = dataGridView1.Rows.Count;
                CurrentRowIndex = dataGridView1.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    txtID.Text = TableID.ToString();
                    dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    txtShift.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    EmployeeId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                    Fill_Employee_Information();
                    gbOutInformation.Visible = true;
                    gbIntInformation.Visible = false;

                    //lbl.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    //WadId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                    //lblWadName.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    //txtQty.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    //txtPurchasedOrderNo.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    //cmbWadFitter.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    //GetWADFitter();
                    //txtBatchNo.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    //rtbStickerHeader.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                    //lblWadName.BackColor = Color.Yellow;
                    //lblCapName.BackColor = Color.Cyan;
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

        private void txtSearchCap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lbEmployeeList.Items.Count > 0)
                {
                    lbEmployeeList.SelectedIndex=0;
                    lbEmployeeList.Focus();
                }
            }
        }

        private void txtInTemperature_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        private void txtOutTemperature_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }
    }
}
