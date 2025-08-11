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
    public partial class CapCOAReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        double RequiredValue = 0, DifferanceRatio = 0, MinValue = 0, MaxValue = 0;

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


        public CapCOAReport()
        {
            InitializeComponent();
            Set_Design();
        }

        private void Set_Design()
        {
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_CAPCOAREPORT);
            objDL.SetButtonDesign(btnReport, BusinessResources.BTN_REPORT);
            
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("COAReport"));
            txtID.Text = IDNo.ToString();
        }

        private void ClearValues()
        {

        }

        private void ClearAll()
        {
            TableID = 0;
            objEP.Clear();
            ClearValues();
            ClearAllCap();
            objRL.Fill_Cap_ListBox(lbCap, txtSearchCap.Text, "All");
            gbCOAParameters.Enabled = false;
            dgvValues.Enabled = false;
            GetID();
            txtSearchCap.Focus();
        }

        private void CapCOAReport_Load(object sender, EventArgs e)
        {
            ClearAll();
            //FillGrid();
            txtSearchCap.Focus();
        }

        private void txtSearchCap_TextChanged(object sender, EventArgs e)
        {
            ClearAllCap();
            if (txtSearchCap.Text != "")
            {
                objRL.Fill_Cap_ListBox(lbCap, txtSearchCap.Text, "Text");
                //lbItem.Focus();
            }
            else
            {
                lbCap.Visible = true;
                objRL.Fill_Cap_ListBox(lbCap, txtSearchCap.Text, "All");
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

            if (BusinessLayer.UserName_Static != BusinessResources.USER_ADMIN)
                UserClause = " and CQC.UserId = " + BusinessLayer.UserId_Static;
            else
                UserClause = string.Empty;

            MainQuery = "select CQC.ID,EntryDate as [Date],CQC.EntryTime as [Time],CQC.CapId,C.CapName as [Cap Name],CQC.InvoiceNumber as [Invoice Number],CQC.SupplierId,S.SupplierName as [Supplier Name],CQC.QCCheckerId,E.FullName as [QC Checker Name] from (((CapQualityControl CQC inner join CapMaster C on C.ID=CQC.CapId) inner join Supplier S on S.ID=CQC.SupplierId) inner join Employee E on E.ID=CQC.QCCheckerId) where CQC.CancelTag=0 and C.CancelTag=0 and S.CancelTag=0 and E.CancelTag=0 and CQC.CapId="+CapId+"";
            OrderByClause = " order by CQC.ID desc";

            //if (DateFlag)
            //    WhereClause = " and CQC.EntryDate between #" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            //else if (SearchTag)
            //    WhereClause = " and C.CapName like '%" + txtSearch.Text + "%'";
            //else if (IDFlag)
            //    WhereClause = " and CQC.ID=" + txtSearchID.Text + "";
            //else
            //    WhereClause = string.Empty;

            objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 ID,
                //1 EntryDate as [Date],
                //2 EntryTime as [Time],
                //3 CQC.CapId,
                //4 C.CapName as [Cap Name],
                //5 CQC.InvoiceNumber as [Invoice Number],
                //6 CQC.SupplierId,
                //7 S.SupplierName as [Supplier Name],
                //8 CQC.QCCheckerId,
                //9 E.FullName as [QC Checker Name]

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[8].Visible = false;

                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[4].Width = 350;
                dataGridView1.Columns[5].Width = 120;
                dataGridView1.Columns[7].Width = 350;
                dataGridView1.Columns[9].Width = 200;

                txtInvoiceNumber.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Invoice Number"]));
                txtSupplierName.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Supplier Name"]));
                txtQCCheckerName.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["QC Checker Name"]));
                //lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        int CapId = 0; bool GridFlag = false;
        private void ClearAllCap()
        {
            if (!GridFlag)
                CapId = 0;

            lblCapName.Text = "";
        }

        private void lbCap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Fill_Cap_Information();
        }

        private void lbCap_Click(object sender, EventArgs e)
        {
            Fill_Cap_Information();
        }

        string CapDetails = string.Empty;
        string Wad = string.Empty;

        private void Fill_Cap_Information()
        {
            ClearAllCap();

            if (TableID == 0)
                CapId = Convert.ToInt32(lbCap.SelectedValue);

            if (CapId != 0)
            {
                lblCapName.Text = "";
                CapDetails = string.Empty;
                Wad = string.Empty;
                objRL.Get_Cap_Records_By_Id(CapId);

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.CapName)))
                    CapDetails = objRL.CapName;
                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Wad)))
                    Wad = objRL.Wad;

                CapId = Convert.ToInt32(objRL.CapId);
                lblCapName.Text = objRL.CapName.ToString();
                lblCapName.BackColor = Color.Cyan;
                //txtInvoiceNumber.Focus();
                FillGrid();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
    }
}
