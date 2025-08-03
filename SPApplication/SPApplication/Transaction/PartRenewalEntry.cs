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
    public partial class PartRenewalEntry : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0, EmployeeId = 0;

        string Status = string.Empty;
        bool SearchTag = false;

        public PartRenewalEntry()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_PARTEQUIPMENTENTRY);
            objRL.Fill_Users(cmbDepartment);
            Set_Department();
            //dtpStartHours.Format = DateTimePickerFormat.Custom;
            //dtpStartHours.CustomFormat = "HH:mm tt";
            //dtpStartHours.Value = DateTime.Now.Date;
            dtpStartDate.Value = DateTime.Now;
            dtpStartDate.CustomFormat = BusinessResources.DATE_FORMAT_WITHTIME;
            dtpExpiryDate.Value = DateTime.Now;
            dtpExpiryDate.CustomFormat = BusinessResources.DATE_FORMAT_WITHTIME;
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("PartRenewalEntry"));
            txtID.Text = IDNo.ToString();
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

        protected void FillGrid()
        {
            if (cmbDepartment.SelectedIndex > -1)
            {
                DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue);

                dataGridView1.DataSource = null;
                DataSet ds = new DataSet();

                if (!SearchTag)
                    objBL.Query = "select PRE.ID,PRE.EntryDate,PRE.EntryTime,PRE.DepartmentId,L.UserName as [Department],PRE.PartId,PM.PartName as [Part Name],PRE.StartDate as [Start Date],PRE.IsCompressor,PRE.CopressorType as [Compressor Type],PRE.RenewarPeriod as [Renewal Period],PRE.RenewarPeriodFor as [Duration],PRE.ExpiryDate as [Expiry Date],PRE.StartReadingNo as [Start Reading],EndReadingNo as [End Reading],PRE.RenewalBy as [Renewal by],PRE.ContactNo as [Contact No],PRE.Naration from ((PartRenewalEntry PRE inner join PartMaster PM on PM.ID=PRE.PartId) inner join Login L on L.ID=PM.DepartmentId) where L.CancelTag=0 and PM.CancelTag=0 and PRE.CancelTag=0 and PRE.DepartmentId=" + DepartmentId + "";
                else
                    objBL.Query = "select PRE.ID,PRE.EntryDate,PRE.EntryTime,PRE.DepartmentId,L.UserName as [Department],PRE.PartId,PM.PartName as [Part Name],PRE.StartDate as [Start Date],PRE.IsCompressor,PRE.CopressorType as [Compressor Type],PRE.RenewarPeriod as [Renewal Period],PRE.RenewarPeriodFor as [Duration],PRE.ExpiryDate as [Expiry Date],PRE.StartReadingNo as [Start Reading],EndReadingNo as [End Reading],PRE.RenewalBy as [Renewal by],PRE.ContactNo as [Contact No],PRE.Naration from ((PartRenewalEntry PRE inner join PartMaster PM on PM.ID=PRE.PartId) inner join Login L on L.ID=PM.DepartmentId) where L.CancelTag=0 and PM.CancelTag=0 and PRE.CancelTag=0 and PRE.DepartmentId=" + DepartmentId + " and PM.PartName like '%" + txtSearch.Text + "%'";

                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //0 PM.ID,
                    //1 PRE.EntryDate,
                    //2 PRE.EntryTime,
                    //3 PRE.DepartmentId,
                    //4 L.UserName as [Department],
                    //5 PRE.PartId,
                    //6 PM.PartName as [Part Name],
                    //7 PRE.StartDate,
                    //8 PRE.IsCompressor,
                    //9 PRE.CopressorType,
                    //10 PRE.RenewarPeriod,
                    //11 PRE.RenewarPeriodFor,
                    //12 PRE.ExpiryDate,
                    //13 PRE.StartReadingNo,
                    //14 EndReadingNo,
                    //15 PRE.RenewalBy,
                    //16 PRE.ContactNo
                    //17 PRE.Naration

                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[8].Visible = false;
                    //dataGridView1.Columns[12].Visible = false;
                    //dataGridView1.Columns[10].Visible = false;

                    dataGridView1.Columns[1].Width = 80;
                    dataGridView1.Columns[4].Width = 100;
                    dataGridView1.Columns[6].Width = 150;
                    dataGridView1.Columns[7].Width = 120;
                    dataGridView1.Columns[9].Width = 120;
                    dataGridView1.Columns[10].Width = 120;
                    dataGridView1.Columns[11].Width = 120;
                    dataGridView1.Columns[12].Width = 120;
                    dataGridView1.Columns[13].Width = 120;
                    dataGridView1.Columns[14].Width = 120;
                    dataGridView1.Columns[15].Width = 120;
                    dataGridView1.Columns[16].Width = 120;
                    dataGridView1.Columns[17].Width = 120;
                    lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                }
            }
        }

        private void ClearAll()
        {
            objEP.Clear();
            TableID = 0;
            txtID.Text = "";
            txtSearchPartName.Text = "";
            dtpDate.Value = DateTime.Now.Date;
            dtpTime.Value = DateTime.Now.Date;
            dtpStartDate.Value = DateTime.Now;
            dtpExpiryDate.Value = DateTime.Now;
          
            txtStartReading.Text = "";
            txtEndReadingNo.Text = "";
            txtRenewalBy.Text = "";
            txtContactNo.Text = "";
            txtNaration.Text = "";
            GetID();
            txtSearchPartName.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
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

        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from PartMaster where CancelTag=0 and Status='Active'  and ID <> " + PartId + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        int IsCompressor = 0;
        private void SaveDB()
        {
            if (!Validation())
            {
                if (!CheckExist())
                {
                    Get_DepartmentId();
                    if (cbIsCompressor.Checked)
                        IsCompressor = 1;
                    else
                        IsCompressor = 0;
                    //objRL.ApostropheSave(txtPartName.Text)
                    if (TableID != 0)
                        if (FlagDelete)
                            objBL.Query = "update PartRenewalEntry set CancelTag=1 where ID=" + TableID + "";
                        else
                            objBL.Query = "update PartRenewalEntry set EntryDate='" + dtpDate.Value.ToShortDateString() + "',EntryTime='" + dtpTime.Value.ToShortTimeString() + "',DepartmentId=" + DepartmentId + ",PartId=" + PartId + ",StartDate='" + dtpStartDate.Value + "',IsCompressor=" + IsCompressor + ",CopressorType='" + cmbType.Text + "',RenewarPeriod='" + txtRenewalPeriod.Text + "',RenewarPeriodFor='" + cmbRenewalPeriodFor.Text + "',ExpiryDate='" + dtpExpiryDate.Value + "',StartReadingNo='" + txtStartReading.Text + "',EndReadingNo='" + txtEndReadingNo.Text + "',RenewalBy='" + txtRenewalBy.Text + "',ContactNo='" + txtContactNo.Text + "',Naration='" + txtNaration.Text + "',ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                    else
                        objBL.Query = "insert into PartRenewalEntry(EntryDate,EntryTime,DepartmentId,PartId,StartDate,IsCompressor,CopressorType,RenewarPeriod,RenewarPeriodFor,ExpiryDate,StartReadingNo,EndReadingNo,RenewalBy,ContactNo,Naration,UserId) values('" + dtpDate.Value.ToShortDateString() + "', '" + dtpTime.Value.ToShortTimeString() + "'," + DepartmentId + "," + PartId + ",'" + dtpStartDate.Value + "'," + IsCompressor + ",'" + cmbType.Text + "','" + txtRenewalPeriod.Text + "','" + cmbRenewalPeriodFor.Text + "','" + dtpExpiryDate.Value + "','" + txtStartReading.Text + "','" + txtEndReadingNo.Text + "','" + txtRenewalBy.Text + "','" + txtContactNo.Text + "','" + txtNaration.Text + "'," + BusinessLayer.UserId_Static + ")";

                    if (objBL.Function_ExecuteNonQuery() > 0)
                    {
                        if (!FlagDelete)
                        {
                            objBL.Query = "update PartMaster set Status='Active' where CancelTag=0 and ID="+PartId+" and Status='Inactive'";
                            objBL.Function_ExecuteNonQuery();

                            objRL.ShowMessage(7, 1);
                        }
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
            else if (DepartmentId == 0)
            {
                cmbDepartment.Focus();
                objEP.SetError(cmbDepartment, "Select Department");
                return true;
            }
            else if (PartId== 0)
            {
                lbPart.Focus();
                objEP.SetError(lbPart, "Select Part Name");
                return true;
            }
            else if (cbIsCompressor.Checked)
            {
                if (cmbType.SelectedIndex == -1)
                {
                    cmbType.Focus();
                    objEP.SetError(cmbType, "Select Compressor Type");
                    return true;
                }
                else
                    return false;
            }
            else if (!cbIsCompressor.Checked)
            {
                if (txtRenewalPeriod.Text == "")
                {
                    txtRenewalPeriod.Focus();
                    objEP.SetError(txtRenewalPeriod, "Enter Renewal Period");
                    return true;
                }
                else if (cmbRenewalPeriodFor.SelectedIndex == -1)
                {
                    cmbRenewalPeriodFor.Focus();
                    objEP.SetError(cmbRenewalPeriodFor, "Select Renewal Period For");
                    return true;
                }
                else
                    return false;
            }
            else if (dtpExpiryDate.Value <= dtpStartDate.Value)
            {
                dtpExpiryDate.Focus();
                objEP.SetError(dtpExpiryDate, "Enter Valid Date");
                return true;
            }
            else if (txtRenewalBy.Text  == "")
            {
                txtRenewalBy.Focus();
                objEP.SetError(txtRenewalBy, "Enter Renewal By");
                return true;
            }
            else if (txtContactNo.Text == "")
            {
                txtContactNo.Focus();
                objEP.SetError(txtContactNo, "Enter Contact No");
                return true;
            }
            else
                return false;
        }

        private void txtSearchPartName_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Part();
            Get_DepartmentId();
            objRL.DepartmentId = DepartmentId;
            lbPart.Visible = true;

            if (txtSearchPartName.Text != "")
            {
                objRL.Fill_Part_ListBox(lbPart, txtSearchPartName.Text, "Text");
            }
            else
            {
                lbPart.Visible = true;
                objRL.Fill_Part_ListBox(lbPart, txtSearchPartName.Text, "All");
            }
        }

        int PartId = 0;
        private void ClearAll_Part()
        {
            PartId = 0;
            rtbPartInformation.Text = "";
            gbRenewalDetails.Enabled = false;
        }

        private void lbPart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Fill_Part_Information();
            }
        }

        private void lbPart_Click(object sender, EventArgs e)
        {
            Fill_Part_Information();
        }

        private void Fill_Part_Information()
        {
            objRL.PartDetails_RTB = "";

            if (TableID == 0)
                PartId = Convert.ToInt32(lbPart.SelectedValue);

            if (PartId != 0)
            {
                rtbPartInformation.Text = "";

                objRL.Get_Part_Records_By_Id(PartId);
                if (objRL.PartDetails_RTB != "")
                {
                    rtbPartInformation.Text = objRL.PartDetails_RTB;
                    lbPart.Visible = false;
                    gbRenewalDetails.Enabled = true;
                    dtpStartDate.Focus();
                }
            }
        }

        int DepartmentId = 0;

        private void Get_DepartmentId()
        {
            if (cmbDepartment.SelectedIndex > -1)
            {
                DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue);
                objRL.DepartmentId = DepartmentId;
                objRL.Fill_Part_ListBox(lbPart, txtSearchPartName.Text, "All");
            }
        }

        private void cmbDepartment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Get_DepartmentId();
        }

        private void txtSearchPartName_Click(object sender, EventArgs e)
        {
            lbPart.Visible = true;
        }

        private void cmbRenewalPeriodFor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Set_ExpiryDate();
        }

        string PeriodType = string.Empty;
        //int Period = 0;
        double Period = 0, TotalHours = 0, MonthsDays = 0, YearDays = 0;
        private void Set_ExpiryDate()
        {
            if (!string.IsNullOrEmpty(txtRenewalPeriod.Text) && cmbRenewalPeriodFor.SelectedIndex > -1)
            {
                if (cmbRenewalPeriodFor.SelectedIndex > -1)
                {
                    Period = 0; TotalHours = 0; MonthsDays = 0; ;
                    PeriodType = cmbRenewalPeriodFor.Text;
                    Period = Convert.ToDouble(txtRenewalPeriod.Text);

                    if (PeriodType == "Days")
                        TotalHours = 24 * Period;
                    else if (PeriodType == "Months")
                    {
                        MonthsDays = Period * 30;
                        TotalHours = MonthsDays * 24;
                    }
                    else
                    {
                        YearDays = Period * 365;
                        TotalHours = YearDays * 24;
                    }
                    dtpExpiryDate.Value = dtpStartDate.Value.AddHours(TotalHours);
                }
            }
        }

        private void cmbType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Set_Copressor_Hours();
        }

        string CType = string.Empty;
        double CHours = 0;
        private void Set_Copressor_Hours()
        {
            if (cbIsCompressor.Checked)
            {
                //NA
                //LPC  2000Hrs-4000Hrs
                //HPC 4000Hrs-8000Hrs
                dtpExpiryDate.Value = DateTime.Now;
                if (cmbType.SelectedIndex > -1)
                {
                    CType = string.Empty;
                    CType = cmbType.Text;
                    if (CType == "LPC  2000Hrs-4000Hrs")
                        CHours = 2000;
                    else if (CType == "HPC 4000Hrs-8000Hrs")
                        CHours = 4000;
                    else
                        CHours = 0;

                    dtpExpiryDate.Value = dtpStartDate.Value.AddHours(CHours);
                }
            }
        }

        private void txtRenewalPeriod_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtRenewalPeriod);
        }

        private void cbIsCompressor_CheckedChanged(object sender, EventArgs e)
        {
            Set_Compressor();
        }

        private void Set_Compressor()
        {
            dtpExpiryDate.Value = DateTime.Now;
            if (cbIsCompressor.Checked)
            {
                cmbType.Visible = true;
                pnRenewal.Visible = false;
                txtRenewalPeriod.Text = ""; cmbRenewalPeriodFor.SelectedIndex = -1;
            }
            else
            {
                cmbType.SelectedIndex = -1;
                cmbType.Visible = false;
                pnRenewal.Visible = true;
                //txtRenewalPeriod.Text = ""; cmbRenewalPeriodFor.SelectedIndex = -1;
            }
        }

        private void PartRenewalEntry_Load(object sender, EventArgs e)
        {
            ClearAll();
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
                    //1 PRE.EntryDate,
                    //2 PRE.EntryTime,
                    //3 PRE.DepartmentId,
                    //4 L.UserName as [Department],
                    //5 PRE.PartId,
                    //6 PM.PartName as [Part Name],
                    //7 PRE.StartDate,
                    //8 PRE.IsCompressor,
                    //9 PRE.CopressorType,
                    //10 PRE.RenewarPeriod,
                    //11 PRE.RenewarPeriodFor,
                    //12 PRE.ExpiryDate,
                    //13 PRE.StartReadingNo,
                    //14 EndReadingNo,
                    //15 PRE.RenewalBy,
                    //16 PRE.ContactNo
                    //17 PRE.Naration

                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    DepartmentId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                    cmbDepartment.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    PartId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                    Fill_Part_Information();
                    dtpStartDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                    IsCompressor = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                    if (IsCompressor == 1)
                    {
                       // pnRenewal.Visible = false;
                        cbIsCompressor.Checked = true;
                        cmbType.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    }
                    else
                    {
                        //pnRenewal.Visible = true;
                        cbIsCompressor.Checked = false;
                        txtRenewalPeriod.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                        cmbRenewalPeriodFor.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    }
                    dtpExpiryDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
                 
                    txtStartReading.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                    txtEndReadingNo.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                    txtRenewalBy.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                    txtContactNo.Text = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
                    txtNaration.Text = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
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
 
        private void cmbDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSearchPartName.Focus();
        }

        private void txtSearchPartName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpStartDate.Focus();
        }

        private void dtpStartDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cbIsCompressor.Focus();
        }

        private void cbIsCompressor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(cbIsCompressor.Checked)
                    cmbType.Focus();
            }
        }

        private void cmbType_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbIsCompressor.Checked)
            {
                if (e.KeyCode == Keys.Enter)
                    txtRenewalPeriod.Focus();
            }
        }

        private void txtRenewalPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbRenewalPeriodFor.Focus();
        }

        private void cmbRenewalPeriodFor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpExpiryDate.Focus();
        }

        private void dtpExpiryDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtStartReading.Focus();
        }

        private void txtStartReading_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtEndReadingNo.Focus();
        }

        private void txtEndReadingNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRenewalBy.Focus();
        }

        private void txtRenewalBy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtContactNo.Focus();
        }

        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNaration.Focus();
        }

        private void txtNaration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

    
    }
}
