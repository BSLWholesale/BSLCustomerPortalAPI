using BSLCustomerPortalAPI.Data_Access_Layer;
using BSLCustomerPortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BSLCustomerPortalAPI.Controllers
{
    public class CustomerVisitController : ApiController
    {
        // GET: CustomerVisit

        DALCustomerVisit _DALCustomerVisit = new DALCustomerVisit();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerVisit/Fn_Get_Meeting_List")]
        public clsCustomerVisitor Fn_Get_Meeting_List(clsCustomerVisitor cs)
        {
            var objResp = new clsCustomerVisitor();
            objResp = _DALCustomerVisit.Fn_Get_Meeting_List(cs);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerVisit/Fn_Fetch_Schedule_Meeting_Month_wise")]
        public List<clsRespScheduleMeeting> Fn_Fetch_Schedule_Meeting_Month_wise(clsReqScheduleMeeting objReq)
        {
            List<clsRespScheduleMeeting> objResp = new List<clsRespScheduleMeeting>();
            objResp = _DALCustomerVisit.Fn_Fetch_Schedule_Meeting_Month_wise(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerVisit/Fn_Get_Customer_By_Email")]
        public clsCustomerVisitor Fn_Get_Customer_By_Email(clsCustomerVisitor objReq)
        {
            var _model = new clsCustomerVisitor();
            _model = _DALCustomerVisit.Fn_Get_Customer_By_Email(objReq);
            return _model;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerVisit/Fn_Schedule_Customer_Meeting")]
        public clsCustomerVisitor Fn_Schedule_Customer_Meeting(clsCustomerVisitor cs)
        {
            var objResp = new clsCustomerVisitor();
            objResp = _DALCustomerVisit.Fn_Schedule_Customer_Meeting(cs);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerVisit/Fn_Get_SearchCustomerEmail")]
        public List<clsCustomerSearchResponse> Fn_Get_SearchCustomerEmail(clsCustomerSearchRequest objReq)
        {
            var _ObjResp = new List<clsCustomerSearchResponse>();
            _ObjResp = _DALCustomerVisit.Fn_Get_SearchCustomerEmail(objReq);
            return _ObjResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerVisit/Fn_Get_CustMeetingMasterList")]
        public List<clsCustomerVisitor> Fn_Get_CustMeetingMasterList(clsCustomerVisitor cs)
        {
            var objResp = new List<clsCustomerVisitor>();
            objResp = _DALCustomerVisit.Fn_Get_CustMeetingMasterList(cs);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerVisit/Fn_Close_CustomerMeeting")]
        public clsCustomerVisitor Fn_Close_CustomerMeeting(clsCustomerVisitor cs)
        {
            var objResp = new clsCustomerVisitor();
            objResp = _DALCustomerVisit.Fn_Close_CustomerMeeting(cs);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerVisit/Fn_Update_Meeting")]
        public clsCustomerVisitor Fn_Update_Meeting(clsCustomerVisitor cs)
        {
            var objResp = new clsCustomerVisitor();
            objResp = _DALCustomerVisit.Fn_Update_Meeting(cs);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerVisit/Fn_Insert_CustMaster")]
        public clsCustomerVisitor Fn_Insert_CustMaster(clsCustomerVisitor cs)
        {
            var objResp = new clsCustomerVisitor();
            objResp = _DALCustomerVisit.Fn_Insert_CustMaster(cs);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerVisit/Fn_Send_Mail_Meeting_Communication")]
        public clsCustomerVisitor Fn_Send_Mail_Meeting_Communication(clsCustomerVisitor cs)
        {
            var objResp = new clsCustomerVisitor();
            objResp = _DALCustomerVisit.Fn_Send_Mail_Meeting_Communication(cs);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerVisit/Fn_Get_MaterialCode")]
        public clsProductList Fn_Get_MaterialCode(clsProductList objReq)
        {
            var _productmodel = new clsProductList();
            _productmodel = _DALCustomerVisit.Fn_Get_MaterialCode(objReq);
            return _productmodel;
        }

    }
}