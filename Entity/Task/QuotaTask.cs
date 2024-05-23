using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entity.Task
{
    public class QuotaTask : BaseTask
    {
        public int maxCount { get; set; }
        private int count { get; set; } = 0;
        public int Count
        {
            get => count; set
            {
                count = value;
                IsCompleted = (count == maxCount);
            }
        }
        [BsonExtraElements]
        public BsonDocument? QuotaExtraElements { get; set; }
        [BsonConstructor]
        public QuotaTask() { }

    }
}
