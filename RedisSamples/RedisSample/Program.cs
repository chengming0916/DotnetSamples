using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

class Program
{
    private static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json", true, true);
        var config = builder.Build().GetSection("Redis").Get<RedisConfig>();

        config ??= new RedisConfig { Host = "127.0.0.1:6379", Db = 0 };
        // var options = ConfigurationOptions.Parse("127.0.0.1:6379,password=123456");
        var options = new ConfigurationOptions
        {
            DefaultDatabase = config.Db ?? 0,
            Password = config.Password,
        };
        var connection = ConnectionMultiplexer.Connect(options);


        // String
        connection.GetDatabase().StringSet("key", "value");
        connection.GetDatabase().StringSet("key", "value", TimeSpan.FromSeconds(10)); // 设置过期时间10s


        var val = connection.GetDatabase().StringGet("key");
        if (val.HasValue)
            Console.WriteLine(val.ToString());


        // List
        for (int i = 0; i < 10; i++)
        {
            connection.GetDatabase().ListLeftPush("list", i); // 从顶部插入
            // connection.GetDatabase().ListRightPush("list", i); // 从底部插入数据
        }

        var length = connection.GetDatabase().ListLength("list");

        var rightPop = connection.GetDatabase().ListRightPop("list"); // 从底部拿出数据
        var leftPop = connection.GetDatabase().ListLeftPop("list"); // 从顶部拿出数据

        var list = connection.GetDatabase().ListRange("list");

        // Hash
        var sampleData = new SampleData { };
        var json = JsonSerializer.Serialize(sampleData);
        connection.GetDatabase().HashSet("test", "hash-1", json);
        connection.GetDatabase().HashSet("test", "hash-2", json);

        // 获取单个对象
        val = connection.GetDatabase().HashGet("test", "test-1");
        if (val.HasValue)
        {
            string hash = val.ToString();
            sampleData = JsonSerializer.Deserialize<SampleData>(hash);
        }
        // 获取List
        var values = connection.GetDatabase().HashValues("test");
        var sampleDatas = new List<SampleData>();
        foreach (var item in values)
        {
            Console.WriteLine(item);
            sampleData = JsonSerializer.Deserialize<SampleData>(item.ToString());
            sampleDatas.Add(sampleData);
        }

        // 集合
    }
}

class SampleData
{

}