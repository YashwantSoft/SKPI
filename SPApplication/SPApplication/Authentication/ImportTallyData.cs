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
using System.Data.OleDb;
using System.Runtime.InteropServices;


namespace SPApplication.Authentication
{
    public partial class ImportTallyData : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        string ReportName = string.Empty;
        int TotalExist = 0, TotalCount = 0, TotalNewArrival = 0;
        int SrNo = 1;
        int rCnt = 0;
        int cCnt = 0;
        int rw = 0;
        int cl = 0;
        int ProductiId = 0;
        string ProductName = string.Empty;
        string ProductType = string.Empty;
        string Status = string.Empty;
        int StatusValue = 0;   //1 Exists   //0-New Product     //2-Spelling Error
        static int GridRowCount;
        string WorksheetName = string.Empty;


        public ImportTallyData()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_GETTALLYDATA);
            btnBrowseExcelFile.Text = BusinessResources.BTN_BROWSEEXCELFILE;
            btnBrowseExcelFile.BackColor = objDL.GetBackgroundColor();
            btnBrowseExcelFile.ForeColor = objDL.GetForeColor();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ClearAll()
        {
            TotalExist = 0; TotalCount = 0; TotalNewArrival = 0;
            txtExcelFilePath.Text = "";
            dgvProduct.Rows.Clear();
            rCnt = 0;
            cCnt = 0;
            rw = 0;
            cl = 0;
            ProductiId = 0;
            ProductName = string.Empty;
            Status = string.Empty;
            StatusValue = 0;
            GridRowCount = 0;
            TotalExist = 0; TotalCount = 0; TotalNewArrival = 0;
            lblTotalNewArrivalProductCount.Text = "";
            lblTotalExistCount.Text = "";
            lblTotalCount.Text = "";
        
            WorksheetName = string.Empty;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
            cmbImportType.SelectedIndex = -1;
            TableName = string.Empty;
            ColumnName = string.Empty;
        }

        private void btnBrowseExcelFile_Click(object sender, EventArgs e)
        {
            if (cmbImportType.SelectedIndex > -1)
            {
                ClearAll();
                string filePath = string.Empty;
                string fileExt = string.Empty;
                OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
                if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
                {
                    filePath = file.FileName;
                    txtExcelFilePath.Text = filePath.ToString();

                    //D:\Tally.ERP9\GetData\StkSum.xlsx
                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    Excel.Range range;

                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Open(filePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    WorksheetName = xlWorkSheet.Name.ToString();

                    if (ImportType == "Product" || ImportType == "Preform")
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(WorksheetName)))
                        {
                            if (WorksheetName == "Bottles" || WorksheetName == "P-Bottle Preforms")
                                ProductType = "Bottle";
                            else
                                ProductType = "Jar";
                        }
                    }
                    else
                        ProductType = "-";

                    range = xlWorkSheet.UsedRange;
                    rw = range.Rows.Count;
                    cl = range.Columns.Count;
                    cCnt = 1;
                    for (rCnt = 12; rCnt <= rw; rCnt++)
                    {
                        if (cCnt == 1)
                        {
                            ProductName = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;

                            if (!string.IsNullOrEmpty(ProductName))
                            {
                                if (ProductName.Contains("Grand Total"))
                                    break;
                            }
                            CheckExistProduct();
                            //MessageBox.Show(str);
                        }
                    }
                    xlWorkBook.Close(true, null, null);
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp);

                    if (dgvProduct.Rows.Count > 0)
                    {
                        TotalCount = Convert.ToInt32(dgvProduct.Rows.Count.ToString());
                        foreach (DataGridViewRow row in dgvProduct.Rows)
                            if (Convert.ToInt32(row.Cells["clmStatusNo"].Value) == 0)
                            {
                                row.DefaultCellStyle.BackColor = Color.Lime;
                                TotalNewArrival++;
                            }
                            else
                            {
                                row.DefaultCellStyle.BackColor = Color.Pink;
                                TotalExist++;
                            }

                        lblTotalNewArrivalProductCount.Text = "New Arrival Product Count-" + TotalNewArrival.ToString();
                        lblTotalExistCount.Text = "Exist Product Count-" + TotalExist.ToString();
                        lblTotalCount.Text = "Total Count-" + TotalCount.ToString();

                        if (TotalNewArrival > 0)
                            btnSave.Visible = true;
                        else
                            btnSave.Visible = false;
                    }
                }


                //for (cCnt = 1; cCnt <= cl; cCnt++)
                //{
                //    if (rCnt >= 12)
                //    {

                //    }
                //}

                //for (rCnt = 1; rCnt <= rw;  rCnt++)
                //{
                //    for (cCnt = 1; cCnt  <= cl; cCnt++)
                //    {
                //        if (rCnt >= 12)
                //        {
                //            str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                //            MessageBox.Show(str);
                //        }
                //    }
                //}

                ////D:\Projects\Shree Khodiyar Plastic Industries\Documents\Read Excel

                //string filePath = string.Empty;
                //string fileExt = string.Empty;
                //OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
                //if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
                //{
                //    filePath = file.FileName; //get the path of the file  
                //    fileExt = Path.GetExtension(filePath); //get the file extension  
                //    if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                //    {
                //        try
                //        {
                //            System.Data.DataTable dtExcel = new System.Data.DataTable();
                //            dtExcel = ReadExcel(filePath, fileExt); //read excel file  
                //            dgvProduct.Visible = true;
                //            dgvProduct.DataSource = dtExcel;
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show(ex.Message.ToString());
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error  
                //    }
                //}  
            }
            else
            {
                objRL.ShowMessage(17, 4);
                cmbImportType.Focus();
                return;
            }
        }

        public System.Data.DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            System.Data.DataTable dtexcel = new System.Data.DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [01)FINISHED GOODS$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch { }
            }
            return dtexcel;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvProduct.Rows.Count > 0)
            {
                for (int i = 0; i < dgvProduct.Rows.Count; i++)
                {
                    ProductType = string.Empty;
                    ProductName = string.Empty;
                    if (!string.IsNullOrEmpty(Convert.ToString(dgvProduct.Rows[i].Cells["clmStatusNo"])))
                    {
                        if (Convert.ToInt32(dgvProduct.Rows[i].Cells["clmStatusNo"].Value.ToString()) == 0)
                        {
                            ProductType = dgvProduct.Rows[i].Cells["clmType"].Value.ToString();
                            ProductName = dgvProduct.Rows[i].Cells["clmProductName"].Value.ToString();
                            InsertValuesTable();

                            //objBL.Query = "insert into Product(ProductType,ProductName) values('" + ProductType + "', '" + ProductName.Replace("'", "''") + "' )";
                            //objBL.Function_ExecuteNonQuery();
                        }
                    }
                }
                objRL.ShowMessage(7, 1);
                ClearAll();
            }
        }

        private void lblJarInformation_Click(object sender, EventArgs e)
        {

        }

        private void TallyData_Load(object sender, EventArgs e)
        {

        }

        string TableName = string.Empty, ColumnName = string.Empty;

        private void InsertValuesTable()
        {
            //GetTableNameAndColumnName();
            if (ImportType == "Product")
                objBL.Query = "insert into Product(ProductType,ProductName) values('" + ProductType + "', '" + ProductName.Replace("'", "''") + "' )";
            else if (ImportType == "Preform")
                objBL.Query = "insert into Preform(PreformType,PreformName) values('" + ProductType + "', '" + ProductName.Replace("'", "''") + "' )";
           else if (ImportType == "Customer" || ImportType == "Supplier")
                objBL.Query = "insert into " + TableName + "(" + ColumnName + ",CityId) values('" + ProductName.Replace("'", "''") + "',374 )";
            else if (ImportType == "Other Material")
                objBL.Query = "insert into OtherMaterial(MaterialName) values('" + ProductName.Replace("'", "''") + "')";
            else
                objBL.Query = "insert into " + TableName + "(" + ColumnName + ") values('" + ProductName.Replace("'", "''") + "' )";

            objBL.Function_ExecuteNonQuery();
        }

        private void GetQuery()
        {
            GetTableNameAndColumnName();
            
            if (TableName != "" && ColumnName != "")
                objBL.Query = "select * from " + TableName + " where " + ColumnName + "= '" + ProductName.Replace("'", "''") + "' and CancelTag=0";
        }

        private void CheckExistProduct()
        {
            //Preform
            //Product
            //Cap
            //Wad
            //Customer
            //Supplier
            //Other Material

            StatusValue = 0;
            if (!string.IsNullOrEmpty(ProductName))
            {
                DataSet ds = new DataSet();
                GetQuery();
                //objBL.Query = "select * from Product where ProductName= '" + ProductName.Replace("'", "''") + "' and CancelTag=0";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
                    {
                        StatusValue = 1;
                        ProductiId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    }
                    else
                    {
                        ProductiId = 0;
                        StatusValue = 0;
                    }
                }
                else
                {
                    ProductiId = 0;
                    StatusValue = 0;
                }

                if (StatusValue == 0)
                    Status = "New Arrival";
                else
                    Status = "Exist";

                dgvProduct.Rows.Add();
                dgvProduct.Rows[GridRowCount].Cells["clmSrNo"].Value = SrNo.ToString();
                dgvProduct.Rows[GridRowCount].Cells["clmItemId"].Value = ProductiId.ToString();
                dgvProduct.Rows[GridRowCount].Cells["clmType"].Value = ProductType.ToString();
                dgvProduct.Rows[GridRowCount].Cells["clmProductName"].Value = ProductName.ToString();
                dgvProduct.Rows[GridRowCount].Cells["clmStatus"].Value = Status.ToString();
                dgvProduct.Rows[GridRowCount].Cells["clmStatusNo"].Value = StatusValue.ToString();
                GridRowCount++;
                SrNo++;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        string ImportType = string.Empty;
        private void cmbImportType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetTableNameAndColumnName();
        }

        private void GetTableNameAndColumnName()
        {
            if (cmbImportType.SelectedIndex > -1)
            {
                ImportType = cmbImportType.Text;

                if (ImportType == "Preform")
                {
                    TableName = "Preform";
                    ColumnName = "PreformName";
                }
                else if (ImportType == "Product")
                {
                    TableName = "Product";
                    ColumnName = "ProductName";
                }
                else if (ImportType == "Cap")
                {
                    TableName = "CapMaster";
                    ColumnName = "CapName";
                }
                else if (ImportType == "Wad")
                {
                    TableName = "WadMaster";
                    ColumnName = "WadName";
                }
                else if (ImportType == "Customer")
                {
                    TableName = "Customer";
                    ColumnName = "CustomerName";
                }
                else if (ImportType == "Supplier")
                {
                    TableName = "Supplier";
                    ColumnName = "SupplierName";
                }
                else if (ImportType == "Other Material")
                {
                    TableName = "OtherMaterial";
                    ColumnName = "MaterialName";
                }
                else
                {
                    TableName = string.Empty;
                    ColumnName = string.Empty;
                }
            }
            else
            {
                TableName = string.Empty;
                ColumnName = string.Empty;
            }
        }
    }
}
