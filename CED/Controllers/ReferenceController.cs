using CED.Models.DTO;
using CED.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CED.Controllers
{
    [Authorize]
    [Route("api/reference")]
    [ApiController]
    public class ReferenceController : CEDBaseController
    {
        private readonly IReferenceService _referenceService;
        public ReferenceController(
            IReferenceService referenceService)
        {
            _referenceService = referenceService;
        }

        [HttpGet("getHabitReferenceData")]
        public async Task<IActionResult> GetAllHabitReferenceData()
        {
            var habitTypes = await _referenceService.GetHabitTypes();
            var scheduleTypes = await _referenceService.GetScheduleTypes();
            var referenceData = new ReferenceDTO()
            {
                habitTypes = habitTypes,
                scheduleTypes = scheduleTypes
            };
            return Ok(GenerateSuccessResponse(null, referenceData));
        }
    }
}
