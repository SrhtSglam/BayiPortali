using Microsoft.EntityFrameworkCore;
using project.entity;

namespace project.data{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}

        public DbSet<User> Users {get; set;} 
    }
}

