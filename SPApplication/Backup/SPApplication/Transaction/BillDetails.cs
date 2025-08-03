using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace TestApplication
{
    public partial class BillDetails : Form
    {
        string mString = "", captionMessage = "";
        static double NewPendingAmount = 0;

        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        ErrorProvider objEP = new ErrorProvider();
        
        
        List<string> Para = new List<string>();

        int rowIndex = 0, saveGridRow = 0, TableID = 0;

        public BillDetails()
        {
            InitializeComponent();
            BillFlag = "Fix";
            rbFix.Checked = true;
        }

        protected void FillSupplier()
        {
            objBL.Query = "select ID,SupplierName from Supplier ";
            objBL.FillComboBox(cmbSupplierName, "SupplierName", "ID");

            //objRL.ClearComboValues();
            //objRL.StoreProcedure = "SP_Select_Supplier_For_ComboBox";
            //objRL.ParaComboFields.Add("TableID");
            //objRL.ParaComboFields.Add("ContractorName");
            //objRL.ParaComboFields.Add("Select Customer Name");
            //objRL.Fill_All_Combo(cmbSupplierName);

        }
        private void BillDetails_Load(object sender, EventArgs e)
        {
            dtpDate.CustomFormat = "dd/MM/yyyy";
            dtpChequeDate.CustomFormat = "dd/MM/yyyy";
            //uf.FillCombo(cmbSupplierName, "TableID", "ContractorName", "SP_Select_Supplier_For_ComboBox");
            FillSupplier();
            CancelAll();
            
        }

        double TotalPurchaseAmount = 0, TotalPaidAmount = 0, NetPendingAmount = 0;
        string CheckNullString = "";

        protected void ClearValuesPurchasePaid()
        {
            txtTotalPurchaseAmount.Text = "";
            txtTotalPaidAmount.Text = "";
            NetPendingAmount = 0;
            TotalPurchaseAmount = 0;
            TotalPaidAmount = 0;
            txtNetPendingAmount.Text = ""; 
        }

        protected void Calculate_Purchase_Paid()
        {
            ClearValuesPurchasePaid();

            DataSet DS_Address = new DataSet();
            objBL.Query = "select ID,SupplierName,Address,MobileNumber,EmailId,GSTNumber,StateCode from Supplier where ID=" + cmbSupplierName.SelectedValue + "";
            DS_Address = objBL.ReturnDataSet();
            if (DS_Address.Tables[0].Rows.Count > 0)
                txtAddress.Text = Convert.ToString(DS_Address.Tables[0].Rows[0]["Address"].ToString());

            DataSet dsPurchse = new DataSet();
            objBL.Query = "select sum(TotalAmount) from BillDetails where SupplierId=" + cmbSupplierName.SelectedValue + " and BillFlag='" + BillFlag + "'";
            dsPurchse = objBL.ReturnDataSet();
            if (dsPurchse.Tables[0].Rows.Count > 0)
                CheckNullString = dsPurchse.Tables[0].Rows[0][0].ToString();
                if(CheckNullString !=null  && CheckNullString !="")
                    TotalPurchaseAmount = Convert.ToDouble(dsPurchse.Tables[0].Rows[0][0].ToString());

                
            DataSet dsPaid = new DataSet();
            objBL.Query = "select sum(PaidAmount) from Payment_Supplier where SupplierId=" + cmbSupplierName.SelectedValue + " and BillFlag='" + BillFlag + "'";
            dsPaid = objBL.ReturnDataSet();
            if (dsPaid.Tables[0].Rows.Count > 0)
                CheckNullString = dsPaid.Tables[0].Rows[0][0].ToString();
            if (CheckNullString != null && CheckNullString != "")
                TotalPaidAmount = Convert.ToDouble(dsPaid.Tables[0].Rows[0][0].ToString());

            txtTotalPurchaseAmount.Text = TotalPurchaseAmount.ToString();
            txtTotalPaidAmount.Text = TotalPaidAmount.ToString();
            NetPendingAmount = TotalPurchaseAmount - TotalPaidAmount;
            txtNetPendingAmount.Text = (TotalPurchaseAmount - TotalPaidAmount).ToString();
        }

        protected bool Validation()
        {
            objEP.Clear();
            if (cmbSupplierName.SelectedIndex == -1)
            {
                objEP.SetError(cmbSupplierName, "Select Supplier Name");
                return true;
            }
            else if (txtBillNumber.Text == "")
            {
                objEP.SetError(txtBillNumber, "Insert Bill Number");
                return true;
            }
            else if (txtCurrentBillAmount.Text == "")
            {
                objEP.SetError(txtCurrentBillAmount, "Insert Current Bill Amount");
                return true;
            }
            else if (txtTotalAmount.Text == "")
            {
                objEP.SetError(txtTotalAmount, "Insert Total Amount");
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool ValidationPayment()
        {
            if (cmbSupplierName.SelectedIndex == -1)
            {
                objEP.SetError(cmbSupplierName, "Select Supplier Name");
                return true;
            }
            else if (txtNetPendingAmount.Text == "")
            {
                objEP.SetError(txtNetPendingAmount, "Insert Bill Number");
                return true;
            }
            else if (txtCurrentPaidAmount.Text == "")
            {
                objEP.SetError(txtCurrentPaidAmount, "Insert Current Bill Amount");
                return true;
            }
            else if (txtReceivedBy.Text == "")
            {
                objEP.SetError(txtReceivedBy, "Insert Total Amount");
                return true;
            }
            else if (txtRemarks.Text == "")
            {
                objEP.SetError(txtRemarks, "Insert Total Amount");
                return true;
            }

            else if (rbCheque.Checked == true)
            {
                if (txtBankName.Text == "")
                {
                    objEP.SetError(txtReceivedBy, "Insert Total Amount");
                    return true;
                }
                if (txtChequeNumber.Text == "")
                {
                    objEP.SetError(txtChequeNumber, "Insert Total Amount");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected void FillGrid()
        {
            try
            {
                string CheckFlag = "";
                dgvBill.DataSource = null;
                DataSet ds = new DataSet();

                if (BillFlag == "Fix")
                    CheckFlag = "Fix";
                else
                    CheckFlag = "UnFix";

                objBL.Query = "select BD.TableID,BD.SupplierId,S.SupplierName as 'Supplier Name',BD.TDate as 'Date',BD.BillNumber as 'Bill Number',BD.CurrentBillAmount as 'Current Bill Amount',BD.VATAmountPer as 'VAT Amount Per',BD.TotalAmount as 'Total Amount',BD.UserId,BD.UpdatedBy from Billdetails BD inner join Supplier S on S.ID=BD.SupplierId where BD.BillFlag='" + CheckFlag + "' and BD.SupplierId= " + cmbSupplierName.SelectedValue + "";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgvBill.DataSource = ds.Tables[0];
                    dgvBill.Columns[0].Visible = false;
                    dgvBill.Columns[1].Visible = false;
                    dgvBill.Columns[1].Width = 200;
                    dgvBill.Columns[2].Width = 200;
                    dgvBill.Columns[3].Width = 200;
                    dgvBill.Columns[4].Width = 200;
                    dgvBill.Columns[5].Width = 200;
                    dgvBill.Columns[6].Width = 200;
                    dgvBill.Columns[7].Width = 200;

                    if (TableID != 0 && rowIndex > 0)
                    {
                        dgvBill.Rows[0].Selected = false;
                        dgvBill.Rows[rowIndex].Selected = true;
                        ClearRowCountAll();
                    }
                    else
                    {
                        saveGridRow = dgvBill.Rows.Count - 1;
                        dgvBill.Rows[0].Selected = false;
                        dgvBill.Rows[saveGridRow].Selected = true;
                        ClearRowCountAll();
                    }
                }
                else
                {
                    dgvBill.DataSource = null;
                }
            }
            catch
            {
                objRL.ShowMessage(9, 9);
                return;
            }
        }

        protected void ClearRowCountAll()
        {
            saveGridRow = 0;
            rowIndex = 0;
        }

        protected void AmountZero()
        {
            TableID = 0;
            CalculationAmount = 0;
            VatAmount = 0;
            TotalAmount = 0;
        }

        protected void Clear_Paid_Save()
        {
            EnableTrue();
            objEP.Clear();
            AmountZero();
            txtTotalPurchaseAmount.Text = "0";
            txtTotalPaidAmount.Text = "0";
            txtBillNumber.Text = "";
            txtCurrentBillAmount.Text = "";
             
            txtTotalAmount.Text = "";
            txtBillNumber.Select();
        }

        protected void Clearfor_Combo()
        {
            TableID = 0;
            CalculationAmount = 0;
            VatAmount = 0;
            TotalAmount = 0;
            txtTotalPurchaseAmount.Text = "0";
            txtTotalPaidAmount.Text = "0";
            txtBillNumber.Text = "";
            txtCurrentBillAmount.Text = "";
            txtTotalAmount.Text = "";

            txtCurrentPaidAmount.Text = "";
            txtReceivedBy.Text = "";
            txtRemarks.Text = "";
            txtBankName.Text = "";
            txtChequeNumber.Text = "";
            txtCurrentPaidAmount.Text = "";
            txtAddress.Text = "";
            txtTotalPurchaseAmount.Text = "";
            txtTotalPaidAmount.Text = "";
            ClearValuesPurchasePaid();
        }

        private void cmbSupplierName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (rbFix.Checked != false || rbUnFix.Checked != false)
            {
                if (cmbSupplierName.SelectedIndex > -1)
                {
                    EnableTrue();
                    Clearfor_Combo();
                    Calculate_Purchase_Paid();
                    FillGrid();
                    FillGrid_Paid();
                    tcBillDetails.Enabled = true;
                    if (NetPendingAmount == 0)
                        EnableFalse();
                    else
                        EnableTrue();
                }
            }
            else
            {
                txtTotalPurchaseAmount.Text = "";
                txtTotalPaidAmount.Text = "";
                cmbSupplierName.SelectedIndex = -1;
                objRL.ShowMessage(45, 9);
            }
        }

        protected void BillEnableTrue()
        {
            btnDeleteBill.Enabled = false;
            //DeleteFlag = false;
            dtpDate.Enabled = true;
            txtBillNumber.Enabled = true;
            txtCurrentBillAmount.Enabled = true;
            txtTotalAmount.Enabled = true;
            txtTotalPurchaseAmount.Enabled = true;
        }

        protected void BillEnableFalse()
        {
            btnDeleteBill.Enabled = true;
            //DeleteFlag = true;
            dtpDate.Enabled = false;
            txtBillNumber.Enabled = false;
            txtCurrentBillAmount.Enabled = false;
            txtTotalAmount.Enabled = false;
            txtTotalPurchaseAmount.Enabled = false;
        }


        static double GridPendingAmount;

        protected void ClearPayment()
        {
            objEP.Clear();
            AmountZero();
            txtCurrentPaidAmount.Text = "";
            txtReceivedBy.Text = "";
            txtRemarks.Text = "";
            txtBankName.Text = "";
            txtChequeNumber.Text = "";
            txtCurrentPaidAmount.Text = "";
            btnSaveBill.Enabled = true;
            DeleteFlag = false;
            txtCurrentPaidAmount.Focus();
        }

        protected void ClearBill()
        {
            objEP.Clear();
            AmountZero();
            btnDeleteBill.Enabled = false;
            DeleteFlag = false;
            txtBillNumber.Text = "";
            txtCurrentBillAmount.Text = "";
            txtTotalAmount.Text = "";
            BillEnableTrue();
            txtBillNumber.Select();
        }

        protected void FillGrid_Paid()
        {
            try
            {
                string CheckFlag = "";

                dgvPayment.DataSource = null;
                DataSet ds = new DataSet();
                Para.Clear();
                Para.Add(cmbSupplierName.SelectedValue.ToString());

                if (BillFlag == "Fix")
                    CheckFlag = "Fix";
                else
                    CheckFlag = "UnFix";

                objBL.Query = "select P.Id,P.SupplierId,S.SupplierName as 'Supplier Name',P.PaymentDate as 'Payment Date',P.PaidAmount as 'Paid Amount',P.ReceivedBy as 'Received By',P.Remark,P.PaymentMode as 'Payment Mode',P.BankName as 'Bank Name',P.ChequeDate as 'Cheque Date',P.ChequeNumber as 'Cheque Number',P.BillFlag as 'Bill Flag',P.UserId,P.UpdatedBy from Payment_Supplier P inner join Supplier S on S.ID=P.SupplierId where P.SupplierId=" + cmbSupplierName.SelectedValue + " and P.BillFlag='" + CheckFlag + "'";

                ds = objBL.ReturnDataSet();

                //ds = ConnectionClass.ExecuteStoredProcedureWithParameters(Para, "SP_FillGrid_Payment");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgvPayment.DataSource = ds.Tables[0];
                    dgvPayment.Columns[0].Visible = false;
                    dgvPayment.Columns[1].Visible = false;
                    dgvPayment.Columns[2].Width = 200;
                    dgvPayment.Columns[3].Width = 200;
                    dgvPayment.Columns[4].Width = 200;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tpAddBill.Text == "Add Bill Details")
                FillGrid();
            else
                FillGrid_Paid();
        }

        private void txtCurrentBillAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtCurrentBillAmount.Text != "")
                CurrentAmount = Convert.ToDouble(txtCurrentBillAmount.Text);
            else
                CurrentAmount = 0;

            Calculate_TotalAmount();
        }

        private void txtCurrentBillAmount_Leave(object sender, EventArgs e)
        {
            if (txtCurrentBillAmount.Text != "")
                CurrentAmount = Convert.ToDouble(txtCurrentBillAmount.Text);
            else
                CurrentAmount = 0;

            Calculate_TotalAmount();
        }

        double VatAmount = 0, CurrentAmount = 0, TotalAmount = 0;

        private void txtVatAmount_Leave(object sender, EventArgs e)
        {
            if (txtCurrentBillAmount.Text != "")
                Calculate_TotalAmount();
            else
                txtTotalAmount.Text = "";
        }

        protected void Calculate_TotalAmount()
        {
            TotalAmount = CurrentAmount + VatAmount;
            txtTotalAmount.Text = TotalAmount.ToString();
        }

        double CalculationAmount = 0;
        bool DeleteFlag = false;

        protected void Cancel_AddBill()
        {
            dgvBill.DataSource = null;
            cmbSupplierName.SelectedIndex = -1;
            cmbSupplierName.Focus();
        }

        private void txtVatAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtCurrentBillAmount.Text != "")
            {
                VatAmount = 0;
                Calculate_TotalAmount();
            }
            else
                txtTotalAmount.Text = "";
        }

        private void rbCheque_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCheque.Checked == true)
            {
                PaymentMode = "Cheque";
                rbCash.Checked = false;
                gbChequeDetails.Enabled = true;
                txtChequeNumber.Enabled = true;
                dtpChequeDate.Enabled = true;
                txtBankName.Enabled = true;
            }
            else
            {
                PaymentMode = "Cash";
                rbCash.Checked = true;
                gbChequeDetails.Enabled = false;
            }
        }

        protected void Clear_Payment()
        {
            rbUnFix.Checked = false;
            rbFix.Checked = false;
            txtAddress.Text = "";
            txtTotalPurchaseAmount.Text = "";
            txtTotalPaidAmount.Text = "";
            txtCurrentPaidAmount.Text = "";
            txtReceivedBy.Text = "";
            txtRemarks.Text = "";
            txtBankName.Text = "";
            txtChequeNumber.Text = "";
            txtAddress.Text = "";
        }

        string BillFlag = "";

        private void rbFix_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFix.Checked == true)
            {
                CancelAll();
                cmbSupplierName.Enabled = true;
                BillFlag = "Fix";
                rbUnFix.Checked = false;
            }
            else
            {
                CancelAll();
                cmbSupplierName.Enabled = true;
                BillFlag = "UnFix";
                rbUnFix.Checked = true;
            }
        }

        private void rbUnFix_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUnFix.Checked == true)
            {
                CancelAll();
                cmbSupplierName.Enabled = true;
                BillFlag = "UnFix";
                rbFix.Checked = false;
            }
            else
            {
                CancelAll();
                cmbSupplierName.Enabled = true;
                BillFlag = "Fix";
                rbFix.Checked = true;
            }
        }

        protected void Clear_TabValues()
        {
            TableID = 0;
            if (cmbSupplierName.SelectedIndex == -1)
            {
                dgvBill.DataSource = null;
                dgvPayment.DataSource = null;
            }
        }
        private void tcBillDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            Calculate_Purchase_Paid();

            if (tcBillDetails.SelectedIndex == 0)
                ClearBill();
            else
            {
                ClearPayment();
                if (NetPendingAmount == 0)
                    EnableFalse();
                else
                    EnableTrue();
            }
        }

        private void txtCurrentBillAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtCurrentBillAmount);
        }

        private void txtVatAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtCurrentBillAmount);
        }

        private void txtCurrentPaidAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.FloatValue(sender, e, txtCurrentBillAmount);
        }

        string PaymentMode = "";

        private void rbCash_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCash.Checked == true)
            {
                PaymentMode = "Cash";
                rbCheque.Checked = false;
                gbChequeDetails.Enabled = false;
            }
            else
            {
                PaymentMode = "Cheque";
                rbCheque.Checked = true;
                gbChequeDetails.Enabled = true;
                txtChequeNumber.Enabled = true;
                dtpChequeDate.Enabled = true;
                txtBankName.Enabled = true;
            }
        }

        double CurrentPaidAmount_Grid = 0;

        protected void EnableFalse()
        {
            btnDeleteBill.Enabled = true;
            //DeleteFlag = true;
            dtpDate.Enabled = false;
            txtCurrentPaidAmount.Enabled = false;
            txtReceivedBy.Enabled = false;
            txtRemarks.Enabled = false;
            rbCash.Enabled = false;
            rbCheque.Enabled = false;
            gbChequeDetails.Enabled = false;
            txtBankName.Enabled = false;
            dtpChequeDate.Enabled = false;
            txtChequeNumber.Enabled = false;
        }

        protected void EnableTrue()
        {
            btnDeleteBill.Enabled = false;
            //DeleteFlag = false;
            dtpDate.Enabled = true;
            txtCurrentPaidAmount.Enabled = true;
            txtReceivedBy.Enabled = true;
            txtRemarks.Enabled = true;
            rbCash.Enabled = true;
            rbCheque.Enabled = true;
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            mString = objRL.MessageString(26);
            captionMessage = objRL.MessageString(2);
            dr = MessageBox.Show(mString, captionMessage, MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                this.Dispose();
        }

        private void txtCurrentPaidAmount_Leave(object sender, EventArgs e)
        {
            Calculate_Purchase_Paid();
        }

        string DeleteString = "";

        protected void Delete_Query()
        {
            if (DeleteString == "Payment")
                objBL.Query = "Delete from Payment_Supplier where Id=" + TableID + "";
            else
                objBL.Query = "Delete from BillDetails where TableId=" + TableID + "";

            objBL.Function_ExecuteNonQuery();
        }


        protected void CancelAll()
        {
            AmountZero();
            ClearBill();
            ClearPayment();
            cmbSupplierName.SelectedIndex = -1;
            txtAddress.Text = "";
            txtTotalPurchaseAmount.Text = "";
            txtTotalPaidAmount.Text = "";
            dgvBill.DataSource = null;
            dgvPayment.DataSource = null;
            tcBillDetails.Enabled = false;
             
            BillFlag = "Fix";
            rbFix.Checked = true;
        }

        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                txtCurrentBillAmount.Select();
        }

        private void txtCurrentBillAmount_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void txtVatAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                btnSaveBill.Select();
        }

        private void txtCurrentPaidAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                txtReceivedBy.Select();
        }

        private void txtReceivedBy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                txtRemarks.Select();
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                rbCash.Select();
        }

        private void rbCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (rbCash.Checked == true)
            {
                if (e.KeyValue.ToString() == "13")
                    btnSaveBill.Select();
            }
            else
            {
                if (e.KeyValue.ToString() == "13")
                    rbCheque.Select();
            }
        }

        private void rbCheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (rbCheque.Checked == true)
            {
                if (e.KeyValue.ToString() == "13")
                    txtBankName.Select();
            }
            else
            {
                if (e.KeyValue.ToString() == "13")
                    btnSaveBill.Select();
            }
        }

        private void txtBankName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                dtpChequeDate.Select();
        }

        private void dtpChequeDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                txtChequeNumber.Select();
        }

        private void txtChequeNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                btnSaveBill.Select();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelAll();
        }

        private void btnNewBill_Click(object sender, EventArgs e)
        {
            Calculate_Purchase_Paid();
            ClearBill();
        }

        private void btnNewPayment_Click_1(object sender, EventArgs e)
        {
            Calculate_Purchase_Paid();
            if (NetPendingAmount != 0)
            {
                EnableTrue();
                ClearPayment();
            }
        }

        private void btnDeleteBill_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                DialogResult dr;
                mString = objRL.MessageString(15);
                captionMessage = objRL.MessageString(16);
                dr = MessageBox.Show(mString, captionMessage, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    DeleteString = "Bill";
                    DeleteFlag = true;
                    Delete_Query();
                    Calculate_Purchase_Paid();
                    ClearBill();
                    FillGrid();
                }
            }
        }


        private void rbFix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                rbUnFix.Select();
        }

        private void rbUnFix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                dtpDate.Select();
        }

        private void cmbSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                txtAddress.Select();
        }

        private void txtPendingAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")
                txtAddress.Select();
        }

        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            // Para.Clear();
            DeleteFlag = false;

            int VatAmount = 0;

            if (Validation() == false)
            {
                if (TableID != 0)

                    objBL.Query = "update Billdetails set SupplierId=" + cmbSupplierName.SelectedValue + ",TDate='" + dtpDate.Value.ToShortDateString() + "',BillNumber='" + txtBillNumber.Text + "',CurrentBillAmount=" + txtCurrentBillAmount.Text + ",VATAmountPer=" + VatAmount + ",TotalAmount=" + txtTotalAmount.Text + ",BillFlag='" + BillFlag + "',UpdatedBy=" + BusinessLayer.UserId_Static + " where TableID=" + TableID + "";
                else
                    objBL.Query = "insert into Billdetails(SupplierId,TDate,BillNumber,CurrentBillAmount,VATAmountPer,TotalAmount,BillFlag,UserId) values(" + cmbSupplierName.SelectedValue + ",'" + dtpDate.Value.ToShortDateString() + "','" + txtBillNumber.Text + "'," + txtCurrentBillAmount.Text + "," + VatAmount + "," + txtTotalAmount.Text + ",'" + BillFlag + "'," + BusinessLayer.UserId_Static + ")";

                objBL.Function_ExecuteNonQuery();
                Calculate_Purchase_Paid();
                objRL.ShowMessage(7, 1);
                FillGrid();
                ClearBill();
            }
            else
            {
                objRL.ShowMessage(38, 9);
                return;
            }
        }

        private void btnSavePayment_Click(object sender, EventArgs e)
        {
            if (ValidationPayment() == false)
            {
                string BankName = "", CheckNumber = "";

                if (PaymentMode == "Cheque")
                {
                    BankName = txtBankName.Text;
                    CheckNumber = txtChequeNumber.Text;
                }
                else
                {
                    BankName = "-";
                    CheckNumber = "-";
                }

                if (TableID != 0)
                    objBL.Query = "update Payment_Supplier set SupplierId=" + cmbSupplierName.SelectedValue + ",PaymentDate='" + dtpDate.Value.ToShortDateString() + "',PaidAmount=" + txtCurrentPaidAmount.Text + ",ReceivedBy='" + txtReceivedBy.Text + "',Remark='" + txtRemarks.Text + "',PaymentMode='" + PaymentMode + "',BankName='" + BankName + "',ChequeDate='" + dtpChequeDate.Value.ToShortDateString() + "',ChequeNumber='" + CheckNumber + "',BillFlag='" + BillFlag + "',UpdatedBy=" + BusinessLayer.UserId_Static + " where Id=" + TableID + "";
                else
                    objBL.Query = "insert into Payment_Supplier(SupplierId,PaymentDate,PaidAmount,ReceivedBy,Remark,PaymentMode,BankName,ChequeDate,ChequeNumber,BillFlag,UserId) values(" + cmbSupplierName.SelectedValue + ",'" + dtpDate.Value.ToShortDateString() + "'," + txtCurrentPaidAmount.Text + ",'" + txtReceivedBy.Text + "','" + txtRemarks.Text + "','" + PaymentMode + "','" + BankName + "','" + dtpChequeDate.Value.ToShortDateString() + "','" + CheckNumber + "','" + BillFlag + "'," + BusinessLayer.UserId_Static + ")";

                objBL.Function_ExecuteNonQuery();
                Calculate_Purchase_Paid();
                objRL.ShowMessage(7, 1);
                FillGrid_Paid();
                ClearPayment();
            }
        }

        private void btnDeletePayment_Click(object sender, EventArgs e)
        {
            if (ValidationPayment() == false)
            {
                DialogResult dr;
                mString = objRL.MessageString(15);
                captionMessage = objRL.MessageString(16);
                dr = MessageBox.Show(mString, captionMessage, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    DeleteString = "Payment";
                    DeleteFlag = true;
                    Delete_Query();
                    Calculate_Purchase_Paid();
                    FillGrid_Paid();
                    ClearPayment();
                    Calculate_Purchase_Paid();

                }
            }
        }

        private void dgvPayment_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EnableFalse();
            TableID = Convert.ToInt32(dgvPayment.Rows[e.RowIndex].Cells[0].Value.ToString());
            cmbSupplierName.Text = dgvPayment.Rows[e.RowIndex].Cells[2].Value.ToString();
            dtpDate.Value = Convert.ToDateTime(dgvPayment.Rows[e.RowIndex].Cells[3].Value.ToString());
            txtCurrentPaidAmount.Text = dgvPayment.Rows[e.RowIndex].Cells[4].Value.ToString();
            CurrentPaidAmount_Grid = Convert.ToDouble(dgvPayment.Rows[e.RowIndex].Cells[4].Value.ToString());
            txtReceivedBy.Text = dgvPayment.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtRemarks.Text = dgvPayment.Rows[e.RowIndex].Cells[6].Value.ToString();

            if (dgvPayment.Rows[e.RowIndex].Cells[7].Value.ToString() == "Cash")
                rbCash.Checked = true;
            else
            {
                rbCheque.Checked = true;
                txtBankName.Text = dgvPayment.Rows[e.RowIndex].Cells[8].Value.ToString();
                dtpChequeDate.Value = Convert.ToDateTime(dgvPayment.Rows[e.RowIndex].Cells[9].Value.ToString());
                txtChequeNumber.Text = dgvPayment.Rows[e.RowIndex].Cells[10].Value.ToString();
            }
            DeleteFlag = true;
        }

        private void dgvBill_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BillEnableFalse();
            TableID = Convert.ToInt32(dgvBill.Rows[e.RowIndex].Cells[0].Value.ToString());
            dtpDate.Value = Convert.ToDateTime(dgvBill.Rows[e.RowIndex].Cells[3].Value.ToString());
            txtBillNumber.Text = Convert.ToString(dgvBill.Rows[e.RowIndex].Cells[4].Value.ToString());
            txtCurrentBillAmount.Text = Convert.ToString(dgvBill.Rows[e.RowIndex].Cells[5].Value.ToString());
            txtTotalAmount.Text = Convert.ToString(dgvBill.Rows[e.RowIndex].Cells[7].Value.ToString());
            GridPendingAmount = Convert.ToDouble(dgvBill.Rows[e.RowIndex].Cells[7].Value.ToString());
            txtTotalPurchaseAmount.Text = NetPendingAmount.ToString();
        }
    }
}

