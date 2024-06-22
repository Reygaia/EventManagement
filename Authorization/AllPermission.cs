using Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission
{
    public class AllPermission
    {
        public MessagePermissions MessagePermission {  get; set; }
        public RolePermissions RolePermission { get; set; }
        public UserPermissions UserPermission { get; set; }
        public TaskPermissions TaskPermission { get; set; }
    }
}
