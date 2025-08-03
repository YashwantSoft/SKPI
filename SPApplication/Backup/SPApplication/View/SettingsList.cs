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

namespace SPApplication
{
    public partial class SettingsList : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        ToolTip objTT = new ToolTip();

        public SettingsList()
        {
            InitializeComponent();
            objBL.Set_List_Desing(this, lblHeader, btnExit, BusinessResources.LBL_HEADER_SETTINGSLIST);
            lbReportList.ForeColor = objBL.GetBackgroundColor();
            //lbReportList.HighlightBackgroundColor = objBL.GetBackgroundColor();
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
                    Users objForm = new Users();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Change Password")
                {
                    ChangePassword objForm = new ChangePassword();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Email Credentials")
                {
                    EmailCredentials objForm = new EmailCredentials();
                    objForm.ShowDialog(this);
                }
                else if (lbReportList.Text == "Backup")
                {
                    objRL.DBBackup();
                }
                else if (lbReportList.Text == "Item Stock Entry")
                {
                    OpeningStock objForm = new OpeningStock();
                    objForm.ShowDialog(this);
                }
                else
                    MessageBox.Show("Enter Valid selection");
            }
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
