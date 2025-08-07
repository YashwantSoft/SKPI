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

namespace SPApplication.Master
{
    public partial class WadMaster : Form
    {
        BusinessLayer objBL = new BusinessLayer();      //Own Developed Class
        ErrorProvider objEP = new ErrorProvider();      //System Class
        RedundancyLogics objRL = new RedundancyLogics();  //Own Developed Class
        DesignLayer objDL = new DesignLayer(); //Own Developed Class

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false;

        double RequiredValue = 0, DifferanceRatio = 0, MinValue = 0, MaxValue = 0;

        public WadMaster()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_WADMASTER);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
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
            else
                ClearAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void WadMaster_Load(object sender, EventArgs e)
        {
            ClearAll();
            //dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            //dataGridView1.GridColor = Color.Black;
            //dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            //dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            FillGrid();
            txtWadName.Focus();
        }

        private void ClearAll()
        {
            btnDelete.Enabled = false;
            Result = 0;
            FlagDelete = false;
            objEP.Clear();
            TableID = 0;
            txtWadName.Text = "";
            txtOuterDiaStandard.Text =""; 
            txtOuterDiaTolerance.Text =""; 
            txtOuterDiaMinValue.Text ="";  
            txtOuterDiaMaxValue.Text ="";  

            txtThicknessStandard.Text ="";  
            txtThicknessTolerance.Text ="";  
            txtThicknessMinValue.Text ="";  
            txtThicknessMaxValue.Text =""; 

            txtWeightStandard.Text =""; 
            txtWeightTolerance.Text ="";
            txtWeightMinValue.Text =""; 
            txtWeightMaxValue.Text =""; 

            txtAverageWeightStandard.Text =""; 
            txtAverageWeightTolerance.Text ="";
            txtAverageWeightMinValue.Text =""; 
            txtAverageWeightMaxValue.Text ="";
            cmbStatus.SelectedIndex = -1;
            txtRemarks.Text = "";
            txtWadName.Focus();
        }

        int Result = 0;
        string AposValue = string.Empty;

