using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace Entity.Task
{
    public class ContentCreationTask : BaseTask
    {
        [JsonConstructor]
        public ContentCreationTask()
        {
        }


        public string ContentType { get; set; }
        public string AssignedTo { get; set; }
        [BsonExtraElements]
        public BsonDocument? ContentExtraElements { get; set; }
    }
}
