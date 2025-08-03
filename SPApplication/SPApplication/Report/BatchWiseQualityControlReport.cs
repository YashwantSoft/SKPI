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
using SPApplication.Transaction;
using SPApplication.Report.RDLCReport;
namespace SPApplication.Report
{
    public partial class BatchWiseQualityControlReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        ErrorProvider objEP = new ErrorProvider();
        DesignLayer objDL = new DesignLayer();

        bool FlagToday = false;
        string QueryParameter = string.Empty;

        int TableID = 0;
        bool BorderFlag = false; bool CellFlag = false;
        string PDFReport = string.Empty;
        int AFlag = 0; int SrNo = 1;
        bool AlignFlag = false; bool boldflag = false;
        int RowCount = 18;
        bool MH_Value = false;
        string ReportName = string.Empty;
        bool ColourFlag = false;
        string ConcatNames = string.Empty;

        Excel.Application myExcelApp;
        Excel.Workbooks myExcelWorkbooks;
        Excel.Workbook myExcelWorkbook;

        public BatchWiseQualityControlReport()
        {
            InitializeComponent();
            objDL.Set_Report_Design(this, lblHeader, btnView, btnReport, btnClear, btnExit, BusinessResources.LBL_REPORT_BATCHWISEQUALITYCONTROLREPORT);
            lblBottleInformation.BackColor = Color.Cyan;
            lblJarInformation.BackColor = Color.Yellow;
        }

