﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using StableManager.Data;
using System;

namespace StableManager.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180201064017_changed lesson lenght type")]
    partial class changedlessonlenghttype
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

                    b.Property<string>("AnimalTypeID");

                    b.Property<string>("Breed");

                    b.Property<string>("DietDetails");

                    b.Property<string>("Notes");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<bool>("SpecialDiet");

                    b.HasKey("AnimalID");

                    b.HasIndex("AnimalTypeID");

                    b.ToTable("Animal");
                });

            modelBuilder.Entity("StableManager.Models.AnimalToUser", b =>
                {
                    b.Property<string>("AnimalToUserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnimalID");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<string>("UserID");

                    b.HasKey("AnimalToUserID");

                    b.HasIndex("AnimalID");

                    b.HasIndex("UserID");

                    b.ToTable("AnimalToUser");
                });

            modelBuilder.Entity("StableManager.Models.AnimalType", b =>
                {
                    b.Property<string>("AnimalTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnimalTypeName");

                    b.Property<string>("AnimalTypeShortName");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.HasKey("AnimalTypeID");

                    b.ToTable("AnimalType");
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

                    b.Property<string>("MiddleName");

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

            modelBuilder.Entity("StableManager.Models.Boarding", b =>
                {
                    b.Property<string>("BoardingID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnimalID");

                    b.Property<string>("BoardingTypeID");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<string>("UserID");

                    b.HasKey("BoardingID");

                    b.HasIndex("AnimalID");

                    b.HasIndex("BoardingTypeID");

                    b.HasIndex("UserID");

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

            modelBuilder.Entity("StableManager.Models.Lesson", b =>
                {
                    b.Property<string>("LessonID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("LessonCost");

                    b.Property<string>("LessonDescription");

                    b.Property<TimeSpan>("LessonLenght");

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

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ModifierUserID");

                    b.Property<string>("TransactionAdditionalDescription");

                    b.Property<DateTime>("TransactionMadeOn");

                    b.Property<string>("TransactionNumber");

                    b.Property<string>("TransactionOwnerID");

                    b.Property<string>("TransactionTypeID");

                    b.Property<double>("TransactionValue");

                    b.HasKey("TransactionID");

                    b.HasIndex("TransactionTypeID");

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

            modelBuilder.Entity("StableManager.Models.Animal", b =>
                {
                    b.HasOne("StableManager.Models.AnimalType", "Type")
                        .WithMany()
                        .HasForeignKey("AnimalTypeID");
                });

            modelBuilder.Entity("StableManager.Models.AnimalToUser", b =>
                {
                    b.HasOne("StableManager.Models.Animal", "Animal")
                        .WithMany()
                        .HasForeignKey("AnimalID");

                    b.HasOne("StableManager.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("StableManager.Models.Boarding", b =>
                {
                    b.HasOne("StableManager.Models.Animal", "Animal")
                        .WithMany()
                        .HasForeignKey("AnimalID");

                    b.HasOne("StableManager.Models.BoardingType", "BoardingType")
                        .WithMany()
                        .HasForeignKey("BoardingTypeID");

                    b.HasOne("StableManager.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("StableManager.Models.Transaction", b =>
                {
                    b.HasOne("StableManager.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeID");
                });
#pragma warning restore 612, 618
        }
    }
}
