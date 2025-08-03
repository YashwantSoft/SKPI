using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using SPApplication;
using SPApplication.Authentication;
using SPApplication.Settings;
using System.IO;
using SPApplication.Transaction;
using SPApplication.Backup;

namespace SPApplication
{
    public partial class SettingsList : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        ToolTip objTT = new ToolTip();

        public SettingsList()
        {
            InitializeComponent();
            lbReportList.ForeColor = objDL.GetBackgroundColor();
            objDL.SetButtonDesign(btnExit, BusinessResources.BTN_EXIT);
            objDL.SetLabelDesign(lblHeader, BusinessResources.LBL_HEADER_SETTINGSLIST);
            listView1_Refresh();
        }

        private void lbReportList_Click(object sender, EventArgs e)
        {
            Select_Report();
        }

        private void lbReportList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Select_Report();
        }

        private void listView1_Refresh()
        {
             
        }

        private void Select_Report()
        {
            if (lbReportList.Items.Count > 0)
            {
                if (lbReportList.Text == "Users")
                {
                    if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN)
                    {
                        Users objForm = new Users();
                        objForm.ShowDialog(this);
                    }
                    else
                    {
                        objRL.ShowMessage(30, 4);
                    }
                }
                else if (lbReportList.Text == "Change Password")
                {
                    ChangePassword objForm = new ChangePassword();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Email Credentials")
                {
                    if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN)
                    {
                        EmailCredentials objForm = new EmailCredentials();
                        objForm.ShowDialog(this);
                    }
                    else
                    {
                        objRL.ShowMessage(30, 4);
                    }
                }
                else if (lbReportList.Text == "Backup")
                {
                    objRL.DBBackup(); objRL.ShowMessage(31,1);
                }
                else if (lbReportList.Text == "Import Tally Data")
                {
                    if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_PRODUCTION || BusinessLayer.UserName_Static == BusinessResources.USER_LOGISTICS || BusinessLayer.UserName_Static == BusinessResources.USER_STORE || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS1)
                    {
                        ImportTallyData objForm = new ImportTallyData();
                        objForm.ShowDialog(this);
                    }
                    else
                    {
                        objRL.ShowMessage(30, 4);
                    }
                }
                else if (lbReportList.Text == "RK")
                {
                    if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN)
                    {
                        ProductSwitchReason objForm = new ProductSwitchReason();
                        objForm.ShowDialog(this);
                    }
                    else
                    {
                        objRL.ShowMessage(30, 4);
                    }
                }
                else if (lbReportList.Text == "Schedule Server Backup")
                {
                    if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN)
                    {
                        ScheduleServerBackup objForm = new ScheduleServerBackup();
                        objForm.ShowDialog(this);
                    }
                    else
                    {
                        objRL.ShowMessage(30, 4);
                    }
                }
                else if (lbReportList.Text == "Logout")
                {
                    if (!CheckExistLogRecord())
                    {
                        DialogResult dr;
                        dr = objRL.Logout_Record_Show_Message(); // MessageBox.Show("Do yo want to delete this record?", "Delete Record", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            objBL.Query = "update LogRecord set LogoutDate='" + DateTime.Now.Date.ToShortDateString() + "',LogoutTime='" + DateTime.Now.ToShortTimeString() + "' where LoginId=" + BusinessLayer.UserId_Static + " and LoginDate= #" + DateTime.Now.Date.ToShortDateString() + "#";
                            objBL.Function_ExecuteNonQuery();

                        }
                    }
                    Application.Exit();
                }
                else if (lbReportList.Text == "QR Code Creation")
                {
                    QRCodeMake objBL = new QRCodeMake();
                    objBL.ShowDialog(this);
                }
                else
                    MessageBox.Show("Enter Valid selection");
            }
        }

        private bool CheckExistLogRecord()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,LoginId,LoginDate,LoginTime,LogoutDate,LogoutTime from LogRecord where CancelTag=0 and LoginId=" + BusinessLayer.UserId_Static + " and LogoutDate= #" + DateTime.Now.Date.ToShortDateString() + "# ";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void Get_Tally_Data()
        {
            //D:\Projects\Shree Khodiyar Plastic Industries\Documents\Read Excel
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void MenuSettings_Load(object sender, EventArgs e)
        {
            //lbReportList.DefaultCellStyle.SelectionBackColor = GetRowSelectionColor(); 
        }

        private void lbReportList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Graphics g = e.Graphics;
            Brush brush = ((e.State & DrawItemState.Selected) == DrawItemState.Selected) ?
                          Brushes.Red : new SolidBrush(e.BackColor);
            g.FillRectangle(brush, e.Bounds);
            e.Graphics.DrawString(lbReportList.Items[e.Index].ToString(), e.Font,
                     new SolidBrush(e.ForeColor), e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();   
        }

        private void lbReportList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void lbReportList_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    if (e.Index < 0) return;
        //    //if the item state is selected them change the back color
        //    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
        //        e = new DrawItemEventArgs(e.Graphics,
        //                                  e.Font,
        //                                  e.Bounds,
        //                                  e.Index,
        //                                  e.State ^ DrawItemState.Selected,
        //                                  e.ForeColor,
        //                                  objBL.GetBackgroundColor());//Choose the color

        //    // Draw the background of the ListBox control for each item.
        //    e.DrawBackground();
        //    // Draw the current item text
        //    e.Graphics.DrawString(lbReportList.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
        //    // If the ListBox has focus, draw a focus rectangle around the selected item.
        //    e.DrawFocusRectangle();

        //    e.DrawBackground();
        //    Graphics g = e.Graphics;
        //    Brush brush = ((e.State & DrawItemState.Selected) == DrawItemState.Selected) ?
        //                  Brushes.Red : new SolidBrush(e.BackColor);
        //    g.FillRectangle(brush, e.Bounds);
        //    e.Graphics.DrawString(lbReportList.Items[e.Index].ToString(), e.Font,
        //             new SolidBrush(e.ForeColor), e.Bounds, StringFormat.GenericDefault);
        //    e.DrawFocusRectangle();        
        //}
    }
}
