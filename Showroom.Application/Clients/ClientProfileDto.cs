using Showroom.Application.Common.Dtos;

namespace Showroom.Application.Clients
{
    public class ClientProfileDto : UserProfileDto
    {
        public ProfileShortDto Reference { get; set; }
    }
}
