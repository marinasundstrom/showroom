
using Essiq.Showroom.Server.Models;

namespace Essiq.Showroom.Server.Dtos
{
    public class UserProfileMappings : AutoMapper.Profile
    {
        public UserProfileMappings()
        {
            //  Common

            CreateMap<ConsultantProfile, ProfileShortDto>();
            CreateMap<ManagerProfile, ProfileShortDto>();
            CreateMap<ClientProfile, ProfileShortDto>();
            CreateMap<UserProfile, ProfileShortDto>();

            // Organizations

            CreateMap<OrganizationDto, Organization>();
            CreateMap<Organization, OrganizationDto>();

            CreateMap<Organization, ProfileOrganizationDto>();

            // Competence Areas

            CreateMap<CompetenceAreaDto, CompetenceArea>();
            CreateMap<CompetenceArea, CompetenceAreaDto>();

            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();

            // Profiles

            CreateMap<UserProfile, UserProfileDto>();
            CreateMap<UpdateUserProfileDto, UserProfile>();

            CreateMap<ConsultantProfile, ConsultantProfileDto>();
            CreateMap<ConsultantProfile, UserProfileDto>();
            CreateMap<AddConsultantProfileDto, ConsultantProfile>();
            CreateMap<UpdateConsultantProfileDto, ConsultantProfile>();

            CreateMap<ClientProfile, ClientProfileDto>();
            CreateMap<ClientProfile, UserProfileDto>();
            CreateMap<AddClientProfileDto, ClientProfile>();
            CreateMap<UpdateClientProfileDto, ClientProfile>();

            CreateMap<ManagerProfile, ManagerProfileDto>();
            CreateMap<ManagerProfile, UserProfileDto>();
            CreateMap<AddManagerProfileDto, ManagerProfile>();
            CreateMap<UpdateManagerProfileDto, ManagerProfile>();
        }
    }
}
