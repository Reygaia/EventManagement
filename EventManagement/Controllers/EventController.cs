using DAL;
using Entity;
using EventManagement.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventManagement.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly UnitOfWork _unitOfWork;

        public EventController(IHttpContextAccessor httpContextAccessor, UnitOfWork unitOfWork)
        {
            _HttpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateEvents([FromBody] EventDTO input)
        {
            var user = _HttpContextAccessor.HttpContext.User;

            if (!user.IsInRole("User"))
            {
                return BadRequest("User is not in role");
            }
            else
            {
                string userid = user.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();

                if (ModelState.IsValid)
                {
                    Event temp = new Event
                    {
                        OwnerId = userid,
                        StaffCount = input.StaffCount,
                        Tasks = input.Tasks,
                        CreatedDate = input.CreatedDate,
                        StartDate = input.StartDate,
                        EndDate = input.EndDate,
                    };
                    _unitOfWork.EventRepository.Insert(temp);
                }
            }
            return Ok(new { message = "Event created with some tasks" });
        }
    }
}
