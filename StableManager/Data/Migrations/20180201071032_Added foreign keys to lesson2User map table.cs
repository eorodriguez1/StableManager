using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class Addedforeignkeystolesson2Usermaptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InstructorUserID",
                table: "LessonToUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientUserID",
                table: "LessonToUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonToUsers_ClientUserID",
                table: "LessonToUsers",
                column: "ClientUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonToUsers_InstructorUserID",
                table: "LessonToUsers",
                column: "InstructorUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonToUsers_AspNetUsers_ClientUserID",
                table: "LessonToUsers",
                column: "ClientUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonToUsers_AspNetUsers_InstructorUserID",
                table: "LessonToUsers",
                column: "InstructorUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonToUsers_AspNetUsers_ClientUserID",
                table: "LessonToUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonToUsers_AspNetUsers_InstructorUserID",
                table: "LessonToUsers");

            migrationBuilder.DropIndex(
                name: "IX_LessonToUsers_ClientUserID",
                table: "LessonToUsers");

            migrationBuilder.DropIndex(
                name: "IX_LessonToUsers_InstructorUserID",
                table: "LessonToUsers");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorUserID",
                table: "LessonToUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientUserID",
                table: "LessonToUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
