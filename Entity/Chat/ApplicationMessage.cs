using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Chat
{
    public class ApplicationMessage
    {
        [BsonConstructor]
        public ApplicationMessage()
        {
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime MessageCreated { get; set; }
        public bool IsEdited { get; set; } = false;
        public ObjectId MessageBy { get; set; }
    }
}
