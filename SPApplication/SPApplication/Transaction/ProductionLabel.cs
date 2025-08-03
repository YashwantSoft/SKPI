using BusinessLayerUtility;
using SPApplication.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Drawing.Imaging;
using System.IO;
using SPApplication.Transaction;

namespace SPApplication.Master
{
    public partial class ProductionLabel : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;

        bool BorderFlag = false; bool CellFlag = false;
        string PDFReport = string.Empty;
        int AFlag = 0; int SrNo = 1;
        bool AlignFlag = false; bool boldflag = false;
        int RowCount = 18;
        bool MH_Value = false;
        string ReportName = string.Empty;

        Excel.Application myExcelApp;
        Excel.Workbooks myExcelWorkbooks;
        Excel.Workbook myExcelWorkbook;

        public ProductionLabel()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_PRODUCTIONLABEL);
            btnSearch.Text = BusinessResources.BTN_SEARCH;
            btnSearch.BackColor = objDL.GetBackgroundColor();
            btnSearch.ForeColor = objDL.GetForeColorError();
            btnPrint.BackColor = objDL.GetBackgroundColor();
            btnPrint.ForeColor = objDL.GetForeColorError();
            objDL.SetPlusButtonDesign(btnAddMachine);
            objDL.SetPlusButtonDesign(btnAddProductMaster);
            objRL.Fill_ProductName(cmbProductName);
            objRL.FillPreformParty(cmbPreformParty);
            lblBottleInformation.BackColor = Color.Cyan;
            lblJarInformation.BackColor = Color.Yellow;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        string QRCodeData = string.Empty;
        string QRCodeDataRTB = string.Empty;
        string NeckHeightWeightQR = string.Empty;

        int Day = 0, Year = 0;
        string YearS = string.Empty;
        string curYear = string.Empty;

        string B1 = string.Empty, B2 = string.Empty, B3 = string.Empty, B4 = string.Empty, B5 = string.Empty;

        string QRImagePath = string.Empty;

        string Shift = string.Empty;

        private static int ShiftId_Form;

        private void ShiftCode()
        {
            ShiftId_Form = 0;
            //objRL.GetShift();
            Shift = objRL.ShiftCode();
            txtShift.Text = Shift;// Shift.ToString();
            dtpTime.CustomFormat = "HH:mm";

            if (txtShift.Text == "")
            {
                btnAddShift.Visible = true;
            }
            else
            {
                ShiftId_Form = objRL.ShiftId;
                btnAddShift.Visible = false;
            }
        }

        bool ShiftFlag = false;

        private void GetInformation()
        {
            if (!Validation())
            {
                if (!CheckExistData())
                {
                    // ShiftCode();

                    string Information = string.Empty;
                    B1 = string.Empty;
                    B2 = string.Empty;
                    B3 = string.Empty;
                    B4 = string.Empty;
                    B5 = string.Empty;
                    QRImagePath = string.Empty;

                    QRCodeData = string.Empty;

                    if (ShiftFlag)
                        Day = DateTime.Now.DayOfYear - 1;
                    else
                        Day = DateTime.Now.DayOfYear;

                    Year = Convert.ToInt32(DateTime.Now.Date.Year);
                    YearS = Year.ToString();
                    curYear = Year.ToString().Substring(2, 2).ToString();

                    // B1 = NeckHeightWeightQR;
                    B1 = lblProductName.Text;
                    B2 = ",Qty-" + txtQty.Text;
                    //B3 = ",Batch-" + Day.ToString() + "/" + curYear.ToString() + "/" + Shift;
                    //B3 = ",Batch-"+ curYear.ToString() + "/" + Day.ToString() + "/" + Shift + "/M-" +cmbMachineNo.Text ;
                    //B3 = ",BNo-" + curYear.ToString() + Day.ToString() + Shift + cmbMachineNo.Text + "M" + txtID.Text;

                    //Changes on 07-06-2025 as per Ajay Sir Requirements
                    B3 = ","+Shift + "-" + curYear.ToString() + Day.ToString() + "-" + cmbMachineNo.Text + "M" + txtID.Text;

                    if (cmbDate.SelectedIndex > -1 && cmbDate.Text == "Yes")
                    {
                        B4 = ",Date-" + DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    }
                    else
                        B4 = "";

                    if (txtPurchaseOrderNo.Text != "")
                    {
                        B5 = ",PO No.-" + txtPurchaseOrderNo.Text;
                    }
                    else
                        B5 = "";

                    //B1 = NeckHeightWeightQR;
                    //B2 = ",Product - " + cmbProductName.Text;
                    //B3 = ",Qty-" + txtQty.Text;
                    //B4 = ",Batch-" + txtID.Text + "/" + Day.ToString() + "/" + curYear.ToString() + "/" + cmbShiftNo.Text;
                    //QRCodeData = NeckHeightWeightQR + ",Product-" + cmbProductName.Text + ",Qty-" + txtQty.Text + ",Batch-" + txtID.Text + "/" + Day.ToString() + "/"  + curYear.ToString() + "/" + cmbShiftNo.Text;

                    QRCodeData = B1 + B2 + B3;// + B4 + B5;
                    //QRCodeData = B1 + B2 + B3;// + B4;

                    //QRCodeDataRTB = NeckHeightWeightQR + "\nProduct-" + cmbProductName.Text + "\nQty-" + txtQty.Text + "\nBatch-" + txtID.Text + "/" + Day.ToString() + "/" + curYear.ToString() + "/" + cmbShiftNo.Text;
                    //Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                    //pbQRCode.Image = barcode.Draw(Information.ToString(), 50);

                    Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                    pbQRCode.Image = qrcode.Draw(QRCodeData.ToString(), 10);
                    QRImagePath = objRL.GetPath("ImagePath");
                    var filePath = QRImagePath;
                    Directory.CreateDirectory(filePath);
                    string FileName = txtID.Text.ToString();
                    pbQRCode.Image.Save(Path.Combine(filePath, FileName), System.Drawing.Imaging.ImageFormat.Png);

                    //Fill Rich Text Box

                    B1 = B1.Replace(",", "");
                    B2 = B2.Replace(",", "");
                    B3 = B3.Replace(",", "");
                    B4 = B4.Replace(",", "");
                    B5 = B5.Replace(",", "");
                    QRCodeDataRTB = B1 + "\n" + B2 + "\n" + B3 + "\n" + B4 + "\n" + B5;
                    rtbStickerHeader.Text = QRCodeDataRTB.ToString();

                    SaveDB();

                    btnPrint.Visible = true;
                    //GetReport();

                    //Working Report

                    //GetReportSingle();

                    //GetReportSingleNew();
                    //NewPrintReport();

                    //ClearAll();
                    FillGrid();


                    //var filePath = @"D:\Images\";
                    //Directory.CreateDirectory(filePath);
                    //string FileName = txtQty.Text.ToString();
                    //pbQRCode.Image.Save(Path.Combine(filePath, FileName), System.Drawing.Imaging.ImageFormat.Png);
                    ////pbQRCode.Image.Save(Path.Combine(filePath, $"'{pbQRCode.Image}'"), System.Drawing.Imaging.ImageFormat.Jpeg);
                    ////pbQRCode.Image.Save("D\\Image\\Q1", ImageFormat.Jpeg);
                    //GetReport();
                }
                else
                {
                    objRL.ShowMessage(41, 4);
                    return;
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        string AposValue = string.Empty;

        int FromRange = 0, ToRange = 0, Result = 0, UsedSticker = 0;

        protected void SaveDB()
        {
            AposValue = rtbStickerHeader.Text;
            FromRange = 0; ToRange = 0; Result = 0;

            FromRange = Convert.ToInt32(objRL.Check_Null_Integer(Convert.ToString(txtFrom.Text)));
            ToRange = Convert.ToInt32(objRL.Check_Null_Integer(Convert.ToString(txtTo.Text)));
            UsedSticker = Convert.ToInt32(objRL.Check_Null_Integer(Convert.ToString(txtUsedSticker.Text)));

            if (TableID != 0)
                if (FlagDelete)
                    objBL.Query = "update ProductionEntry set CancelTag=1 where ID=" + TableID + "";
                else
                    objBL.Query = "update ProductionEntry set EntryDate='" + dtpDate.Value.ToShortDateString() + "',EntryTime='" + dtpTime.Value.ToShortTimeString() + "',Shift='" + txtShift.Text + "',MachinNo='" + cmbMachineNo.Text + "',ProductId=" + ProductId + ",PurchaseOrderNo='" + txtPurchaseOrderNo.Text + "',ProductQty='" + txtQty.Text + "',StickerHeader='" + AposValue.Replace("'", "''") + "',DateFlag='" + cmbDate.Text + "',PreformPartyId=" + cmbPreformParty.SelectedValue + ",FromRange=" + FromRange + ",ToRange=" + ToRange + ",UsedSticker=" + UsedSticker + ",ShiftId=" + ShiftId_Form + ",UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
            else
                objBL.Query = "insert into ProductionEntry(EntryDate,EntryTime,Shift,MachinNo,ProductId,PurchaseOrderNo,ProductQty,StickerHeader,DateFlag,PreformPartyId,FromRange,ToRange,ShiftId,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "','" + txtShift.Text + "','" + cmbMachineNo.Text + "'," + ProductId + ",'" + txtPurchaseOrderNo.Text + "','" + txtQty.Text + "','" + AposValue.Replace("'", "''") + "','" + cmbDate.Text + "'," + cmbPreformParty.SelectedValue + "," + FromRange + "," + ToRange + "," + ShiftId_Form + "," + BusinessLayer.UserId_Static + ")";

            Result = objBL.Function_ExecuteNonQuery();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            GetInformation();
        }

        protected bool Validation()
        {
            objEP.Clear();

            if (ShiftId_Form == 0)
            {
                objEP.SetError(txtShift, "Enter Shift");
                txtShift.Focus();
                return true;
            }
            else if (cmbMachineNo.SelectedIndex == -1)
            {
                objEP.SetError(cmbMachineNo, "Select Machine No");
                cmbMachineNo.Focus();
                return true;
            }
            else if (ProductId == 0)
            {
                objEP.SetError(txtSearchProductName, "Enter Search Product Name");
                objEP.SetError(lbItem, "Select Product Name");
                lbItem.Focus();
                return true;
            }
            else if (txtQty.Text == "")
            {
                objEP.SetError(txtQty, "Enter Qty");
                txtQty.Focus();
                return true;
            }
            else if (txtShift.Text == "")
            {
                objEP.SetError(txtShift, "Enter Shift");
                txtQty.Focus();
                return true;
            }
            //else if (rtbStickerHeader.Text == "")
            //{
            //    objEP.SetError(rtbStickerHeader, "Enter Sticker Qty");
            //    rtbStickerHeader.Focus();
            //    return true;
            //}
            else if (lblProductName.Text == "")
            {
                objEP.SetError(lblProductName, "Enter Product Name");
                lbItem.Focus();
                return true;
            }
            else if (cmbPreformParty.SelectedIndex == -1)
            {
                objEP.SetError(cmbPreformParty, "Select Preform Party");
                cmbPreformParty.Focus();
                return true;
            }
            return false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                DialogResult dr;
                dr = objRL.Delete_Record_Show_Message();

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

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;

        protected void FillGrid()
        {
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;
            UserClause = string.Empty;

            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (BusinessLayer.UserName_Static != BusinessResources.USER_ADMIN)
                UserClause = " and PE.UserId = " + BusinessLayer.UserId_Static;
            else
                UserClause = string.Empty;

            MainQuery = "select PE.ID,PE.EntryDate as [Date],PE.EntryTime as [Time],PE.Shift,PE.MachinNo as [Machine No],PE.ProductId,P.ProductName,PE.PurchaseOrderNo as [PO Number],PE.ProductQty as [Bag Qty],PE.StickerHeader as [Sticker Header],PE.DateFlag,PE.PreformPartyId,PPM.PreformParty as [Preform Party],FromRange,ToRange,UsedSticker as [Used Sticker],ShiftId from ((ProductionEntry PE inner join Product P on P.ID=PE.ProductId) inner join PreformPartyMaster PPM on PPM.ID=PE.PreformPartyId) where PE.CancelTag=0 and P.CancelTag=0 and PPM.CancelTag=0 ";
            OrderByClause = " order by PE.ID desc";

            if (DateFlag)
                WhereClause = " and PE.EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            else if (SearchTag)
                WhereClause = " and P.ProductName like '%" + txtSearch.Text + "%'";
            else if (IDFlag)
                WhereClause = " and PE.ID=" + txtSearchID.Text + "";
            else
                WhereClause = string.Empty;

            objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {

                //0 ID,
                //1 EntryDate as [Date],
                //2 EntryTime as [Time],
                //3 Shift,
                //4 MachinNo as [Machine No],
                //5 ProductId,
                //6 ProductName as [Product Name],
                //7 PurchaseOrderNo as [PO Number],
                //8 ProdutQty as [Bag Qty],
                //9 StickerHeader as [Sticker Header],
                //10 DateFlag
                //11 ,PE.PreformPartyId
                //12 PreformParty as [Preform Party
                //13 FromRange [From],
                //14 ToRange as [To]
                //15 UsedSticker as [Used Sticker]

                //PE.ID,PE.EntryDate as [Date],PE.EntryTime as [Time],PE.Shift,PE.MachinNo as [Machine No],PE.ProductId,P.ProductName,PE.PurchaseOrderNo as [PO Number],PE.ProductQty as [Bag Qty],PE.StickerHeader as [Sticker Header],PE.DateFlag,PPM.PreformParty as [Preform Party]

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[4].Width = 110;
                dataGridView1.Columns[6].Width = 600;
                dataGridView1.Columns[7].Width = 100;
                dataGridView1.Columns[8].Width = 80;
                dataGridView1.Columns[9].Width = 400;
                dataGridView1.Columns[12].Width = 150;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        private void btnAddProductMaster_Click(object sender, EventArgs e)
        {
            Product objForm = new Product();
            objForm.ShowDialog(this);
            //objRL.Fill_Item(cmbProductName);
            txtSearchProductName.Text = "";
            txtSearchProductName.Focus();
        }

        private void cmbProductName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Fill_Product_Information();
        }

        int ProductId = 0; string ProductDetails = string.Empty;
        string Standard = string.Empty;

        private void Fill_Product_Information()
        {
            if (TableID == 0)
                ProductId = Convert.ToInt32(lbItem.SelectedValue);

            if (ProductId != 0)
            {
                lblProductName.Text = "";
                lblProductType.Text = "";
                ProductDetails = string.Empty;

                objRL.Get_Product_Records_By_Id(ProductId);
                //ProductDetails = string.Empty;
                Standard = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(objRL.Standard)))
                    Standard = objRL.Standard;

                //NeckHeightWeightQR = "Neck/Weight-" + objRL.ProductNeckSize.ToString() + "mm/" + Standard + "/" + objRL.ProductWeight;
                //ProductDetails = "Product-\t" + objRL.ProductName.ToString() + "\n" +
                //                    "Party-\t" + objRL.Party.ToString() + "\n" + NeckHeightWeightQR;
                ////"Neck/Height/Weight-" + objRL.ProductNeckSize.ToString() + "mm/" + objRL.ProductHeight.ToString() + "/" + objRL.ProductWeight;

                ProductId = Convert.ToInt32(objRL.ProductId);
                lblProductName.Text = objRL.ProductName.ToString();
                lblProductType.Text = objRL.ProductType.ToString();

                if (objRL.ProductType == "Bottle")
                {
                    lblProductName.BackColor = Color.Cyan;
                    lblProductType.BackColor = Color.Cyan;
                }
                else
                {
                    lblProductName.BackColor = Color.Yellow;
                    lblProductType.BackColor = Color.Yellow;
                }

                if (!string.IsNullOrEmpty(objRL.Qty))
                {
                    txtQty.Text = objRL.Qty.ToString();
                    txtPurchaseOrderNo.Focus();
                }
                else
                {
                    txtQty.Text = "";
                    txtQty.Focus();
                }
            }
        }

        private void cmbMachineNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_Machine_Information();
        }

        private void Fill_Machine_Information()
        {
            if (cmbMachineNo.SelectedIndex > -1)
            {
                int MNo = Convert.ToInt32(cmbMachineNo.Text);

                if (MNo == 1 || MNo == 2 || MNo == 3 || MNo == 4 || MNo == 5 || MNo == 6 || MNo == 7 || MNo == 8 || MNo == 9)
                    txtMachineDetails.Text = cmbMachineNo.Text + "-Manual";
                else
                    txtMachineDetails.Text = cmbMachineNo.Text + "-Automatic";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        bool FlagToday = false;
        private void cbToday_CheckedChanged(object sender, EventArgs e)
        {
            if (cbToday.Checked == true)
            {
                DateFlag = true;
                FlagToday = true;
                dtpFromDate.Value = DateTime.Now.Date;
                dtpToDate.Value = DateTime.Now.Date;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
            }
            else
            {

                FlagToday = false;
                dtpFromDate.Value = DateTime.Now.Date;
                dtpToDate.Value = DateTime.Now.Date;
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
            }
        }

        private void pbQRCode_Paint(object sender, PaintEventArgs e)
        {
            //using (System.Drawing.Font myFont = new System.Drawing.Font("Calibri", 12))
            //{
            //    e.Graphics.DrawString(QRCodeDataRTB, myFont, Brushes.Black, new System.Drawing.Point(190, 2));
            //}
        }

        private void txtSearchProductName_TextChanged(object sender, EventArgs e)
        {
            ClearAll_Item();
            if (txtSearchProductName.Text != "")
            {
                objRL.Fill_Item_ListBox(lbItem, txtSearchProductName.Text, "Text");
                //lbItem.Focus();
            }
            else
            {
                lbItem.Visible = true;
                objRL.Fill_Item_ListBox(lbItem, txtSearchProductName.Text, "All");
            }

        }

        private void ClearAll_Item()
        {
            ProductId = 0;
            lblProductType.Text = "";
            lblProductName.Text = "";
        }

        private void lbItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Fill_Product_Information();
                //txtQty.Focus();
            }
        }

        private void lbItem_Click(object sender, EventArgs e)
        {
            Fill_Product_Information();
        }

        private void txtSearchProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearchProductName.Text != "" && lbItem.Items.Count > 0)
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                {
                    lbItem.SelectedIndex = 0;
                    lbItem.Focus();
                }
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtQty);
        }

        private void BarcodeTest_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            objRL.Fill_Item_ListBox(lbItem, txtSearchProductName.Text, "All");

            GetID();
            ShiftCode();
            cmbDate.Text = "No";
            cmbMachineNo.Focus();
        }

        private bool CheckExistData()
        {
            bool ReturnFlag = false;

            DataSet ds = new DataSet();
            //objBL.Query = "select * from ProductionEntry where CancelTag=0 and EntryDate=#" + dtpDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and Shift='" + txtShift.Text + "' and MachinNo='" + cmbMachineNo.Text + "' and ProductId=" + ProductId + " and ID <> " + TableID + "";
            objBL.Query = "select * from ProductionEntry where CancelTag=0 and ShiftId=" + ShiftId_Form + " and MachinNo='" + cmbMachineNo.Text + "' and ProductId=" + ProductId + " and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                ReturnFlag = true;
            else
                ReturnFlag = false;

            return ReturnFlag;
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            GetInformation();
        }

        bool SearchTag = false;

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
                    //3 Shift,
                    //4 MachinNo as [Machine No],
                    //5 ProductId,
                    //6 ProductName as [Product Name],
                    //7 PurchaseOrderNo as [PO Number],
                    //8 ProdutQty as [Bag Qty],
                    //9 StickerHeader as [Sticker Header],
                    //10 DateFlag
                    //11 ,PE.PreformPartyId
                    //12 PreformParty as [Preform Party
                    //13 FromRange [From],
                    //14 ToRange as [To]
                    //15 Used Sticker
                    //16 ShiftID

                    //btnPrint.Visible = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    txtID.Text = TableID.ToString();
                    dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    txtShift.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cmbMachineNo.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    Fill_Machine_Information();
                    ProductId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                    Fill_Product_Information();
                    lblProductName.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtPurchaseOrderNo.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtQty.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    rtbStickerHeader.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    cmbDate.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    cmbPreformParty.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();

                    txtFrom.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[13].Value));
                    txtTo.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[14].Value));
                    txtUsedSticker.Text = objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[15].Value));
                    ShiftId_Form = objRL.Check_Null_Integer(objRL.Check_Null_String(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[16].Value)));

                    lblUsedSticker.Visible = true;
                    txtUsedSticker.Visible = true;
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

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("ProductionEntry"));
            txtID.Text = IDNo.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateFlag = true;
            IDFlag = false;
            SearchTag = false;
            FillGrid();
        }

        bool DateFlag = false;

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

        bool IDFlag = false;

        private void cmbMachineNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSearchProductName.Focus();
        }

        private void txtPurchaseOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbDate.Focus();
        }

        private void cmbDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbPreformParty.Focus();
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPurchaseOrderNo.Focus();
        }

        protected void ClearAll()
        {
            ProductId = 0;
            TableID = 0;
            objEP.Clear();
            FlagDelete = false;
            SearchTag = false;
            DateFlag = false;
            txtID.Text = "";
            dtpDate.Value = DateTime.Now.Date.Date;
            dtpTime.Value = DateTime.Now;
            cbToday.Checked = true;

            btnAddShift.Visible = false;

            txtMachineDetails.Text = "";
            cmbMachineNo.SelectedIndex = -1;
            txtSearchProductName.Text = "";
            lblProductName.Text = "";
            lblProductType.Text = "";
            cmbProductName.SelectedIndex = -1;
            txtQty.Text = "";
            rtbStickerHeader.Text = "";
            pbQRCode.Image = null;
            txtPurchaseOrderNo.Text = "";
            txtUsedSticker.Text = "";
            cmbDate.Text = "";
            txtFrom.Text = "";
            txtTo.Text = "";
            cmbDate.SelectedIndex = -1;
            cmbPreformParty.SelectedIndex = -1;
            GetID();
            lblProductType.BackColor = Color.White;
            lblProductName.BackColor = Color.White;

            lblUsedSticker.Visible = false;
            txtUsedSticker.Visible = false;

            btnPrint.Visible = false;
            cmbMachineNo.Focus();
        }

        protected void DrawBorder(Excel.Range Functionrange)
        {
            Excel.Borders borders = Functionrange.Borders;
            //borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlBorderWeight.xlHairline;
            borders.Weight = 1D;
        }
        protected void Fill_Merge_Cell(string Cell1, string Cell2, object val1, Excel.Worksheet myExcelWorksheet, string SetValue)
        {
            //CellFlag = false;
            if (!CellFlag)
            {
                Cell1 = Cell1 + RowCount;
                Cell2 = Cell2 + RowCount;
            }

            Microsoft.Office.Interop.Excel.Range AlingRange1 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange1.Merge(val1);

            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            if (boldflag)
                AlingRange1.EntireRow.Font.Bold = true;


            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            Microsoft.Office.Interop.Excel.Range AlingRange2 = myExcelWorksheet.get_Range(Cell1, Cell2);

            if (AFlag == 0)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            if (AFlag == 1)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            if (AFlag == 2)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            if (BorderFlag)
                DrawBorder(AlingRange2);

            if (MH_Value)
            {
                AlingRange2.RowHeight = 60;
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                AlingRange2.WrapText = true;
            }

            if (AlignFlag)
            {
                AlingRange1.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            }
        }

        private void SaveQRCode_Excel(Excel.Worksheet myExcelWorksheet, int RowNo, int ColumnNo,int QRNo)
        {
            string Information = string.Empty;
            B1 = string.Empty;
            B2 = string.Empty;
            B3 = string.Empty;
            B4 = string.Empty;
            B5 = string.Empty;
            QRImagePath = string.Empty;

            QRCodeData = string.Empty;

            if (ShiftFlag)
                Day = DateTime.Now.DayOfYear - 1;
            else
                Day = DateTime.Now.DayOfYear;

            Year = Convert.ToInt32(DateTime.Now.Date.Year);
            YearS = Year.ToString();
            curYear = Year.ToString().Substring(2, 2).ToString();

            // B1 = NeckHeightWeightQR;
            B1 = lblProductName.Text;
            B2 = ",Qty-" + txtQty.Text;
            //B3 = ",Batch-" + Day.ToString() + "/" + curYear.ToString() + "/" + Shift;
            //B3 = ",Batch-"+ curYear.ToString() + "/" + Day.ToString() + "/" + Shift + "/M-" +cmbMachineNo.Text ;
            //B3 = ",BNo-" + curYear.ToString() + Day.ToString() + Shift + cmbMachineNo.Text + "M" + txtID.Text;

            //Changes on 07-06-2025 as per Ajay Sir Requirements
            B3 = "," + Shift + "-" + curYear.ToString() + Day.ToString() + "-" + cmbMachineNo.Text + "M" + txtID.Text;

            if (cmbDate.SelectedIndex > -1 && cmbDate.Text == "Yes")
            {
                B4 = ",Date-" + DateTime.Now.Date.ToString("dd/MMM/yyyy");
            }
            else
                B4 = "";

            if (txtPurchaseOrderNo.Text != "")
            {
                B5 = ",PO No.-" + txtPurchaseOrderNo.Text;
            }
            else
                B5 = "";

            //B1 = NeckHeightWeightQR;
            //B2 = ",Product - " + cmbProductName.Text;
            //B3 = ",Qty-" + txtQty.Text;
            //B4 = ",Batch-" + txtID.Text + "/" + Day.ToString() + "/" + curYear.ToString() + "/" + cmbShiftNo.Text;
            //QRCodeData = NeckHeightWeightQR + ",Product-" + cmbProductName.Text + ",Qty-" + txtQty.Text + ",Batch-" + txtID.Text + "/" + Day.ToString() + "/"  + curYear.ToString() + "/" + cmbShiftNo.Text;

            QRCodeData = B1 + B2 + B3;// + B4 + B5;
            //QRCodeData = B1 + B2 + B3;// + B4;

            //QRCodeDataRTB = NeckHeightWeightQR + "\nProduct-" + cmbProductName.Text + "\nQty-" + txtQty.Text + "\nBatch-" + txtID.Text + "/" + Day.ToString() + "/" + curYear.ToString() + "/" + cmbShiftNo.Text;
            //Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            //pbQRCode.Image = barcode.Draw(Information.ToString(), 50);

            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pbQRCode.Image = qrcode.Draw(QRCodeData.ToString(), 10);
            QRImagePath = objRL.GetPath("ImagePath");
            var filePath = QRImagePath;
            Directory.CreateDirectory(filePath);
            string FileName = txtID.Text.ToString();
            pbQRCode.Image.Save(Path.Combine(filePath, FileName), System.Drawing.Imaging.ImageFormat.Png);
        }

        private void SetQRCode(Excel.Worksheet myExcelWorksheet, int RowNo, int ColumnNo)
        {
            QRImagePath = objRL.GetPath("ImagePath") + txtID.Text;
            Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[RowNo, ColumnNo];
            float Left = (float)((double)oRange.Left);
            float Top = (float)((double)oRange.Top);
            const float ImageSize = 40;
            oRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            oRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);

            // myExcelWorksheet.get_Range("A1", "A14").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        }

        public void GetReport()
        {
            using (new CursorWait())
            {
                BorderFlag = false;

                object misValue = System.Reflection.Missing.Value;
                myExcelApp = new Excel.Application();
                myExcelWorkbooks = myExcelApp.Workbooks;

                objRL.ClearExcelPath();
                objRL.isPDF = true;
                objRL.Form_ExcelFileName = "QR.xlsx";
                objRL.Form_ReportFileName = "QRCode-" + ReportName;
                objRL.Form_DestinationReportFilePath = "\\QRCode\\" + ReportName + "\\";
                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                //objRL.FillCompanyData();


                //B2 = B2.Replace(",", "");
                //B3 = B3.Replace(",", "");
                //B4 = B4.Replace(",", "");
                //B5 = B5.Replace(",", "");

                //myExcelWorksheet.get_Range("B2", misValue).Formula = B1.ToString();
                //myExcelWorksheet.get_Range("B3", misValue).Formula = B2.ToString();
                //myExcelWorksheet.get_Range("B5", misValue).Formula = B3.ToString();
                //myExcelWorksheet.get_Range("B6", misValue).Formula = B4.ToString();

                //string Concat = B1 + "," + B2 + "," + B3 + "," + B4 + "," + B5;
                string Concat = B1 + B2 + B3 + B4 + B5;

                myExcelWorksheet.get_Range("A1", misValue).Formula = Concat.ToString();
                SetQRCode(myExcelWorksheet, 1, 2);
                myExcelWorksheet.get_Range("C1", misValue).Formula = Concat.ToString();
                SetQRCode(myExcelWorksheet, 1, 4);
                myExcelWorksheet.get_Range("E1", misValue).Formula = Concat.ToString();
                SetQRCode(myExcelWorksheet, 1, 6);
                myExcelWorksheet.get_Range("G1", misValue).Formula = Concat.ToString();
                SetQRCode(myExcelWorksheet, 1, 8);


                string C1 = "A", C2 = "H";
                Range RngToCopy = myExcelWorksheet.get_Range("A1", "H1").EntireRow;

                RowCount = 2;
                for (int i = 1; i < 10; i++)
                {
                    RowCount = i;
                    C1 = "A" + RowCount; C2 = "H" + RowCount;
                    Range RngToInsert = myExcelWorksheet.get_Range(C1, C2).EntireRow;
                    RngToInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, RngToCopy.Copy(Type.Missing));
                }

                //Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[1, 2];
                //float Left = (float)((double)oRange.Left);
                //float Top = (float)((double)oRange.Top);
                //const float ImageSize = 40;
                //myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                ////QRCodeDataRTB = NeckHeightWeightQR + "\nProduct-" + cmbProductName.Text + "\nQty-" + txtQty.Text + "\nBatch-" + txtID.Text + "/" + Day.ToString() + "/" + curYear.ToString() + "/" + cmbShiftNo.Text;


                //myExcelWorksheet.get_Range("C1", misValue).Formula = Concat.ToString();
                ////2 Column
                //Microsoft.Office.Interop.Excel.Range oRange1 = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[1, 4];
                //float Left1 = (float)((double)oRange1.Left);
                //float Top1 = (float)((double)oRange1.Top);
                //const float ImageSize1 = 40;
                //myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left1, Top1, ImageSize1, ImageSize1);

                //myExcelWorksheet.get_Range("E1", misValue).Formula = Concat.ToString();
                ////2 Column
                //Microsoft.Office.Interop.Excel.Range oRange2 = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[1, 6];
                //float Left2 = (float)((double)oRange2.Left);
                //float Top2 = (float)((double)oRange2.Top);
                //const float ImageSize2 = 40;
                //myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left2, Top2, ImageSize2, ImageSize2);

                //myExcelWorksheet.get_Range("G1", misValue).Formula = Concat.ToString();
                ////2 Column
                //Microsoft.Office.Interop.Excel.Range oRange3 = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[1, 8];
                //float Left3 = (float)((double)oRange3.Left);
                //float Top3 = (float)((double)oRange3.Top);
                //const float ImageSize3 = 40;
                //myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left3, Top3, ImageSize3, ImageSize3);

                //myExcelWorksheet.get_Range("G2", misValue).Formula = B1.ToString();
                //myExcelWorksheet.get_Range("G3", misValue).Formula = B2.ToString();
                //myExcelWorksheet.get_Range("G5", misValue).Formula = B3.ToString();
                //myExcelWorksheet.get_Range("G6", misValue).Formula = B4.ToString();

                //int countIncrease = 6;
                //RowCount = 1;
                //string C1 = "A", C2 = "H";

                ////for (int i = 0; i < 11; i++)
                ////{
                ////    RowCount = RowCount + countIncrease;
                ////    C1 = "A" + RowCount; C2 = "H" + RowCount;
                //Range RngToCopy = myExcelWorksheet.get_Range("A1", "H6").EntireRow;
                //Range RngToInsert = myExcelWorksheet.get_Range("A7", "H12").EntireRow;
                //RngToInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, RngToCopy.Copy(Type.Missing));
                ////countIncrease = countIncrease + 6;

                // }



                //0 AC.ID,
                //1 AC.EntryDate,
                //2 AC.CollectionTypeId,
                //3 CTM.CollectionType,
                //4 AC.Amount,
                //5 AC.WithFood,
                //6 AC.WithoutFood,
                //7 AC.IssuedBy,
                //8 AC.ReceivedBy 

                // myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName.ToString();
                //myExcelWorksheet.get_Range("A2", misValue).Formula = objRL.Form_ReportFileName.ToString();
                //myExcelWorksheet.get_Range("A3", misValue).Formula = "Report Date From- " + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat) + " To Date: " + dtpToDate.Value.ToString(RedundancyLogics.SystemDateFormat);

                //WithFood_Total = 0; WithoutFood_Total = 0; Amount_Total = 0;
                //RowCount = 6; SrNo = 1;
                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{

                //    AFlag = 1;
                //    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SrNo.ToString());

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[1].Value)))
                //    {
                //        AFlag = 0;
                //        BookingDate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value);
                //        Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, BookingDate.ToString("dd/MM/yyyy"));
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[3].Value)))
                //    {
                //        AFlag = 0;
                //        Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[3].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[7].Value)))
                //    {
                //        AFlag = 0;
                //        Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[7].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[8].Value)))
                //    {
                //        AFlag = 0;
                //        Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[8].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[5].Value)))
                //    {
                //        AFlag = 2;
                //        Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[5].Value.ToString());
                //        WithFood_Total += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[6].Value)))
                //    {
                //        AFlag = 2;
                //        Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[6].Value.ToString());
                //        WithoutFood_Total += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[4].Value)))
                //    {
                //        AFlag = 2;
                //        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[4].Value.ToString());
                //        Amount_Total += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString());
                //    }
                //    SrNo++;
                //    RowCount++;
                //}
                //myExcelWorksheet.get_Range("F4", misValue).Formula = WithFood_Total.ToString();
                //myExcelWorksheet.get_Range("G4", misValue).Formula = WithoutFood_Total.ToString();
                //myExcelWorksheet.get_Range("H4", misValue).Formula = Amount_Total.ToString();

                myExcelWorkbook.Save();


                //  PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");

                try
                {
                    const int xlQualityStandard = 0;
                    myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                    myExcelWorkbook.Close(true, misValue, misValue);
                    myExcelApp.Quit();
                    System.Diagnostics.Process.Start(objRL.RL_DestinationPath);
                    //objRL.ShowMessage(22, 1);

                    //DialogResult dr;
                    //dr = MessageBox.Show("Do you want to view this report", "Report View", MessageBoxButtons.YesNo);
                    //if (dr == DialogResult.Yes)
                    //    System.Diagnostics.Process.Start(objRL.RL_DestinationPath);

                    // System.Diagnostics.Process.Start(PDFReport);
                    //objRL.DeleteExcelFile();

                    //if (!string.IsNullOrEmpty(objRL.EmailId) && cbEmail.Checked)
                    //{
                    //    objRL.EmailId_RL = objRL.EmailId;
                    //    objRL.Subject_RL = "Amount Collection Report";
                    //    //string body = "<div><p>Dear " + objRL.CustomerName_Customer + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
                    //    string body = "<div><p>Dear Sir,<p/><p>Please find attachment of pdf file.</p><p>Amount Collection Report on " + dtpFromDate.Value.ToString("dd/MM/yyyy") + " to " + dtpToDate.Value.ToString("dd/MM/yyyy") + " </p><p>Thanks,</p></div>";

                    //    objRL.Body_RL = body;// "Dear " + RedundancyLogics.SupplierName + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
                    //    objRL.FilePath_RL = PDFReport;
                    //    objRL.SendEMail();
                    //}

                    //if (cbEmail.Checked)
                    //{
                    //    objRL.EmailId_RL = "";
                    //    objRL.Subject_RL = "";
                    //    objRL.Body_RL = "";
                    //    objRL.FilePath_RL = PDFReport;
                    //    //objRL.SendEMail();
                    //}
                }
                catch (Exception ex1)
                {
                    objRL.ShowMessage(27, 4);
                    return;
                }
            }
        }

        static void CopyShapes(Excel.Worksheet worksheet, Excel.Range sourceRange, Excel.Range destinationRange)
        {
            foreach (Excel.Shape shape in worksheet.Shapes)
            {
                // Check if the shape is within the source range
                if (IsShapeInRange(shape, sourceRange))
                {
                    // Copy the shape
                    shape.Copy();

                    // Paste the shape at the destination range
                    worksheet.Paste(destinationRange);

                    // You may want to adjust the position of the pasted shape based on the destination range
                    // This could be done by setting the shape's Left and Top properties to adjust its position
                }
            }
        }

        // Helper method to check if the shape is within the source range
        static bool IsShapeInRange(Excel.Shape shape, Excel.Range range)
        {
            Excel.Range shapeRange = shape.TopLeftCell;
            return (shapeRange.Row >= range.Row && shapeRange.Row <= range.Row + range.Rows.Count - 1 &&
                    shapeRange.Column >= range.Column && shapeRange.Column <= range.Column + range.Columns.Count - 1);
        }

        public void GetReportSingleNew()
        {
            using (new CursorWait())
            {
                int FromRange = 0, ToRange = 0, TotalRange = 0;

                BorderFlag = false;

                object misValue = System.Reflection.Missing.Value;
                myExcelApp = new Excel.Application();
                myExcelWorkbooks = myExcelApp.Workbooks;

                objRL.ClearExcelPath();
                objRL.isPDF = true;
                objRL.Form_ExcelFileName = "QR50X25.xlsx";
                objRL.Form_ReportFileName = "ID-" + txtID.Text;
                objRL.Form_DestinationReportFilePath = "\\Stickers\\";
                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                //objRL.FillCompanyData();


                //B2 = B2.Replace(",", "");
                //B3 = B3.Replace(",", "");
                //B4 = B4.Replace(",", "");
                //B5 = B5.Replace(",", "");

                //myExcelWorksheet.get_Range("B2", misValue).Formula = B1.ToString();
                //myExcelWorksheet.get_Range("B3", misValue).Formula = B2.ToString();
                //myExcelWorksheet.get_Range("B5", misValue).Formula = B3.ToString();
                //myExcelWorksheet.get_Range("B6", misValue).Formula = B4.ToString();

                //string Concat = B1 + "," + B2 + "," + B3 + "," + B4 + "," + B5;
                string Concat = B1 + B2 + B3 + B4 + B5;

                //string Change = B1;
                //Change = Change.Substring(Change.IndexOf(' '));

                //var myString = B1;
                //var newString = myString.Remove(0, myString.IndexOf(' ') + 1);
                //Change = newString.ToString();

                //B1 = B1.TrimStart();

                string sourceString = B1, removeString = " ";
                int index = sourceString.IndexOf(removeString);
                string cleanPath = (index < 0)
                    ? sourceString
                    : sourceString.Remove(index, removeString.Length);

                B1 = cleanPath;
                //B1 = B1.Replace(" ","");
                //50X25 Label

                //Old Code
                //myExcelWorksheet.get_Range("A1", misValue).Formula = B1.ToString();
                //myExcelWorksheet.get_Range("A4", misValue).Formula = B2.ToString();
                ////myExcelWorksheet.get_Range("A5", misValue).Formula = B3.ToString();
                //myExcelWorksheet.get_Range("A6", misValue).Formula = B4.ToString();
                //myExcelWorksheet.get_Range("A7", misValue).Formula = B5.ToString();

                myExcelWorksheet.get_Range("A1", misValue).Formula = B1.ToString();
                myExcelWorksheet.get_Range("A4", misValue).Formula = B2.ToString();
                myExcelWorksheet.get_Range("A5", misValue).Formula = B5.ToString();

                //B3 = B3 + "-PN-1";
                myExcelWorksheet.get_Range("A6", misValue).Formula = B3.ToString();
                myExcelWorksheet.get_Range("A7", misValue).Formula = B4.ToString();

                FromRange = Convert.ToInt32(txtFrom.Text);
                ToRange = Convert.ToInt32(txtTo.Text);

                //if(FromRange ==1)
                //    myExcelWorksheet.get_Range("B7", misValue).Formula = "RNO-1";
                //else
                //    myExcelWorksheet.get_Range("B7", misValue).Formula = "RNO-"+FromRange+"";

                if (ToRange > 1)
                    myExcelWorksheet.get_Range("B7", misValue).Formula = "RNO-" + ToRange + "";

                //    myExcelWorksheet.get_Range("B7", misValue).Formula = "RNO-1";
                //else
                //    myExcelWorksheet.get_Range("B7", misValue).Formula = "RNO-" + FromRange + "";

                //Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, "RNO-" + RNO);

                //SetQRCode(myExcelWorksheet, 2, 2);
                SetQRCode(myExcelWorksheet, 2, 2);


                // Define the source and destination row ranges
                int sourceStartRow = 1;  // Starting row of the source range
                int sourceEndRow = 6;    // Ending row of the source range

                int destinationRow = 8; // Starting row of the destination range

                // Define the source range

                string StartRow = string.Empty, EndRow = string.Empty;

                StartRow = "A" + sourceStartRow;
                EndRow = "B" + sourceEndRow;
                Excel.Range sourceRange = myExcelWorksheet.Range[StartRow + ":" + EndRow];


                //Logic for Multiple Print

                TotalRange = ToRange - FromRange;
                TotalRange++;

                if (TotalRange > 0)
                {
                    destinationRow = 8;

                    //for (int i = FromRange + 1; i <= ToRange; i++)
                    for (int i = ToRange - 1; i >= FromRange; i--)
                    {
                        //2nd Row
                        StartRow = "A" + destinationRow;
                        // Define the destination range
                        Excel.Range destinationRange = myExcelWorksheet.Range[StartRow];

                        // Copy the source range
                        sourceRange.Copy();

                        // Paste the copied range into the destination range
                        destinationRange.PasteSpecial(Excel.XlPasteType.xlPasteAll);  // To paste values only


                        //StartRow = "B" + destinationRow.ToString();
                        //Excel.Range destinationRangeI = myExcelWorksheet.Range[StartRow];

                        //CopyShapes(myExcelWorksheet, sourceRange, destinationRangeI);

                        int RC = 0; RC = destinationRow + 6;
                        string PNumber = "B" + RC.ToString();

                        // myExcelWorksheet.get_Range("A6", misValue).Formula = B3.ToString();
                        int RNO = 0;

                        //RNO = i + 1;
                        RNO = i;

                        //myExcelWorksheet.get_Range(PNumber, misValue).Formula = "RNO-" + RNO;

                        RowCount = RC; boldflag = true;
                        Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, "RNO-" + RNO);

                        //myExcelWorksheet.get_Range("A1", misValue).Formula = B1.ToString();

                        int dirow = destinationRow + 1;
                        SetQRCode(myExcelWorksheet, dirow, 2);

                        destinationRow = destinationRow + 7;
                    }
                }


                // StartRow = "A" + destinationRow;
                // // Define the destination range
                // Excel.Range destinationRange = myExcelWorksheet.Range[StartRow];

                // // Copy the source range
                // sourceRange.Copy();

                // // Paste the copied range into the destination range
                // destinationRange.PasteSpecial(Excel.XlPasteType.xlPasteAll);  // To paste values only
                //// myExcelApp.CutCopyMode = 0;
                // // Optionally, you can also paste formats if needed:
                // // destinationRange.PasteSpecial(Excel.XlPasteType.xlPasteFormats);


                // StartRow = "B" + destinationRow.ToString();
                // Excel.Range destinationRangeI = myExcelWorksheet.Range[StartRow];
                // CopyShapes(myExcelWorksheet, sourceRange, destinationRangeI);

                //75X50 Label
                //myExcelWorksheet.get_Range("A1", misValue).Formula = Concat.ToString();
                //SetQRCode(myExcelWorksheet, 5, 1);




                //myExcelWorksheet.get_Range("C1", misValue).Formula = Concat.ToString();
                //SetQRCode(myExcelWorksheet, 1, 4);
                //myExcelWorksheet.get_Range("E1", misValue).Formula = Concat.ToString();
                //SetQRCode(myExcelWorksheet, 1, 6);
                //myExcelWorksheet.get_Range("G1", misValue).Formula = Concat.ToString();
                //SetQRCode(myExcelWorksheet, 1, 8);


                //string C1 = "A", C2 = "H";
                //Range RngToCopy = myExcelWorksheet.get_Range("A1", "H1").EntireRow;

                //RowCount = 2;
                //for (int i = 1; i < 10; i++)
                //{
                //    RowCount = i;
                //    C1 = "A" + RowCount; C2 = "H" + RowCount;
                //    Range RngToInsert = myExcelWorksheet.get_Range(C1, C2).EntireRow;
                //    RngToInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, RngToCopy.Copy(Type.Missing));
                //}

                //Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[1, 2];
                //float Left = (float)((double)oRange.Left);
                //float Top = (float)((double)oRange.Top);
                //const float ImageSize = 40;
                //myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                ////QRCodeDataRTB = NeckHeightWeightQR + "\nProduct-" + cmbProductName.Text + "\nQty-" + txtQty.Text + "\nBatch-" + txtID.Text + "/" + Day.ToString() + "/" + curYear.ToString() + "/" + cmbShiftNo.Text;


                //myExcelWorksheet.get_Range("C1", misValue).Formula = Concat.ToString();
                ////2 Column
                //Microsoft.Office.Interop.Excel.Range oRange1 = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[1, 4];
                //float Left1 = (float)((double)oRange1.Left);
                //float Top1 = (float)((double)oRange1.Top);
                //const float ImageSize1 = 40;
                //myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left1, Top1, ImageSize1, ImageSize1);

                //myExcelWorksheet.get_Range("E1", misValue).Formula = Concat.ToString();
                ////2 Column
                //Microsoft.Office.Interop.Excel.Range oRange2 = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[1, 6];
                //float Left2 = (float)((double)oRange2.Left);
                //float Top2 = (float)((double)oRange2.Top);
                //const float ImageSize2 = 40;
                //myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left2, Top2, ImageSize2, ImageSize2);

                //myExcelWorksheet.get_Range("G1", misValue).Formula = Concat.ToString();
                ////2 Column
                //Microsoft.Office.Interop.Excel.Range oRange3 = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[1, 8];
                //float Left3 = (float)((double)oRange3.Left);
                //float Top3 = (float)((double)oRange3.Top);
                //const float ImageSize3 = 40;
                //myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left3, Top3, ImageSize3, ImageSize3);

                //myExcelWorksheet.get_Range("G2", misValue).Formula = B1.ToString();
                //myExcelWorksheet.get_Range("G3", misValue).Formula = B2.ToString();
                //myExcelWorksheet.get_Range("G5", misValue).Formula = B3.ToString();
                //myExcelWorksheet.get_Range("G6", misValue).Formula = B4.ToString();

                //int countIncrease = 6;
                //RowCount = 1;
                //string C1 = "A", C2 = "H";

                ////for (int i = 0; i < 11; i++)
                ////{
                ////    RowCount = RowCount + countIncrease;
                ////    C1 = "A" + RowCount; C2 = "H" + RowCount;
                //Range RngToCopy = myExcelWorksheet.get_Range("A1", "H6").EntireRow;
                //Range RngToInsert = myExcelWorksheet.get_Range("A7", "H12").EntireRow;
                //RngToInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, RngToCopy.Copy(Type.Missing));
                ////countIncrease = countIncrease + 6;

                // }



                //0 AC.ID,
                //1 AC.EntryDate,
                //2 AC.CollectionTypeId,
                //3 CTM.CollectionType,
                //4 AC.Amount,
                //5 AC.WithFood,
                //6 AC.WithoutFood,
                //7 AC.IssuedBy,
                //8 AC.ReceivedBy 

                // myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName.ToString();
                //myExcelWorksheet.get_Range("A2", misValue).Formula = objRL.Form_ReportFileName.ToString();
                //myExcelWorksheet.get_Range("A3", misValue).Formula = "Report Date From- " + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat) + " To Date: " + dtpToDate.Value.ToString(RedundancyLogics.SystemDateFormat);

                //WithFood_Total = 0; WithoutFood_Total = 0; Amount_Total = 0;
                //RowCount = 6; SrNo = 1;
                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{

                //    AFlag = 1;
                //    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SrNo.ToString());

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[1].Value)))
                //    {
                //        AFlag = 0;
                //        BookingDate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value);
                //        Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, BookingDate.ToString("dd/MM/yyyy"));
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[3].Value)))
                //    {
                //        AFlag = 0;
                //        Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[3].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[7].Value)))
                //    {
                //        AFlag = 0;
                //        Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[7].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[8].Value)))
                //    {
                //        AFlag = 0;
                //        Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[8].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[5].Value)))
                //    {
                //        AFlag = 2;
                //        Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[5].Value.ToString());
                //        WithFood_Total += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[6].Value)))
                //    {
                //        AFlag = 2;
                //        Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[6].Value.ToString());
                //        WithoutFood_Total += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[4].Value)))
                //    {
                //        AFlag = 2;
                //        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[4].Value.ToString());
                //        Amount_Total += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString());
                //    }
                //    SrNo++;
                //    RowCount++;
                //}
                //myExcelWorksheet.get_Range("F4", misValue).Formula = WithFood_Total.ToString();
                //myExcelWorksheet.get_Range("G4", misValue).Formula = WithoutFood_Total.ToString();
                //myExcelWorksheet.get_Range("H4", misValue).Formula = Amount_Total.ToString();

                myExcelWorkbook.Save();

                //var printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters;

                //int printerIndex = 0;

                //foreach (String s in printers)
                //{
                //    if (s.Equals(@"\\192.168.1.3\TSC TE210"))
                //    {
                //        break;
                //    }
                //    printerIndex++;
                //}

                //try
                //{


                //    //myExcelWorkbook.PrintOutEx(1, 1, 1, false, printerIndex, true, false, objRL.RL_DestinationPath, false);

                //   // myExcelWorkbook.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //    //myExcelWorkbook.PrintOut()

                //}
                //catch (Exception ex1)
                //{
                //    MessageBox.Show(ex1.ToString());
                //}


                //  PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");

                try
                {


                    //const int xlQualityStandard = 0;
                    //myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                    myExcelWorkbook.Close(true, misValue, misValue);
                    myExcelApp.Quit();

                    System.Diagnostics.Process.Start(objRL.RL_DestinationPath);



                    //objRL.ShowMessage(22, 1);

                    //DialogResult dr;
                    //dr = MessageBox.Show("Do you want to view this report", "Report View", MessageBoxButtons.YesNo);
                    //if (dr == DialogResult.Yes)
                    //    System.Diagnostics.Process.Start(objRL.RL_DestinationPath);

                    // System.Diagnostics.Process.Start(PDFReport);
                    //objRL.DeleteExcelFile();

                    //if (!string.IsNullOrEmpty(objRL.EmailId) && cbEmail.Checked)
                    //{
                    //    objRL.EmailId_RL = objRL.EmailId;
                    //    objRL.Subject_RL = "Amount Collection Report";
                    //    //string body = "<div><p>Dear " + objRL.CustomerName_Customer + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
                    //    string body = "<div><p>Dear Sir,<p/><p>Please find attachment of pdf file.</p><p>Amount Collection Report on " + dtpFromDate.Value.ToString("dd/MM/yyyy") + " to " + dtpToDate.Value.ToString("dd/MM/yyyy") + " </p><p>Thanks,</p></div>";

                    //    objRL.Body_RL = body;// "Dear " + RedundancyLogics.SupplierName + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
                    //    objRL.FilePath_RL = PDFReport;
                    //    objRL.SendEMail();
                    //}

                    //if (cbEmail.Checked)
                    //{
                    //    objRL.EmailId_RL = "";
                    //    objRL.Subject_RL = "";
                    //    objRL.Body_RL = "";
                    //    objRL.FilePath_RL = PDFReport;
                    //    //objRL.SendEMail();
                    //}
                }
                catch (Exception ex1)
                {
                    objRL.ShowMessage(27, 4);
                    return;
                }
            }
        }

        public void GetReportSingle()
        {
            using (new CursorWait())
            {
                BorderFlag = false;

                object misValue = System.Reflection.Missing.Value;
                myExcelApp = new Excel.Application();
                myExcelWorkbooks = myExcelApp.Workbooks;

                objRL.ClearExcelPath();
                objRL.isPDF = true;
                objRL.Form_ExcelFileName = "QR50X25.xlsx";
                objRL.Form_ReportFileName = "ID-" + txtID.Text;
                objRL.Form_DestinationReportFilePath = "\\Stickers\\";
                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                //objRL.FillCompanyData();


                //B2 = B2.Replace(",", "");
                //B3 = B3.Replace(",", "");
                //B4 = B4.Replace(",", "");
                //B5 = B5.Replace(",", "");

                //myExcelWorksheet.get_Range("B2", misValue).Formula = B1.ToString();
                //myExcelWorksheet.get_Range("B3", misValue).Formula = B2.ToString();
                //myExcelWorksheet.get_Range("B5", misValue).Formula = B3.ToString();
                //myExcelWorksheet.get_Range("B6", misValue).Formula = B4.ToString();

                //string Concat = B1 + "," + B2 + "," + B3 + "," + B4 + "," + B5;
                string Concat = B1 + B2 + B3 + B4 + B5;

                //string Change = B1;
                //Change = Change.Substring(Change.IndexOf(' '));

                //var myString = B1;
                //var newString = myString.Remove(0, myString.IndexOf(' ') + 1);
                //Change = newString.ToString();

                //B1 = B1.TrimStart();

                string sourceString = B1, removeString = " ";
                int index = sourceString.IndexOf(removeString);
                string cleanPath = (index < 0)
                    ? sourceString
                    : sourceString.Remove(index, removeString.Length);

                B1 = cleanPath;
                //B1 = B1.Replace(" ","");
                //50X25 Label

                //Old Code
                //myExcelWorksheet.get_Range("A1", misValue).Formula = B1.ToString();
                //myExcelWorksheet.get_Range("A4", misValue).Formula = B2.ToString();
                ////myExcelWorksheet.get_Range("A5", misValue).Formula = B3.ToString();
                //myExcelWorksheet.get_Range("A6", misValue).Formula = B4.ToString();
                //myExcelWorksheet.get_Range("A7", misValue).Formula = B5.ToString();

                myExcelWorksheet.get_Range("A1", misValue).Formula = B1.ToString();
                myExcelWorksheet.get_Range("A4", misValue).Formula = B2.ToString();
                myExcelWorksheet.get_Range("A5", misValue).Formula = B5.ToString();

                //B3 = B3 + "-PN-1";
                myExcelWorksheet.get_Range("A6", misValue).Formula = B3.ToString();
                myExcelWorksheet.get_Range("A7", misValue).Formula = B4.ToString();

                // myExcelWorksheet.get_Range("B7", misValue).Formula = "RNO-1";

                //SetQRCode(myExcelWorksheet, 2, 2);
                SetQRCode(myExcelWorksheet, 1, 2);


                //     // Define the source and destination row ranges
                //int sourceStartRow = 1;  // Starting row of the source range
                //int sourceEndRow = 6;    // Ending row of the source range

                //int destinationRow = 8; // Starting row of the destination range

                //// Define the source range

                //string StartRow = string.Empty, EndRow = string.Empty;

                //StartRow = "A" + sourceStartRow;
                //EndRow = "B" + sourceEndRow;
                //Excel.Range sourceRange = myExcelWorksheet.Range[StartRow +":"+ EndRow];


                ////Logic for Multiple Print
                //int FromRange = 0, ToRange = 0, TotalRange = 0;

                //FromRange = Convert.ToInt32(txtFrom.Text);
                //ToRange = Convert.ToInt32(txtTo.Text); 
                //TotalRange = ToRange-FromRange;
                //TotalRange++;

                //if (TotalRange > 0)
                //{
                //    destinationRow=8;

                //    for (int i = 1; i <= TotalRange; i++)
                //    {
                //        //2nd Row
                //        StartRow = "A" + destinationRow;
                //        // Define the destination range
                //        Excel.Range destinationRange = myExcelWorksheet.Range[StartRow];

                //        // Copy the source range
                //        sourceRange.Copy();

                //        // Paste the copied range into the destination range
                //        destinationRange.PasteSpecial(Excel.XlPasteType.xlPasteAll);  // To paste values only


                //        //StartRow = "B" + destinationRow.ToString();
                //        //Excel.Range destinationRangeI = myExcelWorksheet.Range[StartRow];

                //        //CopyShapes(myExcelWorksheet, sourceRange, destinationRangeI);

                //        int RC = 0; RC = destinationRow + 6;
                //        string PNumber = "B" + RC.ToString();

                //       // myExcelWorksheet.get_Range("A6", misValue).Formula = B3.ToString();
                //        int RNO = 0;

                //        RNO = i + 1;
                //        myExcelWorksheet.get_Range(PNumber, misValue).Formula = "RNO-" + RNO;

                //        //myExcelWorksheet.get_Range("A1", misValue).Formula = B1.ToString();

                //        SetQRCode(myExcelWorksheet, destinationRow, 2);

                //        destinationRow = destinationRow + 7;
                //    }
                //}


                // StartRow = "A" + destinationRow;
                // // Define the destination range
                // Excel.Range destinationRange = myExcelWorksheet.Range[StartRow];

                // // Copy the source range
                // sourceRange.Copy();

                // // Paste the copied range into the destination range
                // destinationRange.PasteSpecial(Excel.XlPasteType.xlPasteAll);  // To paste values only
                //// myExcelApp.CutCopyMode = 0;
                // // Optionally, you can also paste formats if needed:
                // // destinationRange.PasteSpecial(Excel.XlPasteType.xlPasteFormats);


                // StartRow = "B" + destinationRow.ToString();
                // Excel.Range destinationRangeI = myExcelWorksheet.Range[StartRow];
                // CopyShapes(myExcelWorksheet, sourceRange, destinationRangeI);

                //75X50 Label
                //myExcelWorksheet.get_Range("A1", misValue).Formula = Concat.ToString();
                //SetQRCode(myExcelWorksheet, 5, 1);




                //myExcelWorksheet.get_Range("C1", misValue).Formula = Concat.ToString();
                //SetQRCode(myExcelWorksheet, 1, 4);
                //myExcelWorksheet.get_Range("E1", misValue).Formula = Concat.ToString();
                //SetQRCode(myExcelWorksheet, 1, 6);
                //myExcelWorksheet.get_Range("G1", misValue).Formula = Concat.ToString();
                //SetQRCode(myExcelWorksheet, 1, 8);


                //string C1 = "A", C2 = "H";
                //Range RngToCopy = myExcelWorksheet.get_Range("A1", "H1").EntireRow;

                //RowCount = 2;
                //for (int i = 1; i < 10; i++)
                //{
                //    RowCount = i;
                //    C1 = "A" + RowCount; C2 = "H" + RowCount;
                //    Range RngToInsert = myExcelWorksheet.get_Range(C1, C2).EntireRow;
                //    RngToInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, RngToCopy.Copy(Type.Missing));
                //}

                //Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[1, 2];
                //float Left = (float)((double)oRange.Left);
                //float Top = (float)((double)oRange.Top);
                //const float ImageSize = 40;
                //myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                ////QRCodeDataRTB = NeckHeightWeightQR + "\nProduct-" + cmbProductName.Text + "\nQty-" + txtQty.Text + "\nBatch-" + txtID.Text + "/" + Day.ToString() + "/" + curYear.ToString() + "/" + cmbShiftNo.Text;


                //myExcelWorksheet.get_Range("C1", misValue).Formula = Concat.ToString();
                ////2 Column
                //Microsoft.Office.Interop.Excel.Range oRange1 = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[1, 4];
                //float Left1 = (float)((double)oRange1.Left);
                //float Top1 = (float)((double)oRange1.Top);
                //const float ImageSize1 = 40;
                //myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left1, Top1, ImageSize1, ImageSize1);

                //myExcelWorksheet.get_Range("E1", misValue).Formula = Concat.ToString();
                ////2 Column
                //Microsoft.Office.Interop.Excel.Range oRange2 = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[1, 6];
                //float Left2 = (float)((double)oRange2.Left);
                //float Top2 = (float)((double)oRange2.Top);
                //const float ImageSize2 = 40;
                //myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left2, Top2, ImageSize2, ImageSize2);

                //myExcelWorksheet.get_Range("G1", misValue).Formula = Concat.ToString();
                ////2 Column
                //Microsoft.Office.Interop.Excel.Range oRange3 = (Microsoft.Office.Interop.Excel.Range)myExcelWorksheet.Cells[1, 8];
                //float Left3 = (float)((double)oRange3.Left);
                //float Top3 = (float)((double)oRange3.Top);
                //const float ImageSize3 = 40;
                //myExcelWorksheet.Shapes.AddPicture(QRImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left3, Top3, ImageSize3, ImageSize3);

                //myExcelWorksheet.get_Range("G2", misValue).Formula = B1.ToString();
                //myExcelWorksheet.get_Range("G3", misValue).Formula = B2.ToString();
                //myExcelWorksheet.get_Range("G5", misValue).Formula = B3.ToString();
                //myExcelWorksheet.get_Range("G6", misValue).Formula = B4.ToString();

                //int countIncrease = 6;
                //RowCount = 1;
                //string C1 = "A", C2 = "H";

                ////for (int i = 0; i < 11; i++)
                ////{
                ////    RowCount = RowCount + countIncrease;
                ////    C1 = "A" + RowCount; C2 = "H" + RowCount;
                //Range RngToCopy = myExcelWorksheet.get_Range("A1", "H6").EntireRow;
                //Range RngToInsert = myExcelWorksheet.get_Range("A7", "H12").EntireRow;
                //RngToInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, RngToCopy.Copy(Type.Missing));
                ////countIncrease = countIncrease + 6;

                // }



                //0 AC.ID,
                //1 AC.EntryDate,
                //2 AC.CollectionTypeId,
                //3 CTM.CollectionType,
                //4 AC.Amount,
                //5 AC.WithFood,
                //6 AC.WithoutFood,
                //7 AC.IssuedBy,
                //8 AC.ReceivedBy 

                // myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName.ToString();
                //myExcelWorksheet.get_Range("A2", misValue).Formula = objRL.Form_ReportFileName.ToString();
                //myExcelWorksheet.get_Range("A3", misValue).Formula = "Report Date From- " + dtpFromDate.Value.ToString(RedundancyLogics.SystemDateFormat) + " To Date: " + dtpToDate.Value.ToString(RedundancyLogics.SystemDateFormat);

                //WithFood_Total = 0; WithoutFood_Total = 0; Amount_Total = 0;
                //RowCount = 6; SrNo = 1;
                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{

                //    AFlag = 1;
                //    Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SrNo.ToString());

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[1].Value)))
                //    {
                //        AFlag = 0;
                //        BookingDate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value);
                //        Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, BookingDate.ToString("dd/MM/yyyy"));
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[3].Value)))
                //    {
                //        AFlag = 0;
                //        Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[3].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[7].Value)))
                //    {
                //        AFlag = 0;
                //        Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[7].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[8].Value)))
                //    {
                //        AFlag = 0;
                //        Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[8].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[5].Value)))
                //    {
                //        AFlag = 2;
                //        Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[5].Value.ToString());
                //        WithFood_Total += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[6].Value)))
                //    {
                //        AFlag = 2;
                //        Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[6].Value.ToString());
                //        WithoutFood_Total += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value.ToString());
                //    }

                //    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[4].Value)))
                //    {
                //        AFlag = 2;
                //        Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, dataGridView1.Rows[i].Cells[4].Value.ToString());
                //        Amount_Total += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString());
                //    }
                //    SrNo++;
                //    RowCount++;
                //}
                //myExcelWorksheet.get_Range("F4", misValue).Formula = WithFood_Total.ToString();
                //myExcelWorksheet.get_Range("G4", misValue).Formula = WithoutFood_Total.ToString();
                //myExcelWorksheet.get_Range("H4", misValue).Formula = Amount_Total.ToString();

                myExcelWorkbook.Save();

                //var printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters;

                //int printerIndex = 0;

                //foreach (String s in printers)
                //{
                //    if (s.Equals(@"\\192.168.1.3\TSC TE210"))
                //    {
                //        break;
                //    }
                //    printerIndex++;
                //}

                //try
                //{


                //    //myExcelWorkbook.PrintOutEx(1, 1, 1, false, printerIndex, true, false, objRL.RL_DestinationPath, false);

                //   // myExcelWorkbook.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //    //myExcelWorkbook.PrintOut()

                //}
                //catch (Exception ex1)
                //{
                //    MessageBox.Show(ex1.ToString());
                //}


                //  PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");

                try
                {


                    //const int xlQualityStandard = 0;
                    //myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                    myExcelWorkbook.Close(true, misValue, misValue);
                    myExcelApp.Quit();

                    System.Diagnostics.Process.Start(objRL.RL_DestinationPath);



                    //objRL.ShowMessage(22, 1);

                    //DialogResult dr;
                    //dr = MessageBox.Show("Do you want to view this report", "Report View", MessageBoxButtons.YesNo);
                    //if (dr == DialogResult.Yes)
                    //    System.Diagnostics.Process.Start(objRL.RL_DestinationPath);

                    // System.Diagnostics.Process.Start(PDFReport);
                    //objRL.DeleteExcelFile();

                    //if (!string.IsNullOrEmpty(objRL.EmailId) && cbEmail.Checked)
                    //{
                    //    objRL.EmailId_RL = objRL.EmailId;
                    //    objRL.Subject_RL = "Amount Collection Report";
                    //    //string body = "<div><p>Dear " + objRL.CustomerName_Customer + " <p/><br/> <p>Please find attachment of pdf file.</p><br/><p>Thanks,</p><br/><p>" + objRL.CI_CompanyName + "</p></div>";
                    //    string body = "<div><p>Dear Sir,<p/><p>Please find attachment of pdf file.</p><p>Amount Collection Report on " + dtpFromDate.Value.ToString("dd/MM/yyyy") + " to " + dtpToDate.Value.ToString("dd/MM/yyyy") + " </p><p>Thanks,</p></div>";

                    //    objRL.Body_RL = body;// "Dear " + RedundancyLogics.SupplierName + ", " + System.Environment.NewLine + " PFA..!" + System.Environment.NewLine + " Thanks, " + System.Environment.NewLine + " " + objRL.CompanyName;
                    //    objRL.FilePath_RL = PDFReport;
                    //    objRL.SendEMail();
                    //}

                    //if (cbEmail.Checked)
                    //{
                    //    objRL.EmailId_RL = "";
                    //    objRL.Subject_RL = "";
                    //    objRL.Body_RL = "";
                    //    objRL.FilePath_RL = PDFReport;
                    //    //objRL.SendEMail();
                    //}
                }
                catch (Exception ex1)
                {
                    objRL.ShowMessage(27, 4);
                    return;
                }
            }
        }

        public class CursorWait : IDisposable
        {
            public CursorWait(bool appStarting = false, bool applicationCursor = false)
            {
                // Wait
                Cursor.Current = appStarting ? Cursors.AppStarting : Cursors.WaitCursor;
                if (applicationCursor) System.Windows.Forms.Application.UseWaitCursor = true;
            }

            public void Dispose()
            {
                // Reset
                Cursor.Current = Cursors.Default;
                System.Windows.Forms.Application.UseWaitCursor = false;
            }
        }

        private void cmbPreformParty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtFrom.Focus();
        }

        private void btnAddPreformParty_Click(object sender, EventArgs e)
        {
            PreformPartyMaster objForm = new PreformPartyMaster();
            objForm.ShowDialog(this);
            objRL.FillPreformParty(cmbPreformParty);
        }

        int SalesProduct = 0;
        private void cbSalesProduct_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbSalesProduct.Checked) 
            //    SalesProduct =
        }


        private void NewPrintReport()
        {
            if (txtFrom.Text != "")
            {

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            GetReportSingleNew();
            ClearAll();
            //FillGrid();
        }

        private void txtPrintQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtFrom);
        }

        private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtTo);
        }

        private void txtUsedSticker_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtUsedSticker);
        }

        private void txtFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTo.Focus();
        }

        private void txtTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnPrint.Focus();
        }

        private void btnAddShift_Click(object sender, EventArgs e)
        {
            ShiftScheduleNew objForm = new ShiftScheduleNew();
            objForm.ShowDialog(this);
            ShiftCode();
        }
    }
}
