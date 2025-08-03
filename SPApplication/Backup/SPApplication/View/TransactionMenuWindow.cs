using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using SPApplication.Transaction;
using TestApplication;

namespace SPApplication
{
    public partial class TransactionMenuWindow : Form
    {
        public TransactionMenuWindow()
        {
            InitializeComponent();
            set_Design();
        }

        private void set_Design()
        {
            btnPurchase.BackgroundImage = BusinessResources.Purchase;
            btnSale.BackgroundImage = BusinessResources.Sale;
            btnDeposit.BackgroundImage = BusinessResources.Deposit;
            btnExpenses.BackgroundImage = BusinessResources.Expenses;
            btnPayment.BackgroundImage = BusinessResources.Payment;
            btnReceipt.BackgroundImage = BusinessResources.Receipts;
            btnExit.BackgroundImage = BusinessResources.Exit;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            Purchase objForm = new Purchase();
            objForm.ShowDialog(this);
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
           // Sale objForm = new Sale();
            SalesTransaction objForm = new SalesTransaction();
            objForm.ShowDialog(this);
        }

        private void btnCreditNote_Click(object sender, EventArgs e)
        {
            CRNote objForm = new CRNote();
            objForm.ShowDialog(this);
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            RedundancyLogics.DE_String = "Expenses";
            Expenses objForm = new Expenses();
            objForm.ShowDialog(this);
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            RedundancyLogics.PR_String = "Payment";
            Payment objForm = new Payment();
            objForm.ShowDialog(this);
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            RedundancyLogics.PR_String = "Receipt";
            RedundancyLogics.SaleReceipt = "No";
            Payment objForm = new Payment();
            objForm.ShowDialog(this);
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            RedundancyLogics.DE_String = "Deposit";
            Expenses objForm = new Expenses();
            objForm.ShowDialog(this);
        }

        private void TransactionMenuWindow_Load(object sender, EventArgs e)
        {

        }

        private void btnDailyBook_Click(object sender, EventArgs e)
        {
            DailyBookEntry objForm = new DailyBookEntry();
            objForm.ShowDialog(this);
        }

        private void btnSupplierPayment_Click(object sender, EventArgs e)
        {
            BillDetails objForm = new BillDetails();
            objForm.ShowDialog(this);
        }
    }
}
