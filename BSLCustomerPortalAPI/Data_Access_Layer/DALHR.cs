using BSLCustomerPortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BSLCustomerPortalAPI.Data_Access_Layer
{
    public class DALHR
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);

        public clsITHelpdesk Fn_Create_Tickets(clsITHelpdesk objReq)
        {
            var objResp = new clsITHelpdesk();
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
                    if (con.State == ConnectionState.Broken)
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed)
                    { con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_CP_ITHelpdesk", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", objReq.Id);
                    cmd.Parameters.AddWithValue("@Heading", objReq.Heading);
                    cmd.Parameters.AddWithValue("@Comments", objReq.Comments);
                    //cmd.Parameters.AddWithValue("@vStatus", objReq.vStatus);
                    cmd.Parameters.AddWithValue("@UserId", objReq.UserId);
                    cmd.Parameters.AddWithValue("@QueryType", objReq.QueryType);
                    int i = 0;
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorMsg = "Failed";
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Create_Tickets", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public List<clsITHelpdesk> Fn_Get_Tickets(clsITHelpdesk objReq)
        {
            List<clsITHelpdesk> objResp = new List<clsITHelpdesk>();
            var obj = new clsITHelpdesk();
            try
            {
                if (objReq.UserId == 0)
                {
                    obj.vErrorMsg = "Please send UserId";
                    objResp.Add(obj);
                }
                else
                {
                    if (con.State == ConnectionState.Broken)
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed)
                    { con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_CP_ITHelpdesk", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@vStatus", objReq.vStatus);
                    cmd.Parameters.AddWithValue("@Id", objReq.Id);
                    cmd.Parameters.AddWithValue("@UserId", objReq.UserId);
                    cmd.Parameters.AddWithValue("@QueryType", objReq.QueryType);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    int i = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        while (ds.Tables[0].Rows.Count > i)
                        {
                            obj = new clsITHelpdesk();
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
                Logger.WriteLog("Function Name : Fn_Get_Tickets", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
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