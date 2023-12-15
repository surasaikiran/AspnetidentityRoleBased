using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace AspnetidentityRoleBased.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string to, string subject, string body)
        {
            var smtpClient = new SmtpClient("hostname or IP address")//SMTP (Simple Mail Transfer Protocol) 
            {
                Port = 587,
                Credentials = new NetworkCredential("saikiransura19@gmail.com", "SaiKiran@19"),// from address
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(""),//To Email Address
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(to);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to send email: {ex.Message}");
            }
        }
    }
}
