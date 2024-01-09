using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService1;

namespace GrpcService1.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        //public override async Task<HelloReply> SayHello(IAsyncStreamReader<HelloRequest> requestStream, ServerCallContext context)
        //{
        //    try
        //    {
        //        await foreach (var request in requestStream.ReadAllAsync(context.CancellationToken))
        //        {
        //            _logger.LogInformation(request.Name);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    return new HelloReply()
        //    {
        //        Message = "done"
        //    };
        //}

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply() { Message = $"Hello {request.Name}!" });
        }
    }
}