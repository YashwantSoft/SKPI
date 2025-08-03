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
    public partial class BackupEXE : Form
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

        public BackupEXE()
        {
            InitializeComponent();
        }

        private void BackupEXE_Load(object sender, EventArgs e)
        {
            //btnExit.Enabled = false;
            //pb.Style = ProgressBarStyle.Marquee;
            //pb.Show();

            //// DO THE WORK ON A DIFFERENT THREAD:
            //await(Task.Run(() =>
            //{
            //    System.Threading.Thread.Sleep(5000); // simulated work (remove this and uncomment the two lines below)
            //    //LoadDatabase.Groups();
            //    //ProcessResults.GroupOne();
                

            //}));

            //// END PROGRESS BAR MOVEMENT:
            //pb.Hide();
            //btnExit.Enabled = true;

           RunBackup();
            //InProgressBar();
            //this.Hide();
        }

    //    private async void btnExit_Click(object sender, EventArgs e)
    //{
    //    //private async void InProgressBar()
    //    //{
    //        // START PROGRESS BAR MOVEMENT:
    //        btnExit.Enabled = false;
    //        pb.Style = ProgressBarStyle.Marquee;
    //        pb.Show();

    //        // DO THE WORK ON A DIFFERENT THREAD:
    //        await  (Task.Run(() =>
    //        {
    //            System.Threading.Thread.Sleep(5000); // simulated work (remove this and uncomment the two lines below)
    //            //LoadDatabase.Groups();
    //            //ProcessResults.GroupOne();
    //            RunBackup();
    //        }));

    //        // END PROGRESS BAR MOVEMENT:
    //        pb.Hide();
    //        btnExit.Enabled = true;
    //    }

        private void ViewBackupDetails()
        {

            DataSet ds = new DataSet();
            objBL.Query = "Select ID,EntryDate,EntryTime,BackupTime,DestinationPath,TotalCount from ScheduleServerBackup where CancelTag=0";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
                    BackupId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DestinationPath"])))
                    DestinationFolderPath = Convert.ToString(ds.Tables[0].Rows[0]["DestinationPath"].ToString());
            }
        }

        private void RunBackup()
        {
            try
            {
                using (new CursorWait())
                {
                    this.timer1.Start();
                    ViewBackupDetails();
                    TodayDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                    //TodayDate = "ServerBackupDaily";

                    DataSet ds = new DataSet();
                    objBL.Query = "select ID,ScheduleServerBackupId,BackupFolderName,BackupPath from ScheduleServerBackupTransaction where CancelTag=0 and ScheduleServerBackupId=" + BackupId + "";
                    ds = objBL.ReturnDataSet();
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
                                //dgv.Rows[i].Cells[2].Value = Convert.ToString(ds.Tables[0].Rows[i]["BackupPath"]);
                                string BackupFolderPathCurrent = Convert.ToString(ds.Tables[0].Rows[i]["BackupPath"]);
                                BackupFolderName = Convert.ToString(ds.Tables[0].Rows[i]["BackupFolderName"]);

                                SourceDirNameCopy = string.Empty;
                                DirectoryInfo dir = new DirectoryInfo(BackupFolderPathCurrent);
                                SourceDirNameCopy = dir.Name.ToString();
                                BackupFullPath = DestinationFolderPath + "\\" + TodayDate + "\\" + BackupFolderName + "\\" + SourceDirNameCopy + "\\";
                                DirectoryCopy(BackupFolderPathCurrent, BackupFullPath, true);
                                //CopyFilesRecursively(BackupFolderPathCurrent, BackupFullPath);
                            }
                        }
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            try
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
            catch (Exception ex1)
            {
                //MessageBox.Show(ex1.ToString());

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

        private void Calculate(int i)
        {
            double pow = Math.Pow(i, i);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            for (int j = 0; j < 10000; j++)
            {
                Calculate(j);
                backgroundWorker.ReportProgress((j * 10) / 100000);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pb.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // TODO: do something with final calculation.
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.pb.Increment(1000);
        }

     
    }
}
