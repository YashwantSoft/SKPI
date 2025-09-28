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
    public partial class WadTolerance : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        public WadTolerance()
        {
            InitializeComponent();
            DesignForm();
        }

        public WadTolerance(int WadId)
        {
            InitializeComponent();
            DesignForm();
            objRL.Get_Wad_Records_By_Id(WadId);
            SetValues();
        }

        private void WadTolerance_Load(object sender, EventArgs e)
        {

        }

        private void DesignForm()
        {
            lblHeader.BackColor = objDL.GetBackgroundColor();
            lblHeader.ForeColor = objDL.GetForeColor();
            lblHeader.Text = BusinessResources.LBL_HEADER_WADTOLLARANCEVALUE1;
            objDL.SetButtonDesign(btnExit, BusinessResources.BTN_EXIT);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void SetValues()
        {
            txtWadName.Text = objRL.Check_Null_String(Convert.ToString(objRL.WadName));
            cmbWadType.Text = objRL.Check_Null_String(Convert.ToString(objRL.WadType));
            cmbCustomerLogo.Text = objRL.Check_Null_String(Convert.ToString(objRL.CustomerLogo));
            txtBoardThikness.Text = objRL.Check_Null_String(Convert.ToString(objRL.BoardThickness));
            cmbBoardType.Text = objRL.Check_Null_String(Convert.ToString(objRL.BoardType));
            txtFoilThikness.Text = objRL.Check_Null_String(Convert.ToString(objRL.FoilThickness));
            cmbFoilSpecs.Text = objRL.Check_Null_String(Convert.ToString(objRL.FoilSpecs));
            txtSealantThikness.Text = objRL.Check_Null_String(Convert.ToString(objRL.SealantThickness));
            cmbSealentSpecs.Text = objRL.Check_Null_String(Convert.ToString(objRL.SealentSpecs));

            txtOuterDiaStandard.Text = objRL.Check_Null_String(Convert.ToString(objRL.OuterDiaStandard));
            txtOuterDiaTolerance.Text = objRL.Check_Null_String(Convert.ToString(objRL.OuterDiaTolerance));
            txtOuterDiaMinValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.OuterDiaMinValue));
            txtOuterDiaMaxValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.OuterDiaMaxValue));

            txtThicknessStandard.Text = objRL.Check_Null_String(Convert.ToString(objRL.ThicknessStandard));
            txtThicknessTolerance.Text = objRL.Check_Null_String(Convert.ToString(objRL.ThicknessTolerance));
            txtThicknessMinValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.ThicknessMinValue));
            txtThicknessMaxValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.ThicknessMaxValue));

            txtWeightStandard.Text = objRL.Check_Null_String(Convert.ToString(objRL.WeightStandard));
            txtWeightTolerance.Text = objRL.Check_Null_String(Convert.ToString(objRL.WeightTolerance));
            txtWeightMinValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.WeightMinValue));
            txtWeightMaxValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.WeightMaxValue));

            txtAverageWeightStandard.Text = objRL.Check_Null_String(Convert.ToString(objRL.AverageWeightStandard));
            txtAverageWeightTolerance.Text = objRL.Check_Null_String(Convert.ToString(objRL.AverageWeightTolerance));
            txtAverageWeightMinValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.AverageWeightMinValue));
            txtAverageWeightMaxValue.Text = objRL.Check_Null_String(Convert.ToString(objRL.AverageWeightMaxValue));
            btnExit.Focus();
        }
    }
}
