using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SPApplication.Master;
using SPApplication.Transaction;
using SPApplication.Backup;
using SPApplication.Report;
using SPApplication.Settings;
using SPApplication.KanBan;

namespace SPApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginWindow());
            //Application.Run(new BackupEXE());
            //Application.Run(new RND());
        }
    }
}
