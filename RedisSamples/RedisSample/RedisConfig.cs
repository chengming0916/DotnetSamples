using System.Text.Json.Serialization;

class RedisConfig
{
    [JsonPropertyName("Host")]
    public string Host { get; set; }

    [JsonPropertyName("Db")]
    public int? Db { get; set; } = 0;

    [JsonPropertyName("Password")]
    public string Password { get; set; }
}