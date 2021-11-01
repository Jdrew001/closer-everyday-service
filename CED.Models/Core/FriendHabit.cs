using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.Core
{
    public class FriendHabit
    {
        public Guid Id { get; set; }
        public Guid HabitId { get; set; }
        public Guid FriendId { get; set; }
        public string FriendFirstName { get; set; }
        public string FriendLastName { get; set; }
        public string FriendEmail { get; set; }
        public Guid OwnerId { get; set; }
    }
}
