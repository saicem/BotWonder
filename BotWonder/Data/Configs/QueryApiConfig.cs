namespace BotWonder.Data.Configs
{
    /// <summary>
    /// 需要查询的API的配置列表
    /// </summary>
    public class QueryApiConfig
    {
        /// <summary>
        /// 需要绑定的配置在配置JSON中的位置
        /// </summary>
        public const string Position = "ApiPath";

        /// <inheritdoc/>
        public Zhxg Zhxg { get; set; }

        /// <inheritdoc/>
        public Jwc Jwc { get; set; }

        /// <inheritdoc/>
        public Cwsf Cwsf { get; set; }
    }

    /// <summary>
    /// 智慧学工相关
    /// </summary>
    public class Zhxg
    {
        /// <summary>
        /// 健康填报
        /// </summary>
        public string HealthCheck { get; set; }
    }

    /// <summary>
    /// 教务处
    /// </summary>
    public class Jwc
    {
        /// <summary>
        /// 课表日程
        /// </summary>
        public string CourseCal { get; set; }
        
        /// <summary>
        /// 课表图片
        /// </summary>
        public string CoursePic { get; set; }
    }

    /// <summary>
    /// 缴费系统
    /// </summary>
    public class Cwsf
    {
        /// <summary>
        /// 马区电费查询
        /// </summary>
        public string ElectricMa { get; set; }

        /// <summary>
        /// 余区电费查询
        /// </summary>
        public string ElectricYu { get; set; }
    }
}


