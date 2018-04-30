using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class RemovedAnimalTypemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_AnimalType_AnimalTypeID",
                table: "Animal");

            migrationBuilder.DropTable(
                name: "AnimalType");

            migrationBuilder.DropIndex(
                name: "IX_Animal_AnimalTypeID",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "AnimalTypeID",
                table: "Animal",
                newName: "Gender");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Animal",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnimalType",
                table: "Animal",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimalType",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Animal",
                newName: "AnimalTypeID");

            migrationBuilder.AlterColumn<string>(
                name: "AnimalTypeID",
                table: "Animal",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AnimalType",
                columns: table => new
                {
                    AnimalTypeID = table.Column<string>(nullable: false),
                    AnimalTypeName = table.Column<string>(nullable: true),
                    AnimalTypeShortName = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifierUserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalType", x => x.AnimalTypeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_AnimalTypeID",
                table: "Animal",
                column: "AnimalTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_AnimalType_AnimalTypeID",
                table: "Animal",
                column: "AnimalTypeID",
                principalTable: "AnimalType",
                principalColumn: "AnimalTypeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
