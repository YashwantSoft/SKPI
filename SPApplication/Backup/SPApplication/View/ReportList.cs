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
using System.Text.RegularExpressions;
using SPApplication;
using SPApplication.Reports;
using SPApplication.Report;

namespace SPApplication
{
    public partial class ReportList : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        ToolTip objTT = new ToolTip();

        public ReportList()
        {
            InitializeComponent();
            objBL.Set_List_Desing(this, lblHeader, btnExit, BusinessResources.LBL_HEADER_REPORTLIST);
            lbReportList.ForeColor = objBL.GetBackgroundColor();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Select_Report()
        {
            if (lbReportList.Items.Count > 0)
            {
                if (lbReportList.Text == "Item Quantity Report")
                {
                    //ItemQuantityReport objForm = new ItemQuantityReport();
                    //objForm.ShowDialog(this);
                    ItemQuantityReport objForm = new ItemQuantityReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Purchase Report")
                {
                    PurchaseReport objForm = new PurchaseReport();
                    objForm.ShowDialog(this);
            }
                else if (lbReportList.Text == "Sale Report")
                {
                    SaleReport objForm = new SaleReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "GST Report")
                {
                    GSTReport objForm = new GSTReport();
                    objForm.ShowDialog(this);
                }
                    
                else if (lbReportList.Text == "Expenses Report")
                {
                    ExpensesReport objForm = new ExpensesReport();
                    objForm.ShowDialog(this);
                }
                    
                else if (lbReportList.Text == "Profit and Loss Report")
                {
                    ProfitLossReport objForm = new ProfitLossReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Supplier Report")
                {
                    SupplierReport objForm = new SupplierReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Bank Satement")
                {
                    BankAccounts objForm = new BankAccounts();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Customer Report")
                {
                    CustomerReport objForm = new CustomerReport();
                    objForm.ShowDialog(this);
                }
                else
                    MessageBox.Show("Enter Valid selection");
            }
        }
        private void lbReportList_Click(object sender, EventArgs e)
        {
            Select_Report();
        }

        private void ReportList_Load(object sender, EventArgs e)
        {
            //objRL.FillColor(lblHeader);
        }

        private void lbReportList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Select_Report();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void lbReportList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
