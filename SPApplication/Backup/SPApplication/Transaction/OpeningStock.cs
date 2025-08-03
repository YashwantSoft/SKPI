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
    public partial class OpeningStock : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();

        int ItemId = 0;

        public OpeningStock()
        {
            InitializeComponent();
            objBL.Set_Design_All(this, lblHeader, btnSave, btnClear, btnDelete, btnExit,"Opening Stock Entry");
            GetItem();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            Item objForm = new Item();
            objForm.ShowDialog(this);
            GetItem();
            cmbItem.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void GetItem()
        {
            objBL.Query = "select ID,ItemName from Item where CancelTag=0 and ID NOT IN (Select ItemId from ItemQuantity where CancelTag=0)";
            objBL.FillComboBox(cmbItem, "ItemName", "ID");
            cmbItem.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbItem.SelectedIndex > -1 && txtQty.Text != "")
            {
                objBL.Query = "insert into ItemQuantity(ItemId,Quantity) values(" + ItemId + "," + txtQty.Text + ")";
                int R= objBL.Function_ExecuteNonQuery();

                if (R > 0)
                {
                    MessageBox.Show("Item quantity added successfully");
                    ClearAll();
                }

                //DataSet ds = new DataSet();
                //objBL.Query = "select ItemId,Quantity from ItemQuantity where CancelTag=0 and ItemId=" + ItemId + "";
                //ds = objBL.ReturnDataSet();

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ItemId"])))
                //    {
                //        ItemId = Convert.ToInt16(ds.Tables[0].Rows[0]["ItemId"].ToString());
                //        AvailableQty = Convert.ToInt16(ds.Tables[0].Rows[0]["Quantity"].ToString());
                //        if (!string.IsNullOrEmpty(txtQty.Text))
                //        {
                //            Qty = Convert.ToInt16(txtQty.Text);
                //            NewQty = AvailableQty + Qty;
                //        }
                //    }
                //}
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void ClearAll()
        {
            ItemId = 0;
            cmbItem.SelectedIndex = -1;
            txtQty.Text = "";
            AvailableQty = 0; Qty = 0; NewQty = 0;
            FillGrid();
            GetItem();
            cmbItem.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        int AvailableQty = 0, Qty = 0, NewQty = 0;

        private void cmbItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbItem.SelectedIndex > -1)
            {
                ItemId = Convert.ToInt16(cmbItem.SelectedValue.ToString());

                //DataSet ds = new DataSet();
                //objBL.Query = "select ItemId,Quantity from ItemQuantity where CancelTag=0 and ItemId=" + ItemId + "";
                //ds = objBL.ReturnDataSet();

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ItemId"])))
                //    {
                //        ItemId = Convert.ToInt16(ds.Tables[0].Rows[0]["ItemId"].ToString());
                //        //AvailableQty = Convert.ToInt16(ds.Tables[0].Rows[0]["Quantity"].ToString());
                //        //if (!string.IsNullOrEmpty(txtQty.Text))
                //        //{
                //        //    Qty = Convert.ToInt16(txtQty.Text);
                //        //    NewQty = AvailableQty + Qty;
                //        //}
                //    }
                //}
            }
        }

        private void FillGrid()
        {
            DataSet ds = new DataSet();
            if (!SearchFlag)
                objBL.Query = "select IQ.ItemId,I.ItemName as [Item Name],IQ.Quantity as [Current Stock] from ItemQuantity IQ inner join Item I on I.ID=IQ.ItemId where IQ.CancelTag=0 and I.CancelTag=0";
            else
                objBL.Query = "select IQ.ItemId,I.ItemName as [Item Name],IQ.Quantity as [Current Stock] from ItemQuantity IQ inner join Item I on I.ID=IQ.ItemId where IQ.CancelTag=0 and I.CancelTag=0  and I.ItemName like '%" + txtSearchItemName.Text + "%' order by I.ItemName";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 550;
                dataGridView1.Columns[2].Width = 130;
            }
        }

        bool SearchFlag = false;
        private void txtSearchItemName_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchItemName.Text != "")
            {
                SearchFlag = true;
                FillGrid();
            }
            else
            {
                SearchFlag = false;
                FillGrid();
            }
        }

        private void ItemStockEntry_Load(object sender, EventArgs e)
        {
            FillGrid();
        }
    }
}
