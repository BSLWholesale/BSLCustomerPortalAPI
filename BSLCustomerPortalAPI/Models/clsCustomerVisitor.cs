using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSLCustomerPortalAPI.Models
{
    public class clsCustomerVisitor
    {
        public int CustId { get; set; }
        public string vCustomerName { get; set; }
        public string vEmailId { get; set; }
        public string vMobile { get; set; }
        public string vCompanyName { get; set; }
        public string vAddress1 { get; set; }
        public string vState { get; set; }
        public string vCity { get; set; }
        public string vPinCode { get; set; }
        public string vErrorMsg { get; set; }

        public int MeetingId { get; set; }
        public string vMeetingVenue { get; set; }
        public string vMeetingDate { get; set; }
        public string vMeetingPurposevisit { get; set; }
        public string vMeetingCommunication { get; set; }
        public int MeetingDetailId { get; set; }
        public string vMaterialDescription { get; set; }
        public int nLoginId { get; set; }
        public string vStatus { get; set; }
        public string vClosureRemarks { get; set; }
        public Nullable<int> FollowUpID { get; set; }
        public List<clsProductList> oProductList { set; get; }
        public clsScheduleMeeting oScheduleMeeting { get; set; }
    }

    public class clsProductList
    {
        public string vProductCategory { get; set; }
        public string vMaterialCode { get; set; }
        public string vScanCode { get; set; }
        public string vSuperImposeImage { get; set; }
        public string vTBLName { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsScheduleMeeting
    {
        public string vScheduleDate { get; set; }
        public string vScheduleVenue { get; set; }
        public string vPurposeofVisit { get; set; }   
        public string vErrorMsg { get; set; }
    }

    public class clsCustomerVisitCount
    {
        public int nCustomerId { get; set; }
        public int nCustomerVisitCount { get; set; }
        public string vStatus { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsReqScheduleMeeting
    {
        public int nLoginId { get; set; }
        public int nMeetingId { get; set; }
        public string vMeetingDate { get; set; }
        public string vScheduleVenue { get; set; }
        public string vCustomer { get; set; }
        public string vMonth { get; set; }
        public string vYear { get; set; }
    }

    public class clsRespScheduleMeeting
    {
        public int nLoginId { get; set; }
        public int nMeetingId { get; set; }
        public int vCustomerId { get; set; }
        public string vCustomerName { get; set; }
        public string vEmail { get; set; }
        public string vMeetingDate { get; set; }
        public string vScheduleVenue { get; set; }
        public string vStatus { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsExpenseMaster
    {
        public Nullable<int> nMeetingId { get; set; }
        public Nullable<int> nLoginId { get; set; }
        public Nullable<int> nExpenseId { get; set; }
        public string vAppliedDate { get; set; }
        public string vStatus { get; set; }
        public int nRowCount { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsExpenseApproval
    {
        public Nullable<int> nLoginId { get; set; }
        public int nExpenseId { get; set; }
        public string vApprovarName { get; set; }
        public string vApprovalDate { get; set; }
        public string vApprovalStatus { get; set; }
        public string vApprovalRemark { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsDashboardCustomerVisitCount
    {
        public int LoginId { get; set; }
        public int CustomerVisitCount { get; set; }
        public string CustomerVisitStatus { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsCustomerSearchRequest
    {
        public string SearchKeyword { get; set; }
    }

    public class clsCustomerSearchResponse
    {
        public string SearchKeyword { get; set; }
    }
}