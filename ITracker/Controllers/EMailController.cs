
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using ITracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace InitiativesTracker.Controllers
{
    [ApiController]
    [Route("/mail")]
    public class EMailController : Controller
    {

        [HttpPost]
        [Authorize(Roles = "Admin,Approver,User")]
        public string SendMail([FromBody] string[] emailArray)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("sadhamusen.it19@bitsathy.ac.in");
                string[] strArray = emailArray;




                for (int i = 0; i < strArray.Length; i++)
                {
                    mail.To.Add(emailArray[i]);
                    mail.Subject = "testing";
                    mail.Body = "<h1>testing</h1>";
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential("sadhamusen.it19@bitsathy.ac.in", "Sadham@2002");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                return "mail sent";
            }
        }
    }
}
