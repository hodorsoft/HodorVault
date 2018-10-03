using FluentMigrator;

namespace HodorVault.Data.Migrations.Migrations
{
    [Migration(1)]
    public class CreateArtistsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Artists")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Country").AsString().Nullable()
                .WithColumn("DiscogsData").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Artists");
        }
    }
}
