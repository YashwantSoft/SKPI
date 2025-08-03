using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace SPApplication.Transaction
{
    public partial class Payment : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();

        int TableId = 0;
        bool DeleteFlag = false;
        string ExecuteType = "";
        bool SearchFlag = false, FlagDelete = false;

        public Payment()
        {
            InitializeComponent();
            objBL.Set_Design_All(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_PAYMENT);
            objRL.Fill_Payment_Type(cmbPaymentMode);
        }

        private string SaveTag = string.Empty;

        private void Payment_Load(object sender, EventArgs e)
        {
            ClearAll();
            lblHeader.Text = RedundancyLogics.PR_String;
            objRL.GetBank(cmbBankName);
            Set_All_Details();
        }

        private void SetCurrency()
        {
            lblCDueAmount.Text = objRL.objRegInfo.CurrencySymbol;
            lblCPaidAmount.Text = objRL.objRegInfo.CurrencySymbol;
            lblCTotalDue.Text = objRL.objRegInfo.CurrencySymbol;
        }

        private void Set_All_Details()
        {
            lbDetails.Items.Clear();
            if (RedundancyLogics.PR_String == "Payment")
            {
                SaveTag = "Payment";
                gbDetails.Text = "Supplier Details";
                lblName.Text = "Supplier Name";
            }
            else
            {
                SaveTag = "Receipt";
                gbDetails.Text = "Customer Details";
                lblName.Text = "Customer Name";
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


        private bool Validation()
        {
            bool ReturnFlag = false;
            objEP.Clear();
            if (ID_Save == 0)
            {
                objEP.SetError(txtDetails, "Enter Name");
                txtDetails.Focus();
                return true;
            }
            else if (txtPaidAmount.Text == "")
            {
                objEP.SetError(txtPaidAmount, "Enter Paid Amount");
                txtPaidAmount.Focus();
                return true;
            }
            else if (txtNaration.Text == "")
            {
                objEP.SetError(txtNaration, "Enter Naration");
                txtNaration.Focus();
                return true;
            }
            else if (cmbPaymentMode.SelectedIndex == -1)
            {
                objEP.SetError(cmbPaymentMode, "Enter Paid Amount");
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

       
        private void SaveDB()
        {
            if (!Validation())
            {
                Fill_BankDetails_PaymentMode();
                if (SaveTag == "Payment")
                {
                    if (TableId != 0)
                        if (FlagDelete == true)
                            objBL.Query = "update Payment set CancelTag=1 where ID=" + TableId + "";
                        else
                            objBL.Query = "update Payment set PaymentDate='" + dtpDate.Value.ToShortDateString() + "',SupplierId=" + ID_Save + ",PaidAmount='" + txtPaidAmount.Text + "',TotalDue='" + txtDueAmount.Text + "',Naration='" + txtNaration.Text + "',PaymentMode='" + cmbPaymentMode.Text + "',BankId=" + BankId + ",BankName='" + BankName + "',AccountNumber='" + AccountNumber + "',TransactionDate='" + dtTransactionDate.ToShortDateString() + "',ChequeNumber='" + ChequeNumber + "',PartyBank='" + PartyBank + "',PartyBankAccountNumber='" + PartyBankAccountNumber + "',UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + TableId + "";
                    else
                        objBL.Query = "insert into Payment(PaymentDate,SupplierId,PaidAmount,TotalDue,Naration,PaymentMode,BankId,BankName,AccountNumber,TransactionDate,ChequeNumber,PartyBank,PartyBankAccountNumber,UserId) values('" + dtpDate.Value.ToShortDateString() + "'," + ID_Save + ",'" + txtPaidAmount.Text + "','" + txtDueAmount.Text + "','" + txtNaration.Text + "','" + cmbPaymentMode.Text + "'," + BankId + ",'" + BankName + "','" + AccountNumber + "','" + dtTransactionDate.ToShortDateString() + "','" + ChequeNumber + "','" + PartyBank + "','" + PartyBankAccountNumber + "'," + BusinessLayer.UserId_Static + ")";
                }
                else
                {
                    if (TableId != 0)
                        if (FlagDelete == true)
                            objBL.Query = "update Receipt set CancelTag=1 where ID=" + TableId + "";
                        else
                            objBL.Query = "update Receipt set PaymentDate='" + dtpDate.Value.ToShortDateString() + "',CustomerId=" + ID_Save + ",PaidAmount='" + txtPaidAmount.Text + "',TotalDue='" + txtDueAmount.Text + "',Naration='" + txtNaration.Text + "',PaymentMode='" + cmbPaymentMode.Text + "',BankId=" + BankId + ",BankName='" + BankName + "',AccountNumber='" + AccountNumber + "',TransactionDate='" + dtTransactionDate.ToShortDateString() + "',ChequeNumber='" + ChequeNumber + "',PartyBank='" + PartyBank + "',PartyBankAccountNumber='" + PartyBankAccountNumber + "',UserId=" + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + TableId + "";
                    else
                        objBL.Query = "insert into Receipt(PaymentDate,CustomerId,PaidAmount,TotalDue,PaymentMode,Naration,BankId,BankName,AccountNumber,TransactionDate,ChequeNumber,PartyBank,PartyBankAccountNumber,UserId) values('" + dtpDate.Value.ToShortDateString() + "'," + ID_Save + ",'" + txtPaidAmount.Text + "','" + txtDueAmount.Text + "','" + cmbPaymentMode.Text + "','" + txtNaration.Text + "'," + BankId + ",'" + BankName + "','" + AccountNumber + "','" + dtTransactionDate.ToShortDateString() + "','" + ChequeNumber + "','" + PartyBank + "','" + PartyBankAccountNumber + "'," + BusinessLayer.UserId_Static + ")";
                }

                int Result = objBL.Function_ExecuteNonQuery();

                if (Result > 0)
                {
                    ViewPendingAmount();
                    PendingAmount_Insert = Convert.ToDouble(txtPaidAmount.Text);
                    PendingAmount_Insert = PendingAmount - PendingAmount_Insert;

                    if (SaveTag == "Payment")
                    {
                        if (objRL.PendingFlag == true)
                            objBL.Query = "update SupplierPendingAmount set PendingAmount='" + PendingAmount_Insert + "' where CancelTag=0 and SupplierId=" + ID_Save + "";
                    }
                    else
                    {
                        if (objRL.PendingFlag == true)
                            objBL.Query = "update CustomerPendingAmount set PendingAmount='" + PendingAmount_Insert + "' where CancelTag=0 and CustomerId=" + ID_Save + "";
                    }

                    objBL.Function_ExecuteNonQuery();
                    FillGrid();
                    Clear_Save();
                    objRL.ShowMessage(7, 1);
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void Clear_Save()
        {
            TableId = 0;
            txtDueAmount.Text = "";
            txtPaidAmount.Text = "";
            txtTotalDue.Text = "";
            cmbPaymentMode.SelectedIndex = -1;
            txtNaration.Text = "";
            cmbBankName.SelectedIndex = -1;
            txtAccountNo.Text = "";
            dtpTransactionDate.Value = DateTime.Now.Date;
            txtChequeNo.Text = "";
            txtTotal.Text = "";
            FillGrid();
            ViewPendingAmount();
        }

        private void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();
            
            if(SaveTag == "Payment")
                objBL.Query = "select P.ID,P.PaymentDate,P.SupplierId,P.PaidAmount,P.TotalDue,P.Naration,P.PaymentMode,P.BankId,P.BankName,P.AccountNumber,P.TransactionDate,P.ChequeNumber,P.PartyBank,P.PartyBankAccountNumber from Payment P inner join Supplier S on S.ID=P.SupplierId where P.CancelTag=0 and S.CancelTag=0 and P.SupplierId=" + ID_Save + " order by P.PaymentDate desc";
            else
                objBL.Query = "select R.ID,R.PaymentDate,R.CustomerId,R.PaidAmount,R.TotalDue,R.Naration,R.PaymentMode,R.BankId,R.BankName,R.AccountNumber,R.TransactionDate,R.ChequeNumber,R.PartyBank,R.PartyBankAccountNumber from Receipt R inner join Customer C on C.ID=R.CustomerId where R.CancelTag=0 and C.CancelTag=0 and R.CustomerId=" + ID_Save + " order by R.PaymentDate desc";

            ds = objBL.ReturnDataSet();
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;

                Total = 0;
                dataGridView1.DataSource = ds.Tables[0];

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                //dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[7].Visible = false;

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = 150;
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Total += Convert.ToDouble(ds.Tables[0].Rows[i][3].ToString());  
                }
                txtTotal.Text = Total.ToString();
            }
        }

        double Total = 0;
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDB();
            }
            catch (Exception ex1) { MessageBox.Show(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void ClearAll()
        {
            SaveTag = RedundancyLogics.PR_String;
            TableId = 0;
            objEP.Clear();
            DeleteFlag = false;
             
            btnDelete.Enabled = false;

            txtDueAmount.Text = "";
            cmbPaymentMode.Text = "";
            txtPaidAmount.Text = "";
            txtTotalDue.Text = "";

            dtpDate.Value = DateTime.Now.Date;
            cmbBankName.Text = "";
            txtAccountNo.Text = "";
            txtChequeNo.Text = "";

            gbAmountDetails.Visible = false;
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteFlag = true;
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAddBankInfo_Click(object sender, EventArgs e)
        {
            Master.CompanyInformation objForm = new SPApplication.Master.CompanyInformation();
            objForm.ShowDialog(this);
        }

        private void cmbPaymentMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            objRL.Set_PaymentMode_Details(cmbPaymentMode, gbChequeDetails, lblChequeNo, txtChequeNo);
        }

        private void Fill_Details()
        {
            ClearAll();
            //dataGridView1.DataSource = null;
            if (txtDetails.Text != "")
                Fill_Comman_ListBox();
            else
                lbDetails.Visible = false;
        }

        string FetchValue = string.Empty;

        private void Fill_Comman_ListBox()
        {
            lbDetails.DataSource = null;
            DataSet ds = new DataSet();
            if (SaveTag == "Payment")
                objBL.Query = "select ID,SupplierName,Address,MobileNumber,EmailId,GSTNumber from Supplier where CancelTag=0 and SupplierName like '%" + txtDetails.Text + "%' order by SupplierName desc";
            else
                objBL.Query = "select ID,CustomerName,Address,Address,MobileNumber,EmailId,GSTNumber from Customer where CancelTag=0 and CustomerName like '%" + txtDetails.Text + "%' order by CustomerName desc";

            ds = objBL.ReturnDataSet();
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbDetails.Visible = true;
                lbDetails.DataSource = ds.Tables[0];
                
                if (SaveTag == "Payment")
                    lbDetails.DisplayMember = "SupplierName";
                else
                    lbDetails.DisplayMember = "CustomerName";

                lbDetails.ValueMember = "ID";
            }
        }
        
        private void txtDetails_TextChanged(object sender, EventArgs e)
        {
            Fill_Details(); 
        }

        private void lbDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Fill_RichTextBox_Details();
        }

        private void lbDetails_Click(object sender, EventArgs e)
        {
            if (lbDetails.Items.Count > 0)
                Fill_RichTextBox_Details();
        }

        int ID_Save=0;
        string SetName = string.Empty; string SetAddress = string.Empty;
        string SetName_Caption = string.Empty; string MobileNumber = string.Empty;
        string GSTNumber = string.Empty;

        private void Fill_RichTextBox_Details()
        {
            ID_Save = Convert.ToInt32(lbDetails.SelectedValue.ToString());

            if (ID_Save != 0)
            {
                DataSet ds = new DataSet();
                if (SaveTag == "Payment")
                    objBL.Query = "select ID,SupplierName,Address,MobileNumber,EmailId,GSTNumber from Supplier where CancelTag=0 and ID=" + ID_Save + "";
                else
                    objBL.Query = "select ID,CustomerName,Address,MobileNumber,EmailId,GSTNumber from Customer where CancelTag=0 and ID=" + ID_Save + "";

                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SetName = "";
                    lbDetails.Visible = false;
                    ID_Save = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());

                    if (SaveTag == "Payment")
                    {
                        SetName_Caption = "Supplier Name:";
                        GSTNumber = ds.Tables[0].Rows[0]["GSTNumber"].ToString();
                        MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                    }
                    else
                    {
                        SetName_Caption = "Customer Name:";
                        GSTNumber = ds.Tables[0].Rows[0]["GSTNumber"].ToString();
                        MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                    }
                    SetName = ds.Tables[0].Rows[0][1].ToString();
                    SetAddress = ds.Tables[0].Rows[0][2].ToString();
                    MobileNumber = ds.Tables[0].Rows[0][2].ToString();

                    rtbDetails.Text = Set_Label();

                    ViewPendingAmount();

                    if (PendingAmount != 0)
                        gbAmountDetails.Visible = true;
                    else
                        gbAmountDetails.Visible = false;

                    FillGrid();
                    txtPaidAmount.Select();
                }
            }
        }

        double PendingAmount_Insert = 0; double PendingAmount = 0;
        private void ViewPendingAmount()    
        {
            if(SaveTag == "Payment")
                PendingAmount = objRL.Get_Pending_Details("SupplierPendingAmount", ID_Save);
            else
                PendingAmount = objRL.Get_Pending_Details("CustomerPendingAmount", ID_Save);

            txtDueAmount.Text = PendingAmount.ToString();
        }

        public string Set_Label()
        {
            return   objRL.StringFormatSet(SetName_Caption, SetName) + "\n" +
                          objRL.StringFormatSet("Company Name:", SetAddress) + "\n" +
                          objRL.StringFormatSet("Mobile No:", MobileNumber) + "\n" +
                          objRL.StringFormatSet("GST Number:", GSTNumber);
        }

        private void txtPaidAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtPaidAmount);
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        double DueAmount = 0, PaidAmount = 0, TotalDue = 0;
        private void CalculateAmount()
        {
            if (txtDueAmount.Text != "" && txtPaidAmount.Text != "")
            {
                DueAmount = 0; PaidAmount = 0; TotalDue = 0;
                double.TryParse(txtDueAmount.Text, out DueAmount);
                double.TryParse(txtPaidAmount.Text, out PaidAmount);

                if (DueAmount > 0)
                {
                    if (PaidAmount <= DueAmount)
                    {
                        TotalDue = DueAmount - PaidAmount;
                        txtTotalDue.Text = TotalDue.ToString();
                    }
                    else
                    {
                        txtPaidAmount.Text = "";
                        txtPaidAmount.Focus();
                        MessageBox.Show("Enter Proper Amount");
                        return;
                    }
                }
                else
                {
                    txtPaidAmount.Text = "";
                    txtPaidAmount.Focus();
                    DueAmount = 0; PaidAmount = 0; TotalDue = 0;
                    MessageBox.Show("Enter Proper Amount");
                    return;
                }
            }
        }

        private void Fill_BankDetails()
        {
            if (cmbBankName.SelectedIndex > -1)
            {
                objRL.GetBankDetails(Convert.ToInt32(cmbBankName.SelectedValue.ToString()));
                txtAccountNo.Text = RedundancyLogics.AccountNo;
            }
        }

        private void cmbBankName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_BankDetails();
        }

        private void cmbPaymentMode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNaration.Focus();
        }

        private void txtPaidAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbPaymentMode.Focus();
        }

        private void txtNaration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbPaymentMode.Text != "CASH")
                {
                    gbChequeDetails.Visible = true;
                    cmbBankName.Focus();
                }
            }
        }

        private void cmbBankName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpTransactionDate.Focus();
        }

        private void dtpTransactionDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtChequeNo.Focus();
        }

        private void txtChequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

         
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //0 P.ID,
                //1 P.PaymentDate,
                //2 P.SupplierId,
                //3 P.PaidAmount,
                //4 P.TotalDue,
                //5 P.Naration,
                //6 P.PaymentMode,
                //7 P.BankId,
                //8 P.BankName,
                //9 P.AccountNumber,
                //10 P.TransactionDate,
                //11 P.ChequeNumber,
                //12 P.PartyBank,
                //13 P.PartyBankAccountNumber
               
                    
                //ClearAll();
                 
                btnDelete.Enabled = true;

                TableId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                dtpDate.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                ID_Save = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                //Fill_RichTextBox_Details();
                txtPaidAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtNaration.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                cmbPaymentMode.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
             
                if (cmbPaymentMode.Text != "CASH" && cmbPaymentMode.Text != "CREDIT")
                {
                    gbChequeDetails.Visible = true;
                    BankId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                    cmbBankName.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    txtAccountNo.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    dtpTransactionDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString());

                    if (cmbPaymentMode.Text == "CHEQUE")
                        txtChequeNo.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    else
                        txtChequeNo.Text = "";

                    txtBankParty.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                    txtAccountNoParty.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                }
                else
                    gbChequeDetails.Visible = false;
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }
    }
}
