using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSLCustomerPortalAPI.Models
{
    public class clsSamplRequest
    {
        public Int64 RequestId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string vCategory { get; set; }
        public string vRequestDate { get; set; }
        public List<clsSamplRequestList> _oList { get; set; }
        public string vStatus { get; set; }
        public string vErrorMsg { get; set; }
        public string vRemark { get; set; }
    }

    public class clsSamplRequestList
    {
        public int Id { get; set; }
        public Int64 RequestId { get; set; }
        public string CreatedDate { get; set; }
        public string vQuality { get; set; }
        public string vGSM { get; set; }
        public string vProduct { get; set; }
        public string Color { get; set; }
        public bool bShadeCard { get; set; }
        public bool bYardage { get; set; }

        // Added new 30-DEC-2024 FOR Yarn

        public string vCount { get; set; }
        public string vBlendDescription { get; set; }
        public string vBlendPercentage { get; set; }
        public string vEndUse { get; set; }
        public string vDye { get; set; }

        // Added new 30-DEC-2024 FOR  Garments

        public string vStyle { get; set; }
        public string vStyleNo { get; set; }
        public string vFabricQuality { get; set; }
        public string vRemark { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsSalesOrder
    {
        public string SO { get; set; }
        public string SALES_PERSON_NAME { get; set; }
        public string DIVISON_NAME { get; set; }
        public string SOLD_PTY { get; set; }
        public string SOLD_TO_PARTY_NAME { get; set; }
        public string BILL_PTY { get; set; }
        public string BILL_TO_PARTY_NAME { get; set; }
        public string CITY { get; set; }
        public string COUNTRY { get; set; }
        public string Inv_Type { get; set; }
        public string Invoice_No { get; set; }
        public string BSL_Inv_Ref_No { get; set; }
        public string Inv_Date { get; set; }
        public string Material { get; set; }
        public string Material_Description { get; set; }
        public string QTY { get; set; }
        public string QTYMTR { get; set; }

        public string UOM { get; set; }
        public string CURRENCY { get; set; }
        public string BASIC_RATE { get; set; }
        public string BASIC_VAL_FC { get; set; }
        public string BASIC_VAL { get; set; }
        public string TAXABLE_AMT_FC { get; set; }
        public string TAXABLE_AMT { get; set; }
        public string INV_VAL_FC { get; set; }
        public string INV_VAL { get; set; }
        public string DIST_CHANNEL_DESC { get; set; }

        public string COMMISSION { get; set; }
        public string COMMISSION_Amt_INR { get; set; }
        public string SO_LI { get; set; }
        public string SO_RATE { get; set; }

        public string BASIC_CURRENCY { get; set; }
        public string BASIC_RT_F { get; set; }
        public string Yarn_Blend_Desc { get; set; }
        public string Fab_Blend_Desc { get; set; }
        public string Yarn_Blend_Value { get; set; }
        public string Fab_Blend_Value { get; set; }
        public string EXCH_RATE { get; set; }
        public string Fab_Shade_Name { get; set; }
        public string CANCELLED { get; set; }
        public string INCO1 { get; set; }
        public string INCO2 { get; set; }
        public string HSN { get; set; }
        public string AGENT { get; set; }

        public string PAYER { get; set; }
        public string SHIP_TO_PTY { get; set; }
        public string SALES_PERSON { get; set; }
        public string SO_QTY { get; set; }
        public string PAYTERM { get; set; }
        public string PAYTERM_DESC { get; set; }
        public string SALES_GROUP_DESC { get; set; }
        public string SHIP_TO_PARTY_NAME { get; set; }
        public string SO_VALUE { get; set; }
        public string SOLDPTY_COUNTRY { get; set; }
        public string TAX_AMT { get; set; }
        public string TAX_AMT_FC { get; set; }
        public string DO { get; set; }
        public string SLOFFICE { get; set; }
        public string SLGROUP { get; set; }
        public string SalesUOM { get; set; }
        public string CHANNEL { get; set; }
        public string AGENT_NAME { get; set; }
        public string vERRORMSG { get; set; }
        public int UserId { get; set; }
        public string CSAPId { get; set; }
        public string Mobile { get; set; }
    }

    public class clsReorder
    {
        public int Id { get; set; }
        public string SO { get; set; }
        public Int64 RID { get; set; }
        public string Material { get; set; }
        public string Descriptions { get; set; }
        public string Shade { get; set; }
        public int Qty { get; set; }
        public string UOM { get; set; }
        public string vStatus { get; set; }
        public string CreatedDate { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public string QueryType { get; set; }
        public string vErrorMsg { get; set; }

    }

}