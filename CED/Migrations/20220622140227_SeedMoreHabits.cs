using FluentMigrator;
using System;

namespace CED
{
  [Migration(20220622140227)]
  public class SeedMoreHabits : Migration
  {
    public override void Up()
    {
      var scheduleId1 = Guid.NewGuid().ToString();
      var scheduleId2 = Guid.NewGuid().ToString();
      var habitId1 = Guid.NewGuid().ToString();
      var habitId2 = Guid.NewGuid().ToString();

      Insert.IntoTable("schedule")
        .Row(new { idschedule = scheduleId1, schedule_type_id = 1, user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", schedule_time = DateTime.Parse("2022-10-12T08:00:00") });
      Insert.IntoTable("schedule")
        .Row(new { idschedule = scheduleId2, schedule_type_id = 1, user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", schedule_time = DateTime.Parse("2022-10-12T08:00:00") });

      Insert.IntoTable("habit")
      .Row(new
      {
        idhabit = habitId1,
        name = "Prayer with God3.0",
        reminder = false,
        visibleToFriends = true,
        description = "Hoping to get better as a person and Christian",
        status = 'A',
        userId = "770c5bee-d9e1-11ec-9672-f23c92435ec3",
        scheduleId = scheduleId1,
        habitTypeId = 1,
        createdAt = new DateTime(),
        active_ind = 'A'
      });

      Insert.IntoTable("habit")
      .Row(new
      {
        idhabit = habitId2,
        name = "Prayer with God3.0",
        reminder = false,
        visibleToFriends = true,
        description = "Hoping to get better as a person and Christian",
        status = 'A',
        userId = "770c5bee-d9e1-11ec-9672-f23c92435ec3",
        scheduleId = scheduleId2,
        habitTypeId = 1,
        createdAt = new DateTime(),
        active_ind = 'A'
      });

      //Sunday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId1, created_at = DateTime.Parse("2022-07-03T08:00:00") });

      //Monday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'P', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId1, created_at = DateTime.Parse("2022-07-04T08:00:00") });

      //Tuesday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId1, created_at = DateTime.Parse("2022-07-05T08:00:00") });

      //Wed
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId1, created_at = DateTime.Parse("2022-07-06T08:00:00") });

      //Thurs
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'P', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId1, created_at = DateTime.Parse("2022-07-07T08:00:00") });

      //Friday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId1, created_at = DateTime.Parse("2022-07-08T08:00:00") });

      //Saturday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId1, created_at = DateTime.Parse("2022-07-09T08:00:00") });
      //----------------------------------------------------------
      //Sunday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId2, created_at = DateTime.Parse("2022-07-03T08:00:00") });

      //Monday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'P', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId2, created_at = DateTime.Parse("2022-07-04T08:00:00") });

      //Tuesday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId2, created_at = DateTime.Parse("2022-07-05T08:00:00") });

      //Wed
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId2, created_at = DateTime.Parse("2022-07-06T08:00:00") });

      //Thurs
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'P', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId2, created_at = DateTime.Parse("2022-07-07T08:00:00") });

      //Friday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId2, created_at = DateTime.Parse("2022-07-08T08:00:00") });

      //Saturday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId2, created_at = DateTime.Parse("2022-07-09T08:00:00") });
    }

    public override void Down()
    {

    }
  }
}
