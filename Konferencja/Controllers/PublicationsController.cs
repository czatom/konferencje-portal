using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Konferencja.Models;
using Microsoft.AspNet.Identity;

namespace Konferencja.Controllers
{
    public class PublicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Publications
        [Authorize(Roles = "canPublish,  canEdit")]
        public ActionResult Index()
        {
            IQueryable<Publication> publications;
            if (User.IsInRole("canEdit"))
            {
                publications = db.Publications.Include(p => p.Conference);
            }
            else if (User.IsInRole("canPublish"))
            {
                string id = User.Identity.GetUserId();
                publications = db.Publications.Where(p => p.ApplicationUser.Id == id).Include(p => p.Conference);
            }
            else
            {
                return new HttpUnauthorizedResult("Nie masz uprawnień do przeglądania tej strony");
            }
            return View(publications.ToList());
        }

        // GET: Publications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = db.Publications.Find(id);
            if (publication == null)
            {
                return HttpNotFound();
            }
            return View(publication);
        }

        // GET: Publications/Create
        [Authorize(Roles = "canPublish")]
        public ActionResult Create()
        {
            ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Theme");
            return View();
        }

        // GET: Publications/Create

        [ActionName("CreateForConference")]
        [Authorize(Roles = "canPublish,  canEdit")]
        public ActionResult Create(int conferenceID)
        {
            ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Theme", conferenceID);
            return View("Create");
        }

        // POST: Publications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canPublish, canEdit")]
        public ActionResult Create([Bind(Include = "ID,ConferenceID,Title,Description,File", Exclude = "ApplicationUserId")] Publication publication)
        {
            ModelState.Remove("ApplicationUserId");
            if (ModelState.IsValid)
            {
                publication.ApplicationUserId = User.Identity.GetUserId();
                db.Publications.Add(publication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Theme", publication.ConferenceID);
            return View(publication);
        }

        // GET: Publications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = db.Publications.Find(id);
            if (publication == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Theme", publication.ConferenceID);
            return View(publication);
        }

        // POST: Publications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ApplicationUserId,ConferenceID,Title,Description,File,Status")] Publication publication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publication).State = EntityState.Modified;
                db.SaveChanges();
                //TODO uzależnić treść maila od statusu
                string body = "<p>Witaj {0}</p><br>" +
                "<p>Zmienił się status publikacji {1}. " +
                "<p>Aby zobaczyć status publikacji wejdź na naszą stronę.</p><br>" +
                "<p>Pozdrawiamy</p><br>" +
                "<p>Zespół konferencje.azurewesites.net</p>";
                var appUser = db.Users.Find(publication.ApplicationUserId);
                string msg = string.Format(body, appUser.FullName,
                    publication.Title);
                Mail.Send("konferencje.agh@gmail.com", publication.ApplicationUser.Email, "Zmiana statusu publikacji", true, msg);

                return RedirectToAction("Index");
            }
            ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Theme", publication.ConferenceID);
            return View(publication);
        }

        // GET: Publications/Delete/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = db.Publications.Find(id);
            if (publication == null)
            {
                return HttpNotFound();
            }
            return View(publication);
        }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult DeleteConfirmed(int id)
        {
            Publication publication = db.Publications.Find(id);
            db.Publications.Remove(publication);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Publications/Manage
        [Authorize(Roles = "canEdit, canPublish")]
        public ActionResult Manage()
        {
            ManagePublicationsViewModel model;
            if (User.IsInRole("canEdit"))
            {
                model = new ManagePublicationsViewModel
                {
                    PendingPublications = db.Publications.Where(p => p.Status == Status.NoAction && p.Reviews.Where(r => r.Grade.HasValue).Count() > 0).ToList(),
                    PublicationsWithoutReviews = db.Publications.Where(p => p.Status == Status.NoAction && p.Reviews.Count == 0).ToList(),
                    AcceptedPublications = db.Publications.Where(p => p.Status == Status.Accepted).ToList(),
                    PublicationsWithoutAssessment = db.Publications.Where(p => p.Status == Status.NoAction && p.Reviews.Where(r => !r.Grade.HasValue).Count() > 0).ToList(),
                    RejectedPublications = db.Publications.Where(p => p.Status == Status.Rejected).ToList()

                };
            }
            else if (User.IsInRole("canPublish"))
            {
                string id = User.Identity.GetUserId();
                model = new ManagePublicationsViewModel
                {
                    PendingPublications = db.Publications.Where(p => p.Status == Status.NoAction && p.Reviews.Where(r => r.Grade.HasValue).Count() > 0 && p.ApplicationUserId == id).ToList(),
                    PublicationsWithoutReviews = db.Publications.Where(p => p.Status == Status.NoAction && p.Reviews.Count == 0 && p.ApplicationUserId == id).ToList(),
                    AcceptedPublications = db.Publications.Where(p => p.Status == Status.Accepted && p.ApplicationUserId == id).ToList(),
                    PublicationsWithoutAssessment = db.Publications.Where(p => p.Status == Status.NoAction && p.Reviews.Where(r => !r.Grade.HasValue).Count() > 0 && p.ApplicationUserId == id).ToList(),
                    RejectedPublications = db.Publications.Where(p => p.Status == Status.Rejected && p.Conference.Date <= DateTime.Now && p.ApplicationUserId == id).ToList()
                };
            }
            else
            {
                return new HttpUnauthorizedResult("Nie masz uprawnień do przeglądania tej strony");
            }
            return View(model);
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
