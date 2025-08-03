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

namespace SPApplication.Transaction
{
    public partial class QRCodeMake : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        string QRCodeData = string.Empty;
        string QRCodeDataRTB = string.Empty;

        public QRCodeMake()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, "QR Code Creation");
        }

        private void QRCodeMake_Load(object sender, EventArgs e)
        {

        }

        string B1 = string.Empty, B2 = string.Empty, B3 = string.Empty, B4 = string.Empty, B5 = string.Empty;

        string QRImagePath = string.Empty;

        protected bool Validation()
        {
            objEP.Clear();

            if (cmbRackNumber.SelectedIndex == -1)
            {
                objEP.SetError(cmbRackNumber, "Select bRack Number");
                cmbRackNumber.Focus();
                return true;
            }
            else if (cmbType.Text == "")
            {
                objEP.SetError(cmbType, "Select Type");
                cmbType.Focus();
                return true;
            }
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string Information = string.Empty;
            B1 = string.Empty;

            B1 = "http://warehouse.2dkapps.com/FormLayouts/" + cmbType.Text + "?rackNumberQR=" + cmbRackNumber.Text;

            //B1 = "http://warehouse.2dkapps.com/" + cmbType.Text + "?rackNumberQR=" + cmbRackNumber.Text;
            B2 = string.Empty;
            B3 = string.Empty;
            B4 = string.Empty;
            B5 = string.Empty;
            QRImagePath = string.Empty;

            QRCodeData = string.Empty;

            QRCodeData = B1;

            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pbQRCode.Image = qrcode.Draw(QRCodeData.ToString(), 10);
            QRImagePath = objRL.GetPath("ImagePath");
            var filePath = QRImagePath;
            Directory.CreateDirectory(filePath);
            string FileName = cmbRackNumber.Text.ToString();
            pbQRCode.Image.Save(Path.Combine(filePath, FileName), System.Drawing.Imaging.ImageFormat.Png);

            rtbStickerHeader.Text = QRCodeData;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }
    }
}
