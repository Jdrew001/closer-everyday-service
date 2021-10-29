using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.Core
{
    public class FriendHabit
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        public int FriendId { get; set; }
        public string FriendFirstName { get; set; }
        public string FriendLastName { get; set; }
        public string FriendEmail { get; set; }
        public int OwnerId { get; set; }
    }
}
