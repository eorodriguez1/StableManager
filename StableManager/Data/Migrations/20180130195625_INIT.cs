using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StableManager.Data.Migrations
{
    public partial class INIT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<bool>(
                name: "ActiveUser",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CanEditAnimals",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanEditBilling",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanEditBoardings",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanEditLessons",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanEditUsers",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanViewAllAnimals",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanViewAllBilling",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanViewAllBoardings",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanViewAllLessons",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanViewAllUsers",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsClient",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployee",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInstructor",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsManager",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTrainer",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoggedOn",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifierUserID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvState",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNumber",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnimalType",
                columns: table => new
                {
                    AnimalTypeID = table.Column<string>(nullable: false),
                    AnimalTypeName = table.Column<string>(nullable: true),
                    AnimalTypeShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalType", x => x.AnimalTypeID);
                });

            migrationBuilder.CreateTable(
                name: "BoardingType",
                columns: table => new
                {
                    BoardingTypeID = table.Column<string>(nullable: false),
                    BoardingPrice = table.Column<double>(nullable: false),
                    BoardingTypeDescription = table.Column<string>(nullable: true),
                    BoardingTypeName = table.Column<string>(nullable: true),
                    BoardingTypeNameShort = table.Column<string>(nullable: true),
                    UserDefined1 = table.Column<string>(nullable: true),
                    UserDefined2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardingType", x => x.BoardingTypeID);
                });

            migrationBuilder.CreateTable(
                name: "StableDetails",
                columns: table => new
                {
                    StableDetailsID = table.Column<string>(nullable: false),
                    ContactName = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifierUserID = table.Column<string>(nullable: true),
                    StableAddress = table.Column<string>(nullable: true),
                    StableCity = table.Column<string>(nullable: true),
                    StableCountry = table.Column<string>(nullable: true),
                    StableEmail = table.Column<string>(nullable: true),
                    StableName = table.Column<string>(nullable: true),
                    StablePhone = table.Column<string>(nullable: true),
                    StablePostalCode = table.Column<string>(nullable: true),
                    StableProvState = table.Column<string>(nullable: true),
                    TaxNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StableDetails", x => x.StableDetailsID);
                });

            migrationBuilder.CreateTable(
                name: "TransactionType",
                columns: table => new
                {
                    TransactionTypeID = table.Column<string>(nullable: false),
                    TransactionDescription = table.Column<string>(nullable: true),
                    TransactionTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionType", x => x.TransactionTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    AnimalID = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    AnimalName = table.Column<string>(nullable: true),
                    AnimalNumber = table.Column<string>(nullable: true),
                    AnimalTypeID = table.Column<string>(nullable: true),
                    Breed = table.Column<string>(nullable: true),
                    DietDetails = table.Column<string>(nullable: true),
                    HealthConcerns = table.Column<string>(nullable: true),
                    SpecialDiet = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.AnimalID);
                    table.ForeignKey(
                        name: "FK_Animal_AnimalType_AnimalTypeID",
                        column: x => x.AnimalTypeID,
                        principalTable: "AnimalType",
                        principalColumn: "AnimalTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionID = table.Column<string>(nullable: false),
                    TransactionAdditionalDescription = table.Column<string>(nullable: true),
                    TransactionMadeOn = table.Column<DateTime>(nullable: false),
                    TransactionNumber = table.Column<string>(nullable: true),
                    TransactionOwnerID = table.Column<string>(nullable: true),
                    TransactionTypeID = table.Column<string>(nullable: true),
                    TransactionValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transaction_TransactionType_TransactionTypeID",
                        column: x => x.TransactionTypeID,
                        principalTable: "TransactionType",
                        principalColumn: "TransactionTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnimalToUser",
                columns: table => new
                {
                    AnimalToUserID = table.Column<string>(nullable: false),
                    AnimalID = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalToUser", x => x.AnimalToUserID);
                    table.ForeignKey(
                        name: "FK_AnimalToUser_Animal_AnimalID",
                        column: x => x.AnimalID,
                        principalTable: "Animal",
                        principalColumn: "AnimalID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnimalToUser_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Boarding",
                columns: table => new
                {
                    BoardingID = table.Column<string>(nullable: false),
                    AnimalID = table.Column<string>(nullable: true),
                    BoardingTypeID = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boarding", x => x.BoardingID);
                    table.ForeignKey(
                        name: "FK_Boarding_Animal_AnimalID",
                        column: x => x.AnimalID,
                        principalTable: "Animal",
                        principalColumn: "AnimalID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Boarding_BoardingType_BoardingTypeID",
                        column: x => x.BoardingTypeID,
                        principalTable: "BoardingType",
                        principalColumn: "BoardingTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Boarding_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_AnimalTypeID",
                table: "Animal",
                column: "AnimalTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalToUser_AnimalID",
                table: "AnimalToUser",
                column: "AnimalID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalToUser_UserID",
                table: "AnimalToUser",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Boarding_AnimalID",
                table: "Boarding",
                column: "AnimalID");

            migrationBuilder.CreateIndex(
                name: "IX_Boarding_BoardingTypeID",
                table: "Boarding",
                column: "BoardingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Boarding_UserID",
                table: "Boarding",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TransactionTypeID",
                table: "Transaction",
                column: "TransactionTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AnimalToUser");

            migrationBuilder.DropTable(
                name: "Boarding");

            migrationBuilder.DropTable(
                name: "StableDetails");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "BoardingType");

            migrationBuilder.DropTable(
                name: "TransactionType");

            migrationBuilder.DropTable(
                name: "AnimalType");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ActiveUser",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CanEditAnimals",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CanEditBilling",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CanEditBoardings",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CanEditLessons",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CanEditUsers",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CanViewAllAnimals",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CanViewAllBilling",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CanViewAllBoardings",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CanViewAllLessons",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CanViewAllUsers",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsClient",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsEmployee",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsInstructor",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsTrainer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastLoggedOn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModifierUserID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProvState",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserNumber",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
