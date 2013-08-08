using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Easyfy.CHS.Data.Raven;
using Easyfy.CHS.Model.Athlete;

namespace Easyfy.CHS.Controllers
{
    public class HomeController : RavenController
    {
        public ActionResult Index()
        {
            string id = User.Identity.Name;
            var model = RavenSession.Load<Athlete>(string.Format("athletes/{0}", id));

            return View(model);
        }

        [ChildActionOnly]
        public PartialViewResult UserInfo()
        {
            string id = User.Identity.Name;
            var model = RavenSession.Load<Athlete>(string.Format("athletes/{0}", id));

            return PartialView(model);
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            return View("Contact");
        }

        [HttpPost]
        public ActionResult Contact(string email, string name, string subject, string body)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress(email, name);

                message.From = fromAddress;//here you can set address
                message.To.Add("sofia.ryden92@gmail.com");//here you can add multiple to
                message.Subject = subject;//subject of email

                message.Body = body;
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Contact", ex);
            }

            return View("Contact");
        }
    }
}
