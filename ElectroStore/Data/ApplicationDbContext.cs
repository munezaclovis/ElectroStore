using ElectroStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Brand> Brands { set; get; }
        public DbSet<Category> Categories { set; get; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
