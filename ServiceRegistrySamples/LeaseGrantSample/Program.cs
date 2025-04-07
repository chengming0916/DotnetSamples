using dotnet_etcd;
using Etcdserverpb;
using Google.Protobuf;

namespace LeaseGrantSample
{
    internal class Program
    {
        const string SERVICE_NAME = "TEST1";
        const string HOST = "127.0.0.1";
        const ushort PORT = 8080;

        static void Main(string[] args)
        {
            string serviceKey = $"Service/{SERVICE_NAME}/{HOST}";

            int number = 0;
            long leaseId = 0;

            var etcdClient = new EtcdClient("127.0.0.1");
            var grantResponse = etcdClient.LeaseGrant(new LeaseGrantRequest { TTL = 10 });
            leaseId = grantResponse.ID;
            etcdClient.Put(new PutRequest
            {
                Key = ByteString.CopyFromUtf8(serviceKey),
                Value = ByteString.CopyFromUtf8($"{HOST}:{PORT}"),
                Lease = leaseId,
            });

            etcdClient.LeaseKeepAlive(leaseId, CancellationToken.None);

            Task.Run(() =>
            {
                var watchReqest = new WatchRequest()
                {
                    CreateRequest = new WatchCreateRequest()
                    {
                        Key = ByteString.CopyFromUtf8(serviceKey),
                        WatchId = leaseId,
                    }
                };
                etcdClient.Watch(watchReqest, response =>
                {
                    foreach (var item in response.Events)
                    {
                        Console.WriteLine($"{item.Kv.Key.ToStringUtf8()}:{item.Kv.Value.ToStringUtf8()}:{item.Type}");
                    }
                });
            });

            while (true)
            {
                var readKey = Console.ReadKey().Key;

                if (readKey == ConsoleKey.Escape) break;

                if (readKey == ConsoleKey.A)
                {
                    etcdClient.Put(new PutRequest
                    {
                        Key = ByteString.CopyFromUtf8(serviceKey),
                        Value = ByteString.CopyFromUtf8($"{HOST}:{PORT}"),
                        Lease = leaseId
                    });
                }

                if (readKey == ConsoleKey.B)
                {
                    var leaseRequest = new LeaseRevokeRequest { ID = leaseId };
                    etcdClient.LeaseRevoke(leaseRequest);
                }
            }
        }
    }
}
