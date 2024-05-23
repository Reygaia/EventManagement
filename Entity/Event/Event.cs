using Entity.Chat;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entity.Event
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string EventName { get; set; }
        public string OwnerId { get; set; }
        public int StaffCount { get; set; } = 0;
        public List<Phase> Phases { get; set; } = new List<Phase>();
        //preparation phase
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        //implement phase
        public DateTime StartDate { get; set; }
        //ending phase
        public DateTime EndDate { get; set; }
    }
}
