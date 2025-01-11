using AutoMapper;
using Collaboration.Application.Features.Authentication.Queries.AuthenticationQuery;
using Collaboration.Application.Features.Board.Commands.CreateBoardCommand;
using Collaboration.Domain.DTOs.Authentication;
using Collaboration.Domain.DTOs.Board;
using Collaboration.Domain.Entities;

namespace Collaboration.MVC.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<LoginDto, AuthenticationQuery>().ReverseMap();

        CreateMap<User, UserInformationDto>().ReverseMap();

        CreateMap<CreateBoardDto, CreateBoardCommand>().ReverseMap();
    }
}
