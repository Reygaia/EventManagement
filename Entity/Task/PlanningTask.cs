using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Entity.Task
{
    public class PlanningTask : BaseTask
    {
        [BsonConstructor]
        public PlanningTask()
        {
        }

        public string Venue { get; set; } 
        [BsonExtraElements]
        public BsonDocument? PlanningExtraElements { get; set; }
    }
}
