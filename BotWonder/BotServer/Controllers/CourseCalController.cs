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
    [StartRoute(Command = "课表日程", Priority = 2)]
    public class CourseCalController : BotControllerBase
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

        /// <summary>
        /// 文本消息处理
        /// </summary>
        /// <returns></returns>
        [FriendText, GroupText]
        private async Task TextMsgHandler()
        {
            var senderQq = Message.SenderInfo.FromQQ;
            var user = await db.GetUser((long)senderQq);
            if (user == null)
            {
                Message.ReplyText($"请私聊机器人进行绑定，格式:\n{HelpContent.BindStuCommand}");
                return;
            }
            var res = await web.QueryCourseCal(user);
            if (res == null)
            {
                Message.ReplyText("服务器错误,等待修复");
                return;
            }
            if (!res.Ok)
            {
                Message.ReplyText("查询失败，检查账号密码正确性");
                return;
            }
            Message.ReplyText(TextFormat(res.Data));
        }

        private static string TextFormat(object url)
        {
            return $"下载后将文件导入日历即可\n链接: {url}";
        }
    }
}
