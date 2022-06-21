using System.Collections.Generic;

namespace CED.Models.DTO
{
  public class DashboardGraphDTO
  {
    public string Title { get; set; }
    public List<GraphDataResponseDTO> Data { get; set; }
    public bool Animation { get; set; }
    public int Total { get; set; }
  }
}
