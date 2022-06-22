using System;

namespace CED.Models.DTO
{
  public class HabitByLogDateResponse
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Reminder { get; set; }
    public DateTime ReminderAt { get; set; }
    public string HabitType { get; set; }
    public string ScheduleType { get; set; }
    public char HabitLogStatus { get; set; }
  }
}
