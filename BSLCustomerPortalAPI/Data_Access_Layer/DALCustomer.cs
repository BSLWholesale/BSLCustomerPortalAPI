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


        public clsCustomerContactMessages Fn_Insert_CustomerContactMessage(clsCustomerContactMessages objReq)
        {
            var objResp = new clsCustomerContactMessages();
            try
            {
                if (objReq.CustId == null || Convert.ToInt32(objReq.CustId) == 0)
                {
                    objResp.ErrorMsg = "Please Enter Customer Id";
                }
                else if (!String.IsNullOrWhiteSpace(objReq.Name))
                {
                    objResp.ErrorMsg = "Please Enter Your Name.";
                }
                else if (!String.IsNullOrWhiteSpace(objReq.Email))
                {
                    objResp.ErrorMsg = "Please Enter Your Email ID";
                }
                else if (!String.IsNullOrWhiteSpace(objReq.Mobile))
                {
                    objResp.ErrorMsg = "Please Enter Your Mobile Number";
                }
                else if (!String.IsNullOrWhiteSpace(objReq.Address))
                {
                    objResp.ErrorMsg = "Please Enter Your Address";
                }
                else if (!String.IsNullOrWhiteSpace(objReq.Message))
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


        public string Fn_SendMail_CustomerContactMessages(clsCustomerContactMessages objResp)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(ConfigurationManager.AppSettings["Email"]);
                mail.CC.Add(objResp.Email);
                mail.Subject = objResp.Name + " " + "trying to Contact You";
                string Body = "<b>Dear CustomerSupport," + "</b><br><br>" + "This message is send by" + "<br><br>" + "<b>Email : </b>" + objResp.Email + "<br>" + "<b>Mobile No : </b>" + objResp.Mobile + "<br><b> Message : </b>" + objResp.Message + "<br><br> Kindly revert to this query ASAP." + "<br><br><b> Thanks & Regards <br></b>" + objResp.Name + "<br>" + objResp.Mobile;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["Server"], Convert.ToInt32(ConfigurationManager.AppSettings["Port"])))
                {
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email"], ConfigurationManager.AppSettings[""]);
                    smtp.Host = ConfigurationManager.AppSettings["Server"];
                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    objResp.ErrorMsg = "Success";
                }
                //return objResp.ErrorMsg;
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_SendMail_CustomerContactMessages", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.ErrorMsg = exp.Message.ToString();
            }
            return objResp.ErrorMsg;
        }



    }
}