        private void QualityReportBatchWise_Load(object sender, EventArgs e)
        {
            ClearAll();
            cmbSearchBy.Text = "Batch Wise";
            View_Search_GroupBox();
            txtBatchNumber.Select();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            objRL.BatchNumber = CreatedDate; // lblDetails.Text;
            ReportViewRDLC objForm = new ReportViewRDLC(dsSend,BusinessResources.Report_BatchWiseQualityControlReport);
            objForm.ShowDialog(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewReport();
        }

        int PlantInchargeId = 0, VolumeCheckerId = 0;

        string MainQuery = string.Empty, PreformParty=string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;
        bool DateFlag = false;
        bool SearchTag = false;
        bool IDFlag = false;
        int ProductId = 0, QCEntryMachineValuesId=0,Result=0;
        
        private void Get_Product_Information()
        {
            if (ProductId != 0)
            {
                objRL.Get_Product_Records_By_Id(ProductId);

                if (!string.IsNullOrEmpty(objRL.ProductType))
                {
                    if (objRL.ProductType == "Bottle")
                        lblProductName.BackColor = Color.Cyan;
                    else
                        lblProductName.BackColor = Color.Yellow;
                }
                btnToleranceValue.Enabled = true;
            }
        }
        string CreatedDate = string.Empty;
        string Shift_D = string.Empty, MachineNumber_D = string.Empty, Supplier_D = string.Empty, PlantIncharge_D = string.Empty, VolumeChecker_D = string.Empty,ConcatDetails = string.Empty;
        private void ViewReport()
        {
            if (txtBatchNumber.Text != "" || ProductId >0)
            {
                SrNo = 1;
                lblProductName.Text = "";
                lblTotalCount.Text = "";
                lblDetails.Text = "";
                //dgvValues.DataSource = null;
                MainQuery = string.Empty;
                WhereClause = string.Empty;
                OrderByClause = string.Empty;
                UserClause = string.Empty;
                dgvValues.Rows.Clear();
                CreatedDate = string.Empty;
                DataSet ds = new DataSet();
                MainQuery = "select QCEMV.ID,SE1.Shift,QCE1.ShiftEntryId,QCEMV.QCEntryId,QCEMV.QCEntryMachineId,QCEMV.ProductionEntryId,QCEMV.MachineNo,QCEM.ProductId,P.ProductName as [Product Name],QCEMV.Supplier,[QCEMV.Weight] as [Weight],[QCEMV.Color] as [Color],[QCEMV.Size] as [Size],QCEMV.InnerDia,QCEMV.OuterDia,QCEMV.RetainerGap,[QCEMV.Height] as [Height],QCEMV.OverflowVolume,QCEMV.MajorAxis,QCEMV.MinorAxis,QCEMV.BottleHeight,QCEMV.Visuals,QCEMV.GoGauge,QCEMV.CaptFitment,QCEMV.WadSealing,QCEMV.LeakTest,QCEMV.DropTest,QCEMV.TopLoadTest,E.FullName as [PlantIncharge],E1.FullName as [VolumeChecker],QCEMV.WeightR,QCEMV.SizeR,QCEMV.InnerDiaR,QCEMV.OuterDiaR,QCEMV.RetainerGapR,QCEMV.HeightR,QCEMV.OverflowVolumeR,QCEMV.MajorAxisR,QCEMV.MinorAxisR,QCEMV.BottleHeightR,QCEMV.VisualsR,QCEMV.GoGaugeR,QCEMV.CaptFitmentR,QCEMV.WadSealingR,QCEMV.LeakTestR,QCEMV.DropTestR,QCEMV.TopLoadTestR,QCEMV.CreatedDate from (((((((QCEntryMachineValues QCEMV inner join QCEntryMachine QCEM on QCEM.ID=QCEMV.QCEntryMachineId) inner join QCEntry QCE1 on QCE1.ID=QCEM.QCEntryId) inner join ProductionEntry PE1 on PE1.ID=QCEM.ProductionEntryId) inner join ShiftEntry SE1 on SE1.ID=QCE1.ShiftEntryId) inner join Employee E on E.ID=SE1.PlantInchargeId) inner join Employee E1 on E1.ID=SE1.VolumeCheckerId) inner join Product P on P.ID=QCEM.ProductId) where P.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and QCEMV.CancelTag=0 and QCE1.CancelTag=0 and SE1.CancelTag=0 and QCEM.CancelTag=0 and PE1.CancelTag=0";
                OrderByClause = " order by QCE1.EntryDate asc";

                //WhereClause = " and PE1.EntryDate between 
                //WhereClause += " and PE1.EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";

                if (txtBatchNumber.Text != "")
                    WhereClause += " and PE1.StickerHeader like '%" + txtBatchNumber.Text + "%'";
                else
                    WhereClause += " and QCEM.ProductId=" + ProductId + " ";
                    

                objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnReport.Visible = true;
                    Shift_D = string.Empty; MachineNumber_D = string.Empty; Supplier_D = string.Empty; PlantIncharge_D = string.Empty; VolumeChecker_D = string.Empty; ConcatDetails = string.Empty;
                    
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Product Name"].ToString())))
                    {
                        ProductId = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"].ToString()));
                        //ConcatDetails += "Product Name-" + Convert.ToString(ds.Tables[0].Rows[0]["Product Name"].ToString()) + System.Environment.NewLine;
                        lblProductName.Text = Convert.ToString(ds.Tables[0].Rows[0]["Product Name"].ToString());
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CreatedDate"].ToString())))
                    {
                        //ProductId = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["CreatedDate"].ToString()));
                        //ConcatDetails += "Product Name-" + Convert.ToString(ds.Tables[0].Rows[0]["Product Name"].ToString()) + System.Environment.NewLine;
                        CreatedDate = "Created Date-" + Convert.ToString(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                        ConcatDetails = "Created Date-" + Convert.ToString(ds.Tables[0].Rows[0]["CreatedDate"].ToString()) + System.Environment.NewLine; 
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Shift"].ToString())))
                    {
                        Shift_D = Convert.ToString(ds.Tables[0].Rows[0]["Shift"].ToString());
                        ConcatDetails += "Shift-" + Shift_D + System.Environment.NewLine;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["MachineNo"].ToString())))
                    {
                        MachineNumber_D = Convert.ToString(ds.Tables[0].Rows[0]["MachineNo"].ToString());
                        ConcatDetails += "Machine Number-" + MachineNumber_D + System.Environment.NewLine;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Supplier"].ToString())))
                    {
                        Supplier_D = Convert.ToString(ds.Tables[0].Rows[0]["Supplier"].ToString());
                        ConcatDetails += "Supplier-" + Supplier_D + System.Environment.NewLine;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PlantIncharge"].ToString())))
                    {
                        PlantIncharge_D = Convert.ToString(ds.Tables[0].Rows[0]["PlantIncharge"].ToString());
                        ConcatDetails += "Plant Incharge-" + PlantIncharge_D + System.Environment.NewLine;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VolumeChecker"].ToString())))
                    {
                        VolumeChecker_D = Convert.ToString(ds.Tables[0].Rows[0]["VolumeChecker"].ToString());
                        ConcatDetails += "Volume Checker-" + VolumeChecker_D + System.Environment.NewLine;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(txtBatchNumber.Text)))
                    {
                        //VolumeChecker_D = Convert.ToString(ds.Tables[0].Rows[0]["VolumeChecker"].ToString());
                        ConcatDetails += "Batch Number-" + txtBatchNumber.Text;
                    }


                    lblDetails.Text = ConcatDetails.ToString();
                    lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                     
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"])))
                    {
                        ProductId = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"]));
                        Get_Product_Information();
                    }

                    string Supplier = string.Empty;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //Supplier_I = Convert.ToString(ds.Tables[0].Rows[i]["Supplier"]);
                        Supplier = string.Empty; Weight_R = 0; Size_R = 0; InnerDia_R = 0; OuterDia_R = 0; RetainerGap_R = 0; Height_R = 0; OverflowVolume_R = 0; MajorAxis_R = 0; MinorAxis_R = 0; BottleHeight_R = 0; Visuals_R = 0; GoGauge_R = 0; CaptFitment_R = 0; WadSealing_R = 0; LeakTest_R = 0; DropTest_R = 0; TopLoadTest_R = 0;

                        Supplier = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ID"])))
                            QCEntryMachineValuesId = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Weight"])))
                            Weight_I = Convert.ToString(ds.Tables[0].Rows[i]["Weight"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Color"])))
                            Color_I = Convert.ToString(ds.Tables[0].Rows[i]["Color"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Size"])))
                            Size_I = Convert.ToString(ds.Tables[0].Rows[i]["Size"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["InnerDia"])))
                            InnerDia_I = Convert.ToString(ds.Tables[0].Rows[i]["InnerDia"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["OuterDia"])))
                            OuterDia_I = Convert.ToString(ds.Tables[0].Rows[i]["OuterDia"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["RetainerGap"])))
                            RetainerGap_I = Convert.ToString(ds.Tables[0].Rows[i]["RetainerGap"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Height"])))
                            Height_I = Convert.ToString(ds.Tables[0].Rows[i]["Height"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["OverflowVolume"])))
                            OverflowVolume_I = Convert.ToString(ds.Tables[0].Rows[i]["OverflowVolume"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["MajorAxis"])))
                            MajorAxis_I = Convert.ToString(ds.Tables[0].Rows[i]["MajorAxis"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["MinorAxis"])))
                            MinorAxis_I = Convert.ToString(ds.Tables[0].Rows[i]["MinorAxis"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["BottleHeight"])))
                            BottleHeight_I = Convert.ToString(ds.Tables[0].Rows[i]["BottleHeight"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Visuals"])))
                            Visuals_I = Convert.ToString(ds.Tables[0].Rows[i]["Visuals"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["GoGauge"])))
                            GoGauge_I = Convert.ToString(ds.Tables[0].Rows[i]["GoGauge"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["CaptFitment"])))
                            CaptFitment_I = Convert.ToString(ds.Tables[0].Rows[i]["CaptFitment"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["WadSealing"])))
                            WadSealing_I = Convert.ToString(ds.Tables[0].Rows[i]["WadSealing"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["LeakTest"])))
                            LeakTest_I = Convert.ToString(ds.Tables[0].Rows[i]["LeakTest"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["DropTest"])))
                            DropTest_I = Convert.ToString(ds.Tables[0].Rows[i]["DropTest"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["TopLoadTest"])))
                            TopLoadTest_I = Convert.ToString(ds.Tables[0].Rows[i]["TopLoadTest"]);

                        dgvValues.Rows.Add();

                        dgvValues.Rows[i].Cells["clmSrNo"].Value = SrNo.ToString();
                        dgvValues.Rows[i].Cells["clmSupplier"].Value = Supplier_D;

                        if (!string.IsNullOrEmpty(Convert.ToString(Weight_I)))
                        {
                           // Weight_R = 1;
                            dgvValues.Rows[i].Cells["clmWeight"].Value = Weight_I;
                            CheckTollarance(2, Convert.ToDouble(Weight_I));
                            if (ResultValue)
                            {
                                dgvValues.Rows[i].Cells["clmWeight"].Style.BackColor = Color.Red;
                                Weight_R = 1;
                            }
                            //else
                            //    dgvValues.Rows[i].Cells["clmWeight"].Style.BackColor = Color.White;
                        }

                        dgvValues.Rows[i].Cells["clmColor"].Value = Color_I;

                        if (!string.IsNullOrEmpty(Convert.ToString(Size_I)))
                        {
                            dgvValues.Rows[i].Cells["clmSize"].Value = Size_I;
                            CheckTollarance(4, Convert.ToDouble(Size_I));
                            if (ResultValue)
                            {
                                dgvValues.Rows[i].Cells["clmSize"].Style.BackColor = Color.Red;
                                Size_R = 1;
                            }

                            //else
                            //    dgvValues.Rows[i].Cells["clmSize"].Style.BackColor = Color.White;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(InnerDia_I)))
                        {
                            dgvValues.Rows[i].Cells["clmInnerDia"].Value = InnerDia_I;
                            CheckTollarance(5, Convert.ToDouble(InnerDia_I));
                            if (ResultValue)
                            {
                                dgvValues.Rows[i].Cells["clmInnerDia"].Style.BackColor = Color.Red;
                                InnerDia_R = 1;
                            }
                            //else
                            //    dgvValues.Rows[i].Cells["clmInnerDia"].Style.BackColor = Color.White;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(OuterDia_I)))
                        {
                            dgvValues.Rows[i].Cells["clmOuterDia"].Value = OuterDia_I;
                            CheckTollarance(6, Convert.ToDouble(OuterDia_I));
                            if (ResultValue)
                            {
                                dgvValues.Rows[i].Cells["clmOuterDia"].Style.BackColor = Color.Red;
                                OuterDia_R = 1;
                            }
                            //else
                            //    dgvValues.Rows[i].Cells["clmOuterDia"].Style.BackColor = Color.White;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(RetainerGap_I)))
                        {
                            dgvValues.Rows[i].Cells["clmRetainerGap"].Value = RetainerGap_I;
                            CheckTollarance(7, Convert.ToDouble(RetainerGap_I));
                            if (ResultValue)
                            {
                                dgvValues.Rows[i].Cells["clmRetainerGap"].Style.BackColor = Color.Red;
                                RetainerGap_R = 1;
                            }
                            //else
                            //    dgvValues.Rows[i].Cells["clmRetainerGap"].Style.BackColor = Color.White;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(Height_I)))
                        {
                            dgvValues.Rows[i].Cells["clmHeight"].Value = Height_I;
                            CheckTollarance(8, Convert.ToDouble(Height_I));
                            if (ResultValue)
                            {
                                dgvValues.Rows[i].Cells["clmHeight"].Style.BackColor = Color.Red;
                                Height_R = 1;
                            }
                            //else
                            //    dgvValues.Rows[i].Cells["clmHeight"].Style.BackColor = Color.White;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(OverflowVolume_I)))
                        {
                            dgvValues.Rows[i].Cells["clmOverflowVolume"].Value = OverflowVolume_I;
                            CheckTollarance(9, Convert.ToDouble(OverflowVolume_I));
                            if (ResultValue)
                            {
                                dgvValues.Rows[i].Cells["clmOverflowVolume"].Style.BackColor = Color.Red;
                                OverflowVolume_R = 1;
                            }
                            //else
                            //    dgvValues.Rows[i].Cells["clmOverflowVolume"].Style.BackColor = Color.White;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(MajorAxis_I)))
                        {
                            dgvValues.Rows[i].Cells["clmMajorAxis"].Value = MajorAxis_I;
                            CheckTollarance(10, Convert.ToDouble(MajorAxis_I));
                            if (ResultValue)
                            {
                                dgvValues.Rows[i].Cells["clmMajorAxis"].Style.BackColor = Color.Red;
                                MajorAxis_R = 1;
                            }
                            //else
                            //    dgvValues.Rows[i].Cells["clmMajorAxis"].Style.BackColor = Color.White;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(MinorAxis_I)))
                        {
                            dgvValues.Rows[i].Cells["clmMinorAxis"].Value = MinorAxis_I;
                            CheckTollarance(11, Convert.ToDouble(MinorAxis_I));
                            if (ResultValue)
                            {
                                dgvValues.Rows[i].Cells["clmMinorAxis"].Style.BackColor = Color.Red;
                                MinorAxis_R = 1;
                            }
                            //else
                            //    dgvValues.Rows[i].Cells["clmMinorAxis"].Style.BackColor = Color.White;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(BottleHeight_I)))
                        {
                            dgvValues.Rows[i].Cells["clmBottleHeight"].Value = BottleHeight_I;
                            CheckTollarance(12, Convert.ToDouble(BottleHeight_I));
                            if (ResultValue)
                            {
                                dgvValues.Rows[i].Cells["clmBottleHeight"].Style.BackColor = Color.Red;
                                BottleHeight_R = 1;
                            }
                            //else
                            //    dgvValues.Rows[i].Cells["clmBottleHeight"].Style.BackColor = Color.White;
                        }

                        dgvValues.Rows[i].Cells["clmVisuals"].Value = Visuals_I;
                        dgvValues.Rows[i].Cells["clmGoGauge"].Value = GoGauge_I;
                        dgvValues.Rows[i].Cells["clmCapFitment"].Value = CaptFitment_I;
                        dgvValues.Rows[i].Cells["clmWadSealing"].Value = WadSealing_I;
                        dgvValues.Rows[i].Cells["clmLeakTest"].Value = LeakTest_I;
                        dgvValues.Rows[i].Cells["clmDropTest"].Value = DropTest_I;
                        dgvValues.Rows[i].Cells["clmTopLoadTest"].Value = TopLoadTest_I;

                        Visuals_R = SetOK_DropDown(Visuals_I);
                        if(Visuals_R==1)
                            dgvValues.Rows[i].Cells["clmVisuals"].Style.BackColor = Color.Red;

                        GoGauge_R = SetOK_DropDown(GoGauge_I);
                        if (GoGauge_R == 1)
                            dgvValues.Rows[i].Cells["clmGoGauge"].Style.BackColor = Color.Red;

                        CaptFitment_R = SetOK_DropDown(CaptFitment_I);
                        if (CaptFitment_R == 1)
                            dgvValues.Rows[i].Cells["clmCapFitment"].Style.BackColor = Color.Red;

                        WadSealing_R = SetOK_DropDown(WadSealing_I);
                        if (WadSealing_R == 1)
                            dgvValues.Rows[i].Cells["clmWadSealing"].Style.BackColor = Color.Red;

                        LeakTest_R = SetOK_DropDown(LeakTest_I);
                        if (LeakTest_R == 1)
                            dgvValues.Rows[i].Cells["clmLeakTest"].Style.BackColor = Color.Red;

                        DropTest_R = SetOK_DropDown(DropTest_I);
                        if (DropTest_R == 1)
                            dgvValues.Rows[i].Cells["clmDropTest"].Style.BackColor = Color.Red;

                        TopLoadTest_R = SetOK_DropDown(TopLoadTest_I);
                        if (TopLoadTest_R == 1)
                            dgvValues.Rows[i].Cells["clmTopLoadTest"].Style.BackColor = Color.Red;

                        //19439

                        RedundancyLogics.ReportBatchNumber = txtBatchNumber.Text;
                        RedundancyLogics.ReportDetails = ConcatDetails;

                        dsSend = ds;
                        if (QCEntryMachineValuesId != 0)
                        {
                            objBL.Query = "update QCEntryMachineValues set WeightR=" + Weight_R + ",SizeR=" + Size_R + ",InnerDiaR=" + InnerDia_R + ",OuterDiaR=" + OuterDia_R + ",RetainerGapR=" + RetainerGap_R + ",HeightR=" + Height_R + ",OverflowVolumeR=" + OverflowVolume_R + ",MajorAxisR=" + MajorAxis_R + ",MinorAxisR=" + MinorAxis_R + ",BottleHeightR=" + BottleHeight_R + ",VisualsR=" + Visuals_R + ",GoGaugeR=" + GoGauge_R + ",CaptFitmentR=" + CaptFitment_R + ",WadSealingR=" + WadSealing_R + ",LeakTestR=" + LeakTest_R + ",DropTestR=" + DropTest_R + ",TopLoadTestR=" + TopLoadTest_R + " where CancelTag=0 and ID=" + QCEntryMachineValuesId + "";
                            Result = objBL.Function_ExecuteNonQuery();
                        }
                        SrNo++;
                        //UpdateQuery
                    }

                    ////0 QCEMV.ID,
                    ////1 SE1.Shift,
                    ////2 QCE1.ShiftEntryId,
                    ////3 QCEMV.QCEntryId,
                    ////4 QCEMV.QCEntryMachineId,
                    ////5 QCEMV.ProductionEntryId,
                    ////6 QCEMV.MachineNo,
                    ////7 QCEM.ProductId,
                    ////8 P.ProductName as [Product Name],
                    ////9 QCEMV.Supplier,
                    ////10 [QCEMV.Weight],
                    ////11 [QCEMV.Color],
                    ////12 [QCEMV.Size],
                    ////13 QCEMV.InnerDia as [Inner Dia],
                    ////14 QCEMV.OuterDia as [Outer Dia],
                    ////15 QCEMV.RetainerGap as [Retainer Gap],
                    ////16 [QCEMV.Height],
                    ////17 QCEMV.OverflowVolume as [Overflow Volume],
                    ////18 QCEMV.MajorAxis as [Major Axis],
                    ////19 QCEMV.MinorAxis as [Minor Axis],
                    ////20 QCEMV.BottleHeight as [Bottle Height],
                    ////21 QCEMV.Visuals,
                    ////22 QCEMV.GoGauge as [Go Gauge],
                    ////23 QCEMV.CaptFitment as [Cap Fitment],
                    ////24 QCEMV.WadSealing as [Wad Sealing],
                    ////25 QCEMV.LeakTest as [Leak Test],
                    ////26 QCEMV.DropTest as [Drop Test],
                    ////27 QCEMV.TopLoadTest as [Top Load Test],
                    ////28 E.FullName as [PlantIncharge],
                    ////29 E1.FullName as [VolumeChecker]

                    //dgvValues.DataSource = ds.Tables[0];
                    //

                    //dgvValues.Columns[0].Visible = false;
                    //dgvValues.Columns[1].Visible = false;
                    //dgvValues.Columns[2].Visible = false;
                    //dgvValues.Columns[3].Visible = false;
                    //dgvValues.Columns[4].Visible = false;
                    //dgvValues.Columns[5].Visible = false;
                    //dgvValues.Columns[6].Visible = false;
                    //dgvValues.Columns[7].Visible = false;
                    //dgvValues.Columns[8].Visible = false;
                    //dgvValues.Columns[9].Visible = false;
                    //dgvValues.Columns[28].Visible = false;
                    //dgvValues.Columns[29].Visible = false;

                    //for (int i = 0; i < dgvValues.Columns.Count; i++)
                    //{
                    //    dgvValues.Columns[i].Width = 130;
                    //}

                    //btnReport.Visible = true;
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        DataSet dsSend = new DataSet();

        private int SetOK_DropDown(string Value1)
        {
            int SetOK = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(Value1)))
            {
                if (Value1 != "Ok")
                    SetOK = 1;
            }

            return SetOK;
        }

        string Supplier_I = "", Weight_I = "", Color_I = "", Size_I = "", InnerDia_I = "", OuterDia_I = "", RetainerGap_I = "", Height_I = "", OverflowVolume_I = "", MajorAxis_I = "", MinorAxis_I = "", BottleHeight_I = "", Visuals_I = "", GoGauge_I = "", CaptFitment_I = "", WadSealing_I = "", LeakTest_I = "", DropTest_I = "", TopLoadTest_I = "";
        int Weight_R = 0, Size_R = 0, InnerDia_R = 0, OuterDia_R = 0, RetainerGap_R = 0, Height_R = 0, OverflowVolume_R = 0, MajorAxis_R = 0, MinorAxis_R = 0, BottleHeight_R = 0, Visuals_R = 0, GoGauge_R = 0, CaptFitment_R = 0, WadSealing_R = 0, LeakTest_R = 0, DropTest_R = 0, TopLoadTest_R = 0;
        //private void Get_QCEntryMachineValues()
        //{
        //    //Clear_Product_GridValues();

        //    DataSet ds = new DataSet();
        //    objBL.Query = "select ID,QCEntryId,QCEntryMachineId,ProductionEntryId,MachineNo,ProductId,Supplier,[Weight],[Color],[Size],InnerDia,OuterDia,RetainerGap,Height,OverflowVolume,MajorAxis,MinorAxis,BottleHeight,Visuals,GoGauge,CaptFitment,WadSealing,LeakTest,DropTest,TopLoadTest from QCEntryMachineValues where CancelTag=0 and QCEntryId=" + QCEntryId + " and MachineNo=" + MachineNo + " and QCEntryMachineId=" + QCEntryMachineId + " and ProductionEntryId=" + ProductionEntryId + " order by ID asc";
        //    // objBL.Query = "select ID,QCEntryId,QCEntryMachineId,ProductionEntryId,MachineNo,ProductId,Supplier,[Weight],[Color],[Size],InnerDia,OuterDia,RetainerGap,Height,OverflowVolume,MajorAxis,MinorAxis,BottleHeight,Visuals,GoGauge,CaptFitment,WadSealing,LeakTest,DropTest,TopLoadTest from QCEntryMachineValues where CancelTag=0 and QCEntryId=" + QCEntryId + " and MachineNo=" + MachineNo + " and QCEntryMachineId=" + QCEntryMachineId + " and ProductionEntryId=" + ProductionEntryId + " order by ID asc";
        //    ds = objBL.ReturnDataSet();

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        int PID = 0;// SwitchFlag = 0;

        //        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"])))
        //        {
        //            PID = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"]));

                   

        //        }
        //    }
        //}

        public void CheckTollarance(int ColumnIndex, double ColumnValue)
        {
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

                case 2: //ProductWeight   Datagridviewcolumn- //02 Weight
                    SetRemark(ColumnValue.ToString(), objRL.ProductWeightMinValue, objRL.ProductWeightMaxValue);
                    break;
                case 4: //ProductNeckSize Datagridviewcolumn- //04 Size
                    SetRemark(ColumnValue.ToString(), objRL.ProductNeckSizeMinValue, objRL.ProductNeckSizeMaxValue);
                    break;
                case 5: //ProductNeckID    Datagridviewcolumn- //05 Inner Dia
                    SetRemark(ColumnValue.ToString(), objRL.ProductNeckIDMinValue, objRL.ProductNeckIDMaxValue);
                    break;
                case 6: //ProductNeckOD Datagridviewcolumn- //06 Outer Dia
                    SetRemark(ColumnValue.ToString(), objRL.ProductNeckODMinValue, objRL.ProductNeckODMaxValue);
                    break;
                case 7: //ProductNeckCollarGap Datagridviewcolumn-   //7 Retainer Gap
                    SetRemark(ColumnValue.ToString(), objRL.ProductNeckCollarGapMinValue, objRL.ProductNeckCollarGapMaxValue);
                    break;
                case 8: //ProductNeckHeight Datagridviewcolumn-   //8 Height
                    SetRemark(ColumnValue.ToString(), objRL.ProductNeckHeightMinValue, objRL.ProductNeckHeightMaxValue);
                    break;
                case 9: //ProductVolume Datagridviewcolumn-   //9 Overflow Volume
                    SetRemark(ColumnValue.ToString(), objRL.ProductVolumeMinValue, objRL.ProductVolumeMaxValue);
                    break;
                case 10: //ProductMajorAxis Datagridviewcolumn-   //10 Major Axis
                    SetRemark(ColumnValue.ToString(), objRL.ProductMajorAxisMinValue, objRL.ProductMajorAxisMaxValue);
                    break;
                case 11: //ProductMinorAxis Datagridviewcolumn-   //11 Minor Axis
                    SetRemark(ColumnValue.ToString(), objRL.ProductMinorAxisMinValue, objRL.ProductMinorAxisMaxValue);
                    break;
                case 12: //ProductHeight   Datagridviewcolumn-   //12 Bottle Height
                    SetRemark(ColumnValue.ToString(), objRL.ProductHeightMinValue, objRL.ProductHeightMaxValue);
                    break;
            }
        }

        private void SetRemark(string CheckerValue_F, string MinValue_F, string MaxValue_F)
        {
            NullValueFlag = false; ResultValue = false;
            checkerValue = 0; MinValue = 0; MaxValue = 0;

            double.TryParse(CheckerValue_F, out checkerValue);
            double.TryParse(MinValue_F, out MinValue);
            double.TryParse(MaxValue_F, out MaxValue);

            //0-Correct
            //1-Wrong


            //Remark_F.Text = "";
            //Remark_F.BackColor = objDL.GetBackgroundBlank();

            if (MinValue > 0 && MaxValue > 0)
            {
                if (checkerValue != 0)
                {
                    //if (Enumerable.Range(MinValue, MaxValue).Contains(checkerValue))
                    if (MinValue <= checkerValue && MaxValue >= checkerValue)
                    {
                        ResultValue = false;
                        //Remark_F.BackColor = objDL.GetBackgroundColorSuccess();
                        //Remark_F.Text = "0";
                    }
                    else
                    {
                        ResultValue = true;
                        //Remark_F.BackColor = objDL.GetForeColorError();
                        //Remark_F.Text = "1";
                    }
                }
                else
                {
                    ResultValue = false;
                    NullValueFlag = true;
                }
            }
            else
                NullValueFlag = true;
        }

        bool NullValueFlag = false;

        double checkerValue = 0, MinValue = 0, MaxValue = 0;
        bool FlagProduct = false;

        bool ResultValue = false;


        private void SetRedCellColour(DataGridView dgv, int i, int CheckCell, int SetCell)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells[CheckCell].Value)))
            {
                if (dgvValues.Rows[i].Cells[CheckCell].Value.ToString() == "1")
                    dgvValues.Rows[i].Cells[SetCell].Style.BackColor = Color.Red;
            }
        }

        //objBL.Query = "select ID,EntryDate,EntryTime,ShiftEntryId from QCEntry where CancelTag=0 and ShiftEntryId=" + ShiftEntryId + "";
        //objBL.Query = "select ID,QCEntryId,ProductionEntryId,MachineNo,ProductId,SwitchFlag,Reason,ReasonInDetails from QCEntryMachine where CancelTag=0 and QCEntryId=" + QCEntryId + " and MachineNo=" + MachineNo + " order by ID desc";
        // objBL.Query = "select ID,QCEntryId,QCEntryMachineId,ProductionEntryId,MachineNo,ProductId,Supplier,[Weight],[Color],[Size],InnerDia,OuterDia,RetainerGap,Height,OverflowVolume,MajorAxis,MinorAxis,BottleHeight,Visuals,GoGauge,CaptFitment,WadSealing,LeakTest,DropTest,TopLoadTest from QCEntryMachineValues where CancelTag=0 and QCEntryId=" + QCEntryId + " and MachineNo=" + MachineNo + " and QCEntryMachineId=" + QCEntryMachineId + " and ProductionEntryId=" + ProductionEntryId + " order by ID asc";

        string Shift = string.Empty;

        private void ClearAll()
        {
            Shift = string.Empty; PlantInchargeId = 0; VolumeCheckerId = 0;
            lblDetails.Text = "";
            txtBatchNumber.Text = "";
            ClearAll_Item();
            ProductId = 0;
            btnReport.Visible = false;
            dgvValues.Rows.Clear();
        }

        private void btnToleranceValue_Click(object sender, EventArgs e)
        {
            if (ProductId != 0)
            {
                Tolerance objForm = new Tolerance(ProductId);
                objForm.ShowDialog(this);
            }
        }

        private void txtSearchProductName_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Item();
            if (txtSearchProductName.Text != "")
            {
                lbItem.Visible = true;
                objRL.Fill_Item_ListBox_OEE(lbItem, txtSearchProductName.Text, "Text");
            }
            else
            {
                lbItem.Visible = true;
                objRL.Fill_Item_ListBox_OEE(lbItem, txtSearchProductName.Text, "All");
            }
        }

