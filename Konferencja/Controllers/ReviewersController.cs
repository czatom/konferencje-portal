using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Konferencja.Models;

namespace Konferencja.Controllers
{
    public class ReviewersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviewers
        public ActionResult Index()
        {
            var reviewers = db.Reviewers.Include(r => r.ApplicationUser);
            return View(reviewers.ToList());
        }

        // GET: Reviewers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reviewer reviewer = db.Reviewers.Find(id);
            if (reviewer == null)
            {
                return HttpNotFound();
            }
            return View(reviewer);
        }

        // GET: Reviewers/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName", -1);
            return View();
        }

        // POST: Reviewers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Surname,Specialisation,Email,Photo,References,ApplicationUserId")] Reviewer reviewer)
        {
            if (ModelState.IsValid)
            {
                db.Reviewers.Add(reviewer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName", reviewer.ApplicationUserId);
            return View(reviewer);
        }

        // GET: Reviewers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reviewer reviewer = db.Reviewers.Find(id);
            if (reviewer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName", reviewer.ApplicationUserId);
            return View(reviewer);
        }

        // POST: Reviewers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Surname,Specialisation,Email,Photo,References,ApplicationUserId")] Reviewer reviewer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviewer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName", reviewer.ApplicationUserId);
            return View(reviewer);
        }

        // GET: Reviewers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reviewer reviewer = db.Reviewers.Find(id);
            if (reviewer == null)
            {
                return HttpNotFound();
            }
            return View(reviewer);
        }

        // POST: Reviewers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reviewer reviewer = db.Reviewers.Find(id);
            db.Reviewers.Remove(reviewer);
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
