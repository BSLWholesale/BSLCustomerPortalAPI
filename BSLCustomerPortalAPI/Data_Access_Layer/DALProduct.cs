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

                SqlCommand cmd = new SqlCommand(strSql, con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.GDType))
                {
                    cmd.Parameters.AddWithValue("@GDType", objReq.GDType);
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


        public List<clsYarn> Fn_Get_Yarn_CatalogueDetail(clsYarn objReq)
        {
            var _YarnCatalogue = new List<clsYarn>();
            string strSql = "";
            try
            {
                if (objReq.vTBLName == "Archieve")
                {
                    strSql = "SELECT * FROM YARNMasterArchieve WHERE 1=1";
                }
                else
                {
                    strSql = "SELECT TOP 500 * FROM YARNMaster WHERE 1=1";
                }

                if (!String.IsNullOrWhiteSpace(objReq.GDType))
                {
                    strSql = strSql + " AND GDType='" + objReq.GDType + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.CountType))
                {
                    strSql = strSql + " AND CountType='" + objReq.CountType + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.BlendDescription))
                {
                    strSql = strSql + " AND BlendDescription='" + objReq.BlendDescription + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SLUB))
                {
                    if (objReq.SLUB == "No Slub")
                    {
                        strSql = strSql + " AND SLUB is null";
                    }
                    else
                    {
                        strSql = strSql + " AND SLUB='" + objReq.SLUB + "'";
                    }
                }
                if (!String.IsNullOrWhiteSpace(objReq.Technique))
                {
                    strSql = strSql + " AND Technique='" + objReq.Technique + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Quality))
                {
                    strSql = strSql + " AND Quality='" + objReq.Quality + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.MaterialCode))
                {
                    strSql = strSql + " AND MaterialCode='" + objReq.MaterialCode + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SpecialFeature))
                {
                    strSql = strSql + " AND SpecialFeature LIKE '%" + objReq.SpecialFeature + "%'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.CountRange))
                {
                    strSql = strSql + " AND CountRange='" + objReq.CountRange + "'";
                }
                if (objReq.vTBLName != "Archieve")
                {
                    strSql = strSql + "ORDER BY CreatedOn DESC";
                }

                if (con.State == ConnectionState.Broken)
                { con.Close(); }
                if (con.State == ConnectionState.Closed)
                { con.Open(); }

                SqlDataAdapter da = new SqlDataAdapter(strSql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        var YarnCatalogue = new clsYarn();
                        YarnCatalogue.MaterialCode = Convert.ToString(ds.Tables[0].Rows[i][0]);
                        YarnCatalogue.MaterialDescription = Convert.ToString(ds.Tables[0].Rows[i][1]);
                        YarnCatalogue.GDType = Convert.ToString(ds.Tables[0].Rows[i][2]);
                        YarnCatalogue.Ply = Convert.ToString(ds.Tables[0].Rows[i][3]);
                        YarnCatalogue.Count = Convert.ToString(ds.Tables[0].Rows[i][4]);
                        YarnCatalogue.CountType = Convert.ToString(ds.Tables[0].Rows[i][5]);
                        YarnCatalogue.BlendDescription = Convert.ToString(ds.Tables[0].Rows[i][6]);
                        YarnCatalogue.SLUB = Convert.ToString(ds.Tables[0].Rows[i][7]);
                        YarnCatalogue.Technique = Convert.ToString(ds.Tables[0].Rows[i][8]);
                        YarnCatalogue.Quality = Convert.ToString(ds.Tables[0].Rows[i][9]);
                        YarnCatalogue.HSNCode = Convert.ToString(ds.Tables[0].Rows[i]["HSNCode"]);
                        YarnCatalogue.SpecialFeature = Convert.ToString(ds.Tables[0].Rows[i]["SpecialFeature"]);
                        YarnCatalogue.CountRange = Convert.ToString(ds.Tables[0].Rows[i]["CountRange"]);
                        YarnCatalogue.vErrorMsg = "Success";

                        _YarnCatalogue.Add(YarnCatalogue);
                        i++;
                    }
                }
                else
                {
                    var YarnCatalogue = new clsYarn();
                    YarnCatalogue.vErrorMsg = "No Yarn Catalogue Found.";
                    _YarnCatalogue.Add(YarnCatalogue);
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_Yarn_CatalogueDetail", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                var YarnCatalogue = new clsYarn();
                YarnCatalogue.vErrorMsg = exp.Message.ToString();
                _YarnCatalogue.Add(YarnCatalogue);
            }
            finally
            {
                con.Close();
            }
            return _YarnCatalogue;
        }



    }
}