using BSLCustomerPortalAPI.Data_Access_Layer;
using BSLCustomerPortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BSLCustomerPortalAPI.Controllers
{
    public class PaymentAPIController : ApiController
    {
        DALPayment _DALPayment = new DALPayment();

        // GET: PaymentAPI

        

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/PaymentAPI/Fn_Get_Payment")]
        public List<clsPayment> Fn_Get_Payment(clsPayment objReq)
        {
            List<clsPayment> objResp = new List<clsPayment>();
            objResp = _DALPayment.Fn_Get_Payment(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/PaymentAPI/Fn_Get_Claim")]
        public List<clsPaymentLedger> Fn_Get_Claim(clsPaymentLedger objReq)
        {
            List<clsPaymentLedger> objResp = new List<clsPaymentLedger>();
            objResp = _DALPayment.Fn_Get_Claim(objReq);
            return objResp;
        }
    }
}