using Grpc.Net.Client;
using GrpcClient;


using var channel = GrpcChannel.ForAddress("https://localhost:7121");

var client = new Train.TrainClient(channel);

WagonListReply wagonListReply = client.GetListPath(new Google.Protobuf.WellKnownTypes.Empty());

foreach (var wagon in wagonListReply.Wagons)
{
    Console.WriteLine($"{wagon.Id} {wagon.AsuNumber} {wagon.IdPark}");
}

Console.ReadLine();