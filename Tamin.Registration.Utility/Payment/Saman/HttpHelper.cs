﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamin.Registration.Utility.Payment.Saman
{
    public class HttpHelper
    {
        /// <summary>
        /// This method prepares an Html form which holds all data in hidden field in the addetion to form submitting script.
        /// </summary>
        /// <param name="url">The destination Url to which the post and redirection will occur, the Url can be in the same App or ouside the App.</param>
        /// <param name="data">A collection of data that will be posted to the destination Url.</param>
        /// <returns>Returns a string representation of the Posting form.</returns>
        /// <Author>Amin Rahimi</Author>
        private static String PreparePOSTForm(string url, NameValueCollection data)
        {
            //Set a name for the form
            string formID = "PostForm";

            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");
            foreach (string key in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key + "\" value=\"" + data[key] + "\">");
            }
            strForm.Append("</form>");

            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." + formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");

            //Return the form and the script concatenated. (The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }
        /// <summary>
        /// POST data and Redirect to the specified url using the specified page.
        /// </summary>
        /// <param name="page">The page which will be the referrer page.</param>
        /// <param name="destinationUrl">The destination Url to which the post and redirection is occuring.</param>
        /// <param name="data">The data should be posted.</param>
        /// <Author>Amin Rahimi</Author>
        public static void RedirectAndPOST( string destinationUrl, NameValueCollection data)
        {
            //Prepare the Posting form
            string strForm = PreparePOSTForm(destinationUrl, data);

            //Add a literal control the specified page holding the Post Form, this is to submit the Posting form with the request.
            //page.Controls.Add(new LiteralControl(strForm));
        }


        public static string RedirectAndPOSTString(string destinationUrl, NameValueCollection data)
        {
            //Prepare the Posting form
            return PreparePOSTForm(destinationUrl, data);

            //Add a literal control the specified page holding the Post Form, this is to submit the Posting form with the request.
            //page.Controls.Add(new LiteralControl(strForm));
        }

    }
}
