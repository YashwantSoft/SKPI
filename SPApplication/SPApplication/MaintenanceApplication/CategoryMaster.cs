using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace SPApplication.Master
{
    public partial class CategoryMaster : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false;

        string MainQuery = string.Empty, OrderByClause = string.Empty, WhereClause = string.Empty;

        public CategoryMaster()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_CATEGORY);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
       
        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            MainQuery = string.Empty; OrderByClause = string.Empty; WhereClause = string.Empty;

            DataSet ds = new DataSet();

            MainQuery = "select ID,CategoryName from CategoryMaster where CancelTag=0";

            if (SearchTag)
                WhereClause = " and CategoryName like '%" + txtSearch.Text + "%'";

            OrderByClause = " order by CategoryName asc";

            objBL.Query = MainQuery + WhereClause + OrderByClause;

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 500;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from CategoryMaster where CancelTag=0 and CategoryName='" + txtCategoryName.Text + "' and ID <> " + TableID + "";
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

        protected void SaveDB()
        {
            if (TableID != 0)
                if (FlagDelete)
                    objBL.Query = "update CategoryMaster set CancelTag=1 where ID=" + TableID + "";
                else
                    objBL.Query = "update CategoryMaster set CategoryName='" + txtCategoryName.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
            else
                objBL.Query = "insert into CategoryMaster(CategoryName,UserId) values('" + txtCategoryName.Text + "'," + BusinessLayer.UserId_Static + ")";

            objBL.Function_ExecuteNonQuery();
        }

        protected bool Validation()
        {
            objEP.Clear();
            if (txtCategoryName.Text == "")
            {
                txtCategoryName.Focus();
                objEP.SetError(txtCategoryName, "Enter Category Name");
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

        private void ClearAll()
        {
            objEP.Clear();
            TableID = 0;
            txtCategoryName.Text = "";
            SearchTag = false;
            txtCategoryName.Focus();
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
                    txtCategoryName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
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

        private void txtManufractureName_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.TxtValue(sender, e, txtCategoryName);
        }

        private void Manufracture_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            txtCategoryName.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }
    }
}
