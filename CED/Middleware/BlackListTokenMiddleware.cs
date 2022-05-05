using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CED.Models.Core;
using CED.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CED.Middleware
{
    public static class BlackListTokenMiddlewareExtension
    {
        public static void UseBlackListTokenMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<BlackListTokenMiddleware>();
        }
    }

    public class BlackListTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BlackListTokenMiddleware> _logger;
        private readonly ITokenService _tokenService;

        public BlackListTokenMiddleware(
            RequestDelegate next,
            ILogger<BlackListTokenMiddleware> logger,
            ITokenService tokenService)
        {
            _next = next;
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("BlackListTokenMiddleware: start Invoke method for blacklist token");
            if (await RetrieveBlacklistToken(context))
            {
                _logger.LogInformation("BlackListTokenMiddleware: start Invoke method for blacklist token");
                await _next.Invoke(context);
            }
        }

        private async Task<bool> RetrieveBlacklistToken(HttpContext context)
        {
            var result = JsonConvert.SerializeObject(new
            {
                code = (int)HttpStatusCode.InternalServerError,
                message = "You are not Authorized to make that request",
                status = "FAILURE",
                error = true
            });

            var isAuthorized = context.GetEndpoint().Metadata?.GetMetadata<IAuthorizeData>() is object;
            if (!isAuthorized)
                return true;

            var token = RetrieveToken(context);
            if (token == null)
            {
                await context.Response.WriteAsync(result);   
                return false;             
            }

            BlackListToken blackListToken = await _tokenService.FetchBlacklistedToken(token);
            if (blackListToken != null)
            {
                await context.Response.WriteAsync(result); 
                return false;
            }

            return true;
        }

        private string RetrieveToken(HttpContext context)
        {
            return context?.Request?.Headers?.FirstOrDefault(a => a.Key == "Authorization")
                .Value.FirstOrDefault().Remove(0, 7);
        }
    }
}