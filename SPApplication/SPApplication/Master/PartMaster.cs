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
    public partial class PartMaster : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false;

        public PartMaster()
        {
            InitializeComponent();
            Set_Design();
            objRL.Fill_Users(cmbDepartment);
            objRL.Fill_Supplier(cmbSupplierName);
            objRL.Fill_UOM(cmbUnit);
            objRL.Fill_UserFor(cmbUseFor);
            objRL.Fill_PlaceMaster(cmbPlace);
            Set_Department();
        }

        private void Set_Department()
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_AJAY)
            {
                //cmbDepartmentSearch.Text = BusinessLayer.UserName_Static.ToString();
                cmbDepartment.Enabled = true;
            }
            else
            {
                cmbDepartment.Enabled = false;
                cmbDepartment.Text = BusinessLayer.UserName_Static.ToString();
            }
        }

        private void Set_Design()
        {
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_PARTEQUIPMENT);
            objDL.SetPlusButtonDesign(btnAddUsedFor);
            objDL.SetPlusButtonDesign(btnAddUOM);
            objDL.SetPlusButtonDesign(btnAddSupplierName);
            objDL.SetPlusButtonDesign(btnAddPlace);
            //Asign Label
            //lblCategory.Text = BusinessResources.LBL_CATEGORY;
            //lblItemName.Text = BusinessResources.LBL_ITEM_NAME;
            //lblHSNCode.Text = BusinessResources.LBL_HSN_CODE;
            dataGridView1.DefaultCellStyle.SelectionBackColor = objBL.GetRowSelectionColor();
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("PartMaster"));
            txtID.Text = IDNo.ToString();
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            cmbUseFor.Focus();
        }

       
        protected void FillGrid()
        {
            if (cmbDepartment.SelectedIndex > -1)
            {
                DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue);

                dataGridView1.DataSource = null;
                DataSet ds = new DataSet();

                if (!SearchTag)
                    objBL.Query = "select PM.ID,PM.DepartmentId,L.UserName as [Department],PM.PartName as [Part Name],PM.Description,PM.UomId,U.UnitOfMessurement as [Unit],PM.SupplierId,S.SupplierName as [Supplier Name],PM.HSNCode as [HSN Code],PM.UsedForId,UFM.UserFor as [Used For],PM.PlaceId,PLM.PlaceName as [Place],PM.OpeningStock,PM.Status from (((((PartMaster PM inner join Login L on L.ID=PM.DepartmentId) inner join UOM U on U.ID=PM.UomId) inner join Supplier S on S.ID=PM.SupplierId) inner join UseForMaster UFM on UFM.ID=PM.UsedForId) inner join PlaceMaster PLM on PLM.ID=PM.PlaceId) where L.CancelTag=0 and PM.CancelTag=0 and U.CancelTag=0 and S.CancelTag=0 and UFM.CancelTag=0 and PLM.CancelTag=0 and PM.DepartmentId="+DepartmentId+"";
                else
                    objBL.Query = "select PM.ID,PM.DepartmentId,L.UserName as [Department],PM.PartName as [Part Name],PM.Description,PM.UomId,U.UnitOfMessurement as [Unit],PM.SupplierId,S.SupplierName as [Supplier Name],PM.HSNCode as [HSN Code],PM.UsedForId,UFM.UserFor as [Used For],PM.PlaceId,PLM.PlaceName as [Place],PM.OpeningStock,PM.Status from (((((PartMaster PM inner join Login L on L.ID=PM.DepartmentId) inner join UOM U on U.ID=PM.UomId) inner join Supplier S on S.ID=PM.SupplierId) inner join UseForMaster UFM on UFM.ID=PM.UsedForId) inner join PlaceMaster PLM on PLM.ID=PM.PlaceId) where L.CancelTag=0 and PM.CancelTag=0 and U.CancelTag=0 and S.CancelTag=0 and UFM.CancelTag=0 and PLM.CancelTag=0 and PM.DepartmentId=" + DepartmentId + " and PM.PartName like '%" + txtSearch.Text + "%'";

                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //0 PM.ID,
                    //1 ,PM.DepartmentId
                    //2 L.UserName as [Department]
                    //3 PM.PartName as [Part Name],
                    //4 PM.Description
                    //5 PM.UomId,
                    //6 U.UnitOfMessurement as [Unit],
                    //7 PM.SupplierId,
                    //8 S.SupplierName as [Supplier Name],
                    //9 PM.HSNCode as [HSN Code],
                    //10 PM.UsedForId,
                    //11 UFM.UserFor as [Used For],
                    //12 PM.PlaceId,
                    //13 PLM.PlaceName as [Place],
                    //14 PM.OpeningStock
                    //15 PM.Status

                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[7].Visible = false;
                    dataGridView1.Columns[12].Visible = false;
                    dataGridView1.Columns[10].Visible = false;

                    dataGridView1.Columns[1].Width = 100;
                    dataGridView1.Columns[3].Width = 200;
                    dataGridView1.Columns[4].Width = 100;
                    dataGridView1.Columns[6].Width = 80;
                    dataGridView1.Columns[8].Width = 150;
                    dataGridView1.Columns[9].Width = 120;
                    dataGridView1.Columns[11].Width = 120;
                    dataGridView1.Columns[13].Width = 120;
                    dataGridView1.Columns[14].Width = 120;
                    dataGridView1.Columns[15].Width = 120;
                    lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                }
            }
        }

        int ManufractureId = 0, DepartmentId = 0;
        private void Get_DepartmentId()
        {
            if (cmbDepartment.SelectedIndex > -1)
            {
                DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue);
            }
        }

        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from PartMaster where CancelTag=0 and PartName='" + txtPartName.Text + "' and DepartmentId="+DepartmentId+" and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
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
                dr = MessageBox.Show("Do yo want to delete this record", "Delete Record", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    FlagDelete = true;
                    SaveDB();
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

        private void SaveDB()
        {
            if (!Validation())
            {
                if (!CheckExist())
                {
                    Get_DepartmentId();

                    if (TableID != 0)
                        if (FlagDelete)
                            objBL.Query = "update PartMaster set CancelTag=1 where ID=" + TableID + "";
                        else
                            objBL.Query = "update PartMaster set DepartmentId=" + DepartmentId + ",PartName='" + objRL.ApostropheSave(txtPartName.Text) + "',Description='" + objRL.ApostropheSave(txtDescription.Text) + "',UomId=" + cmbUnit.SelectedValue + ", SupplierId=" + cmbSupplierName.SelectedValue + ",HSNCode='" + txtHSNCode.Text + "',UsedForId=" + cmbUseFor.SelectedValue + ",PlaceId=" + cmbPlace.SelectedValue + ",Status='"+cmbStatus.Text+"',ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                    else
                        objBL.Query = "insert into PartMaster(DepartmentId,PartName,Description,UomId,SupplierId,HSNCode,UsedForId,PlaceId,OpeningStock,Status,UserId) values(" + DepartmentId + ", '" + objRL.ApostropheSave(txtPartName.Text) + "' ,'" + objRL.ApostropheSave(txtDescription.Text) + "' ," + cmbUnit.SelectedValue + ", " + cmbSupplierName.SelectedValue + ",'" + txtHSNCode.Text + "'," + cmbUseFor.SelectedValue + "," + cmbPlace.SelectedValue + ",'" + txtOpeningStock.Text + "','" + cmbStatus.Text + "' ," + BusinessLayer.UserId_Static + ")";

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

            if (txtID.Text == "")
            {
                txtID.Focus();
                objEP.SetError(txtID, "Select Group Name");
                return true;
            }
            else if (txtPartName.Text == "")
            {
                txtPartName.Focus();
                objEP.SetError(txtPartName, "Enter PartMaster Name");
                return true;
            }
            else if (cmbUnit.SelectedIndex == -1)
            {
                cmbUnit.Focus();
                objEP.SetError(cmbUnit, "Select Unit");
                return true;
            }
            else if (cmbSupplierName.SelectedIndex == -1)
            {
                cmbSupplierName.Focus();
                objEP.SetError(cmbSupplierName, "Select Supplier Name");
                return true;
            }
            else if (cmbUseFor.SelectedIndex == -1)
            {
                cmbUseFor.Focus();
                objEP.SetError(cmbUseFor, "Select Use For");
                return true;
            }
            else if (cmbPlace.SelectedIndex == -1)
            {
                cmbPlace.Focus();
                objEP.SetError(cmbPlace, "Select Place");
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
            txtID.Text = "";
            txtPartName.Text = "";
            cmbUnit.SelectedIndex = -1;
            cmbSupplierName.SelectedIndex = -1;
            txtHSNCode.Text = "";
            cmbUseFor.SelectedIndex = -1;
            cmbPlace.SelectedIndex = -1;
            txtOpeningStock.Text = "";
            GetID();
            Set_Department();
            cmbUseFor.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RowCount_Grid = dataGridView1.Rows.Count;
                CurrentRowIndex = dataGridView1.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    //0 PM.ID,
                    //1 ,PM.DepartmentId
                    //2 L.UserName as [Department]
                    //3 PM.PartName as [Part Name],
                    //4 PM.Description
                    //5 PM.UomId,
                    //6 U.UnitOfMessurement as [Unit],
                    //7 PM.SupplierId,
                    //8 S.SupplierName as [Supplier Name],
                    //9 PM.HSNCode as [HSN Code],
                    //10 PM.UsedForId,
                    //11 UFM.UserFor as [Used For],
                    //12 PM.PlaceId,
                    //13 PLM.PlaceName as [Place],
                    //14 PM.OpeningStock
                    //15 PM.Status

                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    DepartmentId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    cmbDepartment.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtPartName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    cmbUnit.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    cmbSupplierName.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    txtHSNCode.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    cmbUseFor.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    cmbPlace.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                    txtOpeningStock.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                    cmbStatus.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
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
        
        private void cmbManufracture_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbUseFor.SelectedIndex > -1)
                ManufractureId = Convert.ToInt32(cmbUseFor.SelectedValue);
        }

        private void btnAddUsedFor_Click(object sender, EventArgs e)
        {
            UsedForMaster objForm = new UsedForMaster();
            objForm.ShowDialog(this);
            objRL.Fill_UserFor(cmbUseFor);
        }

        private void btnAddUOM_Click(object sender, EventArgs e)
        {
            UnitOfMessurement objForm = new UnitOfMessurement();
            objForm.ShowDialog(this);
            objRL.Fill_UOM(cmbUnit);
            cmbUnit.Focus();
        }

        private void btnAddSupplierName_Click(object sender, EventArgs e)
        {
            Supplier objForm = new Supplier();
            objForm.ShowDialog(this);
            objRL.Fill_Supplier(cmbSupplierName);
        }

        private void btnAddPlace_Click(object sender, EventArgs e)
        {
            PlaceMaster objForm = new PlaceMaster();
            objForm.ShowDialog(this);
            objRL.Fill_PlaceMaster(cmbPlace);
        }

        private void txtOpeningStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtOpeningStock);
        }

        private void cmbDepartment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Get_DepartmentId();
        }

        private void cmbDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPartName.Focus();
        }

        private void txtPartName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDescription.Focus();
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbUnit.Focus();
        }

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbSupplierName.Focus();
        }

        private void cmbSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtHSNCode.Focus();
        }

        private void txtHSNCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbUseFor.Focus();
        }

        private void cmbUseFor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbPlace.Focus();
        }

        private void cmbPlace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtOpeningStock.Focus();
        }

        private void txtOpeningStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbStatus.Focus();
        }

        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }
    }
}
