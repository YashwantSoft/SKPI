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
using System.IO;
 using BusinessLayerUtility;

namespace TestApplication
{
    public partial class DailyBookEntry : Form
    {
        public DailyBookEntry()
        {
            InitializeComponent();
            objBL.Set_Design_All(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, "Daily Book Entry");
        }

        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();

        List<string> Para = new List<string>();
        DataSet ds = new DataSet();
        string mString = "", captionMessage = "";
        string PaymentType = "";

        int TableID = 0;
        int rowIndex = 0, rowCount = 0;

        double OpeningBalance = 0, TotalExpencesAmount = 0, SumDeposite = 0, SumExpenses = 0, TotalExpensesAmount = 0, TotalDepositeAmount = 0;
        double CurrentAmount = 0, NetBalance = 0;
        string CheckString = "";
        string DateFormat = "";

        protected void GetOpeningBalance()
        {
            DateFormat = Convert.ToString(dtpDate.Value.ToString("MM/dd/yyyy"));
            SumDeposite = 0;
            SumExpenses = 0;

            DataSet dsDeposite = new DataSet();
            CheckString = "";
            objBL.Query = "select sum(DepositeAmount) from DailyBookEntry where TDate < #" + DateFormat + "# and DepositeAmount <> 0";
            dsDeposite = objBL.ReturnDataSet();
            if (dsDeposite.Tables[0].Rows.Count > 0)
            {
                CheckString = dsDeposite.Tables[0].Rows[0][0].ToString();
                if (CheckString != null && CheckString != "")
                    SumDeposite = Convert.ToDouble(dsDeposite.Tables[0].Rows[0][0].ToString());
            }

            DataSet dsExpenses = new DataSet();
            objBL.Query = "select sum(ExpensesAmount) from DailyBookEntry where TDate < #" + DateFormat + "# and ExpensesAmount <> 0";
            dsExpenses = objBL.ReturnDataSet();
            if (dsExpenses.Tables[0].Rows.Count > 0)
            {
                CheckString = dsExpenses.Tables[0].Rows[0][0].ToString();
                if (CheckString != null && CheckString != "")
                    SumExpenses = Convert.ToDouble(dsExpenses.Tables[0].Rows[0][0].ToString());
            }

            DateTime dtOpeningBalance;
            DataSet dsDate = new DataSet();
            objBL.Query = "select TDate from DailyBookEntry where TDate < #" + DateFormat + "# and ExpensesAmount <> 0";
            dsDate = objBL.ReturnDataSet();
            if (dsDate.Tables[0].Rows.Count > 0)
            {
                CheckString = dsDate.Tables[0].Rows[0][0].ToString();
                if (CheckString != null && CheckString != "")
                    dtOpeningBalance = Convert.ToDateTime(dsDate.Tables[0].Rows[0][0].ToString());
                else
                    dtOpeningBalance = DateTime.Now.Date;
            }
            else
                dtOpeningBalance = DateTime.Now.Date;

            OpeningBalance = SumDeposite - SumExpenses;

            lblOB.Text = "Opening Balance as on " + dtOpeningBalance.ToShortDateString();
            FillAmount();
        }

        protected void FillAmount()
        {
            if (OpeningBalance <= 0)
                OpeningBalance = 0;

            txtOpeningBalance.Text = OpeningBalance.ToString();
            NetBalance = CurrentAmount + OpeningBalance;

            if (CurrentAmount <= 0)
                CurrentAmount = 0;

            txtCurrentBalanceAmount.Text = CurrentAmount.ToString();

            if (NetBalance <= 0)
                NetBalance = 0;

            txtClosingBalance.Text = NetBalance.ToString();
        }

