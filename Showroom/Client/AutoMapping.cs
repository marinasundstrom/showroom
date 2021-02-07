
using System;

using AutoMapper;

using Showroom.Server.Client;

namespace Showroom.Client
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Organization, Models.ListItem>();
            CreateMap<CompetenceArea, Models.ListItem>();

            CreateMap<UserProfile, UpdateUserProfile>();

            CreateMap<ManagerProfile, UpdateManagerProfile>();
            CreateMap<UpdateConsultantProfile, AddManagerProfile>();

            CreateMap<ConsultantProfile, UpdateConsultantProfile>();
            CreateMap<UpdateManagerProfile, AddConsultantProfile>();

            CreateMap<ClientProfile, UpdateClientProfile>();
            CreateMap<UpdateClientProfile, AddClientProfile>();

            // Necessary?
            CreateMap<Organization, string>().ConvertUsing(op => op.Id);

            CreateMap<ProfileOrganization, string>().ConvertUsing(op => op.Id);
            CreateMap<CompetenceArea, string>().ConvertUsing(op => op.Id);
            CreateMap<ProfileShort, Guid>().ConvertUsing(op => op.Id);
        }
    }
}
