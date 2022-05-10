﻿using CED.Models.DTO;
using CED.Services.Interfaces;
using CED.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            DeviceDTO device = RetrieveDevice();
            var response = await _authenticationService.Register(request, device);
            return response.IsUserCreated ? Ok(GenerateSuccessResponse("Successfully registered account", response)): Ok(GenerateErrorResponse(AppConstants.GENERIC_ERROR, response));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            request.IpAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();
            string deviceUUID = RetrieveDevice().UUID;

            if (deviceUUID == null || deviceUUID.Equals(""))
            {
                return BadRequest("DEVICE NOT FOUND");
            }

            var response = await _authenticationService.Login(request, deviceUUID);
            return !response.Error ? Ok(GenerateSuccessResponse("Successfully logged in", response)): Ok(GenerateErrorResponse("Email or password incorrect", response));
        }

        [HttpGet("validateCode")]
        public async Task<IActionResult> ValidateCode(ValidateCodeDTO validationCodeDto) 
        {
            DeviceDTO device = RetrieveDevice();
            if (validationCodeDto.Email == null || validationCodeDto.Code == null || device.UUID == null)
                return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR));

            var result = await _authenticationService.GetAuthCode(validationCodeDto.Email);
            if (result == null) 
            {
                return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR));
            }

            if (!result.Code.Equals(validationCodeDto.Code))
            {
                return Ok(GenerateErrorResponse("The code provided did not match."));
            }

            var deletionCode = await _authenticationService.DeleteUserAuthCode(validationCodeDto.Email);
            if (deletionCode != null)
                return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR));

            // Update user's verified status
            var response = await _authenticationService.ConfirmUser(validationCodeDto, device);
            return !response.Error ? Ok(GenerateSuccessResponse("Code Validated", response)): Ok(GenerateErrorResponse("Unable to validate using that code", response));
        }

        [HttpGet("resendCode/{email}")]
        public async Task<IActionResult> ResendCode(string email) 
        {
            if (email == null)
                return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR, null));

            var result = await _authenticationService.ResendValidationCode(email);
            return result ? Ok(GenerateSuccessResponse("Successful Code Resend")): BadRequest(GenerateErrorResponse("Unable to resend reset code. Please try again"));
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            refreshTokenDTO.IpAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();
            string deviceUUID = RetrieveDevice().UUID;
            if (deviceUUID == null || deviceUUID.Equals(""))
            {
                return BadRequest("DEVICE NOT FOUND");
            }

            var response = await _authenticationService.RefreshToken(refreshTokenDTO, deviceUUID);
            return !response.Error ? Ok(GenerateSuccessResponse(null, response)): Ok(GenerateErrorResponse(AppConstants.GENERIC_ERROR, response));
        }
    
        [HttpGet("sendEmailForReset/{email}")]
        public async Task<IActionResult> EmailForReset(string email)
        {
            var response = await _authenticationService.EmailForReset(email);
            return response.IsUser ? Ok(GenerateSuccessResponse("Email successfully sent", response)): Ok(GenerateErrorResponse("Unable to reset with that email", response));
        }

        [HttpPost("sendPasswordForReset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO request)
        {
            var response = await _authenticationService.ResetPassword(request.UserId, request.Password);
            return !response.Error ? Ok(GenerateSuccessResponse("Password successfully reset", response)): Ok(GenerateErrorResponse(AppConstants.GENERIC_ERROR, response));
        }

        [Authorize]
        [HttpGet("logout/{userId}")]
        public async Task<IActionResult> LogoutUser(Guid userId) 
        {
            var refreshToken = (await _authenticationService.GetUserRefreshTokens(userId))[0];
            var response = await _authenticationService.Logout(RetrieveToken(), refreshToken.Token);

            return response ?  Ok(GenerateSuccessResponse("You have logged out")): Ok(GenerateErrorResponse("Unable to log you out"));
        }
    }
}
