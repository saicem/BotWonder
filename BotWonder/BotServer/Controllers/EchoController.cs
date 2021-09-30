using System;
using System.Threading.Tasks;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Data.OpqApi;
using YukinoshitaBot.Extensions;

namespace BotWonder.BotServer.Controllers
{
    /// <summary>
    /// 复读控制器
    /// </summary>
    [YukinoshitaController(Command = "echo", MatchMethod = CommandMatchMethod.StartWith, Mode = HandleMode.Break, Priority = 0)]
    public class EchoController : IBotController
    {
        public Task FriendPicMsgHandler(PictureMessage message)
        {
            throw new NotImplementedException();
        }

        public Task FriendTextMsgHandler(TextMessage message)
        {
            message.ReplyTextMsg(message.Content[5..]);
            return Task.CompletedTask;
        }

        public Task GroupPicMsgHandler(PictureMessage message)
        {
            throw new NotImplementedException();
        }

        public Task GroupTextMsgHandler(TextMessage message)
        {
            message.ReplyTextMsg(message.Content[5..]);
            return Task.CompletedTask;
        }
    }
}
