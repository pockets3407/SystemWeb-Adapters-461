// See https://aka.ms/new-console-template for more information
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using GrpcService1;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Channels;


var defaultMethodConfig = new MethodConfig
{
    Names = { MethodName.Default },
    //RetryPolicy = new RetryPolicy
    //{
    //    MaxAttempts = 5,
    //    InitialBackoff = TimeSpan.FromSeconds(1),
    //    MaxBackoff = TimeSpan.FromSeconds(5),
    //    BackoffMultiplier = 1.5,
    //    RetryableStatusCodes = { StatusCode.Unavailable }
    //}
};

var grpcChannel = GrpcChannel.ForAddress("http://localhost:5033", new GrpcChannelOptions()
{
    ServiceConfig = new ServiceConfig { MethodConfigs = { defaultMethodConfig } },
    HttpHandler = new SocketsHttpHandler()
    {
        PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1),
        EnableMultipleHttp2Connections = true,
    },
    LoggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddDebug();
        builder.SetMinimumLevel(LogLevel.Trace);
    })
});
var client = new Greeter.GreeterClient(grpcChannel);

try
{
    var result = client.SayHello(new HelloRequest() { Name = "World" });
    Console.WriteLine(result.Message);
}
catch (Exception ex)
{
    Debug.WriteLine(ex);
}
Console.ReadKey();
//var list = Enumerable.Range(0, 100);

//var stream = client.SayHello();

//var channel = Channel.CreateUnbounded<string>(new UnboundedChannelOptions() { SingleReader = true });
//var action = async (CancellationToken cancellationToken) =>
//{
//    await foreach (var message in channel.Reader.ReadAllAsync(cancellationToken))
//    {
//        try
//        {
//            await stream.RequestStream.WriteAsync(new HelloRequest() { Name = message ?? string.Empty }, cancellationToken);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.ToString());
//        }
//    }
//};



//var test = Task.Run(async () => await action(new CancellationTokenSource().Token));

//Parallel.ForEach(list, (i, cancellationToken) =>
//{
//    // always returns true, since this is unbounded
//    _ = channel.Writer.TryWrite(i.ToString());
//});

//Console.WriteLine("DONE");
//Console.ReadKey();


//channel.Writer.Complete();
//await stream.RequestStream.CompleteAsync();
//await grpcChannel.ShutdownAsync();