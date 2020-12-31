using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recipes.Models;

namespace Recipes.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category>Catergory { get; set; }
        public DbSet<Food> Food { get; set; }
       
        public DbSet<Language> Language { get; set; }
      
        public DbSet<Region> Region { get; set; }
      
        public DbSet<Chef> Chef { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<WorldCuisines> WorldCuisines { get; set; }

    }
}
