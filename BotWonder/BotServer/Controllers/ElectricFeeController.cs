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
    [YukinoshitaController(Command = "电费", MatchMethod = CommandMatchMethod.StartWith, Mode = HandleMode.Break, Priority = 6)]
    public class ElectricFeeController : IBotController
    {
        /// <summary>
        /// 网络请求
        /// </summary>
        private WebQuery web;
        /// <summary>
        /// 数据库查询
        /// </summary>
        private DbHandler db;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="web"></param>
        public ElectricFeeController(WebQuery web, DbHandler db)
        {
            this.web = web;
            this.db = db;
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

        /// <summary>
        /// 共有方法
        /// </summary>
        /// <param name="message"></param>
        public async Task CommonFunc(TextMessage message)
        {
            if ((DateTime.Now.Hour == 23 && DateTime.Now.Minute >= 20) || (DateTime.Now.Hour == 0 && DateTime.Now.Minute <= 10))
            {
                message.ReplyTextMsg("缴费系统正在维护\n请在 23:20~00:10 之外进行查询");
                return;
            }
            var sender_qq = message.SenderInfo.FromQQ;
            var user = await db.GetUser((long)sender_qq);
            if (user == null)
            {
                message.ReplyTextMsg("需要绑定学号及宿舍");
                return;
            }
            if (!user.IsBindRoom())
            {
                message.ReplyTextMsg("需要先绑定宿舍:\n" +
                    "绑定宿舍 (东1-101|西2-201|狮城公寓-302|慧1-103|越1-435|智4-409|北5-505|学海6-724)");
                return;
            }
            var res = await web.QueryEletricFee(user.Username, user.Password, user.MeterId);
            if (res == null)
            {
                message.ReplyTextMsg("服务器错误");
                return;
            }
            var data = res.Data;
            var root = JsonDocument.Parse(data.ToString()).RootElement;
            // TODO 获取数据
            // 剩余电量
            var hasRemainPower = root.TryGetProperty("remainPower", out var docRemainPower);
            // 剩余电量单位
            var hasRemainName = root.TryGetProperty("remainName", out var docRemainName);
            // 剩余电费
            //var hasRemainFee = root.TryGetProperty("meterOverdue", out var docRemainFee);
            if (hasRemainPower && hasRemainName){
                message.ReplyTextMsg($"剩余电量:{docRemainPower.GetString()}{docRemainName.GetString()}");
                return;
            }
            else
            {
                // TODO 别的日志方式
                Console.WriteLine((string)data);
                message.ReplyTextMsg("未能获取电费");
                return;
            }
        }
    }
}
