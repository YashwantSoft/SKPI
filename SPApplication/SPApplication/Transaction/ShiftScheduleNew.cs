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
    public partial class ShiftScheduleNew : Form
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

        public ShiftScheduleNew()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_SHIFTSCHEDULE);
            SetDesign();
            GetID();
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("ShiftEntryNew"));
            txtShiftID.Text = IDNo.ToString();
        }

        private void SetDesign()
        {
            dtpBeginTime1.Format = DateTimePickerFormat.Custom;
            dtpBeginTime1.CustomFormat = "dd/MM/yyyy HH:mm";

            dtpEndTime1.Format = DateTimePickerFormat.Custom;
            dtpEndTime1.CustomFormat = "dd/MM/yyyy HH:mm";

            dtpBeginTime2.Format = DateTimePickerFormat.Custom;
            dtpBeginTime2.CustomFormat = "dd/MM/yyyy HH:mm";

            dtpEndTime2.Format = DateTimePickerFormat.Custom;
            dtpEndTime2.CustomFormat = "dd/MM/yyyy HH:mm";

            dtpBeginTime3.Format = DateTimePickerFormat.Custom;
            dtpBeginTime3.CustomFormat = "dd/MM/yyyy HH:mm";

            dtpEndTime3.Format = DateTimePickerFormat.Custom;
            dtpEndTime3.CustomFormat = "dd/MM/yyyy HH:mm";
        }

        private void ShiftScheduleNew_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            ShiftGet();
        }
        
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;

        //bool SearchTag = false;
        protected void FillGrid()
        {
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;
            UserClause = string.Empty;

            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            //if (!SearchTag)
            //    WhereClause = " and EntryDate=#" + dtpSearchDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "#";

            MainQuery = "select ID,EntryDate as [Date],Format([EntryTime], 'hh:nn:ss AM/PM') as [Time],Shift,ShifFromDate as [Shift From Date],ShiftToDate as [Shift To Date],ShiftHours as [Shift Hours],ShiftAll,Narration from ShiftEntryNew where CancelTag=0 ";

            OrderByClause = " order by ID desc";
            objBL.Query = MainQuery + WhereClause + OrderByClause;

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 distinct EntryDate as [Date],
                //1 ID,
                //2 Format([EntryTime], 'hh:nn:ss AM/PM') as [Time],
                //3 Shift,
                //4 ShifFromDate as [Shift From Date],
                //5 ShiftToDate as [Shift To Date],
                //6 ShiftHours as [Shift Hours],
                //7 ShiftAll,
                //8 Narration
                 
                dataGridView1.DataSource = ds.Tables[0];
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;

                //dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 60;
                dataGridView1.Columns[4].Width = 150;
                dataGridView1.Columns[5].Width = 150;
                dataGridView1.Columns[6].Width = 150;
                dataGridView1.Columns[7].Width = 150;
                dataGridView1.Columns[8].Width = 150;
            }
        }

        private void ShiftGet()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select * from ShiftEntryNew where #" + DateTime.Now + "# between ShifFromDate AND ShiftToDate";
            ds = objBL.ReturnDataSet();
            
            if (ds.Tables[0].Rows.Count > 0)
            {

            }
        }

        private void clbNoOfShift_Leave(object sender, EventArgs e)
        {
            if (clbNoOfShift.CheckedItems.Count > 0 && cmbShiftHours.SelectedIndex > -1)
                Set_Shift_GroupBox();
        }

        int MM = 0, HH = 0;

        private void Set_DTP(DateTimePicker dtp, int HH, int MM, bool flag3shift)
        {
            DateTime DT1 ;

            if (!flag3shift)
                DT1 = DateTime.Now.Date;
            else
                DT1 = DateTime.Now.Date.AddDays(1);
             
            // Create a DateTime object with your desired date and time
            DateTime customDateTime = new DateTime(DT1.Year, DT1.Month, DT1.Day, HH, MM, 0);
            dtp.Value = customDateTime;
        }

        private void Set_Shift_GroupBox()
        {
            string Shift_V = string.Empty;

            VisibleFalse(false);

            if (cmbShiftHours.SelectedIndex > -1 && cmbShifts.SelectedIndex > -1)
            {
                ShiftHours = cmbShiftHours.Text;
                MM = 0; HH = 0;

                Shift_V = cmbShifts.Text;

                if (Shift_V == "1,2,3")
                {
                    VisibleFalse(true);

                    if (ShiftHours == "360")
                    {
                        Set_DTP(dtpBeginTime1, 07, 01, false);
                        Set_DTP(dtpEndTime1, 13, 00, false);
                        Set_DTP(dtpBeginTime2, 13, 01, false);
                        Set_DTP(dtpEndTime2, 19, 00, false);
                        Set_DTP(dtpBeginTime3, 19, 01, false);
                        Set_DTP(dtpEndTime3, 01, 00, true);
                    }
                    else if (ShiftHours == "480")
                    {
                        Set_DTP(dtpBeginTime1, 07, 01, false);
                        Set_DTP(dtpEndTime1, 15, 00, false);
                        Set_DTP(dtpBeginTime2, 15, 01, false);
                        Set_DTP(dtpEndTime2, 23, 00, false);
                        Set_DTP(dtpBeginTime3, 23, 01, false);
                        Set_DTP(dtpEndTime3, 07, 00, true);
                    }
                    //else if (ShiftHours == "720")
                    //{
                    //    Set_DTP(dtpBeginTime1, 07, 01, false);
                    //    Set_DTP(dtpEndTime1, 19, 00, false);
                    //    Set_DTP(dtpBeginTime2, 19, 01, false);
                    //    Set_DTP(dtpEndTime2, 07, 00, true);
                    //}
                    else
                    {

                    }
                }

                if (Shift_V == "1,2")
                {
                    gb1.Visible = true;
                    gb2.Visible = true;
                    if (ShiftHours == "360")
                    {
                        Set_DTP(dtpBeginTime1, 07, 01, false);
                        Set_DTP(dtpEndTime1, 13, 00, false);
                        Set_DTP(dtpBeginTime2, 13, 01, false);
                        Set_DTP(dtpEndTime2, 19, 00, false);
                    }
                    else if (ShiftHours == "480")
                    {
                        Set_DTP(dtpBeginTime1, 07, 01, false);
                        Set_DTP(dtpEndTime1, 15, 00, false);
                        Set_DTP(dtpBeginTime2, 15, 01, false);
                        Set_DTP(dtpEndTime2, 23, 00, false);
                    }
                    else if (ShiftHours == "720")
                    {
                        Set_DTP(dtpBeginTime1, 07, 01, false);
                        Set_DTP(dtpEndTime1, 19, 00, false);
                        Set_DTP(dtpBeginTime2, 19, 01, false);
                        Set_DTP(dtpEndTime2, 07, 00, true);
                    }
                    else
                    {

                    }
                }

                if (Shift_V == "1")
                {
                    gb1.Visible = true;
                    if (ShiftHours == "360")
                    {
                        Set_DTP(dtpBeginTime1, 13, 01, false);
                        Set_DTP(dtpBeginTime1, 19, 00, false);
                    }
                    if (ShiftHours == "480")
                    {
                        Set_DTP(dtpBeginTime1, 07, 01, false);
                        Set_DTP(dtpEndTime1, 15, 00, false);
                    }
                    else if (ShiftHours == "720")
                    {
                        Set_DTP(dtpBeginTime1, 19, 01, false);
                        Set_DTP(dtpBeginTime1, 07, 00, true);
                    }
                    else
                    {

                    }
                }
            }
        }

        private void VisibleFalse(bool tf)
        {
            gb1.Visible = tf;
            gb2.Visible = tf;
            gb3.Visible = tf;
        }

        private void Set_Shift_GroupBox1()
        {
            VisibleFalse(false);

            if (clbNoOfShift.CheckedItems.Count > 0)
            {
                ShiftHours= cmbShiftHours.Text;
                MM = 0; HH = 0;
                foreach (object itemChecked in clbNoOfShift.CheckedItems)
                {
                    DataRowView castedItem = itemChecked as DataRowView;
                    string Shift_V = itemChecked.ToString(); // Convert.ToString(castedItem[1]);

                    if (Shift_V != "")
                    {
                        if (Shift_V == "1st Shift")
                        {
                            if (ShiftHours == "360")
                            {
                                Set_DTP(dtpBeginTime1, 07, 01, false);
                                Set_DTP(dtpEndTime1, 13, 00, false);
                            }
                            else if (ShiftHours == "480")
                            {
                                Set_DTP(dtpBeginTime1, 07, 01, false);
                                Set_DTP(dtpEndTime1, 15, 00, false);
                            }
                            else if (ShiftHours == "720")
                            {
                                Set_DTP(dtpBeginTime1, 07, 01, false);
                                Set_DTP(dtpEndTime1, 19, 00, false);
                            }
                            else
                            {

                            }
                            gb1.Visible = true;
                        }

                        if (Shift_V == "2nd Shift")
                        {
                            if (ShiftHours == "360")
                            {
                                Set_DTP(dtpBeginTime2, 13, 01, false);
                                Set_DTP(dtpEndTime2, 19, 00, false);
                            }
                            else if (ShiftHours == "480")
                            {
                                Set_DTP(dtpBeginTime2, 15, 01, false);
                                Set_DTP(dtpEndTime2, 23, 00, false);
                            }
                            else if (ShiftHours == "720")
                            {
                                Set_DTP(dtpBeginTime2, 19, 01, false);
                                Set_DTP(dtpEndTime2, 07, 00, true);
                            }
                            else
                            {

                            }
                            gb2.Visible = true;
                        }

                        if (Shift_V == "3rd Shift")
                        {
                            //if (ShiftHours == "360")
                            //{
                            //    Set_DTP(dtpBeginTime1, 13, 01, false);
                            //    Set_DTP(dtpBeginTime1, 19, 00, false);
                            //}
                            if (ShiftHours == "480")
                            {
                                Set_DTP(dtpBeginTime3, 23, 01, false);
                                Set_DTP(dtpEndTime3, 07, 00, true);
                            }
                            //else if (ShiftHours == "720")
                            //{
                            //    Set_DTP(dtpBeginTime1, 19, 01, false);
                            //    Set_DTP(dtpBeginTime1, 07, 00, true);
                            //}
                            else
                            {

                            }

                            gb3.Visible = true;
                        }
                    }
                }


                //for (int i = 0; i < clbNoOfShift.CheckedItems.Count; i++)
                //{
                //    if (clbNoOfShift.Items[i].ToString() == "1st Shift")
                //    {
                //        gb1.Visible = true;
                //        dtpBeginTime1.Text = "07:01";
                //        dtpEndTime1.Text = "15:00";
                //    }
                //    if (clbNoOfShift.Items[i].ToString() == "2nd Shift")
                //    {
                //        gb2.Visible = true;
                //        dtpBeginTime2.Text = "15:01";
                //        dtpEndTime2.Text = "23:00";
                //    }
                //    if (clbNoOfShift.Items[i].ToString() == "3rd Shift")
                //    {
                //        gb3.Visible = true;
                //        DateTime DT1= DateTime.Now.Date.AddDays(1);
                //        //dtpBeginTime3.Text = "23:01";
                //        //dtpEndTime3.Text = DT1 + "07:00";

                //        // Create a DateTime object with your desired date and time
                //        DateTime customDateTime = new DateTime(DT1.Year, DT1.Month, DT1.Day , 07, 00, 0); // Year, Month, Day, Hour, Minute, Second

                //        // Set the DateTimePicker's Value to the custom DateTime
                //        dtpEndTime3.Value = customDateTime;

                //    }
                //}
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Fill_Shift()
        {
            if (cmbShiftHours.SelectedIndex > -1)
            {
                VisibleFalse(false);
                cmbShifts.Items.Clear();
                if (cmbShiftHours.Text == "360" || cmbShiftHours.Text == "720")
                {
                    //cmbShifts.Items.Add("1,2,3");
                    cmbShifts.Items.Add("1,2");
                    cmbShifts.Items.Add("1");
                }
                else if (cmbShiftHours.Text == "480")
                {
                    cmbShifts.Items.Add("1,2,3");
                    cmbShifts.Items.Add("1,2");
                    cmbShifts.Items.Add("1");
                }
                else
                {
                }
            }
        }

        private void cmbShiftHours_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbShiftHours.SelectedIndex > -1)
            {
                Fill_Shift();
                //Set_Shift_GroupBox();
            }
        }

        string Shift = string.Empty, ShiftHours = string.Empty;

        private void cmbShifts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbShiftHours.SelectedIndex > -1 && cmbShifts.SelectedIndex > -1)
                Set_Shift_GroupBox();
        }

        private bool Validation()
        {
            objEP.Clear();

            if (cmbShiftHours.SelectedIndex == -1)
            {
                cmbShiftHours.Focus();
                objEP.SetError(cmbShiftHours, "Select Shift Hours");
                return true;
            }
            else if (cmbShifts.SelectedIndex == -1)
            {
                cmbShifts.Focus();
                objEP.SetError(cmbShifts, "Select Shifts");
                return true;
            }
            else if (txtShiftID.Text == "")
            {
                txtShiftID.Focus();
                objEP.SetError(txtShiftID, "Enter Bags");
                return true;
            }
            //else if (dgvProduct.Rows.Count == 0 && dgvCap.Rows.Count == 0 && dgvWad.Rows.Count == 0)
            //{
            //    dgvProduct.Focus();
            //    dgvCap.Focus();
            //    dgvWad.Focus();
            //    objEP.SetError(dgvProduct, "Enter Product");
            //    objEP.SetError(dgvCap, "Enter Cap");
            //    objEP.SetError(dgvWad, "Enter Wad");
            //    return true;
            //}

            else
                return false;
        }

        private bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from ShiftEntryNew where CancelTag=0 and ID <> "+TableID+" and EntryDate=#" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# ";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0][0])))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private void SaveDB()
        {
            if (!Validation())
            {
                if (!CheckExist())
                {
                    if (cmbShifts.SelectedIndex > -1)
                    {
                        if (cmbShifts.Text == "1,2,3")
                        {
                            InsertData("1", dtpBeginTime1.Value, dtpEndTime1.Value);
                            InsertData("2", dtpBeginTime2.Value, dtpEndTime2.Value);
                            InsertData("3", dtpBeginTime3.Value, dtpEndTime3.Value);
                        }
                        else if (cmbShifts.Text == "1,2")
                        {
                            InsertData("1", dtpBeginTime1.Value, dtpEndTime1.Value);
                            InsertData("2", dtpBeginTime2.Value, dtpEndTime2.Value);
                        }
                        else if (cmbShifts.Text == "1")
                        {
                            InsertData("1", dtpBeginTime1.Value, dtpEndTime1.Value);
                        }
                        else
                        {

                        }

                        objRL.ShowMessage(7, 1);
                        ClearAll();
                        FillGrid();
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

        DateTime dtBegin,dtEnd;

        private void InsertData(string Shift_V,DateTime dtBegin,DateTime dtEnd)
        {
            //objBL.Query = "insert into ShiftEntryNew(EntryDate,EntryTime,Shift,PlantInchargeId,VolumeCheckerId,Naration,ShifFromDate,ShiftToDate) values()";
            objBL.Query = "insert into ShiftEntryNew(EntryDate,EntryTime,Shift,ShiftHours,ShifFromDate,ShiftToDate,ShiftAll,Narration) values('" + dtpShiftDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "','" + Shift_V + "','" + cmbShiftHours.Text + "','" + dtBegin + "','" + dtEnd + "','" + cmbShifts.Text + "','" + txtNarration.Text + "')";
            int Result = objBL.Function_ExecuteNonQuery();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDB();
        }

        private void ClearAll()
        {
            TableID = 0;
            objEP.Clear();
            dtpShiftDate.Value = DateTime.Now.Date;
            dtpTime.Value = DateTime.Now;

            cmbShiftHours.SelectedIndex = -1;
            cmbShifts.SelectedIndex = -1;

            gb1.Visible = false;
            gb2.Visible = false;
            gb3.Visible = false;

            GetID();
            cmbShiftHours.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void Fill_Shift_ByDate()
        {
            DataSet ds=new DataSet();
            objBL.Query = "select ID,EntryDate as [Date],Format([EntryTime], 'hh:nn:ss AM/PM') as [Time],Shift,ShifFromDate as [Shift From Date],ShiftToDate as [Shift To Date],ShiftHours as [Shift Hours],ShiftAll,Narration from ShiftEntryNew where CancelTag=0 and EntryDate=#" + dtpShiftDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# order by ID asc";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                string Shift_V = string.Empty;

                //0 ID,
                //1 EntryDate as [Date],
                //2 Format([EntryTime], 'hh:nn:ss AM/PM') as [Time],
                //3 Shift,
                //4 ShifFromDate as [Shift From Date],
                //5 ShiftToDate as [Shift To Date],
                //6 ShiftHours as [Shift Hours],
                //7 ShiftAll
                //8 Naration,
                 
                cmbShiftHours.Text = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0][6]));

                if(cmbShiftHours.SelectedIndex >-1)
                    Fill_Shift();

                Shift_V = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0][7]));
                cmbShifts.Text = Shift_V;
                 
                if (cmbShiftHours.SelectedIndex > -1 && cmbShifts.SelectedIndex > -1)
                    Set_Shift_GroupBox();


                if (Shift_V == "1,2,3")
                {
                    dtpBeginTime1.Value = Convert.ToDateTime(ds.Tables[0].Rows[0][4]);
                    dtpEndTime1.Value = Convert.ToDateTime(ds.Tables[0].Rows[0][5]);

                    dtpBeginTime2.Value = Convert.ToDateTime(ds.Tables[0].Rows[1][4]);
                    dtpEndTime2.Value = Convert.ToDateTime(ds.Tables[0].Rows[1][5]);

                    dtpBeginTime3.Value = Convert.ToDateTime(ds.Tables[0].Rows[2][4]);
                    dtpEndTime3.Value = Convert.ToDateTime(ds.Tables[0].Rows[2][5]);
                }
                else if (Shift_V == "1,2")
                {
                    dtpBeginTime1.Value = Convert.ToDateTime(ds.Tables[0].Rows[0][4]);
                    dtpEndTime1.Value = Convert.ToDateTime(ds.Tables[0].Rows[0][5]);

                    dtpBeginTime2.Value = Convert.ToDateTime(ds.Tables[0].Rows[1][4]);
                    dtpEndTime2.Value = Convert.ToDateTime(ds.Tables[0].Rows[1][5]);
                }
                else if (cmbShifts.Text == "1")
                {
                    dtpBeginTime1.Value = Convert.ToDateTime(ds.Tables[0].Rows[0][4]);
                    dtpEndTime1.Value = Convert.ToDateTime(ds.Tables[0].Rows[0][5]);
                }
                else
                {

                }
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
                    ClearAll();
                    btnDelete.Enabled = true;

                    //0 ID,
                    //1 distinct EntryDate as [Date],
                    //2 Format([EntryTime], 'hh:nn:ss AM/PM') as [Time],
                    //3 Shift,
                    //4 ShifFromDate as [Shift From Date],
                    //5 ShiftToDate as [Shift To Date],
                    //6 ShiftHours as [Shift Hours],
                    //7 ShiftAll,
                    //8 Narration

                    //ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

                    txtShiftID.Text = TableID.ToString();
                    dtpShiftDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    Fill_Shift_ByDate();
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

        private void dtpSearchDate_ValueChanged(object sender, EventArgs e)
        {
            SearchTag = true;
        }

        //private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        RowCount_Grid = dataGridView1.Rows.Count;
        //        CurrentRowIndex = dataGridView1.CurrentRow.Index;

        //        if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
        //        {
        //            ClearAll();
        //            btnDelete.Enabled = true;

        //            //0 ID,
        //            //1 EntryDate as [Date],
        //            //2 EntryTime as [Time],
        //            //3 Shift,
        //            //4 MachinNo as [Machine No],
        //            //5 ProductId,
        //            //6 ProductName as [Product Name],
        //            //7 PurchaseOrderNo as [PO Number],
        //            //8 ProdutQty as [Bag Qty],
        //            //9 StickerHeader as [Sticker Header],
        //            //10 DateFlag
        //            //11 ,PE.PreformPartyId
        //            //12 PreformParty as [Preform Party
        //            //13 FromRange [From],
        //            //14 ToRange as [To]
        //            //15 Used Sticker
        //            //btnPrint.Visible = true;
        //            TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);


        //            txtID.Text = TableID.ToString();
        //            dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
        //            dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
        //            txtShift.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        //            cmbMachineNo.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        //            Fill_Machine_Information();
        //            ProductId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
        //            Fill_Product_Information();
        //            lblProductName.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        //            txtPurchaseOrderNo.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
        //            txtQty.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        //            rtbStickerHeader.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
        //            cmbDate.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
        //            cmbPreformParty.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();

        //            txtFrom.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[13].Value));
        //            txtTo.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[14].Value));
        //            txtUsedSticker.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rdows[e.RowIndex].Cells[15].Value));

        //            lblUsedSticker.Visible = true;
        //            txtUsedSticker.Visible = true;
        //        }
        //    }
        //    catch (Exception ex1)
        //    {
        //        objRL.ErrorMessge(ex1.ToString());
        //        return;
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }
        //}

        //private void Set_Shift_GroupBox()
        //{
        //    Shift = string.Empty; ShiftHours = string.Empty;

        //    if (cmbShiftHours.SelectedIndex > -1)
        //    {
        //        //Shift = cmbShift.Text;
        //        ShiftHours = cmbShiftHours.Text;

        //        if (Shift == "I")
        //        {
        //            if (ShiftHours == "360")
        //                Set_Shift("07:00", "13:00");
        //            else if (ShiftHours == "480")
        //                Set_Shift("07:00", "15:00");
        //            else if (ShiftHours == "720")
        //                Set_Shift("07:00", "19:00");
        //            else
        //            {

        //            }
        //        }
        //        if (Shift == "II")
        //        {
        //            if (ShiftHours == "360")
        //                Set_Shift("13:00", "19:00");
        //            else if (ShiftHours == "480")
        //                Set_Shift("15:00", "23:00");
        //            else if (ShiftHours == "720")
        //                Set_Shift("19:00", "07:00");
        //            else
        //            {
        //            }
        //        }
        //        if (Shift == "III")
        //        {
        //            if (ShiftHours == "360")
        //                Set_Shift("19:00", "23:00");
        //            else if (ShiftHours == "480")
        //                Set_Shift("23:00", "07:00");
        //            else
        //            {
        //            }
        //        }
        //    }

        //    //if (clbNoOfShift.Items[i].ToString() == "1st Shift")
        //    //{
        //    //    gb1.Visible = true;
        //    //    dtpBeginTime1.Text = "07:00";
        //    //    dtpEndTime1.Text = "15:00";
        //    //}
        //    //if (clbNoOfShift.Items[i].ToString() == "2nd Shift")
        //    //{
        //    //    gb2.Visible = true;
        //    //    dtpBeginTime2.Text = "15:00";
        //    //    dtpEndTime2.Text = "23:00";
        //    //}
        //    //if (clbNoOfShift.Items[i].ToString() == "3rd Shift")
        //    //{
        //    //    gb3.Visible = true;
        //    //    dtpBeginTime3.Text = "23:00";
        //    //    dtpEndTime3.Text = "07:00";
        //    //}
        //}

    }
}
