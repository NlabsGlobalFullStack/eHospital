﻿using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Abstractions;

[Route("api/[controller]/[action]")]
[ApiController]
public abstract class ApiController : ControllerBase
{
}
