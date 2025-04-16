using BSLCustomerPortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace BSLCustomerPortalAPI.Data_Access_Layer
{
    public class DALCustomerVisit
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);
        Generic gn = new Generic();
        public int mxMeetingID;
        string strMeetingDate, strMeetingVenue;
        Int32 nMeetingId;

        SqlCommand cmd;
        SqlDataReader dr;

        public clsCustomerVisitor Fn_Get_Meeting_List(clsCustomerVisitor cs)
        {
            var objResp = new clsCustomerVisitor();
            var objProductResp = new List<clsProductList>();
            try
            {
                if (con.State == ConnectionState.Broken)
                { con.Close(); }
                if (con.State == ConnectionState.Closed)
                { con.Open(); }

                string strSql = "Select MeetingId,CustId,MeetingVenue,CONVERT(VARCHAR(20), MeetingDate, 105) + ' ' + CONVERT(VARCHAR(20), MeetingDate, 108) + ' ' + RIGHT(CONVERT(VARCHAR(30), MeetingDate, 109), 2) AS MeetingDate, FollowUpID,MeetingPurposeOfVisit,MeetingCommunication,vStatus,vMailCommuniction from TBL_CustMeetingMaster where MeetingId=" + cs.MeetingId + "";

                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    objResp.CustId = Convert.ToInt32(dr["CustId"].ToString());
                    objResp.vMeetingVenue = Convert.ToString(dr["MeetingVenue"].ToString());
                    objResp.vMeetingDate = Convert.ToString(dr["MeetingDate"].ToString());
                    objResp.vMeetingPurposevisit = Convert.ToString(dr["MeetingPurposeOfVisit"].ToString());
                    objResp.vMeetingCommunication = Convert.ToString(dr["MeetingCommunication"].ToString());
                    objResp.vStatus = Convert.ToString(dr["vStatus"].ToString());
                    objResp.FollowUpID = Convert.ToInt32(dr["FollowUpID"].ToString());
                    objResp.MeetingId = Convert.ToInt32(dr["MeetingId"].ToString());

                }
                dr.Close();
                cmd.Dispose();

                string query = "Select CustomerName,Email,Mobile,vAddress,vState,City,PINCode,CompanyName from TBL_CustMaster where CustId =" + objResp.CustId + "";
                SqlCommand cmd1 = new SqlCommand(query, con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    dr1.Read();
                    objResp.vCustomerName = Convert.ToString(dr1["CustomerName"].ToString());
                    objResp.vEmailId = Convert.ToString(dr1["Email"].ToString());
                    objResp.vMobile = Convert.ToString(dr1["Mobile"].ToString());
                    objResp.vAddress1 = Convert.ToString(dr1["vAddress"].ToString());
                    objResp.vState = Convert.ToString(dr1["vState"].ToString());
                    objResp.vCity = Convert.ToString(dr1["City"].ToString());
                    objResp.vPinCode = Convert.ToString(dr1["PINCode"].ToString());
                    objResp.vCompanyName = Convert.ToString(dr1["CompanyName"].ToString());

                    dr1.Close();
                    cmd1.Dispose();

                    string strQuery = "Select MeetingDetailId,ProductCategory,MaterialCode,ScanCode from TBL_CustMeetingDetail where MeetingId=" + cs.MeetingId + "";
                    SqlDataAdapter da = new SqlDataAdapter(strQuery, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    int i = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        while (ds.Tables[0].Rows.Count > i)
                        {
                            var _product = new clsProductList();
                            _product.vProductCategory = Convert.ToString(ds.Tables[0].Rows[i]["ProductCategory"]);
                            _product.vMaterialCode = Convert.ToString(ds.Tables[0].Rows[i]["MaterialCode"]);
                            _product.vScanCode = Convert.ToString(ds.Tables[0].Rows[i]["ScanCode"]);
                            _product.vErrorMsg = "Success";
                            objProductResp.Add(_product);
                            i++;
                        }
                        objResp.oProductList = objProductResp;

                    }
                    else
                    {
                        var _product = new clsProductList();
                        _product.vErrorMsg = "No Record Found.";
                        objProductResp.Add(_product);
                        objResp.oProductList = objProductResp;
                    }
                    objResp.vErrorMsg = "success";
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog("Function Name : Fn_Get_Meeting_List", " " + "Error Msg : " + ex.Message.ToString(), new StackTrace(ex, true));
                objResp.vErrorMsg = ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public List<clsRespScheduleMeeting> Fn_Fetch_Schedule_Meeting_Month_wise(clsReqScheduleMeeting objReq)
        {
            List<clsRespScheduleMeeting> objResp = new List<clsRespScheduleMeeting>();
            var resp = new clsRespScheduleMeeting();

            try
            {
                if (objReq.nLoginId == 0)
                {
                    resp.vErrorMsg = "Login Id is not supplied.";
                    objResp.Add(resp);
                }
                else
                {
                    resp.vErrorMsg = "";
                }

                if (resp.vErrorMsg == "")
                {
                    if (con.State == ConnectionState.Broken)
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed)
                    { con.Open(); }

                    cmd = new SqlCommand("USP_Get_Schedule_Meeting", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@vLoginId", objReq.nLoginId);
                    cmd.Parameters.AddWithValue("@vYear", objReq.vYear);
                    cmd.Parameters.AddWithValue("@vMonth", objReq.vMonth);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    int i = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        while (ds.Tables[0].Rows.Count > i)
                        {
                            var _resp = new clsRespScheduleMeeting();
                            _resp.nMeetingId = Convert.ToInt32(ds.Tables[0].Rows[i]["MeetingId"]);
                            _resp.vCustomerId = Convert.ToInt32(ds.Tables[0].Rows[i]["CustId"]);
                            _resp.vCustomerName = Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                            _resp.vMeetingDate = Convert.ToString(ds.Tables[0].Rows[i]["MeetingDate"]);
                            _resp.vScheduleVenue = Convert.ToString(ds.Tables[0].Rows[i]["MeetingVenue"]);
                            _resp.vStatus = Convert.ToString(ds.Tables[0].Rows[i]["vStatus"]);
                            _resp.vErrorMsg = "Success";
                            objResp.Add(_resp);
                            i++;
                        }
                    }
                    else
                    {
                        var _resp = new clsRespScheduleMeeting();
                        _resp.vErrorMsg = "No data found.";
                        objResp.Add(_resp);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Function Name : Fn_Fetch_Schedule_Meeting_Month_wise", " " + "Error Msg : " + ex.Message.ToString(), new StackTrace(ex, true));
                var _resp = new clsRespScheduleMeeting();
                _resp.vErrorMsg = ex.Message.ToString();
                objResp.Add(_resp);
            }
            finally
            {
                //cmd.Dispose();
                con.Close();
            }
            return objResp;
        }

        public clsCustomerVisitor Fn_Get_Customer_By_Email(clsCustomerVisitor objReq)
        {
            var _model = new clsCustomerVisitor();
            try
            {
                if (con.State == ConnectionState.Broken)
                { con.Close(); }
                if (con.State == ConnectionState.Closed)
                { con.Open(); }

                cmd = new SqlCommand("USP_Get_Customer_BY_Email", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@vEmail", objReq.vEmailId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _model.CustId = Convert.ToInt32(ds.Tables[0].Rows[0]["CustId"]);
                    _model.vCustomerName = Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]);
                    _model.vEmailId = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
                    _model.vMobile = Convert.ToString(ds.Tables[0].Rows[0]["Mobile"]);
                    _model.vAddress1 = Convert.ToString(ds.Tables[0].Rows[0]["vAddress"]);
                    _model.vState = Convert.ToString(ds.Tables[0].Rows[0]["vState"]);
                    _model.vCity = Convert.ToString(ds.Tables[0].Rows[0]["City"]);
                    _model.vPinCode = Convert.ToString(ds.Tables[0].Rows[0]["PINCode"]);
                    _model.vCompanyName = Convert.ToString(ds.Tables[0].Rows[0]["CompanyName"]);
                    Fn_Get_MeetingVenue(_model.CustId, objReq.nLoginId);
                    _model.vMeetingDate = strMeetingDate;
                    _model.vMeetingVenue = strMeetingVenue;
                    _model.MeetingId = nMeetingId;
                    _model.vErrorMsg = "Success";
                }
                else
                {
                    _model.vErrorMsg = "Customer not found.";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Function Name : Fn_Get_Customer_By_Email", " " + "Error Msg : " + ex.Message.ToString(), new StackTrace(ex, true));
                _model.vErrorMsg = ex.Message.ToString();
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            return _model;
        }


        public void Fn_Get_MeetingVenue(int CustId, int LoginId)
        {
            try
            {
                if (con.State == ConnectionState.Broken)
                { con.Close(); }
                if (con.State == ConnectionState.Closed)
                { con.Open(); }

                string strSQL = "Select CONVERT(varchar,MeetingDate,106) as MeetingDate,MeetingVenue,MeetingId from TBL_CustMeetingMaster where 1=1 and CustId = " + CustId + " and LoginId = " + LoginId + " and vStatus='Pending'";
                cmd = new SqlCommand(strSQL, con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strMeetingDate = Convert.ToString(dr["MeetingDate"].ToString());
                    strMeetingVenue = dr["MeetingVenue"].ToString();
                    nMeetingId = Convert.ToInt32(dr["MeetingId"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Function Name : Fn_Get_MeetingVenue", " " + "Error Msg : " + ex.Message.ToString(), new StackTrace(ex, true));
                ex.Message.ToString();
            }
            finally
            {
                con.Close();
                dr.Close();
                cmd.Dispose();
            }
        }


        public clsCustomerVisitor Fn_Schedule_Customer_Meeting(clsCustomerVisitor cs)
        {
            var objResp = new clsCustomerVisitor();
            Generic _generic = new Generic();
            try
            {
                if (cs.nLoginId == 0)
                {
                    objResp.vErrorMsg = "Login Id is not supplied";
                }

                if (string.IsNullOrWhiteSpace(cs.vCustomerName))
                {
                    objResp.vErrorMsg = "Customer Name can't be Empty";
                }
                else
                {
                    if (_generic.IsValidString(cs.vCustomerName))
                    {
                        objResp.vErrorMsg = "Please Enter only characters.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (string.IsNullOrWhiteSpace(cs.vEmailId))
                {
                    objResp.vErrorMsg = "Email ID can't be Empty";
                }
                else
                {
                    if (!_generic.IsValidEmail(cs.vEmailId))
                    {
                        objResp.vErrorMsg = "Enter Valid Email Id.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (string.IsNullOrWhiteSpace(cs.vMobile))
                {
                    objResp.vErrorMsg = "Mobile Number can't be Empty.";
                }
                else
                {
                    if (!_generic.IsValidMobile(cs.vMobile))
                    {
                        objResp.vErrorMsg = "Enter Valid Mobile Number.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (string.IsNullOrWhiteSpace(cs.vCompanyName))
                {
                    objResp.vErrorMsg = "Company Name can't be blank.";
                }

                if (string.IsNullOrWhiteSpace(cs.vAddress1))
                {
                    objResp.vErrorMsg = "Address can't be blank.";
                }

                if (string.IsNullOrWhiteSpace(cs.vState))
                {
                    objResp.vErrorMsg = "State can't be blank.";
                }
                else
                {
                    if (_generic.IsValidString(cs.vState))
                    {
                        objResp.vErrorMsg = "Please enter only characters.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (string.IsNullOrWhiteSpace(cs.vCity))
                {
                    objResp.vErrorMsg = "City can't be blank.";
                }

                if (string.IsNullOrWhiteSpace(cs.vPinCode))
                {
                    objResp.vErrorMsg = "PIN Code can't be Empty.";
                }
                else
                {
                    if (!_generic.IsValidPin(cs.vPinCode))
                    {
                        objResp.vErrorMsg = "Please Enter only Numbers.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (objResp.vErrorMsg == "")
                {
                    if (con.State == ConnectionState.Broken)
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed)
                    { con.Open(); }


                    SqlCommand cmd = new SqlCommand("USP_Insert_CustMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustName", cs.vCustomerName);
                    cmd.Parameters.AddWithValue("@Email", cs.vEmailId);
                    cmd.Parameters.AddWithValue("@Mobile", cs.vMobile);
                    cmd.Parameters.AddWithValue("@Address", cs.vAddress1);
                    cmd.Parameters.AddWithValue("@State", cs.vState);
                    cmd.Parameters.AddWithValue("@City", cs.vCity);
                    cmd.Parameters.AddWithValue("@PinCode", cs.vPinCode);
                    cmd.Parameters.AddWithValue("@CompanyName", cs.vCompanyName);
                    cmd.Parameters.Add("@CustId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        objResp.vErrorMsg = "";
                    }
                    else
                    {
                        objResp.vErrorMsg = "Failed to insert customer detail.";
                    }
                    cs.CustId = Convert.ToInt32(cmd.Parameters["@CustId"].Value);
                    cmd.Dispose();


                    if (cs.CustId != 0)
                    {
                        objResp.vErrorMsg = "";
                    }
                    // Insert schedule meeting 
                    Fn_Get_Max_Id(cs.CustId);
                    cs.MeetingId = mxMeetingID;

                    if (string.IsNullOrWhiteSpace(cs.vMeetingVenue))
                    {
                        objResp.vErrorMsg = "Meeting Venue can't be blank.";
                    }
                    if (string.IsNullOrWhiteSpace(cs.vMeetingPurposevisit))
                    {
                        objResp.vErrorMsg = "Purpose of Visit can't be blank.";
                    }
                    if (objResp.vErrorMsg == "")
                    {
                        if (con.State == ConnectionState.Broken)
                        { con.Close(); }
                        if (con.State == ConnectionState.Closed)
                        { con.Open(); }

                        var html = "";
                        SqlCommand cmd1 = new SqlCommand("USP_Insert_CustMeetingMaster", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@MeetingId", cs.MeetingId);
                        cmd1.Parameters.AddWithValue("@CustId", cs.CustId);
                        cmd1.Parameters.AddWithValue("@MeetingVenue", cs.vMeetingVenue);
                        cmd1.Parameters.AddWithValue("@MeetingDate", DateTime.ParseExact(cs.vMeetingDate, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture));
                        cmd1.Parameters.AddWithValue("@LoginId", cs.nLoginId);
                        cmd1.Parameters.AddWithValue("@MeetingPurposeOfVisit", cs.vMeetingPurposevisit);
                        cmd1.Parameters.AddWithValue("@Status", "Schedule");
                        cmd1.Parameters.AddWithValue("@QueryType", "Insert");
                        int i1 = cmd1.ExecuteNonQuery();
                        if (i1 >= 1)
                        {
                            objResp.MeetingId = mxMeetingID;
                            objResp.vErrorMsg = "Success";
                        }
                        else
                        {
                            objResp.vErrorMsg = "Failed Schedule meeting";
                        }
                        cmd1.Dispose();

                        // Template added for Schedule Meeting

                        string filename = System.Web.Hosting.HostingEnvironment.MapPath("~/Email/ScheduleMeetTemp.html");
                        string mailbody = System.IO.File.ReadAllText(filename);

                        mailbody = mailbody.Replace("##MEETINGID##", Convert.ToString(cs.MeetingId));
                        mailbody = mailbody.Replace("##MEETINGDATE##", cs.vMeetingDate);
                        mailbody = mailbody.Replace("##CUSTOMERNAME##", cs.vCustomerName);
                        mailbody = mailbody.Replace("##COMPANYNAME##", cs.vCompanyName);
                        mailbody = mailbody.Replace("##EMAIL##", cs.vEmailId);
                        mailbody = mailbody.Replace("##CONTACT##", cs.vMobile);
                        mailbody = mailbody.Replace("##ADDRESS##", cs.vAddress1);
                        mailbody = mailbody.Replace("##MEETINGVENUE##", cs.vMeetingVenue);
                        mailbody = mailbody.Replace("##PURPOSEOFVISIT##", cs.vMeetingPurposevisit);

                        mailbody = mailbody.Replace("##PRODUCTROW##", html);

                        string ToEmail = cs.vEmailId;

                        gn.TriggerEmailNoAttachment(mailbody, "Meeting is Scheduled", ToEmail, "");

                        // Template added for Schedule Meeting
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Function Name : Fn_Schedule_Customer_Meeting", " " + "Error Msg : " + ex.Message.ToString(), new StackTrace(ex, true));
                objResp.vErrorMsg = ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public int Fn_Get_Max_Id(int nCustId)
        {
            try
            {
                if (con.State == ConnectionState.Broken)
                { con.Close(); }
                if (con.State == ConnectionState.Closed)
                { con.Open(); }

                string strSql = "Select Max(MeetingId) as MeetingId from TBL_CustMeetingMaster";
                cmd = new SqlCommand(strSql, con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    string strId = Convert.ToString(dr["MeetingId"].ToString());
                    if (strId == null || strId == string.Empty)
                    {
                        mxMeetingID = 1001;
                    }
                    else
                    {
                        mxMeetingID = Convert.ToInt32(strId);
                        mxMeetingID = mxMeetingID + 1;
                    }
                }
                else
                {
                    mxMeetingID = 1001;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog("Function Name : Fn_Get_Max_Id", " " + "Error Msg : " + ex.Message.ToString(), new StackTrace(ex, true));
                ex.Message.ToString();
            }
            finally
            {
                dr.Close();
                cmd.Dispose();
                con.Close();
            }
            return mxMeetingID;
        }

        public List<clsCustomerSearchResponse> Fn_Get_SearchCustomerEmail(clsCustomerSearchRequest objReq)
        {
            var _ObjResp = new List<clsCustomerSearchResponse>();
            try
            {
                if (con.State == ConnectionState.Broken)
                { con.Close(); }
                if (con.State == ConnectionState.Closed)
                { con.Open(); }

                cmd = new SqlCommand("USP_SearchCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchKeyword", objReq.SearchKeyword);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        var objItem = new clsCustomerSearchResponse();
                        objItem.SearchKeyword = Convert.ToString(ds.Tables[0].Rows[i]["CompanyName"]);
                        _ObjResp.Add(objItem);
                        i++;
                    }
                }
                else
                {
                    // return _ObjResp;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Function Name : Fn_Get_SearchCustomerEmail", " " + "Error Msg : " + ex.Message.ToString(), new StackTrace(ex, true));
                ex.Message.ToString();
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return _ObjResp;
        }

        public List<clsCustomerVisitor> Fn_Get_CustMeetingMasterList(clsCustomerVisitor cs)
        {
            var objResp = new List<clsCustomerVisitor>();
            var _resp = new clsCustomerVisitor();
            try
            {
                if (cs.nLoginId == 0)
                {
                    _resp.vErrorMsg = "Login Id is not Supplied.";
                    objResp.Add(_resp);
                }
                else
                {
                    _resp.vErrorMsg = "";
                }

                if (_resp.vErrorMsg == "")
                {
                    if (con.State == ConnectionState.Broken)
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed)
                    { con.Open(); }

                    cmd = new SqlCommand("USP_Get_All_CustMeetingMasterList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LoginId", cs.nLoginId);
                    cmd.Parameters.AddWithValue("@Status", cs.vStatus);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    int i = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        while (ds.Tables[0].Rows.Count > i)
                        {
                            var objItem = new clsCustomerVisitor();
                            objItem.MeetingId = Convert.ToInt32(ds.Tables[0].Rows[i]["MeetingId"]);

                            string followId = Convert.ToString(ds.Tables[0].Rows[i]["FollowUpID"]);
                            if (followId == "")
                            {
                                followId = "0";
                            }
                            objItem.vCustomerName = Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                            objItem.vMeetingVenue = Convert.ToString(ds.Tables[0].Rows[i]["MeetingVenue"]);
                            objItem.vMeetingDate = Convert.ToString(ds.Tables[0].Rows[i]["MeetingDate"]);
                            objItem.vMeetingPurposevisit = Convert.ToString(ds.Tables[0].Rows[i]["MeetingPurposeOfVisit"]);
                            objItem.vStatus = Convert.ToString(ds.Tables[0].Rows[i]["vStatus"]);
                            objItem.vClosureRemarks = Convert.ToString(ds.Tables[0].Rows[i]["ClosureRemark"]);
                            objItem.FollowUpID = Convert.ToInt32(followId);
                            objItem.vErrorMsg = "success";

                            objResp.Add(objItem);
                            i++;
                        }
                    }
                    else
                    {
                        var objItem = new clsCustomerVisitor();
                        objItem.vErrorMsg = "No Record Found";
                        objResp.Add(objItem);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Function Name : Fn_Get_CustMeetingMasterList", " " + "Error Msg : " + ex.Message.ToString(), new StackTrace(ex, true));
                var objItem = new clsCustomerVisitor();
                objItem.vErrorMsg = ex.Message.ToString();
                objResp.Add(objItem);
            }
            finally
            {
                //cmd.Dispose();
                con.Close();
            }
            return objResp;
        }

        public clsCustomerVisitor Fn_Close_CustomerMeeting(clsCustomerVisitor cs)
        {
            var objResp = new clsCustomerVisitor();
            try
            {
                if (con.State == ConnectionState.Broken)
                { con.Close(); }
                if (con.State == ConnectionState.Closed)
                { con.Open(); }

                cmd = new SqlCommand("USP_Customer_CloseMeeting", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginId", cs.nLoginId);
                cmd.Parameters.AddWithValue("@MeetingId", cs.MeetingId);
                cmd.Parameters.AddWithValue("@vClosureRemarks", cs.vClosureRemarks);

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    objResp.vErrorMsg = "success";
                }
                else
                {
                    objResp.vErrorMsg = "error";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Function Name : Fn_Close_CustomerMeeting", " " + "Error Msg : " + ex.Message.ToString(), new StackTrace(ex, true));
                objResp.vErrorMsg = ex.Message.ToString();
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            return objResp;
        }

        public clsCustomerVisitor Fn_Update_Meeting(clsCustomerVisitor cs)
        {
            var objResp = new clsCustomerVisitor();
            Generic _generic = new Generic();
            try
            {
                if (cs.nLoginId == 0)
                {
                    objResp.vErrorMsg = "Login Id is not supplied";
                }

                if (string.IsNullOrWhiteSpace(cs.vCustomerName))
                {
                    objResp.vErrorMsg = "Customer Name can't be Empty";
                }
                else
                {
                    if (_generic.IsValidString(cs.vCustomerName))
                    {
                        objResp.vErrorMsg = "Please Enter only characters.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (string.IsNullOrWhiteSpace(cs.vEmailId))
                {
                    objResp.vErrorMsg = "Email ID can't be Empty";
                }
                else
                {
                    if (!_generic.IsValidEmail(cs.vEmailId))
                    {
                        objResp.vErrorMsg = "Enter Valid Email Id.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (string.IsNullOrWhiteSpace(cs.vMobile))
                {
                    objResp.vErrorMsg = "Mobile Number can't be Empty.";
                }
                else
                {
                    if (!_generic.IsValidMobile(cs.vMobile))
                    {
                        objResp.vErrorMsg = "Enter Valid Mobile Number.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (string.IsNullOrWhiteSpace(cs.vCompanyName))
                {
                    objResp.vErrorMsg = "Company Name can't be blank.";
                }

                if (string.IsNullOrWhiteSpace(cs.vAddress1))
                {
                    objResp.vErrorMsg = "Address can't be blank.";
                }

                if (string.IsNullOrWhiteSpace(cs.vState))
                {
                    objResp.vErrorMsg = "State can't be blank.";
                }
                else
                {
                    if (_generic.IsValidString(cs.vState))
                    {
                        objResp.vErrorMsg = "Please enter only characters.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (string.IsNullOrWhiteSpace(cs.vCity))
                {
                    objResp.vErrorMsg = "City can't be blank.";
                }

                if (string.IsNullOrWhiteSpace(cs.vPinCode))
                {
                    objResp.vErrorMsg = "PIN Code can't be Empty.";
                }
                else
                {
                    if (!_generic.IsValidPin(cs.vPinCode))
                    {
                        objResp.vErrorMsg = "Please Enter only Numbers.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (string.IsNullOrWhiteSpace(cs.vMeetingVenue))
                {
                    objResp.vErrorMsg = "Meeting Venue can't be blank.";
                }

                if (string.IsNullOrWhiteSpace(cs.vMeetingDate))
                {
                    objResp.vErrorMsg = "Meeting Date can't be blank.";
                }

                if (string.IsNullOrWhiteSpace(cs.vMeetingPurposevisit))
                {
                    objResp.vErrorMsg = "Meeting Purpose of Visit can't be blank.";
                }

                if (string.IsNullOrWhiteSpace(cs.vMeetingCommunication))
                {
                    objResp.vErrorMsg = "Meeting Communication can't be blank.";
                }

                if (objResp.vErrorMsg == "")
                {
                    if (con.State == ConnectionState.Broken)
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed)
                    { con.Open(); }

                    string strSql = "Update TBL_CustMeetingMaster set MeetingVenue= @MeetingVenue, MeetingDate= @MeetingDate, vStatus = @vStatus,";
                    strSql = strSql + " MeetingPurposeOfVisit= @MeetingPurposeOfVisit, MeetingCommunication= @MeetingCommunication  where MeetingId= @MeetingId";
                    strSql = strSql + " AND LoginId = @LoginId";
                    SqlCommand cmd1 = new SqlCommand(strSql, con);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Parameters.AddWithValue("@MeetingVenue", cs.vMeetingVenue);
                    cmd1.Parameters.AddWithValue("@MeetingDate", Convert.ToDateTime(cs.vMeetingDate));
                    if (cs.vClosureRemarks == "SS")
                    {
                        cmd1.Parameters.AddWithValue("@vStatus", "Open");
                    }
                    else
                    {
                        cmd1.Parameters.AddWithValue("@vStatus", cs.vStatus);
                    }
                    cmd1.Parameters.AddWithValue("@MeetingPurposeOfVisit", cs.vMeetingPurposevisit);
                    cmd1.Parameters.AddWithValue("@MeetingCommunication", cs.vMeetingCommunication);
                    cmd1.Parameters.AddWithValue("@MeetingId", cs.MeetingId);
                    cmd1.Parameters.AddWithValue("LoginId", cs.nLoginId);
                    int i1 = cmd1.ExecuteNonQuery();

                    if (i1 >= 1)
                    {
                        objResp.vErrorMsg = "";
                    }
                    else
                    {
                        objResp.vErrorMsg = "Failed to insert Meeting Master.";
                    }
                    cmd1.Dispose();
                    con.Close();
                }

                //  Insert meeting detail

                if (string.IsNullOrWhiteSpace(Convert.ToString(cs.MeetingId)))
                {
                    objResp.vErrorMsg = "Meeting Id can't be blank";
                }
                else
                {
                    if (objResp.vErrorMsg == "")
                    {
                        var html = ""; int SrCount = 1;
                        if (cs.FollowUpID == 0)
                        {
                            if (con.State == ConnectionState.Broken)
                            { con.Close(); }
                            if (con.State == ConnectionState.Closed)
                            { con.Open(); }

                            string strSql = "Delete from TBL_CustMeetingDetail where MeetingId=" + cs.MeetingId + "";
                            SqlCommand cmd = new SqlCommand(strSql, con);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();

                            foreach (clsProductList info in cs.oProductList)
                            {
                                if (info.vProductCategory != "0" && info.vMaterialCode != "0")
                                {
                                    string strImagePath = "";

                                    strImagePath = "https://103.67.180.170:8025/web/Images/" + info.vProductCategory + "/" + info.vMaterialCode + ".jpeg";

                                    SqlCommand cmd2 = new SqlCommand("USP_Insert_CustMeetingDetail", con);
                                    cmd2.CommandType = CommandType.StoredProcedure;
                                    cmd2.Parameters.AddWithValue("@MeetingId", cs.MeetingId);
                                    cmd2.Parameters.AddWithValue("@ProductCategory", info.vProductCategory);
                                    cmd2.Parameters.AddWithValue("@MaterialCode", info.vMaterialCode);
                                    cmd2.Parameters.AddWithValue("@ScanCode", info.vScanCode);
                                    int i2 = cmd2.ExecuteNonQuery();
                                    if (i2 >= 1)
                                    {
                                        objResp.vErrorMsg = "Success";
                                    }
                                    else
                                    {
                                        objResp.vErrorMsg = "Failed to insert Meeting Detail";
                                    }
                                    cmd2.Dispose();

                                    html = html + "<tr style='border: 1px solid #D0D0D0;'>";
                                    html = html + "<td style='text-align: center; border: 1px solid #D0D0D0; background-color: #eaf1be; padding: 10px; color: #10426C; font-size: 16px; font-family: 'Open-sans', sans-serif; font-weight: 400;'>" + SrCount + "</td>";
                                    html = html + "<td style='text-align: center; border: 1px solid #D0D0D0; background-color: #eaf1be; padding: 10px; color: #10426C; font-size: 16px; font-family: 'Open-sans', sans-serif; font-weight: 400;'>" + info.vMaterialCode + "</td>";
                                    html = html + "<td style='text-align: center; border: 1px solid #D0D0D0; background-color: #eaf1be; padding: 10px; color: #10426C; font-size: 16px; font-family: 'Open-sans', sans-serif; font-weight: 400;'>" + info.vProductCategory + "</td>";
                                    html = html + "<td><img alt='img' height='50px' width='50px' src=" + strImagePath + "></td>";
                                    html = html + "<td style='text-align: right; border: 1px solid #D0D0D0; background-color: #eaf1be; padding: 10px; color: #10426C; font-size: 16px; font-family: 'Open-sans', sans-serif; font-weight: 400;'>";
                                    SrCount++;
                                }
                            }
                        }
                        else
                        {
                            objResp.vErrorMsg = "Success";
                        }
                        objResp.MeetingId = cs.MeetingId;
                        objResp.vEmailId = cs.vEmailId;
                        if (cs.vClosureRemarks == "SS")
                        {
                            string filename = System.Web.Hosting.HostingEnvironment.MapPath("~/Email/MeetingTemp.html");
                            string mailbody = System.IO.File.ReadAllText(filename);

                            mailbody = mailbody.Replace("##MEETINGID##", Convert.ToString(cs.MeetingId));
                            mailbody = mailbody.Replace("##MEETINGDATE##", cs.vMeetingDate);
                            mailbody = mailbody.Replace("##CUSTOMERNAME##", cs.vCustomerName);
                            mailbody = mailbody.Replace("##COMPANYNAME##", cs.vCompanyName);
                            mailbody = mailbody.Replace("##EMAIL##", cs.vEmailId);
                            mailbody = mailbody.Replace("##CONTACT##", cs.vMobile);
                            mailbody = mailbody.Replace("##ADDRESS##", cs.vAddress1);
                            mailbody = mailbody.Replace("##REMARK##", cs.vMeetingCommunication);
                            mailbody = mailbody.Replace("##PRODUCTROW##", html);

                            string ToEmail = cs.vEmailId;

                            gn.TriggerEmailNoAttachment(mailbody, "Meeting ID " + Convert.ToString(cs.MeetingId) + " Status is Open", ToEmail, "");
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Function Name : Fn_Update_Meeting", " " + "Error Msg : " + ex.Message.ToString(), new StackTrace(ex, true));
                objResp.vErrorMsg = ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public clsCustomerVisitor Fn_Insert_CustMaster(clsCustomerVisitor cs)
        {
            var objResp = new clsCustomerVisitor();
            Generic _generic = new Generic();
            try
            {
                if (cs.nLoginId == 0)
                {
                    objResp.vErrorMsg = "Login Id is not supplied.";
                }

                if (string.IsNullOrWhiteSpace(cs.vCustomerName))
                {
                    objResp.vErrorMsg = "Customer Name can't be Empty";
                }
                else
                {
                    if (_generic.IsValidString(cs.vCustomerName))
                    {
                        objResp.vErrorMsg = "Please Enter only characters.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (string.IsNullOrWhiteSpace(cs.vEmailId))
                {
                    objResp.vErrorMsg = "Email ID can't be Empty";
                }
                else
                {
                    if (!_generic.IsValidEmail(cs.vEmailId))
                    {
                        objResp.vErrorMsg = "Enter Valid Email Id.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (string.IsNullOrWhiteSpace(cs.vMobile))
                {
                    objResp.vErrorMsg = "Mobile Number can't be Empty.";
                }
                else
                {
                    if (!_generic.IsValidMobile(cs.vMobile))
                    {
                        objResp.vErrorMsg = "Enter Valid Mobile Number.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (string.IsNullOrWhiteSpace(cs.vCompanyName))
                {
                    objResp.vErrorMsg = "Company Name can't be blank.";
                }

                if (string.IsNullOrWhiteSpace(cs.vAddress1))
                {
                    objResp.vErrorMsg = "Address can't be blank.";
                }

                if (string.IsNullOrWhiteSpace(cs.vState))
                {
                    objResp.vErrorMsg = "State can't be blank.";
                }
                else
                {
                    if (_generic.IsValidString(cs.vState))
                    {
                        objResp.vErrorMsg = "Please enter only characters.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (string.IsNullOrWhiteSpace(cs.vCity))
                {
                    objResp.vErrorMsg = "City can't be blank.";
                }
                if (string.IsNullOrWhiteSpace(cs.vPinCode))
                {
                    objResp.vErrorMsg = "PIN Code can't be Empty.";
                }
                else
                {
                    if (!_generic.IsValidPin(cs.vPinCode))
                    {
                        objResp.vErrorMsg = "Please Enter only Numbers.";
                    }
                    else
                    {
                        objResp.vErrorMsg = "";
                    }
                }

                if (objResp.vErrorMsg == "")
                {
                    if (con.State == ConnectionState.Broken)
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed)
                    { con.Open(); }

                    cmd = new SqlCommand("USP_Insert_CustMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustName", cs.vCustomerName);
                    cmd.Parameters.AddWithValue("@Email", cs.vEmailId);
                    cmd.Parameters.AddWithValue("@Mobile", cs.vMobile);
                    cmd.Parameters.AddWithValue("@Address", cs.vAddress1);
                    cmd.Parameters.AddWithValue("@State", cs.vState);
                    cmd.Parameters.AddWithValue("@City", cs.vCity);
                    cmd.Parameters.AddWithValue("@PinCode", cs.vPinCode);
                    cmd.Parameters.AddWithValue("@CompanyName", cs.vCompanyName);
                    cmd.Parameters.Add("@CustId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        objResp.vErrorMsg = "";
                    }
                    else
                    {
                        objResp.vErrorMsg = "Failed to insert customer detail.";
                    }
                    cs.CustId = Convert.ToInt32(cmd.Parameters["@CustId"].Value);
                    cmd.Dispose();

                    con.Close();
                    if (cs.CustId != 0)
                    {
                        objResp.vErrorMsg = "";
                    }
                    // Insert meeting master


                    if (string.IsNullOrWhiteSpace(cs.vMeetingVenue))
                    {
                        objResp.vErrorMsg = "Meeting Venue can't be blank.";
                    }

                    if (string.IsNullOrWhiteSpace(cs.vMeetingDate))
                    {
                        objResp.vErrorMsg = "Meeting Date can't be blank.";
                    }

                    if (string.IsNullOrWhiteSpace(cs.vMeetingPurposevisit))
                    {
                        objResp.vErrorMsg = "Meeting Purpose of Visit can't be blank.";
                    }

                    if (string.IsNullOrWhiteSpace(cs.vMeetingCommunication))
                    {
                        objResp.vErrorMsg = "Meeting Communication can't be blank.";
                    }

                    if (objResp.vErrorMsg == "")
                    {
                        if (con.State == ConnectionState.Broken)
                        { con.Close(); }
                        if (con.State == ConnectionState.Closed)
                        { con.Open(); }

                        SqlCommand cmd11 = new SqlCommand("USP_Insert_CustMeetingMaster", con);
                        cmd11.CommandType = CommandType.StoredProcedure;
                        cmd11.Parameters.AddWithValue("@MeetingId", cs.MeetingId);
                        cmd11.Parameters.AddWithValue("@CustId", cs.CustId);
                        cmd11.Parameters.AddWithValue("@MeetingVenue", cs.vMeetingVenue);
                        cmd11.Parameters.AddWithValue("@MeetingDate", cs.vMeetingDate);
                        cmd11.Parameters.AddWithValue("@MeetingPurposeOfVisit", cs.vMeetingPurposevisit);
                        cmd11.Parameters.AddWithValue("@MeetingCommunication", cs.vMeetingCommunication);
                        cmd11.Parameters.AddWithValue("@LoginId", cs.nLoginId);
                        cmd11.Parameters.AddWithValue("@Status", "Open");
                        cmd11.Parameters.AddWithValue("@QueryType", "Update");
                        int j = cmd11.ExecuteNonQuery();
                        if (j >= 1)
                        {
                            objResp.vErrorMsg = "";
                        }
                        else
                        {
                            objResp.vErrorMsg = "Failed to insert Meeting Master.";
                        }
                        cmd11.Dispose();
                        con.Close();
                    }

                    //  Insert meeting detail

                    if (string.IsNullOrWhiteSpace(Convert.ToString(cs.MeetingId)))
                    {
                        objResp.vErrorMsg = "Meeting Id can't be blank";
                    }
                    else
                    {
                        if (objResp.vErrorMsg == "")
                        {
                            if (con.State == ConnectionState.Broken)
                            { con.Close(); }
                            if (con.State == ConnectionState.Closed)
                            { con.Open(); }

                            foreach (clsProductList info in cs.oProductList)
                            {
                                if (info.vProductCategory != "0" && info.vMaterialCode != "0")
                                {

                                    SqlCommand cmd2 = new SqlCommand("USP_Insert_CustMeetingDetail", con);
                                    cmd2.CommandType = CommandType.StoredProcedure;
                                    cmd2.Parameters.AddWithValue("@MeetingId", cs.MeetingId);
                                    cmd2.Parameters.AddWithValue("@ProductCategory", info.vProductCategory);
                                    cmd2.Parameters.AddWithValue("@MaterialCode", info.vMaterialCode);
                                    cmd2.Parameters.AddWithValue("@ScanCode", info.vScanCode);
                                    int i2 = cmd2.ExecuteNonQuery();
                                    if (i2 >= 1)
                                    {
                                        objResp.vErrorMsg = "Success";
                                    }
                                    else
                                    {
                                        objResp.vErrorMsg = "Failed to insert Meeting Detail";
                                    }
                                    cmd2.Dispose();
                                }
                            }
                            con.Close();
                            objResp.MeetingId = cs.MeetingId;
                            objResp.vEmailId = cs.vEmailId;
                            objResp.vCustomerName = cs.vCustomerName;
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Function Name : Fn_Insert_CustMaster", " " + "Error Msg : " + ex.Message.ToString(), new StackTrace(ex, true));
                objResp.vErrorMsg = ex.Message.ToString();
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            return objResp;
        }

        public clsCustomerVisitor Fn_Send_Mail_Meeting_Communication(clsCustomerVisitor cs)
        {
            var objResp = new clsCustomerVisitor();
            try
            {
                if (con.State == ConnectionState.Broken)
                { con.Close(); }
                if (con.State == ConnectionState.Closed)
                { con.Open(); }

                string strSql = "Update TBL_CustMeetingMaster set vMailCommuniction= @vMailCommuniction where MeetingId= @MeetingId";
                cmd = new SqlCommand(strSql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@vMailCommuniction", cs.vMeetingCommunication);
                cmd.Parameters.AddWithValue("@MeetingId", cs.MeetingId);
                int i;
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    string strSubject = "##" + cs.MeetingId + "## Meeting With " + cs.vCustomerName;
                    Fn_Send_Mail(cs.vEmailId, strSubject, cs.vMeetingCommunication);
                    objResp.vErrorMsg = "Success";
                }
                else
                {
                    objResp.vErrorMsg = "Mail communication Failed";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Function Name : Fn_Send_Mail_Meeting_Communication", " " + "Error Msg : " + ex.Message.ToString(), new StackTrace(ex, true));
                objResp.vErrorMsg = ex.Message.ToString();
            }
            finally
            {
                con.Close();
                //cmd.Dispose();
            }
            return objResp;
        }


        public void Fn_Send_Mail(string vToMailID, string vSubject, string vMessage)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(vToMailID);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["Email"]);
            mail.Subject = vSubject;
            string Body = vMessage;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["Server"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email"], ConfigurationManager.AppSettings["EPwd"]); // Enter seders User name and password       
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        public clsProductList Fn_Get_MaterialCode(clsProductList objReq)
        {
            var _productmodel = new clsProductList();

            try
            {
                if (con.State == ConnectionState.Broken)
                { con.Close(); }
                if (con.State == ConnectionState.Closed)
                { con.Open(); }

                cmd = new SqlCommand("USP_Get_MaterialCode_BY_ScanCode", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@vScanCode", objReq.vScanCode);
                cmd.Parameters.AddWithValue("@vTBLName", objReq.vTBLName);
                cmd.Parameters.AddWithValue("@vProductCategory", objReq.vProductCategory);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _productmodel.vScanCode = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    _productmodel.vMaterialCode = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    _productmodel.vProductCategory = Convert.ToString(ds.Tables[0].Rows[0][1]);
                    _productmodel.vErrorMsg = "Success";
                }
                else
                {
                    _productmodel.vErrorMsg = "Material code not found.";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Function Name : Fn_Get_MaterialCode", " " + "Error Msg : " + ex.Message.ToString(), new StackTrace(ex, true));
                _productmodel.vErrorMsg = ex.Message.ToString();
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return _productmodel;
        }

    }
}