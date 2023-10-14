
using complainSystem.models.Complains;
using ComplainSystem.models;
using complainSystem.models;
using Microsoft.EntityFrameworkCore;
using complainSystem.models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ComplainSystem.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Complain> Complains { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          
            modelBuilder.Entity<IdentityUser>()

                .Ignore(c => c.AccessFailedCount)
                .Ignore(c => c.LockoutEnabled)
                .Ignore(c => c.TwoFactorEnabled)
                .Ignore(c => c.ConcurrencyStamp)
                .Ignore(c => c.LockoutEnd)
                .Ignore(c => c.EmailConfirmed)
                .Ignore(c => c.TwoFactorEnabled)
                .Ignore(c => c.LockoutEnd)
                .Ignore(c => c.AccessFailedCount)
                .Ignore(c => c.PhoneNumberConfirmed)
                .Ignore(c => c.NormalizedEmail)
                .Ignore(c => c.PhoneNumber);



            modelBuilder.Entity<IdentityUser>().ToTable("Users");

            // modelBuilder.Entity<Complain>(entity =>
            // {
            //     entity.HasData(new Complain
            //     {
            //         Id = 1,
            //         ComplainTitle = "Theft",
            //         ComplainDescription = "someone stole my bike",
            //         ComplainDateTime = DateTime.Now,
            //         ComplainStatus = ComplainStatus.Open,
            //         CategoryId = 1

            //         // more properties of User
            //     },
            //     new Complain
            //     {
            //         Id = 2,
            //         ComplainTitle = "Harrasment",
            //         ComplainDescription = "someone stole my bike",
            //         ComplainDateTime = DateTime.Now,
            //         ComplainStatus = ComplainStatus.Open,
            //         CategoryId = 2

            //     },

            // new Complain
            // {
            //     Id = 3,
            //     ComplainTitle = "assult",
            //     ComplainDescription = "someone stole my bike",
            //     ComplainDateTime = DateTime.Now,
            //     ComplainStatus = ComplainStatus.Open,
            //     CategoryId = 3

            // }
            //     );

            // }
            // );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Theft",
                    Description = "Theft is the taking of another person's property or services without that person's permission or consent with the intent to deprive the rightful owner of it."
                },
                    new Category
                    {
                        Id = 2,
                        Name = "Harrasment",
                        Description = "Harassment covers a wide range of behaviors of an offensive nature. It is commonly understood as behavior that demeans, humiliates or embarrasses a person, and it is characteristically identified by its unlikelihood in terms of social and moral reasonableness."
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "assult",
                        Description = "Assault is an act of inflicting physical harm or unwanted physical contact upon a person or, in some specific legal definitions, a threat or attempt to commit such an action."
                    }
            );

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "1",
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "2",
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    ConcurrencyStamp = "3",
                }
            );

        }

    }
}