using CED.Models.DTO;
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
            return Ok(response);
        }

        [HttpGet("validateCode/{code}/{email}/{deviceUUID}/{forReset}")]
        public async Task<IActionResult> ValidateCode(string code, string email, string deviceUUID, bool forReset) 
        {
            if (email == null || code == null || deviceUUID == null)
                return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR));

            var result = await _authenticationService.GetAuthCode(email);
            if (result == null) 
            {
                return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR));
            }

            if (!result.Code.Equals(code))
            {
                return Ok(GenerateErrorResponse("The code provided did not match."));
            }

            var deletionCode = await _authenticationService.DeleteUserAuthCode(email);
            if (deletionCode != null)
                return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR));

            // Update user's verified status
            var response = await _authenticationService.ConfirmUser(email, deviceUUID, forReset);
            return Ok(response);
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
            return Ok(response);
        }
    
        [HttpGet("sendEmailForReset/{email}")]
        public async Task<IActionResult> EmailForReset(string email)
        {
            var response = await _authenticationService.EmailForReset(email);
            return Ok(response);
        }

        [HttpPost("sendPasswordForReset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO request)
        {
            var response = await _authenticationService.ResetPassword(request.UserId, request.Password);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("logout/{userId}")]
        public async Task<IActionResult> LogoutUser(Guid userId) 
        {
            var refreshToken = (await _authenticationService.GetUserRefreshTokens(userId))[0];
            var response = await _authenticationService.Logout(RetrieveToken(), refreshToken.Token);

            return response ?  Ok(GenerateSuccessResponse("You have logged out")): Ok(GenerateErrorResponse(AppConstants.GENERIC_ERROR));
        }
    }
}
