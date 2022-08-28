using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(string email, string description)
        {
            var emailSnder = new EmailSender()
            {
                To = "Shavshindm@gmail.com",
                Subject = email,
                Body = description
            };
            if (emailSnder != null)
            {
                _emailService.ContactUs(emailSnder);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmail(string email, string description)
        {
            var emailSnder = new EmailSender() {
                To = email,
                Body = description
            };
            if (emailSnder != null)
            {
                _emailService.SendEmail(emailSnder);
            }
            return View();
        }
    }
}