        protected void FillGrid()
        {
            DateFormat = Convert.ToString(dtpDate.Value.ToString("MM/dd/yyyy"));
            TotalExpensesAmount = 0;
            TotalDepositeAmount = 0;

            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;

            DataSet dsDeposite = new DataSet();
            objBL.Query = "select Id,TDate,Remarks as Narration,DepositeAmount from DailyBookEntry where TDate=#" + DateFormat + "# and DepositeAmount > 0";
            dsDeposite = objBL.ReturnDataSet();

            if (dsDeposite.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = dsDeposite.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Width = 340;
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[3].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

                for (int i = 0; i < dsDeposite.Tables[0].Rows.Count; i++)
                {
                    TotalDepositeAmount = TotalDepositeAmount + Convert.ToDouble(dsDeposite.Tables[0].Rows[i]["DepositeAmount"].ToString());
                }
            }

            DataSet dsExpenses = new DataSet();
            objBL.Query = "select Id,TDate,Remarks as Narration,ExpensesAmount from DailyBookEntry where TDate=#" + DateFormat + "# and ExpensesAmount <> 0";
            dsExpenses = objBL.ReturnDataSet();

            if (dsExpenses.Tables[0].Rows.Count > 0)
            {
                dataGridView2.DataSource = dsExpenses.Tables[0];
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].Visible = false;
                dataGridView2.Columns[2].Width = 310;
                dataGridView2.Columns[3].Width = 190;
                dataGridView2.Columns[3].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

                for (int j = 0; j < dsExpenses.Tables[0].Rows.Count; j++)
                {
                    TotalExpensesAmount = TotalExpensesAmount + Convert.ToDouble(dsExpenses.Tables[0].Rows[j]["ExpensesAmount"].ToString());
                }
            }

            if (TotalDepositeAmount <= 0)
                TotalDepositeAmount = 0;

            if (TotalExpensesAmount <= 0)
                TotalExpensesAmount = 0;

            txtTotalDepositeAmount.Text = TotalDepositeAmount.ToString();
            txtTotalExpencesAmount.Text = TotalExpensesAmount.ToString();
            CurrentAmount = TotalDepositeAmount - TotalExpensesAmount;
            FillAmount();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //DialogResult dr;
            //mString = objRL.ShowMessage.ShowMessage(14);
            //captionMessage = obRL.ShowMessage(6);
            //dr = MessageBox.Show(mString, captionMessage, MessageBoxButtons.YesNo);
            //if (dr == DialogResult.Yes)
                this.Dispose();
        }

