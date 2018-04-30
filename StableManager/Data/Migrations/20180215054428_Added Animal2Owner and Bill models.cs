using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class AddedAnimal2OwnerandBillmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillID",
                table: "Transaction",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnimalToOwner",
                columns: table => new
                {
                    AnimalToOwnerID = table.Column<string>(nullable: false),
                    AnimalID = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifierUserID = table.Column<string>(nullable: true),
                    OwnerID = table.Column<string>(nullable: true),
                    OwnershipEndedOn = table.Column<DateTime>(nullable: false),
                    OwnershipStartedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalToOwner", x => x.AnimalToOwnerID);
                    table.ForeignKey(
                        name: "FK_AnimalToOwner_Animal_AnimalID",
                        column: x => x.AnimalID,
                        principalTable: "Animal",
                        principalColumn: "AnimalID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnimalToOwner_AspNetUsers_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    BillID = table.Column<string>(nullable: false),
                    BillCreatedOn = table.Column<DateTime>(nullable: false),
                    BillCreatorID = table.Column<string>(nullable: true),
                    BillCurrentAmountDue = table.Column<double>(nullable: false),
                    BillDueOn = table.Column<DateTime>(nullable: false),
                    BillFrom = table.Column<DateTime>(nullable: false),
                    BillNetTotal = table.Column<double>(nullable: false),
                    BillNumber = table.Column<string>(nullable: true),
                    BillPastDueAmountDue = table.Column<double>(nullable: false),
                    BillTaxTotal = table.Column<double>(nullable: false),
                    BillTo = table.Column<DateTime>(nullable: false),
                    BillTotalAmountDue = table.Column<double>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifierUserID = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.BillID);
                    table.ForeignKey(
                        name: "FK_Bill_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BillID",
                table: "Transaction",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalToOwner_AnimalID",
                table: "AnimalToOwner",
                column: "AnimalID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalToOwner_OwnerID",
                table: "AnimalToOwner",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_UserID",
                table: "Bill",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Bill_BillID",
                table: "Transaction",
                column: "BillID",
                principalTable: "Bill",
                principalColumn: "BillID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Bill_BillID",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "AnimalToOwner");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_BillID",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "BillID",
                table: "Transaction");
        }
    }
}
