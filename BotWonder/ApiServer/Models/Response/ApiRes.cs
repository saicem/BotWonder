using System.Text.Json.Serialization;

namespace BotWonder.Models.Response
{
    public class ApiRes
    {
        [JsonPropertyName("ok")]
        public bool Ok { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }
    }
}
