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
    public partial class CapQualityControl : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0,Result=0;

        public CapQualityControl()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_CAPQUALITYCONTROL);
            objRL.Fill_Supplier(cmbSupllier);
            objRL.Fill_Cap_ListBox(lbCap, txtSearchCap.Text, "All");
            objRL.Fill_Employee_By_Designation(cmbQCCheckerName, "Volume Checker");
            btnAddQCSpecs.BackColor = objDL.GetBackgroundColor();
            btnAddQCSpecs.ForeColor = objDL.GetForeColor();
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("CapQualityControl"));
            txtID.Text = IDNo.ToString();
        }

        private void txtSearchCap_TextChanged(object sender, EventArgs e)
        {
            ClearAllCap();
            if (txtSearchCap.Text != "")
            {
                objRL.Fill_Cap_ListBox(lbCap, txtSearchCap.Text, "Text");
                //lbItem.Focus();
            }
            else
            {
                lbCap.Visible = true;
                objRL.Fill_Cap_ListBox(lbCap, txtSearchCap.Text, "All");
            }
        }

        int CapId = 0;
        private void ClearAllCap()
        {
            if (!GridFlag)
                CapId = 0;

            lblCapName.Text = "";
        }

        private void CapQualityControl_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            txtSearchCap.Focus();
        }

        private void lbCap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Fill_Cap_Information();
        }

        string CapDetails = string.Empty;
        string Wad = string.Empty;

        private void Fill_Cap_Information()
        {
            ClearAllCap();

            if (TableID == 0)
                CapId = Convert.ToInt32(lbCap.SelectedValue);

            if (CapId != 0)
            {
                lblCapName.Text = "";
                CapDetails = string.Empty;
                Wad = string.Empty;
                objRL.Get_Cap_Records_By_Id(CapId);

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.CapName)))
                    CapDetails = objRL.CapName;
                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Wad)))
                    Wad = objRL.Wad;

                CapId = Convert.ToInt32(objRL.CapId);
                lblCapName.Text = objRL.CapName.ToString();
                lblCapName.BackColor = Color.Cyan;
                txtInvoiceNumber.Focus();
            }
        }

        private void lbCap_Click(object sender, EventArgs e)
        {
            Fill_Cap_Information();
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

        private void ClearAll()
        {
            objEP.Clear();
            TableID = 0;
            Result = 0;
            FlagDelete = false;
            GridFlag = false;
            ClearAllCap();
            ClearAllCap();
            lblCapName.Text = "";
            txtID.Text = "";
            lblCapName.Text = "";
            txtInvoiceNumber.Text = "";
            cmbSupllier.SelectedIndex = -1;
            cmbQCCheckerName.SelectedIndex = -1;
            GetID();
            CapId = 0;
            dtpDate.Value = DateTime.Now.Date;
            dtpTime.Value = DateTime.Now;
            txtSearchCap.Text = "";
            ClearGrid_Values();
            dgvValues.Rows.Clear();
            txtSearchCap.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
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

        int CapQualityControlId = 0;
        static int dgvRowIndex;

        string Type_I = string.Empty, CustmerLogo = string.Empty, PrintQuality = string.Empty, Material = string.Empty, OuterDia = string.Empty, InnerDiaWithThread = string.Empty, InnerDiaWOThread = string.Empty, CapHeight = string.Empty, InnerDepth = string.Empty, CapWeight =string.Empty, Color_I = string.Empty, VisualAppearance = string.Empty, FlashFinishing = string.Empty, Bend = string.Empty, FitmentWithBottle = string.Empty, Jar = string.Empty, InkTest = string.Empty, DropTest = string.Empty;

        private void ClearGrid_Values()
        {
            CapQualityControlId = 0; Type_I = string.Empty; CustmerLogo = string.Empty; PrintQuality = string.Empty; Material = string.Empty; OuterDia = string.Empty; InnerDiaWithThread = string.Empty; InnerDiaWOThread = string.Empty; CapHeight = string.Empty; InnerDepth = string.Empty; CapWeight = string.Empty; Color_I = string.Empty; VisualAppearance = string.Empty; FlashFinishing = string.Empty; Bend = string.Empty; FitmentWithBottle = string.Empty; Jar = string.Empty; InkTest = string.Empty; DropTest = string.Empty;
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
                CapQualityControlId = 0;

                if (TableID == 0)
                    objBL.Query = "insert into CapQualityControl(EntryDate,EntryTime,CapId,InvoiceNumber,SupplierId,QCCheckerId,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "'," + CapId + ",'" + txtInvoiceNumber.Text + "'," + cmbSupllier.SelectedValue + "," + cmbQCCheckerName.SelectedValue + "," + BusinessLayer.UserId_Static + ") ";
                else
                {
                    if (!FlagDelete)
                        objBL.Query = "Update CapQualityControl set CapId=" + CapId + ",InvoiceNumber='" + txtInvoiceNumber.Text + "',SupplierId=" + cmbSupllier.SelectedValue + ",QCCheckerId=" + cmbQCCheckerName.SelectedValue + ",ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + " ";
                    else
                        objBL.Query = "Delete from CapQualityControl where ID=" + TableID + " ";
                }

                Result = objBL.Function_ExecuteNonQuery();

                if (Result > 0)
                {
                    if (TableID == 0)
                        TableID = objRL.ReturnMaxID_Fix("CapQualityControl", "ID");
                    else
                    {
                        if (FlagDelete)
                        {
                            objBL.Query = "Delete from CapQualityControlValues where ID=" + TableID + " ";
                            Result = objBL.Function_ExecuteNonQuery();
                        }
                    }

                    if (TableID > 0 && dgvValues.Rows.Count > 0 && !FlagDelete)
                    {
                        for (int i = 0; i < dgvValues.Rows.Count; i++)
                        {
                            ClearGrid_Values();

                            if (!string.IsNullOrWhiteSpace(Convert.ToString(dgvValues.Rows[i].Cells["clmType"].Value)) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvValues.Rows[i].Cells["clmOuterDia"].Value)))
                            {
                                Type_I = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmType"].Value));
                                CustmerLogo = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmCustmerLogo"].Value));
                                PrintQuality = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmPrintQuality"].Value));
                                OuterDia = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmOuterDia"].Value));
                                InnerDiaWithThread = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmInnerDiaWithThread"].Value));
                                InnerDiaWOThread = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmInnerDiaWOThread"].Value));
                                CapHeight = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmCapHeight"].Value));
                                InnerDepth = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmInnerDepth"].Value));
                                CapWeight = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmCapWeight"].Value));
                                Color_I = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmColor"].Value));
                                VisualAppearance = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmVisualAppearance"].Value));
                                FlashFinishing = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmFlashFinishing"].Value));
                                Bend = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmBend"].Value));
                                FitmentWithBottle = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmFitmentWithBottleJar"].Value));
                                InkTest = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmInkTest"].Value));
                                DropTest = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmDropTest"].Value));

                                objBL.Query = "insert into CapQualityControlValues(EntryDate,EntryTime,CapId,CapQualityControlId,[Type],CustmerLogo,PrintQuality,OuterDia,InnerDiaWithThread,InnerDiaWOThread,CapHeight,InnerDepth,CapWeight,Color,VisualAppearance,FlashFinishing,Bend,FitmentWithBottle,InkTest,DropTest,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "'," + CapId + "," + TableID + ",'" + Type_I + "','" + CustmerLogo + "','" + PrintQuality + "','" + OuterDia + "','" + InnerDiaWithThread + "','" + InnerDiaWOThread + "','" + CapHeight + "','" + InnerDepth + "','" + CapWeight + "','" + Color_I + "','" + VisualAppearance + "','" + FlashFinishing + "','" + Bend + "','" + FitmentWithBottle + "','" + InkTest + "','" + DropTest + "'," + BusinessLayer.UserId_Static + ")";
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

        private void btnAddQCSpecs_Click(object sender, EventArgs e)
        {
            if (!ValidationMain())
            {
                gbValue.Visible = true;

                //Fill_dgvValues();

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

        private void Fill_dgvValues()
        {
            
            if (dgvValues.Rows.Count == 0)
            {
                for (int i = 0; i < 13; i++)
                {
                    dgvValues.Rows.Add();
                    dgvValues.Rows[i].Cells["clmSrNo"].Value = Convert.ToString(i + 1);
                    
                }
                CellBackColour();
            }
        }

        private void CellBackColour()
        {
             
            //Color.LavenderBlush
            //Color.Honeydew
            //Color.LemonChiffon
            //Color.WhiteSmoke

            //LavenderBlush  Pet Preform
            Fill_Colour(0, Color.LavenderBlush);
            Fill_Colour(1, Color.LavenderBlush);
            Fill_Colour(2, Color.LavenderBlush);
            Fill_Colour(3, Color.LavenderBlush);
            Fill_Colour(4, Color.LavenderBlush);
            

            //LemonChiffon Bottle
            Fill_Colour(5, Color.LemonChiffon);
            Fill_Colour(6, Color.LemonChiffon);
            Fill_Colour(7, Color.LemonChiffon);
            Fill_Colour(8, Color.LemonChiffon);
            Fill_Colour(9, Color.LemonChiffon);
            Fill_Colour(10, Color.LemonChiffon);
            Fill_Colour(11, Color.LemonChiffon);

            //WhiteSmoke Tests
            Fill_Colour(12, Color.WhiteSmoke);
            Fill_Colour(13, Color.WhiteSmoke);
            Fill_Colour(14, Color.WhiteSmoke);
            Fill_Colour(15, Color.WhiteSmoke);
            Fill_Colour(16, Color.WhiteSmoke);
            Fill_Colour(17, Color.WhiteSmoke);
            Fill_Colour(18, Color.WhiteSmoke);
             
            dgvValues.EnableHeadersVisualStyles = false;
        }

        private void Fill_Colour(int CID, Color BC)
        {
            DataGridViewColumn dataGridViewColumn = dgvValues.Columns[CID];
            dataGridViewColumn.HeaderCell.Style.BackColor = BC;
            // dataGridViewColumn.HeaderCell.Style.ForeColor = Color.Yellow;
            //dataGridView1.Rows[0].Cells[0].DefaultCellStyle.BackColor = Color.Beige;
        }
        protected bool ValidationMain()
        {
            objEP.Clear();
            if (lblCapName.Text == "")
            {
                lblCapName.Focus();
                objEP.SetError(lblCapName, "Enter Cap Name");
                return true;
            }
            else if (CapId == 0)
            {
                lblCapName.Focus();
                objEP.SetError(lblCapName, "Enter Cap Name");
                return true;
            }
            else if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text)) //(txtInvoiceNumber.Text == "") 
            {
                txtInvoiceNumber.Focus();
                objEP.SetError(txtInvoiceNumber, "Enter Invoice Number");
                return true;
            }
            else if (cmbSupllier.SelectedIndex == -1)
            {
                cmbSupllier.Focus();
                objEP.SetError(cmbSupllier, "Enter Supllier");
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

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;
        bool DateFlag = false;
        bool SearchTag = false;
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

            MainQuery = "select CQC.ID,EntryDate as [Date],CQC.EntryTime as [Time],CQC.CapId,C.CapName as [Cap Name],CQC.InvoiceNumber as [Invoice Number],CQC.SupplierId,S.SupplierName as [Supplier Name],CQC.QCCheckerId,E.FullName as [QC Checker Name] from (((CapQualityControl CQC inner join CapMaster C on C.ID=CQC.CapId) inner join Supplier S on S.ID=CQC.SupplierId) inner join Employee E on E.ID=CQC.QCCheckerId) where CQC.CancelTag=0 and C.CancelTag=0 and S.CancelTag=0 and E.CancelTag=0";
            OrderByClause = " order by CQC.EntryDate desc";

            if (DateFlag)
                WhereClause = " and CQC.EntryDate between #" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            else if (SearchTag)
                WhereClause = " and C.CapName like '%" + txtSearch.Text + "%'";
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
                //4 C.CapName as [Cap Name],
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

        bool GridFlag = false;

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
                    CapId = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value)));
                    Fill_Cap_Information();
                    txtInvoiceNumber.Text =  objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[5].Value));
                    cmbSupllier.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[7].Value));
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
            objBL.Query = "select * from CapQualityControlValues where CapQualityControlId=" + TableID + " and CancelTag=0";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gbValue.Visible = true;
                dgvValues.Visible = true;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ClearGrid_Values();
                    dgvValues.Rows.Add();
                    dgvValues.Rows[i].Cells["clmType"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Type"]));
                    dgvValues.Rows[i].Cells["clmCustmerLogo"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["CustmerLogo"]));
                    dgvValues.Rows[i].Cells["clmPrintQuality"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["PrintQuality"]));
                   
                    //Tolerances
                    dgvValues.Rows[i].Cells["clmOuterDia"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OuterDia"]));
                    CheckTollarance(6, objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["OuterDia"]))));
                    if (ResultValue)
                        dgvValues.Rows[i].Cells["clmOuterDia"].Style.BackColor = Color.Red; 

                    dgvValues.Rows[i].Cells["clmInnerDiaWithThread"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["InnerDiaWithThread"]));
                    CheckTollarance(7, objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["InnerDiaWithThread"]))));
                    if (ResultValue)
                        dgvValues.Rows[i].Cells["clmInnerDiaWithThread"].Style.BackColor = Color.Red; 

                    dgvValues.Rows[i].Cells["clmInnerDiaWOThread"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["InnerDiaWOThread"]));
                    CheckTollarance(8, objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["InnerDiaWOThread"]))));
                    if (ResultValue)
                        dgvValues.Rows[i].Cells["clmInnerDiaWOThread"].Style.BackColor = Color.Red; 

                    dgvValues.Rows[i].Cells["clmCapHeight"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["CapHeight"]));
                    CheckTollarance(9, objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["CapHeight"]))));
                    if (ResultValue)
                        dgvValues.Rows[i].Cells["clmCapHeight"].Style.BackColor = Color.Red; 

                    dgvValues.Rows[i].Cells["clmInnerDepth"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["InnerDepth"]));
                    CheckTollarance(10, objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["InnerDepth"]))));
                    if (ResultValue)
                        dgvValues.Rows[i].Cells["clmInnerDepth"].Style.BackColor = Color.Red; 

                    dgvValues.Rows[i].Cells["clmCapWeight"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["CapWeight"]));
                    CheckTollarance(11, objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["CapWeight"]))));
                    if (ResultValue)
                        dgvValues.Rows[i].Cells["clmCapWeight"].Style.BackColor = Color.Red; 

                    dgvValues.Rows[i].Cells["clmColor"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Color"]));
                    dgvValues.Rows[i].Cells["clmVisualAppearance"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["VisualAppearance"]));
                    dgvValues.Rows[i].Cells["clmFlashFinishing"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["FlashFinishing"]));
                    dgvValues.Rows[i].Cells["clmBend"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["Bend"]));
                    dgvValues.Rows[i].Cells["clmFitmentWithBottleJar"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["FitmentWithBottle"]));
                    dgvValues.Rows[i].Cells["clmInkTest"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["InkTest"]));
                    dgvValues.Rows[i].Cells["clmDropTest"].Value = objRL.Check_Null_String(Convert.ToString(ds.Tables[0].Rows[i]["DropTest"]));
                }
                Grid_Serial_Number();
            }
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

        private void dgvValues_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[e.RowIndex].Cells["clmNeckID"].Value)))
                if (CapId != 0)
                {
                    int ColInd = e.ColumnIndex;

                    
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

                    if (ColInd == 6 || ColInd == 7 || ColInd == 8 || ColInd == 9 || ColInd == 10 || ColInd == 11 || ColInd == 12 || ColInd == 13 || ColInd == 14 || ColInd == 16 || ColInd == 18)//  || ColInd == 12)//  || ColInd == 10 || ColInd == 11 || ColInd == 12)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)))
                        {
                            double ColumnValue = 0;
                            ColumnValue = objRL.Check_Null_Double(objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)));
                            CheckTollarance(e.ColumnIndex, ColumnValue);

                            if (ResultValue)
                                dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                            else
                            {
                                
                                //if (ColInd == 2)
                                //    dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LavenderBlush;
                                //else if (ColInd == 4 || ColInd == 5 || ColInd == 6 || ColInd == 7 || ColInd == 8)
                                //    dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Honeydew;
                                //else if (ColInd == 9 || ColInd == 10 || ColInd == 11 || ColInd == 12 || ColInd == 13)

                                if (ColInd == 6 || ColInd == 7 || ColInd == 8 || ColInd == 9 || ColInd == 10 || ColInd == 11)
                                    dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LemonChiffon;
                                else
                                    dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                            }

                            if (NullValueFlag)
                                dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;

                            if (ColInd == 12 || ColInd == 13 || ColInd == 14 || ColInd == 16 || ColInd == 18)
                            {
                                Set_OK_Value(e.RowIndex);
                            }
                            
                            btnSave.Enabled = true;
                        }

                        // Check if the edited cell is in the last column
                        //bool isLastColumn = (e.ColumnIndex == dgvValues.ColumnCount - 1);

                        // Check if the edited cell is in the last (non-new) row
                        //bool isLastRow = (e.RowIndex == dgvValues.Rows.Count - 2); // -2 because last row is the "new row"

                        //if (isLastColumn && isLastRow)
                        //{
                        //     Optionally validate row before adding
                        //    if (IsRowComplete(dgvValues.Rows[e.RowIndex]))
                        //    {
                        //         Add new empty row
                        //        dataGridView1.Rows.Add();
                        //    }
                        //}

                        //Grid_Serial_Number();
                    }
                }
            }
            catch (Exception ex1)
            {

            }
            finally { GC.Collect(); }
        }

        private bool IsRowComplete(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        bool FlagAddRow = false;
        private void Set_OK_Value(int RowIndexDGV)
        {
            //ColInd == 12 || ColInd == 13 || ColInd == 14 || ColInd == 16 || ColInd == 18)
            dgvValues.Rows[RowIndexDGV].Cells[12].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[13].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[14].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[16].Value = "Ok";
            dgvValues.Rows[RowIndexDGV].Cells[18].Value = "Ok";
            //dgvValues.Rows[RowIndexDGV].Cells[17].Value = "Ok";
            //dgvValues.Rows[RowIndexDGV].Cells[18].Value = "Ok";
            //dgvValues.Rows[RowIndexDGV].Cells[19].Value = "Ok";
            //dgvValues.Rows[RowIndexDGV].Cells[20].Value = "Ok";

            //if (SwitchFlag == 0)
            //    dgvValues.Rows[RowIndexDGV].Cells[21].Value = "No";
            //else
            //    dgvValues.Rows[RowIndexDGV].Cells[21].Value = "Yes";
        }

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

                //ColInd == 6 || ColInd == 7 || ColInd == 8 || ColInd == 9)

                case 6: //ProductWeight   Datagridviewcolumn- //02 Weight
                    SetRemark(ColumnValue.ToString(), objRL.OuterDiaMinValue, objRL.OuterDiaMaxValue);
                    break;
                case 7: //ProductNeckSize Datagridviewcolumn- //04 Size
                    SetRemark(ColumnValue.ToString(), objRL.InnerDiaWithThreadMinValue, objRL.InnerDiaWithThreadMaxValue);
                    break;
                case 8: //ProductNeckID    Datagridviewcolumn- //05 Inner Dia
                    SetRemark(ColumnValue.ToString(), objRL.InnerDiaWOThreadMinValue, objRL.InnerDiaWOThreadMaxValue);
                    break;
                case 9: //ProductNeckOD Datagridviewcolumn- //06 Outer Dia
                    SetRemark(ColumnValue.ToString(), objRL.CapHeightMinValue, objRL.CapHeightMaxValue);
                    break;
                case 10: //ProductNeckCollarGap Datagridviewcolumn-   //7 Retainer Gap
                    SetRemark(ColumnValue.ToString(), objRL.InnerDepthMinValue, objRL.InnerDepthMaxValue);
                    break;
                case 11: //ProductNeckHeight Datagridviewcolumn-   //8 Height
                    SetRemark(ColumnValue.ToString(), objRL.CapWeightMinValue, objRL.CapWeightMaxValue);
                    break;

                //case 9: //ProductVolume Datagridviewcolumn-   //9 Overflow Volume
                //    SetRemark(ColumnValue.ToString(), objRL.ProductVolumeMinValue, objRL.ProductVolumeMaxValue);
                //    break;
                //case 10: //ProductMajorAxis Datagridviewcolumn-   //10 Major Axis
                //    SetRemark(ColumnValue.ToString(), objRL.ProductMajorAxisMinValue, objRL.ProductMajorAxisMaxValue);
                //    break;
                //case 11: //ProductMinorAxis Datagridviewcolumn-   //11 Minor Axis
                //    SetRemark(ColumnValue.ToString(), objRL.ProductMinorAxisMinValue, objRL.ProductMinorAxisMaxValue);
                //    break;
                //case 12: //ProductHeight   Datagridviewcolumn-   //12 Bottle Height
                //    SetRemark(ColumnValue.ToString(), objRL.ProductHeightMinValue, objRL.ProductHeightMaxValue);
                //    break;
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

        private void dgvValues_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            int ColInd = dgvValues.CurrentCell.ColumnIndex;
            // if (dataGridView1.CurrentCell.ColumnIndex == 0) //Desired Column
            if (ColInd == 6 || ColInd == 7 || ColInd == 8 || ColInd == 9 || ColInd == 10 || ColInd == 11)// || ColInd == 9 || ColInd == 10 || ColInd == 11 || ColInd == 12)
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

        private void dgvValues_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvValues.IsCurrentCellDirty)
            {
                dgvValues.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
