using Microsoft.EntityFrameworkCore.Migrations;

namespace Lavyn.Persistence.Migrations
{
    public partial class AddViewedOnMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "viewed",
                table: "messages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "viewed",
                table: "messages");
        }
    }
}
