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

namespace SPApplication.Planning
{
    public partial class SalesOrderPlanning : Form
    {
        string QRCodeData = string.Empty;
        string QRCodeDataRTB = string.Empty;
        string QRImagePath = string.Empty;

        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();


        public SalesOrderPlanning()
        {
            InitializeComponent();
        }

        private void SalesOrderPlanning_Load(object sender, EventArgs e)
        {
            QRImagePath = string.Empty;
            QRCodeData = string.Empty;

            QRCodeData = "https://www.safesecureengineer.com";

            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pbQRCode.Image = qrcode.Draw(QRCodeData.ToString(), 10);
            QRImagePath = objRL.GetPath("ImagePath");
            var filePath = QRImagePath;
            Directory.CreateDirectory(filePath);
            string FileName = "007";
            pbQRCode.Image.Save(Path.Combine(filePath, FileName), System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
