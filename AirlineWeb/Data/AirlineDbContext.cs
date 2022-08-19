
using AirlineWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineWeb.Data
{
    public class AirlineDbContext : DbContext
    {
        public DbSet<WebhookSubscription> webhookSubscriptions {get; set;}
        public DbSet<FlightDetail> flightDetails{get; set;}

        public AirlineDbContext()
        {
            
        }

        public AirlineDbContext(DbContextOptions<AirlineDbContext> options) : base(options)
        {
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost,1433;Database=AirlineWebDB;User Id=sa;Password=pa55w0rd!");
        }
    }
}
