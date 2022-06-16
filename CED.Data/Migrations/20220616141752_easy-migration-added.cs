using CED.Models.Core;
using FluentMigrator;

namespace CED.Data
{
	[Migration(20220616141752)]
	public class easy_migration_added : Migration
	{
		public override void Up()
		{
          //Create.Table<AuthCode>();
		}

		public override void Down()
		{
          Delete.Table("auth_code");
        }
	}
}  
