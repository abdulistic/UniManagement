using DemoClassLibrary.UsersMgt;
using Restaurant.ClassLibrary.PakClassified;
using Restaurant.ClassLibrary.UsersMgt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary
{
    public class PakClassifiedContext : DbContext 
    {
        public PakClassifiedContext() : base("RestaurantTest")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
      
        public DbSet<AdvertisementStatus> Status { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<UserGender> Genders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Extras> Extrass { get; set; }
        public DbSet<OrderExtra> OrderExtras { get; set; }
        public DbSet<AdvertisementImage> AdvertisementImages { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

    }
}
