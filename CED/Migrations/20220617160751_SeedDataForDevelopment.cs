using FluentMigrator;
using System;

namespace CED
{
  [Migration(202206171607510)]
  [Profile("Development")]
  public class SeedDataForDevelopment : Migration
  {
    public override void Up()
    {
      var scheduleId = Guid.NewGuid().ToString();
      var habitId = Guid.NewGuid().ToString();
      var frequencyId = Guid.NewGuid().ToString();

      Insert.IntoTable("schedule")
        .Row(new { idschedule = scheduleId, schedule_type_id = 1, user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", schedule_time = DateTime.Parse("2022-10-12T08:00:00") });


      Alter.Table("habit").AlterColumn("reminderAt").AsDateTime().Nullable();

      Insert.IntoTable("habit")
        .Row(new
        {
          idhabit = habitId,
          name = "Prayer with God",
          reminder = false,
          visibleToFriends = false,
          description = "Hoping to get better as a person and Christian",
          status = 'A',
          userId = "770c5bee-d9e1-11ec-9672-f23c92435ec3",
          scheduleId = scheduleId,
          habitTypeId = 1,
          createdAt = new DateTime(),
          active_ind = 'A'
        });

      Insert.IntoTable("frequency")
        .Row(new { idfrequency = frequencyId, freqTypeId = 1 });

      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = new DateTime() });
    }

    public override void Down()
    {

    }
  }
}
