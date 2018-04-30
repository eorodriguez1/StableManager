using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StableManager.Models;

namespace StableManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Additional Db Sets
        public DbSet<StableDetails> StableDetails { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<BoardingType> BoardingType { get; set; }
        public DbSet<Boarding> Boardings { get; set; }
        public DbSet<TransactionType> TransactionType { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AnimalHealthUpdates> AnimalHealthUpdates { get; set; }
        public DbSet<AnimalToOwner> AnimalToOwner { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<StableDetails>().ToTable("StableDetails");
            builder.Entity<Animal>().ToTable("Animal");
            builder.Entity<BoardingType>().ToTable("BoardingType");
            builder.Entity<Boarding>().ToTable("Boarding");
            builder.Entity<TransactionType>().ToTable("TransactionType");
            builder.Entity<Transaction>().ToTable("Transaction");
            builder.Entity<AnimalHealthUpdates>().ToTable("AnimalHealthUpdates");
            builder.Entity<AnimalToOwner>().ToTable("AnimalToOwner");
            builder.Entity<Bill>().ToTable("Bill");
            builder.Entity<Client>().ToTable("Client");

        }

        public DbSet<StableManager.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<StableManager.Models.Lesson> Lesson { get; set; }

        public DbSet<StableManager.Models.LessonToUsers> LessonToUsers { get; set; }
    }
}
