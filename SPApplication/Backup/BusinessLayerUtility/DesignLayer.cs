using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BusinessLayerUtility
{
    public class DesignLayer
    {
        public Color GetBackgroundColor()
        {
            Color color = System.Drawing.ColorTranslator.FromHtml(BusinessResources.BACKGROUND_COLOUR); 
            return color;
        }

        public Color GetForeColor()
        {
            Color color = System.Drawing.ColorTranslator.FromHtml(BusinessResources.FORE_COLOUR);
            return color;
        }

        public void SetButtonDesign(Button btn)
        {
            btn.BackColor = GetBackgroundColor();
            btn.ForeColor = GetForeColor();
            btn.Font = new System.Drawing.Font("Calibri", 10.00F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btn.Size = new System.Drawing.Size(75, 30);
        }

        public void SetLabelDesign(Label lbl,string LableText)
        {
            lbl.BackColor = GetBackgroundColor();
            lbl.ForeColor = GetForeColor();
            lbl.Text = LableText.ToString();
        }

        public void SetDesignMaster(Form frm, Label lbl, Button btnSave, Button btnClear, Button btnDelete, Button btnExit, string LableText)
        {
            SetButtonDesign(btnSave);
            btnSave.Text = BusinessResources.BTN_SAVE;

            SetButtonDesign(btnSave);
            btnClear.Text = BusinessResources.BTN_CLEAR;

            SetButtonDesign(btnSave);
            btnDelete.Text = BusinessResources.BTN_DELETE;

            SetButtonDesign(btnSave);
            btnExit.Text = BusinessResources.BTN_EXIT;

            SetLabelDesign(lbl, LableText);
            

            DataGridView dgv = frm.Controls.Find("dataGridView1", true).FirstOrDefault() as DataGridView;
            if (dgv != null)
                dgv.DefaultCellStyle.SelectionBackColor = GetBackgroundColor();
        }
    }
}
