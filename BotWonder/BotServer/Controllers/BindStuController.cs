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
    [YukinoshitaController(Command = "绑定", MatchMethod = CommandMatchMethod.StartWith, Mode = HandleMode.Break, Priority = 4)]
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
        public async Task FriendTextMsgHandler(TextMessage message)
        {
            var match = Regex.Match(message.Content, "绑定\\s+(\\d+?)\\s+(.+)");
            if (match.Success)
            {
                // TODO 账号有效性验证
                await db.BindStuDb(new User
                {
                    Qq = (long)message.SenderInfo.FromQQ,
                    Username = match.Groups[1].Value,
                    Password = match.Groups[2].Value,
                });
                message.ReplyTextMsg("绑定成功");
            }
            else
            {
                message.ReplyTextMsg($"绑定失败 格式错误\n参考格式如下:\n{HelpContent.BindStuCommand}");
            }
        }

        [GroupText]
        public Task GroupTextMsgHandler(TextMessage message)
        {
            message.ReplyTextMsg("请私聊机器人进行绑定");
            return Task.CompletedTask;
        }
    }
}
