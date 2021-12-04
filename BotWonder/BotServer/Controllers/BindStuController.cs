using BotWonder.DAO;
using BotWonder.Data;
using BotWonder.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Extensions;

namespace BotWonder.BotServer.Controllers
{
    /// <summary>
    /// 绑定学号 以能够进行基本的功能
    /// </summary>
    [CmdRoute(Command = "绑定_{username}_{password}", Priority = 4)]
    public class BindStuController : BotControllerBase
    {
        private readonly DbHandler db;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="db"></param>
        public BindStuController(DbHandler db)
        {
            this.db = db;
        }

        /// <summary>
        /// 返回匹配失败的提示信息
        /// </summary>
        public override void OnValidationError()
        {
            ReplyTextMsg($"绑定失败 格式错误\n参考格式如下:\n{HelpContent.BindStuCommand}");
        }

        /// <summary>
        /// 好友消息处理
        /// </summary>
        /// <param name="username">教务处用户名</param>
        /// <param name="password">教务处密码</param>
        /// <returns></returns>
        [FriendText]
        public async Task FriendTextMsgHandler([RegularExpression(@"\d{13}")] string username, string password)
        {
            // TODO 账号有效性验证
            await db.BindStuDb(new User
            {
                Qq = (long)Message.SenderInfo.FromQQ,
                Username = username,
                Password = password,
            });
            Message.ReplyText("绑定成功");
        }

        /// <summary>
        /// 群消息处理
        /// </summary>
        /// <returns></returns>
        [GroupText]
        public Task GroupTextMsgHandler()
        {
            ReplyTextMsg("请私聊机器人进行绑定");
            return Task.CompletedTask;
        }
    }
}
