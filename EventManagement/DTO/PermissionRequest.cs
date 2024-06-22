namespace EventManagement.DTO
{
    public class PermissionRequest
    {
        public string Name { get; set; } = string.Empty;

        public bool ToggleReadMessage { get; set; } = false;
        public bool ToggleSendMessage { get; set; } = false;
        public bool ToggleEditMessage { get; set; } = false;
        public bool ToggleDeleteMessage { get; set; } = false;
        public bool ToggleMessageManage {  get; set; } = false;

        public bool ToggleReadTask { get; set; } = false;
        public bool ToggleSendTask { get; set; } = false;
        public bool ToggleEditTask { get; set; } = false;
        public bool ToggleDeleteTask { get; set; } = false;
        public bool ToggleTaskManage { get; set; } = false;

        public bool ToggleCreateRole { get; set; } = false;
        public bool ToggleEditRole { get; set; } = false;
        public bool ToggleDeleteRole { get; set; } = false;
        public bool ToggleRoleManage { get; set; } = false;

        public bool ToggleEditUserRole { get; set; } = false;
        public bool ToggleMuteUser { get; set; } = false;
        public bool ToggleKickUser { get; set; } = false;
        public bool ToggleBanUser { get; set; } = false;
        public bool ToggleUserManage { get; set; } = false;
    }
}
