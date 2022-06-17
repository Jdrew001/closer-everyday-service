using FluentMigrator;

namespace CED
{
  [Migration(20220617123547)]
  public class ReferenceDataInsert : Migration
  {
    public override void Up()
    {
      Insert.IntoTable("habit_type")
        .Row(new { habitTypeId = 1, habitTypeValue = "Regular", description = "These habits you are trying to form!" })
        .Row(new { habitTypeId = 2, habitTypeValue = "Negative", description = "These habits you are trying to break!" });

      Insert.IntoTable("schedule_type")
        .Row(new { idschedule_type = 1, schedule_value = "Morning" })
        .Row(new { idschedule_type = 2, schedule_value = "Afternoon" })
        .Row(new { idschedule_type = 3, schedule_value = "Evening" })
        .Row(new { idschedule_type = 4, schedule_value = "Anytime" });
    }

    public override void Down()
    {

    }
  }
}
