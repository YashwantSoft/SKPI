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
using SPApplication.Report;

namespace SPApplication.Transaction
{
    public partial class OEECalculation : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, OEEId = 0;

        bool BorderFlag = false; 
        bool CellFlag = false,AlignFlag = false, boldflag = false;
        string PDFReport = string.Empty;
        int AFlag = 0; int SrNo = 1;
        int RowCount = 18;
        bool MH_Value = false;
        string ReportName = string.Empty;
        
        Excel.Application myExcelApp;
        Excel.Workbooks myExcelWorkbooks;
        Excel.Workbook myExcelWorkbook;

        double ShiftLength = 0, ShortMealBreakB = 0, NoPlanning = 0, NoElectricity = 0, TotalProductionTime = 0, Breakdown = 0, Changeover = 0, ManpowerShortage = 0, StartupLoss = 0, MaintainanceMachineShutDownTime = 0;
        double MaterialNotAvailable = 0, PlanningQty = 0, TotalShot = 0, RejectInNos = 0, TotalDowntime = 0, OperatingTime = 0, Availabilty = 0, TotalProductionInNos = 0, Packing = 0, TotalPacket = 0;
        double TargetProduction = 0, Performance = 0, GoodInNos = 0, Quality = 0, OEE = 0, Cavity = 0, IdealRunRate = 0, PreformRejection = 0;

        public OEECalculation()
        {
            //LBL_HEADER_OEE
            InitializeComponent();

            objDL.SetDesignMaster(this, lblHeader, btnNext, btnClear, btnOEEReport, btnExit, BusinessResources.LBL_HEADER_OEE);

            objDL.SetButtonDesign(btnNext, "NEXT");
            objDL.SetButtonDesign(btnOEEReport, "OEE");

            objDL.SetButtonDesign_SmallSize(btnSaveOEE, BusinessResources.BTN_SAVE);
            objDL.SetButtonDesign_SmallSize(btnClearOEE, BusinessResources.BTN_CLEAR);
            objDL.SetButtonDesign_SmallSize(btnDeleteOEE, BusinessResources.BTN_DELETE);

            objDL.SetButtonDesign_SmallSize(btnAddOperatorPacker, BusinessResources.BTN_ADD);
            objDL.SetButtonDesign_SmallSize(btnClearOperatorPacker, BusinessResources.BTN_CLEAR);
            objDL.SetButtonDesign_SmallSize(btnDeleteOperatorPacker, BusinessResources.BTN_DELETE);

            objDL.SetButtonDesign_SmallSize(btnAddMold, BusinessResources.BTN_ADD);
            objDL.SetButtonDesign_SmallSize(btnClearMold, BusinessResources.BTN_CLEAR);
            objDL.SetButtonDesign_SmallSize(btnDeleteMold, BusinessResources.BTN_DELETE);

            objDL.SetButtonDesign_SmallSize(btnAddPorter, BusinessResources.BTN_ADD);
            objDL.SetButtonDesign_SmallSize(btnClearPorter, BusinessResources.BTN_CLEAR);
            objDL.SetButtonDesign_SmallSize(btnDeletePorter, BusinessResources.BTN_DELETE);

            btnPorterPackets.BackColor = objDL.GetBackgroundColor();
            btnPorterPackets.ForeColor = objDL.GetForeColorError();

            dtpShiftDate.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpShiftDate.Value = DateTime.Now;

            objRL.Fill_Machine(cmbMachineNo);
            objRL.Fill_Machine(cmbMachineNoMold);
            //objRL.Fill_Grade(cmbGradeOperatorPacker);
            
            objRL.Fill_Machine(cmbMachineNoOperatorPacker);

            btnNext.BackColor = objDL.GetBackgroundColor();
            btnNext.ForeColor = objDL.GetForeColor();

            btnOEEReport.BackColor = objDL.GetBackgroundColor();
            btnOEEReport.ForeColor = objDL.GetForeColor();
             
            dtpShiftStart.Format = DateTimePickerFormat.Custom;
            dtpShiftStart.CustomFormat = "HH:mm"; //Convert.ToString(BusinessResources.TIME_FORMAT_HHMM_24);
            dtpShiftEnd.CustomFormat = "HH:mm"; // Convert.ToString(BusinessResources.TIME_FORMAT_HHMM_24);

            //objRL.Fill_Designation_Distinct(cmbOriginalDesignationOperatorPacker);
            //objRL.Fill_Designation_Distinct(cmbDesignationOperatorPacker);

            objRL.Fill_CommanMaster(cmbRemarksOperatorPacker, "Reason");
            GetID();
            objRL.Fill_Employee_By_Designation_Operator_Packer(cmbEmployeeOperatorPacker);
            lblBottleInformation.BackColor = Color.Cyan;
            lblJarInformation.BackColor = Color.Yellow;
            //btnSwitch.Text = "Change";

            //objRL.Fill_Designation(cmbDesignationEmployee);
            //objRL.Fill_CommanMaster(cmbChangeFor, "ChangeFor");
            //objRL.Fill_Machine(cmbMachinNoSearch);
            //dtpShiftBegin.CustomFormat = Convert.ToString(BusinessResources.TIME_FORMAT_HHMM_24);
            //dtpShiftEnd.CustomFormat =Convert.ToString(BusinessResources.TIME_FORMAT_HHMM_24);
            //objDL.SetButtonDesign(btn1, "1");
            //objDL.SetButtonDesign(btn2, "2");
            //objDL.SetButtonDesign(btn3, "3");
            //objDL.SetButtonDesign(btn4, "4");
            //objDL.SetButtonDesign(btn5, "5");
            //objDL.SetButtonDesign(btn6, "6");
            //objDL.SetButtonDesign(btn7, "7");
            //objDL.SetButtonDesign(btn8, "8");
            //objDL.SetButtonDesign(btn9, "9");
            //objDL.SetButtonDesign(btn10, "10");
            //objDL.SetButtonDesign(btn11, "11");
            //objDL.SetButtonDesign(btn12, "12");
            //objDL.SetButtonDesign(btn13, "13");
            //objDL.SetButtonDesign(btn14, "14");

            //objRL.Fill_Employee_By_Designation(cmbPlantIncharge, "Plant Incharge");
            //objRL.Fill_Employee_By_Designation(cmbVolumeChecker, "Volume Checker");

            //objRL.Fill_Employee_ComboBox_OperatorPacker(cmbPacker);
            //objRL.Fill_Employee_ComboBox_OperatorPacker(cmbOperator);

            //objRL.Fill_Employee_ComboBox_OperatorPacker(cmbPreformLoadingAuto);

            //objRL.Fill_Employee_ComboBox_Supervisor_Not_In_Id
            //objRL.Fill_Employee_By_Designation(cmbOperator, "Operator");
            //objRL.Fill_Employee_By_Designation(cmbPacker, "Packer");
            //objRL.Fill_Employee_CheckedListBox_By_Designation(clbOperator, "Operator");
            //objRL.Fill_Employee_CheckedListBox_By_Designation(clbPacker, "Packer");
            //objRL.Fill_Employee_CheckedListBox_By_Designation(clbMouldChanger, "Mould Changer");
            //objRL.Fill_Employee_CheckedListBox_By_Designation(clbPorter, "Porter");
            //objRL.Fill_Employee_CheckedListBox_By_Designation(clbSupervisor, "Supervisor");
            //objRL.Fill_Employee_CheckedListBox_By_Designation_Auto(clbPackerAuto);
        }

        private void GetShiftDetails()
        {
            OEEId = 0;
            dtpShiftStart.Value = DateTime.Now;
            dtpShiftEnd.Value = DateTime.Now;
            txtShiftHours.Text = "";
            txtShift.Text = "";
            ShiftId = 0;
            btnAddShift.Visible = false;

            objRL.ShifFromDate = DateTime.Now;
            objRL.ShiftToDate = DateTime.Now;
            objRL.ShiftId = 0;
            objRL.ShiftHours = "";

            //dtpShiftDate.Value = DateTime.Now;
            txtShift.Text = objRL.GetShift_By_Date(dtpShiftDate.Value);

            if (!string.IsNullOrEmpty(Convert.ToString(objRL.Shift)))
            {
                dtpShiftStart.Value = objRL.ShifFromDate;
                dtpShiftEnd.Value = objRL.ShiftToDate;
                ShiftId = objRL.ShiftId;
                txtShiftHours.Text = objRL.ShiftHours;
                btnAddShift.Visible = false;
            }
            else
                btnAddShift.Visible = true;
        }

        private void OEESheet_Load(object sender, EventArgs e)
        {
            OEEId = 0;
            ClearAll();
            FillGrid();
            //dtpShiftDate.Value = DateTime.Now;
           
            //GetShiftDetails();

            //Get_OEEEntry();
            //CheckExistShift();
            //CheckExist_OEE();
        }

        string NoOfShifts = string.Empty;
        //int ShiftScheduleId = 0;

        //private void CheckExistShift()
        //{
        //    cmbShift.SelectedIndex = -1;
        //    DataSet ds = new DataSet();
        //    WhereClause = string.Empty;
        //    MainQuery = string.Empty;

        //    MainQuery = "select EntryDate,EntryTime,FromDate,ToDate,ShiftDate,ShiftHours,NoOfShifts,BeginTime1,EndTime1,BeginTime2,EndTime2,BeginTime3,EndTime3,Naration from ShiftSchedule where CancelTag=0 ";

        //    //if (SearchTag)
        //    WhereClause = " and ShiftDate=#" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# order by ShiftScheduleId desc";

        //    objBL.Query = MainQuery + WhereClause;
        //    ds = objBL.ReturnDataSet();

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        //0 ShiftScheduleId,
        //        //1 EntryDate,
        //        //2 EntryTime,
        //        //3 FromDate,
        //        //4 ToDate,
        //        //5 ShiftDate as [Shift Date],
        //        //6 ShiftHours,
        //        //7 NoOfShifts,
        //        //8 BeginTime1,
        //        //9 EndTime1
        //        //10 BeginTime2,
        //        //11 EndTime2,
        //        //12 BeginTime3,
        //        //13 EndTime3
        //        //14 Naration,

        //        //cmbShift

        //        NoOfShifts = string.Empty;

        //        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["NoOfShifts"].ToString())))
        //        {
        //            CurrentTime = DateTime.Now;

        //            NoOfShifts = Convert.ToString(ds.Tables[0].Rows[0]["NoOfShifts"].ToString());
        //            ShiftScheduleId = Convert.ToInt32(ds.Tables[0].Rows[0]["ShiftScheduleId"].ToString());

        //            BeginTime1 = Convert.ToDateTime(ds.Tables[0].Rows[0]["BeginTime1"].ToString());
        //            EndTime1 = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndTime1"].ToString());
        //            BeginTime2 = Convert.ToDateTime(ds.Tables[0].Rows[0]["BeginTime2"].ToString());
        //            EndTime2 = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndTime2"].ToString());
        //            BeginTime3 = Convert.ToDateTime(ds.Tables[0].Rows[0]["BeginTime3"].ToString());
        //            EndTime3 = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndTime3"].ToString());

        //            TimeSpan CurrentTime1 = DateTime.Now.TimeOfDay;

        //            TimeSpan BTime1_TS = BeginTime1.TimeOfDay;
        //            TimeSpan ETime1_TS = EndTime1.TimeOfDay;
        //            TimeSpan BTime2_TS = BeginTime2.TimeOfDay;
        //            TimeSpan ETime2_TS = EndTime2.TimeOfDay;
        //            TimeSpan BTime3_TS = BeginTime3.TimeOfDay;
        //            TimeSpan ETime4_TS = EndTime3.TimeOfDay;

        //            //if (CurrentTime1 > BTime1_TS && CurrentTime1 < ETime1_TS)
        //            //    cmbShift.Text = "I";
        //            //else if (CurrentTime1 > BTime2_TS && CurrentTime1 < ETime2_TS)
        //            //    cmbShift.Text = "II";
        //            //else
        //            //    cmbShift.Text = "III";

        //            //else if (CurrentTime1 >= BTime3_TS && CurrentTime1 <= ETime4_TS)
        //            //    cmbShift.Text = "III";
        //        }
        //    }
        //    else
        //    {
        //        //ShiftSchedule objForm = new ShiftSchedule();
        //        //objForm.ShowDialog(this);
        //        //CheckExistShift();
        //    }
        //}

        DateTime CurrentTime, BeginTime1, EndTime1, BeginTime2, EndTime2, BeginTime3, EndTime3;

        private void Set_Shift_Checkbox()
        {
            List<string> listStrLineElements = NoOfShifts.Split(',').ToList();

            ////var valueArray = valueList.Split(',');
            //for (int i = 0; i < clbNoOfShift.Items.Count; i++)
            //{
            //    if (listStrLineElements.Contains(clbNoOfShift.Items[i].ToString()))
            //    {
            //        clbNoOfShift.SetItemChecked(i, true);
            //    }
            //}
        }

        //private void Get_OEEEntry()
        //{
        //    if (cmbShift.SelectedIndex > -1)
        //    {
        //        DataSet ds = new DataSet();
        //        objBL.Query = "select OE.ID,OE.EntryDate as [Date],OE.EntryTime as [Time],OE.Shift,OE.PlantInchargeId,E.FullName as[Plant Incharge],OE.VolumeCheckerId,E1.FullName as [Volume Checker] from ((OEEEntry OE inner join Employee E on E.ID=OE.PlantInchargeId) inner join Employee E1 on E1.ID=OE.VolumeCheckerId) where OE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and OE.Shift='" + cmbShift.Text + "' and OE.EntryDate=#" + dtpShiftDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# ";
        //        ds = objBL.ReturnDataSet();

        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            //0 ID,
        //            //1 EntryDate as [Date],
        //            //2 EntryTime as [Time],
        //            //3OE.Shift,
        //            //4 OE.PlantInchargeId,
        //            //5 E.FullName as[Plant Incharge],
        //            //6 OE.VolumeCheckerId,
        //            //7 E1.FullName as [Volume Checker]

        //            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
        //            {
        //                OEEEntryId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
        //                txtOEEId.Text = OEEEntryId.ToString();
        //            }
        //            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Date"])))
        //                dtpShiftDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["Date"].ToString());
        //            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Time"])))
        //                dtpTime.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["Time"].ToString());
        //            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Shift"])))
        //                cmbShift.Text = ds.Tables[0].Rows[0]["Shift"].ToString();

        //            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Plant Incharge"])))
        //                cmbPlantIncharge.Text = ds.Tables[0].Rows[0]["Plant Incharge"].ToString();
        //            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Volume Checker"])))
        //                cmbVolumeChecker.Text = ds.Tables[0].Rows[0]["Volume Checker"].ToString();
        //        }
        //        else
        //        {
        //            dtpShiftDate.Value = DateTime.Now.Date;
        //            dtpTime.Value = DateTime.Now;
        //            cmbPlantIncharge.SelectedIndex = -1;
        //            cmbVolumeChecker.SelectedIndex = -1;
        //            cmbShift.SelectedIndex = -1;
        //            GetID();
        //            ShiftCode();
        //            OEEEntryId = 0;
        //        }
        //    }
        //}

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Employee objForm = new Employee();
            objForm.ShowDialog(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void GetID()
        {
            OEEId = 0;
            int IDNo = 0;
            //IDNo = Convert.ToInt32(objRL.ReturnMaxID("OEE"));
            IDNo = Convert.ToInt32(objRL.ReturnMaxID_Fix_Table("OEE", "OEEId"));
            txtOEEId.Text = IDNo.ToString();
        }

        //private void ShiftCode()
        //{
        //    cmbShift.Text = objRL.ShiftCode();// Shift.ToString();
        //}

        private void cmbVolumChecker_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if (cmbShift.SelectedIndex > -1 && cmbPlantIncharge.SelectedIndex > -1 && cmbVolumeChecker.SelectedIndex > -1)
            //    gbMachineNo.Enabled = true;
            //else
            //    gbMachineNo.Enabled = false;
        }

       
        private void Calculations()
        {
            Clear_Variables();

            double.TryParse(txtCavity.Text, out Cavity);
            // double.TryParse(txtIdealRunRate.Text, out IdealRunRate);
            double.TryParse(txtPacking.Text, out Packing);
            
            double.TryParse(txtShiftLength.Text, out ShiftLength);
            double.TryParse(txtMachineAvailableTime.Text, out MachineAvailableTime);
            double.TryParse(txtShortMealBreakB.Text, out ShortMealBreakB);
            double.TryParse(txtNoPlanning.Text, out NoPlanning);
            double.TryParse(txtNoElectricity.Text, out NoElectricity);
            double.TryParse(txtBreakdown.Text, out Breakdown);
            double.TryParse(txtChangeover.Text, out Changeover);
            double.TryParse(txtManpowerShortage.Text, out ManpowerShortage);
            double.TryParse(txtStartupLoss.Text, out StartupLoss);
            double.TryParse(txtMaintainanceMachineShutDownTime.Text, out MaintainanceMachineShutDownTime);
            double.TryParse(txtMaterialNotAvailable.Text, out MaterialNotAvailable);
            double.TryParse(txtPlanningQty.Text, out PlanningQty);
            double.TryParse(txtTotalShot.Text, out TotalShot);
            double.TryParse(txtRejectInNos.Text, out RejectInNos);
            double.TryParse(txtPreformRejection.Text, out PreformRejection);
            // double.TryParse(txtTotalDowntime.Text, out TotalDowntime);
            //double.TryParse(txtOperatingTime.Text, out OperatingTime);
            //double.TryParse(txtAvailabilty.Text, out Availabilty);
            double.TryParse(txtTotalProductionInNos.Text, out TotalProductionInNos);

            double.TryParse(txtTotalPacket.Text, out TotalPacket);
            double.TryParse(txtTargetProduction.Text, out TargetProduction);
            //double.TryParse(txtPerformance.Text, out Performance);
            double.TryParse(txtGoodInNos.Text, out GoodInNos);
            double.TryParse(txtQuality.Text, out Quality);
            //double.TryParse(txtOEE.Text, out OEE);

            IdealRunRate = Convert.ToDouble(MachineNo_Wise_IdealRunRate());
            IdealRunRate = IdealRunRate * Cavity;
            IdealRunRate = Math.Round(IdealRunRate, 0);
            txtIdealRunRate.Text = IdealRunRate.ToString();

            //Changes as per required by Priti madam shortmeal break not consider in)
            //TotalProductionTime = ShiftLengthA - (NoPlanning + NoElectricity);
            //TotalProductionTime = ShiftLengthA - (ShortMealBreakB + NoPlanning + NoElectricity);
            TotalProductionTime = MachineAvailableTime - (ShortMealBreakB + NoPlanning + NoElectricity);
            txtTotalProductionTime.Text = TotalProductionTime.ToString();

            TotalDowntime = Breakdown + Changeover + ManpowerShortage + StartupLoss + MaintainanceMachineShutDownTime + MaterialNotAvailable;
            TotalDowntime = Math.Round(TotalDowntime, 2);
            txtTotalDowntime.Text = TotalDowntime.ToString();

            OperatingTime = TotalProductionTime - TotalDowntime;
            OperatingTime = Math.Round(OperatingTime, 2);
            txtOperatingTime.Text = OperatingTime.ToString();

            Availabilty = (OperatingTime / TotalProductionTime) * 100;

            if (double.IsNaN(Availabilty))
                Availabilty = 0;
            else
                Availabilty = Convert.ToDouble(Math.Round(Availabilty, 2));

            txtAvailabilty.Text = Availabilty.ToString();

            TotalProductionInNos = Cavity * TotalShot;
            txtTotalProductionInNos.Text = TotalProductionInNos.ToString();

            GoodInNos = TotalProductionInNos - RejectInNos;
            GoodInNos = Convert.ToDouble(Math.Round(GoodInNos, 2));
            txtGoodInNos.Text = GoodInNos.ToString();

            TargetProduction = (OperatingTime / 60) * IdealRunRate;
            TargetProduction = Convert.ToDouble(Math.Round(TargetProduction, 0));
            txtTargetProduction.Text = TargetProduction.ToString();

            Performance = (TotalProductionInNos / TargetProduction) * 100;

            if (double.IsNaN(Performance))
                Performance = 0;
            else
                Performance = Convert.ToDouble(Math.Round(Performance, 2));
            
            txtPerformance.Text = Performance.ToString();

            TotalPacket = GoodInNos / Packing;

            if (double.IsNaN(TotalPacket))
                TotalPacket = 0;
            else
                TotalPacket = Math.Round(TotalPacket, 0);
            
            txtTotalPacket.Text = TotalPacket.ToString();

            Quality = (GoodInNos / TotalProductionInNos) * 100;

            if (double.IsNaN(Quality))
                Quality = 0;
            else
                Quality = Math.Round(Quality, 2);
            
            txtQuality.Text = Quality.ToString();

            //  OEE = (Quality * Performance * Availabilty)/300;
            OEE = ((GoodInNos / TotalProductionInNos) * (TotalProductionInNos / TargetProduction) * (OperatingTime / TotalProductionTime)) * 100;

            if (double.IsNaN(OEE))
                OEE = 0;
            else
                OEE = Math.Round(OEE, 2);

            txtOEE.Text = OEE.ToString();
        }

