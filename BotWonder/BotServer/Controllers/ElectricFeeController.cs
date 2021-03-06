using BotWonder.Services;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Extensions;

namespace BotWonder.BotServer.Controllers
{
    /// <summary>
    /// 电费查询
    /// </summary>
    [StrictRoute(Command = "电费", Priority = 6)]
    public class ElectricFeeController : BotControllerBase
    {
        /// <summary>
        /// 网络请求
        /// </summary>
        private readonly WebQuery web;

        /// <summary>
        /// 数据库查询
        /// </summary>
        private readonly DbHandler db;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="web"></param>
        /// <param name="db"></param>
        public ElectricFeeController(WebQuery web, DbHandler db)
        {
            this.web = web;
            this.db = db;
        }

        /// <summary>
        /// 共有方法
        /// </summary>
        [FriendText, GroupText]
        public async Task TextMsgHandler()
        {
            var msg = Message as TextMessage;
            if ((DateTime.Now.Hour == 23 && DateTime.Now.Minute >= 20) || (DateTime.Now.Hour == 0 && DateTime.Now.Minute <= 10))
            {
                msg.ReplyText("缴费系统正在维护\n请在 23:20~00:10 之外进行查询");
                return;
            }
            var sender_qq = msg.SenderInfo.FromQQ;
            var user = await db.GetUser((long)sender_qq);
            if (user == null)
            {
                msg.ReplyText("需要绑定学号及宿舍");
                return;
            }
            if (!user.IsBindRoom())
            {
                msg.ReplyText("需要先绑定宿舍:\n" +
                    "绑定宿舍 (东1-101|西2-201|狮城公寓-302|慧1-103|越1-435|智4-409|北5-505|学海6-724)");
                return;
            }
            var stuRoom = await db.GetStuRoom(user.RoomName);
            var res = await web.QueryEletricFee(user.Username, user.Password, stuRoom.MeterId, stuRoom.Region);
            if (res == null)
            {
                msg.ReplyText("服务器错误");
                return;
            }
            if (res.Ok == false)
            {
                msg.ReplyText("绑定信息错误，查询失败！");
                return;
            }
            msg.ReplyText(res.Data?.ToString());
        }
    }
}
