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

        [HttpGet("getAllUserHabits/{date}")]
        public async Task<IActionResult> GetAllUserHabits(DateTime date)
        {
            var reqToken = RetrieveToken();
            if (string.IsNullOrEmpty(reqToken))
            {
                return Unauthorized(GenerateErrorResponse("Unable to Process Request. Please notify support.", null));
            }

            var token = await tokenService.ReadJwtToken(RetrieveToken());
            var userId = Int32.Parse(token.Claims.First(x => x.Type == "uid").Value);
            return Ok(GenerateSuccessResponse("Success", await habitService.GetAllUserHabits(userId, date.ToString("yyyy-MM-dd H:mm:ss"))));
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

            // if the date past in from dto is greater than today, throw error
            if (logUpdateDTO.date.Date > DateTime.UtcNow.Date)
                return BadRequest(GenerateErrorResponse("Date must be past or present", null));

            var token = await tokenService.ReadJwtToken(RetrieveToken());
            var userId = Int32.Parse(token.Claims.First(x => x.Type == "uid").Value);
            var status = char.ToUpper(logUpdateDTO.status);
            var result = await habitService.SaveHabitLog(status, userId, logUpdateDTO.habitId, logUpdateDTO.date.ToString("yyyy-MM-dd H:mm:ss"));

            return result == null ? BadRequest(GenerateErrorResponse("Unable to update Habit Log", null)) :
                Ok(GenerateSuccessResponse(null, mapHabitLogDto(result)));
        }

        [HttpGet("updateHabitLogsForDate/{date}")]
        public async Task<IActionResult> UpdateHabitLogsForDate(DateTime date)
        {
            var reqToken = RetrieveToken();
            if (string.IsNullOrEmpty(reqToken))
            {
                return Unauthorized(GenerateErrorResponse("Unable to Process Request. Please notify support.", null));
            }

            var token = await tokenService.ReadJwtToken(RetrieveToken());
            var userId = Int32.Parse(token.Claims.First(x => x.Type == "uid").Value);
            var habits = await habitService.GetAllUserHabits(userId, date.ToString("yyyy-MM-dd H:mm:ss"));
            if (habits != null)
            {
                // Get all ids for habits where habit log is null
                var habitIds = habits.FindAll(o => o.habitLog == null).Select(o => o.Id).ToArray();
                foreach (var id in habitIds)
                {
                    var result = await habitService.SaveHabitLog(char.Parse("P"), userId, id, date.ToString("yyyy-MM-dd H:mm:ss"));
                    if (result == null)
                        return BadRequest(GenerateErrorResponse("Error: Unable to process request", null));
                }
            }

            return Ok(GenerateSuccessResponse("Successfully update logs", null));
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
