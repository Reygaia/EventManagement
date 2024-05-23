using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entity.Task
{
    public class RegistrationTask : BaseTask
    {
        [BsonConstructor]
        public RegistrationTask()
        {
        }

        public int ExpectedAttendees { get; set; }
        private int registeredAttendees { get; set; } = 0;
        public int RegisteredAttendees
        {
            get => registeredAttendees; set
            {
                registeredAttendees = value;
                IsCompleted = (registeredAttendees == ExpectedAttendees);
            }
        }
        [BsonExtraElements]
        public BsonDocument? RegistrationExtraElements { get; set; }
    }
}
