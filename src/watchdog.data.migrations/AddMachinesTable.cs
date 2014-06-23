using FluentMigrator;

namespace watchdog.data.migrations
{
	[Migration(201405021124)]
	public class AddMachinesTable : Migration
	{
		public override void Up()
		{
			Create.Table("Machines")
				.WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
				.WithColumn("Name").AsString().NotNullable().WithDefaultValue("Unknown");
		}

		public override void Down()
		{
			Delete.Table("Machines");
		}
	}
}