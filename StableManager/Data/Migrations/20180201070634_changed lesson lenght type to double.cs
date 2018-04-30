using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class changedlessonlenghttypetodouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonLenght",
                table: "Lesson");

            migrationBuilder.AddColumn<double>(
                name: "LessonLength",
                table: "Lesson",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonLength",
                table: "Lesson");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "LessonLenght",
                table: "Lesson",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
