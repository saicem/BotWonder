//using BotWonder.DAO;
//using BotWonder.Models.Request;
//using BotWonder.Models.Response;
//using BotWonder.Services;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Threading.Tasks;
//using YukinoshitaBot.Data.OpqApi;
//using YukinoshitaBot.Services;

//namespace BotWonder.ApiServer.Controllers
//{
//    /// <summary>
//    /// 使用 Bot 发送消息
//    /// </summary>
//    [Route("api/bot/msg")]
//    [ApiController]
//    public class MsgController : ControllerBase
//    {
//        private readonly OpqApi opqApi;
//        private readonly DbHandler db;

//        /// <summary>
//        /// 对<see cref="MsgController"/>进行依赖注入
//        /// </summary>
//        /// <param name="opqApi"></param>
//        /// <param name="db"></param>
//        public MsgController(OpqApi opqApi, DbHandler db)
//        {
//            this.opqApi = opqApi;
//            this.db = db;
//        }

//        /// <summary>
//        /// 发送文本消息
//        /// </summary>
//        /// <param name="msg">消息内容</param>
//        /// <returns> <see cref="ApiRes"/> </returns>
//        /// <exception cref="NotImplementedException"></exception>
//        [HttpPost("send")]
//        public async Task<ApiRes> MsgSend(MsgIn msg)
//        {
//            var msgToken = await db.CheckMsgToken(msg.Token);
//            if (msgToken == null)
//            {
//                return new BadRes("错误的token值");
//            }
//            var textMsgRequest = new TextMessageRequest(msg.Message);
//            opqApi.AddRequest(msgToken.MsgType switch
//            {
//                MsgType.FriendMsg => textMsgRequest.SendToFriend(msgToken.TargetId),
//                MsgType.GroupMsg => textMsgRequest.SendToGroup(msgToken.TargetId),
//                _ => throw new NotImplementedException(),
//            }
//            );
//            return new GoodRes(msg.Message);
//        }
//    }
//}
