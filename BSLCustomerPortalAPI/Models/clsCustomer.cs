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
        public string CustCreatedDate { get; set; }
        public string CustPanNo { get; set; }
        public string CustName { get; set; }
        public string CustPassword { get; set; }
        public string ErrorMsg { get; set; }
    }

    public class clsFeedback
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Heading { get; set; }
        public string Comments { get; set; }
        public string RequestDate { get; set; }
        public string QueryType { get; set; }
        public string vStatus { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsCustomerContactMessages
    {
        public Int64 CustId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public string ErrorMsg { get; set; }
    }

    public class clsPayment
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DocumentType { get; set; }
        public string BSLRefNo { get; set; }
        public string InvNo { get; set; }
        public string InvDate { get; set; }
        public double DebitAmtINR { get; set; }
        public double CreditAmtINR { get; set; }
        public double BalanceINR { get; set; }
        public double DebitAmtFC { get; set; }
        public double CreditAmtFC { get; set; }
        public string BalanceFC { get; set; }
        public string DocCurrency { get; set; }
        public double DueDays { get; set; }
        public string SalesPersonName { get; set; }
        public string DivisionDescription { get; set; }
        public double OverDueDays { get; set; }
        public string PayTermDesc { get; set; }
        public string SalesOrder { get; set; }
        public string DueDate { get; set; }
        public string FIDocument { get; set; }
        public string FiscalYear { get; set; }
        public double InvQty { get; set; }
        public string LCDate { get; set; }
        public string LCNo { get; set; }
        public string CountryName { get; set; }
        public string QueryType { get; set; }
        public string vErrorMsg { get; set; }

    }
}