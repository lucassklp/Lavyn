using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lavyn.Persistence.Migrations
{
    public partial class AdjustOnRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_message_date",
                table: "rooms",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "rooms",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_message_date",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "name",
                table: "rooms");
        }
    }
}
