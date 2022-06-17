using System;
using System.Collections.Generic;

namespace CED.Models.Core
{
  public class Frequency
  {
    public Guid Id { get; set; }
    public string Value { get; set; }

    public FrequencyType FrequencyType { get; set; }

    public List<Day> Days { get; set; }
  }
}
