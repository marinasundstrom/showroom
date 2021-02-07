using System.Threading.Tasks;

using Showroom.Server.Client;

namespace Showroom.Client.Services
{
    public interface IIdentityService
    {
        Task LoginAsync(string usernId, string password);
        Task LogoutAsync();
        Task<UserProfile> GetUserProfileAsync();
    }
}
