using Microsoft.EntityFrameworkCore;

namespace BotWonder.DAO
{
    public class BotContext : DbContext
    {
        public BotContext() { }

        public BotContext(DbContextOptions<BotContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<MsgToken> MsgToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<MsgToken>();
        }
    }
}
