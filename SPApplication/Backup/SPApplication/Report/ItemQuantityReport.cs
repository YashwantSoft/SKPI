using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using SPApplication;
using SPApplication.Reports;

namespace SPApplication.Reports
{
    public partial class ItemQuantityReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        ToolTip objTT = new ToolTip();

        bool SearchFlag = false;

        bool MH_Value = false;
        Microsoft.Office.Interop.Excel.Application myExcelApp;
        Microsoft.Office.Interop.Excel.Workbooks myExcelWorkbooks;
        Microsoft.Office.Interop.Excel.Workbook myExcelWorkbook;

        public ItemQuantityReport()
        {
            InitializeComponent();
            objBL.Set_Report_Design(this, lblHeader, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_ITEMQUANTITYREPORT);
            dataGridView1.DefaultCellStyle.SelectionBackColor = objBL.GetRowSelectionColor(); 
        }

        private void ItemQuantityReport_Load(object sender, EventArgs e)
        {
            SearchFlag = false;
            txtSearchItemName.Text = "";
            FillGrid();
        }

        private void txtSearchItemName_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchItemName.Text != "")
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

        int SrNo_Report = 1;
        int ItemId = 0,   dgvItemRow = 0;
        double PurchaseQty = 0, SalesQty = 0, BalanceQty = 0;
        string ItemName = "", CompanyName = "";

        private void FillGrid()
        {
            try
            {
                dataGridView1.Rows.Clear();
                dgvItemRow = 0;

                DataSet dsPurchase = new DataSet();
                DataSet dsSales = new DataSet();

                if (!SearchFlag)
                    objBL.Query = "select IPQ.ItemId,I.ItemName,IPQ.Quantity from ItemQuantity IPQ inner join Item I on I.ID=IPQ.ItemId where IPQ.CancelTag=0 and I.CancelTag=0";
                else
                    objBL.Query = "select IPQ.ItemId,I.ItemName,IPQ.Quantity from ItemQuantity IPQ inner join Item I on I.ID=IPQ.ItemId where IPQ.CancelTag=0 and I.CancelTag=0 and I.ItemName like '%" + txtSearchItemName.Text + "%' order by I.ItemName";

                //objBL.Query = "select IPQ.ItemId,I.ItemName,IPQ.Quantity,M.ManufracturerName,I.ItemName as [Item Name],I.BatchNumber,I.HSNCode,I.UOM,I.Cost,I.Price,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from Item I inner join Manufracturer M on I.ManufracturerId=M.ID where I.CancelTag=0";

                dsPurchase = objBL.ReturnDataSet();

                if (dsPurchase.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsPurchase.Tables[0].Rows.Count; i++)
                    {
                        ItemId = 0; PurchaseQty = 0; SalesQty = 0; BalanceQty = 0;
                        if (!string.IsNullOrEmpty(Convert.ToString(dsPurchase.Tables[0].Rows[0]["ItemId"])))
                        {
                            ItemId = Convert.ToInt32(dsPurchase.Tables[0].Rows[i]["ItemId"].ToString());
                            //CompanyName = dsPurchase.Tables[0].Rows[i]["CompanyName"].ToString();
                            ItemName = dsPurchase.Tables[0].Rows[i]["ItemName"].ToString();
                            PurchaseQty = Convert.ToDouble(dsPurchase.Tables[0].Rows[i]["Quantity"].ToString());

                            //SalesQty = 0;
                            //objBL.Query = "select SaleQuantity from ItemSaleQuantity where CancelTag=0 and ItemId=" + ItemId + "";
                            //dsSales = objBL.ReturnDataSet();

                            //if (dsSales.Tables[0].Rows.Count > 0)
                            //{
                            //    if (!string.IsNullOrEmpty(Convert.ToString(dsSales.Tables[0].Rows[0]["SaleQuantity"])))
                            //        SalesQty = Convert.ToDouble(dsSales.Tables[0].Rows[0]["SaleQuantity"]);
                            //    else
                            //        SalesQty = 0;
                            //}

                            //BalanceQty = 0;
                            //BalanceQty = PurchaseQty - SalesQty;

                            if (PurchaseQty > 0)
                            {
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows[dgvItemRow].Cells["clmId"].Value = ItemId;
                                //dataGridView1.Rows[dgvItemRow].Cells["clmCompany"].Value = CompanyName;
                                dataGridView1.Rows[dgvItemRow].Cells["clmItemName"].Value = ItemName;
                                dataGridView1.Rows[dgvItemRow].Cells["clmQty"].Value = PurchaseQty.ToString();
                                dgvItemRow++;
                            }
                        }
                    }
                    SrNo_Add();
                }

                //dataGridView1.Rows.Clear();
                //DataSet dsItem = new DataSet();

                //if (SearchFlag == false)
                //    objBL.Query = "select I.ID, (SELECT COUNT(*) FROM Item I2 WHERE I2.ID <= I.ID and I.CancelTag=0 and I2.CancelTag=0) AS SrNo,I.CompanyName,I.ItemName as [Item Name],I.ItemCode,I.HSNCode,I.Description,I.UOMID,U.UnitOfMessurement as [UOM],I.CGST,I.SGST,I.IGST,I.Cost,I.ItemPrice,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount from Item I inner join UOM U on U.ID=I.UOMID where I.CancelTag=0 and U.CancelTag=0 order by I.ID";
                //else
                //    objBL.Query = "select I.ID, (SELECT COUNT(*) FROM Item I2 WHERE I2.ID <= I.ID and I.CancelTag=0 and I2.CancelTag=0) AS SrNo,I.CompanyName,I.ItemName as [Item Name],I.ItemCode,I.HSNCode,I.Description,I.UOMID,U.UnitOfMessurement as [UOM],I.CGST,I.SGST,I.IGST,I.Cost,I.ItemPrice,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount from Item I inner join UOM U on U.ID=I.UOMID where I.ItemName like '%" + txtSearchItemName.Text + "%' and I.CancelTag=0 and U.CancelTag=0 order by I.ID";

                //dsItem = objBL.ReturnDataSet();

                //if (dsItem.Tables[0].Rows.Count > 0)
                //{
                //    ItemId = 0; PurchaseQty = 0; SalesQty = 0; BalanceQty = 0;

                //    for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                //    {
                //        ItemId = Convert.ToInt32(dsItem.Tables[0].Rows[i]["ID"].ToString());
                //        CompanyName = dsItem.Tables[0].Rows[i]["CompanyName"].ToString();
                //        ItemName = dsItem.Tables[0].Rows[i]["ItemName"].ToString();

                //        DataSet dsPurchase = new DataSet();
                //        objBL.Query = "select sum(Quantity) as [PurchaseQty] from PurchaseTransaction where ItemId=" + ItemId + " and CancelTag=0";
                //        dsPurchase = objBL.ReturnDataSet();

                //        if (dsPurchase.Tables[0].Rows.Count > 0)
                //        {
                //            if (!string.IsNullOrEmpty(Convert.ToString(dsPurchase.Tables[0].Rows[0]["PurchaseQty"])))
                //                PurchaseQty = Convert.ToInt32(dsPurchase.Tables[0].Rows[0]["PurchaseQty"]);
                //            else
                //                PurchaseQty = 0;
                //        }

                //        if (PurchaseQty != 0)
                //        {

                //            objBL.Query = "select sum(Quantity) as [SaleQty] from SaleTransaction where ItemId=" + ItemId + " and CancelTag=0";
                //            dsSales = objBL.ReturnDataSet();

                //            if (dsSales.Tables[0].Rows.Count > 0)
                //            {
                //                if (!string.IsNullOrEmpty(Convert.ToString(dsSales.Tables[0].Rows[0]["SaleQty"])))
                //                    SalesQty = Convert.ToInt32(dsSales.Tables[0].Rows[0]["SaleQty"]);
                //                else
                //                    SalesQty = 0;
                //            }

                //            BalanceQty = PurchaseQty - SalesQty;

                //            dataGridView1.Rows.Add();
                //            dataGridView1.Rows[dgvItemRow].Cells["clmId"].Value = ItemId;
                //            dataGridView1.Rows[dgvItemRow].Cells["clmCompany"].Value = CompanyName;
                //            dataGridView1.Rows[dgvItemRow].Cells["clmItemName"].Value = ItemName;
                //            dataGridView1.Rows[dgvItemRow].Cells["clmQty"].Value = BalanceQty.ToString();
                //            dgvItemRow++;
                //        }
                //    }
                //    SrNo_Add();
                //}
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void SrNo_Add()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int SrNo = 1;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells["clmSrNo"].Value = SrNo.ToString();
                    SrNo++;
                }
                lblTotalCount.Text = "Total Count: " + dataGridView1.Rows.Count;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        int RowCount = 0;
        private void ExcelReport()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                objRL.FillCompanyData();
                object misValue = System.Reflection.Missing.Value;
                myExcelApp = new Microsoft.Office.Interop.Excel.Application();
                myExcelWorkbooks = myExcelApp.Workbooks;

                objRL.ClearExcelPath();
                objRL.isPDF = true;

                objRL.Form_ExcelFileName = "ItemBalance.xlsx";

                objRL.Form_DestinationReportFilePath = "Item Balance Report\\";

                objRL.Path_Comman();

                myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;


                myExcelWorksheet.get_Range("A1", misValue).Formula = objRL.CI_CompanyName;
                myExcelWorksheet.get_Range("A2", misValue).Formula = "Item Qty Report   Date: " + DateTime.Now.Date.ToString(objRL.SetDateFormat_ForReport);


                RowCount = 4;
                string CellDisplay1 = "";
                int CellCheckCount = dataGridView1.Rows.Count;
                int PasteCount = 0;
                PasteCount = RowCount;

                for (int i = 0; i < CellCheckCount; i++)
                {
                    CellDisplay1 = "A" + RowCount;
                    Excel.Range firstRow = myExcelWorksheet.get_Range(CellDisplay1, misValue);
                    firstRow.EntireRow.Copy(misValue);

                    string CellDisplayP = "A" + PasteCount;
                    Excel.Range firstRow1 = myExcelWorksheet.get_Range(CellDisplayP, misValue);
                    firstRow1.EntireRow.PasteSpecial(XlPasteType.xlPasteAll, XlPasteSpecialOperation.xlPasteSpecialOperationNone, misValue, misValue);
                    PasteCount++;
                }

                RowCount = 4;
                DateTime dt = new DateTime();
                SrNo_Report = 1;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //Sr.No
                    CellDisplay1 = "A" + RowCount;
                    myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = SrNo_Report.ToString();

                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells["clmItemName"].Value)))
                    {
                        //Company Name
                        CellDisplay1 = "B" + RowCount;
                        myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dataGridView1.Rows[i].Cells["clmItemName"].Value.ToString());

                        //Item Name
                        CellDisplay1 = "C" + RowCount;
                        myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dataGridView1.Rows[i].Cells["clmQty"].Value.ToString());

                        //Qty
                        //CellDisplay1 = "D" + RowCount;
                        //myExcelWorksheet.get_Range(CellDisplay1, misValue).Formula = Convert.ToString(dataGridView1.Rows[i].Cells["clmQty"].Value.ToString());
                        RowCount++;
                        SrNo_Report++;
                    }
                }



                myExcelWorkbook.Save();

                string PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");
                const int xlQualityStandard = 0;
                myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);

                myExcelWorkbook.Close(true, misValue, misValue);
                myExcelApp.Quit();

                //MessageBox.Show("Report Generated Successfully");

                //DialogResult dr1;
                //dr1 = MessageBox.Show("Do you want to view this report.?", "Report View", MessageBoxButtons.YesNo);
                //if (dr1 == DialogResult.Yes)
                    System.Diagnostics.Process.Start(PDFReport);
                objRL.DeleteExcelFile();
            }
            else
            {
                MessageBox.Show("No Record Found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected void DrawBorder(Range Functionrange)
        {
            Borders borders = Functionrange.Borders;
            //borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;
            borders.Weight = 1D;
        }

        bool AmountFlag = false;
        protected void Fill_Merge_Cell(string Cell1, string Cell2, object val1, Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet, string SetValue)
        {
            Cell1 = Cell1 + RowCount;
            Cell2 = Cell2 + RowCount;
            Microsoft.Office.Interop.Excel.Range AlingRange1 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange1.Merge(val1);
            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            Microsoft.Office.Interop.Excel.Range AlingRange2 = myExcelWorksheet.get_Range(Cell1, Cell2);

            if (AmountFlag == false)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            else
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //DrawBorder(AlingRange2);

            if (MH_Value == true)
            {
                AlingRange2.RowHeight = 40;
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                AlingRange2.WrapText = true;
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ExcelReport();
        }
    }
}
