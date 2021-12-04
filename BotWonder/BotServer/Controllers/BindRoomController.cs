﻿using BotWonder.Data;
using BotWonder.Services;
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
    [StartRoute(Command = "绑定宿舍", Priority = 1)]
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
        /// 文本消息处理
        /// </summary>
        /// <returns></returns>
        [FriendText, GroupText]
        private async Task TextMsgHandler()
        {
            var textMessage = Message as TextMessage;
            // TODO 优化，好像只需要qq号
            var user = await db.GetUser((long)FromQQ);
            if (user == null)
            {
                ReplyTextMsg($"请先私聊机器人绑定学号\n格式:\n{HelpContent.BindStuCommand}");
                return;
            }
            var match = Regex.Match(textMessage.Content, "绑定宿舍\\s*(.{6,10})$");
            if (!match.Success)
            {
                ReplyTextMsg($"绑定宿舍格式:\n{HelpContent.BindRoomCommand}");
                return;
            }
            var roomName = match.Groups[1].Value;
            var retMsg = await db.BindRoomDb((long)FromQQ, roomName);
            ReplyTextMsg(retMsg);
        }
    }
}
