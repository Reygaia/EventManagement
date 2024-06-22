using DAL;
using Entity.Event;
using EventManagement.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EventManagement.Controllers
{
    [Route("api/eventrole")]
    [ApiController]
    public class EventRoleController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public EventRoleController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Route("create/{eventid}")]
        [Authorize]
        public async Task<IActionResult> CreateRole([FromBody] EventRoleDTO input, string eventid)
        {
            if (ModelState.IsValid)
            {
                var role = new EventRole
                {
                    Id = CountId(GetAllRoles(eventid)),
                    Name = input.Name,
                };
                InsertRole(role, eventid);
                return Ok(new
                {
                    response = "Role created"
                });
            }
            return Unauthorized(new
            {
                response = "you are not manager/admin"
            });
        }
        [HttpPut]
        [Route("edit/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditRole(string id, [FromBody] EventRoleDTO input)
        {
            if (ModelState.IsValid)
            {
                var temp = new EventRole
                {
                    Id = 1,
                    Name = input.Name,
                };
                temp.Name = input.Name;
                /*
                 * custom policy and stuff
                 */
                _unitOfWork.EventRoleRepository.Update(temp);
            }
            return Unauthorized(new
            {
                response = "You are not moderator"
            });
        }
        [HttpGet]
        [Route("role/{eventid}/{id}")]
        [Authorize]
        public async Task<IActionResult> GetRoleAsync(string eventid, int id)
        {
            var appEvent = GetEvent(eventid);
            var role = appEvent.Roles.Where(s => s.Id == id).FirstOrDefault();
            return Ok(new
            {
                exRole = role
            });
        }


        [HttpPut]
        [Route("edit/{eventid}/{id}")]
        [Authorize]
        public async Task<IActionResult> EditRole(string eventid, int id, [FromBody] PermissionRequest permissionRequest)
        {
            var appEvent = GetEvent(eventid);
            var role = appEvent.Roles.Where(s => s.Id == id).FirstOrDefault();
            role.Name = permissionRequest.Name;
            PermissionCheck(permissionRequest, role);
            _unitOfWork.EventRepository.Update(appEvent);
            return Ok(new
            {
                exRole = role
            });
        }

        [HttpDelete]
        [Route("delete/{eventid}/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRole(string eventid, int id)
        {
            var appEvent = GetEvent(eventid);
            var role = appEvent.Roles.Where(s => s.Id == id).FirstOrDefault();
            appEvent.Roles.Remove(role);
            _unitOfWork.EventRepository.Update(appEvent);
            return Ok();
        }


        #region non async function
        private static void PermissionCheck(PermissionRequest permissionRequest, EventRole? role)
        {
            // Toggle individual message permissions
            if (permissionRequest.ToggleReadMessage)
            {
                role.ToggleMessagePermission(Authorization.MessagePermissions.ReadMessage);
            }
            if (permissionRequest.ToggleSendMessage)
            {
                role.ToggleMessagePermission(Authorization.MessagePermissions.SendMessage);
            }
            if (permissionRequest.ToggleEditMessage)
            {
                role.ToggleMessagePermission(Authorization.MessagePermissions.EditMessage);
            }
            if (permissionRequest.ToggleDeleteMessage)
            {
                role.ToggleMessagePermission(Authorization.MessagePermissions.DeleteMessage);
            }
            if (permissionRequest.ToggleMessageManage)
            {
                role.ToggleMessagePermission(Authorization.MessagePermissions.MessageManage);
            }

            // Toggle individual task permissions
            if (permissionRequest.ToggleReadTask)
            {
                role.ToggleTaskPermission(Authorization.TaskPermissions.ReadTask);
            }
            if (permissionRequest.ToggleSendTask)
            {
                role.ToggleTaskPermission(Authorization.TaskPermissions.SendTask);
            }
            if (permissionRequest.ToggleEditTask)
            {
                role.ToggleTaskPermission(Authorization.TaskPermissions.EditTask);
            }
            if (permissionRequest.ToggleDeleteTask)
            {
                role.ToggleTaskPermission(Authorization.TaskPermissions.DeleteTask);
            }
            if (permissionRequest.ToggleTaskManage)
            {
                role.ToggleTaskPermission(Authorization.TaskPermissions.TaskManage);
            }

            // Toggle individual role permissions
            if (permissionRequest.ToggleCreateRole)
            {
                role.ToggleRolePermission(Authorization.RolePermissions.CreateRole);
            }
            if (permissionRequest.ToggleEditRole)
            {
                role.ToggleRolePermission(Authorization.RolePermissions.EditRole);
            }
            if (permissionRequest.ToggleDeleteRole)
            {
                role.ToggleRolePermission(Authorization.RolePermissions.DeleteRole);
            }
            if (permissionRequest.ToggleRoleManage)
            {
                role.ToggleRolePermission(Authorization.RolePermissions.RoleManage);
            }

            // Toggle individual user permissions
            if (permissionRequest.ToggleEditUserRole)
            {
                role.ToggleUserPermission(Authorization.UserPermissions.EditUserRole);
            }
            if (permissionRequest.ToggleMuteUser)
            {
                role.ToggleUserPermission(Authorization.UserPermissions.MuteUser);
            }
            if (permissionRequest.ToggleKickUser)
            {
                role.ToggleUserPermission(Authorization.UserPermissions.KickUser);
            }
            if (permissionRequest.ToggleBanUser)
            {
                role.ToggleUserPermission(Authorization.UserPermissions.BanUser);
            }
            if (permissionRequest.ToggleUserManage)
            {
                role.ToggleUserPermission(Authorization.UserPermissions.UserManage);
            }
        }

        public int CountId(IEnumerable<EventRole> roles)
        {
            if (!roles.Any()) return 0;
            else return roles.Count();
        }

        public IEnumerable<EventRole> GetAllRoles(string eventid)
        {
            Event appEvent = GetEvent(eventid);
            return appEvent.Roles;
        }

        private Event GetEvent(string eventid)
        {
            var finder = ObjectId.Parse(eventid);
            var appEvent = _unitOfWork.EventRepository.Get(s => s.Id == finder).FirstOrDefault();
            return appEvent;
        }

        public void InsertRole(EventRole role, string eventid)
        {
            var appEvent = GetEvent(eventid);
            appEvent.Roles.Add(role);
            _unitOfWork.EventRepository.Update(appEvent);
        }
        #endregion
    }
}
