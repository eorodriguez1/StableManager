using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class Namechangeforuserinboarding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boarding_AspNetUsers_UserID",
                table: "Boarding");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Boarding",
                newName: "BillToUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Boarding_UserID",
                table: "Boarding",
                newName: "IX_Boarding_BillToUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Boarding_AspNetUsers_BillToUserID",
                table: "Boarding",
                column: "BillToUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boarding_AspNetUsers_BillToUserID",
                table: "Boarding");

            migrationBuilder.RenameColumn(
                name: "BillToUserID",
                table: "Boarding",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Boarding_BillToUserID",
                table: "Boarding",
                newName: "IX_Boarding_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Boarding_AspNetUsers_UserID",
                table: "Boarding",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
