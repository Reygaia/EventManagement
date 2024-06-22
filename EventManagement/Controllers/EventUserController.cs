using DAL;
using Entity.Event;
using EventManagement.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EventManagement.Controllers
{
    [Route("api/appuser")]
    [ApiController]
    public class EventUserController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public EventUserController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPut]
        [Route("editRole/{eventid}/{userid}")]
        //[Authorize(Policy = "EditUserRolePolicy")]
        public async Task<IActionResult> EditRole([FromBody]EventUserDTO input,string eventid, string userid)
        {
            var Event = GetEvent(eventid);
            var finduser = Event.Users.FirstOrDefault(s => s.UserId == Guid.Parse(userid));
            finduser.RolesId = input.roles;
            _unitOfWork.EventRepository.Update(Event);
            return Ok();
        }



        private Event GetEvent(string id)
        {
            var idfinder = ObjectId.Parse(id);
            var result = _unitOfWork.EventRepository.Get(s => s.Id == idfinder).FirstOrDefault();
            return result;
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
