using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSLCustomerPortalAPI.Models
{
    public class clsCustomer
    {
        public Int64 CustomerId { get; set; }
        public string CustEmailId { get; set; }
        public string CustCompanyName { get; set; }
        public string CustADRNR { get; set; }
        public string CustCountryCode { get; set; }
        public string CustCity { get; set; }
        public string CustPostalCode { get; set; }
        public string CustMobile { get; set; }
        public string CreateDate { get; set; }
        public string CustPanNo { get; set; }
        public string CustName { get; set; }
        public string CustPassword { get; set; }
        public string ErrorMsg { get; set; }
    }
}