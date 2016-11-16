using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace dhpr
{
    /// <summary>
    /// Summary description for Common
    /// </summary>
    public static class UtilityHelper
    {
        public static void SetDefaultCulture(string lang)
        {
            if (lang == "en")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-CA");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-CA");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("fr-FR");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("fr-FR");
            }
        }

        public static List<rdsSearchItem> GetRegulatoryDecisionList(string lang, string term)
        {
            var items = new List<rdsSearchItem>();
            var filteredList = new List<rdsSearchItem>();
            var json = string.Empty;           
            var rdsJsonUrl = ConfigurationManager.AppSettings["rdsJsonUrl"].ToString();

            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    json = webClient.DownloadString(rdsJsonUrl + string.Format("&lang={0}", lang));
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        items = JsonConvert.DeserializeObject<List<rdsSearchItem>>(json);                      
                        if (items != null && items.Count > 0)
                        {
                            items.OrderBy(c => c.date_decision);

                            foreach( var item in items)
                            {
                                if( item.din_list != null && item.din_list.Count > 0)
                                {
                                    item.din = string.Empty;
                                    foreach ( var str in item.din_list)
                                    {
                                        item.din += string.Format("{0} |", str);
                                    }
                                    item.din = item.din.EndsWith("|") ? item.din.Substring(0, item.din.Length - 1) : item.din;
                                }
                            }
                            if ( string.IsNullOrWhiteSpace(term))
                            {
                               filteredList = items;                             
                            }
                            else
                            {
                                if (ValidateDinNumber(term))
                                {
                                    filteredList = (from t in items
                                                  where !string.IsNullOrWhiteSpace(t.din) && t.din.Contains(term)
                                                  select t).ToList();
                                }
                                else
                                {
                                    if (term.Equals("whatsnew"))
                                    {
                                        var newDate = DateTime.Now.AddDays(-30); // issue date <= 30 days.
                                        items.OrderBy(c => c.date_decision);
                                        filteredList = items.Where(c => c.date_decision > newDate).ToList();
                                    }
                                    else
                                    {

                                        filteredList = items.Where(c => c.drug_name.ToLower().Contains(term)
                                                                    || c.manufacturer.ToLower().Contains(term)
                                                                    || c.medical_ingredient.ToLower().Contains(term)
                                                        ).ToList();

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessages = string.Format("UtilityHelper - GetJSonDataFromRegAPI()- Error Message:{0}", ex.Message);
                ExceptionHelper.LogException(ex, errorMessages);
            }
            finally
            {

            }
            return filteredList;
        }

        public static regulatoryDecisionItem GetRdsByID(string rdsID, string lang)
        {
            var item = new regulatoryDecisionItem();
            var json = string.Empty;
            var postData = new Dictionary<string, string>();
            var rdsJsonUrlbyID = string.Format("{0}&id={1}&lang={2}", ConfigurationManager.AppSettings["rdsJsonUrl"].ToString(), rdsID, lang);

            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    json = webClient.DownloadString(rdsJsonUrlbyID);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        item = JsonConvert.DeserializeObject<regulatoryDecisionItem>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessages = string.Format("UtilityHelper - GetRdsByID()- Error Message:{0}", ex.Message);
                ExceptionHelper.LogException(ex, errorMessages);
            }
            finally
            {

            }
            return item;
        }

        public static regulatoryDecisionMedicalDevice GetRDSMedicalDeviceByID(string rdsID, string lang)
        {
            var item = new regulatoryDecisionMedicalDevice();
            var json = string.Empty;
            var postData = new Dictionary<string, string>();
            var rdsJsonUrlbyID = string.Format("{0}&id={1}&lang={2}", ConfigurationManager.AppSettings["rdsmdJsonUrl"].ToString(), rdsID, lang);

            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    json = webClient.DownloadString(rdsJsonUrlbyID);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        item = JsonConvert.DeserializeObject<regulatoryDecisionMedicalDevice>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessages = string.Format("UtilityHelper - GetRDSMedicalDeviceByID()- Error Message:{0}", ex.Message);
                ExceptionHelper.LogException(ex, errorMessages);
            }
            finally
            {

            }
            return item;
        }
        public static List<ssrSearchItem> GetSummarySafetyList(string lang, string term)
        {
            // CertifySSL.EnableTrustedHosts();
            var items = new List<ssrSearchItem>();
            var filteredList = new List<ssrSearchItem>();
            var json = string.Empty;
            // var postData = new Dictionary<string, string>();
            var ssrJsonUrl = ConfigurationManager.AppSettings["ssrJsonUrl"].ToString();

            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    json = webClient.DownloadString(ssrJsonUrl + string.Format("&lang={0}", lang));
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        items = JsonConvert.DeserializeObject<List<ssrSearchItem>>(json);

                        if (items != null && items.Count > 0)
                        {
                            if (string.IsNullOrWhiteSpace(term))
                            {
                                items.OrderBy(c => c.created_date);
                                filteredList = items;
                            }
                            else
                            {
                                if (term.Equals("whatsnew"))
                                {
                                    var newDate = DateTime.Now.AddDays(-30); // issue date <= 30 days.
                                    items.OrderBy(c => c.created_date);
                                    filteredList = items.Where(c => c.created_date > newDate).ToList();
                                }
                                else
                                {
                                    filteredList = items.Where(c => c.drug_name.ToLower().Contains(term) || c.safety_issue.ToLower().Contains(term)).ToList();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessages = string.Format("UtilityHelper - GetSummarySafetyList()- Error Message:{0}", ex.Message);
                ExceptionHelper.LogException(ex, errorMessages);
            }
            finally
            {

            }
            return filteredList;
        }


        public static summarySafetyItem GetSsrByID(string ssrID, string lang)
        {
            // CertifySSL.EnableTrustedHosts();
            var item = new summarySafetyItem();
            var json = string.Empty;
            var postData = new Dictionary<string, string>();
            var ssrJsonUrlbyID = string.Format("{0}&id={1}&lang={2}", ConfigurationManager.AppSettings["ssrJsonUrl"].ToString(), ssrID, lang);

            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    json = webClient.DownloadString(ssrJsonUrlbyID);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        item = JsonConvert.DeserializeObject<summarySafetyItem>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessages = string.Format("UtilityHelper - GetSsrByID()- Error Message:{0}", ex.Message);
                ExceptionHelper.LogException(ex, errorMessages);
            }
            finally
            {

            }
            return item;
        }
        public static summaryBasisItem GetSbdByID(string sbdID, string lang)
        {
            // CertifySSL.EnableTrustedHosts();
            var item = new summaryBasisItem();
            var json = string.Empty;
            var postData = new Dictionary<string, string>();
            var sbdJsonUrlbyID = string.Format("{0}&id={1}&lang={2}", ConfigurationManager.AppSettings["sbdJsonUrl"].ToString(), sbdID, lang);

            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    json = webClient.DownloadString(sbdJsonUrlbyID);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        item = JsonConvert.DeserializeObject<summaryBasisItem>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessages = string.Format("UtilityHelper - GetSbdByID()- Error Message:{0}", ex.Message);
                ExceptionHelper.LogException(ex, errorMessages);
            }
            finally
            {

            }
            return item;
        }

        public static summaryBasisMDItem GetSbdMdByID(string sbdID, string lang)
        {
            // CertifySSL.EnableTrustedHosts();
            var item = new summaryBasisMDItem();
            var json = string.Empty;
            var postData = new Dictionary<string, string>();
            var sbdmdJsonUrlbyID = string.Format("{0}&id={1}&lang={2}", ConfigurationManager.AppSettings["sbdmdJsonUrl"].ToString(), sbdID, lang);

            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    json = webClient.DownloadString(sbdmdJsonUrlbyID);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        item = JsonConvert.DeserializeObject<summaryBasisMDItem>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessages = string.Format("UtilityHelper - GetSbdMdByID()- Error Message:{0}", ex.Message);
                ExceptionHelper.LogException(ex, errorMessages);
            }
            finally
            {

            }
            return item;
        }

        public static List<sbdSearchItem> GetSummaryBasisList(string lang, string term)
        {
            var items = new List<sbdSearchItem>();
            var filteredList = new List<sbdSearchItem>();
            var json = string.Empty;
            var sbdJsonUrl = ConfigurationManager.AppSettings["sbdJsonUrl"].ToString();

            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    json = webClient.DownloadString(sbdJsonUrl + string.Format("&lang={0}", lang));
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        items = JsonConvert.DeserializeObject<List<sbdSearchItem>>(json);

                        if (items != null && items.Count > 0)
                        {
                            items.OrderBy(c => c.date_issued);
                            foreach (var item in items)
                            {
                                if (item.din_list != null && item.din_list.Count > 0)
                                {
                                    item.din = string.Empty;
                                    foreach (var str in item.din_list)
                                    {
                                        item.din += string.Format("{0} |", str);
                                    }
                                    item.din = item.din.EndsWith("|") ? item.din.Substring(0, item.din.Length - 1) : item.din;
                                }
                            }


                            if (string.IsNullOrWhiteSpace(term))
                            {
                                filteredList = items;
                            }
                            else
                            {
                                if (ValidateDinNumber(term))
                                {
                                    filteredList = (from t in items
                                                    where !string.IsNullOrWhiteSpace(t.din) && t.din.Contains(term)
                                                    select t).ToList();
                                }
                                else {
                                    if (term.Equals("whatsnew"))
                                    {
                                        var newDate = DateTime.Now.AddDays(-30); // issue date <= 30 days.
                                        items.OrderBy(c => c.date_issued);
                                        filteredList = items.Where(c => c.date_issued > newDate).ToList();
                                    }
                                    else
                                    {
                                        filteredList =  items.Where(c => c.brand_name.ToLower().Contains(term) 
                                        || c.med_ingredient.ToLower().Contains(term) || c.licence_number.ToLower().Contains(term)
                                        || c.manufacturer.ToLower().Contains(term )).ToList();
                                    }
                                }
                            }                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessages = string.Format("UtilityHelper - GetSummaryBasisList()- Error Message:{0}", ex.Message);
                ExceptionHelper.LogException(ex, errorMessages);
            }
            finally
            {

            }
            return filteredList;
        }

        public static bool ValidateDinNumber(string din)
        {
            //Accepts only 8 digits, no more no less.
            Regex pattern = new Regex(@"(?<!\d)\d{8}(?!\d)");
            if (pattern.IsMatch(din))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
  
}

