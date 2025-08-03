using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using Microsoft.Office.Interop.Excel;

namespace SPApplication.Report
{
    public partial class SupplierReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();

        int TableId = 0;
        bool DeleteFlag = false;
        int CustomerID = 0, ItemID = 0;

        static int dgvItemRow;

        bool GridFlag = false;
        int TempRowIndex = 0;

        int RowCount = 18, SRNO = 1;
        bool MH_Value = false;
        Microsoft.Office.Interop.Excel.Application myExcelApp;
        Microsoft.Office.Interop.Excel.Workbooks myExcelWorkbooks;
        Microsoft.Office.Interop.Excel.Workbook myExcelWorkbook;

        public SupplierReport()
        {
            InitializeComponent();
            objBL.Set_Report_Design(this, lblHeader, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_SUPPLIERREPORT);
        }

        private void SupplierReport_Load(object sender, EventArgs e)
        {
            ClearAll();
            objRL.Fill_Supplier(cmbSupplierName);
            cmbSupplierName.SelectedIndex = -1;
        }

        public void ClearAll()
        {
            cmbSupplierName.SelectedIndex = -1;
        }

        protected bool Validation()
        {
            objEP.Clear();
            if (cmbSupplierName.SelectedIndex == -1)
            {
                objEP.SetError(cmbSupplierName, "Select Supplier Name");
                cmbSupplierName.Focus();
                return true;
            }
            else
                return false;
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
               // ExcelReport();
                RedundancyLogics.SupplierName = cmbSupplierName.Text;
                if (RedundancyLogics.SupplierName != "" && RedundancyLogics.SupplierId != 0)
                {
                    SupplierDetails objForm = new SupplierDetails();
                    objForm.ShowDialog(this);
                }
            }
            else
            {
                objRL.ShowMessage(21, 4);
                return;
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


         


        bool AmountFlag = false;
        protected void Fill_Merge_Cell(string Cell1, string Cell2, object val1, Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet, string SetValue)
        {
            Cell1 = Cell1 + RowCount;
            Cell2 = Cell2 + RowCount;
            Microsoft.Office.Interop.Excel.Range AlingRange1 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange1.Merge(val1);
            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            Microsoft.Office.Interop.Excel.Range AlingRange2 = myExcelWorksheet.get_Range(Cell1, Cell2);

            if (AmountFlag == false)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            else
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //DrawBorder(AlingRange2);

            if (MH_Value == true)
            {
                AlingRange2.RowHeight = 40;
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                AlingRange2.WrapText = true;
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

        private void cmbSupplierName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbSupplierName.SelectedIndex > -1)
            {
                RedundancyLogics.SupplierId = Convert.ToInt32(cmbSupplierName.SelectedValue);
                RedundancyLogics.SupplierName = cmbSupplierName.Text;
            }

        }

        private void cmbSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnReport.Focus();
        }

        private void cmbSupplierName_Leave(object sender, EventArgs e)
        {
            if (cmbSupplierName.SelectedIndex > -1)
            {
                RedundancyLogics.SupplierId = Convert.ToInt32(cmbSupplierName.SelectedValue);
                RedundancyLogics.SupplierName = cmbSupplierName.Text;
            }
        }

    }
}
