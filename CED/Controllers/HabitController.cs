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
    [Authorize]
    [Route("api/habit")]
    [ApiController]
    public class HabitController : CEDBaseController
    {
        private readonly IHabitService habitService;
        private readonly ITokenService tokenService;
        public HabitController(
            IHabitService habitService,
            ITokenService tokenService)
        {
            this.habitService = habitService;
            this.tokenService = tokenService;
        }

        [HttpGet("getAllUserHabits")]
        public async Task<IActionResult> GetAllUserHabits()
        {
            var reqToken = RetrieveToken();
            if (string.IsNullOrEmpty(reqToken))
            {
                return Unauthorized(GenerateErrorResponse("Unable to Process Request. Please notify support.", null));
            }

            var token = await tokenService.ReadJwtToken(RetrieveToken());
            var userId = Int32.Parse(token.Claims.First(x => x.Type == "uid").Value);
            return Ok(GenerateSuccessResponse("Success", await habitService.GetAllUserHabits(userId)));
        }
        
    }
}
