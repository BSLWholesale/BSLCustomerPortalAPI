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
    public class DALInquiry
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);
        Generic gn = new Generic();
        SqlCommand cmd;
        SqlDataReader dr;
        double dInqId;
        string strExistItem = string.Empty;
        string strInq;
        string strVendorEmail = string.Empty;

        public clsQuotation Fn_Make_Quotation(clsQuotation objReq)
        {
            var objResp = new clsQuotation();
            if (objReq.QueryType == "InsertMaster")
            {
                string strQuotationId = gn.Fn_Get_MAX_ID("QuotationMaster");
                objReq.QuotationId = strQuotationId;
            }
            try
            {
                if (String.IsNullOrWhiteSpace(objReq.CustomerName))
                {
                    objResp.vErrorMsg = "CustomerName is empty";
                }
                else if (String.IsNullOrWhiteSpace(objReq.ContactPerson))
                {
                    objResp.vErrorMsg = "ContactPerson is emply";
                }
                else if (String.IsNullOrWhiteSpace(objReq.QuotationId))
                {
                    objResp.vErrorMsg = "QuotationId is emply";
                }
                else if (objReq.UserId == 0)
                {
                    objResp.vErrorMsg = "UserId is emply";
                }
                else
                {
                    if (con.State == ConnectionState.Broken)
                    { con.Close(); }
                    if (con.State == ConnectionState.Closed)
                    { con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_Quotation", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@QuotationId", objReq.QuotationId);
                    cmd.Parameters.AddWithValue("@CompanyName", objReq.CompanyName);
                    cmd.Parameters.AddWithValue("@CustomerName", objReq.CustomerName);
                    cmd.Parameters.AddWithValue("@ContactPerson", objReq.ContactPerson);
                    cmd.Parameters.AddWithValue("@UID", objReq.UserId);
                    cmd.Parameters.AddWithValue("@EmailId", objReq.EmailId);
                    cmd.Parameters.AddWithValue("@Remarks", objReq.vRemarks);
                    // cmd.Parameters.AddWithValue("@QueryType", "InsertMaster");
                    cmd.Parameters.AddWithValue("@UserEmail", objReq.vUserEmail);
                    cmd.Parameters.AddWithValue("@Validity", objReq.vValidity);
                    cmd.Parameters.AddWithValue("@QueryType", objReq.QueryType);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        foreach (var list in objReq._oList)
                        {
                            SqlCommand cm = new SqlCommand("USP_Quotation", con);
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.Parameters.AddWithValue("@QuotationId", objReq.QuotationId);
                            cm.Parameters.AddWithValue("@Article", list.vArticle);
                            cm.Parameters.AddWithValue("@Blend", list.vBlend);
                            cm.Parameters.AddWithValue("@Weight", list.vWeight);
                            cm.Parameters.AddWithValue("@Width", list.vWidth);
                            cm.Parameters.AddWithValue("@Price", list.vPrice);
                            cm.Parameters.AddWithValue("@Unit", list.vUnit);
                            cm.Parameters.AddWithValue("@UnitType", list.vUnitType);
                            cm.Parameters.AddWithValue("@PaymentTerms", list.vPaymentTerms);
                            cm.Parameters.AddWithValue("@DeliveryTerms", list.vDeliveryTerms);

                            cm.Parameters.AddWithValue("@QueryType", "InsertDetail");

                            int j = cm.ExecuteNonQuery();
                            if (j > 0)
                            {
                                objResp.vErrorMsg = "";
                            }
                            else
                            {
                                objResp.vErrorMsg = "Failed";
                            }
                        }

                        if (objResp.vErrorMsg == "")
                        {
                            objResp.vErrorMsg = "Success";
                            objResp.QuotationId = objReq.QuotationId;
                        }
                        else
                        {
                            objResp.vErrorMsg = "Failed";
                        }
                    }
                    else
                    {
                        objResp.vErrorMsg = "Request Failed.";
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Make_Quotation", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public List<clsQuotation> Fn_Get_Quotation(clsQuotation objReq)
        {
            List<clsQuotation> objResp = new List<clsQuotation>();
            var obj = new clsQuotation();
            try
            {
                string strSql = "SELECT  QM.QuotationId, QM.CustomerName, QM.ContactPerson, QM.EmailId, QM.UID, QM.CompanyName,";
                strSql = strSql + " Format(QM.CreatedDate, 'dd-MMM-yyyy') AS CreatedDate, QM.UserEmail, QM.Validity,";
                strSql = strSql + "(CR.CFirstName + ' ' + CR.CLastName) AS USERNAME, QM.Remarks FROM QuotationMaster QM";
                strSql = strSql + " INNER JOIN CustRegistration CR ON QM.UID = CR.CustId  WHERE 1=1 AND QM.UID = @UID";
                if (!String.IsNullOrWhiteSpace(objReq.QuotationId))
                {
                    strSql = strSql + " AND QM.QuotationId = @QuotationId";
                }
                if (!String.IsNullOrWhiteSpace(objReq.CustomerName))
                {
                    strSql = strSql + " AND QM.CustomerName = @CustomerName";
                }
                if (!String.IsNullOrWhiteSpace(objReq.CreatedDate))
                {
                    strSql = strSql + " AND Format(QM.CreatedDate, 'dd-MMM-yyyy') = @CreatedDate";
                }

                SqlCommand cmd = new SqlCommand(strSql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UID", objReq.UserId);

                if (!String.IsNullOrWhiteSpace(objReq.QuotationId))
                {
                    cmd.Parameters.AddWithValue("@QuotationId", objReq.QuotationId);
                }
                if (!String.IsNullOrWhiteSpace(objReq.CustomerName))
                {
                    cmd.Parameters.AddWithValue("@CustomerName", objReq.CustomerName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.CreatedDate))
                {
                    cmd.Parameters.AddWithValue("@CreatedDate", objReq.CreatedDate);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsQuotation();

                        obj.QuotationId = Convert.ToString(ds.Tables[0].Rows[i]["QuotationId"]);
                        obj.CreatedDate = Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        obj.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UID"]);
                        obj.CustomerName = Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        obj.CompanyName = Convert.ToString(ds.Tables[0].Rows[i]["CompanyName"]);
                        obj.ContactPerson = Convert.ToString(ds.Tables[0].Rows[i]["ContactPerson"]);
                        obj.EmailId = Convert.ToString(ds.Tables[0].Rows[i]["EmailId"]);
                        obj.vRemarks = Convert.ToString(ds.Tables[0].Rows[i]["Remarks"]);

                        obj.vUserEmail = Convert.ToString(ds.Tables[0].Rows[i]["UserEmail"]);
                        obj.vValidity = Convert.ToString(ds.Tables[0].Rows[i]["Validity"]);

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
                Logger.WriteLog("Function Name : Fn_Get_Quotation", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public List<clsQuotationList> Fn_Get_Quotation_Detail(clsQuotationList objReq)
        {
            List<clsQuotationList> objResp = new List<clsQuotationList>();
            var obj = new clsQuotationList();
            try
            {
                SqlCommand cmd = new SqlCommand("USP_Quotation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QuotationId", objReq.QuotationId);
                cmd.Parameters.AddWithValue("@QueryType", "SelectDetail");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsQuotationList();

                        obj.Id = Convert.ToInt64(ds.Tables[0].Rows[i]["Id"]);
                        obj.QuotationId = Convert.ToString(ds.Tables[0].Rows[i]["QuotationId"]);
                        obj.CreatedDate = Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        obj.vArticle = Convert.ToString(ds.Tables[0].Rows[i]["vArticle"]);

                        obj.vBlend = Convert.ToString(ds.Tables[0].Rows[i]["vBlend"]);
                        obj.vWeight = Convert.ToString(ds.Tables[0].Rows[i]["vWeight"]);

                        obj.vWidth = Convert.ToString(ds.Tables[0].Rows[i]["vWidth"]);
                        obj.vPrice = Convert.ToString(ds.Tables[0].Rows[i]["vPrice"]);
                        obj.vUnit = Convert.ToString(ds.Tables[0].Rows[i]["vUnit"]);
                        obj.vUnitType = Convert.ToString(ds.Tables[0].Rows[i]["vUnitType"]);
                        obj.vPaymentTerms = Convert.ToString(ds.Tables[0].Rows[i]["vPaymentTerms"]);
                        obj.vDeliveryTerms = Convert.ToString(ds.Tables[0].Rows[i]["vDeliveryTerms"]);

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
                Logger.WriteLog("Function Name : Fn_Get_Quotation_Detail", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                con.Close();
            }
            return objResp;
        }

        public List<clsRequestDropdown> Fn_Fill_AutoComplite(clsRequestDropdown objReq)
        {
            List<clsRequestDropdown> _productmodel = new List<clsRequestDropdown>();
            var obj = new clsRequestDropdown();
            string strSql = "";
            try
            {

                if (con.State == ConnectionState.Broken)
                { con.Close(); }
                if (con.State == ConnectionState.Closed)
                { con.Open(); }


                strSql = "SELECT Distinct " + objReq.vFieldName + " FROM " + objReq.vTBLName + " WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.vValueField))
                {
                    strSql = strSql + " AND " + objReq.vFieldName + " LIKE '%" + objReq.vValueField + "%'";
                }

                SqlDataAdapter da = new SqlDataAdapter(strSql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsRequestDropdown();
                        obj.vFieldName = Convert.ToString(ds.Tables[0].Rows[i][0]);
                        obj.vErrorMsg = "Success";
                        _productmodel.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorMsg = "No Data";
                    _productmodel.Add(obj);
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Fill_AutoComplite", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                _productmodel.Add(obj);
            }
            finally
            {
                con.Close();
            }
            return _productmodel;
        }

        public List<clsQuotationReport> Fn_Get_Quotation_Report(clsQuotationReport objReq)
        {
            List<clsQuotationReport> objResp = new List<clsQuotationReport>();
            var obj = new clsQuotationReport();
            try
            {
                string strSql = " SELECT  QM.QuotationId, QM.CustomerName, QM.ContactPerson, QM.EmailId, QM.CompanyName,";
                strSql = strSql + " Format(QM.CreatedDate, 'dd-MMM-yyyy') AS CreatedDate, QM.UserEmail, QM.Validity, QM.UID,";
                strSql = strSql + " (CR.CFirstName + ' ' + CR.CLastName) AS USERNAME,";
                strSql = strSql + " QM.Remarks, QD.Id, QD.vArticle,QD.vBlend, QD.vWeight,QD.vWidth,QD.vPrice, QD.vUnit,";
                strSql = strSql + " QD.vUnitType, QD.vPaymentTerms,QD.vDeliveryTerms";
                strSql = strSql + " FROM QuotationMaster QM INNER JOIN CustRegistration CR ON QM.UID = CR.CustId";
                strSql = strSql + " INNER JOIN QuotationDetail QD ON QM.QuotationId = QD.QuotationId WHERE 1=1 AND QM.UID = @UID";
                if (!String.IsNullOrWhiteSpace(objReq.QuotationId))
                {
                    strSql = strSql + " AND QM.QuotationId = @QuotationId";
                }
                if (!String.IsNullOrWhiteSpace(objReq.CustomerName))
                {
                    strSql = strSql + " AND QM.CustomerName = @CustomerName";
                }
                if (!String.IsNullOrWhiteSpace(objReq.CreatedDate))
                {
                    strSql = strSql + " AND Format(QM.CreatedDate, 'dd-MMM-yyyy') = @CreatedDate";
                }
                if (!String.IsNullOrWhiteSpace(objReq.vArticle))
                {
                    strSql = strSql + " AND QD.vArticle = @Article ";
                }
                strSql = strSql + " ORDER BY QM.CreatedDate DESC";

                SqlCommand cmd = new SqlCommand(strSql, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@UID", objReq.UserId);

                if (!String.IsNullOrWhiteSpace(objReq.QuotationId))
                {
                    cmd.Parameters.AddWithValue("@QuotationId", objReq.QuotationId);
                }
                if (!String.IsNullOrWhiteSpace(objReq.CustomerName))
                {
                    cmd.Parameters.AddWithValue("@CustomerName", objReq.CustomerName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.CreatedDate))
                {
                    cmd.Parameters.AddWithValue("@CreatedDate", objReq.CreatedDate);
                }
                if (!String.IsNullOrWhiteSpace(objReq.vArticle))
                {
                    cmd.Parameters.AddWithValue("@Article", objReq.vArticle);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsQuotationReport();

                        obj.QuotationId = Convert.ToString(ds.Tables[0].Rows[i]["QuotationId"]);
                        obj.CreatedDate = Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        obj.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UID"]);
                        obj.CustomerName = Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        obj.CompanyName = Convert.ToString(ds.Tables[0].Rows[i]["CompanyName"]);
                        obj.ContactPerson = Convert.ToString(ds.Tables[0].Rows[i]["ContactPerson"]);
                        obj.EmailId = Convert.ToString(ds.Tables[0].Rows[i]["EmailId"]);
                        obj.vRemarks = Convert.ToString(ds.Tables[0].Rows[i]["Remarks"]);

                        obj.vUserEmail = Convert.ToString(ds.Tables[0].Rows[i]["UserEmail"]);
                        obj.vValidity = Convert.ToString(ds.Tables[0].Rows[i]["Validity"]);

                        obj.Id = Convert.ToInt64(ds.Tables[0].Rows[i]["Id"]);
                        obj.QuotationId = Convert.ToString(ds.Tables[0].Rows[i]["QuotationId"]);
                        obj.CreatedDate = Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        obj.vArticle = Convert.ToString(ds.Tables[0].Rows[i]["vArticle"]);

                        obj.vBlend = Convert.ToString(ds.Tables[0].Rows[i]["vBlend"]);
                        obj.vWeight = Convert.ToString(ds.Tables[0].Rows[i]["vWeight"]);

                        obj.vWidth = Convert.ToString(ds.Tables[0].Rows[i]["vWidth"]);
                        obj.vPrice = Convert.ToString(ds.Tables[0].Rows[i]["vPrice"]);
                        obj.vUnit = Convert.ToString(ds.Tables[0].Rows[i]["vUnit"]);
                        obj.vUnitType = Convert.ToString(ds.Tables[0].Rows[i]["vUnitType"]);
                        obj.vPaymentTerms = Convert.ToString(ds.Tables[0].Rows[i]["vPaymentTerms"]);
                        obj.vDeliveryTerms = Convert.ToString(ds.Tables[0].Rows[i]["vDeliveryTerms"]);

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
                Logger.WriteLog("Function Name : Fn_Get_Quotation_Report", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
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