using Authorization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Event
{
    public class EventRole
    {
        [BsonId]
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "New Role";
        public List<Guid>? userIds { get; set; } = new List<Guid>();
        //permission
        public MessagePermissions MessagePermissions { get; set; } = MessagePermissions.None;
        public TaskPermissions TaskPermissions { get; set; } = TaskPermissions.None;
        public RolePermissions RolePermissions { get; set; } = RolePermissions.None;
        public UserPermissions UserPermissions { get; set; } = UserPermissions.None;

        // Toggle individual permissions methods
        public void ToggleMessagePermission(MessagePermissions permission)
        {
            MessagePermissions ^= permission;
        }

        public void ToggleTaskPermission(TaskPermissions permission)
        {
            TaskPermissions ^= permission;
        }

        public void ToggleRolePermission(RolePermissions permission)
        {
            RolePermissions ^= permission;
        }
         
        public void ToggleUserPermission(UserPermissions permission)
        {
            UserPermissions ^= permission;
        }
        //level of hierachy
    }
}
