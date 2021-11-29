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
    [YukinoshitaController(Command = "课表", MatchMethod = CommandMatchMethod.StartWith, Mode = HandleMode.Break, Priority = 3)]
    public class CoursePicController
    {
        public DbHandler db;
        public WebQuery web;


        public CoursePicController(DbHandler db, WebQuery web)
        {
            this.db = db;
            this.web = web;
        }

        [FriendText, GroupText]
        public async Task CommonFunc(TextMessage message)
        {
            var senderQq = message.SenderInfo.FromQQ;
            var user = await db.GetUser((long)senderQq);
            if (user == null)
            {
                message.ReplyTextMsg($"请私聊机器人进行绑定\n{HelpContent.BindStuCommand}");
                return;
            }
            var match = Regex.Match(message.Content, "^课表\\s*(\\d+)$");
            int weekOrder;
            if (match.Success)
            {
                weekOrder = int.Parse(match.Groups[1].Value);
                if (weekOrder >= 20 || weekOrder <= 0)
                {
                    message.ReplyTextMsg("参数应在 1~20");
                    return;
                }
            }
            else
            {
                // 避免 课表在哪里看？ 这样的话触发机器人
                if (message.Content.Length != 2)
                {
                    return;
                }
                weekOrder = (DateTime.Today.Subtract(new DateTime(2021, 9, 5)).Days - 1) / 7 + 1;
            }
            var res = await web.QueryCoursePic(user, weekOrder);
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
            var link = $"http://api.saicem.top{res.Data}";
            message.Reply(new PictureMessageRequest(new Uri(link)));
        }
    }
}
