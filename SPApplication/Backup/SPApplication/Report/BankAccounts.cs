using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using Microsoft.Office.Interop.Excel;

namespace SPApplication
{
    public partial class BankAccounts : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();

        string BankDetails = string.Empty;

        public BankAccounts()
        {
            InitializeComponent();
            objBL.Set_Report_Design(this, lblHeader, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_BANKACCOUNT);
            objRL.GetBank(cmbBankName);
        }

        private void BankDetails_Load(object sender, EventArgs e)
        {
            dtpFromDate.CustomFormat = RedundancyLogics.SystemDateFormat;
            dtpToDate.CustomFormat = RedundancyLogics.SystemDateFormat;
            ClearAll();
        }

        private void ClearAll()
        {
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            cbToday.Checked = true;
            cmbBankName.SelectedIndex = -1;
            rtbBankDetails.Text = "";
            dgvDebit.Rows.Clear();
            IndexR = 0; BankId = 0;
            dgvCredit.Rows.Clear();
            dgvDebit.Rows.Clear();
            txtTotalCredit.Text = "";
            txtTotalDebit.Text = "";
            lblCountDebit.Text = "";
            lblCountCredit.Text = "";
            BankId = 0; Condition = "";
            cbToday.Focus();
        }

         
        string ConcatString = ""; 
        int SrNo = 1;

