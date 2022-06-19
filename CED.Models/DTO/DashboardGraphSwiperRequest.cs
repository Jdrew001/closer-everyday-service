namespace CED.Models.DTO
{
  public class DashboardGraphSwiperRequest
  {
    public int Limit { get; set; }
    public string BoundaryStartDate { get; set; }
    public string BoundaryEndDate { get; set; }
    public string Boundary { get; set; }
  }
}
