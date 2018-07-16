using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Tamin.Registration.Utility.Captcha;

namespace Tamin.Registration.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [NoBrowserCache]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "None")]
        public CaptchaImageResult CaptchaImage(string rndDate)
        {
            return new CaptchaImageResult();
        }
    }
}