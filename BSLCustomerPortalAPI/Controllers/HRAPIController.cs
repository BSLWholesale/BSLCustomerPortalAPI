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
    public class HRAPIController : ApiController
    {
        DALHR _DALHR = new DALHR();

        // GET: HRAPI


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/HRAPI/Fn_Create_Tickets")]
        public clsITHelpdesk Fn_Create_Tickets(clsITHelpdesk objReq)
        {
            var objResp = new clsITHelpdesk();
            objResp = _DALHR.Fn_Create_Tickets(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/HRAPI/Fn_Get_Tickets")]
        public List<clsITHelpdesk> Fn_Get_Tickets(clsITHelpdesk objReq)
        {
            List<clsITHelpdesk> objResp = new List<clsITHelpdesk>();
            objResp = _DALHR.Fn_Get_Tickets(objReq);
            return objResp;
        }
    }
}