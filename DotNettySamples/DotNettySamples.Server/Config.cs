using System.Text.Json.Serialization;

namespace DotnettySamples.Server
{
    public class Config
    {
        [JsonPropertyName("Port")]
        public ushort Port { get; set; }
    }
}
