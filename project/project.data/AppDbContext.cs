using Microsoft.EntityFrameworkCore;
using project.entity;

namespace project.data{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}

        public DbSet<User> Users {get; set;} 
        public DbSet<Category> Categories {get; set;} 
        public DbSet<SubCategory> SubCategories {get; set;} 
        public DbSet<Item> Items {get; set;} 
    }
}

