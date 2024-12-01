using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
namespace GrpcServer.Services
{
    public class TrainService : Train.TrainBase
    {
        private readonly TestTrainsContext db;
        private readonly ILogger<TrainService> _logger;

        public TrainService(TestTrainsContext db, ILogger<TrainService> logger)
        {
            this.db = db;
            _logger = logger;
        }

        public override Task<WagonListReply> GetListPath(Empty request, ServerCallContext context)
        {
            var listReply = new WagonListReply();

            var pathList = db.Paths.Select(item => new WagonReply {Id = item.Id, AsuNumber = item.AsuNumber, IdPark = item.IdPark }).ToList();

            listReply.Wagons.AddRange(pathList);
            return Task.FromResult(listReply);
        }
    }
}
