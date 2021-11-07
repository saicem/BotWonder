using System.ComponentModel.DataAnnotations;

namespace BotWonder.DAO
{
    /// <summary>
    /// 电表
    /// </summary>
    public class ElectricityMeter
    {
        /// <summary>
        /// 寝室的名称
        /// </summary>
        [Key]
        public string RoomName { get; set; }

        /// <summary>
        /// 寝室的电表ID
        /// </summary>
        public string MeterId { get; set; }
    }
}
