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
        private readonly IHabitService _habitService;
        private readonly IHabitStatService _habitStatService;
        public HabitController(
            IHabitService habitService,
            IHabitStatService habitStatService,
            ITokenService tokenService)
            :base(tokenService)
        {
            this._habitService = habitService;
            this._habitStatService = habitStatService;
        }

        [HttpGet("getAllUserHabits/{date}")]
        public async Task<IActionResult> GetAllUserHabits(DateTime date)
        {

            var userId = await GetUserId();
            if (userId == -1)
            {
                return Unauthorized(GenerateErrorResponse("Unable to Process Request. Please notify support.", null));
            }
            return Ok(GenerateSuccessResponse("Success", await _habitService.GetAllUserHabits(userId, date.ToString("yyyy-MM-dd H:mm:ss"))));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHabit(int id)
        {
            var habit = await _habitService.GetHabitById(id);
            if (habit == null)
            {
                return BadRequest(GenerateErrorResponse("ERROR: The habit selected doesn't exist", null));
            }

            return Ok(GenerateSuccessResponse(null, habit));
        }

        public async Task<IActionResult> SaveHabit(Habit habit)
        {
            var userId = await GetUserId();
            if (userId == -1)
            {
                return Unauthorized(GenerateErrorResponse("Unable to Process Request. Please notify support.", null));
            }

            var result = await _habitService.SaveHabit(habit);
            return Ok(GenerateSuccessResponse("Success", habit));
        }

        [HttpPost("updateHabitLog")]
        public async Task<IActionResult> UpdateHabitLog(HabitLogUpdateRequestDTO logUpdateDto)
        {
            var userId = await GetUserId();
            if (userId == -1)
            {
                return Unauthorized(GenerateErrorResponse("Unable to Process Request. Please notify support.", null));
            }

            // if the date past in from dto is greater than today, throw error
            if (logUpdateDto.date.Date > DateTime.UtcNow.Date)
                return BadRequest(GenerateErrorResponse("Date must be past or present", null));

            var status = char.ToUpper(logUpdateDto.status);
            var result = await _habitService.SaveHabitLog(status, userId, logUpdateDto.habitId, logUpdateDto.date.ToString("yyyy-MM-dd H:mm:ss"));

            return result == null ? BadRequest(GenerateErrorResponse("Unable to update Habit Log", null)) :
                Ok(GenerateSuccessResponse(null, MapHabitLogDto(result)));
        }

        [HttpGet("updateHabitLogsForDate/{date}")]
        public async Task<IActionResult> UpdateHabitLogsForDate(DateTime date)
        {
            var userId = await GetUserId();
            if (userId == -1)
            {
                return Unauthorized(GenerateErrorResponse("Unable to Process Request. Please notify support.", null));
            }

            var habits = await _habitService.GetAllUserHabits(userId, date.ToString("yyyy-MM-dd H:mm:ss"));
            if (habits != null)
            {
                // Get all ids for habits where habit log is null
                var habitIds = habits.FindAll(o => o.habitLog == null).Select(o => o.Id).ToArray();
                foreach (var id in habitIds)
                {
                    var result = await _habitService.SaveHabitLog(char.Parse("P"), userId, id, date.ToString("yyyy-MM-dd H:mm:ss"));
                    if (result == null)
                        return BadRequest(GenerateErrorResponse("Error: Unable to process request", null));
                }
            }

            return Ok(GenerateSuccessResponse("Successfully update logs", null));
        }

        [HttpGet("getGlobalStats/{year}")]
        public async Task<IActionResult> GetGlobalHabitStats(int year)
        {
            var userId = await GetUserId();
            if (userId == -1)
            {
                return Unauthorized(GenerateErrorResponse("Unable to Process Request. Please notify support.", null));
            }

            var habitStats = _habitStatService.GetGlobalHabitStats(userId, year);

            return Ok(GenerateSuccessResponse("Success", habitStats));
        }

        [HttpGet("getHabitStats/{habitId}/{year}")]
        public async Task<IActionResult> GetHabitStats(int habitId, int year)
        {
            var habitStats = _habitStatService.GetHabitStats(habitId, year);
            return Ok(GenerateSuccessResponse("Success", habitStats));
        }

        private UpdateHabitLogDTO MapHabitLogDto(HabitLog log)
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
