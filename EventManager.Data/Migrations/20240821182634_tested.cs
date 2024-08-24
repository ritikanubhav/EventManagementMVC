using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class tested : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Atest",
                table: "AspNetUsers",
                newName: "tested");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tested",
                table: "AspNetUsers",
                newName: "Atest");
        }
    }
}
