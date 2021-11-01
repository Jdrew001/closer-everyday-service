using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class HabitLogUpdateRequestDTO
    {
        public char status { get; set; }
        public Guid habitId { get; set; }
        public DateTime date { get; set; }
    }
}
