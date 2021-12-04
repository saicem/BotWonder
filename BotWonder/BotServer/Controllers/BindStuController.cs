using BotWonder.DAO;
using BotWonder.Data;
using BotWonder.Services;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Extensions;

namespace BotWonder.BotServer.Controllers
{
    /// <summary>
    /// 绑定学号 以能够进行基本的功能
    /// </summary>
    [CmdRoute(Command = "绑定_{username}_{password}", Priority = 4)]
    public class BindStuController : BotControllerBase
    {
        private readonly DbHandler db;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="db"></param>
        public BindStuController(DbHandler db)
        {
            this.db = db;
        }

        [FriendText]
        public async Task FriendTextMsgHandler(string username, string password)
        {
            //var match = Regex.Match(Message.Content, "绑定\\s+(\\d+?)\\s+(.+)");

            //if (match.Success)
            //{
            //    // TODO 账号有效性验证
            //    await db.BindStuDb(new User
            //    {
            //        Qq = (long)Message.SenderInfo.FromQQ,
            //        Username = match.Groups[1].Value,
            //        Password = match.Groups[2].Value,
            //    });
            //    Message.ReplyTextMsg("绑定成功");
            //}
            //else
            //{
            //    Message.ReplyTextMsg($"绑定失败 格式错误\n参考格式如下:\n{HelpContent.BindStuCommand}");
            //}

            // TODO 账号有效性验证
            await db.BindStuDb(new User
            {
                Qq = (long)Message.SenderInfo.FromQQ,
                Username = username,
                Password = password,
            });
            Message.ReplyTextMsg("绑定成功");
        }

        [GroupText]
        public Task GroupTextMsgHandler(TextMessage message)
        {
            message.ReplyTextMsg("请私聊机器人进行绑定");
            return Task.CompletedTask;
        }
    }
}
