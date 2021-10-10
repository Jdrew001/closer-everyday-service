using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.Core
{
    public class Habit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Icon { get; set; }
        public bool Reminder { get; set; }
        public DateTime ReminderAt { get; set; }
        public bool VisibleToFriends { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public HabitType HabitType { get; set; }
        public Schedule Schedule { get; set; }
        public int UserId { get; set; }
        public List<Frequency> Frequencies { get; set; }
    }
}
