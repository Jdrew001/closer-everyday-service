using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CED.Models.Core
{
    public class HabitLog
    {
        public int Id { get; set; }
        [JsonIgnore]
        public char Value { get; set; }
        public int UserId { get; set; }
        public int HabitId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
