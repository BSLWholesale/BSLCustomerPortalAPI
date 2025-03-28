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
        public string vErrorMsg { get; set; }
    }
}