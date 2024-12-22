using AutoMapper;
using Collaboration.Application.Features.Authentication.Queries.AuthenticationQuery;
using Collaboration.Domain.DTOs.Authentication;
using Collaboration.Domain.Entities;

namespace Collaboration.MVC.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<LoginDto, AuthenticationQuery>().ReverseMap();

        CreateMap<User, UserInformationDto>().ReverseMap();
    }
}
