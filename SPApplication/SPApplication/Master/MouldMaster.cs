using BusinessLayerUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPApplication.Master
{
    public partial class MouldMaster : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAddCollectionType_Click(object sender, EventArgs e)
        {
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
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

        private void ClearAll()
        {
            objEP.Clear();
            TableID = 0;
            txtMouldNo.Text = "";
            txtSrNo.Text = "";
            txtNeck.Text = "";
            txtTillColarFreshBlow.Text = "";
            txtOfcFreshBlow.Text = "";
            txtTillColarFinal.Text = "";
            txtOfcFinal.Text = "";
            txtDrawingNo.Text = "";
            cmbAutoSemi.SelectedIndex = -1;
            cmbMaterial.SelectedIndex = -1;
            cmbCavity.SelectedIndex = -1;
            txtHeight.Text = "";
            txtLabelSpace.Text = "";
            txtLebalOD.Text = "";
            txtNickName.Text = "";
            cmbCustomer.SelectedIndex = -1;
            txtTallyName.Text = "";
            cmbRepairing.SelectedIndex = -1;
            txtExtraBrushes.Text = "";
            cmbExtraAccessories.SelectedIndex = -1;
            cmbCurrentStatus.SelectedIndex = -1;
            txtSearch.Text = "";
            Fill_MouldNumber();
            txtSrNo.Focus();
        }

        string PartyName = string.Empty;
        protected void SaveDB()
        {
            PartyName = string.Empty;

            PartyName = cmbCustomer.Text;

            if (TableID != 0)
                if (FlagDelete)
                    objBL.Query = "update MouldMaster set CancelTag=1,ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                else
                    objBL.Query = "update MouldMaster set SrNo='" + txtSrNo.Text + "',Neck='" + txtNeck.Text + "',TillColarFreshBlow='" + txtTillColarFreshBlow.Text + "',OfcFreshBlow='" + txtOfcFreshBlow.Text + "',TillColarFinal='" + txtTillColarFinal.Text + "',OfcFinal='" + txtOfcFinal.Text + "',DrawingNo='" + txtDrawingNo.Text + "',AutoSemi='" + cmbAutoSemi.Text + "',Material='" + cmbMaterial.Text + "',Cavity='" + cmbCavity.Text + "',Height='" + txtHeight.Text + "',LebalSpace='" + txtLabelSpace.Text + "',LebalOD='" + txtLebalOD.Text + "',NickName='" + txtNickName.Text + "',Party='" + PartyName.Replace("'", "''") + "',TallyName='" + txtTallyName.Text + "',Repairing='" + cmbRepairing.Text + "',ExtraBrushes='" + txtExtraBrushes.Text + "',ExtraAccessories='" + cmbExtraAccessories.Text + "',CurrentStatus='" + cmbCurrentStatus.Text + "',ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
            else
                objBL.Query = "insert into MouldMaster(SrNo,Neck,TillColarFreshBlow,OfcFreshBlow,TillColarFinal,OfcFinal,DrawingNo,AutoSemi,Material,Cavity,Height,LebalSpace,LebalOD,NickName,Party,TallyName,Repairing,ExtraBrushes,ExtraAccessories,CurrentStatus,UserId) values('" + txtSrNo.Text + "','" + txtNeck.Text + "','" + txtTillColarFreshBlow.Text + "','" + txtOfcFreshBlow.Text + "','" + txtTillColarFinal.Text + "','" + txtOfcFinal.Text + "','" + txtDrawingNo.Text + "','" + cmbAutoSemi.Text + "','" + cmbMaterial.Text + "','" + cmbCavity.Text + "','" + txtHeight.Text + "','" + txtLabelSpace.Text + "','" + txtLebalOD.Text + "','" + txtNickName.Text + "','" + PartyName.Replace("'", "''") + "','" + txtTallyName.Text + "','" + cmbRepairing.Text + "','" + txtExtraBrushes.Text + "','" + cmbExtraAccessories.Text + "','" + cmbCurrentStatus.Text + "'," + BusinessLayer.UserId_Static + ")";

            objBL.Function_ExecuteNonQuery();
        }

        protected bool Validation()
        {
            objEP.Clear();
            if (txtMouldNo.Text == "")
            {
                txtMouldNo.Focus();
                objEP.SetError(txtMouldNo, "Enter Mould No");
                return true;
            }
            else if (txtSrNo.Text == "")
            {
                txtSrNo.Focus();
                objEP.SetError(txtSrNo, "Enter Sr No");
                return true;
            }
            //else if(txtNeck.Text == "")
            //{
            //    txtNeck.Focus();
            //    objEP.SetError(txtNeck, "Enter Neck");
            //    return true;
            //}
            //else if (txtTillColarFreshBlow.Text == "")
            //{
            //    txtSrNo.Focus();
            //    objEP.SetError(txtTillColarFreshBlow, "Enter Till Colar Fresh Blow");
            //    return true;
            //}
            //else if(txtOfcFreshBlow.Text == "")
            //{
            //    txtOfcFreshBlow.Focus();
            //    objEP.SetError(txtOfcFreshBlow, "Enter Ofc Fresh Blow");
            //    return true;
            //}
            //else if (txtTillColarFinal.Text == "")
            //{
            //    txtTillColarFinal.Focus();
            //    objEP.SetError(txtTillColarFinal, "Enter Till Colar Final");
            //    return true;
            //}
            //else if (txtOfcFinal.Text == "")
            //{
            //    txtOfcFinal.Focus();
            //    objEP.SetError(txtOfcFinal, "Enter Ofc Final");
            //    return true;
            //}
            //else if (txtDrawingNo.Text == "")
            //{
            //    txtDrawingNo.Focus();
            //    objEP.SetError(txtDrawingNo, "Enter Drawing No");
            //    return true;
            //}
            else if (cmbAutoSemi.SelectedIndex  == -1)
            {
                cmbAutoSemi.Focus();
                objEP.SetError(cmbAutoSemi, "Select AutoSemi");
                return true;
            }
            else if (cmbCavity.SelectedIndex == -1)
            {
                cmbCavity.Focus();
                objEP.SetError(cmbCavity, "Select Cavity");
                return true;
            }
            //else if (txtHeight.Text == "")
            //{
            //    txtHeight.Focus();
            //    objEP.SetError(txtHeight, "Enter Height");
            //    return true;
            //}
            //else if (txtLabelSpace.Text == "")
            //{
            //    txtLabelSpace.Focus();
            //    objEP.SetError(txtLabelSpace, "Enter Label Space");
            //    return true;
            //}
            //else if (txtLebalOD.Text == "")
            //{
            //    txtLebalOD.Focus();
            //    objEP.SetError(txtLebalOD, "Enter Lebal OD");
            //    return true;
            //}
            //else if (txtNickName.Text == "")
            //{
            //    txtNickName.Focus();
            //    objEP.SetError(txtNickName, "Enter Nick Name");
            //    return true;
            //}
            else if (cmbCustomer.SelectedIndex == -1)
            {
                cmbCustomer.Focus();
                objEP.SetError(cmbCustomer, "Select Party");
                return true;
            }
            else if (txtTallyName.Text == "")
            {
                txtTallyName.Focus();
                objEP.SetError(txtTallyName, "Enter Tally Name");
                return true;
            }
            else if (cmbRepairing.SelectedIndex == -1)
            {
                cmbRepairing.Focus();
                objEP.SetError(cmbRepairing, "Select Repairing");
                return true;
            }
            else if (txtExtraBrushes.Text == "")
            {
                txtExtraBrushes.Focus();
                objEP.SetError(txtExtraBrushes, "Enter Extra Brushes");
                return true;
            }
            else if (cmbExtraAccessories.SelectedIndex == -1)
            {
                cmbExtraAccessories.Focus();
                objEP.SetError(cmbExtraAccessories, "Select Extra Accessories");
                return true;
            }
            else if (cmbCurrentStatus.SelectedIndex == -1)
            {
                cmbCurrentStatus.Focus();
                objEP.SetError(cmbCurrentStatus, "Select Current Status");
                return true;
            }
            else if (cmbMaterial.SelectedIndex == -1)
            {
                cmbMaterial.Focus();
                objEP.SetError(cmbMaterial, "Select Shed");
                return true;
            }
           
            else
                return false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                DialogResult dr;
                dr = MessageBox.Show("Do yo want to delete this record?", "Delete Record", MessageBoxButtons.YesNo);

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
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    txtMouldNo.Text = TableID.ToString();
                    txtSrNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(); 
                    txtNeck.Text  = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();  
                    txtTillColarFreshBlow.Text  = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();  
                    txtOfcFreshBlow.Text  = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();  
                    txtTillColarFinal.Text  = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();  
                    txtOfcFinal.Text  = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();  
                    txtDrawingNo.Text  = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();  
                    cmbAutoSemi.Text  = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    cmbMaterial.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    cmbCavity.Text  = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();  
                    txtHeight.Text  = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();  
                    txtLabelSpace.Text  = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();  
                    txtLebalOD.Text  = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();  
                    txtNickName.Text  = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();  
                    cmbCustomer.Text  = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();  
                    txtTallyName.Text  = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();  
                    cmbRepairing.Text  = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();  
                    txtExtraBrushes.Text  = dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString();  
                    cmbExtraAccessories.Text  = dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString();  
                    cmbCurrentStatus.Text  = dataGridView1.Rows[e.RowIndex].Cells[20].Value.ToString();  
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

        public MouldMaster()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_MOULDMASTER);
            objRL.Fill_Customer(cmbCustomer);
        }

        private void MouldMaster_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            txtSrNo.Focus();
        }

        private void Fill_MouldNumber()
        {
           txtMouldNo.Text= Convert.ToString(objRL.ReturnMaxID("MouldMaster"));
        }
        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (!SearchTag)
                objBL.Query = "select ID,SrNo,Neck,TillColarFreshBlow,OfcFreshBlow,TillColarFinal,OfcFinal,DrawingNo,AutoSemi,Material,Cavity,Height,LebalSpace,LebalOD,NickName,Party,TallyName,Repairing,ExtraBrushes,ExtraAccessories,CurrentStatus,UserId from MouldMaster where CancelTag=0 order by ID";
            else
                objBL.Query = "select ID,SrNo,Neck,TillColarFreshBlow,OfcFreshBlow,TillColarFinal,OfcFinal,DrawingNo,AutoSemi,Material,Cavity,Height,LebalSpace,LebalOD,NickName,Party,TallyName,Repairing,ExtraBrushes,ExtraAccessories,CurrentStatus,UserId from MouldMaster where CancelTag=0 and SrNo like '%" + txtSearch.Text + "%'";

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 150;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }
        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from MouldMaster where CancelTag=0 and SrNo='" + txtSrNo.Text + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}
