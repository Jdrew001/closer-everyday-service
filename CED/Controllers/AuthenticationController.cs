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

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationDTO request)
        {
            request.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var response = await _authenticationService.Register(request);
            return response.IsUserCreated ? Ok(response): Unauthorized(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            request.IpAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();
            string deviceUUID = request.DeviceUUID;

            if (deviceUUID == null || deviceUUID.Equals(""))
            {
                return BadRequest("DEVICE NOT FOUND");
            }

            var response = await _authenticationService.Login(request, deviceUUID);
            return response.IsAuthenticated ? Ok(response): BadRequest(response);
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            refreshTokenDTO.IpAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();
            string deviceUUID = refreshTokenDTO.DeviceUUID;
            if (deviceUUID == null || deviceUUID.Equals(""))
            {
                return BadRequest("DEVICE NOT FOUND");
            }

            refreshTokenDTO.DeviceUUID = deviceUUID;
            var response = await _authenticationService.RefreshToken(refreshTokenDTO);
            return response.IsAuthenticated ? Ok(response): BadRequest(response);
        }
    }
}
