using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Entity.Task
{
    public class MarketingTask : BaseTask
    {
        [BsonConstructor]
        public MarketingTask()
        {
        }

        public string CampaignType { get; set; }
        [BsonExtraElements]
        public BsonDocument? MarketingExtraElements { get; set; }
    }
}
