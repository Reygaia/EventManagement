using Microsoft.AspNetCore.Authorization;

namespace Authorization
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(MessagePermissions permission) : base(policy: permission.ToString())
        {
        }

        public HasPermissionAttribute(RolePermissions permission) : base(policy: permission.ToString())
        {
        }

        public HasPermissionAttribute(TaskPermissions permission) : base(policy: permission.ToString())
        {
        }
        public HasPermissionAttribute(UserPermissions permission) : base(policy: permission.ToString())
        {
        }
    }
}
