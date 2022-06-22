using FluentMigrator;

namespace CED
{
  [Migration(20220622133514)]
  public class GetHabitsByLogDate : Migration
  {
    public override void Up()
    {
      Execute.Script(@"Scripts/20220622133514_GetHabitsByLogDate.sql");
    }

    public override void Down()
    {
    }
  }
}
