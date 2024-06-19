using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    [Flags]
    public enum MessagePermissions
    {
        //Message Permissions
        None = 0,
        ReadMessage = 1 << 0,
        SendMessage = 1 << 1,
        EditMessage = 1 << 2,
        DeleteMessage = 1 << 3,
        //combine permissions
        MessageManage = ReadMessage | SendMessage | EditMessage | DeleteMessage,
    }

    [Flags]
    public enum TaskPermissions
    {
        None = 0,
        ReadTask = 1 << 0,
        SendTask = 1 << 1,
        EditTask = 1 << 2,
        DeleteTask = 1 << 3,
        //combine permissions
        TaskManage = ReadTask | SendTask | EditTask | DeleteTask,
    }
    [Flags]
    public enum RolePermissions
    {
        None = 0,
        CreateRole = 1 << 0,
        EditRole = 1 << 1,
        DeleteRole = 1 << 2,
        //combine permissions
        RoleManage = CreateRole | EditRole | DeleteRole,
    }
    [Flags]
    public enum UserPermissions
    {
        None = 0,
        EditUserRole = 1 << 0,
        MuteUser = 1 << 1,
        KickUser = 1 << 2,
        BanUser = 1 << 3,
        //combine permissions
        UserManage = EditUserRole | MuteUser | KickUser | BanUser,
    }
}
