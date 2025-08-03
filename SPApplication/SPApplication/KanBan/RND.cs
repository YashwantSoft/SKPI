using BusinessLayerUtility;
using BusinessLayerUtility.TableClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPApplication.KanBan
{
    public partial class RND : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();
        ErrorProvider objEP = new ErrorProvider();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false, IDFlag = false;

        public RND()
        {
            InitializeComponent();
        }

        private void RND_Load(object sender, EventArgs e)
        {
            //DataGridViewComboBoxColumn comboBoxColumn = (DataGridViewComboBoxColumn)dgvP.Columns[0];
            //comboBoxColumn.DataSource = Student.GetStudents();
            //comboBoxColumn.DisplayMember = "Name";
            //comboBoxColumn.ValueMember = "StudentId";


            BindProducts();
        }

        private DataTable GetProduct()
        {
            DataTable dt = new DataTable();
            objBL.Query = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard from Product where CancelTag=0 order by ProductName asc";
            dt = objBL.ReturnDataTable();
            return dt;
        }

        private void BindProducts()
        {
           // var products = GetProducts();

            var combobox = (DataGridViewComboBoxColumn)dataGridView1.Columns["Category"];
            combobox.DisplayMember = "ProductName";
            combobox.ValueMember = "ID";
            combobox.DataSource = GetProduct();

            //dataGridView1.DataSource = products;

            //Original COde
            //var products = GetProducts();

            //var combobox = (DataGridViewComboBoxColumn)dataGridView1.Columns["Category"];
            //combobox.DisplayMember = "Name";
            //combobox.ValueMember = "ID";
            //combobox.DataSource = GetCategories();

            //dataGridView1.DataSource = products;

        }

        private List<ProductClassNew> GetProducts()
        {
            return new List<ProductClassNew>
            {
                new ProductClassNew{Name="Engine Oil",Price=2311.20,CategoryID=1},
                new ProductClassNew{Name="Break Oil",Price=2311.20,CategoryID=1},
                new ProductClassNew{Name="Filter",Price=540.4,CategoryID=2},
                new ProductClassNew{Name="Shaft",Price=143.20,CategoryID=2}
            };
        }

        private List<Category> GetCategories()
        {
            return new List<Category>()
            {
                new Category{Name="Oil",ID=1},
                new Category{Name="Vehicle",ID=2}
            };
        }

        public class Student
        {
            public string Name { get; private set; }
            public int StudentId { get; private set; }
            public Student(string name, int studentId)
            {
                Name = name;
                StudentId = studentId;
            }

            private static readonly List<Student> students = new List<Student>
    {
        { new Student("Chuck", 1) },
        { new Student("Bob", 2) }
    };

            public static List<Student> GetStudents()
            {
                return students;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<ProductClassNew> objPC = (List<ProductClassNew>)dgvP.DataSource;
            objPC.Add(new ProductClassNew());
            dgvP.DataSource = null;
            dgvP.DataSource = objPC;

            //0 ID,
            //1 ProductType,
            //2 ProductName

            dgvP.Columns[0].Width = 100;
            dgvP.Columns[1].Width = 300;
            dgvP.Columns[2].Width = 400;
        }

        string ProductType = string.Empty;

        private void dgvP_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var nvalue = dgvP.CurrentRow.Index;

            if (!string.IsNullOrEmpty(Convert.ToString(dgvP.Rows[nvalue].Cells[0].Value)))
            {
                TextBox textBox = e.Control as TextBox;

                var newColIndex = dgvP.Rows[nvalue].Cells[0].Value.ToString();

                ProductType = newColIndex.ToString();

                //var colIndex = dgvP.CurrentCell.ColumnIndex;
                //var colName = dgvP.Columns[colIndex].Name;

                if (textBox != null)
                {
                    //TextBox textBox = (TextBox)e.Control;
                    textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    textBox.AutoCompleteCustomSource = GetProductNames();
                    textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
            }
            else
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.AutoCompleteMode = AutoCompleteMode.None;
                }
            }
        }

        private AutoCompleteStringCollection GetProductNames()
        {
            string[] postSource = GetDataFromTable();
            AutoCompleteStringCollection objASC = new AutoCompleteStringCollection();
            objASC.AddRange(postSource);
            return objASC;
        }

        string QueryNew = string.Empty;
        string SetName = string.Empty;

        private string[] GetDataFromTable()
        {
            objBL.Connect();
            DataTable dtPosts = new DataTable();
            using (OleDbConnection conn = new OleDbConnection(objBL.conString))
            {
                if (ProductType == "Product")
                {
                    SetName = "ProductName";
                    QueryNew = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard from Product where CancelTag=0 order by ProductName asc";
                }
                else if (ProductType == "Cap")
                {
                    SetName = "CapName";
                    QueryNew = "select ID,CapName from CapMaster where CancelTag=0 order by ID";
                }
                else if (ProductType == "Wad")
                {
                    SetName = "WadName";
                    QueryNew = "select ID,WadName from WadMaster where CancelTag=0 order by WadName asc";
                }
                else
                    QueryNew = "";

                conn.Open();
                using (OleDbDataAdapter adapt = new OleDbDataAdapter(QueryNew, conn))
                {
                    adapt.SelectCommand.CommandTimeout = 120;
                    adapt.Fill(dtPosts);
                }
            }

            //ProductType 
            //use LINQ method syntax to pull the Title field from a DT into a string array...
            string[] postSource = dtPosts
                                .AsEnumerable()
                                .Select<System.Data.DataRow, String>(x => x.Field<String>(SetName))
                                .ToArray();

            return postSource;
            //var source = new AutoCompleteStringCollection();
            //source.AddRange(postSource);
            //textBox1.AutoCompleteCustomSource = source;
            //textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        //    if (e.ColumnIndex == 2)
        //    {
        //        //var combobox = (DataGridViewComboBoxColumn)dataGridView1.Columns["Category"];

        //        //string SelectedText = Convert.ToString((DataGridView1.Rows[0].Cells["dgcombocell"] as DataGridViewComboBoxCell).FormattedValue.ToString());
        //        //int SelectedVal = Convert.ToInt32(DataGridView1.Rows[0].Cells["dgcombocell"].Value);

        //        //ComboBox cmb = (ComboBox)dataGridView1.Columns["Category");
        //        //dataGridView1.Rows[e.RowIndex].Cells["clmTest"].Value = combobox.

        //       int SelectedVal = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Category"].Value);
        //       dataGridView1.Rows[e.RowIndex].Cells["clmTest"].Value = SelectedVal.ToString();
        //    }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Category")
            {
                //your code goes here

                int SelectedVal = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Category"].Value);
                dataGridView1.Rows[e.RowIndex].Cells["clmTest"].Value = SelectedVal.ToString();
            }
        }

        private bool _canUpdate = true;

        private bool _needUpdate = false;

        //If text has been changed then start timer
        //If the user doesn't change text while the timer runs then start search

        private void cmbProductName_TextChanged(object sender, EventArgs e)
        {
            //if (_needUpdate)
            //{
            //    if (_canUpdate)
            //    {
            //        _canUpdate = false;
            //        UpdateData();
            //    }
            //    else
            //    {
            //        RestartTimer();
            //    }
            //}
        }

        ////While timer is running don't start search
        ////timer1.Interval = 1500;
        //private void RestartTimer()
        //{
        //    timer1.Stop();
        //    _canUpdate = false;
        //    timer1.Start();
        //}

        ////Update data when timer stops
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    _canUpdate = true;
        //    timer1.Stop();
        //    UpdateData();
        //}

        private void UpdateData()
        {
            //if (cmbProductName.Text.Length > 1)
            //{
            //    List<string> searchData = Search.GetData(cmbProductName.Text);
            //    HandleTextChanged(searchData);
            //}
        }


        //Update combobox with new data
        private void HandleTextChanged(List<string> dataSource)
        {
            var text = cmbProductName.Text;

            if (dataSource.Count() > 0)
            {
                cmbProductName.DataSource = dataSource;

                var sText = cmbProductName.Items[0].ToString();
                cmbProductName.SelectionStart = text.Length;
                cmbProductName.SelectionLength = sText.Length - text.Length;
                cmbProductName.DroppedDown = true;
                return;
            }
            else
            {
                cmbProductName.DroppedDown = false;
                cmbProductName.SelectionStart = text.Length;
            }
        }
    }
}
