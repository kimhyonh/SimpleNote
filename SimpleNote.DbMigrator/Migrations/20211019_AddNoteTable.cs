using FluentMigrator;

namespace SimpleNote.DbMigrator.Migrations
{
    [Migration(20211019121800)]
    public class AddNoteTable : Migration
    {
        public override void Up()
        {
            Create.Table("Note")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Text").AsString();
        }

        public override void Down()
        {
            Delete.Table("Note");
        }
    }
}
