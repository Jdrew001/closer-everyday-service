using FluentMigrator;

namespace CED
{
  [Migration(20220617154418)]
  public class test : Migration
  {
    public override void Up()
    {
      Execute.EmbeddedScript(@"Scripts\20220617154418_test.sql");
    }

    public override void Down()
    {
    }
  }
}
