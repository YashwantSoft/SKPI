using BusinessLayerUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPApplication.Transaction
{
    public partial class ProductSwitchReason : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;

        public ProductSwitchReason()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_PRODUCUSWITCHREASON);
        }

        private void Test_Load(object sender, EventArgs e)
        {
            //string QRCodeData = string.Empty; string QRImagePath = string.Empty;
            ////QRCodeData = "http://krishnarameshwarghatwai.com/";
            //QRCodeData = "http://madhuraagrotourism.com/img/MadhuraAgroTourismMenuCard.pdf";
            //Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            //pbQRCode.Image = qrcode.Draw(QRCodeData.ToString(), 10);
            //QRImagePath = objRL.GetPath("ImagePath");
            //var filePath = QRImagePath;
            //Directory.CreateDirectory(filePath);
            //string FileName = "R.png";
            //pbQRCode.Image.Save(Path.Combine(filePath, FileName), System.Drawing.Imaging.ImageFormat.Png);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbReason.SelectedIndex > -1 && !string.IsNullOrEmpty(Convert.ToString(txtNotes.Text)))
            {
                objRL.Reason = cmbReason.Text;
                objRL.ReasonInDetails = txtNotes.Text.ToString();
                this.Dispose();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
