using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using Microsoft.Office.Interop.Excel;

namespace SPApplication.Report
{
    public partial class ProgressCardReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        ErrorProvider objEP = new ErrorProvider();
        DesignLayer objDL = new DesignLayer();

        int RowCount = 18, AFlag = 0, RentCount = 0, SrNo = 1;
        double TotalAmount = 0;

        bool MH_Value = false;
        Microsoft.Office.Interop.Excel.Application myExcelApp;
        Microsoft.Office.Interop.Excel.Workbooks myExcelWorkbooks;
        Microsoft.Office.Interop.Excel.Workbook myExcelWorkbook;
        object misValue = System.Reflection.Missing.Value;

        string PDFReport = string.Empty;
        string ConcatQuery = string.Empty;
        string FacilityName = string.Empty;
        string Duration = string.Empty;

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string Status = string.Empty;
        int LoginId = 0;
        string UserIdList = string.Empty;
        string DepartmentList = string.Empty, MachineNo = string.Empty, Ratings = string.Empty;

        public ProgressCardReport()
        {
            InitializeComponent();
            objDL.Set_Report_Design(this, lblHeader, btnView, btnReport, btnClear, btnExit, BusinessResources.LBL_PROGRESSCARD_REPORT);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        int MonthDays = 0,MonthNumber=0,Year=0;
        string MonthName = string.Empty;
        string EmployeeName = string.Empty;

        private bool Validation()
        {
            objEP.Clear();
            if (cmbMonth.SelectedIndex == -1)
            {
                objEP.SetError(cmbMonth, "Select Month");
                cmbMonth.Focus();
                return true;
            }
            else if (cmbYear.SelectedIndex == -1)
            {
                objEP.SetError(cmbYear, "Select Year");
                cmbYear.Focus();
                return true;
            }
            else if (cmbDesignation.SelectedIndex == -1)
            {
                objEP.SetError(cmbDesignation, "Select Designation");
                cmbDesignation.Focus();
                return true;
            }
            else
                return false;
        }

        string ColumnName = string.Empty, ColumnText = string.Empty;
        string ShiftNo = string.Empty, OperatorGrade = string.Empty;
        double Amount = 0, TotalShot = 0, TotalTime = 0;
        static int DgvRowsStatic;
        string Designation = string.Empty;

        private void Set_Month_Year()
        {
            if (cmbMonth.SelectedIndex > -1)
            {
                MonthNumber = objRL.GetMonthNumber(cmbMonth.Text);

                if (MonthNumber < 10)
                    MonthString = "0" + MonthNumber.ToString();
                else
                    MonthString = MonthNumber.ToString();
            }
            if (cmbYear.SelectedIndex > -1)
                Year = Convert.ToInt32(cmbYear.Text);
        }

        private void FillGrid()
        {
            if (!Validation())
            {
                dgvReport.Columns.Clear();
                dgvReport.Rows.Clear();
                MonthNumber = 0; Year = 0; DgvRowsStatic = 0;
                MonthNumber = objRL.GetMonthNumber(cmbMonth.Text);
                Year = Convert.ToInt32(cmbYear.Text);
                MonthDays = DateTime.DaysInMonth(Year, MonthNumber);
                Set_Month_Year();

                dgvReport.Columns.Add("clmEmployeeName", "Employee Name");
                dgvReport.Columns["clmEmployeeName"].Width = 200;  
                dgvReport.Columns.Add("clmEmployeeId", "ID");
                dgvReport.Columns["clmEmployeeId"].Visible = false;
                dgvReport.Columns.Add("clmParticulars", "Particulars");
                dgvReport.Columns["clmParticulars"].Width = 150;  

                for (int i = 1; i <= MonthDays; i++)
                {
                    ColumnName = string.Empty; ColumnText = string.Empty;
                    ColumnName = "clm" + i; ColumnText =  i.ToString();
                    dgvReport.Columns.Add(ColumnName, ColumnText);
                    dgvReport.Columns[ColumnName].Width = 60;

                    
                }
                
                DataSet ds = new DataSet();
                Designation = cmbDesignation.Text;
                objBL.Query = "select ID,FullName from Employee where CancelTag=0 and Designation IN('" + Designation + "')";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgvReport.Rows.Clear(); DgvRowsStatic = 0;
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[j][1].ToString())))
                        {
                            GridRowCount = DgvRowsStatic;

                            dgvReport.Rows.Add(); 
                            dgvReport.Rows[DgvRowsStatic].Cells["clmEmployeeName"].Value = ds.Tables[0].Rows[j][1].ToString();
                            dgvReport.Rows[DgvRowsStatic].Cells["clmEmployeeId"].Value = ds.Tables[0].Rows[j][0].ToString();
              
                            dgvReport.Rows[DgvRowsStatic].Cells["clmParticulars"].Value = "Shift No";
                            DgvRowsStatic++;
                            dgvReport.Rows.Add();
                            dgvReport.Rows[DgvRowsStatic].Cells["clmParticulars"].Value = "Machine No";
                            DgvRowsStatic++;
                            dgvReport.Rows.Add();
                            dgvReport.Rows[DgvRowsStatic].Cells["clmParticulars"].Value = "Total Shots/ Hours";
                            DgvRowsStatic++;
                            dgvReport.Rows.Add();
                            dgvReport.Rows[DgvRowsStatic].Cells["clmParticulars"].Value = "Operator Grade";
                            DgvRowsStatic++;
                            dgvReport.Rows.Add();
                            dgvReport.Rows[DgvRowsStatic].Cells["clmParticulars"].Value = "Amount";
                            
                            DgvRowsStatic++;
                        }
                    }

                    Get_Data_OEECalculation();
                }
            }
        }

        static int GridRowCount ;
        double OperatingTime = 0;
        int EmpoyeeId = 0;

        private void Get_Data_OEECalculation()
        {
            for (int i = 0; i < dgvReport.Rows.Count; i=i+5)
            {
                //foreach (DataGridViewRow row in dgvReport.Rows)
                //    row.DefaultCellStyle.BackColor = Color.Red;

                    //if (Convert.ToInt32(row.Cells[7].Value) < Convert.ToInt32(row.Cells[10].Value))
                    //{
                    //    row.DefaultCellStyle.BackColor = Color.Red;
                    //}
                dgvReport.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#C9CADD");
                EmpoyeeId = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(dgvReport.Rows[i].Cells["clmEmployeeId"].Value)))
                {
                    GridRowCount = i;
                    EmpoyeeId = Convert.ToInt32(dgvReport.Rows[i].Cells["clmEmployeeId"].Value.ToString());
                    WhereClause = string.Empty;

                    if (Designation == "Operator")
                        WhereClause = " and OperatorId=" + EmpoyeeId + "";
                    else
                        WhereClause = " and PackerId=" + EmpoyeeId + " or PackerId1=" + EmpoyeeId + " or PackerId2=" + EmpoyeeId + " ";

                    for (int j = 1; j < MonthDays; j++)
                    {
                        GridRowCount = i;
                        Get_Date(j);
                        ColumnName = string.Empty; ColumnText = string.Empty;
                        ColumnName = "clm" + j; ColumnText = j.ToString();

                        if (Convert.ToString(CheckDate.DayOfWeek) != "Tuesday")
                        {
                            MachineNo = string.Empty; TotalShot = 0; OperatorGrade = "NA"; Amount = 0;
                            DataSet ds = new DataSet();
                            objBL.Query = "select ID,EntryDate,EntryTime,ProductionEntryId,OEEEntryId,MachineNo,ProductId,SupervisorId,OperatorId,PackerId,PackerId1,PackerId2,PreformLoadingId,MouldChangerId,MouldChangerId1,ShiftLengthA,ShortMealBreakB,NoPlanning,NoElectricity,TotalProductionTime,Breakdown,Changeover,ManpowerShortage,StartupLoss,MaintainanceMachineShutDownTime,MaterialNotAvailable,TotalDowntime,OperatingTime,Availabilty,IdealRunRate,Cavity,PlanningQty,TotalShot,TotalProductionInNos,Packing,TotalPacket,TargetProduction,Performance,RejectInNos,GoodInNos,Quality,OEE,SwitchFlag,SwitchPackerMachineId,SwitchOperatiorMachineId,Reason,Status,SwitchNote,PreformRejection from OEEEntryMachine where CancelTag=0 " + WhereClause + " and EntryDate=#" + CheckDate.ToString(BusinessResources.DATEFORMATDDMMYYYY) + "#";
                            ds = objBL.ReturnDataSet();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                int OEEEntryId = 0;
                                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["OEEEntryId"].ToString())))
                                {
                                    OEEEntryId = Convert.ToInt32(ds.Tables[0].Rows[0]["OEEEntryId"].ToString());
                                    // if (!string.IsNullOrEmpty(Convert.ToString(objRL.Get_Shift_By_OEEEntryId(OEEEntryId))))
                                    ShiftNo = Convert.ToString(objRL.Get_Shift_By_OEEEntryId(OEEEntryId));
                                }

                                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["MachineNo"].ToString())))
                                    MachineNo = ds.Tables[0].Rows[0]["MachineNo"].ToString();
                                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TotalShot"].ToString())))
                                    TotalShot = Convert.ToDouble(ds.Tables[0].Rows[0]["TotalShot"].ToString());
                                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["OperatingTime"].ToString())))
                                    OperatingTime = Convert.ToDouble(ds.Tables[0].Rows[0]["OperatingTime"].ToString());

                                Get_Operator_Packer_Grade();

                                dgvReport.Rows[GridRowCount].Cells[ColumnName].Value = ShiftNo;
                                GridRowCount++;
                                dgvReport.Rows[GridRowCount].Cells[ColumnName].Value = MachineNo;
                                GridRowCount++;
                                dgvReport.Rows[GridRowCount].Cells[ColumnName].Value = TotalShot.ToString();
                                GridRowCount++;
                                dgvReport.Rows[GridRowCount].Cells[ColumnName].Value = OperatorGrade;
                                GridRowCount++;
                                dgvReport.Rows[GridRowCount].Cells[ColumnName].Value = Amount.ToString();
                                GridRowCount++;
                            }
                            else
                            {
                                dgvReport.Rows[GridRowCount].Cells[ColumnName].Value = ShiftNo;
                                GridRowCount++;
                                dgvReport.Rows[GridRowCount].Cells[ColumnName].Value = MachineNo;
                                GridRowCount++;
                                dgvReport.Rows[GridRowCount].Cells[ColumnName].Value = TotalShot.ToString();
                                GridRowCount++;
                                dgvReport.Rows[GridRowCount].Cells[ColumnName].Value = OperatorGrade;
                                GridRowCount++;
                                dgvReport.Rows[GridRowCount].Cells[ColumnName].Value = Amount.ToString();
                                GridRowCount++;
                            }
                        }
                        else
                        {
                            dgvReport.Columns[ColumnName].DefaultCellStyle.BackColor = Color.Coral;// ColorTranslator.FromHtml("#C9CADD");

                            //DataGridViewColumn dataGridViewColumn = dgvReport.Columns[ColumnName];
                            //dataGridViewColumn.HeaderCell.Style.BackColor = Color.Chocolate;
                            //dgvReport.EnableHeadersVisualStyles = false;
                        }
                    }
                }
            }
        }

        private void Get_Operator_Packer_Grade()
        {
            //1-Working Days            AUTO
            //2-Non-Working Days	N/W	
            //3-Half Days	                HD	
            //4-National Holiday	    NH	
            //5-Paid Leave	                PL	
            //6-Salary Days	            AUTO	
            //7-Causal Leave            CL	
            //8-Sick Leave	                SL	
            //9-Absent Days	            SL	AB	
            //10-Unpaid Leave	        UPL	
            //11-Break	                        BRE
            //12-Left Emp.                  LF

            // RELEASE PERSON	        REL	
            //PREFORM LOADING      PFL

            //=IF(N6<1,"0",IF(N6<2100,"B",IF(N6<2400,"A",IF(N6<2700,"A+",IF(N6<3000,"A++",IF(N6<3300,"AAA",IF(N6<3600,"3*",IF(N6<4500,"5*",))))))))
            //5*         225	
            //3*         200	
            //AAA      175	
            //A++	    150	
            //A+        125	
            //A          100	
            //B            50	
            //Preform Loading   125		
            //AP       100

            if (Designation == "Operator")
            {
                if (TotalShot > 1 && TotalShot < 2100)
                {
                    OperatorGrade = "B";
                    Amount = 50;
                }
                else if (TotalShot > 2101 && TotalShot < 2400)
                {
                    OperatorGrade = "A";
                    Amount = 100;
                }
                else if (TotalShot > 2401 && TotalShot < 2700)
                {
                    OperatorGrade = "A+";
                    Amount = 125;
                }
                else if (TotalShot > 2701 && TotalShot < 3000)
                {
                    OperatorGrade = "A++";
                    Amount = 150;
                }
                else if (TotalShot > 3001 && TotalShot < 3300)
                {
                    OperatorGrade = "AAA";
                    Amount = 175;
                }
                else if (TotalShot > 3301 && TotalShot < 3600)
                {
                    OperatorGrade = "3*";
                    Amount = 200;
                }
                else if (TotalShot > 3601 && TotalShot < 4500)
                {
                    OperatorGrade = "5*";
                    Amount = 225;
                }
                else
                    OperatorGrade = "0";
             
            }
            else
            {
                if (Convert.ToInt32(MachineNo) < 10)
                {
                    if (TotalShot > 1 && TotalShot < 3000)
                    {
                        OperatorGrade = "P-100";
                        Amount = 0;
                    }
                    else if (TotalShot > 3001 && TotalShot < 3300)
                    {
                        OperatorGrade = "P1*";
                        Amount = 35;
                    }
                    else if (TotalShot > 3301 && TotalShot < 3600)
                    {
                        OperatorGrade = "P3*";
                        Amount = 70;
                    }
                    else if (TotalShot > 3601 && TotalShot < 4500)
                    {
                        OperatorGrade = "P5*";
                        Amount = 100;
                    }
                    else
                        OperatorGrade = "0";
                }
                else
                {
                    //OperatingTime
                    if (OperatingTime > 1 && TotalShot < 240)
                    {
                        OperatorGrade = "REL";
                        Amount = 0;
                    }
                    else if (OperatingTime > 240)
                    {
                        OperatorGrade = "Sedel";
                        Amount = 100;
                    }
                    else
                        OperatorGrade = "REL";
                }
            }
        }

        private void Get_Date(int Dt)
        {
            if (Dt < 10)
                DateString = "0" + Dt.ToString();
            else
                DateString = Dt.ToString();

            ConcatDate = DateString + "/" + MonthString + "/" + Year;
            CheckDate = Convert.ToDateTime(ConcatDate);
        }

        DateTime CheckDate;
        string DateString = string.Empty, MonthString = string.Empty, ConcatDate = string.Empty;

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

        }

        private void ClearAll()
        {
            objEP.Clear();
            cmbDesignation.SelectedIndex = -1;
            cmbYear.SelectedIndex = -1;
            cmbMonth.SelectedIndex = -1;
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }
    }
}
