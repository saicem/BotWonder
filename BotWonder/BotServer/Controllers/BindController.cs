using BotWonder.DAO;
using BotWonder.Services;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Extensions;

namespace BotWonder.BotServer.Controllers
{
    [YukinoshitaController(Command = "绑定", MatchMethod = CommandMatchMethod.StartWith, Mode = HandleMode.Break, Priority = 1)]
    public class BindController : IBotController
    {
        private readonly DbHandler db;
        public BindController(DbHandler db)
        {
            this.db = db;
        }

        public Task FriendPicMsgHandler(PictureMessage message)
        {
            return Task.CompletedTask;
        }

        public async Task FriendTextMsgHandler(TextMessage message)
        {
            var match = Regex.Match(message.Content, "绑定\\s+(\\d+?)\\s+(.+)");
            if (match.Success)
            {
                // TODO 账号有效性验证
                await db.NewUser(new User
                {
                    Qq = (long)message.SenderInfo.FromQQ,
                    Username = match.Groups[1].Value,
                    Password = match.Groups[2].Value,
                });
                message.ReplyTextMsg("绑定成功");
            }
            else
            {
                message.ReplyTextMsg("绑定失败 格式错误\n参考格式如下:\n绑定 学号 密码");
            }
        }

        public Task GroupPicMsgHandler(PictureMessage message)
        {
            return Task.CompletedTask;
        }

        public Task GroupTextMsgHandler(TextMessage message)
        {
            message.ReplyTextMsg("请私聊机器人进行绑定");
            return Task.CompletedTask;
        }
    }
}
