using FluentMigrator;

namespace HodorVault.Data.Migrations.Migrations
{
    [Migration(2)]
    public class CreateAlbumsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Albums")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Type").AsString().Nullable()
                .WithColumn("Released").AsInt32().Nullable()
                // might be V/A
                .WithColumn("ArtistId").AsInt32().ForeignKey("FK_Album_Artist", "Artists", "Id").Nullable()
                .WithColumn("Comment").AsString().Nullable()
                .WithColumn("DiscogsData").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Albums");
        }
    }
}
