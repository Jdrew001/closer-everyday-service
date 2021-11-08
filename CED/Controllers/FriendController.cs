using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CED.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CED.Controllers
{
    [Authorize]
    [Route("api/friend")]
    [ApiController]
    public class FriendController
    {
        private readonly IFriendService _friendService;
        private readonly IMapper _mapper;
        private readonly ILogger<FriendController> _logger;

        public FriendController(
            IFriendService friendService,
            IMapper mapper,
            ILogger<FriendController> logger)
        {
            _friendService = friendService;
            _mapper = mapper;
            _logger = logger;
        }

        /*
         * Need three methods
         * 1. Remove friend by id (userId, friendId)
         * 2. Add Friend by id
         * 3. GetUserFriends
         *
         */
    }
}
