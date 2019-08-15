using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tamin.Registration.Utility.Captcha;
using Tamin.Registration.Web.Models;

namespace Tamin.Registration.Web.Controllers
{
    public class RegisterController : Controller
    {
        DataLayer.RegistrationDbContext db = new DataLayer.RegistrationDbContext();
        [HttpGet]
        public ActionResult Index(int? eventId)
        {
            if (eventId.HasValue)
            {

                using (var db = new DataLayer.RegistrationDbContext())
                {
                    var evenx = db.Events.Find(eventId.Value);
                    if (evenx == null)
                    {
                        return View("Error");
                    }
                    var m = new RegisterFormViewModel();
                    m.EventId = eventId.Value;
                    ViewBag.EventTitle = evenx.EName;
                    return View(m);
                }
            }
            return View("Error");
        }

        [HttpPost, ValidateCaptchaAttribute(ExpireTimeCaptchaCodeBySeconds = 1800), ValidateAntiForgeryToken]
        public ActionResult Index(Models.RegisterFormViewModel model)
        {
            

            if (ModelState.IsValid)
            {
                System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                try
                {
                    var total = 296000;
                    //if (model.EventId == 1 || model.EventId == 2)
                    //{
                    //    total = 300000;
                    //}
                    //else if (model.EventId == 3)
                    //{
                    //    total = 500000;
                    //}
                    //else
                    //{
                    //    throw new Exception();
                    //}

                    var entity = new DataLayer.Entities.RegisterForm()
                    {

                        NatinalCode = model.NatinalCode,
                        Username = model.Username,
                        Password = model.Password,
                        Email = model.Email,
                        EName = model.EName,
                        EFamily = model.EFamily,
                        Name = model.Name,
                        Family = model.Family,
                        PhotoFilename = model.PhotoFilename,
                        NatinalCardPhoto = model.NatinalCardPhoto,
                        LastDegrePhotoFilename = model.LastDegrePhotoFilename,
                        University = model.University,
                        Level = model.Level,
                        Major = model.Major,
                        Degree = model.Degree,
                        Job = model.Job,
                        JobTelephone = model.JobTelephone,
                        AltTelephone = model.AltTelephone,
                        Telephone = model.Telephone,
                        Mobile = model.Mobile,
                        Country = model.Country,
                        City = model.City,
                        Adderess = model.Adderess,
                        PostCode = model.PostCode,
                        RegisterOn = DateTime.Now,
                        PaiedOn = null,
                        Total = total,
                        TraceNumber = null,
                        TransactionDate = null,
                        TransactionReferenceID = null,
                        Birthday = DateTime.Now,
                        EventId = model.EventId
                    };

                    db.RegisterForms.Add(entity);
                    db.SaveChanges();
                    return RedirectToAction("RegisterConfirm", new { ncode = entity.NatinalCode });
                }
                catch (DbEntityValidationException e)
                {
                    var content = "";
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        content += (string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            content += (string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    return Content(content);
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
                Username = entity.Username,
                Password = entity.Password,
                Email = entity.Email,
                EName = entity.EName,
                EFamily = entity.EFamily,
                Name = entity.Name,
                Family = entity.Family,
                PhotoFilename = entity.PhotoFilename,
                NatinalCardPhoto = entity.NatinalCardPhoto,
                LastDegrePhotoFilename = entity.LastDegrePhotoFilename,
                University = entity.University,
                Level = entity.Level,
                Major = entity.Major,
                Degree = entity.Degree,
                Job = entity.Job,
                JobTelephone = entity.JobTelephone,
                AltTelephone = entity.AltTelephone,
                Telephone = entity.Telephone,
                Mobile = entity.Mobile,
                Country = entity.Country,
                City = entity.City,
                Adderess = entity.Adderess,
                PostCode = entity.PostCode,
                EventId = entity.EventId,
                NatinalCode = entity.NatinalCode
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