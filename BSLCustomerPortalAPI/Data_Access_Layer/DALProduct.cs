using BSLCustomerPortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace BSLCustomerPortalAPI.Data_Access_Layer
{
    public class DALProduct
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);

        public List<clsYarn> Fn_Get_Yarn_GDType(clsYarn objReq)
        {
            List<clsYarn> objResp = new List<clsYarn>();
            var obj = new clsYarn();
            try
            {
                if (con.State == ConnectionState.Broken) { con.Close(); }
                if (con.State == ConnectionState.Closed) { con.Open(); }

                string strSql = "Select Distinct GDType from YARNMaster";
                SqlDataAdapter da = new SqlDataAdapter(strSql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsYarn();
                        obj.GDType = Convert.ToString(ds.Tables[0].Rows[i]["GDType"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Yarn_GDType", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public List<clsYarn> Fn_Get_Search_Yarn(clsYarn objReq)
        {
            List<clsYarn> objResp = new List<clsYarn>();
            var obj = new clsYarn();
            try
            {
                if (con.State == ConnectionState.Broken) { con.Close(); }
                if (con.State == ConnectionState.Closed) { con.Open(); }

                string strSql = "Select Distinct MaterialCode,MaterialDescription,GDType, Ply,Count, CountType,BlendDescription,SLUB,";
                strSql = strSql + " Technique,Quality,PieceNo,HSNCode,SpecialFeature,CountRange from YARNMaster where 1=1";
                if(!String.IsNullOrWhiteSpace(objReq.GDType))
                {
                    strSql = strSql + " And GDType = @GDType";
                }
                if (!String.IsNullOrWhiteSpace(objReq.MaterialCode))
                {
                    strSql = strSql + " And MaterialCode = @MaterialCode";
                }

                SqlCommand cmd = new SqlCommand(strSql, con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.GDType))
                {
                    cmd.Parameters.AddWithValue("@GDType", objReq.GDType);
                }
                if (!String.IsNullOrWhiteSpace(objReq.MaterialCode))
                {
                    cmd.Parameters.AddWithValue("@MaterialCode", objReq.MaterialCode);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsYarn();
                        obj.MaterialCode = Convert.ToString(ds.Tables[0].Rows[i]["MaterialCode"]);
                        obj.MaterialDescription = Convert.ToString(ds.Tables[0].Rows[i]["MaterialDescription"]);
                        obj.GDType = Convert.ToString(ds.Tables[0].Rows[i]["GDType"]);
                        obj.Ply = Convert.ToString(ds.Tables[0].Rows[i]["Ply"]);
                        obj.Count = Convert.ToString(ds.Tables[0].Rows[i]["Count"]);
                        obj.CountType = Convert.ToString(ds.Tables[0].Rows[i]["CountType"]);
                        obj.BlendDescription = Convert.ToString(ds.Tables[0].Rows[i]["BlendDescription"]);
                        obj.SLUB = Convert.ToString(ds.Tables[0].Rows[i]["SLUB"]);
                        obj.Technique = Convert.ToString(ds.Tables[0].Rows[i]["Technique"]);
                        obj.Quality = Convert.ToString(ds.Tables[0].Rows[i]["Quality"]);
                        obj.PieceNo = Convert.ToString(ds.Tables[0].Rows[i]["PieceNo"]);
                        obj.HSNCode = Convert.ToString(ds.Tables[0].Rows[i]["HSNCode"]);
                        obj.SpecialFeature = Convert.ToString(ds.Tables[0].Rows[i]["SpecialFeature"]);
                        obj.CountRange = Convert.ToString(ds.Tables[0].Rows[i]["CountRange"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Search_Yarn", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public List<clsFabric> Fn_Get_Fabric_Blend(clsFabric objReq)
        {
            List<clsFabric> objResp = new List<clsFabric>();
            var obj = new clsFabric();
            try
            {
                if (con.State == ConnectionState.Broken) { con.Close(); }
                if (con.State == ConnectionState.Closed) { con.Open(); }

                string strSql = "Select Distinct BlendDescription from MaterialMast";
                SqlDataAdapter da = new SqlDataAdapter(strSql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsFabric();
                        obj.BlendDescription = Convert.ToString(ds.Tables[0].Rows[i]["BlendDescription"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Fabric_Blend", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public List<clsFabric> Fn_Get_Search_Fabric(clsFabric objReq)
        {
            List<clsFabric> objResp = new List<clsFabric>();
            var obj = new clsFabric();
            try
            {
                if (con.State == ConnectionState.Broken) { con.Close(); }
                if (con.State == ConnectionState.Closed) { con.Open(); }

                string strSql = "Select distinct MaterialCode, Quality, BlendDescription, BlendValue, WeaveType,";
                strSql = strSql + " GSM, StrechType, DesignPattern, Shade, Usage, Product, FinishType, PieceNo,";
                strSql = strSql + " FinishFabric, AddType, SpecialFeature, MaterialDescription, Base, TypeofDesign,";
                strSql = strSql + " FabricCategory, ShadeName from MaterialMast Where 1=1";
                if(!String.IsNullOrWhiteSpace(objReq.BlendDescription))
                {
                    strSql = strSql + " AND BlendDescription = @BlendDescription";
                }
                SqlCommand cmd = new SqlCommand(strSql, con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.BlendDescription))
                {
                    cmd.Parameters.AddWithValue("@BlendDescription", objReq.BlendDescription);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsFabric();
                        obj.MaterialCode = Convert.ToString(ds.Tables[0].Rows[i]["MaterialCode"]);
                        obj.Quality = Convert.ToString(ds.Tables[0].Rows[i]["Quality"]);
                        obj.BlendDescription = Convert.ToString(ds.Tables[0].Rows[i]["BlendDescription"]);
                        obj.BlendValue = Convert.ToString(ds.Tables[0].Rows[i]["BlendValue"]);
                        obj.WeaveType = Convert.ToString(ds.Tables[0].Rows[i]["WeaveType"]);
                        obj.GSM = Convert.ToInt32(ds.Tables[0].Rows[i]["GSM"]);
                        obj.StrechType = Convert.ToString(ds.Tables[0].Rows[i]["StrechType"]);
                        obj.DesignPattern = Convert.ToString(ds.Tables[0].Rows[i]["DesignPattern"]);
                        obj.Usage = Convert.ToString(ds.Tables[0].Rows[i]["Usage"]);
                        obj.Product = Convert.ToString(ds.Tables[0].Rows[i]["Product"]);
                        obj.FinishType = Convert.ToString(ds.Tables[0].Rows[i]["FinishType"]);
                        obj.PieceNo = Convert.ToString(ds.Tables[0].Rows[i]["PieceNo"]);

                        obj.FinishFabric = Convert.ToString(ds.Tables[0].Rows[i]["FinishFabric"]);
                        obj.AddType = Convert.ToString(ds.Tables[0].Rows[i]["AddType"]);
                        obj.SpecialFeature = Convert.ToString(ds.Tables[0].Rows[i]["SpecialFeature"]);
                        obj.MaterialDescription = Convert.ToString(ds.Tables[0].Rows[i]["MaterialDescription"]);
                        obj.Base = Convert.ToString(ds.Tables[0].Rows[i]["Base"]);
                        obj.TypeofDesign = Convert.ToString(ds.Tables[0].Rows[i]["TypeofDesign"]);
                        obj.FabricCategory = Convert.ToString(ds.Tables[0].Rows[i]["FabricCategory"]);
                        obj.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);

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
                Logger.WriteLog("Function Name : Fn_Get_Search_Fabric", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
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