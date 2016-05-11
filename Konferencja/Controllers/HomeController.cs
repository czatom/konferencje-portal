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
            using (MailMessage mm = new MailMessage("subiektywny.brud@gmail.com", "conference_admin@sharklasers.com"))
            {
                mm.Subject = "temat";
                mm.Body = "test";
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("subiektywny.brud@gmail.com", "esperanza84");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                //await smtp.SendMailAsync(mm);
                smtp.Send(mm);  
            }
            return View(db.Conferences.Where(c=>c.Date >= DateTime.Now).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}