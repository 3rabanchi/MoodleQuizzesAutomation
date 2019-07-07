using Microsoft.EntityFrameworkCore.Migrations;

namespace TeachingTool.Migrations
{
    public partial class userToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userToken",
                table: "Questions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userToken",
                table: "Questions");
        }
    }
}
