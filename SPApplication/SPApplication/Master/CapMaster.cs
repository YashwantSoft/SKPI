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

        public CapMaster()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_CAPMASTER);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();

          //  string ValueNew = Decrypt("qEWwKoRZw1tw2CxkJm6dOZ9YUrWDtASKeyX04vmEn2M=");
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
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    txtCapName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    cmbIsWad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
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
            btnDelete.Enabled = false;
            Result = 0;
            FlagDelete = false;
            objEP.Clear();
            TableID = 0;
            txtCapName.Text = "";
            cmbIsWad.SelectedIndex = -1;
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
                            objBL.Query = "update CapMaster set CapName='" + AposValue.Replace("'", "''") + "',Wad='" + cmbIsWad.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                    else
                        objBL.Query = "insert into CapMaster(CapName,Wad,UserId) values('" + AposValue.Replace("'", "''") + "','" + cmbIsWad.Text + "'," + BusinessLayer.UserId_Static + ")";

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

        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (!SearchTag)
                objBL.Query = "select ID,CapName,Wad from CapMaster where CancelTag=0";
            else
                objBL.Query = "select ID,CapName,Wad from CapMaster where CancelTag=0 and CapName like '%" + txtSearch.Text + "%'";

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 400;
                dataGridView1.Columns[2].Width = 150;
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
    }
}
