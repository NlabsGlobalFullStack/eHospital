using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers;

public sealed class DoctorsController(IUserService userService) : ApiController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllDoctors(CancellationToken cancellationToken)
    {
        var response = await userService.GetAllDoctorsAsync(cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
