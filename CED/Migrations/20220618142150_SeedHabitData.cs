using FluentMigrator;
using System;

namespace CED
{
  [Migration(20220618142150)]
  public class SeedHabitData : Migration
  {
    public override void Up()
    {
      var scheduleId = Guid.NewGuid().ToString();
      var habitId = Guid.NewGuid().ToString();

      Insert.IntoTable("schedule")
        .Row(new { idschedule = scheduleId, schedule_type_id = 1, user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", schedule_time = DateTime.Parse("2022-10-12T08:00:00") });

      Insert.IntoTable("habit")
      .Row(new
      {
        idhabit = habitId,
        name = "Prayer with God2.0",
        reminder = false,
        visibleToFriends = true,
        description = "Hoping to get better as a person and Christian",
        status = 'A',
        userId = "770c5bee-d9e1-11ec-9672-f23c92435ec3",
        scheduleId = scheduleId,
        habitTypeId = 1,
        createdAt = new DateTime(),
        active_ind = 'A'
      });

      #region -4 Week

      //Sunday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-15T08:00:00") });

      //Monday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-16T08:00:00") });

      //Tuesday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-17T08:00:00") });

      //Wed
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-18T08:00:00") });

      //Thurs
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-19T08:00:00") });

      //Friday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-20T08:00:00") });

      //Saturday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-21T08:00:00") });

      #endregion

      #region -3 Week

      //Sunday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-22T08:00:00") });

      //Monday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-23T08:00:00") });

      //Tuesday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-24T08:00:00") });

      //Wed
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-25T08:00:00") });

      //Thurs
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-26T08:00:00") });

      //Friday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-27T08:00:00") });

      //Saturday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-28T08:00:00") });

      #endregion

      #region -2 Week

      //Sunday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-29T08:00:00") });

      //Monday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-30T08:00:00") });

      //Tuesday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-05-31T08:00:00") });

      //Wed
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-01T08:00:00") });

      //Thurs
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-02T08:00:00") });

      //Friday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-03T08:00:00") });

      //Saturday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-04T08:00:00") });

      #endregion

      #region -1 Week

      //Sunday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-05T08:00:00") });

      //Monday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-06T08:00:00") });

      //Tuesday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-07T08:00:00") });

      //Wed
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-08T08:00:00") });

      //Thurs
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-09T08:00:00") });

      //Friday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-10T08:00:00") });

      //Saturday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-11T08:00:00") });

      #endregion

      #region Current Week

      //Sunday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-12T08:00:00") });

      //Monday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-13T08:00:00") });

      //Tuesday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-14T08:00:00") });

      //Wed
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-15T08:00:00") });

      //Thurs
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-16T08:00:00") });

      //Friday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-17T08:00:00") });

      //Saturday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-18T08:00:00") });

      #endregion

      #region +1 Week

      //Sunday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-19T08:00:00") });

      //Monday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-20T08:00:00") });

      //Tuesday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-21T08:00:00") });

      //Wed
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-22T08:00:00") });

      //Thurs
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-23T08:00:00") });

      //Friday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-24T08:00:00") });

      //Saturday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-25T08:00:00") });

      #endregion

      #region +2 Week

      //Sunday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-26T08:00:00") });

      //Monday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-27T08:00:00") });

      //Tuesday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-28T08:00:00") });

      //Wed
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-29T08:00:00") });

      //Thurs
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-06-30T08:00:00") });

      //Friday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-01T08:00:00") });

      //Saturday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-02T08:00:00") });

      #endregion

      #region +3 Week

      //Sunday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-03T08:00:00") });

      //Monday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-04T08:00:00") });

      //Tuesday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-05T08:00:00") });

      //Wed
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-06T08:00:00") });

      //Thurs
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-07T08:00:00") });

      //Friday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-08T08:00:00") });

      //Saturday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-09T08:00:00") });

      #endregion

      #region +4 Week

      //Sunday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-10T08:00:00") });

      //Monday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-11T08:00:00") });

      //Tuesday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-12T08:00:00") });

      //Wed
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-13T08:00:00") });

      //Thurs
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-14T08:00:00") });

      //Friday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-15T08:00:00") });

      //Saturday
      Insert.IntoTable("habit_log")
        .Row(new { idhabit_log = Guid.NewGuid().ToString(), log_value = 'C', user_id = "770c5bee-d9e1-11ec-9672-f23c92435ec3", habit_id = habitId, created_at = DateTime.Parse("2022-07-16T08:00:00") });

      #endregion
    }

    public override void Down()
    {

    }
  }
}
