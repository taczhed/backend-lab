using Client.ResponseHandler;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Progress;

using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new Progressor.ProgressorClient(channel);

var progress = new Progress<int>(i => Console.WriteLine($"Progress: {i}%"));

var result = await ServerStreamingCallExample(client, progress);

Console.WriteLine("Preparing results...");
await Task.Delay(TimeSpan.FromSeconds(2));

foreach (var item in result.Items)
{
    Console.WriteLine(item);
}

Console.WriteLine("Shutting down");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

static ResponseProgress<HistoryResult, int> ServerStreamingCallExample(Progressor.ProgressorClient client, IProgress<int> progress)
{
    var call = client.RunHistory(new Empty());

    return GrpcProgress.Create(call.ResponseStream, progress);
}