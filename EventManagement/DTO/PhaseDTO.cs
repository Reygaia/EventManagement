using MongoDB.Bson;

namespace EventManagement.DTO
{
    public class PhaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public ObjectId EventId { get; set; }
    }
}
