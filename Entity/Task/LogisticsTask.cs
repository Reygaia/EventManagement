using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Entity.Task
{
    public class LogisticsTask : BaseTask
    {
        [BsonConstructor]
        public LogisticsTask()
        {
        }

        public string TransportationDetails { get; set; }
        [BsonExtraElements]
        public BsonDocument? LogisticsExtraElements { get; set; }
    }
}
