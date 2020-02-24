using Microsoft.EntityFrameworkCore.Migrations;

namespace Lavyn.Persistence.Migrations
{
    public partial class AddIndexUserOnline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_user_is_online",
                table: "user",
                column: "is_online");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_is_online",
                table: "user");
        }
    }
}
