using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Event
{
    public class EventRole : Enumeration<EventRole>
    {
        public EventRole(int value, string name) : base(value, name)
        {
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Name { get; set; } = "New Role";
        public ObjectId eventId { get; set; }
        //permission
        //level of hierachy
        //policy
    }
}
