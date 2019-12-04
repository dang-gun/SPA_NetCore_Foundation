using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation06.Migrations
{
    public partial class DB생성 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    idUser = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SignEmail = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.idUser);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "idUser", "Password", "SignEmail" },
                values: new object[] { 1L, "1111", "test01@test.com" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "idUser", "Password", "SignEmail" },
                values: new object[] { 2L, "1111", "test02@test.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
