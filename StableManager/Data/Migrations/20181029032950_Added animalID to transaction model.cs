using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class AddedanimalIDtotransactionmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnimalID",
                table: "Transaction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AnimalID",
                table: "Transaction",
                column: "AnimalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Animal_AnimalID",
                table: "Transaction",
                column: "AnimalID",
                principalTable: "Animal",
                principalColumn: "AnimalID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Animal_AnimalID",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_AnimalID",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "AnimalID",
                table: "Transaction");
        }
    }
}
