using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TafeGrandeGift.Models;

namespace TafeGrandeGift.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<TafeGrandeGift.Models.Product> Product { get; set; }

        public DbSet<TafeGrandeGift.Models.Hamper> Hamper { get; set; }

        public DbSet<TafeGrandeGift.Models.Category> Category { get; set; }

        public DbSet<TafeGrandeGift.Models.HamperProduct> hamperProducts { get; set; }

        public DbSet<TafeGrandeGift.Models.HamperFeedBack> hamperFeedBacks { get; set; }

        public DbSet<TafeGrandeGift.Models.UserAddress> UserAddress { get; set; }

        public DbSet<TafeGrandeGift.Models.Order> Order { get; set; }
        public DbSet<TafeGrandeGift.Models.OrderHamper> OrderHamper { get; set; }

        //public DbSet<TafeGrandeGift.Models.CartItem> ShoppingCartItems { get; set; }
    }
}
