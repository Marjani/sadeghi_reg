using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tamin.Registration.Utility
{
    public class Parser
    {
        public static string AdminUrl
        {
            get
            {
                return "http://m.mehr-mandegar.com";
            }
        }
        public static string PaireJsonValue(HttpRequestBase request, string field, string value)
        {
            var mats = request.Form.AllKeys.Where(o => o.StartsWith(field)).ToList();
            var tablelist = new Dictionary<string, string>();
            foreach (var item in mats)
            {
                try
                {
                    tablelist.Add(request[item], request[value + int.Parse(item.Split('-').LastOrDefault())]);
                }
                catch (Exception)
                {

                }

            }
            var tableString = JsonConvert.SerializeObject(tablelist);
            return tableString;
        }

        public static DateTime FixDateTime(string date)
        {
            var sp = date.Split('/');

            var year = int.Parse(sp[0]);
            var month = int.Parse(sp[1]);
            var day = int.Parse(sp[2]);

            DateTime dt = new DateTime(year, month, day, new PersianCalendar());

            return dt;
        }

        public static string PhotoPath(string path)
        {
            return AdminUrl + path;
        }

        public static string BodyPathFix(string body)
        {
            if (string.IsNullOrEmpty(body))
            {
                return null;
            }
            body = body.Replace("href=\"/", "href=\"" + AdminUrl + "/");
            body = body.Replace("src=\"/", "src=\"" + AdminUrl + "/");
            return body;
        }
        public static string FixTable(HttpRequestBase request, string startWith)
        {
            var r = string.Empty;

            var keys = request.Form.AllKeys.Where(o => o.StartsWith(startWith));
            var pureKeys = new List<string>();
            foreach (var item in keys)
            {
                var s = item.Split('-');
                pureKeys.Add(s[0] + "-" + s[1]);
            }
            var groupKeys = pureKeys.Distinct();
            var result = new List<List<string>>();
            foreach (var item in groupKeys)
            {
                var current = new List<string>();
                foreach (var key in keys)
                {
                    if (key.StartsWith(item))
                    {
                        current.Add(request[key]);
                    }
                }
                result.Add(current);
            }

            r = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return r;
        }
    }
}
