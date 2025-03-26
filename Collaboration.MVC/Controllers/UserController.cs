using AutoMapper;
using Collaboration.Application.Features.User.Queries.GetUserQuery;
using Collaboration.Domain.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers
{
    public class UserController(IMapper _mapper, IMediator _mediator) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = await _mediator.Send(new GetUserQuery(1));
            return Ok(_mapper.Map<List<GetUserDto>>(query.ToList()));
        }
    }
}
