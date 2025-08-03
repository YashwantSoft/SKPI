using BusinessLayerUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPApplication.Transaction
{
    public partial class Tolerance : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        public Tolerance()
        {
            InitializeComponent();
            DesignForm();
        }

        public Tolerance(int ProductId)
        {
            InitializeComponent();
            DesignForm();
            objRL.Get_Product_Records_By_Id(ProductId);
            SetValues();
        }

        private void DesignForm()
        {
            lblHeader.BackColor = objDL.GetBackgroundColor();
            lblHeader.ForeColor = objDL.GetForeColor();
            lblHeader.Text = BusinessResources.LBL_HEADER_PRODUCTTOLLARANCEVALUE;
            objDL.SetButtonDesign(btnExit, BusinessResources.BTN_EXIT);
        }

        private void TollaranceValueForm_Load(object sender, EventArgs e)
        {
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void SetValues()
        {
            cmbProductType.Text = objRL.ProductType.ToString();
            txtProductName.Text = objRL.ProductName.ToString();

            txtProductNeckSize.Text = objRL.ProductNeckSize.ToString();
            txtNeckSizeTolerance.Text = objRL.ProductNeckSizeRatio.ToString();
            txtProductNeckSizeMinValue.Text = objRL.ProductNeckSizeMinValue.ToString();
            txtProductNeckSizeMaxValue.Text = objRL.ProductNeckSizeMaxValue.ToString();

            txtProductNeckID.Text = objRL.ProductNeckID.ToString();
            txtNeckIDTolerance.Text = objRL.ProductNeckIDRatio.ToString();
            txtProductNeckIDMinValue.Text = objRL.ProductNeckIDMinValue.ToString();
            txtProductNeckIDMaxValue.Text = objRL.ProductNeckIDMaxValue.ToString();

            txtProductNeckOD.Text = objRL.ProductNeckOD.ToString();
            txtNeckODTolerance.Text = objRL.ProductNeckODRatio.ToString();
            txtProductNeckODMinValue.Text = objRL.ProductNeckODMinValue.ToString();
            txtProductNeckODMaxValue.Text = objRL.ProductNeckODMaxValue.ToString();

            txtProductNeckCollarGap.Text = objRL.ProductNeckCollarGap.ToString();
            txtNeckCollarGapTolerance.Text = objRL.ProductNeckCollarGapRatio.ToString();
            txtProductNeckCollarGapMinValue.Text = objRL.ProductNeckCollarGapMinValue.ToString();
            txtProductNeckCollarGapMaxValue.Text = objRL.ProductNeckCollarGapMaxValue.ToString();

            txtProductNeckHeight.Text = objRL.ProductNeckHeight.ToString();
            txtNeckHeightTolerance.Text = objRL.ProductNeckHeightRatio.ToString();
            txtProductNeckHeightMinValue.Text = objRL.ProductNeckHeightMinValue.ToString();
            txtProductNeckHeightMaxValue.Text = objRL.ProductNeckHeightMaxValue.ToString();

            txtProductHeight.Text = objRL.ProductHeight.ToString();
            txtHeightTolerance.Text = objRL.ProductHeightRatio.ToString();
            txtProductHeightMinValue.Text = objRL.ProductHeightMinValue.ToString();
            txtProductHeightMaxValue.Text = objRL.ProductHeightMaxValue.ToString();

            txtProductWeight.Text = objRL.ProductWeight.ToString();
            txtWeightTolerance.Text = objRL.ProductWeightRatio.ToString();
            txtProductWeightMinValue.Text = objRL.ProductWeightMinValue.ToString();
            txtProductWeightMaxValue.Text = objRL.ProductWeightMaxValue.ToString();

            txtProductVolume.Text = objRL.ProductVolume.ToString();
            txtVolumeTolerance.Text = objRL.ProductVolumeRatio.ToString();
            txtProductVolumeMinValue.Text = objRL.ProductVolumeMinValue.ToString();
            txtProductVolumeMaxValue.Text = objRL.ProductVolumeMaxValue.ToString();

           txtMajorAxis.Text = objRL.ProductMajorAxis;
           txtMajorAxisTolerance.Text = objRL.ProductMajorAxisRatio.ToString();
            txtMajorAxisMinValue.Text = objRL.ProductMajorAxisMinValue;
            txtMajorAxisMaxValue.Text = objRL.ProductMajorAxisMaxValue;

            txtMinorAxis.Text = objRL.ProductMinorAxis;
            txtMinorAxisTolerance.Text = objRL.ProductMinorAxisRatio.ToString();
            txtMinorAxisMinValue.Text = objRL.ProductMinorAxisMinValue;
            txtMinorAxisMaxValue.Text = objRL.ProductMinorAxisMaxValue;
            btnExit.Focus();
        }
    }
}
