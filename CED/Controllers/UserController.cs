using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CED.Models;
using CED.Models.DTO;
using CED.Services.Interfaces;
using CED.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CED.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CEDBaseController
    {
        private IMapper _mapper;
        private IUserService _userService;
        private ILogger<UserController> _logger;

        public UserController(
            ITokenService tokenService,
            IMapper mapper,
            IUserService userService,
            ILogger<UserController> logger) : base(tokenService)
        {
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("searchForUser/{param}")]
        public async Task<IActionResult> SearchForUser(string param)
        {
            IList<UserDTO> userDTO = null;
            try
            {
                var user = await _userService.SearchForUser(param);
                userDTO = _mapper.Map<List<User>, List<UserDTO>>(user);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Exception inside search for user - param: {param}", param);
                return BadRequest(GenerateErrorResponse(AppConstants.GENERIC_ERROR, null));
            }

            return Ok(GenerateSuccessResponse("Success", userDTO));
        }
    }
}
