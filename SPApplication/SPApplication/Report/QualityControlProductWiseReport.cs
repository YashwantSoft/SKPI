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
using SPApplication.Report.RDLCReport;
//using System.Windows.Media;

namespace SPApplication.Report
{
    public partial class QualityControlProductWiseReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        ErrorProvider objEP = new ErrorProvider();
        DesignLayer objDL = new DesignLayer();

        bool FlagToday = false;
        string QueryParameter = string.Empty;
        
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

        DateTime BookingDate;
        int Result= 0;

        public QualityControlProductWiseReport()
        {
            InitializeComponent();
            objDL.Set_Report_Design(this, lblHeader, btnView, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_QUALITYCONTROLPRODUCTWISEREPORT);
            lblBottleInformation.BackColor = Color.Cyan;
            lblJarInformation.BackColor = Color.Yellow;
            //  this.lbItem.DrawMode = DrawMode.OwnerDrawFixed;
            //this.lbItem.DrawItem += new System.Windows.Forms.DrawItemEventHandler(lbItem_DrawItem);
        }

        private void QualityControlProductWiseReport_Load(object sender, EventArgs e)
        {
            objRL.Fill_Item_ListBox(lbItem, txtSearchProductName.Text, "All");
            ClearAll();
            //FillGrid();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ClearAll_Item()
        {
            ProductId = 0;
            lblProductType.Text = "";
            lblProductName.Text = "";
        }

        private void txtSearchProductName_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Item();
            if (txtSearchProductName.Text != "")
            {
                objRL.Fill_Item_ListBox(lbItem, txtSearchProductName.Text, "Text");
                //lbItem.Focus();
            }
            else
            {
                lbItem.Visible = true;
                objRL.Fill_Item_ListBox(lbItem, txtSearchProductName.Text, "All");
            }
        }

