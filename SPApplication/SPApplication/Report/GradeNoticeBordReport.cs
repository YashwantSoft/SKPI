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
    public partial class GradeNoticeBordReport : Form
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

        string MainQuery = string.Empty, WhereClause = string.Empty, OrderByClause = string.Empty, UserClause = string.Empty;

        public GradeNoticeBordReport()
        {
            InitializeComponent();
            objDL.Set_Report_Design(this, lblHeader, btnView, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_GradeNoticeBordReport);
        }

        private void ProgressCardNoticeBoardReport_Load(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            

            //DataGridViewTextBoxColumn notesColumn = new DataGridViewTextBoxColumn();
            //notesColumn.Name = "Notes";
            //notesColumn.HeaderText = "Notes";

            //dgvPacker.Columns.Insert(dgvPacker.Columns.Count, notesColumn);
            dataGridView1.Visible = true;
            Fill_ColumnName();

            Fill_Employee();
        }

        private void Fill_ColumnName()
        {
            dataGridView1.Columns.Clear();

            DataGridViewTextBoxColumn clmEmp = new DataGridViewTextBoxColumn();
            clmEmp.Name = "clmEmployeeId";
            clmEmp.HeaderText = "EmployeeId";
            clmEmp.Visible = false;
            dataGridView1.Columns.Insert(dataGridView1.Columns.Count, clmEmp);

            DataGridViewTextBoxColumn clmEmp1 = new DataGridViewTextBoxColumn();
            clmEmp1.Name = "clmEmployeeName";
            clmEmp1.HeaderText = "Employee Name";
            clmEmp1.Visible = true;
            clmEmp1.Width = 200;
            dataGridView1.Columns.Insert(dataGridView1.Columns.Count, clmEmp1);


            DataSet ds = new DataSet();
            WhereClause = " and GradeDesignation='" + cmbDesignationEmployee.Text + "'";
            MainQuery = "select * from GradeMaster1 where CancelTag=0";
            OrderByClause = " order by GradeId asc";
            objBL.Query = MainQuery + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dgvRowCount = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataGridViewTextBoxColumn notesColumn = new DataGridViewTextBoxColumn();
                    notesColumn.Name = "clmGrade"+i;
                    notesColumn.HeaderText = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Grade"]));
                    notesColumn.Width =55;
                    dataGridView1.Columns.Insert(dataGridView1.Columns.Count, notesColumn);
                }
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ClearAll()
        {
            MainQuery = string.Empty; WhereClause = string.Empty; OrderByClause = string.Empty; UserClause = string.Empty;
            FlagToday = false;
            objEP.Clear();
            cmbDesignationEmployee.SelectedIndex = -1;
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            dgvPacker.Rows.Clear();
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

        int dgvRowCount = 0;

        private void Fill_Employee()
        {
            dataGridView1.Rows.Clear();
            DataSet ds = new DataSet();
            WhereClause = " and Designation='" + cmbDesignationEmployee.Text + "'";
            MainQuery = "select * from Employee where CancelTag=0";
            OrderByClause = " order by FullName asc";
            objBL.Query = MainQuery + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dgvRowCount = 0;
                lblTotalCount.Text = "Total Employees: " + ds.Tables[0].Rows.Count;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    EmployeeId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ID"])));
                    EmployeeName = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["FullName"]));

                    dataGridView1.Rows[dgvRowCount].Cells["clmEmployeeId"].Value = EmployeeId.ToString();
                    dataGridView1.Rows[dgvRowCount].Cells["clmEmployeeName"].Value = EmployeeName.ToString();

                    //int ColIndex=2;
                    //for (int j = 0; j < GradePacker.Length; j++)
                    //{
                    //    //string header = dataGridView1.Columns[0].HeaderText;

                    //    dgvPacker.Rows[dgvRowCount].Cells[ColIndex].Value = Convert.ToString(Get_Value_Grade(EmployeeId, GradePacker[j]));
                    //    ColIndex++;
                    //}

                    int ColIndex = 2;
                    for (int j = 2; j < dataGridView1.Columns.Count; j++)
                    {
                        //string header = dataGridView1.Columns[0].HeaderText;
                        string header = dataGridView1.Columns[j].HeaderText;
                        dataGridView1.Rows[dgvRowCount].Cells[ColIndex].Value = Convert.ToString(Get_Value_Grade(EmployeeId, header));
                        ColIndex++;
                    }

                    dgvRowCount++;
                }
            }
        }

        //private void Fill_Employee()
        //{
        //    dgvPacker.Rows.Clear();
        //    DataSet ds = new DataSet();
        //    WhereClause = " and Designation='" + cmbDesignationEmployee.Text + "'";
        //    MainQuery = "select * from Employee where CancelTag=0";
        //    OrderByClause = " order by FullName asc";
        //    objBL.Query = MainQuery + WhereClause + OrderByClause;
        //    ds = objBL.ReturnDataSet();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        dgvRowCount = 0;
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            dgvPacker.Rows.Add();
        //            EmployeeId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ID"])));
        //            EmployeeName = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["FullName"]));

        //            dgvPacker.Rows[dgvRowCount].Cells["clmEmployeeId"].Value = EmployeeId.ToString();
        //            dgvPacker.Rows[dgvRowCount].Cells["clmEmployeeName"].Value = EmployeeName.ToString();

        //            //int ColIndex=2;
        //            //for (int j = 0; j < GradePacker.Length; j++)
        //            //{
        //            //    //string header = dataGridView1.Columns[0].HeaderText;

        //            //    dgvPacker.Rows[dgvRowCount].Cells[ColIndex].Value = Convert.ToString(Get_Value_Grade(EmployeeId, GradePacker[j]));
        //            //    ColIndex++;
        //            //}

        //            int ColIndex = 2;
        //            for (int j = 2; j < dataGridView1.Columns.Count; j++)
        //            {
        //                //string header = dataGridView1.Columns[0].HeaderText;
        //                string header = dataGridView1.Columns[i].HeaderText;
        //                dgvPacker.Rows[dgvRowCount].Cells[ColIndex].Value = Convert.ToString(Get_Value_Grade(EmployeeId, header));
        //                ColIndex++;
        //            }

        //            dgvRowCount++;
        //        }
        //    }
        //}

        string[] GradePacker = { "Sidel", "Sidel-2.5", "P5*", "P3*", "P1*", "P-2", "P-HE", "5*", "3*", "AAA", "A++", "A+", "A", "B" };
        string[] GradeOperator = { "5*", "3*", "AAA", "A++", "A+", "A", "B", "AP", "AP-1.5", "AP-2", "S-2.5", "H-2.5" };

        private double Get_Value_Grade(int EID, string Grade)
        {
            if (EID == 107)
            {

            }
            double Count = 0;
            DataSet ds = new DataSet();
            MainQuery = string.Empty; WhereClause = string.Empty; OrderByClause = string.Empty; UserClause = string.Empty;
            WhereClause += " and SEN.EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            WhereClause += " and O.Grade='" + Grade + "'";
            MainQuery = "select count(val(OEEEmployeeId)) from OEEOperatorPacker O inner join ShiftEntryNew SEN on SEN.ID=O.ShiftId where SEN.CancelTag=0 and O.CancelTag=0 and O.EmployeeId=" + EID + "";
            objBL.Query = MainQuery + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                Count = objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0][0])));

            return Count;
        }
    }
}
