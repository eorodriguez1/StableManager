using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class AddedforeignkeystoTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionOwnerID",
                table: "Transaction",
                newName: "UserChargedID");

            migrationBuilder.AlterColumn<string>(
                name: "UserChargedID",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserChargedID",
                table: "Transaction",
                column: "UserChargedID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_AspNetUsers_UserChargedID",
                table: "Transaction",
                column: "UserChargedID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_AspNetUsers_UserChargedID",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_UserChargedID",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "UserChargedID",
                table: "Transaction",
                newName: "TransactionOwnerID");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionOwnerID",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
