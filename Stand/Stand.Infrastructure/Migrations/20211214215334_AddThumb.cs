using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stand.Infrastructure.Migrations
{
    public partial class AddThumb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Thumb",
                table: "Viaturas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumb",
                table: "Viaturas");
        }
    }
}
