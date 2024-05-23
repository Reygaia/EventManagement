using MongoDB.Bson;

namespace EventManagement.DTO
{
    public class EventRoleDTO
    {
        public string Name { get; set; } = "New Role";
        public string eventId { get; set; } = string.Empty;
    }
}
