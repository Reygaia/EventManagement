using Entity.Task;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.DTO
{
    public class EventDTO
    {
        [Required]
        public string EventName { get; set; }
        public int StaffCount { get; set; } = 0;
        public List<BaseTask> Tasks { get; set; } = new List<BaseTask>();
        //preparation phase
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //implement phase
        public DateTime StartDate { get; set; }

        //ending phase
        public DateTime EndDate { get; set; }
    }
}
