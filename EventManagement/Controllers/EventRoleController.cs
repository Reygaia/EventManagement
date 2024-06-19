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
        [Route("create")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateRole([FromBody] EventRoleDTO input)
        {
            if (ModelState.IsValid)
            {
                var temp = new EventRole(1, "cc");
                return Ok(new
                {
                    response = "Role created "
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
        public async Task<IActionResult> EditRole(string id, [FromBody]EventRoleDTO input)
        {
            if (ModelState.IsValid)
            {
                var temp = new EventRole(1,"cc");
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

    }
}
