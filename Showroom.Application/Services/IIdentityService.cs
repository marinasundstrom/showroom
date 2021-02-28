using System.Threading.Tasks;
using Showroom.Domain.Entities;

namespace Showroom.Application.Services
{
    public interface IIdentityService
    {
        Task<User> GetUserAsync();
    }
}
