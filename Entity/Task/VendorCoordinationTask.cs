using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Entity.Task
{
    public class VendorCoordinationTask : BaseTask
    {
        [BsonConstructor]
        public VendorCoordinationTask()
        {
        }

        public string VendorName { get; set; }
        public string VendorService { get; set; }
        [BsonExtraElements]
        public BsonDocument? VendorExtraElements { get; set; }
    }
}
