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


    }
}