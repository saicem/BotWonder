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
    [CmdRoute(Command = "echo{content}", Priority = 0)]
    public class EchoController : BotControllerBase
    {
        /// <summary>
        /// 文本消息处理
        /// </summary>
        /// <param name="content">复读的内容</param>
        [FriendText, GroupText]
        public void TextMsgHandler(string content)
        {
            content = content.Trim();
            if (content.Length > 0)
            {
                ReplyTextMsg(content);
            }
        }
    }
}
