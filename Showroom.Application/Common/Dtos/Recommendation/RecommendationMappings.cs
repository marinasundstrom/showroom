using Showroom.Domain.Entities;

namespace Showroom.Application.Common.Dtos
{
    public class RecommendationMappings : AutoMapper.Profile
    {
        public RecommendationMappings()
        {
            CreateMap<RecommendConsultantCommand, ConsultantRecommendation>();
            CreateMap<ShowInterestCommand, ClientConsultantInterest>();
            CreateMap<UpdateInterestCommand, ClientConsultantInterest>();

            CreateMap<ConsultantRecommendation, ClientConsultantRecommendationDto>();
            CreateMap<ClientConsultantInterest, ClientConsultantInterestDto>();
        }
    }
}
