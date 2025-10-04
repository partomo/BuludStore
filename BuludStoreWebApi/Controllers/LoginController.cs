using BuludStore.Identity.Application.Commands.Users;
using BuludStore.Identity.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuludStore.Controllers;

[ApiController]
[Route("api/[controller]")]

public class LoginController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TokenDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var result = await mediator.Send(command);
        return Created(nameof(Login), result);
    }
}