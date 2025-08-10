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
    public partial class WadQualityControl : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0, Result = 0;
        bool SearchTag = false;

        public WadQualityControl()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_WADQUALITYCONTROL);
            objRL.Fill_Supplier(cmbSupllier);
            objRL.Fill_Wad_ListBox(lbWad, txtSearchWad.Text, "All");
            objRL.Fill_Employee_By_Designation(cmbQCCheckerName, "Volume Checker");
            btnAddQCSpecs.BackColor = objDL.GetBackgroundColor();
            btnAddQCSpecs.ForeColor = objDL.GetForeColor();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSearchWad_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Wad();
            if (txtSearchWad.Text != "")
            {
                objRL.Fill_Wad_ListBox(lbWad, txtSearchWad.Text, "Text");
                //lbItem.Focus();
            }
            else
            {
                objRL.Fill_Wad_ListBox(lbWad, txtSearchWad.Text, "All");
            }
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("CapLabelEntry"));
            txtID.Text = IDNo.ToString();
        }

        int WadId = 0;
        private void ClearAll_Wad()
        {
            WadId = 0;
            lblWadName.Text = "";
        }


        private void WadQualityControl_Load(object sender, EventArgs e)
        {
            ClearAll();
            //FillGrid();
            lbWad.Focus();
        }

        private void ClearAll()
        {
            objEP.Clear();
            TableID = 0;
            FlagDelete = false;
            
            txtSearchWad.Text = "";
            lblWadName.Text = "";
             
            txtID.Text = "";
            GetID();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;
        bool DateFlag = false;
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
                UserClause = " and UserId = " + BusinessLayer.UserId_Static;
            else
                UserClause = string.Empty;

            MainQuery = "select ID,EntryDate as [Date],EntryTime as [Time],Shift,CapId,CapName as [Cap Name],WadId,WadName as [Wad Name],Qty,PONumber,WadFitter  as [Wad Fitter],BatchNumber  as [Batch No],StickerHeader as [Sticker Header] from CapLabelEntry where CancelTag=0 ";
            OrderByClause = " order by EntryDate desc";

            if (DateFlag)
                WhereClause = " and EntryDate between #" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            else if (SearchTag)
                WhereClause = " and CapName like '%" + txtSearch.Text + "%'";
            else if (IDFlag)
                WhereClause = " and ID=" + txtSearchID.Text + "";
            else
                WhereClause = string.Empty;

            objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 ID,
                //1 EntryDate as [Date],
                //2 EntryTime as [Time],
                //3 Shift,
                //4 CapId,
                //5 CapName as [Cap Name],
                //6 WadId,
                //7 WadName as [Wad Name],
                //8 Qty,
                //9 PONumber,
                //10 WadFitter  as [Wad Fitter],
                //11 BatchNumber  as [Batch No],
                //12 StickerHeader as [Sticker Header]
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[5].Width = 350;
                dataGridView1.Columns[7].Width = 350;
                dataGridView1.Columns[8].Width = 60;
                dataGridView1.Columns[10].Width = 200;
                dataGridView1.Columns[11].Width = 100;
                dataGridView1.Columns[12].Width = 150;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        private void cmbSupllier_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbSupllier.SelectedIndex > -1)
            {
            }
        }

        private void lbWad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Fill_Wad_Information();
            }
        }

        private void lbWad_Click(object sender, EventArgs e)
        {
            Fill_Wad_Information();
        }

        string WadName = string.Empty;
        private void Fill_Wad_Information()
        {
            if (TableID == 0)
                WadId = Convert.ToInt32(lbWad.SelectedValue);

            if (WadId != 0)
            {
                lblWadName.Text = "";
                objRL.Get_Wad_Records_By_Id(WadId);

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.WadName)))
                    WadName = objRL.WadName;

                lblWadName.Text = objRL.WadName.ToString();
                lblWadName.BackColor = Color.Yellow;
                txtInvoiceNumber.Focus();
            }
        }

        private void btnAddQCSpecs_Click(object sender, EventArgs e)
        {
            if (!ValidationMain())
            {

                gbValue.Visible = true;

                if (dgvValues.Rows.Count == 0)
                    dgvValues.Rows.Add();
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void cmbQCCheckerName_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        protected bool ValidationMain()
        {
            objEP.Clear();
            if (lblWadName.Text == "")
            {
                lblWadName.Focus();
                objEP.SetError(lblWadName, "Enter Wads Name");
                return true;
            }
            else if (WadId == 0)
            {
                lblWadName.Focus();
                objEP.SetError(lblWadName, "Enter Wads Name");
                return true;
            }
            else if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text)) //(txtInvoiceNumber.Text == "") 
            {
                txtInvoiceNumber.Focus();
                objEP.SetError(txtInvoiceNumber, "Enter Invoice Number");
                return true;
            }
            else if (cmbSupllier.SelectedIndex == -1)
            {
                cmbSupllier.Focus();
                objEP.SetError(cmbSupllier, "Enter Supllier");
                return true;
            }
            else if (cmbQCCheckerName.SelectedIndex == -1)
            {
                cmbQCCheckerName.Focus();
                objEP.SetError(cmbQCCheckerName, "Enter Supllier");
                return true;
            }
            else
                return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
        }

        int WadQualityControlId = 0;
        static int dgvRowIndex;

        string Type_I = string.Empty, CustmerLogo = string.Empty, PrintQuality = string.Empty, BoardThikness = string.Empty, BoardType = string.Empty, FoilThikness = string.Empty, FoilSpecs = string.Empty, SealantThikness = string.Empty, SealentSpecs = string.Empty, OuterDia = string.Empty, Thikness = string.Empty, Weight = string.Empty, AverageWeight = string.Empty, VisualAppearance = string.Empty, SideFinishing = string.Empty, Bend = string.Empty, FitmentWithCap = string.Empty, TopLoadTest = string.Empty, InkTest = string.Empty, IndSealTest = string.Empty;
        private void ClearGrid_Values()
        {
            WadQualityControlId = 0; Type_I = string.Empty; CustmerLogo = string.Empty; PrintQuality = string.Empty; BoardThikness = string.Empty; BoardType = string.Empty; FoilThikness = string.Empty; FoilSpecs = string.Empty; SealantThikness = string.Empty; SealentSpecs = string.Empty; OuterDia = string.Empty; Thikness = string.Empty; Weight = string.Empty; AverageWeight = string.Empty; VisualAppearance = string.Empty; SideFinishing = string.Empty; Bend = string.Empty; FitmentWithCap = string.Empty; TopLoadTest = string.Empty; InkTest = string.Empty; IndSealTest = string.Empty;
        }



        private void SaveDB()
        {
            if (!ValidationMain())
            {
                //Save CapQualityControl
                Result = 0;
                WadQualityControlId = 0;

                if (TableID == 0)
                    objBL.Query = "insert into WadQualityControl(EntryDate,EntryTime,WadId,InvoiceNumber,SupplierId,QCCheckerId,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "'," + WadId + ",'" + txtInvoiceNumber.Text + "'," + cmbSupllier.SelectedValue + "," + cmbQCCheckerName.SelectedValue + "," + BusinessLayer.UserId_Static + ") ";
                else
                {
                    if (!FlagDelete)
                        objBL.Query = "Update WadQualityControl set WadId=" + WadId + ",InvoiceNumber='" + txtInvoiceNumber.Text + "',SupplierId=" + cmbSupllier.SelectedValue + ",QCCheckerId=" + cmbQCCheckerName.SelectedValue + ",ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + " ";
                    else
                        objBL.Query = "Delete from WadQualityControl where ID=" + TableID + " ";
                }

                Result = objBL.Function_ExecuteNonQuery();

                if (Result > 0)
                {
                    if (TableID == 0)
                        TableID = objRL.ReturnMaxID_Fix("WadQualityControl", "ID");
                    else
                    {
                        if (FlagDelete)
                        {
                            objBL.Query = "Delete from WadQualityControlValues where ID=" + TableID + " ";
                            Result = objBL.Function_ExecuteNonQuery();
                        }
                    }

                    if (TableID > 0 && dgvValues.Rows.Count > 0 && !FlagDelete)
                    {
                        for (int i = 0; i < dgvValues.Rows.Count; i++)
                        {
                            ClearGrid_Values();
                            Type_I = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmType"].Value));
                            CustmerLogo = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmCustmerLogo"].Value));
                            PrintQuality = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmPrintQuality"].Value));
                            BoardThikness = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmBoardThikness"].Value));
                            BoardType = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmBoardType"].Value));
                            FoilThikness = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmFoilThikness"].Value));
                            FoilSpecs = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmFoilSpecs"].Value));
                            SealantThikness = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmSealantThikness"].Value));
                            SealentSpecs = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmSealentSpecs"].Value));
                            OuterDia = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmOuterDia"].Value));
                            Thikness = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmThikness"].Value));
                            Weight = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmWeight"].Value));
                            AverageWeight = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmAverageWeight"].Value));
                            VisualAppearance = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmVisualAppearance"].Value));
                            SideFinishing = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmSideFinishing"].Value));
                            Bend = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmBend"].Value));
                            FitmentWithCap = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmFitmentWithCap"].Value));
                            InkTest = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmInkTest"].Value));
                            IndSealTest = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmIndSealTest"].Value));

                            objBL.Query = "insert into WadQualityControlValues(EntryDate,EntryTime,WadId,WadQualityControlId,[Type],CustmerLogo,PrintQuality,BoardThikness,BoardType,FoilThikness,FoilSpecs,SealantThikness,SealentSpecs,OuterDia,Thikness,Weight,AverageWeight,VisualAppearance,SideFinishing,Bend,FitmentWithCap,TopLoadTest,InkTest,IndSealTest,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "'," + WadId + "," + TableID + ",'" + Type_I + "','" + CustmerLogo + "','" + PrintQuality + "','" + BoardThikness + "','" + BoardType + "','" + FoilThikness + "','" + FoilSpecs + "','" + SealantThikness + "','" + SealentSpecs + "','" + OuterDia + "','" + Thikness + "','" + Weight + "','" + AverageWeight + "','" + VisualAppearance + "','" + SideFinishing + "','" + Bend + "','" + FitmentWithCap + "','" + TopLoadTest + "','" + InkTest + "','" + IndSealTest + "'," + BusinessLayer.UserId_Static + ")";
                            Result = objBL.Function_ExecuteNonQuery();

                            if (Result > 0)
                                Result++;
                        }
                    }
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
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }
    }
}
