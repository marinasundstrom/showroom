using System.Threading.Tasks;

namespace Showroom.Application.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}