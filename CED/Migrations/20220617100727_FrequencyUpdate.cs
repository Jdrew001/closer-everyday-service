using FluentMigrator;

namespace CED
{
  [Migration(202206171007271)]
  public class FrequencyUpdate : Migration
  {
    public override void Up()
    {
      //Delete.Table("day");
      //Delete.Table("frequency_type");

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
    }

    public override void Down()
    {

    }
  }
}
