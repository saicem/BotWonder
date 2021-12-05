using BotWonder.Data;
using BotWonder.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;

namespace BotWonder.BotServer.Controllers
{
    /// <summary>
    /// 绑定宿舍
    /// </summary>
    [CmdRoute(Command = "绑定宿舍{room}", Priority = 1)]
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

        /// <summary>
        /// 参数验证失败时的返回值
        /// </summary>
        public override void OnValidationError()
        {
            ReplyTextMsg($"绑定宿舍格式:\n{HelpContent.BindRoomCommand}");
        }

        /// <summary>
        /// 文本消息处理
        /// </summary>
        /// <returns></returns>
        [FriendText, GroupText]
        private async Task TextMsgHandler([MaxLength(10), MinLength(6)] string room)
        {
            // TODO 优化，好像只需要qq号
            var user = await db.GetUser((long)FromQQ);
            if (user == null)
            {
                ReplyTextMsg($"请先私聊机器人绑定学号\n格式:\n{HelpContent.BindStuCommand}");
                return;
            }
            var retMsg = await db.BindRoomDb((long)FromQQ, room);
            ReplyTextMsg(retMsg);
        }
    }
}
