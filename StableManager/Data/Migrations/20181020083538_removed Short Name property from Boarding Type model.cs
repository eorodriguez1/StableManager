using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class removedShortNamepropertyfromBoardingTypemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardingTypeNameShort",
                table: "BoardingType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BoardingTypeNameShort",
                table: "BoardingType",
                nullable: true);
        }
    }
}
