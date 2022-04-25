﻿using CED.Models.DTO;
using CED.Services.Interfaces;
using CED.Utils;
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
    public class AuthenticationController : CEDBaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(
            IAuthenticationService authenticationService,
            ITokenService tokenService)
            :base(tokenService)
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

        [HttpGet("validateCode/{code}/{email}/{deviceUUID}")]
        public async Task<IActionResult> ValidateCode(string code, string email, string deviceUUID) 
        {
            if (email == null || code == null || deviceUUID == null)
                return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR, null));

            var result = await _authenticationService.GetAuthCode(email);
            if (result == null) 
            {
                return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR, null));
            }

            if (!result.Code.Equals(code))
            {
                return BadRequest(GenerateErrorResponse("The code provided did not match.", null));
            }

            var deletionCode = await _authenticationService.DeleteUserAuthCode(email);
            if (deletionCode != null)
                return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR, null));

            // Update user's verified status
            var response = await _authenticationService.ConfirmUser(email, deviceUUID);
            return response.IsAuthenticated ? Ok(response): BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR, response));
        }

        [HttpGet("resendCode/{email}")]
        public async Task<IActionResult> ResendCode(string email) 
        {
            if (email == null)
                return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR, null));

            var result = await _authenticationService.ResendValidationCode(email);
            return result ? Ok(GenerateSuccessResponse("Successful Code Resend")): BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR));
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
