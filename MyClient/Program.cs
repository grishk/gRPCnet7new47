// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Threading.Tasks;
using Grpc.Net.Client;
using gRPC_My;
using MyClient;

var timer = Stopwatch.StartNew();
//using var channel = GrpcChannel.ForAddress("http://localhost:5129");
using var channel = GrpcChannel.ForAddress("http://localhost:50051");
var client = new Greeter.GreeterClient(channel);
timer.Stop();

Console.WriteLine("Client created: " + timer.ElapsedMilliseconds);
timer.Start();

var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });

Console.WriteLine("Main Rpc call 1: " + reply.Message);
timer.Stop();

Console.WriteLine("Elapsed: " + timer.ElapsedMilliseconds);
Console.WriteLine("Second client");

new SeparateClient().GreaterCall("GreeterSecondClient");

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
