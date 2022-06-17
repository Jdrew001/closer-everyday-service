using FluentMigrator;

namespace CED
{
  [Migration(20220617154418)]
  public class test : Migration
  {
    public override void Up()
    {
      Execute.Script(@"Scripts\20220617154418_test.sql");
    }

    public override void Down()
    {
    }
  }
}
