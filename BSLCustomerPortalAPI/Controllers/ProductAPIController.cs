﻿using BSLCustomerPortalAPI.Data_Access_Layer;
using BSLCustomerPortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BSLCustomerPortalAPI.Controllers
{
    public class ProductAPIController : ApiController
    {
        DALProduct _DALProduct = new DALProduct();

        // GET: ProductAPI

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ProductAPI/Fn_Get_Yarn_GDType")]
        public List<clsYarn> Fn_Get_Yarn_GDType(clsYarn objReq)
        {
            List<clsYarn> objResp = new List<clsYarn>();
            objResp = _DALProduct.Fn_Get_Yarn_GDType(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ProductAPI/Fn_Get_Search_Yarn")]
        public List<clsYarn> Fn_Get_Search_Yarn(clsYarn objReq)
        {
            List<clsYarn> objResp = new List<clsYarn>();
            objResp = _DALProduct.Fn_Get_Search_Yarn(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ProductAPI/Fn_Get_Fabric_Blend")]
        public List<clsFabric> Fn_Get_Fabric_Blend(clsFabric objReq)
        {
            List<clsFabric> objResp = new List<clsFabric>();
            objResp = _DALProduct.Fn_Get_Fabric_Blend(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ProductAPI/Fn_Get_Search_Fabric")]
        public List<clsFabric> Fn_Get_Search_Fabric(clsFabric objReq)
        {
            List<clsFabric> objResp = new List<clsFabric>();
            objResp = _DALProduct.Fn_Get_Search_Fabric(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ProductAPI/Fn_Get_Yarn_CatalogueDetail")]
        public List<clsYarn> Fn_Get_Yarn_CatalogueDetail(clsYarn objReq)
        {
            var _YarnCatalogue = new List<clsYarn>();
            _YarnCatalogue = _DALProduct.Fn_Get_Yarn_CatalogueDetail(objReq);
            return _YarnCatalogue;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ProductAPI/Fn_Get_Garments_Category")]
        public List<clsGarments> Fn_Get_Garments_Category(clsGarments objReq)
        {
            List<clsGarments> objResp = new List<clsGarments>();
            objResp = _DALProduct.Fn_Get_Garments_Category(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ProductAPI/Fn_Get_Search_Garments")]
        public List<clsGarments> Fn_Get_Search_Garments(clsGarments objReq)
        {
            List<clsGarments> objResp = new List<clsGarments>();
            objResp = _DALProduct.Fn_Get_Search_Garments(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ProductAPI/Fn_Get_Fabric_CatalogueDetail")]
        public List<clsFabric> Fn_Get_Fabric_CatalogueDetail(clsFabric objReq)
        {
            var _FabricCatalogue = new List<clsFabric>();
            _FabricCatalogue = _DALProduct.Fn_Get_Fabric_CatalogueDetail(objReq);
            return _FabricCatalogue;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ProductAPI/Fn_Get_RMG_CatalogueDetail")]
        public List<clsGarments> Fn_Get_RMG_CatalogueDetail(clsGarments objReq)
        {
            var _RMGCatalogue = new List<clsGarments>();
            _RMGCatalogue = _DALProduct.Fn_Get_RMG_CatalogueDetail(objReq);
            return _RMGCatalogue;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ProductAPI/Fn_Get_GlobalSearch")]
        public List<clsAutoCompliteResponse> Fn_Get_GlobalSearch(clsAutoCompliteRequest obj)
        {
            var objResp = new List<clsAutoCompliteResponse>();
            objResp = _DALProduct.Fn_Get_GlobalSearch(obj);
            return objResp;
        }

    }
}