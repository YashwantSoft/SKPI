using BusinessLayerUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SPApplication.Backup
{
    public partial class ScheduleServerBackup : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        string SourceDirNameCopy = string.Empty;
        string BackupFullPath = string.Empty;
        string TodayDate = string.Empty;
        string BackupFolderName = string.Empty;
        int BackupId = 0;
        string SeverFolderPath = string.Empty;
        string DestinationFolderPath = string.Empty;
        int IndexGrid = 0;
        static int DataGridIndex;
        int SrNo = 1;
        int TotalCount = 0;
        public ScheduleServerBackup()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_ADDBACKUPFOLDER);
            btnAddGrid.BackColor = objDL.GetBackgroundColor();
            btnClearGrid.BackColor = objDL.GetBackgroundColor();
            btnAddGrid.ForeColor = objDL.GetForeColor();
            btnClearGrid.ForeColor = objDL.GetForeColor();
            objDL.SetButtonDesign(btnBrowse, BusinessResources.BTN_BROWSE);
            objDL.SetButtonDesign(btnBrowseDestination, BusinessResources.BTN_BROWSE);
            //objDL.SetButtonDesign(btnBrowse, BusinessResources.BTN_BROWSEEXCELFILE);
        }

        //public static string GetLocalIPAddress()
        //{
        //    var host = Dns.GetHostEntry(Dns.GetHostName());
        //    foreach (var ip in host.AddressList)
        //    {
        //        if (ip.AddressFamily == AddressFamily.InterNetwork)
        //        {
        //            return ip.ToString();
        //        }
        //    }
        //    throw new Exception("No network adapters with an IPv4 address in the system!");
        //}

        private void Fill_IP_Address()
        {
            //string ipAddress = "";
            //ipAddress = GetLocalIPAddress();

            //IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

            //if (Dns.GetHostAddresses(Dns.GetHostName()).Length > 0)
            //{
            //    ipAddress = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
            //}
        }

        private void AddBackupFolder_Load(object sender, EventArgs e)
        {
            ViewBackupDetails();
            txtAddBackupFolder.Focus();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            SeverFolderPath = string.Empty;
            OpenFileDialog objOFD = new OpenFileDialog(); //open dialog to choose file  
            FolderBrowserDialog objFBD = new FolderBrowserDialog();
            
            if (objFBD.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
            {
                //string directoryPath = Path.GetDirectoryName(file);
                txtAddBackupFolder.Text = objFBD.SelectedPath.ToString();
                SeverFolderPath = objFBD.SelectedPath.ToString();
                //filePath = file.InitialDirectory.ToString();
                //txtAddBackupFolder.Text = filePath.ToString();
            }

            //if (objOFD.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
            //{
            //    //string directoryPath = Path.GetDirectoryName(file);
            //   //// file
            //    SeverFolderPath = objOFD.InitialDirectory.ToString();
            //    txtAddBackupFolder.Text = SeverFolderPath.ToString();
            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool ValidationMain()
        {
            objEP.Clear();

            if (dgv.Rows.Count == 0)
            {
                dgv.Focus();
                objEP.SetError(dgv, "Add Paths");
                return true;
            }
            else if (txtDestinationPath.Text == "")
            {
                txtDestinationPath.Focus();
                objEP.SetError(txtDestinationPath, "Enter Destination Path");
                return true;
            }
            else
                return false;
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidationMain())
            {
                TotalCount = dgv.Rows.Count;

                if (BackupId == 0)
                {
                    objBL.Query = "insert into ScheduleServerBackup(EntryDate,EntryTime,BackupTime,DestinationPath,TotalCount) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "','" + dtpBackupTime.Value.ToShortTimeString() + "','" + DestinationFolderPath.ToString() + "'," + TotalCount + ")";
                    objBL.Function_ExecuteNonQuery();
                    BackupId = objRL.Return_Transaction_ID("ScheduleServerBackup");
                }
                else
                {
                    objBL.Query = "update ScheduleServerBackup set BackupTime='" + dtpBackupTime.Value.ToShortTimeString() + "',DestinationPath='" + DestinationFolderPath.ToString() + "' ,TotalCount=" + TotalCount + " where ID=" + BackupId + "";
                    objBL.Function_ExecuteNonQuery();
                    objBL.Query = "Delete from ScheduleServerBackupTransaction where ScheduleServerBackupId=" + BackupId + " and CancelTag=0";
                    objBL.Function_ExecuteNonQuery();
                }
                
                if (dgv.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        SeverFolderPath = Convert.ToString(dgv.Rows[i].Cells[2].Value.ToString());
                        BackupFolderName = Convert.ToString(dgv.Rows[i].Cells[3].Value.ToString());
                        objBL.Query = "insert into ScheduleServerBackupTransaction(ScheduleServerBackupId,BackupFolderName,BackupPath) values(" + BackupId + ",'" + BackupFolderName + "','" + SeverFolderPath + "')";
                        objBL.Function_ExecuteNonQuery();
                    }
                    objRL.ShowMessage(7, 1);
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
            ViewBackupDetails();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void ClearAll()
        {
            SourceDirNameCopy = string.Empty;
            BackupFullPath = string.Empty;
            TodayDate = string.Empty;
            BackupFolderName = string.Empty;
            BackupId = 0;
            SeverFolderPath = string.Empty;
            DestinationFolderPath = string.Empty;
            IndexGrid = 0;
            DataGridIndex = 0;
            SrNo = 1;
            TotalCount = 0;
            dtpDate.Value = DateTime.Now.Date;
            dtpTime.Value = DateTime.Now;
            txtAddBackupFolder.Text = "";
            txtFolderName.Text = "";
            dgv.Rows.Clear();
            lblCount.Text = "";
            //ViewBackupDetails();
        }

        private void FillSRNO()
        {
            SrNo = 1;
            if (dgv.Rows.Count > 0)
            {
                lblCount.Text = "Total Count : " + dgv.Rows.Count.ToString();
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].Cells[0].Value = "Delete";
                    dgv.Rows[i].Cells[1].Value = SrNo.ToString();
                    SrNo++;
                }
            }
        }

        private void Add_In_DataGridView()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(txtAddBackupFolder.Text)))
            {
                if (dgv.Rows.Count > 0)
                    DataGridIndex = dgv.Rows.Count;
                else
                    DataGridIndex = 0;

                dgv.Rows.Add();
                dgv.Rows[DataGridIndex].Cells[0].Value = "Delete";
                dgv.Rows[DataGridIndex].Cells[2].Value = txtAddBackupFolder.Text.ToString();
                dgv.Rows[DataGridIndex].Cells[3].Value = txtFolderName.Text.ToString();
                DataGridIndex++;
                FillSRNO();
                ClearAllGridItem();
            }
        }

        private void ClearAllGridItem()
        {
            DataGridIndex = 0;
            txtAddBackupFolder.Text = "";
            txtFolderName.Text = "";
        }

        protected bool Validation()
        {
            objEP.Clear();
            if (txtAddBackupFolder.Text == "")
            {
                txtAddBackupFolder.Focus();
                objEP.SetError(btnBrowse, "Select Backup Folder");
                return true;
            }
            else if (txtFolderName.Text == "")
            {
                txtFolderName.Focus();
                objEP.SetError(txtFolderName, "Enter Backup Folder Name");
                return true;
            }
            else
                return false;
        }

        private bool CheckExist()
        {
            bool ReturnFlag = false;
            DataSet ds = new DataSet();
            objBL.Query = "select ID,ScheduleServerBackupId,BackupFolderName,BackupPath from ScheduleServerBackupTransaction where CancelTag=0 and ScheduleServerBackupId=" + BackupId + " and BackupPath='" + SeverFolderPath + "'";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
                    ReturnFlag = true;
                else
                    ReturnFlag = false;
            }
            else
                ReturnFlag = false;

            return ReturnFlag;
        }

        private bool CheckExistPath()
        {
            bool ReturnFlag = false;

            if (!string.IsNullOrEmpty(Convert.ToString(txtAddBackupFolder.Text)))
                SeverFolderPath = Convert.ToString(txtAddBackupFolder.Text);

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (SeverFolderPath == Convert.ToString(dgv.Rows[i].Cells[2].Value))
                {
                    ReturnFlag =true;
                    break;
                }
                else
                    ReturnFlag = false;
            }
            return ReturnFlag;
        }

        private void SaveDB()
        {
            if (!Validation())
            {
                if (!CheckExistPath())
                    Add_In_DataGridView();
                else
                {
                    txtAddBackupFolder.Text = "";
                    txtFolderName.Text = "";
                    objRL.ShowMessage(12, 9);
                    return;
                }
                
                //if (!CheckExist())
                //{
                //    objBL.Query = "insert into ScheduleServerBackupTransaction(ScheduleServerBackupId,BackupPath) values(" + BackupId + ",'" + SeverFolderPath + "')";
                //    objBL.Function_ExecuteNonQuery();

                //    FillGrid();
                //    ClearAll();
                //    objRL.ShowMessage(7, 1);
                //}
                //else
                //{
                //    objRL.ShowMessage(12, 9);
                //    return;
                //}
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void FillGrid()
        {
            dgv.Rows.Clear();
            DataSet ds = new DataSet();
            objBL.Query = "select ID,ScheduleServerBackupId,BackupFolderName,BackupPath from ScheduleServerBackupTransaction where CancelTag=0 and ScheduleServerBackupId=" + BackupId + "";
            ds=objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                SrNo = 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["BackupPath"])))
                    {
                        dgv.Rows.Add();
                        dgv.Rows[i].Cells[0].Value = "Delete";
                        dgv.Rows[i].Cells[1].Value = SrNo.ToString();
                        dgv.Rows[i].Cells[2].Value = Convert.ToString(ds.Tables[0].Rows[i]["BackupPath"]);
                        dgv.Rows[i].Cells[3].Value = Convert.ToString(ds.Tables[0].Rows[i]["BackupFolderName"]);
                        SrNo++;
                    }
                }
            }
        }

        private void btnAddGrid_Click(object sender, EventArgs e)
        {
            SaveDB();
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            txtAddBackupFolder.Text = "";
            txtFolderName.Text = "";
        }



        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                DialogResult dr;
                dr = objRL.ReturnDialogResult_Delete();
                if (dr == DialogResult.Yes)
                {
                    dgv.Rows.RemoveAt(e.RowIndex);
                    FillSRNO();
                    objRL.ShowMessage(9, 1);
                }
            }
        }

        private void btnBrowseDestination_Click(object sender, EventArgs e)
        {
            DestinationFolderPath = string.Empty;
            OpenFileDialog objOFD = new OpenFileDialog(); //open dialog to choose file  
            FolderBrowserDialog objFBD = new FolderBrowserDialog();

            if (objFBD.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
            {
                //string directoryPath = Path.GetDirectoryName(file);
                txtDestinationPath.Text = objFBD.SelectedPath.ToString();
                DestinationFolderPath = objFBD.SelectedPath.ToString();
                //filePath = file.InitialDirectory.ToString();
                //txtAddBackupFolder.Text = filePath.ToString();
            }

            //if (objOFD.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
            //{
            //    //string directoryPath = Path.GetDirectoryName(file);
            //   //// file
            //    SeverFolderPath = objOFD.InitialDirectory.ToString();
            //    txtAddBackupFolder.Text = SeverFolderPath.ToString();
            //}
        }

        private bool CheckExistBackup()
        {
            bool ReturnFlag = false;
            DataSet ds = new DataSet();
            objBL.Query="Select * from ScheduleServerBackup where CancelTag=0";
            ds=objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if(!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0][0])))
                    ReturnFlag = true;
                else
                    ReturnFlag = false;
            }
            else
                ReturnFlag = false;

            return ReturnFlag;
        }

        private void btnAddBackup_Click(object sender, EventArgs e)
        {
            gbListOfBackup.Enabled = true;

            if (!CheckExistBackup())
            {
                gbListOfBackup.Visible = true;
                objBL.Query = "insert into ScheduleServerBackup(EntryDate,EntryTime,BackupTime,DestinationPath,TotalCount) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "','" + dtpBackupTime.Value.ToShortTimeString() + "','" + DestinationFolderPath.ToString() + "',0)";
                objBL.Function_ExecuteNonQuery();
                BackupId = objRL.Return_Transaction_ID("ScheduleServerBackup");
               // btnBrowse.Focus();
            }
            else
            {
                ViewBackupDetails();
            }
        }
        
        private void ViewBackupDetails()
        {
            ClearAll();
            DataSet ds = new DataSet();
            objBL.Query = "Select ID,EntryDate,EntryTime,BackupTime,DestinationPath,TotalCount from ScheduleServerBackup where CancelTag=0";
            ds=objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
                    BackupId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["EntryDate"])))
                    dtpDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["EntryDate"]);
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["EntryTime"])))
                    dtpTime.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["EntryTime"]);
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["BackupTime"])))
                    dtpBackupTime.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["BackupTime"]);
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DestinationPath"])))
                {
                    DestinationFolderPath = Convert.ToString(ds.Tables[0].Rows[0]["DestinationPath"].ToString());
                    txtDestinationPath.Text = DestinationFolderPath.ToString();
                }
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TotalCount"])))
                    lblCount.Text = "Total Count : " + ds.Tables[0].Rows[0]["TotalCount"].ToString();

                FillGrid();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewBackupDetails();
            TodayDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
           
            DataSet ds = new DataSet();
            objBL.Query = "select ID,ScheduleServerBackupId,BackupFolderName,BackupPath from ScheduleServerBackupTransaction where CancelTag=0 and ScheduleServerBackupId=" + BackupId + "";
            ds=objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                SrNo = 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["BackupPath"])))
                    {
                        //dgv.Rows.Add();
                        //dgv.Rows[i].Cells[0].Value = "Delete";
                        //dgv.Rows[i].Cells[1].Value = SrNo.ToString();
                        dgv.Rows[i].Cells[2].Value = Convert.ToString(ds.Tables[0].Rows[i]["BackupPath"]);
                        string BackupFolderPathCurrent= Convert.ToString(ds.Tables[0].Rows[i]["BackupPath"]);
                        BackupFolderName = Convert.ToString(ds.Tables[0].Rows[i]["BackupFolderName"]);

                        SourceDirNameCopy = string.Empty;
                        DirectoryInfo dir = new DirectoryInfo(BackupFolderPathCurrent);
                        SourceDirNameCopy = dir.Name.ToString();
                        BackupFullPath = DestinationFolderPath + "\\" + TodayDate + "\\" + BackupFolderName + "\\" + SourceDirNameCopy + "\\";
                        DirectoryCopy(BackupFolderPathCurrent, BackupFullPath,true);
                        //CopyFilesRecursively(BackupFolderPathCurrent, BackupFullPath);
                    }
                }
            }
        }

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }



        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void txtAddBackupFolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtFolderName.Focus();
        }

        private void txtFolderName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddGrid.Focus();
        }


    }
}
