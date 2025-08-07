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
            CapId = 0;
            lblCapName.Text = "";
        }

        private void CapQualityControl_Load(object sender, EventArgs e)
        {

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
            ClearAllCap();
            ClearAllCap();
            lblCapName.Text = "";
            txtID.Text = "";
            GetID();
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

        string Type_I = string.Empty, CustmerLogo = string.Empty, PrintQuality = string.Empty, Material = string.Empty, OuterDia = string.Empty, InnerDia = string.Empty, Height = string.Empty, Weight = string.Empty, Color_I = string.Empty, VisualAppearance = string.Empty, FlashFinishing = string.Empty, Bend = string.Empty, FitmentWithBottle = string.Empty, Jar = string.Empty, InkTest = string.Empty, DropTest = string.Empty;

        private void ClearGrid_Values()
        {
            CapQualityControlId = 0; Type_I = string.Empty; CustmerLogo = string.Empty; PrintQuality = string.Empty; Material = string.Empty; OuterDia = string.Empty; InnerDia = string.Empty; Height = string.Empty; Weight = string.Empty; Color_I = string.Empty; VisualAppearance = string.Empty; FlashFinishing = string.Empty; Bend = string.Empty; FitmentWithBottle = string.Empty; Jar = string.Empty; InkTest = string.Empty; DropTest = string.Empty;
        }

        private void SaveDB()
        {
            if (!ValidationMain())
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
                    {
                        TableID = objRL.ReturnMaxID_Fix("CapQualityControl", "ID");
                    }
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

                            Type_I = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmType"].Value));
                            CustmerLogo = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmCustmerLogo"].Value));
                            PrintQuality = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmPrintQuality"].Value));
                            OuterDia = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmOuterDia"].Value));
                            InnerDia = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmInnerDia"].Value));
                            Height = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmHeight"].Value));
                            Weight = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmWeight"].Value));
                            Color_I = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmColor"].Value));
                            VisualAppearance = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmVisualAppearance"].Value));
                            FlashFinishing = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmFlashFinishing"].Value));
                            Bend = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmBend"].Value));
                            FitmentWithBottle = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmFitmentWithBottleJar"].Value));
                            InkTest = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmInkTest"].Value));
                            DropTest = objRL.Check_Null_String(Convert.ToString(dgvValues.Rows[i].Cells["clmDropTest"].Value));

                            objBL.Query = "insert into CapQualityControlValues(EntryDate,EntryTime,CapId,CapQualityControlId,[Type],CustmerLogo,PrintQuality,OuterDia,InnerDia,Height,Weight,Color,VisualAppearance,FlashFinishing,Bend,FitmentWithBottle,InkTest,DropTest,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "'," + CapId + "," + TableID + ",'" + Type_I + "','" + CustmerLogo + "','" + PrintQuality + "','" + OuterDia + "','" + InnerDia + "','" + Height + "','" + Weight + "','" + Color_I + "','" + VisualAppearance + "','" + FlashFinishing + "','" + Bend + "','" + FitmentWithBottle + "','" + InkTest + "','" + DropTest + "'," + BusinessLayer.UserId_Static + ")";

                            Result = objBL.Function_ExecuteNonQuery();

                            if (Result > 0)
                            {
                                Result++;
                            }
                        }
                    }

                    if(Result>0)
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

                if (dgvValues.Rows.Count == 0)
                    dgvValues.Rows.Add();
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
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
                UserClause = " and UserId = " + BusinessLayer.UserId_Static;
            else
                UserClause = string.Empty;

            MainQuery = "select CQC.ID,EntryDate as [Date],CQC.EntryTime as [Time],CQC.CapId,C.CapName as [Cap Name],CQC.InvoiceNumber as [Invoice Number],CQC.SupplierId,S.SupplierName as [Supplier Name],CQC.QCCheckerId,E.FullName as [QC Checker Name] from (((CapQualityControl CQC inner join CapMaster C on C.ID=CQC.CapId) inner join Supplier S on S.ID=CQC.SupplierId) inner join Employee E on E.ID=CQC.QCCheckerId) where CQC.CancelTag=0 and C.CancelTag=0 and S.CancelTag=0 and E.CancelTag=0";
            OrderByClause = " order by EntryDate desc";

            if (DateFlag)
                WhereClause = " and EntryDate between #" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpSearchDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            else if (SearchTag)
                WhereClause = " and CapName like '%" + txtSearch.Text + "%'";
            else if (IDFlag)
                WhereClause = " and ID=" + txtSearchID.Text + "";
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
                dataGridView1.Columns[4].Width = 200;
                dataGridView1.Columns[5].Width = 120;
                dataGridView1.Columns[7].Width = 200;
                dataGridView1.Columns[9].Width = 200;
                 
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }
    }
}
