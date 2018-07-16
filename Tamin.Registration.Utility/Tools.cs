using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tamin.Registration.Utility
{
    public static class Tools
    {
        public static string GetUserIP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }

        public enum URLType
        {
            Icon = 1,
            Page = 2
        }
        public static bool URLExists(URLType type, string name)
        {
            var r = false;
            var url = Parser.AdminUrl + "/images/";
            switch (type)
            {
                case URLType.Icon:
                    url += "icons/" + name + ".png";
                    break;
                case URLType.Page:
                    url += "pages/" + name + ".jpg";
                    break;
                default:
                    break;
            }

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Timeout = 1200; // miliseconds
            webRequest.Method = "HEAD";

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
                r = true;
            }
            catch
            { }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return r;
        }

        public static bool URLExists(URLType type, int name)
        {
            var r = false;
            var url = Parser.AdminUrl + "/images/";
            switch (type)
            {
                case URLType.Icon:
                    url += "icons/" + name + ".png";
                    break;
                case URLType.Page:
                    url += "pages/" + name + ".jpg";
                    break;
                default:
                    break;
            }

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Timeout = 1200; // miliseconds
            webRequest.Method = "HEAD";

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
                r = true;
            }
            catch
            { }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return r;
        }

        public static bool URLExists(string url)
        {
            var r = false;
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Timeout = 1200; // miliseconds
            webRequest.Method = "HEAD";

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
                r = true;
            }
            catch
            { }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return r;
        }

        public static string GetAlaphabetMonth(int month)
        {
            var monthNames = new string[]
{
                    "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن",
                    "اسفند",
                    ""
                };
            return monthNames[month-1];
        }
        public static string URLBuilder(URLType type, string name)
        {
            var url = Parser.AdminUrl + "/images/";
            switch (type)
            {
                case URLType.Icon:
                    url += "icons/" + name + ".png";
                    break;
                case URLType.Page:
                    url += "pages/" + name + ".jpg";
                    break;
                default:
                    break;
            }
            return url;
        }
        public static string URLBuilder(URLType type, int name)
        {
            var url = Parser.AdminUrl + "/images/";
            switch (type)
            {
                case URLType.Icon:
                    url += "icons/" + name + ".png";
                    break;
                case URLType.Page:
                    url += "pages/" + name + ".jpg";
                    break;
                default:
                    break;
            }
            return url;
        }
    }
}
