using BotWonder.DAO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotWonder.Services
{
    /// <summary>
    /// 数据库处理类
    /// </summary>
    public class DbHandler
    {
        private readonly BotContext _context;

        /// <summary>
        /// 依赖注入<see cref="BotContext"/>
        /// </summary>
        /// <param name="context"></param>
        public DbHandler(BotContext context)
        {
            _context = context;
        }

        internal async Task NewUser(User newUser)
        {
            var user = await _context.User.FindAsync(newUser.Qq);

            if (user == null)
            {
                newUser.Active();
                await _context.User.AddAsync(newUser);
            }
            else
            {
                user.Username = newUser.Username;
                user.Password = newUser.Password;
                user.Active();
            }
            await _context.SaveChangesAsync();
        }

        internal async Task<User> GetUser(long qq)
        {
            return await _context.FindAsync<User>(qq);
        }

        internal async Task<MsgToken> CheckMsgToken(string token) => await _context.MsgToken.FindAsync(token);
    }
}
