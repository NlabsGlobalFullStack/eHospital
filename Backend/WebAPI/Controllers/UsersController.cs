﻿using Business.Services;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers;

public sealed class UsersController(IUserService userService) : ApiController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create(CreateUserDto request, CancellationToken cancellationToken)
    {
        var response = await userService.CreateUserAsync(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
