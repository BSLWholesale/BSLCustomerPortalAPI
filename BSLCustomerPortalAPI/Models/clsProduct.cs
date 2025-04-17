using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSLCustomerPortalAPI.Models
{
    public class clsYarn
    {
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string GDType { get; set; }
        public string Ply { get; set; }
        public string Count { get; set; }
        public string CountType { get; set; }
        public string BlendDescription { get; set; }
        public string SLUB { get; set; }
        public string Technique { get; set; }
        public string Quality { get; set; }
        public string PieceNo { get; set; }
        public string HSNCode { get; set; }
        public string AddType { get; set; }
        public string SpecialFeature { get; set; }
        public string OldMaterialCode { get; set; }
        public string CountRange { get; set; }
        public string QueryType { get; set; }
        public string vQuery { get; set; }
        public string vTBLName { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsFabric
    {
        public string MaterialCode { get; set; }
        public string Quality { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string BlendDescription { get; set; }
        public string BlendValue { get; set; }
        public string WeaveType { get; set; }
        public int GSM { get; set; }
        public string StrechType { get; set; }
        public string DesignPattern { get; set; }
        public string Shade { get; set; }
        public string Usage { get; set; }
        public string Remarks { get; set; }
        public string Product { get; set; }
        public string FinishType { get; set; }
        public string PieceNo { get; set; }
        public string FinishFabric { get; set; }
        public string AddType { get; set; }
        public string SpecialFeature { get; set; }
        public string OldMaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string Base { get; set; }
        public string TypeofDesign { get; set; }
        public string FabricCategory { get; set; }
        public string ShadeName { get; set; }
        public string QueryType { get; set; }
        public string vQuery { get; set; }
        public string Address { get; set; }
        public string vTBLName { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsGarments
    {
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string Category { get; set; }
        public string Categoy { get; set; }
        public string Products { get; set; }
        public string Ranges { get; set; }
        public string Fabricblends { get; set; }
        public string Fabrictype { get; set; }
        public string Season { get; set; }
        public string PieceNo { get; set; }
        public string Fabricquality { get; set; }
        public string Fabricgsm { get; set; }
        public string Fabricshade { get; set; }
        public string Thumbnail { get; set; }
        public string AddType { get; set; }
        public string SpecialFeature { get; set; }
        public string OldMaterialCode { get; set; }
        public string Style { get; set; }
        public string QueryType { get; set; }
        public string vQuery { get; set; }
        public string vTBLName { get; set; }
        public string vErrorMsg { get; set; }
    }


    public class clsQuotation
    {
        public string QuotationId { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string EmailId { get; set; }
        public string CreatedDate { get; set; }
        public List<clsQuotationList> _oList { get; set; }
        public string vErrorMsg { get; set; }
        public string vRemarks { get; set; }
        public string QueryType { get; set; }
        public string vUserEmail { get; set; }
        public string vValidity { get; set; }
    }

    public class clsQuotationList
    {
        public Int64 Id { get; set; }
        public string QuotationId { get; set; }
        public string vArticle { get; set; }
        public string vBlend { get; set; }
        public string vWeight { get; set; }
        public string vWidth { get; set; }
        public string vPrice { get; set; }
        public string vUnit { get; set; }
        public string vUnitType { get; set; }
        public string vPaymentTerms { get; set; }
        public string vDeliveryTerms { get; set; }
        public string CreatedDate { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsQuotationReport
    {
        public string QuotationId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string EmailId { get; set; }
        public string CreatedDate { get; set; }
        public string vRemarks { get; set; }
        public string vUserEmail { get; set; }
        public string vValidity { get; set; }


        public Int64 Id { get; set; }
        public string vArticle { get; set; }
        public string vBlend { get; set; }
        public string vWeight { get; set; }
        public string vWidth { get; set; }
        public string vPrice { get; set; }
        public string vUnit { get; set; }
        public string vUnitType { get; set; }
        public string vPaymentTerms { get; set; }
        public string vDeliveryTerms { get; set; }
        public string DetailDate { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsRequestDropdown
    {
        public string vFieldName { get; set; }
        public string vValueField { get; set; }
        public string vTBLName { get; set; }
        public string vCriteria { get; set; }
        public string vErrorMsg { get; set; }
    }
    public class clsResponseDropdown
    {
        public string vValueField { get; set; }
        public string vFieldName { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsAutoCompliteRequest
    {
        public string SearchKeyword { get; set; }
    }
    public class clsAutoCompliteResponse
    {
        public string SearchKeyword { get; set; }
        public string SearchField { get; set; }
    }


}