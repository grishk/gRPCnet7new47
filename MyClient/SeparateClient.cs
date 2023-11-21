using System.Diagnostics;
using Grpc.Core;
using Grpc.Net.Client;
using gRPC_My;

namespace MyClient; 

public class SeparateClient :IDisposable{
    private readonly GrpcChannel _channel;
    private readonly Greeter.GreeterClient _client;
    public SeparateClient() {
        Console.WriteLine("Separate client constructor");
        var stopwatch = Stopwatch.StartNew();
        //_channel = GrpcChannel.ForAddress("http://localhost:5129");
        _channel = GrpcChannel.ForAddress("http://localhost:50051");
        _client = new Greeter.GreeterClient(_channel);
        stopwatch.Stop();
        Console.WriteLine("Constructir time: " + stopwatch.ElapsedMilliseconds);
    }

    public void GreaterCall(string greeting)
    {
        Console.WriteLine();
        var stopwatch = Stopwatch.StartNew();
        var reply = _client.SayHello(new HelloRequest { Name = greeting });
        stopwatch.Stop();
        Console.WriteLine("Separate call 1: " + stopwatch.ElapsedMilliseconds);
        Console.WriteLine("Rpc call 1" + reply.Message);
        Console.WriteLine();
        stopwatch.Restart();
        reply = _client.SayHello(new HelloRequest { Name = greeting });
        stopwatch.Stop();
        Console.WriteLine("Separate call 2: " + stopwatch.ElapsedMilliseconds);
        Console.WriteLine("Rpc call 2" + reply.Message);
    }

    public void Dispose() {
        _channel.Dispose();
    }
}