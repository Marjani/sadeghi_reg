using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tamin.Registration.DataLayer;
using Tamin.Registration.DataLayer.Entities;

namespace Registration.Manager.Controllers
{
    [Authorize]
    public class RegisterFormsController : Controller
    {
        private RegistrationDbContext db = new RegistrationDbContext();

        // GET: RegisterForms
        public ActionResult Index()
        {
            var registerForms = db.RegisterForms.Include(r => r.Event);
            return View(registerForms.ToList());
        }

        // GET: RegisterForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterForm registerForm = db.RegisterForms.Find(id);
            if (registerForm == null)
            {
                return HttpNotFound();
            }
            return View(registerForm);
        }

        // GET: RegisterForms/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name");
            return View();
        }

        // POST: RegisterForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,Email,EName,EFamily,NatinalCardPhoto,Job,JobTelephone,PostCode,Peygiri,PeygiriPhotoFilename,Name,Family,FatherName,NatinalCode,ShenasnameCode,Birthday,City,Adderess,Mobile,Telephone,AltTelephone,PhotoFilename,Degree,Level,Average,Gender,SeatNumber,RegisterOn,Total,IsPaied,PaiedOn,TraceNumber,ReferenceNumber,TransactionReferenceID,TransactionDate,LastDegrePhotoFilename,University,Major,Country,EventId")] RegisterForm registerForm)
        {
            if (ModelState.IsValid)
            {
                db.RegisterForms.Add(registerForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", registerForm.EventId);
            return View(registerForm);
        }

        // GET: RegisterForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterForm registerForm = db.RegisterForms.Find(id);
            if (registerForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", registerForm.EventId);
            return View(registerForm);
        }

        // POST: RegisterForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Email,EName,EFamily,NatinalCardPhoto,Job,JobTelephone,PostCode,Peygiri,PeygiriPhotoFilename,Name,Family,FatherName,NatinalCode,ShenasnameCode,Birthday,City,Adderess,Mobile,Telephone,AltTelephone,PhotoFilename,Degree,Level,Average,Gender,SeatNumber,RegisterOn,Total,IsPaied,PaiedOn,TraceNumber,ReferenceNumber,TransactionReferenceID,TransactionDate,LastDegrePhotoFilename,University,Major,Country,EventId")] RegisterForm registerForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registerForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", registerForm.EventId);
            return View(registerForm);
        }

        // GET: RegisterForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterForm registerForm = db.RegisterForms.Find(id);
            if (registerForm == null)
            {
                return HttpNotFound();
            }
            return View(registerForm);
        }

        // POST: RegisterForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisterForm registerForm = db.RegisterForms.Find(id);
            db.RegisterForms.Remove(registerForm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
