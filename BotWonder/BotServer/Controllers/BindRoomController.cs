﻿using BotWonder.Services;
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
    public class BindRoomController : IBotController
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
            // TODO 优化，好像只需要qq号
            var user = await db.GetUser((long)senderQq);
            if (user == null)
            {
                message.ReplyTextMsg("请先私聊机器人绑定学号\n格式:\n绑定 学号 密码");
                return;
            }
            var match = Regex.Match(message.Content, "绑定宿舍\\s*(.{6,10})$");
            if (!match.Success)
            {
                message.ReplyTextMsg("绑定宿舍格式:\n" +
                "绑定宿舍 (东1-101|西2-201|狮城公寓-302|慧1-103|越1-435|智4-409|北5-505|学海6-724)");
                return;
            }
            var roomName = match.Groups[1].Value;
            var retMsg = await db.BindRoomDb((long)senderQq, roomName);
            message.ReplyTextMsg(retMsg);
        }
    }
}