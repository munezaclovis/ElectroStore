using ElectroStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElectroStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<Brand> Brands { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Product> Products { set; get; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;

                    entity.GetType().GetProperty("Deleted").SetValue(entity, true);
                }
            }
            return base.SaveChanges();
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;

                    entity.GetType().GetProperty("Deleted").SetValue(entity, true);
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //Defining primary keys of every table
            builder.Entity<Brand>().HasKey(x => x.Id);
            builder.Entity<Category>().HasKey(x => x.Id);
            builder.Entity<Product>().HasKey(x => x.Id);
            builder.Entity<User>().HasIndex(x => x.UserName).IsUnique();


            //Defining Default Values of some fields
            builder.Entity<Category>().Property(x => x.Icon).HasDefaultValue("");
            builder.Entity<Brand>().Property(x => x.Deleted).HasDefaultValue(0);
            builder.Entity<Category>().Property(x => x.Deleted).HasDefaultValue(0);
            builder.Entity<Product>().Property(x => x.Deleted).HasDefaultValue(0);
            builder.Entity<User>().Property(x => x.Deleted).HasDefaultValue(0);


            builder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
            builder.Entity<Product>().HasOne(p => p.Brand).WithMany(c => c.Products).HasForeignKey(p => p.BrandId);


            //Rename Identity Tables With Cute Names :)
            builder.Entity<User>().ToTable("User");
            builder.Entity<Role>().ToTable("Role");
            builder.Entity<UserRole>().ToTable("UserRole");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }
    }
}
