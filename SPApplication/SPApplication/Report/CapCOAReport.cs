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
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("CapCOAReport"));
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
            ByDefaultValues();
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

                txtBatchNo.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["ID"]));
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
                Fill_DGV_VALUES();
                
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnReport_Click(object sender, EventArgs e)
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

        string Colour_Excel = string.Empty, CheckerName_Excel=string.Empty;

        private void ByDefaultValues()
        {
            txtSubject.Text = "Quality Check reports (Randam Samples different cavities)";
            txtSupplierMaterialRef.Text = "Bottle Grade Pet Resin 0.76 to 0.84 I.V. from \n" +
                                          "Reliance industries ltd. \n" +
                                           "chiripalpolyfilms \t RIL \n" +
                                          "Dhunseri Petrochem";

        }

        private void ExcelReportMail()
        {
            objRL.FillCompanyData();

            object misValue = System.Reflection.Missing.Value;
            myExcelApp = new Microsoft.Office.Interop.Excel.Application();
            myExcelWorkbooks = myExcelApp.Workbooks;

            objRL.ClearExcelPath();
            objRL.isPDF = true;
            objRL.Form_ExcelFileName = "CapCOAReport.xlsx";
            objRL.Form_ReportFileName = "CAP COA Report-" + DateTime.Now.Date.ToString("dd-MMM-yyyy");
            objRL.Form_DestinationReportFilePath = "CAP COA Report\\";

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

            myExcelWorksheet.get_Range("C5", misValue).Formula = objRL.CapName;
            //myExcelWorksheet.get_Range("C7", misValue).Formula = txtSupplierName.Text;
            myExcelWorksheet.get_Range("G5", misValue).Formula = dtpDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);
            myExcelWorksheet.get_Range("G6", misValue).Formula = txtID.Text;
            myExcelWorksheet.get_Range("G7", misValue).Formula = txtBatchNo.Text;
            myExcelWorksheet.get_Range("C8", misValue).Formula = "Quality Check reports (Randam Samples different cavities)";
            myExcelWorksheet.get_Range("C9", misValue).Formula = objRL.CapName;
            myExcelWorksheet.get_Range("C10", misValue).Formula = txtSupplierName.Text;
            myExcelWorksheet.get_Range("C11", misValue).Formula = Colour_Excel.ToString();
            myExcelWorksheet.get_Range("A13", misValue).Formula = txtSupplierMaterialRef.Text;

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
            Fill_Merge_Cell("A", "G", misValue, myExcelWorksheet, "Remark - All 12 Points are within Standards");
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        //string[] DGValues = { "Type", "Custmer Logo", "Print Quality", "Outer Dia (mm)", "Inner Dia With Thread (mm)", "Neck Height (mm)", "Inner Dia WO Thread (mm)", "Cap Height (mm)", "Inner Depth (mm)", "Cap Weight (gms)", "Color", "Visual Appearance", "Flash Finishing", "Bend", "Wad Sealing", "Fitmen tWit hBottle", "Ink Test", "Drop Test" };
        string[] DGValues = { "Type", "Custmer Logo", "Print Quality", "Outer Dia (mm)", "Inner Dia With Thread (mm)", "Inner Dia WO Thread (mm)", "Cap Height (mm)", "Inner Depth (mm)", "Cap Weight (gms)", "Color", "Visual Appearance", "Flash Finishing", "Bend", "Fitment With Bottle", "Ink Test", "Drop Test" };
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
                          "CQC.CapId," +
                          "C.CapName as [Cap Name],"+
                          "OuterDiaStandard," +
                          "C.OuterDiaTolerance," +
                          "C.OuterDiaMinValue," +
                          "C.OuterDiaMaxValue," +
                          "C.InnerDiaWithThreadStandard," +
                          "C.InnerDiaWithThreadTolerance," +
                          "C.InnerDiaWithThreadMinValue," +
                          "C.InnerDiaWithThreadMaxValue," +
                          "C.InnerDiaWOThreadStandard," +
                          "C.InnerDiaWOThreadTolerance," +
                          "C.InnerDiaWOThreadMinValue," +
                          "C.InnerDiaWOThreadMaxValue," +
                          "C.CapHeightStandard," +
                          "C.CapHeightTolerance," +
                          "C.CapHeightMinValue," +
                          "C.CapHeightMaxValue," +
                          "C.InnerDepthStandard," +
                          "C.InnerDepthTolerance," +
                          "C.InnerDepthMinValue," +
                          "C.InnerDepthMaxValue," +
                          "C.CapWeightStandard," +
                          "C.CapWeightTolerance," +
                          "C.CapWeightMinValue," +
                          "C.CapWeightMaxValue," +
                          "CQC.InvoiceNumber," +
                          "CQC.SupplierId," +
                          "S.SupplierName," +
                          "CQC.QCCheckerId," +
                          "E.FullName,"+
                          "CQCV.Type," +
                          "CQCV.CustmerLogo," +
                          "CQCV.PrintQuality," +
                          "CQCV.OuterDia," +
                          "CQCV.InnerDiaWithThread," +
                          "CQCV.InnerDiaWOThread," +
                          "CQCV.CapHeight," +
                          "CQCV.InnerDepth," +
                          "CQCV.CapWeight," +
                          "CQCV.Color," +
                          "CQCV.VisualAppearance," +
                          "CQCV.FlashFinishing," +
                          "CQCV.Bend," +
                          "CQCV.FitmentWithBottle," +
                          "CQCV.InkTest," +
                          "CQCV.DropTest " +
                          " from ((((CapQualityControl CQC inner join "+
                          " CapQualityControlValues CQCV on CQC.ID=CQCV.CapQualityControlId) inner join " +
                          " CapMaster C on C.ID=CQC.CapId) inner join " +
                          " Supplier S on S.ID=CQC.SupplierId) inner join "+
                          " Employee E on E.ID=CQC.QCCheckerId) "+
                          " where CQC.CancelTag=0 and C.CancelTag=0 and S.CancelTag=0 and E.CancelTag=0 and CQC.CapId=" + CapId + "";
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
                    btnReport.Visible = true;

                    int i = 0;

                    CheckerName_Excel = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["FullName"]));

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "-";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "-";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Type"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "-";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "-";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["CustmerLogo"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "-";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["PrintQuality"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["OuterDiaStandard"]));
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["OuterDiaTolerance"]));
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["OuterDia"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InnerDiaWithThreadStandard"]));
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InnerDiaWithThreadTolerance"]));
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InnerDiaWithThread"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InnerDiaWOThreadStandard"]));
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InnerDiaWOThreadTolerance"]));
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InnerDiaWOThread"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["CapHeightStandard"]));
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["CapHeightTolerance"]));
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["CapHeight"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InnerDepthStandard"]));
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InnerDepthTolerance"]));
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InnerDepth"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["CapWeightStandard"]));
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["CapWeightTolerance"]));
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["CapWeight"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "-";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    Colour_Excel = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Color"]));
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = Colour_Excel;
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["VisualAppearance"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["FlashFinishing"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Bend"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["FitmentWithBottle"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InkTest"]));
                    i++;

                    dgvValues.Rows[i].Cells["clmStandards"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmQCValue"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["DropTest"]));
                    i++;


                    //dgvValues.Rows[i].Cells[5].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Type"]));
                    

                    //dgvValues.Rows[i].Cells[5].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["CustmerLogo"]));
                    //dgvValues.Rows[i].Cells[5].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["PrintQuality"]));
                    //dgvValues.Rows[i].Cells[5].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["OuterDia"]));
                    //dgvValues.Rows[i].Cells[5].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InnerDiaWithThread"]));
                    //dgvValues.Rows[i].Cells[5].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InnerDiaWOThread"]));
                    //dgvValues.Rows[i].Cells[5].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["CapHeight"]));
                    //dgvValues.Rows[i].Cells[5].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["InnerDepth"]));
                    //dgvValues.Rows[i].Cells[5].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["CapWeight"]));
                     
                }
            }
        }

    }
}
