using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class addedownerpropertytoanimalmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnimalOwnerID",
                table: "Animal",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animal_AnimalOwnerID",
                table: "Animal",
                column: "AnimalOwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_AspNetUsers_AnimalOwnerID",
                table: "Animal",
                column: "AnimalOwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_AspNetUsers_AnimalOwnerID",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_AnimalOwnerID",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "AnimalOwnerID",
                table: "Animal");
        }
    }
}
