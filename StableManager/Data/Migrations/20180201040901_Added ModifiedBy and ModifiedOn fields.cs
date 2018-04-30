using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class AddedModifiedByandModifiedOnfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserDefined1",
                table: "BoardingType");

            migrationBuilder.RenameColumn(
                name: "UserDefined2",
                table: "BoardingType",
                newName: "ModifierUserID");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "TransactionType",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifierUserID",
                table: "TransactionType",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Transaction",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifierUserID",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "BoardingType",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Boarding",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifierUserID",
                table: "Boarding",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "AnimalType",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifierUserID",
                table: "AnimalType",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "AnimalToUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifierUserID",
                table: "AnimalToUser",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Animal",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifierUserID",
                table: "Animal",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    LessonID = table.Column<string>(nullable: false),
                    LessonCost = table.Column<double>(nullable: false),
                    LessonDescription = table.Column<string>(nullable: true),
                    LessonLenght = table.Column<DateTime>(nullable: false),
                    LessonNumber = table.Column<string>(nullable: true),
                    LessonTime = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifierUserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.LessonID);
                });

            migrationBuilder.CreateTable(
                name: "LessonToUsers",
                columns: table => new
                {
                    LessonToUsersID = table.Column<string>(nullable: false),
                    ClientUserID = table.Column<string>(nullable: true),
                    InstructorUserID = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifierUserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonToUsers", x => x.LessonToUsersID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "LessonToUsers");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "TransactionType");

            migrationBuilder.DropColumn(
                name: "ModifierUserID",
                table: "TransactionType");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ModifierUserID",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "BoardingType");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Boarding");

            migrationBuilder.DropColumn(
                name: "ModifierUserID",
                table: "Boarding");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "AnimalType");

            migrationBuilder.DropColumn(
                name: "ModifierUserID",
                table: "AnimalType");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "AnimalToUser");

            migrationBuilder.DropColumn(
                name: "ModifierUserID",
                table: "AnimalToUser");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "ModifierUserID",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "ModifierUserID",
                table: "BoardingType",
                newName: "UserDefined2");

            migrationBuilder.AddColumn<string>(
                name: "UserDefined1",
                table: "BoardingType",
                nullable: true);
        }
    }
}
