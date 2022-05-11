﻿using System;
using CED.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using CED.Services.Interfaces;
using Newtonsoft.Json;

namespace CED.Controllers
{
    public class CEDBaseController : ControllerBase
    {
        protected ITokenService _tokenService;
        public CEDBaseController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        protected string RetrieveToken()
        {
            return HttpContext?.Request?.Headers?.FirstOrDefault(a => a.Key == "Authorization")
                .Value.FirstOrDefault().Remove(0, 7);
        }

        protected DeviceDTO RetrieveDevice()
        {
            var deviceString = HttpContext?.Request?.Headers?.FirstOrDefault(a => a.Key == "Device").Value;
            return JsonConvert.DeserializeObject<DeviceDTO>(deviceString);
        }

        protected async Task<Guid> GetUserId()
        {
            var reqToken = RetrieveToken();
            if (string.IsNullOrEmpty(reqToken))
                return Guid.Empty;

            var token = await _tokenService.ReadJwtToken(RetrieveToken());
            if (token == null)
                return Guid.Empty;

            return Guid.Parse(token.Claims.First(x => x.Type == "uid").Value);
        }

        protected GenericResponseDTO GenerateErrorResponse(string message, object data = null)
        {
            return new GenericResponseDTO()
            {
                message = message,
                status = "FAILURE",
                data = data,
                Error = true
            };
        }

        protected GenericResponseDTO GenerateSuccessResponse(string message, object data = null)
        {
            return new GenericResponseDTO()
            {
                message = message,
                status = "SUCCESS",
                data = data,
                Error = false
            };
        }
    }
}
