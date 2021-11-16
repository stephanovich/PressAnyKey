using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PressAnyKey.Data.Migrations
{
    public partial class dataPublicacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataPostagem",
                table: "Postagem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataPostagem",
                table: "Postagem");
        }
    }
}
