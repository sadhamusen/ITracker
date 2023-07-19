
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using ITracker.Models;
using Microsoft.AspNetCore.Authorization;
using InitiativeTracker.Models;
using InitiativeTracker.DataBaseConnection;
using Microsoft.EntityFrameworkCore;

namespace InitiativesTracker.Controllers
{
    [ApiController]
    [Route("/mail")]
    public class EMailController : Controller
    {
        private readonly DatabaseAccess databaseAccess;

        public EMailController(DatabaseAccess databaseAccess)
        {
            this.databaseAccess = databaseAccess;
        }

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


        [HttpGet]
        [Route("{userid}")]
        public async Task<ActionResult<User>> getemail([FromRoute] int userid){
           var u=databaseAccess.usersTable.Find(userid);

            return Ok(new { email=u.email});
          }
        //[HttpGet]
        //[Route("all")]
        //public async Task<ActionResult<User>> getemail(User user)
        //{
        //    var u =await databaseAccess.usersTable.Where(x=>x.email!=null).ToListAsync();
            
        //    return Ok(u);
        //}

    }
}
