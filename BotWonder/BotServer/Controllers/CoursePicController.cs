using BotWonder.Data;
using BotWonder.Services;
using System;
using System.ComponentModel.DataAnnotations;
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
    [CmdRoute(Command = "课表_[week]", Priority = 3)]
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
        public async Task TextMsgHandler([Range(0, 20, ErrorMessage = "课表周数参数应在 0~20")] int week = 0)
        {
            var user = await db.GetUser((long)FromQQ);
            if (user == null)
            {
                ReplyTextMsg($"请私聊机器人进行绑定\n{HelpContent.BindStuCommand}");
                return;
            }
            if (week == 0)
            {
                week = (DateTime.Today.Subtract(new DateTime(2021, 9, 5)).Days - 1) / 7 + 1;
            }
            var res = await web.QueryCoursePic(user, week);
            if (res == null)
            {
                ReplyTextMsg("服务器错误,等待修复");
                return;
            }
            if (!res.Ok)
            {
                ReplyTextMsg("查询失败，检查账号密码正确性");
                return;
            }
            var link = $"http://api.saicem.top{res.Data}";
            ReplyPictureMsg(new Uri(link));
        }
    }
}
