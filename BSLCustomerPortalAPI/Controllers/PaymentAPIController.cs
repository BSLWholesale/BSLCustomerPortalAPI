using BSLCustomerPortalAPI.Data_Access_Layer;
using BSLCustomerPortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BSLCustomerPortalAPI.Controllers
{
    public class PaymentAPIController : Controller
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

    }
}