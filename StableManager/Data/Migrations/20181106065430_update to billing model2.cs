using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class updatetobillingmodel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Bill_BillID",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_BillID",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "BillID",
                table: "Transaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillID",
                table: "Transaction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BillID",
                table: "Transaction",
                column: "BillID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Bill_BillID",
                table: "Transaction",
                column: "BillID",
                principalTable: "Bill",
                principalColumn: "BillID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
