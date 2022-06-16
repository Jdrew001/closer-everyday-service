using CED.Models.Core;
using EasyMigrator;
using FluentMigrator;

namespace CED.Data
{
  [Migration(20220616171237)]
  public class TestTableMigration : Migration
  {
    public override void Up()
    {
      Delete.Table("test_table");

      Create.Table<TestTable>();
    }

    public override void Down()
    {
          //Delete.Table<TestTable>();
    }
  }
}  
