using CED.Data;
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
      #region Day Migration
      Create.Table("day")
         .WithColumn("dayid").AsInt32().PrimaryKey().NotNullable()
         .WithColumn("value").AsCustom($"varchar(255) {DataConstants.COLLATION_TYPE}").NotNullable()
         .WithColumn("detail").AsCustom($"varchar(255) {DataConstants.COLLATION_TYPE}").NotNullable();

      Insert.IntoTable("day")
        .Row(new { dayid = 1, value = "0", detail = "Sunday" })
        .Row(new { dayid = 2, value = "1", detail = "Monday" })
        .Row(new { dayid = 3, value = "2", detail = "Tuesday" })
        .Row(new { dayid = 4, value = "3", detail = "Wednesday" })
        .Row(new { dayid = 5, value = "4", detail = "Thursday" })
        .Row(new { dayid = 6, value = "5", detail = "Friday" })
        .Row(new { dayid = 7, value = "6", detail = "Saturday" });
      #endregion

      #region Frequency Type Migration
      Create.Table("frequency_type")
         .WithColumn("freqTypeId").AsInt64().PrimaryKey().NotNullable()
         .WithColumn("value").AsCustom($"varchar(255) {DataConstants.COLLATION_TYPE}").NotNullable();

      Insert.IntoTable("frequency_type")
        .Row(new { freqTypeId = 1, value = "Daily" })
        .Row(new { freqTypeId = 2, value = "Weekly" })
        .Row(new { freqTypeId = 3, value = "Monthly" })
        .Row(new { freqTypeId = 4, value = "Yearly" });
      #endregion

      Create.Table("frequency_day")
         .WithColumn("id").AsCustom($"varchar(255) {DataConstants.COLLATION_TYPE}").PrimaryKey().NotNullable() // guid
         .WithColumn("frequencyid").AsCustom($"varchar(255) {DataConstants.COLLATION_TYPE}").NotNullable().ForeignKey("frequency", "idfrequency") // guid
         .WithColumn("dayid").AsInt32().NotNullable().ForeignKey("day", "dayid");

      Alter.Table("frequency")
        .AddColumn("freqTypeId").AsInt64().ForeignKey("frequency_type", "freqTypeId").NotNullable();


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
