using BotWonder.Data;
using BotWonder.Services;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Data.OpqApi;
using YukinoshitaBot.Extensions;

namespace BotWonder.BotServer.Controllers
{
    /// <summary>
    /// 生成课表图片
    /// </summary>
    [StartRoute(Command = "课表", Priority = 3)]
    public class CoursePicController : BotControllerBase
    {
        public DbHandler db;
        public WebQuery web;


        public CoursePicController(DbHandler db, WebQuery web)
        {
            this.db = db;
            this.web = web;
        }

        /// <summary>
        /// 文本消息处理
        /// </summary>
        /// <returns></returns>
        [FriendText, GroupText]
        public async Task TextMsgHandler()
        {
            var msg = Message as TextMessage;
            var senderQq = msg.SenderInfo.FromQQ;
            var user = await db.GetUser((long)senderQq);
            if (user == null)
            {
                msg.ReplyText($"请私聊机器人进行绑定\n{HelpContent.BindStuCommand}");
                return;
            }
            var match = Regex.Match(msg.Content, "^课表\\s*(\\d+)$");
            int weekOrder;
            if (match.Success)
            {
                weekOrder = int.Parse(match.Groups[1].Value);
                if (weekOrder >= 20 || weekOrder <= 0)
                {
                    msg.ReplyText("参数应在 1~20");
                    return;
                }
            }
            else
            {
                // 避免 课表在哪里看？ 这样的话触发机器人
                if (msg.Content.Length != 2)
                {
                    return;
                }
                weekOrder = (DateTime.Today.Subtract(new DateTime(2021, 9, 5)).Days - 1) / 7 + 1;
            }
            var res = await web.QueryCoursePic(user, weekOrder);
            if (res == null)
            {
                msg.ReplyText("服务器错误,等待修复");
                return;
            }
            if (!res.Ok)
            {
                msg.ReplyText("查询失败，检查账号密码正确性");
                return;
            }
            var link = $"http://api.saicem.top{res.Data}";
            msg.Reply(new PictureMessageRequest(new Uri(link)));
        }
    }
}
