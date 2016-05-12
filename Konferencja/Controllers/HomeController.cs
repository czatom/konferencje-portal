using Konferencja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Konferencja.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Conferences.Where(c=>c.Date >= DateTime.Now).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "O portalu.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt z nami.";

            return View();
        }
    }
}