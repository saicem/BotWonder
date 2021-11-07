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
                "_ 需要在实际指令中以空格代替\n" +
                "1.绑定学号:\n" +
                "绑定_(学号)_(密码)\n" +
                "2.课表查询:\n" +
                "课表_[周数]\n" +
                "3.课表日程，生成一个可导入日历的文件，导入后可在日程中查看课表:\n" +
                "课表日程\n" +
                "4.绑定宿舍:\n" +
                "绑定宿舍_(东1-101|西2-201|狮城公寓-302|慧1-103|越1-435|智4-409|北5-505|学海6-724)\n" +
                "5.电费查询:\n" +
                "电费";
            message.ReplyTextMsg(msg);
        }
    }
}
