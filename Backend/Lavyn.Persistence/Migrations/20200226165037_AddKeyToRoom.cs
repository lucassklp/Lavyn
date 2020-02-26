using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lavyn.Persistence.Migrations
{
    public partial class AddKeyToRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "key",
                table: "rooms",
                maxLength: 64,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tokens",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<long>(nullable: false),
                    type = table.Column<int>(nullable: false),
                    value = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_tokens_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_rooms_key",
                table: "rooms",
                column: "key");

            migrationBuilder.CreateIndex(
                name: "ix_tokens_type",
                table: "tokens",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "ix_tokens_user_id",
                table: "tokens",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tokens");

            migrationBuilder.DropIndex(
                name: "ix_rooms_key",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "key",
                table: "rooms");
        }
    }
}
