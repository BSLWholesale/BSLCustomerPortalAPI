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
using System.Web.Http;
using System.Web.Mvc;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

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
                if (objReq.CustEmailId == "" || objReq.CustMobile == "")
                {
                    objResp.ErrorMsg = "Please Enter Customer Id Or Email Id";
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
                    //cmd.Parameters.AddWithValue("@CustomerId", objReq.CustEmailId);
                    cmd.Parameters.AddWithValue("@CustEmailId", objReq.CustEmailId);
                    cmd.Parameters.AddWithValue("@CustMobileNo", objReq.CustEmailId);
                    cmd.Parameters.AddWithValue("@CustPassword", encryptPassword);
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        objResp.CustomerId = Convert.ToInt32(dr["CustId"]);
                        objResp.SAPCustId = Convert.ToInt32(dr["SAPCustId"]);
                        objResp.CustEmailId = Convert.ToString(dr["CEmailId"]).Trim();
                        //objResp.CustEmailId = Convert.ToString(dr["EmailId"]);
                        objResp.CustCompanyName = Convert.ToString(dr["CompanyName"]).Trim();
                        objResp.CustMobile = Convert.ToString(dr["CMobileNo"]);
                        objResp.CustName = Convert.ToString(dr["CustName"]).Trim();
                        objResp.CustUserType = Convert.ToString(dr["CustUserType"]);
                        objResp.CustADRNR = Convert.ToString(dr["ADRNR"]);
                        objResp.CustCreatedDate = Convert.ToString(dr["CreateDate"]);
                        string decryptPassword = Generic.DecryptText(Convert.ToString(dr["CPassword"]));
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


        public clsCustomerContactMessages Fn_Insert_CustomerContactMessage(clsCustomerContactMessages objReq)
        {
            var objResp = new clsCustomerContactMessages();
            try
            {
                if (objReq.CustId == null || Convert.ToInt32(objReq.CustId) == 0)
                {
                    objResp.ErrorMsg = "Please Enter Customer Id";
                }
                else if (String.IsNullOrWhiteSpace(objReq.Name))
                {
                    objResp.ErrorMsg = "Please Enter Your Name.";
                }
                else if (String.IsNullOrWhiteSpace(objReq.Email))
                {
                    objResp.ErrorMsg = "Please Enter Your Email ID";
                }
                else if (String.IsNullOrWhiteSpace(objReq.Mobile))
                {
                    objResp.ErrorMsg = "Please Enter Your Mobile Number";
                }
                else if (String.IsNullOrWhiteSpace(objReq.Address))
                {
                    objResp.ErrorMsg = "Please Enter Your Address";
                }
                else if (String.IsNullOrWhiteSpace(objReq.Message))
                {
                    objResp.ErrorMsg = "Please Enter Your Message";
                }
                else
                {
                    if (con.State == ConnectionState.Broken)
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed)
                    { con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_Insert_CustContactMessages", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustId", objReq.CustId);
                    cmd.Parameters.AddWithValue("@Name", objReq.Name);
                    cmd.Parameters.AddWithValue("@Email", objReq.Email);
                    cmd.Parameters.AddWithValue("@Mobile", objReq.Mobile);
                    cmd.Parameters.AddWithValue("@Address", objReq.Address);
                    cmd.Parameters.AddWithValue("@Message", objReq.Message);

                    int i = cmd.ExecuteNonQuery();

                    if (i >= 1)
                    {
                        Fn_SendMail_CustomerContactMessages(objReq);
                        objResp.ErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.ErrorMsg = "Error";
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Insert_CustomerContactMessage", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
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
                    if(con.State == ConnectionState.Broken) 
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed) 
                    { con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_CP_Feedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", objReq.Id);
                    cmd.Parameters.AddWithValue("@Heading", objReq.Heading);
                    cmd.Parameters.AddWithValue("@Comments", objReq.Comments);
                    cmd.Parameters.AddWithValue("@vStatus", objReq.vStatus);
                    cmd.Parameters.AddWithValue("@UserId", objReq.UserId);
                    cmd.Parameters.AddWithValue("@QueryType", objReq.QueryType);
                    int i = 0;
                    i = cmd.ExecuteNonQuery();
                    if(i> 0)
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
                    if (con.State == ConnectionState.Broken) 
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed) 
                    { con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_CP_Feedback", con);
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

        public clsFeedback Fn_Delete_Feedback(clsFeedback objReq)
        {
            var objResp = new clsFeedback();
            try
            {
                if (objReq.Id == 0)
                {
                    objResp.vErrorMsg = "Id is not supplied";
                }
                else if (objReq.UserId == 0)
                {
                    objResp.vErrorMsg = "UserId is not supplied";
                }
                else
                {
                    if (con.State == ConnectionState.Broken) 
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed) 
                    { con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_CP_Feedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", objReq.Id);
                    cmd.Parameters.AddWithValue("@UserId", objReq.UserId);
                    cmd.Parameters.AddWithValue("@QueryType", "Delete");
                    int i = 0;
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorMsg = "Deleting Failed";
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Delete_Feedback", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public string Fn_SendMail_CustomerContactMessages(clsCustomerContactMessages objResp)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(ConfigurationManager.AppSettings["Email"]);
                mail.CC.Add(objResp.Email);
                mail.Subject = objResp.Name + " " + "trying to Contact You";
                string Body = "<b>Dear Support," + "</b><br><br>" + "This message is send by" + "<br><br>" + "<b>Email : </b>" + objResp.Email + "<br>" + "<b>Mobile No : </b>" + objResp.Mobile + "<br> <b>Message : </b>" + objResp.Message + "<br><br> Kindly revert to this query ASAP." + "<br><br><b> Thanks & Regards <br></b>" + objResp.Name + "<br>" + objResp.Mobile;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["Server"], Convert.ToInt32(ConfigurationManager.AppSettings["Port"])))
                {
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email"], ConfigurationManager.AppSettings["EPwd"]);
                    smtp.Host = ConfigurationManager.AppSettings["Server"];
                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    objResp.ErrorMsg = "success";
                }
                return objResp.ErrorMsg;
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_SendMail_CustomerContactMessages", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.ErrorMsg = exp.Message.ToString();
                return objResp.ErrorMsg;
            }
        }



    }
}