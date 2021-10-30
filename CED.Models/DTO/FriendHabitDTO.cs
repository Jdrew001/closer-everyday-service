﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CED.Models.Core;
using Newtonsoft.Json;

namespace CED.Models.DTO
{
    public class FriendHabitDTO
    {
        [JsonProperty("friendId")]
        public int FriendId { get; set; }
        
        [JsonProperty("ownerId")]
        public int OwnerId { get; set; }
    }
}