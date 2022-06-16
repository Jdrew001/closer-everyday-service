using CED.Models.Core;
using EasyMigrator;
using FluentMigrator;

namespace CED.Data
{
  [Migration(10220616162246)]
  public class NewMigration : Migration
  {
    public override void Down()
    {
      Delete.Table<TestTable>();
    }

    public override void Up()
    {
      Create.Table<TestTable>();
    }
  }
}  
