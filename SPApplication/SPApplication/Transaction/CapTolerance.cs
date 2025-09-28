using BusinessLayerUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPApplication.Transaction
{
    public partial class CapTolerance : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        public CapTolerance()
        {
            InitializeComponent();
            DesignForm();
        }

        public CapTolerance(int CapId)
        {
            InitializeComponent();
            DesignForm();
            objRL.Get_Cap_Records_By_Id(CapId);
            SetValues();
        }

        private void DesignForm()
        {
            lblHeader.BackColor = objDL.GetBackgroundColor();
            lblHeader.ForeColor = objDL.GetForeColor();
            lblHeader.Text = BusinessResources.LBL_HEADER_WADTOLLARANCEVALUE1;
            objDL.SetButtonDesign(btnExit, BusinessResources.BTN_EXIT);
        }

        private void SetValues()
        {

            txtCapName.Text = objRL.Check_Null_String(Convert.ToString(objRL.CapName));
            cmbIsWad.Text = objRL.Check_Null_String(Convert.ToString(objRL.Wad));

            cmbMaterialUsed.Text = objRL.Check_Null_String(Convert.ToString(objRL.MaterialUsed));
            cmbCapColor.Text = objRL.Check_Null_String(Convert.ToString(objRL.CapColor));
            cmbCapType.Text = objRL.Check_Null_String(Convert.ToString(objRL.CapType));

            cmbCustomerLogo.Text = objRL.Check_Null_String(Convert.ToString(objRL.CustomerLogo));
            cmbPrintType.Text = objRL.Check_Null_String(Convert.ToString(objRL.PrintType));
            txtMasterBatchDetails.Text = objRL.Check_Null_String(Convert.ToString(objRL.MasterBatchDetails));

            txtOuterDiaStandard.Text = objRL.Check_Null_String(Convert.ToString(objRL.OuterDiaStandard));
            txtOuterDiaTolerance.Text = objRL.Check_Null_String(Convert.ToString(objRL.OuterDiaTolerance));
            txtOuterDiaMinValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.OuterDiaMinValue));
            txtOuterDiaMaxValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.OuterDiaMaxValue));

            txtInnerDiaWithThreadStandard.Text = objRL.Check_Null_String(Convert.ToString(objRL.InnerDiaWithThreadStandard));
            txtInnerDiaWithThreadTolerance.Text = objRL.Check_Null_String(Convert.ToString(objRL.InnerDiaWithThreadTolerance));
            txtInnerDiaWithThreadMinValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.InnerDiaWithThreadMinValue));
            txtInnerDiaWithThreadMaxValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.InnerDiaWithThreadMaxValue));

            txtInnerDiaWOThreadStandard.Text = objRL.Check_Null_String(Convert.ToString(objRL.InnerDiaWOThreadStandard));
            txtInnerDiaWOThreadTolerance.Text = objRL.Check_Null_String(Convert.ToString(objRL.InnerDiaWOThreadTolerance));
            txtInnerDiaWOThreadMinValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.InnerDiaWOThreadMinValue));
            txtInnerDiaWOThreadMaxValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.InnerDiaWOThreadMaxValue));

            txtCapHeightStandard.Text = objRL.Check_Null_String(Convert.ToString(objRL.CapHeightStandard));
            txtCapHeightTolerance.Text = objRL.Check_Null_String(Convert.ToString(objRL.CapHeightTolerance));
            txtCapHeightMinValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.CapHeightMinValue));
            txtCapHeightMaxValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.CapHeightMaxValue));

            txtInnerDepthStandard.Text = objRL.Check_Null_String(Convert.ToString(objRL.InnerDepthStandard));
            txtInnerDepthTolerance.Text = objRL.Check_Null_String(Convert.ToString(objRL.InnerDepthTolerance));
            txtInnerDepthMinValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.InnerDepthMinValue));
            txtInnerDepthMaxValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.InnerDepthMaxValue));

            txtCapWeightStandard.Text = objRL.Check_Null_String(Convert.ToString(objRL.CapWeightStandard));
            txtCapWeightTolerance.Text = objRL.Check_Null_String(Convert.ToString(objRL.CapWeightTolerance));
            txtCapWeightMinValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.CapWeightMinValue));
            txtCapWeightMaxValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.CapWeightMaxValue));
            btnExit.Focus();
        }
        private void CapTolerance_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
