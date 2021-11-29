using System.Text.Json.Serialization;

namespace BotWonder.DTO
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
