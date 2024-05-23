using Entity.Task;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Chat
{
    public class Phase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<BaseTask> Tasks { get; set; } = new List<BaseTask>();
        public ObjectId EventId { get; set; }
        [BsonConstructor]
        public Phase() { }
    }
}
