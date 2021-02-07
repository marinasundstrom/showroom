using System.Threading.Tasks;

namespace Showroom.Server.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}