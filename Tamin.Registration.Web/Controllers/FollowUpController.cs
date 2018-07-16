using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tamin.Registration.Utility.Captcha;


using Tamin.Registration.Web.Models;

namespace Tamin.Registration.Web.Controllers
{
    public class FollowUpController : Controller
    {
        // GET: FollowUp
        public ActionResult Index()
        {
            ViewBag.Id = 0;
            return View();
        }
        [HttpPost, ValidateCaptchaAttribute(ExpireTimeCaptchaCodeBySeconds = 1800), ValidateAntiForgeryToken]

        public ActionResult Index(FollowUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

                var db = new DataLayer.RegistrationDbContext();
                var reg = db.RegisterForms.Where(o => o.NatinalCode == model.NatinalCode && o.Mobile == model.Mobile ).FirstOrDefault();
                if (reg == null)
                {
                    ModelState.AddModelError("NatinalCode", "اطلاعات درخواستی در سیستم ثبت نام وجود ندارد!");
                    return View(model);
                } else
                {
                    if (reg.IsPaied)
                    {
                        return View("Cart", new CartViewModel() {
                            Family = reg.Family,
                            Name = reg.Name,
                            NatinalCode = reg.NatinalCode,
                            PhotoFilename = reg.PhotoFilename,
                            RegisterOn = pc.GetYear(reg.RegisterOn) + "/" + pc.GetMonth(reg.RegisterOn) + "/" + pc.GetDayOfMonth(reg.RegisterOn),
                            SeatNumber = reg.SeatNumber
                        });
                    }else
                    {
                        model.CaptchaInputText = "";
                        ViewBag.Id = reg.Id;
                        ViewBag.IsPaied = false;
                        ModelState.AddModelError("Id", "اطلاعات ثبت شده است، ولی پرداخت به درستی انجام نشده است!");
                    }
                }

            }
            model.CaptchaInputText = "";
            return View(model);
        }
    }
}