using BotWonder.Data;
using System.Threading.Tasks;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Extensions;

namespace BotWonder.BotServer.Controllers
{
    /// <summary>
    /// 帮助信息
    /// </summary>
    [YukinoshitaController(Command = "帮助", MatchMethod = CommandMatchMethod.Strict, Mode = HandleMode.Break, Priority = 5)]
    public class HelpController : IBotController
    {
        /// <inheritdoc/>
        public Task FriendPicMsgHandler(PictureMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task FriendTextMsgHandler(TextMessage message)
        {
            CommonFunc(message);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task GroupPicMsgHandler(PictureMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task GroupTextMsgHandler(TextMessage message)
        {
            CommonFunc(message);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 帮助信息一览
        /// </summary>
        /// <param name="message"></param>
        public void CommonFunc(TextMessage message)
        {
            var msg = "当前机器人开放以下指令\n" +
                "( ) 内的为必要参数\n" +
                "[ ] 内的为可选参数\n" +
                "有多个可选项时以 | 分割\n" +
                "1.绑定学号:\n" +
                $"{HelpContent.BindStu}\n" +
                "2.课表查询:\n" +
                $"{HelpContent.GetCourse}\n" +
                "3.课表日程，生成一个可导入日历的文件，导入后可在日程中查看课表:\n" +
                $"{HelpContent.GetCal}\n" +
                "4.绑定宿舍:\n" +
                $"{HelpContent.BindRoom}\n" +
                "5.电费查询:\n" +
                $"{HelpContent.GetEleFee}";
            message.ReplyTextMsg(msg);
        }
    }
}
