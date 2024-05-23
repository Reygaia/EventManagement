using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entity.Task
{
    /*    public enum TaskType
        {
            Check,
            Count
        }*/

    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(ContentCreationTask),
                    typeof(LogisticsTask),
                    typeof(MarketingTask),
                    typeof(PlanningTask),
                    typeof(QuotaTask),
                    typeof(RegistrationTask),
                    typeof(VendorCoordinationTask))]
    [BsonIgnoreExtraElements]
    public class BaseTask
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public ObjectId? PhaseId { get; set; }
        public ObjectId? EventId { get; set; }
        public Guid CreatedBy { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; } = false;
        [BsonExtraElements]
        public BsonDocument? ExtraElements { get; set; }
    }
}
