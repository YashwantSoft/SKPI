using BusinessLayerUtility;
using BusinessLayerUtility.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPApplication.Transaction
{
    public partial class WadQualityControl : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0, Result = 0;
        bool SearchTag = false;

        public WadQualityControl()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_WADQUALITYCONTROL);
            // objRL.Fill_Supplier(cmbSupllier);
            objRL.Fill_Wad_ListBox(lbWad, txtSearchWad.Text, "All");
            objRL.Fill_Employee_By_Designation(cmbQCCheckerName, "Volume Checker");
            btnAddQCSpecs.BackColor = objDL.GetBackgroundColor();
            btnAddQCSpecs.ForeColor = objDL.GetForeColor();

            btnTollerance.BackColor = objDL.GetBackgroundColor();
            btnTollerance.ForeColor = objDL.GetForeColor();
            //LoadSuppliers();

            txtSearchSupplier.TextChanged += TxtSearch_TextChanged;
            lstResults.Click += LstResults_Click;
            lstResults.KeyDown += LstResults_KeyDown;
            lstResults.Visible = false;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSearchWad_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Wad();
            if (txtSearchWad.Text != "")
            {
                objRL.Fill_Wad_ListBox(lbWad, txtSearchWad.Text, "Text");
                //lbItem.Focus();
            }
            else
            {
                objRL.Fill_Wad_ListBox(lbWad, txtSearchWad.Text, "All");
            }
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("WadQualityControl"));
            txtID.Text = IDNo.ToString();
        }

        int WadId = 0; bool GridFlag = false;
        private void ClearAll_Wad()
        {
            if (!GridFlag)
                WadId = 0;

            lblWadName.Text = "";
        }


        private void WadQualityControl_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            lbWad.Focus();

            //LoadSupplierData();
            //SetupComboBox();

        }

        DataTable supplierTable = new DataTable();

        private void LoadSupplierData()
        {
            using (OleDbConnection conn = new OleDbConnection(objBL.conString))
            {
                string query = "SELECT ID, SupplierName FROM Supplier ";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                adapter.Fill(supplierTable);
            }
        }

        private void SetupComboBox()
        {
            cmbSupllier.DataSource = supplierTable;
            cmbSupllier.DisplayMember = "SupplierName";
            cmbSupllier.ValueMember = "ID";

            // Set AutoComplete
            AutoCompleteStringCollection autoCollection = new AutoCompleteStringCollection();
            foreach (DataRow row in supplierTable.Rows)
            {
                autoCollection.Add(row["SupplierName"].ToString());
            }

            cmbSupllier.AutoCompleteCustomSource = autoCollection;
            cmbSupllier.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbSupllier.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void ClearAll()
        {
            objEP.Clear();
            TableID = 0;
            Result = 0;
            FlagDelete = false;
            GridFlag = false;
            ClearAll_Wad();
            lblWadName.Text = "";
            txtID.Text = "";
            txtInvoiceNumber.Text = "";
            cmbSupllier.SelectedIndex = -1;
            cmbQCCheckerName.SelectedIndex = -1;
            GetID();
            WadId = 0;
            dtpDate.Value = DateTime.Now.Date;
            dtpTime.Value = DateTime.Now;
            txtSearchWad.Text = "";
            ClearGrid_Values();
            dgvValues.Rows.Clear();
            txtSearchWad.Focus();
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
            else
                ClearAll();
        }

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;
        bool DateFlag = false;
        bool IDFlag = false;

        protected void FillGrid()
        {
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;
            UserClause = string.Empty;

            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (BusinessLayer.UserName_Static != BusinessResources.USER_ADMIN)
                UserClause = " and CQC.UserId = " + BusinessLayer.UserId_Static;
            else
                UserClause = string.Empty;

            MainQuery = "select CQC.ID,EntryDate as [Date],CQC.EntryTime as [Time],CQC.WadId,C.WadName as [Wad Name],CQC.InvoiceNumber as [Invoice Number],CQC.SupplierId,S.SupplierName as [Supplier Name],CQC.QCCheckerId,E.FullName as [QC Checker Name] from (((WadQualityControl CQC inner join WadMaster C on C.ID=CQC.WadId) inner join Supplier S on S.ID=CQC.SupplierId) inner join Employee E on E.ID=CQC.QCCheckerId) where CQC.CancelTag=0 and C.CancelTag=0 and S.CancelTag=0 and E.CancelTag=0";
            OrderByClause = " order by CQC.EntryDate desc";

            if (DateFlag)
                WhereClause = " and CQC.EntryDate between #" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            else if (SearchTag)
                WhereClause = " and C.WadName like '%" + txtSearch.Text + "%'";
            else if (IDFlag)
                WhereClause = " and CQC.ID=" + txtSearchID.Text + "";
            else
                WhereClause = string.Empty;

            objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 ID,
                //1 EntryDate as [Date],
                //2 EntryTime as [Time],
                //3 CQC.CapId,
                //4 C.WadName as [Cap Name],
                //5 CQC.InvoiceNumber as [Invoice Number],
                //6 CQC.SupplierId,
                //7 S.SupplierName as [Supplier Name],
                //8 CQC.QCCheckerId,
                //9 E.FullName as [QC Checker Name]

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[8].Visible = false;

                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[4].Width = 350;
                dataGridView1.Columns[5].Width = 120;
                dataGridView1.Columns[7].Width = 350;
                dataGridView1.Columns[9].Width = 200;

                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        private void cmbSupllier_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbSupllier.SelectedIndex > -1)
            {
            }
        }

        private void lbWad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Fill_Wad_Information();
        }

        private void lbWad_Click(object sender, EventArgs e)
        {
            Fill_Wad_Information();
        }

        string WadName = string.Empty;
        private void Fill_Wad_Information()
        {
            ClearAll_Wad();

            if (TableID == 0)
                WadId = Convert.ToInt32(lbWad.SelectedValue);

            if (WadId != 0)
            {
                lblWadName.Text = "";
                objRL.Get_Wad_Records_By_Id(WadId);

                objRL.FillWadDetailsRichTextBox(rtbWadDetails, WadId);

                if (!string.IsNullOrEmpty(objRL.WadDetails_RTB))
                {
                    rtbWadDetails.Visible = true;
                    lbWad.Visible = false;
                    WadId = Convert.ToInt32(objRL.WadId);
                    lblWadName.Text = objRL.WadName.ToString();
                    lblWadName.BackColor = Color.Yellow;
                    cmbQCCheckerName.Focus();
                }
                else
                    lbWad.Visible = true;

                //if (!string.IsNullOrEmpty(Convert.ToString(objRL.WadName)))
                //    WadName = objRL.WadName;


            }
        }

        private void btnAddQCSpecs_Click(object sender, EventArgs e)
        {
            if (!ValidationMain())
            {
                gbValue.Visible = true;

                if (dgvValues.Rows.Count == 0)
                {
                    dgvValues.Rows.Add();
                    Grid_Serial_Number();
                }
                Grid_Serial_Number();
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void cmbQCCheckerName_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        protected bool ValidationMain()
        {
            objEP.Clear();
            if (lblWadName.Text == "")
            {
                lblWadName.Focus();
                objEP.SetError(lblWadName, "Enter Wads Name");
                return true;
            }
            else if (WadId == 0)
            {
                lblWadName.Focus();
                objEP.SetError(lblWadName, "Enter Wads Name");
                return true;
            }
            else if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text)) //(txtInvoiceNumber.Text == "") 
            {
                txtInvoiceNumber.Focus();
                objEP.SetError(txtInvoiceNumber, "Enter Invoice Number");
                return true;
            }
            else if (SupplierId == 0)
            {
                txtSearchSupplier.Focus();
                objEP.SetError(txtSearchSupplier, "Enter Supllier");
                return true;
            }
            else if (cmbQCCheckerName.SelectedIndex == -1)
            {
                cmbQCCheckerName.Focus();
                objEP.SetError(cmbQCCheckerName, "Enter Supllier");
                return true;
            }
            else
                return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
        }

        int WadQualityControlId = 0;
        static int dgvRowIndex;
        int OuterDiaResult = 0, ThicknessResult = 0, WeightResult = 0, AverageWeightResult = 0, VisualAppearanceResult = 0, PrintQualityResult = 0, SideFinishingResult = 0, BendResult = 0, FitmentWithCapResult = 0, InkTestResult = 0, IndSealTestResult = 0;

        string Type_I = string.Empty, CustmerLogo = string.Empty, PrintQuality = string.Empty, BoardThikness = string.Empty, BoardType = string.Empty, FoilThikness = string.Empty, FoilSpecs = string.Empty, SealantThikness = string.Empty, SealentSpecs = string.Empty, OuterDia = string.Empty, Thickness = string.Empty, Weight = string.Empty, AverageWeight = string.Empty, VisualAppearance = string.Empty, SideFinishing = string.Empty, Bend = string.Empty, FitmentWithCap = string.Empty, InkTest = string.Empty, IndSealTest = string.Empty;

        private void ClearGrid_Values()
        {
            WadQualityControlId = 0; Type_I = string.Empty; CustmerLogo = string.Empty; PrintQuality = string.Empty; BoardThikness = string.Empty; BoardType = string.Empty; FoilThikness = string.Empty; FoilSpecs = string.Empty; SealantThikness = string.Empty; SealentSpecs = string.Empty; OuterDia = string.Empty; Thickness = string.Empty; Weight = string.Empty; AverageWeight = string.Empty; VisualAppearance = string.Empty; SideFinishing = string.Empty; Bend = string.Empty; FitmentWithCap = string.Empty; InkTest = string.Empty; IndSealTest = string.Empty;
            OuterDiaResult = 0; ThicknessResult = 0; WeightResult = 0; AverageWeightResult = 0; VisualAppearanceResult = 0; PrintQualityResult = 0; SideFinishingResult = 0; BendResult = 0; FitmentWithCapResult = 0; InkTestResult = 0; IndSealTestResult = 0;
        }


        private bool ValidateDataGridView()
        {
            foreach (DataGridViewRow row in dgvValues.Rows)
            {
                // Skip the new row placeholder
                if (row.IsNewRow) continue;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        MessageBox.Show("Empty cell found. Please fill all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // Optionally highlight the empty cell
                        cell.Style.BackColor = Color.Red;
                        return false;
                    }
                    else
                    {
                        // Reset background color in case of re-validation
                        cell.Style.BackColor = Color.White;
                    }
                }
            }

            return true;
        }

        private bool Validation()
        {
            objEP.Clear();
            if (ValidationMain())
            {
                return true;
            }
            else if (dgvValues.Rows.Count == 0)
            {
                dgvValues.Focus();
                objEP.SetError(dgvValues, "Enter QC Entry");
                return true;
            }
            else if (SupplierId == 0)
            {
                txtSearchSupplier.Focus();
                objEP.SetError(txtSearchSupplier, "Select Supplier");
                return true;
            }
            //else if (!ValidateDataGridView())
            //{
            //    return true;
            //}
            //else if (!ValidateDataGridView())
            //{
            //    return true;
            //}
            else
                return false;
        }

        private void SaveDB()
        {
            if (!Validation())
            {
                //Save CapQualityControl
                Result = 0;
                WadQualityControlId = 0;

                if (TableID == 0)
                    objBL.Query = "insert into WadQualityControl(EntryDate,EntryTime,WadId,InvoiceNumber,SupplierId,QCCheckerId,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "'," + WadId + ",'" + txtInvoiceNumber.Text + "'," + SupplierId + "," + cmbQCCheckerName.SelectedValue + "," + BusinessLayer.UserId_Static + ") ";
                else
                {
                    if (!FlagDelete)
                        objBL.Query = "Update WadQualityControl set WadId=" + WadId + ",InvoiceNumber='" + txtInvoiceNumber.Text + "',SupplierId=" + SupplierId + ",QCCheckerId=" + cmbQCCheckerName.SelectedValue + ",ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + " ";
                    else
                        objBL.Query = "Delete from WadQualityControl where ID=" + TableID + " ";
                }

                Result = objBL.Function_ExecuteNonQuery();

                if (Result > 0)
                {
                    if (TableID == 0)
                        TableID = objRL.ReturnMaxID_Fix("WadQualityControl", "ID");
                    else
                    {
                        objBL.Query = "Delete from WadQualityControlValues where WadQualityControlId=" + TableID + " ";
                        Result = objBL.Function_ExecuteNonQuery();
                    }

                    if (TableID > 0 && dgvValues.Rows.Count > 0 && !FlagDelete)
                    {
                        for (int i = 0; i < dgvValues.Rows.Count; i++)
                        {
                            ClearGrid_Values();
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(dgvValues.Rows[i].Cells["clmOuterDia"].Value)) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvValues.Rows[i].Cells["clmThickness"].Value)))
                            {
                                OuterDia = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmOuterDia"].Value));
                                OuterDiaResult = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmOuterDiaResult"].Value)));
                                Thickness = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmThickness"].Value));
                                ThicknessResult = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmThicknessResult"].Value)));
                                Weight = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmWeight"].Value));
                                WeightResult = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmWeightResult"].Value)));
                                AverageWeight = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmAverageWeight"].Value));
                                AverageWeightResult = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmAverageWeightResult"].Value)));
                                VisualAppearance = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmVisualAppearance"].Value));
                                VisualAppearanceResult = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmVisualAppearanceResult"].Value)));
                                PrintQuality = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmPrintQuality"].Value));
                                PrintQualityResult = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmPrintQualityResult"].Value)));
                                SideFinishing = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmSideFinishing"].Value));
                                SideFinishingResult = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmSideFinishingResult"].Value)));
                                Bend = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmBend"].Value));
                                BendResult = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmBendResult"].Value)));
                                FitmentWithCap = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmFitmentWithCap"].Value));
                                FitmentWithCapResult = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmFitmentWithCapResult"].Value)));
                                InkTest = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmInkTest"].Value));
                                InkTestResult = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmInkTestResult"].Value)));
                                IndSealTest = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmIndSealTest"].Value));
                                IndSealTestResult = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmIndSealTestResult"].Value)));


                                objBL.Query = "insert into WadQualityControlValues(EntryDate,EntryTime,WadId,WadQualityControlId,OuterDia,OuterDiaResult,Thickness,ThicknessResult,Weight,WeightResult,AverageWeight,AverageWeightResult,VisualAppearance,VisualAppearanceResult,PrintQuality,PrintQualityResult,SideFinishing,SideFinishingResult,Bend,BendResult,FitmentWithCap,FitmentWithCapResult,InkTest,InkTestResult,IndSealTest,IndSealTestResult,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "'," + WadId + "," + TableID + ",'" + OuterDia + "'," + OuterDiaResult + ",'" + Thickness + "'," + ThicknessResult + ",'" + Weight + "'," + WeightResult + ",'" + AverageWeight + "'," + AverageWeightResult + ",'" + VisualAppearance + "'," + VisualAppearanceResult + ",'" + PrintQuality + "'," + PrintQualityResult + ",'" + SideFinishing + "'," + SideFinishingResult + ",'" + Bend + "'," + BendResult + ",'" + FitmentWithCap + "'," + FitmentWithCapResult + ",'" + InkTest + "'," + InkTestResult + ",'" + IndSealTest + "'," + IndSealTestResult + "," + BusinessLayer.UserId_Static + ")";
                                Result = objBL.Function_ExecuteNonQuery();

                                if (Result > 0)
                                    Result++;
                            }
                        }
                    }
                    if (Result > 0)
                    {
                        if (FlagDelete)
                            objRL.ShowMessage(9, 1);
                        else
                            objRL.ShowMessage(7, 1);

                        ClearAll();
                        FillGrid();
                    }
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void dgvValues_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[e.RowIndex].Cells["clmNeckID"].Value)))
                if (WadId != 0)
                {
                    int ColInd = e.ColumnIndex;

                    // Get the column name
                    string columnName = dgvValues.Columns[e.ColumnIndex].Name;
                    
                    // Optionally, get the new value of the edited cell
                    //object newValue = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                    // Debug output
                    //Console.WriteLine($"Edited column: {columnName}, New value: {newValue}");

                    //0 Id  ||
                    //1 Sr.No     ||
                    //2 CustmerLogo  || 
                    //3 PrintQuality

                    //4 OuterDia
                    //5 InnerDia
                    //6 Height
                    //7 Weight

                    //8 Color
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
                    int CID = 0;

                    if (ColInd == 2 || ColInd == 4 || ColInd == 6 || ColInd == 8 || ColInd == 9 || ColInd == 10 || ColInd == 11 || ColInd == 12)//  || ColInd == 12)//  || ColInd == 10 || ColInd == 11 || ColInd == 12)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)))
                        {
                            double ColumnValue = 0;
                            ColumnValue = objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)));
                            CheckTollarance(columnName, ColumnValue);

                            if (ResultValue)
                            {
                                dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                                CID = e.ColumnIndex + 1;
                                dgvValues.Rows[e.RowIndex].Cells[CID].Value = 1;
                            }
                            else
                            {
                                dgvValues.Rows[e.RowIndex].Cells[CID].Value = 0;
                                //if (ColInd == 2)
                                //    dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LavenderBlush;
                                //else if (ColInd == 4 || ColInd == 5 || ColInd == 6 || ColInd == 7 || ColInd == 8)
                                //    dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Honeydew;
                                //else if (ColInd == 9 || ColInd == 10 || ColInd == 11 || ColInd == 12 || ColInd == 13)

                                if (ColInd == 2 || ColInd == 3 || ColInd == 4 || ColInd == 5)
                                    dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LemonChiffon;
                                else
                                    dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                            }

                            if (NullValueFlag)
                                dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;

                            if (ColInd == 6 || ColInd == 7 || ColInd == 8 || ColInd == 10 || ColInd == 12)
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

        private void Set_OK_Value(int RowIndexDGV)
        {
            //ColInd == 12 || ColInd == 13 || ColInd == 14 || ColInd == 16 || ColInd == 18)
            dgvValues.Rows[RowIndexDGV].Cells[6].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[7].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[8].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[10].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[12].Value = "Ok";
            //dgvValues.Rows[RowIndexDGV].Cells[17].Value = "Ok";
            //dgvValues.Rows[RowIndexDGV].Cells[18].Value = "Ok";
            //dgvValues.Rows[RowIndexDGV].Cells[19].Value = "Ok";
            //dgvValues.Rows[RowIndexDGV].Cells[20].Value = "Ok";

            //if (SwitchFlag == 0)
            //    dgvValues.Rows[RowIndexDGV].Cells[21].Value = "No";
            //else
            //    dgvValues.Rows[RowIndexDGV].Cells[21].Value = "Yes";
        }

        public void CheckTollarance(string ColumnIndex, double ColumnValue)
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

                //ColInd == 6 || ColInd == 7 || ColInd == 8 || ColInd == 9)

                case "clmOuterDia": //ProductWeight   Datagridviewcolumn- //02 Weight
                    SetRemark(ColumnValue.ToString(), objRL.OuterDiaMinValue, objRL.OuterDiaMaxValue);
                    break;
                case "clmThickness": //ProductNeckSize Datagridviewcolumn- //04 Size
                    SetRemark(ColumnValue.ToString(), objRL.ThicknessMinValue, objRL.ThicknessMaxValue);
                    break;
                case "clmWeight": //ProductNeckID    Datagridviewcolumn- //05 Inner Dia
                    SetRemark(ColumnValue.ToString(), objRL.WeightMinValue, objRL.WeightMaxValue);
                    break;
                case "clmAverageWeight": //ProductNeckOD Datagridviewcolumn- //06 Outer Dia
                    SetRemark(ColumnValue.ToString(), objRL.AverageWeightMinValue, objRL.AverageWeightMaxValue);
                    break;
            }
        }

        bool NullValueFlag = false, ResultValue = false;
        double checkerValue = 0, MinValue = 0, MaxValue = 0;

        private void SetRemark(string CheckerValue_F, string MinValue_F, string MaxValue_F)
        {
            NullValueFlag = false; ResultValue = false;
            checkerValue = 0; MinValue = 0; MaxValue = 0;

            double.TryParse(CheckerValue_F, out checkerValue);
            double.TryParse(MinValue_F, out MinValue);
            double.TryParse(MaxValue_F, out MaxValue);

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

        private void dgvValues_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvValues.IsCurrentCellDirty)
            {
                dgvValues.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvValues_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            int ColInd = dgvValues.CurrentCell.ColumnIndex;
            // if (dataGridView1.CurrentCell.ColumnIndex == 0) //Desired Column
            if (ColInd == 11 || ColInd == 12 || ColInd == 13 || ColInd == 14 || ColInd == 5 || ColInd == 7 || ColInd == 9) // || ColInd == 10 || ColInd == 11 || ColInd == 12)
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
                    //3 CQC.CapId,
                    //4 C.CapName as [Cap Name],
                    //5 CQC.InvoiceNumber as [Invoice Number],
                    //6 CQC.SupplierId,
                    //7 S.SupplierName as [Supplier Name],
                    //8 CQC.QCCheckerId,
                    //9 E.FullName as [QC Checker Name]

                    GridFlag = true;
                    TableID = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value)));
                    txtID.Text = TableID.ToString();
                    dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    WadId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value)));
                    Fill_Wad_Information();
                    txtInvoiceNumber.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[5].Value));
                    SupplierId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[6].Value)));
                    txtSearchSupplier.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[7].Value));
                    cmbQCCheckerName.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[9].Value));
                    Fill_QC_Values_Grid();
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

        private void Fill_QC_Values_Grid()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select * from WadQualityControlValues where WadQualityControlId=" + TableID + " and CancelTag=0";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gbValue.Visible = true;
                dgvValues.Visible = true;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ClearGrid_Values();
                    dgvValues.Rows.Add();

                    //dgvValues.Rows[i].Cells["clmType"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Type"]));
                    //dgvValues.Rows[i].Cells["clmCustmerLogo"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["CustmerLogo"]));
                    //dgvValues.Rows[i].Cells["clmBoardThikness"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["BoardThikness"]));
                    //dgvValues.Rows[i].Cells["clmBoardType"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["BoardType"]));
                    //dgvValues.Rows[i].Cells["clmFoilThikness"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["FoilThikness"]));
                    //dgvValues.Rows[i].Cells["clmFoilSpecs"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["FoilSpecs"]));
                    //dgvValues.Rows[i].Cells["clmSealantThikness"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["SealantThikness"]));
                    //dgvValues.Rows[i].Cells["clmSealentSpecs"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["SealentSpecs"]));

                    dgvValues.Rows[i].Cells["clmOuterDia"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OuterDia"]));
                    dgvValues.Rows[i].Cells["clmOuterDiaResult"].Value = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OuterDiaResult"])));
                    CheckTollarance("clmOuterDia", objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OuterDia"]))));
                    if (ResultValue)
                    {
                        dgvValues.Rows[i].Cells["clmOuterDia"].Style.BackColor = Color.Red;
                        dgvValues.Rows[i].Cells["clmOuterDiaResult"].Value = 1;
                    }
                    
                    dgvValues.Rows[i].Cells["clmThickness"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Thickness"]));
                    dgvValues.Rows[i].Cells["clmThicknessResult"].Value = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["ThicknessResult"])));
                    CheckTollarance("clmThickness", objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Thickness"]))));
                    if (ResultValue)
                    {
                        dgvValues.Rows[i].Cells["clmThickness"].Style.BackColor = Color.Red;
                        dgvValues.Rows[i].Cells["clmThicknessResult"].Value = 1;
                    }

                    dgvValues.Rows[i].Cells["clmWeight"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Weight"]));
                    dgvValues.Rows[i].Cells["clmWeightResult"].Value = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["WeightResult"])));
                    CheckTollarance("clmWeight", objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Weight"]))));
                    if (ResultValue)
                    {
                        dgvValues.Rows[i].Cells["clmWeight"].Style.BackColor = Color.Red;
                        dgvValues.Rows[i].Cells["clmWeightResult"].Value = 1;
                    }

                    dgvValues.Rows[i].Cells["clmAverageWeight"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["AverageWeight"]));
                    dgvValues.Rows[i].Cells["clmAverageWeightResult"].Value = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["AverageWeightResult"])));
                    CheckTollarance("clmAverageWeight", objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["AverageWeight"]))));
                    if (ResultValue)
                    {
                        dgvValues.Rows[i].Cells["clmAverageWeight"].Style.BackColor = Color.Red;
                        dgvValues.Rows[i].Cells["clmAverageWeightResult"].Value = 1;
                    }

                    dgvValues.Rows[i].Cells["clmVisualAppearance"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["VisualAppearance"]));
                    dgvValues.Rows[i].Cells["clmVisualAppearanceResult"].Value = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["VisualAppearanceResult"])));
                    dgvValues.Rows[i].Cells["clmPrintQuality"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["PrintQuality"]));
                    dgvValues.Rows[i].Cells["clmPrintQualityResult"].Value = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["PrintQualityResult"])));
                    dgvValues.Rows[i].Cells["clmSideFinishing"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["SideFinishing"]));
                    dgvValues.Rows[i].Cells["clmSideFinishingResult"].Value = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["SideFinishingResult"])));
                    dgvValues.Rows[i].Cells["clmBend"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Bend"]));
                    dgvValues.Rows[i].Cells["clmBendResult"].Value = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["BendResult"])));
                    dgvValues.Rows[i].Cells["clmFitmentWithCap"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["FitmentWithCap"]));
                    dgvValues.Rows[i].Cells["clmFitmentWithCapResult"].Value = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["FitmentWithCapResult"])));
                    dgvValues.Rows[i].Cells["clmInkTest"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["InkTest"])); ;
                    dgvValues.Rows[i].Cells["clmInkTestResult"].Value = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["InkTestResult"])));
                    dgvValues.Rows[i].Cells["clmIndSealTest"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["IndSealTest"]));
                    dgvValues.Rows[i].Cells["clmIndSealTestResult"].Value = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["IndSealTestResult"])));
                }
                Grid_Serial_Number();
            }
        }

        private void dgvValues_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header row
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Check if edited column is last column (ComboBox)
            bool isLastColumn = (e.ColumnIndex == dgvValues.Columns.Count - 1);

            // Check if edited row is the last editable row
            bool isLastRow = (e.RowIndex == dgvValues.Rows.Count - 1);

            if (isLastColumn && isLastRow)
            {
                dgvValues.Rows.Add();
                Grid_Serial_Number();
                // Optionally check that all fields in this row are filled
                //if (IsRowComplete(dgvValues.Rows[e.RowIndex]))
                //{
                //    dgvValues.Rows.Add(); // Add a new row
                //}
            }
        }

        private void Grid_Serial_Number()
        {
            if (dgvValues.Rows.Count > 0)
            {
                for (int i = 0; i < dgvValues.Rows.Count; i++)
                {
                    //dgvValues.Rows.Add();
                    dgvValues.Rows[i].Cells["clmSrNo"].Value = Convert.ToString(i + 1);

                }
                //CellBackColour();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        //private Dictionary<string, int> supplierDict = new Dictionary<string, int>();
        Dictionary<string, int> supplierDict = new Dictionary<string, int>();

        //private void cmbSupllier_TextChanged(object sender, EventArgs e)
        //{
        //    string typedText = cmbSupllier.Text.ToLower();
        //    var filteredList = suppliers
        //        .Where(item => item.ToLower().Contains(typedText))
        //        .ToArray();

        //    var autoSource = new AutoCompleteStringCollection();
        //    autoSource.AddRange(filteredList);

        //    cmbSupllier.AutoCompleteCustomSource = autoSource;
        //}

        //private void InitializeComboBox()
        //{
        //    cmbSupllier.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    cmbSupllier.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    cmbSupllier.DropDownStyle = ComboBoxStyle.DropDown;

        //    // Set up the AutoCompleteCustomSource initially
        //    var autoSource = new AutoCompleteStringCollection();
        //    autoSource.AddRange(dataSource.ToArray());
        //    cmbSupllier.AutoCompleteCustomSource = autoSource;

        //    // Optional: Bind to dropdown list as well
        //    cmbSupllier.Items.AddRange(dataSource.ToArray());

        //    // Handle text changed event
        //    cmbSupllier.TextChanged += cmbSupllier_TextChanged;
        //}

        //List<SupplierClass> suppliers = new List<SupplierClass>();
        OleDbDataReader reader;

        //// Load data from MySQL
        //private void LoadSuppliersFromDatabase()
        //{
        //    try
        //    {
        //        objBL.Query = "SELECT ID, SupplierName FROM suppliers ORDER BY name";
        //        reader = objBL.ReturnDataReader();

        //        suppliers.Clear();

        //        while (reader.Read())
        //        {
        //            suppliers.Add(new SupplierClass
        //            {
        //                ID = reader.GetInt32("ID"),
        //                SupplierName = reader.GetString("SupplierName")
        //            });
        //        }

        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error loading data: " + ex.Message);
        //    }
        //}

        //private void LoadSuppliers()
        //{
        //    // Path to your Access database

        //    objBL.Connect();
        //    string connectionString = objBL.conString;

        //    string query = "SELECT ID, SupplierName FROM Supplier where CancelTag=0";

        //    AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

        //    using (OleDbConnection conn = new OleDbConnection(connectionString))
        //    {
        //        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        //        {
        //            conn.Open();

        //            using (OleDbDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    int id = reader.GetInt32(0);
        //                    string name = reader.GetString(1);

        //                    // Add to ComboBox
        //                    cmbSupllier.Items.Add(name);

        //                    // Add to autocomplete collection
        //                    autoComplete.Add(name);

        //                    // Save mapping
        //                    if (!supplierDict.ContainsKey(name))
        //                        supplierDict.Add(name, id);
        //                }
        //            }
        //        }
        //    }

        //    cmbSupllier.AutoCompleteCustomSource = autoComplete;
        //}

        private void cmbSupllier_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedName = cmbSupllier.Text;

            if (supplierDict.ContainsKey(selectedName))
            {
                int selectedID = supplierDict[selectedName];
                //MessageBox.Show($"Selected Supplier ID: {selectedID}");
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            objBL.Connect();

            string keyword = txtSearchSupplier.Text.Trim();

            if (keyword.Length == 0)
            {
                lstResults.Visible = false;
                return;
            }

            // Query Access DB using LIKE
            string query = "SELECT ID, SupplierName FROM Supplier WHERE SupplierName LIKE @kw";

            lstResults.Items.Clear();
            supplierDict.Clear();

            //objBL.Query = "SELECT ID, SupplierName FROM Suppliers WHERE SupplierName LIKE @kw ORDER BY SupplierName";
            //reader = objBL.ReturnDataReader();

            using (OleDbConnection conn = new OleDbConnection(objBL.conString))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                conn.Open();
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int ID = reader.GetInt32(0);
                        string SupplierName = reader.GetString(1);

                        lstResults.Items.Add(SupplierName);
                        supplierDict[SupplierName] = ID;
                    }
                }
            }

            lstResults.Visible = lstResults.Items.Count > 0;
           
        }


        int SupplierId = 0; string selectedName = string.Empty, selectedNameGrid = string.Empty;

        private void LstResults_Click(object sender, EventArgs e)
        {
            Get_SupplierId();
        }

        private void LstResults_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Do something when Enter is pressed
                if (lstResults.SelectedItem != null)
                {
                    Get_SupplierId();
                }
            }
        }

        private void Get_SupplierId()
        {
            SupplierId = 0;selectedName = string.Empty;

            if (lstResults.SelectedItem != null)
            {
              
                selectedName = lstResults.SelectedItem.ToString();
                
                txtSearchSupplier.Text = selectedName;
                lstResults.Visible = false;

                //supplierDict.TryGetValue(selectedName,out 

                if (supplierDict.TryGetValue(selectedName, out SupplierId))
                {
                   // MessageBox.Show(SupplierId.ToString());
                    // You can now use supplierId in your app logic
                }
            }
        }

        private void txtSearchSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstResults.Items.Count > 0)
                {
                    if (lstResults.SelectedIndex < 0)
                    {
                        // No selection yet – select the first item
                        lstResults.SelectedIndex = 0;
                    }
                    else if (lstResults.SelectedIndex < lstResults.Items.Count - 1)
                    {
                        // Move to next item
                        lstResults.SelectedIndex++;
                    }

                    lstResults.Focus(); // Move focus so Up/Down works in the list
                    e.Handled = true;
                }
            }
        }

        private void txtSearchWad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lbWad.Items.Count > 0)
                {
                    if (lbWad.SelectedIndex < 0)
                    {
                        // No selection yet – select the first item
                        lbWad.SelectedIndex = 0;
                    }
                    else if (lbWad.SelectedIndex < lbWad.Items.Count - 1)
                    {
                        // Move to next item
                        lbWad.SelectedIndex++;
                    }
                    lbWad.Focus(); // Move focus so Up/Down works in the list
                    e.Handled = true;
                }
            }
        }

        private void txtSearchSupplier_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTollerance_Click(object sender, EventArgs e)
        {
            if (WadId != 0)
            {
                WadTolerance objForm = new WadTolerance(WadId);
                objForm.ShowDialog(this);
            }
        }
    }
}
