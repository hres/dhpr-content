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
                            rdsItem = UtilityHelper.GetRdsByID(linkId, lang);
                            if( !string.IsNullOrWhiteSpace(rdsItem.LinkId))
                            {
                                rdsItem.Decision = string.Format("{0}{1}", rdsItem.Decision, rdsItem.DecisionDescr);
                                jsonResult = JsonHelper.JsonSerializer<regulatoryDecisionItem>(rdsItem);
                                context.Response.Write(jsonResult);
                            }
                            else
                            {
                                context.Response.Write("{\"LinkId\":\"\"}");
                            }
                            break;
                        case programType.ssr:
                            var ssrItem = new summarySafetyItem();
                            ssrItem = UtilityHelper.GetSsrByID(linkId, lang);

                            if ( !string.IsNullOrWhiteSpace(ssrItem.LinkId))
                            {
                                jsonResult = JsonHelper.JsonSerializer<summarySafetyItem>(ssrItem);
                                context.Response.Write(jsonResult);
                            }
                            else
                            {
                                context.Response.Write("{\"LinkId\":\"\"}");
                            }
                            break;
                        case programType.sbd:
                            var sbdItem = new summaryBasisItem();
                            sbdItem = UtilityHelper.GetSbdByID(linkId, lang);

                            if ( !string.IsNullOrWhiteSpace(sbdItem.LinkId))
                            {
                                if(sbdItem.PostActivityList.Count > 0)
                                {
                                    sbdItem.PostActivityList.OrderBy(i => i.RowNum);
                                }
                                jsonResult = JsonHelper.JsonSerializer<summaryBasisItem>(sbdItem);
                                jsonResult = jsonResult.Replace("PostActivityList", "data");
                                context.Response.Write(jsonResult);
                            }
                            else
                            {
                                context.Response.Write("{\"LinkId\":\"\"}");
                            }
                            break;


                        case programType.sbdmd:
                            var sbdMdItem = new summaryBasisMDItem();
                            sbdMdItem = UtilityHelper.GetSbdMdByID(linkId, lang);

                            if ( !string.IsNullOrWhiteSpace(sbdMdItem.LinkId))
                            {
                                jsonResult = JsonHelper.JsonSerializer<summaryBasisMDItem>(sbdMdItem);
                                jsonResult = jsonResult.Replace("PlatList", "data");
                                context.Response.Write(jsonResult);
                            }
                            else
                            {
                                context.Response.Write("{\"LinkId\":\"\"}");
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
                                    x.DinList = new List<string>();

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