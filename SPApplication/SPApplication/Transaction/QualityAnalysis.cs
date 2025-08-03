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

namespace SPApplication.Transaction
{
    public partial class QualityAnalysis : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;

        bool BorderFlag = false; bool CellFlag = false;
        string PDFReport = string.Empty;
        int AFlag = 0; int SrNo = 1;
        bool AlignFlag = false; bool boldflag = false;
        int RowCount = 18;
        bool MH_Value = false;
        string ReportName = string.Empty;
        DateTime BookingDate, BookingTime;

        Excel.Application myExcelApp;
        Excel.Workbooks myExcelWorkbooks;
        Excel.Workbook myExcelWorkbook;

        public QualityAnalysis()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_QUALITY_CONTROL_REGISTER_ENTRY);
            objDL.SetButtonDesign(btnReport, BusinessResources.BTN_REPORT);
            objDL.SetPlusButtonDesign(btnAddShiftDetails);
            objDL.SetPlusButtonDesign(btnAddProductMaster);
            objDL.SetPlusButtonDesign(btnAddPreformParty);
            objRL.FillPreformParty(cmbPreformParty);
            lblBottleInformation.BackColor = Color.Cyan;
            lblJarInformation.BackColor = Color.Yellow;
            lblOCorrect.BackColor = objDL.GetBackgroundColorSuccess();
            lbl1Error.BackColor = objDL.GetForeColorError();
        }

        private void QualityAnalysis_Load(object sender, EventArgs e)
        {
            btnReport.Visible = false;
            ClearAll();
            objRL.Fill_Item_ListBox(lbItem, txtSearchProductName.Text, "All");
            FillGrid();
            cbToday.Checked = true;
            cmbMachineNo.Focus();
        }

        string ShiftDetails = string.Empty;
        int PlantInchargeId = 0, VolumeCheckerId = 0,ShiftEntryId=0;

        private void GetShiftDetails()
        {
            txtShiftDetails.Text = "";
            ShiftCode();
            DataSet ds = new DataSet();

            if(!GridFlag)
                objBL.Query = "select SE.ID,SE.EntryDate as [Date],SE.EntryTime as [Time],SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],SE.Naration from ((ShiftEntry SE inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) where SE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and SE.EntryDate=#" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and SE.Shift='"+Shift+"'";
            else
                objBL.Query = "select SE.ID,SE.EntryDate as [Date],SE.EntryTime as [Time],SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],SE.Naration from ((ShiftEntry SE inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) where SE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and SE.ID=" + ShiftEntryId + "";
           
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ShiftDetails = string.Empty;

                if ((!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"].ToString()))))
                    ShiftEntryId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());

                if ((!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Shift"].ToString()))))
                {
                    ShiftDetails = "Shift- " + ds.Tables[0].Rows[0]["Shift"].ToString() + ", ";
                    Shift = ds.Tables[0].Rows[0]["Shift"].ToString();
                }
                if ((!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Plant Incharge"].ToString()))))
                {
                    ShiftDetails += "Plant Incharge- " + ds.Tables[0].Rows[0]["Plant Incharge"].ToString() + ", ";
                    PlantInchargeId = Convert.ToInt32(ds.Tables[0].Rows[0]["PlantInchargeId"].ToString());
                }
                if ((!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Volume Checker"].ToString()))))
                {
                    ShiftDetails += "Volume Checker- " + ds.Tables[0].Rows[0]["Volume Checker"].ToString();
                    VolumeCheckerId = Convert.ToInt32(ds.Tables[0].Rows[0]["VolumeCheckerId"].ToString());
                }

                if ((!string.IsNullOrEmpty(Convert.ToString(ShiftDetails.ToString()))))
                {
                    txtShiftDetails.Text = ShiftDetails.ToString();
                    MachineDetailsVisibleTrue();
                }
            }
        }

        private void cmbMachineNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_Machine_Information();
        }

        private void Fill_Machine_Information()
        {
            if(cmbMachineNo.SelectedIndex >-1)
            {
                int MNo = Convert.ToInt32(cmbMachineNo.Text);

                if (MNo == 1 || MNo == 2 || MNo == 3 || MNo == 4 || MNo == 5 || MNo == 6 || MNo == 7 || MNo == 8 || MNo == 9)
                    lblMachineDetails.Text = "Manual";
                else
                    lblMachineDetails.Text = "Automatic";

                if (txtShiftDetails.Text != "")
                {
                    gbSearchProduct.Visible = true;
                    gbProductQC.Visible = false;
                }
            }
        }

        private void cmbProductName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_Product_Information();
        }
        int ProductId = 0; string ProductDetails = string.Empty;
        string Cavity = string.Empty;
        string ProductName = string.Empty;

        private void Fill_Product_Information()
        {
            Cavity = string.Empty;
            
            if(!GridFlag)
                ProductId = Convert.ToInt32(lbItem.SelectedValue);

            if (ProductId !=0)
            {
                ProductDetails = string.Empty;
                objRL.Get_Product_Records_By_Id(ProductId);
                ProductDetails = string.Empty;

                ProductDetails = "Mould No-\t" + objRL.SrNoMould.ToString() + "\t" + "Party-\t" + objRL.Party.ToString() + "\n" +
                                 "Cavity-\t\t" + objRL.Cavity.ToString() + "\t" + "Type-\t" + objRL.AutoSemi.ToString() + "\n" +
                                 "Preform Name-\t\t" + objRL.PreformName.ToString() + "\n" +
                                 "Nick Name-\t" + objRL.ProductNickName.ToString();

                lblProductName.Text = objRL.ProductName.ToString();
                ProductName =  objRL.ProductName.ToString();

                if(!string.IsNullOrEmpty(objRL.Cavity))
                {
                    Cavity = objRL.Cavity;
                    if (Cavity == "2")
                        gbProductII.Visible = true;
                    else
                        gbProductII.Visible = false;
                }

                if (!string.IsNullOrEmpty(objRL.ProductType))
                {
                    lblProductType.Text = objRL.ProductType.ToString();
                    if(objRL.ProductType =="Bottle")
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
                gbProductQC.Visible = true;

                txtPreformNeckSize.Text = objRL.PreformNeckSize.ToString();
                txtPreformWeight.Text = objRL.PreformWeight.ToString();
                txtPreformNeckID.Text = objRL.PreformNeckID.ToString();
                txtPreformNeckOD.Text = objRL.PreformNeckOD.ToString();
                txtPreformNeckCollarGap.Text = objRL.PreformNeckCollarGap.ToString();
                txtPreformNeckHeight.Text = objRL.PreformNeckHeight.ToString();

                //txtProductINeckSize.Text = objRL.ProductNeckSize.ToString();
                //txtProductIWeight.Text = objRL.ProductWeight.ToString();
                //txtProductINeckID.Text = objRL.ProductNeckID.ToString();
                //txtProductINeckOD.Text = objRL.ProductNeckOD.ToString();
                //txtProductINeckCollarGap.Text = objRL.ProductNeckCollarGap.ToString();
                //txtProductINeckHeight.Text = objRL.ProductNeckHeight.ToString();
                //txtProductIHeight.Text = objRL.ProductHeight.ToString();
                //txtProductIVolume.Text = objRL.ProductVolume.ToString();

                txtProductNeckSize.Text = objRL.ProductNeckSize.ToString();
              //  txtProductNeckSizeRatio.Text = objRL.ProductNeckSizeRatio.ToString();
                txtNeckSizeMinValue.Text = objRL.ProductNeckSizeMinValue.ToString();
                txtNeckSizeMaxValue.Text = objRL.ProductNeckSizeMaxValue.ToString();

                txtProductWeight.Text = objRL.ProductWeight.ToString();
              //  txtProductWeightRatio.Text = objRL.ProductWeightRatio.ToString();
                txtWeightMinValue.Text = objRL.ProductWeightMinValue.ToString();
                txtWeightMaxValue.Text = objRL.ProductWeightMaxValue.ToString();

                txtProductNeckID.Text = objRL.ProductNeckID.ToString();
             //   txtProductNeckIDRatio.Text = objRL.ProductNeckIDRatio.ToString();
                txtNeckIDMinValue.Text = objRL.ProductNeckIDMinValue.ToString();
                txtNeckIDMaxValue.Text = objRL.ProductNeckIDMaxValue.ToString();

                txtProductNeckOD.Text = objRL.ProductNeckOD.ToString();
              //  txtProductNeckODRatio.Text = objRL.ProductNeckODRatio.ToString();
                txtNeckODMinValue.Text = objRL.ProductNeckODMinValue.ToString();
                txtNeckODMaxValue.Text = objRL.ProductNeckODMaxValue.ToString();

                txtProductNeckCollarGap.Text = objRL.ProductNeckCollarGap.ToString();
               // txtProductNeckCollarGapRatio.Text = objRL.ProductNeckCollarGapRatio.ToString();
                txtNeckCollarGapMinValue.Text = objRL.ProductNeckCollarGapMinValue.ToString();
                txtNeckCollarGapMaxValue.Text = objRL.ProductNeckCollarGapMaxValue.ToString();

                txtProductNeckHeight.Text = objRL.ProductNeckHeight.ToString();
               // txtProductNeckHeightRatio.Text = objRL.ProductNeckHeightRatio.ToString();
                txtNeckHeightMinValue.Text = objRL.ProductNeckHeightMinValue.ToString();
                txtNeckHeightMaxValue.Text = objRL.ProductNeckHeightMaxValue.ToString();

                txtProductHeight.Text = objRL.ProductHeight.ToString();
              //  txtProductHeightRatio.Text = objRL.ProductHeightRatio.ToString();
                txtProductHeightMinValue.Text = objRL.ProductHeightMinValue.ToString();
                txtProductHeightMaxValue.Text = objRL.ProductHeightMaxValue.ToString();

                txtProductVolume.Text = objRL.ProductVolume.ToString();
              //  txtProductVolumeRatio.Text = objRL.ProductVolumeRatio.ToString();
                txtProductVolumeMinValue.Text = objRL.ProductVolumeMinValue.ToString();
                txtProductVolumeMaxValue.Text = objRL.ProductVolumeMaxValue.ToString();

                txtNeckSize1.Focus();
            }
        }
        
        public void CalculateValue(int CheckValue)
        {
            switch (CheckValue)
            {
                case 1:
                    if(!FlagProduct)
                        SetRemark(txtNeckSize1.Text, txtNeckSizeMinValue.Text, txtNeckSizeMaxValue.Text, txtNeckSizeRemark1);
                    else
                        SetRemark(txtNeckSize2.Text, txtNeckSizeMinValue.Text, txtNeckSizeMaxValue.Text, txtNeckSizeRemark2);
                    break;
                case 2:
                    if (!FlagProduct)
                        SetRemark(txtWeight1.Text, txtWeightMinValue.Text, txtWeightMaxValue.Text, txtWeightRemark1);
                    else
                        SetRemark(txtWeight2.Text, txtWeightMinValue.Text, txtWeightMaxValue.Text, txtWeightRemark2);
                    break;
                case 3:
                    if (!FlagProduct)
                        SetRemark(txtNeckID1.Text, txtNeckIDMinValue.Text, txtNeckIDMaxValue.Text, txtNeckIDRemark1);
                    else
                        SetRemark(txtNeckID2.Text, txtNeckIDMinValue.Text, txtNeckIDMaxValue.Text, txtNeckIDRemark2);
                    break;
                case 4:
                    if (!FlagProduct)
                        SetRemark(txtNeckOD1.Text, txtNeckODMinValue.Text, txtNeckODMaxValue.Text, txtNeckODRemark1);
                    else
                        SetRemark(txtNeckOD2.Text, txtNeckODMinValue.Text, txtNeckODMaxValue.Text, txtNeckODRemark2);
                    break;
                case 5:
                    if (!FlagProduct)
                        SetRemark(txtNeckCollarGap1.Text, txtNeckCollarGapMinValue.Text, txtNeckCollarGapMaxValue.Text, txtNeckCollarGapRemark1);
                    else
                        SetRemark(txtNeckCollarGap2.Text, txtNeckCollarGapMinValue.Text, txtNeckCollarGapMaxValue.Text, txtNeckCollarGapRemark2);
                    break;
                case 6:
                    if (!FlagProduct)
                        SetRemark(txtNeckHeight1.Text, txtNeckHeightMinValue.Text, txtNeckHeightMaxValue.Text, txtNeckHeightRemark1);
                    else
                        SetRemark(txtNeckHeight2.Text, txtNeckHeightMinValue.Text, txtNeckHeightMaxValue.Text, txtNeckHeightRemark2);
                    break;
                case 7:
                    if (!FlagProduct)
                        SetRemark(txtProductHeight1.Text, txtProductHeightMinValue.Text, txtProductHeightMaxValue.Text, txtProductHeightRemark1);
                    else
                        SetRemark(txtProductHeight2.Text, txtProductHeightMinValue.Text, txtProductHeightMaxValue.Text, txtProductHeightRemark2);
                    break;
                case 8:
                    if (!FlagProduct)
                        SetRemark(txtProductVolume1.Text, txtProductVolumeMinValue.Text, txtProductVolumeMaxValue.Text, txtProductVolumeRemark1);
                    else
                        SetRemark(txtProductVolume2.Text, txtProductVolumeMinValue.Text, txtProductVolumeMaxValue.Text, txtProductVolumeRemark2);
                    break;
                case 9:
                    if (!FlagProduct)
                        SetCapWadRemark(cmbCapSealing1, txtCapSealingRemark1);
                    else
                        SetCapWadRemark(cmbCapSealing2, txtCapSealingRemark2);
                    break;
                case 10:
                    if (!FlagProduct)
                        SetCapWadRemark(cmbWadSealing1, txtWadSealingRemark1);
                    else
                        SetCapWadRemark(cmbWadSealing2, txtWadSealingRemark2);
                    break;
            }
        }

        private void SetCapWadRemark(ComboBox ComboBox_F,System.Windows.Forms.TextBox Remark_F)
        {
            if (ComboBox_F.SelectedIndex > -1)
            {
                if (ComboBox_F.Text == "Ok")
                {
                    Remark_F.BackColor = objDL.GetBackgroundColorSuccess();
                    Remark_F.Text = "0";
                }
                else if (ComboBox_F.Text == "Not Ok")
                {
                    Remark_F.BackColor = objDL.GetForeColorError();
                    Remark_F.Text = "1";
                }
                else if (ComboBox_F.Text == "No Wad")
                {
                    Remark_F.BackColor = objDL.GetBackgroundBlank(); 
                    Remark_F.Text = "0";
                }
                else
                {
                    Remark_F.BackColor = objDL.GetBackgroundBlank();
                    Remark_F.Text = "";
                }
            }
        }

        double checkerValue = 0, MinValue = 0, MaxValue = 0;
        bool FlagProduct = false;

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnReport.Visible = true;
            ClearAll();
            cbToday.Checked = true;
            FillGrid();
            cmbMachineNo.Focus();
        }

        private void SetRemark(string CheckerValue_F, string MinValue_F, string MaxValue_F, System.Windows.Forms.TextBox Remark_F)
        {
            checkerValue = 0; MinValue = 0; MaxValue = 0;

            double.TryParse(CheckerValue_F, out checkerValue);
            double.TryParse(MinValue_F , out MinValue);
            double.TryParse(MaxValue_F , out MaxValue);

            //0-NA
            //1-Correct
            //2-Wrong

            Remark_F.Text = "";
            Remark_F.BackColor = objDL.GetBackgroundBlank();

            if (MinValue > 0 && MaxValue > 0)
            {
                if (checkerValue != 0)
                {
                    //if (Enumerable.Range(MinValue, MaxValue).Contains(checkerValue))
                    if (MinValue <= checkerValue && MaxValue >= checkerValue)
                    {
                        Remark_F.BackColor = objDL.GetBackgroundColorSuccess();
                        Remark_F.Text = "0";
                    }
                    else
                    {
                        Remark_F.BackColor = objDL.GetForeColorError();
                        Remark_F.Text = "1";
                    }
                }
            }
        }

        private void NeckSize1_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = false;
            CalculateValue(1);
        }
        private void txtWeight1_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = false;
            CalculateValue(2);
        }

        private void txtWeight2_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = true;
            CalculateValue(2);
        }
        private void txtNeckID1_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = false;
            CalculateValue(3);
        }
        private void txtNeckID2_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = true;
            CalculateValue(3);
        }
        private void txtNeckOD1_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = false;
            CalculateValue(4);
        }
        private void txtNeckOD2_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = true;
            CalculateValue(4);
        }
        private void txtNeckCollarGap1_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = false;
            CalculateValue(5);
        }

        private void txtNeckCollarGap2_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = true;
            CalculateValue(5);
        }

        private void txtNeckHeight1_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = false;
            CalculateValue(6);
        }

        private void txtNeckHeight2_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = true;
            CalculateValue(6);
        }

        private void txtProductHeight1_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = false;
            CalculateValue(7);
        }

        private void txtProductHeight2_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = true;
            CalculateValue(7);
        }

        private void txtProductVolume1_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = false;
            CalculateValue(8);
        }

        private void ProductVolume2_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = true;
            CalculateValue(8);
        }

        private void cmbCapSealing1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FlagProduct = false;
            CalculateValue(9);
        }

        private void cmbCapSealing2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FlagProduct = true;
            CalculateValue(9);
        }

        private void cmbWadSealing1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FlagProduct = false;
            CalculateValue(10);
        }

        private void cmbWadSealing2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FlagProduct = true;
            CalculateValue(10);
        }

        private void txtProductNeckSizeMinValue_TextChanged(object sender, EventArgs e)
        {

        }

        int ErrorCount = 0;
        string Remark = string.Empty;

        private void CheckErrorCount(System.Windows.Forms.TextBox txtCheck)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(txtCheck.Text)))
            {
                if (txtCheck.Text == "1")
                    ErrorCount = ErrorCount + 1;
                
                if (ErrorCount >= 1)
                        Remark = "1";
                    else
                        Remark = "0";
            }
        }
        
        private void SetRemarkMain()
        {
            ErrorCount = 0;
            CheckErrorCount(txtNeckSizeRemark1);
            CheckErrorCount(txtWeightRemark1);
            CheckErrorCount(txtNeckIDRemark1);
            CheckErrorCount(txtNeckODRemark1);
            CheckErrorCount(txtNeckCollarGapRemark1);
            CheckErrorCount(txtNeckHeightRemark1);
            CheckErrorCount(txtProductHeightRemark1);
            CheckErrorCount(txtProductVolumeRemark1);
            CheckErrorCount(txtCapSealingRemark1);
            CheckErrorCount(txtWadSealingRemark1);
             
            if (Cavity == "2")
            {
                CheckErrorCount(txtNeckSizeRemark2);
                CheckErrorCount(txtWeightRemark2);
                CheckErrorCount(txtNeckIDRemark2);
                CheckErrorCount(txtNeckODRemark2);
                CheckErrorCount(txtNeckCollarGapRemark2);
                CheckErrorCount(txtNeckHeightRemark2);
                CheckErrorCount(txtProductHeightRemark2);
                CheckErrorCount(txtProductVolumeRemark2);
                CheckErrorCount(txtCapSealingRemark1);
                CheckErrorCount(txtWadSealingRemark1);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
        }

        protected void SaveDB()
        {
            if (!Validation())
            {
                SetRemarkMain();
                if (TableID != 0)
                    if (FlagDelete)
                        objBL.Query = "update QCEntry set CancelTag=1 where ID=" + TableID + "";
                    else
                        objBL.Query = "update QCEntry set EntryDate='" + dtpDate.Value.ToShortDateString() + "',EntryTime='" + dtpTime.Value.ToShortTimeString() + "',ShiftEntryId=" + ShiftEntryId + ",MachinNo='" + cmbMachineNo.Text + "',ProductId=" + ProductId + ",PreformPartyId=" + cmbPreformParty.SelectedValue + ",NeckSize1='" + txtNeckSize1.Text + "',NeckSizeRemark1='" + txtNeckSizeRemark1.Text + "',NeckID1='" + txtNeckID1.Text + "',NeckIDRemark1='" + txtNeckIDRemark1.Text + "',NeckOD1='" + txtNeckOD1.Text + "',NeckODRemark1='" + txtNeckODRemark1.Text + "',NeckCollarGap1='" + txtNeckCollarGap1.Text + "',NeckCollarGapRemark1='" + txtNeckCollarGapRemark1.Text + "',NeckHeight1='" + txtNeckHeight1.Text + "',NeckHeightRemark1='" + txtNeckHeightRemark1.Text + "',ProductHeight1='" + txtProductHeight1.Text + "',ProductHeightRemark1='" + txtProductHeightRemark1.Text + "',Weight1='" + txtWeight1.Text + "',WeightRemark1='" + txtWeightRemark1.Text + "',ProductVolume1='" + txtProductVolume1.Text + "',ProductVolumeRemark1='" + txtProductVolumeRemark1.Text + "',CapSealing1='" + cmbCapSealing1.Text + "',CapSealingRemark1='" + txtCapSealingRemark1.Text + "',WadSealing1='" + cmbWadSealing1.Text + "',WadSealingRemark1='" + txtWadSealingRemark1.Text + "',NeckSize2='" + txtNeckSize2.Text + "',NeckSizeRemark2='" + txtNeckSizeRemark2.Text + "',NeckID2='" + txtNeckID2.Text + "',NeckIDRemark2='" + txtNeckIDRemark2.Text + "',NeckOD2='" + txtNeckOD2.Text + "',NeckODRemark2='" + txtNeckODRemark2.Text + "',NeckCollarGap2='" + txtNeckCollarGap2.Text + "',NeckCollarGapRemark2='" + txtNeckCollarGapRemark2.Text + "',NeckHeight2='" + txtNeckHeight2.Text + "',NeckHeightRemark2='" + txtNeckHeightRemark2.Text + "',ProductHeight2='" + txtProductHeight2.Text + "',ProductHeightRemark2='" + txtProductHeightRemark2.Text + "',Weight2='" + txtWeight2.Text + "',WeightRemark2='" + txtWeightRemark2.Text + "',ProductVolume2='" + txtProductVolume2.Text + "',ProductVolumeRemark2='" + txtProductVolumeRemark2.Text + "',CapSealing2='" + cmbCapSealing2.Text + "',CapSealingRemark2='" + txtCapSealingRemark2.Text + "',WadSealing2='" + cmbWadSealing2.Text + "',WadSealingRemark2='" + txtWadSealingRemark2.Text + "',CheckerNote='" + txtCheckerNote.Text + "',Remark='" + Remark + "',ErrorCount=" + ErrorCount + ",ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                else
                    objBL.Query = "insert into QCEntry(EntryDate,EntryTime,ShiftEntryId,MachinNo,ProductId,PreformPartyId,NeckSize1,NeckSizeRemark1,NeckID1,NeckIDRemark1,NeckOD1,NeckODRemark1,NeckCollarGap1,NeckCollarGapRemark1,NeckHeight1,NeckHeightRemark1,ProductHeight1,ProductHeightRemark1,Weight1,WeightRemark1,ProductVolume1,ProductVolumeRemark1,CapSealing1,CapSealingRemark1,WadSealing1,WadSealingRemark1,NeckSize2,NeckSizeRemark2,NeckID2,NeckIDRemark2,NeckOD2,NeckODRemark2,NeckCollarGap2,NeckCollarGapRemark2,NeckHeight2,NeckHeightRemark2,ProductHeight2,ProductHeightRemark2,Weight2,WeightRemark2,ProductVolume2,ProductVolumeRemark2,CapSealing2,CapSealingRemark2,WadSealing2,WadSealingRemark2,CheckerNote,Remark,ErrorCount,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "'," + ShiftEntryId + ",'" + cmbMachineNo.Text + "'," + ProductId + "," + cmbPreformParty.SelectedValue + ",'" + txtNeckSize1.Text + "','" + txtNeckSizeRemark1.Text + "','" + txtNeckID1.Text + "','" + txtNeckIDRemark1.Text + "','" + txtNeckOD1.Text + "','" + txtNeckODRemark1.Text + "','" + txtNeckCollarGap1.Text + "','" + txtNeckCollarGapRemark1.Text + "','" + txtNeckHeight1.Text + "','" + txtNeckHeightRemark1.Text + "','" + txtProductHeight1.Text + "','" + txtProductHeightRemark1.Text + "','" + txtWeight1.Text + "','" + txtWeightRemark1.Text + "','" + txtProductVolume1.Text + "','" + txtProductVolumeRemark1.Text + "','" + cmbCapSealing1.Text + "','" + txtCapSealingRemark1.Text + "','" + cmbWadSealing1.Text + "','" + txtWadSealingRemark1.Text + "','" + txtNeckSize2.Text + "','" + txtNeckSizeRemark2.Text + "','" + txtNeckID2.Text + "','" + txtNeckIDRemark2.Text + "','" + txtNeckOD2.Text + "','" + txtNeckODRemark2.Text + "','" + txtNeckCollarGap2.Text + "','" + txtNeckCollarGapRemark2.Text + "','" + txtNeckHeight2.Text + "','" + txtNeckHeightRemark2.Text + "','" + txtProductHeight2.Text + "','" + txtProductHeightRemark2.Text + "','" + txtWeight2.Text + "','" + txtWeightRemark2.Text + "','" + txtProductVolume2.Text + "','" + txtProductVolumeRemark2.Text + "','" + cmbCapSealing2.Text + "','" + txtCapSealingRemark2.Text + "','" + cmbWadSealing2.Text + "','" + txtWadSealingRemark2.Text + "','" + txtCheckerNote.Text + "','" + Remark + "'," + ErrorCount + "," + BusinessLayer.UserId_Static + ")";

                if (objBL.Function_ExecuteNonQuery() > 0)
                {
                    if (!FlagDelete)
                        objRL.ShowMessage(7, 1);
                    else
                        objRL.ShowMessage(9, 1);

                    FillGrid();
                    ClearAll();
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        protected bool Validation()
        {
            bool ReturnFlag = false;

            if (txtID.Text == "")
            {
                objEP.SetError(txtID, "Enter QC ID");
                txtID.Focus();
                ReturnFlag = true;
            }
            else if (Shift == "")
            {
                objEP.SetError(txtShiftDetails, "Enter Shift");
                txtShiftDetails.Focus();
                ReturnFlag = true;
            }
            else if (cmbMachineNo.SelectedIndex == -1)
            {
                objEP.SetError(cmbMachineNo, "Select Machine No");
                cmbMachineNo.Focus();
                ReturnFlag = true;
            }
            else if (PlantInchargeId == 0)
            {
                objEP.SetError(txtShiftDetails, "Enter Shift");
                txtShiftDetails.Focus();
                ReturnFlag = true;
            }
            else if (VolumeCheckerId == 0)
            {
                objEP.SetError(txtShiftDetails, "Enter Shift");
                txtShiftDetails.Focus();
                ReturnFlag = true;
            }
            else if (ProductId == 0)
            {
                objEP.SetError(lbItem, "Select Product");
                lbItem.Focus();
                ReturnFlag = true;
            }
            else if (lblProductName.Text == "")
            {
                objEP.SetError(lblProductName, "Enter Product Name");
                lblProductName.Focus();
                ReturnFlag = true;
            }
            else if (cmbPreformParty.SelectedIndex == -1)
            {
                objEP.SetError(cmbPreformParty, "Selec Preform Party");
                cmbPreformParty.Focus();
                ReturnFlag = true;
            }
            else
                ReturnFlag = false;

            if (!ReturnFlag)
            {
                if (txtNeckSize1.Text == "" && txtNeckSizeRemark1.Text == "")
                {
                    objEP.SetError(txtNeckSize1, "Enter Product I Neck Size");
                    txtNeckSize1.Focus();
                    ReturnFlag = true;
                }
                else if (txtWeight1.Text == "" && txtWeightRemark1.Text == "")
                {
                    objEP.SetError(txtWeight1, "Enter Product I Weight");
                    txtNeckSize1.Focus();
                    ReturnFlag = true;
                }
                else if (txtNeckID1.Text == "" && txtNeckIDRemark1.Text == "")
                {
                    objEP.SetError(txtNeckSize1, "Enter Product I Neck ID");
                    txtNeckID1.Focus();
                    ReturnFlag = true;
                }
                else if (txtNeckOD1.Text == "" && txtNeckODRemark1.Text == "")
                {
                    objEP.SetError(txtNeckOD1, "Enter Product I Neck OD");
                    txtNeckOD1.Focus();
                    ReturnFlag = true;
                }
                else if (txtNeckHeight1.Text == "" && txtNeckHeightRemark1.Text == "")
                {
                    objEP.SetError(txtNeckHeight1, "Enter Product I Neck Height");
                    txtNeckHeight1.Focus();
                    ReturnFlag = true;
                }
                else if (txtProductHeight1.Text == "" && txtProductHeightRemark1.Text == "")
                {
                    objEP.SetError(txtProductHeight1, "Enter Product I Height");
                    txtProductHeight1.Focus();
                    ReturnFlag = true;
                }
                else if (txtProductVolume1.Text == "" && txtProductHeightRemark1.Text == "")
                {
                    objEP.SetError(txtProductVolume1, "Enter Product I Volume");
                    txtProductVolume1.Focus();
                    ReturnFlag = true;
                }
                else if (cmbCapSealing1.SelectedIndex == -1 && txtCapSealingRemark1.Text == "")
                {
                    objEP.SetError(cmbCapSealing1, "Select Cap Sealing I");
                    cmbCapSealing1.Focus();
                    ReturnFlag = true;
                }
                else if (cmbWadSealing1.SelectedIndex == -1)
                {
                    objEP.SetError(cmbWadSealing1, "Select Wad Sealing I");
                    cmbWadSealing1.Focus();
                    ReturnFlag = true;
                }
                else
                    ReturnFlag = false;

                if (!ReturnFlag)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(txtPreformNeckCollarGap.Text)))
                    {
                        if (txtPreformNeckCollarGap.Text != "0")
                        {
                            if (txtNeckCollarGap1.Text == "" && txtProductVolumeRemark1.Text == "")
                            {
                                objEP.SetError(txtNeckCollarGap1, "Enter Product I Neck Size");
                                txtNeckCollarGap1.Focus();
                                ReturnFlag = true;
                            }
                            else
                                ReturnFlag = false;
                        }
                    }
                }
            }

            if (Cavity == "2")
            {
                if (!ReturnFlag)
                {
                    if (txtNeckSize2.Text == "" && txtNeckSizeRemark2.Text == "")
                    {
                        objEP.SetError(txtNeckSize2, "Enter Product II Neck Size");
                        txtNeckSize2.Focus();
                        ReturnFlag = true;
                    }
                    else if (txtWeight2.Text == "" && txtWeightRemark2.Text == "")
                    {
                        objEP.SetError(txtWeight2, "Enter Product II Weight");
                        txtNeckSize2.Focus();
                        ReturnFlag = true;
                    }
                    else if (txtNeckID2.Text == "" && txtNeckIDRemark2.Text == "")
                    {
                        objEP.SetError(txtNeckSize2, "Enter Product II Neck ID");
                        txtNeckID2.Focus();
                        ReturnFlag = true;
                    }
                    else if (txtNeckOD2.Text == "" && txtNeckODRemark2.Text == "")
                    {
                        objEP.SetError(txtNeckOD2, "Enter Product II Neck OD");
                        txtNeckOD2.Focus();
                        ReturnFlag = true;
                    }
                    else if (txtNeckHeight2.Text == "" && txtNeckHeightRemark2.Text == "")
                    {
                        objEP.SetError(txtNeckHeight2, "Enter Product II Neck Height");
                        txtNeckHeight2.Focus();
                        ReturnFlag = true;
                    }
                    else if (txtProductHeight2.Text == "" && txtProductHeightRemark2.Text == "")
                    {
                        objEP.SetError(txtProductHeight2, "Enter Product II Height");
                        txtProductHeight2.Focus();
                        ReturnFlag = true;
                    }
                    else if (txtProductVolume2.Text == "" && txtProductHeightRemark2.Text == "")
                    {
                        objEP.SetError(txtProductVolume2, "Enter Product II Volume");
                        txtProductVolume2.Focus();
                        ReturnFlag = true;
                    }
                    else if (cmbCapSealing2.SelectedIndex == -1 && txtCapSealingRemark2.Text == "")
                    {
                        objEP.SetError(cmbCapSealing2, "Select Cap Sealing II");
                        cmbCapSealing1.Focus();
                        ReturnFlag = true;
                    }
                    else if (cmbWadSealing2.SelectedIndex == -1)
                    {
                        objEP.SetError(cmbWadSealing2, "Select Wad Sealing II");
                        cmbWadSealing1.Focus();
                        ReturnFlag = true;
                    }
                    else
                        ReturnFlag = false;

                    if (!ReturnFlag)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(txtPreformNeckCollarGap.Text)))
                        {
                            if (txtPreformNeckCollarGap.Text != "0")
                            {
                                if (txtNeckCollarGap2.Text == "" && txtProductVolumeRemark2.Text == "")
                                {
                                    objEP.SetError(txtNeckCollarGap2, "Enter Product II Neck Size");
                                    txtNeckCollarGap2.Focus();
                                    ReturnFlag = true;
                                }
                                else
                                    ReturnFlag = false;
                            }
                        }
                    }
                }
            }
            return ReturnFlag;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = objRL.Delete_Record_Show_Message(); // MessageBox.Show("Do yo want to delete this record?", "Delete Record", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                FlagDelete = true;
                SaveDB();
            }
        }

        private void cbToday_CheckedChanged(object sender, EventArgs e)
        {
            DateFlag = true;
            if(cbToday.Checked)
            {
                dtpSearchDate.Enabled = false;
                dtpSearchDate.Value = DateTime.Now.Date;
            }
            else
            {
                dtpSearchDate.Enabled = true;
                dtpSearchDate.Value = DateTime.Now.Date;
            }
        }

        private void btnAddProductMaster_Click(object sender, EventArgs e)
        {
            Product objForm = new Product();
            objForm.ShowDialog(this);
            objRL.Fill_Item_ListBox(lbItem, txtSearchProductName.Text, "All");
        }

        private void btnAddSupervisor_Click(object sender, EventArgs e)
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
                //txtQty.Focus();
            }
        }

        private void lbItem_Click(object sender, EventArgs e)
        {
            Fill_Product_Information();
        }

        private void txtNeckSize2_TextChanged(object sender, EventArgs e)
        {
            FlagProduct = true;
            CalculateValue(1);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected void ClearAll()
        {
            TableID = 0;
            objEP.Clear();

            gbProductQC.Visible = false;
            FlagDelete = false;
            btnDelete.Enabled = false;
            txtShiftDetails.Text = "";
            txtID.Text = "";
            dtpDate.Value = DateTime.Now.Date.Date;
            dtpTime.Value = DateTime.Now;
            cbToday.Checked = true;

            cmbMachineNo.SelectedIndex = -1;
            lblMachineDetails.Text = "";
            Shift = "";
            PlantInchargeId = 0;
            VolumeCheckerId = 0;
            lblProductType.Text = "";
            lblProductType.BackColor = Color.White;

            lblProductName.Text = "";
            lblProductName.BackColor = Color.White;

            txtNeckSize1.Text = "";
            txtNeckSizeRemark1.Text = "";
            txtNeckSize2.Text = "";
            txtNeckSizeRemark2.Text = "";
            txtPreformNeckSize.Text = "";
            txtProductNeckSize.Text = "";
           // txtProductNeckSizeRatio.Text = "";
            txtNeckSizeMinValue.Text = "";
            txtNeckSizeMaxValue.Text = "";

            txtWeight1.Text = "";
            txtWeightRemark1.Text = "";
            txtWeight2.Text = "";
            txtWeightRemark2.Text = "";
            txtPreformWeight.Text = "";
            txtProductWeight.Text = "";
           // txtProductWeightRatio.Text = "";
            txtWeightMinValue.Text = "";
            txtWeightMaxValue.Text = "";

            txtNeckID1.Text = "";
            txtNeckIDRemark1.Text = "";
            txtNeckID2.Text = "";
            txtNeckIDRemark2.Text = "";
            txtPreformNeckID.Text = "";
            txtProductNeckID.Text = "";
            //txtProductNeckIDRatio.Text = "";
            txtNeckIDMinValue.Text = "";
            txtNeckIDMaxValue.Text = "";

            txtNeckOD1.Text = "";
            txtNeckODRemark1.Text = "";
            txtNeckOD2.Text = "";
            txtNeckODRemark2.Text = "";
            txtPreformNeckOD.Text = "";
            txtProductNeckOD.Text = "";
            //txtProductNeckODRatio.Text = "";
            txtNeckODMinValue.Text = "";
            txtNeckODMaxValue.Text = "";

            txtNeckCollarGap1.Text = "";
            txtNeckCollarGapRemark1.Text = "";
            txtNeckCollarGap2.Text = "";
            txtNeckCollarGapRemark2.Text = "";
            txtPreformNeckCollarGap.Text = "";
            txtProductNeckCollarGap.Text = "";
           // txtProductNeckCollarGapRatio.Text = "";
            txtNeckCollarGapMinValue.Text = "";
            txtNeckCollarGapMaxValue.Text = "";

            txtNeckHeight1.Text = "";
            txtNeckHeightRemark1.Text = "";
            txtNeckHeight2.Text = "";
            txtNeckHeightRemark2.Text = "";
            txtPreformNeckHeight.Text = "";
            txtProductNeckHeight.Text = "";
            //txtProductNeckHeightRatio.Text = "";
            txtNeckHeightMinValue.Text = "";
            txtNeckHeightMaxValue.Text = "";

            txtProductHeight1.Text = "";
            txtProductHeightRemark1.Text = "";
            txtProductHeight2.Text = "";
            txtProductHeightRemark2.Text = "";
            txtProductHeight.Text = "";
            //txtProductHeightRatio.Text = "";
            txtProductHeightMinValue.Text = "";
            txtProductHeightMaxValue.Text = "";

            txtProductVolume1.Text = "";
            txtProductVolumeRemark1.Text = "";
            txtProductVolume2.Text = "";
            txtProductVolumeRemark2.Text = "";
            txtProductVolume.Text = "";
            //txtProductVolumeRatio.Text = "";
            txtProductVolumeMinValue.Text = "";
            txtProductVolumeMaxValue.Text = "";

            cmbCapSealing1.SelectedIndex = -1;
            txtCapSealingRemark1.Text = "";
            cmbCapSealing2.SelectedIndex = -1;
            txtCapSealingRemark2.Text = "";

            cmbWadSealing1.SelectedIndex = -1;
            txtWadSealingRemark1.Text = "";
            cmbWadSealing2.SelectedIndex = -1;
            txtWadSealingRemark2.Text = "";
            txtCheckerNote.Text = "";
            MachineDetailsVisibleFalse();
            GetID();
            GetShiftDetails();
            dtpDate.Focus();
        }

        private void MachineDetailsVisibleFalse()
        {
            lblMachineNo.Visible = false;
            cmbMachineNo.Visible = false;
            lblMachineDetails.Visible = false;
        }

        private void MachineDetailsVisibleTrue()
        {
            lblMachineNo.Visible = true;
            cmbMachineNo.Visible = true;
            lblMachineDetails.Visible = true;
            cmbMachineNo.Focus();
        }

        string Shift = string.Empty;
        bool ShiftFlag = false;

        private void ShiftCode()
        {
            TimeSpan StartTimeShift1 = new TimeSpan(07, 0, 0); //10 o'clock
            TimeSpan EndTimeShift1 = new TimeSpan(15, 0, 0); //12 o'clock

            TimeSpan StartTimeShift2 = new TimeSpan(15, 0, 0); //10 o'clock
            TimeSpan EndTimeShift2 = new TimeSpan(23, 0, 0); //12 o'clock

            TimeSpan StartTimeShift3 = new TimeSpan(23, 0, 0); //10 o'clock
            TimeSpan EndTimeShift3 = new TimeSpan(7, 0, 0); //12 o'clock

            TimeSpan StartTimeShift3Other = new TimeSpan(24, 0, 0); //10 o'clock

            DateTime TodayTime;
            TimeSpan now = DateTime.Now.TimeOfDay;
            ShiftFlag = false;

            if ((now > StartTimeShift1) && (now < EndTimeShift1))
                Shift = "I";
            else if ((now > StartTimeShift2) && (now < EndTimeShift2))
                Shift = "II";
            else
            {
                Shift = "III";
                int Checkhours = now.Hours;

                if (Checkhours == 23)
                    ShiftFlag = false;
                else
                    ShiftFlag = true;
            }
            //txtShift.Text = Shift.ToString();
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("QCEntry"));
            txtID.Text = IDNo.ToString();
        }

        private void txtSearchID_TextChanged(object sender, EventArgs e)
        {
            DateFlag = false;
            SearchTag = false;
            if (txtSearchID.Text != "")
                IDFlag = true;
            else
                IDFlag = false;

            FillGrid();
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
                UserClause = " and QCE.UserId = " + BusinessLayer.UserId_Static;
            else
                UserClause = string.Empty;

            //MainQuery = "select ID,EntryDate as [Date],EntryTime as [Time],Shift,MachinNo as [Machine No],PlantInchargeId,VolumeCheckerId,ProductId,ProductName  as [Product Name],NeckSizeI,NeckSizeRemarkI,WeightI,WeightRemarkI,NeckIDI,NeckIDRemarkI,NeckODI,NeckODRemarkI,NeckCollarGapI,NeckCollarGapRemarkI,NeckHeightI,NeckHeightRemarkI,ProductHeightI,ProductHeightRemarkI,ProductVolumeI,ProductVolumeRemarkI,CapSealingI,CapSealingRemarkI,WadSealingI,WadSealingRemarkI,NeckSizeII,NeckSizeRemarkII,WeightII,WeightRemarkII,NeckIDII,NeckIDRemarkII,NeckODII,NeckODRemarkII,NeckCollarGapII,NeckCollarGapRemarkII,NeckHeightII,NeckHeightRemarkII,ProductHeightII,ProductHeightRemarkII,ProductVolumeII,ProductVolumeRemarkII,CapSealingII,CapSealingRemarkII,WadSealingII,WadSealingRemarkII,CheckerNote,Remark,ErrorCount,ShiftEntryId,PlantInchargeId as[Plant Incharge],VolumeCheckerId as [Volume Checker],Cavity from QualityRegisterEntry where CancelTag=0 ";

            MainQuery = "select QCE.ID,QCE.EntryDate as [Date],QCE.EntryTime as [Time],QCE.ShiftEntryId,SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],QCE.MachinNo as [Machine No],QCE.ProductId,P.ProductType as [Product Type],P.ProductName as [Product Name],P.MouldId,MM.Cavity,QCE.PreformPartyId,PPM.PreformParty as [Preform Party],QCE.NeckSize1 as [Neck Size 1],QCE.NeckSizeRemark1,QCE.NeckID1 as [Neck ID 1],QCE.NeckIDRemark1,QCE.NeckOD1 as [Neck OD 1],QCE.NeckODRemark1,QCE.NeckCollarGap1 as [Neck Collar Gap 1],QCE.NeckCollarGapRemark1,QCE.NeckHeight1 as [Neck Height 1],QCE.NeckHeightRemark1,QCE.ProductHeight1 as [Product Height 1],QCE.ProductHeightRemark1,QCE.Weight1 as [Weight 1],QCE.WeightRemark1,QCE.ProductVolume1 as [Product Volume 1],QCE.ProductVolumeRemark1,QCE.CapSealing1 as [Cap Sealing 1],QCE.CapSealingRemark1,QCE.WadSealing1 as [Wad Sealing 1],QCE.WadSealingRemark1,QCE.NeckSize2 as [Neck Size 2],QCE.NeckSizeRemark2,QCE.NeckID2 as [Neck ID 2],QCE.NeckIDRemark2,QCE.NeckOD2 as [Neck OD 2],QCE.NeckODRemark2,QCE.NeckCollarGap2 as [Neck Collar Gap 2],QCE.NeckCollarGapRemark2,QCE.NeckHeight2 as [Neck Height 2],QCE.NeckHeightRemark2,QCE.ProductHeight2 as [Product Height 2],QCE.ProductHeightRemark2,QCE.Weight2 as [Weight 2],QCE.WeightRemark2,QCE.ProductVolume2 as [Product Volume 2],QCE.ProductVolumeRemark2,QCE.CapSealing2 as [Cap Sealing 2],QCE.CapSealingRemark2,QCE.WadSealing2 as [Wad Sealing 2],QCE.WadSealingRemark2,QCE.CheckerNote as [Checker Note],QCE.Remark,QCE.ErrorCount,ProductionEntryId,E.FirstName as [Plant],E1.FirstName as [Volume] from ((((((QCEntry QCE inner join Product P on P.ID=QCE.ProductId) inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join PreformPartyMaster PPM on PPM.ID=QCE.PreformPartyId) inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) inner join MouldMaster MM on MM.ID=P.MouldId) where QCE.CancelTag=0 and P.CancelTag=0 and SE.CancelTag=0  and PPM.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and MM.CancelTag=0";

            //MainQuery = "select QRE.ID,QRE.EntryDate as [Date],QRE.EntryTime as [Time],QRE.Shift,QRE.MachinNo as [Machine No],QRE.PlantInchargeId,QRE.VolumeCheckerId,QRE.ProductId,QRE.ProductName  as [Product Name],QRE.NeckSizeI,QRE.NeckSizeRemarkI,QRE.WeightI,QRE.WeightRemarkI,QRE.NeckIDI,QRE.NeckIDRemarkI,QRE.NeckODI,QRE.NeckODRemarkI,QRE.NeckCollarGapI,QRE.NeckCollarGapRemarkI,QRE.NeckHeightI,QRE.NeckHeightRemarkI,QRE.ProductHeightI,QRE.ProductHeightRemarkI,QRE.ProductVolumeI,QRE.ProductVolumeRemarkI,QRE.CapSealingI,QRE.CapSealingRemarkI,QRE.WadSealingI,QRE.WadSealingRemarkI,QRE.NeckSizeII,QRE.NeckSizeRemarkII,QRE.WeightII,QRE.WeightRemarkII,QRE.NeckIDII,QRE.NeckIDRemarkII,QRE.NeckODII,QRE.NeckODRemarkII,QRE.NeckCollarGapII,QRE.NeckCollarGapRemarkII,QRE.NeckHeightII,QRE.NeckHeightRemarkII,QRE.ProductHeightII,QRE.ProductHeightRemarkII,QRE.ProductVolumeII,QRE.ProductVolumeRemarkII,QRE.CapSealingII,QRE.CapSealingRemarkII,QRE.WadSealingII,QRE.WadSealingRemarkII,QRE.CheckerNote,QRE.Remark,QRE.ErrorCount from QualityRegisterEntry QRE where CancelTag=0 ";
            OrderByClause = " order by QCE.EntryDate desc";

            if (DateFlag)
                WhereClause = " and QCE.EntryDate=#" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            else if (SearchTag)
                WhereClause = " and P.ProductName like '%" + txtSearch.Text + "%'";
            else if (IDFlag)
                WhereClause = " and QCE.ID=" + txtSearchID.Text + "";
            else
                WhereClause = string.Empty;

            objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
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
                //61 E.FirstName as [Plant],
                //62 E1.FirstName as [Volume]

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

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[13].Visible = false;
                dataGridView1.Columns[14].Visible = false;
                dataGridView1.Columns[15].Visible = false;
                dataGridView1.Columns[58].Visible = false;
                dataGridView1.Columns[59].Visible = false;
                dataGridView1.Columns[60].Visible = false;
                dataGridView1.Columns[61].Visible = false;
                dataGridView1.Columns[62].Visible = false;
              
               

                //dataGridView1.Columns[0].Width = 40;
                //dataGridView1.Columns[1].Width = 80;
                //dataGridView1.Columns[2].Width = 80;
                //dataGridView1.Columns[3].Width = 50;
                //dataGridView1.Columns[4].Width = 100;
                //dataGridView1.Columns[5].Width = 150;
                //dataGridView1.Columns[6].Width = 150;
                //dataGridView1.Columns[8].Width = 300;
                //dataGridView1.Columns[53].Width = 150;
                //dataGridView1.Columns[54].Width = 150;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;

                //Visible False
                for (int k = 18; k <= 56; k = k + 2)
                {
                    dataGridView1.Columns[k].Visible = false;
                }
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = 130;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 18; j <= 56; j = j + 2)
                    {
                        SetRedCellColour(dataGridView1, i, j, j - 1);
                    }
                }
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 70;
                dataGridView1.Columns[4].Width = 50;
                dataGridView1.Columns[9].Width =100;
                dataGridView1.Columns[12].Width = 200;
                dataGridView1.Columns[16].Width = 110;
                btnReport.Visible = true;
            }
        }

        private void SetRedCellColour(DataGridView dgv,int i,int CheckCell,int SetCell)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[CheckCell].Value)))
            {
                if (dataGridView1.Rows[i].Cells[CheckCell].Value.ToString() == "1")
                    dataGridView1.Rows[i].Cells[SetCell].Style.BackColor = Color.Red;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DateFlag = false;
            IDFlag = false;
            if (txtSearch.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

      

        bool GridFlag = false;

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RowCount_Grid = dataGridView1.Rows.Count;
                CurrentRowIndex = dataGridView1.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    ClearAll();
                    btnDelete.Enabled = true;

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

                    GridFlag = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    txtID.Text = TableID.ToString();
                    dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    ShiftEntryId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                    Shift = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    GetShiftDetails();
                    cmbMachineNo.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    ProductId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
                    gbSearchProduct.Visible = true;
                    Fill_Product_Information();
                    lblProductName.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                    ProductName = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                    Cavity = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                    cmbPreformParty.Text = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
                    
                    txtNeckSize1.Text = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
                    txtNeckSizeRemark1.Text = dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString();
                    txtNeckID1.Text = dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString();
                    txtNeckIDRemark1.Text = dataGridView1.Rows[e.RowIndex].Cells[20].Value.ToString();
                    txtNeckOD1.Text = dataGridView1.Rows[e.RowIndex].Cells[21].Value.ToString();
                    txtNeckODRemark1.Text = dataGridView1.Rows[e.RowIndex].Cells[22].Value.ToString();
                    txtNeckCollarGap1.Text = dataGridView1.Rows[e.RowIndex].Cells[23].Value.ToString();
                    txtNeckCollarGapRemark1.Text = dataGridView1.Rows[e.RowIndex].Cells[24].Value.ToString();
                    txtNeckHeight1.Text = dataGridView1.Rows[e.RowIndex].Cells[25].Value.ToString();
                    txtNeckHeightRemark1.Text = dataGridView1.Rows[e.RowIndex].Cells[26].Value.ToString();
                    txtProductHeight1.Text = dataGridView1.Rows[e.RowIndex].Cells[27].Value.ToString();
                    txtProductHeightRemark1.Text = dataGridView1.Rows[e.RowIndex].Cells[28].Value.ToString();
                    txtWeight1.Text = dataGridView1.Rows[e.RowIndex].Cells[29].Value.ToString();
                    txtWeightRemark1.Text = dataGridView1.Rows[e.RowIndex].Cells[30].Value.ToString();
                    txtProductVolume1.Text = dataGridView1.Rows[e.RowIndex].Cells[31].Value.ToString();
                    txtProductVolumeRemark1.Text = dataGridView1.Rows[e.RowIndex].Cells[32].Value.ToString();
                    cmbCapSealing1.Text = dataGridView1.Rows[e.RowIndex].Cells[33].Value.ToString();
                    txtCapSealingRemark1.Text = dataGridView1.Rows[e.RowIndex].Cells[34].Value.ToString();
                    cmbWadSealing1.Text = dataGridView1.Rows[e.RowIndex].Cells[35].Value.ToString();
                    txtWadSealingRemark1.Text = dataGridView1.Rows[e.RowIndex].Cells[36].Value.ToString();

                    txtNeckSize2.Text = dataGridView1.Rows[e.RowIndex].Cells[37].Value.ToString();
                    txtNeckSizeRemark2.Text = dataGridView1.Rows[e.RowIndex].Cells[38].Value.ToString();
                    txtNeckID2.Text = dataGridView1.Rows[e.RowIndex].Cells[39].Value.ToString();
                    txtNeckIDRemark2.Text = dataGridView1.Rows[e.RowIndex].Cells[40].Value.ToString();
                    txtNeckOD2.Text = dataGridView1.Rows[e.RowIndex].Cells[41].Value.ToString();
                    txtNeckODRemark2.Text = dataGridView1.Rows[e.RowIndex].Cells[42].Value.ToString();
                    txtNeckCollarGap2.Text = dataGridView1.Rows[e.RowIndex].Cells[43].Value.ToString();
                    txtNeckCollarGapRemark2.Text = dataGridView1.Rows[e.RowIndex].Cells[44].Value.ToString();
                    txtNeckHeight2.Text = dataGridView1.Rows[e.RowIndex].Cells[45].Value.ToString();
                    txtNeckHeightRemark2.Text = dataGridView1.Rows[e.RowIndex].Cells[46].Value.ToString();
                    txtProductHeight2.Text = dataGridView1.Rows[e.RowIndex].Cells[47].Value.ToString();
                    txtProductHeightRemark2.Text = dataGridView1.Rows[e.RowIndex].Cells[48].Value.ToString();
                    txtWeight2.Text = dataGridView1.Rows[e.RowIndex].Cells[49].Value.ToString();
                    txtWeightRemark2.Text = dataGridView1.Rows[e.RowIndex].Cells[50].Value.ToString();
                    txtProductVolume2.Text = dataGridView1.Rows[e.RowIndex].Cells[51].Value.ToString();
                    txtProductVolumeRemark2.Text = dataGridView1.Rows[e.RowIndex].Cells[52].Value.ToString();
                    cmbCapSealing2.Text = dataGridView1.Rows[e.RowIndex].Cells[53].Value.ToString();
                    txtCapSealingRemark2.Text = dataGridView1.Rows[e.RowIndex].Cells[54].Value.ToString();
                    cmbWadSealing2.Text = dataGridView1.Rows[e.RowIndex].Cells[55].Value.ToString();
                    txtWadSealingRemark2.Text = dataGridView1.Rows[e.RowIndex].Cells[56].Value.ToString();
                    txtCheckerNote.Text = dataGridView1.Rows[e.RowIndex].Cells[57].Value.ToString();
                    Remark = dataGridView1.Rows[e.RowIndex].Cells[58].Value.ToString();
                    ErrorCount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[59].Value.ToString());
                    txtProductionEntryId.Text = dataGridView1.Rows[e.RowIndex].Cells[60].Value.ToString();
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

        private void btnAddShiftDetails_Click(object sender, EventArgs e)
        {
            ShiftEntry objForm = new ShiftEntry();
            objForm.ShowDialog(this);
            GetShiftDetails();
        }

        private void btnAddMachine_Click(object sender, EventArgs e)
        {

        }

        private void txtSearchProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearchProductName.Text != "" && lbItem.Items.Count > 0)
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                {
                    lbItem.SelectedIndex = 0;
                   lbItem.Focus();
                }
            }
        }

        private void NeckSize1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNeckID1.Focus();
        }

        private void txtNeckID1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNeckOD1.Focus();
        }

        private void txtNeckOD1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNeckCollarGap1.Focus();
        }

        private void txtNeckCollarGap1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNeckHeight1.Focus();
        }

        private void txtNeckHeight1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductHeight1.Focus();
        }

        private void txtProductHeight1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtWeight1.Focus();
        }

        private void txtWeight1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductVolume1.Focus();
        }

        private void txtProductVolume1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNeckSize2.Focus();
        }

        private void cmbCapSealing1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbWadSealing1.Focus();
        }

        private void cmbWadSealing1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNeckSize2.Focus();
        }

        private void txtNeckSize2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNeckID2.Focus();
        }

        private void txtNeckID2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNeckOD2.Focus();
        }

        private void txtNeckOD2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNeckCollarGap2.Focus();
        }

        private void txtNeckCollarGap2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNeckHeight2.Focus();
        }

        private void txtNeckHeight2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductHeight2.Focus();
        }

        private void txtProductHeight2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtWeight2.Focus();
        }

        private void txtWeight2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductVolume2.Focus();
        }

        private void ProductVolume2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbCapSealing2.Focus();
        }

        private void cmbCapSealing2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbWadSealing2.Focus();
        }

        private void cmbWadSealing2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCheckerNote.Focus();
        }

        private void txtCheckerNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        private void btnAddPreformParty_Click(object sender, EventArgs e)
        {
            PreformPartyMaster objForm = new PreformPartyMaster();
            objForm.ShowDialog(this);
            objRL.FillPreformParty(cmbPreformParty);
        }

        private void txtNeckSize1_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNeckSize1);
        }

        private void txtNeckID1_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNeckID1);
        }

        private void txtNeckOD1_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNeckOD1);
        }

        private void txtNeckCollarGap1_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNeckCollarGap1);
        }

        private void txtNeckHeight1_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNeckHeight1);
        }

        private void txtProductHeight1_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtProductHeight1);
        }

        private void txtWeight1_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtWeight1);
        }

        private void txtProductVolume1_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtProductVolume1);
        }

        private void txtNeckSize2_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNeckSize2);
        }

        private void txtNeckID2_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNeckID2);
        }

        private void txtNeckOD2_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNeckOD2);
        }

        private void txtNeckCollarGap2_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNeckCollarGap2);
        }

        private void txtNeckHeight2_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNeckHeight2);
        }

        private void txtProductHeight2_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtProductHeight2);
        }

        private void txtWeight2_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtWeight2);
        }

        private void txtProductVolume2_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtProductVolume2);
        }

        private void dtpSearchDate_ValueChanged(object sender, EventArgs e)
        {
            DateFlag = true;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            GetReport();
        }

        bool ColourFlag = false;

     
        string ConcatNames = string.Empty;

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

                    //ReportName = "MachineNo-" + cmbMachineNo.Text;

                    objRL.Form_ExcelFileName = "QCReport.xlsx";
                    objRL.Form_ReportFileName = "QCReport-" + ReportName;
                    objRL.Form_DestinationReportFilePath = "\\Quality Control Report\\" + ReportName + "\\";
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
                    myExcelWorksheet.get_Range("A3", misValue).Formula = "Report Date- " + dtpSearchDate.Value.ToString(RedundancyLogics.SystemDateFormat);
                    // // + " To Date: " + dtpToDate.Value.ToString(RedundancyLogics.SystemDateFormat);
                    myExcelWorksheet.get_Range("A4", misValue).Formula = "Report Created By:" + BusinessLayer.UserName_Static.ToString();

                    myExcelWorksheet.get_Range("O3", misValue).Formula = "Date-" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATDDMMYYYY);
                   // myExcelWorksheet.get_Range("M4", misValue).Formula = "Machine No-" + cmbMachineNo.Text.ToString();

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

                        //Product Name
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[12].Value)))
                        {
                            AFlag = 0;
                            Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[12].Value.ToString());
                        }

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

                                //Product Name
                                if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[12].Value)))
                                {
                                    AFlag = 0;
                                    Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[12].Value.ToString());
                                }
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
        string[] CellArray = { "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q"};
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

    }


}
