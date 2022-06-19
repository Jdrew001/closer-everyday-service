using System.Collections.Generic;

namespace CED.Models.DTO
{
  public class DashboardGraphDTO
  {
    public List<GraphDataResponseDTO> Data { get; set; }
    public List<string> Keys => new List<string>() { "Sun", "Mon", "Tues", "Wed", "Thurs", "Fri", "Sat" };
    public bool Animation { get; set; }
    public int Total { get; set; }
  }
}
