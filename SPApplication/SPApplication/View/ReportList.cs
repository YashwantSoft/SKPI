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
using SPApplication.Report;
using SPApplication.Reports;

namespace SPApplication
{
    public partial class ReportList : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();
        ToolTip objTT = new ToolTip();

        public ReportList()
        {
            InitializeComponent();
            lbReportList.ForeColor = objDL.GetBackgroundColor();
            objDL.SetButtonDesign(btnExit, BusinessResources.BTN_EXIT);
            objDL.SetLabelDesign(lblHeader, BusinessResources.LBL_HEADER_REPORTLIST);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Select_Report()
        {
            if (lbReportList.Items.Count > 0)
            {
                if (lbReportList.Text == "Certificate of Analysis") //Task Assign Report
                {
                    COAReport objForm = new COAReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Temperature Report") //TemperatureReport
                {
                    TemperatureReport objForm = new TemperatureReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Monthly Production Report") //Quality Control Machine Wise Report
                {
                    MonthlyProductionReport objForm = new MonthlyProductionReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Progress Card Report") //Quality Control Machine Wise Report
                {
                    ProgressCardReport objForm = new ProgressCardReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Quality Control Product Wise Report") //Task Assign Report
                {
                    QualityControlProductWiseReport objForm = new QualityControlProductWiseReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Assign Task Report") //Task Assign Report
                {
                    AssignTaskReport objForm = new AssignTaskReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Log Report") //Task Assign Report
                {
                    LogReport objForm = new LogReport();
                    objForm.ShowDialog(this);
                }

                else if (lbReportList.Text == "Part / Equipment / Software / Hardware Due Date Report") //Task Assign Report
                {
                    DueDateReport objForm = new DueDateReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "QC Batch Wise Report")
                {
                    QualityControlMachineWiseReport objForm = new QualityControlMachineWiseReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == BusinessResources.LBL_REPORT_BATCHWISEQUALITYCONTROLREPORT)
                {
                    BatchWiseQualityControlReport objForm = new BatchWiseQualityControlReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == BusinessResources.LBL_HEADER_PRODUCTION_QUANTITY_REPORT)
                {
                    ProductionQuantityReport objForm = new ProductionQuantityReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == BusinessResources.LBL_HEADER_GradeNoticeBordReport)
                {
                    GradeNoticeBordReport objForm = new GradeNoticeBordReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Cap Certificate of Analysis")
                {
                    CapCOAReport objForm = new CapCOAReport();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Wad Certificate of Analysis")
                {
                    WadCOAReport objForm = new WadCOAReport();
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
        private void lbReportList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
