using System.Threading.Tasks;

namespace Essiq.Showroom.Server.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}