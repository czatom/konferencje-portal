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
        public ActionResult Index()
        {
            var publications = db.Publications.Include(p => p.Conference);
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
        [Authorize(Roles = "canPublish")]
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
        [Authorize(Roles = "canPublish")]
        public ActionResult Create([Bind(Include = "ID,ConferenceID,Title,Description,File", Exclude ="ApplicationUserId")] Publication publication)
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
        public ActionResult Edit([Bind(Include = "ID,ApplicationUserId,ConferenceID,Title,Description,File")] Publication publication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Theme", publication.ConferenceID);
            return View(publication);
        }

        // GET: Publications/Delete/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            Publication publication = db.Publications.Find(id);
            db.Publications.Remove(publication);
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
