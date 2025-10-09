using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_Assignment.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAppRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "AspNetRoles");
        }
    }
}
