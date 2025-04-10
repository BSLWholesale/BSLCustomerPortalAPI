using BSLCustomerPortalAPI.Data_Access_Layer;
using BSLCustomerPortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

namespace BSLCustomerPortalAPI.Controllers
{
    public class InquiryAPIController : ApiController
    {
        // GET: InquiryAPI
        //public ActionResult Index()
        //{
        //    return View();
        //}

        DALInquiry _DALInquiry = new DALInquiry();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/InquiryApi/Fn_Make_Quotation")]
        public clsQuotation Fn_Make_Quotation(clsQuotation objReq)
        {
            var objResp = new clsQuotation();
            objResp = _DALInquiry.Fn_Make_Quotation(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/InquiryApi/Fn_Get_Quotation")]
        public List<clsQuotation> Fn_Get_Quotation(clsQuotation objReq)
        {
            List<clsQuotation> objResp = new List<clsQuotation>();
            objResp = _DALInquiry.Fn_Get_Quotation(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/InquiryApi/Fn_Get_Quotation_Detail")]
        public List<clsQuotationList> Fn_Get_Quotation_Detail(clsQuotationList objReq)
        {
            List<clsQuotationList> objResp = new List<clsQuotationList>();
            objResp = _DALInquiry.Fn_Get_Quotation_Detail(objReq);
            return objResp;
        }
    }
}