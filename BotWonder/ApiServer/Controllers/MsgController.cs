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
//            //var msgToken = await db.CheckMsgToken(msg.Token);
//            //if (msgToken == null)
//            //{
//            //    return new BadRes("错误的token值");
//            //}
//            var textMsgRequest = new TextMessageRequest(msg.Message);
//            var IsMatch = true;
//            opqApi.AddRequest(msg.MsgType switch
//            {
//                MsgType.Friend => textMsgRequest.SendToFriend(msg.TargetId),
//                MsgType.Group => textMsgRequest.SendToGroup(msg.TargetId),
//                _ => throw new NotImplementedException(),
//            }
//            );
//            if (IsMatch)
//            {
//                return new GoodRes("成功发送");
//            }
//            return new BadRes("不合法的消息类型");
//        }
//    }
//}
