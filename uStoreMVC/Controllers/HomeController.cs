
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using uStoreMVC.Models;

namespace uStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Authorize]
        public ActionResult FAQ()
        {


            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }
            //We are accepting a ContactViewModel object since our Contact.cshtml is now a strongly typed view.
            //SIMILAR TO HOW REGULAR FORM PASSED VALUE:
            //Due to the way the @Html methods are rendered, the "name" attribute is created and gets assigned to
            //the correct ContactViewModel property

            //Create the body/content for the email from the ContactViewModel properties
            string body = string.Format(
                $"Name: {contact.Name}<br/>" +
                $"Email: {contact.Email}<br/>" +
                $"Subject: {contact.Subject}<br/>" +
                $"Message: <br/>{contact.Message}"
                );

            //Create and configure the MailMessage object
            MailMessage msg = new MailMessage(
                "no-reply@laastdev.com", //from address
                "laastleford@gmail.com",//where i want to receive it
                contact.Subject,//email subject
                body);// the body of the email message

            //Set properties of the MailMessage object (msg)
            msg.IsBodyHtml = true;
            //msg.CC.Add("student@domain.com");
            msg.Priority = MailPriority.High;

            //Create and configure the SMTP client (this is what will send the email)
            SmtpClient client = new SmtpClient("mail.laastdev.com");
            client.Credentials = new NetworkCredential("no-reply@laastdev.com", "P@ssw0rd0802");


            //Using the client, try and send the email message.  Stop/close the SMTPClient object (client)
            //when this is done trying to send the email.
            using (client)
            {
                try
                {
                    //We are going to tRY to send the email msg
                    client.Send(msg);
                }
                catch
                {
                    //Create a ViewBag.ErrorMessage to return to the user and send them back to the Contact View
                    ViewBag.ErrorMessage = "There was an error sending your email.  Please try again.";
                    return View(contact);
                }//catch
            }//using - client

            //Send the user to a "ContactConfirmation" View and send the contact object with it.
            return View("ContactConfirmation", contact);

        }//Contact() POST

    }
}
