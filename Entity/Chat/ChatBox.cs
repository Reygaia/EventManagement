using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Chat
{
    public class ChatBox
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        List<ApplicationMessage> messages { get; set; } = new List<ApplicationMessage>();
        public ObjectId PhaseTaskId { get; set; } //either task or phase id
    }
}
