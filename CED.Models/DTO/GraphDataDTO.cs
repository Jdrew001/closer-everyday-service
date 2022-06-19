namespace CED.Models.DTO
{
  public class GraphDataDTO
  {
    public GraphDataDTO() { }

    public GraphDataDTO(string Key, int Value, string Date)
    {
      this.Key = Key;
      this.Value = Value;
      this.Date = Date;
    }
    public string Key { get; set; }

    public int Value { get; set; }

    public string Date { get; set; }
  }
}
