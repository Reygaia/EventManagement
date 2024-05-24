using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    [Flags]
    public enum Permission
    {
        None = 0,
        ReadMessages = 1 << 0,  // 1
        SendMessages = 1 << 1,  // 2
        ManageMessages = 1 << 2,  // 4
        ManageChannels = 1 << 3,  // 8
        ManageUsers = 1 << 4, //16
        Admin = 1 << 5  //32
    }
}
