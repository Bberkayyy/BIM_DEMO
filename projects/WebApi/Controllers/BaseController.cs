﻿using Core.Shared;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(Response<T> response)
        {
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response);
                case System.Net.HttpStatusCode.NotFound:
                    return NotFound(response);
                case System.Net.HttpStatusCode.Created:
                    return Created("", response);
                case System.Net.HttpStatusCode.BadRequest:
                    return BadRequest(response);
                case System.Net.HttpStatusCode.Accepted:
                    return Accepted(response);
                default:
                    return NoContent();
            }
        }
    }
}