        protected bool Validation()
        {
            objEP.Clear();
            if (rbDeposite.Checked == false && rbExpenses.Checked == false)
            {
                objEP.SetError(rbDeposite, "Select Deposite");
                objEP.SetError(rbExpenses, "Select Expencess");
                return true;
            }
            else if (txtAmount.Text == "")
            {
                objEP.SetError(txtAmount, "Insert Amount");
                return true;
            }
            else
                return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                if (TableID != 0)
                    if (PaymentType == "Deposite")
                        objBL.Query = "update DailyBookEntry set DepositeAmount=" + txtAmount.Text + ",Remarks=" + txtNarration.Text + " where Id=" + TableID + "";
                    else
                        objBL.Query = "update DailyBookEntry set ExpensesAmount=" + txtAmount.Text + ",Remarks=" + txtNarration.Text + " where Id=" + TableID + "";
                else
                    if (PaymentType == "Deposite")
                        objBL.Query = "insert into DailyBookEntry(TDate,DepositeAmount,ExpensesAmount,Remarks) values('" + dtpDate.Value.ToShortDateString() + "'," + txtAmount.Text + ",0,'" + txtNarration.Text + "')";
                    else
                        objBL.Query = "insert into DailyBookEntry(TDate,DepositeAmount,ExpensesAmount,Remarks) values('" + dtpDate.Value.ToShortDateString() + "',0," + txtAmount.Text + ",'" + txtNarration.Text + "')";

                objBL.Function_ExecuteNonQuery();
                ClearAll();
                FillGrid();
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void rbDeposite_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDeposite.Checked == true)
            {
                PaymentType = "Deposite";
                rbExpenses.Checked = false;
            }
        }

        private void DailyBookEntryNew_Load(object sender, EventArgs e)
        {
            dtpDate.CustomFormat = "dd/MM/yyyy";
            GetOpeningBalance();
            FillGrid();
        }

        protected void ClearAll()
        {
            TableID = 0;
            objEP.Clear();
            txtAmount.Text = "";
            txtNarration.Text = "";
            rbExpenses.Checked = false;
            rbDeposite.Checked = false;
            btnDelete.Enabled = false;
            dtpDate.Select();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                DialogResult dr;
                mString = "Do you want to delete this report?";
                captionMessage = "Warning";
                dr = MessageBox.Show(mString, captionMessage, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    //Para.Clear();
                    //Para.Add(TableID.ToString());

                    objBL.Query = "delete from DailyBookEntry where Id=" + TableID + "";
                    objBL.Function_ExecuteNonQuery();

                    //ConnectionClass.ExecuteNonQuery(Para, "SP_Delete_DailyBookEntry");
                    objRL.ShowMessage(3, 16);
                    dataGridView2.Rows.RemoveAt(rowIndex);
                    FillGrid();
                    ClearAll();
                }
                else
                {
                    ClearAll();
                    btnDelete.Enabled = false;
                    return;
                }
            }
            else
            {
                objRL.ShowMessage(10, 11);
                return;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowIndex = dataGridView1.CurrentRow.Index;
                rowCount = dataGridView1.Rows.Count;

                if (rowIndex >= 0 && rowCount > 0)
                {
                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    rbDeposite.Checked = true;
                    dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    txtAmount.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                    txtNarration.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                }
            }
            catch
            {
                objRL.ShowMessage(31, 9);
                return;
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void PrintReport()
        {
            ExcelReport();
        }

        protected void ExcelReport()
        {
            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;
            myExcelApp = new Excel.ApplicationClass();
            myExcelWorkbooks = myExcelApp.Workbooks;

            string PPath = "";
            string PathNew = "";

            PPath = @"D:\Maharaja Report\Excel Format\DEReport.xlsx";

            FileInfo fi = new FileInfo(PPath);
            fi.IsReadOnly = false;

            //PathNew = @"D:\Maharaja Report\Reports\";
            PathNew = @"D:\Maharaja Report\Report\Daily Book Report\";

            DirectoryInfo DI = new DirectoryInfo(PathNew);
            DI.Create();

            //PathNew = PathNew + "Dateon" + Convert.ToString(DateTime.Now.Date.ToString("dd-MMM-yyyy")) + "FromDate-" + dtpFromDate.Value.ToString("dd-MMM-yyyy") + "-ToDate-" + dtpToDate.Value.ToString("dd-MMM-yyyy") + ".xlsx";
            PathNew = PathNew + dtpDate.Value.ToString("dd-MMM-yyyy") + ".xlsx";

            FileInfo allExist = new FileInfo(PathNew);

            if (allExist.Exists == true)
                allExist.Delete();

            fi.CopyTo(PathNew);

            myExcelWorkbook = myExcelWorkbooks.Open(PathNew, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

            myExcelWorksheet.get_Range("A1", misValue).Formula = "Maharaja for Men, Wai";
            myExcelWorksheet.get_Range("A2", misValue).Formula = "Daily Book Entry Report on " + Convert.ToString(dtpDate.Value.ToString("dd/MMM/yyyy"));
            myExcelWorksheet.get_Range("A3", misValue).Formula = "Report Generated by -:  Vijay";

            int CellCheckCount = 0;

            Para.Clear();
            Para.Add(dtpDate.Value.ToString("MM/dd/yyyy"));
            Para.Add("Deposite");
            DataSet ds = new DataSet();

            objBL.Query = "select Id,TDate,Remarks as Narration,DepositeAmount from DailyBookEntry where TDate=#" + dtpDate.Value.ToShortDateString() + "# and DepositeAmount <> 0";
            ds = objBL.ReturnDataSet();

            //ds = ConnectionClass.ExecuteStoredProcedureWithParameters(Para, "SP_FillGrid_DailyBookEntry");
            //{
            //}

            Para.Clear();
            Para.Add(dtpDate.Value.ToString("MM/dd/yyyy"));
            Para.Add("Expencess");
            DataSet ds1 = new DataSet();

            objBL.Query = "select Id,TDate,Remarks as Narration,ExpensesAmount from DailyBookEntry where TDate=#" + dtpDate.Value.ToShortDateString() + "# and ExpensesAmount <> 0";
            ds1 = objBL.ReturnDataSet();

            //ds1 = ConnectionClass.ExecuteStoredProcedureWithParameters(Para, "SP_FillGrid_DailyBookEntry");
            //{
            //}

            if (ds.Tables[0].Rows.Count > ds1.Tables[0].Rows.Count)
                CellCheckCount = ds.Tables[0].Rows.Count;
            else
                CellCheckCount = ds1.Tables[0].Rows.Count;

            //for (int i = 0; i < CellCheckCount; i++)
            //{
            //    string addRow = "A" + RowCount;

            //    //Excel.Range firstRow = myExcelWorksheet.get_Range((addRow, misValue);
            //    Excel.Range firstRow = myExcelWorksheet.get_Range(addRow, misValue);
            //    firstRow.EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown, System.Type.Missing);
            //    RowCount++;
            //}

            string CellDisplay1 = "";
            int RowCount = 8;

            for (int i = 0; i < CellCheckCount; i++)
            {
                CellDisplay1 = "A" + RowCount;
                Excel.Range firstRow = myExcelWorksheet.get_Range(CellDisplay1, misValue);
                firstRow.EntireRow.Copy(misValue);

                int PasteCount = 0;
                PasteCount = RowCount + 1;
                string CellDisplayP = "A" + PasteCount;
                Excel.Range firstRow1 = myExcelWorksheet.get_Range(CellDisplayP, misValue);
                firstRow1.EntireRow.PasteSpecial(XlPasteType.xlPasteAll, XlPasteSpecialOperation.xlPasteSpecialOperationNone, misValue, misValue);
                RowCount++;
            }

            int cellCount = 8;
            CellDisplay1 = "";
            int SRNO = 1;
            double DepositeAmount = 0, ExpencessAmount = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //Sr.No
                CellDisplay1 = "A" + cellCount;
                myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = SRNO.ToString();

                //Narration
                CellDisplay1 = "B" + cellCount;
                myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(ds.Tables[0].Rows[i]["Narration"].ToString());

                //Deposite Amount
                CellDisplay1 = "F" + cellCount;
                myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(ds.Tables[0].Rows[i]["DepositeAmount"].ToString());

                DepositeAmount = DepositeAmount + Convert.ToDouble(ds.Tables[0].Rows[i]["DepositeAmount"].ToString());

                cellCount++;
                SRNO++;
            }

            CellDisplay1 = "B" + cellCount;
            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = "Total Deposite Amount";
            Excel.Range AlingRange = myExcelWorksheet.get_Range(CellDisplay1, misValue);
            AlingRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            CellDisplay1 = "F" + cellCount;
            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(DepositeAmount);

            cellCount = 8;
            CellDisplay1 = "";
            SRNO = 1;
            ExpencessAmount = 0;
            RowCount = 8;

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                //Sr.No
                CellDisplay1 = "H" + cellCount;
                myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = SRNO.ToString();

                //Narration
                CellDisplay1 = "I" + cellCount;
                myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(ds1.Tables[0].Rows[i]["Narration"].ToString());

                //Expencess Amount
                CellDisplay1 = "M" + cellCount;
                myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(ds1.Tables[0].Rows[i]["ExpensesAmount"].ToString());

                ExpencessAmount = ExpencessAmount + Convert.ToDouble(ds1.Tables[0].Rows[i]["ExpensesAmount"].ToString());
                cellCount++;
                SRNO++;
            }

            CellDisplay1 = "I" + cellCount;
            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = "Total Expenses Amount";
            Excel.Range AlingRange1 = myExcelWorksheet.get_Range(CellDisplay1, misValue);
            AlingRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            CellDisplay1 = "M" + cellCount;
            myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(ExpencessAmount);

            RowCount = CellCheckCount + 3 + 8;

            string Cell1 = "", Cell2 = "";
            Cell1 = "A" + RowCount;
            Cell2 = "C" + RowCount;
            Excel.Range AlingRange32 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange32.Merge(misValue);
            myExcelWorksheet.get_Range(Cell1, misValue).Formula = "Oening Balance";
            Excel.Range AlingRange36 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange36.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            Excel.Borders borders36 = AlingRange36.Borders;
            borders36[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders36[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders36[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders36[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

            Cell2 = "D" + RowCount;
            myExcelWorksheet.get_Range(Cell2, misValue).Formula = OpeningBalance.ToString();


            RowCount++;
            Cell1 = "A" + RowCount;
            Cell2 = "C" + RowCount;
            Excel.Range AlingRange2 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange2.Merge(misValue);
            myExcelWorksheet.get_Range(Cell1, misValue).Formula = "Total Deposite Amount";
            Excel.Range AlingRange6 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange6.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            Excel.Borders borders = AlingRange6.Borders;
            borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

            Cell2 = "D" + RowCount;
            myExcelWorksheet.get_Range(Cell2, misValue).Formula = DepositeAmount.ToString();

            Excel.Range AlingRange11 = myExcelWorksheet.get_Range(Cell2, misValue);
            AlingRange11.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            Excel.Borders borders1 = AlingRange11.Borders;
            borders1[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders1[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders1[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders1[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;


            string Cell3 = "", Cell4 = "";
            RowCount++;
            Cell3 = "A" + RowCount;
            Cell4 = "C" + RowCount;
            Excel.Range AlingRange3 = myExcelWorksheet.get_Range(Cell3, Cell4);
            AlingRange3.Merge(misValue);
            myExcelWorksheet.get_Range(Cell3, misValue).Formula = "Total Expenses Amount";
            Excel.Range AlingRange7 = myExcelWorksheet.get_Range(Cell3, Cell4);
            AlingRange7.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            Excel.Borders borders2 = AlingRange7.Borders;
            borders2[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders2[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders2[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders2[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

            Cell2 = "D" + RowCount;
            myExcelWorksheet.get_Range(Cell2, misValue).Formula = ExpencessAmount.ToString();
            Excel.Range AlingRange8 = myExcelWorksheet.get_Range(Cell2, misValue);
            AlingRange8.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            Excel.Borders borders3 = AlingRange8.Borders;
            borders3[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders3[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders3[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders3[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;


            string Cell5 = "", Cell6 = "";
            RowCount++;
            Cell5 = "A" + RowCount;
            Cell6 = "C" + RowCount;
            Excel.Range AlingRange4 = myExcelWorksheet.get_Range(Cell5, Cell6);
            AlingRange4.Merge(misValue);
            myExcelWorksheet.get_Range(Cell5, misValue).Formula = "Balance Amount";
            Excel.Range AlingRange9 = myExcelWorksheet.get_Range(Cell5, Cell6);
            AlingRange9.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            Excel.Borders borders4 = AlingRange9.Borders;
            borders4[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders4[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders4[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders4[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;


            Cell5 = "D" + RowCount;
            double NetAmount = 0;
            NetAmount = DepositeAmount - ExpencessAmount;
            myExcelWorksheet.get_Range(Cell5, misValue).Formula = NetAmount.ToString();
            Excel.Range AlingRange10 = myExcelWorksheet.get_Range(Cell5, misValue);
            AlingRange10.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            Excel.Borders borders5 = AlingRange10.Borders;
            borders5[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders5[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders5[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders5[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

            myExcelWorkbook.Save();
            myExcelWorkbook.Close(true, misValue, misValue);
            myExcelApp.Quit();
            objRL.ShowMessage(39, 5);
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtAmount);
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDate.Value.Date <= DateTime.Now.Date)
            {
                lblDay.Text = "Day-: " + dtpDate.Value.DayOfWeek.ToString();
                GetOpeningBalance();
                FillGrid();
            }
            else
            {
                dtpDate.Value = DateTime.Now.Date;
                MessageBox.Show("Please enter proper date");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintReport();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dtpDate.Value = DateTime.Now.Date;
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowIndex = dataGridView2.CurrentRow.Index;
                rowCount = dataGridView2.Rows.Count;

                if (rowIndex >= 0 && rowCount > 0)
                {
                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                    rbExpenses.Checked = true;
                    dtpDate.Value = Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells[1].Value);
                    txtAmount.Text = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[3].Value);
                    txtNarration.Text = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[2].Value);
                }
            }
            catch
            {
                objRL.ShowMessage(31, 9);
                return;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                rbDeposite.Select();
        }

        private void rbDeposite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                rbDeposite.Select();
        }

        private void rbExpences_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                txtAmount.Select();
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                txtNarration.Select();
        }

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                rbDeposite.Select();
        }

        private void rbExpenses_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExpenses.Checked == true)
            {
                PaymentType = "Expencess";
                rbDeposite.Checked = false;
            }
        }
    }
}
