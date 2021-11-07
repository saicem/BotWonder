using System;
using System.ComponentModel.DataAnnotations;

namespace BotWonder.DAO
{
    /// <summary>
    /// 消息
    /// </summary>
    public class MsgToken
    {
        /// <summary>
        /// Token 用于唯一识别
        /// </summary>
        [Key]
        public string Token { get; set; }
        
        /// <summary>
        /// 消息类型 群消息或是好友消息
        /// </summary>
        public MsgType MsgType { get; set; }

        /// <summary>
        /// 发送对象 QQ号 或 QQ 群号
        /// </summary>
        public long TargetId { get; set; }

        /// <summary>
        /// 初始化消息
        /// </summary>
        public MsgToken()
        {
            Token = GetRandStr(5);
        }

        private static string GetRandStr(int length)
        {
            var ticks = DateTime.Now.Ticks;
            string s = string.Empty;
            var str = "0123456789" + "abcdefghijklmnopqrstuvwxyz" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new(BitConverter.ToInt32(b, 0));
            while (ticks > 0)
            {
                s += str.Substring((int)(ticks % 62), 1);
                ticks /= 62;
            }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }
    }

    /// <summary>
    /// 发送的消息类型
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 好友消息
        /// </summary>
        FriendMsg = 0,

        /// <summary>
        /// 群消息
        /// </summary>
        GroupMsg = 1,
    }
}
