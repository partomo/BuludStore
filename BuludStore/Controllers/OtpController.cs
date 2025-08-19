using BuludStore.Application.Commands.Otp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuludStore.Controllers;

[ApiController]
[Route("api/[controller]")]

public class OtpController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Send([FromBody] SendOtpCommand command)
    {
        var otp = await mediator.Send(command);
        return Created(nameof(Send), otp);
    }
}