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
    public class DALPayment
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);

        public List<clsPayment> Fn_Get_Payment(clsPayment objReq)
        {
            List<clsPayment> objResp = new List<clsPayment>();
            var obj = new clsPayment();
            try
            {
                if (con.State == ConnectionState.Broken) { con.Close(); }
                if (con.State == ConnectionState.Closed) { con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_CP_PAYMENT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "Select");
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > i)
                {
                    while(ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsPayment();
                        obj.CustomerCode = Convert.ToString(ds.Tables[0].Rows[i]["CustomerCode"]);
                        obj.CustomerName = Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        obj.DocumentType = Convert.ToString(ds.Tables[0].Rows[i]["DocumentType"]);
                        obj.BSLRefNo = Convert.ToString(ds.Tables[0].Rows[i]["BSLRefNo"]);
                        obj.InvNo = Convert.ToString(ds.Tables[0].Rows[i]["InvNo"]);
                        obj.InvDate = Convert.ToString(ds.Tables[0].Rows[i]["InvDate"]);
                        obj.DebitAmtINR = Convert.ToDouble(ds.Tables[0].Rows[i]["DebitAmtINR"]);
                        obj.CreditAmtINR = Convert.ToDouble(ds.Tables[0].Rows[i]["CreditAmtINR"]);
                        obj.BalanceINR = Convert.ToDouble(ds.Tables[0].Rows[i]["BalanceINR"]);
                        obj.DocCurrency = Convert.ToString(ds.Tables[0].Rows[i]["DocCurrency"]);
                        obj.DueDays = Convert.ToDouble(ds.Tables[0].Rows[i]["DueDays"]);
                        obj.SalesPersonName = Convert.ToString(ds.Tables[0].Rows[i]["SalesPersonName"]);
                        obj.DivisionDescription = Convert.ToString(ds.Tables[0].Rows[i]["DivisionDescription"]);
                        obj.OverDueDays = Convert.ToDouble(ds.Tables[0].Rows[i]["OverDueDays"]);
                        obj.PayTermDesc = Convert.ToString(ds.Tables[0].Rows[i]["PayTermDesc"]);
                        obj.SalesOrder = Convert.ToString(ds.Tables[0].Rows[i]["SalesOrder"]);
                        obj.DueDate = Convert.ToString(ds.Tables[0].Rows[i]["DueDate"]);
                        obj.FIDocument = Convert.ToString(ds.Tables[0].Rows[i]["FIDocument"]);
                        obj.FiscalYear = Convert.ToString(ds.Tables[0].Rows[i]["FiscalYear"]);

                        obj.InvQty = Convert.ToDouble(ds.Tables[0].Rows[i]["InvQty"]);
                        obj.LCDate = Convert.ToString(ds.Tables[0].Rows[i]["LCDate"]);
                        obj.LCNo = Convert.ToString(ds.Tables[0].Rows[i]["LCNo"]);
                        obj.CountryName = Convert.ToString(ds.Tables[0].Rows[i]["CountryName"]);
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
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Send_Sample_Request", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {

            }
            return objResp;
        }
    }
}