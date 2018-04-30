using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class CorrectiontoHealthUpdatedatabasetoccuredonfromtexttodatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOccured",
                table: "AnimalHealthUpdates",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DateOccured",
                table: "AnimalHealthUpdates",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
