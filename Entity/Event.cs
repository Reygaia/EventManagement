using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entity
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string OwnerId { get; set; } = string.Empty;
        public int StaffCount { get; set; } = 0;
        public List<BaseTask> Tasks { get; set; } = new List<BaseTask>();
        //preparation phase
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //implement phase
        public DateTime StartDate { get; set; }

        //ending phase
        public DateTime EndDate { get; set; }
    }
}
