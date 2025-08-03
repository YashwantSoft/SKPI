using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SPApplication.Master;

namespace SPApplication.Report
{
    public partial class ExpensesReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        ErrorProvider objEP = new ErrorProvider();

        bool FlagToday = false;
        double TAmount = 0;


        public ExpensesReport()
        {
            InitializeComponent();
            objBL.Set_Report_Design(this, lblHeader, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_EXPENSESREPORT);
        }

      
        private void btnReport_Click(object sender, EventArgs e)
        {
            Report();
        }

        private void ClearAll()
        {
            objEP.Clear();
            cbToday.Checked = true;
            cbSelectAll.Checked = true;
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            dataGridView1.DataSource = null;
            lblCount.Text = "";
            cmbExpensesHead.SelectedIndex = -1;
            dtpFromDate.Focus();
        }

        private void ExpensesReport_Load(object sender, EventArgs e)
        {
            ClearAll();
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            cbToday.Checked = true;
            FillExpensesHead();
            //Report();
        }

        private void FillExpensesHead()
        {
            objBL.Query = "select ID,ExpensesHeadMain from ExpensesHead where CancelTag=0";
            objBL.FillComboBox(cmbExpensesHead, "ExpensesHeadMain", "ID");
            cmbExpensesHead.SelectedIndex = -1;
        }

        private bool Validation()
        {
            objEP.Clear();
            if (dtpToDate.Value.Date < dtpFromDate.Value.Date)
            {
                objEP.SetError(dtpToDate, "Select Proper Date");
                return true;
            }
            else
                return false;
        }

