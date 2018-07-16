using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Tamin.Registration.Utility
{
    public static class Helpers
    {
        public static string IsActiveStatus(this HtmlHelper helper, bool val)
        {
            if (val)
            {
                return "فعال";
            }
            else
            {
                return "غیرفعال";
            }
        }
        public static MvcHtmlString IsActive(this HtmlHelper htmlHelper, string action, string controller, string activeClass, string inActiveClass = "")
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController);
            var r = new MvcHtmlString(returnActive ? activeClass : inActiveClass);
            return r;
        }
        public static IHtmlString PhotoPreview(this HtmlHelper helper, string title, string path)
        {
            return new HtmlString(string.Format("<button type=\"button\" class=\"btn btn-primary\" data-toggle=\"modal\" data-target=\"#photoPreview\" data-path=\"" + path + "\" data-title=\"" + title + "\" >نمایش</button>"));
        }

        public static string FixNatinalCode(Int64 natinalCode)
        {
            if (natinalCode.ToString().Length == 10)
            {
                return natinalCode.ToString();
            }
            var b = 10 - natinalCode.ToString().Length;
            var r = natinalCode.ToString();
            for (int i = 0; i < b; i++)
            {
                r += "0" + r;
            }
            return r;
        }


        public static IHtmlString AboutLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/About/" + id + "/" + title);
        }

        public static IHtmlString ServiceLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new  HtmlString("/Service/" + id + "/" + title);
        }

        public static IHtmlString YoursLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/Yours/" + id + "/" + title);
        }

        public static IHtmlString GalleryLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/Galleries/" + id + "/" + title);
        }
        public static IHtmlString MemberLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/Member/" + id + "/" + title);
        }

        public static IHtmlString LibraryLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/Library/" + id + "/" + title);
        }

        public static IHtmlString TeamLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/Member#go_" + id);
        }

        public static IHtmlString NewsLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/News/" + id + "/" + title);
        }

        public static IHtmlString NewsPostLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/News/Post/" + id + "/" + title);
        }

        public static IHtmlString ResourceLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/Resource/" + id + "/" + title);
        }

        public static IHtmlString ResourcePostLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/Resource/Post/" + id + "/" + title);
        }

        public static IHtmlString BankLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/Bank/" + id + "/" + title);
        }

        public static IHtmlString BankPostLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/Bank/Post/" + id + "/" + title);
        }

        public static IHtmlString EntertainmentLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/Entertainment/" + id + "/" + title);
        }

        public static IHtmlString ContentLink(this HtmlHelper helper, int id, string title, int category)
        {
            title = FixTitle(title);
            return ContentLink(helper, id, title, GetCategory(category));
        }
        public static IHtmlString ContentLink(this HtmlHelper helper, int id, string title, string category)
        {
            title = FixTitle(title);
            return new HtmlString("/" + category + "/" + id + "/" + title);
        }
        public static IHtmlString EntertainmentPostLink(this HtmlHelper helper, int id, string title)
        {
            title = FixTitle(title);
            return new HtmlString("/Entertainment/Post/" + id + "/" + title);
        }
        public static IHtmlString PageTitle(this HtmlHelper helper, string title)
        {

            return new HtmlString("<title>" + title + " | موسسه خیریه مهرماندگار</title>");
        }
        public static IHtmlString PageTitle(this HtmlHelper helper, string title, string category)
        {

            return new HtmlString("<title>" + title + " - " + category + " | موسسه خیریه مهرماندگار</title>");
        }
        public static IHtmlString OtherMetas()
        {
            return new HtmlString(@"<meta name=""Author"" content=""mehr-mandegar.com""><meta name=""Robots"" content=""index, follow"">");

        }
        public static IHtmlString PageDecription(this HtmlHelper helper, string description)
        {
            return new HtmlString("<meta name=\"Description\" content=\"" + description + "\">");

        }

        public static IHtmlString PageDecription(this HtmlHelper helper, string description, bool isBody)
        {
            if (!isBody)
            {
                return PageDecription(helper, description);
            }

            try
            {
                if (description.Length > 120)
                {
                    var xxx = Tamin.Registration.Utility.HtmlRemoval.StripTagsRegex(description);
                    if (xxx.Length > 100)
                    {
                        return PageDecription(helper, xxx.Substring(0, 100) + " ...");
                    }
                }
                return PageDecription(helper, "موسسه خیریه مهرماندگار ناصری");
            }
            catch
            {
                return PageDecription(helper, "موسسه خیریه مهرماندگار ناصری");

            }
        }

        private static string FixTitle(string title)
        {
            return title.Replace(' ', '-').Replace('?', '-').Replace('"', '-');
        }

        private static string GetCategory(int id)
        {
            switch (id)
            {
                case 3:
                    return "About";
                case 17:
                    return "Page";
                case 5:
                    return "Service";
                case 6:
                    return "Yours";
                case 10:
                    return "Library";
                case 18:
                    return "News";
                default:
                    return null;

            }
        }
    }
}
