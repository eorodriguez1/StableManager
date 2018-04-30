using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class AddedHealthUpdatesmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "TransactionType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnimalHealthUpdates",
                columns: table => new
                {
                    AnimalHealthUpdatesID = table.Column<string>(nullable: false),
                    AnimalID = table.Column<string>(nullable: true),
                    DateOccured = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifierUserID = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UserBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalHealthUpdates", x => x.AnimalHealthUpdatesID);
                    table.ForeignKey(
                        name: "FK_AnimalHealthUpdates_Animal_AnimalID",
                        column: x => x.AnimalID,
                        principalTable: "Animal",
                        principalColumn: "AnimalID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalHealthUpdates_AnimalID",
                table: "AnimalHealthUpdates",
                column: "AnimalID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalHealthUpdates");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "TransactionType");
        }
    }
}
