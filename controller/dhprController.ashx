<%@ WebHandler Language="C#" Class="dhpr.dhprController" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using dhpr;


namespace dhpr
{
    public class dhprController : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            try
            {
                var jsonResult = string.Empty;
                var lang = string.IsNullOrEmpty(context.Request.QueryString.GetLang().Trim()) ? "en" : context.Request.QueryString.GetLang().Trim();
                if (lang == "en")
                {
                    UtilityHelper.SetDefaultCulture("en");
                }
                else
                {
                    UtilityHelper.SetDefaultCulture("fr");
                }

                //Get All the QueryStrings
                var term  = context.Request.QueryString.GetSearchTerm().ToLower().Trim();
                var pType = string.IsNullOrEmpty(context.Request.QueryString.GetProgramType().Trim()) ? programType.dhpr : (programType)Enum.Parse(typeof(programType), context.Request.QueryString.GetProgramType().Trim());
                var linkId = string.IsNullOrWhiteSpace(context.Request.QueryString.GetLinkID().Trim())? string.Empty: context.Request.QueryString.GetLinkID().Trim();
              

                if( !string.IsNullOrWhiteSpace(linkId))
                {
                    switch (pType)
                    {
                        case programType.rds:
                                var rdsItem = new regulatoryDecisionItem();
                                rdsItem = UtilityHelper.GetRdsByID(linkId, lang );
                                if( !string.IsNullOrWhiteSpace(rdsItem.link_id))
                                {
                                    rdsItem.decision = string.Format("{0}{1}", rdsItem.decision, rdsItem.decision_descr);
                                    jsonResult = JsonHelper.JsonSerializer<regulatoryDecisionItem>(rdsItem);
                                    context.Response.Write(jsonResult);
                                }
                                else
                                {
                                    context.Response.Write("{\"link_id\":\"\"}");
                                }
                                break;
                            case programType.rdsmd:
                           
                                var rdsMDItem = new regulatoryDecisionMedicalDevice();
                                rdsMDItem = UtilityHelper.GetRDSMedicalDeviceByID(linkId, lang );
                                if( !string.IsNullOrWhiteSpace(rdsMDItem.link_id))
                                {
                                    rdsMDItem.decision = string.Format("{0}{1}", rdsMDItem.decision, rdsMDItem.decision_descr);
                                    jsonResult = JsonHelper.JsonSerializer<regulatoryDecisionMedicalDevice>(rdsMDItem);
                                    context.Response.Write(jsonResult);
                                }
                                else
                                {
                                    context.Response.Write("{\"link_id\":\"\"}");
                                }
                                break;
                        case programType.ssr:
                            var ssrItem = new summarySafetyItem();
                            ssrItem = UtilityHelper.GetSsrByID(linkId, lang);

                            if ( !string.IsNullOrWhiteSpace(ssrItem.link_id))
                            {
                                jsonResult = JsonHelper.JsonSerializer<summarySafetyItem>(ssrItem);
                                context.Response.Write(jsonResult);
                            }
                            else
                            {
                                context.Response.Write("{\"link_id\":\"\"}");
                            }
                            break;
                        case programType.sbd:
                            var sbdItem = new summaryBasisItem();
                            sbdItem = UtilityHelper.GetSbdByID(linkId, lang);

                            if ( !string.IsNullOrWhiteSpace(sbdItem.link_id))
                            {
                                if(sbdItem.post_activity_list.Count > 0)
                                {
                                    sbdItem.post_activity_list.OrderBy(i => i.row_num);
                                }
                                else
                                {
                                    sbdItem.paat_message = string.Empty;
                                    if ( sbdItem.date_issued > new DateTime(2012, 12, 31))
                                    {
                                        sbdItem.paat_message =  string.Format(Resources.Resource.NoPaatData, sbdItem.brand_name);
                                    }
                                }
                                //<a href=\"\"></a>
                                //if(!string.IsNullOrWhiteSpace(sbdItem.Contact) )
                                //{
                                //    sbdItem.Contact = sbdItem.Contact.Replace("</a>", "No value</a>");
                                //}
                                jsonResult = JsonHelper.JsonSerializer<summaryBasisItem>(sbdItem);
                                jsonResult = jsonResult.Replace("post_activity_list", "data");
                                context.Response.Write(jsonResult);
                            }
                            else
                            {
                                context.Response.Write("{\"link_id\":\"\"}");
                            }
                            break;
                        case programType.sbdmd:
                            var sbdMdItem = new summaryBasisMDItem();
                            sbdMdItem = UtilityHelper.GetSbdMdByID(linkId, lang);

                            if ( !string.IsNullOrWhiteSpace(sbdMdItem.link_id))
                            {
                                jsonResult = JsonHelper.JsonSerializer<summaryBasisMDItem>(sbdMdItem);
                                jsonResult = jsonResult.Replace("plat_list", "data");
                                context.Response.Write(jsonResult);
                            }
                            else
                            {
                                context.Response.Write("{\"link_id\":\"\"}");
                            }
                            break;

                    }
                }
                else
                {
                    switch (pType)
                    {
                        case programType.rds:
                            var rdsList = new List<rdsSearchItem>();
                            rdsList =  UtilityHelper.GetRegulatoryDecisionList(lang, term);
                            if (rdsList != null && rdsList.Count > 0)
                            {
                                rdsList.ForEach(x =>
                                {
                                    x.din_list = new List<string>();

                                });
                                jsonResult = JsonHelper.JsonSerializer<List<rdsSearchItem>>(rdsList);
                                jsonResult = "{\"data\":" + jsonResult + "}";
                                context.Response.Write(jsonResult);
                            }
                            else
                            {
                                context.Response.Write("{\"data\":[]}");
                            }
                            break;
                        case programType.ssr:
                            var ssrList = new List<ssrSearchItem>();
                            ssrList =  UtilityHelper.GetSummarySafetyList(lang, term);
                            if (ssrList != null && ssrList.Count > 0)
                            {
                                jsonResult = JsonHelper.JsonSerializer<List<ssrSearchItem>>(ssrList);
                                jsonResult = "{\"data\":" + jsonResult + "}";
                                context.Response.Write(jsonResult);
                            }
                            else
                            {
                                context.Response.Write("{\"data\":[]}");
                            }
                            break;
                        case programType.sbd:
                        case programType.sbdmd:
                            var sbdList = new List<sbdSearchItem>();
                            sbdList =  UtilityHelper.GetSummaryBasisList(lang, term);
                            if (sbdList != null && sbdList.Count > 0)
                            {
                                sbdList.ForEach(x =>
                                {
                                    // x.d = new List<string>();

                                });
                                jsonResult = JsonHelper.JsonSerializer<List<sbdSearchItem>>(sbdList);
                                jsonResult = "{\"data\":" + jsonResult + "}";
                                context.Response.Write(jsonResult);
                            }
                            else
                            {
                                context.Response.Write("{\"data\":[]}");
                            }
                            break;
                        default:
                            context.Response.Write("{\"data\":[]}");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogException(ex, "dhprController.ashx");
                context.Response.Write("{\"data\":[]}");
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}