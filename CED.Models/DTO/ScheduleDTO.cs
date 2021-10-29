using System;
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
    public class ScheduleDTO
    {
        [JsonProperty("scheduleType")]
        public int ScheduleType { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("scheduleTime")]
        public DateTime ScheduleTime { get; set; }
    }
}
