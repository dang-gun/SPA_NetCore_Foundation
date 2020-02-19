using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation06.Migrations
{
    public partial class Setting_Data_추가 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setting_Data",
                columns: table => new
                {
                    idSetting_Data = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ValueData = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting_Data", x => x.idSetting_Data);
                });

            migrationBuilder.InsertData(
                table: "Setting_Data",
                columns: new[] { "idSetting_Data", "Description", "Name", "Number", "ValueData" },
                values: new object[] { 1L, "프로그램 전체에 표시될 이름", "Title", 1, "ASP.NET Core SPA Foundation" });

            migrationBuilder.InsertData(
                table: "Setting_Data",
                columns: new[] { "idSetting_Data", "Description", "Name", "Number", "ValueData" },
                values: new object[] { 2L, "접속 허용 여부", "ConnectionAllow", 2, "true" });

            migrationBuilder.InsertData(
                table: "Setting_Data",
                columns: new[] { "idSetting_Data", "Description", "Name", "Number", "ValueData" },
                values: new object[] { 3L, "테스트용 타입. TestType 사용", "TestType", 3, "0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Setting_Data");
        }
    }
}
