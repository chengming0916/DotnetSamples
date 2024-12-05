using System.Text.Json.Serialization;

namespace DotNettySamples.Client
{
    class Config
    {
        [JsonPropertyName("Server")]
        public ServerConfig Server { get; set; }
    }

    class ServerConfig
    {
        [JsonPropertyName("Host")]
        public string Host { get; set; }

        [JsonPropertyName("Port")]
        public ushort Port { get; set; }
    }
}
