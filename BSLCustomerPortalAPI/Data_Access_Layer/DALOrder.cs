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
    public class DALOrder
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);
        Int64 NewRequestId;

        public clsSamplRequest Fn_Send_Sample_Request(clsSamplRequest objReq)
        {
            var objResp = new clsSamplRequest();
            Fn_Get_Max_InqId();
            try
            {
                if (NewRequestId == 0)
                {
                    objResp.vErrorMsg = "SampleId not supplied";
                }
                else if (String.IsNullOrWhiteSpace(objReq.vCategory))
                {
                    objResp.vErrorMsg = "Division is emply";
                }
                else if (objReq._oList.Count == 0 || objReq._oList == null)
                {
                    objResp.vErrorMsg = "Sample list is emply";
                }
                else
                {
                    if (con.State == ConnectionState.Broken)
                    {
                        con.Close();
                    }
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("USP_SampleRequest", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SampleId", NewRequestId);
                    cmd.Parameters.AddWithValue("@Division", objReq.vCategory);
                    cmd.Parameters.AddWithValue("@UID", objReq.UserId);
                    cmd.Parameters.AddWithValue("@Remarks", objReq.vRemark);
                    cmd.Parameters.AddWithValue("@QueryType", "InsertMaster");
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {

                        foreach (var list in objReq._oList)
                        {
                            SqlCommand cm = new SqlCommand("USP_SampleRequest", con);
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.Parameters.AddWithValue("@SampleId", NewRequestId);
                            cm.Parameters.AddWithValue("@ProductNo", list.vProduct);
                            cmd.Parameters.AddWithValue("@Remarks", list.vRemark);
                            if (objReq.vCategory == "Fabric")
                            {

                                cm.Parameters.AddWithValue("@ColourNo", list.Color);
                                cm.Parameters.AddWithValue("@ShadeCard", list.bShadeCard);
                                cm.Parameters.AddWithValue("@Yardage", list.bYardage);
                            }
                            else if (objReq.vCategory == "Garments")
                            {
                                cm.Parameters.AddWithValue("@Style", list.vStyle);
                                cm.Parameters.AddWithValue("@StyleNo", list.vStyleNo);
                                cm.Parameters.AddWithValue("@FabricQuality", list.vFabricQuality);
                            }
                            else if (objReq.vCategory == "Yarn")
                            {
                                cm.Parameters.AddWithValue("@Count", list.vCount);
                                cm.Parameters.AddWithValue("@BlendDescription", list.vBlendDescription);
                                cm.Parameters.AddWithValue("@BlendPercentage", list.vBlendPercentage);
                                cm.Parameters.AddWithValue("@EndUse", list.vEndUse);
                                cm.Parameters.AddWithValue("@Dye", list.vDye);
                            }
                            else { }
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
                        }
                        else
                        {
                            objResp.vErrorMsg = "Faild";
                        }

                    }
                    else
                    {
                        objResp.vErrorMsg = "Request Faild.";
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Send_Sample_Request", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public List<clsSamplRequest> Fn_Get_Sample_Request(clsSamplRequest objReq)
        {
            List<clsSamplRequest> objResp = new List<clsSamplRequest>();
            var obj = new clsSamplRequest();
            try
            {
                SqlCommand cmd = new SqlCommand("USP_SampleRequest", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UID", objReq.UserId);
                cmd.Parameters.AddWithValue("@QueryType", "SelectMaster");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsSamplRequest();

                        obj.RequestId = Convert.ToInt64(ds.Tables[0].Rows[i]["SampleId"]);
                        obj.vRequestDate = Convert.ToString(ds.Tables[0].Rows[i]["SampleDate"]);
                        obj.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UID"]);
                        obj.vStatus = Convert.ToString(ds.Tables[0].Rows[i]["vStatus"]);
                        obj.vCategory = Convert.ToString(ds.Tables[0].Rows[i]["Division"]);
                        obj.UserName = Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);

                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorMsg = "No Data";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_Sample_Request", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public List<clsSamplRequestList> Fn_Get_Sample_Request_Detail(clsSamplRequestList objReq)
        {
            List<clsSamplRequestList> objResp = new List<clsSamplRequestList>();
            var obj = new clsSamplRequestList();
            try
            {
                SqlCommand cmd = new SqlCommand("USP_SampleRequest", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SampleId", objReq.RequestId);
                cmd.Parameters.AddWithValue("@QueryType", "SelectDetail");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsSamplRequestList();

                        obj.RequestId = Convert.ToInt64(ds.Tables[0].Rows[i]["SampleId"]);
                        obj.CreatedDate = Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        obj.vProduct = Convert.ToString(ds.Tables[0].Rows[i]["ProductNo"]);

                        obj.Color = Convert.ToString(ds.Tables[0].Rows[i]["ColourNo"]);
                        string strShadeCard = Convert.ToString(ds.Tables[0].Rows[i]["ShadeCard"]);
                        if (strShadeCard != "")
                        {
                            obj.bShadeCard = Convert.ToBoolean(ds.Tables[0].Rows[i]["ShadeCard"]);
                        }

                        string strYardage = Convert.ToString(ds.Tables[0].Rows[i]["Yardage"]);
                        if (strYardage != "")
                        {
                            obj.bYardage = Convert.ToBoolean(ds.Tables[0].Rows[i]["Yardage"]);
                        }

                        obj.vCount = Convert.ToString(ds.Tables[0].Rows[i]["vCount"]);
                        obj.vBlendDescription = Convert.ToString(ds.Tables[0].Rows[i]["vBlendDescription"]);
                        obj.vBlendPercentage = Convert.ToString(ds.Tables[0].Rows[i]["vBlendPercentage"]);
                        obj.vEndUse = Convert.ToString(ds.Tables[0].Rows[i]["vEndUse"]);
                        obj.vDye = Convert.ToString(ds.Tables[0].Rows[i]["vDye"]);

                        obj.vStyle = Convert.ToString(ds.Tables[0].Rows[i]["vStyle"]);
                        obj.vStyleNo = Convert.ToString(ds.Tables[0].Rows[i]["vStyleNo"]);
                        obj.vFabricQuality = Convert.ToString(ds.Tables[0].Rows[i]["vFabricQuality"]);

                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorMsg = "No Data";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_Sample_Request_Detail", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public Int64 Fn_Get_Max_InqId()
        {

            try
            {

                if (con.State == ConnectionState.Broken)
                { con.Close(); }
                if (con.State == ConnectionState.Closed)
                { con.Open(); }

                string strSql = "Select Concat(format(getdate(),'ddMMyyyy'),substring(format(isnull(max(SampleId)+1,1),'00000000000000'),9,6)) from SampleMaster where Convert(date,SampleDate)=Convert(date,getdate())";

                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    NewRequestId = Convert.ToInt64(dr[0].ToString());
                }
                else
                {
                    string dt = DateTime.Now.ToString("ddMMyyyy");
                    string Inq = dt + "000001";
                    NewRequestId = Convert.ToInt64(Inq);

                }
                dr.Close();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_Max_InqId", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                exp.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return NewRequestId;
        }
    }
}