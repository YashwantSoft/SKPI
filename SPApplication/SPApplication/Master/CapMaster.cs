using BusinessLayerUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SPApplication.Master
{
    public partial class CapMaster : Form
    {
        BusinessLayer objBL = new BusinessLayer();      //Own Developed Class
        ErrorProvider objEP = new ErrorProvider();      //System Class
        RedundancyLogics objRL = new RedundancyLogics();  //Own Developed Class
        DesignLayer objDL = new DesignLayer(); //Own Developed Class

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false;

        double RequiredValue = 0, DifferanceRatio = 0, MinValue = 0, MaxValue = 0;

        public CapMaster()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_CAPMASTER);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            //string ValueNew = Decrypt("qEWwKoRZw1tw2CxkJm6dOZ9YUrWDtASKeyX04vmEn2M=");
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "BJS1234";// ConfigurationManager.AppSettings["KEY"].ToString();
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
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
            else
                ClearAll();
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

                    //0 "ID," +
                    //1 "CapName as [Cap Name]," +
                    //2 "Wad,
                    //3 "NeckSizeStandard as [Neck Size Standard]," +
                    //4 "NeckSizeTolerance as [Neck Size Tolerance]," +
                    //5 "NeckSizeMinValue as [Neck Size Min Value]," +
                    //6 "NeckSizeMaxValue as [Neck Size Max Value]," +
                    //7 "CapWeightStandard as [Cap Weight Standard]," +
                    //8 "CapWeightTolerance as [Cap Weight Tolerance]," +
                    //9 "CapWeightMinValue as [Cap Weight Min Value]," +
                    //10 "CapWeightMaxValue as [Cap Weight Max Value]," +
                    //12 "InnerDiaWOThreadStandard as [Inner Dia WO Thread Standard]," +
                    //12 "InnerDiaWOThreadTolerance as [Inner Dia WO Thread Tolerance]," +
                    //13 "InnerDiaWOThreadMinValue as [Inner Dia WO Thread Min Value]," +
                    //14 "InnerDiaWOThreadMaxValue as [Inner Dia WO Thread Max Value]," +
                    //15 "InnerDiaWithThreadStandard as [Inner Dia With Thread Standard]," +
                    //16 "InnerDiaWithThreadTolerance as [Inner Dia With Thread Tolerance]," +
                    //17 "InnerDiaWithThreadMinValue as [Inner Dia With Thread Min Value]," +
                    //18 "InnerDiaWithThreadMaxValue as [Inner Dia With Thread Max Value]," +
                    //19 "OuterDiaStandard as [Outer Dia Standard]," +
                    //20 "OuterDiaTolerance as [Outer Dia Tolerance]," +
                    //21 "OuterDiaMinValue as [Outer Dia Min Value]," +
                    //22 "OuterDiaMaxValue as [Outer Dia Max Value]," +
                    //23 "Status," +
                    //24 "Remarks " + 

                    TableID = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value)));
                    txtCapName.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value));
                    cmbIsWad.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[2].Value));

                    txtNeckSizeStandard.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value));
                    txtNeckSizeTolerance.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[4].Value));
                    txtNeckSizeMinValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[5].Value));
                    txtNeckSizeMaxValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[6].Value));
                    txtCapWeightStandard.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[7].Value));
                    txtCapWeightTolerance.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[8].Value));
                    txtCapWeightMinValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[9].Value));
                    txtCapWeightMaxValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[10].Value));
                    txtInnerDiaWOThreadStandard.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[11].Value));
                    txtInnerDiaWOThreadTolerance.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[12].Value));
                    txtInnerDiaWOThreadMinValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[13].Value));
                    txtInnerDiaWOThreadMaxValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[14].Value));
                    txtInnerDiaWithThreadStandard.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[15].Value));
                    txtInnerDiaWithThreadTolerance.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[16].Value));
                    txtInnerDiaWithThreadMinValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[17].Value));
                    txtInnerDiaWithThreadMaxValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[18].Value));
                    txtOuterDiaStandard.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[19].Value));
                    txtOuterDiaTolerance.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[20].Value));
                    txtOuterDiaMinValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[21].Value));
                    txtOuterDiaMaxValue.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[22].Value));
                    cmbStatus.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[23].Value));
                    txtRemarks.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[24].Value));
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

        private void CapMaster_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            txtCapName.Focus();
        }

        private void ClearAll()
        {
            objEP.Clear();
            btnDelete.Enabled = false;
            Result = 0;
            FlagDelete = false;
            objEP.Clear();
            TableID = 0;
            txtCapName.Text = "";
            cmbIsWad.SelectedIndex = -1;

            txtNeckSizeStandard.Text = "";
            txtNeckSizeTolerance.Text = "";
            txtNeckSizeMinValue.Text = "";
            txtNeckSizeMaxValue.Text = "";
            txtCapWeightStandard.Text = "";
            txtCapWeightTolerance.Text = "";
            txtCapWeightMinValue.Text = "";
            txtCapWeightMaxValue.Text = "";
            txtInnerDiaWOThreadStandard.Text = "";
            txtInnerDiaWOThreadTolerance.Text = "";
            txtInnerDiaWOThreadMinValue.Text = "";
            txtInnerDiaWOThreadMaxValue.Text = "";
            txtInnerDiaWithThreadStandard.Text = "";
            txtInnerDiaWithThreadTolerance.Text = "";
            txtInnerDiaWithThreadMinValue.Text = "";
            txtInnerDiaWithThreadMaxValue.Text = "";
            txtOuterDiaStandard.Text = "";
            txtOuterDiaTolerance.Text = "";
            txtOuterDiaMinValue.Text = "";
            txtOuterDiaMaxValue.Text = "";
            cmbStatus.SelectedIndex = -1;
            txtRemarks.Text = "";
            txtSearch.Text = "";
            txtCapName.Focus();
        }

        int Result = 0;
        string AposValue=string.Empty;

        protected void SaveDB()
        {
            Result = 0;AposValue=string.Empty;

            if (!Validation())
            {
                if (!CheckExist())
                {
                   AposValue=txtCapName.Text; 
                    //'" + ItemName.Replace("'", "''") + "',

                    if (TableID != 0)
                        if (FlagDelete)
                            objBL.Query = "update CapMaster set CancelTag=1 where ID=" + TableID + "";
                        else
                            objBL.Query = "update CapMaster set CapName='" + AposValue.Replace("'", "''") + "',Wad='" + cmbIsWad.Text + "',NeckSizeStandard='" + txtNeckSizeStandard.Text + "',NeckSizeTolerance='" + txtNeckSizeTolerance.Text + "',NeckSizeMinValue='" + txtNeckSizeMinValue.Text + "',NeckSizeMaxValue='" + txtNeckSizeMaxValue.Text + "',CapWeightStandard='" + txtCapWeightStandard.Text + "',CapWeightTolerance='" + txtCapWeightTolerance.Text + "',CapWeightMinValue='" + txtCapWeightMinValue.Text + "',CapWeightMaxValue='" + txtCapWeightMaxValue.Text + "',InnerDiaWOThreadStandard='" + txtInnerDiaWOThreadStandard.Text + "',InnerDiaWOThreadTolerance='" + txtInnerDiaWOThreadTolerance.Text + "',InnerDiaWOThreadMinValue='" + txtInnerDiaWOThreadMinValue.Text + "',InnerDiaWOThreadMaxValue='" + txtInnerDiaWOThreadMaxValue.Text + "',InnerDiaWithThreadStandard='" + txtInnerDiaWithThreadStandard.Text + "',InnerDiaWithThreadTolerance='" + txtInnerDiaWithThreadTolerance.Text + "',InnerDiaWithThreadMinValue='" + txtInnerDiaWithThreadMinValue.Text + "',InnerDiaWithThreadMaxValue='" + txtInnerDiaWithThreadMaxValue.Text + "',OuterDiaStandard='" + txtOuterDiaStandard.Text + "',OuterDiaTolerance='" + txtOuterDiaTolerance.Text + "',OuterDiaMinValue='" + txtOuterDiaMinValue.Text + "',OuterDiaMaxValue='" + txtOuterDiaMaxValue.Text + "',Status='" + cmbStatus.Text + "',Remarks='" + txtRemarks.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                    else
                        objBL.Query = "insert into CapMaster(CapName,Wad,NeckSizeStandard,NeckSizeTolerance,NeckSizeMinValue,NeckSizeMaxValue,CapWeightStandard,CapWeightTolerance,CapWeightMinValue,CapWeightMaxValue,InnerDiaWOThreadStandard,InnerDiaWOThreadTolerance,InnerDiaWOThreadMinValue,InnerDiaWOThreadMaxValue,InnerDiaWithThreadStandard,InnerDiaWithThreadTolerance,InnerDiaWithThreadMinValue,InnerDiaWithThreadMaxValue,OuterDiaStandard,OuterDiaTolerance,OuterDiaMinValue,OuterDiaMaxValue,Status,Remarks,UserId) values('" + AposValue.Replace("'", "''") + "','" + cmbIsWad.Text + "','" + txtNeckSizeStandard.Text + "','" + txtNeckSizeTolerance.Text + "','" + txtNeckSizeMinValue.Text + "','" + txtNeckSizeMaxValue.Text + "','" + txtCapWeightStandard.Text + "','" + txtCapWeightTolerance.Text + "','" + txtCapWeightMinValue.Text + "','" + txtCapWeightMaxValue.Text + "','" + txtInnerDiaWOThreadStandard.Text + "','" + txtInnerDiaWOThreadTolerance.Text + "','" + txtInnerDiaWOThreadMinValue.Text + "','" + txtInnerDiaWOThreadMaxValue.Text + "','" + txtInnerDiaWithThreadStandard.Text + "','" + txtInnerDiaWithThreadTolerance.Text + "','" + txtInnerDiaWithThreadMinValue.Text + "','" + txtInnerDiaWithThreadMaxValue.Text + "','" + txtOuterDiaStandard.Text + "','" + txtOuterDiaTolerance.Text + "','" + txtOuterDiaMinValue.Text + "','" + txtOuterDiaMaxValue.Text + "','" + cmbStatus.Text + "','" + txtRemarks.Text + "'," + BusinessLayer.UserId_Static + ")";

                    Result = objBL.Function_ExecuteNonQuery();
                    if (Result > 0)
                    {
                        if(FlagDelete)
                            objRL.ShowMessage(9, 1);
                        else
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

        protected bool Validation()
        {
            objEP.Clear();
            if (txtCapName.Text == "")
            {
                txtCapName.Focus();
                objEP.SetError(txtCapName, "Enter Cap Name");
                return true;
            }
            else if (cmbIsWad.SelectedIndex == -1)
            {
                cmbIsWad.Focus();
                objEP.SetError(cmbIsWad, "Select Is Wad");
                return true;
            }
            else
                return false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

        string MainQuery = string.Empty, WhereClause = string.Empty, OrderByClause = string.Empty;

        protected void FillGrid()
        {
            MainQuery = string.Empty; WhereClause = string.Empty; OrderByClause = string.Empty;

            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            MainQuery = "select " +
                          "ID,"+
                          "CapName as [Cap Name],"+
                          "Wad,NeckSizeStandard as [Neck Size Standard]," +
                          "NeckSizeTolerance as [Neck Size Tolerance]," +
                          "NeckSizeMinValue as [Neck Size Min Value]," +
                          "NeckSizeMaxValue as [Neck Size Max Value]," +
                          "CapWeightStandard as [Cap Weight Standard]," +
                          "CapWeightTolerance as [Cap Weight Tolerance]," +
                          "CapWeightMinValue as [Cap Weight Min Value]," +
                          "CapWeightMaxValue as [Cap Weight Max Value]," +
                          "InnerDiaWOThreadStandard as [Inner Dia WO Thread Standard]," +
                          "InnerDiaWOThreadTolerance as [Inner Dia WO Thread Tolerance]," +
                          "InnerDiaWOThreadMinValue as [Inner Dia WO Thread Min Value]," +
                          "InnerDiaWOThreadMaxValue as [Inner Dia WO Thread Max Value]," +
                          "InnerDiaWithThreadStandard as [Inner Dia With Thread Standard]," +
                          "InnerDiaWithThreadTolerance as [Inner Dia With Thread Tolerance]," +
                          "InnerDiaWithThreadMinValue as [Inner Dia With Thread Min Value]," +
                          "InnerDiaWithThreadMaxValue as [Inner Dia With Thread Max Value]," +
                          "OuterDiaStandard as [Outer Dia Standard]," +
                          "OuterDiaTolerance as [Outer Dia Tolerance]," +
                          "OuterDiaMinValue as [Outer Dia Min Value]," +
                          "OuterDiaMaxValue as [Outer Dia Max Value]," +
                          "Status," +
                          "Remarks " + 
                          " from CapMaster where CancelTag=0";


            if (SearchTag)
                if (!string.IsNullOrEmpty(Convert.ToString(txtSearch.Text)))
                    WhereClause = " and CapName like '%" + txtSearch.Text + "%'";

            OrderByClause = " order by CapName asc";

            objBL.Query = MainQuery + WhereClause + OrderByClause;

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 "ID," +
                //1 "CapName as [Cap Name]," +
                //2 "Wad,NeckSizeStandard as [Neck Size Standard]," +
                //3 "NeckSizeTolerance as [Neck Size Tolerance]," +
                //4 "NeckSizeMinValue as [Neck Size Min Value]," +
                //5 "NeckSizeMaxValue as [Neck Size Max Value]," +
                //6 "CapWeightStandard as [Cap Weight Standard]," +
                //7 "CapWeightTolerance as [Cap Weight Tolerance]," +
                //8 "CapWeightMinValue as [Cap Weight Min Value]," +
                //9 "CapWeightMaxValue as [Cap Weight Max Value]," +
                //10 "InnerDiaWOThreadStandard as [Inner Dia WO Thread Standard]," +
                //11 "InnerDiaWOThreadTolerance as [Inner Dia WO Thread Tolerance]," +
                //12 "InnerDiaWOThreadMinValue as [Inner Dia WO Thread Min Value]," +
                //13 "InnerDiaWOThreadMaxValue as [Inner Dia WO Thread Max Value]," +
                //14 "InnerDiaWithThreadStandard as [Inner Dia With Thread Standard]," +
                //15 "InnerDiaWithThreadTolerance as [Inner Dia With Thread Tolerance]," +
                //16 "InnerDiaWithThreadMinValue as [Inner Dia With Thread Min Value]," +
                //17 "InnerDiaWithThreadMaxValue as [Inner Dia With Thread Max Value]," +
                //18 "OuterDiaStandard as [Outer Dia Standard]," +
                //19 "OuterDiaTolerance as [Outer Dia Tolerance]," +
                //20 "OuterDiaMinValue as [Outer Dia Min Value]," +
                //21 "OuterDiaMaxValue as [Outer Dia Max Value]," +
                //22 "Status," +
                //23 "Remarks " + 

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 400;
                dataGridView1.Columns[2].Width = 60;

                for (int i = 3; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = 100;
                }
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from CapMaster where CancelTag=0 and CapName='" + txtCapName.Text + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void txtNeckSizeStandard_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNeckSizeStandard);
        }

        private void txtNeckSizeTolerance_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtNeckSizeTolerance);
        }

        private void txtCapWeightStandard_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtCapWeightStandard);
        }

        private void txtCapWeightTolerance_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtCapWeightTolerance);
        }

        private void txtInnerDiaWOThreadStandard_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtInnerDiaWOThreadStandard);
        }

        private void txtInnerDiaWOThreadTolerance_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtInnerDiaWOThreadTolerance);
        }

        private void txtInnerDiaWithThreadStandard_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtInnerDiaWithThreadStandard);
        }

        private void txtInnerDiaWithThreadTolerance_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtInnerDiaWithThreadTolerance);
        }

        private void txtOuterDiaStandard_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtOuterDiaStandard);
        }

        private void txtOuterDiaTolerance_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtOuterDiaTolerance);
        }

        private void txtNeckSizeStandard_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(1);
        }

        private void txtNeckSizeTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(1);
        }

        private void txtCapWeightStandard_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(2);
        }

        private void txtCapWeightTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(2);
        }

        private void txtInnerDiaWOThreadStandard_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(3);
        }

        private void txtInnerDiaWOThreadTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(3);
        }

        private void txtInnerDiaWithThreadStandard_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(4);
        }

        private void txtInnerDiaWithThreadTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(4);
        }

        private void txtOuterDiaStandard_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(5);
        }

        private void txtOuterDiaTolerance_TextChanged(object sender, EventArgs e)
        {
            CalculateValue(5);
        }

        public void CalculateValue(int CheckValue)
        {
            switch (CheckValue)
            {
                case 1:
                    SetValueMinMax(txtNeckSizeStandard, txtNeckSizeTolerance, txtNeckSizeMinValue, txtNeckSizeMaxValue);
                    break;
                case 2:
                    SetValueMinMax(txtCapWeightStandard, txtCapWeightTolerance, txtCapWeightMinValue, txtCapWeightMaxValue);
                    break;
                case 3:
                    SetValueMinMax(txtInnerDiaWOThreadStandard, txtInnerDiaWOThreadTolerance, txtInnerDiaWOThreadMinValue, txtInnerDiaWOThreadMaxValue);
                    break;
                case 4:
                    SetValueMinMax(txtInnerDiaWithThreadStandard, txtInnerDiaWithThreadTolerance, txtInnerDiaWithThreadMinValue, txtInnerDiaWithThreadMaxValue);
                    break;
                case 5:
                    SetValueMinMax(txtOuterDiaStandard, txtOuterDiaTolerance, txtOuterDiaMinValue, txtOuterDiaMaxValue);
                    break;
            }
        }

        private void SetValueMinMax(TextBox RequiredValue_F, TextBox DifferanceRatio_F, TextBox MinValue_F, TextBox MaxValue_F)
        {
            RequiredValue = 0; DifferanceRatio = 0; MinValue = 0; MaxValue = 0;

            double.TryParse(RequiredValue_F.Text, out RequiredValue);
            double.TryParse(DifferanceRatio_F.Text, out DifferanceRatio);

            if (RequiredValue != 0)
            {
                MinValue = RequiredValue - DifferanceRatio;
                //MinValue = RequiredValue - DifferanceRatio;
                MaxValue = RequiredValue + DifferanceRatio;

                MinValue_F.Text = MinValue.ToString();
                MaxValue_F.Text = MaxValue.ToString();
            }
            else
            {
                MinValue_F.Text = "";
                MaxValue_F.Text = "";
            }
        }

        private void txtCapName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbIsWad.Focus();
        }

        private void cmbIsWad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNeckSizeStandard.Focus();
        }

        private void txtNeckSizeStandard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNeckSizeTolerance.Focus();
        }

        private void txtNeckSizeTolerance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCapWeightStandard.Focus();
        }

        private void txtCapWeightStandard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCapWeightTolerance.Focus();
        }

        private void txtCapWeightTolerance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtInnerDiaWOThreadStandard.Focus();
        }

        private void txtInnerDiaWOThreadStandard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtInnerDiaWOThreadTolerance.Focus();
        }

        private void txtInnerDiaWOThreadTolerance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtInnerDiaWithThreadStandard.Focus();
        }

        private void txtInnerDiaWithThreadStandard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtInnerDiaWithThreadTolerance.Focus();
        }

        private void txtInnerDiaWithThreadTolerance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtOuterDiaStandard.Focus();
        }

        private void txtOuterDiaStandard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtOuterDiaTolerance.Focus();
        }

        private void txtOuterDiaTolerance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbStatus.Focus();
        }

        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRemarks.Focus();
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }
    }
}
