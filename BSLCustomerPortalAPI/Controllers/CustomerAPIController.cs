using BSLCustomerPortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using BSLCustomerPortalAPI.Data_Access_Layer;

namespace BSLCustomerPortalAPI.Controllers
{
    public class CustomerAPIController : ApiController
    {
        // GET: CustomerAPI
        //public ActionResult Index()
        //{
        //    return View();
        //}

        DALCustomer _DALCustomer = new DALCustomer();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerAPI/Fn_LogIn_Customer")]
        public clsCustomer Fn_LogIn_Customer(clsCustomer objReq)
        {
            var objResp = new clsCustomer();
            objResp = _DALCustomer.Fn_LogIn_Customer(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerAPI/Fn_Add_Feedback")]
        public clsFeedback Fn_Add_Feedback(clsFeedback objReq)
        {
            var objResp = new clsFeedback();
            objResp = _DALCustomer.Fn_Add_Feedback(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerAPI/Fn_Get_Feedback")]
        public List<clsFeedback> Fn_Get_Feedback(clsFeedback objReq)
        {
            List<clsFeedback> objResp = new List<clsFeedback>();
            objResp = _DALCustomer.Fn_Get_Feedback(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerAPI/Fn_Delete_Feedback")]
        public clsFeedback Fn_Delete_Feedback(clsFeedback objReq)
        {
            var objResp = new clsFeedback();
            objResp = _DALCustomer.Fn_Delete_Feedback(objReq);
            return objResp;
        }
    }
}