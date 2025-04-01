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
}