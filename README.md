# NatsDistributedCache

NatsDistributedCache is a high-performance, distributed caching library that uses NATS as its backend store. It is designed to be fast, scalable, and resilient, making it an ideal choice for systems that require efficient state sharing or caching strategies. This library is a part of the [Apollo](https://github.com/rickdotnet/Apollo) ecosystem, which offers a suite of tools and libraries built around NATS technology.

## Features

- Leverages the lightning-fast NATS messaging system for cache operations.
- Implements the `IDistributedCache` interface for easy integration with .NET applications.
- Supports both synchronous and asynchronous cache operations.
- Utilizes MessagePack for efficient serialization of cache items.
- Provides an option to set cache expiration times for automatic invalidation.

## Getting Started

To use NatsDistributedCache in your project, follow these steps:

### Prerequisites

Ensure you have access to a [NATS server](https://docs.nats.io/running-a-nats-service/introduction/installation).

### Installation

Currently, as this library is in POC (Proof of Concept) stage. It will soon be available as a NuGet package.

### Usage

Here's a quick example of how to use NatsDistributedCache in a console application:

```csharp
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using NATS.Client.KeyValueStore;
using NatsDistributedCache;

// Initialize NATS connection options
var opts = new NatsOpts();
await using var nats = new NatsConnection(opts);

// Create NATS JetStream and KeyValue contexts
var js = new NatsJSContext(nats);
var kv = new NatsKVContext(js);

// Initialize the NatsDistributedCache
var natsCache = new NatsDistributedCache(kv);

// Set a value in the cache with an expiration time
await natsCache.SetAsync("my-key", Encoding.UTF8.GetBytes("my-value"), new DistributedCacheEntryOptions
{
    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
});

// Retrieve the value from the cache
var value = natsCache.Get("my-key");
Console.WriteLine($"[GET] {Encoding.UTF8.GetString(value)}");
```

### Contributing

This is an early POC, contributions are welcome. Please feel free to submit issues and pull requests to the repository.

## License

This project is licensed under the MIT License - see the LICENSE file for details.