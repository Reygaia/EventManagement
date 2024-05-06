using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entity
{
    public enum TaskType
    {
        Check,
        Count
    }

    public class BaseTask
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public TaskType Type { get; set; }
        public DateTime Deadline { get; set; }
    }
}
