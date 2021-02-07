using System.Threading.Tasks;

namespace Showroom.Client.Services
{
    public interface IJSHelpers
    {
        Task Alert(string message);
        Task<bool> Confirm(string message);
        Task<string> Prompt(string message, string @default);
        Task SetTitle(string title);
        Task ScrollToTop();
        Task ScrollToBottom();
    }
}
