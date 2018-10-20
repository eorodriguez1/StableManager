using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class removednamepropertyfromanimalupdatemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AnimalUpdates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AnimalUpdates",
                nullable: true);
        }
    }
}
