using DAL;
using Entity.Event;
using EventManagement.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
        [Route("create")]
        [Authorize(Policy = "SendMessagePolicy")]
        public async Task<IActionResult> CreateEvent([FromBody] EventDTO input)
        {
            string userid = GetUserId();

            if (ModelState.IsValid)
            {
                Event temp = CreateEvent(input, userid);
                temp.Users.Add(GetEventUser(userid));
                _unitOfWork.EventRepository.Insert(temp);
            }
            return Ok(new { message = "Event created with some tasks" });
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            var result = GetEvent(id);
            if (result.OwnerId == GetUserId())
            {
                _unitOfWork.EventRepository.Delete(result.Id);
                TaskCleanUp(result);
                return Ok(new
                {
                    response = "Event is deleted"
                });
            }
            return Unauthorized(new
            {
                response = "You are not owner",
            });
        }

        [HttpPut]
        [Route("edit/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditEvent(string id, [FromBody] EventDTO input)
        {
            var result = GetEvent(id);
            if (result.OwnerId == GetUserId())
            {
                UpdateEvent(input, result);
                return Ok(new
                {
                    response = "Event is editted"
                });
            }
            return Unauthorized(new
            {
                response = "You are not owner",
            });
        }

        [HttpPut]
        [Route("edit/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ChangeOwner(string id, [FromBody]string ownerId)
        {
            var result = GetEvent(id);
            
            if(result.OwnerId == GetUserId())
            {
                var newId = Guid.Parse(ownerId);
                result.OwnerId = ownerId;
                _unitOfWork.EventRepository.Update(result);
                var name = _unitOfWork.UserRepository.Get(s => s.Id == newId).FirstOrDefault().UserName;
                return Ok(new
                {
                    response = "now the owner is "+ name, 
                });
            }
            return Unauthorized(new
            {
                response = "You are not owner"
            });
        }

        [HttpDelete]
        [Route("leave/{eventid}")]
        [Authorize]
        public async Task<IActionResult> LeaveEvent(string eventid)
        {
            var UserId = GetUserId();
            var Event = GetEvent(eventid);
            Event.Users.Remove(GetEventUser(UserId));
            _unitOfWork.EventRepository.Update(Event);
            return Ok();
        }




        private void UpdateEvent(EventDTO input, Event result)
        {
            result.EventName = input.EventName;
            result.EndDate = input.EndDate;
            result.StartDate = input.StartDate;
            result.StaffCount = input.StaffCount;
            _unitOfWork.EventRepository.Update(result);
        }

        private void TaskCleanUp(Event result)
        {
            
        }

        private Event CreateEvent(EventDTO input, string userid)
        {
            return new Event
            {
                OwnerId = userid,
                EventName = input.EventName,
                StaffCount = input.StaffCount,
                CreatedDate = input.CreatedDate,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                Roles = input.Roles
            };
        }

        private Event GetEvent(string id)
        {
            var idfinder = ObjectId.Parse(id);
            var result = _unitOfWork.EventRepository.Get(s => s.Id == idfinder).FirstOrDefault();
            return result;
        }

        private string GetUserId()
        {
            var user = _HttpContextAccessor.HttpContext.User;
            string userid = user.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            return userid;
        }

        private EventUser GetEventUser(string userId)
        {
            var idFinder = Guid.Parse(userId);
            var user = _unitOfWork.UserRepository.Get(s => s.Id == idFinder).FirstOrDefault();
            var returnUser = new EventUser
            {
                UserId = user.Id,
                NickName = user.UserName,
            };
            return returnUser;
        }

        
    }
}
