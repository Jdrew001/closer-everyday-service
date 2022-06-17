﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Models.Core;

namespace CED.Models.DTO
{
    public class HabitDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Icon { get; set; }
        public bool Reminder { get; set; }
        public DateTime ReminderAt { get; set; }
        public bool VisibleToFriends { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public HabitType HabitType { get; set; }
        public Schedule Schedule { get; set; } // TODO: DTOs!!
        public Guid UserId { get; set; }
        public Frequency Frequency { get; set; }
        public List<FriendHabit> friendHabits { get; set; }
        public HabitLog habitLog { get; set; }
    }
}
