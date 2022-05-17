using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniProjekt.DAL.Migrations
{
    public partial class rettet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Author");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Author",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