        private void FillSrNo(DataGridView dgv)
        {
            if (dgv.Rows.Count > 0)
            {
                SrNo = 1;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].Cells[0].Value = SrNo.ToString();
                    SrNo++;
                }
            }
        }

        double AmountSet = 0;
        private void Fill_Amount(DataGridView dgv)
        {
            if (dgv.Rows.Count > 0)
            {
                AmountSet = 0;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dgv.Rows[i].Cells[4].Value)))
                        AmountSet +=Convert.ToDouble(dgv.Rows[i].Cells[4].Value.ToString());
                }
            }
        }

        string Date_Grid = "", Particulars_Grid = "", PaymentMode_Grid = "", Amount_Grid = "";
        int IndexR = 0;

        private void Fill_Data_GridView(DataGridView dgv)
        {
            dgv.Rows.Add();
            dgv.Rows[IndexR].Cells[1].Value = Date_Grid;
            dgv.Rows[IndexR].Cells[2].Value = Particulars_Grid;
            dgv.Rows[IndexR].Cells[3].Value = PaymentMode_Grid;
            dgv.Rows[IndexR].Cells[4].Value = Amount_Grid;
            IndexR++;

            Save_Bank_Statement();
        }

        int BankId = 0; string Condition = ""; string ChequeNumber = "";
        string TransactionDate = string.Empty;

        private void Save_Bank_Statement()
        {
            DateTime dt_EntryDate;
            dt_EntryDate = Convert.ToDateTime(Date_Grid);

            DateTime dt_TransactionDate;
            dt_TransactionDate = Convert.ToDateTime(TransactionDate);

            objBL.Query = "insert into TempBankStatement(Type,EntryDate,Particulars,PaymentMode,TransactionDate,ChequeNo,Amount) values('" + Type + "','" + dt_EntryDate.ToShortDateString() + "','" + Particulars_Grid + "','" + PaymentMode_Grid + "','" + dt_TransactionDate.ToShortDateString() + "','" + ChequeNumber + "','" + Amount_Grid + "')";
            objBL.Function_ExecuteNonQuery();
        }

        string Type = string.Empty;

        private void btnReport_Click(object sender, EventArgs e)
        {
            IndexR = 0; BankId = 0; AmountSet = 0;
            dgvCredit.Rows.Clear();
            dgvDebit.Rows.Clear();

            if (cmbBankName.SelectedIndex > -1)
            {
                BankId = Convert.ToInt32(cmbBankName.SelectedValue.ToString());
                Condition = " and P.BankId=" + BankId + "";
            }
            else
                Condition = "";
            //Debit 
            Type = "Debit";

            DataSet dsPurchase = new DataSet();
            //objBL.Query = "select P.ID,P.PaymentDate,P.SupplierId,S.SupplierName,P.PaidAmount,P.TotalDue,P.PaymentMode,P.Naration,P.BankId,P.BankName,P.AccountNumber,P.TransactionDate,P.ChequeNumber from Payment P inner join Supplier S on S.ID=P.SupplierId where P.CancelTag=0 and S.CancelTag=0 and P.PaymentDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and P.PaymentMode in('CHEQUE','NEFT','RTGS','BANK DEPOSITE') " + Condition + ""; ;
            objBL.Query = "select P.ID,P.PurchaseDate,P.PurchaseNo,P.ChallanNo,P.BillNo,P.SupplierId,S.SupplierName,S.Address,S.MobileNumber,S.EmailId,S.GSTNumber,P.SubTotal,P.TotalGST,P.FreightCharges,P.LoadingAndPackingCharges,P.InsuranceCharges,P.OtherCharges,P.InvoiceTotal,P.PaymentMode,P.BankId,P.BankName,P.AccountNumber,P.TransactionDate,P.ChequeNumber,P.PartyBank,P.PartyBankAccountNumber from Purchase P inner join Supplier S on S.ID=P.SupplierId where P.CancelTag=0 and S.CancelTag=0 and P.IsGST=1 and P.PurchaseDate BETWEEN " + "#" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and P.PaymentMode in('CHEQUE','NEFT','RTGS','BANK DEPOSITE') " + Condition + " order by P.PurchaseDate desc";
            dsPurchase = objBL.ReturnDataSet();

            if (dsPurchase.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsPurchase.Tables[0].Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dsPurchase.Tables[0].Rows[i]["ID"].ToString()))
                    {
                        ConcatString = "";
                        ConcatString = objRL.Check_Null_String(Convert.ToString(dsPurchase.Tables[0].Rows[i]["SupplierName"]));

                        if (!string.IsNullOrEmpty(Convert.ToString(dsPurchase.Tables[0].Rows[i]["PaymentMode"])))
                            PaymentMode_Grid = dsPurchase.Tables[0].Rows[i]["PaymentMode"].ToString();

                        if (PaymentMode_Grid == "CHEQUE")
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dsPurchase.Tables[0].Rows[i]["ChequeNumber"])))
                            {
                                ChequeNumber = objRL.Check_Null_String(Convert.ToString(dsPurchase.Tables[0].Rows[i]["ChequeNumber"]));
                                TransactionDate = objRL.Return_Date_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(dsPurchase.Tables[0].Rows[i]["TransactionDate"])));

                                ConcatString += "   " + ChequeNumber;
                                ConcatString += "  Date: " + TransactionDate;
                            }
                        }

                        TransactionDate = objRL.Return_Date_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(dsPurchase.Tables[0].Rows[i]["TransactionDate"])));
                        Date_Grid = objRL.Return_Date_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(dsPurchase.Tables[0].Rows[i]["PurchaseDate"])));
                        Particulars_Grid = ConcatString;
                        PaymentMode_Grid = objRL.Check_Null_String(Convert.ToString(dsPurchase.Tables[0].Rows[i]["PaymentMode"]));
                        Amount_Grid = objRL.Check_Null_String(Convert.ToString(dsPurchase.Tables[0].Rows[i]["InvoiceTotal"]));
                        Fill_Data_GridView(dgvDebit);
                    }
                }
            }


            DataSet dsPayment = new DataSet();
            objBL.Query = "select P.ID,P.PaymentDate,P.SupplierId,S.SupplierName,P.PaidAmount,P.TotalDue,P.PaymentMode,P.Naration,P.BankId,P.BankName,P.AccountNumber,P.TransactionDate,P.ChequeNumber from Payment P inner join Supplier S on S.ID=P.SupplierId where P.CancelTag=0 and S.CancelTag=0 and P.PaymentDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and P.PaymentMode in('CHEQUE','NEFT','RTGS','BANK DEPOSITE') " + Condition + ""; ;
            dsPayment = objBL.ReturnDataSet();
            
            if (dsPayment.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsPayment.Tables[0].Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dsPayment.Tables[0].Rows[i]["ID"].ToString()))
                    {
                        ConcatString = "";
                        ConcatString = objRL.Check_Null_String(Convert.ToString(dsPayment.Tables[0].Rows[i]["SupplierName"]));

                        if (!string.IsNullOrEmpty(Convert.ToString(dsPayment.Tables[0].Rows[i]["PaymentMode"])))
                            PaymentMode_Grid = dsPayment.Tables[0].Rows[i]["PaymentMode"].ToString();

                        if (PaymentMode_Grid == "CHEQUE")
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dsPayment.Tables[0].Rows[i]["ChequeNumber"])))
                            {
                                ChequeNumber = objRL.Check_Null_String(Convert.ToString(dsPayment.Tables[0].Rows[i]["ChequeNumber"]));
                                TransactionDate = objRL.Return_Date_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(dsPayment.Tables[0].Rows[i]["TransactionDate"])));
                               
                                ConcatString += "   " + ChequeNumber;
                                ConcatString += "  Date: " + TransactionDate;
                            }
                        }

                        TransactionDate = objRL.Return_Date_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(dsPayment.Tables[0].Rows[i]["TransactionDate"])));
                        Date_Grid = objRL.Return_Date_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(dsPayment.Tables[0].Rows[i]["PaymentDate"])));
                        Particulars_Grid = ConcatString;
                        PaymentMode_Grid = objRL.Check_Null_String(Convert.ToString(dsPayment.Tables[0].Rows[i]["PaymentMode"]));
                        Amount_Grid = objRL.Check_Null_String(Convert.ToString(dsPayment.Tables[0].Rows[i]["PaidAmount"]));
                        Fill_Data_GridView(dgvDebit);
                    }
                }
            }

            DataSet dsExpenses = new DataSet();
            //objBL.Query = "select P.ID,P.PaymentDate,P.SupplierId,S.SupplierName,P.PaidAmount,P.TotalDue,P.PaymentMode,P.Naration,P.BankId,P.BankName,P.AccountNumber,P.TransactionDate,P.ChequeNumber from Payment P inner join Supplier S on S.ID=P.SupplierId where P.CancelTag=0 and S.CancelTag=0 and P.PaymentDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and P.PaymentMode in('CHEQUE','NEFT','RTGS','BANK DEPOSITE')";
            objBL.Query = "select P.ID,P.EntryDate,P.ExpensesHeadId,P.ExpensesHead,P.Naration,P.Amount,P.PaymentMode,P.BankName,P.AccountNumber,P.TransactionDate,P.Narration from Expenses P where P.CancelTag=0 and P.EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and P.PaymentMode in('CHEQUE','NEFT','RTGS','BANK DEPOSITE') " + Condition + "";
            dsExpenses = objBL.ReturnDataSet();
            if (dsExpenses.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsExpenses.Tables[0].Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dsExpenses.Tables[0].Rows[i]["ID"].ToString()))
                    {
                        ConcatString = "";
                        ConcatString = objRL.Check_Null_String(Convert.ToString(dsExpenses.Tables[0].Rows[i]["ExpensesHead"]));
                        ConcatString += "   " + objRL.Check_Null_String(Convert.ToString(dsExpenses.Tables[0].Rows[i]["ExpensesHead"]));
                        ConcatString += "   " + objRL.Check_Null_String(Convert.ToString(dsExpenses.Tables[0].Rows[i]["Narration"]));

                        Date_Grid = objRL.Return_Date_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(dsExpenses.Tables[0].Rows[i]["EntryDate"])));
                        Particulars_Grid = ConcatString;
                        PaymentMode_Grid = objRL.Check_Null_String(Convert.ToString(dsExpenses.Tables[0].Rows[i]["PaymentMode"]));
                        Amount_Grid = objRL.Check_Null_String(Convert.ToString(dsExpenses.Tables[0].Rows[i]["Amount"]));
                        Fill_Data_GridView(dgvDebit);
                    }
                }
            }

            FillSrNo(dgvDebit);
            Fill_Amount(dgvDebit);
            txtTotalDebit.Text = AmountSet.ToString();
            lblCountDebit.Text = "Total Debit Count: " + dgvDebit.Rows.Count;

            //Credit
            Type = "Credit";
            IndexR = 0; AmountSet = 0;
            DataSet dsReceipt = new DataSet();
            //objBL.Query = "select P.ID,P.PaymentDate,P.SupplierId,S.SupplierName,P.PaidAmount,P.TotalDue,P.PaymentMode,P.Naration,P.BankId,P.BankName,P.AccountNumber,P.TransactionDate,P.ChequeNumber from Payment P inner join Supplier S on S.ID=P.SupplierId where P.CancelTag=0 and S.CancelTag=0 and P.PaymentDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and P.PaymentMode in('CHEQUE','NEFT','RTGS','BANK DEPOSITE')";
            objBL.Query = "select P.ID,P.PaymentDate,P.CustomerId,P.PaidAmount,P.TotalDue,P.PaymentMode,P.Naration,P.BankId,P.BankName,P.AccountNumber,P.TransactionDate,P.ChequeNumber from Receipt P inner join Customer C on C.ID=P.CustomerId where P.CancelTag=0 and C.CancelTag=0 and P.PaymentDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and P.PaymentMode in('CHEQUE','NEFT','RTGS','BANK DEPOSITE') " + Condition + "";
            dsReceipt = objBL.ReturnDataSet();
            if (dsReceipt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsReceipt.Tables[0].Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dsReceipt.Tables[0].Rows[i]["ID"].ToString()))
                    {
                        ConcatString = "";
                        ConcatString = objRL.Check_Null_String(Convert.ToString(dsReceipt.Tables[0].Rows[i]["Naration"]));

                        if (!string.IsNullOrEmpty(Convert.ToString(dsReceipt.Tables[0].Rows[i]["ChequeNumber"])))
                            ConcatString += "   " + objRL.Check_Null_String(Convert.ToString(dsReceipt.Tables[0].Rows[i]["ChequeNumber"]));

                        Date_Grid = objRL.Return_Date_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(dsReceipt.Tables[0].Rows[i]["PaymentDate"])));
                        Particulars_Grid = ConcatString;
                        PaymentMode_Grid = objRL.Check_Null_String(Convert.ToString(dsReceipt.Tables[0].Rows[i]["PaymentMode"]));
                        Amount_Grid = objRL.Check_Null_String(Convert.ToString(dsReceipt.Tables[0].Rows[i]["PaidAmount"]));
                        Fill_Data_GridView(dgvCredit);
                    }
                }
            }


            DataSet dsDeposite = new DataSet();
            //objBL.Query = "select P.ID,P.PaymentDate,P.SupplierId,S.SupplierName,P.PaidAmount,P.TotalDue,P.PaymentMode,P.Naration,P.BankId,P.BankName,P.AccountNumber,P.TransactionDate,P.ChequeNumber from Payment P inner join Supplier S on S.ID=P.SupplierId where P.CancelTag=0 and S.CancelTag=0 and P.PaymentDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and P.PaymentMode in('CHEQUE','NEFT','RTGS','BANK DEPOSITE')";
            //objBL.Query = "select ID,EntryDate,ExpensesHeadId,ExpensesHead,Naration,Amount,PaymentMode,BankName,AccountNumber,ChequeDate,Narration,UserId from Expenses where CancelTag=0 and EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and PaymentMode in('CHEQUE','NEFT','RTGS','BANK DEPOSITE')"; ;
            objBL.Query = "select P.ID,P.EntryDate,P.DepositHeadId,P.DepositHead,P.Naration,P.Amount,P.PaymentMode,P.BankName,P.AccountNumber,P.TransactionDate,P.Narration from Deposit P where P.CancelTag=0 and P.EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and P.PaymentMode in('CHEQUE','NEFT','RTGS','BANK DEPOSITE') " + Condition + "";
            dsDeposite = objBL.ReturnDataSet();
            if (dsDeposite.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsDeposite.Tables[0].Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dsDeposite.Tables[0].Rows[i]["ID"].ToString()))
                    {
                        ConcatString = "";
                        ConcatString = objRL.Check_Null_String(Convert.ToString(dsDeposite.Tables[0].Rows[i]["DepositHead"]));
                        ConcatString += "   " + objRL.Check_Null_String(Convert.ToString(dsDeposite.Tables[0].Rows[i]["Naration"]));
                        ConcatString += "   " + objRL.Check_Null_String(Convert.ToString(dsDeposite.Tables[0].Rows[i]["Narration"]));

                        Date_Grid = objRL.Return_Date_String_DDMMYYYY(objRL.Check_Null_String(Convert.ToString(dsDeposite.Tables[0].Rows[i]["EntryDate"])));
                        Particulars_Grid = ConcatString;
                        PaymentMode_Grid = objRL.Check_Null_String(Convert.ToString(dsDeposite.Tables[0].Rows[i]["PaymentMode"]));
                        Amount_Grid = objRL.Check_Null_String(Convert.ToString(dsDeposite.Tables[0].Rows[i]["Amount"]));
                        Fill_Data_GridView(dgvCredit);
                    }
                }
            }
            FillSrNo(dgvCredit);
            Fill_Amount(dgvCredit);
            txtTotalCredit.Text = AmountSet.ToString();
            lblCountCredit.Text = "Total Credit Count: " + dgvCredit.Rows.Count;
        }
       
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Fill_FillBankDetails()
        {
            if (cmbBankName.SelectedIndex > -1)
            {
                rtbBankDetails.Text = "";
                BankDetails = string.Empty;
                objRL.GetBankDetails(Convert.ToInt32(cmbBankName.SelectedValue.ToString()));
                Set_Label();
            }
        }

       
        public void Set_Label()
        {
            BankDetails = objRL.StringFormatSet("Account Number:      ",RedundancyLogics.AccountNo) + "\n" +
                          objRL.StringFormatSet("Bank Address:        ", RedundancyLogics.BankAddress) + "\n" +
                          objRL.StringFormatSet("Account Type:        ", RedundancyLogics.AccountType) + "\n" +
                          objRL.StringFormatSet("Account Holder Name: ", RedundancyLogics.AccountHolderName) + "\n" +
                          objRL.StringFormatSet("IFSC Code:                 ", RedundancyLogics.IFSCCode);

            rtbBankDetails.Text = BankDetails;
        }

        private void cmbBankName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_FillBankDetails();
        }

        bool DateFlag = false;
        private void cbToday_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbToday.Checked)
            {
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
                DateFlag = true;
            }
            else
            {
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
            }
        }
    }
}
