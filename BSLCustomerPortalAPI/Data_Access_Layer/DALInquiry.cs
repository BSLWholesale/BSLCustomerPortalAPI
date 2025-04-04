using BSLCustomerPortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Mail;


namespace BSLCustomerPortalAPI.Data_Access_Layer
{
    public class DALInquiry
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);
        Generic gn = new Generic();
        SqlCommand cmd;
        SqlDataReader dr;
        double dInqId;
        string strExistItem = string.Empty;
        string strInq;
        string strVendorEmail = string.Empty;

        public clsQuotation Fn_Make_Quotation(clsQuotation objReq)
        {
            var objResp = new clsQuotation();
            if (objReq.QueryType == "InsertMaster")
            {
                string strQuotationId = gn.Fn_Get_MAX_ID("QuotationMaster");
                objReq.QuotationId = strQuotationId;
            }
            try
            {
                if (String.IsNullOrWhiteSpace(objReq.CustomerName))
                {
                    objResp.vErrorMsg = "CustomerName is empty";
                }
                else if (String.IsNullOrWhiteSpace(objReq.ContactPerson))
                {
                    objResp.vErrorMsg = "ContactPerson is emply";
                }
                else if (String.IsNullOrWhiteSpace(objReq.QuotationId))
                {
                    objResp.vErrorMsg = "QuotationId is emply";
                }
                else if (objReq.UserId == 0)
                {
                    objResp.vErrorMsg = "UserId is emply";
                }
                else
                {
                    if (con.State == ConnectionState.Broken)
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed)
                    { con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_Quotation", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@QuotationId", objReq.QuotationId);
                    cmd.Parameters.AddWithValue("@CompanyName", objReq.CompanyName);
                    cmd.Parameters.AddWithValue("@CustomerName", objReq.CustomerName);
                    cmd.Parameters.AddWithValue("@ContactPerson", objReq.ContactPerson);
                    cmd.Parameters.AddWithValue("@UID", objReq.UserId);
                    cmd.Parameters.AddWithValue("@EmailId", objReq.EmailId);
                    cmd.Parameters.AddWithValue("@Remarks", objReq.vRemarks);
                    // cmd.Parameters.AddWithValue("@QueryType", "InsertMaster");
                    cmd.Parameters.AddWithValue("@UserEmail", objReq.vUserEmail);
                    cmd.Parameters.AddWithValue("@Validity", objReq.vValidity);
                    cmd.Parameters.AddWithValue("@QueryType", objReq.QueryType);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        foreach (var list in objReq._oList)
                        {
                            SqlCommand cm = new SqlCommand("USP_Quotation", con);
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.Parameters.AddWithValue("@QuotationId", objReq.QuotationId);
                            cm.Parameters.AddWithValue("@Article", list.vArticle);
                            cm.Parameters.AddWithValue("@Blend", list.vBlend);
                            cm.Parameters.AddWithValue("@Weight", list.vWeight);
                            cm.Parameters.AddWithValue("@Width", list.vWidth);
                            cm.Parameters.AddWithValue("@Price", list.vPrice);
                            cm.Parameters.AddWithValue("@Unit", list.vUnit);
                            cm.Parameters.AddWithValue("@UnitType", list.vUnitType);
                            cm.Parameters.AddWithValue("@PaymentTerms", list.vPaymentTerms);
                            cm.Parameters.AddWithValue("@DeliveryTerms", list.vDeliveryTerms);

                            cm.Parameters.AddWithValue("@QueryType", "InsertDetail");

                            int j = cm.ExecuteNonQuery();
                            if (j > 0)
                            {
                                objResp.vErrorMsg = "";
                            }
                            else
                            {
                                objResp.vErrorMsg = "Faild";
                            }
                        }

                        if (objResp.vErrorMsg == "")
                        {
                            objResp.vErrorMsg = "Success";
                            objResp.QuotationId = objReq.QuotationId;
                        }
                        else
                        {
                            objResp.vErrorMsg = "Failed";
                        }
                    }
                    else
                    {
                        objResp.vErrorMsg = "Request Failed.";
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Make_Quotation", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public string Fn_Get_MAX_ID(string strTableName)
        {
            try
            {
                string prefix = string.Empty;

                if (con.State == ConnectionState.Broken)
                { con.Close(); }
                if (con.State == ConnectionState.Closed)
                { con.Open(); }


                string strSql;
                if (strTableName == "QuotationMaster")
                {
                    prefix = "QOT-";
                    strSql = "Select Concat(format(getdate(),'ddMMyyyy'), FORMAT(ISNULL(max(cast(substring(QuotationId,13,6) as int))+1,1),'000000')) from QuotationMaster where Convert(date, CreatedDate) = Convert(date, getdate())";
                }
                else
                {
                    strSql = "";
                }

                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    New_MAXId = prefix + dr[0].ToString();
                }
                else
                {
                    string dt = DateTime.Now.ToString("ddMMyyyy");
                    New_MAXId = prefix + dt + "000001";
                }
                dr.Close();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_MAX_ID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
            }
            finally
            {
                con.Close();
            }
            return New_MAXId;
        }

    }
}