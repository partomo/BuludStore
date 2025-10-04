using Bazta.Identity.Application.Commands.Users;
using BuludStore.Identity.Application.Commands.Users;
using BuludStore.Identity.Application.DTOs;
using BuludStore.Identity.Application.Queries.Users;
using Bulud.Base;
using Bulud.Base.Attributes;
using Bulud.Base.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuludStore.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResult<UserDto>))]
    [PermissionRequirement("Users:Retrieve")]
    public async Task<IActionResult> List([FromQuery] GetUsersRequestQuery query)
    {
        var members = await mediator.Send(query);
        return Ok(members);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [PermissionRequirement("Users:Retrieve")]
    public async Task<IActionResult> Return(string id,[FromQuery] RequestQuery query)
    {
        var member = await mediator.Send(new GetUserQuery { UserId = id , Query = query});
        return Ok(member);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [PermissionRequirement("Users:Create")]
    public async Task<IActionResult> Add([FromBody] AddUserCommand command)
    {
        var member = await mediator.Send(command);
        return Created(nameof(Add), member);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [PermissionRequirement("Users:Update")]
    public async Task<IActionResult> Update([FromBody] EditUserCommand command)
    {
        var member = await mediator.Send(command);
        return Ok(member);
    }

    [HttpPatch]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [PermissionRequirement("Users:Update")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        var member = await mediator.Send(command);
        return Ok(member);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [PermissionRequirement("Users:Delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteUserCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}