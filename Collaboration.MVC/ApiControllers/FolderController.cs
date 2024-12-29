using AutoMapper;
using Collaboration.MVC.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.ApiControllers;

public class FolderController(IMapper _mapper, IMediator _mediator) : TaskController
{
    public IActionResult Create()
    {
        return View();
    }
}
