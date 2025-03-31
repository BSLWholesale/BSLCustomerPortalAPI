using BSLCustomerPortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BSLCustomerPortalAPI.Data_Access_Layer
{
    public class DALCustomer
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        DataSet ds;

        public clsCustomer Fn_LogIn_Customer(clsCustomer objReq)
        {
            var objResp = new clsCustomer();
            try
            {
                if (objReq.CustomerId == null || Convert.ToInt32(objReq.CustomerId) == 0)
                {
                    objResp.ErrorMsg = "Please Enter Customer Id";
                }
                else if (String.IsNullOrWhiteSpace(objReq.CustPassword))
                {
                    objResp.ErrorMsg = "Please Enter Password";
                }
                else
                {
                    if (con.State == ConnectionState.Broken)
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed)
                    { con.Open(); }

                    string encryptPassword = Generic.EncryptText(objReq.CustPassword);

                    cmd = new SqlCommand("USP_SAP_CustomerLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustId", objReq.CustomerId);
                    cmd.Parameters.AddWithValue("@CustPassword", encryptPassword);
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        string decryptPassword = Generic.DecryptText(Convert.ToString(dr["CPassword"]));
                        objResp.CustomerId = Convert.ToInt32(dr["CustId"]);
                        objResp.CustEmailId = Convert.ToString(dr["EmailId"]);
                        objResp.CustCompanyName = Convert.ToString(dr["CompanyName"]);
                        objResp.CustMobile = Convert.ToString(dr["Mobile"]);
                        objResp.CustName = Convert.ToString(dr["CustName"]);
                        objResp.CustPassword = decryptPassword;

                        objResp.ErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.ErrorMsg = "Failed";
                    }
                    dr.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_LogIn_Customer", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.ErrorMsg = exp.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public clsFeedback Fn_Add_Feedback(clsFeedback objReq)
        {
            var objResp = new clsFeedback();
            try
            {
                if (String.IsNullOrWhiteSpace(objReq.Heading))
                {
                    objResp.vErrorMsg = "Please Enter Heading";
                }
                else if (String.IsNullOrWhiteSpace(objReq.Comments))
                {
                    objResp.vErrorMsg = "Please Enter Comments";
                }
                else
                {
                    if(con.State == ConnectionState.Broken) { con.Open(); }
                    if (con.State == ConnectionState.Closed) { con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_CP_Feedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Heading", objReq.Heading);
                    cmd.Parameters.AddWithValue("@Comments", objReq.Comments);
                    cmd.Parameters.AddWithValue("@vStatus", objReq.vStatus);
                    cmd.Parameters.AddWithValue("@UserId", objReq.UserId);
                    cmd.Parameters.AddWithValue("@QueryType", "Insert");
                    int i = 0;
                    i = cmd.ExecuteNonQuery();
                    if(i> 0)
                    {
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorMsg = "Faild to record";
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Add_Feedback", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public List<clsFeedback> Fn_Get_Feedback(clsFeedback objReq)
        {
            List<clsFeedback> objResp = new List<clsFeedback>();
            var obj = new clsFeedback();
            try
            {
                if (objReq.UserId == 0)
                {
                    obj.vErrorMsg = "Please send UserId";
                    objResp.Add(obj);
                }
                else
                {
                    if (con.State == ConnectionState.Broken) { con.Open(); }
                    if (con.State == ConnectionState.Closed) { con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_CP_Feedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@vStatus", objReq.vStatus);
                    cmd.Parameters.AddWithValue("@UserId", objReq.UserId);
                    cmd.Parameters.AddWithValue("@QueryType", "Select");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    int i = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        while(ds.Tables[0].Rows.Count > i)
                        {
                            obj = new clsFeedback();
                            obj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"]);
                            obj.Heading = Convert.ToString(ds.Tables[0].Rows[i]["Heading"]);
                            obj.Comments = Convert.ToString(ds.Tables[0].Rows[i]["Comments"]);
                            obj.vStatus = Convert.ToString(ds.Tables[0].Rows[i]["vStatus"]);
                            obj.RequestDate = Convert.ToString(ds.Tables[0].Rows[i]["RequestDate"]);
                            obj.vErrorMsg = "Success";
                            objResp.Add(obj);

                            i++;

                        }
                    }
                    else
                    {
                        obj.vErrorMsg = "No Record Found";
                        objResp.Add(obj);
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_Feedback", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }
    }
}