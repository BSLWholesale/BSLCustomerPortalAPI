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

        #region Start for SO

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/OrderAPI/Fn_GET_Sales_Order")]
        public List<clsSalesOrder> Fn_GET_Sales_Order(clsSalesOrder objReq)
        {
            List<clsSalesOrder> objResp = new List<clsSalesOrder>();
            objResp = _DALProduct.Fn_GET_Sales_Order(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/OrderAPI/Fn_GET_Sales_OrderDetail")]
        public List<clsSalesOrder> Fn_GET_Sales_OrderDetail(clsSalesOrder objReq)
        {
            List<clsSalesOrder> objResp = new List<clsSalesOrder>();
            objResp = _DALProduct.Fn_GET_Sales_OrderDetail(objReq);
            return objResp;
        }

        #endregion End for SO

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/OrderAPI/Fn_Create_Reorder")]
        public clsReorder Fn_Create_Reorder(List<clsReorder> objReq)
        {
            var objResp = new clsReorder();
            objResp = _DALProduct.Fn_Create_Reorder(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/OrderAPI/Fn_GET_Reorder")]
        public List<clsReorder> Fn_GET_Reorder(clsReorder objReq)
        {
            List<clsReorder> objResp = new List<clsReorder>();
            objResp = _DALProduct.Fn_GET_Reorder(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/OrderAPI/Fn_GET_Delivey")]
        public List<clsSalesOrder> Fn_GET_Delivey(clsSalesOrder objReq)
        {
            List<clsSalesOrder> objResp = new List<clsSalesOrder>();
            objResp = _DALProduct.Fn_GET_Delivey(objReq);
            return objResp;
        }
    }
}