        private void lbItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Fill_Product_Information();
            }
        }

        string ProductDetails = string.Empty;
        //string Cavity = string.Empty;
        //string ProductName = string.Empty;

        private void Fill_Product_Information()
        {
            Cavity = string.Empty;
            ProductId = Convert.ToInt32(lbItem.SelectedValue);

            if (ProductId != 0)
            {
                ProductDetails = string.Empty;
                objRL.Get_Product_Records_By_Id(ProductId);
                ProductDetails = string.Empty;

                ProductDetails = "Mould No-\t" + objRL.SrNoMould.ToString() + "\t" + "Party-\t" + objRL.Party.ToString() + "\n" +
                                 "Cavity-\t\t" + objRL.Cavity.ToString() + "\t" + "Type-\t" + objRL.AutoSemi.ToString() + "\n" +
                                 "Preform Name-\t\t" + objRL.PreformName.ToString() + "\n" +
                                 "Nick Name-\t" + objRL.ProductNickName.ToString();

                lblProductName.Text = objRL.ProductName.ToString();
                ProductName = objRL.ProductName.ToString();

                if (!string.IsNullOrEmpty(objRL.Cavity))
                {
                    Cavity = objRL.Cavity;
                }

                if (!string.IsNullOrEmpty(objRL.ProductType))
                {
                    lblProductType.Text = objRL.ProductType.ToString();
                    if (objRL.ProductType == "Bottle")
                    {
                        lblProductName.BackColor = Color.Cyan;
                        lblProductType.BackColor = Color.Cyan;
                    }
                    else
                    {
                        lblProductName.BackColor = Color.Yellow;
                        lblProductType.BackColor = Color.Yellow;
                    }
                }
                btnView.Focus();
                FillGrid();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewReport();
        }

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;
        bool DateFlag = false;

        private bool Validation()
        {
            if (ProductId == 0)
            {
                lbItem.Focus();
                objEP.SetError(lbItem, "Select Item");
                return true;
            }
            else
                return false;
        }

        DateTime EntryDate,EntryTime;

        string Shift=string.Empty,PlantInchargeName=string.Empty,VolumeChecker=string.Empty,MachineNo=string.Empty,ProductType=string.Empty,ProductName=string.Empty,SrNoS=string.Empty,Cavity=string.Empty;
        string Supplier = string.Empty, Weight = string.Empty, ColorS = string.Empty, SizeS = string.Empty, InnerDia = string.Empty, OuterDia = string.Empty, RetainerGap = string.Empty, HeightS = string.Empty, OverflowVolume = string.Empty, MajorAxis = string.Empty, MinorAxis = string.Empty, BottleHeight = string.Empty, Visuals=string.Empty,GoGauge = string.Empty, CaptFitment = string.Empty, WadSealing = string.Empty, LeakTest = string.Empty, DropTest = string.Empty;
        string TopLoadTest = string.Empty, WeightR = string.Empty, SizeR = string.Empty, InnerDiaR = string.Empty, OuterDiaR = string.Empty, RetainerGapR = string.Empty, HeightR = string.Empty, OverflowVolumeR = string.Empty, MajorAxisR = string.Empty, MinorAxisR = string.Empty, BottleHeightR = string.Empty, VisualsR = string.Empty, GoGaugeR = string.Empty, CaptFitmentR = string.Empty, WadSealingR = string.Empty, LeakTestR = string.Empty, DropTestR = string.Empty, TopLoadTestR = string.Empty, BaseInformation = string.Empty, BaseInformationR = string.Empty;
        int QCEntryId = 0, QCEntryMachineId = 0, ProductionEntryId = 0, ShiftEntryId = 0, PlantInchargeId = 0, VolumeCheckerId = 0, MouldId = 0, ProductId = 0, QCEntryMachineValuesId=0;


        private void ClearValues()
        {
            Shift=string.Empty;PlantInchargeName=string.Empty;VolumeChecker=string.Empty;MachineNo=string.Empty;ProductType=string.Empty;ProductName=string.Empty;SrNoS=string.Empty;Cavity=string.Empty;
            Supplier = string.Empty; Weight = string.Empty; ColorS = string.Empty; SizeS = string.Empty; InnerDia = string.Empty; OuterDia = string.Empty; RetainerGap = string.Empty; HeightS = string.Empty; 
            OverflowVolume = string.Empty; MajorAxis = string.Empty; MinorAxis = string.Empty; BottleHeight = string.Empty; Visuals=string.Empty;GoGauge = string.Empty; CaptFitment = string.Empty; WadSealing = string.Empty; 
            LeakTest = string.Empty; DropTest = string.Empty; TopLoadTest = string.Empty; WeightR = string.Empty; SizeR = string.Empty; InnerDiaR = string.Empty; OuterDiaR = string.Empty; RetainerGapR = string.Empty; HeightR = string.Empty; 
            OverflowVolumeR = string.Empty; MajorAxisR = string.Empty; MinorAxisR = string.Empty; BottleHeightR = string.Empty; VisualsR = string.Empty; GoGaugeR = string.Empty; CaptFitmentR = string.Empty; WadSealingR = string.Empty; LeakTestR = string.Empty;
            DropTestR = string.Empty; TopLoadTestR = string.Empty; BaseInformation = string.Empty; BaseInformationR = string.Empty;
            QCEntryId = 0; QCEntryMachineId = 0; ProductionEntryId = 0; ShiftEntryId = 0; PlantInchargeId = 0; VolumeCheckerId = 0; MouldId = 0; ProductId = 0; QCEntryMachineValuesId=0;
        }

        private void ViewReport()
        {
            if (!Validation())
            {
                dataGridView1.DataSource = null;
                MainQuery = string.Empty;
                WhereClause = string.Empty;
                OrderByClause = string.Empty;
                UserClause = string.Empty;

                dataGridView1.DataSource = null;
                DataSet ds = new DataSet();

                if (BusinessLayer.UserName_Static != BusinessResources.USER_ADMIN)
                    UserClause = " and QCE.UserId = " + BusinessLayer.UserId_Static;
                else
                    UserClause = string.Empty;

                //MainQuery = "select ID,EntryDate as [Date],EntryTime as [Time],Shift,MachinNo as [Machine No],PlantInchargeId,VolumeCheckerId,ProductId,ProductName  as [Product Name],NeckSizeI,NeckSizeRemarkI,WeightI,WeightRemarkI,NeckIDI,NeckIDRemarkI,NeckODI,NeckODRemarkI,NeckCollarGapI,NeckCollarGapRemarkI,NeckHeightI,NeckHeightRemarkI,ProductHeightI,ProductHeightRemarkI,ProductVolumeI,ProductVolumeRemarkI,CapSealingI,CapSealingRemarkI,WadSealingI,WadSealingRemarkI,NeckSizeII,NeckSizeRemarkII,WeightII,WeightRemarkII,NeckIDII,NeckIDRemarkII,NeckODII,NeckODRemarkII,NeckCollarGapII,NeckCollarGapRemarkII,NeckHeightII,NeckHeightRemarkII,ProductHeightII,ProductHeightRemarkII,ProductVolumeII,ProductVolumeRemarkII,CapSealingII,CapSealingRemarkII,WadSealingII,WadSealingRemarkII,CheckerNote,Remark,ErrorCount,ShiftEntryId,PlantInchargeId as[Plant Incharge],VolumeCheckerId as [Volume Checker],Cavity from QualityRegisterEntry where CancelTag=0 ";

                //MainQuery = "select QCE.ID,QCE.EntryDate as [Date],QCE.EntryTime as [Time],QCE.ShiftEntryId,SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],QCE.MachinNo as [Machine No],QCE.ProductId,P.ProductType as [Product Type],P.ProductName as [Product Name],P.MouldId,MM.Cavity,QCE.PreformPartyId,PPM.PreformParty as [Preform Party],QCE.NeckSize1 as [Neck Size 1],QCE.NeckSizeRemark1,QCE.NeckID1 as [Neck ID 1],QCE.NeckIDRemark1,QCE.NeckOD1 as [Neck OD 1],QCE.NeckODRemark1,QCE.NeckCollarGap1 as [Neck Collar Gap 1],QCE.NeckCollarGapRemark1,QCE.NeckHeight1 as [Neck Height 1],QCE.NeckHeightRemark1,QCE.ProductHeight1 as [Product Height 1],QCE.ProductHeightRemark1,QCE.Weight1 as [Weight 1],QCE.WeightRemark1,QCE.ProductVolume1 as [Product Volume 1],QCE.ProductVolumeRemark1,QCE.CapSealing1 as [Cap Sealing 1],QCE.CapSealingRemark1,QCE.WadSealing1 as [Wad Sealing 1],QCE.WadSealingRemark1,QCE.NeckSize2 as [Neck Size 2],QCE.NeckSizeRemark2,QCE.NeckID2 as [Neck ID 2],QCE.NeckIDRemark2,QCE.NeckOD2 as [Neck OD 2],QCE.NeckODRemark2,QCE.NeckCollarGap2 as [Neck Collar Gap 2],QCE.NeckCollarGapRemark2,QCE.NeckHeight2 as [Neck Height 2],QCE.NeckHeightRemark2,QCE.ProductHeight2 as [Product Height 2],QCE.ProductHeightRemark2,QCE.Weight2 as [Weight 2],QCE.WeightRemark2,QCE.ProductVolume2 as [Product Volume 2],QCE.ProductVolumeRemark2,QCE.CapSealing2 as [Cap Sealing 2],QCE.CapSealingRemark2,QCE.WadSealing2 as [Wad Sealing 2],QCE.WadSealingRemark2,QCE.CheckerNote as [Checker Note],QCE.Remark,QCE.ErrorCount,ProductionEntryId from ((((((QCEntry QCE inner join Product P on P.ID=QCE.ProductId) inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join PreformPartyMaster PPM on PPM.ID=QCE.PreformPartyId) inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) inner join MouldMaster MM on MM.ID=P.MouldId) where QCE.CancelTag=0 and P.CancelTag=0 and SE.CancelTag=0  and PPM.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and MM.CancelTag=0";

                //MainQuery = "select QCE.ID,QCE.EntryDate as [Date],QCE.EntryTime as [Time],QCE.ShiftEntryId,SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],QCE.MachinNo as [Machine No],QCE.ProductId,P.ProductType as [Product Type],P.ProductName as [Product Name],P.MouldId,MM.Cavity,QCE.PreformPartyId,PPM.PreformParty as [Preform Party],QCE.NeckSize1 as [Neck Size 1],QCE.NeckSizeRemark1,QCE.NeckID1 as [Neck ID 1],QCE.NeckIDRemark1,QCE.NeckOD1 as [Neck OD 1],QCE.NeckODRemark1,QCE.NeckCollarGap1 as [Neck Collar Gap 1],QCE.NeckCollarGapRemark1,QCE.NeckHeight1 as [Neck Height 1],QCE.NeckHeightRemark1,QCE.ProductHeight1 as [Product Height 1],QCE.ProductHeightRemark1,QCE.Weight1 as [Weight 1],QCE.WeightRemark1,QCE.ProductVolume1 as [Product Volume 1],QCE.ProductVolumeRemark1,QCE.CapSealing1 as [Cap Sealing 1],QCE.CapSealingRemark1,QCE.WadSealing1 as [Wad Sealing 1],QCE.WadSealingRemark1,QCE.NeckSize2 as [Neck Size 2],QCE.NeckSizeRemark2,QCE.NeckID2 as [Neck ID 2],QCE.NeckIDRemark2,QCE.NeckOD2 as [Neck OD 2],QCE.NeckODRemark2,QCE.NeckCollarGap2 as [Neck Collar Gap 2],QCE.NeckCollarGapRemark2,QCE.NeckHeight2 as [Neck Height 2],QCE.NeckHeightRemark2,QCE.ProductHeight2 as [Product Height 2],QCE.ProductHeightRemark2,QCE.Weight2 as [Weight 2],QCE.WeightRemark2,QCE.ProductVolume2 as [Product Volume 2],QCE.ProductVolumeRemark2,QCE.CapSealing2 as [Cap Sealing 2],QCE.CapSealingRemark2,QCE.WadSealing2 as [Wad Sealing 2],QCE.WadSealingRemark2,QCE.CheckerNote as [Checker Note],QCE.Remark,QCE.ErrorCount,ProductionEntryId,E.FirstName as [Plant],E1.FirstName as [Volume] from ((((((QCEntry QCE inner join Product P on P.ID=QCE.ProductId) inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join PreformPartyMaster PPM on PPM.ID=QCE.PreformPartyId) inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) inner join MouldMaster MM on MM.ID=P.MouldId) where QCE.CancelTag=0 and P.CancelTag=0 and SE.CancelTag=0  and PPM.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and MM.CancelTag=0";
                //MainQuery = "select QCE.ID,QCE.EntryDate as [Date],QCE.EntryTime as [Time],QCE.ShiftEntryId,SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],QCE.MachinNo as [Machine No],QCE.ProductId,P.ProductType as [Product Type],P.ProductName as [Product Name],P.MouldId,MM.Cavity,QCE.PreformPartyId,PPM.PreformParty as [Preform Party],QCE.NeckSize1 as [Neck Size 1],QCE.NeckSizeRemark1,QCE.NeckID1 as [Neck ID 1],QCE.NeckIDRemark1,QCE.NeckOD1 as [Neck OD 1],QCE.NeckODRemark1,QCE.NeckCollarGap1 as [Neck Collar Gap 1],QCE.NeckCollarGapRemark1,QCE.NeckHeight1 as [Neck Height 1],QCE.NeckHeightRemark1,QCE.ProductHeight1 as [Product Height 1],QCE.ProductHeightRemark1,QCE.Weight1 as [Weight 1],QCE.WeightRemark1,QCE.ProductVolume1 as [Product Volume 1],QCE.ProductVolumeRemark1,QCE.CapSealing1 as [Cap Sealing 1],QCE.CapSealingRemark1,QCE.WadSealing1 as [Wad Sealing 1],QCE.WadSealingRemark1,QCE.NeckSize2 as [Neck Size 2],QCE.NeckSizeRemark2,QCE.NeckID2 as [Neck ID 2],QCE.NeckIDRemark2,QCE.NeckOD2 as [Neck OD 2],QCE.NeckODRemark2,QCE.NeckCollarGap2 as [Neck Collar Gap 2],QCE.NeckCollarGapRemark2,QCE.NeckHeight2 as [Neck Height 2],QCE.NeckHeightRemark2,QCE.ProductHeight2 as [Product Height 2],QCE.ProductHeightRemark2,QCE.Weight2 as [Weight 2],QCE.WeightRemark2,QCE.ProductVolume2 as [Product Volume 2],QCE.ProductVolumeRemark2,QCE.CapSealing2 as [Cap Sealing 2],QCE.CapSealingRemark2,QCE.WadSealing2 as [Wad Sealing 2],QCE.WadSealingRemark2,QCE.CheckerNote as [Checker Note],QCE.Remark,QCE.ErrorCount,QCEM.ProductionEntryId,E.FirstName as [Plant],E1.FirstName as [Volume] from (((((((QCEntryMachineValues QCEMV inner join QCEntryMachine QCEM on QCEM.ID=QCEMV.QCEntryMachineId) inner join QCEntry QCE on QCE.ID=QCEM.QCEntryId) inner join ProductionEntry PE1 on PE1.ID=QCEM.ProductionEntryId) inner join ShiftEntry SE1 on SE1.ID=QCE1.ShiftEntryId) inner join Employee E on E.ID=SE1.PlantInchargeId) inner join Employee E1 on E1.ID=SE1.VolumeCheckerId) inner join Product P on P.ID=QCEM.ProductId) where QCEMV.CancelTag=0 and QCEM.CancelTag=0 and QCE1.CancelTag=0 and PE1.CancelTag=0 and SE1.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and P.CancelTag=0 ";

                MainQuery = "select " +
                            "Distinct QCEMV.ID from " +
                             " (((((((( " +
                            " QCEntryMachineValues QCEMV inner join QCEntryMachine QCEM on QCEM.ID=QCEMV.QCEntryMachineId) " +
                            " inner join QCEntry QCE on QCE.ID=QCEM.QCEntryId) " +
                            " inner join ProductionEntry PE on PE.ID=QCEM.ProductionEntryId) " +
                            " inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) " +
                            " inner join Employee E on E.ID=SE.PlantInchargeId) " +
                            " inner join Employee E1 on E1.ID=SE.VolumeCheckerId) " +
                            " inner join Product P on P.ID=QCEM.ProductId) " +
                            " inner join MouldMaster MM on MM.ID=P.MouldId) " +
                            " where QCEMV.CancelTag=0 and QCEM.CancelTag=0 and QCE.CancelTag=0 and PE.CancelTag=0 and SE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and P.CancelTag=0 and MM.CancelTag=0 and QCEM.ProductId=250 ";

                            
                //MainQuery = "select QCEMV.ID,SE1.Shift,QCE1.ShiftEntryId,QCEMV.QCEntryId,QCEMV.QCEntryMachineId,QCEMV.ProductionEntryId,QCEMV.MachineNo,QCEM.ProductId,P.ProductName as [Product Name],QCEMV.Supplier,[QCEMV.Weight] as [Weight],[QCEMV.Color] as [Color],[QCEMV.Size] as [Size],QCEMV.InnerDia,QCEMV.OuterDia,QCEMV.RetainerGap,[QCEMV.Height] as [Height],QCEMV.OverflowVolume,QCEMV.MajorAxis,QCEMV.MinorAxis,QCEMV.BottleHeight,QCEMV.Visuals,QCEMV.GoGauge,QCEMV.CaptFitment,QCEMV.WadSealing,QCEMV.LeakTest,QCEMV.DropTest,QCEMV.TopLoadTest,E.FullName as [PlantIncharge],E1.FullName as [VolumeChecker],WeightR,SizeR,InnerDiaR,OuterDiaR,RetainerGapR,HeightR,OverflowVolumeR,MajorAxisR,MinorAxisR,BottleHeightR,VisualsR,GoGaugeR,CaptFitmentR,WadSealingR,LeakTestR,DropTestR,TopLoadTestR from (((((((QCEntryMachineValues QCEMV inner join QCEntryMachine QCEM on QCEM.ID=QCEMV.QCEntryMachineId) inner join QCEntry QCE1 on QCE1.ID=QCEM.QCEntryId) inner join ProductionEntry PE1 on PE1.ID=QCEM.ProductionEntryId) inner join ShiftEntry SE1 on SE1.ID=QCE1.ShiftEntryId) inner join Employee E on E.ID=SE1.PlantInchargeId) inner join Employee E1 on E1.ID=SE1.VolumeCheckerId) inner join Product P on P.ID=QCEM.ProductId) where P.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and QCEMV.CancelTag=0 and QCE1.CancelTag=0 and SE1.CancelTag=0 and QCEM.CancelTag=0 and PE1.CancelTag=0";
                //MainQuery = "select QRE.ID,QRE.EntryDate as [Date],QRE.EntryTime as [Time],QRE.Shift,QRE.MachinNo as [Machine No],QRE.PlantInchargeId,QRE.VolumeCheckerId,QRE.ProductId,QRE.ProductName  as [Product Name],QRE.NeckSizeI,QRE.NeckSizeRemarkI,QRE.WeightI,QRE.WeightRemarkI,QRE.NeckIDI,QRE.NeckIDRemarkI,QRE.NeckODI,QRE.NeckODRemarkI,QRE.NeckCollarGapI,QRE.NeckCollarGapRemarkI,QRE.NeckHeightI,QRE.NeckHeightRemarkI,QRE.ProductHeightI,QRE.ProductHeightRemarkI,QRE.ProductVolumeI,QRE.ProductVolumeRemarkI,QRE.CapSealingI,QRE.CapSealingRemarkI,QRE.WadSealingI,QRE.WadSealingRemarkI,QRE.NeckSizeII,QRE.NeckSizeRemarkII,QRE.WeightII,QRE.WeightRemarkII,QRE.NeckIDII,QRE.NeckIDRemarkII,QRE.NeckODII,QRE.NeckODRemarkII,QRE.NeckCollarGapII,QRE.NeckCollarGapRemarkII,QRE.NeckHeightII,QRE.NeckHeightRemarkII,QRE.ProductHeightII,QRE.ProductHeightRemarkII,QRE.ProductVolumeII,QRE.ProductVolumeRemarkII,QRE.CapSealingII,QRE.CapSealingRemarkII,QRE.WadSealingII,QRE.WadSealingRemarkII,QRE.CheckerNote,QRE.Remark,QRE.ErrorCount from QualityRegisterEntry QRE where CancelTag=0 ";

                //"QCEMV.Supplier as [Preform Party]," +
                //"QCEMV.NeckSize1 as [Neck Size 1]," +
                //"QCEMV.NeckSizeRemark1," +
                //"QCEMV.NeckID1 as [Neck ID 1]," +
                //"QCEMV.NeckIDRemark1," +
                //"QCEMV.NeckOD1 as [Neck OD 1]," +
                //"QCEMV.NeckODRemark1," +
                //"QCEMV.NeckCollarGap1 as [Neck Collar Gap 1]," +
                //"QCEMV.NeckCollarGapRemark1," +
                //"QCEMV.NeckHeight1 as [Neck Height 1]," +
                //"QCEMV.NeckHeightRemark1," +
                //"QCEMV.ProductHeight1 as [Product Height 1]," +
                //"QCEMV.ProductHeightRemark1," +
                //"QCEMV.Weight1 as [Weight 1]," +
                //"QCEMV.WeightRemark1," +
                //"QCEMV.ProductVolume1 as [Product Volume 1]," +
                //"QCEMV.ProductVolumeRemark1," +
                //"QCEMV.CapSealing1 as [Cap Sealing 1]," +
                //"QCEMV.CapSealingRemark1," +
                //"QCEMV.WadSealing1 as [Wad Sealing 1]," +
                //"QCEMV.WadSealingRemark1," +
                //"QCEMV.NeckSize2 as [Neck Size 2]," +
                //"QCEMV.NeckSizeRemark2," +
                //"QCEMV.NeckID2 as [Neck ID 2]," +
                //"QCEMV.NeckIDRemark2," +
                //"QCEMV.NeckOD2 as [Neck OD 2]," +
                //"QCEMV.NeckODRemark2," +
                //"QCEMV.NeckCollarGap2 as [Neck Collar Gap 2]," +
                //"QCEMV.NeckCollarGapRemark2," +
                //"QCEMV.NeckHeight2 as [Neck Height 2]," +
                //"QCEMV.NeckHeightRemark2," +
                //"QCEMV.ProductHeight2 as [Product Height 2]," +
                //"QCEMV.ProductHeightRemark2," +
                //"QCEMV.Weight2 as [Weight 2]," +
                //"QCEMV.WeightRemark2," +
                //"QCEMV.ProductVolume2 as [Product Volume 2]," +
                //"QCEMV.ProductVolumeRemark2," +
                //"QCEMV.CapSealing2 as [Cap Sealing 2]," +
                //"QCEMV.CapSealingRemark2," +
                //"QCEMV.WadSealing2 as [Wad Sealing 2]," +
                //"QCEMV.WadSealingRemark2," +
                //"QCEMV.CheckerNote as [Checker Note]," +
                //"QCEMV.Remark," +
                //"QCEMV.ErrorCount," +
                //"QCEM.ProductionEntryId " +
                if (DateFlag)
                    WhereClause = " and QCE.EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
                //else
                //    WhereClause = string.Empty;

                if (ProductId > 0)
                    WhereClause += " and QCEM.ProductId=" + ProductId + "";

                OrderByClause = " order by QCEMV.ID asc";

                objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnReport.Visible = true;
                    int QCEntryId = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        QCEntryMachineValuesId = 0;
                        QCEntryMachineValuesId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ID"])));

                        DataSet dsQC = new DataSet();
                        MainQuery = "select " +
                            "QCE.ID as [QCEntryId], " +
                            "QCE.EntryDate as [Date]," +
                            "QCE.EntryTime as [Time]," +
                            "SE.Shift," +
                            "E.FullName as [Plant Incharge]," +
                            "E1.FullName as [Volume Checker]," +
                            "QCEMV.MachineNo as [Machine No]," +
                            "P.ProductType as [Product Type]," +
                            "P.ProductName as [Product Name]," +
                            "MM.SrNo," +
                            "MM.Cavity," +
                            "QCEMV.Supplier," +
                            "[QCEMV.Weight] as [Weight]," +
                            "[QCEMV.Color] as [Color]," +
                            "[QCEMV.Size] as [Size]," +
                            "QCEMV.InnerDia," +
                            "QCEMV.OuterDia," +
                            "QCEMV.RetainerGap," +
                            "[QCEMV.Height] as [Height]," +
                            "QCEMV.OverflowVolume," +
                            "QCEMV.MajorAxis," +
                            "QCEMV.MinorAxis," +
                            "QCEMV.BottleHeight," +
                            "QCEMV.Visuals," +
                            "QCEMV.GoGauge," +
                            "QCEMV.CaptFitment," +
                            "QCEMV.WadSealing," +
                            "QCEMV.LeakTest," +
                            "QCEMV.DropTest," +
                            "QCEMV.TopLoadTest," +
                            "QCEMV.WeightR," +
                            "QCEMV.SizeR," +
                            "QCEMV.InnerDiaR," +
                            "QCEMV.OuterDiaR," +
                            "QCEMV.RetainerGapR,"+
                            "QCEMV.HeightR," +
                            "QCEMV.OverflowVolumeR,"+
                            "QCEMV.MajorAxisR," +
                            "QCEMV.MinorAxisR," +
                            "QCEMV.BottleHeightR," +
                            "QCEMV.VisualsR," +
                            "QCEMV.GoGaugeR," +
                            "QCEMV.CaptFitmentR," +
                            "QCEMV.WadSealingR," +
                            "QCEMV.LeakTestR," +
                            "QCEMV.DropTestR," +
                            "QCEMV.TopLoadTestR," +
                            "QCEMV.BaseInformation," +
                            "QCEMV.BaseInformationR," +
                            "QCEMV.QCEntryId," +
                            "QCEMV.QCEntryMachineId," +
                            "QCEMV.ProductionEntryId," +
                             "QCE.ShiftEntryId," +
                             "SE.PlantInchargeId," +
                            "SE.VolumeCheckerId," +
                            "QCEM.ProductId," +
                            "P.MouldId," +
                            "QCEMV.ID" +
                            " from " +
                            " (((((((( " +
                            " QCEntryMachineValues QCEMV inner join QCEntryMachine QCEM on QCEM.ID=QCEMV.QCEntryMachineId) " +
                            " inner join QCEntry QCE on QCE.ID=QCEM.QCEntryId) " +
                            " inner join ProductionEntry PE on PE.ID=QCEM.ProductionEntryId) " +
                            " inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) " +
                            " inner join Employee E on E.ID=SE.PlantInchargeId) " +
                            " inner join Employee E1 on E1.ID=SE.VolumeCheckerId) " +
                            " inner join Product P on P.ID=QCEM.ProductId) " +
                            " inner join MouldMaster MM on MM.ID=P.MouldId) " +
                            " where QCEMV.ID=" + QCEntryMachineValuesId + " and QCEMV.CancelTag=0 and QCEM.CancelTag=0 and QCE.CancelTag=0 and PE.CancelTag=0 and SE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and P.CancelTag=0 and MM.CancelTag=0 and QCEM.ProductId=250 ";

                        objBL.Query = MainQuery;
                        dsQC = objBL.ReturnDataSet();

                        if (dsQC.Tables[0].Rows.Count > 0)
                        {
                            ClearValues();

                            if (!string.IsNullOrEmpty(Convert.ToString(dsQC.Tables[0].Rows[0]["Date"])))
                                EntryDate = Convert.ToDateTime(dsQC.Tables[0].Rows[0]["Date"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(dsQC.Tables[0].Rows[0]["Time"])))
                                EntryTime = Convert.ToDateTime(dsQC.Tables[0].Rows[0]["Time"]);

                            Shift = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["Shift"]));
                            PlantInchargeName = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["Plant Incharge"]));
                            VolumeChecker = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["Volume Checker"]));
                            MachineNo = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["Machine No"]));
                            ProductType = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["Product Type"]));
                            ProductName = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["Product Name"]));
                            SrNoS = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["SrNo"]));

                            objRL.MouldNo = SrNoS;
                            objRL.Cavity = Cavity;
                            objRL.ProductType = ProductType;

                            Cavity = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["Cavity"]));
                            Supplier = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["Supplier"]));
                            Weight = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["Weight"]));
                            ColorS = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["Color"]));
                            SizeS = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["Size"]));
                            InnerDia = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["InnerDia"]));
                            OuterDia = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["OuterDia"]));
                            RetainerGap = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["RetainerGap"]));
                            HeightS = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["Height"]));
                            OverflowVolume = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["OverflowVolume"]));
                            MajorAxis = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["MajorAxis"]));
                            MinorAxis = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["MinorAxis"]));
                            BottleHeight = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["BottleHeight"]));
                            Visuals = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["Visuals"]));
                            GoGauge = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["GoGauge"]));
                            CaptFitment = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["CaptFitment"]));
                            WadSealing = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["WadSealing"]));
                            LeakTest = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["LeakTest"]));
                            DropTest = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["DropTest"]));
                            TopLoadTest = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["TopLoadTest"]));

                            WeightR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["WeightR"]));
                            SizeR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["SizeR"]));
                            InnerDiaR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["InnerDiaR"]));
                            OuterDiaR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["OuterDiaR"]));
                            RetainerGapR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["RetainerGapR"]));
                            HeightR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["HeightR"]));
                            OverflowVolumeR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["OverflowVolumeR"]));
                            MajorAxisR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["MajorAxisR"]));
                            MinorAxisR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["MinorAxisR"]));
                            BottleHeightR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["BottleHeightR"]));
                            VisualsR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["VisualsR"]));
                            GoGaugeR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["GoGaugeR"]));
                            CaptFitmentR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["CaptFitmentR"]));
                            WadSealingR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["WadSealingR"]));
                            LeakTestR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["LeakTestR"]));
                            DropTestR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["DropTestR"]));
                            TopLoadTestR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["TopLoadTestR"]));
                            BaseInformation = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["BaseInformation"]));
                            BaseInformationR = objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["BaseInformationR"]));
                            QCEntryId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["QCEntryId"])));
                            QCEntryMachineId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["QCEntryMachineId"])));
                            ProductionEntryId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["ProductionEntryId"])));
                            QCEntryMachineId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["QCEntryMachineId"])));
                            ShiftEntryId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["ShiftEntryId"])));
                            PlantInchargeId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["PlantInchargeId"])));
                            VolumeCheckerId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["VolumeCheckerId"])));
                            MouldId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["MouldId"])));
                            ProductId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["ProductId"])));
                            QCEntryMachineValuesId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dsQC.Tables[0].Rows[0]["ID"])));

                            DataSet dsCheckExist = new DataSet();
                            objBL.Query = "select * from ReportTemp where CancelTag=0 and QCEntryMachineValuesId=" + QCEntryMachineValuesId + " and CancelTag=0";
                            dsCheckExist = objBL.ReturnDataSet();
                            if (dsCheckExist.Tables[0].Rows.Count == 0)
                            {

                                objBL.Query = "insert into ReportTemp " +
                                            "(EntryDate,EntryTime,Shift,PlantInchargeName,VolumeChecker,MachineNo,ProductType,ProductName,SrNo,Cavity,Supplier,[Weight],[Color],[Size],InnerDia,OuterDia,RetainerGap,Height,OverflowVolume,MajorAxis,MinorAxis,BottleHeight,Visuals,GoGauge,CaptFitment,WadSealing,LeakTest,DropTest,TopLoadTest,WeightR,SizeR,InnerDiaR,OuterDiaR,RetainerGapR,HeightR,OverflowVolumeR,MajorAxisR,MinorAxisR,BottleHeightR,VisualsR,GoGaugeR,CaptFitmentR,WadSealingR,LeakTestR,DropTestR,TopLoadTestR,BaseInformation,BaseInformationR,QCEntryId,QCEntryMachineId,ProductionEntryId,ShiftEntryId,PlantInchargeId,VolumeCheckerId,MouldId,ProductId,QCEntryMachineValuesId,UserId) " +
                                            " values( " +
                                            "'" + EntryDate.ToShortDateString() + "'," +
                                            "'" + EntryTime.ToShortTimeString() + "'," +
                                            "'" + Shift + "'," +
                                            "'" + PlantInchargeName + "'," +
                                            "'" + VolumeChecker + "'," +
                                            "'" + MachineNo + "'," +
                                            "'" + ProductType + "'," +
                                            "'" + ProductName + "'," +
                                            "'" + SrNoS + "'," +
                                            "'" + Cavity + "'," +
                                            "'" + Supplier + "'," +
                                            "'" + Weight + "'," +
                                            "'" + ColorS + "'," +
                                            "'" + SizeS + "'," +
                                            "'" + InnerDia + "'," +
                                            "'" + OuterDia + "'," +
                                            "'" + RetainerGap + "'," +
                                            "'" + Height + "'," +
                                            "'" + OverflowVolume + "'," +
                                            "'" + MajorAxis + "'," +
                                            "'" + MinorAxis + "'," +
                                            "'" + BottleHeight + "'," +
                                            "'" + Visuals + "'," +
                                            "'" + GoGauge + "'," +
                                            "'" + CaptFitment + "'," +
                                            "'" + WadSealing + "'," +
                                            "'" + LeakTest + "'," +
                                            "'" + DropTest + "'," +
                                            "'" + TopLoadTest + "'," +
                                            "'" + WeightR + "'," +
                                            "'" + SizeR + "'," +
                                            "'" + InnerDiaR + "'," +
                                            "'" + OuterDiaR + "'," +
                                            "'" + RetainerGapR + "'," +
                                            "'" + HeightR + "'," +
                                            "'" + OverflowVolumeR + "'," +
                                            "'" + MajorAxisR + "'," +
                                            "'" + MinorAxisR + "'," +
                                            "'" + BottleHeightR + "'," +
                                            "'" + VisualsR + "'," +
                                            "'" + GoGaugeR + "'," +
                                            "'" + CaptFitmentR + "'," +
                                            "'" + WadSealingR + "'," +
                                            "'" + LeakTestR + "'," +
                                            "'" + DropTestR + "'," +
                                            "'" + TopLoadTestR + "'," +
                                            "'" + BaseInformation + "'," +
                                            "'" + BaseInformationR + "'," +
                                            "" + QCEntryId + "," +
                                            "" + QCEntryMachineId + "," +
                                            "" + ProductionEntryId + "," +
                                            "" + ShiftEntryId + "," +
                                            "" + PlantInchargeId + "," +
                                            "" + VolumeCheckerId + "," +
                                            "" + MouldId + "," +
                                            "" + ProductId + "," +
                                            "" + QCEntryMachineValuesId + "," +
                                            "" + BusinessLayer.UserId_Static + ")";

                                Result = objBL.Function_ExecuteNonQuery();
                                if (Result > 0)
                                {

                                }
                            }
                        }
                    }
                                           
                    

                    //0 QCE.ID,
                    //1 QCE.EntryDate as [Date],
                    //2 QCE.EntryTime as [Time],
                    //3 QCE.ShiftEntryId,
                    //4 SE.Shift,
                    //5 SE.PlantInchargeId,
                    //6 E.FullName as [Plant Incharge],
                    //7 SE.VolumeCheckerId,
                    //8 E1.FullName as [Volume Checker],
                    //9 QCE.MachinNo as [Machine No],
                    //10 QCE.ProductId,
                    //11 P.ProductType as [Product Type],
                    //12 P.ProductName as [Product Name],
                    //13 P.MouldId,
                    //14 MM.Cavity,
                    //15 QCE.PreformPartyId,
                    //16 PPM.PreformParty as [Preform Party],
                    //17 QCE.NeckSize1 as [Neck Size 1],
                    //18 QCE.NeckSizeRemark1,
                    //19 QCE.NeckID1 as [Neck ID 1],
                    //20 QCE.NeckIDRemark1,
                    //21 QCE.NeckOD1  as [Neck OD 1],
                    //22 QCE.NeckODRemark1,
                    //23 QCE.NeckCollarGap1 as [Neck Collar Gap 1],
                    //24 QCE.NeckCollarGapRemark1,
                    //25 QCE.NeckHeightI as [Neck Height I],
                    //26 QCE.NeckHeightRemark1,
                    //27 QCE.ProductHeightI as [Product Height I],
                    //28 QCE.ProductHeightRemark1,
                    //29 QCE.Weight1 as [Weight 1],
                    //30 QCE.WeightRemark1,
                    //31 QCE.ProductVolume1 as [Product Volume 1],
                    //32 QCE.ProductVolumeRemark1,
                    //33 QCE.CapSealing1 as [Cap Sealing 1],
                    //34 QCE.CapSealingRemark1,
                    //35 QCE.WadSealing1 as [Wad Sealing 1],
                    //36 QCE.WadSealingRemark1,
                    //37 QCE.NeckSize2 as [Neck Size 2],
                    //38 QCE.NeckSizeRemarkII,
                    //39 QCE.NeckID2 as [Neck ID 2],
                    //40 QCE.NeckIDRemark2,
                    //41 QCE.NeckOD2 as [Neck OD 2],
                    //42 QCE.NeckODRemark2,
                    //43 QCE.NeckCollarGap2 as [Neck Collar Gap 2],
                    //44 QCE.NeckCollarGapRemark2,
                    //45 QCE.NeckHeight2 as [Neck Height 2],
                    //46 QCE.NeckHeightRemark2,
                    //47 QCE.ProductHeight2 as [Product Height 2],
                    //48 QCE.ProductHeightRemark2,
                    //49 QCE.Weight2 as [Weight 2],
                    //50 QCE.WeightRemark2,
                    //51 QCE.ProductVolume2 as [Product Volume 2],
                    //52 QCE.ProductVolumeRemark2,
                    //53 QCE.CapSealing2 as [Cap Sealing 2],
                    //54 QCE.CapSealingRemark2,
                    //55 QCE.WadSealing2 as [Wad Sealing 2],
                    //56 QCE.WadSealingRemark2,
                    //57 QCE.CheckerNote as [Checker Note],
                    //58 QCE.Remark as [],
                    //59 QCE.ErrorCount
                    //60 QCE.ProductionEntryId

                    //17 QCE.NeckSize1 as [Neck Size 1],
                    //18 QCE.NeckSizeRemark1,
                    //19 QCE.NeckID1 as [Neck ID 1],
                    //20 QCE.NeckIDRemark1,
                    //21 QCE.NeckOD1  as [Neck OD 1],
                    //22 QCE.NeckODRemark1,
                    //23 QCE.NeckCollarGap1 as [Neck Collar Gap 1],
                    //24 QCE.NeckCollarGapRemark1,
                    //25 QCE.NeckHeightI as [Neck Height I],
                    //26 QCE.NeckHeightRemark1,
                    //27 QCE.ProductHeightI as [Product Height I],
                    //28 QCE.ProductHeightRemark1,
                    //29 QCE.Weight1 as [Weight 1],
                    //30 QCE.WeightRemark1,
                    //31 QCE.ProductVolume1 as [Product Volume 1],
                    //32 QCE.ProductVolumeRemark1,
                    //33 QCE.CapSealing1 as [Cap Sealing 1],
                    //34 QCE.CapSealingRemark1,
                    //35 QCE.WadSealing1 as [Wad Sealing 1],
                    //36 QCE.WadSealingRemark1,
                    //37 QCE.NeckSize2 as [Neck Size 2],
                    //38 QCE.NeckSizeRemarkII,
                    //39 QCE.NeckID2 as [Neck ID 2],
                    //40 QCE.NeckIDRemark2,
                    //41 QCE.NeckOD2 as [Neck OD 2],
                    //42 QCE.NeckODRemark2,
                    //43 QCE.NeckCollarGap2 as [Neck Collar Gap 2],
                    //44 QCE.NeckCollarGapRemark2,
                    //45 QCE.NeckHeight2 as [Neck Height 2],
                    //46 QCE.NeckHeightRemark2,
                    //47 QCE.ProductHeight2 as [Product Height 2],
                    //48 QCE.ProductHeightRemark2,
                    //49 QCE.Weight2 as [Weight 2],
                    //50 QCE.WeightRemark2,
                    //51 QCE.ProductVolume2 as [Product Volume 2],
                    //52 QCE.ProductVolumeRemark2,
                    //53 QCE.CapSealing2 as [Cap Sealing 2],
                    //54 QCE.CapSealingRemark2,
                    //55 QCE.WadSealing2 as [Wad Sealing 2],
                    //56 QCE.WadSealingRemark2,

                    //dataGridView1.DataSource = ds.Tables[0];
                    //dataGridView1.Columns[0].Visible = false;
                    //dataGridView1.Columns[3].Visible = false;
                    //dataGridView1.Columns[5].Visible = false;
                    //dataGridView1.Columns[6].Visible = false;
                    //dataGridView1.Columns[8].Visible = false;
                    //dataGridView1.Columns[7].Visible = false;
                    //dataGridView1.Columns[5].Visible = false;
                    //dataGridView1.Columns[10].Visible = false;
                    //dataGridView1.Columns[11].Visible = false;
                    //dataGridView1.Columns[13].Visible = false;
                    //dataGridView1.Columns[14].Visible = false;
                    //dataGridView1.Columns[15].Visible = false;
                    //dataGridView1.Columns[58].Visible = false;
                    //dataGridView1.Columns[59].Visible = false;
                    //dataGridView1.Columns[60].Visible = false;

                    

                    ////Visible False
                    //for (int k = 18; k <= 56; k = k + 2)
                    //{
                    //    dataGridView1.Columns[k].Visible = false;
                    //}
                    //for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    //{
                    //    dataGridView1.Columns[i].Width = 130;
                    //}
                    //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    //{
                    //    for (int j = 18; j <= 56; j = j + 2)
                    //    {
                    //        SetRedCellColour(dataGridView1, i, j, j - 1);
                    //    }
                    //}
                    //dataGridView1.Columns[1].Width = 80;
                    //dataGridView1.Columns[2].Width = 70;
                    //dataGridView1.Columns[4].Width = 50;
                    //dataGridView1.Columns[9].Width = 100;
                    //dataGridView1.Columns[12].Width = 200;
                    //dataGridView1.Columns[16].Width = 110;
                    //btnReport.Visible = true;

                    FillGrid();
                }
                else
                {
                    objRL.ShowMessage(25, 4);
                    return;
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void FillGrid()
        {
            if (ProductId > 0)
            {
                dataGridView1.DataSource = null;

                DataSet ds = new DataSet();
                objBL.Query = "select ID,EntryDate,EntryTime,Shift,PlantInchargeName,VolumeChecker,MachineNo,ProductType,ProductName,SrNo,Cavity,Supplier,[Weight],[Color],[Size],InnerDia,OuterDia,RetainerGap,Height,OverflowVolume,MajorAxis,MinorAxis,BottleHeight,Visuals,GoGauge,CaptFitment,WadSealing,LeakTest,DropTest,TopLoadTest,WeightR,SizeR,InnerDiaR,OuterDiaR,RetainerGapR,HeightR,OverflowVolumeR,MajorAxisR,MinorAxisR,BottleHeightR,VisualsR,GoGaugeR,CaptFitmentR,WadSealingR,LeakTestR,DropTestR,TopLoadTestR,BaseInformation,BaseInformationR,QCEntryId,QCEntryMachineId,ProductionEntryId,ShiftEntryId,PlantInchargeId,VolumeCheckerId,MouldId,ProductId,QCEntryMachineValuesId from ReportTemp where CancelTag=0 and ProductId=" + ProductId + "";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnReport.Visible = true;
                    dsSend = ds;

                    lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;

                    //0 ID
                    //1 EntryDate,
                    //2 EntryTime,
                    //3 Shift,
                    //4 PlantInchargeName,
                    //5 VolumeChecker,
                    //6 MachineNo,
                    //7 ProductType,
                    //8 ProductName,
                    //9 SrNo,
                    //10 Cavity,
                    //11 Supplier,
                    //12 [Weight],
                    //13 [Color],
                    //14 [Size],
                    //15 InnerDia,
                    //16 OuterDia,
                    //17 RetainerGap,
                    //18 Height,
                    //19 OverflowVolume,
                    //20 MajorAxis,
                    //21 MinorAxis,
                    //22 BottleHeight,
                    //23 Visuals,
                    //24 GoGauge,
                    //25 CaptFitment,
                    //26 WadSealing,
                    //27 LeakTest,
                    //28 DropTest,
                    //29 TopLoadTest,
                    //30 WeightR,
                    //31 SizeR,
                    //32 InnerDiaR,
                    //33 OuterDiaR,
                    //34 RetainerGapR,
                    //35 HeightR,
                    //36 OverflowVolumeR,
                    //37 MajorAxisR,
                    //38 MinorAxisR,
                    //39 BottleHeightR,
                    //40 VisualsR,
                    //41 GoGaugeR,
                    //42 CaptFitmentR,
                    //43 WadSealingR,
                    //44 LeakTestR,
                    //45 DropTestR,
                    //46 TopLoadTestR,
                    //47 BaseInformation,
                    //48 BaseInformationR,
                    //49 QCEntryId,
                    //50 QCEntryMachineId,
                    //51 ProductionEntryId,
                    //52 ShiftEntryId,
                    //53 PlantInchargeId,
                    //54 VolumeCheckerId,
                    //55 MouldId,
                    //56 ProductId
                    //57 QCEntryMachineValuesId

                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;

                    //dataGridView1.Columns[29].Visible = false;
                    dataGridView1.Columns[30].Visible = false;
                    dataGridView1.Columns[31].Visible = false;
                    dataGridView1.Columns[32].Visible = false;
                    dataGridView1.Columns[33].Visible = false;
                    dataGridView1.Columns[34].Visible = false;
                    dataGridView1.Columns[35].Visible = false;
                    dataGridView1.Columns[36].Visible = false;
                    dataGridView1.Columns[37].Visible = false;
                    dataGridView1.Columns[38].Visible = false;
                    dataGridView1.Columns[39].Visible = false;
                   
                    dataGridView1.Columns[40].Visible = false;
                    dataGridView1.Columns[41].Visible = false;
                    dataGridView1.Columns[42].Visible = false;
                    dataGridView1.Columns[43].Visible = false;
                    dataGridView1.Columns[44].Visible = false;
                    dataGridView1.Columns[45].Visible = false;
                    dataGridView1.Columns[46].Visible = false;
                    dataGridView1.Columns[47].Visible = false;
                    dataGridView1.Columns[48].Visible = false;
                    dataGridView1.Columns[49].Visible = false;
                    dataGridView1.Columns[50].Visible = false;
                    dataGridView1.Columns[51].Visible = false;
                    dataGridView1.Columns[52].Visible = false;
                    dataGridView1.Columns[53].Visible = false;
                    dataGridView1.Columns[54].Visible = false;
                    dataGridView1.Columns[55].Visible = false;
                    dataGridView1.Columns[56].Visible = false;
                    dataGridView1.Columns[57].Visible = false;

                    //0 ID
                    //1 EntryDate,
                    //2 EntryTime,
                    //3 Shift,
                    //4 PlantInchargeName,
                    //5 VolumeChecker,
                    //6 MachineNo,
                    //7 ProductType,
                    //8 ProductName,
                    //9 SrNo,
                    //10 Cavity,
                    //11 Supplier,
                    //12 [Weight],
                    //13 [Color],
                    //14 [Size],
                    //15 InnerDia,
                    //16 OuterDia,
                    //17 RetainerGap,
                    //18 Height,
                    //19 OverflowVolume,
                    //20 MajorAxis,
                    //21 MinorAxis,
                    //22 BottleHeight,
                    //23 Visuals,
                    //24 GoGauge,
                    //25 CaptFitment,
                    //26 WadSealing,
                    //27 LeakTest,
                    //28 DropTest,
                    //29 TopLoadTest,

                    dataGridView1.Columns[1].Width = 80;
                    dataGridView1.Columns[2].Width = 80;
                    dataGridView1.Columns[3].Width = 50;
                    dataGridView1.Columns[4].Width = 150;
                    dataGridView1.Columns[5].Width = 150;
                    dataGridView1.Columns[6].Width = 100;
                    dataGridView1.Columns[7].Width = 120;
                    dataGridView1.Columns[8].Width = 300;
                    dataGridView1.Columns[9].Width = 100;
                    dataGridView1.Columns[10].Width = 100;
                    dataGridView1.Columns[11].Width = 100;

                    dataGridView1.Columns[12].Width = 100;
                    dataGridView1.Columns[13].Width = 100;
                    dataGridView1.Columns[14].Width = 100;
                    dataGridView1.Columns[15].Width = 100;

                    dataGridView1.Columns[16].Width = 100;
                    dataGridView1.Columns[17].Width = 100;
                    dataGridView1.Columns[18].Width = 100;
                    dataGridView1.Columns[19].Width = 100;

                    dataGridView1.Columns[20].Width = 100;
                    dataGridView1.Columns[21].Width = 100;
                    dataGridView1.Columns[22].Width = 100;
                    dataGridView1.Columns[23].Width = 100;

                    dataGridView1.Columns[24].Width = 100;
                    dataGridView1.Columns[25].Width = 100;
                    dataGridView1.Columns[26].Width = 100;
                    dataGridView1.Columns[27].Width = 100;
                    dataGridView1.Columns[28].Width = 100;
                    dataGridView1.Columns[29].Width = 100;

                    lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                }
            }
        }

        //private void ViewReport()
        //{
        //    if (!Validation())
        //    {
        //        dataGridView1.DataSource = null;
        //        MainQuery = string.Empty;
        //        WhereClause = string.Empty;
        //        OrderByClause = string.Empty;
        //        UserClause = string.Empty;

        //        dataGridView1.DataSource = null;
        //        DataSet ds = new DataSet();

        //        if (BusinessLayer.UserName_Static != BusinessResources.USER_ADMIN)
        //            UserClause = " and QCE.UserId = " + BusinessLayer.UserId_Static;
        //        else
        //            UserClause = string.Empty;

        //        //MainQuery = "select ID,EntryDate as [Date],EntryTime as [Time],Shift,MachinNo as [Machine No],PlantInchargeId,VolumeCheckerId,ProductId,ProductName  as [Product Name],NeckSizeI,NeckSizeRemarkI,WeightI,WeightRemarkI,NeckIDI,NeckIDRemarkI,NeckODI,NeckODRemarkI,NeckCollarGapI,NeckCollarGapRemarkI,NeckHeightI,NeckHeightRemarkI,ProductHeightI,ProductHeightRemarkI,ProductVolumeI,ProductVolumeRemarkI,CapSealingI,CapSealingRemarkI,WadSealingI,WadSealingRemarkI,NeckSizeII,NeckSizeRemarkII,WeightII,WeightRemarkII,NeckIDII,NeckIDRemarkII,NeckODII,NeckODRemarkII,NeckCollarGapII,NeckCollarGapRemarkII,NeckHeightII,NeckHeightRemarkII,ProductHeightII,ProductHeightRemarkII,ProductVolumeII,ProductVolumeRemarkII,CapSealingII,CapSealingRemarkII,WadSealingII,WadSealingRemarkII,CheckerNote,Remark,ErrorCount,ShiftEntryId,PlantInchargeId as[Plant Incharge],VolumeCheckerId as [Volume Checker],Cavity from QualityRegisterEntry where CancelTag=0 ";

        //        //MainQuery = "select QCE.ID,QCE.EntryDate as [Date],QCE.EntryTime as [Time],QCE.ShiftEntryId,SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],QCE.MachinNo as [Machine No],QCE.ProductId,P.ProductType as [Product Type],P.ProductName as [Product Name],P.MouldId,MM.Cavity,QCE.PreformPartyId,PPM.PreformParty as [Preform Party],QCE.NeckSize1 as [Neck Size 1],QCE.NeckSizeRemark1,QCE.NeckID1 as [Neck ID 1],QCE.NeckIDRemark1,QCE.NeckOD1 as [Neck OD 1],QCE.NeckODRemark1,QCE.NeckCollarGap1 as [Neck Collar Gap 1],QCE.NeckCollarGapRemark1,QCE.NeckHeight1 as [Neck Height 1],QCE.NeckHeightRemark1,QCE.ProductHeight1 as [Product Height 1],QCE.ProductHeightRemark1,QCE.Weight1 as [Weight 1],QCE.WeightRemark1,QCE.ProductVolume1 as [Product Volume 1],QCE.ProductVolumeRemark1,QCE.CapSealing1 as [Cap Sealing 1],QCE.CapSealingRemark1,QCE.WadSealing1 as [Wad Sealing 1],QCE.WadSealingRemark1,QCE.NeckSize2 as [Neck Size 2],QCE.NeckSizeRemark2,QCE.NeckID2 as [Neck ID 2],QCE.NeckIDRemark2,QCE.NeckOD2 as [Neck OD 2],QCE.NeckODRemark2,QCE.NeckCollarGap2 as [Neck Collar Gap 2],QCE.NeckCollarGapRemark2,QCE.NeckHeight2 as [Neck Height 2],QCE.NeckHeightRemark2,QCE.ProductHeight2 as [Product Height 2],QCE.ProductHeightRemark2,QCE.Weight2 as [Weight 2],QCE.WeightRemark2,QCE.ProductVolume2 as [Product Volume 2],QCE.ProductVolumeRemark2,QCE.CapSealing2 as [Cap Sealing 2],QCE.CapSealingRemark2,QCE.WadSealing2 as [Wad Sealing 2],QCE.WadSealingRemark2,QCE.CheckerNote as [Checker Note],QCE.Remark,QCE.ErrorCount,ProductionEntryId from ((((((QCEntry QCE inner join Product P on P.ID=QCE.ProductId) inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join PreformPartyMaster PPM on PPM.ID=QCE.PreformPartyId) inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) inner join MouldMaster MM on MM.ID=P.MouldId) where QCE.CancelTag=0 and P.CancelTag=0 and SE.CancelTag=0  and PPM.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and MM.CancelTag=0";

        //        //MainQuery = "select QCE.ID,QCE.EntryDate as [Date],QCE.EntryTime as [Time],QCE.ShiftEntryId,SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],QCE.MachinNo as [Machine No],QCE.ProductId,P.ProductType as [Product Type],P.ProductName as [Product Name],P.MouldId,MM.Cavity,QCE.PreformPartyId,PPM.PreformParty as [Preform Party],QCE.NeckSize1 as [Neck Size 1],QCE.NeckSizeRemark1,QCE.NeckID1 as [Neck ID 1],QCE.NeckIDRemark1,QCE.NeckOD1 as [Neck OD 1],QCE.NeckODRemark1,QCE.NeckCollarGap1 as [Neck Collar Gap 1],QCE.NeckCollarGapRemark1,QCE.NeckHeight1 as [Neck Height 1],QCE.NeckHeightRemark1,QCE.ProductHeight1 as [Product Height 1],QCE.ProductHeightRemark1,QCE.Weight1 as [Weight 1],QCE.WeightRemark1,QCE.ProductVolume1 as [Product Volume 1],QCE.ProductVolumeRemark1,QCE.CapSealing1 as [Cap Sealing 1],QCE.CapSealingRemark1,QCE.WadSealing1 as [Wad Sealing 1],QCE.WadSealingRemark1,QCE.NeckSize2 as [Neck Size 2],QCE.NeckSizeRemark2,QCE.NeckID2 as [Neck ID 2],QCE.NeckIDRemark2,QCE.NeckOD2 as [Neck OD 2],QCE.NeckODRemark2,QCE.NeckCollarGap2 as [Neck Collar Gap 2],QCE.NeckCollarGapRemark2,QCE.NeckHeight2 as [Neck Height 2],QCE.NeckHeightRemark2,QCE.ProductHeight2 as [Product Height 2],QCE.ProductHeightRemark2,QCE.Weight2 as [Weight 2],QCE.WeightRemark2,QCE.ProductVolume2 as [Product Volume 2],QCE.ProductVolumeRemark2,QCE.CapSealing2 as [Cap Sealing 2],QCE.CapSealingRemark2,QCE.WadSealing2 as [Wad Sealing 2],QCE.WadSealingRemark2,QCE.CheckerNote as [Checker Note],QCE.Remark,QCE.ErrorCount,ProductionEntryId,E.FirstName as [Plant],E1.FirstName as [Volume] from ((((((QCEntry QCE inner join Product P on P.ID=QCE.ProductId) inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join PreformPartyMaster PPM on PPM.ID=QCE.PreformPartyId) inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) inner join MouldMaster MM on MM.ID=P.MouldId) where QCE.CancelTag=0 and P.CancelTag=0 and SE.CancelTag=0  and PPM.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and MM.CancelTag=0";
        //        //MainQuery = "select QCE.ID,QCE.EntryDate as [Date],QCE.EntryTime as [Time],QCE.ShiftEntryId,SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],QCE.MachinNo as [Machine No],QCE.ProductId,P.ProductType as [Product Type],P.ProductName as [Product Name],P.MouldId,MM.Cavity,QCE.PreformPartyId,PPM.PreformParty as [Preform Party],QCE.NeckSize1 as [Neck Size 1],QCE.NeckSizeRemark1,QCE.NeckID1 as [Neck ID 1],QCE.NeckIDRemark1,QCE.NeckOD1 as [Neck OD 1],QCE.NeckODRemark1,QCE.NeckCollarGap1 as [Neck Collar Gap 1],QCE.NeckCollarGapRemark1,QCE.NeckHeight1 as [Neck Height 1],QCE.NeckHeightRemark1,QCE.ProductHeight1 as [Product Height 1],QCE.ProductHeightRemark1,QCE.Weight1 as [Weight 1],QCE.WeightRemark1,QCE.ProductVolume1 as [Product Volume 1],QCE.ProductVolumeRemark1,QCE.CapSealing1 as [Cap Sealing 1],QCE.CapSealingRemark1,QCE.WadSealing1 as [Wad Sealing 1],QCE.WadSealingRemark1,QCE.NeckSize2 as [Neck Size 2],QCE.NeckSizeRemark2,QCE.NeckID2 as [Neck ID 2],QCE.NeckIDRemark2,QCE.NeckOD2 as [Neck OD 2],QCE.NeckODRemark2,QCE.NeckCollarGap2 as [Neck Collar Gap 2],QCE.NeckCollarGapRemark2,QCE.NeckHeight2 as [Neck Height 2],QCE.NeckHeightRemark2,QCE.ProductHeight2 as [Product Height 2],QCE.ProductHeightRemark2,QCE.Weight2 as [Weight 2],QCE.WeightRemark2,QCE.ProductVolume2 as [Product Volume 2],QCE.ProductVolumeRemark2,QCE.CapSealing2 as [Cap Sealing 2],QCE.CapSealingRemark2,QCE.WadSealing2 as [Wad Sealing 2],QCE.WadSealingRemark2,QCE.CheckerNote as [Checker Note],QCE.Remark,QCE.ErrorCount,QCEM.ProductionEntryId,E.FirstName as [Plant],E1.FirstName as [Volume] from (((((((QCEntryMachineValues QCEMV inner join QCEntryMachine QCEM on QCEM.ID=QCEMV.QCEntryMachineId) inner join QCEntry QCE on QCE.ID=QCEM.QCEntryId) inner join ProductionEntry PE1 on PE1.ID=QCEM.ProductionEntryId) inner join ShiftEntry SE1 on SE1.ID=QCE1.ShiftEntryId) inner join Employee E on E.ID=SE1.PlantInchargeId) inner join Employee E1 on E1.ID=SE1.VolumeCheckerId) inner join Product P on P.ID=QCEM.ProductId) where QCEMV.CancelTag=0 and QCEM.CancelTag=0 and QCE1.CancelTag=0 and PE1.CancelTag=0 and SE1.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and P.CancelTag=0 ";

        //        MainQuery = "select " +
        //                    "Distinct QCE.ID, " +

        //                    "QCE.EntryDate as [Date]," +
        //                    "QCE.EntryTime as [Time]," +
        //                    "QCE.ShiftEntryId," +
        //                    "SE.Shift," +
        //                    "SE.PlantInchargeId," +
        //                    "E.FullName as [Plant Incharge]," +
        //                    "SE.VolumeCheckerId," +
        //                    "E1.FullName as [Volume Checker]," +
        //                    "QCEMV.MachineNo as [Machine No]," +
        //                    "QCEMV.ProductId," +
        //                    "P.ProductType as [Product Type]," +
        //                    "P.ProductName as [Product Name]," +
        //                    "P.MouldId," +
        //                    "MM.SrNo," +
        //                    "MM.Cavity," +
        //                    //"P.ProductName as [Product Name]," +
        //                    //"QCEMV.Supplier," +
        //                    "[QCEMV.Weight] as [Weight]," +
        //                    "[QCEMV.Color] as [Color]," +
        //                    "[QCEMV.Size] as [Size]," +
        //                    "QCEMV.InnerDia," +
        //                    "QCEMV.OuterDia," +
        //                    "QCEMV.RetainerGap," +
        //                    "[QCEMV.Height] as [Height]," +
        //                    "QCEMV.OverflowVolume," +
        //                    "QCEMV.MajorAxis," +
        //                    "QCEMV.MinorAxis," +
        //                    "QCEMV.BottleHeight," +
        //                    "QCEMV.Visuals," +
        //                    "QCEMV.GoGauge," +
        //                    "QCEMV.CaptFitment," +
        //                    "QCEMV.WadSealing," +
        //                    "QCEMV.LeakTest," +
        //                    "QCEMV.DropTest," +
        //                    "QCEMV.TopLoadTest," +
        //                    "QCEMV.QCEntryId," +
        //                    "QCEMV.QCEntryMachineId," +
        //                    "QCEMV.ProductionEntryId," +
        //                    "QCEM.ProductId" +
        //                    " from " +
        //                    " (((((((( " +
        //                    " QCEntryMachineValues QCEMV inner join QCEntryMachine QCEM on QCEM.ID=QCEMV.QCEntryMachineId) " +
        //                    " inner join QCEntry QCE on QCE.ID=QCEM.QCEntryId) " +
        //                    " inner join ProductionEntry PE on PE.ID=QCEM.ProductionEntryId) " +
        //                    " inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) " +
        //                    " inner join Employee E on E.ID=SE.PlantInchargeId) " +
        //                    " inner join Employee E1 on E1.ID=SE.VolumeCheckerId) " +
        //                    " inner join Product P on P.ID=QCEM.ProductId) " +
        //                    " inner join MouldMaster MM on MM.ID=P.MouldId) " +
        //                    " where QCEMV.CancelTag=0 and QCEM.CancelTag=0 and QCE.CancelTag=0 and PE.CancelTag=0 and SE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and P.CancelTag=0 and MM.CancelTag=0 and QCEM.ProductId=250 ";

        //        //MainQuery = "select QCEMV.ID,SE1.Shift,QCE1.ShiftEntryId,QCEMV.QCEntryId,QCEMV.QCEntryMachineId,QCEMV.ProductionEntryId,QCEMV.MachineNo,QCEM.ProductId,P.ProductName as [Product Name],QCEMV.Supplier,[QCEMV.Weight] as [Weight],[QCEMV.Color] as [Color],[QCEMV.Size] as [Size],QCEMV.InnerDia,QCEMV.OuterDia,QCEMV.RetainerGap,[QCEMV.Height] as [Height],QCEMV.OverflowVolume,QCEMV.MajorAxis,QCEMV.MinorAxis,QCEMV.BottleHeight,QCEMV.Visuals,QCEMV.GoGauge,QCEMV.CaptFitment,QCEMV.WadSealing,QCEMV.LeakTest,QCEMV.DropTest,QCEMV.TopLoadTest,E.FullName as [PlantIncharge],E1.FullName as [VolumeChecker],WeightR,SizeR,InnerDiaR,OuterDiaR,RetainerGapR,HeightR,OverflowVolumeR,MajorAxisR,MinorAxisR,BottleHeightR,VisualsR,GoGaugeR,CaptFitmentR,WadSealingR,LeakTestR,DropTestR,TopLoadTestR from (((((((QCEntryMachineValues QCEMV inner join QCEntryMachine QCEM on QCEM.ID=QCEMV.QCEntryMachineId) inner join QCEntry QCE1 on QCE1.ID=QCEM.QCEntryId) inner join ProductionEntry PE1 on PE1.ID=QCEM.ProductionEntryId) inner join ShiftEntry SE1 on SE1.ID=QCE1.ShiftEntryId) inner join Employee E on E.ID=SE1.PlantInchargeId) inner join Employee E1 on E1.ID=SE1.VolumeCheckerId) inner join Product P on P.ID=QCEM.ProductId) where P.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and QCEMV.CancelTag=0 and QCE1.CancelTag=0 and SE1.CancelTag=0 and QCEM.CancelTag=0 and PE1.CancelTag=0";
        //        //MainQuery = "select QRE.ID,QRE.EntryDate as [Date],QRE.EntryTime as [Time],QRE.Shift,QRE.MachinNo as [Machine No],QRE.PlantInchargeId,QRE.VolumeCheckerId,QRE.ProductId,QRE.ProductName  as [Product Name],QRE.NeckSizeI,QRE.NeckSizeRemarkI,QRE.WeightI,QRE.WeightRemarkI,QRE.NeckIDI,QRE.NeckIDRemarkI,QRE.NeckODI,QRE.NeckODRemarkI,QRE.NeckCollarGapI,QRE.NeckCollarGapRemarkI,QRE.NeckHeightI,QRE.NeckHeightRemarkI,QRE.ProductHeightI,QRE.ProductHeightRemarkI,QRE.ProductVolumeI,QRE.ProductVolumeRemarkI,QRE.CapSealingI,QRE.CapSealingRemarkI,QRE.WadSealingI,QRE.WadSealingRemarkI,QRE.NeckSizeII,QRE.NeckSizeRemarkII,QRE.WeightII,QRE.WeightRemarkII,QRE.NeckIDII,QRE.NeckIDRemarkII,QRE.NeckODII,QRE.NeckODRemarkII,QRE.NeckCollarGapII,QRE.NeckCollarGapRemarkII,QRE.NeckHeightII,QRE.NeckHeightRemarkII,QRE.ProductHeightII,QRE.ProductHeightRemarkII,QRE.ProductVolumeII,QRE.ProductVolumeRemarkII,QRE.CapSealingII,QRE.CapSealingRemarkII,QRE.WadSealingII,QRE.WadSealingRemarkII,QRE.CheckerNote,QRE.Remark,QRE.ErrorCount from QualityRegisterEntry QRE where CancelTag=0 ";

        //        //"QCEMV.Supplier as [Preform Party]," +
        //        //"QCEMV.NeckSize1 as [Neck Size 1]," +
        //        //"QCEMV.NeckSizeRemark1," +
        //        //"QCEMV.NeckID1 as [Neck ID 1]," +
        //        //"QCEMV.NeckIDRemark1," +
        //        //"QCEMV.NeckOD1 as [Neck OD 1]," +
        //        //"QCEMV.NeckODRemark1," +
        //        //"QCEMV.NeckCollarGap1 as [Neck Collar Gap 1]," +
        //        //"QCEMV.NeckCollarGapRemark1," +
        //        //"QCEMV.NeckHeight1 as [Neck Height 1]," +
        //        //"QCEMV.NeckHeightRemark1," +
        //        //"QCEMV.ProductHeight1 as [Product Height 1]," +
        //        //"QCEMV.ProductHeightRemark1," +
        //        //"QCEMV.Weight1 as [Weight 1]," +
        //        //"QCEMV.WeightRemark1," +
        //        //"QCEMV.ProductVolume1 as [Product Volume 1]," +
        //        //"QCEMV.ProductVolumeRemark1," +
        //        //"QCEMV.CapSealing1 as [Cap Sealing 1]," +
        //        //"QCEMV.CapSealingRemark1," +
        //        //"QCEMV.WadSealing1 as [Wad Sealing 1]," +
        //        //"QCEMV.WadSealingRemark1," +
        //        //"QCEMV.NeckSize2 as [Neck Size 2]," +
        //        //"QCEMV.NeckSizeRemark2," +
        //        //"QCEMV.NeckID2 as [Neck ID 2]," +
        //        //"QCEMV.NeckIDRemark2," +
        //        //"QCEMV.NeckOD2 as [Neck OD 2]," +
        //        //"QCEMV.NeckODRemark2," +
        //        //"QCEMV.NeckCollarGap2 as [Neck Collar Gap 2]," +
        //        //"QCEMV.NeckCollarGapRemark2," +
        //        //"QCEMV.NeckHeight2 as [Neck Height 2]," +
        //        //"QCEMV.NeckHeightRemark2," +
        //        //"QCEMV.ProductHeight2 as [Product Height 2]," +
        //        //"QCEMV.ProductHeightRemark2," +
        //        //"QCEMV.Weight2 as [Weight 2]," +
        //        //"QCEMV.WeightRemark2," +
        //        //"QCEMV.ProductVolume2 as [Product Volume 2]," +
        //        //"QCEMV.ProductVolumeRemark2," +
        //        //"QCEMV.CapSealing2 as [Cap Sealing 2]," +
        //        //"QCEMV.CapSealingRemark2," +
        //        //"QCEMV.WadSealing2 as [Wad Sealing 2]," +
        //        //"QCEMV.WadSealingRemark2," +
        //        //"QCEMV.CheckerNote as [Checker Note]," +
        //        //"QCEMV.Remark," +
        //        //"QCEMV.ErrorCount," +
        //        //"QCEM.ProductionEntryId " +
        //        if (DateFlag)
        //            WhereClause = " and QCE.EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
        //        //else
        //        //    WhereClause = string.Empty;

        //        if (ProductId > 0)
        //            WhereClause += " and QCEM.ProductId=" + ProductId + "";

        //        OrderByClause = " order by QCE.EntryDate desc";

        //        objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
        //        ds = objBL.ReturnDataSet();

        //        if (ds.Tables[0].Rows.Count > 0)
        //        {

        //            //0 QCE.ID,
        //            //1 QCE.EntryDate as [Date],
        //            //2 QCE.EntryTime as [Time],
        //            //3 QCE.ShiftEntryId,
        //            //4 SE.Shift,
        //            //5 SE.PlantInchargeId,
        //            //6 E.FullName as [Plant Incharge],
        //            //7 SE.VolumeCheckerId,
        //            //8 E1.FullName as [Volume Checker],
        //            //9 QCE.MachinNo as [Machine No],
        //            //10 QCE.ProductId,
        //            //11 P.ProductType as [Product Type],
        //            //12 P.ProductName as [Product Name],
        //            //13 P.MouldId,
        //            //14 MM.Cavity,
        //            //15 QCE.PreformPartyId,
        //            //16 PPM.PreformParty as [Preform Party],
        //            //17 QCE.NeckSize1 as [Neck Size 1],
        //            //18 QCE.NeckSizeRemark1,
        //            //19 QCE.NeckID1 as [Neck ID 1],
        //            //20 QCE.NeckIDRemark1,
        //            //21 QCE.NeckOD1  as [Neck OD 1],
        //            //22 QCE.NeckODRemark1,
        //            //23 QCE.NeckCollarGap1 as [Neck Collar Gap 1],
        //            //24 QCE.NeckCollarGapRemark1,
        //            //25 QCE.NeckHeightI as [Neck Height I],
        //            //26 QCE.NeckHeightRemark1,
        //            //27 QCE.ProductHeightI as [Product Height I],
        //            //28 QCE.ProductHeightRemark1,
        //            //29 QCE.Weight1 as [Weight 1],
        //            //30 QCE.WeightRemark1,
        //            //31 QCE.ProductVolume1 as [Product Volume 1],
        //            //32 QCE.ProductVolumeRemark1,
        //            //33 QCE.CapSealing1 as [Cap Sealing 1],
        //            //34 QCE.CapSealingRemark1,
        //            //35 QCE.WadSealing1 as [Wad Sealing 1],
        //            //36 QCE.WadSealingRemark1,
        //            //37 QCE.NeckSize2 as [Neck Size 2],
        //            //38 QCE.NeckSizeRemarkII,
        //            //39 QCE.NeckID2 as [Neck ID 2],
        //            //40 QCE.NeckIDRemark2,
        //            //41 QCE.NeckOD2 as [Neck OD 2],
        //            //42 QCE.NeckODRemark2,
        //            //43 QCE.NeckCollarGap2 as [Neck Collar Gap 2],
        //            //44 QCE.NeckCollarGapRemark2,
        //            //45 QCE.NeckHeight2 as [Neck Height 2],
        //            //46 QCE.NeckHeightRemark2,
        //            //47 QCE.ProductHeight2 as [Product Height 2],
        //            //48 QCE.ProductHeightRemark2,
        //            //49 QCE.Weight2 as [Weight 2],
        //            //50 QCE.WeightRemark2,
        //            //51 QCE.ProductVolume2 as [Product Volume 2],
        //            //52 QCE.ProductVolumeRemark2,
        //            //53 QCE.CapSealing2 as [Cap Sealing 2],
        //            //54 QCE.CapSealingRemark2,
        //            //55 QCE.WadSealing2 as [Wad Sealing 2],
        //            //56 QCE.WadSealingRemark2,
        //            //57 QCE.CheckerNote as [Checker Note],
        //            //58 QCE.Remark as [],
        //            //59 QCE.ErrorCount
        //            //60 QCE.ProductionEntryId

        //            //17 QCE.NeckSize1 as [Neck Size 1],
        //            //18 QCE.NeckSizeRemark1,
        //            //19 QCE.NeckID1 as [Neck ID 1],
        //            //20 QCE.NeckIDRemark1,
        //            //21 QCE.NeckOD1  as [Neck OD 1],
        //            //22 QCE.NeckODRemark1,
        //            //23 QCE.NeckCollarGap1 as [Neck Collar Gap 1],
        //            //24 QCE.NeckCollarGapRemark1,
        //            //25 QCE.NeckHeightI as [Neck Height I],
        //            //26 QCE.NeckHeightRemark1,
        //            //27 QCE.ProductHeightI as [Product Height I],
        //            //28 QCE.ProductHeightRemark1,
        //            //29 QCE.Weight1 as [Weight 1],
        //            //30 QCE.WeightRemark1,
        //            //31 QCE.ProductVolume1 as [Product Volume 1],
        //            //32 QCE.ProductVolumeRemark1,
        //            //33 QCE.CapSealing1 as [Cap Sealing 1],
        //            //34 QCE.CapSealingRemark1,
        //            //35 QCE.WadSealing1 as [Wad Sealing 1],
        //            //36 QCE.WadSealingRemark1,
        //            //37 QCE.NeckSize2 as [Neck Size 2],
        //            //38 QCE.NeckSizeRemarkII,
        //            //39 QCE.NeckID2 as [Neck ID 2],
        //            //40 QCE.NeckIDRemark2,
        //            //41 QCE.NeckOD2 as [Neck OD 2],
        //            //42 QCE.NeckODRemark2,
        //            //43 QCE.NeckCollarGap2 as [Neck Collar Gap 2],
        //            //44 QCE.NeckCollarGapRemark2,
        //            //45 QCE.NeckHeight2 as [Neck Height 2],
        //            //46 QCE.NeckHeightRemark2,
        //            //47 QCE.ProductHeight2 as [Product Height 2],
        //            //48 QCE.ProductHeightRemark2,
        //            //49 QCE.Weight2 as [Weight 2],
        //            //50 QCE.WeightRemark2,
        //            //51 QCE.ProductVolume2 as [Product Volume 2],
        //            //52 QCE.ProductVolumeRemark2,
        //            //53 QCE.CapSealing2 as [Cap Sealing 2],
        //            //54 QCE.CapSealingRemark2,
        //            //55 QCE.WadSealing2 as [Wad Sealing 2],
        //            //56 QCE.WadSealingRemark2,

        //            dataGridView1.DataSource = ds.Tables[0];
        //            dataGridView1.Columns[0].Visible = false;
        //            dataGridView1.Columns[3].Visible = false;
        //            dataGridView1.Columns[5].Visible = false;
        //            dataGridView1.Columns[6].Visible = false;
        //            dataGridView1.Columns[8].Visible = false;
        //            dataGridView1.Columns[7].Visible = false;
        //            dataGridView1.Columns[5].Visible = false;
        //            dataGridView1.Columns[10].Visible = false;
        //            dataGridView1.Columns[11].Visible = false;
        //            dataGridView1.Columns[13].Visible = false;
        //            dataGridView1.Columns[14].Visible = false;
        //            dataGridView1.Columns[15].Visible = false;
        //            dataGridView1.Columns[58].Visible = false;
        //            dataGridView1.Columns[59].Visible = false;
        //            dataGridView1.Columns[60].Visible = false;

        //            lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;

        //            //Visible False
        //            for (int k = 18; k <= 56; k = k + 2)
        //            {
        //                dataGridView1.Columns[k].Visible = false;
        //            }
        //            for (int i = 0; i < dataGridView1.Columns.Count; i++)
        //            {
        //                dataGridView1.Columns[i].Width = 130;
        //            }
        //            for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //            {
        //                for (int j = 18; j <= 56; j = j + 2)
        //                {
        //                    SetRedCellColour(dataGridView1, i, j, j - 1);
        //                }
        //            }
        //            dataGridView1.Columns[1].Width = 80;
        //            dataGridView1.Columns[2].Width = 70;
        //            dataGridView1.Columns[4].Width = 50;
        //            dataGridView1.Columns[9].Width = 100;
        //            dataGridView1.Columns[12].Width = 200;
        //            dataGridView1.Columns[16].Width = 110;
        //            btnReport.Visible = true;
        //        }
        //        else
        //        {
        //            objRL.ShowMessage(25, 4);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        objRL.ShowMessage(17, 4);
        //        return;
        //    }
        //}

        private void SetRedCellColour(DataGridView dgv, int i, int CheckCell, int SetCell)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[CheckCell].Value)))
            {
                if (dataGridView1.Rows[i].Cells[CheckCell].Value.ToString() == "1")
                    dataGridView1.Rows[i].Cells[SetCell].Style.BackColor = Color.Red;
            }
        }

        DataSet dsSend = new DataSet();
        private void Get_RDLC_Report()
        {
            ReportViewRDLC objForm = new ReportViewRDLC(dsSend, BusinessResources.Report_ProductWiseQualityControlReport);
            objForm.ShowDialog(this);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            objRL.ProductName = ProductName.ToString();
            objRL.ReportPeriod = "From- " + dtpFromDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY) + "-To- " + dtpToDate.Value.ToString(BusinessResources.DATEFORMATDDMMYYYY);
            Get_RDLC_Report();
            //GetReport();
        }

        protected void DrawBorder(Excel.Range Functionrange)
        {
            Excel.Borders borders = Functionrange.Borders;
            //borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders.Weight = 1D;
        }
        protected void Fill_Merge_Cell(string Cell1, string Cell2, object val1, Excel.Worksheet myExcelWorksheet, string SetValue)
        {
            //CellFlag = false;
            if (!CellFlag)
            {
                Cell1 = Cell1 + RowCount;
                Cell2 = Cell2 + RowCount;
            }

            Microsoft.Office.Interop.Excel.Range AlingRange1 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange1.Merge(val1);

            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            if (boldflag)
                AlingRange1.EntireRow.Font.Bold = true;


            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            Microsoft.Office.Interop.Excel.Range AlingRange2 = myExcelWorksheet.get_Range(Cell1, Cell2);

            if (AFlag == 0)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            if (AFlag == 1)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            if (AFlag == 2)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            if (BorderFlag)
                DrawBorder(AlingRange2);

            if (MH_Value)
            {
                AlingRange2.RowHeight = 60;
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                AlingRange2.WrapText = true;
            }

            if (AlignFlag)
                AlingRange1.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;

            if (ColourFlag)
                myExcelWorksheet.get_Range(Cell1, Cell2).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
        }

        public void GetReport()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                using (new CursorWait())
                {
                    BorderFlag = false;

                    object misValue = System.Reflection.Missing.Value;
                    myExcelApp = new Excel.Application();
                    myExcelWorkbooks = myExcelApp.Workbooks;

                    objRL.ClearExcelPath();
                    objRL.isPDF = true;

                    ReportName = "ProductWise-" + ProductId;

                    objRL.Form_ExcelFileName = "QCProductWiseReport.xlsx";
                    objRL.Form_ReportFileName = "QCProductWiseReport-" + ReportName;
                    objRL.Form_DestinationReportFilePath = "\\QC Product Wise Report\\" + ReportName + "\\";
                    objRL.Path_Comman();

                    myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                    Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                    objRL.FillCompanyData();

                    //1 Sr.No	
                    //2 Date	
                    //3 Shift	
                    //4 Plant Incharge and Volume Checker	
                    //5 Product Name	
                    //6 Neck Size
                    //7 Neck ID	
                    //8 Neck OD	
                    //9 Neck Collar Gap	
                    //10 Neck Height	
                    //11 Product Height	
                    //12 Weight	
                    //13 Product Volume	
                    //14 Cap Sealing	
                    //15 Wad Sealing

                    //0 QCE.ID,
                    //1 QCE.EntryDate as [Date],
                    //2 QCE.EntryTime as [Time],
                    //3 QCE.ShiftEntryId,
                    //4 SE.Shift,
                    //5 SE.PlantInchargeId,
                    //6 E.FullName as [Plant Incharge],
                    //7 SE.VolumeCheckerId,
                    //8 E1.FullName as [Volume Checker],
                    //9 QCE.MachinNo as [Machine No],
                    //10 QCE.ProductId,
                    //11 P.ProductType as [Product Type],
                    //12 P.ProductName as [Product Name],
                    //13 P.MouldId,
                    //14 MM.Cavity,
                    //15 QCE.PreformPartyId,
                    //16 PPM.PreformParty as [Preform Party],
                    //17 QCE.NeckSize1 as [Neck Size 1],
                    //18 QCE.NeckSizeRemark1,
                    //19 QCE.NeckID1 as [Neck ID 1],
                    //20 QCE.NeckIDRemark1,
                    //21 QCE.NeckOD1  as [Neck OD 1],
                    //22 QCE.NeckODRemark1,
                    //23 QCE.NeckCollarGap1 as [Neck Collar Gap 1],
                    //24 QCE.NeckCollarGapRemark1,
                    //25 QCE.NeckHeightI as [Neck Height I],
                    //26 QCE.NeckHeightRemark1,
                    //27 QCE.ProductHeightI as [Product Height I],
                    //28 QCE.ProductHeightRemark1,
                    //29 QCE.Weight1 as [Weight 1],
                    //30 QCE.WeightRemark1,
                    //31 QCE.ProductVolume1 as [Product Volume 1],
                    //32 QCE.ProductVolumeRemark1,
                    //33 QCE.CapSealing1 as [Cap Sealing 1],
                    //34 QCE.CapSealingRemark1,
                    //35 QCE.WadSealing1 as [Wad Sealing 1],
                    //36 QCE.WadSealingRemark1,
                    //37 QCE.NeckSize2 as [Neck Size 2],
                    //38 QCE.NeckSizeRemarkII,
                    //39 QCE.NeckID2 as [Neck ID 2],
                    //40 QCE.NeckIDRemark2,
                    //41 QCE.NeckOD2 as [Neck OD 2],
                    //42 QCE.NeckODRemark2,
                    //43 QCE.NeckCollarGap2 as [Neck Collar Gap 2],
                    //44 QCE.NeckCollarGapRemark2,
                    //45 QCE.NeckHeight2 as [Neck Height 2],
                    //46 QCE.NeckHeightRemark2,
                    //47 QCE.ProductHeight2 as [Product Height 2],
                    //48 QCE.ProductHeightRemark2,
                    //49 QCE.Weight2 as [Weight 2],
                    //50 QCE.WeightRemark2,
                    //51 QCE.ProductVolume2 as [Product Volume 2],
                    //52 QCE.ProductVolumeRemark2,
                    //53 QCE.CapSealing2 as [Cap Sealing 2],
                    //54 QCE.CapSealingRemark2,
                    //55 QCE.WadSealing2 as [Wad Sealing 2],
                    //56 QCE.WadSealingRemark2,
                    //57 QCE.CheckerNote as [Checker Note],
                    //58 QCE.Remark as [],
                    //59 QCE.ErrorCount
                    //60 QCE.ProductionEntryId

                    //17 QCE.NeckSize1 as [Neck Size 1],
                    //18 QCE.NeckSizeRemark1,
                    //19 QCE.NeckID1 as [Neck ID 1],
                    //20 QCE.NeckIDRemark1,
                    //21 QCE.NeckOD1  as [Neck OD 1],
                    //22 QCE.NeckODRemark1,
                    //23 QCE.NeckCollarGap1 as [Neck Collar Gap 1],
                    //24 QCE.NeckCollarGapRemark1,
                    //25 QCE.NeckHeightI as [Neck Height I],
                    //26 QCE.NeckHeightRemark1,
                    //27 QCE.ProductHeightI as [Product Height I],
                    //28 QCE.ProductHeightRemark1,
                    //29 QCE.Weight1 as [Weight 1],
                    //30 QCE.WeightRemark1,
                    //31 QCE.ProductVolume1 as [Product Volume 1],
                    //32 QCE.ProductVolumeRemark1,
                    //33 QCE.CapSealing1 as [Cap Sealing 1],
                    //34 QCE.CapSealingRemark1,
                    //35 QCE.WadSealing1 as [Wad Sealing 1],
                    //36 QCE.WadSealingRemark1,

                    //37 QCE.NeckSize2 as [Neck Size 2],
                    //38 QCE.NeckSizeRemark2,
                    //39 QCE.NeckID2 as [Neck ID 2],
                    //40 QCE.NeckIDRemark2,
                    //41 QCE.NeckOD2 as [Neck OD 2],
                    //42 QCE.NeckODRemark2,
                    //43 QCE.NeckCollarGap2 as [Neck Collar Gap 2],
                    //44 QCE.NeckCollarGapRemark2,
                    //45 QCE.NeckHeight2 as [Neck Height 2],
                    //46 QCE.NeckHeightRemark2,
                    //47 QCE.ProductHeight2 as [Product Height 2],
                    //48 QCE.ProductHeightRemark2,
                    //49 QCE.Weight2 as [Weight 2],
                    //50 QCE.WeightRemark2,
                    //51 QCE.ProductVolume2 as [Product Volume 2],
                    //52 QCE.ProductVolumeRemark2,
                    //53 QCE.CapSealing2 as [Cap Sealing 2],
                    //54 QCE.CapSealingRemark2,
                    //55 QCE.WadSealing2 as [Wad Sealing 2],
                    //56 QCE.WadSealingRemark2,

                    myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName.ToString();
                    myExcelWorksheet.get_Range("A2", misValue).Formula = objRL.Form_ReportFileName.ToString();
                    myExcelWorksheet.get_Range("A3", misValue).Formula = "Report Date- " + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat) + "-" + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat);
                    myExcelWorksheet.get_Range("A4", misValue).Formula = "Product Name" + ProductName.ToString();
                    myExcelWorksheet.get_Range("N3", misValue).Formula = "Date-" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATDDMMYYYY);
                    myExcelWorksheet.get_Range("N4", misValue).Formula = "Report by-" + BusinessLayer.UserName_Static.ToString();

                    RowCount = 6; SrNo = 1;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        ColourFlag = false;
                        BorderFlag = true;
                        //Sr.No
                        AFlag = 1;
                        Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SrNo.ToString());

                        //Date
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[1].Value)))
                        {
                            AFlag = 0;
                            BookingDate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value);
                            Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, BookingDate.ToString("dd/MM/yyyy"));
                        }

                        //Shift
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[4].Value)))
                        {
                            AFlag = 0;
                            Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[4].Value.ToString());
                        }

                        //Machine No
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[9].Value)))
                        {
                            AFlag = 1;
                            Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[9].Value.ToString());
                        }

                        ConcatNames = string.Empty;
                        //Plant Incharge and Volume Checker
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[61].Value)) && !string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[62].Value)))
                        {
                            ConcatNames = Convert.ToString(dataGridView1.Rows[i].Cells[61].Value) + "/" + Convert.ToString(dataGridView1.Rows[i].Cells[62].Value);
                            AFlag = 0;
                            Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, ConcatNames.ToString());
                        }

                        //Preform Party
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[16].Value)))
                        {
                            AFlag = 0;
                            Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[16].Value.ToString());
                        }

                        ////Product Name
                        //if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[12].Value)))
                        //{
                        //    AFlag = 0;
                        //    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[12].Value.ToString());
                        //}

                        //Neck Size
                        objectValue = misValue; myExcelWorksheet1 = myExcelWorksheet;
                        ArrayCount = 0;
                        // private void ExcelValuesColour(int i, int CellIndex, int DisplayIndex)

                        int CheckIndex = 18;
                        for (int j = 17; j <= 36; j = j + 2)
                        {
                            //ExcelValuesColour(i, 10, 9);
                            ExcelValuesColour(i, CheckIndex, j);
                            CheckIndex = CheckIndex + 2;
                        }

                        //If Cavity Required 2 then print again
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[14].Value)))
                        {
                            if (Convert.ToString(dataGridView1.Rows[i].Cells[14].Value.ToString()) == "2")
                            {
                                SrNo++;
                                RowCount++;

                                ColourFlag = false;
                                BorderFlag = true;
                                //Sr.No
                                AFlag = 1;
                                Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SrNo.ToString());

                                //Date
                                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[1].Value)))
                                {
                                    AFlag = 0;
                                    BookingDate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value);
                                    Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, BookingDate.ToString("dd/MM/yyyy"));
                                }

                                //Shift
                                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[4].Value)))
                                {
                                    AFlag = 0;
                                    Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[4].Value.ToString());
                                }

                                //Machine No
                                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[9].Value)))
                                {
                                    AFlag = 1;
                                    Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[9].Value.ToString());
                                }

                                ConcatNames = string.Empty;
                                //Plant Incharge and Volume Checker
                                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[61].Value)) && !string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[62].Value)))
                                {
                                    ConcatNames = Convert.ToString(dataGridView1.Rows[i].Cells[61].Value) + "/" + Convert.ToString(dataGridView1.Rows[i].Cells[62].Value);
                                    AFlag = 0;
                                    Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, ConcatNames.ToString());
                                }

                                //Preform Party
                                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[16].Value)))
                                {
                                    AFlag = 0;
                                    Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[16].Value.ToString());
                                }

                                ////Product Name
                                //if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[12].Value)))
                                //{
                                //    AFlag = 0;
                                //    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[12].Value.ToString());
                                //}
                                //Neck Size
                                objectValue = misValue; myExcelWorksheet1 = myExcelWorksheet;
                                ArrayCount = 0;
                                // private void ExcelValuesColour(int i, int CellIndex, int DisplayIndex)

                                CheckIndex = 38;
                                for (int j = 37; j <= 56; j = j + 2)
                                {
                                    //ExcelValuesColour(i, 10, 9);
                                    ExcelValuesColour(i, CheckIndex, j);
                                    CheckIndex = CheckIndex + 2;
                                }
                            }
                        }
                        SrNo++;
                        RowCount++;
                    }

                    myExcelWorkbook.Save();
                    PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");

                    try
                    {
                        const int xlQualityStandard = 0;
                        myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                        myExcelWorkbook.Close(true, misValue, misValue);
                        myExcelApp.Quit();

                        //objRL.ShowMessage(22, 1);

                        //DialogResult dr;
                        //dr = MessageBox.Show("Do you want to view this report", "Report View", MessageBoxButtons.YesNo);
                        //if (dr == DialogResult.Yes)
                        //    System.Diagnostics.Process.Start(objRL.RL_DestinationPath);

                        System.Diagnostics.Process.Start(PDFReport);
                        //objRL.DeleteExcelFile();

                        //if (!string.IsNullOrEmpty(objRL.EmailId) && cbEmail.Checked)
                        //{
                        //    objRL.EmailId_RL = objRL.EmailId;
                        //    objRL.Subject_RL = "Amount Collection Report";
                        //    //string body = "<div><p>Dear " + objRL.CustomerName_Customer + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
                        //    string body = "<div><p>Dear Sir,<p/><p>Please find attachment of pdf file.</p><p>Amount Collection Report on " + dtpFromDate.Value.ToString("dd/MM/yyyy") + " to " + dtpToDate.Value.ToString("dd/MM/yyyy") + " </p><p>Thanks,</p></div>";

                        //    objRL.Body_RL = body;// "Dear " + RedundancyLogics.SupplierName + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
                        //    objRL.FilePath_RL = PDFReport;
                        //    objRL.SendEMail();
                        //}

                        //if (cbEmail.Checked)
                        //{
                        //    objRL.EmailId_RL = "";
                        //    objRL.Subject_RL = "";
                        //    objRL.Body_RL = "";
                        //    objRL.FilePath_RL = PDFReport;
                        //    //objRL.SendEMail();
                        //}
                    }
                    catch (Exception ex1)
                    {
                        objRL.ShowMessage(27, 4);
                        return;
                    }
                }
            }
            else
            {

            }
        }
        string[] CellArray = { "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
        static int ArrayCount;

        object objectValue; Excel.Worksheet myExcelWorksheet1;
        private void ExcelValuesColour(int i, int CheckIndex, int DisplayIndex)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[CheckIndex].Value)) && !string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[DisplayIndex].Value)))
            {
                AFlag = 1;
                if (Convert.ToString(dataGridView1.Rows[i].Cells[CheckIndex].Value.ToString()) == "1")
                    ColourFlag = true;
                else
                    ColourFlag = false;

                Fill_Merge_Cell(CellArray[ArrayCount], CellArray[ArrayCount], objectValue, myExcelWorksheet1, dataGridView1.Rows[i].Cells[DisplayIndex].Value.ToString());
                ArrayCount++;
                ColourFlag = false;
            }
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

        private void lbItem_Click(object sender, EventArgs e)
        {
            Fill_Product_Information();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            dataGridView1.DataSource = null;
            cbToday.Checked = true;
            txtSearchProductName.Text = "";
            lblProductName.Text = "";
            lblProductType.Text = "";
            lblProductType.Text = "";
            ProductId = 0;
            ClearValues();
            btnReport.Visible = false;
            cbToday.Focus();
        }

        private void cbToday_CheckedChanged(object sender, EventArgs e)
        {
            if (cbToday.Checked == true)
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

        //private void lbItem_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    e.DrawBackground();
        //    Graphics g = e.Graphics;
        //    Brush brush = ((e.State & DrawItemState.Selected) == DrawItemState.Selected) ?
        //                  Brushes.Red : new SolidBrush(e.BackColor);
        //    g.FillRectangle(brush, e.Bounds);
        //    e.Graphics.DrawString(lbItem.Items[e.Index].ToString(), e.Font,
        //             new SolidBrush(e.ForeColor), e.Bounds, StringFormat.GenericDefault);
        //    e.DrawFocusRectangle();    

        //  //  if (e.Index < 0) return;
        //  ////  System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(BusinessResources.BACKGROUND_COLOUR);
        //  ////  e.BackColor = col; // System.Drawing.ColorTranslator.FromHtml(BusinessResources.BACKGROUND_COLOUR);

        //  //////  Color color = (Color)ColorConverter.ConvertFromString("#FF         
        //  ////  DFD991");
        //  //  //Color color = (Color)ColorConverter.ConvertFromString("#FFDFD991");

        //  //  //if the item state is selected them change the back color 
        //  //  if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
        //  //      e = new DrawItemEventArgs(e.Graphics,
        //  //                                e.Font,
        //  //                                e.Bounds,
        //  //                                e.Index,
        //  //                                e.State ^ DrawItemState.Selected,
        //  //                                e.ForeColor,
        //  //                                System.Drawing.ColorTranslator.FromHtml(BusinessResources.BACKGROUND_COLOUR));//Choose the color

        //  //  // Draw the background of the ListBox control for each item.
        //  //  e.DrawBackground();
        //  //  // Draw the current item text
        //  //  e.Graphics.DrawString(lbItem.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
        //  //  // If the ListBox has focus, draw a focus rectangle around the selected item.
        //  //  e.DrawFocusRectangle();
        //}
    }
}
