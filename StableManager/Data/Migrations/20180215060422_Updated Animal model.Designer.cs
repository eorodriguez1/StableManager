﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using StableManager.Data;
using StableManager.Models;
using System;

namespace StableManager.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180215060422_Updated Animal model")]
    partial class UpdatedAnimalmodel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("StableManager.Models.Animal", b =>
                {
                    b.Property<string>("AnimalID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("AnimalName");

                    b.Property<string>("AnimalNumber");

                    b.Property<string>("AnimalType");

                    b.Property<string>("Breed");

                    b.Property<string>("DietDetails");

                    b.Property<string>("Gender");

                    b.Property<string>("HealthConcerns");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<bool>("SpecialDiet");

                    b.HasKey("AnimalID");

                    b.ToTable("Animal");
                });

            modelBuilder.Entity("StableManager.Models.AnimalUpdates", b =>
                {
                    b.Property<string>("AnimalUpdatesID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnimalID");

                    b.Property<string>("DateOccured");

                    b.Property<string>("Description");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<string>("Name");

                    b.Property<string>("UserBy");

                    b.HasKey("AnimalUpdatesID");

                    b.HasIndex("AnimalID");

                    b.ToTable("AnimalUpdates");
                });

            modelBuilder.Entity("StableManager.Models.AnimalToOwner", b =>
                {
                    b.Property<string>("AnimalToOwnerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnimalID");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<string>("OwnerID");

                    b.Property<DateTime>("OwnershipEndedOn");

                    b.Property<DateTime>("OwnershipStartedOn");

                    b.HasKey("AnimalToOwnerID");

                    b.HasIndex("AnimalID");

                    b.HasIndex("OwnerID");

                    b.ToTable("AnimalToOwner");
                });

            modelBuilder.Entity("StableManager.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("ActiveUser");

                    b.Property<string>("Address");

                    b.Property<bool>("CanEditAnimals");

                    b.Property<bool>("CanEditBilling");

                    b.Property<bool>("CanEditBoardings");

                    b.Property<bool>("CanEditLessons");

                    b.Property<bool>("CanEditUsers");

                    b.Property<bool>("CanViewAllAnimals");

                    b.Property<bool>("CanViewAllBilling");

                    b.Property<bool>("CanViewAllBoardings");

                    b.Property<bool>("CanViewAllLessons");

                    b.Property<bool>("CanViewAllUsers");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Country");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsClient");

                    b.Property<bool>("IsEmployee");

                    b.Property<bool>("IsInstructor");

                    b.Property<bool>("IsManager");

                    b.Property<bool>("IsTrainer");

                    b.Property<DateTime>("LastLoggedOn");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PostalCode");

                    b.Property<string>("ProvState");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("UserNumber");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("StableManager.Models.Bill", b =>
                {
                    b.Property<string>("BillID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BillCreatedOn");

                    b.Property<string>("BillCreatorID");

                    b.Property<double>("BillCurrentAmountDue");

                    b.Property<DateTime>("BillDueOn");

                    b.Property<DateTime>("BillFrom");

                    b.Property<double>("BillNetTotal");

                    b.Property<string>("BillNumber");

                    b.Property<double>("BillPastDueAmountDue");

                    b.Property<double>("BillTaxTotal");

                    b.Property<DateTime>("BillTo");

                    b.Property<double>("BillTotalAmountDue");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<string>("UserID");

                    b.HasKey("BillID");

                    b.HasIndex("UserID");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("StableManager.Models.Boarding", b =>
                {
                    b.Property<string>("BoardingID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnimalID");

                    b.Property<string>("BillToUserID");

                    b.Property<string>("BoardingTypeID");

                    b.Property<DateTime>("EndedBoard");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<DateTime>("StartedBoard");

                    b.HasKey("BoardingID");

                    b.HasIndex("AnimalID");

                    b.HasIndex("BillToUserID");

                    b.HasIndex("BoardingTypeID");

                    b.ToTable("Boarding");
                });

            modelBuilder.Entity("StableManager.Models.BoardingType", b =>
                {
                    b.Property<string>("BoardingTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BoardingPrice");

                    b.Property<string>("BoardingTypeDescription");

                    b.Property<string>("BoardingTypeName");

                    b.Property<string>("BoardingTypeNameShort");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.HasKey("BoardingTypeID");

                    b.ToTable("BoardingType");
                });

            modelBuilder.Entity("StableManager.Models.Client", b =>
                {
                    b.Property<string>("ClientID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlternativeVet");

                    b.Property<string>("AlternativeVetDetails");

                    b.Property<string>("ClientNumber");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<string>("PreferredVet");

                    b.Property<string>("PreferredVetDetails");

                    b.Property<string>("UserID");

                    b.HasKey("ClientID");

                    b.HasIndex("UserID");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("StableManager.Models.Lesson", b =>
                {
                    b.Property<string>("LessonID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("LessonCost");

                    b.Property<string>("LessonDescription");

                    b.Property<double>("LessonLength");

                    b.Property<string>("LessonNumber");

                    b.Property<DateTime>("LessonTime");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.HasKey("LessonID");

                    b.ToTable("Lesson");
                });

            modelBuilder.Entity("StableManager.Models.LessonToUsers", b =>
                {
                    b.Property<string>("LessonToUsersID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientUserID");

                    b.Property<string>("InstructorUserID");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.HasKey("LessonToUsersID");

                    b.HasIndex("ClientUserID");

                    b.HasIndex("InstructorUserID");

                    b.ToTable("LessonToUsers");
                });

            modelBuilder.Entity("StableManager.Models.StableDetails", b =>
                {
                    b.Property<string>("StableDetailsID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactName");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<string>("StableAddress");

                    b.Property<string>("StableCity");

                    b.Property<string>("StableCountry");

                    b.Property<string>("StableEmail");

                    b.Property<string>("StableName");

                    b.Property<string>("StablePhone");

                    b.Property<string>("StablePostalCode");

                    b.Property<string>("StableProvState");

                    b.Property<string>("TaxNumber");

                    b.HasKey("StableDetailsID");

                    b.ToTable("StableDetails");
                });

            modelBuilder.Entity("StableManager.Models.Transaction", b =>
                {
                    b.Property<string>("TransactionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BillID");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<string>("TransactionAdditionalDescription");

                    b.Property<DateTime>("TransactionMadeOn");

                    b.Property<string>("TransactionNumber");

                    b.Property<string>("TransactionTypeID");

                    b.Property<double>("TransactionValue");

                    b.Property<string>("UserChargedID");

                    b.HasKey("TransactionID");

                    b.HasIndex("BillID");

                    b.HasIndex("TransactionTypeID");

                    b.HasIndex("UserChargedID");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("StableManager.Models.TransactionType", b =>
                {
                    b.Property<string>("TransactionTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<string>("TransactionDescription");

                    b.Property<string>("TransactionTypeName");

                    b.Property<int>("Type");

                    b.HasKey("TransactionTypeID");

                    b.ToTable("TransactionType");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("StableManager.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("StableManager.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StableManager.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("StableManager.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StableManager.Models.AnimalUpdates", b =>
                {
                    b.HasOne("StableManager.Models.Animal", "Animal")
                        .WithMany("HealthUpdates")
                        .HasForeignKey("AnimalID");
                });

            modelBuilder.Entity("StableManager.Models.AnimalToOwner", b =>
                {
                    b.HasOne("StableManager.Models.Animal", "Animal")
                        .WithMany()
                        .HasForeignKey("AnimalID");

                    b.HasOne("StableManager.Models.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerID");
                });

            modelBuilder.Entity("StableManager.Models.Bill", b =>
                {
                    b.HasOne("StableManager.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("StableManager.Models.Boarding", b =>
                {
                    b.HasOne("StableManager.Models.Animal", "Animal")
                        .WithMany()
                        .HasForeignKey("AnimalID");

                    b.HasOne("StableManager.Models.ApplicationUser", "BillToUser")
                        .WithMany()
                        .HasForeignKey("BillToUserID");

                    b.HasOne("StableManager.Models.BoardingType", "BoardingType")
                        .WithMany()
                        .HasForeignKey("BoardingTypeID");
                });

            modelBuilder.Entity("StableManager.Models.Client", b =>
                {
                    b.HasOne("StableManager.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("StableManager.Models.LessonToUsers", b =>
                {
                    b.HasOne("StableManager.Models.ApplicationUser", "ClientUser")
                        .WithMany()
                        .HasForeignKey("ClientUserID");

                    b.HasOne("StableManager.Models.ApplicationUser", "InstructorUser")
                        .WithMany()
                        .HasForeignKey("InstructorUserID");
                });

            modelBuilder.Entity("StableManager.Models.Transaction", b =>
                {
                    b.HasOne("StableManager.Models.Bill")
                        .WithMany("Transactions")
                        .HasForeignKey("BillID");

                    b.HasOne("StableManager.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeID");

                    b.HasOne("StableManager.Models.ApplicationUser", "UserCharged")
                        .WithMany()
                        .HasForeignKey("UserChargedID");
                });
#pragma warning restore 612, 618
        }
    }
}
