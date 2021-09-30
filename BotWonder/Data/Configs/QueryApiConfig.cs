namespace BotWonder.Data.Configs
{
    public class QueryApiConfig
    {
        public const string Position = "ApiPath";
        public Zhxg Zhxg { get; set; }
        public Jwc Jwc { get; set; }
        public Cwsf Cwsf { get; set; }
    }

    public class Zhxg
    {
        public string HealthCheck { get; set; }
    }

    public class Jwc
    {
        public string CourseCal { get; set; }
        public string CoursePic { get; set; }
    }

    public class Cwsf
    {
        public string ElectricFee { get; set; }
    }
}


