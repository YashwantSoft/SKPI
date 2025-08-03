using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using SPApplication.Master;

namespace SPApplication.Transaction
{
    public partial class Expenses : Form
    {
        public Expenses()
        {
            InitializeComponent();
            objBL.Set_Design_All(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_EXPENSES);
            Set_All_Details();
            objRL.Fill_Payment_Type(cmbPaymentMode);
        }

        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        private string SaveTag = string.Empty;
        private void Set_All_Details()
        {
            if (RedundancyLogics.DE_String == "Deposit")
            {
                SaveTag = "Deposit";
                lblHead.Text = "Deposit Head";
            }
            else
            {
                SaveTag = "Expenses";
                lblHead.Text = "Expenses Head";
            }
        }

        private void cmbPaymentMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            objRL.Set_PaymentMode_Details(cmbPaymentMode, gbChequeDetails, lblChequeNo, txtChequeNo);
        }


        private void Expenses_Load(object sender, EventArgs e)
        {
            ClearAll();
            SearchFlag = false;
            FillExpensesHead();
            FillGrid();
            objRL.GetBank(cmbBankName);
            lblHeader.Text = RedundancyLogics.DE_String;
            cmbExpensesHead.Focus();
        }

        private void FillBankName()
        {
            objBL.Query = "select ID,BankName from BankDetails where CancelTag=0";
            objBL.FillComboBox(cmbBankName, "BankName", "ID");
        }

        private void FillExpensesHead()
        {
            if (RedundancyLogics.DE_String == "Deposit")
            {
                objBL.Query = "select ID,DepositHeadMain from DepositHead where CancelTag=0";
                objBL.FillComboBox(cmbExpensesHead, "DepositHeadMain", "ID");
            }
            else
            {
                objBL.Query = "select ID,ExpensesHeadMain from ExpensesHead where CancelTag=0";
                objBL.FillComboBox(cmbExpensesHead, "ExpensesHeadMain", "ID");
            }

        }

