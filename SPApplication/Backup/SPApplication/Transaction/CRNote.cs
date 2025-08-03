using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace SPApplication.Transaction
{
    public partial class CRNote : Form
    {
        public CRNote()
        {
            InitializeComponent();
            objBL.Set_Design_All(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_CRNOTE);
        }

        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        private void CRNote_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            objRL.Fill_Supplier(cmbSupplierName);
            cmbSupplierName.SelectedIndex = -1;
            
        }

        public void ClearAll()
        {
            dtpDate.Value = DateTime.Now.Date;
            checkBox1.Checked = false;
            cbPurchase.Checked = false;
            cmbSupplierName.SelectedIndex = -1;
            dgvItem.DataSource = null;
            txtDescription.Text = "";
            dgvItem.Rows.Clear();
            txtCRNo.Text = Convert.ToString(Convert.ToInt32(objRL.ReturnMaxID("CRNote")));
            
        }
        int SupplierID = 0;
        protected bool Validation()
        {
            objEP.Clear();
            if (cbPurchase.Checked == false)
            {
                if (cmbSupplierName.SelectedIndex == -1)
                {
                    objEP.SetError(cmbSupplierName, "Select Supplier Name");
                    cmbSupplierName.Focus();
                    return true;
                }
                else
                    return false;
            }
            else if (cbPurchase.Checked == true)
            {
                if (txtDescription.Text == "")
                {
                    objEP.SetError(txtDescription, "Enter Description");
                    txtDescription.Focus();
                    return true;
                }
                else
                    return false;
            }
            else if (dgvItem.Rows.Count==0)
            {
                objEP.SetError(dgvItem, "Select Item from Grid");
                dgvItem.Focus();
                return true;
            }
            else
                return false;
        }

        string PurchaseNo = "", ChallanNo = "", BillNo = "", NetTotal = "", TotalGST = "", FreightCharges = "", LoadingAndPackingCharges = "", InsuranceCharges = "", OtherCharge = "", InvoiceTotal = "";
        int TableId = 0;
        bool DeleteFlag = false;
        protected void SaveDB()
        {
            if (!Validation())
            {
                if (TableId != 0)
                {
                    if (DeleteFlag == false)
                    {
                        objBL.Query = "update CRNote set CRDate='" + dtpDate.Value.ToString("MM/dd/yyyy") + "',IsPurchase='" + IsPurchase + "',SupplierId='" + SupplierID + "',Description='" + txtDescription.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableId + " and CancelTag=0 ";
                    }
                    else
                        objBL.Query = "update CRNote set CancelTag=1 where ID=" + TableId + " and CancelTag=0 ";
                }
                else
                {
                    objBL.Query = "insert into CRNote(CRNo,CRDate,IsPurchase,SupplierId,Description,UserId) values('" + txtCRNo.Text + "','" + dtpDate.Value.ToString("MM/dd/yyyy") + "','" + IsPurchase + "'," + SupplierID + ",'" + txtDescription.Text + "'," + BusinessLayer.UserId_Static + ") ";
                }
                objBL.Function_ExecuteNonQuery();

                if (TableId == 0)
                {
                    objBL.Query = "select max(ID) from CRNote where CancelTag=0";
                    DataSet ds = new DataSet();
                    ds = objBL.ReturnDataSet();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string AB = ds.Tables[0].Rows[0][0].ToString();
                        if (AB != "")
                            TableId = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                        else
                            TableId = 1;
                    }
                }
                else
                {
                    objBL.Query = "delete from CRNoteTransaction where CRNoteId=" + TableId + "";
                    objBL.Function_ExecuteNonQuery();
                }

                if (TableId != 0)
                {
                    SaveItemList();
                    objRL.ShowMessage(7, 1);
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

        private void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();
            objBL.Query = "select C.ID,C.CRNo,C.CRDate,C.IsPurchase,C.SupplierId,C.Description,I.ItemName,CN.HSNCode,CN.UOM,CN.AQty,CN.RQty from ((CRNote C inner join CRNoteTransaction CN on C.ID=CN.CRNoteId) inner join Item I on I.ID=CN.ItemId) where C.CancelTag=0 and CN.CancelTag=0 and I.CancelTag=0 and C.CRDate=#" + dtpDate.Value.ToString("dd/MM/yyyy") + "#";
            
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                
                dataGridView1.Columns[1].Width = 60;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 60;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[6].Width = 120;
                dataGridView1.Columns[7].Width = 60;
                dataGridView1.Columns[8].Width = 60;
                dataGridView1.Columns[9].Width = 60;
                dataGridView1.Columns[10].Width = 60;
               
                this.dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        protected void SaveItemList()
        {
            for (int i = 0; i < dgvItem.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvItem.Rows[i].Cells["chkItem"].Value) == true)
                {
                    if (DeleteFlag == false)
                    {
                        objBL.Query = "insert into CRNoteTransaction (CRNoteId,ItemId,HSNCode,UOM,AQty,RQty,UserId) values(" + TableId + "," + dgvItem.Rows[i].Cells["clmItemId"].Value.ToString() + ",'" + dgvItem.Rows[i].Cells["clmHSNCode"].Value.ToString() + "','" + dgvItem.Rows[i].Cells["clmUOM"].Value.ToString() + "','" + dgvItem.Rows[i].Cells["clmAQty"].Value.ToString() + "','" + dgvItem.Rows[i].Cells["clmReturnQty"].Value.ToString() + "'," + BusinessLayer.UserId_Static + ")";
                        objBL.Function_ExecuteNonQuery();
                    }
                }
            }
        }

        string IsPurchase = "";
        private void cbPurchase_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPurchase.Checked == false)
            {
                cmbSupplierName.Enabled = false;
                cmbSupplierName.SelectedIndex = -1;
                dgvItem.DataSource = null;
                dgvItemRow = 0;
                dgvItem.Rows.Clear();
                FilldgvGrid();
                IsPurchase = "No";
            }
            else
            {
                cmbSupplierName.Enabled = true;
                dgvItem.DataSource = null;
                dgvItem.Rows.Clear();
                dgvItemRow = 0;
                IsPurchase = "Yes";
                
            }
        }
        static int dgvItemRow;
        private void FilldgvGrid()
        {
            dgvItem.DataSource = null;
            DataSet ds = new DataSet();
            if(cbPurchase.Checked==true)
                objBL.Query = "select P.ID,P.ItemId,P.Quantity,P.UOM,P.Cost,P.CGSTAmount,P.SGSTAmount,P.IGSTAmount,P.CGSTTax,P.SGSTTax,P.IGSTTax,P.TotalTaxAmount,C.CompanyName,C.ItemName,C.ItemCode,C.HSNCode,S.AvailableQty from (((PurchaseTransaction P inner join Item C on C.ID=P.ItemId) inner join Purchase DC on P.PurchaseID=DC.ID) inner join ItemStock S on S.ItemId=C.ID) where S.CancelTag=0 and P.CancelTag=0 and C.CancelTag=0 and DC.CancelTag=0 and DC.SupplierId=" + cmbSupplierName.SelectedValue + "";
            else
                objBL.Query = "select P.ID,P.ItemId,P.Quantity,P.UOM,P.Cost,P.CGSTAmount,P.SGSTAmount,P.IGSTAmount,P.CGSTTax,P.SGSTTax,P.IGSTTax,P.TotalTaxAmount,C.CompanyName,C.ItemName,C.ItemCode,C.HSNCode,S.AvailableQty from (((PurchaseTransaction P inner join Item C on C.ID=P.ItemId) inner join Purchase DC on P.PurchaseID=DC.ID)  inner join ItemStock S on S.ItemId=C.ID) where S.CancelTag=0 and P.CancelTag=0 and C.CancelTag=0 and DC.CancelTag=0";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgvItem.Rows.Add();
                    dgvItem.Rows[dgvItemRow].Cells["clmItemId"].Value = ds.Tables[0].Rows[i]["ItemId"].ToString();
                    dgvItem.Rows[dgvItemRow].Cells["clmItemName"].Value = ds.Tables[0].Rows[i]["ItemName"].ToString();
                    dgvItem.Rows[dgvItemRow].Cells["clmUOM"].Value = ds.Tables[0].Rows[i]["UOM"].ToString();
                    dgvItem.Rows[dgvItemRow].Cells["clmAQty"].Value = ds.Tables[0].Rows[i]["AvailableQty"].ToString();
                    dgvItem.Rows[dgvItemRow].Cells["clmReturnQty"].Value = "";
                    dgvItem.Rows[dgvItemRow].Cells["clmHSNCode"].Value = ds.Tables[0].Rows[i]["HSNCode"].ToString();
                    dgvItemRow++;
                }
                SrNo_Add();
            }
        }

        private void SrNo_Add()
        {
            if (dgvItem.Rows.Count > 0)
            {
                int SrNo = 1;
                for (int i = 0; i < dgvItem.Rows.Count; i++)
                {
                    dgvItem.Rows[i].Cells["clmSrNo"].Value = SrNo.ToString();
                    SrNo++;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDB();
        }

        private void cmbSupplierName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbSupplierName.SelectedIndex > -1)
            {
                 SupplierID= Convert.ToInt32(cmbSupplierName.SelectedValue);
                 FilldgvGrid();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClearAll();
                TableId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                
                txtCRNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                if(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()=="Yes")
                cbPurchase.Checked=true;
                else
                    cbPurchase.Checked=false;
                txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                if (TableId != 0)
                {
                    dgvItemRow = 0;
                    DataSet ds = new DataSet();
                    objBL.Query = "select C.ID,C.CRNo,C.CRDate,C.IsPurchase,C.SupplierId,C.Description,I.ItemName,CN.HSNCode,CN.UOM,CN.AQty,CN.RQty,CN.ItemId from ((CRNote C inner join CRNoteTransaction CN on C.ID=CN.CRNoteId) inner join Item I on I.ID=CN.ItemId) where C.CancelTag=0 and CN.CancelTag=0 and I.CancelTag=0 and C.ID=" + TableId + "";

                    ds = objBL.ReturnDataSet();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dgvItem.Rows.Add();
                            dgvItem.Rows[dgvItemRow].Cells["clmItemId"].Value = ds.Tables[0].Rows[i]["ItemId"].ToString();
                            dgvItem.Rows[dgvItemRow].Cells["clmItemName"].Value = ds.Tables[0].Rows[i]["ItemName"].ToString();
                            dgvItem.Rows[dgvItemRow].Cells["clmUOM"].Value = ds.Tables[0].Rows[i]["UOM"].ToString();
                            dgvItem.Rows[dgvItemRow].Cells["clmAQty"].Value = ds.Tables[0].Rows[i]["AQty"].ToString();
                            dgvItem.Rows[dgvItemRow].Cells["clmReturnQty"].Value = ds.Tables[0].Rows[i]["RQty"].ToString();
                            dgvItem.Rows[dgvItemRow].Cells["clmHSNCode"].Value = ds.Tables[0].Rows[i]["HSNCode"].ToString();
                            dgvItemRow++;
                        }
                        SrNo_Add();
                    }
                }
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }
    }
}
