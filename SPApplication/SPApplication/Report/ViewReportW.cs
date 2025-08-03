using BusinessLayerUtility;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPApplication.Report
{
    public partial class ViewReportW : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;
        string OEEType = string.Empty;

        public ViewReportW()
        {
            InitializeComponent();
        }

        public ViewReportW(string ReportName)
        {
            this.ReportName = string.Empty;
            InitializeComponent();
            this.ReportName = ReportName;
            //Get_Report_Name();

            Get_New_Report();
        }

        //string VolumeChecker = string.Empty;
        //string PlantIncharge = string.Empty;
        //string Supervisor = string.Empty;
        //string Packer = string.Empty;
        //string Operator = string.Empty;

        bool ParameterFla = false;
        DateTime EntryDate, EntryTime, ShiftDate, ShiftBeginTime, ShiftEndTime;

        int OEEId = 0, OEEMachineId = 0, MachineId = 0, ShiftScheduleId = 0, ProductId = 0, MouldId = 0;


        string OEEReportId = string.Empty, Shift = string.Empty,

                            ShiftHours = string.Empty,
                            PlantIncharge = string.Empty,
                            VolumeChecker = string.Empty,
                            Supervisor = string.Empty,
                            Packer = string.Empty,
                            Operator = string.Empty,
                            PreformLoader = string.Empty,
                            MouldChanger = string.Empty,
                            Porter = string.Empty,

                            MachineNo = string.Empty,
                            ProductName = string.Empty,

                            MouldNo = string.Empty,
                            ShiftLengthA = string.Empty,
                            ShortMealBreakB = string.Empty,
                            NoPlanning = string.Empty,
                            NoElectricity = string.Empty,
                            TotalProductionTime = string.Empty,
                            Breakdown = string.Empty,
                            Changeover = string.Empty,
                            ManpowerShortage = string.Empty,
                            StartupLoss = string.Empty,
                            MaintainanceMachineShutDownTime = string.Empty,
                            MaterialNotAvailable = string.Empty,
                            TotalDowntime = string.Empty,
                            OperatingTime = string.Empty,
                            Availabilty = string.Empty,
                            IdealRunRate = string.Empty,
                            Cavity = string.Empty,
                            PlanningQty = string.Empty,
                            TotalShot = string.Empty,
                            TotalProductionInNos = string.Empty,
                            Packing = string.Empty,
                            TotalPacket = string.Empty,
                            TargetProduction = string.Empty,
                            Performance = string.Empty,
                            RejectInNos = string.Empty,
                            GoodInNos = string.Empty,
                            Quality = string.Empty,
                            OEE = string.Empty,
                            SwitchFlag = string.Empty,
                            SwitchPackerMachineId = string.Empty,
                            SwitchOperatiorMachineId = string.Empty,
                            Status = string.Empty,
                            Reason = string.Empty,
                            SwitchNote = string.Empty,
                            PreformRejection = string.Empty;

        private void ClearReportValues()
        {
            ShiftHours = string.Empty;
            PlantIncharge = string.Empty;
            VolumeChecker = string.Empty;
            Supervisor = string.Empty;
            Packer = string.Empty;
            Operator = string.Empty;
            PreformLoader = string.Empty;
            MouldChanger = string.Empty;
            Porter = string.Empty;
            OEEId = 0;
            OEEMachineId = 0;
            MachineId = 0;
            MachineNo = string.Empty;
            ShiftScheduleId = 0;
            ProductId = 0;
            ProductName = string.Empty;
            MouldId = 0;
            MouldNo = string.Empty;
            ShiftLengthA = string.Empty;
            ShortMealBreakB = string.Empty;
            NoPlanning = string.Empty;
            NoElectricity = string.Empty;
            TotalProductionTime = string.Empty;
            Breakdown = string.Empty;
            Changeover = string.Empty;
            ManpowerShortage = string.Empty;
            StartupLoss = string.Empty;
            MaintainanceMachineShutDownTime = string.Empty;
            MaterialNotAvailable = string.Empty;
            TotalDowntime = string.Empty;
            OperatingTime = string.Empty;
            Availabilty = string.Empty;
            IdealRunRate = string.Empty;
            Cavity = string.Empty;
            PlanningQty = string.Empty;
            TotalShot = string.Empty;
            TotalProductionInNos = string.Empty;
            Packing = string.Empty;
            TotalPacket = string.Empty;
            TargetProduction = string.Empty;
            Performance = string.Empty;
            RejectInNos = string.Empty;
            GoodInNos = string.Empty;
            Quality = string.Empty;
            OEE = string.Empty;
            SwitchFlag = string.Empty;
            SwitchPackerMachineId = string.Empty;
            SwitchOperatiorMachineId = string.Empty;
            Status = string.Empty;
            Reason = string.Empty;
            SwitchNote = string.Empty;
            PreformRejection = string.Empty;
        }

        private void Get_New_Report()
        {
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;

            objBL.Query = "delete from OEEReport where CancelTag=0";
            objBL.Function_ExecuteNonQuery();

            VolumeChecker = string.Empty;
            PlantIncharge = string.Empty;
            Supervisor = string.Empty;
            Packer = string.Empty;
            Operator = string.Empty;

            DataSet ds = new DataSet();

            //objBL.Query = "select OM.OEEMachineId,OM.Shift,OM.EntryDate,OM.EntryTime,OM.OEEId,OM.ShiftScheduleId,SS.ShiftDate,SS.ShiftHours,SS.BeginTime1,SS.EndTime1,SS.BeginTime2,SS.EndTime2,SS.BeginTime3,SS.EndTime3,OM.Shift,OM.MachineId,MM.MachineNo,OM.ProductId,P.ProductName,P.MouldNo,P.MouldId,OM.ShiftLengthA,OM.ShortMealBreakB,OM.NoPlanning,OM.NoElectricity,OM.TotalProductionTime,OM.Breakdown,OM.Changeover,OM.ManpowerShortage,OM.StartupLoss,OM.MaintainanceMachineShutDownTime,OM.MaterialNotAvailable,OM.TotalDowntime,OM.OperatingTime,OM.Availabilty,IdealRunRate,OM.Cavity,OM.PlanningQty,OM.TotalShot,OM.TotalProductionInNos,OM.Packing,OM.TotalPacket,OM.TargetProduction,OM.Performance,OM.RejectInNos,OM.GoodInNos,OM.Quality,OM.OEE,OM.SwitchFlag,OM.SwitchPackerMachineId,OM.SwitchOperatiorMachineId,OM.Status,OM.Reason,OM.SwitchNote,OM.PreformRejection from (((OEEMachine OM inner join MachineMaster MM on MM.MachineId=OM.MachineId) inner join Product P on P.ID=OM.ProductId) inner join ShiftSchedule SS on SS.ShiftScheduleId=OM.ShiftScheduleId) where SS.CancelTag=0 and P.CancelTag=0 and MM.CancelTag=0 and OM.CancelTag=0 and SS.ShiftDate=#" + objRL.EntryDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# "; // and OM.MachineId=" + objRL.MachineId + "";// and OM.Shift='" + objRL.Shift + "'";
            //objBL.Query = "select OM.OEEMachineId,OM.EntryDate,Format([OM.EntryTime], 'HH:mm') AS [EntryTime],OM.OEEId,OEE.ShiftDate,OEE.Shift,OEE.ShiftHours,Format([OEE.ShiftBeginTime], 'HH:mm') AS [ShiftBeginTime],Format([OEE.ShiftEndTime], 'HH:mm') AS [ShiftEndTime],OM.MachineId,MM.MachineNo,OM.ProductId,P.ProductName,OM.ShiftLengthA,OM.ShortMealBreakB,OM.NoPlanning,OM.NoElectricity,OM.TotalProductionTime,OM.Breakdown,OM.Changeover,OM.ManpowerShortage,OM.StartupLoss,OM.MaintainanceMachineShutDownTime,OM.MaterialNotAvailable,OM.TotalDowntime,OM.OperatingTime,OM.Availabilty,OM.IdealRunRate,OM.Cavity,OM.PlanningQty,OM.TotalShot,OM.TotalProductionInNos,OM.Packing,OM.TotalPacket,OM.TargetProduction,OM.Performance,OM.RejectInNos,OM.GoodInNos,OM.Quality,OM.OEE,OM.Status,OM.SwitchFlag,OM.Reason,OM.ReasonForChange,OM.SwitchFlag from ((OEEMachine OM inner join OEE OEE on OEE.OEEId=OM.OEEId) inner join MachineMaster MM on MM.MachineId=OM.MachineId) inner join Product P on P.ID=OM.ProductId where P.CancelTag=0 and OM.CancelTag=0 and OEE.CancelTag=0 and MM.CancelTag=0 and OM.OEEId=" + objRL.OEEId + " ";
            //objBL.Query = "select OM.OEEMachineId,OM.EntryDate,Format([OM.EntryTime], 'HH:mm') AS [EntryTime],OM.OEEId,OEE.ShiftDate,OEE.Shift,OEE.ShiftHours,Format([OEE.ShiftBeginTime], 'HH:mm') AS [ShiftBeginTime],Format([OEE.ShiftEndTime], 'HH:mm') AS [ShiftEndTime],OM.MachineId,MM.MachineNo,OM.ProductId,P.ProductName,OM.ShiftLengthA,OM.ShortMealBreakB,OM.NoPlanning,OM.NoElectricity,OM.TotalProductionTime,OM.Breakdown,OM.Changeover,OM.ManpowerShortage,OM.StartupLoss,OM.MaintainanceMachineShutDownTime,OM.MaterialNotAvailable,OM.TotalDowntime,OM.OperatingTime,OM.Availabilty,OM.IdealRunRate,OM.Cavity,OM.PlanningQty,OM.TotalShot,OM.TotalProductionInNos,OM.Packing,OM.TotalPacket,OM.TargetProduction,OM.Performance,OM.PreformRejection,OM.RejectInNos,OM.GoodInNos,OM.Quality,OM.OEE,OM.Status,OM.SwitchFlag,OM.Reason,OM.ReasonForChange,OM.SwitchFlag from ((OEEMachine OM inner join OEE OEE on OEE.OEEId=OM.OEEId) inner join MachineMaster MM on MM.MachineId=OM.MachineId) inner join Product P on P.ID=OM.ProductId where P.CancelTag=0 and OM.CancelTag=0 and OEE.CancelTag=0 and MM.CancelTag=0 and OM.OEEId=" + objRL.OEEId + " ";

            MainQuery = "select OM.OEEMachineId,OM.EntryDate as [Date],Format([OM.EntryTime], 'HH:mm') AS [Time],OM.OEEId,OM.ShiftId,SEN.Shift,SEN.EntryDate as [Shift Date],SEN.ShifFromDate as [Shift Start],SEN.ShiftToDate as [Shift End],SEN.ShiftHours as [Shift Hours],OM.MachineId,MM.MachineNo,OM.ProductId,P.ProductName as [Product Name],OM.ShiftLengthA,OM.MachineAvailableTime,OM.BalanceLength,OM.ShortMealBreakB,OM.NoPlanning,OM.NoElectricity,OM.TotalProductionTime,OM.Breakdown,OM.Changeover,OM.ManpowerShortage,OM.StartupLoss,OM.MaintainanceMachineShutDownTime,OM.MaterialNotAvailable,OM.TotalDowntime,OM.OperatingTime,OM.Availabilty,OM.IdealRunRate,OM.Cavity,OM.PlanningQty,OM.TotalShot,OM.TotalProductionInNos,OM.Packing,OM.TotalPacket,OM.TargetProduction,OM.Performance,OM.PreformRejection,OM.RejectInNos,OM.GoodInNos,OM.Quality,OM.OEE,OM.Status,OM.SwitchFlag,OM.Reason,OM.SwitchFlag,OM.Remarks from (((OEEMachine OM inner join OEE OEE on OEE.OEEId=OM.OEEId) inner join MachineMaster MM on MM.MachineId=OM.MachineId) inner join Product P on P.ID=OM.ProductId) inner join ShiftEntryNew SEN on SEN.ID=OM.ShiftId where SEN.CancelTag=0 and P.CancelTag=0 and OM.CancelTag=0 and OEE.CancelTag=0 and MM.CancelTag=0 and SEN.EntryDate=#" + RedundancyLogics.ReportDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# ";

            //,(select E.FullName from OEEEmployee OEmp inner join Employee E on OEmp.EmployeeId=E.ID where OEmp.OEEId=" + objRL.OEEId + " and OEmp.OEEType='Operator' and E.CancelTag=0 and OEmp.CancelTag=0) as [Operator]

            OrderByClause = " order by OM.MachineId asc, Val(SEN.Shift) asc ";
            objBL.Query = MainQuery + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                SaveOEEReport(ds);
                ParameterFlag = false;
                ReportDS = "dsOEENew";
                RDLC_ReportName = "OEEReportNew.rdlc";
                Load_Report(Get_Report_Query());
            }
        }

        private void SaveOEEReport(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ClearReportValues();

                    //OM.OEEMachineId,
                    //OM.EntryDate,
                    //Format([OM.EntryTime], 'HH:mm') AS [EntryTime],
                    //OM.OEEId,
                    //OEE.ShiftDate,
                    //OEE.Shift,
                    //OEE.ShiftHours,
                    //Format([OEE.ShiftBeginTime], 'HH:mm') AS [ShiftBeginTime],
                    //Format([OEE.ShiftEndTime], 'HH:mm') AS [ShiftEndTime],
                    //OM.MachineId,
                    //MM.MachineNo,
                    //OM.ProductId,
                    //P.ProductName,
                    //OM.ShiftLengthA,
                    //OM.ShortMealBreakB,
                    //OM.NoPlanning,
                    //OM.NoElectricity,
                    //OM.TotalProductionTime,
                    //OM.Breakdown,
                    //OM.Changeover,
                    //OM.ManpowerShortage,
                    //OM.StartupLoss,
                    //OM.MaintainanceMachineShutDownTime,
                    //OM.MaterialNotAvailable,OM.TotalDowntime,OM.OperatingTime,OM.Availabilty,OM.IdealRunRate,OM.Cavity,OM.PlanningQty,OM.TotalShot,OM.TotalProductionInNos,OM.Packing,OM.TotalPacket,OM.TargetProduction,OM.Performance,OM.RejectInNos,OM.GoodInNos,OM.Quality,OM.OEE,OM.Status,OM.SwitchFlag,OM.Reason,OM.ReasonForChange,OM.SwitchFlag

                    ShiftId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftId"])));
                    OEEMachineId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEEMachineId"])));

                    OEEId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEEId"])));
                    //ShiftScheduleId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftScheduleId"])));
                    MachineId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MachineId"])));
                    MachineNo = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MachineNo"]));

                    //EntryDate = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["EntryDate"]));
                    //EntryTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["EntryTime"]));

                    ShiftDate = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["Shift Date"]));
                    Shift = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift"]));
                    ShiftHours = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift Hours"]));

                    ShiftBeginTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["Shift Start"]));
                    ShiftEndTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["Shift End"]));

                    //if (Shift == "I")
                    //{
                    //    ShiftBeginTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["BeginTime1"]));
                    //    ShiftEndTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["EndTime1"]));
                    //}
                    //else if (Shift == "II")
                    //{
                    //    ShiftBeginTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["BeginTime2"]));
                    //    ShiftEndTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["EndTime2"]));
                    //}
                    //else if (Shift == "III")
                    //{
                    //    ShiftBeginTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["BeginTime3"]));
                    //    ShiftEndTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["EndTime3"]));
                    //}
                    //else
                    //{

                    //}

                    OEEType = "PlantIncharge";
                    PlantIncharge = GetEmployee();

                    OEEType = "VolumeChecker";
                    VolumeChecker = GetEmployee();

                    OEEType = "Supervisor";
                    Supervisor = GetEmployee();

                    //Other by Machine ID
                    OEEType = "Packer";
                    Packer = GetEmployee();

                    OEEType = "Operator";
                    Operator = GetEmployee();

                    OEEType = "PreformLoader";
                    PreformLoader = GetEmployee();

                    OEEType = "Porter";
                    Porter = GetEmployee();

                    OEEType = "MouldChanger";
                    MouldChanger = GetEmployee();

                    ProductId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ProductId"])));

                    ProductName = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Product Name"]));

                    ProductName = ProductName.Replace("'", "''");

                    //MouldId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MouldId"])));
                    //MouldNo = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MouldNo"]));
                    ShiftLengthA = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftLengthA"]));
                    ShortMealBreakB = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShortMealBreakB"]));
                    NoPlanning = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["NoPlanning"]));
                    NoElectricity = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["NoElectricity"]));
                    TotalProductionTime = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["TotalProductionTime"]));
                    Breakdown = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Breakdown"]));
                    Changeover = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Changeover"]));
                    ManpowerShortage = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ManpowerShortage"]));
                    StartupLoss = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["StartupLoss"]));
                    MaintainanceMachineShutDownTime = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MaintainanceMachineShutDownTime"]));
                    MaterialNotAvailable = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MaterialNotAvailable"]));
                    TotalDowntime = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["TotalDowntime"]));
                    OperatingTime = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OperatingTime"]));
                    Availabilty = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Availabilty"]));
                    IdealRunRate = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["IdealRunRate"]));
                    Cavity = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Cavity"]));
                    PlanningQty = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["PlanningQty"]));
                    TotalShot = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["TotalShot"]));
                    TotalProductionInNos = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["TotalProductionInNos"]));
                    Packing = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Packing"]));
                    TotalPacket = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["TotalPacket"]));
                    TargetProduction = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["TargetProduction"]));
                    Performance = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Performance"]));
                    RejectInNos = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["RejectInNos"]));
                    GoodInNos = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["GoodInNos"]));
                    Quality = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Quality"]));
                    OEE = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEE"]));
                    SwitchFlag = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["SwitchFlag"]));
                    //SwitchPackerMachineId = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["SwitchPackerMachineId"]));
                    //SwitchOperatiorMachineId = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["SwitchOperatiorMachineId"]));
                    Status = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Status"]));
                    Reason = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Reason"]));
                    //SwitchNote = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["SwitchNote"]));
                    PreformRejection = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["PreformRejection"]));

                    int ShiftId_I = 0;
                    string BalanceLength = string.Empty, MachineAvailableTime = string.Empty;

                    ShiftId_I = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftId"])));
                    BalanceLength = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["BalanceLength"]));
                    MachineAvailableTime = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MachineAvailableTime"]));

                    // Shift = "III";
                    objBL.Query = "insert into OEEReport(" +
                                                        "EntryDate," +
                                                        "EntryTime," +
                                                        "ShiftDate," +
                                                        "Shift," +
                                                        "ShiftBeginTime," +
                                                        "ShiftEndTime," +
                                                        "ShiftHours," +
                                                        "PlantIncharge," +
                                                        "VolumeChecker," +
                                                        "Supervisor," +
                                                        "Packer," +
                                                        "Operator," +
                                                        "PreformLoader," +
                                                        "MouldChanger," +
                                                        "Porter," +
                                                        "OEEId," +
                                                        "OEEMachineId," +
                                                        "MachineId," +
                                                        "MachineNo," +
                                                        "ShiftScheduleId," +
                                                        "ProductId," +
                                                        "ProductName," +
                                                        "MouldId," +
                                                        "MouldNo," +
                                                        "ShiftLengthA," +
                                                        "ShortMealBreakB," +
                                                        "NoPlanning," +
                                                        "NoElectricity," +
                                                        "TotalProductionTime," +
                                                        "Breakdown," +
                                                        "Changeover," +
                                                        "ManpowerShortage," +
                                                        "StartupLoss," +
                                                        "MaintainanceMachineShutDownTime," +
                                                        "MaterialNotAvailable," +
                                                        "TotalDowntime," +
                                                        "OperatingTime," +
                                                        "Availabilty," +
                                                        "IdealRunRate," +
                                                        "Cavity," +
                                                        "PlanningQty," +
                                                        "TotalShot," +
                                                        "TotalProductionInNos," +
                                                        "Packing," +
                                                        "TotalPacket," +
                                                        "TargetProduction," +
                                                        "Performance," +
                                                        "RejectInNos," +
                                                        "GoodInNos," +
                                                        "Quality," +
                                                        "OEE," +
                                                        "SwitchFlag," +
                                                        "Status," +
                                                        "Reason," +
                                                        "PreformRejection,ShiftId,MachineAvailableTime,BalanceLength) values(" +
                                                        "'" + EntryDate + "'," +
                                                        "'" + EntryTime + "'," +
                                                        "'" + ShiftDate + "'," +
                                                        "'" + Shift + "'," +
                                                        "'" + ShiftBeginTime.ToShortTimeString() + "'," +
                                                        "'" + ShiftEndTime.ToShortTimeString() + "'," +
                                                        "'" + ShiftHours + "'," +
                                                        "'" + PlantIncharge + "'," +
                                                        "'" + VolumeChecker + "'," +
                                                        "'" + Supervisor + "'," +
                                                        "'" + Packer + "'," +
                                                        "'" + Operator + "'," +
                                                        "'" + PreformLoader + "'," +
                                                        "'" + MouldChanger + "'," +
                                                        "'" + Porter + "'," +
                                                        "" + OEEId + "," +
                                                        "" + OEEMachineId + "," +
                                                        "" + MachineId + "," +
                                                        "'" + MachineNo + "'," +
                                                        "" + ShiftScheduleId + "," +
                                                        "" + ProductId + "," +
                                                        "'" + ProductName + "'," +
                                                        "" + MouldId + "," +
                                                        "'" + MouldNo + "'," +
                                                        "'" + ShiftLengthA + "'," +
                                                        "'" + ShortMealBreakB + "'," +
                                                        "'" + NoPlanning + "'," +
                                                        "'" + NoElectricity + "'," +
                                                        "'" + TotalProductionTime + "'," +
                                                        "'" + Breakdown + "'," +
                                                        "'" + Changeover + "'," +
                                                        "'" + ManpowerShortage + "'," +
                                                        "'" + StartupLoss + "'," +
                                                        "'" + MaintainanceMachineShutDownTime + "'," +
                                                        "'" + MaterialNotAvailable + "'," +
                                                        "'" + TotalDowntime + "'," +
                                                        "'" + OperatingTime + "'," +
                                                        "'" + Availabilty + "'," +
                                                        "'" + IdealRunRate + "'," +
                                                        "'" + Cavity + "'," +
                                                        "'" + PlanningQty + "'," +
                                                        "'" + TotalShot + "'," +
                                                        "'" + TotalProductionInNos + "'," +
                                                        "'" + Packing + "'," +
                                                        "'" + TotalPacket + "'," +
                                                        "'" + TargetProduction + "'," +
                                                        "'" + Performance + "'," +
                                                        "'" + RejectInNos + "'," +
                                                        "'" + GoodInNos + "'," +
                                                        "'" + Quality + "'," +
                                                        "'" + OEE + "'," +
                                                        "" + SwitchFlag + "," +
                                                        "'" + Status + "'," +
                                                        "'" + Reason + "'," +
                                                        "'" + PreformRejection + "'," +
                                                        "" + ShiftId_I + "," +
                                                        "'" + MachineAvailableTime + "'," +
                                                        "'" + BalanceLength + "')";
                    int Result = objBL.Function_ExecuteNonQuery();
                }
            }
        }

        private void Get_Report_Name()
        {
            objBL.Query = "delete from OEEReport where CancelTag=0";
            objBL.Function_ExecuteNonQuery();

            VolumeChecker = string.Empty;
            PlantIncharge = string.Empty;
            Supervisor = string.Empty;
            Packer = string.Empty;
            Operator = string.Empty;

            DataSet ds = new DataSet();

            if (!string.IsNullOrEmpty(Convert.ToString(ReportName)))
            {
                if (ReportName == BusinessResources.R_OEEReportMachineWise)
                {
                    ParameterFlag = true;

                    ReportDS = "dsOEE";
                    RDLC_ReportName = "OEEReport.rdlc";
                    //objBL.Query = "select OE.ID,OE.EntryDate as [Date],OE.EntryTime as [Time],OE.Shift,OE.PlantInchargeId,E.FullName as[Plant Incharge],OE.VolumeCheckerId,E1.FullName as [Volume Checker] from ((OEEEntry OE inner join Employee E on E.ID=OE.PlantInchargeId) inner join Employee E1 on E1.ID=OE.VolumeCheckerId) where OE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and OE.Shift='" + cmbShift.Text + "' and OE.EntryDate=#" + dtpDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# ";

                    //objBL.Query = "select OM.OEEMachineId,OM.EntryDate,OM.EntryTime,OM.OEEId,OM.ShiftScheduleId,SS.ShiftDate,SS.ShiftHours,SS.BeginTime1,SS.EndTime1,SS.BeginTime2,SS.EndTime2,SS.BeginTime3,SS.EndTime3,OM.Shift,OM.MachineId,MM.MachineNo,OM.ProductId,P.ProductName,P.MouldNo,OM.ShiftLengthA,OM.ShortMealBreakB,OM.NoPlanning,OM.NoElectricity,OM.TotalProductionTime,OM.Breakdown,OM.Changeover,OM.ManpowerShortage,OM.StartupLoss,OM.MaintainanceMachineShutDownTime,OM.MaterialNotAvailable,OM.TotalDowntime,OM.OperatingTime,OM.Availabilty,IdealRunRate,OM.Cavity,OM.PlanningQty,OM.TotalShot,OM.TotalProductionInNos,OM.Packing,OM.TotalPacket,OM.TargetProduction,OM.Performance,OM.RejectInNos,OM.GoodInNos,OM.Quality,OM.OEE,OM.SwitchFlag,OM.SwitchPackerMachineId,OM.SwitchOperatiorMachineId,OM.Status,OM.Reason,OM.SwitchNote,OM.PreformRejection from (((OEEMachine OM inner join MachineMaster MM on MM.MachineId=OM.MachineId) inner join Product P on P.ID=OM.ProductId) inner join ShiftSchedule SS on SS.ShiftScheduleId=OM.ShiftScheduleId) where SS.CancelTag=0 and P.CancelTag=0 and MM.CancelTag=0 and OM.CancelTag=0 and OM.EntryDate=#" + objRL.EntryDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# ";
                    //objBL.Query = "select OM.OEEMachineId,OM.EntryDate,OM.EntryTime,OM.OEEId,OM.ShiftScheduleId,SS.ShiftDate,SS.ShiftHours,SS.BeginTime1,SS.EndTime1,SS.BeginTime2,SS.EndTime2,SS.BeginTime3,SS.EndTime3,OM.Shift,OM.MachineId,MM.MachineNo,OM.ProductId,P.ProductName,P.MouldNo,OM.ShiftLengthA,OM.ShortMealBreakB,OM.NoPlanning,OM.NoElectricity,OM.TotalProductionTime,OM.Breakdown,OM.Changeover,OM.ManpowerShortage,OM.StartupLoss,OM.MaintainanceMachineShutDownTime,OM.MaterialNotAvailable,OM.TotalDowntime,OM.OperatingTime,OM.Availabilty,IdealRunRate,OM.Cavity,OM.PlanningQty,OM.TotalShot,OM.TotalProductionInNos,OM.Packing,OM.TotalPacket,OM.TargetProduction,OM.Performance,OM.RejectInNos,OM.GoodInNos,OM.Quality,OM.OEE,OM.SwitchFlag,OM.SwitchPackerMachineId,OM.SwitchOperatiorMachineId,OM.Status,OM.Reason,OM.SwitchNote,OM.PreformRejection from (((OEEMachine OM inner join MachineMaster MM on MM.MachineId=OM.MachineId) inner join Product P on P.ID=OM.ProductId) inner join ShiftSchedule SS on SS.ShiftScheduleId=OM.ShiftScheduleId) where SS.CancelTag=0 and P.CancelTag=0 and MM.CancelTag=0 and OM.CancelTag=0 and OM.EntryDate=#" + objRL.EntryDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and OM.MachineId=" + objRL.MachineId + "";// and OM.Shift='" + objRL.Shift + "'";

                    objBL.Query = "select OM.OEEMachineId,OM.Shift,OM.EntryDate,OM.EntryTime,OM.OEEId,OM.ShiftScheduleId,SS.ShiftDate,SS.ShiftHours,SS.BeginTime1,SS.EndTime1,SS.BeginTime2,SS.EndTime2,SS.BeginTime3,SS.EndTime3,OM.Shift,OM.MachineId,MM.MachineNo,OM.ProductId,P.ProductName,P.MouldNo,P.MouldId,OM.ShiftLengthA,OM.ShortMealBreakB,OM.NoPlanning,OM.NoElectricity,OM.TotalProductionTime,OM.Breakdown,OM.Changeover,OM.ManpowerShortage,OM.StartupLoss,OM.MaintainanceMachineShutDownTime,OM.MaterialNotAvailable,OM.TotalDowntime,OM.OperatingTime,OM.Availabilty,IdealRunRate,OM.Cavity,OM.PlanningQty,OM.TotalShot,OM.TotalProductionInNos,OM.Packing,OM.TotalPacket,OM.TargetProduction,OM.Performance,OM.RejectInNos,OM.GoodInNos,OM.Quality,OM.OEE,OM.SwitchFlag,OM.SwitchPackerMachineId,OM.SwitchOperatiorMachineId,OM.Status,OM.Reason,OM.SwitchNote,OM.PreformRejection from (((OEEMachine OM inner join MachineMaster MM on MM.MachineId=OM.MachineId) inner join Product P on P.ID=OM.ProductId) inner join ShiftSchedule SS on SS.ShiftScheduleId=OM.ShiftScheduleId) where SS.CancelTag=0 and P.CancelTag=0 and MM.CancelTag=0 and OM.CancelTag=0 and SS.ShiftDate=#" + objRL.EntryDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# "; // and OM.MachineId=" + objRL.MachineId + "";// and OM.Shift='" + objRL.Shift + "'";

                    ds = objBL.ReturnDataSet();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ClearReportValues();

                            OEEMachineId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEEMachineId"])));
                            OEEId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEEId"])));
                            ShiftScheduleId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftScheduleId"])));
                            MachineId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MachineId"])));
                            MachineNo = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MachineNo"]));

                            EntryDate = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["EntryDate"]));
                            EntryTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["EntryTime"]));

                            ShiftDate = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["ShiftDate"]));
                            Shift = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift"]));
                            ShiftHours = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftHours"]));

                            if (Shift == "I")
                            {
                                ShiftBeginTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["BeginTime1"]));
                                ShiftEndTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["EndTime1"]));
                            }
                            else if (Shift == "II")
                            {
                                ShiftBeginTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["BeginTime2"]));
                                ShiftEndTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["EndTime2"]));
                            }
                            else if (Shift == "III")
                            {
                                ShiftBeginTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["BeginTime3"]));
                                ShiftEndTime = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["EndTime3"]));
                            }
                            else
                            {

                            }

                            OEEType = "PlantIncharge";
                            PlantIncharge = GetEmployee();

                            OEEType = "VolumeChecker";
                            VolumeChecker = GetEmployee();

                            OEEType = "Supervisor";
                            Supervisor = GetEmployee();

                            //Other by Machine ID
                            OEEType = "Packer";
                            Packer = GetEmployee();

                            OEEType = "Operator";
                            Operator = GetEmployee();

                            OEEType = "PreformLoader";
                            PreformLoader = GetEmployee();

                            OEEType = "Porter";
                            Porter = GetEmployee();

                            OEEType = "MouldChanger";
                            MouldChanger = GetEmployee();

                            ProductId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ProductId"])));

                            ProductName = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ProductName"]));

                            ProductName = ProductName.Replace("'", "''");

                            MouldId = Convert.ToInt32(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MouldId"])));
                            MouldNo = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MouldNo"]));
                            ShiftLengthA = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftLengthA"]));
                            ShortMealBreakB = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShortMealBreakB"]));
                            NoPlanning = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["NoPlanning"]));
                            NoElectricity = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["NoElectricity"]));
                            TotalProductionTime = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["TotalProductionTime"]));
                            Breakdown = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Breakdown"]));
                            Changeover = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Changeover"]));
                            ManpowerShortage = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ManpowerShortage"]));
                            StartupLoss = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["StartupLoss"]));
                            MaintainanceMachineShutDownTime = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MaintainanceMachineShutDownTime"]));
                            MaterialNotAvailable = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["MaterialNotAvailable"]));
                            TotalDowntime = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["TotalDowntime"]));
                            OperatingTime = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OperatingTime"]));
                            Availabilty = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Availabilty"]));
                            IdealRunRate = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["IdealRunRate"]));
                            Cavity = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Cavity"]));
                            PlanningQty = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["PlanningQty"]));
                            TotalShot = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["TotalShot"]));
                            TotalProductionInNos = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["TotalProductionInNos"]));
                            Packing = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Packing"]));
                            TotalPacket = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["TotalPacket"]));
                            TargetProduction = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["TargetProduction"]));
                            Performance = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Performance"]));
                            RejectInNos = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["RejectInNos"]));
                            GoodInNos = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["GoodInNos"]));
                            Quality = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Quality"]));
                            OEE = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OEE"]));
                            SwitchFlag = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["SwitchFlag"]));
                            SwitchPackerMachineId = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["SwitchPackerMachineId"]));
                            SwitchOperatiorMachineId = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["SwitchOperatiorMachineId"]));
                            Status = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Status"]));
                            Reason = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Reason"]));
                            SwitchNote = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["SwitchNote"]));
                            PreformRejection = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["PreformRejection"]));

                           // Shift = "III";
                            objBL.Query = "insert into OEEReport(" +
                                                                "EntryDate,"+
                                                                "EntryTime,"+
                                                                "ShiftDate,"+
                                                                "Shift,"+
                                                                "ShiftBeginTime,"+
                                                                "ShiftEndTime,"+
                                                                "ShiftHours,"+
                                                                "PlantIncharge,"+
                                                                "VolumeChecker,"+
                                                                "Supervisor,"+
                                                                "Packer,"+
                                                                "Operator,"+
                                                                "PreformLoader,"+
                                                                "MouldChanger,"+
                                                                "Porter,"+
                                                                "OEEId,"+
                                                                "OEEMachineId,"+
                                                                "MachineId,"+
                                                                "MachineNo,"+
                                                                "ShiftScheduleId,"+
                                                                "ProductId,"+
                                                                "ProductName,"+
                                                                "MouldId,"+
                                                                "MouldNo,"+
                                                                "ShiftLengthA,"+
                                                                "ShortMealBreakB,"+
                                                                "NoPlanning,"+
                                                                "NoElectricity,"+
                                                                "TotalProductionTime,"+
                                                                "Breakdown,"+
                                                                "Changeover,"+
                                                                "ManpowerShortage,"+
                                                                "StartupLoss,"+
                                                                "MaintainanceMachineShutDownTime,"+
                                                                "MaterialNotAvailable,"+
                                                                "TotalDowntime,"+
                                                                "OperatingTime,"+
                                                                "Availabilty,"+
                                                                "IdealRunRate,"+
                                                                "Cavity,"+
                                                                "PlanningQty,"+
                                                                "TotalShot,"+
                                                                "TotalProductionInNos,"+
                                                                "Packing,"+
                                                                "TotalPacket,"+
                                                                "TargetProduction,"+
                                                                "Performance,"+
                                                                "RejectInNos,"+
                                                                "GoodInNos,"+
                                                                "Quality,"+
                                                                "OEE,"+
                                                                "SwitchFlag,"+
                                                                "SwitchPackerMachineId,"+
                                                                "SwitchOperatiorMachineId,"+
                                                                "Status,"+
                                                                "Reason,"+
                                                                "SwitchNote,"+
                                                                "PreformRejection) values(" +
                                                                "'" + EntryDate + "'," +
                                                                "'" + EntryTime + "'," +
                                                                "'" + ShiftDate + "'," +
                                                                "'" + Shift + "'," +
                                                                "'" + ShiftBeginTime + "'," +
                                                                "'" + ShiftEndTime + "'," +
                                                                "'" + ShiftHours + "'," +
                                                                "'" + PlantIncharge + "'," +
                                                                "'" + VolumeChecker + "'," +
                                                                "'" + Supervisor + "'," +
                                                                "'" + Packer + "'," +
                                                                "'" + Operator + "'," +
                                                                "'" + PreformLoader + "'," +
                                                                "'" + MouldChanger + "'," +
                                                                "'" + Porter + "'," +
                                                                "" + OEEId + "," +
                                                                "" + OEEMachineId + "," +
                                                                "" + MachineId + "," +
                                                                "'" + MachineNo + "'," +
                                                                "" + ShiftScheduleId + "," +
                                                                "" + ProductId + "," +
                                                                "'" + ProductName + "'," +
                                                                "" + MouldId + "," +
                                                                "'" + MouldNo + "'," +
                                                                "'" + ShiftLengthA + "'," +
                                                                "'" + ShortMealBreakB + "'," +
                                                                "'" + NoPlanning + "'," +
                                                                "'" + NoElectricity + "'," +
                                                                "'" + TotalProductionTime + "'," +
                                                                "'" + Breakdown + "'," +
                                                                "'" + Changeover + "'," +
                                                                "'" + ManpowerShortage + "'," +
                                                                "'" + StartupLoss + "'," +
                                                                "'" + MaintainanceMachineShutDownTime + "'," +
                                                                "'" + MaterialNotAvailable + "'," +
                                                                "'" + TotalDowntime + "'," +
                                                                "'" + OperatingTime + "'," +
                                                                "'" + Availabilty + "'," +
                                                                "'" + IdealRunRate + "'," +
                                                                "'" + Cavity + "'," +
                                                                "'" + PlanningQty + "'," +
                                                                "'" + TotalShot + "'," +
                                                                "'" + TotalProductionInNos + "'," +
                                                                "'" + Packing + "'," +
                                                                "'" + TotalPacket + "'," +
                                                                "'" + TargetProduction + "'," +
                                                                "'" + Performance + "'," +
                                                                "'" + RejectInNos + "'," +
                                                                "'" + GoodInNos + "'," +
                                                                "'" + Quality + "'," +
                                                                "'" + OEE + "'," +
                                                                "" + SwitchFlag + "," +
                                                                "" + SwitchPackerMachineId + "," +
                                                                "" + SwitchOperatiorMachineId + "," +
                                                                "'" + Status + "'," +
                                                                "'" + Reason + "'," +
                                                                "'" + SwitchNote + "'," +
                                                                "'" + PreformRejection + "')";
                            int Result= objBL.Function_ExecuteNonQuery();
                        }
                        
                        //OE.OEEEmployeeId,OE.OEEId,OE.ShiftScheduleId,OE.Shift,OE.OEEType,OE.EmployeeId,E.FullName
                        Load_Report(Get_Report_Query());
                    }
                }
            }
        }


        int ShiftId = 0;
        private string GetEmployee()
        {
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;

            string EName = string.Empty;
            DataSet ds = new DataSet();

            MainQuery = "select OE.OEEEmployeeId,OE.OEEId,OE.ShiftId,OE.OEEType,OE.EmployeeId,E.FullName from OEEEmployee OE inner join Employee E on E.ID=OE.EmployeeId where OE.CancelTag=0 and E.CancelTag=0 and OE.OEEId=" + OEEId + " and OE.ShiftId=" + ShiftId + " and OEEType='" + OEEType + "'";

            if (OEEType != "PlantIncharge" && OEEType != "VolumeChecker" && OEEType != "Porter" && OEEType != "MouldChanger" && OEEType != "Supervisor")
            {
                if (MachineId != 0 && OEEMachineId != 0)
                    WhereClause = " and OE.OEEMachineId=" + OEEMachineId + " and MachineId=" + MachineId + "";
            }

            OrderByClause = " order by OE.OEEEmployeeId desc";
            objBL.Query = MainQuery + WhereClause + OrderByClause;

            //objBL.Query = "select OE.OEEEmployeeId,OE.OEEId,OE.ShiftScheduleId,OE.Shift,OE.OEEType,OE.EmployeeId,E.FullName from OEEEmployee OE inner join Employee E on E.ID=OE.EmployeeId where OE.CancelTag=0 and E.CancelTag=0 and OE.OEEId=" + OEEId + " and OE.ShiftScheduleId=" + ShiftScheduleId + " and OE.Shift='" + Shift + "' and OEEType='" + OEEType + "' order by OE.OEEEmployeeId desc";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                EName = "";
                if (ds.Tables[0].Rows.Count > 1)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EName += objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["FullName"].ToString()));
                        EName += ",";
                    }
                    EName = EName.Substring(0, EName.Length - 1);
                }
                else
                    EName = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["FullName"].ToString()));
            }
            return EName;
        }

        private void ViewReportW_Load(object sender, EventArgs e)
        {
            this.rvReport.RefreshReport();
        }

        string ReportPath = string.Empty;
        string ReportDS = string.Empty, ReportDS1 = string.Empty, ReportDS_CompanyComman = string.Empty;
        string RDLC_ReportName = string.Empty;
        string ReportConcatPath = string.Empty;
        string ReportName = string.Empty;

        bool ParameterFlag = false;

        string rpReportName = string.Empty, rpReportDate = string.Empty, rpReportPeriod = string.Empty, rpReportBy = string.Empty;

        DataSet ds1 = new DataSet();
        private void Load_Report(DataSet ds)
        {
            rvReport.Clear();
            rvReport.LocalReport.DataSources.Clear();

            ReportConcatPath = string.Empty;
            ReportConcatPath = objRL.GetPath_WithoutServer("RdlcPath");

            //ParameterFlag = false;
            rpReportName = string.Empty; rpReportDate = string.Empty; rpReportPeriod = string.Empty; rpReportBy = string.Empty;

            rpReportName = "Report Name: " + ReportName;
            rpReportDate = "Date: " + DateTime.Now.Date.ToString("dd/MMM/yyyy");
            //rpReportPeriod = objQL.ReportPeriod;
            rpReportBy = BusinessLayer.UserName_Static;

            ReportConcatPath += RDLC_ReportName;
            
            rvReport.Visible = true;
            rvReport.ProcessingMode = ProcessingMode.Local;
            rvReport.LocalReport.ReportPath = ReportConcatPath;

            ReportDS1 = "dsEmployee";
            //RDLC_ReportName = "OEEReportNew.rdlc";

           // objBL.Query="select * from 

            objBL.Query = "select OE.OEEEmployeeId,OE.OEEId,OE.ShiftId,OE.OEEType,OE.EmployeeId,E.FullName from OEEEmployee OE inner join Employee E on E.ID=OE.EmployeeId where OE.CancelTag=0 and E.CancelTag=0 and OE.OEEId=" + objRL.OEEId + " and OE.OEEType NOT IN('Operator','Packer')";
            ds1 = objBL.ReturnDataSet();

            ReportDataSource rds = new ReportDataSource(ReportDS, ds.Tables[0]);
            ReportDataSource rds1 = new ReportDataSource(ReportDS1, ds1.Tables[0]);

            PlantIncharge = "Plant Incharge: " + PlantIncharge;
            Supervisor = "Supervisor: " + Supervisor;
            VolumeChecker = "Volume Checker: " + VolumeChecker;
            string DT = objRL.EntryDate.ToString(BusinessResources.DATEFORMATDDMMYYYY).ToString();

            if (ParameterFlag)
            {
                ReportParameter[] parameters = new ReportParameter[1];
                //parameters[0] = new ReportParameter("rpPlantIncharge", PlantIncharge);
                //parameters[1] = new ReportParameter("rpSupervisor", Supervisor);
                //parameters[2] = new ReportParameter("rpVolumeChecker", VolumeChecker);
                parameters[0] = new ReportParameter("rpDateTime", DT);
                //parameters[4] = new ReportParameter("rpOperator", Operator);
                //parameters[5] = new ReportParameter("rpPacker", Packer);

                rvReport.LocalReport.SetParameters(parameters);
            }

            rvReport.LocalReport.DataSources.Add(rds);
            rvReport.LocalReport.DataSources.Add(rds1);
            rvReport.LocalReport.Refresh();
            rvReport.RefreshReport();
        }

        string ReportQuery = string.Empty;

        private DataSet Get_Report_Query()
        {
            DataSet ds = new DataSet();
            ReportQuery = "select " +
                            "OEEReportId, " +
                            "EntryDate, " +
                            "EntryTime, " +
                            "ShiftDate, " +
                            "Shift, " +
                            "ShiftBeginTime, " +
                            "ShiftEndTime, " +
                            "ShiftHours, " +
                            "PlantIncharge, " +
                            "VolumeChecker, " +
                            "Supervisor, " +
                            "Packer, " +
                            "Operator, " +
                            "PreformLoader, " +
                            "MouldChanger, " +
                            "Porter, " +
                            "OEEId, " +
                            "OEEMachineId, " +
                            "MachineId, " +
                            "MachineNo, " +
                            "ShiftScheduleId, " +
                            "ProductId, " +
                            "ProductName, " +
                            "MouldId, " +
                            "MouldNo, " +
                            "ShiftLengthA, " +
                            "ShortMealBreakB, " +
                            "NoPlanning, " +
                            "NoElectricity, " +
                            "TotalProductionTime, " +
                            "Breakdown, " +
                            "Changeover, " +
                            "ManpowerShortage, " +
                            "StartupLoss, " +
                            "MaintainanceMachineShutDownTime, " +
                            "MaterialNotAvailable, " +
                            "TotalDowntime, " +
                            "OperatingTime, " +
                            "Availabilty, " +
                            "IdealRunRate, " +
                            "Cavity, " +
                            "PlanningQty, " +
                            "TotalShot, " +
                            "TotalProductionInNos, " +
                            "Packing, " +
                            "TotalPacket, " +
                            "TargetProduction, " +
                            "Performance, " +
                            "RejectInNos, " +
                            "GoodInNos, " +
                            "Quality, " +
                            "OEE, " +
                            "SwitchFlag, " +
                            "SwitchPackerMachineId, " +
                            "SwitchOperatiorMachineId, " +
                            "Status, " +
                            "Reason, " +
                            "SwitchNote, " +
                            "PreformRejection," +
                            "ShiftId," +
                            "MachineAvailableTime," +
                            "BalanceLength " +
                            " from OEEReport where CancelTag=0 order by MachineId asc";

            objBL.Query = ReportQuery;
            ds = objBL.ReturnDataSet();
            return ds;
        }
    }
}
