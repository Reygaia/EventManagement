using Authorization;
using Microsoft.AspNetCore.Authorization;

namespace OptionsSetup.Policy
{
    public static class AuthorizationPolicies
    {
        public static void AddAuthorizationPolicies(AuthorizationOptions options)
        {
            // MessagePermissions policies
            options.AddPolicy("ReadMessagePolicy", policy =>
                policy.RequireClaim("permissions", MessagePermissions.ReadMessage.ToString()));

            options.AddPolicy("SendMessagePolicy", policy =>
                policy.RequireClaim("permissions", MessagePermissions.SendMessage.ToString()));

            options.AddPolicy("EditMessagePolicy", policy =>
                policy.RequireClaim("permissions", MessagePermissions.EditMessage.ToString()));

            options.AddPolicy("DeleteMessagePolicy", policy =>
                policy.RequireClaim("permissions", MessagePermissions.DeleteMessage.ToString()));

            options.AddPolicy("MessageManagePolicy", policy =>
                policy.RequireClaim("permissions", MessagePermissions.MessageManage.ToString()));

            // TaskPermissions policies
            options.AddPolicy("ReadTaskPolicy", policy =>
                policy.RequireClaim("permissions", TaskPermissions.ReadTask.ToString()));

            options.AddPolicy("SendTaskPolicy", policy =>
                policy.RequireClaim("permissions", TaskPermissions.SendTask.ToString()));

            options.AddPolicy("EditTaskPolicy", policy =>
                policy.RequireClaim("permissions", TaskPermissions.EditTask.ToString()));

            options.AddPolicy("DeleteTaskPolicy", policy =>
                policy.RequireClaim("permissions", TaskPermissions.DeleteTask.ToString()));

            options.AddPolicy("TaskManagePolicy", policy =>
                policy.RequireClaim("permissions", TaskPermissions.TaskManage.ToString()));

            // RolePermissions policies
            options.AddPolicy("CreateRolePolicy", policy =>
                policy.RequireClaim("permissions", RolePermissions.CreateRole.ToString()));

            options.AddPolicy("EditRolePolicy", policy =>
                policy.RequireClaim("permissions", RolePermissions.EditRole.ToString()));

            options.AddPolicy("DeleteRolePolicy", policy =>
                policy.RequireClaim("permissions", RolePermissions.DeleteRole.ToString()));

            options.AddPolicy("RoleManagePolicy", policy =>
                policy.RequireClaim("permissions", RolePermissions.RoleManage.ToString()));

            // UserPermissions policies
            options.AddPolicy("EditUserRolePolicy", policy =>
                policy.RequireClaim("permissions", UserPermissions.EditUserRole.ToString()));

            options.AddPolicy("MuteUserPolicy", policy =>
                policy.RequireClaim("permissions", UserPermissions.MuteUser.ToString()));

            options.AddPolicy("KickUserPolicy", policy =>
                policy.RequireClaim("permissions", UserPermissions.KickUser.ToString()));

            options.AddPolicy("BanUserPolicy", policy =>
                policy.RequireClaim("permissions", UserPermissions.BanUser.ToString()));

            options.AddPolicy("UserManagePolicy", policy =>
                policy.RequireClaim("permissions", UserPermissions.UserManage.ToString()));
        }
    }
}
