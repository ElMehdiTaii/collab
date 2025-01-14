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

        CreateMap<Board, GetAllBoardDto>()
            .ForMember(dest => dest.TaskProgress, opt => opt.MapFrom(src => CalculateProgress(src.Tasks.ToList())))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => GetGlobalStatus(src.Tasks.ToList())))
            .ForMember(dest => dest.BoardTeam, opt => opt.MapFrom(src => src.Tasks
            .Select(t => t.User)
            .Distinct()
            .Select(u => new BoardTeamDto(u.FullName))
            .ToList()))
            .ForMember(dest => dest.TaskCount, opt => opt.MapFrom(src => src.Tasks.Count()))
            .ForMember(dest => dest.CompletedTask, opt => opt.MapFrom(src => src.Tasks.Where(t => t.Status == (int)Domain.Enums.TaskStatus.COMPLETED).Count()));

        CreateMap<User, BoardTeamDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
    }
    private static int CalculateProgress(List<Domain.Entities.Task> tasks)
    {
        var totalTasks = tasks.Count(t => t.Status != (int)Domain.Enums.TaskStatus.CLOSED);

        if (totalTasks == 0)
            return 0;

        var completedTasks = tasks.Count(t => t.Status == (int)Domain.Enums.TaskStatus.COMPLETED);

        return (int)((double)completedTasks / totalTasks * 100);
    }
    private static string GetGlobalStatus(IEnumerable<Domain.Entities.Task> tasks)
    {
        int totalTasks = tasks.Count(t => t.Status != (int)Domain.Enums.TaskStatus.CLOSED);

        bool completedTasks = tasks.Count(task => task.Status == (int)Domain.Enums.TaskStatus.COMPLETED) == totalTasks && totalTasks != 0;
        
        int openTasks = tasks.Count(task => task.Status == (int)Domain.Enums.TaskStatus.OPEN);
        int newTasks = tasks.Count(task => task.Status == (int)Domain.Enums.TaskStatus.NEW);
        int closedTasks = tasks.Count(task => task.Status == (int)Domain.Enums.TaskStatus.CLOSED);

        if (completedTasks)
        {
            return nameof(Domain.Enums.TaskStatus.COMPLETED);
        }

        if (openTasks > 0)
        {
            return nameof(Domain.Enums.TaskStatus.OPEN);
        }

        if (newTasks > 0)
        {
            return nameof(Domain.Enums.TaskStatus.NEW);
        }

        if (closedTasks > 0)
        {
            return nameof(Domain.Enums.TaskStatus.NEW);
        }

        return string.Empty;
    }
}
