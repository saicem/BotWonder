namespace BotWonder.Models.Request
{
    /// <summary>
    /// 用于接受请求JSON的实体
    /// </summary>
    public class UserJwcRequest
    {
        /// <summary>
        /// QQ号
        /// </summary>
        public long Qq { get; set; }

        /// <summary>
        /// 教务处的用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 教务处的密码
        /// </summary>
        public string Password { get; set; }
    }
}
