using System.Threading.Tasks;

using Essiq.Showroom.Server.Client;

namespace Essiq.Showroom.Client.Services
{
    public interface IIdentityService
    {
        Task LoginAsync(string usernId, string password);
        Task LogoutAsync();
        Task<UserProfile> GetUserProfileAsync();
    }
}
