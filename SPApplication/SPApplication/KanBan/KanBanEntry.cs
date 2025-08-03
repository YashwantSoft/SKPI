using SPApplication.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Services;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace SPApplication.KanBan
{
    public partial class KanBanEntry : Form
    {
        DesignLayer objDL = new DesignLayer();
        
      
       
        public KanBanEntry()
        {
            InitializeComponent();
            btnSalesOrder.BackgroundImage = BusinessResources.KanBanSalesOrder;
            btnCommercialOrder.BackgroundImage = BusinessResources.Commercial;
            btnPurchasesOrder.BackgroundImage = BusinessResources.PurchaseOrder;
            btnManufactruringOrder.BackgroundImage = BusinessResources.ManufacturingOrder;
            btnProductionOrder.BackgroundImage = BusinessResources.ProductionOrder;
            btnExit.BackgroundImage = BusinessResources.Exit;

        }

        private void btnSalesOrder_Click(object sender, EventArgs e)
        {
            DispatchSchedule objForm = new DispatchSchedule();
            objForm.ShowDialog(this);
        }

        private void btnCommercialOrder_Click(object sender, EventArgs e)
        {
            KB_CommercialOrder objform = new KB_CommercialOrder();
            objform.ShowDialog(this);

        }

        private void btnPurchasesOrder_Click(object sender, EventArgs e)
        {
            KB_PurchaseOrder objForm = new KB_PurchaseOrder();
            objForm.ShowDialog(this);
        }

        private void btnManufactruringOrder_Click(object sender, EventArgs e)
        {
            KB_MunufacturingOrder objForm = new KB_MunufacturingOrder();
            objForm.ShowDialog(this);
        }

        private void btnProductionOrder_Click(object sender, EventArgs e)
        {
            KB_ProductionOrder objForm = new KB_ProductionOrder();
            objForm.ShowDialog(this);
        }
        
        public class Login
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();

            //Login objLogin = new Login();
            //{
                
            //}
            ////Webservice Code
            ////MyWebService objFormService = new MyWebService();

            ////MessageBox.Show(objFormService.GetUserSuccess().ToString());

            ////Web Services
            //// http://webservices.2dkapps.com/api/GetLogin/Yash/Yash

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://webservices.2dkapps.com");

            //string TT1 = string.Empty, TT2 = string.Empty; ;

            //TT1 = Convert.ToString(T1.Text);
            //TT2 = Convert.ToString(T2.Text);

            //objLogin.UserName = TT1;
            //objLogin.Password = TT2;
        }

            //HttpResponseMessage response = client.GetAsync("api/GetLogin/" + TT1 + "/"+TT2+"").Result;
        // [WebService(Namespace = "http://menucard.2dkapps.com/ConnectionDatabase.asmx")]
        public class MyWebService
        {
            //MyServices.ConnectionDatabase objWS = new MyServices.ConnectionDatabase();

            //public string GetUserSuccess()
            //{
            //    string HWorld = objWS.HelloWorld();

            //    string ReturnValue = objWS.GetUsersList("9922138778", "a");
            //    return ReturnValue;
            //}

            // implementation
        }

        private void KanBanEntry_Load(object sender, EventArgs e)
        {
            
     }

    }
}
 