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
    public class OrderAPIController : ApiController
    {
        DALOrder _DALProduct = new DALOrder();

        // GET: OrderAPI


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/OrderAPI/Fn_Send_Sample_Request")]
        public clsSamplRequest Fn_Send_Sample_Request(clsSamplRequest objReq)
        {
            var objResp = new clsSamplRequest();
            objResp = _DALProduct.Fn_Send_Sample_Request(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/OrderAPI/Fn_Get_Sample_Request")]
        public List<clsSamplRequest> Fn_Get_Sample_Request(clsSamplRequest objReq)
        {
            List<clsSamplRequest> objResp = new List<clsSamplRequest>();
            objResp = _DALProduct.Fn_Get_Sample_Request(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/OrderAPI/Fn_Get_Sample_Request_Detail")]
        public List<clsSamplRequestList> Fn_Get_Sample_Request_Detail(clsSamplRequestList objReq)
        {
            List<clsSamplRequestList> objResp = new List<clsSamplRequestList>();
            objResp = _DALProduct.Fn_Get_Sample_Request_Detail(objReq);
            return objResp;
        }
    }
}