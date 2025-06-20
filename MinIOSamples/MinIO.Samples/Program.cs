using Minio;
using Minio.DataModel.Args;

namespace MinIO.Samples;

class Program
{
    const string SERVER_ENDPOINT = "minio.example.io";
    const string ACCESS_KEY = "your-access";
    const string SECRET_KEY = "your-secret";
    const string BUCKET_NAME = "my-bucket";


    static void Main(string[] args)
    {
        var minioClient = new MinioClient()
            .WithEndpoint(SERVER_ENDPOINT)
            .WithCredentials(ACCESS_KEY, SECRET_KEY)
            .Build();

        var bucketExists = minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(BUCKET_NAME)).Result;
        if (!bucketExists)
        {
            minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(BUCKET_NAME)).Wait();
            Console.WriteLine($"Bucket '{BUCKET_NAME}' created.");
        }

        string objectName = "example.txt";            // 存储在 MinIO 中的对象名称
        string fileName = "path/to/your/example.txt"; // 本地文件路径

        // 上传
        minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(BUCKET_NAME)
            .WithObject(objectName)
            .WithFileName(fileName)).Wait();
        Console.WriteLine($"Object '{objectName}' uploaded to bucket '{BUCKET_NAME}'.");

        // 下载
        minioClient.GetObjectAsync(new GetObjectArgs()
            .WithBucket(BUCKET_NAME)
            .WithObject(objectName)
            //.WithCallbackStream(stream =>
            //{
            //    using (var reader = new StreamReader(stream))
            //    {
            //        string content = reader.ReadToEnd();
            //        Console.WriteLine($"Content of '{objectName}':\n{content}");
            //    }
            //})
            .WithFile(fileName)).Wait();
        Console.WriteLine($"Object '{objectName}' downloaded from bucket '{BUCKET_NAME}' to '{fileName}'.");

        // 删除对象
        minioClient.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(BUCKET_NAME)
            .WithObject(objectName)).Wait();
    }
}