        private void Report()
        {
            dataGridView1.DataSource = null;
            if (!Validation())
            {
                string ConcatOption = "";
                DataSet ds = new DataSet();
                
                if (!cbSelectAll.Checked)
                    ConcatOption = " where ExpensesHead='" + cmbExpensesHead.Text + "' ";
                else
                    ConcatOption = " where CancelTag=0";

                //objBL.Query = "select B.ID,B.CommanId,B.SaveType,B.EntryDate,B.BillNumber,B.PatientId,P.LastName  +' '+ P.FirstName  +' '+ P.MiddleName as FullName,B.Amount,B.Tax,B.TaxAmount,B.Discount,B.TotalAmount,B.PaymentMode,B.ChequeDate,B.ChequeBankName,B.ChequeNumber,B.NEFTDate,B.NEFTBankName,B.NEFTAccountNumber,B.BillStatus,B.PrintFlag,B.PrintCount,B.UserId from Bill B inner join Patient P on P.ID=B.PatientId " + ConcatOption + " and B.EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
                objBL.Query = "select ID,EntryDate,ExpensesHeadId,ExpensesHead,Naration,Amount,PaymentMode,BankId,BankName,AccountNumber,TransactionDate,ChequeNumber from Expenses " + ConcatOption + " and EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";

                

                //if (FlagToday == true)
                //    objBL.Query = "select ID,HospitalId,PatientHospitalNumber,PatientRegistrationNumber as [Hospital Reg No],Format([EntryDate],'dd/MM/yyyy') as [1st Visit Date],Salutation+'.'+LastName+' '+ FirstName+' '+MiddleName as [Name],Sex,Format([DOB],'dd/MM/yyyy') as [DOB],Age,Occupation,Email,MobileNumber from PatientBasicInformation where Month(DOB)=" + dtpFromDate.Value.Month + " and Day(DOB)=" + dtpFromDate.Value.Day + " and CancelTag=0";
                //else
                //    objBL.Query = "select ID,CommanId,SaveType,EntryDate,BillNumber,PatientId,Amount,Tax,TaxAmount,Discount,TotalAmount,PaymentMode,ChequeDate,ChequeBankName,ChequeNumber,NEFTDate,NEFTBankName,NEFTAccountNumber,BillStatus,PrintFlag,PrintCount,UserId from Bill " + ConcatOption + "";
                //    objBL.Query = "select ID,HospitalId,PatientHospitalNumber,PatientRegistrationNumber as [Hospital Reg No],Format([EntryDate],'dd/MM/yyyy') as [1st Visit Date],Salutation+'.'+LastName+' '+ FirstName+' '+MiddleName as [Name],Sex,Format([DOB],'dd/MM/yyyy') as [DOB],Age,Occupation,Email,MobileNumber from PatientBasicInformation where Month(DOB) between " + dtpFromDate.Value.Month + " and " + dtpToDate.Value.Month + " and Day(DOB) between " + dtpFromDate.Value.Day + " and " + dtpToDate.Value.Day + " and CancelTag=0";

                ds = objBL.ReturnDataSet();

                //DataSet ds = new DataSet();
                //objBL.Query = "select ID,CommanId,SaveType,EntryDate,BillNumber,PatientId,Amount,Tax,TaxAmount,Discount,TotalAmount,PaymentMode,ChequeDate,ChequeBankName,ChequeNumber,NEFTDate,NEFTBankName,NEFTAccountNumber,BillStatus,PrintFlag,PrintCount,UserId from Bill " + ConcatOption + "";
                //ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblCount.Text = "Total Count-" + ds.Tables[0].Rows.Count.ToString();
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    //dataGridView1.Columns[12].Visible = false;

                    dataGridView1.Columns[1].Width = 100;
                    dataGridView1.Columns[3].Width = 200;
                    dataGridView1.Columns[4].Width = 200;
                    dataGridView1.Columns[5].Width = 100;
                    dataGridView1.Columns[6].Width = 100;
                    dataGridView1.Columns[7].Width = 100;
                    dataGridView1.Columns[8].Width = 100;
                    dataGridView1.Columns[9].Width = 100;
                    dataGridView1.Columns[10].Width = 100;
                    dataGridView1.Columns[11].Width = 100;

                    TAmount = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TAmount += Convert.ToDouble(ds.Tables[0].Rows[i]["Amount"].ToString());
                    }
                    txtTotalAmount.Text = TAmount.ToString();
                    ExcelReport();
                }
                else
                {
                    MessageBox.Show("Record not found");
                    return;
                }
            }
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

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSelectAll.Checked)
            {
                cmbExpensesHead.Enabled = false;
                cmbExpensesHead.SelectedIndex = -1;
            }
            else
            {
                cmbExpensesHead.Enabled = true;
                cmbExpensesHead.SelectedIndex = -1;
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


        int RowCount = 0;
        bool MH_Value = false;
        Excel.Application myExcelApp;
        Excel.Workbooks myExcelWorkbooks;
        Excel.Workbook myExcelWorkbook;

        int AFlag = 0;

        private void ExcelReport()
        {
            int SRNO = 1;
            RowCount = 4;
            DialogResult dr;
            dr = MessageBox.Show("Do you want to create this report.?", "Report View", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                objRL.FillCompanyData();

                object misValue = System.Reflection.Missing.Value;
                myExcelApp = new Excel.Application();
                myExcelWorkbooks = myExcelApp.Workbooks;

                objRL.ClearExcelPath();
                objRL.isPDF = true;
                objRL.Form_ExcelFileName = "Expenses.xlsx";
                objRL.Form_ReportFileName = "ExpensesReport";

                string A1 = string.Empty;

                if (cbSelectAll.Checked)
                    A1 = "All";
                else
                    A1 = cmbExpensesHead.Text;

                objRL.Form_DestinationReportFilePath = "\\Expenses Report\\" + A1 + "\\";
                objRL.Path_Comman();


                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName.ToString();
                myExcelWorksheet.get_Range("A3", misValue).Formula = "From Date-" + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat) + "-To Date-" + dtpToDate.Value.ToString(RedundancyLogics.SystemDateFormat);

                Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, "Sr.No.");
                Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, "Date");
                Fill_Merge_Cell("C", "D", misValue, myExcelWorksheet, "Expenses Head");
                Fill_Merge_Cell("E", "H", misValue, myExcelWorksheet, "Description");
                Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, "Payment Mode");
                Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, "Total Amount");
                RowCount++;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //"select ID,EntryDate,ExpensesHeadId,ExpensesHead,Naration,Amount,PaymentMode,ChequeDate,ChequeBankName,ChequeNumber,NEFTDate,NEFTBankName,NEFTAccountNumber,UserId from Expenses " + ConcatOption + " and EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
                    AFlag = 0;
                    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SRNO.ToString());
                    AFlag = 1;
                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, objRL.String_To_Date(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                    Fill_Merge_Cell("C", "D", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[3].Value.ToString());
                    Fill_Merge_Cell("E", "H", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[4].Value.ToString());
                    Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[6].Value.ToString());
                    AFlag = 2;
                    Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[5].Value.ToString());
                    RowCount++;
                    SRNO++;
                }

                RowCount++;
                RowCount++;

                myExcelWorkbook.Save();

                string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
                const int xlQualityStandard = 0;
                myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                myExcelWorkbook.Close(true, misValue, misValue);
                myExcelApp.Quit();

                objRL.ShowMessage(22, 1);

                DialogResult dr1;
                dr1 = MessageBox.Show("Do you want to view this report", "Report View", MessageBoxButtons.YesNo);
                if (dr1 == DialogResult.Yes)
                    System.Diagnostics.Process.Start(PDFReport);
                objRL.DeleteExcelFile();
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
            Cell1 = Cell1 + RowCount;
            Cell2 = Cell2 + RowCount;
            Excel.Range AlingRange1 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange1.Merge(val1);
            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            Excel.Range AlingRange2 = myExcelWorksheet.get_Range(Cell1, Cell2);
            if(AFlag==0)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            else if (AFlag == 1)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            else
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            DrawBorder(AlingRange2);

            if (MH_Value == true)
                AlingRange2.RowHeight = 60;
        }

        private void dtpFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpToDate.Focus();
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cbToday.Focus();
        }

        private void cbToday_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbExpensesHead.Focus();
        }

        private void cmbExpensesHead_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cbSelectAll.Focus();
        }

        private void cbSelectAll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnReport.Focus();
        }
    }
}
