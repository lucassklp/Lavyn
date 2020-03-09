using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lavyn.Persistence.Migrations
{
    public partial class AddViewedOnGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "viewed",
                table: "messages");

            migrationBuilder.AddColumn<DateTime>(
                name: "last_seen",
                table: "user_has_room",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_seen",
                table: "user_has_room");

            migrationBuilder.AddColumn<bool>(
                name: "viewed",
                table: "messages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
