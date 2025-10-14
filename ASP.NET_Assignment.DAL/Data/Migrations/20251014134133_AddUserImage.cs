using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_Assignment.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "AspNetUsers");
        }
    }
}
