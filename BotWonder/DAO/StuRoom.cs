using System.ComponentModel.DataAnnotations;

namespace BotWonder.DAO
{
    /// <summary>
    /// 电表
    /// </summary>
    public class StuRoom
    {
        /// <summary>
        /// 寝室的名称
        /// </summary>
        [Key]
        public string RoomName { get; set; }

        /// <summary>
        /// 寝室的电表ID 或是 Roomno
        /// </summary>
        public string MeterId { get; set; }

        /// <summary>
        /// 寝室所在的区域 (余区，马区)
        /// </summary>
        public string Region { get; set; }
    }
}
