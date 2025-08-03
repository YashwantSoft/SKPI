using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using SPApplication.Master;

namespace SPApplication
{
    public partial class Product : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        double RequiredValue = 0, DifferanceRatio = 0, MinValue = 0, MaxValue = 0;

        public Product()
        {
            InitializeComponent();
            Set_Design();
            objRL.Fill_Mould(cmbMouldNo);
            objRL.Fill_PreformName(cmbPreformName);
        }

        private void Set_Design()
        {
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_ITEM);
            objDL.SetPlusButtonDesign(btnAddMouldNo);
            objDL.SetPlusButtonDesign(btnAddMouldNo);
            dataGridView1.DefaultCellStyle.SelectionBackColor = objBL.GetRowSelectionColor();
        }
        private void Product_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            cmbMouldNo.Focus();
        }

        bool SearchTag = false;
        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (!SearchTag)
                objBL.Query = "select ID,PreformId,PreformName,ProductType,ProductName,ProductNickName,MouldId,MouldNo,Standard,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,ProductMajorAxis,ProductMajorAxisRatio,ProductMajorAxisMinValue,ProductMajorAxisMaxValue,ProductMinorAxis,ProductMinorAxisRatio,ProductMinorAxisMinValue,ProductMinorAxisMaxValue,Status,NoteR,PackingQty,Semi,Auto1,Auto2,Servo from Product where CancelTag=0";
            else
                objBL.Query = "select ID,PreformId,PreformName,ProductType,ProductName,ProductNickName,MouldId,MouldNo,Standard,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,ProductMajorAxis,ProductMajorAxisRatio,ProductMajorAxisMinValue,ProductMajorAxisMaxValue,ProductMinorAxis,ProductMinorAxisRatio,ProductMinorAxisMinValue,ProductMinorAxisMaxValue,Status,NoteR,PackingQty,Semi,Auto1,Auto2,Servo from Product where CancelTag=0 and ProductName like '%" + txtSearchItemName.Text + "%'";

            //if (!SearchTag)
            //    objBL.Query = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard,Qty from Product where CancelTag=0 and ProductType='" + ProductType + "'";
            //else
            //    objBL.Query = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard,Qty from Product where CancelTag=0 and ProductType='" + ProductType + "' and ProductName like '%" + txtSearchItemName.Text + "%'";

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[6].Visible = false;

                dataGridView1.Columns[2].Width = 200;
                dataGridView1.Columns[3].Width = 120;
                dataGridView1.Columns[4].Width = 400;
                dataGridView1.Columns[5].Width = 120;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        int ManufractureId = 0;
        protected bool CheckExist()
        {
             string ItemName=string.Empty;
            ItemName= txtProductName.Text;
           
            DataSet ds = new DataSet();
            objBL.Query = "select ID from Product where CancelTag=0 and ProductName='" + ItemName.Replace("'", "''") + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                if (!CheckExist())
                {
                    SaveDB();
                    FillGrid();
                    ClearAll();
                    objRL.ShowMessage(7, 1);
                }
                else
                {
                    objRL.ShowMessage(12, 9);
                    return;
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                DialogResult dr;
                dr = objRL.Delete_Record_Show_Message();

                if (dr == DialogResult.Yes)
                {
                    FlagDelete = true;

                    SaveDB();
                    ClearAll();
                    FillGrid();
                    objRL.ShowMessage(9, 1);
                }
                else
                    ClearAll();
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        protected void SaveDB()
        {
            string ItemName = string.Empty;

            //if (txtItemName.Text != "" && cmbManufracture.Text != "Other")
            //    ItemName = txtItemName.Text + "-" + cmbManufracture.Text;
            //else
            ItemName = txtProductName.Text;

            if (TableID != 0)
                if (FlagDelete)
                    objBL.Query = "update Product set CancelTag=1 where ID=" + TableID + "";
                else
                    objBL.Query = "update Product set PreformId='" + cmbPreformName.SelectedValue + "',PreformName='" + cmbPreformName.Text + "',ProductType='" + cmbProductType.Text + "',ProductName='" + ItemName.Replace("'", "''") + "',ProductNickName='" + txtProductNickName.Text + "',MouldId=" + cmbMouldNo.SelectedValue + ",MouldNo='" + cmbMouldNo.Text + "',Standard='" + txtStandard.Text + "',PreformNeckSize='" + txtPreformNeckSize.Text + "',PreformWeight='" + txtPreformWeight.Text + "',PreformNeckID='" + txtPreformNeckID.Text + "',PreformNeckOD='" + txtPreformNeckOD.Text + "',PreformNeckCollarGap='" + txtPreformNeckCollarGap.Text + "',PreformNeckHeight='" + txtPreformNeckHeight.Text + "',ProductNeckSize='" + txtProductNeckSize.Text + "',ProductNeckSizeRatio='" + txtNeckSizeTolerance.Text + "',ProductNeckSizeMinValue='" + txtProductNeckSizeMinValue.Text + "',ProductNeckSizeMaxValue='" + txtProductNeckSizeMaxValue.Text + "',ProductWeight='" + txtProductWeight.Text + "',ProductWeightRatio='" + txtWeightTolerance.Text + "',ProductWeightMinValue='" + txtProductWeightMinValue.Text + "',ProductWeightMaxValue='" + txtProductWeightMaxValue.Text + "',ProductNeckID='" + txtProductNeckID.Text + "',ProductNeckIDRatio='" + txtNeckIDTolerance.Text + "',ProductNeckIDMinValue='" + txtProductNeckIDMinValue.Text + "',ProductNeckIDMaxValue='" + txtProductNeckIDMaxValue.Text + "',ProductNeckOD='" + txtProductNeckOD.Text + "',ProductNeckODRatio='" + txtNeckODTolerance.Text + "',ProductNeckODMinValue='" + txtProductNeckODMinValue.Text + "',ProductNeckODMaxValue='" + txtProductNeckODMaxValue.Text + "',ProductNeckCollarGap='" + txtProductNeckCollarGap.Text + "',ProductNeckCollarGapRatio='" + txtNeckCollarGapTolerance.Text + "',ProductNeckCollarGapMinValue='" + txtProductNeckCollarGapMinValue.Text + "',ProductNeckCollarGapMaxValue='" + txtProductNeckCollarGapMaxValue.Text + "',ProductNeckHeight='" + txtProductNeckHeight.Text + "',ProductNeckHeightRatio='" + txtNeckHeightTolerance.Text + "',ProductNeckHeightMinValue='" + txtProductNeckHeightMinValue.Text + "',ProductNeckHeightMaxValue='" + txtProductNeckHeightMaxValue.Text + "',ProductHeight='" + txtProductHeight.Text + "',ProductHeightRatio='" + txtHeightTolerance.Text + "',ProductHeightMinValue='" + txtProductHeightMinValue.Text + "',ProductHeightMaxValue='" + txtProductHeightMaxValue.Text + "',ProductVolume='" + txtProductVolume.Text + "',ProductVolumeRatio='" + txtVolumeTolerance.Text + "',ProductVolumeMinValue='" + txtProductVolumeMinValue.Text + "',ProductVolumeMaxValue='" + txtProductVolumeMaxValue.Text + "',ProductMajorAxis='" + txtMajorAxis.Text + "',ProductMajorAxisRatio='" + txtMajorAxisTolerance.Text + "',ProductMajorAxisMinValue='" + txtMajorAxisMinValue.Text + "',ProductMajorAxisMaxValue='" + txtMajorAxisMaxValue.Text + "',ProductMinorAxis='" + txtMinorAxis.Text + "',ProductMinorAxisRatio='" + txtMinorAxisTolerance.Text + "',ProductMinorAxisMinValue='" + txtMinorAxisMinValue.Text + "',ProductMinorAxisMaxValue='" + txtMinorAxisMaxValue.Text + "',Status='" + cmbStatus.Text + "',NoteR='" + txtNote.Text + "',PackingQty='" + txtPackingQty.Text + "',Semi='" + txtSemi.Text + "',Auto1='" + txtAuto1.Text + "',Auto2='" + txtAuto2.Text + "',Servo='" + txtServo.Text + "',ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
            else
                objBL.Query = "insert into Product(PreformId,PreformName,ProductType,ProductName,ProductNickName,MouldId,MouldNo,Standard,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,ProductMajorAxis,ProductMajorAxisRatio,ProductMajorAxisMinValue,ProductMajorAxisMaxValue,ProductMinorAxis,ProductMinorAxisRatio,ProductMinorAxisMinValue,ProductMinorAxisMaxValue,Status,NoteR,PackingQty,Semi,Auto1,Auto2,Servo,UserId) values(" + cmbPreformName.SelectedValue + ",'" + cmbPreformName.Text + "','" + cmbProductType.Text + "','" + ItemName.Replace("'", "''") + "','" + txtProductNickName.Text + "'," + cmbMouldNo.SelectedValue + ",'" + cmbMouldNo.Text + "','" + txtStandard.Text + "','" + txtPreformNeckSize.Text + "','" + txtPreformWeight.Text + "','" + txtPreformNeckID.Text + "','" + txtPreformNeckOD.Text + "','" + txtPreformNeckCollarGap.Text + "','" + txtPreformNeckHeight.Text + "','" + txtProductNeckSize.Text + "','" + txtNeckSizeTolerance.Text + "','" + txtProductNeckSizeMinValue.Text + "','" + txtProductNeckSizeMaxValue.Text + "','" + txtProductWeight.Text + "','" + txtWeightTolerance.Text + "','" + txtProductWeightMinValue.Text + "','" + txtProductWeightMaxValue.Text + "','" + txtProductNeckID.Text + "','" + txtNeckIDTolerance.Text + "','" + txtProductNeckIDMinValue.Text + "','" + txtProductNeckIDMaxValue.Text + "','" + txtProductNeckOD.Text + "','" + txtNeckODTolerance.Text + "','" + txtProductNeckODMinValue.Text + "','" + txtProductNeckODMaxValue.Text + "','" + txtProductNeckCollarGap.Text + "','" + txtNeckCollarGapTolerance.Text + "','" + txtProductNeckCollarGapMinValue.Text + "','" + txtProductNeckCollarGapMaxValue.Text + "','" + txtProductNeckHeight.Text + "','" + txtNeckHeightTolerance.Text + "','" + txtProductNeckHeightMinValue.Text + "','" + txtProductNeckHeightMaxValue.Text + "','" + txtProductHeight.Text + "','" + txtHeightTolerance.Text + "','" + txtProductHeightMinValue.Text + "','" + txtProductHeightMaxValue.Text + "','" + txtProductVolume.Text + "','" + txtVolumeTolerance.Text + "','" + txtProductVolumeMinValue.Text + "','" + txtProductVolumeMaxValue.Text + "','" + txtMajorAxis.Text + "','" + txtMajorAxisTolerance.Text + "','" + txtMajorAxisMinValue.Text + "','" + txtMajorAxisMaxValue.Text + "','" + txtMinorAxis.Text + "','" + txtMinorAxisTolerance.Text + "','" + txtMinorAxisMinValue.Text + "','" + txtMinorAxisMaxValue.Text + "','" + cmbStatus.Text + "','" + txtNote.Text + "','" + txtPackingQty.Text + "','" + txtSemi.Text + "','" + txtAuto1.Text + "','" + txtAuto2.Text + "','" + txtServo.Text + "'," + BusinessLayer.UserId_Static + ")";

            objBL.Function_ExecuteNonQuery();
        }

        protected bool Validation()
        {
            objEP.Clear();

            if (cmbPreformName.Text == "")
            {
                cmbPreformName.Focus();
                objEP.SetError(cmbPreformName, "Enter Preform Name");
                return true;
            }
            else if (txtPreformNeckSize.Text == "")
            {
                txtPreformNeckSize.Focus();
                objEP.SetError(txtPreformNeckSize, "Enter Preform Neck Size");
                return true;
            }
            else if (txtPreformWeight.Text == "")
            {
                txtPreformWeight.Focus();
                objEP.SetError(txtPreformWeight, "Enter Batch Number");
                return true;
            }
            else
                return false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected void ClearAll()
        {
            TableID = 0;
            objEP.Clear();
            FlagDelete = false;
            btnDelete.Enabled = false;

            cmbPreformName.SelectedIndex = -1;
            cmbProductType.SelectedIndex = -1;
            txtProductName.Text = "";
            txtProductNickName.Text = "";

            cmbMouldNo.SelectedIndex = -1;
            rtbMouldDescription.Text = "";

            txtStandard.Text = "";
            txtPreformNeckSize.Text = "";
            txtPreformWeight.Text = "";
            cmbPreformWeight.SelectedIndex = -1;
            txtPreformNeckID.Text = "";
            txtPreformNeckOD.Text = "";
            txtPreformNeckCollarGap.Text = "";
            txtPreformNeckHeight.Text = "";
            
            txtProductNeckSize.Text = "";
            txtNeckSizeTolerance.Text = "";
            txtProductNeckSizeMinValue.Text = "";
            txtProductNeckSizeMaxValue.Text = "";

            txtProductWeight.Text = "";
            txtWeightTolerance.Text = "";
            txtProductWeightMinValue.Text = "";
            txtProductWeightMaxValue.Text = "";

            txtProductNeckID.Text = "";
            txtNeckIDTolerance.Text = "";
            txtProductNeckIDMinValue.Text = "";
            txtProductNeckIDMaxValue.Text = "";

            txtProductNeckOD.Text = "";
            txtNeckODTolerance.Text = "";
            txtProductNeckODMinValue.Text = "";
            txtProductNeckODMaxValue.Text = "";

            txtProductNeckCollarGap.Text = "";
            txtNeckCollarGapTolerance.Text = "";
            txtProductNeckCollarGapMinValue.Text = "";
            txtProductNeckCollarGapMaxValue.Text = "";

            txtProductNeckHeight.Text = "";
            txtNeckHeightTolerance.Text = "";
            txtProductNeckHeightMinValue.Text = "";
            txtProductNeckHeightMaxValue.Text = "";

            txtProductHeight.Text = "";
            txtHeightTolerance.Text = "";
            txtProductHeightMinValue.Text = "";
            txtProductHeightMaxValue.Text = "";

            txtProductVolume.Text = "";
            txtVolumeTolerance.Text = "";
            txtProductVolumeMinValue.Text = "";
            txtProductVolumeMaxValue.Text = "";

            txtMajorAxis.Text = "";
            txtMajorAxisTolerance.Text = "";
            txtMajorAxisMinValue.Text = "";
            txtMajorAxisMaxValue.Text = "";

            txtMinorAxis.Text = "";
            txtMinorAxisTolerance.Text = "";
            txtMinorAxisMinValue.Text = "";
            txtMinorAxisMaxValue.Text = "";

            txtSemi.Text = "";
            txtAuto1.Text = "";
            txtAuto2.Text = "";
            txtServo.Text = "";

            cmbStatus.SelectedIndex = -1;
            txtNote.Text = "";
            
            txtPackingQty.Text = "";
            cmbMouldNo.Focus();
        }

        private void txtSearchItemName_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchItemName.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

        string MouldDetails = string.Empty;
        int MouldId = 0;
        private void Fill_Mould_Data()
        {
            if(cmbMouldNo.SelectedIndex >-1)
            {
                string MaterialIn = string.Empty;
                rtbMouldDescription.Text = "";
                MouldDetails = string.Empty;
                MouldId = Convert.ToInt32(cmbMouldNo.SelectedValue);
                objRL.Get_Mould_Records_By_Id(MouldId);
                MouldDetails = string.Empty;

                if (!string.IsNullOrEmpty(objRL.Material))
                    MaterialIn = objRL.Material;

                MouldDetails = "Mould No-\t" + objRL.SrNoMould.ToString() + "\t" + "Party-\t" + objRL.Party.ToString() + "\n" +
                                "Cavity-\t\t" + objRL.Cavity.ToString() + "\t" + "Type-\t" + objRL.AutoSemi.ToString() + "\n" +
                                "Material-\t" + MaterialIn.ToString() + "\t" + "Tally Name-\t" + objRL.TallyName.ToString();

                rtbMouldDescription.Text = MouldDetails.ToString();
            }
        }

        private void cmbMouldNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_Mould_Data();
        }

        private void txtProductNeckSize_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(2);
        }

        private void txtNeckSizeTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(2);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RowCount_Grid = dataGridView1.Rows.Count;
                CurrentRowIndex = dataGridView1.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    //0 ID 
                    //1 PreformId,
                    //2 PreformName,
                    //3 ProductType,
                    //4 ProductName,
                    //5 ProductNickName,
                    //6 MouldId,
                    //7 MouldNo,
                    //8 Standard,
                    //9 PreformNeckSize,
                    //10 PreformWeight,
                    //11 PreformNeckID,
                    //12 PreformNeckOD,
                    //13 PreformNeckCollarGap,
                    //14 PreformNeckHeight,
                    //15 ProductNeckSize,
                    //16 ProductNeckSizeRatio,
                    //17 ProductNeckSizeMinValue,
                    //18 ProductNeckSizeMaxValue,
                    //19 ProductWeight,
                    //20 ProductWeightRatio,
                    //21 ProductWeightMinValue,
                    //22 ProductWeightMaxValue,
                    //23 ProductNeckID,
                    //24 ProductNeckIDRatio,
                    //25 ProductNeckIDMinValue,
                    //26 ProductNeckIDMaxValue,
                    //27 ProductNeckOD,
                    //28 ProductNeckODRatio,
                    //29 ProductNeckODMinValue,
                    //30 ProductNeckODMaxValue,
                    //31 ProductNeckCollarGap,
                    //32 ProductNeckCollarGapRatio,
                    //33 ProductNeckCollarGapMinValue,
                    //34 ProductNeckCollarGapMaxValue,
                    //35 ProductNeckHeight,
                    //36 ProductNeckHeightRatio,
                    //37 ProductNeckHeightMinValue,
                    //38 ProductNeckHeightMaxValue,
                    //39 ProductHeight,
                    //40 ProductHeightRatio,
                    //41 ProductHeightMinValue,
                    //42 ProductHeightMaxValue,
                    //43 ProductVolume,
                    //44 ProductVolumeRatio,
                    //45 ProductVolumeMinValue,
                    //46 ProductVolumeMaxValue,
                    //47 ProductMajorAxisRatio,
                    //48 ProductMajorAxisMinValue,
                    //49 ProductMajorAxisMaxValue,
                    //50 ProductMinorAxisRatio,
                    //51 ProductMinorAxisMinValue,
                    //52 ProductMinorAxisMaxValue
                    //53 Status,
                    //54 NoteR,
                    //55 PackingQty
                    //56 Semi,
                    //57 Auto1,
                    //58 Auto2,
                    //59 Servo

                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value)))
                    {
                        PreformId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                        cmbPreformName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    }
                    else
                    {
                        cmbPreformName.SelectedIndex = -1;
                        PreformId = 0;
                    }
                    
                    //FillPreformInformation();
                    cmbProductType.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtProductName.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtProductNickName.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    MouldId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                    cmbMouldNo.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    Fill_Mould_Data();

                    txtStandard.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    txtPreformNeckSize.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    txtPreformWeight.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    txtPreformNeckID.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    txtPreformNeckOD.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                    txtPreformNeckCollarGap.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                    txtPreformNeckHeight.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                    
                    txtProductNeckSize.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                    txtNeckSizeTolerance.Text = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
                    txtProductNeckSizeMinValue.Text = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
                    txtProductNeckSizeMaxValue.Text = dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString();
                    txtProductWeight.Text = dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString();
                    txtWeightTolerance.Text = dataGridView1.Rows[e.RowIndex].Cells[20].Value.ToString();
                    txtProductWeightMinValue.Text = dataGridView1.Rows[e.RowIndex].Cells[21].Value.ToString();
                    txtProductWeightMaxValue.Text = dataGridView1.Rows[e.RowIndex].Cells[22].Value.ToString();
                    txtProductNeckID.Text = dataGridView1.Rows[e.RowIndex].Cells[23].Value.ToString();
                    txtNeckIDTolerance.Text = dataGridView1.Rows[e.RowIndex].Cells[24].Value.ToString();
                    txtProductNeckIDMinValue.Text = dataGridView1.Rows[e.RowIndex].Cells[25].Value.ToString();
                    txtProductNeckIDMaxValue.Text = dataGridView1.Rows[e.RowIndex].Cells[26].Value.ToString();
                    txtProductNeckOD.Text = dataGridView1.Rows[e.RowIndex].Cells[27].Value.ToString();
                    txtNeckODTolerance.Text = dataGridView1.Rows[e.RowIndex].Cells[28].Value.ToString();
                    txtProductNeckODMinValue.Text = dataGridView1.Rows[e.RowIndex].Cells[29].Value.ToString();
                    txtProductNeckODMaxValue.Text = dataGridView1.Rows[e.RowIndex].Cells[30].Value.ToString();
                    txtProductNeckCollarGap.Text = dataGridView1.Rows[e.RowIndex].Cells[31].Value.ToString();
                    txtNeckCollarGapTolerance.Text = dataGridView1.Rows[e.RowIndex].Cells[32].Value.ToString();
                    txtProductNeckCollarGapMinValue.Text = dataGridView1.Rows[e.RowIndex].Cells[33].Value.ToString();
                    txtProductNeckCollarGapMaxValue.Text = dataGridView1.Rows[e.RowIndex].Cells[34].Value.ToString();
                    txtProductNeckHeight.Text = dataGridView1.Rows[e.RowIndex].Cells[35].Value.ToString();
                    txtNeckHeightTolerance.Text = dataGridView1.Rows[e.RowIndex].Cells[36].Value.ToString();
                    txtProductNeckHeightMinValue.Text = dataGridView1.Rows[e.RowIndex].Cells[37].Value.ToString();
                    txtProductNeckHeightMaxValue.Text = dataGridView1.Rows[e.RowIndex].Cells[38].Value.ToString();
                    txtProductHeight.Text = dataGridView1.Rows[e.RowIndex].Cells[39].Value.ToString();
                    txtHeightTolerance.Text = dataGridView1.Rows[e.RowIndex].Cells[40].Value.ToString();
                    txtProductHeightMinValue.Text = dataGridView1.Rows[e.RowIndex].Cells[41].Value.ToString();
                    txtProductHeightMaxValue.Text = dataGridView1.Rows[e.RowIndex].Cells[42].Value.ToString();
                    txtProductVolume.Text = dataGridView1.Rows[e.RowIndex].Cells[43].Value.ToString();
                    txtVolumeTolerance.Text = dataGridView1.Rows[e.RowIndex].Cells[44].Value.ToString();
                    txtProductVolumeMinValue.Text = dataGridView1.Rows[e.RowIndex].Cells[45].Value.ToString();
                    txtProductVolumeMaxValue.Text = dataGridView1.Rows[e.RowIndex].Cells[46].Value.ToString();

                    txtMajorAxis.Text = dataGridView1.Rows[e.RowIndex].Cells[47].Value.ToString();
                    txtMajorAxisTolerance.Text = dataGridView1.Rows[e.RowIndex].Cells[48].Value.ToString();
                    txtMajorAxisMinValue.Text = dataGridView1.Rows[e.RowIndex].Cells[49].Value.ToString();
                    txtMajorAxisMaxValue.Text = dataGridView1.Rows[e.RowIndex].Cells[50].Value.ToString();

                    txtMinorAxis.Text = dataGridView1.Rows[e.RowIndex].Cells[51].Value.ToString();
                    txtMinorAxisTolerance.Text = dataGridView1.Rows[e.RowIndex].Cells[52].Value.ToString();
                    txtMinorAxisMinValue.Text = dataGridView1.Rows[e.RowIndex].Cells[53].Value.ToString();
                    txtMinorAxisMaxValue.Text = dataGridView1.Rows[e.RowIndex].Cells[54].Value.ToString();
                    
                    cmbStatus.Text = dataGridView1.Rows[e.RowIndex].Cells[55].Value.ToString();
                    txtNote.Text = dataGridView1.Rows[e.RowIndex].Cells[56].Value.ToString();
                    txtPackingQty.Text = dataGridView1.Rows[e.RowIndex].Cells[57].Value.ToString();

                    txtSemi.Text = dataGridView1.Rows[e.RowIndex].Cells[58].Value.ToString();
                    txtAuto1.Text = dataGridView1.Rows[e.RowIndex].Cells[59].Value.ToString();
                    txtAuto2.Text = dataGridView1.Rows[e.RowIndex].Cells[60].Value.ToString();
                    txtServo.Text = dataGridView1.Rows[e.RowIndex].Cells[61].Value.ToString();
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

        private void txtProductWeight_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(1);
        }

        private void txtWeightTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(1);
        }

        private void txtProductNeckID_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(3);
        }

        private void txtNeckIDTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(3);
        }

        private void txtProductNeckOD_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(4);
        }

        private void txtNeckODTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(4);
        }

        private void txtIProductNeckCollarGap_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(5);
        }

        private void txtNeckCollarGapTolerance_TextChanged(object sender, EventArgs e)
        {
           CalculateValue(5);
        }

        private void txtProductNeckHeight_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(6);
        }

        private void txtNeckHeightTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(6);
        }

        private void txtProductHeight_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(7);
        }

        private void txtHeightTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(7);
        }

        private void txtProductVolume_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(8);
        }

        private void txtVolumeTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(8);
        }

        private void txtPreformNeckSize_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPreformNeckSize.Text))
                txtProductNeckSize.Text = txtPreformNeckSize.Text;
        }

        private void cmbPreformWeight_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbPreformWeight.SelectedIndex >-1)
                txtProductWeight.Text = cmbPreformWeight.Text;
        }

        private void txtPreformNeckID_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPreformNeckID.Text))
                txtProductNeckID.Text = txtPreformNeckID.Text;
        }

        private void txtPreformNeckOD_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPreformNeckOD.Text))
                txtProductNeckOD.Text = txtPreformNeckOD.Text;
        }

        private void txtPreformNeckCollerGap_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPreformNeckCollarGap.Text))
                txtProductNeckCollarGap.Text = txtPreformNeckCollarGap.Text;
        }

        private void txtPreformNeckHeight_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPreformNeckHeight.Text))
                txtProductNeckHeight.Text = txtPreformNeckHeight.Text;
        }

        private void cmbMouldNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        string ProductType = string.Empty;
        private void cmbProductType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if(cmbProductType.SelectedIndex >-1)
            //{
            //    ProductType = cmbProductType.Text;
            //    FillGrid();
            //}
                
        }

        private void txtPreformWeight_TextChanged(object sender, EventArgs e)
        {
            if (txtPreformWeight.Text != "")
                txtProductWeight.Text = txtPreformWeight.Text;
        }

        private void btnAddPreform_Click(object sender, EventArgs e)
        {
            Preform objForm = new Preform();
            objForm.ShowDialog(this);
        }

        private void cmbPreformName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillPreformInformation();
        }

        int PreformId = 0;
        private void FillPreformInformation()
        {
            if (cmbPreformName.SelectedIndex > -1)
            {
                cmbProductType.SelectedIndex = -1;
                txtStandard.Text = "";
                txtPreformNeckSize.Text = "";
                txtProductNeckSize.Text = "";
                txtPreformWeight.Text = "";
                txtProductWeight.Text = "";
                txtPreformNeckID.Text = "";
                txtProductNeckID.Text = "";
                txtPreformNeckOD.Text = "";
                txtProductNeckOD.Text = "";
                txtPreformNeckCollarGap.Text = "";
                txtProductNeckCollarGap.Text = "";
                txtPreformNeckHeight.Text = "";
                txtProductNeckHeight.Text = "";

                PreformId = Convert.ToInt32(cmbPreformName.SelectedValue.ToString());
                objRL.Get_Preform_Records_By_Id(PreformId);

                if (objRL.CheckNull_String(Convert.ToString(objRL.PreformType)))
                    cmbProductType.Text = objRL.PreformType;
                //if (objRL.CheckNull_String(Convert.ToString(objRL.PreformName.ToString())))
                //    objRL.PreformName;
                if (objRL.CheckNull_String(Convert.ToString(objRL.Standard)))
                    txtStandard.Text = objRL.Standard;
                if (objRL.CheckNull_String(Convert.ToString(objRL.PreformNeckSize)))
                {
                    txtPreformNeckSize.Text = objRL.PreformNeckSize;
                    txtProductNeckSize.Text = objRL.PreformNeckSize;
                }

                if (objRL.CheckNull_String(Convert.ToString(objRL.PreformWeight)))
                {
                    txtPreformWeight.Text = objRL.PreformWeight;
                    txtProductWeight.Text = objRL.PreformWeight;
                }

                if (objRL.CheckNull_String(Convert.ToString(objRL.PreformNeckID)))
                {
                    txtPreformNeckID.Text = objRL.PreformNeckID.ToString();
                    txtProductNeckID.Text = objRL.PreformNeckID.ToString();
                }
                   
                if (objRL.CheckNull_String(Convert.ToString(objRL.PreformNeckOD)))
                {
                    txtPreformNeckOD.Text = objRL.PreformNeckOD.ToString();
                    txtProductNeckOD.Text = objRL.PreformNeckOD.ToString();
                }
                    
                if (objRL.CheckNull_String(Convert.ToString(objRL.PreformNeckCollarGap)))
                {
                    txtPreformNeckCollarGap.Text = objRL.PreformNeckCollarGap.ToString();
                    txtProductNeckCollarGap.Text = objRL.PreformNeckCollarGap.ToString();
                }
                    
                if (objRL.CheckNull_String(Convert.ToString(objRL.PreformNeckHeight)))
                {
                    txtPreformNeckHeight.Text = objRL.PreformNeckHeight.ToString();
                    txtProductNeckHeight.Text = objRL.PreformNeckHeight.ToString();
                }
            }
        }
        private void cmbCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductName.Focus();
        }

        private void btnAddMouldNo_Click(object sender, EventArgs e)
        {
            MouldMaster objForm = new MouldMaster();
            objForm.ShowDialog(this);
            objRL.Fill_Mould(cmbMouldNo);
        }

        public void CalculateValue(int CheckValue)
        {
            switch (CheckValue)
            {
                case 1:
                    SetValueMinMax(txtProductWeight, txtWeightTolerance, txtProductWeightMinValue, txtProductWeightMaxValue);
                    break;
                case 2:
                    SetValueMinMax(txtProductNeckSize, txtNeckSizeTolerance, txtProductNeckSizeMinValue, txtProductNeckSizeMaxValue);
                    break;
                case 3:
                    SetValueMinMax(txtProductNeckID, txtNeckIDTolerance, txtProductNeckIDMinValue, txtProductNeckIDMaxValue);
                    break;
                case 4:
                    SetValueMinMax(txtProductNeckOD, txtNeckODTolerance, txtProductNeckODMinValue, txtProductNeckODMaxValue);
                    break;
                case 5:
                    SetValueMinMax(txtProductNeckCollarGap, txtNeckCollarGapTolerance, txtProductNeckCollarGapMinValue, txtProductNeckCollarGapMaxValue);
                    break;
                case 6:
                    SetValueMinMax(txtProductNeckHeight, txtNeckHeightTolerance, txtProductNeckHeightMinValue, txtProductNeckHeightMaxValue);
                    break;
                case 7:
                    SetValueMinMax(txtProductHeight, txtHeightTolerance, txtProductHeightMinValue, txtProductHeightMaxValue);
                    break;
                case 8:
                    SetValueMinMax(txtProductVolume, txtVolumeTolerance, txtProductVolumeMinValue, txtProductVolumeMaxValue);
                    break;
                case 9:
                    SetValueMinMax(txtMajorAxis, txtMajorAxisTolerance, txtMajorAxisMinValue, txtMajorAxisMaxValue);
                    break;
                case 10:
                    SetValueMinMax(txtMinorAxis, txtMinorAxisTolerance, txtMinorAxisMinValue, txtMinorAxisMaxValue);
                    break;
            }
        }

        private void SetValueMinMax(TextBox RequiredValue_F, TextBox DifferanceRatio_F, TextBox MinValue_F, TextBox MaxValue_F)
        {
            RequiredValue = 0; DifferanceRatio = 0; MinValue = 0; MaxValue = 0;

            double.TryParse(RequiredValue_F.Text, out RequiredValue);
            double.TryParse(DifferanceRatio_F.Text, out DifferanceRatio);

            if (RequiredValue != 0)
            {
                MinValue = RequiredValue-DifferanceRatio;
                //MinValue = RequiredValue - DifferanceRatio;
                MaxValue = RequiredValue + DifferanceRatio;

                MinValue_F.Text = MinValue.ToString();
                MaxValue_F.Text = MaxValue.ToString();
            }
            else
            {
                MinValue_F.Text = "";
                MaxValue_F.Text = "";
            }
        }

        private void btnManufracture_Click(object sender, EventArgs e)
        {
            UsedForMaster objForm = new UsedForMaster();
            objForm.ShowDialog(this);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void CheckMinMaxValue(double CheckingValue, TextBox txtValue,bool Flag)
        {
            double Value1 = 0;
            double.TryParse(txtValue.Text, out Value1);

            if (Flag)
            {
                if (CheckingValue < Value1)
                {
                    txtValue.Text = ""; txtValue.Focus();
                    objRL.ShowMessage(17, 4);
                }
            }
            else
            {
                if (CheckingValue > Value1)
                {
                    txtValue.Text = ""; txtValue.Focus();
                    objRL.ShowMessage(17, 4);
                }
            }
        }

        private void txtProductNeckSizeMinValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtProductNeckSize.Text), txtProductNeckSizeMinValue, true);
        }

        private void txtProductNeckSizeMaxValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtProductNeckSize.Text), txtProductNeckSizeMaxValue, false);
        }


        private void txtProductNeckODMinValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtProductNeckID.Text), txtProductNeckIDMinValue, true);
        }

        private void txtProductNeckODMaxValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtProductNeckID.Text), txtProductNeckIDMaxValue, false);
        }

        private void txtProductNeckCollarGapMinValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtProductNeckOD.Text), txtProductNeckODMinValue, true);
        }

        private void txtProductNeckCollarGapMaxValue_Leave(object sender, EventArgs e)
        {
            
            CheckMinMaxValue(Convert.ToDouble(txtProductNeckCollarGap.Text), txtProductNeckCollarGapMinValue, false);
        }

        private void txtProductNeckHeightMinValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtProductNeckHeight.Text), txtProductNeckHeightMinValue, true);
           
        }

        private void txtProductNeckHeightMaxValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtProductNeckHeight.Text), txtProductNeckHeightMaxValue, false);
        }

        private void txtProductHeightMinValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtProductHeight.Text), txtProductHeightMinValue, true);
        }

        private void txtProductHeightMaxValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtProductHeight.Text), txtProductHeightMaxValue, false);
        }

        private void txtProductWeightMinValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtProductWeight.Text), txtProductWeightMinValue, true);
        }

        private void txtProductWeightMaxValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtProductWeight.Text), txtProductWeightMaxValue, false);
        }

        private void txtProductVolumeMinValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtProductVolume.Text), txtProductVolumeMinValue, true);
        }

        private void txtProductVolumeMaxValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtProductVolume.Text), txtProductVolumeMinValue, false);
        }


     

     

       

        private void txtProductNeckSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductNeckSizeMinValue.Focus();
        }

        private void txtProductNeckSizeMinValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductNeckSizeMaxValue.Focus();
        }

        private void txtProductNeckSizeMaxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductNeckID.Focus();
        }

        private void txtProductNeckID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductNeckIDMinValue.Focus();
        }

        private void txtProductNeckIDMinValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductNeckIDMaxValue.Focus();
        }

        private void txtProductNeckIDMaxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductNeckOD.Focus();
        }

        private void txtProductNeckOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductNeckODMinValue.Focus();
        }

        private void txtProductNeckODMinValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductNeckODMaxValue.Focus();
        }

        private void txtProductNeckODMaxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductNeckCollarGap.Focus();
        }

        private void txtProductNeckCollarGap_KeyDown(object sender, KeyEventArgs e)
        {
           if (e.KeyCode == Keys.Enter)
               txtProductNeckCollarGapMinValue.Focus();
        }

        private void txtProductNeckCollarGapMinValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductNeckCollarGapMaxValue.Focus();
        }

        private void txtProductNeckCollarGapMaxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductNeckHeight.Focus();
        }

        private void txtProductNeckHeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductNeckHeightMinValue.Focus();
        }

        private void txtProductNeckHeightMinValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductNeckHeightMaxValue.Focus();
        }

        private void txtProductNeckHeightMaxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductHeight.Focus();
        }

        private void txtProductHeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtHeightTolerance.Focus();
        }

        private void txtProductHeightMinValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductHeightMaxValue.Focus();
        }

        private void txtProductHeightMaxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductWeight.Focus();
        }

        private void txtProductWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductWeightMinValue.Focus();
        }

        private void txtProductWeightMinValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductWeightMaxValue.Focus();
        }

        private void txtProductWeightMaxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductVolume.Focus();
        }

        private void txtProductVolume_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProductVolumeMinValue.Focus();
        }

        private void txtProductVolumeMinValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        private void txtProductHeightMinValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMajorAxisMinValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtMajorAxis.Text), txtMajorAxisMinValue, true);
        }

        private void txtMajorAxisMaxValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtMajorAxis.Text), txtMajorAxisMaxValue, false);
        }

        private void txtMinorAxisMinValue_Leave(object sender, EventArgs e)
        {
            CheckMinMaxValue(Convert.ToDouble(txtMajorAxis.Text), txtMinorAxisMinValue, true);
        }

        private void txtMinorAxisMaxValue_Leave(object sender, EventArgs e)
        {
          //  if(txtMinorAxisMaxValue.Text !="")
             CheckMinMaxValue(Convert.ToDouble(txtMinorAxisMaxValue.Text), txtMinorAxisMaxValue, false);
        }

        private void txtProductWeightMaxValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMajorAxisTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(9);
        }

        private void txtMinorAxisTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(10);
        }

        private void txtMajorAxis_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(9);
        }

        private void txtMinorAxis_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(10);
        }

        private void txtSemi_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtSemi);
        }

        private void txtAuto1_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtAuto1);
        }

        private void txtAuto2_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtAuto2);
        }

        private void txtServo_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtServo);
        }

        private void txtHeightTolerance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSemi.Focus();
        }

        private void txtSemi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAuto1.Focus();
        }

        private void txtAuto1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAuto2.Focus();
        }

        private void txtAuto2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtServo.Focus();
        }

        private void txtServo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNote.Focus();
        }

        private void txtNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        int IsSample = 0;
        private void cbIsSample_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsSample.Checked)
                IsSample = 1;
            else
                IsSample = 0;
        }
    }
}