        private void ClearAll_Item()
        {
            ProductId = 0;
            //lblProductType.Text = "";
            lblProductName.Text = "";
        }

        private void cmbSearchBy_SelectionChangeCommitted(object sender, EventArgs e)
        {
            View_Search_GroupBox();
        }

        private void View_Search_GroupBox()
        {
            if (cmbSearchBy.SelectedIndex > -1)
            {
                if (cmbSearchBy.Text == "Batch Wise")
                {
                    gbBatchNumber.Visible = true;
                    gbProductInformation.Visible = false;
                    ClearAll_Item();
                }
                else
                {
                    gbProductInformation.Visible = true;
                    gbBatchNumber.Visible = false;
                }
            }
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

        private void lbItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Fill_Product_Information();
            }
        }

        private void lbItem_Click(object sender, EventArgs e)
        {
            Fill_Product_Information();
        }

        string ProductDetails = string.Empty;
        string Standard = string.Empty;

        string QTYDisplay = string.Empty;
        string IdealRunRateDisplay = string.Empty;
         

        private void Fill_Product_Information()
        {
            if (TableID == 0)
                ProductId = Convert.ToInt32(lbItem.SelectedValue);

            if (ProductId != 0)
            {
                lblProductName.Text = "";
                //lblProductType.Text = "";
                ProductDetails = string.Empty;

                objRL.Get_Product_Records_By_Id(ProductId);
                //ProductDetails = string.Empty;
                Standard = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Standard)))
                    Standard = objRL.Standard;

                //if (!string.IsNullOrEmpty(Convert.ToString(objRL.Cavity)))
                //    txtCavity.Text = objRL.Cavity.ToString();

                //NeckHeightWeightQR = "Neck/Weight-" + objRL.ProductNeckSize.ToString() + "mm/" + Standard + "/" + objRL.ProductWeight;
                //ProductDetails = "Product-\t" + objRL.ProductName.ToString() + "\n" +
                //                    "Party-\t" + objRL.Party.ToString() + "\n" + NeckHeightWeightQR;
                ////"Neck/Height/Weight-" + objRL.ProductNeckSize.ToString() + "mm/" + objRL.ProductHeight.ToString() + "/" + objRL.ProductWeight;

                ProductId = Convert.ToInt32(objRL.ProductId);
                lblProductName.Text = objRL.ProductName.ToString();
                //lblProductType.Text = objRL.ProductType.ToString();

                if (objRL.ProductType == "Bottle")
                {
                    lblProductName.BackColor = Color.Cyan;
                    //lblProductType.BackColor = Color.Cyan;
                }
                else
                {
                    lblProductName.BackColor = Color.Yellow;
                    //lblProductType.BackColor = Color.Yellow;
                }
                
                lbItem.Visible = false;
                
            }
        }
    }
}
