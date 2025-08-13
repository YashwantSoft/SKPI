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
    public partial class WadCOAReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0,Result = 0;
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
        
        public WadCOAReport()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_WADQUALITYCONTROL);
            objRL.Fill_Wad_ListBox(lbWad, txtSearchWad.Text, "All");
            btnSave.Text = BusinessResources.BTN_REPORT;
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("WadQualityControl"));
            txtID.Text = IDNo.ToString();
        }

        int WadId = 0; bool GridFlag = false;
        private void ClearAll_Wad()
        {
            if (!GridFlag)
                WadId = 0;

            lblWadName.Text = "";
        }

        private void txtSearchWad_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Wad();
            if (txtSearchWad.Text != "")
            {
                objRL.Fill_Wad_ListBox(lbWad, txtSearchWad.Text, "Text");
                //lbItem.Focus();
            }
            else
            {
                objRL.Fill_Wad_ListBox(lbWad, txtSearchWad.Text, "All");
            }
        }

        private void lbWad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Fill_Wad_Information();
        }

        private void lbWad_Click(object sender, EventArgs e)
        {
                Fill_Wad_Information();
        }

        string WadName = string.Empty;
        private void Fill_Wad_Information()
        {
            ClearAll_Wad();

            if (TableID == 0)
                WadId = Convert.ToInt32(lbWad.SelectedValue);

            if (WadId != 0)
            {
                lblWadName.Text = "";
                objRL.Get_Wad_Records_By_Id(WadId);

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.WadName)))
                    WadName = objRL.WadName;

                lblWadName.Text = objRL.WadName.ToString();
                lblWadName.BackColor = Color.Yellow;
                FillGrid();
                Fill_DGV_VALUES();
                btnSave.Focus();
            }
        }



        string Colour_Excel = string.Empty, CheckerName_Excel = string.Empty;

        private void ByDefaultValues()
        {
            txtSubject.Text = "Quality Check reports (Randam Samples different cavities)";
            //txtSupplierMaterialRef.Text = "Bottle Grade Pet Resin 0.76 to 0.84 I.V. from \n" +
            //                              "Reliance industries ltd. \n" +
            //                               "chiripalpolyfilms \t RIL \n" +
            //                              "Dhunseri Petrochem";

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

            MainQuery = "select CQC.ID,EntryDate as [Date],CQC.EntryTime as [Time],CQC.WadId,C.WadName as [Wad Name],CQC.InvoiceNumber as [Invoice Number],CQC.SupplierId,S.SupplierName as [Supplier Name],CQC.QCCheckerId,E.FullName as [QC Checker Name] from (((WadQualityControl CQC inner join WadMaster C on C.ID=CQC.WadId) inner join Supplier S on S.ID=CQC.SupplierId) inner join Employee E on E.ID=CQC.QCCheckerId) where CQC.CancelTag=0 and C.CancelTag=0 and S.CancelTag=0 and E.CancelTag=0 and CQC.WadId=" + WadId + "";
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

                txtBatchNo.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["ID"]));
                txtInvoiceNumber.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Invoice Number"]));
                txtSupplierName.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Supplier Name"]));
                txtQCCheckerName.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["QC Checker Name"]));
                //lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                Fill_DGV_VALUES();
            }
        }

        private void WadCOAReport_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            ByDefaultValues();
            lbWad.Focus();
        }

        private void ClearAll()
        {
            objEP.Clear();
            TableID = 0;
            Result = 0;
            FlagDelete = false;
            GridFlag = false;
            ClearAll_Wad();

            lblWadName.Text = "";
            txtID.Text = "";
            txtInvoiceNumber.Text = "";
            txtBatchNo.Text = "";
            txtInvoiceNumber.Text = "";
            txtSupplierName.Text = "";
            txtQCCheckerName.Text = "";
            GetID();
            WadId = 0;
            dtpDate.Value = DateTime.Now.Date;
            dtpTime.Value = DateTime.Now;
            txtSearchWad.Text = "";
            dgvValues.Rows.Clear();
            txtSearchWad.Focus();
        }

        //string[] DGValues = { "Type", "Custmer Logo", "Print Quality", "Outer Dia (mm)", "Inner Dia With Thread (mm)", "Neck Height (mm)", "Inner Dia WO Thread (mm)", "Cap Height (mm)", "Inner Depth (mm)", "Cap Weight (gms)", "Color", "Visual Appearance", "Flash Finishing", "Bend", "Wad Sealing", "Fitmen tWit hBottle", "Ink Test", "Drop Test" };
        //string[] DGValues = { "Type", "Custmer Logo", "Print Quality", "Board Thikness", "Board Type", "Foil Thikness", "Foil Specs", "Sealant Thikness", "Sealent Specs", "Outer Dia (mm)", "Thikness (mm)", "Weight (gms)", "Average Weight (gms)", "Visual Appearance", "Side Finishing", "Bend", "Fitment With Cap", "Ink Test", "Ind Seal Test" };
        string[] DGValues = { "Outer Dia (mm)", "Thikness (mm)", "Weight (gms)", "Average Weight (gms)", "Visual Appearance", "Side Finishing", "Bend", "Fitment With Cap", "Ink Test", "Ind Seal Test" };
        private void Fill_DGV_VALUES()
        {
            SrNo = 1;
            dgvValues.Rows.Clear();

            for (int i = 0; i < DGValues.Length; i++)
            {
                dgvValues.Rows.Add();
                dgvValues.Rows[i].Cells["clmSrNo"].Value = SrNo.ToString();
                dgvValues.Rows[i].Cells["clmParameters"].Value = DGValues[i].ToString();
                SrNo++;
            }
            Fill_Grid_Values();
        }

        protected void Fill_Grid_Values()
        {
            BoardThikness = string.Empty; BoardType = string.Empty; FoilThikness = string.Empty; FoilSpecs = string.Empty; SealantThikness = string.Empty; SealentSpecs = string.Empty;

            if (dgvValues.Rows.Count > 0)
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

                MainQuery = "select CQC.ID," +
                          "CQC.EntryDate as [Date]," +
                          "CQC.EntryTime as [Time]," +
                          "CQC.WadId," +
                          "C.WadName as [Wad Name]," +
                          "C.OuterDiaStandard," +
                          "C.OuterDiaTolerance," +
                          "C.OuterDiaMinValue," +
                          "C.OuterDiaMaxValue," +
                          "C.ThicknessStandard," +
                          "C.ThicknessTolerance," +
                          "C.ThicknessMinValue," +
                          "C.ThicknessMaxValue," +
                          "C.WeightStandard," +
                          "C.WeightTolerance," +
                          "C.WeightMinValue," +
                          "C.WeightMaxValue," +
                          "C.AverageWeightStandard," +
                          "C.AverageWeightTolerance," +
                          "C.AverageWeightMinValue," +
                          "C.AverageWeightMaxValue," +
                          "CQC.InvoiceNumber," +
                          "CQC.SupplierId," +
                          "S.SupplierName," +
                          "CQC.QCCheckerId," +
                          "E.FullName," +
                          "CQCV.Type," +
                          "CQCV.CustmerLogo," +
                          "CQCV.PrintQuality," +
                          "CQCV.BoardThikness," +
                          "CQCV.BoardType," +
                          "CQCV.FoilThikness," +
                          "CQCV.FoilSpecs," +
                          "CQCV.SealantThikness," +
                          "CQCV.SealentSpecs," +
                          "CQCV.OuterDia," +
                          "CQCV.Thikness," +
                          "CQCV.Weight," +
                          "CQCV.AverageWeight," +
                          "CQCV.VisualAppearance," +
                          "CQCV.SideFinishing," +
                          "CQCV.Bend, " +
                          "CQCV.FitmentWithCap, " +
                          "CQCV.InkTest, " +
                          "CQCV.IndSealTest " +
                          " from ((((WadQualityControl CQC inner join " +
                          " WadQualityControlValues CQCV on CQC.ID=CQCV.WadQualityControlId) inner join " +
                          " WadMaster C on C.ID=CQC.WadId) inner join " +
                          " Supplier S on S.ID=CQC.SupplierId) inner join " +
                          " Employee E on E.ID=CQC.QCCheckerId) " +
                          " where CQC.CancelTag=0 and C.CancelTag=0 and S.CancelTag=0 and E.CancelTag=0 and CQC.WadId=" + WadId + "";
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
                    btnSave.Visible = true;
                   
                    int i = 0;

                    CheckerName_Excel = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["FullName"]));

                    //dgvValues.Rows[i].Cells["clmStandards"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmTolerance"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Type"]));
                    //i++;

                    //dgvValues.Rows[i].Cells["clmStandards"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmTolerance"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["CustmerLogo"]));
                    //i++;

                    //dgvValues.Rows[i].Cells["clmStandards"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    //dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["PrintQuality"]));
                    //i++;

                    BoardThikness = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["BoardThikness"]));
                    BoardType = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["BoardType"]));
                    FoilThikness = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["FoilThikness"]));
                    FoilSpecs = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["FoilSpecs"]));
                    SealantThikness = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["SealantThikness"]));
                    SealentSpecs = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["SealentSpecs"]));

                    //dgvValues.Rows[i].Cells["clmStandards"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmTolerance"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["BoardThikness"]));
                    //i++;

                    //dgvValues.Rows[i].Cells["clmStandards"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmTolerance"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["BoardType"]));
                    //i++;

                    //dgvValues.Rows[i].Cells["clmStandards"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmTolerance"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["FoilThikness"]));
                    //i++;

                    //dgvValues.Rows[i].Cells["clmStandards"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmTolerance"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["FoilSpecs"]));
                    //i++;


                    //dgvValues.Rows[i].Cells["clmStandards"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmTolerance"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["SealantThikness"]));
                    //i++;

                    //dgvValues.Rows[i].Cells["clmStandards"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmTolerance"].Value = "-";
                    //dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["SealentSpecs"]));
                    //i++;
                     
                    dgvValues.Rows[i].Cells["clmStandards"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["OuterDiaStandard"]));
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["OuterDiaTolerance"]));
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["OuterDia"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["ThicknessStandard"]));
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["ThicknessTolerance"]));
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Thikness"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["WeightStandard"]));
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["WeightTolerance"]));
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Weight"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["AverageWeightStandard"]));
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["AverageWeightTolerance"]));
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["AverageWeight"]));
                    i++;
                     
                    dgvValues.Rows[i].Cells["clmStandards"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["VisualAppearance"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["SideFinishing"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Bend"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["FitmentWithCap"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InkTest"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["IndSealTest"]));
                    i++;

                    //250GMS Pulp Board, 25 µm Aluminum Foil,23µm HSPET
                    
                    //BoardThikness = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["BoardThikness"]));
                    //BoardType = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["BoardType"]));
                    //FoilThikness = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["FoilThikness"]));
                    //FoilSpecs = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["FoilSpecs"]));
                    //SealantThikness = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["SealantThikness"]));
                    //SealentSpecs =

                        string ConcatSupplierMaterialRefrences=string.Empty;
                    ConcatSupplierMaterialRefrences= BoardThikness +" gms "+BoardType+", "+FoilThikness+" µm "+FoilSpecs + SealantThikness+" µm "+ SealentSpecs;

                    txtSupplierMaterialRef.Text = ConcatSupplierMaterialRefrences.ToString();
                } 
            }
        }

        string BoardThikness = string.Empty, BoardType = string.Empty, FoilThikness = string.Empty, FoilSpecs = string.Empty, SealantThikness = string.Empty, SealentSpecs = string.Empty;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvValues.Rows.Count > 0)
            {
                ExcelReportMail();
            }
            else
            {
                objRL.ShowMessage(25, 4);
                return;
            }
        }

        private void ExcelReportMail()
        {
            objRL.FillCompanyData();

            object misValue = System.Reflection.Missing.Value;
            myExcelApp = new Microsoft.Office.Interop.Excel.Application();
            myExcelWorkbooks = myExcelApp.Workbooks;

            objRL.ClearExcelPath();
            objRL.isPDF = true;
            objRL.Form_ExcelFileName = "WadCOAReport.xlsx";
            objRL.Form_ReportFileName = "Wad COA Report-" + DateTime.Now.Date.ToString("dd-MMM-yyyy");
            objRL.Form_DestinationReportFilePath = "Wad COA Report\\";

            objRL.Path_Comman();

            myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
            Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

            //myExcelWorksheet.get_Range("J6", misValue).Formula = dtpDated.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);
            //myExcelWorksheet.get_Range("J9", misValue).Formula = BusinessLayer.UserName_Static.ToString();
            //myExcelWorksheet.get_Range("C7", misValue).Formula = txtQuantity.Text;
            //myExcelWorksheet.get_Range("C8", misValue).Formula = txtInvoiceNo.Text;
            //myExcelWorksheet.get_Range("C10", misValue).Formula = txtNoOfPackages.Text;
            //myExcelWorksheet.get_Range("C11", misValue).Formula = txtVehicalNo.Text;
            //myExcelWorksheet.get_Range("C12", misValue).Formula = txtOrderReferanceNo.Text;
            //myExcelWorksheet.get_Range("J7", misValue).Formula = dtpOrderDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);

            myExcelWorksheet.get_Range("C5", misValue).Formula = objRL.WadName;
            //myExcelWorksheet.get_Range("C7", misValue).Formula = txtSupplierName.Text;
            myExcelWorksheet.get_Range("G5", misValue).Formula = dtpDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);
            myExcelWorksheet.get_Range("G6", misValue).Formula = txtID.Text;
            myExcelWorksheet.get_Range("G7", misValue).Formula = txtBatchNo.Text;
            myExcelWorksheet.get_Range("C8", misValue).Formula = "Quality Check reports (Randam Samples different cavities)";
            myExcelWorksheet.get_Range("C9", misValue).Formula = objRL.WadName;
            myExcelWorksheet.get_Range("C10", misValue).Formula = txtSupplierName.Text;
            myExcelWorksheet.get_Range("C11", misValue).Formula = Colour_Excel.ToString();
            myExcelWorksheet.get_Range("A13", misValue).Formula = txtSupplierMaterialRef.Text;

            //250GMS Pulp Board, 25 µm Aluminum Foil,23µm HSPET

            SrNo = 1; RowCount = 20; BorderFlag = false;

            for (int i = 0; i < dgvValues.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmSrNo"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, Convert.ToString(dgvValues.Rows[i].Cells["clmSrNo"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmParameters"].Value)))
                {
                    AFlag = 0;
                    Fill_Merge_Cell("B", "D", misValue, myExcelWorksheet, Convert.ToString(dgvValues.Rows[i].Cells["clmParameters"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmStandards"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, Convert.ToString(dgvValues.Rows[i].Cells["clmStandards"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmTolerance"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, Convert.ToString(dgvValues.Rows[i].Cells["clmTolerance"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmQCValue"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, Convert.ToString(dgvValues.Rows[i].Cells["clmQCValue"].Value));
                }

                RowCount++;
            }

            RowCount++; BorderFlag = true;
            AFlag = 0;
            Fill_Merge_Cell("A", "G", misValue, myExcelWorksheet, "Remark - All 10 Points are within Standards");
            RowCount++;
            Fill_Merge_Cell("A", "G", misValue, myExcelWorksheet, "Inspected by- Firoj Rashid Shaikh, QC Check by - " + CheckerName_Excel + "");

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

            //System.Diagnostics.Process.Start(PDFReport);
            //System.Diagnostics.Process.Start(PDFReport);
            System.Diagnostics.Process.Start(objRL.RL_DestinationPath);
            //objRL.DeleteExcelFile();

            //if (!string.IsNullOrEmpty(objRL.EmailId) && cbEmail.Checked)
            //{
            //    objRL.EmailId_RL = objRL.EmailId;
            //    objRL.Subject_RL = "Working Report";
            //    //string body = "<div><p>Dear " + objRL.CustomerName_Customer + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
            //    string body = "<div> <p>Please find attachment of pdf file.</p><p>Thanks,</p><p>" + objRL.CI_CompanyName + "</p></div>";

            //    objRL.Body_RL = body;// "Dear " + RedundancyLogics.SupplierName + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
            //    objRL.FilePath_RL = PDFReport;
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
