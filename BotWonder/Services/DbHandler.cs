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

        /// <summary>
        /// 绑定学号和密码 如果不存在此用户则创建新的用户
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        internal async Task BindStuDb(User newUser)
        {
            var user = await FindUser(newUser.Qq);
            if (user == null)
            {
                newUser.Active();
                await _context.User.AddAsync(newUser);
            }
            else
            {
                user.BindStu(newUser.Username, newUser.Password);
                user.Active();
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="qq"></param>
        /// <returns></returns>
        internal async Task<User> GetUser(long qq)
        {
            var user = await FindUser(qq);
            if (user != null)
            {
                await UserActive(user);
            }
            return user;
        }

        /// <summary>
        /// 使用此函数需确保用户存在
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="roomName"></param>
        /// <returns>应返回给用户的消息</returns>
        internal async Task<string> BindRoomDb(long qq, string roomName)
        {
            var user = await FindUser(qq);
            if (user == null)
            {
                return "绑定宿舍前应先绑定学号";
            }
            await UserActive(user);
            var meterId = await GetMeterId(roomName);
            if (meterId == null)
            {
                return $"不存在该宿舍:{roomName}";
            }
            user.BindRoom(roomName);
            await _context.SaveChangesAsync();
            return "绑定宿舍成功";
        }

        internal async Task<StuRoom> GetStuRoom(string roomName) => await _context.FindAsync<StuRoom>(roomName);

        internal async Task<MsgToken> CheckMsgToken(string token) => await _context.FindAsync<MsgToken>(token);

        private async Task<User> FindUser(long qq) => await _context.FindAsync<User>(qq);

        private async Task UserActive(User user)
        {
            user.Active();
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 获取电表ID
        /// </summary>
        /// <param name="roomName"></param>
        /// <returns></returns>
        private async Task<string> GetMeterId(string roomName)
        {
            var roomInfo = await _context.StuRoom.FindAsync(roomName);
            return roomInfo?.MeterId;
        }
    }
}
