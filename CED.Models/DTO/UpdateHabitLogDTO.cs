using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class UpdateHabitLogDTO
    {
        public char status { get; set; }
        public int habitId { get; set; }
        public DateTime createdAt { get; set; }
    }
}
