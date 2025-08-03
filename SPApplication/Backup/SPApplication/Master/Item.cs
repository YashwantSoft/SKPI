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
    public partial class Item : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;

        public Item()
        {
            InitializeComponent();
            Set_Design();

        }

        private void Set_Design()
        {
            objBL.Set_Design_All(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.FRM_ITEM);

            objBL.Button_Add(btnManufracture);
            objBL.Button_Add(btnAddUOM);

            //Asign Label
            gbItemDetails.Text = BusinessResources.GR_ITEM_DETAILS;
            lblCategory.Text = BusinessResources.LBL_CATEGORY;

            lblItemName.Text = BusinessResources.LBL_ITEM_NAME;
            lblBatchNumber.Text = BusinessResources.LBL_BATCH_NUMBER;
            lblHSNCode.Text = BusinessResources.LBL_HSN_CODE;
            lblContain.Text = BusinessResources.LBL_CONTAIN;


            gbPricing.Text = BusinessResources.GR_PRICING;
            lblPrice.Text = BusinessResources.LBL_PRICE;
            lblCost.Text = BusinessResources.LBL_COST;
            lblMRP.Text = BusinessResources.LBL_MRP;
            lblProfitMarginInPer.Text = BusinessResources.LBL_PROFIT_MARGIN_PER;
            lblProfitMarginAmount.Text = BusinessResources.LBL_PROFIT_MARGIN_AMOUNT;

            gbTax.Text = BusinessResources.GR_TAXES;
            lblCGSTPer.Text = BusinessResources.LBL_CGST_PER;
            lblSGSTPer.Text = BusinessResources.LBL_SGST_PER;
            lblIGSTPer.Text = BusinessResources.LBL_IGST_PER;

            dataGridView1.DefaultCellStyle.SelectionBackColor = objBL.GetRowSelectionColor();
            objRL.Fill_UOM(cmbUOM);
        }

        private void Fill_All_ComboBox()
        {
            //objRL.Fill_CategoryName(cmbManufracture);
            objBL.Query = "select ID,ManufracturerName from Manufracturer where CancelTag=0";
            objBL.FillComboBox(cmbManufracture, "ManufracturerName", "ID");
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            Fill_All_ComboBox();
            ClearAll();
            FillGrid();
            cmbManufracture.Focus();
        }

        bool SearchTag = false;
        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (!SearchTag)
                objBL.Query = "select I.ID,I.ManufracturerId,M.ManufracturerName as [Manufracturer Name],I.ItemName as [Item Name],I.BatchNumber,I.HSNCode,I.UOM,I.Cost,I.Price,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from Item I inner join Manufracturer M on I.ManufracturerId=M.ID where I.CancelTag=0";
            else
                objBL.Query = "select I.ID,I.ManufracturerId,M.ManufracturerName as [Manufracturer Name],I.ItemName as [Item Name],I.BatchNumber,I.HSNCode,I.UOM,I.Cost,I.Price,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from Item I inner join Manufracturer M on I.ManufracturerId=M.ID where I.CancelTag=0 and I.ItemName like '%" + txtSearchItemName.Text + "%'";

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[2].Width = 200;
                dataGridView1.Columns[3].Width = 200;
                dataGridView1.Columns[7].Width = 100;
                dataGridView1.Columns[8].Width = 100;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                
            }
        }

        int ManufractureId = 0;
        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from Item where CancelTag=0 and ManufracturerId=" + ManufractureId + " and ItemName='" + txtItemName.Text + "' and ID <> " + TableID + "";
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
                dr = MessageBox.Show("Do yo want to delete this record", "Delete Record", MessageBoxButtons.YesNo);

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

            if (txtItemName.Text != "" && cmbManufracture.Text != "Other")
                ItemName = txtItemName.Text + "-" + cmbManufracture.Text;
            else
                ItemName = txtItemName.Text;

            if (TableID != 0)
                if (FlagDelete)
                    objBL.Query = "update Item set CancelTag=1 where ID=" + TableID + "";
                else
                    objBL.Query = "update Item set ManufracturerId=" + ManufractureId + ",ItemName='" + ItemName + "' ,BatchNumber='" + txtBatchNumber.Text + "',HSNCode='" + txtHSNCode.Text + "',UOM='NOS',Price='" + txtPrice.Text + "',Cost='" + txtCost.Text + "',MRP='" + txtMRP.Text + "',ProfitMarginPer='" + txtProfitMarginPer.Text + "',ProfitMarginAmount='" + txtProfitMarginAmount.Text + "',CGST='" + txtCGSTPer.Text + "',SGST='" + txtSGSTPer.Text + "',IGST='" + txtIGSTPer.Text + "', UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
            else
                objBL.Query = "insert into Item(ManufracturerId,ItemName,BatchNumber,HSNCode,UOM,Price,Cost,MRP,ProfitMarginPer,ProfitMarginAmount,CGST,SGST,IGST,UserId) values(" + ManufractureId + ",'" + ItemName + "','" + txtBatchNumber.Text + "','" + txtHSNCode.Text + "','NOS','" + txtPrice.Text + "','" + txtCost.Text + "','" + txtMRP.Text + "','" + txtProfitMarginPer.Text + "','" + txtProfitMarginAmount.Text + "','" + txtCGSTPer.Text + "','" + txtSGSTPer.Text + "','" + txtIGSTPer.Text + "'," + BusinessLayer.UserId_Static + ")";

            objBL.Function_ExecuteNonQuery();
        }

        protected bool Validation()
        {
            objEP.Clear();

            if (cmbManufracture.Text == "")
            {
                cmbManufracture.Focus();
                objEP.SetError(cmbManufracture, "Select Group Name");
                return true;
            }

            else if (txtItemName.Text == "")
            {
                txtItemName.Focus();
                objEP.SetError(txtItemName, "Enter Item Name");
                return true;
            }
            //else if (txtBatchNumber.Text == "")
            //{
            //    txtBatchNumber.Focus();
            //    objEP.SetError(txtBatchNumber, "Enter Batch Number");
            //    return true;
            //}
            //else if (txtHSNCode.Text == "")
            //{
            //    txtHSNCode.Focus();
            //    objEP.SetError(txtHSNCode, "Enter HSN Code");
            //    return true;
            //}
            //else if (txtContain.Text == "")
            //{
            //    txtContain.Focus();
            //    objEP.SetError(txtContain, "Enter Contain");
            //    return true;
            //}
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
            cmbManufracture.SelectedIndex = -1;
            txtItemName.Text = "";
            cmbUOM.SelectedIndex = -1;
            txtBatchNumber.Text = "";
            txtHSNCode.Text = "";
            txtContain.Text = "";
            cmbManufracture.Text = "";
            txtCost.Text = "";
            txtPrice.Text = "";
            txtMRP.Text = "";
            txtProfitMarginPer.Text = "";
            txtProfitMarginAmount.Text = "";
            txtCGSTPer.Text = "";
            txtSGSTPer.Text = "";
            txtIGSTPer.Text = "";
            cmbManufracture.Focus();
        }

        private void txtSearchItemName_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchItemName.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }


        private void CalculateMargin()
        {
            objRL.Price = 0; objRL.Cost = 0; objRL.ProfitMarginPer = 0; objRL.ProfitMarginAmount = 0;

            double.TryParse(txtPrice.Text, out objRL.Price);
            double.TryParse(txtCost.Text, out objRL.Cost);

            if (objRL.Price != 0 && objRL.Cost != 0)
            {
                objRL.Calculate_ProfitMargin();

                txtProfitMarginPer.Text = objRL.ProfitMarginPer.ToString();
                txtProfitMarginAmount.Text = objRL.ProfitMarginAmount.ToString();
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
                    //0 I.ID,
                    //1 I.ManufracturerId,
                    //2 M.ManufracturerName as [Manufracturer Name],
                    //3 I.ItemName as [Item Name],
                    //4 I.BatchNumber,
                    //5 I.HSNCode,
                    //6 I.UOM,
                    //7 I.Cost,
                    //8 I.Price,
                    //9 I.MRP,
                    //10 I.ProfitMarginPer,
                    //11 I.ProfitMarginAmount,
                    //12 I.CGST,
                    //13 I.SGST,
                    //14 I.IGST 

                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    ManufractureId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    cmbManufracture.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtItemName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtBatchNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtHSNCode.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtContain.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtCost.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    txtMRP.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    txtProfitMarginPer.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    txtProfitMarginAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    txtCGSTPer.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                    txtSGSTPer.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                    txtIGSTPer.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
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

        private void txtCost_Leave(object sender, EventArgs e)
        {
            CalculateMargin();
        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            CalculateMargin();
        }

        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtCost);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtPrice);
        }

        private void txtMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtMRP);
        }

        private void txtCGSTPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtCGSTPer);
        }

        private void txtSGSTPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtSGSTPer);
        }

        private void txtIGSTPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtIGSTPer);
        }

        private void cmbCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtItemName.Focus();
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtBatchNumber.Focus();
        }

        private void txtBatchNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtHSNCode.Focus();
        }

        private void txtHSNCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtContain.Focus();
        }

        private void txtContain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCost.Focus();
        }

        private void txtCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPrice.Focus();
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMRP.Focus();
        }

        private void txtMRP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCGSTPer.Focus();
        }

        private void txtCGSTPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSGSTPer.Focus();
        }

        private void txtSGSTPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtIGSTPer.Focus();
        }

        private void txtIGSTPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        private void cmbManufracture_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbManufracture.SelectedIndex > -1)
                ManufractureId = Convert.ToInt32(cmbManufracture.SelectedValue);
        }

        private void btnManufracture_Click(object sender, EventArgs e)
        {
            Manufracturer objForm = new Manufracturer();
            objForm.ShowDialog(this);
            Fill_All_ComboBox();
        }

        private void btnAddUOM_Click(object sender, EventArgs e)
        {
            UnitOfMessurement objForm = new UnitOfMessurement();
            objForm.ShowDialog(this);
            objRL.Fill_UOM(cmbUOM);
            cmbUOM.Focus();
        }
    }
}
