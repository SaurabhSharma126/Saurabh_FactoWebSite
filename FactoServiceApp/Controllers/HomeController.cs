using FactoServiceApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace FactoServiceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult saveContactInfo(string contactInfo)
        {
            try
            {
                MailMessage msg = new MailMessage();

                msg.From = new MailAddress("saurabhsharma126@gmail.com");
                msg.To.Add("saurabhsharma126@gmail.com");
                msg.Subject = "Contact Information received on - " + DateTime.UtcNow;
                msg.Body = @"Please find the contact information - " + contactInfo;
                //msg.Priority = MailPriority.High;


                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("saurabhsharma126@gmail.com", "hvcqxxgfkipwejdw");
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    client.Send(msg);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Contact information saving Failed!"
                });
            }
            return Json(new
            {
                Success = true,
                Message = "Contact Info Submitted !"
            });
        }
    }
}