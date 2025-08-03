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
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;

        //string ItemType = string.Empty;

        string MainQuery = string.Empty, OrderByClause = string.Empty, WhereClause = string.Empty;

        public Item()
        {
            InitializeComponent();
            Set_Design();
            //ItemType = objBL.ItemType;
        }

        private void Set_Design()
        {
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_ITEMMASTER);
            objDL.SetPlusButtonDesign(btnManufracture);
            objDL.SetPlusButtonDesign(btnAddUOM);

            //Asign Label
            gbItemDetails.Text = BusinessResources.GR_ITEM_DETAILS;
            lblCategory.Text = BusinessResources.LBL_CATEGORY;

            lblItemName.Text = BusinessResources.LBL_ITEM_NAME;
            lblBatchNumber.Text = BusinessResources.LBL_BATCH_NUMBER;
            lblHSNCode.Text = BusinessResources.LBL_HSN_CODE;
            //lblContain.Text = BusinessResources.LBL_CONTAIN;

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
            FillCategory();
        }

        private void FillCategory()
        {
            objBL.FillComboBox_TableWise(cmbCategory, "CategoryMaster", "ID", "CategoryName");
            objBL.FillComboBox_TableWise(cmbManufacturer, "Manufracturer", "ID", "ManufracturerName");
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            ClearAll();
            cmbCategory.Focus();
        }

        bool SearchTag = false;
        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            MainQuery = string.Empty; OrderByClause = string.Empty; WhereClause = string.Empty;

            if (cmbCategory.SelectedIndex > -1)
            {
                DataSet ds = new DataSet();

                MainQuery = "select I.ID,I.CategoryId,CM.CategoryName as [Category],I.ItemName as [Item Name],I.Description,I.ItemCode,I.UOM,I.PartNumber,I.OldPartNumber,I.ManufacturerId,I.OpeningStock,I.BalanceQty from Item I inner join CategoryMaster CM on I.CategoryId=CM.ID where I.CancelTag=0 and I.CategoryId=" + cmbCategory.SelectedValue + "";

                
                //I.BatchNumber,I.HSNCode,I.Cost,I.Price,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST,I.OpeningStock from Item I inner join CategoryMaster CM on I.CategoryId=CM.ID where I.CancelTag=0 and I.CategoryId=" + cmbCategory.SelectedValue + "";

                if (SearchTag)
                    WhereClause = " and I.ItemName like '%" + txtSearchItemName.Text + "%'";

                OrderByClause = " order by I.ItemName asc";

                objBL.Query = MainQuery + WhereClause + OrderByClause;

                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //0 I.ID,
                    //1 I.CategoryId,
                    //2 CM.CategoryName as [Category],
                    //3 I.ItemName as [Item Name],
                    //4 I.UOM
                    //5 ,I.OpeningStock,
                    //6 I.BalanceQty

                    dataGridView1.DataSource = ds.Tables[0];
                    lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;

                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    //dataGridView1.Columns[4].Visible = false;
                    //dataGridView1.Columns[5].Visible = false;
                    //dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[2].Width = 200;
                    dataGridView1.Columns[3].Width = 500;
                    dataGridView1.Columns[4].Width = 100;
                    dataGridView1.Columns[5].Width = 120;
                    dataGridView1.Columns[6].Width = 120;
                }
            }
        }

        int CategoryId = 0;
        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from Item where CancelTag=0 and CategoryId=" + CategoryId + " and ItemName='" + txtItemName.Text + "' and ID <> " + TableID + "";
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
            cmbCategory.SelectedIndex = -1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr;
                dr = objRL.Delete_Record_Show_Message();

                if (dr == DialogResult.Yes)
                {
                    FlagDelete = true;
                    SaveDB();
                }
                else
                    ClearAll();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }
        
        bool FlagOSInsert = false;
        double OpeningStock = 0, CurrentQty = 0, PreviousQty = 0;
        int ManufacturerId = 0;

        protected void SaveDB()
        {
            string ItemName = string.Empty;

            //if (txtItemName.Text != "" && cmbManufracture.Text != "Other")
            //    ItemName = txtItemName.Text + "-" + cmbManufracture.Text;
            //else
                ItemName = txtItemName.Text;


            if(cmbManufacturer.SelectedIndex >-1)
                ManufacturerId = Convert.ToInt32(cmbManufacturer.SelectedValue);

            if (TableID != 0)
                if (FlagDelete)
                    objBL.Query = "update Item set CancelTag=1 where ID=" + TableID + "";
                else
                    objBL.Query = "update Item set CategoryId=" + CategoryId + ",ItemName='" + ItemName + "' ,Description='" + txtDescription.Text + "',ItemCode='" + txtItemCode.Text + "',UOM='" + cmbUOM.Text + "',PartNumber='" + txtPartNumber.Text + "',OldPartNumber='" + txtOldPartNumber.Text + "',ManufacturerId=" + ManufacturerId + ",BatchNumber='" + txtBatchNumber.Text + "',HSNCode='" + txtHSNCode.Text + "',Price='" + txtPrice.Text + "',Cost='" + txtCost.Text + "',MRP='" + txtMRP.Text + "',ProfitMarginPer='" + txtProfitMarginPer.Text + "',ProfitMarginAmount='" + txtProfitMarginAmount.Text + "',CGST='" + txtCGSTPer.Text + "',SGST='" + txtSGSTPer.Text + "',IGST='" + txtIGSTPer.Text + "',OpeningStock='" + txtOpeningStock.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
            else
                objBL.Query = "insert into Item(CategoryId,ItemName,Description,ItemCode,UOM,PartNumber,OldPartNumber,ManufacturerId,BatchNumber,HSNCode,Price,Cost,MRP,ProfitMarginPer,ProfitMarginAmount,CGST,SGST,IGST,OpeningStock,UserId) values(" + CategoryId + ",'" + ItemName + "','" + txtDescription.Text + "','" + txtItemCode.Text + "','" + cmbUOM.Text + "','" + txtPartNumber.Text + "','" + txtOldPartNumber.Text + "'," + ManufacturerId + ",'" + txtBatchNumber.Text + "','" + txtHSNCode.Text + "','" + txtPrice.Text + "','" + txtCost.Text + "','" + txtMRP.Text + "','" + txtProfitMarginPer.Text + "','" + txtProfitMarginAmount.Text + "','" + txtCGSTPer.Text + "','" + txtSGSTPer.Text + "','" + txtIGSTPer.Text + "','" + txtOpeningStock.Text + "'," + BusinessLayer.UserId_Static + ")";

           int Result= objBL.Function_ExecuteNonQuery();

            if (Result > 0)
            {
                if (TableID != 0)
                {
                    if (!FlagDelete)
                    {
                        //if (!string.IsNullOrEmpty(Convert.ToString(txtOpeningStock.Text)) && FlagOSInsert)
                        //{
                        //    DataSet dsItemPurchaseQuantity = new DataSet();
                        //    objBL.Query = "select ItemId,Quantity from ItemQuantity where CancelTag=0 and ItemId=" + TableID + "";
                        //    dsItemPurchaseQuantity = objBL.ReturnDataSet();

                        //    OpeningStock = 0; CurrentQty = 0;
                        //    OpeningStock = Convert.ToDouble(txtOpeningStock.Text);

                        //    if (dsItemPurchaseQuantity.Tables[0].Rows.Count > 0)
                        //    {
                        //        if (!string.IsNullOrEmpty(dsItemPurchaseQuantity.Tables[0].Rows[0][0].ToString()))
                        //        {
                        //            if(!string.IsNullOrEmpty(Convert.ToString(dsItemPurchaseQuantity.Tables[0].Rows[0]["Quantity"])))
                        //                CurrentQty = Convert.ToDouble(dsItemPurchaseQuantity.Tables[0].Rows[0]["Quantity"].ToString());

                        //            CurrentQty = CurrentQty + OpeningStock;
                        //            objBL.Query = "Update ItemQuantity set Quantity='" + CurrentQty + "',UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0  and ItemId=" + TableID + "";
                        //            objBL.Function_ExecuteNonQuery();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        objBL.Query = "insert into ItemQuantity(ItemId,Quantity,UserId) values(" + TableID + ",'" + OpeningStock + "'," + BusinessLayer.UserId_Static + ")";
                        //        objBL.Function_ExecuteNonQuery();
                        //    }
                        //}
                    }
                }
            }
        }

        protected bool Validation()
        {
            objEP.Clear();

            if (cmbCategory.Text == "")
            {
                cmbCategory.Focus();
                objEP.SetError(cmbCategory, "Select Category Name");
                return true;
            }
            else if (txtItemName.Text == "")
            {
                txtItemName.Focus();
                objEP.SetError(txtItemName, "Enter Item Name");
                return true;
            }
            else if (cmbUOM.Text == "")
            {
                cmbUOM.Focus();
                objEP.SetError(cmbUOM, "Select UOM");
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
            CurrentQty = 0; OpeningStock=0;
            FlagOSInsert = false;

            TableID = 0;
            objEP.Clear();
            FlagDelete = false;
            btnDelete.Enabled = false;
            
            txtItemName.Text = "";
            txtDescription.Text = "";
            txtItemCode.Text = "";
            txtPartNumber.Text = "";
            txtOldPartNumber.Text = "";
            cmbManufacturer.SelectedIndex = -1;
            cmbUOM.SelectedIndex = -1;
            txtBatchNumber.Text = "";
            txtHSNCode.Text = "";
            txtItemCode.Text = "";
            //cmbCategory.Text = "";
            txtCost.Text = "";
            txtPrice.Text = "";
            txtMRP.Text = "";
            txtProfitMarginPer.Text = "";
            txtProfitMarginAmount.Text = "";
            txtCGSTPer.Text = "";
            txtSGSTPer.Text = "";
            txtIGSTPer.Text = "";
            txtOpeningStock.Text = "";
            //lblOpeningStock.Visible = false;
            //txtOpeningStock.Visible = false;
            cmbCategory.Focus();
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

            double Price_F = objRL.Price;
            double Cost_F = objRL.Cost;
            double.TryParse(txtPrice.Text, out Price_F);
            double.TryParse(txtCost.Text, out Cost_F);

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
                    //1 I.CategoryId,
                    //2 CM.CategoryName as [Category],
                    //3 I.ItemName as [Item Name],
                    //4 I.UOM
                    //5 ,I.OpeningStock,
                    //6 I.BalanceQty

                    ClearAll();
                    btnDelete.Enabled = true;
                   
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    CategoryId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    cmbCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtItemName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    //txtBatchNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    //txtHSNCode.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    cmbUOM.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtOpeningStock.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

                    //txtCost.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    //txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    //txtMRP.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    //txtProfitMarginPer.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    //txtProfitMarginAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    //txtCGSTPer.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                    //txtSGSTPer.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                    //txtIGSTPer.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                   

                    //if(!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[15].Value)))
                    //{
                    //    txtOpeningStock.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                    //    FlagOSInsert = false;
                    //    lblOpeningStock.Visible = true;
                    //    txtOpeningStock.Visible = true;
                    //    lblOpeningStock.Enabled = false;
                    //    txtOpeningStock.Enabled = false;
                    //}
                    //else
                    //{
                    //    txtOpeningStock.Text ="";
                    //    FlagOSInsert = true;
                    //    lblOpeningStock.Visible = true;
                    //    txtOpeningStock.Visible = true;
                    //    lblOpeningStock.Enabled = true;
                    //    txtOpeningStock.Enabled = true;
                    //}
                   // ItemType = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
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
                txtItemCode.Focus();
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
            if (cmbCategory.SelectedIndex > -1)
            {
                CategoryId = Convert.ToInt32(cmbCategory.SelectedValue);
                FillGrid();
            }
        }

        private void btnManufracture_Click(object sender, EventArgs e)
        {
            CategoryMaster objForm = new CategoryMaster();
            objForm.ShowDialog(this);
            FillCategory();
        }

        private void btnAddUOM_Click(object sender, EventArgs e)
        {
            UnitOfMessurement objForm = new UnitOfMessurement();
            objForm.ShowDialog(this);
            objRL.Fill_UOM(cmbUOM);
            cmbUOM.Focus();
        }

        private void btnAddManufacturer_Click(object sender, EventArgs e)
        {
            Manufracturer objForm = new Manufracturer();
            objForm.ShowDialog(this);
        }
    }
}
