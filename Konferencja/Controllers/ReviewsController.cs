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

        [ActionName("CreateForPublication")]
        [Authorize(Roles = "canPublish,  canEdit")]
        public ActionResult Create(int publicationID)
        {
            ViewBag.PublicationID = new SelectList(db.Publications, "ID", "Title", publicationID);
            ViewBag.ReviewerID = new SelectList(db.Reviewers, "ID", "FullName");
            return View("Create");
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

                var reviewer = db.Reviewers.Find(review.ReviewerID);
                var pub = db.Publications.Find(review.PublicationID);

                string body = "<p>Witaj {0}</p><br>" + 
                    "<p>Przesyłamy do recenzji publikację zatytułowaną {1}. " +
                    "Publikacja dostępna jest pod adresem <a href=\"{2}\">tutaj</a>.</p><br>" +
                    "<p>Aby edytować recenzję i wystawić ocenę wejdź <a href=\"{3}/Reviews/ReviewersEdit?token={4}\">tutaj</a>.</p><br>" + 
                    "<p>Pozdrawiamy</p><br>" + 
                    "<p>Zespół konferencje.azurewesites.net</p>";
                string msg = string.Format(body, reviewer.FullName, 
                    pub.Title,
                    pub.File,
                    Request.Url.Host, review.Token);
                Mail.Send("konferencje.agh@gmail.com", reviewer.Email, "Prośba o recenzję", true, msg);

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

        // GET: Reviews/ReviewersEdit?token=ED93A579-05F8-E511-BBA1-001A7DDA7108
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

                var pub = db.Publications.Find(review.PublicationID);
                var user = db.Users.Find(pub.ApplicationUserId);
                string body = "<p>Witaj {0}</p><br>" +
                "<p>Pojawiła się recenzja publikacji {1}. " +
                "<p>Aby zobaczyć status publikacji wejdź na naszą stronę.</p><br>" +
                "<p>Pozdrawiamy</p><br>" +
                "<p>Zespół konferencje.azurewesites.net</p>";
                string msg = string.Format(body, user.FullName,
                    pub.Title);
                Mail.Send("konferencje.agh@gmail.com", user.Email, "Recenzja publikacji", true, msg);

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
