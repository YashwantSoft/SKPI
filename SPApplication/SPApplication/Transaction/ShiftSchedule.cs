using BusinessLayerUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPApplication.Transaction
{
    public partial class ShiftSchedule : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false;
        string WhereClause = string.Empty;
        string MainQuery = string.Empty;

        public ShiftSchedule()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_SHIFTSCHEDULE);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
        }

        private void ShiftSchedule_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FlagDelete = true;
            SaveDB();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ShiftScheduleId from ShiftSchedule where CancelTag=0 and ShiftDate=#" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and ShiftScheduleId <> " + TableID + ""; // dtpShiftDate FromDate='" + dtpFromDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "' and ToDate='" + dtpToDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "' and ShiftScheduleId <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        string NoOfShifts=string.Empty;

        private bool Validation_CheckListBox()
        {
            objEP.Clear();
            bool checkCheck = false;

            for (int i = 0; i < clbNoOfShift.Items.Count; i++)
            {
                checkCheck = clbNoOfShift.GetItemChecked(i);
                if (checkCheck)
                    break;
            }
            return checkCheck;
        }

        string ShiftName_List = string.Empty;

        private void Get_Shift_CheckBox()
        {
            NoOfShifts = string.Empty;
            for (int i = 0; i < clbNoOfShift.CheckedItems.Count; i++)
            {
                NoOfShifts += clbNoOfShift.Items[i].ToString() +",";
            }

            if (!string.IsNullOrEmpty(Convert.ToString(NoOfShifts)))
                NoOfShifts = NoOfShifts.Remove(NoOfShifts.Length - 1);

        }

        private void Set_Shift_Checkbox()
        {
            List<string> listStrLineElements = NoOfShifts.Split(',').ToList();

            //var valueArray = valueList.Split(',');
            for (int i = 0; i < clbNoOfShift.Items.Count; i++)
            {
                if (listStrLineElements.Contains(clbNoOfShift.Items[i].ToString()))
                {
                    clbNoOfShift.SetItemChecked(i, true);
                }
            }
        }

        private void SaveDB()
        {
            if (!Validation())
            {
                if (!CheckExist())
                {
                    Get_Shift_CheckBox();

                    if (TableID != 0)
                        if (FlagDelete)
                            objBL.Query = "update ShiftSchedule set CancelTag=1 where ShiftScheduleId=" + TableID + "";
                        else
                            objBL.Query = "update ShiftSchedule set EntryDate='" + dtpDate.Value.ToShortDateString() + "',EntryTime='" + dtpTime.Value.ToShortTimeString() + "',FromDate='" + dtpFromDate.Value.ToShortDateString() + "',ToDate='" + dtpToDate.Value.ToShortDateString() + "',ShiftDate='" + dtpShiftDate.Value.ToShortDateString() + "',ShiftHours='" + cmbShiftHours.Text + "',NoOfShifts='" + NoOfShifts + "',BeginTime1='" + dtpBeginTime1.Value.ToShortTimeString() + "',EndTime1='" + dtpEndTime1.Value.ToShortTimeString() + "',BeginTime2='" + dtpBeginTime2.Value.ToShortTimeString() + "',EndTime2='" + dtpEndTime2.Value.ToShortTimeString() + "',BeginTime3='" + dtpBeginTime3.Value.ToShortTimeString() + "',EndTime3='" + dtpEndTime3.Value.ToShortTimeString() + "',Naration='" + objRL.ApostropheSave(txtNaration.Text) + "',ModifiedId=" + BusinessLayer.UserId_Static + " where ShiftScheduleId=" + TableID + "";
                    else
                        objBL.Query = "insert into ShiftSchedule(EntryDate,EntryTime,FromDate,ToDate,ShiftDate,ShiftHours,NoOfShifts,BeginTime1,EndTime1,BeginTime2,EndTime2,BeginTime3,EndTime3,Naration,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "','" + dtpFromDate.Value.ToShortDateString() + "','" + dtpToDate.Value.ToShortDateString() + "','" + dtpShiftDate.Value.ToShortDateString() + "','" + cmbShiftHours.Text + "','" + NoOfShifts + "','" + dtpBeginTime1.Value.ToShortTimeString() + "','" + dtpEndTime1.Value.ToShortTimeString() + "','" + dtpBeginTime2.Value.ToShortTimeString() + "','" + dtpEndTime2.Value.ToShortTimeString() + "','" + dtpBeginTime3.Value.ToShortTimeString() + "','" + dtpEndTime3.Value.ToShortTimeString() + "', '" + objRL.ApostropheSave(txtNaration.Text) + "'," + BusinessLayer.UserId_Static + ")";

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

            if (cmbShiftHours.SelectedIndex == -1)
            {
                cmbShiftHours.Focus();
                objEP.SetError(cmbShiftHours, "Select Shift Hours");
                return true;
            }
            else if (!Validation_CheckListBox())
            {
                clbNoOfShift.Focus();
                objEP.SetError(clbNoOfShift, "Select Shift");
                return true;
            }
            else
                return false;
        }

        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();
            WhereClause = string.Empty;
            MainQuery = string.Empty;

            MainQuery = "select ShiftScheduleId,EntryDate as [Entry Date],EntryTime as [Entry Time],FromDate as [From Date],ToDate as [To Date],ShiftDate as [Shift Date],ShiftHours as [Shift Hours],NoOfShifts as [No of Shifts],BeginTime1,EndTime1,BeginTime2,EndTime2,BeginTime3,EndTime3,Naration from ShiftSchedule where CancelTag=0 ";
            
            //MainQuery = "select ShiftScheduleId,EntryDate as [Entry Date],EntryTime as [Entry Time],FromDate as [From Date],ToDate as [To Date],ShiftDate as [Shift Date],ShiftHours as [Shift Hours],NoOfShifts as [No of Shifts],BeginTime1,EndTime1,BeginTime2,EndTime2,BeginTime3,EndTime3,Naration from ShiftSchedule where CancelTag=0 ";

            if (SearchTag)
                WhereClause = " and EntryDate='" + dtpSearchDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY);

            objBL.Query = MainQuery + WhereClause+ "order by ShiftScheduleId desc";
            ds = objBL.ReturnDataSet();
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 ShiftScheduleId,
                //1 EntryDate,
                //2 EntryTime,
                //3 FromDate,
                //4 ToDate,
                //5 ShiftDate as [Shift Date],
                //6 ShiftHours,
                //7 NoOfShifts,
                //8 BeginTime1,
                //9 EndTime1
                //10 BeginTime2,
                //11 EndTime2,
                //12 BeginTime3,
                //13 EndTime3
                //14 Naration,

                dataGridView1.Rows.Clear();
                int RowCountGrid = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[RowCountGrid].Cells["clmShiftScheduleId"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ShiftScheduleId"]));
                    dataGridView1.Rows[RowCountGrid].Cells["clmEntryDate"].Value = objRL.Return_Date_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Entry Date"])));
                    dataGridView1.Rows[RowCountGrid].Cells["clmEntryTime"].Value = objRL.Return_Time_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Entry Time"])));
                    dataGridView1.Rows[RowCountGrid].Cells["clmFromDate"].Value = objRL.Return_Date_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["From Date"])));
                    dataGridView1.Rows[RowCountGrid].Cells["clmToDate"].Value = objRL.Return_Date_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["To Date"])));
                    dataGridView1.Rows[RowCountGrid].Cells["clmShiftDate"].Value = objRL.Return_Date_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift Date"])));
                    dataGridView1.Rows[RowCountGrid].Cells["clmShiftHours"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Shift Hours"]));
                    dataGridView1.Rows[RowCountGrid].Cells["clmNoOfShifts"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["No of Shifts"]));
                    dataGridView1.Rows[RowCountGrid].Cells["clmBeginTime1"].Value = objRL.Return_Time_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["BeginTime1"])));
                    dataGridView1.Rows[RowCountGrid].Cells["clmEndTime1"].Value = objRL.Return_Time_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["EndTime1"])));
                    dataGridView1.Rows[RowCountGrid].Cells["clmBeginTime2"].Value = objRL.Return_Time_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["BeginTime2"])));
                    dataGridView1.Rows[RowCountGrid].Cells["clmEndTime2"].Value = objRL.Return_Time_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["EndTime2"])));
                    dataGridView1.Rows[RowCountGrid].Cells["clmBeginTime3"].Value = objRL.Return_Time_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["BeginTime3"])));
                    dataGridView1.Rows[RowCountGrid].Cells["clmEndTime3"].Value = objRL.Return_Time_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["EndTime3"])));
                    dataGridView1.Rows[RowCountGrid].Cells["clmNaration"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Naration"]));
                    dataGridView1.Columns["clmFromDate"].Visible = false;
                    dataGridView1.Columns["clmToDate"].Visible = false;
                    RowCountGrid++;
                }

                //dataGridView1.DataSource = ds.Tables[0];
                //dataGridView1.Columns[0].Visible = false;
                //dataGridView1.Columns[3].Visible = false;
                //dataGridView1.Columns[4].Visible = false;
                //dataGridView1.Columns[1].Width = 120;
                //dataGridView1.Columns[2].Width = 120;
                //dataGridView1.Columns[3].Width = 120;
                //dataGridView1.Columns[4].Width = 120;
                //dataGridView1.Columns[5].Width = 120;
                //dataGridView1.Columns[6].Width = 120;
                //dataGridView1.Columns[7].Width = 120;
                //dataGridView1.Columns[8].Width = 120;
                //dataGridView1.Columns[9].Width = 120;
                //dataGridView1.Columns[10].Width = 120;
                //dataGridView1.Columns[11].Width = 120;
                //dataGridView1.Columns[12].Width = 120;
                //dataGridView1.Columns[13].Width = 120;
                //dataGridView1.Columns[14].Width = 120;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        private void Clear_Shifts(bool checkFT)
        {
            for (int i = 0; i < clbNoOfShift.Items.Count; i++)
            {
                clbNoOfShift.SetItemChecked(i,checkFT);
            }
        }

        protected void ClearAll()
        {
            TableID = 0;
            objEP.Clear();
            FlagDelete = false;
            btnDelete.Enabled = false;
            cmbShiftHours.SelectedIndex = -1;
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            dtpShiftDate.Value = DateTime.Now.Date;
            cmbShiftHours.SelectedIndex = -1;
            Clear_Shifts(false);
            dtpBeginTime1.Value = DateTime.Now;
            dtpEndTime1.Value = DateTime.Now;
            dtpBeginTime2.Value = DateTime.Now;
            dtpEndTime2.Value = DateTime.Now;
            dtpBeginTime3.Value = DateTime.Now;
            dtpEndTime3.Value = DateTime.Now;
            txtNaration.Text = "";
            NoOfShifts = string.Empty;
            dtpFromDate.Focus();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RowCount_Grid = dataGridView1.Rows.Count;
                CurrentRowIndex = dataGridView1.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    //0 ShiftScheduleId,
                    //1 EntryDate,
                    //2 EntryTime,
                    //3 FromDate,
                    //4 ToDate,
                    //5 ShiftHours,
                    //6 NoOfShifts,
                    //7 BeginTime1,
                    //8 EndTime1
                    //9 BeginTime2,
                    //10 EndTime2,
                    //11 BeginTime3,
                    //12 EndTime3
                    //13 Naration

                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    dtpDate.Value =Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    dtpFromDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    dtpToDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                    dtpShiftDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                    cmbShiftHours.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    NoOfShifts = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    Set_Shift_Checkbox();
                    dtpBeginTime1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());
                    dtpEndTime1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString());
                    dtpBeginTime2.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString());
                    dtpEndTime2.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString());
                    dtpBeginTime3.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString());
                    dtpEndTime3.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString());
                    txtNaration.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
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

        private void clbNoOfShift_SelectedValueChanged(object sender, EventArgs e)
          {
            //Set_Shift_GroupBox();
        }


        private void Set_Shift_GroupBox()
        {
            gb1.Visible = false;
            gb2.Visible = false;
            gb3.Visible = false;
            if (clbNoOfShift.CheckedItems.Count > 0)
            {
                for (int i = 0; i < clbNoOfShift.CheckedItems.Count; i++)
                {
                    if (clbNoOfShift.Items[i].ToString() == "1st Shift")
                    {
                        gb1.Visible = true;
                        dtpBeginTime1.Text = "07:00";
                        dtpEndTime1.Text = "15:00";
                    }
                    if (clbNoOfShift.Items[i].ToString() == "2nd Shift")
                    {
                        gb2.Visible = true;
                        dtpBeginTime2.Text = "15:00";
                        dtpEndTime2.Text = "23:00";
                    }
                    if (clbNoOfShift.Items[i].ToString() == "3rd Shift")
                    {
                        gb3.Visible = true;
                        dtpBeginTime3.Text = "23:00";
                        dtpEndTime3.Text = "07:00";
                    }
                }
            }
        }

        private void clbNoOfShift_ItemCheck(object sender, ItemCheckEventArgs e)
        {
           // Set_Shift_GroupBox();
        }

        private void clbNoOfShift_Leave(object sender, EventArgs e)
        {
            Set_Shift_GroupBox();
        }

        private void clbNoOfShift_MouseClick(object sender, MouseEventArgs e)
         {
            //Set_Shift_GroupBox();
        }
    }
}
