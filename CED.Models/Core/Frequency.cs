using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.Core
{
    public class Frequency
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public FrequencyType FrequencyType { get; set; }

        public List<Day> Days { get; set; }
    }
}
