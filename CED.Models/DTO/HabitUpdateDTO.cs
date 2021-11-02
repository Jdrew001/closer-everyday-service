using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CED.Models.DTO
{
    public class HabitUpdateDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public byte[] Icon { get; set; }

        [JsonProperty("reminder")]
        public bool Reminder { get; set; }

        [JsonProperty("reminderAt")]
        public DateTime ReminderAt { get; set; }

        [JsonProperty("visibleToFriends")]
        public bool VisibleToFriends { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("habitType")]
        public int HabitType { get; set; }

        [JsonProperty("schedule")]
        public ScheduleDTO Schedule { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("frequencies")]
        public List<FrequencyDTO> Frequencies { get; set; }

        [JsonProperty("friendHabits")]
        public List<FriendHabitDTO> FriendHabits { get; set; }
    }
}
