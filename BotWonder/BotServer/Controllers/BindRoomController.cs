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
    /// 绑定宿舍
    /// </summary>
    [YukinoshitaController(Command = "绑定宿舍", MatchMethod = CommandMatchMethod.StartWith, Mode = HandleMode.Break, Priority = 1)]
    public class BindRoomController : BotControllerBase
    {
        private DbHandler db;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="db"></param>
        public BindRoomController(DbHandler db)
        {
            this.db = db;
        }

        [FriendText, GroupText]
        private async Task CommonFunc(TextMessage message)
        {
            var senderQq = message.SenderInfo.FromQQ;
            // TODO 优化，好像只需要qq号
            var user = await db.GetUser((long)senderQq);
            if (user == null)
            {
                message.ReplyTextMsg($"请先私聊机器人绑定学号\n格式:\n{HelpContent.BindStuCommand}");
                return;
            }
            var match = Regex.Match(message.Content, "绑定宿舍\\s*(.{6,10})$");
            if (!match.Success)
            {
                message.ReplyTextMsg("绑定宿舍格式:\n" +
                $"{HelpContent.BindRoomCommand}");
                return;
            }
            var roomName = match.Groups[1].Value;
            var retMsg = await db.BindRoomDb((long)senderQq, roomName);
            message.ReplyTextMsg(retMsg);
        }
    }
}
