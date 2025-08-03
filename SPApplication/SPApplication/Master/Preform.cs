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
    public partial class Preform : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        
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

        bool SearchTag = false;
        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (!SearchTag)
                objBL.Query = "select ID,PreformType as [Preform Type],PreformName as [Preform Name],Standard,PreformNeckSize as [Preform Neck Size],PreformWeight as [Preform Weight],PreformNeckID as [Preform Neck ID],PreformNeckOD as [Preform Neck OD],PreformNeckCollarGap as [Preform Neck Collar Gap],PreformNeckHeight as [Preform Neck Height] from Preform where CancelTag=0";
            else
                objBL.Query = "select ID,PreformType as [Preform Type],PreformName as [Preform Name],Standard,PreformNeckSize as [Preform Neck Size],PreformWeight as [Preform Weight],PreformNeckID as [Preform Neck ID],PreformNeckOD as [Preform Neck OD],PreformNeckCollarGap as [Preform Neck Collar Gap],PreformNeckHeight as [Preform Neck Height] from Preform where CancelTag=0 and PreformName like '%" + txtSearchItemName.Text + "%'";

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 ID,
                //1 PreformType as [Preform Type]
                //2 PreformName as [Preform Name],
                //3 Standard
                //4 PreformNeckSize as [Preform Neck Size],
                //5 PreformWeight as [Preform Weight],
                //6 PreformNeckID as [Preform Neck ID],
                //7 PreformNeckOD as [Preform Neck OD],
                //8 PreformNeckCollarGap as [Preform Neck Collar Gap],
                //9 PreformNeckHeight as [Preform Neck Height],

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;

                dataGridView1.Columns[1].Width = 110;
                dataGridView1.Columns[2].Width = 180;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[4].Width = 130;
                dataGridView1.Columns[5].Width = 120;
                dataGridView1.Columns[6].Width = 120;
                dataGridView1.Columns[7].Width = 130;
                dataGridView1.Columns[8].Width = 170;
                dataGridView1.Columns[9].Width = 150;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from Preform where CancelTag=0 and PreformName='" + txtPreformName.Text + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        protected void ClearAll()
        {
            TableID = 0;
            objEP.Clear();
            FlagDelete = false;
            btnDelete.Enabled = false;
            cmbPreformType.SelectedIndex = -1;
            txtPreformName.Text = "";
            txtStandard.Text = "";
            txtPreformNeckSize.Text = "";
            txtPreformWeight.Text = "";
            txtPreformNeckID.Text = "";
            txtPreformNeckOD.Text = "";
            txtPreformNeckCollarGap.Text = "";
            txtPreformNeckHeight.Text = "";
            cmbPreformType.Focus();
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
                dr = objRL.Delete_Record_Show_Message();

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

        private void txtSearchItemName_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchItemName.Text !="")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

        private void Preform_Load(object sender, EventArgs e)
        {
            ClearAll();
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
                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    cmbPreformType.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtPreformName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtStandard.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtPreformNeckSize.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtPreformWeight.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtPreformNeckID.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtPreformNeckOD.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtPreformNeckCollarGap.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    txtPreformNeckHeight.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
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

        public Preform()
        {
            InitializeComponent();
            Set_Design();
        }

        private void Set_Design()
        {
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_PREFORM);
            dataGridView1.DefaultCellStyle.SelectionBackColor = objBL.GetRowSelectionColor();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtPreformName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPreformNeckSize.Focus();
        }

        private void txtPreformNeckSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPreformWeight.Focus();
        }

        private void txtPreformWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPreformNeckID.Focus();
        }

        private void txtPreformNeckID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPreformNeckOD.Focus();
        }

        private void txtPreformNeckOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPreformNeckCollarGap.Focus();
        }

        private void txtPreformNeckCollarGap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPreformNeckHeight.Focus();
        }

       

        private void txtPreformNeckHeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        private void txtPreformNeckHeight_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        protected void SaveDB()
        {
            if (TableID != 0)
                if (FlagDelete)
                    objBL.Query = "update Preform set CancelTag=1 where ID=" + TableID + "";
                else
                    objBL.Query = "update Preform set PreformType='" + cmbPreformType.Text + "',PreformName='" + txtPreformName.Text + "',Standard='"+txtStandard.Text+"',PreformNeckSize='" + txtPreformNeckSize.Text + "',PreformWeight='" + txtPreformWeight.Text + "',PreformNeckID='" + txtPreformNeckID.Text + "',PreformNeckOD='" + txtPreformNeckOD.Text + "',PreformNeckCollarGap='" + txtPreformNeckCollarGap.Text + "',PreformNeckHeight='" + txtPreformNeckHeight.Text + "',ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
            else
                objBL.Query = "insert into Preform(PreformType,PreformName,Standard,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,UserId) values('" + cmbPreformType.Text + "','" + txtPreformName.Text + "','" + txtStandard.Text + "','" + txtPreformNeckSize.Text + "','" + txtPreformWeight.Text + "','" + txtPreformNeckID.Text + "','" + txtPreformNeckOD.Text + "','" + txtPreformNeckCollarGap.Text + "','" + txtPreformNeckHeight.Text + "'," + BusinessLayer.UserId_Static + ")";

            objBL.Function_ExecuteNonQuery();
        }
        protected bool Validation()
        {
            objEP.Clear();

            if (cmbPreformType.SelectedIndex == -1)
            {
                cmbPreformType.Focus();
                objEP.SetError(cmbPreformType, "Select Product Type");
                return true;
            }
            else if (txtPreformName.Text == "")
            {
                txtPreformName.Focus();
                objEP.SetError(txtPreformName, "Enter Preform Name");
                return true;
            }
            else if (txtPreformNeckSize.Text == "")
            {
                txtPreformNeckSize.Focus();
                objEP.SetError(txtPreformNeckSize, "Enter Preform Neck Size");
                return true;
            }
            else if (txtPreformWeight.Text == "")
            {
                txtPreformWeight.Focus();
                objEP.SetError(txtPreformWeight, "Enter Batch Number");
                return true;
            }
            else
                return false;
        }
    }
}
