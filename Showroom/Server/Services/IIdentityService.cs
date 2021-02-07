using System.Threading.Tasks;
using Showroom.Server.Models;

namespace Showroom.Server.Services
{
    public interface IIdentityService
    {
        Task<User> GetUserAsync();
    }
}