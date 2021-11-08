using BotWonder.Data;
using BotWonder.Services;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Extensions;

namespace BotWonder.BotServer.Controllers
{
    /// <summary>
    /// 教务处相关的控制器
    /// </summary>
    [YukinoshitaController(Command = "课表日程", MatchMethod = CommandMatchMethod.StartWith, Mode = HandleMode.Break, Priority = 2)]
    public class CourseCalController : IBotController
    {
        public DbHandler db;
        public WebQuery web;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="db"></param>
        /// <param name="web"></param>
        public CourseCalController(DbHandler db, WebQuery web)
        {
            this.db = db;
            this.web = web;
        }

        /// <inheritdoc/>
        public Task FriendPicMsgHandler(PictureMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task FriendTextMsgHandler(TextMessage message)
        {
            await CommonFunc(message);
        }

        /// <inheritdoc/>
        public Task GroupPicMsgHandler(PictureMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task GroupTextMsgHandler(TextMessage message)
        {
            await CommonFunc(message);
        }

        private async Task CommonFunc(TextMessage message)
        {
            var senderQq = message.SenderInfo.FromQQ;
            var user = await db.GetUser((long)senderQq);
            if (user == null)
            {
                message.ReplyTextMsg($"请私聊机器人进行绑定，格式:\n{HelpContent.BindStu}");
                return;
            }
            var res = await web.QueryCourseCal(user);
            if (res == null)
            {
                message.ReplyTextMsg("服务器错误,等待修复");
                return;
            }
            if (!res.Ok)
            {
                message.ReplyTextMsg("查询失败，检查账号密码正确性");
                return;
            }
            message.ReplyTextMsg(TextFormat(res.Data));
        }

        private static string TextFormat(object url)
        {
            return $"下载后将文件导入日历即可\n链接: {url}";
        }
    }
}
