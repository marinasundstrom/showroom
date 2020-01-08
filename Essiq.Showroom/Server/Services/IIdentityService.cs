using System.Threading.Tasks;
using Essiq.Showroom.Server.Models;

namespace Essiq.Showroom.Server.Services
{
    public interface IIdentityService
    {
        Task<User> GetUserAsync();
    }
}