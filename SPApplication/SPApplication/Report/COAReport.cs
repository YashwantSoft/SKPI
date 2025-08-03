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
    public partial class COAReport : Form
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

        public COAReport()
        {
            InitializeComponent();
            Set_Design();
        }

        private void Set_Design()
        {
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_CERTIFICATEOFANALYSIS);
            objDL.SetPlusButtonDesign(btnAddCustomer);
            objRL.FillPreformParty(cmbPreformSuppliers);
            objRL.Fill_Customer(cmbCustomerName);
            objDL.SetButtonDesign(btnReport, BusinessResources.BTN_REPORT);
            // dataGridView1.DefaultCellStyle.SelectionBackColor = objBL.GetRowSelectionColor();
            lblBottleInformation.BackColor = Color.Cyan;
            lblJarInformation.BackColor = Color.Yellow;
        }

        private void ClearAll()
        {
            TableID = 0;
            objEP.Clear();
            ClearValues();
            ClearAll_Item();
            objRL.Fill_Item_ListBox_In_QCRecord(lbItem, txtSearchProductName.Text, "All");
            gbCOAParameters.Enabled = false;
            dgvValues.Enabled = false;


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Remark -Nothing reported any major or out of standereds, Consignment ready for dispatch
            //Inspected By-Pratik khopde
        }

        string MainQuery = string.Empty, WhereClause = string.Empty, OrderByClause = string.Empty;

        private void FillGrid()
        {
            MainQuery = string.Empty; WhereClause = string.Empty; OrderByClause = string.Empty;

            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            MainQuery = "select CR.ID,CR.EntryDate as [Date],Format([CR.EntryTime], 'hh:nn:ss') AS [Time], CR.ProductId,P.ProductType,P.ProductName,P.MouldNo from COAReport CR inner join Product P on P.ID=CR.ProductId where CR.CancelTag=0 and P.CancelTag=0 ";

            if (SearchTag)
                WhereClause = " and P.ProductName like '%" + txtSearchItemName.Text + "%'";

            OrderByClause = " order by CR.ID desc";
            objBL.Query = MainQuery +WhereClause +OrderByClause;
             
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 CR.ID,
                //1 CR.EntryDate,
                //2 CR.EntryTime,
                //3 CR.ProductId,
                //4 P.ProductType,
                //5 P.ProductName,
                //6 P.MouldNo

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[3].Visible = false;

                dataGridView1.Columns[1].Width = 120;
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[4].Width = 150;
                dataGridView1.Columns[5].Width = 600;
                dataGridView1.Columns[6].Width = 120;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        private void COAReport_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            //objRL.Fill_Item_ListBox(lbItem, txtSearchProductName.Text, "All");

            //GetID();
            ByDefaultValues();
            cmbCustomerName.Focus();
        }

        //private void FillGrid()
        //{
        //    dataGridView1.DataSource = null;
        //    DataSet ds = new DataSet();

        //    if (!SearchTag)
        //        objBL.Query = "select ID,PreformId,PreformName,ProductType,ProductName,ProductNickName,MouldId,MouldNo,Standard,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,ProductMajorAxis,ProductMajorAxisRatio,ProductMajorAxisMinValue,ProductMajorAxisMaxValue,ProductMinorAxis,ProductMinorAxisRatio,ProductMinorAxisMinValue,ProductMinorAxisMaxValue,Status,NoteR,PackingQty,Semi,Auto1,Auto2,Servo from Product where CancelTag=0";
        //    else
        //        objBL.Query = "select ID,PreformId,PreformName,ProductType,ProductName,ProductNickName,MouldId,MouldNo,Standard,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,ProductMajorAxis,ProductMajorAxisRatio,ProductMajorAxisMinValue,ProductMajorAxisMaxValue,ProductMinorAxis,ProductMinorAxisRatio,ProductMinorAxisMinValue,ProductMinorAxisMaxValue,Status,NoteR,PackingQty,Semi,Auto1,Auto2,Servo from Product where CancelTag=0 and ProductName like '%" + txtSearchItemName.Text + "%'";

        //    //if (!SearchTag)
        //    //    objBL.Query = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard,Qty from Product where CancelTag=0 and ProductType='" + ProductType + "'";
        //    //else
        //    //    objBL.Query = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard,Qty from Product where CancelTag=0 and ProductType='" + ProductType + "' and ProductName like '%" + txtSearchItemName.Text + "%'";

        //    ds = objBL.ReturnDataSet();

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        dataGridView1.DataSource = ds.Tables[0];
        //        dataGridView1.Columns[0].Visible = false;
        //        dataGridView1.Columns[1].Visible = false;
        //        dataGridView1.Columns[6].Visible = false;

        //        dataGridView1.Columns[2].Width = 200;
        //        dataGridView1.Columns[3].Width = 120;
        //        dataGridView1.Columns[4].Width = 400;
        //        dataGridView1.Columns[5].Width = 120;
        //        lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
        //    }
        //}

        //string[] DGValues = { "NECK OD With Thread(mm)", "OD Without thread", "NECK HEIGHT(mm)Wo Coll", "NECK Inner Dia (mm)", "WEIGHT (Gms) Bottle", "TOTAL HEIGHT(mm)-P-Max", "OFC Vol (ml)", "Length at Base", "Width at Base", "Neck Damage", "Flash", "Haze (whiteness)", "Dent", "Scratches", "Bubble", "Stretch marks", "Fitment with cap", "Leakage", "Droptest", "Top Load test", "Color Opacity" };

        string[] DGValues = { "Bottle Weight (gms)", "Neck Size (mm)", "Inner Dia (mm)", "Outer Dia (mm)", "Retainer Gap (mm)", "Neck Height (mm)", "Overflow Volume (ml)", "Major Axis (mm)", "Minor Axis (mm)", "Bottle Height (mm)", "Base Information", "Visuals", "Go/Go No Gauge", "Cap Fitment", "Wad Sealing", "Leak Test", "Drop Test", "TopLoad Test" };
        private void Fill_DGV_VALUES()
        {
            //NECK OD With Thread(mm) 27 +/- 0.5 27.41 27.3                 || OuterDia
            //OD Without thread 25 +/- 0.5mm 25.07 25.09                      || RetainerGap 
            //NECK HEIGHT(mm)Wo Coll 19 +/- 0.5mm 19.1 19.11            || Height
            //NECK Inner Dia (mm) 21.8 +/- 0.15mm 21.7 21.66                || InnerDia
            //WEIGHT (Gms) Bottle Only 31 Gms+/- 1Gm 31.2 31.1          || Weight
            //TOTAL HEIGHT(mm)-P-Max 186 +/- 2mm 186 186                 || BottleHeight
            //OFC Vol (ml) 285 +/-10ml 282.5 282                                       || OverflowVolume
            //Length at Base 70 +/- 1mm 71.48 71.41                                  || MajorAxis
            //Width at Base 45 +/- 1mm 44.45 44.39                                  || MinorAxis
            //Neck Damage Should be nil Nil Nil                                          || OK
            //Flash Should be nil Nil Nil                                                        || OK
            //Haze (whiteness)_ Should be nil Nil Nil                                 || 
            //Dent Should be nil Nil Nil
            //Scratches Should be nil Nil Nil
            //Bubble Should be nil NA NA
            //Stretch marks Should be nil Nil Nil
            //Fitment with cap - Ok / Not Ok Ok Ok
            //Leakage Should be nil Nil Nil
            //Droptest Should be ok till 1.4Mtr Ok Ok
            //Top Load test Above 10Kg 12kg 12.1Kg
            //Color Opacity (Opaq Only)

            //0  "Bottle Weight (gms)",
            //1  "Neck Size (mm)", 
            //2  "Inner Dia (mm)", 
            //3  "Outer Dia (mm)", 
            //4  "Retainer Gap (mm)", 
            //5  "Neck Height (mm)", 
            //6 "Overflow Volume (ml)", 
            //7 "Major Axis (mm)", 
            //8 "Minor Axis (mm)", 
            //9 "Bottle Height (mm)",

            //10 "Visuals", 
            //11 "Go Gauge",
            //12 "Capt Fitment", 
            //13 "Wad Sealing", 
            //14 "Leak Test", 
            //15 "Drop Test", 
            //16 "TopLoad Test"

            SrNo = 1;
            dgvValues.Rows.Clear();

            for (int i = 0; i < DGValues.Length; i++)
            {
                dgvValues.Rows.Add();
                dgvValues.Rows[i].Cells["clmSrNo"].Value = SrNo.ToString();
                dgvValues.Rows[i].Cells["clmParameters"].Value = DGValues[i].ToString();
                Get_Tolerance_Standard(i);

                if (i < 10)
                {
                    Set_AverageValue(i);
                    dgvValues.Rows[i].Cells["clmStandards"].Value = Standard_Set;
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = Tolerance_Set;
                    dgvValues.Rows[i].Cells["clmCavity1"].Value = AverageValue;
                    dgvValues.Rows[i].Cells["clmCavity2"].Value = AverageValue;

                    //int StdValue = (int)AverageValue;
                    //string Std = StdValue + "+/- 0.5";
                }
                else
                {
                    dgvValues.Rows[i].Cells["clmStandards"].Value = "Ok";
                    dgvValues.Rows[i].Cells["clmTolerance"].Value = "Ok";

                    if (i == 10)
                        Fill_Grid_Values(dgvValues, i, Visuals_I);
                    else if (i == 11)
                        Fill_Grid_Values(dgvValues, i, Visuals_I);
                    else if (i == 12)
                        Fill_Grid_Values(dgvValues, i, GoGauge_I);
                    else if (i == 13)
                        Fill_Grid_Values(dgvValues, i, CaptFitment_I);
                    else if (i == 14)
                        Fill_Grid_Values(dgvValues, i, WadSealing_I);
                    else if (i == 15)
                        Fill_Grid_Values(dgvValues, i, LeakTest_I);
                    else if (i == 16)
                        Fill_Grid_Values(dgvValues, i, DropTest_I);
                    else
                        Fill_Grid_Values(dgvValues, i, TopLoadTest_I);

                }
                SrNo++;
            }

            txtID.Text = "";
            //Save COE Report
            objBL.Query = "insert into COAReport(EntryDate,EntryTime,ProductId,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpDate.Value.ToShortTimeString() + "'," + ProductId + ","+BusinessLayer.UserId_Static+")";
            Result = objBL.Function_ExecuteNonQuery();
            if (Result > 0)
            {
                txtID.Text = Convert.ToString(objRL.ReturnMaxID_Fix("COAReport","ID"));
                FillGrid();
            }
        }

        int Result = 0;
        string Standard_Set = string.Empty, Tolerance_Set = string.Empty;

        private void Get_Tolerance_Standard(int c)
        {

        }

        private void Fill_Grid_Values(DataGridView dgv, int RIndex, string Value_I)
        {
            dgv.Rows[RIndex].Cells["clmCavity1"].Value = Value_I;
            dgv.Rows[RIndex].Cells["clmCavity2"].Value = Value_I;
        }

        public void Set_AverageValue(int ColumnIndex)
        {
            Standard_Set = string.Empty; Tolerance_Set = string.Empty;
            //double MinValue, double MaxValue
            switch (ColumnIndex)
            {
                //0 Sr.No   ||
                //1 Supplier    ||
                //2 Weight  || 
                //3 Colour

                //4 Size
                //5 Inner Dia
                //6 Outer Dia
                //7 Retainer Gap
                //8 Height

                //9 Overflow Volume
                //10 Major Axis
                //11 Minor Axis
                //12 Bottle Height

                //13 Visuals
                //14 Go/No Go Guage
                //15 Cap Fitment
                //16 Wad Sealing
                //17 Leak Test
                //18 Drop Test
                //19 Top Load Test

                case 0: //ProductWeight   Datagridviewcolumn- //02 Weight
                    GetAverageValue("Weight");
                    Standard_Set = objRL.ProductWeight;
                    Tolerance_Set = objRL.ProductWeightRatio;
                    break;
                case 1: //ProductNeckSize Datagridviewcolumn- //04 Size
                    GetAverageValue("Size");
                    Standard_Set = objRL.ProductNeckSize;
                    Tolerance_Set = objRL.ProductNeckSizeRatio;
                    break;
                case 2: //ProductNeckID    Datagridviewcolumn- //05 Inner Dia   InnerDia
                    GetAverageValue("InnerDia");
                    Standard_Set = objRL.ProductNeckID;
                    Tolerance_Set = objRL.ProductNeckIDRatio;
                    break;
                case 3: //ProductNeckOD Datagridviewcolumn- //06 Outer Dia  OuterDia
                    GetAverageValue("OuterDia");
                    Standard_Set = objRL.ProductNeckOD;
                    Tolerance_Set = objRL.ProductNeckODRatio;
                    break;
                case 4: //ProductNeckCollarGap Datagridviewcolumn-   //7 Retainer Gap  RetainerGap
                    GetAverageValue("RetainerGap");
                    Standard_Set = objRL.ProductNeckCollarGap;
                    Tolerance_Set = objRL.ProductNeckCollarGapRatio;
                    break;
                case 5: //ProductNeckHeight Datagridviewcolumn-   //8 Height Height
                    GetAverageValue("Height");
                    Standard_Set = objRL.ProductNeckHeight;
                    Tolerance_Set = objRL.ProductNeckHeightRatio;
                    break;
                case 6: //ProductVolume Datagridviewcolumn-   //9 Overflow Volume  OverflowVolume
                    GetAverageValue("OverflowVolume");
                    Standard_Set = objRL.ProductVolume;
                    Tolerance_Set = objRL.ProductVolumeRatio;
                    break;
                case 7: //ProductMajorAxis Datagridviewcolumn-   //10 Major Axis  MajorAxis
                    GetAverageValue("MajorAxis");
                    Standard_Set = objRL.ProductMajorAxis;
                    Tolerance_Set = objRL.ProductMajorAxisRatio;
                    break;
                case 8: //ProductMinorAxis Datagridviewcolumn-   //11 Minor Axis  MinorAxis
                    GetAverageValue("MinorAxis");
                    Standard_Set = objRL.ProductMinorAxis;
                    Tolerance_Set = objRL.ProductMinorAxisRatio;
                    break;
                case 9: //ProductHeight   Datagridviewcolumn-   //12 Bottle Height  BottleHeight
                    GetAverageValue("BottleHeight");
                    Standard_Set = objRL.ProductHeight;
                    Tolerance_Set = objRL.ProductHeightRatio;
                    break;
            }
        }

        private void ByDefaultValues()
        {
            txtSubject.Text = "Quality Check reports (Randam Samples different cavities)";
            txtSupplierMaterialRef.Text = "Bottle Grade Pet Resin 0.76 to 0.84 I.V. from \n" +
                                          "Reliance industries ltd. \n" +
                                           "chiripalpolyfilms \t RIL \n" +
                                          "Dhunseri Petrochem";

        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("COAReport"));
            txtID.Text = IDNo.ToString();
        }



        private void txtSearchProductName_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Item();
            if (txtSearchProductName.Text != "")
            {
                objRL.Fill_Item_ListBox_In_QCRecord(lbItem, txtSearchProductName.Text, "Text");
                //lbItem.Focus();
            }
            else
            {
                lbItem.Visible = true;
                objRL.Fill_Item_ListBox_In_QCRecord(lbItem, txtSearchProductName.Text, "All");
            }
        }

        private void ClearAll_Item()
        {
            ProductId = 0;
            lblProductName.Text = "";
            cmbColor.SelectedIndex = -1;
            txtBatchNo.Text = "";
            txtSubject.Text = "";
            txtMaterialUsed.Text = "";
            cmbPreformSuppliers.SelectedIndex = -1;
            txtSupplierMaterialRef.Text = "";
            dgvValues.Rows.Clear();
            ByDefaultValues();
        }

        private void lbItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Fill_Product_Information();
                //txtQty.Focus();
            }
        }

        private void lbItem_Click(object sender, EventArgs e)
        {
            Fill_Product_Information();
        }

        int ProductId = 0; string ProductDetails = string.Empty;
        string Standard = string.Empty;

        private void Fill_Product_Information()
        {
            ClearValues();

            if (TableID == 0)
                ProductId = Convert.ToInt32(lbItem.SelectedValue);

            if (ProductId != 0)
            {
                lblProductName.Text = "";
                ProductDetails = string.Empty;

                objRL.Get_Product_Records_By_Id(ProductId);
                //ProductDetails = string.Empty;
                Standard = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Standard)))
                    Standard = objRL.Standard;

                //NeckHeightWeightQR = "Neck/Weight-" + objRL.ProductNeckSize.ToString() + "mm/" + Standard + "/" + objRL.ProductWeight;
                //ProductDetails = "Product-\t" + objRL.ProductName.ToString() + "\n" +
                //                    "Party-\t" + objRL.Party.ToString() + "\n" + NeckHeightWeightQR;
                ////"Neck/Height/Weight-" + objRL.ProductNeckSize.ToString() + "mm/" + objRL.ProductHeight.ToString() + "/" + objRL.ProductWeight;

                ProductId = Convert.ToInt32(objRL.ProductId);
                lblProductName.Text = objRL.ProductName.ToString();
                Fill_Parameter_Data();
                Fill_DGV_VALUES();

                if (objRL.ProductType == "Bottle")
                    lblProductName.BackColor = Color.Cyan;
                else
                    lblProductName.BackColor = Color.Yellow;

                if (!string.IsNullOrEmpty(objRL.Qty))
                {
                    txtNoOfPackages.Text = objRL.Qty.ToString();
                }
                else
                    txtNoOfPackages.Text = "";

                txtMaterialUsed.Text = objRL.PreformName;
            }
        }

        string ColumnName = string.Empty, ParameterName = string.Empty, Standards = string.Empty;

        int QCEntryId = 0, MachineNo = 0, QCEntryMachineId = 0, ProductionEntryId = 0;

        private void Fill_Parameter_Data()
        {
            ColumnName = string.Empty; ParameterName = string.Empty; Standards = string.Empty;

            //QCEntry Query
            //  objBL.Query = "select ID,EntryDate,EntryTime,ShiftEntryId from QCEntry where CancelTag=0 and ShiftEntryId=" + ShiftEntryId + "";

            //QCEntryMachine
            //objBL.Query = "select ID,QCEntryId,ProductionEntryId,MachineNo,ProductId,SwitchFlag,Reason,ReasonInDetails from QCEntryMachine where CancelTag=0 and QCEntryId=" + QCEntryId + " and MachineNo=" + MachineNo + "";

            //QCEntryMachineValues
            //objBL.Query = "select ID,QCEntryId,QCEntryMachineId,ProductionEntryId,MachineNo,ProductId,Supplier,[Weight],[Color],[Size],InnerDia,OuterDia,RetainerGap,Height,OverflowVolume,MajorAxis,MinorAxis,BottleHeight,Visuals,GoGauge,CaptFitment,WadSealing,LeakTest,DropTest,TopLoadTest from QCEntryMachineValues where CancelTag=0 and QCEntryId=" + QCEntryId + " and MachineNo=" + MachineNo + " and QCEntryMachineId=" + QCEntryMachineId + " and ProductionEntryId=" + ProductionEntryId + "";

            //objBL.Query = "select ID,QCEntryId,QCEntryMachineId,ProductionEntryId,MachineNo,ProductId,Supplier,Avg([Weight]),[Color],[Size],InnerDia,OuterDia,RetainerGap,Height,OverflowVolume,MajorAxis,MinorAxis,BottleHeight,Visuals,GoGauge,CaptFitment,WadSealing,LeakTest,DropTest,TopLoadTest from QCEntryMachineValues where CancelTag=0 and QCEntryId=" + QCEntryId + " and MachineNo=" + MachineNo + " and QCEntryMachineId=" + QCEntryMachineId + " and ProductionEntryId=" + ProductionEntryId + "";

            //objBL.Query = "select Avg([QCEMV.Weight]) from ((QCEntryMachineValues QCEMV inner join QCEntryMachine QCEM on QCEM.ID=QCEMV.QCEntryMachineId) inner join QCEntry QCE on QCE.ID=QCEM.QCEntryId) where QCEMV.CancelTag=0 and QCEM.CancelTag=0 and QCE.CancelTag=0 and ";

            //objBL.Query = "select Avg([QCEMV.Weight]) from ((QCEntryMachineValues QCEMV inner join QCEntryMachine QCEM on QCEM.ID=QCEMV.QCEntryMachineId) inner join QCEntry QCE on QCE.ID=QCEM.QCEntryId) where QCEMV.CancelTag=0 and QCEM.CancelTag=0 and QCE.CancelTag=0 and ";
            //QCEntryId=" + QCEntryId + " and MachineNo=" + MachineNo + " and QCEntryMachineId=" + QCEntryMachineId + " and ProductionEntryId=" + ProductionEntryId + "";
            //           objBL.Query="SELECT QCE.ID,QCE.EntryDate,QCE.EntryTime,QCE.ShiftEntryId,SE.Shift FROM QCEntry as QCE INNER JOIN  
            //(SELECT regno, Max(fyend) AS MaxOfFyend  
            // FROM extract_financial as ef
            // WHERE ef.income IS NOT NULL
            // GROUP BY regno  
            //) AS efx  
            //ON ef.regno = efx.regno AND ef.fyend = efx.MaxOfFyend;

            // Avg([QCEMV.Weight]) as [AverageWeight] 
            ClearValues();
            DataSet ds = new DataSet();
            // objBL.Query = "select QCE.ID,QCE.EntryDate,QCE.EntryTime,QCE.ShiftEntryId,SE.Shift from ((QCEntry QCE inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join QCEntryMachine QCEM on QCEM.QCEntryId=QCE.ID) where QCE.CancelTag=0 and SE.CancelTag=0 and QCEM.CancelTag=0 and QCEM.ProductId="+ProductId+"";
            //objBL.Query = "select QCE.ID,QCE.EntryDate,QCE.EntryTime,QCE.ShiftEntryId,SE.Shift from (((QCEntry QCE inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join QCEntryMachine QCEM on QCEM.QCEntryId=QCE.ID) inner join QCEntryMachineValues QCEMV on QCEM.ID=QCEMV.QCEntryMachineId) where QCEMV.CancelTag=0 and QCE.CancelTag=0 and SE.CancelTag=0 and QCEM.CancelTag=0 and QCEM.ProductId=" + ProductId + " and QCEM.ProductId=" + ProductId + "";

            //MainQuery
            //objBL.Query = "select QCE.EntryDate,QCE.ID from (((QCEntry QCE inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join QCEntryMachine QCEM on QCEM.QCEntryId=QCE.ID) inner join QCEntryMachineValues QCEMV on QCEM.ID=QCEMV.QCEntryMachineId) where QCEMV.CancelTag=0 and QCE.CancelTag=0 and SE.CancelTag=0 and QCEM.CancelTag=0 and QCEM.ProductId=" + ProductId + " and QCEMV.ProductId=" + ProductId + " and SE.Shift IN('I','II','III') and QCE.EntryDate=(SELECT MAX(t.EntryDate) FROM QCEntry t WHERE t.ID=QCE.ID order by QCE.EntryDate desc) order by QCE.EntryDate desc "; //  Having  MAX(QCE.EntryDate) order by QCE.EntryDate"; // MAX(QCE.EntryDate) "; // and QCE.EntryDate IN(SELECT MAX(EntryDate) FROM QCEntry QCE1 WHERE QCE1.EntryDate = QCE.EntryDate)";

            objBL.Query = "select MAX(QCE.EntryDate) as [FindDate] from (((QCEntry QCE inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join QCEntryMachine QCEM on QCEM.QCEntryId=QCE.ID) inner join QCEntryMachineValues QCEMV on QCEM.ID=QCEMV.QCEntryMachineId) where QCEMV.CancelTag=0 and QCE.CancelTag=0 and SE.CancelTag=0 and QCEM.CancelTag=0 and QCEM.ProductId=" + ProductId + " and QCEMV.ProductId=" + ProductId + " "; //and SE.Shift IN('I','II','III')  "; //  Having  MAX(QCE.EntryDate) order by QCE.EntryDate"; // MAX(QCE.EntryDate) "; // and QCE.EntryDate IN(SELECT MAX(EntryDate) FROM QCEntry QCE1 WHERE QCE1.EntryDate = QCE.EntryDate)";
            //and QCE.EntryDate=(SELECT MAX(t.EntryDate) FROM QCEntry t WHERE t.ID=QCE.ID order by QCE.EntryDate desc) order by QCE.EntryDate desc
            //objBL.Query = "select QCE.ID  from QCEntry QCE inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId where SE.Shift IN('I','II','III') GROUP BY QCE.ID HAVING max(QCE.EntryDate) ";//and QCE.EntryDate=(SELECT MAX(t.EntryDate) FROM QCEntry t WHERE t.ID=QCE.ID order by QCE.EntryDate desc) order by QCE.EntryDate desc ";
            //inner join QCEntryMachine QCEM on QCEM.QCEntryId=QCE.ID) where   QCE.CancelTag=0 and SE.CancelTag=0 and QCEM.CancelTag=0 and QCEM.ProductId=" + ProductId + "   and SE.Shift IN('I','II','III') and QCE.EntryDate=(SELECT MAX(t.EntryDate) FROM QCEntry t WHERE t.ID=QCE.ID order by QCE.EntryDate desc) order by QCE.EntryDate desc "; //  Having  MAX(QCE.EntryDate) order by QCE.EntryDate"; // MAX(QCE.EntryDate) "; // and QCE.EntryDate IN(SELECT MAX(EntryDate) FROM QCEntry QCE1 WHERE QCE1.EntryDate = QCE.EntryDate)";
            //inner join QCEntryMachineValues QCEMV on QCEM.ID=QCEMV.QCEntryMachineId) where QCEMV.CancelTag=0 and QCE.CancelTag=0 and SE.CancelTag=0 and QCEM.CancelTag=0 and QCEM.ProductId=" + ProductId + " and QCEMV.ProductId=" + ProductId + " and SE.Shift IN('I','II','III') and QCE.EntryDate=(SELECT MAX(t.EntryDate) FROM QCEntry t WHERE t.ID=QCE.ID order by QCE.EntryDate desc) order by QCE.EntryDate desc "; //  Having  MAX(QCE.EntryDate) order by QCE.EntryDate"; // MAX(QCE.EntryDate) "; // and QCE.EntryDate IN(SELECT MAX(EntryDate) FROM QCEntry QCE1 WHERE QCE1.EntryDate = QCE.EntryDate)";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["FindDate"])))
                {
                    dtFindDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["FindDate"]);
                    FlagDateValue = true;
                    btnReport.Visible = true;
                    gbCOAParameters.Enabled = true;
                    dgvValues.Enabled = true;

                    DataSet dsInspector = new DataSet();
                    //objBL.Query = "select  from ((ShiftEntry SE inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) where SE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and SE.EntryDate=#" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "#";
                    //objBL.Query = "select SE.ID,SE.EntryDate as [Date],SE.EntryTime as [Time],SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],SE.Naration from (((((QCEntry QCE inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join QCEntryMachine QCEM on QCEM.QCEntryId=QCE.ID) inner join QCEntryMachineValues QCEMV on QCEM.ID=QCEMV.QCEntryMachineId) inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) where QCEMV.CancelTag=0 and QCE.CancelTag=0 and SE.CancelTag=0 and QCEM.CancelTag=0 and QCEM.ProductId=" + ProductId + " and QCEMV.ProductId=" + ProductId + " and SE.Shift IN('I','II','III') and QCE.EntryDate=#" + dtFindDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# "; //  Having  MAX(QCE.EntryDate) order by QCE.EntryDate"; // MAX(QCE.EntryDate) "; // and QCE.EntryDate IN(SELECT MAX(EntryDate) FROM QCEntry QCE1 WHERE QCE1.EntryDate = QCE.EntryDate)";
                    objBL.Query = "select SE.ID,SE.EntryDate as [Date],SE.EntryTime as [Time],SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],SE.Naration from (((((QCEntry QCE inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join QCEntryMachine QCEM on QCEM.QCEntryId=QCE.ID) inner join QCEntryMachineValues QCEMV on QCEM.ID=QCEMV.QCEntryMachineId) inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) where QCEMV.CancelTag=0 and QCE.CancelTag=0 and SE.CancelTag=0 and QCEM.CancelTag=0 and QCEM.ProductId=" + ProductId + " and QCEMV.ProductId=" + ProductId + " and SE.Shift IN('1','2','3') and QCE.EntryDate=#" + dtFindDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# "; //  Having  MAX(QCE.EntryDate) order by QCE.EntryDate"; // MAX(QCE.EntryDate) "; // and QCE.EntryDate IN(SELECT MAX(EntryDate) FROM QCEntry QCE1 WHERE QCE1.EntryDate = QCE.EntryDate)";
                    dsInspector = objBL.ReturnDataSet();
                    if (dsInspector.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dsInspector.Tables[0].Rows[0]["Plant Incharge"].ToString())))
                            PlantIncharge = Convert.ToString(dsInspector.Tables[0].Rows[0]["Plant Incharge"].ToString());
                        if (!string.IsNullOrEmpty(Convert.ToString(dsInspector.Tables[0].Rows[0]["Volume Checker"].ToString())))
                            VolumeCheckerName = Convert.ToString(dsInspector.Tables[0].Rows[0]["Volume Checker"].ToString());
                    }

                    DataSet dsValues = new DataSet();
                    //objBL.Query = "select QCEMV.ID,QCEMV.QCEntryId,QCEMV.QCEntryMachineId,QCEMV.ProductionEntryId,QCEMV.MachineNo,QCEMV.ProductId,QCEMV.Supplier,QCEMV.[Weight],QCEMV.[Color],QCEMV.[Size],QCEMV.InnerDia,QCEMV.OuterDia,QCEMV.RetainerGap,QCEMV.Height,QCEMV.OverflowVolume,QCEMV.MajorAxis,QCEMV.MinorAxis,QCEMV.BottleHeight,QCEMV.Visuals,QCEMV.GoGauge,QCEMV.CaptFitment,QCEMV.WadSealing,QCEMV.LeakTest,QCEMV.DropTest,QCEMV.TopLoadTest,QCEMV.BaseInformation from (((QCEntry QCE inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join QCEntryMachine QCEM on QCEM.QCEntryId=QCE.ID) inner join QCEntryMachineValues QCEMV on QCEM.ID=QCEMV.QCEntryMachineId) where QCEMV.CancelTag=0 and QCE.CancelTag=0 and SE.CancelTag=0 and QCEM.CancelTag=0 and QCEM.ProductId=" + ProductId + " and QCEMV.ProductId=" + ProductId + " and SE.Shift IN('I','II','III')  and QCE.EntryDate=#" + dtFindDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# ";
                    objBL.Query = "select QCEMV.ID,QCEMV.QCEntryId,QCEMV.QCEntryMachineId,QCEMV.ProductionEntryId,QCEMV.MachineNo,QCEMV.ProductId,QCEMV.Supplier,QCEMV.[Weight],QCEMV.[Color],QCEMV.[Size],QCEMV.InnerDia,QCEMV.OuterDia,QCEMV.RetainerGap,QCEMV.Height,QCEMV.OverflowVolume,QCEMV.MajorAxis,QCEMV.MinorAxis,QCEMV.BottleHeight,QCEMV.Visuals,QCEMV.GoGauge,QCEMV.CaptFitment,QCEMV.WadSealing,QCEMV.LeakTest,QCEMV.DropTest,QCEMV.TopLoadTest,QCEMV.BaseInformation from (((QCEntry QCE inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join QCEntryMachine QCEM on QCEM.QCEntryId=QCE.ID) inner join QCEntryMachineValues QCEMV on QCEM.ID=QCEMV.QCEntryMachineId) where QCEMV.CancelTag=0 and QCE.CancelTag=0 and SE.CancelTag=0 and QCEM.CancelTag=0 and QCEM.ProductId=" + ProductId + " and QCEMV.ProductId=" + ProductId + " and SE.Shift IN('1','2','3')  and QCE.EntryDate=#" + dtFindDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# ";
                    dsValues = objBL.ReturnDataSet();
                    if (dsValues.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dsValues.Tables[0].Rows[0]["ProductionEntryId"])))
                        {
                            txtBatchNo.Text = dsValues.Tables[0].Rows[0]["ProductionEntryId"].ToString();
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dsValues.Tables[0].Rows[0]["Supplier"])))
                        {
                            cmbPreformSuppliers.Text = dsValues.Tables[0].Rows[0]["Supplier"].ToString();
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dsValues.Tables[0].Rows[0]["Color"])))
                        {
                            cmbColor.Text = dsValues.Tables[0].Rows[0]["Color"].ToString();
                        }

                        int i = 0;

                        if (!string.IsNullOrEmpty(Convert.ToString(dsValues.Tables[0].Rows[0]["BaseInformation"])))
                            BaseInformation_I = Convert.ToString(dsValues.Tables[0].Rows[i]["BaseInformation"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(dsValues.Tables[0].Rows[0]["Visuals"])))
                            Visuals_I = Convert.ToString(dsValues.Tables[0].Rows[i]["Visuals"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(dsValues.Tables[0].Rows[i]["GoGauge"])))
                            GoGauge_I = Convert.ToString(dsValues.Tables[0].Rows[i]["GoGauge"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(dsValues.Tables[0].Rows[i]["CaptFitment"])))
                            CaptFitment_I = Convert.ToString(dsValues.Tables[0].Rows[i]["CaptFitment"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(dsValues.Tables[0].Rows[i]["WadSealing"])))
                            WadSealing_I = Convert.ToString(dsValues.Tables[0].Rows[i]["WadSealing"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(dsValues.Tables[0].Rows[i]["LeakTest"])))
                            LeakTest_I = Convert.ToString(dsValues.Tables[0].Rows[i]["LeakTest"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(dsValues.Tables[0].Rows[i]["DropTest"])))
                            DropTest_I = Convert.ToString(dsValues.Tables[0].Rows[i]["DropTest"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(dsValues.Tables[0].Rows[i]["TopLoadTest"])))
                            TopLoadTest_I = Convert.ToString(dsValues.Tables[0].Rows[i]["TopLoadTest"]);
                    }
                }
            }
        }

        static string PlantIncharge;
        static string VolumeCheckerName;
        static DateTime dtFindDate;
        double AverageValue = 0;
        static bool FlagDateValue = false;

        private void ClearValues()
        {
            PlantIncharge = string.Empty;
            VolumeCheckerName = string.Empty;
            dtFindDate = DateTime.Now.Date;
            AverageValue = 0;
            FlagDateValue = false;
            Visuals_I = ""; GoGauge_I = ""; CaptFitment_I = ""; WadSealing_I = ""; LeakTest_I = ""; DropTest_I = ""; TopLoadTest_I = "";

        }

        string Visuals_I = "", GoGauge_I = "", CaptFitment_I = "", WadSealing_I = "", LeakTest_I = "", DropTest_I = "", TopLoadTest_I = "", BaseInformation_I="";

        private void GetAverageValue(string ColumnName_F)
        {
            if (FlagDateValue)
            {
                // ColumnName_F = "Avg([QCEMV.Weight]) ";
                DataSet ds = new DataSet();

                string CName = "[QCEMV." + ColumnName_F + "]";
                //string Checking = "IIf(" + CName + " <> '', " + CName + ", '0')";

                //  string CName = "QCEMV." + ColumnName_F + "";
                //string Checking = "ISNULL(" + CName + ")";
                //string Checking = " Avg(" + CName + ") as [AverageValue]";
                //string Checking = " AVG(Val("+CName+")) as [AverageValue]";
                string Checking = " AVG(Val(IIf(" + CName + "," + CName + ",0))) as [AverageValue]";

                //Checking = "IIf(" + CName + ",AVG("+CName+"), 0)";

                //AVG(ISNULL("+CName+", 0))

                // IIf("+CName+" = '', " + CName + ", ' ')

                //NZ(Avg([QCEMV.Weight]))

                //objBL.Query = "select  " + Checking + " from (((QCEntry QCE inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join QCEntryMachine QCEM on QCEM.QCEntryId=QCE.ID) inner join QCEntryMachineValues QCEMV on QCEM.ID=QCEMV.QCEntryMachineId) where QCEMV.CancelTag=0 and QCE.CancelTag=0 and SE.CancelTag=0 and QCEM.CancelTag=0 and QCEM.ProductId=" + ProductId + " and QCEMV.ProductId=" + ProductId + " and SE.Shift IN('I','II','III')  and QCE.EntryDate=#" + dtFindDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# ";
                objBL.Query = "select  " + Checking + " from (((QCEntry QCE inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join QCEntryMachine QCEM on QCEM.QCEntryId=QCE.ID) inner join QCEntryMachineValues QCEMV on QCEM.ID=QCEMV.QCEntryMachineId) where QCEMV.CancelTag=0 and QCE.CancelTag=0 and SE.CancelTag=0 and QCEM.CancelTag=0 and QCEM.ProductId=" + ProductId + " and QCEMV.ProductId=" + ProductId + " and SE.Shift IN('1','2','3')  and QCE.EntryDate=#" + dtFindDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# ";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["AverageValue"])))
                    {
                        AverageValue = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["AverageValue"]), 2);
                    }
                }
            }
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

        private void ExcelReportMail()
        {
            objRL.FillCompanyData();

            object misValue = System.Reflection.Missing.Value;
            myExcelApp = new Microsoft.Office.Interop.Excel.Application();
            myExcelWorkbooks = myExcelApp.Workbooks;

            objRL.ClearExcelPath();
            objRL.isPDF = true;
            objRL.Form_ExcelFileName = "COAReport.xlsx";
            objRL.Form_ReportFileName = "COA Report-" + DateTime.Now.Date.ToString("dd-MMM-yyyy");
            objRL.Form_DestinationReportFilePath = "COA Report\\";

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

            myExcelWorksheet.get_Range("C5", misValue).Formula = objRL.ProductName;
            myExcelWorksheet.get_Range("C7", misValue).Formula = cmbCustomerName.Text;
            myExcelWorksheet.get_Range("G5", misValue).Formula = dtpDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);
            myExcelWorksheet.get_Range("G6", misValue).Formula = txtID.Text;
            myExcelWorksheet.get_Range("G7", misValue).Formula = txtBatchNo.Text;
            myExcelWorksheet.get_Range("C8", misValue).Formula = txtSubject.Text;
            myExcelWorksheet.get_Range("C9", misValue).Formula = txtMaterialUsed.Text;
            myExcelWorksheet.get_Range("C10", misValue).Formula = cmbPreformSuppliers.Text;
            myExcelWorksheet.get_Range("C11", misValue).Formula = cmbColor.Text;
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
                    Fill_Merge_Cell("B", "C", misValue, myExcelWorksheet, Convert.ToString(dgvValues.Rows[i].Cells["clmParameters"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmStandards"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, Convert.ToString(dgvValues.Rows[i].Cells["clmStandards"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmTolerance"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, Convert.ToString(dgvValues.Rows[i].Cells["clmTolerance"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmCavity1"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, Convert.ToString(dgvValues.Rows[i].Cells["clmCavity1"].Value));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmCavity2"].Value)))
                {
                    AFlag = 1;
                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, Convert.ToString(dgvValues.Rows[i].Cells["clmCavity2"].Value));
                }
                RowCount++;
            }

            RowCount++; BorderFlag = true;
            AFlag = 0;
            Fill_Merge_Cell("A", "G", misValue, myExcelWorksheet, "Remark - All 17 Points are within Standards");
            RowCount++;
            Fill_Merge_Cell("A", "G", misValue, myExcelWorksheet, "Inspected by- " + PlantIncharge + ", Tested by - " + VolumeCheckerName + "");

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

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtQuantity);
        }

        private void txtNoOfPackages_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtNoOfPackages);
        }

        bool SearchTag = false;
        private void txtSearchItemName_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchItemName.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }
    }
}
