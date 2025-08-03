using BusinessLayerUtility;
using SPApplication.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace SPApplication.Transaction
{
    public partial class QualityControl : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;

        bool BorderFlag = false; bool CellFlag = false;
        string PDFReport = string.Empty;
        int AFlag = 0; int SrNo = 1;
        bool AlignFlag = false; bool boldflag = false;
        int RowCount = 18;
        bool MH_Value = false;
        string ReportName = string.Empty;
        DateTime BookingDate, BookingTime;

        Excel.Application myExcelApp;
        Excel.Workbooks myExcelWorkbooks;
        Excel.Workbook myExcelWorkbook;

        public QualityControl()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_QUALITY_CONTROL_REGISTER_ENTRY);
            objDL.SetButtonDesign(btn1, "1");
            objDL.SetButtonDesign(btn2, "2");
            objDL.SetButtonDesign(btn3, "3");
            objDL.SetButtonDesign(btn4, "4");
            objDL.SetButtonDesign(btn5, "5");
            objDL.SetButtonDesign(btn6, "6");
            objDL.SetButtonDesign(btn7, "7");
            objDL.SetButtonDesign(btn8, "8");
            objDL.SetButtonDesign(btn9, "9");
            objDL.SetButtonDesign(btn10, "10");
            objDL.SetButtonDesign(btn11, "11");
            objDL.SetButtonDesign(btn12, "12");
            objDL.SetButtonDesign(btn13, "13");
            objDL.SetButtonDesign(btn14, "14");

            lblBottleInformation.BackColor = Color.Cyan;
            lblJarInformation.BackColor = Color.Yellow;
            objDL.SetPlusButtonDesign(btnAddShiftDetails);
            //objDL.SetPlusButtonDesign(btnAddProductMaster);
            //btnViewProduct.BackColor = objDL.GetBackgroundBlank();
            //btnViewProduct.ForeColor = objDL.GetForeColor();

            btnToleranceValue.BackColor = objDL.GetBackgroundColor();
            btnToleranceValue.ForeColor = objDL.GetForeColor();

            btnSwitch.BackColor = objDL.GetBackgroundColor();
            btnSwitch.ForeColor = objDL.GetForeColor();
        }

        private void CellBackColour()
        {
            //DataGridViewColumn dataGridViewColumn = dgvValues.Columns[2];
            //dataGridViewColumn.HeaderCell.Style.BackColor = Color.LavenderBlush;


            //dataGridViewColumn.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;

            //Color.LavenderBlush
            //Color.Honeydew
            //Color.LemonChiffon
            //Color.WhiteSmoke

            //LavenderBlush  Pet Preform
            Fill_Colour(0, Color.LavenderBlush);
            Fill_Colour(1, Color.LavenderBlush);
            Fill_Colour(2, Color.LavenderBlush);
            Fill_Colour(3, Color.LavenderBlush);
            //foreach (DataGridViewRow row in dgvValues.Rows)
            //    if (  Convert.ToInt32(row.Cells[0].c) || Convert.ToInt32(row.Cells[2].Value))
            //    {
            //        row.DefaultCellStyle.BackColor = Color.LavenderBlush;
            //    }

            //Honeydew Neck
            Fill_Colour(4, Color.Honeydew);
            Fill_Colour(5, Color.Honeydew);
            Fill_Colour(6, Color.Honeydew);
            Fill_Colour(7, Color.Honeydew);
            Fill_Colour(8, Color.Honeydew);

            //LemonChiffon Bottle
            Fill_Colour(9, Color.LemonChiffon);
            Fill_Colour(10, Color.LemonChiffon);
            Fill_Colour(11, Color.LemonChiffon);
            Fill_Colour(12, Color.LemonChiffon);
            Fill_Colour(13, Color.LemonChiffon);

            //WhiteSmoke Tests
            Fill_Colour(14, Color.WhiteSmoke);
            Fill_Colour(15, Color.WhiteSmoke);
            Fill_Colour(16, Color.WhiteSmoke);
            Fill_Colour(17, Color.WhiteSmoke);
            Fill_Colour(18, Color.WhiteSmoke);
            Fill_Colour(19, Color.WhiteSmoke);
            Fill_Colour(20, Color.WhiteSmoke);


            //dgvValues.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
            //dgvValues.EnableHeadersVisualStyles = false;

            //foreach (DataGridView row in dgvValues.Columns)
            ////if (Convert.ToInt32(row.Cells[7].Value) < Convert.ToInt32(row.Cells[10].Value))
            ////{
            //{
            //    if(row.Columns[0].Index ==0)
            //    row.RowsDefaultCellStyle.BackColor = Color.Red;
            //}


            //}
            dgvValues.EnableHeadersVisualStyles = false;
        }

        private void Fill_Colour(int CID, Color BC)
        {
            DataGridViewColumn dataGridViewColumn = dgvValues.Columns[CID];
            dataGridViewColumn.HeaderCell.Style.BackColor = BC;
            // dataGridViewColumn.HeaderCell.Style.ForeColor = Color.Yellow;
            //dataGridView1.Rows[0].Cells[0].DefaultCellStyle.BackColor = Color.Beige;
        }

        private void QCTest_Load(object sender, EventArgs e)
        {
            // objRL.Fill_Item_ListBox(lbItem, txtSearchProductName.Text, "All");
            ClearAll();
            //FillGrid();
        }

        private void Fill_DGV_Colum_Supplier()
        {
            //Alpla
            //Chemco
            //Shivraj
            //Sunpet
            //GR
            //CPI
            //DataSet ds = new DataSet();

            //DataGridViewComboBoxColumn dgvCboColumn = new DataGridViewComboBoxColumn();
            //dgvCboColumn.Name = "clmSupplier";
            //objRL.FillPreformParty_DGV(dgvCboColumn);
            ////dgvCboColumn.DataSource = dtContacts; //DataTable that contains contact details
            ////dgvCboColumn.DisplayMember = "Name";
            ////dgvCboColumn.ValueMember = "Id";
            //dgvValues.Columns.Add(dgvCboColumn);


            //DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            //dgvCmb.HeaderText = "Name";
            //dgvCmb.Items.Add("Ghanashyam");
            //dgvCmb.Items.Add("Jignesh");
            //dgvCmb.Items.Add("Ishver");
            //dgvCmb.Items.Add("Anand");
            //dgvCmb.Name = "clmSupplier";
            //dgvValues.Columns.Add(dgvCmb);  
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected void ClearAll()
        {
            Clear_Product_GridValues();


            GridFlag = false;
            btnSave.Enabled = false;
            // txtSearchProductName.Text = "";
            ShiftId = 0;
            objRL.ShiftId = 0;

            TableID = 0;
            objEP.Clear();
            VisibleFalse_ClearAll();

            FlagDelete = false;
            btnDelete.Enabled = false;
            txtShiftDetails.Text = "";
            txtID.Text = "";
            dtpDate.Value = DateTime.Now.Date.Date;
            dtpTime.Value = DateTime.Now;
            cbToday.Checked = true;

            ClearID();
            //foreach (var button in this.Controls.OfType<System.Windows.Forms.Button>())
            foreach (var button in this.gbMachineNo.Controls.OfType<System.Windows.Forms.Button>())
            //this.gbMachineNo.Controls.OfType<System.Windows.Forms.Button>()
            {
                button.BackColor = objDL.GetBackgroundColor();
                button.ForeColor = objDL.GetForeColor();
            }

            Shift = "";
            PlantInchargeId = 0;
            VolumeCheckerId = 0;

            lblProductName.Text = "";
            lblProductName.BackColor = Color.White;

            GetID();
            
            dtpDate.Focus();
        }

        string ShiftDetails = string.Empty;
        int PlantInchargeId = 0, VolumeCheckerId = 0, ShiftEntryId = 0;
        bool GridFlag = false;
        string Shift = string.Empty;
        bool ShiftFlag = false;

        private void ShiftCode()
        {
            TimeSpan StartTimeShift1 = new TimeSpan(07, 0, 0); //10 o'clock
            TimeSpan EndTimeShift1 = new TimeSpan(15, 0, 0); //12 o'clock

            TimeSpan StartTimeShift2 = new TimeSpan(15, 0, 0); //10 o'clock
            TimeSpan EndTimeShift2 = new TimeSpan(23, 0, 0); //12 o'clock

            TimeSpan StartTimeShift3 = new TimeSpan(23, 0, 0); //10 o'clock
            TimeSpan EndTimeShift3 = new TimeSpan(7, 0, 0); //12 o'clock

            TimeSpan StartTimeShift3Other = new TimeSpan(24, 0, 0); //10 o'clock

            DateTime TodayTime;
            TimeSpan now = DateTime.Now.TimeOfDay;
            ShiftFlag = false;

            if ((now > StartTimeShift1) && (now < EndTimeShift1))
                Shift = "I";
            else if ((now > StartTimeShift2) && (now < EndTimeShift2))
                Shift = "II";
            else
            {
                Shift = "III";
                int Checkhours = now.Hours;

                if (Checkhours == 23)
                    ShiftFlag = false;
                else
                    ShiftFlag = true;
            }
            //txtShift.Text = Shift.ToString();
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("QCEntry"));
            txtID.Text = IDNo.ToString();
        }

        int ShiftId = 0;
        private void GetShiftDetails()
        {
            ShiftId = 0;
            Shift = "";
            QCEntryId = 0;
            txtShiftDetails.Text = "";
            Shift= objRL.ShiftCode();
            ShiftId = objRL.ShiftId;

            DataSet ds = new DataSet();

            if (!GridFlag)
                //objBL.Query = "select SE.ID,SE.EntryDate as [Date],SE.EntryTime as [Time],SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],SE.Naration from ((ShiftEntry SE inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) where SE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and SE.EntryDate=#" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and SE.Shift='" + Shift + "'";
                objBL.Query = "select SE.ID,SE.EntryDate as [Date],SE.EntryTime as [Time],SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],SE.Naration from ((ShiftEntry SE inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) where SE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and SE.ShiftId=" + ShiftId + "";
            else
                objBL.Query = "select SE.ID,SE.EntryDate as [Date],SE.EntryTime as [Time],SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],SE.Naration from ((ShiftEntry SE inner join Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) where SE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and SE.ID=" + ShiftEntryId + "";

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ShiftDetails = string.Empty;

                if ((!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"].ToString()))))
                    ShiftEntryId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());

                if ((!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Shift"].ToString()))))
                {
                    ShiftDetails = "Shift- " + ds.Tables[0].Rows[0]["Shift"].ToString() + ", ";
                    Shift = ds.Tables[0].Rows[0]["Shift"].ToString();
                }
                if ((!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Plant Incharge"].ToString()))))
                {
                    ShiftDetails += "Plant Incharge- " + ds.Tables[0].Rows[0]["Plant Incharge"].ToString() + ", ";
                    PlantInchargeId = Convert.ToInt32(ds.Tables[0].Rows[0]["PlantInchargeId"].ToString());
                }
                if ((!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Volume Checker"].ToString()))))
                {
                    ShiftDetails += "Volume Checker- " + ds.Tables[0].Rows[0]["Volume Checker"].ToString();
                    VolumeCheckerId = Convert.ToInt32(ds.Tables[0].Rows[0]["VolumeCheckerId"].ToString());
                }

                if ((!string.IsNullOrEmpty(Convert.ToString(ShiftDetails.ToString()))))
                {
                    txtShiftDetails.Text = ShiftDetails.ToString();
                    gbMachineNo.Enabled = true;
                    objEP.Clear();
                }
                Get_QCEntry();
            }
            else
            {
                objEP.SetError(btnAddShiftDetails, "Please Enter Shift Details Click on + Button");
                btnAddShiftDetails.Focus();
                //gbMachineNo.Enabled = false;
                //ShiftEntry objForm = new ShiftEntry();
                //objForm.Show();
                //GetShiftDetails();
            }
        }

        private void VisibleFalse_ClearAll()
        {
            gbMachineNo.Enabled = false;
            //  lbItem.Enabled = false;
            gbProduct.Enabled = false;
            gbValue.Enabled = false;
            btnToleranceValue.Enabled = false;
            btnSwitch.Enabled = false;
        }


        int ProductId = 0;
        private void ClearAll_Item()
        {
            ProductId = 0;

            lblProductName.Text = "";
        }



        string ProductDetails = string.Empty;
        string Cavity = string.Empty;
        string ProductName = string.Empty;

        private void Clear_Product_GridValues()
        {
            ProductId = 0;
            SwitchFlag = 0;
            dgvRowIndex = 0;
            Result = 0;
            //GridFlag = false;
           // GridFlag = true;
            ProductSwitchFlag = false;
            btnToleranceValue.Enabled = false;
            txtProductionEntryId.Text = "";
            lblProductName.Text = "";
            PreformParty = "";
            lblProductName.Text = "";
            lblProductName.BackColor = Color.White;
            txtProductionEntryId.Text = "";
            dgvValues.Rows.Clear();
            ClearGrid_Values();
        }

        private void Get_Product_Information()
        {
            if (ProductId != 0)
            {
                objRL.Get_Product_Records_By_Id(ProductId);
                ProductName = objRL.ProductName.ToString();
                lblProductName.Text = objRL.ProductName.ToString();

                if (!string.IsNullOrEmpty(objRL.ProductType))
                {
                    if (objRL.ProductType == "Bottle")
                        lblProductName.BackColor = Color.Cyan;
                    else
                        lblProductName.BackColor = Color.Yellow;
                }
                btnToleranceValue.Enabled = true;
            }
        }

        //private void Fill_Product_Information()
        //{
        //   // ProductionEntryId = 0;
        //    Cavity = string.Empty;

        //    //if (!GridFlag)
        //    //    ProductId = Convert.ToInt32(lbItem.SelectedValue);

        //    if (ProductId != 0)
        //    {
        //        ProductDetails = string.Empty;
        //        objRL.Get_Product_Records_By_Id(ProductId);
        //        ProductDetails = string.Empty;

        //       // Get_ProductEntry();

        //        if (ProductionEntryId != 0)
        //        {
        //            ProductDetails = "Mould No-\t" + objRL.SrNoMould + "\t" + "Party-\t" + objRL.Party + "\n" +
        //                             "Cavity-\t\t" + objRL.Cavity + "\t" + "Type-\t" + objRL.AutoSemi + "\n" +
        //                             "Preform Name-\t\t" + objRL.PreformName + "\n" +
        //                             "Nick Name-\t" + objRL.ProductNickName;

        //            lblProductName.Text = objRL.ProductName.ToString();
        //            ProductName = objRL.ProductName.ToString();

        //            if (!string.IsNullOrEmpty(objRL.Cavity))
        //            {
        //                Cavity = objRL.Cavity;

        //                //if (Cavity == "2")
        //                //    gbProductII.Visible = true;
        //                //else
        //                //    gbProductII.Visible = false;
        //            }


        //           // lbItem.Visible = false;
        //            gbValue.Enabled = true;

        //            btnTollaranceValue.Enabled = true;
        //            gbValue.Enabled = true;
        //            btnSave.Enabled = true;

        //            if(!GridFlag)
        //                Fill_dgvValues();
        //            // btnSwitch.Visible = true;
        //        }
        //        else
        //        {
        //            dgvValues.Rows.Clear();
        //            dgvRowIndex = 0;
        //            lblProductName.Text = "";
        //            txtProductionEntryId.Text = "";
        //            btnTollaranceValue.Enabled = false;
        //            btnSwitch.Enabled = false;
        //            gbValue.Enabled = true;
        //            btnSave.Enabled = false;
        //            //lbItem.Enabled = true;
        //            objRL.ShowMessage(35, 4);
        //        }
        //    }
        //}

        private void Fill_dgvValues()
        {
            //if (dgvValues.Rows.Count == 0)
            //{
            //    dgvValues.Rows.Add();


            //    CellBackColour();

            //}
            //SetSRNO();
            ////Fill_DGV_Colum_Supplier();

            if (dgvValues.Rows.Count == 0)
            {
                for (int i = 0; i < 13; i++)
                {
                    dgvValues.Rows.Add();
                    dgvValues.Rows[i].Cells["clmSrNo"].Value = Convert.ToString(i + 1);
                    //dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.LavenderBlush;
                    //dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.LavenderBlush;

                    if (PreformParty != "")
                        dgvValues.Rows[i].Cells["clmSupplier"].Value = PreformParty.ToString();
                }
                CellBackColour();
            }
        }

        private void SetSRNO()
        {
            SrNo = 1;
            for (int i = 0; i < dgvValues.Rows.Count; i++)
            {
                //dgvValues.Rows.Add();
                dgvValues.Rows[i].Cells["clmSrNo"].Value = SrNo.ToString();
                SrNo++;
                //dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.LavenderBlush;
                //dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.LavenderBlush;
            }
        }

        static int MachineNo;
        private void ButtonClick_Event(object sender)
        {
            ClearID();
            //foreach (var button in this.Controls.OfType<System.Windows.Forms.Button>())
            foreach (var button in this.gbMachineNo.Controls.OfType<System.Windows.Forms.Button>())
            //this.gbMachineNo.Controls.OfType<System.Windows.Forms.Button>()
            {
                button.BackColor = objDL.GetBackgroundColor();
                button.ForeColor = objDL.GetForeColor();
            }
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Lime;
            ctrl.ForeColor = Color.Black;
            gbProduct.Visible = true;
            MachineNo = Convert.ToInt32(ctrl.Text);
            btnSwitch.Visible = true;
            Get_QCEntryMachine();
        }

        int QCEntryId = 0, QCEntryMachineId = 0, QCEntryMachineValuesId = 0, Result = 0, ProductionEntryId = 0;
        bool FlagQCEntry = false, FlagQCEntryMachine = false;
        string ProductSwitchNotes = string.Empty;
        string Reason = string.Empty;

        private bool ValidationQCEntry()
        {
            objEP.Clear();
            if (ShiftEntryId == 0)
            {
                objEP.SetError(txtShiftDetails, "Please Enter Shift Details");
                btnAddShiftDetails.Focus();
                return true;
            }
            else
                return false;
        }

        private bool CheckExist_QCEntry()
        {
            DataSet ds = new DataSet();
            if (!GridFlag)
                objBL.Query = "select ID,EntryDate,EntryTime,ShiftEntryId from QCEntry where CancelTag=0 and EntryDate=#" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and ShiftEntryId=" + ShiftEntryId + "";
            else
                objBL.Query = "select ID,EntryDate,EntryTime,ShiftEntryId from QCEntry where CancelTag=0 and ShiftEntryId=" + ShiftEntryId + "";

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void Get_QCEntry()
        {
            Result = 0; QCEntryId = 0;
            //ShiftEntryId
            DataSet ds = new DataSet();
            if (!GridFlag)
                //objBL.Query = "select ID,EntryDate,EntryTime,ShiftEntryId from QCEntry where CancelTag=0 and EntryDate=#" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and ShiftEntryId=" + ShiftEntryId + "";
                objBL.Query = "select ID,EntryDate,EntryTime,ShiftEntryId from QCEntry where CancelTag=0 and ShiftId="+ShiftId+""; // EntryDate=#" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and ShiftEntryId=" + ShiftEntryId + "";
            else
                objBL.Query = "select ID,EntryDate,EntryTime,ShiftEntryId from QCEntry where CancelTag=0 and ShiftEntryId=" + ShiftEntryId + "";

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
                {
                    QCEntryId = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["ID"]));
                    FlagQCEntry = true;
                }
                else
                    FlagQCEntry = false;
            }
            else
                FlagQCEntry = false;
        }

        private void Save_QCEntry()
        {
            if (!FlagQCEntry)
            {
                if (!ValidationQCEntry())
                {
                    objBL.Query = "insert into QCEntry(EntryDate,EntryTime,ShiftEntryId,ShiftId,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "'," + ShiftEntryId + "," + ShiftId + "," + BusinessLayer.UserId_Static + ") ";
                    Result = objBL.Function_ExecuteNonQuery();
                    if (Result > 0)
                        QCEntryId = objRL.ReturnMaxID_Fix("QCEntry","ID");
                    else
                        QCEntryId = 0;
                }
            }
        }

        private bool ValidationQCEntryMachine()
        {
            objEP.Clear();
            if (ShiftEntryId == 0)
            {
                objEP.SetError(txtShiftDetails, "Please Enter Shift Details");
                btnAddShiftDetails.Focus();
                return true;
            }
            //else if (QCEntryId == 0)
            //{
            //    objEP.SetError(txtShiftDetails, "Please Entry ID is not valid");
            //    btnAddShiftDetails.Focus();
            //    return true;
            //}
            else if (ProductionEntryId == 0)
            {
                objEP.SetError(lblProductName, "Please add Production Entry Id");
                lblProductName.Focus();
                return true;
            }
            else if (MachineNo == 0)
            {
                objEP.SetError(btn1, "Please Enter Machine No");
                btn1.Focus();
                return true;
            }
            else if (ProductId == 0)
            {
                objEP.SetError(lblProductName, "Please Enter Machine No");
                lblProductName.Focus();
                return true;
            }
            else if (ProductSwitchFlag)
            {
                if (objRL.Reason == "")
                {
                    objEP.SetError(btnSwitch, "Please Enter Reason of Switch Product");
                    btnSwitch.Focus();
                    return true;
                }
                else if (objRL.ReasonInDetails == "")
                {
                    objEP.SetError(btnSwitch, "Please Enter Reason of Switch Product");
                    btnSwitch.Focus();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        int SwitchFlag = 0;

        private bool CheckExist_QCEntryMachine()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,QCEntryId,ProductionEntryId,MachineNo,ProductId,SwitchFlag,Reason,ReasonInDetails from QCEntryMachine where CancelTag=0 and QCEntryId=" + QCEntryId + " and ProductionEntryId=" + ProductionEntryId + " and MachineNo=" + MachineNo + " and ProductId= " + ProductId + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                QCEntryMachineId = objRL.ReturnMaxID_Fix("QCEntryMachine","ID");
                return true;
            }
            else
                return false;
        }

        private void Save_QCEntryMachine()
        {
            SaveFlag = false;

            if (!FlagQCEntryMachine || ProductSwitchFlag)
            {
                if (!ValidationQCEntryMachine())
                {
                    if (ProductSwitchFlag)
                    {
                        SwitchFlag = 1;
                        Reason = objRL.Reason;
                        ProductSwitchNotes = objRL.ReasonInDetails;
                    }
                    else
                    {
                        SwitchFlag = 0;
                        Reason = "";
                        ProductSwitchNotes = "";
                    }

                    if (!CheckExist_QCEntryMachine())
                    {
                        objBL.Query = "insert into QCEntryMachine(QCEntryId,ProductionEntryId,MachineNo,ProductId,SwitchFlag,Reason,ReasonInDetails,UserId) values(" + QCEntryId + "," + ProductionEntryId + "," + MachineNo + ", " + ProductId + "," + SwitchFlag + ",'" + Reason + "', '" + ProductSwitchNotes + "'," + BusinessLayer.UserId_Static + ") ";
                        Result = objBL.Function_ExecuteNonQuery();
                        if (Result > 0)
                        {
                            QCEntryMachineId = objRL.ReturnMaxID_Fix("QCEntryMachine","ID");
                            SaveFlag = true;
                        }
                        else
                            QCEntryMachineId = 0;
                    }
                }
            }

            if (QCEntryMachineId != 0 && dgvValues.Rows.Count > 0)
            {
                for (int i = 0; i < dgvValues.Rows.Count; i++)
                {
                    ClearGrid_Values();

                    if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmSupplier"].Value)) && !string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmWeight"].Value)))
                    {
                        Supplier_I = dgvValues.Rows[i].Cells["clmSupplier"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmWeight"].Value)))
                            Weight_I = dgvValues.Rows[i].Cells["clmWeight"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmColor"].Value)))
                            Color_I = dgvValues.Rows[i].Cells["clmColor"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmSize"].Value)))
                            Size_I = dgvValues.Rows[i].Cells["clmSize"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmInnerDia"].Value)))
                            InnerDia_I = dgvValues.Rows[i].Cells["clmInnerDia"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmOuterDia"].Value)))
                            OuterDia_I = dgvValues.Rows[i].Cells["clmOuterDia"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmRetainerGap"].Value)))
                            RetainerGap_I = dgvValues.Rows[i].Cells["clmRetainerGap"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmHeight"].Value)))
                            Height_I = dgvValues.Rows[i].Cells["clmHeight"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmOverflowVolume"].Value)))
                            OverflowVolume_I = dgvValues.Rows[i].Cells["clmOverflowVolume"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmMajorAxis"].Value)))
                            MajorAxis_I = dgvValues.Rows[i].Cells["clmMajorAxis"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmMinorAxis"].Value)))
                            MinorAxis_I = dgvValues.Rows[i].Cells["clmMinorAxis"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmBottleHeight"].Value)))
                            BottleHeight_I = dgvValues.Rows[i].Cells["clmBottleHeight"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmVisuals"].Value)))
                            Visuals_I = dgvValues.Rows[i].Cells["clmVisuals"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmGoGauge"].Value)))
                            GoGauge_I = dgvValues.Rows[i].Cells["clmGoGauge"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmCapFitment"].Value)))
                            CaptFitment_I = dgvValues.Rows[i].Cells["clmCapFitment"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmWadSealing"].Value)))
                            WadSealing_I = dgvValues.Rows[i].Cells["clmWadSealing"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmLeakTest"].Value)))
                            LeakTest_I = dgvValues.Rows[i].Cells["clmLeakTest"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmDropTest"].Value)))
                            DropTest_I = dgvValues.Rows[i].Cells["clmDropTest"].Value.ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmTopLoadTest"].Value)))
                            TopLoadTest_I = dgvValues.Rows[i].Cells["clmTopLoadTest"].Value.ToString();

                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmId"].Value)))
                            QCEntryMachineValuesId = Convert.ToInt32(dgvValues.Rows[i].Cells["clmId"].Value);
                        else
                            QCEntryMachineValuesId = 0;

                        if (ProductSwitchFlag)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[i].Cells["clmSwitchProduct"].Value)))
                            {
                                if (Convert.ToString(dgvValues.Rows[i].Cells["clmSwitchProduct"].Value) == "Yes")
                                    SwitchFlag = 1;
                                else
                                    SwitchFlag = 0;
                            }
                            //    SwitchFlag = Convert.ToInt32(dgvValues.Rows[i].Cells["clmSwitchProduct"].Value);
                            //else
                            //    SwitchFlag = 0;
                        }
                        else
                            SwitchFlag = 0;

                        if (QCEntryMachineValuesId == 0)
                            objBL.Query = "insert into QCEntryMachineValues(QCEntryId,QCEntryMachineId,ProductionEntryId,MachineNo,ProductId,Supplier,[Weight],[Color],[Size],InnerDia,OuterDia,RetainerGap,Height,OverflowVolume,MajorAxis,MinorAxis,BottleHeight,Visuals,GoGauge,CaptFitment,WadSealing,LeakTest,DropTest,TopLoadTest,BaseInformation,UserId) values(" + QCEntryId + "," + QCEntryMachineId + "," + ProductionEntryId + "," + MachineNo + "," + ProductId + ",'" + Supplier_I + "','" + Weight_I + "','" + Color_I + "','" + Size_I + "','" + InnerDia_I + "','" + OuterDia_I + "','" + RetainerGap_I + "','" + Height_I + "','" + OverflowVolume_I + "','" + MajorAxis_I + "','" + MinorAxis_I + "','" + BottleHeight_I + "','" + Visuals_I + "','" + GoGauge_I + "','" + CaptFitment_I + "','" + WadSealing_I + "','" + LeakTest_I + "','" + DropTest_I + "','" + TopLoadTest_I + "','" + BaseInformation_I + "'," + BusinessLayer.UserId_Static + ")";
                        else //QCEntryId,QCEntryMachineId,ProductionEntryId,MachineNo,ProductId,
                            objBL.Query = "update QCEntryMachineValues set Supplier='" + Supplier_I + "',[Weight]='" + Weight_I + "',[Color]='" + Color_I + "',[Size]='" + Size_I + "',InnerDia= '" + InnerDia_I + "',OuterDia='" + OuterDia_I + "',RetainerGap='" + RetainerGap_I + "',Height= '" + Height_I + "',OverflowVolume= '" + OverflowVolume_I + "',MajorAxis='" + MajorAxis_I + "',MinorAxis='" + MinorAxis_I + "',BottleHeight= '" + BottleHeight_I + "',Visuals='" + Visuals_I + "',GoGauge= '" + GoGauge_I + "',CaptFitment='" + CaptFitment_I + "',WadSealing= '" + WadSealing_I + "',LeakTest='" + LeakTest_I + "',DropTest='" + DropTest_I + "',TopLoadTest='" + TopLoadTest_I + "',BaseInformation='" + BaseInformation_I + "' where CancelTag=0 and ID=" + QCEntryMachineValuesId + "";

                        Result = objBL.Function_ExecuteNonQuery();

                        if (Result > 0)
                        {
                            SaveFlag = true;
                        }
                        else
                            SaveFlag = false;
                    }
                }
                if (SaveFlag)
                {
                    objRL.ShowMessage(7, 1);
                    FillGrid();
                    Get_QCEntryMachine();
                }

                //if (SaveFlag)
                //{

                //}
                //else
                //{
                //    objRL.ShowMessage(17, 4);
                //    return;
                //}
            }
        }

        bool SaveFlag = false;

        private void Get_QCEntryMachine()
        {
            Clear_Product_GridValues();

            Get_QCEntry();

            if (QCEntryId > 0)
            {
                DataSet ds = new DataSet();
                objBL.Query = "select ID,QCEntryId,ProductionEntryId,MachineNo,ProductId,SwitchFlag,Reason,ReasonInDetails from QCEntryMachine where CancelTag=0 and QCEntryId=" + QCEntryId + " and MachineNo=" + MachineNo + " order by ID desc";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gbValue.Enabled = true;
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
                    {
                        QCEntryMachineId = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["ID"]));
                        FlagQCEntryMachine = true;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ProductionEntryId"])))
                    {
                        ProductionEntryId = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["ProductionEntryId"]));
                        FlagQCEntryMachine = true;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SwitchFlag"])))
                    {
                        SwitchFlag = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["SwitchFlag"]));
                        if (SwitchFlag == 1)
                        {
                            btnSwitch.Enabled = false;
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Reason"])))
                                objRL.Reason = Convert.ToString(ds.Tables[0].Rows[0]["Reason"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ReasonInDetails"])))
                                objRL.ReasonInDetails = Convert.ToString(ds.Tables[0].Rows[0]["ReasonInDetails"]);
                            ProductSwitchFlag = true;
                        }
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"])))
                    {
                        //ProductId = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"]));
                        //GridFlag = true;
                        //Fill_Product_Information();
                        //txtProductionEntryId.Text = ProductionEntryId.ToString();
                        //lblProductName.Text = objRL.ProductName.ToString();
                        //gbProduct.Enabled = false;
                        //btnTollaranceValue.Enabled = true;
                        //FlagQCEntryMachine = true;
                        //btnSwitch.Visible = true;
                        //btnSwitch.Enabled = true;
                        Get_QCEntryMachineValues();
                    }
                    //lbItem.Visible = false;
                }
                else
                {
                    Call_Empty_Records_Product();
                }
            }
            else
                {
                    Call_Empty_Records_Product();
                    //lbItem.Enabled = true;
                }
        }

        private void Call_Empty_Records_Product()
        {
            Get_ProductEntry();
            Get_Product_Information();
            if (ProductId != 0)
                Fill_dgvValues();
            FlagQCEntryMachine = false;
            gbProduct.Enabled = true;
            gbValue.Enabled = true;
        }

        //Change
        string Supplier_I = "", Weight_I = "", Color_I = "", Size_I = "", InnerDia_I = "", OuterDia_I = "", RetainerGap_I = "", Height_I = "", OverflowVolume_I = "", MajorAxis_I = "", MinorAxis_I = "", BottleHeight_I = "", Visuals_I = "", GoGauge_I = "", CaptFitment_I = "", WadSealing_I = "", LeakTest_I = "", DropTest_I = "", TopLoadTest_I = "", BaseInformation_I = "";
        int Weight_R = 0, Size_R = 0, InnerDia_R = 0, OuterDia_R = 0, RetainerGap_R = 0, Height_R = 0, OverflowVolume_R = 0, MajorAxis_R = 0, MinorAxis_R = 0, BottleHeight_R = 0, Visuals_R = 0, GoGauge_R = 0, CaptFitment_R = 0, WadSealing_R = 0, LeakTest_R = 0, DropTest_R = 0, TopLoadTest_R = 0, BaseInformation_R=0;

        private void Get_QCEntryMachineValues()
        {
            //Clear_Product_GridValues();

            DataSet ds = new DataSet();
            objBL.Query = "select ID,QCEntryId,QCEntryMachineId,ProductionEntryId,MachineNo,ProductId,Supplier,[Weight],[Color],[Size],InnerDia,OuterDia,RetainerGap,Height,OverflowVolume,MajorAxis,MinorAxis,BottleHeight,Visuals,GoGauge,CaptFitment,WadSealing,LeakTest,DropTest,TopLoadTest,BaseInformation from QCEntryMachineValues where CancelTag=0 and QCEntryId=" + QCEntryId + " and MachineNo=" + MachineNo + " and QCEntryMachineId=" + QCEntryMachineId + " and ProductionEntryId=" + ProductionEntryId + " order by ID asc";
            // objBL.Query = "select ID,QCEntryId,QCEntryMachineId,ProductionEntryId,MachineNo,ProductId,Supplier,[Weight],[Color],[Size],InnerDia,OuterDia,RetainerGap,Height,OverflowVolume,MajorAxis,MinorAxis,BottleHeight,Visuals,GoGauge,CaptFitment,WadSealing,LeakTest,DropTest,TopLoadTest from QCEntryMachineValues where CancelTag=0 and QCEntryId=" + QCEntryId + " and MachineNo=" + MachineNo + " and QCEntryMachineId=" + QCEntryMachineId + " and ProductionEntryId=" + ProductionEntryId + " order by ID asc";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                int PID = 0;// SwitchFlag = 0;
                Get_ProductEntry();

                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"])))
                {
                    PID = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"]));

                    if (ProductId != PID)
                    {
                        DialogResult dr;
                        dr = objRL.Switch_Product();
                        if (dr == DialogResult.Yes)
                        {
                            ProductSwitchFlag = true;
                            ProductSwitchReason objForm = new ProductSwitchReason();
                            objForm.ShowDialog(this);

                            if (!string.IsNullOrEmpty(Convert.ToString(objRL.Reason)) && !string.IsNullOrEmpty(Convert.ToString(objRL.ReasonInDetails)))
                            {
                                Get_Product_Information();
                                if (ProductId != 0)
                                    Fill_dgvValues();

                                txtProductionEntryId.Text = ProductionEntryId.ToString();
                                ProductSwitchFlag = true;
                                GridFlag = false;
                                SwitchFlag = 1;
                                return;
                            }
                            else
                                ProductSwitchFlag = false;
                        }
                        else
                            ProductSwitchFlag = false;
                    }

                    Fill_dgvValues();

                    if (!ProductSwitchFlag)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"])))
                        {
                            GridFlag = true;
                            ProductId = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"]));
                            Get_Product_Information();
                            ProductionEntryId = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["ProductionEntryId"]));
                            txtProductionEntryId.Text = ProductionEntryId.ToString();
                        }
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ClearGrid_Values();

                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ID"])))
                                QCEntryMachineValuesId = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[i]["ID"]));
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Supplier"])))
                                Supplier_I = Convert.ToString(ds.Tables[0].Rows[i]["Supplier"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Weight"])))
                                Weight_I = Convert.ToString(ds.Tables[0].Rows[i]["Weight"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Color"])))
                                Color_I = Convert.ToString(ds.Tables[0].Rows[i]["Color"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Size"])))
                                Size_I = Convert.ToString(ds.Tables[0].Rows[i]["Size"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["InnerDia"])))
                                InnerDia_I = Convert.ToString(ds.Tables[0].Rows[i]["InnerDia"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["OuterDia"])))
                                OuterDia_I = Convert.ToString(ds.Tables[0].Rows[i]["OuterDia"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["RetainerGap"])))
                                RetainerGap_I = Convert.ToString(ds.Tables[0].Rows[i]["RetainerGap"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Height"])))
                                Height_I = Convert.ToString(ds.Tables[0].Rows[i]["Height"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["OverflowVolume"])))
                                OverflowVolume_I = Convert.ToString(ds.Tables[0].Rows[i]["OverflowVolume"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["MajorAxis"])))
                                MajorAxis_I = Convert.ToString(ds.Tables[0].Rows[i]["MajorAxis"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["MinorAxis"])))
                                MinorAxis_I = Convert.ToString(ds.Tables[0].Rows[i]["MinorAxis"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["BottleHeight"])))
                                BottleHeight_I = Convert.ToString(ds.Tables[0].Rows[i]["BottleHeight"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Visuals"])))
                                Visuals_I = Convert.ToString(ds.Tables[0].Rows[i]["Visuals"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["GoGauge"])))
                                GoGauge_I = Convert.ToString(ds.Tables[0].Rows[i]["GoGauge"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["CaptFitment"])))
                                CaptFitment_I = Convert.ToString(ds.Tables[0].Rows[i]["CaptFitment"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["WadSealing"])))
                                WadSealing_I = Convert.ToString(ds.Tables[0].Rows[i]["WadSealing"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["LeakTest"])))
                                LeakTest_I = Convert.ToString(ds.Tables[0].Rows[i]["LeakTest"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["DropTest"])))
                                DropTest_I = Convert.ToString(ds.Tables[0].Rows[i]["DropTest"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["TopLoadTest"])))
                                TopLoadTest_I = Convert.ToString(ds.Tables[0].Rows[i]["TopLoadTest"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["BaseInformation"])))
                                BaseInformation_I = Convert.ToString(ds.Tables[0].Rows[i]["BaseInformation"]);
                            if (SwitchFlag == 0)
                                dgvValues.Rows[i].Cells["clmSwitchProduct"].Value = "No";
                            else
                                dgvValues.Rows[i].Cells["clmSwitchProduct"].Value = "Yes";

                            //dgvValues.Rows.Add();
                            dgvValues.Rows[i].Cells["clmId"].Value = QCEntryMachineValuesId.ToString();
                            dgvValues.Rows[i].Cells["clmSrNo"].Value = SrNo.ToString();
                            dgvValues.Rows[i].Cells["clmSupplier"].Value = Supplier_I;

                            if (!string.IsNullOrEmpty(Convert.ToString(Weight_I)))
                            {
                                dgvValues.Rows[i].Cells["clmWeight"].Value = Weight_I;
                                CheckTollarance(2, Convert.ToDouble(Weight_I));
                                if (ResultValue)
                                {
                                    dgvValues.Rows[i].Cells["clmWeight"].Style.BackColor = Color.Red;
                                    Weight_R = 1;
                                }
                                //else
                                //    dgvValues.Rows[i].Cells["clmWeight"].Style.BackColor = Color.White;
                            }

                            dgvValues.Rows[i].Cells["clmColor"].Value = Color_I;

                            if (!string.IsNullOrEmpty(Convert.ToString(Size_I)))
                            {
                                dgvValues.Rows[i].Cells["clmSize"].Value = Size_I;
                                CheckTollarance(4, Convert.ToDouble(Size_I));
                                if (ResultValue)
                                {
                                    dgvValues.Rows[i].Cells["clmSize"].Style.BackColor = Color.Red;
                                    Size_R = 1;
                                }
                               
                                //else
                                //    dgvValues.Rows[i].Cells["clmSize"].Style.BackColor = Color.White;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(InnerDia_I)))
                            {
                                dgvValues.Rows[i].Cells["clmInnerDia"].Value = InnerDia_I;
                                CheckTollarance(5, Convert.ToDouble(InnerDia_I));
                                if (ResultValue)
                                {
                                    dgvValues.Rows[i].Cells["clmInnerDia"].Style.BackColor = Color.Red;
                                    InnerDia_R = 1;
                                }
                                //else
                                //    dgvValues.Rows[i].Cells["clmInnerDia"].Style.BackColor = Color.White;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(OuterDia_I)))
                            {
                                dgvValues.Rows[i].Cells["clmOuterDia"].Value = OuterDia_I;
                                CheckTollarance(6, Convert.ToDouble(OuterDia_I));
                                if (ResultValue)
                                {
                                    dgvValues.Rows[i].Cells["clmOuterDia"].Style.BackColor = Color.Red;
                                    OuterDia_R = 1;
                                }
                                //else
                                //    dgvValues.Rows[i].Cells["clmOuterDia"].Style.BackColor = Color.White;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(RetainerGap_I)))
                            {
                                dgvValues.Rows[i].Cells["clmRetainerGap"].Value = RetainerGap_I;
                                CheckTollarance(7, Convert.ToDouble(RetainerGap_I));
                                if (ResultValue)
                                {
                                    dgvValues.Rows[i].Cells["clmRetainerGap"].Style.BackColor = Color.Red;
                                    RetainerGap_R = 1;
                                }
                                //else
                                //    dgvValues.Rows[i].Cells["clmRetainerGap"].Style.BackColor = Color.White;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(Height_I)))
                            {
                                dgvValues.Rows[i].Cells["clmHeight"].Value = Height_I;
                                CheckTollarance(8, Convert.ToDouble(Height_I));
                                if (ResultValue)
                                {
                                    dgvValues.Rows[i].Cells["clmHeight"].Style.BackColor = Color.Red;
                                    Height_R = 1;
                                }
                                //else
                                //    dgvValues.Rows[i].Cells["clmHeight"].Style.BackColor = Color.White;
                            }
                            if (!string.IsNullOrEmpty(Convert.ToString(OverflowVolume_I)))
                            {
                                dgvValues.Rows[i].Cells["clmOverflowVolume"].Value = OverflowVolume_I;
                                CheckTollarance(9, Convert.ToDouble(OverflowVolume_I));
                                if (ResultValue)
                                {
                                    dgvValues.Rows[i].Cells["clmOverflowVolume"].Style.BackColor = Color.Red;
                                    OverflowVolume_R = 1;
                                }
                                //else
                                //    dgvValues.Rows[i].Cells["clmOverflowVolume"].Style.BackColor = Color.White;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(MajorAxis_I)))
                            {
                                dgvValues.Rows[i].Cells["clmMajorAxis"].Value = MajorAxis_I;
                                CheckTollarance(10, Convert.ToDouble(MajorAxis_I));
                                if (ResultValue)
                                {
                                    dgvValues.Rows[i].Cells["clmMajorAxis"].Style.BackColor = Color.Red;
                                    MajorAxis_R = 1;
                                }
                                //else
                                //    dgvValues.Rows[i].Cells["clmMajorAxis"].Style.BackColor = Color.White;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(MinorAxis_I)))
                            {
                                dgvValues.Rows[i].Cells["clmMinorAxis"].Value = MinorAxis_I;
                                CheckTollarance(11, Convert.ToDouble(MinorAxis_I));
                                if (ResultValue)
                                {
                                    dgvValues.Rows[i].Cells["clmMinorAxis"].Style.BackColor = Color.Red;
                                    MinorAxis_R = 1;
                                }
                                //else
                                //    dgvValues.Rows[i].Cells["clmMinorAxis"].Style.BackColor = Color.White;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(BottleHeight_I)))
                            {
                                dgvValues.Rows[i].Cells["clmBottleHeight"].Value = BottleHeight_I;
                                CheckTollarance(12, Convert.ToDouble(BottleHeight_I));
                                if (ResultValue)
                                {
                                    dgvValues.Rows[i].Cells["clmBottleHeight"].Style.BackColor = Color.Red;
                                    BottleHeight_R = 1;
                                }
                                //else
                                //    dgvValues.Rows[i].Cells["clmBottleHeight"].Style.BackColor = Color.White;
                            }

                            dgvValues.Rows[i].Cells["clmVisuals"].Value = Visuals_I;
                            dgvValues.Rows[i].Cells["clmGoGauge"].Value = GoGauge_I;
                            dgvValues.Rows[i].Cells["clmCapFitment"].Value = CaptFitment_I;
                            dgvValues.Rows[i].Cells["clmWadSealing"].Value = WadSealing_I;
                            dgvValues.Rows[i].Cells["clmLeakTest"].Value = LeakTest_I;
                            dgvValues.Rows[i].Cells["clmDropTest"].Value = DropTest_I;
                            dgvValues.Rows[i].Cells["clmTopLoadTest"].Value = TopLoadTest_I;
                            dgvValues.Rows[i].Cells["clmBaseInformation"].Value = BaseInformation_I;

                            Visuals_R= SetOK_DropDown(Visuals_I);
                            GoGauge_R = SetOK_DropDown(GoGauge_I);
                            CaptFitment_R = SetOK_DropDown(CaptFitment_I);
                            WadSealing_R = SetOK_DropDown(WadSealing_I);
                            LeakTest_R = SetOK_DropDown(LeakTest_I);
                            DropTest_R = SetOK_DropDown(DropTest_I);
                            TopLoadTest_R = SetOK_DropDown(TopLoadTest_I);
                            BaseInformation_R = SetOK_DropDown(BaseInformation_I);

                            if (SwitchFlag == 1)
                                dgvValues.Rows[i].Cells["clmSwitchProduct"].Value = "Yes";
                            else
                                dgvValues.Rows[i].Cells["clmSwitchProduct"].Value = "No";

                            //dgvRowIndex++;
                            //SrNo++;
                            SetSRNO();

                            if (QCEntryMachineValuesId != 0)
                            {
                                objBL.Query = "update QCEntryMachineValues set WeightR=" + Weight_R + ",SizeR=" + Size_R + ",InnerDiaR=" + InnerDia_R + ",OuterDiaR=" + OuterDia_R + ",RetainerGapR=" + RetainerGap_R + ",HeightR=" + Height_R + ",OverflowVolumeR=" + OverflowVolume_R + ",MajorAxisR=" + MajorAxis_R + ",MinorAxisR=" + MinorAxis_R + ",BottleHeightR=" + BottleHeight_R + ",VisualsR=" + Visuals_R + ",GoGaugeR=" + GoGauge_R + ",CaptFitmentR=" + CaptFitment_R + ",WadSealingR=" + WadSealing_R + ",LeakTestR=" + LeakTest_R + ",DropTestR=" + DropTest_R + ",TopLoadTestR=" + TopLoadTest_R + ",BaseInformationR=" + BaseInformation_R + " where CancelTag=0 and ID=" + QCEntryMachineValuesId + "";
                                Result = objBL.Function_ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            else
            {
                Fill_dgvValues();
            }
        }

        private int SetOK_DropDown(string Value1)
        {
            int SetOK = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(Value1)))
            {
                if (Value1 != "OK")
                    SetOK = 1;
            }

            return SetOK;
        }

        static int dgvRowIndex;

        private void ClearGrid_Values()
        {
            QCEntryMachineValuesId = 0; Supplier_I = ""; Weight_I = ""; Color_I = ""; Size_I = ""; InnerDia_I = ""; OuterDia_I = ""; RetainerGap_I = ""; Height_I = ""; OverflowVolume_I = ""; MajorAxis_I = ""; MinorAxis_I = ""; BottleHeight_I = ""; Visuals_I = ""; GoGauge_I = ""; CaptFitment_I = ""; WadSealing_I = ""; LeakTest_I = ""; DropTest_I = ""; TopLoadTest_I = "";
        }

        private void ClearID()
        {
            ProductionEntryId = 0;
            MachineNo = 0;
            QCEntryMachineId = 0;
            ProductId = 0;
            ProductSwitchNotes = "";
            Reason = "";
            txtProductionEntryId.Text = "";
            gbProduct.Enabled = true;
            Clear_Product_GridValues();
        }

        static string PreformParty;

        private void Get_ProductEntry()
        {
            //Get_ProductEntry();
            ProductSwitchFlag = false;
            PreformParty = "";
            txtProductionEntryId.Text = "";
            DataSet ds = new DataSet();
            //objBL.Query = "select PE.ID,PE.EntryDate,PE.EntryTime,PE.Shift,PE.MachinNo,PE.ProductId,PE.PurchaseOrderNo,PE.ProductQty,PE.StickerHeader,PE.DateFlag,PE.PreformPartyId,PPM.PreformParty from ProductionEntry PE inner join PreformPartyMaster PPM on PPM.ID=PE.PreformPartyId where PE.CancelTag=0 and PE.MachinNo='" + MachineNo + "' and PE.Shift='" + Shift + "' and PE.EntryDate=#" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# order by PE.ID desc";
            objBL.Query = "select PE.ID,PE.EntryDate,PE.EntryTime,PE.Shift,PE.MachinNo,PE.ProductId,PE.PurchaseOrderNo,PE.ProductQty,PE.StickerHeader,PE.DateFlag,PE.PreformPartyId,PPM.PreformParty from ProductionEntry PE inner join PreformPartyMaster PPM on PPM.ID=PE.PreformPartyId where PE.CancelTag=0 and PE.MachinNo='" + MachineNo + "' and PE.ShiftId=" + ShiftId + "  order by PE.ID desc";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!ProductSwitchFlag)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
                        {
                            ProductionEntryId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                            txtProductionEntryId.Text = ProductionEntryId.ToString();
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ProductId"])))
                            ProductId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductId"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PreformParty"])))
                            PreformParty = ds.Tables[0].Rows[0]["PreformParty"].ToString();
                    }
                }
            }
            else
            {
                objRL.ShowMessage(37, 4);
                return;
            }
        }

        private void ButtonClickMain(object sender, EventArgs e)
        {
            ButtonClick_Event(sender);
        }


        private void btnAddShiftDetails_Click(object sender, EventArgs e)
        {
            ShiftEntry objForm = new ShiftEntry();
            objForm.ShowDialog(this);
            GetShiftDetails();
        }


        private void dgvValues_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[e.RowIndex].Cells["clmNeckID"].Value)))
                if (ProductId != 0)
                {
                    int ColInd = e.ColumnIndex;

                    //0 Sr.No   ||
                    //1 Supplier    ||
                    //2 Weight  || 
                    //3 Colour

                    //4 Size
                    //5 Inner Dia
                    //6 Outer Dia
                    //7 Retainer Gap
                    //8 Height

                    //9 Overflow Volume
                    //10 Major Axis
                    //11 Minor Axis
                    //12 Bottle Height

                    //13 BaseInformation
                    //14 Visuals
                    //15 Go/No Go Guage
                    //16 Cap Fitment
                    //17 Wad Sealing
                    //18 Leak Test
                    //19 Drop Test
                    //20 Top Load Test

                    if (ColInd == 2 || ColInd == 4 || ColInd == 5 || ColInd == 6 || ColInd == 7 || ColInd == 8 || ColInd == 9 || ColInd == 10 || ColInd == 11 || ColInd == 12)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)))
                        {
                            double ColumnValue = 0;
                            ColumnValue = Convert.ToDouble(dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                            CheckTollarance(e.ColumnIndex, ColumnValue);

                            if (ResultValue)
                                dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                            else
                            {
                                // Fill_Colour(4, Color.Honeydew);
                                //Fill_Colour(5, Color.Honeydew);
                                //Fill_Colour(6, Color.Honeydew);
                                //Fill_Colour(7, Color.Honeydew);
                                //Fill_Colour(8, Color.Honeydew);

                                ////LemonChiffon Bottle
                                //Fill_Colour(9, Color.LemonChiffon);
                                //Fill_Colour(10, Color.LemonChiffon);
                                //Fill_Colour(11, Color.LemonChiffon);
                                //Fill_Colour(12, Color.LemonChiffon);

                                if (ColInd == 2)
                                    dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LavenderBlush;
                                else if (ColInd == 4 || ColInd == 5 || ColInd == 6 || ColInd == 7 || ColInd == 8)
                                    dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Honeydew;
                                else if (ColInd == 9 || ColInd == 10 || ColInd == 11 || ColInd == 12 || ColInd == 13)
                                    dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LemonChiffon;
                                else
                                    dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                            }

                            if (NullValueFlag)
                                dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;

                            if (ColInd == 2)
                            {
                                Set_OK_Value(e.RowIndex);
                            }

                            btnSave.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex1)
            {

            }
            finally { GC.Collect(); }
        }

        bool FlagAddRow = false;
        private void Set_OK_Value(int RowIndexDGV)
        {
            dgvValues.Rows[RowIndexDGV].Cells[13].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[14].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[15].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[16].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[17].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[18].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[19].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[20].Value = "Ok";

            if (SwitchFlag == 0)
                dgvValues.Rows[RowIndexDGV].Cells[21].Value = "No";
            else
                dgvValues.Rows[RowIndexDGV].Cells[21].Value = "Yes";
        }
        //double ProductNeckSize = 0, ProductNeckSizeRatio = 0, ProductNeckSizeMinValue = 0, ProductNeckSizeMaxValue = 0, ProductWeight = 0, ProductWeightRatio = 0, ProductWeightMinValue = 0, ProductWeightMaxValue = 0;
        //double ProductNeckID = 0, ProductNeckIDRatio = 0, ProductNeckIDMinValue = 0, ProductNeckIDMaxValue = 0, ProductNeckOD = 0, ProductNeckODRatio = 0, ProductNeckODMinValue = 0, ProductNeckODMaxValue = 0;
        //double ProductNeckCollarGap = 0, ProductNeckCollarGapRatio = 0, ProductNeckCollarGapMinValue = 0, ProductNeckCollarGapMaxValue = 0, ProductNeckHeight = 0, ProductNeckHeightRatio = 0, ProductNeckHeightMinValue = 0, ProductNeckHeightMaxValue = 0;
        //double ProductHeight = 0, ProductHeightRatio = 0, ProductHeightMinValue = 0, ProductHeightMaxValue = 0, ProductVolume = 0, ProductVolumeRatio = 0, ProductVolumeMinValue = 0, ProductVolumeMaxValue = 0;
        //double ProductMajorAxisRatio = 0, ProductMajorAxisMinValue = 0, ProductMajorAxisMaxValue = 0, ProductMinorAxisRatio = 0, ProductMinorAxisMinValue = 0, ProductMinorAxisMaxValue = 0;

        public void CheckTollarance(int ColumnIndex, double ColumnValue)
        {
            //double MinValue, double MaxValue
            switch (ColumnIndex)
            {
                //0 Sr.No   ||
                //1 Supplier    ||
                //2 Weight  || 
                //3 Colour

                //4 Size
                //5 Inner Dia
                //6 Outer Dia
                //7 Retainer Gap
                //8 Height

                //9 Overflow Volume
                //10 Major Axis
                //11 Minor Axis
                //12 Bottle Height

                //13 Visuals
                //14 Go/No Go Guage
                //15 Cap Fitment
                //16 Wad Sealing
                //17 Leak Test
                //18 Drop Test
                //19 Top Load Test

                case 2: //ProductWeight   Datagridviewcolumn- //02 Weight
                    SetRemark(ColumnValue.ToString(), objRL.ProductWeightMinValue, objRL.ProductWeightMaxValue);
                    break;
                case 4: //ProductNeckSize Datagridviewcolumn- //04 Size
                    SetRemark(ColumnValue.ToString(), objRL.ProductNeckSizeMinValue, objRL.ProductNeckSizeMaxValue);
                    break;
                case 5: //ProductNeckID    Datagridviewcolumn- //05 Inner Dia
                    SetRemark(ColumnValue.ToString(), objRL.ProductNeckIDMinValue, objRL.ProductNeckIDMaxValue);
                    break;
                case 6: //ProductNeckOD Datagridviewcolumn- //06 Outer Dia
                    SetRemark(ColumnValue.ToString(), objRL.ProductNeckODMinValue, objRL.ProductNeckODMaxValue);
                    break;
                case 7: //ProductNeckCollarGap Datagridviewcolumn-   //7 Retainer Gap
                    SetRemark(ColumnValue.ToString(), objRL.ProductNeckCollarGapMinValue, objRL.ProductNeckCollarGapMaxValue);
                    break;
                case 8: //ProductNeckHeight Datagridviewcolumn-   //8 Height
                    SetRemark(ColumnValue.ToString(), objRL.ProductNeckHeightMinValue, objRL.ProductNeckHeightMaxValue);
                    break;
                case 9: //ProductVolume Datagridviewcolumn-   //9 Overflow Volume
                    SetRemark(ColumnValue.ToString(), objRL.ProductVolumeMinValue, objRL.ProductVolumeMaxValue);
                    break;
                case 10: //ProductMajorAxis Datagridviewcolumn-   //10 Major Axis
                    SetRemark(ColumnValue.ToString(), objRL.ProductMajorAxisMinValue, objRL.ProductMajorAxisMaxValue);
                    break;
                case 11: //ProductMinorAxis Datagridviewcolumn-   //11 Minor Axis
                    SetRemark(ColumnValue.ToString(), objRL.ProductMinorAxisMinValue, objRL.ProductMinorAxisMaxValue);
                    break;
                case 12: //ProductHeight   Datagridviewcolumn-   //12 Bottle Height
                    SetRemark(ColumnValue.ToString(), objRL.ProductHeightMinValue, objRL.ProductHeightMaxValue);
                    break;
            }
        }

        private void SetRemark(string CheckerValue_F, string MinValue_F, string MaxValue_F)
        {
            NullValueFlag = false; ResultValue = false;
            checkerValue = 0; MinValue = 0; MaxValue = 0;

            double.TryParse(CheckerValue_F, out checkerValue);
            double.TryParse(MinValue_F, out MinValue);
            double.TryParse(MaxValue_F, out MaxValue);

            //0-Correct
            //1-Wrong


            //Remark_F.Text = "";
            //Remark_F.BackColor = objDL.GetBackgroundBlank();

            if (MinValue > 0 && MaxValue > 0)
            {
                if (checkerValue != 0)
                {
                    //if (Enumerable.Range(MinValue, MaxValue).Contains(checkerValue))
                    if (MinValue <= checkerValue && MaxValue >= checkerValue)
                    {
                        ResultValue = false;
                        //Remark_F.BackColor = objDL.GetBackgroundColorSuccess();
                        //Remark_F.Text = "0";
                    }
                    else
                    {
                        ResultValue = true;
                        //Remark_F.BackColor = objDL.GetForeColorError();
                        //Remark_F.Text = "1";
                    }
                }
                else
                {
                    ResultValue = false;
                    NullValueFlag = true;
                }
            }
            else
                NullValueFlag = true;
        }

        bool NullValueFlag = false;

        double checkerValue = 0, MinValue = 0, MaxValue = 0;
        bool FlagProduct = false;

        bool ResultValue = false;


        private void btn3_Click(object sender, EventArgs e)
        {
            ButtonClick_Event(sender);
        }

        private void btnToleranceValue_Click(object sender, EventArgs e)
        {
            if (ProductId != 0)
            {
                Tolerance objForm = new Tolerance(ProductId);
                objForm.ShowDialog(this);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

        }

        private void txtSearchProductName_Click(object sender, EventArgs e)
        {
            // lbItem.Visible = true;
        }

        private void btnViewProduct_Click(object sender, EventArgs e)
        {
            ///lbItem.Visible = true;
        }

        private void txtBatchNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtProductionEntryId);
        }

        private void Clear_ProductSwitchValues()
        {
            dgvValues.Rows.Clear();
            gbProduct.Enabled = true;
            //txtSearchProductName.Focus();
            //  lbItem.Visible = true;
            lblProductName.Text = "";
            txtProductionEntryId.Text = "";
        }

        bool ProductSwitchFlag = false;

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (dgvValues.Rows.Count > 0)
            {
                ProductSwitchFlag = true;
                ProductSwitchReason objForm = new ProductSwitchReason();
                objForm.ShowDialog(this);

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Reason)) && !string.IsNullOrEmpty(Convert.ToString(objRL.ReasonInDetails)))
                {
                    Clear_ProductSwitchValues();
                    ProductSwitchFlag = true;
                    GridFlag = false;
                }
            }
        }

        private void SaveDB()
        {
            if (!ValidationQCEntryMachine())
            {
                if (!CheckExist_QCEntry())
                    Save_QCEntry();

                Save_QCEntryMachine();
                // FillGrid();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDB();
        }

        private void dgvValues_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //FlagAddRow = true;
            //Fill_dgvValues();
            //DataGridViewRow newRow = this.dgvValues.Rows[e.RowIndex];
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
            GetShiftDetails();
        }

        private void txtSearchID_TextChanged(object sender, EventArgs e)
        {
            DateFlag = false;
            SearchTag = false;
            if (txtSearchID.Text != "")
                IDFlag = true;
            else
                IDFlag = false;

            FillGrid();
        }

        private void cbToday_CheckedChanged(object sender, EventArgs e)
        {
            if (cbToday.Checked)
            {
                dtpSearchDate.Enabled = false;
                DateFlag = true;
                FillGrid();
            }
            else
            {
                dtpSearchDate.Enabled = true;
                DateFlag = true;
            }
        }

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;
        bool DateFlag = false;
        bool SearchTag = false;
        bool IDFlag = false;

        private void FillGrid()
        {
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;
            UserClause = string.Empty;

            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (BusinessLayer.UserName_Static != BusinessResources.USER_ADMIN)
                UserClause = " and QCE.UserId = " + BusinessLayer.UserId_Static;
            else
                UserClause = string.Empty;

            MainQuery = "select QCE.ID as [QC No],QCE.EntryDate as [Date],QCE.EntryTime as [Time],QCE.ShiftEntryId,SE.Shift,SE.PlantInchargeId,E.FullName as [Plant Incharge],SE.VolumeCheckerId,E1.FullName as [Volume Checker],SE.Naration,SE.ShiftId from (((QCEntry QCE inner join ShiftEntry SE on SE.ID=QCE.ShiftEntryId) inner join  Employee E on E.ID=SE.PlantInchargeId) inner join Employee E1 on E1.ID=SE.VolumeCheckerId) where QCE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and SE.CancelTag=0 ";
            OrderByClause = " order by QCE.EntryDate desc";

            if (DateFlag)
                WhereClause = " and QCE.EntryDate=#" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# ";
            else if (SearchTag)
                WhereClause = " and E.FullName like '%" + txtSearch.Text + "%'";
            else if (IDFlag)
                WhereClause = " and QCE.ID=" + txtSearchID.Text + "";
            else
                WhereClause = string.Empty;

            objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 ID,
                //1 EntryDate as [Date],
                //2 EntryTime as [Time],
                //3QCE.ShiftEntryId,
                //4 SE.Shift,
                //5 SE.PlantInchargeId,
                //6 E.FullName as [Plant Incharge],
                //7 SE.VolumeCheckerId,
                //8 E1.FullName as [Volume Checker],
                //9 SE.Naration
                //10 SE.ShiftId

                dataGridView1.DataSource = ds.Tables[0];
                //dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[7].Visible = false;

                dataGridView1.Columns[0].Width = 80;
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[6].Width = 300;
                dataGridView1.Columns[8].Width = 300;
                dataGridView1.Columns[9].Width = 200;

                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DateFlag = false;
            IDFlag = false;
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
                    ClearAll();
                    btnDelete.Enabled = true;

                    //0 ID,
                    //1 EntryDate as [Date],
                    //2 EntryTime as [Time],
                    //3QCE.ShiftEntryId,
                    //4 SE.Shift,
                    //5 SE.PlantInchargeId,
                    //6 E.FullName as [Plant Incharge],
                    //7 SE.VolumeCheckerId,
                    //8 E1.FullName as [Volume Checker],
                    //9 SE.Naration
                    //10 SE.ShiftId

                    GridFlag = true;

                    //GetShiftDetails();
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    txtID.Text = TableID.ToString();
                    dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    ShiftEntryId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    ShiftId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[10].Value)));
                    GetShiftDetails();
                    Get_QCEntry();
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

        private void QCTest_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
            //{
            //    SaveDB();
            //}
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                SaveDB();
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dtpSearchDate_ValueChanged(object sender, EventArgs e)
        {
            DateFlag = true;
            FillGrid();
        }


        private void dgvValues_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            int ColInd = dgvValues.CurrentCell.ColumnIndex;
            // if (dataGridView1.CurrentCell.ColumnIndex == 0) //Desired Column
            if (ColInd == 2 || ColInd == 4 || ColInd == 5 || ColInd == 6 || ColInd == 7 || ColInd == 8 || ColInd == 9 || ColInd == 10 || ColInd == 11 || ColInd == 12)
            {
                System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //objRL.FloatValue(sender, e);
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true;
            //}

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }


            if ((e.KeyChar == '.') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1))
            {
                if ((sender as System.Windows.Forms.TextBox).Text != ".")
                {
                    e.Handled = true;
                }
            }
        }
    }
}
