using CED.Models.DTO;
using CED.Services.Interfaces;
using CED.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
    private readonly ILogger<AuthenticationController> _log;

    public AuthenticationController(
        IAuthenticationService authenticationService,
        ITokenService tokenService,
        ILogger<AuthenticationController> log)
        : base(tokenService)
    {
      _authenticationService = authenticationService;
      _log = log;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationDTO request, [FromHeader] DeviceDTO device)
    {
      request.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
      var info = HttpContext?.Request?.Headers?.FirstOrDefault(a => a.Key == "Device");
      var stringInfo = info.Value;
      _log.LogInformation("AuthenticationController: Info on device (Register) : Device info non formatted {info}, Device: {stringInfo}", info, stringInfo);


      var response = await _authenticationService.Register(request, device);
      return response.IsUserCreated ? Ok(GenerateSuccessResponse("Successfully registered account", response)) : Ok(GenerateErrorResponse(AppConstants.GENERIC_ERROR, response));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDTO request, [FromHeader] DeviceDTO device)
    {
      request.IpAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();
      string deviceUUID = device.UUID;

      if (deviceUUID == null || deviceUUID.Equals(""))
      {
        return BadRequest("DEVICE NOT FOUND");
      }

      var response = await _authenticationService.Login(request, deviceUUID);
      return !response.Error ? Ok(GenerateSuccessResponse("Successfully logged in", response)) : Ok(GenerateErrorResponse("Email or password incorrect", response));
    }

    [HttpPost("validateCode")]
    public async Task<IActionResult> ValidateCode(ValidateCodeDTO validationCodeDto, [FromHeader] DeviceDTO device)
    {
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

      // Update user's verified status
      var response = await _authenticationService.ConfirmUser(validationCodeDto, device);
      if (response.Error)
        return Ok(GenerateErrorResponse("Unable to validate using that code", response));

      var deletionCode = await _authenticationService.DeleteUserAuthCode(validationCodeDto.Email);
      if (deletionCode != null)
        return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR));

      return Ok(GenerateSuccessResponse("Code Validated", response));
    }

    [HttpGet("resendCode/{email}")]
    public async Task<IActionResult> ResendCode(string email)
    {
      if (email == null)
        return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR, null));

      var result = await _authenticationService.ResendValidationCode(email);
      return result ? Ok(GenerateSuccessResponse("Successful Code Resend")) : BadRequest(GenerateErrorResponse("Unable to resend reset code. Please try again"));
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken(RefreshTokenDTO refreshTokenDTO, [FromHeader] DeviceDTO device)
    {
      refreshTokenDTO.IpAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();
      string deviceUUID = device.UUID;
      if (deviceUUID == null || deviceUUID.Equals(""))
      {
        return BadRequest("DEVICE NOT FOUND");
      }

      var response = await _authenticationService.RefreshToken(refreshTokenDTO, deviceUUID);
      return !response.Error ? Ok(GenerateSuccessResponse(null, response)) : Ok(GenerateErrorResponse(AppConstants.GENERIC_ERROR, response));
    }

    [HttpGet("sendEmailForReset/{email}")]
    public async Task<IActionResult> EmailForReset(string email)
    {
      var response = await _authenticationService.EmailForReset(email);
      return response.IsUser ? Ok(GenerateSuccessResponse("Email successfully sent", response)) : Ok(GenerateErrorResponse("Unable to reset with that email", response));
    }

    [HttpPost("sendPasswordForReset")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDTO request)
    {
      var response = await _authenticationService.ResetPassword(request.UserId, request.Password);
      return !response.Error ? Ok(GenerateSuccessResponse("Password successfully reset", response)) : Ok(GenerateErrorResponse(AppConstants.GENERIC_ERROR, response));
    }

    [Authorize]
    [HttpGet("logout/{userId}")]
    public async Task<IActionResult> LogoutUser(Guid userId)
    {
      var refreshToken = (await _authenticationService.GetUserRefreshTokens(userId))[0];
      var response = await _authenticationService.Logout(RetrieveToken(), refreshToken.Token);

      return response ? Ok(GenerateSuccessResponse("You have logged out")) : Ok(GenerateErrorResponse("Unable to log you out"));
    }
  }
}
