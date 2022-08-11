using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.API.Migrations
{
    public partial class todoaddpriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "ToDoTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "ToDoTasks");
        }
    }
}