        private void txtShiftLengthA_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtShortMealBreakB_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtNoPlanning_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtNoElectricity_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtBreakdown_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtChangeover_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtManpowerShortage_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtStartupLoss_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtMaintainanceMachineShutDownTime_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtMaterialNotAvailable_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtPlanningQty_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtTotalShot_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtRejectInNos_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtTotalDowntime_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtOperatingTime_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtAvailabilty_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtTotalProductionInNos_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtPacking_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtTotalPacket_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtTargetProduction_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtPerformance_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtGoodInNos_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtQuality_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtOEE_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtShortMealBreakB_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtShortMealBreakB);
        }

        private void txtNoPlanning_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNoPlanning);
        }

        private void txtNoElectricity_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNoElectricity);
        }

        private void txtBreakdown_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtBreakdown);
        }

        private void txtChangeover_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtChangeover);
        }

        private void txtManpowerShortage_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtManpowerShortage);
        }

        private void txtStartupLoss_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtStartupLoss);
        }

        private void txtMaintainanceMachineShutDownTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtMaintainanceMachineShutDownTime);
        }

        private void txtMaterialNotAvailable_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtMaterialNotAvailable);
        }

        private void txtPlanningQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtPlanningQty);
        }

        private void txtTotalShot_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtTotalShot);
        }

        private void txtRejectInNos_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtRejectInNos);
        }

        private void txtShiftLengthA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtShortMealBreakB.Focus();
        }

        private void txtShortMealBreakB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNoPlanning.Focus();
        }

        private void txtNoPlanning_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNoElectricity.Focus();
        }

        private void txtNoElectricity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtBreakdown.Focus();
        }

        private void txtBreakdown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtChangeover.Focus();
        }

        private void txtChangeover_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtManpowerShortage.Focus();
        }

        private void txtManpowerShortage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtStartupLoss.Focus();
        }

        private void txtStartupLoss_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMaintainanceMachineShutDownTime.Focus();
        }

        private void txtMaintainanceMachineShutDownTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMaterialNotAvailable.Focus();
        }

        private void txtMaterialNotAvailable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPreformRejection.Focus();
        }

        private void txtPlanningQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTotalShot.Focus();
        }

        private void txtTotalShot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRejectInNos.Focus();
        }

        private void txtRejectInNos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSaveOEE.Focus();
        }

        private void ButtonClickMain(object sender, EventArgs e)
        {
            ButtonClick_Event(sender);
        }

        static int MachineNo, MachineId; int TempNo = 0, NewId = 0, Test = 0;

        private void Machine_OEE_Values()
        {
            MachineId = Convert.ToInt32(Convert.ToInt32(cmbMachineNo.SelectedValue));
            MachineNo = Convert.ToInt32(Convert.ToInt32(cmbMachineNo.Text));
            Clear_Production_Planning();

            //ClearProductPlanning();
            //Get_OEEMachine();
            //Get_SupervisorId();
        }

        private void ButtonClick_Event(object sender)
        {
            // ClearID();
            //foreach (var button in this.Controls.OfType<System.Windows.Forms.Button>())
            //foreach (var button in this.gbMachineNo.Controls.OfType<System.Windows.Forms.Button>())
            ////this.gbMachineNo.Controls.OfType<System.Windows.Forms.Button>()
            //{
            //    button.BackColor = objDL.GetBackgroundColor();
            //    button.ForeColor = objDL.GetForeColor();
            //}
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Lime;
            ctrl.ForeColor = Color.Black;
            
            MachineNo = Convert.ToInt32(ctrl.Text);
            //ClearProductPlanning();
           // Get_OEEMachine();
            Get_SupervisorId();
        }

        int[] SupervisorId_A;


        private void Get_SupervisorId()
        {
            SupervisorId_A = null;
            DataSet ds = new DataSet();
            //objBL.Query = "select OEM.SupervisorId from OEEEntryMachine OEM inner join OEEEntry OEE on OEE.ID=OEM.OEEEntryId where OEE.CancelTag=0 and OEM.CancelTag=0 and OEE.Shift='" + cmbShift.Text + "' and OEM.EntryDate=#" + dtpDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and OEM.MachineNo NOT IN(" + MachineNo + ")";
            objBL.Query = "select OEM.EmployeeId from OEEEmployee OEM inner join OEE OEE on OEE.OEEId=OEM.OEEId where OEE.CancelTag=0 and OEM.CancelTag=0 and OEE.ShiftId=" + ShiftId + " and OEE.EntryDate=#" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and OEM.OEEMachineId NOT IN(" + OEEMachineId + ") and OEM.OEEType='Supervisor'";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                SupervisorId_A = new int[ds.Tables[0].Rows.Count];

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SupervisorId_A[i] = Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            //objRL.Fill_Employee_ComboBox_Supervisor_Not_In_Id(cmbSupervisor, SupervisorId_A);

        }

        int ProductId = 0; string ProductName = string.Empty;

        private void Get_Product_Information()
        {
            if (ProductId != 0)
            {
                objRL.Get_Product_Records_By_Id(ProductId);
                ProductName = objRL.ProductName.ToString();
                lblProductName.Text = objRL.ProductName.ToString();
                //txtIdealRunRate.Text= MachineNo_Wise_IdealRunRate();
                IdealRunRate = Convert.ToDouble(MachineNo_Wise_IdealRunRate());

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Cavity)))
                    txtCavity.Text = objRL.Cavity.ToString();
                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Qty)))
                    txtPacking.Text = objRL.Qty.ToString();

                if (!string.IsNullOrEmpty(objRL.ProductType))
                {
                    if (objRL.ProductType == "Bottle")
                        lblProductName.BackColor = Color.Cyan;
                    else
                        lblProductName.BackColor = Color.Yellow;
                }
                //btnSwitch.Enabled = true;
            }
        }

        private string MachineNo_Wise_IdealRunRate()
        {
            string ReturnRate = "0";

            if (cmbMachineNo.SelectedIndex > -1)
            {

                //lblPacker.Visible = false;
                //cmbPacker.Visible = false;
                //lblPackerListAuto.Visible = true;
                //clbPackerAuto.Visible = true;
                //lblPreforLoadingAuto.Visible = true;
                //cmbPreformLoadingAuto.Visible = true;

                MachineNo = Convert.ToInt32(cmbMachineNo.Text);

                if (MachineNo < 10)
                {
                    //lblPacker.Visible = true;
                    //cmbPacker.Visible = true;
                    //lblPackerListAuto.Visible = false;
                    //clbPackerAuto.Visible = false;
                    //lblPreforLoadingAuto.Visible = false;
                    //cmbPreformLoadingAuto.Visible = false;

                    if (!string.IsNullOrEmpty(Convert.ToString(objRL.Semi)))
                        ReturnRate = objRL.Semi.ToString();
                }
                else if (MachineNo == 11 || MachineNo == 12)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(objRL.Auto1)))
                        ReturnRate = objRL.Auto1.ToString();
                }
                else if (MachineNo == 13)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(objRL.Auto2)))
                        ReturnRate = objRL.Auto2.ToString();
                }
                else if (MachineNo == 10 || MachineNo == 14)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(objRL.Servo)))
                        ReturnRate = objRL.Servo.ToString();
                }
                else
                    ReturnRate = "0";
            }
            return ReturnRate;
        }

        bool FlagQCEntryMachine = false, ProductSwitchFlag = false;

        int ProductionEntryId = 0, SwitchFlag = 0;

        private void Get_MachineWise_Details()
        {
            OEEMachineId = 0;
            //ClearProductPlanning();
            Clear_Production_Planning();

            if (cmbMachineNo.SelectedIndex > -1)
            {
                MachineId = Convert.ToInt32(cmbMachineNo.SelectedValue);
                DataSet ds = new DataSet();
                objBL.Query = "select OEEMachineId,EntryDate,EntryTime,OEEId,MachineId,ProductId,ShiftLengthA,ShortMealBreakB,NoPlanning,NoElectricity,TotalProductionTime,Breakdown,Changeover,ManpowerShortage,StartupLoss,MaintainanceMachineShutDownTime,MaterialNotAvailable,TotalDowntime,OperatingTime,Availabilty,IdealRunRate,Cavity,PlanningQty,TotalShot,TotalProductionInNos,Packing,TotalPacket,TargetProduction,Performance,PreformRejection,RejectInNos,GoodInNos,Quality,OEE,Status,Reason,ReasonForChange from OEEMachine where CancelTag=0 and OEEId=" + OEEId + " and MachineId=" + MachineId + "  order by OEEMachineId desc";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"])))
                    {
                        ProductId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductId"]);
                        Get_Product_Information();

                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["OEEMachineId"])))
                            OEEMachineId = Convert.ToInt32(ds.Tables[0].Rows[0]["OEEMachineId"]);

                        OEEId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["OEEId"])));

                       // Shift = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Shift"]));
                        txtShiftLength.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["ShiftLengthA"]));
                        txtShortMealBreakB.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["ShortMealBreakB"]));
                        txtNoPlanning.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["NoPlanning"]));
                        txtNoElectricity.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["NoElectricity"]));
                        txtTotalProductionTime.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["TotalProductionTime"]));
                        txtBreakdown.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Breakdown"]));
                        txtChangeover.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Changeover"]));
                        txtManpowerShortage.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["ManpowerShortage"]));
                        txtStartupLoss.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["StartupLoss"]));
                        txtMaintainanceMachineShutDownTime.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["MaintainanceMachineShutDownTime"]));
                        txtMaterialNotAvailable.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["MaterialNotAvailable"]));
                        txtTotalDowntime.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["TotalDowntime"]));
                        txtOperatingTime.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["OperatingTime"]));
                        txtAvailabilty.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Availabilty"]));
                        txtIdealRunRate.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["IdealRunRate"]));
                        txtCavity.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Cavity"]));
                        txtPlanningQty.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["PlanningQty"]));
                        txtTotalShot.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["TotalShot"]));
                        txtTotalProductionInNos.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["TotalProductionInNos"]));
                        txtPacking.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Packing"]));
                        txtTotalPacket.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["TotalPacket"]));
                        txtTargetProduction.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["TargetProduction"]));
                        txtPerformance.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Performance"]));
                        txtPreformRejection.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformRejection"]));
                        txtRejectInNos.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["RejectInNos"]));
                        txtGoodInNos.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["GoodInNos"]));
                        txtQuality.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Quality"]));
                        txtOEE.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["OEE"]));
                        cmbStatus.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Status"]));
                        cmbReason.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Reason"]));
                        txtRemarks.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["ReasonForChange"]));

                        //Super
                        OEEType = "Supervisor";
                        Get_SupervisorId();
                        //objRL.Fill_Employee_ComboBox_Supervisor_Not_In_Id(cmbSupervisor, SupervisorId_A);

                        //cmbSupervisor.Text = GetEmployee();

                        if (objRL.MachineDescription == "Auto")
                        {
                            OEEType = "PreformLoader";
                            //cmbPreformLoadingAuto.Text = GetEmployee();

                            OEEType = "Operator";
                            //Fill_CheckListBox(clbPackerAuto);
                        }
                        else
                        {
                            OEEType = "Operator";
                            //cmbOperator.Text = GetEmployee();

                            OEEType = "Packer";
                            //cmbPacker.Text = GetEmployee();
                        }
                    }
                }
                else
                {
                    txtSearchProductName.Focus();
                }
            }
        }

        //private void Get_OEEMachine()
        //{
        //    DataSet ds = new DataSet();
        //    objBL.Query = "select ID,EntryDate,EntryTime,ProductionEntryId,OEEEntryId,MachineNo,ProductId,SupervisorId,OperatorId,PackerId,PackerId1,PackerId2,PreformLoadingId,MouldChangerId,MouldChangerId1,ShiftLengthA,ShortMealBreakB,NoPlanning,NoElectricity,TotalProductionTime,Breakdown,Changeover,ManpowerShortage,StartupLoss,MaintainanceMachineShutDownTime,MaterialNotAvailable,TotalDowntime,OperatingTime,Availabilty,IdealRunRate,Cavity,PlanningQty,TotalShot,TotalProductionInNos,Packing,TotalPacket,TargetProduction,Performance,RejectInNos,GoodInNos,Quality,OEE,SwitchFlag,SwitchPackerMachineId,SwitchOperatiorMachineId,Reason,Status,SwitchNote,PreformRejection from OEEEntryMachine where CancelTag=0 and OEEEntryId=" + OEEEntryId + " and MachineNo=" + MachineNo + " order by ID desc";
        //    ds = objBL.ReturnDataSet();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
        //        {
        //            OEEEntryMachineId = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["ID"]));
        //            FlagQCEntryMachine = true;
        //        }
        //        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ProductionEntryId"])))
        //        {
        //            ProductionEntryId = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["ProductionEntryId"]));
        //            FlagQCEntryMachine = true;
        //        }

        //        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SwitchFlag"])))
        //        {
        //            SwitchFlag = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["SwitchFlag"]));
        //            if (SwitchFlag == 1)
        //            {
        //                //btnSwitch.Enabled = false;
        //                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Reason"])))
        //                    objRL.Reason = Convert.ToString(ds.Tables[0].Rows[0]["Reason"]);
        //                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ReasonInDetails"])))
        //                    objRL.ReasonInDetails = Convert.ToString(ds.Tables[0].Rows[0]["ReasonInDetails"]);
        //                ProductSwitchFlag = true;
        //            }
        //        }
                
        //        gbProductionPlanning.Enabled = true;

        //        //ID,EntryDate,EntryTime,ProductionEntryId,OEEEntryId,MachineNo,ProductId,SupervisorId,OperatorId,PackerId,PackerId1,PackerId2,PreformLoadingId,MouldChangerId,MouldChangerId1,ShiftLengthA,ShortMealBreakB,NoPlanning,NoElectricity,TotalProductionTime,Breakdown,Changeover,ManpowerShortage,StartupLoss,MaintainanceMachineShutDownTime,MaterialNotAvailable,TotalDowntime,OperatingTime,Availabilty,IdealRunRate,Cavity,PlanningQty,TotalShot,TotalProductionInNos,Packing,TotalPacket,TargetProduction,Performance,RejectInNos,GoodInNos,Quality,OEE,SwitchFlag,SwitchPackerMachineId,SwitchOperatiorMachineId,Reason,Status,SwitchNote,PreformRejection

        //        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SupervisorId"].ToString())))
        //        {
        //            //cmbSupervisor.Text = objRL.Get_Employee_Name_By_Id(Convert.ToInt32(ds.Tables[0].Rows[0]["SupervisorId"].ToString()));
        //        }
        //        if (MachineNo < 9)
        //        {
        //            //if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["OperatorId"].ToString())))
        //            //{
        //            //    cmbOperator.Text = objRL.Get_Employee_Name_By_Id(Convert.ToInt32(ds.Tables[0].Rows[0]["OperatorId"].ToString()));
        //            //}
        //            //if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PackerId"].ToString())))
        //            //{
        //            //    cmbPacker.Text = objRL.Get_Employee_Name_By_Id(Convert.ToInt32(ds.Tables[0].Rows[0]["PackerId"].ToString()));
        //            //}
        //        }
        //        else
        //        {
        //            //if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PreformLoadingId"].ToString())))
        //            //{
        //            //    if (Convert.ToInt32(ds.Tables[0].Rows[0]["PreformLoadingId"].ToString()) != 0)
        //            //    {
        //            //        cmbPreformLoadingAuto.Text = objRL.Get_Employee_Name_By_Id(Convert.ToInt32(ds.Tables[0].Rows[0]["PreformLoadingId"].ToString()));
        //            //    }
        //            //}
        //        }
        //    }
        //    else
        //    {
        //        //Get_ProductEntry();
        //        Get_Product_Information();
        //        FlagQCEntryMachine = false;
        //    }
        //}

        //private void Get_ProductEntry()
        //{
        //    ProductSwitchFlag = false;
        //    DataSet ds = new DataSet();
        //    objBL.Query = "select PE.ID,PE.EntryDate,PE.EntryTime,PE.Shift,PE.MachinNo,PE.ProductId,PE.PurchaseOrderNo,PE.ProductQty,PE.StickerHeader,PE.DateFlag,PE.PreformPartyId,PPM.PreformParty from ProductionEntry PE inner join PreformPartyMaster PPM on PPM.ID=PE.PreformPartyId where PE.CancelTag=0 and PE.MachinNo='" + MachineNo + "' and PE.Shift='" + cmbShift.Text + "' and PE.EntryDate=#" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# order by PE.ID desc";
        //    ds = objBL.ReturnDataSet();

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (!ProductSwitchFlag)
        //        {
        //            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
        //            {
        //                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
        //                {
        //                    ProductionEntryId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
        //                    //txtProductionEntryId.Text = ProductionEntryId.ToString();
                            
        //                    gbProductionPlanning.Enabled = true;
        //                }
        //                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"])))
        //                {
        //                    ProductId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductId"]);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
                
        //        gbProductionPlanning.Enabled = false;
        //        objRL.ShowMessage(37, 4);
        //        return;
        //    }
        //}

        private void btnClear_Click(object sender, EventArgs e)
        {
            OEEId = 0;
            ClearAll();
        }

        private void Clear_CheckListBox(CheckedListBox clb)
        {
            for (int i = 0; i < clb.Items.Count; i++)
            {
                clb.SetItemChecked(i, false);
            }
        }

        private void ClearAll()
        {
            objEP.Clear();
            //OEEId = 0;
             
            GridFlag = false;
            FlagDelete = false;
            txtOEEId.Text = "";
            txtShift.Text = "";
            txtShiftHours.Text = "";

            //OEEId = 0;
           
            //cmbShift.SelectedIndex = -1;
            //cmbPlantIncharge.SelectedIndex = -1;
            //cmbVolumeChecker.SelectedIndex = -1;
           
         
            //cmbShift.SelectedIndex = -1;
            //cmbShiftHours.SelectedIndex = -1;

            //gbMachineNo.Enabled = false;
            lblProductName.Text = "";
            //Clear_CheckListBox(clbPorter);
            //Clear_CheckListBox(clbMouldChanger);
            //Clear_CheckListBox(clbPackerAuto);
            //Clear_CheckListBox(clbMouldChangerChange);
            GetID();
            //ClearProductPlanning();
            Clear_Production_Planning();
            //cmbShiftHours.SelectedIndex = -1;
            //cmbShift.SelectedIndex = -1;
            Clear_Variables();
            Clear_Machine_Details();
            ClearAll_Item();
            ClearAll_OperatorPacker();
            ClearAll_Porter();
            ClearAll_Mold();
            //pChangeProduct.Visible = false;
            
            dtpEntryDate.Value = DateTime.Now.Date;
            dtpEntryTime.Value = DateTime.Now;
            dtpShiftDate.Value = DateTime.Now;

            //GetShiftDetails();
        }

        //private void ClearProductPlanning()
        //{
        //    if(!GridFlag)
        //        OEEMachineId = 0;

        //    //OEEEntryId = 0;
            
        //    //if (SwitchFlag == 0)
        //    //{
        //    //    //cmbSupervisor.SelectedIndex = -1;
        //    //    cmbOperator.SelectedIndex = -1;
        //    //    cmbPacker.SelectedIndex = -1;
        //    //    cmbPreformLoadingAuto.SelectedIndex = -1;
        //    //}

        //    txtShiftLength.Text = txtShiftHours.Text;
        //    txtShortMealBreakB.Text = "";
        //    txtNoPlanning.Text = "";
        //    txtNoElectricity.Text = "";
        //    txtTotalProductionTime.Text = "";
        //    txtBreakdown.Text = "";
        //    txtChangeover.Text = "";
        //    txtManpowerShortage.Text = "";
        //    txtStartupLoss.Text = "";
        //    txtMaintainanceMachineShutDownTime.Text = "";
        //    txtMaterialNotAvailable.Text = "";
        //    txtTotalDowntime.Text = "";
        //    txtOperatingTime.Text = "";
        //    txtAvailabilty.Text = "";
        //    txtIdealRunRate.Text = "";
        //    txtCavity.Text = "";
        //    txtPlanningQty.Text = "";
        //    txtTotalShot.Text = "";
        //    txtTotalProductionInNos.Text = "";
        //    txtPacking.Text = "";
        //    txtTotalPacket.Text = "";
        //    txtTargetProduction.Text = "";
        //    txtPerformance.Text = "";
        //    txtPreformRejection.Text = "";
        //    txtRejectInNos.Text = "";
        //    txtGoodInNos.Text = "";
        //    txtQuality.Text = "";
        //    txtOEE.Text = "";
        //    cmbStatus.SelectedIndex = -1;
        //    cmbReason.SelectedIndex = -1;
        //    txtNaration.Text = "";
        //}

        private void ClearAllSwitch()
        {
            SwitchFlag = 0;
            SwitchNote = "";
            cmbReason.SelectedIndex = -1;
            //txtNaration.Text = "";
            //pChangeProduct.Visible = false;
        }


        //int OEEEntryId = 0, OEEEntryMachineId = 0, 
            int Result = 0;
        int PorterId = 0;
        string PorterName = string.Empty;

        //   int MouldChangerId = 0;
        string MouldChangerName = string.Empty;

        //private void SavePorter()
        //{
        //    objBL.Query = "delete from OEEEntryPorter where OEEEntryId=" + OEEEntryId + " and CancelTag=0";
        //    objBL.Function_ExecuteNonQuery();

        //    foreach (object itemChecked in clbPorter.CheckedItems)
        //    {
        //        DataRowView castedItem = itemChecked as DataRowView;
        //        int? id = Convert.ToInt32(castedItem["ID"]);
        //        PorterId = (int)id;
        //        PorterName = castedItem["FullName"].ToString();
        //        objBL.Query = "insert into OEEEntryPorter(OEEEntryId,PorterId,UserId) values(" + OEEEntryId + "," + PorterId + "," + BusinessLayer.UserId_Static + ")";
        //        objBL.Function_ExecuteNonQuery();
        //    }
        //}

        private void SaveMouldChanger()
        {
            MouldChangerId = 0;
            MouldChangerName = string.Empty;

            //objBL.Query = "delete from OEEEntryMouldChanger where OEEEntryId=" + OEEEntryId + " and CancelTag=0";
            //objBL.Function_ExecuteNonQuery();

            //foreach (object itemChecked in clbMouldChanger.CheckedItems)
            //{
            //    DataRowView castedItem = itemChecked as DataRowView;
            //    int? id = Convert.ToInt32(castedItem["ID"]);
            //    MouldChangerId = (int)id;
            //    MouldChangerName = castedItem["FullName"].ToString();
            //    objBL.Query = "insert into OEEEntryMouldChanger(OEEEntryId,MouldChangerId,UserId) values(" + OEEEntryId + "," + MouldChangerId + "," + BusinessLayer.UserId_Static + ")";
            //    objBL.Function_ExecuteNonQuery();
            //}
        }

        //private void SaveDB_OEEEntry()
        //{
        //    SaveDB_OEEMachine();

        //    if (!Validation())
        //    {

        //        if (OEEEntryId == 0)
        //            objBL.Query = "insert into OEEEntry(EntryDate,EntryTime,Shift,UserId) values('" + dtpShiftDate.Value.ToShortDateString() + "','" + dtpShiftDate.Value.ToShortTimeString() + "','"+txtShift.Text+"'," + BusinessLayer.UserId_Static + ")";
        //        else
        //            objBL.Query = "update OEEEntry set EntryDate='" + dtpShiftDate.Value.ToShortDateString() + "',EntryTime='" + dtpShiftDate.Value.ToShortTimeString() + "',Shift='" + txtShift.Text + "',ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + OEEEntryId + " and CancelTag=0";

        //        Result = objBL.Function_ExecuteNonQuery();

        //        if (Result > 0)
        //        {
        //            if (OEEEntryId == 0)
        //                OEEEntryId = objRL.ReturnMaxID_Fix("OEEEntry", "ID");

        //            //SaveMouldChanger();
        //            //SavePorter();
        //            //SaveDB_OEEEntryMachine();
        //            objRL.ShowMessage(7, 1);
        //        }
        //    }
        //    else
        //    {
        //        objRL.ShowMessage(14, 4);
        //        return;
        //    }
        //}

        private bool Validation_OEEEntryMachine()
        {
            if (txtShiftLength.Text == "")
            {
                objEP.SetError(txtShiftLength, "Select Shift Length A");
                txtShiftLength.Focus();
                return true;
            }
            else if (cmbStatus.SelectedIndex == -1)
            {
                objEP.SetError(cmbStatus, "Select  Status");
                cmbStatus.Focus();
                return true;
            }
            else
                return false;

        }

        string Reason = "", Status = "", SwitchNote = "";
        int SupervisorId = 0, OperatorId = 0, PackerId = 0, PackerId1 = 0, PackerId2 = 0, PreformLoadingId = 0, MouldChangerId = 0, MouldChangerId1 = 0, SwitchPackerMachineId = 0, SwitchOperatiorMachineId = 0;

        int OEEMachineId = 0;

        //private void SaveDB_OEEMachine1()
        //{
        //    if (!Validation())
        //    {
        //        if (OEEMachineId == 0)
        //            objBL.Query = "insert into OEEMachine(EntryDate,EntryTime,OEEId,Shift,MachineId,ProductId,ShiftLengthA,ShortMealBreakB,NoPlanning,NoElectricity,TotalProductionTime,Breakdown,Changeover,ManpowerShortage,StartupLoss,MaintainanceMachineShutDownTime,MaterialNotAvailable,TotalDowntime,OperatingTime,Availabilty,IdealRunRate,Cavity,PlanningQty,TotalShot,TotalProductionInNos,Packing,TotalPacket,TargetProduction,Performance,RejectInNos,GoodInNos,Quality,OEE,SwitchFlag,SwitchPackerMachineId,SwitchOperatiorMachineId,Status,Reason,SwitchNote,PreformRejection,UserId) values('" + dtpShiftDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "'," + OEEId + ",'" + cmbShift.Text + "'," + MachineId + "," + ProductId + ",'" + ShiftLengthA + "','" + ShortMealBreakB + "','" + NoPlanning + "','" + NoElectricity + "','" + TotalProductionTime + "','" + Breakdown + "','" + Changeover + "','" + ManpowerShortage + "','" + StartupLoss + "','" + MaintainanceMachineShutDownTime + "','" + MaterialNotAvailable + "','" + TotalDowntime + "','" + OperatingTime + "','" + Availabilty + "','" + IdealRunRate + "','" + Cavity + "','" + PlanningQty + "','" + TotalShot + "','" + TotalProductionInNos + "','" + Packing + "','" + TotalPacket + "','" + TargetProduction + "','" + Performance + "','" + RejectInNos + "','" + GoodInNos + "','" + Quality + "','" + OEE + "'," + SwitchFlag + "," + SwitchPackerMachineId + "," + SwitchOperatiorMachineId + ",'" + Status + "','" + Reason + "','" + SwitchNote + "','" + PreformRejection + "'," + BusinessLayer.UserId_Static + ")";
        //        else
        //        {
        //            objBL.Query = "update OEEEntryMachine set EntryDate= '" + dtpShiftDate.Value.ToShortDateString() + "',EntryTime='" + dtpTime.Value.ToShortTimeString() + "',OEEId=" + OEEId + ",Shift='" + cmbShift.Text + "',MachineId=" + MachineId + ",ProductId=" + ProductId + ",ShiftLengthA='" + ShiftLengthA + "', " +
        //        "ShortMealBreakB='" + ShortMealBreakB + "',NoPlanning='" + NoPlanning + "',NoElectricity='" + NoElectricity + "',TotalProductionTime= '" + TotalProductionTime + "',Breakdown= '" + Breakdown + "'," +
        //        "Changeover='" + Changeover + "',ManpowerShortage='" + ManpowerShortage + "',StartupLoss='" + StartupLoss + "',MaintainanceMachineShutDownTime=,'" + MaintainanceMachineShutDownTime + "'," +
        //        "MaterialNotAvailable= '" + MaterialNotAvailable + "',TotalDowntime='" + TotalDowntime + "',OperatingTime='" + OperatingTime + "',Availabilty='" + Availabilty + "',IdealRunRate='" + IdealRunRate + "'," +
        //        "Cavity='" + Cavity + "',PlanningQty= '" + PlanningQty + "',TotalShot='" + TotalShot + "',TotalProductionInNos='" + TotalProductionInNos + "',Packing='" + Packing + "',TotalPacket='" + TotalPacket + "',TargetProduction='" + TargetProduction + "'," +
        //        "Performance='" + Performance + "',RejectInNos='" + RejectInNos + "',GoodInNos='" + GoodInNos + "',Quality='" + Quality + "',OEE='" + OEE + "',SwitchFlag=" + SwitchFlag + ",SwitchPackerMachineId=" + SwitchPackerMachineId + "," +
        //        "SwitchOperatiorMachineId=" + SwitchOperatiorMachineId + ",Status='" + Status + "',Reason='" + Reason + "',SwitchNote='" + SwitchNote + "',PreformRejection='" + PreformRejection + "',ModifiedId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and OEEMachineId=" + OEEMachineId + "";
        //        }

        //        Result = objBL.Function_ExecuteNonQuery();

        //        if (Result > 0)
        //        {
        //            objRL.ShowMessage(7, 1);
        //        }
        //    }
        //    else
        //    {
        //        objRL.ShowMessage(14, 4);
        //        return;
        //    }
        //}

        private void FillGrid_MachineEntry()
        {
            MainQuery=string.Empty;
            WhereClause=string.Empty;
            OrderByClause=string.Empty;

            dgvMachine.DataSource = null;
            DataSet ds = new DataSet();
            //objBL.Query = "select OM.OEEMachineId,OM.EntryDate as [Date],Format([OM.EntryTime], 'HH:mm') AS [Time],OM.OEEId,OEE.ShiftDate,OEE.Shift,OEE.ShiftHours, Format([OEE.ShiftBeginTime], 'HH:mm') AS [Shift Begin Time],Format([OEE.ShiftEndTime], 'HH:mm') AS [Shift End Time],OM.MachineId,MM.MachineNo,OM.ProductId,P.ProductName as [Product Name],OM.ShiftLengthA,OM.ShortMealBreakB,OM.NoPlanning,OM.NoElectricity,OM.TotalProductionTime,OM.Breakdown,OM.Changeover,OM.ManpowerShortage,OM.StartupLoss,OM.MaintainanceMachineShutDownTime,OM.MaterialNotAvailable,OM.TotalDowntime,OM.OperatingTime,OM.Availabilty,OM.IdealRunRate,OM.Cavity,OM.PlanningQty,OM.TotalShot,OM.TotalProductionInNos,OM.Packing,OM.TotalPacket,OM.TargetProduction,OM.Performance,OM.PreformRejection,OM.RejectInNos,OM.GoodInNos,OM.Quality,OM.OEE,OM.Status,OM.SwitchFlag,OM.Reason,OM.ReasonForChange,OM.SwitchFlag from ((OEEMachine OM inner join OEE OEE on OEE.OEEId=OM.OEEId) inner join MachineMaster MM on MM.MachineId=OM.MachineId) inner join Product P on P.ID=OM.ProductId where P.CancelTag=0 and OM.CancelTag=0 and OEE.CancelTag=0 and MM.CancelTag=0 and OM.OEEId=" + OEEId + " and OM.MachineId=" + MachineId + " ";
            //objBL.Query = "select OM.OEEMachineId,OM.EntryDate as [Date],Format([OM.EntryTime], 'HH:mm') AS [Time],OM.OEEId,OEE.ShiftDate,OEE.Shift,OEE.ShiftHours, Format([OEE.ShiftBeginTime], 'HH:mm') AS [Shift Begin Time],Format([OEE.ShiftEndTime], 'HH:mm') AS [Shift End Time],OM.MachineId,MM.MachineNo,OM.ProductId,P.ProductName as [Product Name],OM.ShiftLengthA,OM.ShortMealBreakB,OM.NoPlanning,OM.NoElectricity,OM.TotalProductionTime,OM.Breakdown,OM.Changeover,OM.ManpowerShortage,OM.StartupLoss,OM.MaintainanceMachineShutDownTime,OM.MaterialNotAvailable,OM.TotalDowntime,OM.OperatingTime,OM.Availabilty,OM.IdealRunRate,OM.Cavity,OM.PlanningQty,OM.TotalShot,OM.TotalProductionInNos,OM.Packing,OM.TotalPacket,OM.TargetProduction,OM.Performance,OM.PreformRejection,OM.RejectInNos,OM.GoodInNos,OM.Quality,OM.OEE,OM.Status,OM.SwitchFlag,OM.Reason,OM.ReasonForChange,OM.SwitchFlag from ((OEEMachine OM inner join OEE OEE on OEE.OEEId=OM.OEEId) inner join MachineMaster MM on MM.MachineId=OM.MachineId) inner join Product P on P.ID=OM.ProductId where P.CancelTag=0 and OM.CancelTag=0 and OEE.CancelTag=0 and MM.CancelTag=0 and OM.OEEId=" + OEEId + " and OM.MachineId=" + MachineId + " ";
            // and OM.MachineId=" + MachineId + "

            //MainQuery = "select OM.OEEMachineId as [OEE Machine No],OM.EntryDate as [Date],Format([OM.EntryTime], 'HH:mm') AS [Time],OM.OEEId,OM.ShiftId,SEN.EntryDate as [Shift Date],Format([SEN.ShifFromDate], 'HH:mm') AS [Shift Start],Format([SEN.ShiftToDate], 'HH:mm') AS [Shift End],SEN.Shift,SEN.ShiftHours as [Shift Hours],OM.MachineId,MM.MachineNo,OM.ProductId,P.ProductName as [Product Name],OM.ShiftLengthA,OM.MachineAvailableTime,OM.BalanceLength,OM.ShortMealBreakB,OM.NoPlanning,OM.NoElectricity,OM.TotalProductionTime,OM.Breakdown,OM.Changeover,OM.ManpowerShortage,OM.StartupLoss,OM.MaintainanceMachineShutDownTime,OM.MaterialNotAvailable,OM.TotalDowntime,OM.OperatingTime,OM.Availabilty,OM.IdealRunRate,OM.Cavity,OM.PlanningQty,OM.TotalShot,OM.TotalProductionInNos,OM.Packing,OM.TotalPacket,OM.TargetProduction,OM.Performance,OM.PreformRejection,OM.RejectInNos,OM.GoodInNos,OM.Quality,OM.OEE,OM.Status,OM.SwitchFlag,OM.Reason,OM.ReasonForChange from (((OEEMachine OM inner join OEE OEE on OEE.OEEId=OM.OEEId) inner join MachineMaster MM on MM.MachineId=OM.MachineId) inner join Product P on P.ID=OM.ProductId) inner join ShiftEntryNew SEN on SEN.ID=OM.ShiftId where SEN.CancelTag=0 and P.CancelTag=0 and OM.CancelTag=0 and OEE.CancelTag=0 and MM.CancelTag=0 and OM.OEEId=" + OEEId + " ";
            MainQuery = "select OM.OEEMachineId as [OEE Machine No],OM.EntryDate as [Date],Format([OM.EntryTime], 'HH:mm') AS [Time],OM.OEEId as [OEE No],OM.ShiftId,SEN.EntryDate as [Shift Date],Format([SEN.ShifFromDate], 'HH:mm') AS [Shift Start],Format([SEN.ShiftToDate], 'HH:mm') AS [Shift End],SEN.Shift,SEN.ShiftHours as [Shift Hours],OM.MachineId,MM.MachineNo,OM.ProductId,P.ProductName as [Product Name],OM.ShiftLengthA as [Shift Lenght(A)],OM.MachineAvailableTime as [Machine Available Time],OM.BalanceLength as [Balance Length],OM.ShortMealBreakB as [Short Meal Break(B)],OM.NoPlanning as [No Planning],OM.NoElectricity as [No Electricity],OM.TotalProductionTime as [Total Production Time],OM.Breakdown,OM.Changeover,OM.ManpowerShortage as [Manpower Shortage],OM.StartupLoss as [Startup Loss],OM.MaintainanceMachineShutDownTime as [Maintainance Machine Shut Down Time],OM.MaterialNotAvailable as [Material Not Available],OM.TotalDowntime as [Total Downtime],OM.OperatingTime as [Operating Time],OM.Availabilty,OM.IdealRunRate as [Ideal Run Rate],OM.Cavity,OM.PlanningQty as [Planning Qty],OM.TotalShot as [Total Shot],OM.TotalProductionInNos as [Total Production In Nos],OM.Packing,OM.TotalPacket as [Total Packet],OM.TargetProduction as [Target Production],OM.Performance,OM.PreformRejection as [Preform Rejection],OM.RejectInNos as [Reject In Nos],OM.GoodInNos as [Good In Nos],OM.Quality,OM.OEE,OM.Status,OM.Reason,OM.Remarks from (((OEEMachine OM inner join OEE OEE on OEE.OEEId=OM.OEEId) inner join MachineMaster MM on MM.MachineId=OM.MachineId) inner join Product P on P.ID=OM.ProductId) inner join ShiftEntryNew SEN on SEN.ID=OM.ShiftId where SEN.CancelTag=0 and P.CancelTag=0 and OM.CancelTag=0 and OEE.CancelTag=0 and MM.CancelTag=0 and OM.OEEId=" + OEEId + " ";
            OrderByClause = " order by OM.OEEMachineId desc ";

            if (SearchProduct)
                WhereClause = " and P.ProductName like '%" + txtSearchProduct.Text + "%'";

            objBL.Query = MainQuery + WhereClause + OrderByClause;

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dgvMachine.DataSource = ds.Tables[0];

                lblTotalCountOEE.Text = "Total :" + ds.Tables[0].Rows.Count;

                dgvMachine.Columns[1].Visible = false;
                dgvMachine.Columns[2].Visible = false;
                //dgvMachine.Columns[3].Visible = false;
                dgvMachine.Columns[4].Visible = false;

                dgvMachine.Columns[5].Visible = false;
                dgvMachine.Columns[7].Visible = false;
                dgvMachine.Columns[8].Visible = false;
                dgvMachine.Columns[9].Visible = false;

                dgvMachine.Columns[10].Visible = false;
                dgvMachine.Columns[12].Visible = false;
                dgvMachine.Columns[41].Visible = false;
                //dgvMachine.Columns[45].Visible = false;
                //dgvMachine.Columns[48].Visible = false;

                for (int i = 0; i < dgvMachine.Columns.Count; i++)
                {
                    dgvMachine.Columns[i].Width = 120;
                }

                dgvMachine.Columns[25].Width = 200;
                dgvMachine.Columns[13].Width = 300;

                //0 OM.OEEMachineId,
                //1 OM.EntryDate as [Date],
                //2 Format([OM.EntryTime], 'HH:mm') AS [Time],
                //3 OM.OEEId,
                //4 OM.ShiftId,
                //5 SEN.EntryDate as [Shift Date],
                //6 SEN.ShifFromDate as [Shift Start],
                //7 SEN.ShiftToDate as [Shift End],
                //8 SEN.Shift,
                //9 SEN.ShiftHours as [Shift Hours],
                //10 OM.MachineId,
                //11 MM.MachineNo,
                //12 OM.ProductId,
                //13 P.ProductName as [Product Name],
                //14 OM.ShiftLengthA,
                //15 OM.MachineAvailableTime,
                //16 OM.BalanceLength
                //17 OM.ShortMealBreakB,
                //18 OM.NoPlanning,
                //19 OM.NoElectricity,
                //20 OM.TotalProductionTime,
                //21 OM.Breakdown,
                //22 OM.Changeover,
                //23 OM.ManpowerShortage,
                //24 OM.StartupLoss,
                //25 OM.MaintainanceMachineShutDownTime,
                //26 OM.MaterialNotAvailable,
                //27 OM.TotalDowntime,
                //28 OM.OperatingTime,
                //29 OM.Availabilty,
                //30 OM.IdealRunRate,
                //31 OM.Cavity,
                //32 OM.PlanningQty,
                //33 OM.TotalShot,
                //34 OM.TotalProductionInNos,
                //35 OM.Packing,
                //36 OM.TotalPacket,
                //37 OM.TargetProduction,
                //38 OM.Performance,
                //39 OM.PreformRejection,
                //40 OM.RejectInNos,
                //41 OM.GoodInNos,
                //42 OM.Quality,
                //43 OM.OEE,
                //44 OM.Status,
                //45 OM.Reason,
                //46 OM.Remarks
                 
                
                //49 OM.ShiftId,
                //OM.MachineAvailableTime,OM.BalanceLength


            }
        }


        private void Clear_Variables()
        {
            BalanceLength = 0;
            ShiftLength = 0;
            BalanceLength = 0;
            MachineAvailableTime = 0;
            ShortMealBreakB = 0;
            NoPlanning = 0;
            NoElectricity = 0;
            TotalProductionTime = 0;

            Breakdown = 0;
            Changeover = 0;
            ManpowerShortage = 0;
            StartupLoss = 0;
            MaintainanceMachineShutDownTime = 0;
            MaterialNotAvailable = 0;
            TotalDowntime = 0;
            OperatingTime = 0;
            Availabilty = 0;

            IdealRunRate = 0;
            Cavity = 0;
            PlanningQty = 0;
            TotalShot = 0;
            TotalProductionInNos = 0;
            Packing = 0;
            TotalPacket = 0;
            TargetProduction = 0;

            Performance = 0;
            PreformRejection = 0;
            RejectInNos = 0;
            GoodInNos = 0;           
            Quality=0;
            OEE = 0;
        }

        private void Clear_Machine_Details()
        {
            cmbMachineNo.SelectedIndex = -1;
            lblMachineDetails.Text = "";
            cmbStatus.SelectedIndex = -1;
        }

        private void Clear_Production_Planning()
        {
            if (!GridFlag)
                OEEMachineId = 0;

            //OEEMachineId = 0;

            //cmbMachineNo.SelectedIndex = -1;
            //lblMachineDetails.Text = "";
            //cmbStatus.SelectedIndex = -1;
            cmbReason.SelectedIndex = -1;
            txtRemarks.Text = "";

            ClearAll_Item();
            ProductId = 0;
            txtSearchProductName.Text = "";
            lblProductName.Text = "";

            txtRemarks.Text = "";
            
            txtShiftLength.Text = txtShiftHours.Text;
            txtBalanceLength.Text = "";
            txtMachineAvailableTime.Text = "";
            txtShortMealBreakB.Text = "";
            txtNoPlanning.Text = "";
            txtNoElectricity.Text = "";
            txtTotalProductionTime.Text = "";
           
            txtBreakdown.Text = "";
            txtChangeover.Text = "";
            txtManpowerShortage.Text = "";
            txtStartupLoss.Text = "";
            txtMaintainanceMachineShutDownTime.Text = "";
            txtMaterialNotAvailable.Text = "";
            txtTotalDowntime.Text = "";
            txtOperatingTime.Text = "";
            txtAvailabilty.Text = "";

            txtIdealRunRate.Text = "";
            txtCavity.Text = "";
            txtPlanningQty.Text = "";
            txtTotalShot.Text = "";
            txtTotalProductionInNos.Text = "";
            txtPacking.Text = "";
            txtTotalPacket.Text = "";
            txtTargetProduction.Text = "";
            txtPerformance.Text = "";

            txtPreformRejection.Text = "";
            txtRejectInNos.Text = "";
            txtGoodInNos.Text = "";
            txtQuality.Text = "";
            txtOEE.Text = "";

            txtSearchProduct.Text = "";
        }

        private void SaveDB_OEEMachine()
        {
            if (!Validation_OEEMachine())
            {
                //OEEMachineId = 0;

                BalanceLength = ShiftLength - MachineAvailableTime;
                MachineId= Convert.ToInt32(cmbMachineNo.SelectedValue);
                
                if (OEEMachineId == 0)
                    objBL.Query = "insert into OEEMachine(EntryDate,EntryTime,OEEId,MachineId,ProductId,ShiftLengthA,ShortMealBreakB,NoPlanning,NoElectricity,TotalProductionTime,Breakdown,Changeover,ManpowerShortage,StartupLoss,MaintainanceMachineShutDownTime,MaterialNotAvailable,TotalDowntime,OperatingTime,Availabilty,IdealRunRate,Cavity,PlanningQty,TotalShot,TotalProductionInNos,Packing,TotalPacket,TargetProduction,Performance,PreformRejection,RejectInNos,GoodInNos,Quality,OEE,Status,Reason,Remarks,ShiftId,MachineAvailableTime,BalanceLength,UserId) values('" + dtpEntryDate.Value.ToShortDateString() + "','" + dtpEntryTime.Value.ToShortTimeString() + "'," + OEEId + "," + MachineId + "," + ProductId + ",'" + ShiftLength + "','" + ShortMealBreakB + "','" + NoPlanning + "','" + NoElectricity + "','" + TotalProductionTime + "','" + Breakdown + "','" + Changeover + "','" + ManpowerShortage + "','" + StartupLoss + "','" + MaintainanceMachineShutDownTime + "','" + MaterialNotAvailable + "','" + TotalDowntime + "','" + OperatingTime + "','" + Availabilty + "','" + IdealRunRate + "','" + Cavity + "','" + PlanningQty + "','" + TotalShot + "','" + TotalProductionInNos + "','" + Packing + "','" + TotalPacket + "','" + TargetProduction + "','" + Performance + "','" + PreformRejection + "','" + RejectInNos + "','" + GoodInNos + "','" + Quality + "','" + OEE + "','" + cmbStatus.Text + "','" + cmbReason.Text + "','" + txtRemarks.Text + "'," + ShiftId + ",'" + txtMachineAvailableTime.Text + "','" + BalanceLength + "'," + BusinessLayer.UserId_Static + ")";
                else
                {
                    if (!FlagDelete)
                    {
                        objBL.Query = "update OEEMachine set "+ 
                            "EntryDate='" + dtpEntryDate.Value.ToShortDateString() + "',"+
                            "EntryTime='" + dtpEntryTime.Value.ToShortTimeString() + "',"+
                            "MachineId=" + MachineId + ","+
                            "ProductId=" + ProductId + ","+
                            "ShiftLengthA='" + ShiftLength + "', " +
                            "ShortMealBreakB='" + ShortMealBreakB + "',"+
                            "NoPlanning='" + NoPlanning + "'," +
                            "NoElectricity='" + NoElectricity + "',"+
                            "TotalProductionTime= '" + TotalProductionTime + "',"+
                            "Breakdown= '" + Breakdown + "'," +
                            "Changeover='" + Changeover + "',"+
                            "ManpowerShortage='" + ManpowerShortage + "',"+
                            "StartupLoss='" + StartupLoss + "',"+
                            "MaintainanceMachineShutDownTime='" + MaintainanceMachineShutDownTime + "'," +
                            "MaterialNotAvailable= '" + MaterialNotAvailable + "',"+
                            "TotalDowntime='" + TotalDowntime + "',"+
                            "OperatingTime='" + OperatingTime + "',"+
                            "Availabilty='" + Availabilty + "'," +
                            "IdealRunRate='" + IdealRunRate + "'," +
                            "Cavity='" + Cavity + "',"+
                            "PlanningQty= '" + PlanningQty + "',"+
                            "TotalShot='" + TotalShot + "',"+
                            "TotalProductionInNos='" + TotalProductionInNos + "',"+
                            "Packing='" + Packing + "',"+
                            "TotalPacket='" + TotalPacket + "',"+
                            "TargetProduction='" + TargetProduction + "'," +
                            "Performance='" + Performance + "'," +
                            "PreformRejection='" + PreformRejection + "',"+
                            "RejectInNos='" + RejectInNos + "',"+
                            "GoodInNos='" + GoodInNos + "',"+
                            "Quality='" + Quality + "',"+
                            "OEE='" + OEE + "'," +
                            "Status='" + cmbStatus.Text + "',"+
                            "Reason='" + cmbReason.Text + "',"+
                            "Remarks='" + txtRemarks.Text + "',"+
                            "ShiftId=" + ShiftId + ","+
                            "MachineAvailableTime='" + txtMachineAvailableTime.Text + "'," +
                            "BalanceLength='" + BalanceLength + "',"+
                            "ModifiedId=" + BusinessLayer.UserId_Static + " "+
                            " where CancelTag=0 and OEEMachineId=" + OEEMachineId + "";
                    }
                    else
                    {
                        objBL.Query = "update OEEMachine set CancelTag=1 where OEEMachineId=" + OEEMachineId + " and CancelTag=0";
                    }
                }

                Result = objBL.Function_ExecuteNonQuery();

                if (Result > 0)
                {
                    FillGrid_MachineEntry();

                    if (!FlagDelete)
                        objRL.ShowMessage(7, 1);
                    else
                        objRL.ShowMessage(9, 1);

                    SwitchFlag = 0;
                    //ClearProductPlanning();
                    Clear_Production_Planning();
                    ClearAllSwitch();
                    GridFlag = false;
                    OEEMachineId = 0;

                    //Fill_Balance_Length();
                    //if (OEEMachineId == 0)
                    //    OEEMachineId = objRL.ReturnMaxID_Fix("OEEMachine", "OEEMachineId");

                    //MachineId = Convert.ToInt32(cmbMachineNo.SelectedValue);

                    //if (OEEMachineId != 0)
                    //{
                    //    //Supervisor
                    //    //OEEType = "Supervisor";
                    //    //Save_Employee_PlantIncaharge_VolumeChecker(cmbSupervisor);

                    //    if (objRL.MachineDescription == "Auto")
                    //    {
                    //        OEEType = "PreformLoader";
                    //        //Save_Employee_PlantIncaharge_VolumeChecker(cmbPreformLoadingAuto);
                    //        OEEType = "Packer";
                    //        //Save_Employee(clbPackerAuto);
                    //    }
                    //    else
                    //    {
                    //        //Operator
                    //        OEEType = "Operator";
                    //       // Save_Employee_PlantIncaharge_VolumeChecker(cmbOperator);
                    //        //Packer
                    //        OEEType = "Packer";
                    //        //Save_Employee_PlantIncaharge_VolumeChecker(cmbPacker);
                    //    }
                    //}
                   
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        //private void SaveDB_OEEEntryMachine()
        //{
        //    //if (cmbSupervisor.SelectedIndex > -1)
        //    //    SupervisorId = Convert.ToInt32(cmbSupervisor.SelectedValue);

        //    if (SwitchFlag == 1)
        //    {
        //        int i = 0, MouldCId = 0;
        //        //foreach (object itemChecked in clbMouldChanger.CheckedItems)
        //        //{
        //        //    DataRowView castedItem = itemChecked as DataRowView;
        //        //    int? id = Convert.ToInt32(castedItem["ID"]);
        //        //    MouldCId = (int)id;

        //        //    if (i < 2)
        //        //    {
        //        //        if (i == 0)
        //        //            MouldChangerId = MouldCId;
        //        //        if (i == 1)
        //        //            MouldChangerId1 = MouldCId;

        //        //        i++;
        //        //    }
        //        //}
        //    }

        //    if (MachineNo < 10)
        //    {
        //        if (cmbPacker.SelectedIndex > -1)
        //            PackerId = Convert.ToInt32(cmbPacker.SelectedValue);

        //        if (cmbOperator.SelectedIndex > -1)
        //            OperatorId = Convert.ToInt32(cmbOperator.SelectedValue);
        //    }
        //    else
        //    {
        //        if (cmbPreformLoadingAuto.SelectedIndex > -1)
        //            PreformLoadingId = Convert.ToInt32(cmbPreformLoadingAuto.SelectedValue);

        //        if (clbPackerAuto.CheckedItems.Count > 0)
        //        {
        //            int i = 0, PackId = 0;
        //            foreach (object itemChecked in clbPackerAuto.CheckedItems)
        //            {
        //                DataRowView castedItem = itemChecked as DataRowView;
        //                int? id = Convert.ToInt32(castedItem["ID"]);
        //                PackId = (int)id;

        //                if (i < 3)
        //                {
        //                    if (i == 0)
        //                        PackerId = PackId;
        //                    if (i == 1)
        //                        PackerId1 = PackId;
        //                    if (i == 2)
        //                        PackerId2 = PackId;
        //                    i++;
        //                }
        //            }
        //        }
        //    }

        //    if (OEEEntryMachineId == 0)
        //        objBL.Query = "insert into OEEEntryMachine(EntryDate,EntryTime,ProductionEntryId,OEEEntryId,MachineNo,ProductId,SupervisorId,OperatorId,PackerId,PackerId1,PackerId2,PreformLoadingId,MouldChangerId,MouldChangerId1,ShiftLengthA,ShortMealBreakB,NoPlanning,NoElectricity,TotalProductionTime,Breakdown,Changeover,ManpowerShortage,StartupLoss,MaintainanceMachineShutDownTime,MaterialNotAvailable,TotalDowntime,OperatingTime,Availabilty,IdealRunRate,Cavity,PlanningQty,TotalShot,TotalProductionInNos,Packing,TotalPacket,TargetProduction,Performance,RejectInNos,GoodInNos,Quality,OEE,SwitchFlag,SwitchPackerMachineId,SwitchOperatiorMachineId,Status,Reason,SwitchNote,PreformRejection,UserId) values('" + dtpEntryDate.Value.ToShortDateString() + "','" + dtpEntryTime.Value.ToShortTimeString() + "'," + ProductionEntryId + "," + OEEEntryId + ",'" + MachineNo + "'," + ProductId + "," + SupervisorId + "," + OperatorId + "," + PackerId + "," + PackerId1 + "," + PackerId2 + "," + PreformLoadingId + "," + MouldChangerId + "," + MouldChangerId1 + ",'" + ShiftLengthA + "','" + ShortMealBreakB + "','" + NoPlanning + "','" + NoElectricity + "','" + TotalProductionTime + "','" + Breakdown + "','" + Changeover + "','" + ManpowerShortage + "','" + StartupLoss + "','" + MaintainanceMachineShutDownTime + "','" + MaterialNotAvailable + "','" + TotalDowntime + "','" + OperatingTime + "','" + Availabilty + "','" + IdealRunRate + "','" + Cavity + "','" + PlanningQty + "','" + TotalShot + "','" + TotalProductionInNos + "','" + Packing + "','" + TotalPacket + "','" + TargetProduction + "','" + Performance + "','" + RejectInNos + "','" + GoodInNos + "','" + Quality + "','" + OEE + "'," + SwitchFlag + "," + SwitchPackerMachineId + "," + SwitchOperatiorMachineId + ",'" + Status + "','" + Reason + "','" + SwitchNote + "','" + PreformRejection + "'," + BusinessLayer.UserId_Static + ")";
        //    else
        //    {
        //        objBL.Query = "update OEEEntryMachine set EntryDate= '" + dtpShiftDate.Value.ToShortDateString() + "',EntryTime= '" + dtpEntryTime.Value.ToShortTimeString() + "',ProductionEntryId= " + ProductionEntryId + ", " +
        //   " OEEEntryId= " + OEEEntryId + ",MachineNo= '" + MachineNo + "',ProductId= " + ProductId + ",SupervisorId= " + SupervisorId + ",OperatorId=" + OperatorId + ",PackerId= " + PackerId + ",PackerId1= " + PackerId1 + ", " +
        //    "PackerId2=" + PackerId2 + ",PreformLoadingId=" + PreformLoadingId + ",MouldChangerId= " + MouldChangerId + ",MouldChangerId1=" + MouldChangerId1 + ",ShiftLengthA='" + ShiftLengthA + "', " +
        //    "ShortMealBreakB='" + ShortMealBreakB + "',NoPlanning='" + NoPlanning + "',NoElectricity='" + NoElectricity + "',TotalProductionTime= '" + TotalProductionTime + "',Breakdown= '" + Breakdown + "'," +
        //    "Changeover='" + Changeover + "',ManpowerShortage='" + ManpowerShortage + "',StartupLoss='" + StartupLoss + "',MaintainanceMachineShutDownTime=,'" + MaintainanceMachineShutDownTime + "'," +
        //    "MaterialNotAvailable= '" + MaterialNotAvailable + "',TotalDowntime='" + TotalDowntime + "',OperatingTime='" + OperatingTime + "',Availabilty='" + Availabilty + "',IdealRunRate='" + IdealRunRate + "'," +
        //    "Cavity='" + Cavity + "',PlanningQty= '" + PlanningQty + "',TotalShot='" + TotalShot + "',TotalProductionInNos='" + TotalProductionInNos + "',Packing='" + Packing + "',TotalPacket='" + TotalPacket + "',TargetProduction='" + TargetProduction + "'," +
        //    "Performance='" + Performance + "',RejectInNos='" + RejectInNos + "',GoodInNos='" + GoodInNos + "',Quality='" + Quality + "',OEE='" + OEE + "',SwitchFlag=" + SwitchFlag + ",SwitchPackerMachineId=" + SwitchPackerMachineId + "," +
        //    "SwitchOperatiorMachineId=" + SwitchOperatiorMachineId + ",Status='" + Status + "',Reason='" + Reason + "',SwitchNote='" + SwitchNote + "',PreformRejection='" + PreformRejection + "',ModifiedId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + OEEEntryMachineId + "";
        //    }
        //    Result = objBL.Function_ExecuteNonQuery();

        //    if (Result > 0)
        //    {
        //        objRL.ShowMessage(7, 1);
        //    }
        //}

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;
        bool DateFlag = false;
        bool SearchTag = false;
        bool IDFlag = false;

        string Shift = string.Empty;

        DateTime ShiftBeginTime, ShiftEndTime;
        DateTime ShiftDate, Date, Time;

        //private void FillGrid1()
        //{
        //    dgvOEE.Rows.Clear();
        //    Shift = string.Empty;
        //    MainQuery = string.Empty;
        //    WhereClause = string.Empty;
        //    OrderByClause = string.Empty;
        //    UserClause = string.Empty;

        //    DataSet ds = new DataSet();

        //    if (BusinessLayer.UserName_Static != BusinessResources.USER_ADMIN)
        //        UserClause = " and QCE.UserId = " + BusinessLayer.UserId_Static;
        //    else
        //        UserClause = string.Empty;

        //    //SELECT Date(), Date() -3, * FROM Master WHERE [Created On]<(Date()-3);
        //    //SELECT  * FROM PurchaseOrder WHERE [EntryDate]= (Date()-10);

        //    MainQuery = "select O.OEEId,O.EntryDate as [Date],O.EntryTime as [Time],O.Shift,O.ShiftScheduleId,SS.EntryDate,SS.EntryTime,SS.FromDate,SS.ToDate,SS.ShiftDate,SS.ShiftHours,SS.NoOfShifts,SS.BeginTime1,SS.EndTime1,SS.BeginTime2,SS.EndTime2,SS.BeginTime3,SS.EndTime3,SS.Naration from OEE O inner join ShiftSchedule SS on SS.ShiftScheduleId=O.ShiftScheduleId where O.CancelTag=0 and SS.CancelTag=0 ";


        //    // MainQuery = "select OE.ID,OE.EntryDate as [Date],OE.EntryTime as [Time],OE.Shift,OE.PlantInchargeId,E.FullName as[Plant Incharge],OE.VolumeCheckerId,E1.FullName as [Volume Checker] from ((OEEEntry OE inner join Employee E on E.ID=OE.PlantInchargeId) inner join Employee E1 on E1.ID=OE.VolumeCheckerId) where OE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0";
        //    OrderByClause = " order by O.EntryDate desc";

        //    if (DateFlag)
        //        WhereClause += " and O.EntryDate=#" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# ";
        //    //else if (SearchTag)
        //    //    WhereClause = " and E.FullName like '%" + txtSearch.Text + "%'";
        //    if (IDFlag)
        //        WhereClause += " and O.OEEId=" + txtSearchID.Text + "";
        //    //else
        //    //    WhereClause += "  and [O.EntryDate]>(Date()-10)";

        //    objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
        //    ds = objBL.ReturnDataSet();

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        //0 O.OEEId,
        //        //1 O.EntryDate as [Date],
        //        //2 O.EntryTime as [Time],
        //        //3 O.Shift,
        //        //4 O.ShiftScheduleId

                

        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            dgvOEE.Rows.Add();
        //            dgvOEE.Rows[i].Cells["clmOEEId"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEEId"].ToString()));

        //            Date = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"].ToString());
        //            dgvOEE.Rows[i].Cells["clmEntryDate"].Value = Date.ToString(BusinessResources.DATEFORMATDDMMYYYY);

        //            Time = Convert.ToDateTime(ds.Tables[0].Rows[i]["Time"].ToString());
        //            dgvOEE.Rows[i].Cells["clmEntryTime"].Value = Time.ToString(BusinessResources.TIME_FORMAT_HHMM_24);

        //            ShiftDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ShiftDate"].ToString());
        //            dgvOEE.Rows[i].Cells["clmShiftDate"].Value = ShiftDate.ToString(BusinessResources.DATEFORMATDDMMYYYY);

        //            dgvOEE.Rows[i].Cells["clmShift"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift"].ToString()));
        //            dgvOEE.Rows[i].Cells["clmShiftScheduleId"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftScheduleId"].ToString()));

        //            Shift = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift"].ToString()));
        //            OEEId = Convert.ToInt32(ds.Tables[0].Rows[i]["OEEId"].ToString());
        //            ShiftScheduleId = Convert.ToInt32(ds.Tables[0].Rows[i]["ShiftScheduleId"].ToString());

        //            if (Shift == "I")
        //            {
        //                ShiftBeginTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["BeginTime1"]));
        //                ShiftEndTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["EndTime1"]));
        //            }
        //            else if (Shift == "II")
        //            {
        //                ShiftBeginTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["BeginTime2"]));
        //                ShiftEndTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["EndTime2"]));
        //            }
        //            else if (Shift == "III")
        //            {
        //                ShiftBeginTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["BeginTime3"]));
        //                ShiftEndTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["EndTime3"]));
        //            }
        //            else
        //            {

        //            }

        //            string BTime = string.Empty, ETime = string.Empty;

        //            BTime = ShiftBeginTime.ToString(BusinessResources.TIME_FORMAT_HHMM_24);
        //            ETime = ShiftEndTime.ToString(BusinessResources.TIME_FORMAT_HHMM_24);

        //            dgvOEE.Rows[i].Cells["clmShiftBeginTime"].Value = BTime.ToString();
        //            dgvOEE.Rows[i].Cells["clmShiftEndTime"].Value = ETime.ToString();
        //            dgvOEE.Rows[i].Cells["clmShiftHours"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftHours"].ToString()));

        //            OEEType = "PlantIncharge";
        //            dgvOEE.Rows[i].Cells["clmPlantIncharge"].Value = GetEmployee();
        //            OEEType = "VolumeChecker";
        //            dgvOEE.Rows[i].Cells["clmVolumeChecker"].Value = GetEmployee();
        //            //GetEmployee();
        //        }
        //        //0 clmOEEId
        //        //1 clmDate
        //        //2 clmTime
        //        //3 clmShift
        //        //4 clmShiftScheduleId
        //        //5 clmShiftScheduleId
        //        //6 clmPlantIncharge
        //        //7 clmVolumeChecker


        //        //dgvOEE.Rows[i].Cells["clmOEEId"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEEId"].ToString()));
        //        //  DateTime dt; 
        //        //  dt =Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"].ToString());

        //        //  dgvOEE.Rows[i].Cells["clmDate"].Value = dt.ToString(BusinessResources.DATEFORMATDDMMYYYY);
        //        //  dgvOEE.Rows[i].Cells["clmTime"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Time"].ToString()));
        //        //  dgvOEE.Rows[i].Cells["clmShift"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift"].ToString()));
        //        //  dgvOEE.Rows[i].Cells["clmShiftScheduleId"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftScheduleId"].ToString()));

        //        //  Shift = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift"].ToString()));
        //        //  OEEId = Convert.ToInt32(ds.Tables[0].Rows[i]["OEEId"].ToString());
        //        //  ShiftScheduleId = Convert.ToInt32(ds.Tables[0].Rows[i]["ShiftScheduleId"].ToString());

        //        //  OEEType = "PlantIncharge";
        //        //  dgvOEE.Rows[i].Cells["clmPlantIncharge"].Value = GetEmployee();
        //        //  OEEType = "VolumeChecker";
        //        //  dgvOEE.Rows[i].Cells["clmVolumeChecker"].Value 

        //        //dataGridView1.DataSource = ds.Tables[0];
        //        //dataGridView1.Columns[4].Visible = false;
        //        //dataGridView1.Columns[6].Visible = false;

        //        //dataGridView1.Columns[0].Width = 80;
        //        //dataGridView1.Columns[1].Width = 100;
        //        //dataGridView1.Columns[2].Width = 100;
        //        //dataGridView1.Columns[5].Width = 300;
        //        //dataGridView1.Columns[7].Width = 300;

        //        lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
        //    }
        //}

        private void FillGrid()
        {
            dgvOEE.Rows.Clear();
            Shift = string.Empty;
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;
            UserClause = string.Empty;

            DataSet ds = new DataSet();

            if (BusinessLayer.UserName_Static != BusinessResources.USER_ADMIN)
                UserClause = " and O.UserId = " + BusinessLayer.UserId_Static;
            else
                UserClause = string.Empty;

            //SELECT Date(), Date() -3, * FROM Master WHERE [Created On]<(Date()-3);
            //SELECT  * FROM PurchaseOrder WHERE [EntryDate]= (Date()-10);

            MainQuery = "select O.OEEId,O.EntryDate as [Date],O.EntryTime as [Time],O.ShiftId,SEN.Shift,SEN.EntryDate as [Shift Date],SEN.ShifFromDate as [Shift Start],SEN.ShiftToDate as [Shift End],SEN.ShiftHours as [Shift Hours] from OEE O inner join ShiftEntryNew SEN on SEN.ID=O.ShiftId where O.CancelTag=0 and SEN.CancelTag=0 ";

            // MainQuery = "select OE.ID,OE.EntryDate as [Date],OE.EntryTime as [Time],OE.Shift,OE.PlantInchargeId,E.FullName as[Plant Incharge],OE.VolumeCheckerId,E1.FullName as [Volume Checker] from ((OEEEntry OE inner join Employee E on E.ID=OE.PlantInchargeId) inner join Employee E1 on E1.ID=OE.VolumeCheckerId) where OE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0";
            OrderByClause = " order by O.OEEId desc";

            if (DateFlag)
                WhereClause += " and SEN.EntryDate=#" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# ";
            //else if (SearchTag)
            //    WhereClause = " and E.FullName like '%" + txtSearch.Text + "%'";
            if (IDFlag)
                WhereClause += " and O.OEEId=" + txtSearchID.Text + "";
            //else
            //    WhereClause += "  and [O.EntryDate]>(Date()-10)";

            objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 O.OEEId,
                //1 O.EntryDate as [Date],
                //2 O.EntryTime as [Time],
                //3 O.ShiftId,
                //4 SEN.Shift,
                //5 SEN.EntryDate as [Shift Date],
                //6 SEN.ShifFromDate as [Shift Start],
                //7 SEN.ShiftToDate as [Shift End],
                //8 SEN.ShiftHours as [Shift Hours]

                lblCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgvOEE.Rows.Add();

                    int OEENO = 0;
                    OEEId = 0;
                    OEEId= objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEEId"].ToString())));
                    dgvOEE.Rows[i].Cells["clmOEEId"].Value = OEEId.ToString();

                    Date = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"].ToString());
                    dgvOEE.Rows[i].Cells["clmEntryDate"].Value = Date.ToString(BusinessResources.DATEFORMATDDMMYYYY);

                    Time = Convert.ToDateTime(ds.Tables[0].Rows[i]["Time"].ToString());
                    dgvOEE.Rows[i].Cells["clmEntryTime"].Value = Time.ToString(BusinessResources.TIME_FORMAT_HHMM_24);

                    dgvOEE.Rows[i].Cells["clmShiftId"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftId"].ToString()));

                    ShiftDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Shift Date"].ToString());
                    dgvOEE.Rows[i].Cells["clmShiftDate"].Value = ShiftDate.ToString(BusinessResources.DATEFORMATDDMMYYYY);

                    dgvOEE.Rows[i].Cells["clmShift"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift"].ToString()));
                    dgvOEE.Rows[i].Cells["clmShiftHours"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift Hours"].ToString()));

                    ShiftBeginTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["Shift Start"].ToString());
                    dgvOEE.Rows[i].Cells["clmShiftBeginTime"].Value = ShiftBeginTime.ToString(BusinessResources.DATE_FORMAT_WITHTIME_DDMMYYYHHMM);

                    ShiftEndTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["Shift End"].ToString());
                    dgvOEE.Rows[i].Cells["clmShiftEndTime"].Value = ShiftEndTime.ToString(BusinessResources.DATE_FORMAT_WITHTIME_DDMMYYYHHMM);

                    //OEEType = "PlantIncharge";
                    //dgvOEE.Rows[i].Cells["clmPlantIncharge"].Value = GetEmployee();
                    //OEEType = "VolumeChecker";
                    //dgvOEE.Rows[i].Cells["clmVolumeChecker"].Value = GetEmployee();
                }
               // OEEId = 0;
            }
        }

        string BaseQuery = string.Empty;

        private string GetEmployee()
        {
            BaseQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;

            string EName = string.Empty;
            DataSet ds = new DataSet();

            BaseQuery = "select OE.OEEEmployeeId,OE.OEEId,OE.ShiftId,OE.OEEType,OE.EmployeeId,E.FullName from OEEEmployee OE inner join Employee E on E.ID=OE.EmployeeId where OE.CancelTag=0 and E.CancelTag=0 and OE.OEEId=" + OEEId + " and OEEType='" + OEEType + "'";

            if (MachineId != 0 && OEEMachineId != 0)
                WhereClause = " and OE.OEEMachineId=" + OEEMachineId + " and OE.MachineId=" + MachineId + "";

            OrderByClause = " order by OE.OEEEmployeeId desc";
            objBL.Query = BaseQuery + WhereClause + OrderByClause;

            //objBL.Query = "select OE.OEEEmployeeId,OE.OEEId,OE.ShiftScheduleId,OE.Shift,OE.OEEType,OE.EmployeeId,E.FullName from OEEEmployee OE inner join Employee E on E.ID=OE.EmployeeId where OE.CancelTag=0 and E.CancelTag=0 and OE.OEEId=" + OEEId + " and OE.ShiftScheduleId=" + ShiftScheduleId + " and OE.Shift='" + Shift + "' and OEEType='" + OEEType + "' order by OE.OEEEmployeeId desc";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                EName = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["FullName"].ToString()));
            return EName;
        }

        private DataSet GetEmployee_CheckListBox()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select OE.OEEEmployeeId,OE.OEEId,OE.ShiftId,OE.OEEType,OE.EmployeeId,E.FullName from OEEEmployee OE inner join Employee E on E.ID=OE.EmployeeId where OE.CancelTag=0 and E.CancelTag=0 and OE.OEEId=" + OEEId + " and OE.ShiftId=" + ShiftId + " and OEEType='" + OEEType + "' order by OE.OEEEmployeeId desc";
            ds = objBL.ReturnDataSet();
            return ds;
        }

        //private void FillGrid1()
        //{
        //    MainQuery = string.Empty;
        //    WhereClause = string.Empty;
        //    OrderByClause = string.Empty;
        //    UserClause = string.Empty;


        //    DataSet ds = new DataSet();

        //    if (BusinessLayer.UserName_Static != BusinessResources.USER_ADMIN)
        //        UserClause = " and QCE.UserId = " + BusinessLayer.UserId_Static;
        //    else
        //        UserClause = string.Empty;

        //    //MainQuery = "select QCE.ID as [QC No],QCE.EntryDate as [Date],QCE.EntryTime as [Time],QCE.ShiftEntryId,SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],SE.Naration from (((QCEntry QCE inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join  Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) where QCE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and SE.CancelTag=0 ";
        //    MainQuery = "select OE.ID,OE.EntryDate as [Date],OE.EntryTime as [Time],OE.Shift,OE.PlantInchargeId,E.FullName as[Plant Incharge],OE.VolumeCheckerId,E1.FullName as [Volume Checker] from ((OEEEntry OE inner join Employee E on E.ID=OE.PlantInchargeId) inner join Employee E1 on E1.ID=OE.VolumeCheckerId) where OE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0";
        //    OrderByClause = " order by OE.EntryDate desc";

        //    if (DateFlag)
        //        WhereClause = " and OE.EntryDate=#" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# ";
        //    else if (SearchTag)
        //        WhereClause = " and E.FullName like '%" + txtSearch.Text + "%'";
        //    else if (IDFlag)
        //        WhereClause = " and OE.ID=" + txtSearchID.Text + "";
        //    else
        //        WhereClause = string.Empty;

        //    objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
        //    ds = objBL.ReturnDataSet();

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        //0 ID,
        //        //1 EntryDate as [Date],
        //        //2 EntryTime as [Time],
        //        //3OE.Shift,
        //        //4 OE.PlantInchargeId,
        //        //5 E.FullName as[Plant Incharge],
        //        //6 OE.VolumeCheckerId,
        //        //7 E1.FullName as [Volume Checker]

        //        dataGridView1.DataSource = ds.Tables[0];
        //        dataGridView1.Columns[4].Visible = false;
        //        dataGridView1.Columns[6].Visible = false;

        //        dataGridView1.Columns[0].Width = 80;
        //        dataGridView1.Columns[1].Width = 100;
        //        dataGridView1.Columns[2].Width = 100;
        //        dataGridView1.Columns[5].Width = 300;
        //        dataGridView1.Columns[7].Width = 300;

        //        lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
        //    }
        //}

        private void btnSaveOEE_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB_OEEMachine();
            btnDeleteOEE.Visible = false;
            //SaveDB_OEEEntry();
        }

        private bool Validation()
        {
            objEP.Clear();
            if (txtOEEId.Text == "")
            {
                objEP.SetError(txtOEEId, "Enter ID");
                txtOEEId.Focus();
                return true;
            }
            //else if (cmbShift.SelectedIndex == -1)
            //{
            //    objEP.SetError(cmbShift, "Enter Shift");
            //    cmbShift.Focus();
            //    return true;
            //}
           
            //else if (clbMouldChanger.CheckedItems.Count == 0)
            //{
            //    objEP.SetError(clbMouldChanger, "Select  at least 1 Mould Changer");
            //    clbMouldChanger.Focus();
            //    return true;
            //}
            //else if (clbPorter.CheckedItems.Count == 0)
            //{
            //    objEP.SetError(clbPorter, "Select  at least 1 Porter");
            //    clbPorter.Focus();
            //    return true;
            //}
            //else if (Validation_Packer())
            //{
            //    return true;
            //}
            else
                return false;
        }

        private bool Validation_OEEMachine()
        {
            objEP.Clear();
            if (OEEId == 0)
            {
                objEP.SetError(txtOEEId, "Enter OEE Number");
                txtOEEId.Focus();
                return true;
            }
            else if (Convert.ToInt32(txtOEEId.Text) != OEEId)
            {
                objEP.SetError(txtOEEId, "Enter Worong OEE Number");
                txtOEEId.Focus();
                return true;
            }
            else if (ShiftId == 0)
            {
                objEP.SetError(txtShift, "Enter Shift");
                txtShift.Focus();
                return true;
            }
            else if (MachineId == 0)
            {
                objEP.SetError(cmbMachineNo, "Enter Shift");
                cmbMachineNo.Focus();
                return true;
            }
            else if (txtShiftHours.Text  == "")
            {
                objEP.SetError(txtShiftHours, "Enter Shift Hours");
                txtShiftHours.Focus();
                return true;
            }
            //else if (cmbPlantIncharge.SelectedIndex == -1)
            //{
            //    objEP.SetError(cmbPlantIncharge, "Select PlantIncharge");
            //    cmbPlantIncharge.Focus();
            //    return true;
            //}
            //else if (cmbVolumeChecker.SelectedIndex == -1)
            //{
            //    objEP.SetError(cmbVolumeChecker, "Select Volum Checker");
            //    cmbVolumeChecker.Focus();
            //    return true;
            //}
            //else if (cmbSupervisor.SelectedIndex==-1)
            //{
            //    objEP.SetError(cmbSupervisor, "Select Supervisor");
            //    cmbSupervisor.Focus();
            //    return true;
            //}
            //else if (objRL.MachineDescription == "Auto" || objRL.MachineDescription == "Sarvo" && cmbStatus.Text != "No Work")
            //{
            //    if (Validation_CheckListBox(clbPackerAuto))
            //        return false;
            //    else
            //        return true;
            //}
            //else if (objRL.MachineDescription == "Auto" && cmbStatus.Text != "No Work")
            //{
            //    if (Validation_ComboBox(cmbPreformLoadingAuto))
            //        return false;
            //    else
            //        return true;
            //}
            //else if (objRL.MachineDescription != "Auto" && cmbStatus.Text != "No Work")
            //{
            //    if (Validation_ComboBox(cmbPacker))
            //        return true;
            //    else
            //        return false;
            //}
            //else if (objRL.MachineDescription != "Auto" && cmbStatus.Text != "No Work")
            //{
            //    if (Validation_ComboBox(cmbOperator))
            //        return true;
            //    else
            //        return false;
            //}
            //else if (SwitchFlag == 1)
            //{
            //    if (cmbReason.SelectedIndex == -1)
            //    {
            //        objEP.SetError(cmbReason, "Select PlantIncharge");
            //        cmbReason.Focus();
            //        return true;
            //    }
            //    else
            //        return false;

            //}
            else if (objRL.ProductId == 0)
            {
                objEP.SetError(cmbStatus, "Select Product");
                cmbStatus.Focus();
                return true;
            }
            else
                return false;
        }

        //private bool Validation_CheckListBox_OEEMachine(CheckedListBox clb)
        //{
        //    if (clb.CheckedItems.Count == 0)
        //    {
        //        objEP.SetError(clb, "Select Name");
        //        clb.Focus();
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        private bool Validation_ComboBox(ComboBox cmb)
        {
            if (cmb.SelectedIndex == -1)
            {
                objEP.SetError(cmb, "Select Employee Name");
                cmb.Focus();
                return true;
            }
            else
                return false;
        }

        //private bool Validation_Packer()
        //{
        //    if (objRL.MachineDescription == "Auto") // < 10)
        //    {
        //        if (clbPackerAuto.CheckedItems.Count == 0)
        //        {
        //            objEP.SetError(clbPackerAuto, "Select atleast Packer Name");
        //            clbPackerAuto.Focus();
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //    else
        //    {
        //        if (cmbPacker.SelectedIndex == -1)
        //        {
        //            objEP.SetError(cmbPacker, "Select Packer Name");
        //            cmbPacker.Focus();
        //            return true;
        //        }
        //        else if (cmbOperator.SelectedIndex == -1)
        //        {
        //            objEP.SetError(cmbOperator, "Select Operator Name");
        //            cmbOperator.Focus();
        //            return true;
        //        }
        //        else
        //            return false;
        //    }


        //}

        
        private void cmbSupervisor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if (cmbSupervisor.SelectedIndex > -1)
            //{
            //    SupervisorId = Convert.ToInt32(cmbSupervisor.SelectedValue);
            //}
        }

        // int[] EmpId;
        List<int> OperatorId_L = new List<int>();

        //private void Get_Packer_Id()
        //{
        //    DataSet ds = new DataSet();
        //    objBL.Query = "select OEM.OperatorId from OEEEntryMachine OEM inner join OEEEntry OEE on OEE.ID=OEM.OEEEntryId where OEE.CancelTag=0 and OEM.CancelTag=0 and OEE.Shift='0' and OEM.EntryDate=#" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and OEM.MachineNo NOT IN(" + MachineNo + ")";
        //    ds = objBL.ReturnDataSet();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            OperatorId_L.Add(Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString()));
        //        }
        //    }

        //    if (cmbOperator.SelectedIndex > -1)
        //    {
        //        OperatorId_L.Add(Convert.ToInt32(cmbOperator.SelectedValue));
        //    }

        //    if (MachineNo < 10)
        //        objRL.Fill_Employee_ComboBox_OperatorPacker_Not_In_Id(cmbPacker, OperatorId_L);
        //    else
        //    {
        //        objRL.Fill_Employee_ComboBox_OperatorPacker_Not_In_Id(cmbPreformLoadingAuto, OperatorId_L);
        //        //objRL.Fill_Employee_ComboBox_OperatorPacker(cmbPreformLoading);
        //        objRL.Fill_Employee_CheckedListBox_By_OperatorPacker(clbPackerAuto);
        //    }
        //}

        //private void Fill_Packer()
        //{
        //    Get_Packer_Id();

        //    //if (cmbOperator.SelectedIndex > -1)
        //    //{
        //    //    OperatorId = Convert.ToInt32(cmbOperator.SelectedValue);
        //    //    EmpId = new int[1];
        //    //    EmpId[0] = OperatorId;
        //    //    objRL.Fill_Employee_ComboBox_OperatorPacker_Not_In_Id(cmbPacker, EmpId);
        //    //}
        //}

        //private void cmbOperator_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    Fill_Packer();
        //}

        

        private void txtPreformRejection_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtPreformRejection_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtPreformRejection);
        }

        bool GridFlag = false;
        //private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        RowCount_Grid = dataGridView1.Rows.Count;
        //        CurrentRowIndex = dataGridView1.CurrentRow.Index;

        //        if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
        //        {
        //            ClearAll();
        //            btnSwitch.Enabled = true;

        //            //0 ID,
        //            //1 EntryDate as [Date],
        //            //2 EntryTime as [Time],
        //            //3OE.Shift,
        //            //4 OE.PlantInchargeId,
        //            //5 E.FullName as[Plant Incharge],
        //            //6 OE.VolumeCheckerId,
        //            //7 E1.FullName as [Volume Checker]

        //            GridFlag = true;
        //            TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
        //            txtOEEId.Text = TableID.ToString();
        //            OEEEntryId = TableID;
        //            dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
        //            dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
        //            cmbShift.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
        //            cmbPlantIncharge.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
        //            cmbVolumeChecker.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
        //            //Fill_MouldChanger_List();
        //            Fill_Porter_List();
        //            gbMachineNo.Enabled = true;
        //        }
        //    }
        //    catch (Exception ex1)
        //    {
        //        objRL.ErrorMessge(ex1.ToString());
        //        return;
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }
        //}

        List<int> objPorter = new List<int>();
        //private void Fill_Porter_List()
        //{
        //    DataSet ds = new DataSet();
        //    objBL.Query = "Select ID,OEEEntryId,PorterId from OEEEntryPorter where CancelTag=0 and OEEEntryId=" + TableID + "";
        //    ds = objBL.ReturnDataSet();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        objPorter = new List<int>();

        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["PorterId"])))
        //            {
        //                int Iid = Convert.ToInt32(ds.Tables[0].Rows[i]["PorterId"].ToString());
        //                objPorter.Add(Iid);
        //            }
        //        }
        //        int value = 0;
        //        for (int i = 0; i < clbPorter.Items.Count; i++)
        //        {
        //            DataRowView view = clbPorter.Items[i] as DataRowView;
        //            value = (int)view["ID"];
        //            if (objPorter.Contains(value))
        //                clbPorter.SetItemChecked(i, true);
        //        }
        //    }
        //}

        //List<int> objMouldChanger = new List<int>();
        //private void Fill_MouldChanger_List()
        //{
        //    DataSet ds = new DataSet();
        //    objBL.Query = "Select ID,OEEEntryId,MouldChangerId from OEEEntryMouldChanger where CancelTag=0 and OEEEntryId=" + TableID + "";
        //    ds = objBL.ReturnDataSet();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        objMouldChanger = new List<int>();

        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["MouldChangerId"])))
        //            {
        //                int Iid = Convert.ToInt32(ds.Tables[0].Rows[i]["MouldChangerId"].ToString());
        //                objMouldChanger.Add(Iid);
        //            }
        //        }
        //        int value = 0;
        //        for (int i = 0; i < clbMouldChanger.Items.Count; i++)
        //        {
        //            DataRowView view = clbMouldChanger.Items[i] as DataRowView;
        //            value = (int)view["ID"];
        //            if (objMouldChanger.Contains(value))
        //                clbMouldChanger.SetItemChecked(i, true);
        //        }
        //    }
        //}

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchID_TextChanged(object sender, EventArgs e)
        {

        }

        //Changed Product 
        //1.Production Complete
        //2.Mould Change
        //3.Product Issue
        //3.Machine not working
        //4.

        string ResonsForChange = string.Empty;

        int PreviousProductId = 0;

        //private void btnSwitch_Click(object sender, EventArgs e)
        //{
        //    SwitchFlag = 0;
        //    if (ProductId != 0)
        //    {
        //        DialogResult dr;
        //        dr = objRL.Switch_Product();

        //        if (dr == System.Windows.Forms.DialogResult.Yes)
        //        {
        //            SwitchFlag = 1;
        //            ClearProductPlanning();

        //            txtSearchProductName.Text = "";
        //            lblProductName.Text = "";
                   
        //            PreviousProductId = ProductId;
        //            ProductId = 0;
        //            OEEMachineId = 0;
        //            pChangeProduct.Visible = true;
        //            //lblMouldChanger.Visible = true;
        //            //clbMouldChangerChange.Visible = true;
        //            //Fill_MouldChanger_Change_List();
        //        }
        //    }
        //    else
        //    {
        //        //lblMouldChanger.Visible = false;
        //        //clbMouldChangerChange.Visible = false;
        //    }
        //}

        //List<int> objMouldChangerChange = new List<int>();

        //private void Fill_MouldChanger_Change_List()
        //{
        //    DataSet ds = new DataSet();
        //    objBL.Query = "Select ID,OEEEntryId,MouldChangerId from OEEEntryMouldChanger where CancelTag=0 and OEEEntryId=" + OEEEntryId + "";
        //    ds = objBL.ReturnDataSet();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        objMouldChangerChange = new List<int>();

        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["MouldChangerId"])))
        //            {
        //                int Iid = Convert.ToInt32(ds.Tables[0].Rows[i]["MouldChangerId"].ToString());
        //                objMouldChangerChange.Add(Iid);
        //            }
        //        }
        //        int value = 0;
        //        //for (int i = 0; i < clbMouldChangerChange.Items.Count; i++)
        //        //{
        //        //    DataRowView view = clbMouldChangerChange.Items[i] as DataRowView;
        //        //    value = (int)view["ID"];
        //        //    if (objMouldChangerChange.Contains(value))
        //        //        clbMouldChangerChange.SetItemChecked(i, true);
        //        //}
        //    }
        //}

        private void dtpSearchDate_ValueChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void btnOEE_Click(object sender, EventArgs e)
        {
            RedundancyLogics.ReportDate = dtpOEEReportDate.Value;
            Get_SDLC_Report();
            //GetReportSingle();
        }

        public class CursorWait : IDisposable
        {
            public CursorWait(bool appStarting = false, bool applicationCursor = false)
            {
                // Wait
                Cursor.Current = appStarting ? Cursors.AppStarting : Cursors.WaitCursor;
                if (applicationCursor) System.Windows.Forms.Application.UseWaitCursor = true;
            }

            public void Dispose()
            {
                // Reset
                Cursor.Current = Cursors.Default;
                System.Windows.Forms.Application.UseWaitCursor = false;
            }
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

        public void GetReportSingle()
        {
            using (new CursorWait())
            {
                BorderFlag = false;

                object misValue = System.Reflection.Missing.Value;
                myExcelApp = new Excel.Application();
                myExcelWorkbooks = myExcelApp.Workbooks;

                objRL.ClearExcelPath();
                objRL.isPDF = true;
                objRL.Form_ExcelFileName = "OEECalculations.xlsx";
                objRL.Form_ReportFileName = "ID-" + txtOEEId.Text;
                objRL.Form_DestinationReportFilePath = "\\OEE Calculations\\";
                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                //objRL.FillCompanyData();
                DataSet ds = new DataSet();
                objBL.Query = "select ID,EntryDate,EntryTime,ProductionEntryId,OEEEntryId,MachineNo,ProductId,SupervisorId,OperatorId,PackerId,PackerId1,PackerId2,PreformLoadingId,MouldChangerId,MouldChangerId1,ShiftLengthA,ShortMealBreakB,NoPlanning,NoElectricity,TotalProductionTime,Breakdown,Changeover,ManpowerShortage,StartupLoss,MaintainanceMachineShutDownTime,MaterialNotAvailable,TotalDowntime,OperatingTime,Availabilty,IdealRunRate,Cavity,PlanningQty,TotalShot,TotalProductionInNos,Packing,TotalPacket,TargetProduction,Performance,RejectInNos,GoodInNos,Quality,OEE,SwitchFlag,SwitchPackerMachineId,SwitchOperatiorMachineId,Reason,Status,SwitchNote,PreformRejection from OEEEntryMachine where CancelTag=0 and EntryDate=#" + dtpSearchDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# order by ID asc";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    RowCount = 5;
                    for (int i = 0; i < 10; i++)
                    {
                        //i = 0;
                        Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(ds.Tables[0].Rows[0]["EntryDate"].ToString())));
                        //Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, btn1.Text.ToString());
                        //Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, cmbShift.Text.ToString());
                        Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, lblProductName.Text.ToString());
                        Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, txtIdealRunRate.Text);
                        //Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, cmbOperator.Text);
                        Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, txtShiftLength.Text);
                        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, txtShiftLength.Text);
                        Fill_Merge_Cell("I", "I", misValue, myExcelWorksheet, txtShortMealBreakB.Text);
                        Fill_Merge_Cell("J", "J", misValue, myExcelWorksheet, txtNoPlanning.Text);

                        Fill_Merge_Cell("K", "K", misValue, myExcelWorksheet, txtNoElectricity.Text);
                        Fill_Merge_Cell("L", "L", misValue, myExcelWorksheet, txtTotalProductionTime.Text);
                        Fill_Merge_Cell("M", "M", misValue, myExcelWorksheet, txtBreakdown.Text);
                        Fill_Merge_Cell("N", "N", misValue, myExcelWorksheet, txtChangeover.Text);
                        Fill_Merge_Cell("O", "O", misValue, myExcelWorksheet, txtManpowerShortage.Text);
                        Fill_Merge_Cell("P", "P", misValue, myExcelWorksheet, txtStartupLoss.Text);

                        Fill_Merge_Cell("Q", "Q", misValue, myExcelWorksheet, txtMaterialNotAvailable.Text);
                        Fill_Merge_Cell("R", "R", misValue, myExcelWorksheet, txtTotalDowntime.Text);
                        Fill_Merge_Cell("S", "S", misValue, myExcelWorksheet, txtOperatingTime.Text);
                        Fill_Merge_Cell("T", "T", misValue, myExcelWorksheet, txtAvailabilty.Text);

                        Fill_Merge_Cell("U", "U", misValue, myExcelWorksheet, txtCavity.Text);
                        Fill_Merge_Cell("V", "V", misValue, myExcelWorksheet, txtTotalShot.Text);

                        Fill_Merge_Cell("W", "W", misValue, myExcelWorksheet, txtTotalProductionInNos.Text);
                        Fill_Merge_Cell("X", "X", misValue, myExcelWorksheet, txtPacking.Text);
                        Fill_Merge_Cell("Y", "Y", misValue, myExcelWorksheet, txtTotalPacket.Text);
                        Fill_Merge_Cell("Z", "Z", misValue, myExcelWorksheet, txtTargetProduction.Text);
                        Fill_Merge_Cell("AA", "AA", misValue, myExcelWorksheet, txtTargetProduction.Text);
                        Fill_Merge_Cell("AB", "AB", misValue, myExcelWorksheet, txtPerformance.Text);
                        Fill_Merge_Cell("AC", "AC", misValue, myExcelWorksheet, txtPreformRejection.Text);
                        Fill_Merge_Cell("AD", "AD", misValue, myExcelWorksheet, txtRejectInNos.Text);
                        Fill_Merge_Cell("AE", "AE", misValue, myExcelWorksheet, txtGoodInNos.Text);
                        Fill_Merge_Cell("AF", "AF", misValue, myExcelWorksheet, txtQuality.Text);
                        Fill_Merge_Cell("AG", "AG", misValue, myExcelWorksheet, txtOEE.Text);
                        RowCount++;
                        SrNo++;
                        i++;
                    }
                }

                myExcelWorkbook.Save();
                string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
                const int xlQualityStandard = 0;
                myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                myExcelWorkbook.Close(true, misValue, misValue);
                myExcelApp.Quit();
                System.Diagnostics.Process.Start(objRL.RL_DestinationPath);
            }
        }

        private void txtSearchProductName_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Item();
            if (txtSearchProductName.Text != "")
            {
                lbItem.Visible = true;
                //objRL.Fill_Item_ListBox(lbItem, txtSearchProductName.Text, "Text");
                objRL.Fill_Item_ListBox_OEE(lbItem, txtSearchProductName.Text, "Text");
                //lbItem.Focus();
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
            objRL.ProductId = 0;
            //lblProductType.Text = "";
            lblProductName.Text = "";
        }

        private void lbItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Fill_Product_Information();
                //txtQty.Focus();
            }
        }

        string ProductDetails = string.Empty;
        string Standard = string.Empty;

        string QTYDisplay = string.Empty;
        string IdealRunRateDisplay = string.Empty;

        private void Get_Packing_IdealRunRate()
        {
            //Semi(1 to 8)	Auto-1(11to12)	Auto-2(13)	Sarvo(9,10,14)

            txtIdealRunRate.Text = "";
            txtPacking.Text = "";

            if (!string.IsNullOrEmpty(Convert.ToString(objRL.Qty)))
            {
                QTYDisplay = objRL.Qty.ToString();
                txtPacking.Text = QTYDisplay.ToString();
            }

            if (objRL.MachineStatus == "Semi")
            {
                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Semi)))
                    IdealRunRateDisplay = objRL.Semi.ToString();
            }
            else if (objRL.MachineStatus == "Auto-1")
            {
                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Auto1)))
                    IdealRunRateDisplay = objRL.Auto1.ToString();
            }
            else if (objRL.MachineStatus == "Auto-2")
            {
                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Auto2)))
                    IdealRunRateDisplay = objRL.Auto2.ToString();
            }
            else if (objRL.MachineStatus == "Sarvo")
            {
                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Servo)))
                    IdealRunRateDisplay = objRL.Servo.ToString();
            }
            else
                IdealRunRateDisplay = "";

            if (!string.IsNullOrEmpty(Convert.ToString(IdealRunRateDisplay)))
            {
                IdealRunRate = Convert.ToDouble(IdealRunRateDisplay);
                txtIdealRunRate.Text = IdealRunRate.ToString();
            }
            //if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Semi"])))
            //    Semi = Convert.ToDouble(ds.Tables[0].Rows[0]["Semi"].ToString());
            //if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Auto1"])))
            //    Auto1 = Convert.ToDouble(ds.Tables[0].Rows[0]["Auto1"].ToString());
            //if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Auto2"])))
            //    Auto2 = Convert.ToDouble(ds.Tables[0].Rows[0]["Auto2"].ToString());
            //if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Servo"])))
            //    Servo = Convert.ToDouble(ds.Tables[0].Rows[0]["Servo"].ToString());

        }

        private void Fill_Product_Information()
        {
            if (OEEId == 0)
            {
                //objRL.ProductId = 0;
                ProductId = Convert.ToInt32(lbItem.SelectedValue);
            }
            else
            {
                if (objRL.ProductId != 0)
                    ProductId = objRL.ProductId;
            }

            if (ProductId == 0)
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

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Cavity)))
                    txtCavity.Text = objRL.Cavity.ToString();

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
                else if (objRL.ProductType == "Jar")
                {
                    lblProductName.BackColor = Color.Yellow;
                    //lblProductType.BackColor = Color.Yellow;
                }
                else
                    lblProductName.BackColor = Color.White;

                if (objRL.ProductType != "NA")
                    Get_Packing_IdealRunRate();

                lbItem.Visible = false;
               // gbProductionPlanning.Enabled = true;
                gbAvailabilty.Enabled = true;
                gbPerformance.Enabled = true;
                gbQuality.Enabled = true;
            }
        }

        private void lbItem_Click(object sender, EventArgs e)
        {
            Fill_Product_Information();
        }

        private void btnAddMachineNo_Click(object sender, EventArgs e)
        {
            MachineMaster objForm = new MachineMaster();
            objForm.ShowDialog(this);
            objRL.Fill_Machine(cmbMachineNo);
        }

        private void cmbMachineNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_MachineDetails();
        }

        private void Fill_MachineDetails()
        {
            if (cmbMachineNo.SelectedIndex > -1)
            {
                //ClearProductPlanning();
                Clear_Production_Planning();

                lblMachineDetails.Text = "";
                objRL.MachineId = Convert.ToInt32(cmbMachineNo.SelectedValue);
                MachineId = Convert.ToInt32(cmbMachineNo.SelectedValue);
                objRL.Get_Machine_Details();
                lblMachineDetails.Text = "Machine No-" + cmbMachineNo.Text + ", Description=" + objRL.MachineDescription + "";
                
                //Get_MachineWise_Details();
                //Machine_OEE_Values();
                //gbProductDetails.Visible = true;

                //if(cmbSupervisor.SelectedIndex == -1)
                //    Get_SupervisorId();

                //Operator_Packer_Visible();

                FillGrid_MachineEntry();
                Fill_Balance_Length();
                //objRL.Fill_Employee_ComboBox_OperatorPacker_Not_In_Id(cmbPreformLoadingAuto, OperatorId_L);
            }
        }
        
        //private void Visible_False()
        //{
        //    lblOperator.Visible = false;
        //    cmbOperator.Visible = false;
        //    lblPacker.Visible = false;
        //    cmbPacker.Visible = false;
        //    lblPreforLoadingAuto.Visible = false;
        //    cmbPreformLoadingAuto.Visible = false;
        //    lblPackerListAuto.Visible = false;
        //    clbPackerAuto.Visible = false;
        //}

        //private void Operator_Packer_Visible()
        //{
        //    Visible_False();

        //    if (objRL.MachineDescription == "Auto" || objRL.MachineDescription =="Sarvo")
        //    {
        //        cmbOperator.SelectedIndex = -1;
        //        cmbPacker.SelectedIndex = -1;
        //        lblOperator.Visible = false;
        //        cmbOperator.Visible = false;
        //        lblPacker.Visible = false;
        //        cmbPacker.Visible = false;
        //        lblPreforLoadingAuto.Visible = true;
        //        cmbPreformLoadingAuto.Visible = true;
        //        lblPackerListAuto.Visible = true;
        //        clbPackerAuto.Visible = true;
        //    }
        //    else
        //    {
        //        //cmbOperator.SelectedIndex = -1;
        //        //cmbPacker.SelectedIndex = -1;
        //        lblOperator.Visible = true;
        //        cmbOperator.Visible = true;
        //        lblPacker.Visible = true;
        //        cmbPacker.Visible = true;
        //    }
        //}

        private void btnShiftAdd_Click(object sender, EventArgs e)
        {
            ShiftSchedule objForm = new ShiftSchedule();
            objForm.ShowDialog(this);
        }

        private bool Validation_CheckListBox(CheckedListBox clb)
        {
            objEP.Clear();
            bool checkCheck = false;

            for (int i = 0; i < clb.Items.Count; i++)
            {
                checkCheck = clb.GetItemChecked(i);
                if (checkCheck)
                    break;
            }
            return checkCheck;
        }

        private bool ValidationOEE()
        {
            objEP.Clear();
            if (txtOEEId.Text == "")
            {
                objEP.SetError(txtOEEId, "Enter OEE Id");
                txtOEEId.Focus();
                return true;
            }
            else if (txtShift.Text == "")
            {
                objEP.SetError(txtShift, "Enter Shift");
                txtShift.Focus();
                return true;
            }
            else if (txtShiftHours.Text == "")
            {
                objEP.SetError(txtShiftHours, "Enter Shift Hours");
                txtShiftHours.Focus();
                return true;
            }
            else if (ShiftId == 0)
            {
                objEP.SetError(txtShift, "Enter Shift");
                txtShift.Focus();
                return true;
            }
            else if (objRL.ShiftHours =="")
            {
                objEP.SetError(txtShiftHours, "Enter Shif Hours");
                txtShiftHours.Focus();
                return true;
            }
            //else if (cmbPlantIncharge.SelectedIndex == -1)
            //{
            //    objEP.SetError(cmbPlantIncharge, "Select PlantIncharge");
            //    cmbPlantIncharge.Focus();
            //    return true;
            //}
            //else if (cmbVolumeChecker.SelectedIndex == -1)
            //{
            //    objEP.SetError(cmbVolumeChecker, "Select Volum Checker");
            //    cmbVolumeChecker.Focus();
            //    return true;
            //}
            //else if (clbSupervisor.CheckedItems.Count == 0)
            //{
            //    clbSupervisor.Focus();
            //    objEP.SetError(clbSupervisor, "Select Supervisor");
            //    return true;
            //}
            //else if (clbMouldChanger.CheckedItems.Count == 0)
            //{
            //    clbMouldChanger.Focus();
            //    objEP.SetError(clbMouldChanger, "Select Mould Changer");
            //    return true;
            //}
            //else if (clbPorter.CheckedItems.Count == 0)
            //{
            //    clbPorter.Focus();
            //    objEP.SetError(clbPorter, "Select Porter");
            //    return true;
            //}
            //else if (clbMouldChanger.CheckedItems.Count == 0)
            //{
            //    clbMouldChanger.Focus();
            //    objEP.SetError(clbMouldChanger, "Select Mould");
            //    return true;
            //}
            //else if (!Validation_CheckListBox(clbMouldChanger))
            //{
            //    clbMouldChanger.Focus();
            //    objEP.SetError(clbMouldChanger, "Select Mould Changer");
            //    return true;
            //}
            //else if (!Validation_CheckListBox(clbPorter))
            //{
            //    clbMouldChanger.Focus();
            //    objEP.SetError(clbMouldChanger, "Select Porter");
            //    return true;
            //}
            //else if (ShiftScheduleId == 0)
            //{
            //    objEP.SetError(btnShiftAdd, "Enter Shift Schedule");
            //    cmbShift.Focus();
            //    return true;
            //}
            else
                return false;
        }

        private void VisibleTrue(bool TF)
        {
            //gbMachineDetails.Visible = TF;
            //gbProductDetails.Visible = TF;
           // gbProductionPlanning.Visible = TF;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
            tcOEE.Select();
            cmbMachineNo.Focus();
        }

        int ShiftId = 0, PlantInchargeId = 0, VolumeCheckerId = 0;

        //protected void CheckExist_OEE()
        //{
        //    OEEId = 0;
        //    DataSet ds = new DataSet();
        //    Shift = cmbShift.Text;
        //    gbMachineDetails.Visible = false;

        //    //MainQuery = "select O.OEEId,O.EntryDate as [Date],O.EntryTime as [Time],O.Shift,O.ShiftScheduleId from OEE O inner join ShiftSchedule SS on SS.ShiftScheduleId=O.ShiftScheduleId where O.CancelTag=0 and SS.CancelTag=0 ";

        //    objBL.Query = "select O.OEEId,O.EntryDate as [Date],O.EntryTime as [Time],O.Shift from OEE O inner join ShiftSchedule SS on SS.ShiftScheduleId=O.ShiftScheduleId where O.CancelTag=0 and SS.CancelTag=0 and O.ShiftScheduleId=" + ShiftScheduleId + " and O.Shift='" + Shift + "' and O.EntryDate=#" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# "; // dtpShiftDate FromDate='" + dtpFromDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "' and ToDate='" + dtpToDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "' and ShiftScheduleId <> " + TableID + "";
        //    ds = objBL.ReturnDataSet();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["OEEId"].ToString())))
        //        {
        //            OEEId = Convert.ToInt32(ds.Tables[0].Rows[0]["OEEId"].ToString());

        //            Shift = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Shift"].ToString()));
        //            OEEId = Convert.ToInt32(ds.Tables[0].Rows[0]["OEEId"].ToString());
        //            ShiftScheduleId = Convert.ToInt32(ds.Tables[0].Rows[0]["ShiftScheduleId"].ToString());

        //            OEEType = "PlantIncharge";
        //            cmbPlantIncharge.Text = GetEmployee();
        //            OEEType = "VolumeChecker";
        //            cmbVolumeChecker.Text = GetEmployee();

        //            OEEType = "MouldChanger";
        //            Fill_CheckListBox(clbMouldChanger);

        //            OEEType = "Supervisor";
        //            Fill_CheckListBox(clbSupervisor);

        //            OEEType = "Porter";
        //            Fill_CheckListBox(clbPorter);

        //            gbMachineDetails.Visible = true;
        //        }
        //    }
        //}

      

        List<int> objItem = new List<int>();
        int MouldChangerId_Checker = 0, PorterId_Checker = 0;

        string Designation = string.Empty;

        private void Fill_CheckListBox(CheckedListBox clb)
        {
            BaseQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;

            string EName = string.Empty;
            DataSet ds = new DataSet();

            BaseQuery = "select OE.OEEEmployeeId,OE.OEEId,OE.ShiftId,OE.OEEType,OE.EmployeeId,E.FullName from OEEEmployee OE inner join Employee E on E.ID=OE.EmployeeId where OE.CancelTag=0 and E.CancelTag=0 and OE.OEEId=" + OEEId + " and OEEType='" + OEEType + "'";

            if (MachineId != 0 && OEEMachineId != 0)
                WhereClause = " and OE.OEEMachineId=" + OEEMachineId + " and MachineId=" + MachineId + "";

            OrderByClause = " order by OE.OEEEmployeeId desc";
            objBL.Query = BaseQuery + WhereClause + OrderByClause;

            //objBL.Query = "select OE.OEEEmployeeId,OE.OEEId,OE.ShiftScheduleId,OE.Shift,OE.OEEType,OE.EmployeeId,E.FullName from OEEEmployee OE inner join Employee E on E.ID=OE.EmployeeId where OE.CancelTag=0 and E.CancelTag=0 and OE.OEEId=" + OEEId + " and OE.ShiftScheduleId=" + ShiftScheduleId + " and OE.Shift='" + Shift + "' and OEEType='" + OEEType + "' order by OE.OEEEmployeeId desc";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                objItem = new List<int>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["EmployeeId"])))
                    {
                        int Iid = Convert.ToInt32(ds.Tables[0].Rows[i]["EmployeeId"].ToString());
                        objItem.Add(Iid);
                    }
                }

                int value = 0;
                for (int i = 0; i < clb.Items.Count; i++)
                {
                    DataRowView castedItem = clb.Items[i] as DataRowView;
                    int? id = Convert.ToInt32(castedItem[0]);
                    MouldChangerId_Checker = (int)id;

                    if (objItem.Contains(MouldChangerId_Checker))
                        clb.SetItemChecked(i, true);
                }
            }
        }

        private bool CheckExistOEE()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select * from OEE where CancelTag=0 and ShiftId=" + ShiftId + " and OEEId <> " + OEEId + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        //Changes on 25-09-2024
        private void SaveDB()
        {
            try
            {
                if (!ValidationOEE())
                {
                    if (!CheckExistOEE())
                    {
                        //PlantInchargeId = Convert.ToInt32(cmbPlantIncharge.SelectedValue);
                        //VolumeCheckerId = Convert.ToInt32(cmbVolumeChecker.SelectedValue);

                        if (OEEId != 0)
                            if (FlagDelete)
                                objBL.Query = "update OEE set CancelTag=1 where OEEId=" + OEEId + "";
                            else
                                objBL.Query = "update OEE set EntryDate='" + dtpEntryDate.Value.ToShortDateString() + "',EntryTime='" + dtpEntryTime.Value.ToShortTimeString() + "',ShiftId=" + ShiftId + ",ModifiedId=" + BusinessLayer.UserId_Static + " where OEEId=" + OEEId + "";
                        else
                            objBL.Query = "insert into OEE(EntryDate,EntryTime,ShiftId,UserId) values('" + dtpEntryDate.Value.ToShortDateString() + "','" + dtpEntryTime.Value.ToShortTimeString() + "'," + ShiftId + "," + BusinessLayer.UserId_Static + ")";

                        if (objBL.Function_ExecuteNonQuery() > 0)
                        {
                            if (OEEId == 0)
                                OEEId = objRL.ReturnMaxID_Fix("OEE", "OEEId");

                            MachineId = 0; OEEMachineId = 0;

                            //OEEType = "PlantIncharge";
                            //Save_Employee_PlantIncaharge_VolumeChecker(cmbPlantIncharge);

                            //OEEType = "VolumeChecker";
                            //Save_Employee_PlantIncaharge_VolumeChecker(cmbVolumeChecker);

                            //OEEType = "Supervisor";
                            //Save_Employee(clbSupervisor);

                            //OEEType = "MouldChanger";
                            //Save_Employee(clbMouldChanger);

                            //OEEType = "Porter";
                            //Save_Employee(clbPorter);

                            if (!FlagDelete)
                                objRL.ShowMessage(7, 1);
                            else
                                objRL.ShowMessage(9, 1);

                            //FillGrid();
                            //ClearAll();

                            FillGrid();
                            //gbMachineDetails.Visible = true;
                            //gbProductDetails.Visible = true;

                            tcOEE.SelectedIndex = 1;
                            txtShiftLength.Text = txtShiftHours.Text;
                            cmbMachineNo.Focus();
                        }
                    }
                    else
                    {
                        objRL.ShowMessage(41, 4);
                        return;
                    }
                }
                else
                {
                    objRL.ShowMessage(17, 4);
                    return;
                }
            }
            catch (Exception ex1)
            {

            }
            finally { }
        }

        //private void SaveDB_Old()
        //{
        //    try
        //    {
        //        CheckExistShift1();

        //        if (!ValidationOEE())
        //        {
        //            //CheckExist_OEE();
                    

        //            if (ShiftScheduleId != 0)
        //            {
        //                PlantInchargeId = Convert.ToInt32(cmbPlantIncharge.SelectedValue);
        //                VolumeCheckerId = Convert.ToInt32(cmbVolumeChecker.SelectedValue);

        //                if (OEEId != 0)
        //                    if (FlagDelete)
        //                        objBL.Query = "update OEE set CancelTag=1 where OEEId=" + OEEId + "";
        //                    else
        //                        objBL.Query = "update OEE set EntryDate='" + dtpShiftDate.Value.ToShortDateString() + "',EntryTime='" + dtpTime.Value.ToShortTimeString() + "',Shift='" + cmbShift.Text + "',ShiftScheduleId=" + ShiftScheduleId + ",ModifiedId=" + BusinessLayer.UserId_Static + " where OEEId=" + OEEId + "";
        //                else
        //                    objBL.Query = "insert into OEE(EntryDate,EntryTime,Shift,ShiftScheduleId,UserId) values('" + dtpShiftDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "','" + cmbShift.Text + "'," + ShiftScheduleId + "," + BusinessLayer.UserId_Static + ")";

        //                if (objBL.Function_ExecuteNonQuery() > 0)
        //                {
        //                    if (OEEId == 0)
        //                        OEEId = objRL.ReturnMaxID_Fix("OEE", "OEEId");

        //                    MachineId = 0; OEEMachineId = 0;

        //                    OEEType = "PlantIncharge";
        //                    Save_Employee_PlantIncaharge_VolumeChecker(cmbPlantIncharge);

        //                    OEEType = "VolumeChecker";
        //                    Save_Employee_PlantIncaharge_VolumeChecker(cmbVolumeChecker);

        //                    OEEType = "Supervisor";
        //                    Save_Employee(clbSupervisor);

        //                    OEEType = "MouldChanger";
        //                    Save_Employee(clbMouldChanger);

        //                    OEEType = "Porter";
        //                    Save_Employee(clbPorter);

        //                    if (!FlagDelete)
        //                        objRL.ShowMessage(7, 1);
        //                    else
        //                        objRL.ShowMessage(9, 1);

        //                    //FillGrid();
        //                    //ClearAll();

        //                    gbMachineDetails.Visible = true;
        //                    gbProductDetails.Visible = true;

        //                    tcOEE.SelectedIndex = 1;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            objRL.ShowMessage(17, 4);
        //            return;
        //        }
        //    }
        //    catch (Exception ex1)
        //    {

        //    }
        //    finally { }
        //}

        string OEEType = string.Empty;
        int EmployeeId = 0;

        private bool CheckExist_Employee()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select OEEId,ShiftId,OEEType,EmployeeId from OEEEmployee where CancelTag=0 and OEEId=" + OEEId + " and ShiftId=" + ShiftId + " and OEEType='" + OEEType + "' and EmployeeId=" + EmployeeId + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void Save_Employee(CheckedListBox clb)
        {
            EmployeeId = 0;
            foreach (object itemChecked in clb.CheckedItems)
            {
                DataRowView castedItem = itemChecked as DataRowView;
                int? id = Convert.ToInt32(castedItem[0]);
                EmployeeId = (int)id;

                if (EmployeeId != 0)
                {
                    if (!CheckExist_Employee())
                    {
                        objBL.Query = "insert into OEEEmployee(OEEId,ShiftId,OEEType,EmployeeId,UserId) values(" + OEEId + "," + ShiftId + ",'" + OEEType + "'," + EmployeeId + "," + BusinessLayer.UserId_Static + ")";
                        objBL.Function_ExecuteNonQuery();
                    }
                }
            }
        }

        private void Save_Employee_PlantIncaharge_VolumeChecker(ComboBox clb)
        {
            EmployeeId = 0;
            EmployeeId = Convert.ToInt32(clb.SelectedValue);

            if (EmployeeId != 0)
            {
                if (!CheckExist_Employee())
                {
                    objBL.Query = "insert into OEEEmployee(OEEId,ShiftId,OEEType,EmployeeId,MachineId,OEEMachineId,UserId) values(" + OEEId + "," + ShiftId + ",'" + OEEType + "'," + EmployeeId + "," + MachineId + "," + OEEMachineId + "," + BusinessLayer.UserId_Static + ")";
                    Result= objBL.Function_ExecuteNonQuery();
                }
            }
        }

        private void txtIdealRunRate_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void txtCavity_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void Get_SDLC_Report()
        {
            //if (cmbMachineNo.SelectedIndex > -1 && ProductId !=0 && cmbShift.SelectedIndex >-1)
            //{
            objRL.OEEId = OEEId;
            objRL.ShiftId = ShiftId;
            objRL.OEEMachineId = OEEMachineId;
            //objRL.ShiftScheduleId = ShiftScheduleId;
            objRL.MachineId = Convert.ToInt32(cmbMachineNo.SelectedValue);
            objRL.EntryDate = dtpShiftDate.Value;
            //objRL.Shift = cmbShift.Text;
            ViewReportW objForm = new ViewReportW(BusinessResources.R_OEEReportMachineWise);
            objForm.ShowDialog(this);
            // }
        }

        private void dgvOEE_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RowCount_Grid = dgvOEE.Rows.Count;
                CurrentRowIndex = dgvOEE.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    //0 O.OEEId,
                    //1 O.EntryDate as [Date],
                    //2 O.EntryTime as [Time],
                    //3 O.ShiftId,
                    //4 SEN.Shift,
                    //5 SEN.EntryDate as [Shift Date],
                    //6 SEN.ShifFromDate as [Shift Start],
                    //7 SEN.ShiftToDate as [Shift End],
                    //8 SEN.ShiftHours as [Shift Hours]

                    ClearAll();
                    //btnDelete.Enabled = true;

                    OEEId = 0;
                    OEEMachineId = 0;
                    //OEEEntryMachineId = 0;
                   //ShiftScheduleId = 0;

                   

                    dtpEntryDate.Value = Convert.ToDateTime(dgvOEE.Rows[e.RowIndex].Cells["clmEntryDate"].Value.ToString());
                    dtpEntryTime.Value = Convert.ToDateTime(dgvOEE.Rows[e.RowIndex].Cells["clmEntryTime"].Value.ToString());
                    dtpShiftDate.Value = Convert.ToDateTime(dgvOEE.Rows[e.RowIndex].Cells["clmShiftBeginTime"].Value.ToString());

                    ShiftId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvOEE.Rows[e.RowIndex].Cells["clmShiftId"].Value)));
                    GetShiftDetails();
                    txtShift.Text = objRL.Check_Null_String(Convert.ToString(dgvOEE.Rows[e.RowIndex].Cells["clmShift"].Value));
                    txtShiftHours.Text = objRL.Check_Null_String(Convert.ToString(dgvOEE.Rows[e.RowIndex].Cells["clmShiftHours"].Value));
                    dtpShiftStart.Value = Convert.ToDateTime(dgvOEE.Rows[e.RowIndex].Cells["clmShiftBeginTime"].Value.ToString());
                    dtpShiftEnd.Value = Convert.ToDateTime(dgvOEE.Rows[e.RowIndex].Cells["clmShiftEndTime"].Value.ToString());

                    OEEId = Convert.ToInt32(dgvOEE.Rows[e.RowIndex].Cells["clmOEEId"].Value);
                    txtOEEId.Text = OEEId.ToString();

                    //cmbShift.Text = objRL.Check_Null_String(Convert.ToString(dgvOEE.Rows[e.RowIndex].Cells["clmShift"].Value));
                    //Fill_ShiftHours();
                    //Set_Shift_GroupBox();
                    //cmbShiftHours.Text = objRL.Check_Null_String(Convert.ToString(dgvOEE.Rows[e.RowIndex].Cells["clmShiftHours"].Value));

                    //dtpShiftStart.Value = Convert.ToDateTime(dgvOEE.Rows[e.RowIndex].Cells["clmShiftBeginTime"].Value.ToString());
                    //dtpShiftEnd.Value = Convert.ToDateTime(dgvOEE.Rows[e.RowIndex].Cells["clmShiftEndTime"].Value.ToString());
                     
                    //cmbPlantIncharge.Text = objRL.Check_Null_String(Convert.ToString(dgvOEE.Rows[e.RowIndex].Cells["clmPlantIncharge"].Value));
                    //cmbVolumeChecker.Text = objRL.Check_Null_String(Convert.ToString(dgvOEE.Rows[e.RowIndex].Cells["clmVolumeChecker"].Value));

                    //OEEType = "MouldChanger";
                    //Fill_CheckListBox(clbMouldChanger);

                    //OEEType = "Supervisor";
                    //Fill_CheckListBox(clbSupervisor);

                    //OEEType = "Porter";
                    //Fill_CheckListBox(clbPorter);

                    //gbMachineDetails.Visible = true;

                    FillGrid_MachineEntry();
                    //Fill_Employee_Grid();
                    tcOEE.SelectedIndex = 1;
                    FillGrid_OperatorPacker();
                }
            }
            catch (Exception ex1)
            {
                objRL.ErrorMessge(ex1.ToString());
                return;
            }
            finally
            {
                GC.Collect();
            }
        }
         
        string PlantIncharge = string.Empty, VolumeChecker = string.Empty;

        private void tpOEE_Click(object sender, EventArgs e)
        {

        }

        private void btnChange_Click(object sender, EventArgs e)
        {

        }

        private void cmbShiftNew_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Set_Shift_Begin_End();
        }

        private void Set_Shift_Begin_End()
        {
            //if (cmbShift.SelectedIndex > -1)
            //{
            //    Shift = cmbShift.Text;
            //    //ShiftScheduleId = Convert.ToInt32(cmbShift.SelectedValue);

            //    if (Shift == "I")
            //    {
            //        dtpShiftStart.Text = "07:00";
            //        dtpShiftEnd.Text = "15:00";
            //    }
            //    else if (Shift == "II")
            //    {
            //        dtpShiftStart.Text = "15:00";
            //        dtpShiftEnd.Text = "23:00";
            //    }
            //    else if (Shift == "III")
            //    {
            //        dtpShiftStart.Text = "23:00";
            //        dtpShiftEnd.Text = "07:00";
            //    }
            //    else
            //    {

            //    }
            //}
        }

        private void Fill_ShiftHours()
        {
            //cmbShiftHours.Items.Clear();

            //if (cmbShift.SelectedIndex > -1)
            //{
            //    Shift = cmbShift.Text;

            //    if (Shift == "I")
            //    {
            //        cmbShiftHours.Items.Add("360");
            //        cmbShiftHours.Items.Add("480");
            //        cmbShiftHours.Items.Add("720");
            //    }
            //    else if (Shift == "II")
            //    {
            //        cmbShiftHours.Items.Add("360");
            //        cmbShiftHours.Items.Add("480");
            //        cmbShiftHours.Items.Add("720");
            //    }
            //    else if (Shift == "III")
            //    {
            //        //cmbHours.Items.Add("360");
            //        cmbShiftHours.Items.Add("480");
            //        //cmbHours.Items.Add("720");
            //    }
            //    else
            //    {

            //    }
            //}
        }

        //private void cmbShift_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    //Set_Shift_Begin_End();

        //    if(cmbShift.SelectedIndex >-1)
        //        Fill_ShiftHours();

        //    //if (cmbHours.SelectedIndex > -1 && cmbShift.SelectedIndex > -1)
        //    //    Set_Shift_GroupBox();
        //}

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            //CheckExistShift();
            
            //txtShift.Text= objRL.GetShift_By_Date(dtpShiftDate.Value);
        }

        private void Get_ShiftSchedule()
        {
            //CheckExistShift();
        }

        //private void CheckExistShift1()
        //{
        //    //cmbShift.SelectedIndex = -1;
        //    DataSet ds = new DataSet();
        //    WhereClause = string.Empty;
        //    MainQuery = string.Empty;

        //    MainQuery = "select ShiftScheduleId,EntryDate,EntryTime,FromDate,ToDate,ShiftDate,ShiftHours,NoOfShifts,BeginTime1,EndTime1,BeginTime2,EndTime2,BeginTime3,EndTime3,Naration from ShiftSchedule where CancelTag=0 and ShiftDate=#" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "#";

        //    //if (SearchTag)
        //    WhereClause = " and ShiftDate=#" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# order by ShiftScheduleId desc";

        //    objBL.Query = MainQuery + WhereClause;
        //    ds = objBL.ReturnDataSet();

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        //0 ShiftScheduleId,
        //        //1 EntryDate,
        //        //2 EntryTime,
        //        //3 FromDate,
        //        //4 ToDate,
        //        //5 ShiftDate as [Shift Date],
        //        //6 ShiftHours,
        //        //7 NoOfShifts,
        //        //8 BeginTime1,
        //        //9 EndTime1
        //        //10 BeginTime2,
        //        //11 EndTime2,
        //        //12 BeginTime3,
        //        //13 EndTime3
        //        //14 Naration,

        //        //cmbShift

        //        NoOfShifts = string.Empty;

        //        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["NoOfShifts"].ToString())))
        //        {
        //            CurrentTime = DateTime.Now;

        //            NoOfShifts = Convert.ToString(ds.Tables[0].Rows[0]["NoOfShifts"].ToString());
        //            ShiftScheduleId = Convert.ToInt32(ds.Tables[0].Rows[0]["ShiftScheduleId"].ToString());

        //            BeginTime1 = Convert.ToDateTime(ds.Tables[0].Rows[0]["BeginTime1"].ToString());
        //            EndTime1 = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndTime1"].ToString());
        //            BeginTime2 = Convert.ToDateTime(ds.Tables[0].Rows[0]["BeginTime2"].ToString());
        //            EndTime2 = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndTime2"].ToString());
        //            BeginTime3 = Convert.ToDateTime(ds.Tables[0].Rows[0]["BeginTime3"].ToString());
        //            EndTime3 = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndTime3"].ToString());

        //            TimeSpan CurrentTime1 = DateTime.Now.TimeOfDay;

        //            TimeSpan BTime1_TS = BeginTime1.TimeOfDay;
        //            TimeSpan ETime1_TS = EndTime1.TimeOfDay;
        //            TimeSpan BTime2_TS = BeginTime2.TimeOfDay;
        //            TimeSpan ETime2_TS = EndTime2.TimeOfDay;
        //            TimeSpan BTime3_TS = BeginTime3.TimeOfDay;
        //            TimeSpan ETime4_TS = EndTime3.TimeOfDay;

        //            //if (CurrentTime1 > BTime1_TS && CurrentTime1 < ETime1_TS)
        //            //    cmbShift.Text = "I";
        //            //else if (CurrentTime1 > BTime2_TS && CurrentTime1 < ETime2_TS)
        //            //    cmbShift.Text = "II";
        //            //else
        //            //    cmbShift.Text = "III";

        //            //else if (CurrentTime1 >= BTime3_TS && CurrentTime1 <= ETime4_TS)
        //            //    cmbShift.Text = "III";
        //        }
        //    }
        //    else
        //    {
        //        ShiftSchedule objForm = new ShiftSchedule();
        //        objForm.ShowDialog(this);
        //        CheckExistShift();
        //    }
        //}

        //private void cmbChangeFor_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    GetEmployee_Chang();
        //}

        string ChangeFor = string.Empty;

        //private void GetEmployee_Chang()
        //{
        //    //Supervisor Change
        //    //Mould Changer
        //    //Volume Checker
        //    //Porter Change
        //    //Packer Change
        //    //Operator Change
        //    //Packer Change
        //    //Product Change
        //    ChangeFor = string.Empty;

        //    if (cmbChangeFor.SelectedIndex > -1)
        //    {
        //        ChangeFor = cmbChangeFor.Text;

        //        if (ChangeFor == "Supervisor Change")
        //        {

        //        }
        //    }

        //}

        private void txtIdealRunRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtIdealRunRate);
        }

        private void txtProductionEntryId_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAll.Checked)
            {
                DateFlag = false;
                dtpSearchDate.Enabled = false;
                dtpSearchDate.Value= DateTime.Now.Date;
            }
            else
            {
                DateFlag = true;
                dtpSearchDate.Enabled = true;
                dtpSearchDate.Value = DateTime.Now.Date;
            }

            FillGrid();
        }

        private void txtSearchID_TextChanged_1(object sender, EventArgs e)
        {
            if (txtSearchID.Text != "")
                IDFlag = true;
            else
                IDFlag = false;

            FillGrid();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            Product objForm = new Product();
            objForm.ShowDialog(this);
            objRL.Fill_Item_ListBox_OEE(lbItem, txtSearchProductName.Text, "All");
        }

        private void cmbStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Set_Status();
        }

        private void Set_Status()
        {
            if (cmbStatus.SelectedIndex > -1)
            {
                if (cmbStatus.Text == "No Work")
                {
                    //ClearMachineValues();
                    Clear_Production_Planning();

                    //Set_No_Work_Values();
                    Fill_Balance_Length();
                    //gbProductDetails.Visible = true;
                    objRL.Get_Product_ID_By_Name("NA");
                    Fill_Product_Information();

                    txtNoPlanning.Text = txtBalanceLength.Text;
                     //gbProductionPlanning.Enabled = false;

                    
                }
                //else
                //    gbProductionPlanning.Enabled = true;
                //else
                //    gbProductDetails.Visible = true;
            }
        }

        //private void Set_No_Work_Values()
        //{
        //    //gbProductDetails.Visible = true;
        //    objRL.Get_Product_ID_By_Name("NA");
        //    Fill_Product_Information();
        //    cmbPacker.Text = "NA";
        //    cmbOperator.Text = "NA";
        //    cmbPreformLoadingAuto.Text = "NA";
        //    CheckItemByName("NA");
        //}

        //private void CheckItemByName(string itemName)
        //{
        //    for (int i = 0; i < clbPackerAuto.Items.Count; i++)
        //    {
        //        if (clbPackerAuto.Items[i].ToString() == itemName)
        //        {
        //            clbPackerAuto.SetItemChecked(i, true);
        //            break; // Exit the loop once the item is found and checked
        //        }
        //    }
        //}

        private void txtShiftLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtShiftLength);
        }

        private void txtShiftLength_TextChanged(object sender, EventArgs e)
        {
            if (txtShiftLength.Text != "")
            {
                ShiftLength = Convert.ToDouble(txtShiftLength.Text);
                Calculations();
            }
        }

        double MachineAvailableTime = 0, BalanceLength=0;

        private void Fill_Balance_Length()
        {
            BalanceLength = 0;
            DataSet ds = new DataSet();
            objBL.Query = "select sum(MachineAvailableTime) from OEEMachine where CancelTag=0 and OEEId=" + OEEId + " and MachineId=" + cmbMachineNo.SelectedValue + " and ShiftId=" + ShiftId + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                MachineAvailableTime = objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0][0])));

            BalanceLength = ShiftLength - MachineAvailableTime;

            if (BalanceLength == 0)
                BalanceLength = ShiftLength;
            
            txtMachineAvailableTime.Text = BalanceLength.ToString();
            txtBalanceLength.Text = BalanceLength.ToString();

            //if (BalanceLength <= 0)
            //    gbProductionPlanning.Enabled = false;
            //else
            //    gbProductionPlanning.Enabled = true;
        }

        //private void cmbShiftHours_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    if (cmbShiftHours.SelectedIndex > -1 && cmbShift.SelectedIndex >-1)
        //    {
        //        Set_Shift_GroupBox();
        //    }
        //}

        private void Set_Shift(string BeginTime, string EndTime)
        {
            //if (cmbHours.Text == "360")
            //{
               
            //}
            //if (cmbHours.Text == "480")
            //{
            //    dtpShiftBegin.Text = "15:00";
            //    dtpShiftEnd.Text = "23:00";
            //}
            //if (cmbHours.Text == "720")
            //{
            //    dtpShiftBegin.Text = "23:00";
            //    dtpShiftEnd.Text = "07:00";
            //}

            if (BeginTime != "" && EndTime != "")
            {
                dtpShiftStart.Text = BeginTime;
                dtpShiftEnd.Text = EndTime;
            }
        }

        string ShiftHours = string.Empty;

        private void Set_Shift_GroupBox()
        {
            Shift = string.Empty; ShiftHours = string.Empty;

            //if (cmbShiftHours.SelectedIndex > -1 && cmbShift.SelectedIndex >-1)
            //{
            //    Shift = cmbShift.Text; 
            //    ShiftHours = cmbShiftHours.Text;
            //    txtShiftLength.Text = cmbShiftHours.Text;

            //    if (Shift == "I")
            //    {
            //        if (ShiftHours == "360")
            //            Set_Shift("07:00", "13:00");
            //        else if (ShiftHours == "480")
            //            Set_Shift("07:00", "15:00");
            //        else if (ShiftHours == "720")
            //            Set_Shift("07:00", "19:00");
            //        else
            //        {

            //        }
            //    }
            //    if (Shift == "II")
            //    {
            //        if (ShiftHours == "360")
            //            Set_Shift("13:00", "19:00");
            //        else if (ShiftHours == "480")
            //            Set_Shift("15:00", "23:00");
            //        else if (ShiftHours == "720")
            //            Set_Shift("19:00", "07:00");
            //        else
            //        {
            //        }
            //    }
            //    if (Shift == "III")
            //    {
            //        if (ShiftHours == "360")
            //            Set_Shift("19:00", "23:00");
            //        else if (ShiftHours == "480")
            //            Set_Shift("23:00", "07:00");
            //        else
            //        {
            //        }
            //    }
            //}

            //if (clbNoOfShift.Items[i].ToString() == "1st Shift")
            //{
            //    gb1.Visible = true;
            //    dtpBeginTime1.Text = "07:00";
            //    dtpEndTime1.Text = "15:00";
            //}
            //if (clbNoOfShift.Items[i].ToString() == "2nd Shift")
            //{
            //    gb2.Visible = true;
            //    dtpBeginTime2.Text = "15:00";
            //    dtpEndTime2.Text = "23:00";
            //}
            //if (clbNoOfShift.Items[i].ToString() == "3rd Shift")
            //{
            //    gb3.Visible = true;
            //    dtpBeginTime3.Text = "23:00";
            //    dtpEndTime3.Text = "07:00";
            //}
        }

        private void btnClearOEE_Click(object sender, EventArgs e)
        {
            //ClearMachineValues();
            Clear_Machine_Details();
            Clear_Production_Planning();
            ClearAll_Item();
            OEEMachineId = 0;
            GridFlag = false;
            FlagDelete = false;
        }

        //private void ClearMachineValues()
        //{
        //    btnDelete.Visible = false;
        //    ProductId = 0;

        //    //GridFlag = false;

        //    if(!GridFlag)
        //        OEEMachineId = 0;
            
        //    txtSearchProductName.Text = "";
        //    lblProductName.Text = "";
        //    txtSearchProductName.Text = "";
        //    //cmbStatus.SelectedIndex = -1;
        //    cmbReason.SelectedIndex = -1;
        //    txtNaration.Text = "";
           
        //    txtShiftLength.Text = txtShiftHours.Text;
        //    txtShortMealBreakB.Text = "";
        //    txtNoPlanning.Text = "";
        //    txtNoElectricity.Text = "";
        //    txtTotalProductionTime.Text = "";
        //    txtBreakdown.Text = "";
        //    txtChangeover.Text = "";
        //    txtManpowerShortage.Text = "";
        //    txtStartupLoss.Text = "";

        //    txtMaintainanceMachineShutDownTime.Text = "";
        //    txtMaterialNotAvailable.Text = "";
        //    txtTotalDowntime.Text = "";
        //    txtOperatingTime.Text = "";
        //    txtAvailabilty.Text = "";
        //    txtIdealRunRate.Text = "";

        //    txtCavity.Text = "";
        //    txtPlanningQty.Text = "";
        //    txtTotalShot.Text = "";
        //    txtTotalProductionInNos.Text = "";
        //    txtPacking.Text = "";
        //    txtTotalPacket.Text = "";

        //    txtTargetProduction.Text = "";
        //    txtPerformance.Text = "";
        //    txtPreformRejection.Text = "";
        //    txtRejectInNos.Text = "";
        //    txtGoodInNos.Text = "";
        //    txtQuality.Text = "";
        //    txtOEE.Text = "";

        //    //if (cmbShiftHours.SelectedIndex > -1)
        //    //    txtShiftLength.Text = cmbShiftHours.Text;
        //}

        private void btnReport_Click(object sender, EventArgs e)
        {
            GET_RDLC_Report();   
        }

        private void GET_RDLC_Report()
        {
            //if (cmbMachineNo.SelectedIndex > -1 && ProductId !=0 && cmbShift.SelectedIndex >-1)
            //{
            objRL.OEEId = OEEId;
            objRL.OEEMachineId = OEEMachineId;
            //objRL.ShiftScheduleId = ShiftScheduleId;
            objRL.MachineId = Convert.ToInt32(cmbMachineNo.SelectedValue);
            objRL.EntryDate = dtpShiftDate.Value;
            //objRL.Shift = cmbShift.Text;

            //objRL.OEEId = OEEId;
            //objRL.MachineId = MachineId;
            ViewReportW objForm = new ViewReportW(BusinessResources.R_OEEReportMachineWise);
            objForm.ShowDialog(this);
            // }
        }

        private void dgvMachine_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RowCount_Grid = dgvMachine.Rows.Count;
                CurrentRowIndex = dgvMachine.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    //0 OM.OEEMachineId,
                    //1 OM.EntryDate as [Date],
                    //2 Format([OM.EntryTime], 'HH:mm') AS [Time],
                    //3 OM.OEEId,
                    //4 OM.ShiftId,
                    //5 SEN.EntryDate as [Shift Date],
                    //6 SEN.ShifFromDate as [Shift Start],
                    //7 SEN.ShiftToDate as [Shift End],
                    //8 SEN.Shift,
                    //9 SEN.ShiftHours as [Shift Hours],
                    //10 OM.MachineId,
                    //11 MM.MachineNo,
                    //12 OM.ProductId,
                    //13 P.ProductName as [Product Name],
                    //14 OM.ShiftLengthA,
                    //15 OM.MachineAvailableTime,
                    //16 OM.BalanceLength
                    //17 OM.ShortMealBreakB,
                    //18 OM.NoPlanning,
                    //19 OM.NoElectricity,
                    //20 OM.TotalProductionTime,
                    //21 OM.Breakdown,
                    //22 OM.Changeover,
                    //23 OM.ManpowerShortage,
                    //24 OM.StartupLoss,
                    //25 OM.MaintainanceMachineShutDownTime,
                    //26 OM.MaterialNotAvailable,
                    //27 OM.TotalDowntime,
                    //28 OM.OperatingTime,
                    //29 OM.Availabilty,
                    //30 OM.IdealRunRate,
                    //31 OM.Cavity,
                    //32 OM.PlanningQty,
                    //33 OM.TotalShot,
                    //34 OM.TotalProductionInNos,
                    //35 OM.Packing,
                    //36 OM.TotalPacket,
                    //37 OM.TargetProduction,
                    //38 OM.Performance,
                    //39 OM.PreformRejection,
                    //40 OM.RejectInNos,
                    //41 OM.GoodInNos,
                    //42 OM.Quality,
                    //43 OM.OEE,
                    //44 OM.Status,
                    //45 OM.Reason,
                    //46 OM.Remarks,

                    GridFlag = true;
                    btnDeleteOEE.Visible = true;
                    OEEMachineId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[0].Value)));
                    OEEId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[3].Value)));

                    cmbMachineNo.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[10].Value));
                    Fill_MachineDetails();

                    //OEEMachineId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[10].Value)));
                    
                    cmbStatus.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[44].Value));
                    cmbReason.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[45].Value));
                    txtRemarks.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[46].Value));

                    ProductId =objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[12].Value)));
                    Fill_Product_Information();

                    txtShiftLength.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[14].Value));
                    txtMachineAvailableTime.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[15].Value));
                    txtBalanceLength.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[16].Value));

                    txtShortMealBreakB.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[17].Value));
                    txtNoPlanning.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[18].Value));
                    txtNoElectricity.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[19].Value));
                    txtTotalProductionTime.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[20].Value));
                    txtBreakdown.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[21].Value));
                    txtChangeover.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[22].Value));
                    txtManpowerShortage.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[23].Value));

                    txtStartupLoss.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[24].Value));
                    txtMaintainanceMachineShutDownTime.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[25].Value));
                    txtMaterialNotAvailable.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[26].Value));
                    txtTotalDowntime.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[27].Value));
                    txtOperatingTime.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[28].Value));
                    txtAvailabilty.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[29].Value));
                    txtIdealRunRate.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[30].Value));
                    txtCavity.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[31].Value));

                    txtPlanningQty.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[32].Value));
                    txtTotalShot.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[33].Value));
                    txtTotalProductionInNos.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[34].Value));
                    txtPacking.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[35].Value));
                    txtTotalPacket.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[36].Value));
                    txtTargetProduction.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[37].Value));
                    txtPerformance.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[38].Value));

                    txtPreformRejection.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[39].Value));
                    txtRejectInNos.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[40].Value));
                    txtGoodInNos.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[41].Value));
                    txtQuality.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[42].Value));
                    txtOEE.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[43].Value));

                   // cmbStatus.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[44].Value));

                    //OEEType = "Operator";
                    //cmbOperator.Text = GetEmployee();

                    //OEEType = "Packer";
                    //cmbPacker.Text = GetEmployee();

                    GridFlag = true;

                    //tpEmployees.Hide();

                    //cmbOperator.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[43].Value));
                    //cmbPacker.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[43].Value));
                    //cmbPreformLoadingAuto.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[43].Value));
                    //clbPackerAuto.Text = objRL.Check_Null_String(Convert.ToString(dgvMachine.Rows[e.RowIndex].Cells[43].Value));
                }
            }
            catch (Exception ex1)
            {
                objRL.ErrorMessge(ex1.ToString());
                return;
            }
            finally
            {
                GC.Collect();
            }

        }

        private void dtpShiftDate_Leave(object sender, EventArgs e)
        {
            GetShiftDetails();
        }

        private void txtMachineAvailableTime_TextChanged(object sender, EventArgs e)
        {

        }

        bool SearchProduct = false;
        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            if(txtSearchProduct.Text !="")
                SearchProduct = true;
            else
                SearchProduct = false;

            FillGrid_MachineEntry();
        }

        //private void cmbPlantIncharge_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //        cmbVolumeChecker.Focus();
        //}

        //private void cmbVolumeChecker_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //        clbSupervisor.Focus();
        //}

        private void txtPreformRejection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRejectInNos.Focus();
        }

        private bool Validation_Employee()
        {
            objEP.Clear();
            if (OEEId == 0)
            {
                objEP.SetError(txtOEEId, "Enter OEE Id");
                txtOEEId.Focus();
                return true;
            }
            else if (txtOEEId.Text == "")
            {
                objEP.SetError(txtOEEId, "Enter OEE Id");
                txtOEEId.Focus();
                return true;
            }
            else if (ShiftId == 0)
            {
                objEP.SetError(txtShift, "Enter Shift");
                txtShift.Focus();
                return true;
            }
            else if (objRL.ShiftHours == "")
            {
                objEP.SetError(txtShiftHours, "Enter Shift Hours");
                txtShiftHours.Focus();
                return true;
            }
            else if (cmbEmployeeOperatorPacker.SelectedIndex == -1)
            {
                objEP.SetError(cmbEmployeeOperatorPacker, "Select Employee Name");
                cmbEmployeeOperatorPacker.Focus();
                return true;
            }
            else if (cmbOriginalDesignationOperatorPacker.SelectedIndex == -1)
            {
                objEP.SetError(cmbOriginalDesignationOperatorPacker, "Select Original Designation Operator Packer");
                cmbOriginalDesignationOperatorPacker.Focus();
                return true;
            }
            else if (cmbDesignationOperatorPacker.SelectedIndex == -1)
            {
                objEP.SetError(cmbDesignationOperatorPacker, "Select Designation");
                cmbDesignationOperatorPacker.Focus();
                return true;
            }
            else if (cmbStatusOperatorPacker.SelectedIndex == -1)
            {
                objEP.SetError(cmbStatusOperatorPacker, "Select Status");
                cmbStatusOperatorPacker.Focus();
                return true;
            }
            else
                return false;
        }

        int DataGridIndex = 0;

        private void btnAddOperatorPacker_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB_OEEOperatorPacker();
        }

        int OEEEmployeeId = 0;

        //private bool Check_Exist()
        //{

        //}

        private void SaveDB_OEEOperatorPacker()
        {
            if (!Validation_Employee() && !Validation_Grade())
            {
                if (OEEEmployeeId == 0)
                    //objBL.Query = "insert into OEEEmployeeNew(OEEId,ShiftId,ShiftDate,Shift,Designation,EmployeeId,MachineNo,Status,ChangeReason,Remarks,Shots,Packets,Grade,Amount,UserId) values(" + OEEId + "," + ShiftId + ",'" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "','" + txtShift.Text + "','" + cmbDesignationOperatorPacker.Text + "'," + cmbOperatorPacker.SelectedValue + ",'" + cmbMachineNoOperatorPacker.Text + "','" + cmbStatusOperatorPacker.Text + "','" + cmbRemarksOperatorPacker.Text + "','" + txtShotsOperatorPacker.Text + "','" + txtPacketsEmployee.Text + "','" + cmbGradeOperatorPacker.Text + "','" + txtAmountOperatorPacker.Text + "'," + BusinessLayer.UserId_Static + ") ";
                    objBL.Query = "insert into OEEOperatorPacker(OEEId,ShiftId,ShiftDate,Shift,EmployeeId,OriginalDesignation,Designation,DesignationFlag,Status,MachineId,MachineNo,Shots,Grade,Amount,Remarks,UserId) values(" + OEEId + "," + ShiftId + ",'" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "','" + txtShift.Text + "'," + cmbEmployeeOperatorPacker.SelectedValue + ",'" + cmbOriginalDesignationOperatorPacker.Text + "','" + cmbDesignationOperatorPacker.Text + "','" + lblDesignationFlag.Text + "','" + cmbStatusOperatorPacker.Text + "'," + cmbMachineNoOperatorPacker.SelectedValue + ",'" + cmbMachineNoOperatorPacker.Text + "','" + txtShotsOperatorPacker.Text + "','" + cmbGradeOperatorPacker.Text + "','" + txtAmountOperatorPacker.Text + "','" + cmbRemarksOperatorPacker.Text + "'," + BusinessLayer.UserId_Static + ") ";
                else
                {
                    if(!FlagDelete)
                        objBL.Query = "update OEEOperatorPacker set OEEId=" + OEEId + ",ShiftId=" + ShiftId + ",ShiftDate='" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "',Shift='" + txtShift.Text + "',EmployeeId=" + cmbEmployeeOperatorPacker.SelectedValue + ",OriginalDesignation='" + cmbOriginalDesignationOperatorPacker.Text + "',Designation='" + cmbDesignationOperatorPacker.Text + "',DesignationFlag='" + lblDesignationFlag.Text + "',Status='" + cmbStatusOperatorPacker.Text + "',MachineId=" + cmbMachineNoOperatorPacker.SelectedValue + ",MachineNo='" + cmbMachineNoOperatorPacker.Text + "',Shots='" + txtShotsOperatorPacker.Text + "',Grade='" + cmbGradeOperatorPacker.Text + "',Amount='" + txtAmountOperatorPacker.Text + "',Remarks='" + cmbRemarksOperatorPacker.Text + "',ModifiedId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and OEEEmployeeId=" + OEEEmployeeId + "";
                    else
                        objBL.Query = "update OEEOperatorPacker set CancelTag=1,ModifiedId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and OEEEmployeeId=" + OEEEmployeeId + "";
                }

                Result = objBL.Function_ExecuteNonQuery();

                if (Result > 0)
                {
                    string ColumnName = string.Empty, ColumnValue = string.Empty;
                    WhereClause = string.Empty;

                    if (!FlagDelete)
                        objRL.ShowMessage(7, 1);
                    else
                        objRL.ShowMessage(9, 1);

                    ClearAll_OperatorPacker();
                    FillGrid_OperatorPacker();

                    //Insert Records
                    //Supervisor
                    //Loading
                    //Plant Incharge
                    //Volume Checker
                    //Mould Changer
                    //Operator
                    //Packer
                    //Porter
                    
                    //if (Convert.ToInt32(cmbMachineNo.Text) < 10)
                    //{
                    //    ColumnName = " OperatorName ";
                    //    ColumnValue = cmbEmployeeName.Text;
                    //    WhereClause = " and MachineId=" + cmbMachineNoEmployee.SelectedValue + "";
                    //}
                    //else
                    //{

                    //}

                    //if (cmbDesignationEmployee.Text == "Operator")
                    //{
                    //    if (Convert.ToInt32(cmbMachineNo.Text) < 10)
                    //    {
                    //        ColumnName = " OperatorName ";
                    //        ColumnValue = cmbEmployeeName.Text;
                    //        WhereClause = " and MachineId=" + cmbMachineNoEmployee.SelectedValue + "";
                    //    }
                    //    //objBL.Query = "update OEEMachine set OperatorName='" + cmbEmployeeName.Text + "' where CancelTag=0 and MachineId=" + cmbMachineNoEmployee.SelectedValue + " and OEEId=" + OEEId + " ";
                    //}
                    //else if(cmbDesignationEmployee.Text == "Packer")
                    //{
                    //    ColumnName = " PackerName ";
                    //    ColumnValue = cmbEmployeeName.Text;
                    //    WhereClause = " and MachineId=" + cmbMachineNoEmployee.SelectedValue + "";
                    //}
                    //else if (cmbDesignationEmployee.Text == "Preform Loader")
                    //{
                    //    ColumnName = " PreformLoaderName ";
                    //    ColumnValue = cmbEmployeeName.Text;
                    //    WhereClause = " and MachineId=" + cmbMachineNoEmployee.SelectedValue + "";
                    //}
                    //else
                    //{
                    //}

                    //if (ColumnName != "")
                    //{
                    //    objBL.Query = "update OEEMachine set " + ColumnName + "='" + ColumnValue + "' where CancelTag=0 and OEEId=" + OEEId + WhereClause + "";
                    //    Result = objBL.Function_ExecuteNonQuery();
                    //}
                }
            }
        }

        double TotalAmount_Final = 0;
        private void FillGrid_OperatorPacker()
        {
            TotalAmount_Final = 0; txtTotalAmountOperatorPacker.Text = "";
            dgvOperatorPacker.Rows.Clear();
            DataGridIndex = 0;
            DataSet ds = new DataSet();
            objBL.Query = "select OEEE.OEEEmployeeId,OEEE.OEEId,OEEE.ShiftId,OEEE.ShiftDate,OEEE.Shift,OEEE.EmployeeId,E.FullName,OEEE.OriginalDesignation,OEEE.Designation,OEEE.DesignationFlag,OEEE.Status,OEEE.MachineId,OEEE.MachineNo,OEEE.Shots,OEEE.Grade,OEEE.Amount,OEEE.Remarks from OEEOperatorPacker OEEE inner join Employee E on E.ID=OEEE.EmployeeId where OEEE.CancelTag=0 and E.CancelTag=0 and OEEE.OEEId=" + OEEId + " and OEEE.ShiftId=" + ShiftId + " order by OEEE.OEEEmployeeId desc";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataGridIndex = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgvOperatorPacker.Rows.Add();
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmOEEEmployeeId"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEEEmployeeId"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmOEEIdEmployee"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEEId"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmShiftIdEmployee"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftId"]));
                    DateTime dtS;
                    dtS = Convert.ToDateTime(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftDate"])));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmShiftDateEmployee"].Value = dtS.ToString(BusinessResources.DATEFORMATDDMMYYYY);
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmShiftEmployee"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmEmployeeId"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["EmployeeId"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmEmployeeName"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["FullName"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmOriginalDesignation"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OriginalDesignation"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmDesignation"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Designation"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmDesignationFlag"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["DesignationFlag"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmStatusEmployee"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Status"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmMachineId"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MachineId"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmMachineNo"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MachineNo"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmShots"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shots"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmGrade"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Grade"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmAmount"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Amount"]));
                    dgvOperatorPacker.Rows[DataGridIndex].Cells["clmRemarks"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Remarks"]));
                    //dgvOperatorPacker.Rows[DataGridIndex].Cells["clmDelete"].Value = "Delete";

                    TotalAmount_Final += objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Amount"])));
                    DataGridIndex++;
                }

                txtTotalAmountOperatorPacker.Text = TotalAmount_Final.ToString();
            }
            else
            {

            }

            //if (!Validation_Employee())
            //{
            //    if (dgvEmployee.Rows.Count > 0)
            //        DataGridIndex = dgvEmployee.Rows.Count;
            //    else
            //        DataGridIndex = 0;

            //    dgvEmployee.Rows.Add();
            //    dgvEmployee.Rows[DataGridIndex].Cells["clmOEEIdEmployee"].Value = OEEId.ToString();
            //    dgvEmployee.Rows[DataGridIndex].Cells["clmShiftIdEmployee"].Value = ShiftId.ToString();
            //    dgvEmployee.Rows[DataGridIndex].Cells["clmShiftDateEmployee"].Value = dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);
            //    dgvEmployee.Rows[DataGridIndex].Cells["clmEmployeeId"].Value = cmbEmployeeName.SelectedValue.ToString();
            //    dgvEmployee.Rows[DataGridIndex].Cells["clmEmployeeName"].Value = cmbEmployeeName.Text;
            //    dgvEmployee.Rows[DataGridIndex].Cells["clmDesignation"].Value = cmbDesignationEmployee.Text;
            //    dgvEmployee.Rows[DataGridIndex].Cells["clmMachineId"].Value = cmbMachineNo.Text;
            //    dgvEmployee.Rows[DataGridIndex].Cells["clmMachineNo"].Value = cmbMachineNo.Text;
            //    dgvEmployee.Rows[DataGridIndex].Cells["clmStatusEmployee"].Value = cmbStatusEmployee.Text;
            //    dgvEmployee.Rows[DataGridIndex].Cells["clmChangeReason"].Value = cmbChangeReasonEmployee.Text;
            //    dgvEmployee.Rows[DataGridIndex].Cells["clmRemarks"].Value = txtRemarksEmployee.Text;
            //    dgvEmployee.Rows[DataGridIndex].Cells["clmDelete"].Value = "Delete";
            //    DataGridIndex++;
            //    FillSRNO(dgvEmployee);
            //    Clear_Employee();
            //}
            //else
            //{
            //    objRL.ShowMessage(17, 4);
            //    return;
            //}
        }

        private void ClearAll_OperatorPacker()
        {
            FlagDelete = false;
            objEP.Clear();
            GridFlag = false;
            DataGridIndex = 0;
            OEEEmployeeId = 0;
            btnDeleteOperatorPacker.Visible = false;
            cmbEmployeeOperatorPacker.SelectedIndex = -1;
            cmbOriginalDesignationOperatorPacker.SelectedIndex = -1;
            cmbDesignationOperatorPacker.SelectedIndex = -1;
            lblDesignationFlag.Text = "";
            cmbStatusOperatorPacker.SelectedIndex = -1;
            cmbMachineNoOperatorPacker.SelectedIndex = -1;
            txtShotsOperatorPacker.Text = "";
            cmbGradeOperatorPacker.SelectedIndex = -1;
            txtAmountOperatorPacker.Text = "";
            cmbRemarksOperatorPacker.SelectedIndex = -1;
            txtTotalAmountOperatorPacker.Text = "";
            lblCountOP.Text = "";
        }


        //int SrNo = 1;

        private void FillSRNO(DataGridView dgv)
        {
            SrNo = 1;
            if (dgv.Rows.Count > 0)
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].Cells[0].Value = SrNo.ToString();
                    SrNo++;
                }
            }
        }

        private void cmbDesignationOperatorPacker_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_Employees();
            Check_Designation_Flag();
            //Get_Porter_Packets_Count();
        }

        private void Fill_Grade_Operator_Packer()
        {
            objRL.Fill_Grade_By_GradeDesignation(cmbGradeOperatorPacker, cmbDesignationOperatorPacker.Text);
        }

        private void Check_Designation_Flag()
        {
            lblDesignationFlag.ForeColor = Color.Black;
            if (cmbOriginalDesignationOperatorPacker.SelectedIndex > -1 && cmbDesignationOperatorPacker.SelectedIndex > -1)
            {
                if (cmbDesignationOperatorPacker.Text != cmbOriginalDesignationOperatorPacker.Text)
                {
                    lblDesignationFlag.Text = "Yes";
                    lblDesignationFlag.ForeColor = Color.Red;
                }
                else
                {
                    lblDesignationFlag.Text = "No";
                }
            }
        }

        private void Fill_Employees()
        {
            WhereClause = string.Empty;
            ColumnName = string.Empty;

            //Designation
            //Owner
            //Partner
            //Plant Incharge
            //Supervisor
            //Operator
            //Packer
            //Helper
            //Accountant
            //IT Manager
            //HR

            if (cmbDesignationOperatorPacker.SelectedIndex > -1)
            {
                Fill_Grade_Operator_Packer();

                //objRL.Fill_Employee_By_Designation(cmbEmployeeName, cmbDesignationEmployee.Text);
                PacketsEmployee = 0;
                cmbMachineNoOperatorPacker.SelectedIndex = -1;
                label63.Visible = false;
                cmbMachineNoOperatorPacker.Visible = false;


                if (cmbDesignationOperatorPacker.Text == "Operator" || cmbDesignationOperatorPacker.Text == "Packer")
                {
                    cmbMachineNoOperatorPacker.SelectedIndex = -1;
                    label63.Visible = true;
                    cmbMachineNoOperatorPacker.Visible = true;
                }
                //else if (cmbDesignationOperatorPacker.Text == "Porter")
                //{
                //    //TotalPacket
                    
                //    ColumnName = "TotalPacket";
                //    PacketsEmployee = Get_Shots_Packets_Query();
                //}
                else
                {
                    PacketsEmployee = 0;
                    cmbMachineNoOperatorPacker.SelectedIndex = -1;
                    label63.Visible = false;
                    cmbMachineNoOperatorPacker.Visible = false;
                }
                 
            }
        }

        private void btnClearOperatorPacker_Click(object sender, EventArgs e)
        {
            ClearAll_OperatorPacker();
        }

        private void dgvEmployee_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RowCount_Grid = 0;
                CurrentRowIndex = 0;
                RowCount_Grid = dgvOperatorPacker.Rows.Count;
                CurrentRowIndex = dgvOperatorPacker.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    ClearAll_OperatorPacker();
                    GridFlag = true;
                    btnDeleteOperatorPacker.Visible = true;
                    OEEEmployeeId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvOperatorPacker.Rows[e.RowIndex].Cells["clmOEEEmployeeId"].Value)));
                    cmbEmployeeOperatorPacker.Text = objRL.Check_Null_String(Convert.ToString(dgvOperatorPacker.Rows[e.RowIndex].Cells["clmEmployeeName"].Value));
                    Fill_Employee_Original_Designation();
                    //Fill_Employees();
                    cmbOriginalDesignationOperatorPacker.Text = objRL.Check_Null_String(Convert.ToString(dgvOperatorPacker.Rows[e.RowIndex].Cells["clmOriginalDesignation"].Value));
                    cmbDesignationOperatorPacker.Text = objRL.Check_Null_String(Convert.ToString(dgvOperatorPacker.Rows[e.RowIndex].Cells["clmDesignation"].Value));
                    lblDesignationFlag.Text = objRL.Check_Null_String(Convert.ToString(dgvOperatorPacker.Rows[e.RowIndex].Cells["clmDesignationFlag"].Value));
                    cmbStatusOperatorPacker.Text = objRL.Check_Null_String(Convert.ToString(dgvOperatorPacker.Rows[e.RowIndex].Cells["clmStatusEmployee"].Value));
                    cmbMachineNoOperatorPacker.Text = objRL.Check_Null_String(Convert.ToString(dgvOperatorPacker.Rows[e.RowIndex].Cells["clmMachineNo"].Value));
                    txtShotsOperatorPacker.Text = objRL.Check_Null_String(Convert.ToString(dgvOperatorPacker.Rows[e.RowIndex].Cells["clmShots"].Value));
                    cmbGradeOperatorPacker.Text = objRL.Check_Null_String(Convert.ToString(dgvOperatorPacker.Rows[e.RowIndex].Cells["clmGrade"].Value));
                    txtAmountOperatorPacker.Text = objRL.Check_Null_String(Convert.ToString(dgvOperatorPacker.Rows[e.RowIndex].Cells["clmAmount"].Value));
                    cmbRemarksOperatorPacker.Text = objRL.Check_Null_String(Convert.ToString(dgvOperatorPacker.Rows[e.RowIndex].Cells["clmRemarks"].Value));
                    //txtRemarksEmployee.Text = objRL.Check_Null_String(Convert.ToString(dgvOperatorPacker.Rows[e.RowIndex].Cells["clmRemarks"].Value));
                }
            }
            catch (Exception ex1)
            {
                objRL.ErrorMessge(ex1.ToString());
                return;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnDeleteOEE_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            DialogResult dr;
            dr = objRL.Delete_Record_Show_Message();
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                FlagDelete = true;
                SaveDB_OEEMachine();
                btnDeleteOEE.Visible = false;
            }
        }

        private void dtpOEEReportDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbMachineNoEmployee_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtShotsOperatorPacker.Text = "";
            Get_Shots_Packets();
        }

        string ColumnName=string.Empty;
        private double Get_Shots_Packets_Query()
        {
            double ReturnValue = 0;
            DataSet ds = new DataSet();
            objBL.Query = "Select sum(Val(" + ColumnName + ")) from OEEMachine where CancelTag=0 and OEEId=" + OEEId +  WhereClause ;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ReturnValue = objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0][0])));
            }

            return ReturnValue;

        }

        double GradeEmployee = 0, ShotsEmployee = 0, PacketsEmployee=0;


        private void Get_Shots_Packets()
        {
            WhereClause = string.Empty;
            ColumnName = string.Empty;
            ShotsEmployee = 0;

            if (cmbMachineNoOperatorPacker.SelectedIndex > -1 && cmbDesignationOperatorPacker.SelectedIndex >-1)
            {
                if (cmbDesignationOperatorPacker.Text == "Operator" || cmbDesignationOperatorPacker.Text == "Packer")
                {
                   ColumnName = "TotalShot";
                   WhereClause = " and MachineId=" + cmbMachineNoOperatorPacker.SelectedValue + " ";
                   ShotsEmployee = Get_Shots_Packets_Query();

                   txtShotsOperatorPacker.Text = Convert.ToString(ShotsEmployee);
                   cmbGradeOperatorPacker.Text = Calculate_Grade(ShotsEmployee);
                   Fill_Grade_Amount();
                    //TotalShot
                }
                else
                {

                }
            }
        }

        private string Calculate_Grade(double Values)
        {
            string CalculateGrade = string.Empty;
            if (cmbDesignationOperatorPacker.Text == "Operator")
            {
                //IF(F5<1,"0",
                //IF(F5<2100,"B",
                //IF(F5<2400,"A",
                //IF(F5<2700,"A+",
                //IF(F5<3000,"A++",
                //IF(F5<3300,"AAA",
                //IF(F5<3600,"3*",
                //IF(F5<3900,"5*",

                if (Values < 1)
                    CalculateGrade = "0";
                else if (Values >1 && Values < 2100)
                    CalculateGrade = "B";
                else if (Values > 2100 && Values < 2400)
                    CalculateGrade = "A";
                else if (Values > 2400 && Values < 2700)
                    CalculateGrade = "A+";
                else if (Values > 2700 && Values < 3000)
                    CalculateGrade = "A++";
                else if (Values > 3000 && Values < 3300)
                    CalculateGrade = "AAA";
                else if (Values > 3300 && Values < 3600)
                    CalculateGrade = "3*";
                else if (Values > 3600 && Values < 3900)
                    CalculateGrade = "5*";
                else
                {
                    CalculateGrade = "0";
                }
            }
            else if (cmbDesignationOperatorPacker.Text == "Packer")
            {
                //IF(S4<1,"0",
                //IF(S4<2100,"B",
                //IF(S4<2400,"A",
                //IF(S4<2700,"A+",
                //IF(S4<3000,"A++",
                //IF(S4<3300,"AAA",
                //IF(S4<3600,"3*",
                //IF(S4<3900,"5*",

               //IF(S4<1,"0",
               //IF(S4<2100,"B",
               //IF(S4<2400,"A",
               //IF(S4<2700,"A+",
               //IF(S4<3000,"A++",
               //IF(S4<3300,"AAA",
               //IF(S4<3600,"3*",
               //IF(S4<3900,"5*",
               //))))))))

                //=IF(F3<3000,"0",
                //IF(F3<3300,"P1*",
                //IF(F3<3600,"P3*",
                //IF(F3<4500,"P5*"))))
                if (Values <= 0)
                    CalculateGrade = "0";
                else if (Values > 0 && Values < 3000)
                    CalculateGrade = "P-100";
                else if (Values > 3000 && Values < 3300)
                    CalculateGrade = "P1*";
                else if (Values > 3300 && Values < 3600)
                    CalculateGrade = "P3*";
                else if (Values > 3600 && Values < 4500)
                    CalculateGrade = "P5*";
                else
                {
                    CalculateGrade = "0";
                }
            }
            else if (cmbDesignationOperatorPacker.Text == "Porter")
            {

            }
            else
            {

            }

            return CalculateGrade;
        }

        private void txtShotsEmployee_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbGradeOperatorPacker_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_Grade_Amount();
        }

        private bool Validation_Grade()
        {
            objEP.Clear();

            if (cmbEmployeeOperatorPacker.SelectedIndex == -1)
            {
                cmbEmployeeOperatorPacker.Focus();
                objEP.SetError(cmbEmployeeOperatorPacker, "Select  Employee Name");
                return true;
            }
            else if (cmbOriginalDesignationOperatorPacker.SelectedIndex == -1)
            {
                cmbOriginalDesignationOperatorPacker.Focus();
                objEP.SetError(cmbOriginalDesignationOperatorPacker, "Select Designation Original");
                return true;
            }
            else if (cmbDesignationOperatorPacker.SelectedIndex == -1)
            {
                cmbDesignationOperatorPacker.Focus();
                objEP.SetError(cmbDesignationOperatorPacker, "Select Designation Employee");
                return true;
            }
            else if (cmbMachineNoOperatorPacker.SelectedIndex == -1)
            {
                cmbMachineNo.Focus();
                objEP.SetError(cmbMachineNo, "Select MachineNo");
                return true;
            }
            else if (cmbGradeOperatorPacker.SelectedIndex == -1)
            {
                cmbGradeOperatorPacker.Focus();
                objEP.SetError(cmbGradeOperatorPacker, "Select Grade");
                return true;
            }
            else
                return false;

        }

        private void Fill_Grade_Amount()
        {
            if (!Validation_Grade())
            {
                txtAmountOperatorPacker.Text = Convert.ToString(objRL.Get_Grade_Amount(cmbGradeOperatorPacker.Text));
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void cmbEmployeeName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_Employee_Original_Designation();
        }

        string DesignationEmployee = string.Empty;
        private void Fill_Employee_Original_Designation()
        {
            if (cmbEmployeeOperatorPacker.SelectedIndex > -1)
            {
                cmbOriginalDesignationOperatorPacker.SelectedIndex = -1;
                cmbDesignationOperatorPacker.SelectedIndex = -1;

                DesignationEmployee = string.Empty;
                objRL.Fill_Designation_Distinct_Operator_Packer(cmbDesignationOperatorPacker);
                objRL.Fill_Designation_Distinct_Operator_Packer(cmbOriginalDesignationOperatorPacker);
                cmbDesignationOperatorPacker.Text = objRL.Fill_Designation_By_EmployeeId(Convert.ToInt32(cmbEmployeeOperatorPacker.SelectedValue));
                cmbOriginalDesignationOperatorPacker.Text = objRL.Fill_Designation_By_EmployeeId(Convert.ToInt32(cmbEmployeeOperatorPacker.SelectedValue));
                Fill_Employees();
            }
        }

        private void tpEmployees_Click(object sender, EventArgs e)
        {

        }

        private void cmbDesignationOriginal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Check_Designation_Flag();
            Get_Porter_Packets_Count();
            //Get_Porter_Packets_Count();
        }

        private void Get_Porter_Packets_Count()
        {
             
                txtTotalPacketsEmployee.Text = "";
                double TotalPackets = 0;
                DataSet ds = new DataSet();
                objBL.Query = "select sum(Val(TotalPacket)) from OEEMachine where CancelTag=0 and OEEId=" + OEEId + " and ShiftId=" + ShiftId + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TotalPackets = objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0][0])));
                    txtTotalPacketsEmployee.Text = TotalPackets.ToString();
                }
            
        }

        private void txtShiftLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMachineAvailableTime.Focus();
        }

        private void txtMachineAvailableTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtShortMealBreakB.Focus();
        }

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbStatusOperatorPacker_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void btnDeleteOperatorPacker_Click(object sender, EventArgs e)
        {
            FlagDelete = true;
            SaveDB_OEEOperatorPacker();
        }

        private void tcOEE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcOEE.SelectedTab == tpOperatorPackerEmployees)
            {
                //Fill Operator Packer Details
                ClearAll_OperatorPacker();
                FillGrid_OperatorPacker();

            }
            else if (tcOEE.SelectedTab == tpMold)
            {
                //Fill Mould Details
                ClearAll_Mold();
                objRL.Fill_Employee_By_Designation(cmbEmployeeMold, "Mould Changer");
                objRL.Fill_Grade_By_GradeDesignation(cmbGradeMold, "Mold");
                FillGrid_Mold();
                //Get_Porter_Packets_Count();
            }
            else if (tcOEE.SelectedTab == tpPorter)
            {
                // TabPage2 is selected
                //MessageBox.Show("tpPorter is now open!");
                ClearAll_Porter();
                objRL.Fill_Employee_By_Designation(cmbEmployeePorter, "Porter");
                Get_Porter_Packets_Count();
                FillGrid_Porter();
            }
            else
            {
                ClearAll_OperatorPacker();
                ClearAll_Mold();
                ClearAll_Porter();
            }
        }

        int OEEPorterId = 0;

        private void ClearAll_Porter()
        {
            FlagDelete = false;
            btnDeletePorter.Visible = false;
            OEEPorterId = 0;
            objEP.Clear();
            cmbEmployeePorter.SelectedIndex = -1;
            cmbStatusPorter.SelectedIndex = -1;
            cmbOTPorter.SelectedIndex = -1;
            txtTotalPacketsEmployee.Text = "";
            txtTotalPacketsEmployee.Text = "";
        }

        private bool Validation_Porter()
        {
            objEP.Clear();

            if (cmbEmployeePorter.SelectedIndex == -1)
            {
                objEP.SetError(cmbEmployeePorter, "Select Employee Porter");
                cmbEmployeePorter.Focus();
                return true;
            }
            else if (cmbStatusPorter.SelectedIndex == -1)
            {
                objEP.SetError(cmbStatusPorter, "Select Status Porter");
                cmbStatusPorter.Focus();
                return true;
            }
            else if (cmbOTPorter.SelectedIndex == -1)
            {
                objEP.SetError(cmbOTPorter, "Select OT Porter");
                cmbOTPorter.Focus();
                return true;
            }
            else
                return false;
        }

        private void SaveDB_Porter()
        {
            Result = 0;
            if (!Validation_Porter())
            {
                //objBL.Query = "insert into OEEPorter(OEEId,ShiftId,ShiftDate,Shift,EmployeeId,Status,OTApplicable,ActualPackets,BasicPackets,ExtraPackets,OTPackets,DifferancePackets,Amount,Remarks,UserId) values(" + OEEId + "," + ShiftId + ",'" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "','" + txtShift.Text + "'," + cmbEmployeePorter.SelectedValue + ",'" + cmbStatusPorter.Text + "','" + cmbOTPorter.Text + "'," + BusinessLayer.UserId_Static + ")";
                if (OEEPorterId == 0)
                    objBL.Query = "insert into OEEPorter(OEEId,ShiftId,ShiftDate,Shift,EmployeeId,Status,OTApplicable,UserId) values(" + OEEId + "," + ShiftId + ",'" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "','" + txtShift.Text + "'," + cmbEmployeePorter.SelectedValue + ",'" + cmbStatusPorter.Text + "','" + cmbOTPorter.Text + "'," + BusinessLayer.UserId_Static + ")";
                else
                {
                    if (!FlagDelete)
                        objBL.Query = "update OEEPorter set OEEId=" + OEEId + ",ShiftId=" + ShiftId + ",ShiftDate='" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "',Shift='" + txtShift.Text + "',EmployeeId=" + cmbEmployeePorter.SelectedValue + ",Status='" + cmbStatusPorter.Text + "',OTApplicable='" + cmbOTPorter.Text + "',ModifiedId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and OEEPorterId=" + OEEPorterId + "";
                    else
                        objBL.Query = "update OEEPorter set CancelTag=1,ModifiedId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and OEEPorterId=" + OEEPorterId + "";
                }

                Result= objBL.Function_ExecuteNonQuery();

                if (Result > 0)
                {
                    if (!FlagDelete)
                        objRL.ShowMessage(7, 1);
                    else
                        objRL.ShowMessage(9, 1);

                    FillGrid_Porter();
                    ClearAll_Porter();
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        int TotalCount_Porter = 0;
        private void FillGrid_Porter()
        {
            TotalCount_Porter = 0; txtTotalAmountOperatorPacker.Text = "";
            dgvPorter.Rows.Clear();
            DataGridIndex = 0;
            DataSet ds = new DataSet();
            objBL.Query = "select OEEE.OEEPorterId,OEEE.OEEId,OEEE.ShiftId,OEEE.ShiftDate,OEEE.Shift,OEEE.EmployeeId,E.FullName,OEEE.Status,OEEE.OTApplicable,OEEE.ActualPackets,OEEE.BasicPackets,OEEE.ExtraPackets,OEEE.OTPackets,OEEE.DifferancePackets,OEEE.Amount,OEEE.Remarks from OEEPorter OEEE inner join Employee E on E.ID=OEEE.EmployeeId where OEEE.CancelTag=0 and E.CancelTag=0 and OEEE.OEEId=" + OEEId + " and OEEE.ShiftId=" + ShiftId + " order by OEEE.OEEPorterId desc";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataGridIndex = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgvPorter.Rows.Add();
                    dgvPorter.Rows[DataGridIndex].Cells["clmOEEPorterId"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEEPorterId"]));
                    dgvPorter.Rows[DataGridIndex].Cells["clmOEEIdPorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEEId"]));
                    dgvPorter.Rows[DataGridIndex].Cells["clmShiftIdPorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftId"]));
                    DateTime dtS;
                    dtS = Convert.ToDateTime(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftDate"])));
                    dgvPorter.Rows[DataGridIndex].Cells["clmShiftDatePorter"].Value = dtS.ToString(BusinessResources.DATEFORMATDDMMYYYY);
                    dgvPorter.Rows[DataGridIndex].Cells["clmShiftPorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift"]));
                    dgvPorter.Rows[DataGridIndex].Cells["clmEmployeeIdPorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["EmployeeId"]));
                    dgvPorter.Rows[DataGridIndex].Cells["clmEmployeeNamePorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["FullName"]));
                    dgvPorter.Rows[DataGridIndex].Cells["clmStatusPorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Status"]));
                    dgvPorter.Rows[DataGridIndex].Cells["clmOTApplicablePorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OTApplicable"]));
                    dgvPorter.Rows[DataGridIndex].Cells["clmActualPacketsPorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ActualPackets"]));
                    dgvPorter.Rows[DataGridIndex].Cells["clmBasicPacketsPorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["BasicPackets"]));
                    dgvPorter.Rows[DataGridIndex].Cells["clmExtraPacketsPorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ExtraPackets"]));
                    dgvPorter.Rows[DataGridIndex].Cells["clmOTPacketsPorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OTPackets"]));
                    dgvPorter.Rows[DataGridIndex].Cells["clmDifferancePorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["DifferancePackets"]));
                    dgvPorter.Rows[DataGridIndex].Cells["clmAmountPorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Amount"]));
                    dgvPorter.Rows[DataGridIndex].Cells["clmRemarksPorter"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Remarks"]));
                   
                    TotalAmount_Final += objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Amount"])));
                    DataGridIndex++;
                }
                Packets_Calculations_Porter_Grid();

                //txtTotalAmountOperatorPacker.Text = TotalAmount_Final.ToString();
            }
            else
            {

            }
        }

        private void btnAddPorter_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB_Porter();
        }

        private void btnClearPorter_Click(object sender, EventArgs e)
        {
            ClearAll_Porter();
        }

        private void btnDeletePorter_Click(object sender, EventArgs e)
        {
            FlagDelete = true;
            SaveDB_Porter();
            //,OTApplicable,ActualPackets,BasicPackets,ExtraPackets,OTPackets,DifferancePackets,Amount,Remarks
        }

        private void cmbMachineNoMold_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cmbGradeMold_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(cmbGradeMold.SelectedIndex >-1)
            {
                txtAmountMold.Text = Convert.ToString(objRL.Get_Grade_Amount(cmbGradeMold.Text));
            }
        }

        private void ClearAll_Mold()
        {
            OEEMoldId = 0;
            FlagDelete = false;
            btnDeleteMold.Visible = false;
            objEP.Clear();
            cmbEmployeeMold.SelectedIndex = -1;
            cmbStatusMold.SelectedIndex = -1;
            cmbWorkMold.SelectedIndex = -1;
            cmbMachineNoMold.SelectedIndex = -1;
            cmbMachineTypeMold.SelectedIndex = -1;
            cmbGradeMold.SelectedIndex = -1;
            txtAmountMold.Text = "";
            cmbEmployeeMold.Focus();
        }

        int OEEMoldId = 0;
        private void SaveDB_Mold()
        {
            if (!Validation_Mold())
            {
                if (OEEMoldId == 0)
                    objBL.Query = "insert into OEEMold(OEEId,ShiftId,ShiftDate,Shift,EmployeeId,Status,[Work],MachineId,MachineNo,MachineType,Grade,Amount,Remarks,UserId) Values(" + OEEId + "," + ShiftId + ",'" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "','" + txtShift.Text + "'," + cmbEmployeeMold.SelectedValue + ",'" + cmbStatusMold.Text + "','" + cmbWorkMold.Text + "'," + cmbMachineNoMold.SelectedValue + ",'" + cmbMachineNoMold.Text + "','" + cmbMachineTypeMold.Text + "','" + cmbGradeMold.Text + "','" + txtAmountMold.Text + "','" + txtRemarksMold.Text + "'," + BusinessLayer.UserId_Static + ")";
                else
                {
                    if(!FlagDelete)
                        objBL.Query = "update OEEMold set OEEId=" + OEEId + ",ShiftId=" + ShiftId + ",ShiftDate='" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "',Shift='" + txtShift.Text + "',EmployeeId=" + cmbEmployeeMold.SelectedValue + ",Status='" + cmbStatusPorter.Text + "',Work='" + cmbWorkMold.Text + "',MachineId=" + cmbMachineNoMold.SelectedValue + ",MachineNo='" + cmbMachineNoMold.Text + "',MachineType='" + cmbMachineTypeMold.Text + "',Grade='" + cmbGradeMold.Text + "',Amount='" + txtAmountMold.Text + "',Remarks='" + txtRemarksMold.Text + "',ModifiedId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and OEEMoldId=" + OEEMoldId + " ";
                    else
                        objBL.Query = "update OEEMold set CancelTag=1 where CancelTag=0 and OEEMoldId=" + OEEMoldId + " ";
                }

                Result = objBL.Function_ExecuteNonQuery();

                if (Result > 0)
                {
                    FillGrid_Mold();
                    ClearAll_Mold();

                    if (!FlagDelete)
                        objRL.ShowMessage(7, 1);
                    else
                        objRL.ShowMessage(9, 1);
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        int TotalAmount_Mold = 0; 

        private void FillGrid_Mold()
        {
            TotalAmount_Mold = 0;  
            dgvMold.Rows.Clear();
            DataGridIndex = 0;
            DataSet ds = new DataSet();
            objBL.Query = "select OEEE.OEEMoldId,OEEE.OEEId,OEEE.ShiftId,OEEE.ShiftDate,OEEE.Shift,OEEE.EmployeeId,E.FullName,OEEE.Status,OEEE.Work,OEEE.MachineId,OEEE.MachineNo,OEEE.MachineType,OEEE.Grade,OEEE.Amount,OEEE.Remarks from OEEMold OEEE inner join Employee E on E.ID=OEEE.EmployeeId where OEEE.CancelTag=0 and E.CancelTag=0 and OEEE.OEEId=" + OEEId + " and OEEE.ShiftId=" + ShiftId + " order by OEEE.OEEMoldId desc";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataGridIndex = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgvMold.Rows.Add();
                    dgvMold.Rows[DataGridIndex].Cells["clmOEEMoldId"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEEMoldId"]));
                    dgvMold.Rows[DataGridIndex].Cells["clmOEEIdMold"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEEId"]));
                    dgvMold.Rows[DataGridIndex].Cells["clmShiftIdMold"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftId"]));
                    DateTime dtS;
                    dtS = Convert.ToDateTime(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftDate"])));
                    dgvMold.Rows[DataGridIndex].Cells["clmShiftDateMold"].Value = dtS.ToString(BusinessResources.DATEFORMATDDMMYYYY);
                    dgvMold.Rows[DataGridIndex].Cells["clmShiftMold"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift"]));
                    dgvMold.Rows[DataGridIndex].Cells["clmEmployeeIdMold"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["EmployeeId"]));
                    dgvMold.Rows[DataGridIndex].Cells["clmEmployeeNameMold"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["FullName"]));
                    dgvMold.Rows[DataGridIndex].Cells["clmStatusMold"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Status"]));  //
                    dgvMold.Rows[DataGridIndex].Cells["clmWorkMold"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Work"]));
                    dgvMold.Rows[DataGridIndex].Cells["clmMachineIdMold"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MachineId"]));
                    dgvMold.Rows[DataGridIndex].Cells["clmMachineNoMold"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MachineNo"]));
                    dgvMold.Rows[DataGridIndex].Cells["clmMachineTypeMold"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MachineType"]));
                    dgvMold.Rows[DataGridIndex].Cells["clmGradeMold"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Grade"]));
                    dgvMold.Rows[DataGridIndex].Cells["clmAmountMold"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Amount"]));
                    dgvMold.Rows[DataGridIndex].Cells["clmRemarksMold"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Remarks"]));
                    
                    //TotalAmount_Final += objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Amount"])));
                    DataGridIndex++;
                }
            }
            else
            {

            }
        }

        private bool Validation_Mold()
        {
            objEP.Clear();

            if (cmbEmployeeMold.SelectedIndex == -1)
            {
                objEP.SetError(cmbEmployeeMold, "Select Employee Name");
                cmbEmployeeMold.Focus();
                return true;
            }
            else if (cmbStatusMold.SelectedIndex == -1)
            {
                objEP.SetError(cmbStatusMold, "Select Status ");
                cmbStatusMold.Focus();
                return true;
            }
            else if (cmbWorkMold.SelectedIndex == -1)
            {
                objEP.SetError(cmbWorkMold, "Select Work");
                cmbWorkMold.Focus();
                return true;
            }
            else if (cmbMachineNoMold.SelectedIndex == -1)
            {
                objEP.SetError(cmbMachineNoMold, "Select Machine No");
                cmbMachineNoMold.Focus();
                return true;
            }
            else if (cmbMachineTypeMold.SelectedIndex == -1)
            {
                objEP.SetError(cmbMachineTypeMold, "Select Machine Type Mold ");
                cmbMachineTypeMold.Focus();
                return true;
            }
            else if (cmbGradeMold.SelectedIndex == -1)
            {
                objEP.SetError(cmbGradeMold, "Select Grade  ");
                cmbGradeMold.Focus();
                return true;
            }
            else if (txtAmountMold.Text == "")
            {
                objEP.SetError(txtAmountMold, "Enter Amount");
                txtAmountMold.Focus();
                return true;
            }
            else
                return false;
        }

        private void btnAddMold_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB_Mold();
        }

        private void btnClearMold_Click(object sender, EventArgs e)
        {
            ClearAll_Mold();
        }

        private void btnDeleteMold_Click(object sender, EventArgs e)
        {
            FlagDelete = true;
            SaveDB_Mold();
        }

        double TotalPackets = 0, ActualPerPackets = 0, PerHeadePackets = 0, HalfDaysCount = 0, FullDaysCount = 0,TotalEmployees=0;
        double HalfDayPackets = 0, FullDayPackets = 0,HalfDayPerPackets = 0;

        private void btnPorterPackets_Click(object sender, EventArgs e)
        {
            Packets_Calculations_Porter_Grid();
        }

        private void Packets_Calculations_Porter_Grid()
        {
            TotalPackets = 0; ActualPerPackets = 0; PerHeadePackets = 0; HalfDaysCount = 0; FullDaysCount = 0;
            HalfDayPackets = 0; FullDayPackets = 0; HalfDayPerPackets = 0;

            TotalPackets = objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(txtTotalPacketsEmployee.Text)));

            if (TotalPackets > 0)
            {
                if (dgvPorter.Rows.Count > 0)
                {
                    TotalEmployees = dgvPorter.Rows.Count;
                    PerHeadePackets = TotalPackets / TotalEmployees;

                    for (int i = 0; i < dgvPorter.Rows.Count; i++)
                    {
                        string HD = objRL.Check_Null_String(Convert.ToString(dgvPorter.Rows[i].Cells["clmStatusPorter"].Value));

                        if (HD == "Half Day")
                        {
                            HalfDaysCount = HalfDaysCount + 2;
                        }

                        if (HD == "Full Day")
                        {
                            FullDaysCount++;
                        }
                    }

                    if (HalfDaysCount > 0)
                    {
                        HalfDayPackets = PerHeadePackets / HalfDaysCount;
                        HalfDayPackets = Math.Round(HalfDayPackets);
                        HalfDayPerPackets = HalfDayPackets / FullDaysCount;
                    }

                    ActualPerPackets = PerHeadePackets + HalfDayPerPackets;

                    ActualPerPackets = Math.Round(ActualPerPackets);


                    for (int i = 0; i < dgvPorter.Rows.Count; i++)
                    {
                        string HD = objRL.Check_Null_String(Convert.ToString(dgvPorter.Rows[i].Cells["clmStatusPorter"].Value));

                        if (HD == "Half Day")
                        {
                            //HalfDaysCount = HalfDaysCount + 2;
                            dgvPorter.Rows[i].Cells["clmPerHeadPacketsPorter"].Value = HalfDayPackets.ToString();
                        }

                        if (HD == "Full Day")
                        {
                            dgvPorter.Rows[i].Cells["clmPerHeadPacketsPorter"].Value = ActualPerPackets.ToString();
                        }

                        dgvPorter.Rows[i].Cells["clmBasicPacketsPorter"].Value = "450";

                        if (ActualPerPackets > 450)
                        {
                            int ExtraPackets = Convert.ToInt32(ActualPerPackets - 450);
                            dgvPorter.Rows[i].Cells["clmExtraPacketsPorter"].Value = ExtraPackets.ToString();

                            double AmountExtaPackets = 0;
                            AmountExtaPackets = ExtraPackets * 1;

                            dgvPorter.Rows[i].Cells["clmAmountPorter"].Value = AmountExtaPackets.ToString();
                        }
                    }
                }
            }
        }

        private void dgvMold_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RowCount_Grid = 0;
                CurrentRowIndex = 0;
                RowCount_Grid = dgvMold.Rows.Count;
                CurrentRowIndex = dgvMold.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    ClearAll_Mold();
                    GridFlag = true;
                    btnDeleteMold.Visible = true;
                    OEEMoldId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvMold.Rows[e.RowIndex].Cells["clmOEEMoldId"].Value)));
                    cmbEmployeeMold.Text = objRL.Check_Null_String(Convert.ToString(dgvMold.Rows[e.RowIndex].Cells["clmEmployeeNameMold"].Value));
                    //Fill_Employees();
                    cmbStatusMold.Text = objRL.Check_Null_String(Convert.ToString(dgvMold.Rows[e.RowIndex].Cells["clmStatusMold"].Value));
                    cmbMachineNoMold.Text = objRL.Check_Null_String(Convert.ToString(dgvMold.Rows[e.RowIndex].Cells["clmMachineNoMold"].Value));
                    cmbStatusMold.Text = objRL.Check_Null_String(Convert.ToString(dgvMold.Rows[e.RowIndex].Cells["clmStatusMold"].Value));
                    cmbWorkMold.Text = objRL.Check_Null_String(Convert.ToString(dgvMold.Rows[e.RowIndex].Cells["clmWorkMold"].Value));
                    cmbMachineTypeMold.Text = objRL.Check_Null_String(Convert.ToString(dgvMold.Rows[e.RowIndex].Cells["clmMachineTypeMold"].Value));
                    cmbGradeMold.Text = objRL.Check_Null_String(Convert.ToString(dgvMold.Rows[e.RowIndex].Cells["clmGradeMold"].Value));
                    txtAmountMold.Text = objRL.Check_Null_String(Convert.ToString(dgvMold.Rows[e.RowIndex].Cells["clmAmountMold"].Value));
                    txtRemarksMold.Text = objRL.Check_Null_String(Convert.ToString(dgvMold.Rows[e.RowIndex].Cells["clmRemarksMold"].Value));
                    //txtRemarksEmployee.Text = objRL.Check_Null_String(Convert.ToString(dgvMold.Rows[e.RowIndex].Cells["clmRemarks"].Value));
                }
            }
            catch (Exception ex1)
            {
                objRL.ErrorMessge(ex1.ToString());
                return;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void dgvPorter_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RowCount_Grid = 0;
                CurrentRowIndex = 0;
                RowCount_Grid = dgvPorter.Rows.Count;
                CurrentRowIndex = dgvPorter.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    ClearAll_Mold();
                    GridFlag = true;
                    btnDeletePorter.Visible = true;
                    OEEPorterId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvPorter.Rows[e.RowIndex].Cells["clmOEEPorterId"].Value)));
                    cmbEmployeePorter.Text = objRL.Check_Null_String(Convert.ToString(dgvPorter.Rows[e.RowIndex].Cells["clmEmployeeNamePorter"].Value));
                    cmbStatusPorter.Text = objRL.Check_Null_String(Convert.ToString(dgvMold.Rows[e.RowIndex].Cells["clmStatusPorter"].Value));
                    cmbOTPorter.Text = objRL.Check_Null_String(Convert.ToString(dgvPorter.Rows[e.RowIndex].Cells["clmOTApplicablePorter"].Value));
                    cmbRemarksPorter.Text = objRL.Check_Null_String(Convert.ToString(dgvPorter.Rows[e.RowIndex].Cells["clmRemarksPorter"].Value));
                }
            }
            catch (Exception ex1)
            {
                objRL.ErrorMessge(ex1.ToString());
                return;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
