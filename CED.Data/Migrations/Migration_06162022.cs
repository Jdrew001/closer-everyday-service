using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Data.Migrations
{
  [Migration(06162022, "initial migration")]
  public class Migration06162022 : Migration
  {
    public override void Down()
    {
      Delete.Table("testTable");
    }

    public override void Up()
    {
      Create.Table("testTable")
         .WithColumn("id").AsInt32().NotNullable().Identity().PrimaryKey()
         .WithColumn("testName").AsString().Nullable();
    }
  }
}
