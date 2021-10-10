using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Controllers
{
    [Authorize]
    [Route("api/reference")]
    [ApiController]
    public class ReferenceController : CEDBaseController
    {
        public ReferenceController()
        {

        }

        [HttpGet("getHabitReferenceData")]
        public async Task<IActionResult> GetAllHabitReferenceData()
        {
            // TODO: Call service
            return Ok();
        }
    }
}
