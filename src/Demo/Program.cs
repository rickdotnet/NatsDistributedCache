// See https://aka.ms/new-console-template for more information

using System.Text;
using Apollo.Caching.Nats;
using Microsoft.Extensions.Caching.Distributed;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.KeyValueStore;

//var opts = new NatsOpts();
var opts = new NatsOpts { Url = "nats://nats.rhinostack.com:4222" };
await using var nats = new NatsConnection(opts);

var js = new NatsJSContext(nats);
var kv = new NatsKVContext(js);

var natsCache = new NatsDistributedCache(kv);
await natsCache.SetAsync("my-key", "my-value"u8.ToArray(), new DistributedCacheEntryOptions
{
    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
});

var value = natsCache.Get("my-key");
Console.WriteLine($"[GET] {Encoding.UTF8.GetString(value!)}");