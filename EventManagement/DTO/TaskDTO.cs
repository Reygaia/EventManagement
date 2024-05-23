using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace EventManagement.DTO
{
    public enum TaskType
    {
        ContentCreation,
        Logistics,
        Marketing,
        Planning,
        Quota,
        Registration,
        VendorCoordination
    }


    public class TaskDTO
    {
        public string Description { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TaskType Type { get; set; }
        public DateTime Deadline { get; set; } = DateTime.Now + TimeSpan.FromDays(1);
        public string? EventId { get; set; }
        public string? PhaseId { get; set; }
        public string? CreatedBy { get; set; }

        //Content creation
        public string? ContentType { get; set; } = string.Empty;
        public string? AssignedTo { get; set; } = string.Empty;
        //logistics
        public string? TransportationDetails { get; set; } = string.Empty;
        //marketing
        public string? CampaignType { get; set; } = string.Empty;
        //planning
        public string? Venue { get; set; } = string.Empty;
        //quota
        public int? MaxCount { get; set; } = 0;
        //registration
        public int? ExpectedAttendees { get; set; } = 0;
        //vendor
        public string VendorName { get; set; } = string.Empty;
        public string VendorService { get; set; } = string.Empty;


    }
}
