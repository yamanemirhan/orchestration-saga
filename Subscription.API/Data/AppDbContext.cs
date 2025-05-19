using Microsoft.EntityFrameworkCore;
using Subscription.API.Entities;
using Subscription.API.Sagas;

namespace Subscription.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SubscriptionEntity> Subscriptions { get; set; }

        public DbSet<SubscriptionState> SubscriptionStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SubscriptionState>().HasKey(s => s.CorrelationId); 
        }
    }
}
