﻿using BSLCustomerPortalAPI.Models;
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
    }
}