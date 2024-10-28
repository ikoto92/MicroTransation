using Microsoft.EntityFrameworkCore;
//using MicroTransation.Migrations;
using MicroTransation.Models;

namespace MicroTransation.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;/// attribut confifuration
                                                        /// int

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //injection de dépendance
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("bdd"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
