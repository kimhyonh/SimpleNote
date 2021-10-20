using FluentMigrator;

namespace SimpleNote.DbMigrator.Migrations
{
    [Migration(20211020121800)]
    public class UpdateNoteTable : Migration
    {
        public override void Up()
        {
            Alter.Table("Note")
                .AddColumn("CreatedDate").AsDateTime2().NotNullable();
        }

        public override void Down()
        {
            Delete.Column("CreatedDate")
                .FromTable("Note");
        }
    }
}
