using Microsoft.EntityFrameworkCore;

namespace BotWonder.DAO
{
    /// <summary>
    /// 机器人数据库上下文
    /// </summary>
    public class BotContext : DbContext
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public BotContext() { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="options">配置</param>
        public BotContext(DbContextOptions<BotContext> options) : base(options) { }

        /// <summary>
        /// 机器人用户
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// 消息识别码
        /// </summary>
        public DbSet<MsgToken> MsgToken { get; set; }

        /// <summary>
        /// 电表数据库
        /// </summary>
        public DbSet<StuRoom> StuRoom { get; set;}

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasOne(u => u.StuRoom);
            });
            modelBuilder.Entity<MsgToken>();
            modelBuilder.Entity<StuRoom>(builder =>
            {
                builder.HasKey(e => e.RoomName);
            });
        }
    }
}
