using CED.Models.DTO;
using CED.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CED.Models.Core;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHabit(int id)
        {
            var habit = await habitService.GetHabitById(id);
            if (habit == null)
            {
                return BadRequest(GenerateErrorResponse("ERROR: The habit selected doesn't exist", null));
            }

            return Ok(GenerateSuccessResponse(null, habit));
        }

        [HttpPost("updateHabitLog")]
        public async Task<IActionResult> UpdateHabitLog(HabitLogUpdateRequestDTO logUpdateDTO)
        {
            var reqToken = RetrieveToken();
            if (string.IsNullOrEmpty(reqToken))
            {
                return Unauthorized(GenerateErrorResponse("Unable to Process Request. Please notify support.", null));
            }

            var token = await tokenService.ReadJwtToken(RetrieveToken());
            var userId = Int32.Parse(token.Claims.First(x => x.Type == "uid").Value);
            var status = char.ToUpper(logUpdateDTO.status);
            var result = await habitService.SaveHabitLog(status, userId, logUpdateDTO.habitId);

            return result == null ? BadRequest(GenerateErrorResponse("Unable to update Habit Log", null)) :
                Ok(GenerateSuccessResponse(null, mapHabitLogDto(result)));
        }

        private UpdateHabitLogDTO mapHabitLogDto(HabitLog log)
        {
            return new UpdateHabitLogDTO()
            {
                status = log.Value,
                habitId = log.HabitId,
                createdAt = log.CreatedAt
            };
        }
        
    }
}