        private void btnAddExpensesHead_Click(object sender, EventArgs e)
        {
            ExpensesHead objForm = new ExpensesHead();
            objForm.ShowDialog(this);
            FillExpensesHead();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool Validation()
        {
            objEP.Clear();

            if (cmbExpensesHead.SelectedIndex == -1)
            {
                objEP.SetError(cmbExpensesHead, "Select Expenses Head");
                cmbExpensesHead.Focus();
                return true;
            }
            else if (txtNaration.Text == "")
            {
                objEP.SetError(txtNaration, "Enter Expenses Description");
                txtNaration.Focus();
                return true;
            }
            else if (txtAmount.Text == "")
            {
                objEP.SetError(txtAmount, "Enter Expenses Description");
                txtAmount.Focus();
                return true;
            }
            else if (cmbPaymentMode.SelectedIndex == -1)
            {
                objEP.SetError(cmbPaymentMode, "Select Payment Type");
                cmbPaymentMode.Focus();
                return true;
            }
            else if (ValidationBank())
            {
                return true;
            }
            else
                return false;
        }

        private bool Validation_CheckNEFT()
        {
            bool ReturnFlag = false;
            objEP.Clear();

            if (cmbPaymentMode.Text == "Cheque")
            {
                if (txtChequeNo.Text == "")
                {
                    ReturnFlag = true;
                    objEP.SetError(txtChequeNo, "Enter Cheque Number");
                    txtChequeNo.Focus();
                }
                else if (cmbBankName.SelectedIndex == -1)
                {
                    ReturnFlag = true;
                    objEP.SetError(cmbBankName, "Select Bank Name");
                    cmbBankName.Focus();
                }
                else
                    ReturnFlag = false;
            }
            else
                ReturnFlag = false;

            return ReturnFlag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                SaveDB();
                FillGrid();
                ClearAll();
                objRL.ShowMessage(7, 1);
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void ClearAll()
        {
            objEP.Clear();
            dtpFromDate.Enabled = false;
            lbl.Enabled = false;
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            gbChequeDetails.Visible = false;
            cmbExpensesHead.SelectedIndex = -1;
            cmbPaymentMode.SelectedIndex = -1;
            cmbBankName.SelectedIndex = -1;
            txtNaration.Text = "";
            txtSearch.Text = "";
            SearchFlag = false;
            txtAmount.Text = "";
            txtChequeNo.Text = "";
            txtAccountNo.Text = "";
            txtAccountNoParty.Text = "";
            txtBankParty.Text = "";
            TableID = 0;
            dtpDate.Value = DateTime.Now.Date;
            dtpTransactionDate.Value = DateTime.Now.Date;
            cmbExpensesHead.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void FillGrid()
        {
            string ConcatString = " and EntryDate BETWEEN #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# ORDER BY EntryDate";
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();
            if (RedundancyLogics.DE_String == "Deposit")
            {
                if (SearchFlag == false)
                    objBL.Query = "select ID,ID as [SrNo],EntryDate as [Date],DepositHeadId,DepositHead as [Deposit Head],Naration,Amount,PaymentMode as [Payment Mode],BankId,BankName as [Bank Name],AccountNumber as [Account Number],TransactionDate as [Transaction Date],ChequeNumber as [Cheque Number],PartyBank as [Party Bank],PartyBankAccountNumber as [Party Account Number] from Deposit where CancelTag=0 " + ConcatString + "";
                else
                    objBL.Query = "select ID,ID as [SrNo],EntryDate as [Date],DepositHeadId,DepositHead as [Deposit Head],Naration,Amount,PaymentMode as [Payment Mode],BankId,BankName as [Bank Name],AccountNumber as [Account Number],TransactionDate as [Transaction Date],ChequeNumber as [Cheque Number],PartyBank as [Party Bank],PartyBankAccountNumber as [Party Account Number] from Deposit where CancelTag=0 and DepositHead like '%" + txtSearch.Text + "%' " + ConcatString + "";
            }
            else
            {
                if (SearchFlag == false)
                    objBL.Query = "select ID,ID as [SrNo],EntryDate as [Date],ExpensesHeadId,ExpensesHead as [Expenses Head],Naration,Amount,PaymentMode as [Payment Mode],BankId,BankName as [Bank Name],AccountNumber as [Account Number],TransactionDate as [Transaction Date],ChequeNumber as [Cheque Number],PartyBank as [Party Bank],PartyBankAccountNumber as [Party Account Number] from Expenses where CancelTag=0 " + ConcatString + "";
                else
                    objBL.Query = "select ID,ID as [SrNo],EntryDate as [Date],ExpensesHeadId,ExpensesHead as [Expenses Head],Naration,Amount,PaymentMode as [Payment Mode],BankId,BankName as [Bank Name],AccountNumber as [Account Number],TransactionDate as [Transaction Date],ChequeNumber as [Cheque Number],PartyBank as [Party Bank],PartyBankAccountNumber as [Party Account Number] from Expenses where CancelTag=0 and ExpensesHead like '%" + txtSearch.Text + "%' " + ConcatString + "";
            }
            ds = objBL.ReturnDataSet();
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                SrNo = 1; TotalCount = 0;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[12].Visible = false;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TotalCount += Convert.ToDouble(objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[i].Cells[6].Value)));
                    dataGridView1.Rows[i].Cells[1].Value = SrNo.ToString();
                    SrNo++;
                }
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = 170;
                }
                dataGridView1.Columns[1].Width = 60;
                this.dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                txtTotalAmount.Text = TotalCount.ToString();
            }
        }

        double TotalCount = 0; int SrNo = 1;
        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
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
                    FillBankName();
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    cmbExpensesHead.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtNaration.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    cmbPaymentMode.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                     

                    if (cmbPaymentMode.Text != "CASH" && cmbPaymentMode.Text != "CREDIT")
                    {
                        gbChequeDetails.Visible = true;
                        BankId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());
                        cmbBankName.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                        txtAccountNo.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                        dtpTransactionDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString());

                        if (cmbPaymentMode.Text == "CHEQUE")
                            txtChequeNo.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                        else
                            txtChequeNo.Text = "";

                        txtBankParty.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                        txtAccountNoParty.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                    }
                    else
                        gbChequeDetails.Visible = false;
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

        string ChequeNumber = "", PartyBankAccountNumber = "", PartyBank = "", AccountNumber = "", BankName = "";
        int BankId = 0;
        DateTime dtTransactionDate;

        private void Fill_BankDetails_PaymentMode()
        {
            if (cmbPaymentMode.Text != "CASH" && cmbPaymentMode.Text != "CREDIT")
            {
                if (cmbPaymentMode.Text == "CHEQUE")
                    ChequeNumber = txtChequeNo.Text;

                BankId = Convert.ToInt32(cmbBankName.SelectedValue);
                AccountNumber = txtAccountNo.Text;
                BankName = cmbBankName.Text;

                PartyBankAccountNumber = txtAccountNoParty.Text;
                PartyBank = txtBankParty.Text;
                dtTransactionDate = dtpTransactionDate.Value;
            }
        }

        

        private bool ValidationBank()
        {
            bool ReturnFlag = false;
            objEP.Clear();

            if (cmbPaymentMode.Text != "CASH" && cmbPaymentMode.Text != "CREDIT")
            {
                if (cmbBankName.SelectedIndex == -1)
                {
                    cmbBankName.Focus();
                    objEP.SetError(cmbBankName, "Select Bank Name");
                    ReturnFlag = true;
                }
                else
                    ReturnFlag = false;

                if (!ReturnFlag)
                {
                    if (cmbBankName.Text == "CHEQUE")
                    {
                        if (txtChequeNo.Text == "")
                        {
                            txtChequeNo.Focus();
                            objEP.SetError(txtChequeNo, "Enter Cheque Number");
                            ReturnFlag = true;
                        }
                        else
                            ReturnFlag = false;
                    }
                    else
                        ReturnFlag = false;
                }
                if (!ReturnFlag)
                {
                    if (txtBankParty.Text == "")
                    {
                        txtBankParty.Focus();
                        objEP.SetError(txtBankParty, "Enter Bank Party");
                        ReturnFlag = true;
                    }
                    else
                        ReturnFlag = false;
                }
                if (!ReturnFlag)
                {
                    if (txtAccountNoParty.Text == "")
                    {
                        txtAccountNoParty.Focus();
                        objEP.SetError(txtAccountNoParty, "Enter Account No Party");
                        ReturnFlag = true;
                    }
                    else
                        ReturnFlag = false;
                }
            }
            else
                ReturnFlag = false;

            return ReturnFlag;
        }
        
        protected void SaveDB()
        {
            if (!Validation())
            {
                Fill_BankDetails_PaymentMode();
                if (TableID != 0)
                {
                    if (FlagDelete == true)
                    {
                        if (RedundancyLogics.DE_String == "Deposit")
                            objBL.Query = "update Deposit set CancelTag=1 where ID=" + TableID + "";
                        else
                            objBL.Query = "update Expenses set CancelTag=1 where ID=" + TableID + "";
                    }
                    else
                    {
                        if (RedundancyLogics.DE_String == "Deposit")
                            objBL.Query = "update Deposit set EntryDate='" + dtpDate.Value.ToShortDateString() + "',DepositHeadId=" + cmbExpensesHead.SelectedValue + ",DepositHead='" + cmbExpensesHead.Text + "',Naration='" + txtNaration.Text + "', Amount='" + txtAmount.Text + "',PaymentMode='" + cmbPaymentMode.Text + "', BankId=" + BankId + ",BankName='" + BankName + "',AccountNumber='" + AccountNumber + "',TransactionDate='" + dtTransactionDate.ToShortDateString() + "',PartyBank='" + PartyBank + "',PartyBankAccountNumber='" + PartyBankAccountNumber + "',UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + TableID + "";
                        else
                            objBL.Query = "update Expenses set EntryDate='" + dtpDate.Value.ToShortDateString() + "',ExpensesHeadId=" + cmbExpensesHead.SelectedValue + ",ExpensesHead='" + cmbExpensesHead.Text + "',Naration='" + txtNaration.Text + "', Amount='" + txtAmount.Text + "',PaymentMode='" + cmbPaymentMode.Text + "', BankId=" + BankId + ",BankName='" + BankName + "',AccountNumber='" + AccountNumber + "',TransactionDate='" + dtTransactionDate.ToShortDateString() + "',PartyBank='" + PartyBank + "',PartyBankAccountNumber='" + PartyBankAccountNumber + "',UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + TableID + "";
                    }
                }
                else
                {
                    if (RedundancyLogics.DE_String == "Deposit")
                        objBL.Query = "insert into Deposit(EntryDate,DepositHeadId,DepositHead,Naration,Amount,PaymentMode,BankId,BankName,AccountNumber,TransactionDate,ChequeNumber,PartyBank,PartyBankAccountNumber,UserId) values('" + dtpDate.Value.ToShortDateString() + "'," + cmbExpensesHead.SelectedValue + ",'" + cmbExpensesHead.Text + "','" + txtNaration.Text + "','" + txtAmount.Text + "','" + cmbPaymentMode.Text + "'," + BankId + ",'" + BankName + "','" + AccountNumber + "','" + dtTransactionDate.ToShortDateString() + "','" + ChequeNumber + "','" + PartyBank + "','" + PartyBankAccountNumber + "'," + BusinessLayer.UserId_Static + ")";
                    else
                        objBL.Query = "insert into Expenses(EntryDate,ExpensesHeadId,ExpensesHead,Naration,Amount,PaymentMode,BankId,BankName,AccountNumber,TransactionDate,ChequeNumber,PartyBank,PartyBankAccountNumber,UserId) values('" + dtpDate.Value.ToShortDateString() + "'," + cmbExpensesHead.SelectedValue + ",'" + cmbExpensesHead.Text + "','" + txtNaration.Text + "','" + txtAmount.Text + "','" + cmbPaymentMode.Text + "'," + BankId + ",'" + BankName + "','" + AccountNumber + "','" + dtTransactionDate.ToShortDateString() + "','" + ChequeNumber + "','" + PartyBank + "','" + PartyBankAccountNumber + "'," + BusinessLayer.UserId_Static + ")";
                }

                objBL.Function_ExecuteNonQuery();
                FillGrid();
                ClearAll();
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
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
                    ClearAll();
                    FillGrid();
                    objRL.ShowMessage(9, 1);
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

        private void cmbExpensesHead_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNaration.Focus();
        }

        private void txtExpensesDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAmount.Focus();
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbPaymentMode.Focus();
        }

        private void dtpChequeDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbBankName.Focus();
        }

        private void txtBankNameCheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtChequeNo.Focus();
        }

        private void txtChequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        private void txtAccountNoNEFT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        private void btnAddBankInfo_Click(object sender, EventArgs e)
        {
            CompanyInformation objForm = new CompanyInformation();
            objForm.ShowDialog(this);
        }

        private void cmbBankName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbBankName.SelectedIndex > -1)
            {
                objRL.GetBankDetails(Convert.ToInt32(cmbBankName.SelectedValue));
                {
                    RedundancyLogics.BankID = 0;
                    txtAccountNo.Text = RedundancyLogics.AccountNo;
                    RedundancyLogics.BankID = Convert.ToInt32(cmbBankName.SelectedValue);
                }
            }
        }
        public bool SearchFlag = false;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                SearchFlag = true;
                FillGrid();
            }
            else
            {
                SearchFlag = false;
                FillGrid();
            }
        }

        bool DateFlag = false;
        private void cbToday_CheckedChanged(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;

            if (!cbToday.Checked)
            {
                dtpFromDate.Enabled = true;
                lbl.Enabled = true;
                DateFlag = true;
            }
            else
            {
                dtpFromDate.Enabled = false;
                lbl.Enabled = false;
            }

            FillGrid();
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void cmbBankName_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            Fill_BankDetails();
        }

        private void Fill_BankDetails()
        {
            if (cmbBankName.SelectedIndex > -1)
            {
                objRL.GetBankDetails(Convert.ToInt32(cmbBankName.SelectedValue.ToString()));
                txtAccountNo.Text = RedundancyLogics.AccountNo;
            }
        }
    }
}
