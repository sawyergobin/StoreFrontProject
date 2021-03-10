using StoreFrontLab.UI.MVC.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace StoreFrontLab.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Authorize] Commented out until logins exits
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel contactFormVM)
        {
            if (!ModelState.IsValid)
            {
                //repopulate the form and show validation errors if form isn't valid
                return View();
            }

            string message = $"You have received an email from {contactFormVM.Name} with a subject of " +
                $"{(contactFormVM.Subject == null ? "Email From -WebsiteName-" : contactFormVM.Subject)}. Please respond to" +
                $"{contactFormVM.EmailAddress} with your response to the following message:<br /><br />" +
                $"{contactFormVM.Message}";

            //use system.net.mail as system.web.mail is deprecated
            MailMessage mm = new MailMessage("admin@sawyergobin.com", "sawyergobin@outlook.com", contactFormVM.Subject, message);

            //++++++++++++MailMessage Properties+++++++++++++++++++++
            mm.IsBodyHtml = true;

            mm.Priority = MailPriority.High;

            //Use ReplyToList as ReplyTo is deprecated
            mm.ReplyToList.Add(contactFormVM.EmailAddress);

            SmtpClient client = new SmtpClient("mail.sawyergobin.com"); // name of the mail host server on smarterasp.net

            //++++++++++++++++++++++DO NOT UPLOAD THIS TO A PUBLIC REPO WITHOUT BLOCKING OUT PASSWORDS++++++++++++++++++++++++++
            client.Credentials = new NetworkCredential("admin@sawyergobin.com", "XWv8Mqe6Wt_");

            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry, but your request could not be completed at this time. There appears to be a problem with the mail host. Please try again later." +
                    $"<br />Error Message: <br /><br /> {ex.StackTrace}";
                return View(contactFormVM);
            }

            //if all goes well, we return a confirmation view
            return View("EmailConfirmation", contactFormVM);
            
        }//end Contact Email ActionResult
    }
}


