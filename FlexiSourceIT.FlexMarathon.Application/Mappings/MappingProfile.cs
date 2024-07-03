using AutoMapper;
using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;

namespace FlexiSourceIT.FlexMarathon.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserProfileModel, UserProfile>().ReverseMap();
        CreateMap<ActivityModel, Activity>().ReverseMap();
    }
}

