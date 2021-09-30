using System.ComponentModel.DataAnnotations;

namespace BotWonder.DAO
{
    public class MsgToken
    {
        // todo 如何生成唯一随机数
        [Key]
        public string Token { get; set; }
        public MsgType MsgType { get; set; }
        /// <summary>
        /// 发送对象 QQ号 或 QQ 群号
        /// </summary>
        public long TargetId { get; set; }
    }

    public enum MsgType
    {
        FriendMsg = 0,
        GroupMsg = 1,
    }
}
