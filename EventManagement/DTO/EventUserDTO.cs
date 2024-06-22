namespace EventManagement.DTO
{
    public class EventUserDTO
    {
        public string NickName { get; set; } = string.Empty;
        public List<int> roles { get; set; } = new List<int>();
    }
}
