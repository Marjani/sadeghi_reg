using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tamin.Registration.Utility.Captcha;

namespace Tamin.Registration.Web.Controllers
{
    public class RegisterController : Controller
    {
        DataLayer.RegistrationDbContext db = new DataLayer.RegistrationDbContext();
        [HttpGet]
        public ActionResult Index(int? id)
        {
            return View();
        }

        [HttpPost, ValidateCaptchaAttribute(ExpireTimeCaptchaCodeBySeconds = 1800), ValidateAntiForgeryToken]
        public ActionResult Index(Models.RegisterFormViewModel model)
        {
            var exist = db.RegisterForms.Where(o => o.NatinalCode == model.NatinalCode).FirstOrDefault();
            if (exist != null)
            {
                ModelState.AddModelError("NatinalCode", new Exception("این کد ملی قبلا ثبت شده است!"));
                model.CaptchaInputText = "";
                return View(model);

            }
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var validExt = new string[] { "jpg", "jpeg", "png" };
                    var fileName = Path.GetFileName(file.FileName);
                    var name = fileName.Split('.')[0];
                    var ext = fileName.Split('.')[1];
                    if (!validExt.Contains(ext.ToLower()))
                    {
                        model.CaptchaInputText = "";
                        ModelState.AddModelError("PhotoFilename", new Exception("یک تصویر مناسب انتخاب کنید."));
                        return View(model);
                    }
                    fileName = Guid.NewGuid().ToString() + "." + ext;
                    var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                    model.PhotoFilename = fileName;
                    file.SaveAs(path);
                }
                else { ModelState.AddModelError("PhotoFilename", "یک تصویر مناسب انتخاب کنید."); }
            }
            else
            {
                ModelState.AddModelError("PhotoFilename", new Exception("یک تصویر مناسب انتخاب کنید."));
            }

            if (ModelState.IsValid)
            {
                System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                try
                {
                    var total = 350000;
                    try
                    {
                        total = int.Parse(System.Configuration.ConfigurationManager.AppSettings["total"]);
                    }
                    catch (Exception)
                    {

                    }
                    var entity = new DataLayer.Entities.RegisterForm()
                    {
                        Adderess = model.Adderess,
                        AltTelephone = model.AltTelephone,
                        Average = 0,
                        Birthday = new DateTime(int.Parse(model.Birthday.Split('/')[0]), int.Parse(model.Birthday.Split('/')[1]), int.Parse(model.Birthday.Split('/')[2]), pc),
                        City = model.City,
                        Degree = model.Degree,
                        Family = model.Family,
                        FatherName = model.FatherName,
                        Gender = model.Gender,
                        IsPaied = false,
                        Level = model.Level,
                        Mobile = model.Mobile,
                        Name = model.Name,
                        NatinalCode = model.NatinalCode,
                        PaiedOn = null,
                        PhotoFilename = model.PhotoFilename,
                        ReferenceNumber = null,
                        RegisterOn = DateTime.Now,
                        SeatNumber = null,
                        ShenasnameCode = model.ShenasnameCode,
                        Telephone = model.Telephone,
                        Total = total,
                        TraceNumber = null,
                        TransactionDate = null,
                        TransactionReferenceID = null
                    };

                    db.RegisterForms.Add(entity);
                    db.SaveChanges();
                    return RedirectToAction("RegisterConfirm", new { ncode = entity.NatinalCode });
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                catch (Exception ex)

                {
                    model.CaptchaInputText = "";
                    return View(model);
                }
            }
            model.CaptchaInputText = "";
            return View(model);
        }

        public ActionResult RegisterConfirm(string ncode)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

            var entity = db.RegisterForms.Where(o => o.NatinalCode == ncode).FirstOrDefault();
            if (entity == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new Models.RegisterFormViewModel()
            {
                Id = entity.Id,
                Adderess = entity.Adderess,
                NatinalCode = entity.NatinalCode,
                AltTelephone = entity.AltTelephone,
                Average = entity.Average,
                Birthday = pc.GetYear(entity.Birthday) + "/" + pc.GetMonth(entity.Birthday) + "/" + pc.GetDayOfMonth(entity.Birthday),
                CaptchaInputText = "",
                City = entity.City,
                Degree = entity.Degree,
                Family = entity.Family,
                FatherName = entity.FatherName,
                Gender = entity.Gender,
                Level = entity.Level,
                Mobile = entity.Mobile,
                Name = entity.Name,
                PhotoFilename = entity.PhotoFilename,
                ShenasnameCode = entity.ShenasnameCode,
                Telephone = entity.Telephone
            };

            return View(model);
        }

        //[HttpPost]
        //public ActionResult delete(int id)
        //{
        //    using (var db = new DataLayer.RegistrationDbContext())
        //    {
        //        db.RegisterForms.Remove(db.RegisterForms.Find(id));
        //        db.SaveChanges();

        //        ViewData["t"] = "info";
        //        ViewData["m"] = "ثبت نام شماره " + id + " حذف شد!";

        //        return RedirectToAction("Index","Home");
        //    }
        //}
    }
}