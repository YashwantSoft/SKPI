using BusinessLayerUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPApplication.Transaction
{
    public partial class ShiftEntry : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;

        public ShiftEntry()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_SHIFTENTRY);
        }

        string Shift = string.Empty;
        bool ShiftFlag = false;

        private void ShiftCode()
        {
            txtShift.Text = objRL.ShiftCode();// Shift.ToString();
            ShiftId = objRL.ShiftId;
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("ShiftEntry"));
            txtID.Text = IDNo.ToString();
        }

        private void ShiftEntry_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ClearAll()
        {
            objEP.Clear();
            TableID = 0;
            
            txtID.Text = "";
            txtShift.Text = "";
            dtpDate.Value = DateTime.Now.Date;
            dtpTime.Value = DateTime.Now;
            cmbPlantIncharge.SelectedIndex = -1;
            cmbVolumChecker.SelectedIndex = -1;
            txtNaration.Text = "";
            GetID();
            ShiftCode();
            FillEmployee();
            cmbPlantIncharge.Focus();
        }

        private void FillEmployee()
        {
            objRL.Fill_Employee_By_Designation(cmbPlantIncharge, "Plant Incharge");
            objRL.Fill_Employee_By_Designation(cmbVolumChecker, "Volume Checker");
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
            DialogResult dr;
            dr = objRL.Delete_Record_Show_Message(); // MessageBox.Show("Do yo want to delete this record?", "Delete Record", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                FlagDelete = true;
                SaveDB();
            }
        }

        protected bool Validation()
        {
            objEP.Clear();
            if (txtID.Text == "")
            {
                txtID.Focus();
                objEP.SetError(txtID, "Enter ID");
                return true;
            }
            else if (ShiftId == 0)
            {
                txtShift.Focus();
                objEP.SetError(txtShift, "Enter Shift");
                return true;
            }
            else
                return false;
        }

        bool SearchTag = false;
        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if(!SearchTag)
                objBL.Query = "select SE.ID,SE.EntryDate as [Date],SE.EntryTime as [Time],SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],SE.Naration,SE.ShiftId from ((ShiftEntry SE inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) where SE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and SE.EntryDate=#" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "#";
            else
                objBL.Query = "select SE.ID,SE.EntryDate as [Date],SE.EntryTime as [Time],SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],SE.Naration,SE.ShiftId from ((ShiftEntry SE inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) where SE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and SE.EntryDate=#" + dtpSearchDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "#";
          
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                 //0 SE.ID,
                //1 SE.EntryDate,
                //2 SE.EntryTime,
                //3 SE.Shift,
                //4 SE.PlantInchargeId,
                //5 E.FullName as [Plant Incharge],
                //6 SE.VolumeCheckerId,
                //7 E1.FullName as [Volume Checker],
                //8 SE.Naration

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 60;
                dataGridView1.Columns[5].Width = 250;
                dataGridView1.Columns[7].Width = 250;
                 
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }
        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            //objBL.Query = "select ID from ShiftEntry where CancelTag=0 and Shift='" + txtShift.Text + "' and EntryDate=#" + dtpDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and ID <> " + TableID + "";
            objBL.Query = "select ID from ShiftEntry where CancelTag=0 and ShiftId=" + ShiftId + " "; // and EntryDate=#" + dtpDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        int ShiftId = 0;
        private void SaveDB()
        {
            if (!Validation())
            {
                if (!CheckExist())
                {
                      if (TableID != 0)
                        if (FlagDelete)
                            objBL.Query = "update  ShiftEntry set CancelTag=1 where ID=" + TableID + "";
                        else
                            objBL.Query = "update ShiftEntry set EntryDate='" + dtpDate.Value.ToShortDateString() + "',EntryTime='" + dtpTime.Value.ToShortTimeString() + "',Shift='" + txtShift.Text + "',PlantInchargeId=" + cmbPlantIncharge.SelectedValue + ",VolumeCheckerId=" + cmbVolumChecker.SelectedValue + ",Naration='" + txtNaration.Text + "',ShiftId=" + ShiftId + ",ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                    else
                          objBL.Query = "insert into ShiftEntry(EntryDate,EntryTime,Shift,PlantInchargeId,VolumeCheckerId,Naration,ShiftId,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "','" + txtShift.Text + "'," + cmbPlantIncharge.SelectedValue + "," + cmbVolumChecker.SelectedValue + ",'" + txtNaration.Text + "',"+ShiftId+", " + BusinessLayer.UserId_Static + ")";

                      if (objBL.Function_ExecuteNonQuery() > 0)
                      {
                          if (FlagDelete)
                              objRL.ShowMessage(9, 1);
                          else
                          {
                              objRL.ShowMessage(7, 1);
                              cmbPlantIncharge.Focus();
                          }
                          FillGrid();
                          ClearAll();
                          this.Dispose();
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

        private void dtpSearchDate_ValueChanged(object sender, EventArgs e)
        {
            SearchTag = true;
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
                    txtID.Text = TableID.ToString();
                    dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    txtShift.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cmbPlantIncharge.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    cmbVolumChecker.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    ShiftId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[8].Value)));
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

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
