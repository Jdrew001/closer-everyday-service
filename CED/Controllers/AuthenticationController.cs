using CED.Models.DTO;
using CED.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("Test")]
        public async Task<IActionResult> Test()
        {
            //request.IpAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();
            //await _authenticationService.Login(request);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            request.IpAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();
            await _authenticationService.Login(request);
            return Ok();
        }
    }
}
