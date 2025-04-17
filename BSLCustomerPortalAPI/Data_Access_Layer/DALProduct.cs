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
                if (!String.IsNullOrWhiteSpace(objReq.MaterialCode))
                {
                    strSql = strSql + " AND MaterialCode = @MaterialCode";
                }
                SqlCommand cmd = new SqlCommand(strSql, con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.BlendDescription))
                {
                    cmd.Parameters.AddWithValue("@BlendDescription", objReq.BlendDescription);
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
                        obj = new clsFabric();
                        obj.MaterialCode = Convert.ToString(ds.Tables[0].Rows[i]["MaterialCode"]);
                        obj.Quality = Convert.ToString(ds.Tables[0].Rows[i]["Quality"]);
                        obj.BlendDescription = Convert.ToString(ds.Tables[0].Rows[i]["BlendDescription"]);
                        obj.BlendValue = Convert.ToString(ds.Tables[0].Rows[i]["BlendValue"]);
                        obj.WeaveType = Convert.ToString(ds.Tables[0].Rows[i]["WeaveType"]);
                        string GSM = Convert.ToString(ds.Tables[0].Rows[i]["GSM"]);
                        if(!String.IsNullOrWhiteSpace(GSM))
                        {
                            obj.GSM = Convert.ToInt32(GSM);
                        }
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
                    strSql = "SELECT TOP 100 * FROM YARNMaster WHERE 1=1";
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

        public List<clsGarments> Fn_Get_Garments_Category(clsGarments objReq)
        {
            List<clsGarments> objResp = new List<clsGarments>();
            var obj = new clsGarments();
            try
            {
                if (con.State == ConnectionState.Broken) { con.Close(); }
                if (con.State == ConnectionState.Closed) { con.Open(); }

                string strSql = "Select Distinct Categoy from RMGMaster";
                SqlDataAdapter da = new SqlDataAdapter(strSql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsGarments();
                        obj.Category = Convert.ToString(ds.Tables[0].Rows[i]["Categoy"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Garments_Category", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public List<clsGarments> Fn_Get_Search_Garments(clsGarments objReq)
        {
            List<clsGarments> objResp = new List<clsGarments>();
            var obj = new clsGarments();
            try
            {
                if (con.State == ConnectionState.Broken) { con.Close(); }
                if (con.State == ConnectionState.Closed) { con.Open(); }

                string strSql = "Select Distinct MaterialCode,MaterialDescription, Categoy, Products, Ranges,";
                strSql = strSql + " Fabricblends,Fabrictype,Season,PieceNo,Fabricquality,Fabricgsm, Fabricshade,";
                strSql = strSql + " Thumbnail,Style,SpecialFeature from RMGMaster Where 1=1";
                if(!String.IsNullOrWhiteSpace(objReq.Category))
                {
                    strSql = strSql + " AND Categoy = @Categoy";
                }
                if (!String.IsNullOrWhiteSpace(objReq.MaterialCode))
                {
                    strSql = strSql + " AND MaterialCode = @MaterialCode";
                }
                SqlCommand cmd = new SqlCommand(strSql, con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.Category))
                {
                    cmd.Parameters.AddWithValue("@Categoy", objReq.Category);
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
                        obj = new clsGarments();
                        obj.MaterialCode = Convert.ToString(ds.Tables[0].Rows[i]["MaterialCode"]);
                        obj.MaterialDescription = Convert.ToString(ds.Tables[0].Rows[i]["MaterialDescription"]);
                        obj.Category = Convert.ToString(ds.Tables[0].Rows[i]["Categoy"]);
                        obj.Products = Convert.ToString(ds.Tables[0].Rows[i]["Products"]);
                        obj.Ranges = Convert.ToString(ds.Tables[0].Rows[i]["Ranges"]);
                        obj.Fabricblends = Convert.ToString(ds.Tables[0].Rows[i]["Fabricblends"]);
                        obj.Fabrictype = Convert.ToString(ds.Tables[0].Rows[i]["Fabrictype"]);
                        obj.Season = Convert.ToString(ds.Tables[0].Rows[i]["Season"]);
                        obj.PieceNo = Convert.ToString(ds.Tables[0].Rows[i]["PieceNo"]);
                        obj.Fabricquality = Convert.ToString(ds.Tables[0].Rows[i]["Fabricquality"]);
                        obj.Fabricgsm = Convert.ToString(ds.Tables[0].Rows[i]["Fabricgsm"]);
                        obj.Fabricshade = Convert.ToString(ds.Tables[0].Rows[i]["Fabricshade"]);
                        obj.Thumbnail = Convert.ToString(ds.Tables[0].Rows[i]["Thumbnail"]);
                        obj.Style = Convert.ToString(ds.Tables[0].Rows[i]["Style"]);
                        obj.SpecialFeature = Convert.ToString(ds.Tables[0].Rows[i]["SpecialFeature"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Search_Garments", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }
        public List<clsFabric> Fn_Get_Fabric_CatalogueDetail(clsFabric objReq)
        {
            var _FabricCatalgoue = new List<clsFabric>();
            string strSql = "";
            try
            {
                if (objReq.vTBLName == "Archieve")
                {
                    strSql = "SELECT * FROM MaterialMastArchieve WHERE 1=1";
                }
                else
                {
                    strSql = "SELECT TOP 100 * FROM MaterialMast WHERE 1=1";
                }

                if (!String.IsNullOrWhiteSpace(objReq.BlendDescription))
                {
                    strSql = strSql + " AND BlendDescription ='" + objReq.BlendDescription + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.WeaveType))
                {
                    strSql = strSql + " AND WeaveType ='" + objReq.WeaveType + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StrechType))
                {
                    strSql = strSql + " AND StrechType ='" + objReq.StrechType + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.DesignPattern))
                {
                    strSql = strSql + " AND DesignPattern ='" + objReq.DesignPattern + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Shade))
                {
                    strSql = strSql + " AND Shade ='" + objReq.Shade + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Usage))
                {
                    strSql = strSql + " AND Usage ='" + objReq.Usage + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.MaterialCode))
                {
                    strSql = strSql + " AND MaterialCode ='" + objReq.MaterialCode + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SpecialFeature))
                {
                    strSql = strSql + " AND SpecialFeature LIKE '%" + objReq.SpecialFeature + "%'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.MaterialDescription))
                {
                    strSql = strSql + " AND MaterialDescription ='" + objReq.MaterialDescription + "%'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Base))
                {
                    strSql = strSql + " AND Base ='" + objReq.Base + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.TypeofDesign))
                {
                    strSql = strSql + " AND TypeofDesign ='" + objReq.TypeofDesign + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.FabricCategory))
                {
                    strSql = strSql + " AND FabricCategory ='" + objReq.FabricCategory + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.ShadeName)) 
                {
                    strSql = strSql + " AND ShadeName ='" + objReq.ShadeName + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Quality))
                {
                    strSql = strSql + " AND Quality ='" + objReq.Quality + "'";
                }
                strSql = strSql + " ORDER BY CreatedOn DESC ";

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
                        var FabricCatalogue = new clsFabric();
                        FabricCatalogue.MaterialCode = Convert.ToString(ds.Tables[0].Rows[i]["MaterialCode"]);
                        FabricCatalogue.Quality = Convert.ToString(ds.Tables[0].Rows[i]["Quality"]);
                        FabricCatalogue.Price = Convert.ToString(ds.Tables[0].Rows[i]["Price"]);
                        FabricCatalogue.Currency = Convert.ToString(ds.Tables[0].Rows[i]["Currency"]);
                        FabricCatalogue.BlendDescription = Convert.ToString(ds.Tables[0].Rows[i]["BlendDescription"]);
                        FabricCatalogue.BlendValue = Convert.ToString(ds.Tables[0].Rows[i]["BlendValue"]);
                        FabricCatalogue.WeaveType = Convert.ToString(ds.Tables[0].Rows[i]["WeaveType"]);
                        FabricCatalogue.GSM = Convert.ToInt32(ds.Tables[0].Rows[i]["GSM"]);
                        FabricCatalogue.StrechType = Convert.ToString(ds.Tables[0].Rows[i]["StrechType"]);
                        FabricCatalogue.DesignPattern = Convert.ToString(ds.Tables[0].Rows[i]["DesignPattern"]);
                        FabricCatalogue.Shade = Convert.ToString(ds.Tables[0].Rows[i]["Shade"]);
                        FabricCatalogue.Usage = Convert.ToString(ds.Tables[0].Rows[i]["Usage"]);
                        FabricCatalogue.Remarks = Convert.ToString(ds.Tables[0].Rows[i]["Remarks"]);
                        FabricCatalogue.Product = Convert.ToString(ds.Tables[0].Rows[i]["Product"]);
                        FabricCatalogue.Address = Convert.ToString(ds.Tables[0].Rows[i]["Address"]);
                        FabricCatalogue.FinishType = Convert.ToString(ds.Tables[0].Rows[i]["FinishType"]);
                        FabricCatalogue.FinishFabric = Convert.ToString(ds.Tables[0].Rows[i]["FinishFabric"]);
                        FabricCatalogue.PieceNo = Convert.ToString(ds.Tables[0].Rows[i]["PieceNo"]);
                        FabricCatalogue.SpecialFeature = Convert.ToString(ds.Tables[0].Rows[i]["SpecialFeature"]);
                        FabricCatalogue.MaterialDescription = Convert.ToString(ds.Tables[0].Rows[i]["MaterialDescription"]);
                        FabricCatalogue.Base = Convert.ToString(ds.Tables[0].Rows[i]["Base"]);
                        FabricCatalogue.TypeofDesign = Convert.ToString(ds.Tables[0].Rows[i]["TypeofDesign"]);
                        FabricCatalogue.FabricCategory = Convert.ToString(ds.Tables[0].Rows[i]["FabricCategory"]);
                        FabricCatalogue.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);

                        FabricCatalogue.vErrorMsg = "Success";

                        _FabricCatalgoue.Add(FabricCatalogue);
                        i++;
                    }
                }
                else
                {
                    var FabricCatalogue = new clsFabric();
                    FabricCatalogue.vErrorMsg = "No Fabric Record Found.";
                    _FabricCatalgoue.Add(FabricCatalogue);
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_Fabric_CatalogueDetail", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                var FabricCatalogue = new clsFabric();
                FabricCatalogue.vErrorMsg = exp.Message.ToString();
                _FabricCatalgoue.Add(FabricCatalogue);
            }
            finally
            {
                con.Close();
            }
            return _FabricCatalgoue;
        }


        public List<clsGarments> Fn_Get_RMG_CatalogueDetail(clsGarments objReq)
        {
            var _RMGCatalogue = new List<clsGarments>();
            string strSql = "";
            try
            {
                if (objReq.vTBLName == "Archieve")
                {
                    strSql = "SELECT * FROM RMGMasterArchieve WHERE 1=1";
                }
                else
                {
                    strSql = "SELECT TOP 100 * FROM RMGMaster WHERE 1=1";
                }

                if (!String.IsNullOrWhiteSpace(objReq.Categoy))
                {
                    strSql = strSql + " AND Categoy='" + objReq.Categoy + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Products))
                {
                    strSql = strSql + " AND Products='" + objReq.Products + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Ranges))
                {
                    strSql = strSql + " AND Ranges='" + objReq.Ranges + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Fabricblends))
                {
                    strSql = strSql + " AND Fabricblends='" + objReq.Fabricblends + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Fabrictype))
                {
                    strSql = strSql + " AND Fabrictype='" + objReq.Fabrictype + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Season))
                {
                    strSql = strSql + " AND Season='" + objReq.Season + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.MaterialCode))
                {
                    strSql = strSql + " AND MaterialCode='" + objReq.MaterialCode + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.PieceNo))
                {
                    strSql = strSql + " AND PieceNo='" + objReq.PieceNo + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Fabricquality))
                {
                    strSql = strSql + " AND Fabricquality='" + objReq.Fabricquality + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Fabricgsm))
                {
                    strSql = strSql + " AND Fabricgsm='" + objReq.Fabricgsm + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Fabricshade))
                {
                    strSql = strSql + " AND Fabricshade='" + objReq.Fabricshade + "'";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SpecialFeature))
                {
                    strSql = strSql + " and SpecialFeature LIKE '%" + objReq.SpecialFeature + "%'";
                }
                strSql = strSql + " ORDER BY createdOn DESC";

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
                        var RMGCatalogue = new clsGarments();
                        RMGCatalogue.MaterialCode = Convert.ToString(ds.Tables[0].Rows[i][0]);
                        RMGCatalogue.MaterialDescription = Convert.ToString(ds.Tables[0].Rows[i][1]);
                        RMGCatalogue.Category = Convert.ToString(ds.Tables[0].Rows[i][2]);
                        RMGCatalogue.Products = Convert.ToString(ds.Tables[0].Rows[i][3]);
                        RMGCatalogue.Ranges = Convert.ToString(ds.Tables[0].Rows[i][4]);
                        RMGCatalogue.Fabricblends = Convert.ToString(ds.Tables[0].Rows[i][5]);
                        RMGCatalogue.Fabrictype = Convert.ToString(ds.Tables[0].Rows[i][6]);
                        RMGCatalogue.Season = Convert.ToString(ds.Tables[0].Rows[i][7]);
                        RMGCatalogue.PieceNo = Convert.ToString(ds.Tables[0].Rows[i][8]);
                        RMGCatalogue.Fabricquality = Convert.ToString(ds.Tables[0].Rows[i][9]);
                        RMGCatalogue.Fabricgsm = Convert.ToString(ds.Tables[0].Rows[i][10]);
                        RMGCatalogue.Fabricshade = Convert.ToString(ds.Tables[0].Rows[i][11]);
                        RMGCatalogue.SpecialFeature = Convert.ToString(ds.Tables[0].Rows[i]["SpecialFeature"]);
                        RMGCatalogue.Style = Convert.ToString(ds.Tables[0].Rows[i]["Style"]);

                        RMGCatalogue.vErrorMsg = "Success";
                        
                        _RMGCatalogue.Add(RMGCatalogue);
                        i++;
                    }
                }
                else
                {
                    var RMGCatalogue = new clsGarments();
                    RMGCatalogue.vErrorMsg = "No RMG Items Found.";
                    _RMGCatalogue.Add(RMGCatalogue);
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_Garments_Detail", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                var RMGCatalogue = new clsGarments();
                RMGCatalogue.vErrorMsg = exp.Message.ToString();
                _RMGCatalogue.Add(RMGCatalogue);
            }
            finally
            {
                con.Close();
            }
            return _RMGCatalogue;
        }


        public List<clsAutoCompliteResponse> Fn_Get_GlobalSearch(clsAutoCompliteRequest obj)
        {
            var objResp = new List<clsAutoCompliteResponse>();
            try
            {
                if (con.State == ConnectionState.Broken)
                { con.Close(); }
                if (con.State == ConnectionState.Closed)
                { con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_GlobalSearch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchKeyword", obj.SearchKeyword);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        var objItem = new clsAutoCompliteResponse();
                        objItem.SearchKeyword = Convert.ToString(ds.Tables[0].Rows[i]["SearchKeyword"]) + "--" + Convert.ToString(ds.Tables[0].Rows[i]["SearchField"]) + "--" + Convert.ToString(ds.Tables[0].Rows[i]["ProductType"]);
                        // objItem.SearchField = Convert.ToString(ds.Tables[0].Rows[i]["SearchField"]);
                        objResp.Add(objItem);
                        i++;
                    }
                }
                else
                {

                }
                cmd.Dispose();
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_GlobalSearch", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                exp.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }



    }
}