using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace SPApplication
{
    public partial class ExpensesHead : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        private string SaveTag = string.Empty;


        public ExpensesHead()
        {
            InitializeComponent();
            objBL.Set_Design_All(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_EXPENSES);
            Set_All_Details();
        }

        private void Set_All_Details()
        {
            if (RedundancyLogics.DE_String == "Deposit")
            {
                SaveTag = "Deposit";
                lblHead.Text = BusinessResources.LBL_HEADER_DEPOSITHEAD;
                lblHeader.Text = BusinessResources.LBL_HEADER_DEPOSITHEAD;

            }
            else
            {
                SaveTag = "Expenses";
                lblHead.Text = BusinessResources.LBL_HEADER_EXPENSESHEAD;
                lblHeader.Text = BusinessResources.LBL_HEADER_EXPENSESHEAD;
            }
        }

        int TableId = 0; bool DeleteFlag = false;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected void ClearAll()
        {
            TableId = 0;
            objEP.Clear();
            DeleteFlag = false;
            txtExpencesHead.Text = "";
            txtExpencesHead.Select();
        }

        protected bool Validation()
        {
            if (txtExpencesHead.Text == "")
            {
                if (RedundancyLogics.DE_String == "Deposit")
                {
                    objEP.SetError(txtExpencesHead, "Insert Deposit Head");
                    return true;
                }
                else
                {
                    objEP.SetError(txtExpencesHead, "Insert Expences Head");
                    return true;
                }
            }
            else
                return false;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                SaveDB();
                objRL.ShowMessage(7, 1);
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                DeleteFlag = true;
                SaveDB();
                objRL.ShowMessage(9, 1);
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        protected void SaveDB()
        {
            objBL.Query = "";

            if (DeleteFlag == false)
            {
                if (TableId != 0)
                {
                    if(SaveTag == "Deposit")
                        objBL.Query = "update DepositHeadMain set DepositHead='" + txtExpencesHead.Text + "' where ID=" + TableId + "";
                    else
                        objBL.Query = "update ExpensesHead set ExpensesHeadMain='" + txtExpencesHead.Text + "' where ID=" + TableId + "";
                }
                else
                {
                    if (SaveTag == "Deposit")
                        objBL.Query = "insert into DepositHead(DepositHeadMain) values ('" + txtExpencesHead.Text + "')";
                    else
                        objBL.Query = "insert into ExpensesHead(ExpensesHeadMain) values ('" + txtExpencesHead.Text + "')";
                }
            }
            else
            {
                if (SaveTag == "Deposit")
                    objBL.Query = "delete from DepositHead where ID= " + TableId + "";
                else
                    objBL.Query = "delete from ExpensesHead where ID= " + TableId + "";
            }

            objBL.Function_ExecuteNonQuery();
            FillGrid();
            ClearAll();
        }

        protected void FillGrid()
        {
            objBL.Query = "";
            if (RedundancyLogics.DE_String == "Deposit")
            {
                if(!SearchFlag)
                    objBL.Query = "select ID,DepositHeadMain as [Deposit Head] from DepositHead where CancelTag=0";
                else
                    objBL.Query = "select ID,DepositHeadMain as [Deposit Head] from DepositHead where CancelTag=0 and DepositHeadMain like '%" + txtSearch.Text + "%'";
            }
            else
            {
                if (!SearchFlag)
                    objBL.Query = "select ID,ExpensesHeadMain as [Expenses Head] from ExpensesHead where CancelTag=0";
                else
                    objBL.Query = "select ID,ExpensesHeadMain as [Expenses Head] from ExpensesHead where CancelTag=0 and ExpensesHeadMain like '%" + txtSearch.Text + "%'";
            }

            DataSet ds = new DataSet();
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 800;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClearAll();
                TableId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                txtExpencesHead.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch (Exception ex1)
            {
                objRL.ErrorMessge(ex1.ToString());
            }
        }

        private void ExpencesHead_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void txtExpencesHead_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        bool SearchFlag = false;
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchFlag = true;
            else
                SearchFlag = false;

            FillGrid();
        }
    }
}
