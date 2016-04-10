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
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviews
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Publication).Include(r => r.Reviewer);
            return View(reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.PublicationID = new SelectList(db.Publications, "ID", "Title");
            ViewBag.ReviewerID = new SelectList(db.Reviewers, "ID", "FullName");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Grade,Description,Token,ReviewerID,PublicationID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PublicationID = new SelectList(db.Publications, "ID", "Title", review.PublicationID);
            ViewBag.ReviewerID = new SelectList(db.Reviewers, "ID", "FullName", review.ReviewerID);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.PublicationID = new SelectList(db.Publications, "ID", "Title", review.PublicationID);
            ViewBag.ReviewerID = new SelectList(db.Reviewers, "ID", "FullName", review.ReviewerID);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Grade,Description,Token,ReviewerID,PublicationID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PublicationID = new SelectList(db.Publications, "ID", "Title", review.PublicationID);
            ViewBag.ReviewerID = new SelectList(db.Reviewers, "ID", "FullName", review.ReviewerID);
            return View(review);
        }

        // GET: Reviews/ReviewersEdit/ED93A579-05F8-E511-BBA1-001A7DDA7108
        public ActionResult ReviewersEdit(Guid? token)
        {
            if (token == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Where(r => r.Token == token).First();
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.PublicationID = new SelectList(db.Publications, "ID", "Title", review.PublicationID);
            ViewBag.ReviewerID = new SelectList(db.Reviewers, "ID", "FullName", review.ReviewerID);
            return View(review);
        }

        // POST: Reviews/ReviewersEdit/ED93A579-05F8-E511-BBA1-001A7DDA7108
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReviewersEdit([Bind(Include = "ID,Grade,Description,Token,ReviewerID,PublicationID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PublicationID = new SelectList(db.Publications, "ID", "Title", review.PublicationID);
            ViewBag.ReviewerID = new SelectList(db.Reviewers, "ID", "FullName", review.ReviewerID);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