        protected void SaveDB()
        {
            Result = 0; AposValue = string.Empty;
            if (!Validation())
            {
                if (!CheckExist())
                {
                    AposValue = txtWadName.Text;
                    //'" + ItemName.Replace("'", "''") + "',

                    if (TableID != 0)
                        if (FlagDelete)
                            objBL.Query = "update WadMaster set CancelTag=1 where ID=" + TableID + "";
                        else
                            objBL.Query = "update WadMaster set WadName='" + AposValue.Replace("'", "''") + "',OuterDiaStandard='" + txtOuterDiaStandard.Text + "',OuterDiaTolerance='" + txtOuterDiaTolerance.Text + "',OuterDiaMinValue='" + txtOuterDiaMinValue.Text + "',OuterDiaMaxValue='" + txtOuterDiaMaxValue.Text + "',ThicknessStandard='" + txtThicknessStandard.Text + "',ThicknessTolerance='" + txtThicknessTolerance.Text + "',ThicknessMinValue='" + txtThicknessMinValue.Text + "',ThicknessMaxValue='" + txtThicknessMaxValue.Text + "',WeightStandard='" + txtWeightStandard.Text + "',WeightTolerance='" + txtWeightTolerance.Text + "',WeightMinValue='" + txtWeightMinValue.Text + "',WeightMaxValue='" + txtWeightMaxValue.Text + "',AverageWeightStandard='" + txtAverageWeightStandard.Text + "',AverageWeightTolerance='" + txtAverageWeightTolerance.Text + "',AverageWeightMinValue='" + txtAverageWeightMinValue.Text + "',AverageWeightMaxValue='" + txtAverageWeightMaxValue.Text + "',Status='" + cmbStatus.Text + "',Remarks='" + txtRemarks.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                    else
                        objBL.Query = "insert into WadMaster(WadName,OuterDiaStandard,OuterDiaTolerance,OuterDiaMinValue,OuterDiaMaxValue,ThicknessStandard,ThicknessTolerance,ThicknessMinValue,ThicknessMaxValue,WeightStandard,WeightTolerance,WeightMinValue,WeightMaxValue,AverageWeightStandard,AverageWeightTolerance,AverageWeightMinValue,AverageWeightMaxValue,Status,Remarks,UserId) values('" + AposValue.Replace("'", "''") + "','" + txtOuterDiaStandard.Text + "','" + txtOuterDiaTolerance.Text + "','" + txtOuterDiaMinValue.Text + "','" + txtOuterDiaMaxValue.Text + "','" + txtThicknessStandard.Text + "','" + txtThicknessTolerance.Text + "','" + txtThicknessMinValue.Text + "','" + txtThicknessMaxValue.Text + "','" + txtWeightStandard.Text + "','" + txtWeightTolerance.Text + "','" + txtWeightMinValue.Text + "','" + txtWeightMaxValue.Text + "','" + txtAverageWeightStandard.Text + "','" + txtAverageWeightTolerance.Text + "','" + txtAverageWeightMinValue.Text + "','" + txtAverageWeightMaxValue.Text + "','" + cmbStatus.Text + "','" + txtRemarks.Text + "'," + BusinessLayer.UserId_Static + ")";

                    Result = objBL.Function_ExecuteNonQuery();
                    if (Result > 0)
                    {
                        if (FlagDelete)
                            objRL.ShowMessage(9, 1);
                        else
                            objRL.ShowMessage(7, 1);

                        ClearAll();
                        FillGrid();
                    }
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

        protected bool Validation()
        {
            objEP.Clear();
            if (txtWadName.Text == "")
            {
                txtWadName.Focus();
                objEP.SetError(txtWadName, "Enter Wad Name");
                return true;
            }
            else if (cmbStatus.SelectedIndex == -1)
            {
                cmbStatus.Focus();
                objEP.SetError(cmbStatus, "Select Status");
                return true;
            }
            else
                return false;
        }
        
        string MainQuery = string.Empty, WhereClause = string.Empty, OrderByClause = string.Empty;

        protected void FillGrid()
        {
            MainQuery = string.Empty; WhereClause = string.Empty; OrderByClause = string.Empty;

            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();
             
            MainQuery = "select "+
                        "ID,"+
                        "WadName as [Wad Name],"+
                        "OuterDiaStandard as [Outer Dia Standard]," +
                        "OuterDiaTolerance as [Outer Dia Tolerance]," +
                        "OuterDiaMinValue as [Outer Dia Min Value]," +
                        "OuterDiaMaxValue," +
                        "ThicknessStandard as [Thickness Standard]," +
                        "ThicknessTolerance as [Thickness Tolerance]," +
                        "ThicknessMinValue as [Thickness Min Value]," +
                        "ThicknessMaxValue as [Thickness Max Value]," +
                        "WeightStandard as [Weight Standard]," +
                        "WeightTolerance as [Weight Tolerance]," +
                        "WeightMinValue as [Weight Min Value]," +
                        "WeightMaxValue as [WeightMax Value]," +
                        "AverageWeightStandard as [Average Weight Standard]," +
                        "AverageWeightTolerance as [Average Weight Tolerance]," +
                        "AverageWeightMinValue as [AverageWeight Min Value]," +
                        "AverageWeightMaxValue as [Average Weight Max Value]," +
                        "Status," +
                        "Remarks" +
                        " from WadMaster where CancelTag=0";

            if (SearchTag)
                if (!string.IsNullOrEmpty(Convert.ToString(txtSearch.Text)))
                    WhereClause = " and WadName like '%" + txtSearch.Text + "%'";

            OrderByClause = " order by WadName asc";

            objBL.Query = MainQuery + WhereClause + OrderByClause;

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 "ID," +
                //1 "WadName as [Wad Name]," +
                //2 "OuterDiaStandard as [Outer Dia Standard]," +
                //3 "OuterDiaTolerance as [Outer Dia Tolerance]," +
                //4 "OuterDiaMinValue as [Outer Dia Min Value]," +
                //5 "OuterDiaMaxValue," +
                //6 "ThicknessStandard as [Thickness Standard]," +
                //7 "ThicknessTolerance as [Thickness Tolerance]," +
                //8 "ThicknessMinValue as [Thickness Min Value]," +
                //9 "ThicknessMaxValue as [Thickness Max Value]," +
                //10 "WeightStandard as [Weight Standard]," +
                //11 "WeightTolerance as [Weight Tolerance]," +
                //12 "WeightMinValue as [Weight Min Value]," +
                //13 "WeightMaxValue as [WeightMax Value]," +
                //14 "AverageWeightStandard as [Average Weight Standard]," +
                //15 "AverageWeightTolerance as [Average Weight Tolerance]," +
                //16 "AverageWeightMinValue as [AverageWeight Min Value]," +
                //17 "AverageWeightMaxValue as [Average Weight Max Value]" +
                //18 "Status," +
                //19 "Note," +

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 400;
                //for (int i =2; i < dataGridView1.Columns.Count; i++)
                //{
                //    dataGridView1.Columns[i].Width = 100;
                //}
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from WadMaster where CancelTag=0 and WadName='" + txtWadName.Text + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

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

                    //0 "ID," +
                    //1 "WadName as [Wad Name]," +
                    //2 "OuterDiaStandard as [Outer Dia Standard]," +
                    //3 "OuterDiaTolerance as [Outer Dia Tolerance]," +
                    //4 "OuterDiaMinValue as [Outer Dia Min Value]," +
                    //5 "OuterDiaMaxValue," +
                    //6 "ThicknessStandard as [Thickness Standard]," +
                    //7 "ThicknessTolerance as [Thickness Tolerance]," +
                    //8 "ThicknessMinValue as [Thickness Min Value]," +
                    //9 "ThicknessMaxValue as [Thickness Max Value]," +
                    //10 "WeightStandard as [Weight Standard]," +
                    //11 "WeightTolerance as [Weight Tolerance]," +
                    //12 "WeightMinValue as [Weight Min Value]," +
                    //13 "WeightMaxValue as [WeightMax Value]," +
                    //14 "AverageWeightStandard as [Average Weight Standard]," +
                    //15 "AverageWeightTolerance as [Average Weight Tolerance]," +
                    //16 "AverageWeightMinValue as [AverageWeight Min Value]," +
                    //17 "AverageWeightMaxValue as [Average Weight Max Value]" +
                    //18 "Status," +
                    //19 "Note," +

                    TableID = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value)));
                    txtWadName.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value));

                    txtOuterDiaStandard.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[2].Value));
                    txtOuterDiaTolerance.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value));
                    txtOuterDiaMinValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[4].Value));
                    txtOuterDiaMaxValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[5].Value));

                    txtThicknessStandard.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[6].Value));
                    txtThicknessTolerance.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[7].Value));
                    txtThicknessMinValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[8].Value));
                    txtThicknessMaxValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[9].Value));
                    
                    txtWeightStandard.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[10].Value));
                    txtWeightTolerance.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[11].Value));
                    txtWeightMinValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[12].Value));
                    txtWeightMaxValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[13].Value));
                    
                    txtAverageWeightStandard.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[14].Value));
                    txtAverageWeightTolerance.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[15].Value));
                    txtAverageWeightMinValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[16].Value));
                    txtAverageWeightMaxValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[17].Value));

                    cmbStatus.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[18].Value));
                    txtRemarks.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[19].Value));
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

      

        public void CalculateValue(int CheckValue)
        {
            switch (CheckValue)
            {
                case 1:
                    SetValueMinMax(txtOuterDiaStandard, txtOuterDiaTolerance, txtOuterDiaMinValue, txtOuterDiaMaxValue);
                    break;
                case 2:
                    SetValueMinMax(txtThicknessStandard, txtThicknessTolerance, txtThicknessMinValue, txtThicknessMaxValue);
                    break;
                case 3:
                    SetValueMinMax(txtWeightStandard, txtWeightTolerance, txtWeightMinValue, txtWeightMaxValue);
                    break;
                case 4:
                    SetValueMinMax(txtAverageWeightStandard, txtAverageWeightTolerance, txtAverageWeightMinValue, txtAverageWeightMaxValue);
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
                MinValue = RequiredValue - DifferanceRatio;
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

        private void txtOuterDiaStandard_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(1);
        }

        private void txtOuterDiaTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(1);
        }

        private void txtThicknessStandard_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(2);
        }

        private void txtThicknessTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(2);
        }

        private void txtWeightStandard_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(3);
        }

        private void txtWeightTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(3);
        }

        private void txtAverageWeightStandard_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(4);
        }

        private void txtAverageWeightTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(4);
        }

        private void txtOuterDiaStandard_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtOuterDiaStandard);
        }

        private void txtOuterDiaTolerance_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtOuterDiaTolerance);
        }

        private void txtThicknessStandard_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtThicknessStandard);
        }

        private void txtThicknessTolerance_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtThicknessTolerance);
        }

        private void txtWeightStandard_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtWeightStandard);
        }

        private void txtWeightTolerance_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtWeightTolerance);
        }

        private void txtAverageWeightStandard_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtAverageWeightStandard);
        }

        private void txtAverageWeightTolerance_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtAverageWeightTolerance);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

        private void txtWadName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtOuterDiaStandard.Focus();
        }

        private void txtOuterDiaStandard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtOuterDiaTolerance.Focus();
        }

        private void txtOuterDiaTolerance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtThicknessStandard.Focus();
        }

        private void txtThicknessStandard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtThicknessTolerance.Focus();
        }

        private void txtThicknessTolerance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtWeightStandard.Focus();
        }

        private void txtWeightStandard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtWeightTolerance.Focus();
        }

        private void txtWeightTolerance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAverageWeightStandard.Focus();
        }

        private void txtAverageWeightStandard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAverageWeightTolerance.Focus();
        }

        private void txtAverageWeightTolerance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbStatus.Focus();
        }

        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRemarks.Focus();
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }
    }
}
