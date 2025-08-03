using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace SPApplication.Report
{
    public partial class CustomerReport : Form
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
        public CustomerReport()
        {
            InitializeComponent();
            objBL.Set_Report_Design(this, lblHeader, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_CUSTOMERREPORT);
        }
        
        private void CustomerReport_Load(object sender, EventArgs e)
        {
            ClearAll();
            objRL.Fill_Customer(cmbCustomerName);
            cmbCustomerName.SelectedIndex = -1;
        }
        public void ClearAll()
        {
            cmbCustomerName.SelectedIndex = -1;
            cmbCustomerName.Focus();
        }
        protected bool Validation()
        {
            objEP.Clear();
            if (cmbCustomerName.SelectedIndex == -1)
            {
                objEP.SetError(cmbCustomerName, "Select Customer Name");
                cmbCustomerName.Focus();
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
                RedundancyLogics.CustomerName = cmbCustomerName.Text;
                if (RedundancyLogics.CustomerName != "" && RedundancyLogics.CustomerId != 0)
                {
                    CustomerDetails objForm = new CustomerDetails();
                    objForm.ShowDialog(this);
                }
            }
            else
            {
                objRL.ShowMessage(21, 4);
                return;
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

        private void cmbCustomerName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbCustomerName.SelectedIndex > -1)
            {
                RedundancyLogics.CustomerId = Convert.ToInt32(cmbCustomerName.SelectedValue);
                RedundancyLogics.CustomerName = cmbCustomerName.Text;
            }
        }

        private void cmbCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnReport.Focus();
        }

        private void cmbCustomerName_Leave(object sender, EventArgs e)
        {
            if (cmbCustomerName.SelectedIndex > -1)
            {
                RedundancyLogics.CustomerId = Convert.ToInt32(cmbCustomerName.SelectedValue);
                RedundancyLogics.CustomerName = cmbCustomerName.Text;
            }
        }

       
    }
}
