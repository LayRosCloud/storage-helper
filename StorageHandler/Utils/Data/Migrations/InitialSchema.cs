using System.Data;
using FluentMigrator;

namespace StorageHandler.Utils.Data.Migrations;

[Migration(1, "initialize")]
public class InitialSchema : Migration
{
    public override void Up()
    {
        Create.Table("units")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("name").AsString(10).NotNullable()
            .WithColumn("archive_at").AsDateTimeOffset().Nullable();

        Create.Table("resources")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("name").AsString(100).NotNullable()
            .WithColumn("archive_at").AsDateTimeOffset().Nullable();

        Create.Table("entrances")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("number").AsString(30).NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable();

        Create.Table("entrances_buckets")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("resource_id").AsInt64().ForeignKey("fk_buckets_resources","resources", "id").OnDelete(Rule.None).NotNullable()
            .WithColumn("unit_id").AsInt64().ForeignKey("fk_buckets_units","units", "id").OnDelete(Rule.None).NotNullable()
            .WithColumn("entrance_id").AsInt64().ForeignKey("fk_buckets_entrances", "entrances", "id").OnDelete(Rule.Cascade).NotNullable()
            .WithColumn("quantity").AsDouble().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable();

    }

    public override void Down()
    {
        Delete.Table("units");
        Delete.Table("resources");
        Delete.Table("entrances");
        Delete.Table("entrances_buckets");
    }
}