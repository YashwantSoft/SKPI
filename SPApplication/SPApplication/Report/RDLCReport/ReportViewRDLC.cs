using BusinessLayerUtility;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPApplication.Report.RDLCReport
{
    public partial class ReportViewRDLC : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        ErrorProvider objEP = new ErrorProvider();
        DesignLayer objDL = new DesignLayer();

        string ReportPath = string.Empty;
        string ReportDS = string.Empty, ReportDS1 = string.Empty, ReportDS_CompanyComman = string.Empty;
        string RDLC_ReportName = string.Empty;
        string ReportConcatPath = string.Empty;

        bool ParameterFlag = false;

        string rpReportName = string.Empty, rpReportDate = string.Empty, rpReportPeriod = string.Empty, rpReportBy = string.Empty;

        string ReportName = string.Empty;
        string rpProductName = string.Empty, rpMouldSrNo = string.Empty, rpProductType = string.Empty, rpMouldCavity = string.Empty;

        public ReportViewRDLC()
        {
            InitializeComponent();
            SetDesign();
        }

        public ReportViewRDLC(DataSet ds,string ReportNameS)
        {
            InitializeComponent();
            
            ReportName = ReportNameS;
            SetDesign();

            if(ReportNameS == BusinessResources.Report_BatchWiseQualityControlReport)
                FillReport(ds);
            else if (ReportNameS == BusinessResources.Report_ProductWiseQualityControlReport)
            {
                rpProductName = "Product Name:" + objRL.ProductName;
                rpProductType = "Product Type:" + objRL.ProductType;
                rpMouldSrNo = "Mould Number:" + objRL.MouldNo;
                rpMouldCavity = "Cavity:" + objRL.Cavity;
                FillReport(ds);
            }
            else
            {
            }
           
        }

        private void SetDesign()
        {
            lblHeader.Text = ReportName;
            lblHeader.BackColor = objDL.GetBackgroundColor();
            lblHeader.ForeColor = objDL.GetForeColor();
            btnExit.BackColor = objDL.GetBackgroundColor();
            btnExit.ForeColor = objDL.GetForeColor();
            btnExit.Text = BusinessResources.BTN_EXIT;
        }

        private void ReportViewRDLC_Load(object sender, EventArgs e)
        {
            this.rvRDLC.RefreshReport();
        }

        string SupplierName = string.Empty, ProductName = string.Empty, Shift_D = string.Empty, MachineNumber_D = string.Empty, Supplier_D = string.Empty, PlantIncharge_D = string.Empty, VolumeChecker_D = string.Empty, ConcatDetails = string.Empty;

        private void FillReport_Old(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Shift_D = string.Empty; MachineNumber_D = string.Empty; Supplier_D = string.Empty; PlantIncharge_D = string.Empty; VolumeChecker_D = string.Empty; ConcatDetails = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Product Name"].ToString())))
                {
                    ProductName = Convert.ToString(Convert.ToString(ds.Tables[0].Rows[0]["Product Name"].ToString()));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Product Name"].ToString())))
                {
                    SupplierName = Convert.ToString(Convert.ToString(ds.Tables[0].Rows[0]["Supplier"].ToString()));
                }
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Shift"].ToString())))
                {
                    Shift_D = Convert.ToString(ds.Tables[0].Rows[0]["Shift"].ToString());
                }
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["MachineNo"].ToString())))
                {
                    MachineNumber_D = Convert.ToString(ds.Tables[0].Rows[0]["MachineNo"].ToString());
                }
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Supplier"].ToString())))
                {
                    Supplier_D = Convert.ToString(ds.Tables[0].Rows[0]["Supplier"].ToString());
                }
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PlantIncharge"].ToString())))
                {
                    PlantIncharge_D = Convert.ToString(ds.Tables[0].Rows[0]["PlantIncharge"].ToString());
                }
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VolumeChecker"].ToString())))
                {
                    VolumeChecker_D = Convert.ToString(ds.Tables[0].Rows[0]["VolumeChecker"].ToString());
                }

                string exeFolder = Application.StartupPath;
                string reportPath = Path.Combine(exeFolder, @"D:\BitBucketProjects\Shree Khodiyar Plastic Industries\SPApplication\SPApplication\Report\RDLCReport\BatchWiseQualityControl_RD.rdlc");

                rvRDLC.Visible = true;
                rvRDLC.ProcessingMode = ProcessingMode.Local;
                rvRDLC.LocalReport.ReportPath = reportPath;
                ReportDataSource rds = new ReportDataSource("BSDataSet", ds.Tables[0]);
                rvRDLC.LocalReport.DataSources.Clear();

               
                ReportParameter[] parameters = new ReportParameter[10];
                parameters[0] = new ReportParameter("rpDate", "Date-" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATDDMMYYYY));
                parameters[1] = new ReportParameter("rpBatchNumber", "Batch Number-" + RedundancyLogics.ReportBatchNumber);
                parameters[2] = new ReportParameter("rpReportName", BusinessResources.LBL_REPORT_BATCHWISEQUALITYCONTROLREPORT);
                parameters[3] = new ReportParameter("rpProductName", "Product Name-" + ProductName);
                parameters[4] = new ReportParameter("rpSupplier", "Supplier Name-" + SupplierName);
                parameters[5] = new ReportParameter("rpMachineNumber", "Machine Number-" + MachineNumber_D);
                parameters[6] = new ReportParameter("rpShift", "Shift-" + Shift_D);
                parameters[7] = new ReportParameter("rpPlantIncharge", "Plant Incharge-" + PlantIncharge_D);
                parameters[8] = new ReportParameter("rpVolumeChecker", "Volume Checker-" + VolumeChecker_D);
                parameters[9] = new ReportParameter("rpFirmName", BusinessResources.LBL_FIRMNAME);

                //parameters[x] = new ReportParameter("namex", valuex);
                rvRDLC.LocalReport.SetParameters(parameters);
                rvRDLC.LocalReport.DataSources.Add(rds);
                rvRDLC.LocalReport.Refresh();
                rvRDLC.RefreshReport();
            }
        }


        private void FillReport(DataSet ds)
        {
            ParameterFlag = false;
            rpReportName = string.Empty; rpReportDate = string.Empty; rpReportPeriod = string.Empty; rpReportBy = string.Empty;

            rpReportName =  ReportName;
            rpReportDate = "Date: " + DateTime.Now.Date.ToString("dd/MMM/yyyy");

            rpReportPeriod = objRL.ReportPeriod;
            rpReportBy = BusinessLayer.UserName_Static;

            ReportDS_CompanyComman = string.Empty;

            ReportConcatPath = string.Empty;
            ReportConcatPath = objRL.GetPath_WithoutServer("RdlcPath");

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ReportName == BusinessResources.Report_BatchWiseQualityControlReport)
                {
                    Shift_D = string.Empty; MachineNumber_D = string.Empty; Supplier_D = string.Empty; PlantIncharge_D = string.Empty; VolumeChecker_D = string.Empty; ConcatDetails = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Product Name"].ToString())))
                    {
                        ProductName = Convert.ToString(Convert.ToString(ds.Tables[0].Rows[0]["Product Name"].ToString()));
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Product Name"].ToString())))
                    {
                        SupplierName = Convert.ToString(Convert.ToString(ds.Tables[0].Rows[0]["Supplier"].ToString()));
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Shift"].ToString())))
                    {
                        Shift_D = Convert.ToString(ds.Tables[0].Rows[0]["Shift"].ToString());
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["MachineNo"].ToString())))
                    {
                        MachineNumber_D = Convert.ToString(ds.Tables[0].Rows[0]["MachineNo"].ToString());
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Supplier"].ToString())))
                    {
                        Supplier_D = Convert.ToString(ds.Tables[0].Rows[0]["Supplier"].ToString());
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PlantIncharge"].ToString())))
                    {
                        PlantIncharge_D = Convert.ToString(ds.Tables[0].Rows[0]["PlantIncharge"].ToString());
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VolumeChecker"].ToString())))
                    {
                        VolumeChecker_D = Convert.ToString(ds.Tables[0].Rows[0]["VolumeChecker"].ToString());
                    }


                    ParameterFlag = true;
                    ReportDS = "BSDataSet";
                    RDLC_ReportName = "BatchWiseQualityControl_RD.rdlc";
                }
                else if (ReportName == BusinessResources.Report_ProductWiseQualityControlReport)
                {
                    ParameterFlag = true;
                    ReportDS = "ProductWiseDataSet";
                    RDLC_ReportName = "QCProductWiseRDLC.rdlc";
                }
                else
                {

                }

                ReportConcatPath += RDLC_ReportName;
                rvRDLC.Visible = true;
                this.rvRDLC.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rvRDLC.LocalReport.ReportPath = ReportConcatPath;

                ReportDataSource rds = new ReportDataSource(ReportDS, ds.Tables[0]);

                objRL.FillCompanyData();
                rvRDLC.LocalReport.DataSources.Clear();

                if (ParameterFlag)
                {
                    if (ReportName == BusinessResources.Report_BatchWiseQualityControlReport)
                    {
                        ReportParameter[] parameters = new ReportParameter[11];
                        parameters[0] = new ReportParameter("rpDate", "Date-" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATDDMMYYYY));
                        parameters[1] = new ReportParameter("rpBatchNumber", "Batch Number-" + RedundancyLogics.ReportBatchNumber);
                        parameters[2] = new ReportParameter("rpReportName", BusinessResources.LBL_REPORT_BATCHWISEQUALITYCONTROLREPORT);
                        parameters[3] = new ReportParameter("rpProductName", "Product Name-" + ProductName);
                        parameters[4] = new ReportParameter("rpSupplier", "Supplier Name-" + SupplierName);
                        parameters[5] = new ReportParameter("rpMachineNumber", "Machine Number-" + MachineNumber_D);
                        parameters[6] = new ReportParameter("rpShift", "Shift-" + Shift_D);
                        parameters[7] = new ReportParameter("rpPlantIncharge", "Plant Incharge-" + PlantIncharge_D);
                        parameters[8] = new ReportParameter("rpVolumeChecker", "Volume Checker-" + VolumeChecker_D);
                        parameters[9] = new ReportParameter("rpFirmName", objRL.CI_CompanyName);
                        parameters[10] = new ReportParameter("rpDetails", objRL.BatchNumber);

                        rvRDLC.LocalReport.SetParameters(parameters);
                    }
                    else if (ReportName == BusinessResources.Report_ProductWiseQualityControlReport)
                    {

                        ReportParameter[] parameters = new ReportParameter[9];
                        parameters[0] = new ReportParameter("rpFirmName", objRL.CI_CompanyName);
                        parameters[1] = new ReportParameter("rpReportName", rpReportName);
                        parameters[2] = new ReportParameter("rpReportDate", rpReportDate);
                        parameters[3] = new ReportParameter("rpReportPeriod", rpReportPeriod);
                        parameters[4] = new ReportParameter("rpReportBy", rpReportBy);
                        parameters[5] = new ReportParameter("rpProductName", rpProductName);
                        parameters[6] = new ReportParameter("rpMouldSrNo", rpMouldSrNo);
                        parameters[7] = new ReportParameter("rpProductType", rpProductType);
                        parameters[8] = new ReportParameter("rpMouldCavity", rpMouldCavity);
                        rvRDLC.LocalReport.SetParameters(parameters);
                    }
                    else
                    {
                    }
                }

                rvRDLC.LocalReport.DataSources.Add(rds);
                rvRDLC.LocalReport.Refresh();
                rvRDLC.RefreshReport();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
