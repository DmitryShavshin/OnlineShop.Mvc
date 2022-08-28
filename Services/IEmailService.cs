using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface IEmailService
    {
        void ContactUs(EmailSender request);
        void SendEmail(EmailSender request);
    }
}
